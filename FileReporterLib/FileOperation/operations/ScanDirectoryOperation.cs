using FileReporterLib.Filter.DateFilter;
using System.Diagnostics;

namespace FileReporterDecorator.FileOperation.operations
{
    public class ScanDirectoryOperation : FileOperation
    {

        private readonly string _destinationPath;
        private readonly int _totalFileCount;
        private readonly int _threadCount;
        private readonly FileOperation _fileOperation;
        private readonly Action<int, string> _showOnScreenCallback;
        private readonly Action<int, TimeSpan> _showOnScreenCallbackMaximize;
        private readonly Action<List<string>> _saveDialogUnAccessAuthorizeCallback;
        private readonly IDateOption _dateOption;
        private readonly DateTime _dateTime;

        public ScanDirectoryOperation(
            FileOperation fileOperation, DateTime dateTime,
            int totalFileCount, int threadCount,
            string targetPath, IDateOption dateOption,
            Action<int, string> showOnScreenCallback,
            Action<int, TimeSpan> showOnScreenCallbackMaximize,
            Action<List<string>> saveDialogUnAccessAuthorizeCallback)
        {
            _saveDialogUnAccessAuthorizeCallback = saveDialogUnAccessAuthorizeCallback;
            _dateTime = dateTime;
            _totalFileCount = totalFileCount;
            _threadCount = threadCount;
            _showOnScreenCallback = showOnScreenCallback;
            _destinationPath = targetPath;
            _showOnScreenCallbackMaximize = showOnScreenCallbackMaximize;
            _dateOption = dateOption;
            _fileOperation = fileOperation;
        }


        /*
         * 
         * Scan files (include subfolders) and classify it recursively.
         * 
         */
        public void ScanFileSystemAndClassifyFiles(string path)
        {
            foreach (string file in Directory.GetFiles(path))
            {
                ClassifyBySelectedDate(_dateOption, file);

                lock (_locker)
                    _locker.COUNTER++;
            }

            Parallel.ForEach(Directory.GetDirectories(path), new ParallelOptions { MaxDegreeOfParallelism = _threadCount }, subDir =>
            {
                var name = new DirectoryInfo(subDir);

                try
                {
                    if (Directory.EnumerateFileSystemEntries(name.FullName).Any() && _dateOption.SetDate(new DirectoryInfo(path), _dateTime))
                        AddDirectoryList(name.FullName);

                    else AddEmptyDirectoryList(name.FullName);

                    if (String.IsNullOrWhiteSpace(name.Name) || name.Name is null)
                        return;

                    ScanFileSystemAndClassifyFiles(subDir);
                }
                catch (UnauthorizedAccessException ex)
                {
                    AddUnAccessDirectoryList(name.FullName);
                }
            });
        }




        /*
         * 
         * Classify the files (date) and add files to list.
         * 
         */
        private void ClassifyBySelectedDate(IDateOption dateOption, string file)
        {
            if (_locker.COUNTER % 1000 == 0)
                _showOnScreenCallback(_locker.COUNTER, file);

            if (dateOption.SetDate(new FileInfo(file), _dateTime))
                AddNewFileList(file);

            else AddOldFileList(file);
        }

        /*
         * 
         * 
         * Clear all lists on decorator abstract class. 
         * 
         */
        private void ClearAllList()
        {
            GetNewFileList().Clear();
            GetOldFileList().Clear();
            GetDirectoryList().Clear();
            GetUnAccessFolderDirectoryList().Clear();
        }



        /*
         * 
         * 
         * Trigger method for Scan Operation 
         * 
         */
        public async override Task Run()
        {
            ClearAllList();
         
            _locker.COUNTER = 0;

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(() => ScanFileSystemAndClassifyFiles(_destinationPath));

            stopWatch.Stop();

            _saveDialogUnAccessAuthorizeCallback.Invoke(GetUnAccessFolderDirectoryList().ToList()); // Save unauthorize files to selected directory

            _showOnScreenCallbackMaximize(_locker.COUNTER, stopWatch.Elapsed);

            if (_fileOperation != null && _fileOperation.GetType().Name != "EmptyOperation")
                await _fileOperation.Run();
        }
    }
}

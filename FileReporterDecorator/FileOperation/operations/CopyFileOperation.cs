using DocumentFormat.OpenXml.Wordprocessing;
using FileReporterDecorator.Util;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace FileReporterDecorator.FileOperation.operations
{
    internal class CopyFileOperation : FileOperation
    {
        private readonly FileOperation scanProcess;
        private readonly int totalFileCount;
        private readonly int threadCount;
        private readonly string destinationPath;
        private readonly string targetPath;
        private readonly Action<int, int, string> showOnScreenCallback;
        private readonly Action minimumProgressBar;
        private readonly Action<string> setTimeLabelAction;
        private COUNTER_LOCK _newLocker = new COUNTER_LOCK();
        Action<int, TimeSpan> _showOnScreenCallbackMaximize;

        public CopyFileOperation(FileOperation scanProcess, int totalFileCount, int threadCount, string destinationPath,
            string targetPath, Action<int, int, string> showOnScreenCallback, Action minimumProgressBar,
            Action<int, TimeSpan> showMaxProgressBar,
            Action<string> setTimeLabelAction)
        {
            _showOnScreenCallbackMaximize = showMaxProgressBar;
            this.scanProcess = scanProcess;
            this.totalFileCount = totalFileCount;
            this.threadCount = threadCount;
            this.destinationPath = destinationPath;
            this.targetPath = targetPath;
            this.showOnScreenCallback = showOnScreenCallback;
            this.minimumProgressBar = minimumProgressBar;
            this.setTimeLabelAction = setTimeLabelAction;
        }

        private void CopyFileCallback()
        {
            var isConflict = false;
            var conflictFileList = new ConcurrentBag<string>();
            

            Parallel.ForEach(scanProcess.GetNewFileList(), new ParallelOptions { MaxDegreeOfParallelism = threadCount }, file =>
            {
                lock (_newLocker)
                {
                    _newLocker.COUNTER++;
                }
                showOnScreenCallback(_newLocker.COUNTER, totalFileCount, file);

                var targetFile = file.Replace(destinationPath, targetPath);

                try
                {
                    File.Copy(file, targetFile, IsOwerrite());

                    if (IsCopyNtfsPermissions())
                        _copyNtfsPermissions.Invoke(new FileInfo(file), new FileInfo(file.Replace(destinationPath, targetPath)));
                }
                catch (IOException ex)
                {
                    isConflict = true;
                    conflictFileList.Add(file);
                }
            });
          
            if (IsEmptyFolder())
                Parallel.ForEach(scanProcess.GetEmptyDirectoryList(), new ParallelOptions { MaxDegreeOfParallelism = threadCount }, dir =>
                {
                    try
                    {
                        Directory.CreateDirectory(dir.Replace(destinationPath, targetPath));
                    }
                    catch (Exception ex) { }
                });

            if (isConflict)
                MessageBox.Show($"{conflictFileList.Count} files are conflicted!", "Conflicted Files",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

        }
        public override async Task Run()
        {
            scanProcess.GetDirectoryList().ToList().ForEach(d => Directory.CreateDirectory(d.Replace(destinationPath, targetPath)));

            minimumProgressBar.Invoke();

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(() => CopyFileCallback());

            stopWatch.Stop();

            _showOnScreenCallbackMaximize(_locker.COUNTER, stopWatch.Elapsed);
            setTimeLabelAction.Invoke("Copy Operation was completed! Total Elapsed Time: " + stopWatch.Elapsed);
        }
    }
}

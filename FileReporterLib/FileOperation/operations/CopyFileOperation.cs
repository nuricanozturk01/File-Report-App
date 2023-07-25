using FileReporterDecorator.Util;
using System.Diagnostics;
using static FileReporterDecorator.Util.ExceptionUtil;
using static FileReporterDecorator.Util.ParallelWrapper;
namespace FileReporterDecorator.FileOperation.operations
{
    public class CopyFileOperation : FileOperation
    {
        private readonly int _threadCount;
        private readonly string _destinationPath;
        private readonly string _targetPath;
        private readonly Action<int, string> _showOnScreenCallback;
        private readonly Action<string> _setTimeLabelAction;
        private readonly Action<string> _errorLabelTextCallback;
        private readonly Action<int, TimeSpan> _showOnScreenCallbackMaximize;
        private readonly Action _StartOperationCallback;
        private readonly FileOperation _scanProcess;

        private COUNTER_LOCK _newLocker = new COUNTER_LOCK();







        public CopyFileOperation(FileOperation scanProcess, int threadCount, string destinationPath, string targetPath,
            Action<int, string> showOnScreenCallback, Action<int, TimeSpan> showMaxProgressBar,
            Action<string> setTimeLabelAction, Action<string> errorLabelTextCallback, Action StartOperationCallback)
        {
            _StartOperationCallback = StartOperationCallback;
            _showOnScreenCallbackMaximize = showMaxProgressBar;
            _scanProcess = scanProcess;
            _threadCount = threadCount;
            _destinationPath = destinationPath;
            _targetPath = targetPath;
            _showOnScreenCallback = showOnScreenCallback;
            _setTimeLabelAction = setTimeLabelAction;
            _errorLabelTextCallback = errorLabelTextCallback;
        }









        /*
         *
         * Copy files to destination path. Also this method decide the copy empty folders
         * 
         */
        private void CopyFileCallback()
        {
            if (_scanProcess.IsEmptyFolder())
                ForEachParallel(_scanProcess.GetEmptyDirectoryList(), _threadCount,
                    dir => Directory.CreateDirectory(dir.Replace(_destinationPath, _targetPath)));


            ForEachParallel(_scanProcess.GetNewFileList(), _threadCount,
                file => ThrowUnAuthorizedException(
                () => CopyFiles(file),
                () => _errorLabelTextCallback.Invoke("Copied files if necessary permissions valid!")));
        }








        /*
         *
         * Callback for CopyFileCallback metod. In here ntfs permissions copied.
         * 
         */
        private void CopyFiles(string file)
        {
            lock (_newLocker)
                _newLocker.COUNTER++;

            _showOnScreenCallback(_newLocker.COUNTER, file);

            var targetFile = file.Replace(_destinationPath, _targetPath);

            ThrowCopyConflictException(
                () => File.Copy(file, targetFile, _scanProcess.IsOwerrite()),
                () => _errorLabelTextCallback.Invoke("Files Are Conflicted! Non Conflicted Files Are Copied!"));


            if (_scanProcess.IsCopyNtfsPermissions())
                _scanProcess.GetNtfsPermissionAction().Invoke(new FileInfo(file), new FileInfo(file.Replace(_destinationPath, _targetPath)));
        }







        /*
         *
         * Trigger method for CopyFileOperation.
         * 
         */
        public override async Task Run()
        {
            _scanProcess.GetDirectoryList().ToList().ForEach(d => Directory.CreateDirectory(d.Replace(_destinationPath, _targetPath)));

            var stopWatch = new Stopwatch();

            _StartOperationCallback.Invoke();
            
            stopWatch.Start();

            await Task.Run(() => CopyFileCallback());

            stopWatch.Stop();

            _showOnScreenCallbackMaximize(_locker.COUNTER, stopWatch.Elapsed);

            _setTimeLabelAction.Invoke("Copy Operation was completed! Total Elapsed Time: " + stopWatch.Elapsed);
        }
    }
}

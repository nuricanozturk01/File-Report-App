using FileReporterDecorator.Util;
using FileReporterLib.Util;
using System.Diagnostics;
using System.Security.AccessControl;
using static FileReporterDecorator.Util.ExceptionUtil;
using static FileReporterDecorator.Util.ParallelWrapper;
namespace FileReporterDecorator.FileOperation.operations
{
    public class CopyFileOperation : FileOperation
    {
        private readonly int totalFileCount;
        private readonly int threadCount;
        private readonly string destinationPath;
        private readonly string targetPath;
        private readonly Action<int, int, string> showOnScreenCallback;
        private readonly Action minimumProgressBar;
        private readonly Action<string> setTimeLabelAction;
        private readonly Action<string> errorLabelTextCallback;
        private readonly Action<int, TimeSpan> _showOnScreenCallbackMaximize;

        private readonly FileOperation scanProcess;
        private COUNTER_LOCK _newLocker = new COUNTER_LOCK();
        
        public CopyFileOperation(FileOperation scanProcess, int totalFileCount, int threadCount,
            string destinationPath, string targetPath,
            Action<int, int, string> showOnScreenCallback,
            Action minimumProgressBar,
            Action<int, TimeSpan> showMaxProgressBar,
            Action<string> setTimeLabelAction,
            Action<string> errorLabelTextCallback)
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
            this.errorLabelTextCallback = errorLabelTextCallback;
        }

        /*
         *
         * Copy files to destination path. Also this method decide the copy empty folders
         * 
         */
        private void CopyFileCallback()
        {
            if (scanProcess.IsEmptyFolder())
                ForEachParallel(scanProcess.GetEmptyDirectoryList(), threadCount,
                    dir => Directory.CreateDirectory(dir.Replace(destinationPath, targetPath)));

         
            ForEachParallel(scanProcess.GetNewFileList(), threadCount, 
                file => ThrowUnAuthorizedException(
                () => CopyFiles(file), 
                () => errorLabelTextCallback.Invoke("Copied files if necessary permissions valid!")));
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

            showOnScreenCallback(_newLocker.COUNTER, totalFileCount, file);

            var targetFile = file.Replace(destinationPath, targetPath);

            ThrowCopyConflictException(
                () => File.Copy(file, targetFile, scanProcess.IsOwerrite()),
                () => errorLabelTextCallback.Invoke("Files Are Conflicted! Non Conflicted Files Are Copied!"));


            if (scanProcess.IsCopyNtfsPermissions())
                scanProcess.GetNtfsPermissionAction().Invoke(new FileInfo(file), new FileInfo(file.Replace(destinationPath, targetPath)));
        }


        /*
         *
         * Trigger method for CopyFileOperation.
         * 
         */
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

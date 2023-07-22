using FileReporterDecorator.Util;
using System.Diagnostics;
using static FileReporterDecorator.Util.ExceptionUtil;
using static FileReporterDecorator.Util.ParallelWrapper;
namespace FileReporterDecorator.FileOperation.operations
{
    public class CopyFileOperation : FileOperation
    {
        private readonly FileOperation scanProcess;
        private readonly int totalFileCount;
        private readonly int threadCount;
        private readonly string destinationPath;
        private readonly string targetPath;
        private readonly Action<int, int, string> showOnScreenCallback;
        private readonly Action minimumProgressBar;
        private readonly Action<string> setTimeLabelAction;
        private readonly Action<string> errorLabelTextCallback;
        private COUNTER_LOCK _newLocker = new COUNTER_LOCK();
        Action<int, TimeSpan> _showOnScreenCallbackMaximize;

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

        private void CopyFileCallback()
        {
            ForEachParallel(scanProcess.GetNewFileList(), threadCount, file => CopyFiles(file));

            if (scanProcess.IsEmptyFolder())
                ForEachParallel(scanProcess.GetEmptyDirectoryList(), threadCount,
                    dir => Directory.CreateDirectory(dir.Replace(destinationPath, targetPath)));
        }

        private void CopyFiles(string file)
        {

            lock (_newLocker)
                _newLocker.COUNTER++;

            showOnScreenCallback(_newLocker.COUNTER, totalFileCount, file);

            var targetFile = file.Replace(destinationPath, targetPath);

            ThrowCopyConflictException(() => File.Copy(file, targetFile, scanProcess.IsOwerrite()),
                () => errorLabelTextCallback.Invoke("Files Are Conflicted! Non Conflicted Files Are Copied!"));


            if (scanProcess.IsCopyNtfsPermissions())
                scanProcess.GetNtfsPermissionAction().Invoke(new FileInfo(file), new FileInfo(file.Replace(destinationPath, targetPath)));
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

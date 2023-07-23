using FileReporterDecorator.Util;
using System.Collections.Concurrent;
using System.Diagnostics;
using static FileReporterDecorator.Util.ExceptionUtil;
using static FileReporterDecorator.Util.ParallelWrapper;

namespace FileReporterDecorator.FileOperation.operations
{
    public class MoveFileOperation : FileOperation
    {
        private readonly FileOperation _scanProcess;
        private readonly int totalFileCount;
        private readonly int threadCount;
        private readonly string destinationPath;
        private readonly string targetPath;
        private readonly Action<int, int, string> showOnScreenCallback;
        private readonly Action minimumProgressBar;
        private readonly Action<string> setTimeLabelAction;

        private Action<int, TimeSpan> _showOnScreenCallbackMaximize;
        private readonly Action<string> errorLabelTextCallback;
        private COUNTER_LOCK _moveCounter = new COUNTER_LOCK();

        public MoveFileOperation(FileOperation scanProcess, int totalFileCount, int threadCount, string destinationPath,
            string targetPath, Action<int, int, string> showOnScreenCallback, Action minimumProgressBar,
            Action<string> setTimeLabelAction, Action<int, TimeSpan> showMaxProgressBar, Action<string> _errorLabelTextCallback)
        {
            _showOnScreenCallbackMaximize = showMaxProgressBar;
            errorLabelTextCallback = _errorLabelTextCallback;
            _scanProcess = scanProcess;
            this.totalFileCount = totalFileCount;
            this.threadCount = threadCount;
            this.destinationPath = destinationPath;
            this.targetPath = targetPath;
            this.showOnScreenCallback = showOnScreenCallback;
            this.minimumProgressBar = minimumProgressBar;
            this.setTimeLabelAction = setTimeLabelAction;
        }

        private void MoveFile(string file)
        {   
            showOnScreenCallback(_moveCounter.COUNTER, totalFileCount, file);

            ThrowCopyConflictException(
                () => File.Move(file, file.Replace(destinationPath, targetPath), _scanProcess.IsOwerrite()),
                () => errorLabelTextCallback.Invoke("Files Are Conflicted! Non Conflicted Files Are Moved!"));
            
            lock (_moveCounter)
                _moveCounter.COUNTER++;
        }
        private void MoveFileCallback()
        {
            ForEachParallel(_scanProcess.GetNewFileList(), threadCount, file => ThrowCopyAndMoveException(() => MoveFile(file), () => { }));

            if (_scanProcess.IsEmptyFolder())
                ForEachParallel(_scanProcess.GetEmptyDirectoryList(), threadCount,
                    dir => ThrowCopyAndMoveException(() =>
                    {
                        if (!Directory.Exists(dir.Replace(destinationPath, targetPath)))
                            Directory.Move(dir, dir.Replace(destinationPath, targetPath));
                        Directory.Delete(dir, true);  
                    }, () => { }));

            var dirList = _scanProcess.GetDirectoryList().Select(d => new DirectoryInfo(d)).ToList();
            ForEachParallel(dirList, threadCount, dir => ThrowCopyAndMoveException(() => RemoveDirectory(dir), () => { }));
        }



        private void RemoveDirectory(DirectoryInfo dir)
        {
            if (dir.GetFiles().Length == 0)
                dir.Delete(true);
        }

        public override async Task Run()
        {
            _scanProcess.GetDirectoryList().ToList().ForEach(d => Directory.CreateDirectory(d.Replace(destinationPath, targetPath)));

            minimumProgressBar.Invoke();

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(MoveFileCallback);

            stopWatch.Stop();

            _showOnScreenCallbackMaximize(_moveCounter.COUNTER, stopWatch.Elapsed);

            setTimeLabelAction.Invoke("Copy Operation was completed! Total Elapsed Time: " + stopWatch.Elapsed);
        }
    }
}

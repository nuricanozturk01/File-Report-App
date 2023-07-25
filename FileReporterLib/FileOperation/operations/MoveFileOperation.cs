﻿using FileReporterDecorator.Util;
using System.Diagnostics;
using static FileReporterDecorator.Util.ExceptionUtil;
using static FileReporterDecorator.Util.ParallelWrapper;

namespace FileReporterDecorator.FileOperation.operations
{
    public class MoveFileOperation : FileOperation
    {
        private readonly FileOperation _scanProcess;

        private readonly int _threadCount;
        private readonly string _destinationPath;
        private readonly string _targetPath;
        private readonly Action<int, string> _showOnScreenCallback;
        private readonly Action<string> _setTimeLabelAction;
        private readonly Action<int, TimeSpan> _showOnScreenCallbackMaximize;
        private readonly Action<string> _errorLabelTextCallback;

        private COUNTER_LOCK _moveCounter = new COUNTER_LOCK();

        public MoveFileOperation(FileOperation scanProcess, int threadCount, string destinationPath,
                                 string targetPath, Action<int, string> showOnScreenCallback,
                                Action<string> setTimeLabelAction, Action<int, TimeSpan> showMaxProgressBar,
                                Action<string> errorLabelTextCallback)
        {
            _showOnScreenCallbackMaximize = showMaxProgressBar;
            _errorLabelTextCallback = errorLabelTextCallback;
            _scanProcess = scanProcess;
            _threadCount = threadCount;
            _destinationPath = destinationPath;
            _targetPath = targetPath;
            _showOnScreenCallback = showOnScreenCallback;
            _setTimeLabelAction = setTimeLabelAction;
        }


        /*
         * 
         * Callback for MoveFileCallback. 
         * This method Move file to target path and show on screen which file moved.
         * 
         */
        private void MoveFile(string file)
        {
            _showOnScreenCallback(_moveCounter.COUNTER, file);

            ThrowCopyConflictException(
                () => File.Move(file, file.Replace(_destinationPath, _targetPath), _scanProcess.IsOwerrite()),
                () => _errorLabelTextCallback.Invoke("Files Are Conflicted! Non Conflicted Files Are Moved!"));

            lock (_moveCounter)
                _moveCounter.COUNTER++;
        }

        /*
         * 
         * This method move files also, if user selected the Empty Folder option,
         * Move Empty folders to target path and removed from destination path.
         * 
         */
        private void MoveFileCallback()
        {
            ForEachParallel(_scanProcess.GetNewFileList(), _threadCount, file => ThrowCopyAndMoveException(() => MoveFile(file), () => { }));

            if (_scanProcess.IsEmptyFolder())
                ForEachParallel(_scanProcess.GetEmptyDirectoryList(), _threadCount,
                    dir => ThrowCopyAndMoveException(() =>
                    {
                        Directory.CreateDirectory(dir.Replace(_destinationPath, _targetPath));
                        Directory.Delete(dir, true);
                    }, () => { }));

            //ForEachParallel(_scanProcess.GetEmptyDirectoryList(), _threadCount, dir => Directory.Delete(dir, true));

            var dirList = _scanProcess.GetDirectoryList().Select(d => new DirectoryInfo(d)).ToList();

            ForEachParallel(dirList, _threadCount, dir => ThrowCopyAndMoveException(() => RemoveDirectory(dir), () => { }));
        }


        /*
         *
         * Remove Directory if directory is empty.
         */
        private void RemoveDirectory(DirectoryInfo dir)
        {
            if (dir.GetFiles().Length == 0)
                dir.Delete(true);
        }


        /*
         * 
         * Trigger method for Move Operation. 
         * 
         */
        public override async Task Run()
        {
            _scanProcess.GetDirectoryList().ToList().ForEach(d => Directory.CreateDirectory(d.Replace(_destinationPath, _targetPath)));

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(MoveFileCallback);

            stopWatch.Stop();

            _showOnScreenCallbackMaximize(_moveCounter.COUNTER, stopWatch.Elapsed);

            _setTimeLabelAction.Invoke("Copy Operation was completed! Total Elapsed Time: " + stopWatch.Elapsed);
        }
    }
}

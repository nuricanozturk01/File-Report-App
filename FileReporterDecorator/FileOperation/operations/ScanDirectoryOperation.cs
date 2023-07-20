﻿using FileReporterDecorator.ServiceApp.filter.DateFilter;
using System.Diagnostics;
namespace FileReporterDecorator.FileOperation.operations
{
    internal class ScanDirectoryOperation : FileOperation
    {

        private readonly string _destinationPath;
        private readonly int _totalFileCount;
        private readonly int _threadCount;
        private readonly FileOperation _fileOperation;
        private readonly Action<int, int, string> _showOnScreenCallback;
        private readonly Action<int, TimeSpan> _showOnScreenCallbackMaximize;
        private readonly IDateOption _dateOption;
        private readonly DateTime _dateTime;


        public ScanDirectoryOperation(
            FileOperation fileOperation, DateTime dateTime,
            int totalFileCount, int threadCount,
            string targetPath, IDateOption dateOption,
            Action<int, int, string> showOnScreenCallback, Action<int, TimeSpan> showOnScreenCallbackMaximize)
        {
            _dateTime = dateTime;
            _totalFileCount = totalFileCount;
            _threadCount = threadCount;
            _showOnScreenCallback = showOnScreenCallback;
            _destinationPath = targetPath;
            _showOnScreenCallbackMaximize = showOnScreenCallbackMaximize;
            _dateOption = dateOption;
            _fileOperation = fileOperation;
        }


        public void ScanFileSystemAndClassifyFiles(IDateOption dateOption, string path)
        {
            foreach (string file in Directory.GetFiles(path))
            {
                ClassifyBySelectedDate(dateOption, file);

                lock (_locker)
                {
                    _locker.COUNTER++;
                }
            }

            Parallel.ForEach(Directory.GetDirectories(path), new ParallelOptions { MaxDegreeOfParallelism = _threadCount }, subDir =>
            {
                var name = new DirectoryInfo(subDir);

                if (Directory.EnumerateFileSystemEntries(name.FullName).Any() && dateOption.setDate(new DirectoryInfo(path), _dateTime))
                    AddDirectoryList(name.FullName);

                else AddEmptyDirectoryList(name.FullName);

                if (String.IsNullOrWhiteSpace(name.Name) || name.Name is null)
                    return;

                ScanFileSystemAndClassifyFiles(dateOption, subDir);
            });
        }

        private void ClassifyBySelectedDate(IDateOption dateOption, string file)
        {
            if (_locker.COUNTER % 100 == 0)
            {
                _showOnScreenCallback(_locker.COUNTER, _totalFileCount, file);
            }

            if (dateOption.setDate(new FileInfo(file), _dateTime))
                AddNewFileList(file);

            else AddOldFileList(file);
        }


        public async override Task Run()
        {
            try
            {
                _locker.COUNTER = 0;

                GetNewFileList().Clear();
                GetOldFileList().Clear();
                GetDirectoryList().Clear();

                _locker.COUNTER = 0;

                var stopWatch = new Stopwatch();

                stopWatch.Start();

                await Task.Run(() => ScanFileSystemAndClassifyFiles(_dateOption, _destinationPath));

                stopWatch.Stop();

                _showOnScreenCallbackMaximize(_locker.COUNTER, stopWatch.Elapsed);

                if (_fileOperation != null && _fileOperation.GetType().Name != "EmptyOperation")
                    await _fileOperation.Run();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("You do not have access to Directory! Please run the root mode");
            }
        }
    }
}

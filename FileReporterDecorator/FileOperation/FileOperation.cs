using FileReporterDecorator.Util;
using System.Collections.Concurrent;

namespace FileReporterDecorator.FileOperation
{
    public abstract class FileOperation
    {
        protected Action<FileInfo, FileInfo> _copyNtfsPermissions;

        private readonly ConcurrentBag<string> _newFileList;
        private readonly ConcurrentBag<string> _oldFileList;
        private readonly ConcurrentBag<string> _directoryList;
        private readonly ConcurrentBag<string> _emptyDirectoryList;
        protected static COUNTER_LOCK _locker = new();

        private bool _isOverwrite;
        private bool _allowNtfsPermissions;
        private bool _emptyFolder;
        public FileOperation()
        {
            _newFileList = new();
            _oldFileList = new();
            _directoryList = new();
            _emptyDirectoryList = new();
        }

        protected bool IsOwerrite() => _isOverwrite;
        protected bool IsEmptyFolder() => _emptyFolder;
        protected bool IsCopyNtfsPermissions() => _copyNtfsPermissions != null;


        public void SetNtfsPermissionAction(Action<FileInfo, FileInfo> action) => _copyNtfsPermissions = action;
        public void SetNtfsPermissions(bool ntfs) => _allowNtfsPermissions = ntfs;
        public void SetEmptyFolder(bool emptyFolder) => _emptyFolder = emptyFolder;
        public void SetOverwrite(bool overwrite) => _isOverwrite = overwrite;

        public void AddNewFileList(string newFile) => _newFileList.Add(newFile);
        public void AddOldFileList(string oldFile) => _oldFileList.Add(oldFile);
        public void AddDirectoryList(string dir) => _directoryList.Add(dir);
        public void AddEmptyDirectoryList(string dir) => _emptyDirectoryList.Add(dir);

        public ConcurrentBag<string> GetNewFileList() => _newFileList;
        public ConcurrentBag<string> GetOldFileList() => _oldFileList;
        public ConcurrentBag<string> GetDirectoryList() => _directoryList;
        public ConcurrentBag<string> GetEmptyDirectoryList() => _emptyDirectoryList;

        public abstract Task Run();
    }
}

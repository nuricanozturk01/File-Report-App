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

        public bool IsOwerrite() => _isOverwrite;
        public bool IsEmptyFolder() => _emptyFolder;
        public bool IsCopyNtfsPermissions() => _copyNtfsPermissions != null;


        // Get NtfsPermissionAction action. implemented on NtfsSecurityOptionDecorator
        public Action<FileInfo, FileInfo> GetNtfsPermissionAction() => _copyNtfsPermissions;


        // Get total file count (old and new)
        public int GetTotalFileCount() => _newFileList.Count + _oldFileList.Count;

        // Set NtfsPermissionAction action. implemented on NtfsSecurityOptionDecorator
        public void SetNtfsPermissionAction(Action<FileInfo, FileInfo> action) => _copyNtfsPermissions = action;
        
        // Set NTFS permissions boolean.
        public void SetNtfsPermissions(bool ntfs) => _allowNtfsPermissions = ntfs;


        // Set empty folder option
        public void SetEmptyFolder(bool emptyFolder) => _emptyFolder = emptyFolder;


        // Set overwrite file option
        public void SetOverwrite(bool overwrite) => _isOverwrite = overwrite;



        // Add newFile to NewFileList
        public void AddNewFileList(string newFile) => _newFileList.Add(newFile);


        // Add oldFile to OldFileList
        public void AddOldFileList(string oldFile) => _oldFileList.Add(oldFile);


        // Add directory to DirectoryList
        public void AddDirectoryList(string dir) => _directoryList.Add(dir);



        // Add Empty Directory to Directory List
        public void AddEmptyDirectoryList(string dir) => _emptyDirectoryList.Add(dir);


        // Get newFileList
        public ConcurrentBag<string> GetNewFileList() => _newFileList;

        // Get oldFileList
        public ConcurrentBag<string> GetOldFileList() => _oldFileList;


        // Get directoryList
        public ConcurrentBag<string> GetDirectoryList() => _directoryList;


        // Get emptyDirectoryList
        public ConcurrentBag<string> GetEmptyDirectoryList() => _emptyDirectoryList;

        public abstract Task Run();
    }
}

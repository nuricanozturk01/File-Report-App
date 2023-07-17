using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.filter;
using FileReporterApp.ServiceApp.options;
using FileReporterApp.Util;
using FileReportServiceLib.ServiceApp.FileOperation;
using FileReportServiceLib.ServiceApp.filter;

namespace FileAccessProject.ServiceApp
{
    public class FileReporterSystemApp
    {
        private string _destinationPath;
        private string _targetPath; // DELETE (?)
        private DateOptions _dateOption;
        private BasicOptions _basicOption; // DELETE (UNUSED)
        private int _threadCount; //USED
        private List<OtherOptions> _otherOptionList; // DELETE (UNUSED)
        private DateTime _dateTime;
        private readonly IFilterService<FileInfo> _fileInfoFilterService;
        private readonly IFilterService<DirectoryInfo> _directoryInfoFilterService;
        private readonly IFileOperation _fileOperation;



        private FileReporterSystemApp()
        {
            _fileOperation = new FileOperation();
            _fileInfoFilterService = new FileInfoFilterService();
            _directoryInfoFilterService = new DirectoryInfoFilterService();
        }

        public void ReportByFileFormat(FileType format, string path, List<FileInfo> newFileList, List<FileInfo> oldFileList)
        {
            ExceptionUtil.DoForAction<Exception>(() => Task.Run(() => FileWriter.WriteFile(newFileList, oldFileList, format, path)), "Somethings are wrong!");
        }




        public void MoveFilesAnother(string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            // GetFullFolders(_destinationPath).AsParallel().ForAll(async dir => await Task.Run(() => Directory.CreateDirectory(dir.Replace(_destinationPath, targetPath))));
            //GetFiles(TimeEnum.AFTER).AsParallel().ForAll(async dir => await Task.Run(() => File.Move(dir.FullName, dir.FullName.Replace(_destinationPath, targetPath), overwrite)));
        }



        public bool FilterFileInfo(FileInfo f, DateTime dateTime, TimeEnum time)
        {

            return _dateOption switch
            {
                DateOptions.CREATED => _fileInfoFilterService.FilterByCreationDate(f, dateTime, time),
                DateOptions.ACCESSED => _fileInfoFilterService.FilterByAccessDate(f, dateTime, time),
                DateOptions.MODIFIED => _fileInfoFilterService.FilterByModifiedDate(f, dateTime, time),

                _ => throw new NotImplementedException("UNSUPPORTED OPERATION!"),
            };
        }
        public bool FilterDirectoryInfo(DirectoryInfo di, DateTime dateTime, TimeEnum time)
        {
            return _dateOption switch
            {
                DateOptions.CREATED => _directoryInfoFilterService.FilterByCreationDate(di, dateTime, time),
                DateOptions.ACCESSED => _directoryInfoFilterService.FilterByAccessDate(di, dateTime, time),
                DateOptions.MODIFIED => _directoryInfoFilterService.FilterByModifiedDate(di, dateTime, time),

                _ => throw new NotImplementedException("UNSUPPORTED OPERATION!"),
            };
        }
        public async void CopyFiles(Dictionary<DirectoryInfo, List<FileInfo>> directoryFileInfoMap, string targetPath, bool overwrite, bool emptyFolders, bool ntfsPermissions)
        {
            await Task.Run(() => CopyFilesCallback(directoryFileInfoMap, targetPath, overwrite, emptyFolders, ntfsPermissions));
        }

        public async void CopyFilesCallback(Dictionary<DirectoryInfo, List<FileInfo>> directoryFileInfoMap, string targetPath, bool overwrite, bool emptyFolders, bool ntfsPermissions)
        {
            foreach (KeyValuePair<DirectoryInfo, List<FileInfo>> entry in directoryFileInfoMap)
            {
                var path = targetPath + "\\" + entry.Key.Name;
                Directory.CreateDirectory(path);

                foreach (FileInfo fileInfo in entry.Value)
                    File.Copy(fileInfo.FullName, path + "\\" + fileInfo.Name, overwrite);
            }
        }

        public void CopyFile(Dictionary<string, FileInfo> map, int counter, int totalFileCount, DirectoryInfo directoryInfo, string targetPath, double totalByte,
         Action<double, string> showMessageCallback, Action<TimeSpan, int, double, double> showFinishMessageCallback)
        {
            _fileOperation.CopyFile(map, counter, totalFileCount, directoryInfo, targetPath, totalByte, showMessageCallback, showFinishMessageCallback);
        }

        public class Builder
        {
            private readonly FileReporterSystemApp _reporterSystem;
            public Builder() => _reporterSystem = new FileReporterSystemApp();
            public Builder SetDestinationPath(string destinationPath)
            {
                _reporterSystem._destinationPath = destinationPath;
                return this;
            }

            public Builder SetTargetPath(string targetPath)
            {
                _reporterSystem._targetPath = targetPath;
                return this;
            }

            public Builder SetDateOption(DateOptions dateOption)
            {
                _reporterSystem._dateOption = dateOption;
                return this;
            }

            public Builder SetThreadCount(int threadCount)
            {
                _reporterSystem._threadCount = threadCount;
                return this;
            }

            public Builder SetBasicOption(BasicOptions basicOption)
            {
                _reporterSystem._basicOption = basicOption;
                return this;
            }

            public Builder SetOtherOptions(List<OtherOptions> otherOptions)
            {
                _reporterSystem._otherOptionList = otherOptions;
                return this;
            }
            public Builder setDateTime(DateTime dateTime)
            {
                _reporterSystem._dateTime = dateTime;
                return this;
            }

            public FileReporterSystemApp Build() => _reporterSystem;
        }
    }
}
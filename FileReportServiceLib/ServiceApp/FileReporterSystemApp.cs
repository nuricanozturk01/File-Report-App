using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.filter;
using FileReporterApp.ServiceApp.options;
using FileReporterApp.Util;


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
        private readonly IFilterService<FileInfo> _filterService;
        private List<FileInfo> _scannedMergedList;


        private FileReporterSystemApp() => _filterService = new FilterService();
        public void SetScannedMergedList(List<FileInfo> scannedMergedFiles) => _scannedMergedList = scannedMergedFiles;

        public IEnumerable<FileInfo> GetFiles(TimeEnum timeEnum)
        {
            return new DirectoryInfo(_destinationPath).GetFiles("*", SearchOption.AllDirectories).AsParallel().Where(f => Filter(f, _dateTime, timeEnum));
        }
        public IEnumerable<FileInfo> GetFiles(DateTime dateTime, TimeEnum timeEnum, List<string> path)
        {
            return path.SelectMany(path => new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories).Where(f => Filter(f, dateTime, timeEnum)));
        }


        public void ReportByFileFormat(FileType format, string path, List<FileInfo> fileList)
        {
            ExceptionUtil.DoForAction<Exception>(() => Task.Run(() => FileWriter.WriteFile(fileList, format, path)), "Somethings are wrong!");
        }

        private IEnumerable<string> GetFullFolders(string path)
        {
            return Directory.GetDirectories(path, "*", SearchOption.AllDirectories)
                    .Where(dir => Directory.GetFiles(dir).Any(f => Filter(new FileInfo(f), _dateTime, TimeEnum.AFTER)))
                    .Where(dir => Directory.GetFiles(dir).Length != 0);
        }


        public void MoveFilesAnother(string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            GetFullFolders(_destinationPath).AsParallel().ForAll(async dir => await Task.Run(() => Directory.CreateDirectory(dir.Replace(_destinationPath, targetPath))));
            GetFiles(TimeEnum.AFTER).AsParallel().ForAll(async dir => await Task.Run(() => File.Move(dir.FullName, dir.FullName.Replace(_destinationPath, targetPath), overwrite)));
        }

        public void CopyFilesAnother(string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            GetFullFolders(_destinationPath).AsParallel().ForAll(async dir => await Task.Run(() => Directory.CreateDirectory(dir.Replace(_destinationPath, targetPath))));
            GetFiles(TimeEnum.AFTER).AsParallel().ForAll(async dir => await Task.Run(() => File.Copy(dir.FullName, dir.FullName.Replace(_destinationPath, targetPath), overwrite)));
        }


        public bool Filter(FileInfo f, DateTime dateTime, TimeEnum time)
        {
            return _dateOption switch
            {
                DateOptions.CREATED => _filterService.FilterByCreationDate(f, dateTime, time),
                DateOptions.ACCESSED => _filterService.FilterByAccessDate(f, dateTime, time),
                DateOptions.MODIFIED => _filterService.FilterByModifiedDate(f, dateTime, time),

                _ => throw new NotImplementedException("UNSUPPORTED OPERATION!"),
            };
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

        //Delete (UNUSED)
       /* public void MoveFiles(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            afterFileList.AsParallel().ForAll(async fi => await Task.Run(() => File.Move(fi.FullName, targetPath + "\\" + fi.Name, overwrite)));
        }*/
        //Delete (UNUSED)
       /* public async void CopyFiles(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            foreach (var fi in afterFileList)
                await Task.Run(() => File.Copy(fi.FullName, targetPath + "\\" + fi.Name, overwrite));
        }*/
        //Delete (UNUSED)
       // private IEnumerable<string> GetEmptyFolders(string path) => Directory.GetDirectories(path, "*", SearchOption.AllDirectories).Where(dir => Directory.GetFiles(dir).Length == 0);
    }
}
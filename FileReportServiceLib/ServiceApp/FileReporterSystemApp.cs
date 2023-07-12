
using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.filter;
using FileReporterApp.ServiceApp.options;
using FileReporterApp.Util;

namespace FileAccessProject.ServiceApp
{
    public class FileReporterSystemApp
    {
        private string _destinationPath;
        private string _targetPath;
        private DateOptions _dateOption;
        private BasicOptions _basicOption;
        private int _threadCount;
        private List<OtherOptions> _otherOptionList;

        private readonly IFilterService<FileInfo> _filterService;
        private List<FileInfo> _scannedMergedList;
        private FileReporterSystemApp() => _filterService = new FilterService();

        public void SetScannedMergedList(List<FileInfo> scannedMergedFiles) => _scannedMergedList = scannedMergedFiles;
        public IEnumerable<FileInfo> GetFiles(DateTime dateTime, TimeEnum timeEnum) => new DirectoryInfo(_destinationPath).GetFiles("*", SearchOption.AllDirectories).Where(f => Filter(f, dateTime, timeEnum));


        private bool Filter(FileInfo f, DateTime dateTime, TimeEnum time)
        {
            return _dateOption switch
            {
                DateOptions.CREATED => _filterService.FilterByCreationDate(f, dateTime, time),
                DateOptions.ACCESSED => _filterService.FilterByAccessDate(f, dateTime, time),
                DateOptions.MODIFIED => _filterService.FilterByModifiedDate(f, dateTime, time),

                _ => throw new NotImplementedException("UNSUPPORTED OPERATION!"),
            };
        }

        public void MoveFiles(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            afterFileList.AsParallel().ForAll(async fi => await Task.Run(() => File.Move(fi.FullName, targetPath + "\\" + fi.Name, overwrite)));
        }

        public async void CopyFiles(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            foreach (var fi in afterFileList)
                await Task.Run(() => File.Copy(fi.FullName, targetPath + "\\" + fi.Name, overwrite));
        }


        public void ReportByFileFormat(FileType format, string path) => ExceptionUtil.DoForAction<Exception>(() => ReportByFileFormatCallback(format, path), "Somethings are wrong!");


        // [QUESTION] Burada WriteFile'ın içindeki Excel Writer ve TextFileWriter da asenkron kullanıldı. Sadece burada kullanılsa olur mu?
        // Burada Kullanılma amacı Senaryo başarılı ilerlediğinde MessageBox'un işlemden sonra çıkmasını sağlamak
        private async void ReportByFileFormatCallback(FileType format, string path)
        {
            await Task.Run(() => FileWriter.WriteFile(_scannedMergedList, format, path));
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

            public FileReporterSystemApp Build() => _reporterSystem;
        }
    }
}

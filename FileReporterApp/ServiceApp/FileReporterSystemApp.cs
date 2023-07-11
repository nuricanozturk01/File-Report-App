using FileReporterApp.ServiceApp.FileReader;
using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.filter;
using FileReporterApp.ServiceApp.options;

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
        private readonly IFileReaderXService _filerReaderService;
        private readonly IFileWriteService _fileWriteService;
        private readonly IFilterService<FileInfo> _filterService;

        private FileReporterSystemApp()
        {
            _filerReaderService = new FileReaderXService();
            _fileWriteService = new FileWriterService();
            _filterService = new FilterService();
        }

        public IEnumerable<FileInfo> GetFiles(DateTime dateTime, TimeEnum timeEnum) => Directory.GetFiles(_destinationPath).Select(f => new FileInfo(f)).Where(f => filter(f, dateTime, timeEnum));
        

        private bool filter(FileInfo f, DateTime dateTime, TimeEnum time)
        {
            return _dateOption switch
            {
                DateOptions.CREATED => _filterService.filterByCreationDate(f, dateTime, time),
                DateOptions.ACCESSED => _filterService.filterByAccessDate(f, dateTime, time),
                DateOptions.MODIFIED => _filterService.filterByModifiedDate(f, dateTime, time),

                _ => throw new NotImplementedException("UNSUPPORTED OPERATION!"),
            };
        }

        public void writeFileTest(List<FileInfo> list) => File.WriteAllLines(_targetPath, list
            .Select(f => String.Format("{0} | {1} | {2} | {3} | {4}\n", f.Name, f.FullName, f.CreationTime, f.LastWriteTime, f.LastAccessTime)));

        public class Builder
        {
            private readonly FileReporterSystemApp _reporterSystem;

            public Builder() => _reporterSystem = new FileReporterSystemApp();
            public Builder setDestinationPath(string destinationPath)
            {
                _reporterSystem._destinationPath = destinationPath;
                return this;
            }

            public Builder setTargetPath(string targetPath)
            {
                _reporterSystem._targetPath = targetPath;
                return this;
            }

            public Builder setDateOption(DateOptions dateOption)
            {
                _reporterSystem._dateOption = dateOption;
                return this;
            }

            public Builder setThreadCount(int threadCount)
            {
                _reporterSystem._threadCount = threadCount;
                return this;
            }

            public Builder setBasicOption(BasicOptions basicOption)
            {
                _reporterSystem._basicOption = basicOption;
                return this;
            }

            public Builder setOtherOptions(List<OtherOptions> otherOptions)
            {
                _reporterSystem._otherOptionList = otherOptions;
                return this;
            }

            public FileReporterSystemApp build() => _reporterSystem;
        }
    }


}

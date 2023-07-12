
using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.filter;
using FileReporterApp.ServiceApp.options;
using System.Diagnostics;
using System.Security.AccessControl;

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



        internal void SetScannedMergedList(List<FileInfo> scannedMergedFiles) => _scannedMergedList = scannedMergedFiles;
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

        internal void MoveFiles(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            afterFileList.AsParallel().ForAll(async fi => await Task.Run(() => File.Move(fi.FullName, targetPath + "\\" + fi.Name, overwrite)));
           
        }

        internal async void CopyFiles(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            foreach(var fi in afterFileList)
                await Task.Run(() => File.Copy(fi.FullName, targetPath + "\\" + fi.Name, overwrite));
        }

        internal async void Scan(List<FileInfo> mergedList, ListBox ResultListBox)
        {
            if (ResultListBox.InvokeRequired)
            {
                ResultListBox.Invoke(() => Scan(mergedList, ResultListBox));
                return;
            }

            var startTime = Stopwatch.GetTimestamp();

            for (var i = 0; i < mergedList.Count; ++i)
            {
                ResultListBox.Items[0] = (i + 1) + " items were scanned!";
                ResultListBox.Items[2] = mergedList[i];
                await Task.Delay(45);
            }

            var finishTime = Stopwatch.GetTimestamp();

            ResultListBox.Items[4] = "Scan was completed! Total Elapsed Time: " + String.Format("{0}", TimeSpan.FromMilliseconds(finishTime - startTime).ToString(@"hh\:mm\:ss"));
        }

        internal void ReportByFileFormat(FileType format, string path)
        {
            try
            {
                FileWriter.WriteFile(_scannedMergedList, format, path);
                MessageBox.Show("Report exported successfully!");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
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

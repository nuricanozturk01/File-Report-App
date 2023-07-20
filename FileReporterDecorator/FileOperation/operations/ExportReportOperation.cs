using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.options;

namespace FileReporterDecorator.FileOperation.operations
{
    internal class ExportReportOperation : FileOperation
    {
        private readonly FileType fileFormat;
        private readonly string path;
        private readonly FileOperation _scanProcess;
        public ExportReportOperation(FileOperation scanProcess, FileType fileFormat, string path)
        {
            this.fileFormat = fileFormat;
            this.path = path;
            _scanProcess = scanProcess;
        }

        public override async Task Run()
        {
            var _newFiles = _scanProcess.GetNewFileList().Select(newFile => new FileInfo(newFile)).ToList();
            var _oldFiles = _scanProcess.GetOldFileList().Select(oldFile => new FileInfo(oldFile)).ToList();

            await Task.Run(() => FileWriter.WriteFile(_newFiles, _oldFiles, fileFormat, path));
        }
    }
}

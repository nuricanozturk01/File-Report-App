using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.options;

namespace FileReporterDecorator.FileOperation.operations
{
    internal class ExportReportOperation : FileOperation
    {
        private readonly FileType fileFormat;
        private readonly string path;
        private readonly FileOperation _fileOperation;
        public ExportReportOperation(FileOperation fileOperation, FileType fileFormat,string path)
        {
            this.fileFormat = fileFormat;
            this.path = path;
            _fileOperation = fileOperation;
        }

        public override async Task Run()
        {
            var _newFiles = _fileOperation.GetNewFileList().Select(newFile => new FileInfo(newFile)).ToList();
            var _oldFiles = _fileOperation.GetOldFileList().Select(oldFile => new FileInfo(oldFile)).ToList();

            await Task.Run(() => FileWriter.WriteFile(_newFiles, _oldFiles, fileFormat, path));
        }
    }
}

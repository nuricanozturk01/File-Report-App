using FileReporterLib.FileWriter;
using FileReporterLib.Options;

namespace FileReporterDecorator.FileOperation.operations
{
    public class ExportReportOperation : FileOperation
    {
        private readonly FileType _fileFormat;
        private readonly string _destinationPath;
        private readonly FileOperation _scanProcess;
        
        public ExportReportOperation(FileOperation scanProcess, FileType fileFormat, string path)
        {
            _fileFormat = fileFormat;
            _destinationPath = path;
            _scanProcess = scanProcess;
        }








        /*
         * 
         *  Trigger method for export report opearation. This method call the WriteFile method on FileWRiter singleton class.
         * 
         */
        public override async Task Run()
        {
            var _newFiles = _scanProcess.GetNewFileList().Select(newFile => new FileInfo(newFile)).ToList();
            var _oldFiles = _scanProcess.GetOldFileList().Select(oldFile => new FileInfo(oldFile)).ToList();

            await Task.Run(() => FileWriter.WriteFile(_newFiles, _oldFiles, _fileFormat, _destinationPath));
        }
    }
}

using FileReporterApp.ServiceApp.options;

namespace FileReporterApp.ServiceApp.FileWriter
{
    public partial class FileWriter
    {
        private IFileWrite _textFileWriter;
        private IFileWrite _excelFileWriter;
        private static readonly FileWriter _fileWriter = new FileWriter();
        private FileWriter()
        {
            _textFileWriter = new TextFileWriter();
            _excelFileWriter = new ExcelFileWriter();
        }

        public static FileWriter getInstance() => _fileWriter is null ? new FileWriter() : _fileWriter;

        public static void WriteFile(List<FileInfo> scannedMergedList, FileType format, string targetPath)
        {
            switch (format)
            {
                case FileType.EXCEL:
                    getInstance()._excelFileWriter.Write(scannedMergedList, targetPath);
                    break;

                default:
                    getInstance()._textFileWriter.Write(scannedMergedList, targetPath);
                    break;
            }
        }
    }
}

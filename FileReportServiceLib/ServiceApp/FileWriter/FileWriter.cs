using FileReporterApp.ServiceApp.options;

namespace FileReporterApp.ServiceApp.FileWriter
{
    public partial class FileWriter
    {
        private IFileWrite _textFileWriter;
        private IFileWrite _excelFileWriter;
        private static readonly FileWriter _fileWriter = new();
        private FileWriter()
        {
            _textFileWriter = new TextFileWriter();
            _excelFileWriter = new ExcelFileWriter();
        }

        public static FileWriter GetInstance() => _fileWriter is null ? new FileWriter() : _fileWriter;

        public static void WriteFile(List<FileInfo> scannedMergedList, FileType format, string targetPath)
        {
            switch (format)
            {
                case FileType.EXCEL:
                    GetInstance()._excelFileWriter.Write(scannedMergedList, targetPath);
                    break;

                default:
                    GetInstance()._textFileWriter.Write(scannedMergedList, targetPath);
                    break;
            }
        }
    }
}

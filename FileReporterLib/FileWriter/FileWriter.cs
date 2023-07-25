using FileReporterLib.Options;

namespace FileReporterLib.FileWriter
{
    /*
     * 
     * FileWriter is a singleton class for report action. 
     * 
     */
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





        /*
         * 
         * Decide the report format is excel or text. 
         * 
         */
        public static void WriteFile(List<FileInfo> newFileList, List<FileInfo> oldFileList, FileType format, string targetPath)
        {
            switch (format)
            {
                case FileType.EXCEL:
                    GetInstance()._excelFileWriter.Write(newFileList, oldFileList, targetPath);
                    break;

                default:
                    GetInstance()._textFileWriter.Write(newFileList, oldFileList, targetPath);
                    break;
            }
        }
    }
}

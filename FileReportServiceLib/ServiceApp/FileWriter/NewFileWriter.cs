using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReportServiceLib.ServiceApp.FileWriter
{
    public partial class NewFileWriter
    {
        private IFileWriter _textFileWriter;
        //private IFileWrite _excelFileWriter;
        private static NewFileWriter _fileWriter;
        private NewFileWriter(string targetPath)
        {
            _textFileWriter = new TextFileWriterAsync(targetPath);
            //_excelFileWriter = new ExcelFileWriter();
        }

        public static NewFileWriter GetInstance(string targetPath) => _fileWriter is null ? new NewFileWriter(targetPath) : _fileWriter;

        public static void WriteFile(string str, FileType format, string targetPath)
        {
            switch (format)
            {
               /* case FileType.EXCEL:
                    GetInstance()._excelFileWriter.Write(scannedMergedList, targetPath);
                    break;
               */
                default:
                    GetInstance(targetPath)._textFileWriter.Write(new FileInfo(targetPath), str);
                    break;
            }
        }
    }
}

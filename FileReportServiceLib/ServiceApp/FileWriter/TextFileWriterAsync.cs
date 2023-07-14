using FileReporterApp.ServiceApp.FileWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReportServiceLib.ServiceApp.FileWriter
{
    internal class TextFileWriterAsync : IFileWriter
    {
        private readonly string DELIMITER = "|";
        private readonly FileStream _fileStream;
        private readonly StreamWriter _streamWriter;

        public TextFileWriterAsync(string targetPath)
        {
            _fileStream = new FileStream(targetPath, FileMode.Append, FileAccess.Write);
            _streamWriter = new StreamWriter(_fileStream, Encoding.UTF8);
        }
        public async void Write(List<FileInfo> scannedMergedList, string targetPath) => await Task.Run(() => WriteTextFileCallback(scannedMergedList, targetPath));

        private void WriteTextFileCallback(List<FileInfo> scannedMergedList, string targetPath)
        {
           
        }
        private string GetFormattedString(FileInfo f) => $"{f.Name} {DELIMITER} {f.FullName} {DELIMITER} {f.CreationTime} {DELIMITER} {f.LastWriteTime} {DELIMITER} {f.LastAccessTime}\n";

        public void Write(FileInfo file, string str)
        {

            try
            {
                _streamWriter.WriteLine(GetFormattedString(file));
            }
            catch { }
            finally
            {
               
            }
        }
    }
}

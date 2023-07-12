using FileReporterApp.ServiceApp.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterApp.ServiceApp.FileWriter
{
    internal class TextFileWriter : IFileWrite
    {
        private readonly string DELIMITER = "|";

        public void Write(List<FileInfo> scannedMergedList, string targetPath) => File.WriteAllLines(targetPath,
            scannedMergedList.Select(f => $"{f.Name} {DELIMITER} {f.FullName} {DELIMITER} {f.CreationTime} {DELIMITER} {f.LastWriteTime} {DELIMITER} {f.LastAccessTime}\n"));

    }
}

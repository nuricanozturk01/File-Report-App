namespace FileReporterApp.ServiceApp.FileWriter
{
    internal class TextFileWriter : IFileWrite
    {
        private readonly string DELIMITER = "|";

        public async void Write(List<FileInfo> scannedMergedList, string targetPath) => await Task.Run(() => WriteTextFileCallback(scannedMergedList, targetPath));

        private void WriteTextFileCallback(List<FileInfo> scannedMergedList, string targetPath)
        {
            try
            {
                if (scannedMergedList != null) 
                    File.WriteAllLines(targetPath, scannedMergedList.AsParallel().Select(f => GetFormattedString(f)));
            }
            catch { }
        }
        private string GetFormattedString(FileInfo f) => $"{f.Name} {DELIMITER} {f.FullName} {DELIMITER} {f.CreationTime} {DELIMITER} {f.LastWriteTime} {DELIMITER} {f.LastAccessTime}\n";
    }
}
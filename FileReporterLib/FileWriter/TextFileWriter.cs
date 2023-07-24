namespace FileReporterLib.FileWriter
{
    internal class TextFileWriter : IFileWrite
    {
        private readonly string DELIMITER = "|";

        public async void Write(List<FileInfo> newFileList, List<FileInfo> oldFileList, string targetPath) 
            => await Task.Run(() => WriteTextFileCallback(newFileList, oldFileList, targetPath));

        /*
         *
         * Callback for Write method.
         * Write files to text file.
         * 
         */
        private void WriteTextFileCallback(List<FileInfo> newFileList, List<FileInfo> oldFileList, string targetPath)
        {
            try
            {
                if (newFileList != null)
                    File.WriteAllLines(targetPath, newFileList.AsParallel().Select(f => GetFormattedString(f)));

                if (oldFileList != null)
                    File.AppendAllLines(targetPath, oldFileList.AsParallel().Select(f => GetFormattedString(f)));
            }
            catch { }
        }


        /*
         * 
         *  String format for writing text file
         * 
         */
        private string GetFormattedString(FileInfo f) => 
            $"{f.Name} {DELIMITER} {f.FullName} {DELIMITER} {f.CreationTime} {DELIMITER} {f.LastWriteTime} {DELIMITER} {f.LastAccessTime}\n";
    }
}
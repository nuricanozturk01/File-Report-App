using FileReporterApp.ServiceApp.options;

namespace FileReporterApp.ServiceApp.FileWriter
{
    public interface IFileWrite
    {
        public void Write(List<FileInfo> scannedMergedList, string targetPath);
    }
}
using System.Collections.ObjectModel;

namespace FileReporterApp.ServiceApp.FileWriter
{
    public interface IFileWrite
    {
        public void Write(List<FileInfo> newFileList, List<FileInfo> oldFileList, string targetPath);
    }
}
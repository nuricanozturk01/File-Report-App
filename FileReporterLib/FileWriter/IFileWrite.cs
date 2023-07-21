namespace FileReporterLib.FileWriter
{
    public interface IFileWrite
    {
        public void Write(List<FileInfo> newFileList, List<FileInfo> oldFileList, string targetPath);
    }
}
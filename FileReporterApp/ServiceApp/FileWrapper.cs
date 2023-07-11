using System;

namespace FileAccessProject.ServiceApp
{
    public class FileWrapper
    {
        private readonly string _fileName;
        private readonly string _filePath;
        private readonly DateTime _createdDate;
        private readonly DateTime _modifiedDate;
        private readonly DateTime _accessedDate;
        private readonly double _fileSize;

        public FileWrapper(string fileName, string filePath, DateTime createdDate, DateTime modifiedDate, DateTime accessedDate, double fileSize)
        {
            _fileName = fileName;
            _filePath = filePath;
            _createdDate = createdDate;
            _modifiedDate = modifiedDate;
            _accessedDate = accessedDate;
            _fileSize = fileSize;
        }

        public string getFileName() => _fileName;
        public string getFilePath() => _filePath;
        public DateTime getCreatedDate() => _createdDate;
        public DateTime getLastModifiedDate() => _modifiedDate;
        public DateTime getLastAccessedDate() => _accessedDate;
        public double getFileSize() => _fileSize;
    }
}

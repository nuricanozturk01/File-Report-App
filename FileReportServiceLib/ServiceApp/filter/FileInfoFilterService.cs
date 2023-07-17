using FileReporterApp.ServiceApp.options;

namespace FileReporterApp.ServiceApp.filter
{
    public class FileInfoFilterService : IFilterService<FileInfo>
    {
        public FileInfoFilterService() { }

        public bool FilterByCreationDate(FileInfo fi, DateTime date, TimeEnum timeEnum)
        {
            return timeEnum == TimeEnum.BEFORE ? fi.CreationTime.Date < date.Date: fi.CreationTime.Date >= date.Date;
        }
        public bool FilterByModifiedDate(FileInfo fi, DateTime date, TimeEnum timeEnum)
        {
            return timeEnum == TimeEnum.BEFORE ? fi.LastAccessTime.Date < date.Date : fi.LastAccessTime.Date >= date.Date;
        }
        public bool FilterByAccessDate(FileInfo fi, DateTime date, TimeEnum timeEnum)
        {
            return timeEnum == TimeEnum.BEFORE ? fi.LastAccessTime.Date < date.Date : fi.LastAccessTime.Date >= date.Date;
        }
    }
}
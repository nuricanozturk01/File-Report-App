namespace FileReporterApp.ServiceApp.filter
{
    public class FilterService : IFilterService<FileInfo>
    {
        public FilterService() { }

        public bool filterByDate(FileInfo filterObj) => throw new NotImplementedException();

        public bool filterByCreationDateBefore(FileInfo filterObj, DateTime date) => filterObj.CreationTime.CompareTo(date) < 0;

        public bool filterByCreationDateAfter(FileInfo filterObj, DateTime date) => filterObj.CreationTime.CompareTo(date) >= 0;

        public bool filterByModifiedDateBefore(FileInfo filterObj, DateTime date) => filterObj.LastAccessTime.CompareTo(date) < 0;

        public bool filterByModifiedDateAfter(FileInfo filterObj, DateTime date) => filterObj.LastAccessTime.CompareTo(date) >= 0;
        public bool filterByAccessDateBefore(FileInfo filterObj, DateTime date) => filterObj.LastAccessTime.CompareTo(date) < 0;
        public bool filterByAccessDateAfter(FileInfo filterObj, DateTime date) => filterObj.LastAccessTime.CompareTo(date) >= 0;
    }
}

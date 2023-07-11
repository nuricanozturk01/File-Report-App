﻿using FileReporterApp.ServiceApp.options;

namespace FileReporterApp.ServiceApp.filter
{
    public class FilterService : IFilterService<FileInfo>
    {
        public FilterService() { }

        public bool filterByCreationDate(FileInfo filterObj, DateTime date, TimeEnum timeEnum) => 
            timeEnum == TimeEnum.BEFORE ? filterObj.CreationTime.CompareTo(date) < 0 : filterObj.CreationTime.CompareTo(date) >= 0;
        public bool filterByModifiedDate(FileInfo filterObj, DateTime date, TimeEnum timeEnum) =>
            timeEnum == TimeEnum.BEFORE ? filterObj.LastAccessTime.CompareTo(date) < 0 : filterObj.LastAccessTime.CompareTo(date) >= 0;
        public bool filterByAccessDate(FileInfo filterObj, DateTime date, TimeEnum timeEnum) => 
            timeEnum == TimeEnum.BEFORE ? filterObj.LastAccessTime.CompareTo(date) < 0 : filterObj.LastAccessTime.CompareTo(date) >= 0;
    }
}

using FileReporterApp.ServiceApp.filter;
using FileReporterApp.ServiceApp.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReportServiceLib.ServiceApp.filter
{
    internal class DirectoryInfoFilterService : IFilterService<DirectoryInfo>
    {
        public bool FilterByAccessDate(DirectoryInfo filterObj, DateTime date, TimeEnum timeEnum)
        {
            return timeEnum == TimeEnum.BEFORE ? filterObj.CreationTime.Date < date : filterObj.CreationTime.Date >= date;
        }

        public bool FilterByCreationDate(DirectoryInfo filterObj, DateTime date, TimeEnum timeEnum)
        {
            return timeEnum == TimeEnum.BEFORE ? filterObj.LastAccessTime.Date < date : filterObj.LastAccessTime.Date >= date;
        }

        public bool FilterByModifiedDate(DirectoryInfo filterObj, DateTime date, TimeEnum timeEnum)
        {
            return timeEnum == TimeEnum.BEFORE ? filterObj.LastAccessTime.Date < date: filterObj.LastAccessTime.Date >= date;
        }
    }
}

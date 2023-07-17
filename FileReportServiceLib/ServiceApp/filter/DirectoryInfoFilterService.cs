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
            return timeEnum == TimeEnum.BEFORE ? filterObj.CreationTime.Date < date.Date : filterObj.CreationTime.Date >= date.Date;
        }

        public bool FilterByCreationDate(DirectoryInfo filterObj, DateTime date, TimeEnum timeEnum)
        {
            return timeEnum == TimeEnum.BEFORE ? filterObj.LastAccessTime.Date < date.Date : filterObj.LastAccessTime.Date >= date.Date;
        }

        public bool FilterByModifiedDate(DirectoryInfo filterObj, DateTime date, TimeEnum timeEnum)
        {
            return timeEnum == TimeEnum.BEFORE ? filterObj.LastAccessTime.Date < date.Date : filterObj.LastAccessTime.Date >= date.Date;
        }
    }
}

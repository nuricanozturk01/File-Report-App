using System;
using FileReporterApp.ServiceApp.options;

namespace FileReporterApp.ServiceApp.filter
{
    internal interface IFilterService<T>
    {
        bool filterByDate(T filterObj);
        bool filterByCreationDateBefore(T filterObj, DateTime date);
        bool filterByCreationDateAfter(T filterObj, DateTime date);

        bool filterByModifiedDateBefore(T filterObj, DateTime date);
        bool filterByModifiedDateAfter(T filterObj, DateTime date);

        bool filterByAccessDateBefore(T filterObj, DateTime date);
        bool filterByAccessDateAfter(T filterObj, DateTime date);
    }
}
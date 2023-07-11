using FileReporterApp.ServiceApp.options;

namespace FileReporterApp.ServiceApp.filter
{
    internal interface IFilterService<T>
    {
        bool filterByCreationDate(T filterObj, DateTime date, TimeEnum timeEnum);


        bool filterByModifiedDate(T filterObj, DateTime date, TimeEnum timeEnum);


        bool filterByAccessDate(T filterObj, DateTime date, TimeEnum timeEnum);

    }
}
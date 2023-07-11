using FileReporterApp.ServiceApp.options;

namespace FileReporterApp.ServiceApp.filter
{
    internal interface IFilterService<T>
    {
        bool FilterByCreationDate(T filterObj, DateTime date, TimeEnum timeEnum);


        bool FilterByModifiedDate(T filterObj, DateTime date, TimeEnum timeEnum);


        bool FilterByAccessDate(T filterObj, DateTime date, TimeEnum timeEnum);

    }
}
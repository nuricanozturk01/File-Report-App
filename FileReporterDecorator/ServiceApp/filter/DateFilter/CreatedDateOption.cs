namespace FileReporterDecorator.ServiceApp.filter.DateFilter
{
    internal class CreatedDateOption : IDateOption
    {
        public bool setDate(FileInfo srcDate, DateTime date) => srcDate.CreationTime.Date >= date.Date;

        public bool setDate(DirectoryInfo srcDate, DateTime targetDate) => srcDate.CreationTime.Date >= targetDate.Date;
    }
}

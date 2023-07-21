namespace FileReporterLib.Filter.DateFilter
{
    internal class CreatedDateOption : IDateOption
    {
        public bool SetDate(FileInfo srcDate, DateTime date) => srcDate.CreationTime.Date >= date.Date;

        public bool SetDate(DirectoryInfo srcDate, DateTime targetDate) => srcDate.CreationTime.Date >= targetDate.Date;
    }
}

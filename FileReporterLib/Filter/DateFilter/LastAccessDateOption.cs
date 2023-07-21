namespace FileReporterLib.Filter.DateFilter
{
    public class LastAccessDateOption : IDateOption
    {
        public bool SetDate(FileInfo srcDate, DateTime date) => srcDate.LastAccessTime.Date >= date.Date;

        public bool SetDate(DirectoryInfo srcDate, DateTime targetDate) => srcDate.LastAccessTime.Date >= targetDate.Date;
    }
}

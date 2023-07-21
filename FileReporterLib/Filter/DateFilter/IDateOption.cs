namespace FileReporterLib.Filter.DateFilter
{
    public interface IDateOption
    {
        public bool SetDate(FileInfo srcDate, DateTime targetDate);
        public bool SetDate(DirectoryInfo srcDate, DateTime targetDate);
    }
}

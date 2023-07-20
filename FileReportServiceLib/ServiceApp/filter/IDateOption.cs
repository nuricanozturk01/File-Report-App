namespace FileReportServiceLib.ServiceApp.filter
{
    public interface IDateOption
    {
        public bool setDate(FileInfo srcDate, DateTime targetDate);
        public bool setDate(DirectoryInfo srcDate, DateTime targetDate);
    }
}

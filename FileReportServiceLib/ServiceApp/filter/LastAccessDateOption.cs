namespace FileReportServiceLib.ServiceApp.filter
{
    public class LastAccessDateOption : IDateOption
    {

        public bool setDate(FileInfo srcDate, DateTime date) => srcDate.LastAccessTime.Date >= date.Date;

        public bool setDate(DirectoryInfo srcDate, DateTime targetDate) => srcDate.LastAccessTime.Date >= targetDate.Date;
    }
}

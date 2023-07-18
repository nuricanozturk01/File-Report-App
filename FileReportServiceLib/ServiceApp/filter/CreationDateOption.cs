namespace FileReportServiceLib.ServiceApp.filter
{
    public class CreationDateOption : IDateOption
    {


        public bool setDate(FileInfo srcDate, DateTime date) => srcDate.CreationTime.Date >= date.Date;

        public bool setDate(DirectoryInfo srcDate, DateTime targetDate) => srcDate.CreationTime.Date >= targetDate.Date;
    }
}

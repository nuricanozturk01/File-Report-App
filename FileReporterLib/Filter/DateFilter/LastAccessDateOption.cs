namespace FileReporterLib.Filter.DateFilter
{
    public class LastAccessDateOption : IDateOption
    {
        /*
         * 
         * Compare the file LastAccessTime dates
         *
         */
        public bool SetDate(FileInfo srcDate, DateTime date) => srcDate.LastAccessTime.Date >= date.Date;

        /*
         * 
         * Compare the folder LastAccessTime dates
         *
         */
        public bool SetDate(DirectoryInfo srcDate, DateTime targetDate) => srcDate.LastAccessTime.Date >= targetDate.Date;
    }
}

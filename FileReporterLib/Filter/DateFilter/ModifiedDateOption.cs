namespace FileReporterLib.Filter.DateFilter
{
    public class ModifiedDateOption : IDateOption
    {
        /*
         * 
         * Compare the file LastWriteTime dates
         *
         */
        public bool SetDate(FileInfo srcDate, DateTime date) => srcDate.LastWriteTime.Date >= date.Date;





        /*
         * 
         * Compare the folder LastWriteTime dates
         *
         */
        public bool SetDate(DirectoryInfo srcDate, DateTime targetDate) => srcDate.LastWriteTime.Date >= targetDate.Date;
    }
}

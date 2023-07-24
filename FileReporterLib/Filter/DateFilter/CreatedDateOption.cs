namespace FileReporterLib.Filter.DateFilter
{
    internal class CreatedDateOption : IDateOption
    {
        /*
         * 
         * Compare the file Creation dates  
         *
         */
        public bool SetDate(FileInfo srcDate, DateTime date) => srcDate.CreationTime.Date >= date.Date;

        /*
         * 
         * Compare the folder Creation dates  
         *
         */
        public bool SetDate(DirectoryInfo srcDate, DateTime targetDate) => srcDate.CreationTime.Date >= targetDate.Date;
    }
}

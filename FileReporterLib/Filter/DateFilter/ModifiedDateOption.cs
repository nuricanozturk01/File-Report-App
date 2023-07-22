﻿namespace FileReporterLib.Filter.DateFilter
{
    public class ModifiedDateOption : IDateOption
    {
        public bool SetDate(FileInfo srcDate, DateTime date) => srcDate.LastWriteTime.Date >= date.Date;

        public bool SetDate(DirectoryInfo srcDate, DateTime targetDate) => srcDate.LastWriteTime.Date >= targetDate.Date;
    }
}

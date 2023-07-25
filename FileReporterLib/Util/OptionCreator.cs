using FileReporterLib.Filter.DateFilter;
using FileReporterLib.Options;

namespace FileReportServiceLib.Util
{
    public class OptionCreator
    {


        /*
         * 
         * Create the Date Option 
         *
         */
        public static IDateOption GetSelectedDateOption(bool createdDate, bool modifiedDate) // Return base operation
        {
            if (createdDate)
                return new CreatedDateOption();

            else if (modifiedDate)
                return new ModifiedDateOption();

            return new LastAccessDateOption();
        }






        /*
         * 
         * This method returns the File Type. (excel or text) 
         *
         */
        public static FileType GetFileFormat(int filterIndex) => filterIndex == 1 ? FileType.TEXT : FileType.EXCEL;
    }
}

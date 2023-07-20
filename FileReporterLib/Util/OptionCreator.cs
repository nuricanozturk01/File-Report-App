using FileReporterApp.ServiceApp.options;
using FileReporterDecorator.ServiceApp.filter.DateFilter;


namespace FileReportServiceLib.Util
{
    public class OptionCreator
    {
        public static IDateOption GetSelectedDateOption(bool createdDate, bool modifiedDate) // Return base operation
        {
            if (createdDate)
                return new CreatedDateOption();

            else if (modifiedDate)
                return new ModifiedDateOption();

            return new LastAccessDateOption();
        }

        public static FileType GetFileFormat(int filterIndex) => filterIndex == 1 ? FileType.TEXT : FileType.EXCEL;
    }
}

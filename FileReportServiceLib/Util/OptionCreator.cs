using FileReporterApp.ServiceApp.options;
using FileReportServiceLib.ServiceApp.filter;

namespace FileReportServiceLib.Util
{
    public class OptionCreator
    {
        public static IDateOption GetSelectedDateOption(bool createdDate, bool modifiedDate)
        {
            if (createdDate)
                return new CreationDateOption();

            else if (modifiedDate)
                return new ModifiedDateOption();

            return new LastAccessDateOption();
        }

        public static FileType GetFileFormat(int filterIndex) => filterIndex == 1 ? FileType.TEXT : FileType.EXCEL;
    }
}

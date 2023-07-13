using FileAccessProject.ServiceApp;
using static FileReporterApp.ServiceApp.options.EnumConverter;
namespace FileReporterApp.ServiceApp
{
    public class FileReporterFactory
    {
        public static FileReporterSystemApp CreateReporterService(string destinationPath, string targetPath, DateTime dateTime, 
                                                                  string dateOptName, int threadCount, string fileOptName, List<string> otherOpts)
        {
            return new FileReporterSystemApp.Builder()
                    .SetBasicOption(ToBasicOption(fileOptName))
                    .SetOtherOptions(ToOtherOptionList(otherOpts))
                    .SetDateOption((ToDateOption(dateOptName)))
                    .SetDestinationPath(destinationPath)
                    .SetTargetPath(targetPath)
                    .setDateTime(dateTime)
                    .SetThreadCount(threadCount)
                    .Build();
        }
    }
}
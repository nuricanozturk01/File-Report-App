using FileAccessProject.ServiceApp;
using FileReporterApp.ServiceApp.options;

namespace FileReporterApp.ServiceApp
{
    public class FileReporterFactory
    {
        public static FileReporterSystemApp CreateReporterService(string destinationPath, string targetPath, DateTime dateTime, string dateOptName, int threadCount, string fileOptName, List<string> otherOpts)
        {
            return new FileReporterSystemApp.Builder()
                    .SetBasicOption(EnumConverter.ToBasicOption(fileOptName))
                    .SetOtherOptions(EnumConverter.ToOtherOptionList(otherOpts))
                    .SetDateOption((EnumConverter.ToDateOption(dateOptName)))
                    .SetDestinationPath(destinationPath)
                    .SetTargetPath(targetPath)
                    .setDateTime(dateTime)
                    .SetThreadCount(threadCount)
                    .Build();
        }
    }
}
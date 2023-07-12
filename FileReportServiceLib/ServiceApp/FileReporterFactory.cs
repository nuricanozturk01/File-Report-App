using FileAccessProject.ServiceApp;
using FileReporterApp.ServiceApp.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterApp.ServiceApp
{
    public class FileReporterFactory
    {
        public static FileReporterSystemApp CreateReporterService(string destinationPath, string targetPath, RadioButton dateOpt, int threadCount, RadioButton fileOpt, List<CheckBox> otherOpts)
        {
            return new FileReporterSystemApp.Builder()
                    .SetBasicOption(EnumConverter.ToBasicOption(fileOpt.Name))
                    .SetOtherOptions(EnumConverter.ToOtherOptionList(otherOpts.Select(opt => opt.Name).ToList()))
                    .SetDateOption((EnumConverter.ToDateOption(dateOpt.Name)))
                    .SetDestinationPath(destinationPath)
                    .SetTargetPath(targetPath)
                    .SetThreadCount(threadCount)
                    .Build();
        }
    }
}

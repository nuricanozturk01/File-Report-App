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
                    .SetBasicOption(OptionConverter.ToBasicOption(fileOpt.Name))
                    .SetOtherOptions(OptionConverter.ToOtherOptionList(otherOpts.Select(opt => opt.Name).ToList()))
                    .SetDateOption((OptionConverter.ToDateOption(dateOpt.Name)))
                    .SetDestinationPath(destinationPath)
                    .SetTargetPath(targetPath)
                    .SetThreadCount(threadCount)
                    .Build();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReportServiceLib.ServiceApp.FileOperation
{
    internal interface IFileOperation
    {
       public Task CopyFile(Dictionary<string, FileInfo> map, int counter, int totalFileCount, DirectoryInfo directoryInfo, string targetPath, double totalByte
           ,Action<double, string> apply1, Action<TimeSpan, int, double, double> showFinishMessageCallback);
    }
}

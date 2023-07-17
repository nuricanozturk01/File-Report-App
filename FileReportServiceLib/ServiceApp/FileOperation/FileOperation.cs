using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReportServiceLib.ServiceApp.FileOperation
{
    internal class FileOperation : IFileOperation

    {
        public async Task CopyFile(Dictionary<string, FileInfo> map, int counter, int totalFileCount, DirectoryInfo directoryInfo,string targetPath, 
            double totalByte, Action<double, string> showMessageCallback, Action<TimeSpan, int, double, double> showFinishMessageCallback)
        {
            var stopWatch = new Stopwatch();
            long totalLength = 0;

            stopWatch.Start();
            
            await Task.Run(() => map.AsParallel().ForAll(async x =>
            {
                totalLength += x.Value.Length;
                File.Copy(x.Key, x.Value.FullName.Replace(directoryInfo.FullName, targetPath), true);

                showMessageCallback.Invoke(totalLength, x.Value.FullName);
            }));

            stopWatch.Stop();
            showFinishMessageCallback.Invoke(stopWatch.Elapsed, counter, totalLength, totalByte);
        }
    }
}

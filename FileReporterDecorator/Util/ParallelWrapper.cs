using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterDecorator.Util
{
    internal class ParallelWrapper
    {
        public static void ForEachParallel(ConcurrentBag<string> fileList, int threadCount, Action<string> action)
        {
            Parallel.ForEach(fileList, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, file =>
            {
                action.Invoke(file);
            });
        }

        public static void ForEachParallel(List<DirectoryInfo> fileList, int threadCount, Action<DirectoryInfo> action)
        {
            Parallel.ForEach(fileList, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, file =>
            {
                action.Invoke(file);
            });
        }

    }
}

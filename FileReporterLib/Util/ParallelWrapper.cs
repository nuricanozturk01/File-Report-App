using System.Collections.Concurrent;

namespace FileReporterDecorator.Util
{
    /*
     * 
     * This class written for wrap the Parallel.ForEach. I write this class because, code looks like ugly and too long.
     * 
     */
    internal class ParallelWrapper
    {
        public static void ForEachParallel(ConcurrentBag<string> fileList, int threadCount, Action<string> action)
        {
            Parallel.ForEach(fileList, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, file => action.Invoke(file));
        }




        public static void ForEachParallel(List<DirectoryInfo> fileList, int threadCount, Action<DirectoryInfo> action)
        {
            Parallel.ForEach(fileList, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, file => action.Invoke(file));
        }
    }
}

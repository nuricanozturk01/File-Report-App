using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReportServiceLib.Util
{
    public static class NumberUtil
    {
        public static int GetPerShowNumber(int fileCount)
        {
            if (fileCount <= 9)
                return 5;
            if (fileCount > 10 && fileCount <= 99)
                return 10;
            if (fileCount > 100 && fileCount <= 999)
                return 100;

            else return (int)Math.Pow(10, Math.Log10(fileCount) + 1);
        }
    }
}

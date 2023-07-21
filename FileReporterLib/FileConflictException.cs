using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterLib
{
    public class FileConflictException : Exception
    {
        public FileConflictException(string message) : base(message) 
        {
            
        }
    }
}

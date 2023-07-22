using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterDecorator.Exceptions
{
    internal class TargetPathNullException : Exception
    {
        public TargetPathNullException(string message) : base(message) 
        {
        }
    }
}

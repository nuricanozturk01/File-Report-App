using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterDecorator.Exceptions
{
    internal class InvalidThreadCountException : Exception
    {
        public InvalidThreadCountException(string message) : base(message) 
        {
            MessageBox.Show(message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

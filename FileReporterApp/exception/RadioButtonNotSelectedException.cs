using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterApp.exception
{
    internal class RadioButtonNotSelectedException : Exception
    {
        private readonly Label label;
        public RadioButtonNotSelectedException(string? message) : base(message)
        {
            
        }

        public void CreateMessage() => label.Text = Message.ToUpper();
    }
}

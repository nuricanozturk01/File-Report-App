using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterApp.exception
{
    internal class RadioButtonNotSelectedException : Exception
    {
        private readonly ListBox listBox;
        public RadioButtonNotSelectedException(string? message, ListBox listBox) : base(message)
        {
            this.listBox = listBox;
        }

        public void CreateMessage() => listBox.Items.Add(Message.ToUpper());
    }
}

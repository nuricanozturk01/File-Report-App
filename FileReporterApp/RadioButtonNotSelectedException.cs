using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileReporterApp
{
    public class RadioButtonNotSelectedException : Exception
    {
        private readonly ListBox listBox;
        public RadioButtonNotSelectedException(string? message, ListBox listBox) : base(message)
        {
            this.listBox = listBox;
        }

        public void createMessage() => listBox.Items.Add(Message.ToUpper());


    }
}

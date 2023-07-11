namespace FileReporterApp.exception
{
    public class RadioButtonNotSelectedException : Exception
    {
        private readonly ListBox listBox;
        public RadioButtonNotSelectedException(string? message, ListBox listBox) : base(message)
        {
            this.listBox = listBox;
        }

        public void CreateMessage() => listBox.Items.Add(Message.ToUpper());


    }
}

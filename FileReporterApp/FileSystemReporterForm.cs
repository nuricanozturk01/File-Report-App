using FileAccessProject.ServiceApp;
using FileReporterApp.ServiceApp.options;
using System.Diagnostics;

namespace FileReporterApp
{
    public partial class FileSystemReporterForm : Form
    {
        private FileReporterSystemApp _fileReportService;


        public FileSystemReporterForm()
        {
            InitializeComponent();
        }

        /**
         *  Click on the Browse Button for Destination Path 
         */

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                PathTextBox.Text = folderBrowser.SelectedPath;
        }

        // File and Other Options selection problems
        private void RunButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResultListBox.Items.Clear();

                var destinationPath = PathTextBox.Text;
                var targetPath = TargetPathTextBox.Text;
                var dateOpt = DateOptionGroup.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);

                var threadCount = Decimal.ToInt32(ThreadCounter.Value);
                var fileOpt = OptionsGroup.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
                var otherOpts = OtherOptionsGroup.Controls.OfType<CheckBox>().Where(rb => rb.Checked).ToList();

                if (dateOpt is null)
                    throw new RadioButtonNotSelectedException("Please Select the Date option!", ResultListBox);
                if (fileOpt is null)
                    throw new RadioButtonNotSelectedException("Please Select the File option!", ResultListBox);

                _fileReportService = createFilterService(destinationPath, targetPath, dateOpt, threadCount, fileOpt, otherOpts);

                var startTime = Stopwatch.GetTimestamp();
                var beforeFileList = _fileReportService.GetFiles(DateTimePicker.Value, TimeEnum.BEFORE);
                var afterFileList = _fileReportService.GetFiles(DateTimePicker.Value, TimeEnum.AFTER);
                var finishTime = Stopwatch.GetTimestamp();

                addToListBox(beforeFileList, afterFileList);

                ResultListBox.Items.Add("Scan was completed! Total Elapsed Time: " + String.Format("{0}", TimeSpan.FromMilliseconds(finishTime - startTime).ToString(@"hh\:mm\:ss")));

            }
            catch (RadioButtonNotSelectedException ex)
            {
                ex.createMessage();
            }

        }

        private void addToListBox(IEnumerable<FileInfo> beforeFileList, IEnumerable<FileInfo> afterFileList)
        {
            ResultListBox.Items.Add(beforeFileList.Count() + afterFileList.Count() + " items were scanned!");
            ResultListBox.Items.Add("Before Date");

            beforeFileList.ToList().ForEach(beforeFile => ResultListBox.Items.Add(beforeFile));
            ResultListBox.Items.Add("");
            ResultListBox.Items.Add("");
            ResultListBox.Items.Add("After Date\n");

            afterFileList.ToList().ForEach(beforeFile => ResultListBox.Items.Add(beforeFile));
            ResultListBox.Items.Add("");
            ResultListBox.Items.Add("");
        }

        private FileReporterSystemApp createFilterService(string destinationPath, string targetPath, RadioButton dateOpt, int threadCount, RadioButton fileOpt, List<CheckBox> otherOpts)
        {
            return new FileReporterSystemApp.Builder()
                    .setBasicOption(OptionConverter.toBasicOption(fileOpt.Name))
                    .setOtherOptions(OptionConverter.toOtherOptionList(otherOpts.Select(opt => opt.Name).ToList()))
                    .setDateOption((OptionConverter.toDateOption(dateOpt.Name)))
                    .setDestinationPath(destinationPath)
                    .setTargetPath(targetPath)
                    .setThreadCount(threadCount)
                    .build();
        }

        private void browseTargetButton_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && (myStream = saveFileDialog1.OpenFile()) != null)
                myStream.Close();

            TargetPathTextBox.Text = Path.GetFullPath(saveFileDialog1.FileName);
        }

        private void ReportButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NOT IMPLEMENTED YET");
            throw new NotImplementedException("NOT IMPLEMENTED YET");
        }
    }
}
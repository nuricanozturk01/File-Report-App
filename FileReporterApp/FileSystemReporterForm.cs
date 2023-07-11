using FileAccessProject.ServiceApp;
using FileReporterApp.exception;
using FileReporterApp.ServiceApp;
using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.options;
using System.Diagnostics;

namespace FileReporterApp
{
    public partial class FileSystemReporterForm : Form
    {
        private FileReporterSystemApp _fileReportService;
        private readonly IFileWriteService _fileWriteService;
        public FileSystemReporterForm()
        {
            InitializeComponent();
            _fileWriteService = new FileWriterService();
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

                _fileReportService = FileReporterFactory.CreateReporterService(destinationPath, targetPath, dateOpt, threadCount, fileOpt, otherOpts);

                var startTime = Stopwatch.GetTimestamp();
                var beforeFileList = _fileReportService.GetFiles(DateTimePicker.Value, TimeEnum.BEFORE);
                var afterFileList = _fileReportService.GetFiles(DateTimePicker.Value, TimeEnum.AFTER);
                var finishTime = Stopwatch.GetTimestamp();

                Scan(afterFileList, beforeFileList, startTime, finishTime);

                /*if (CopyRadioButton.Checked)
                    CopyNewFilesToTarget(afterFileList, targetPath, OverwriteChoiceBox.Checked, EmptyFoldersChoiceBox.Checked, NtfsChoiceBox.Checked);
                if (MoveRadioButton.Checked)
                    MoveNewFilesToTarget(afterFileList, targetPath, OverwriteChoiceBox.Checked, EmptyFoldersChoiceBox.Checked, NtfsChoiceBox.Checked);
            */
                }
            catch (RadioButtonNotSelectedException ex)
            {
                ex.CreateMessage();
            }
        }

        private void MoveNewFilesToTarget(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            throw new NotImplementedException();
        }

        private void CopyNewFilesToTarget(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders)
        {
            throw new NotImplementedException();
        }

        private void Scan(IEnumerable<FileInfo> afterFileList, IEnumerable<FileInfo> beforeFileList, long startTime, long finishTime)
        {
            AddToListBox(beforeFileList, afterFileList);

            ResultListBox.Items.Add("Scan was completed! Total Elapsed Time: " + String.Format("{0}", TimeSpan.FromMilliseconds(finishTime - startTime).ToString(@"hh\:mm\:ss")));
        }

        private void AddToListBox(IEnumerable<FileInfo> beforeFileList, IEnumerable<FileInfo> afterFileList)
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

        private void BrowseTargetButton_Click(object sender, EventArgs e)
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
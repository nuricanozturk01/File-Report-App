using FileAccessProject.ServiceApp;
using FileReporterApp.exception;
using FileReporterApp.ServiceApp;
using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.options;
using System;
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


                var beforeFileList = _fileReportService.GetFiles(DateTimePicker.Value, TimeEnum.BEFORE);
                var afterFileList = _fileReportService.GetFiles(DateTimePicker.Value, TimeEnum.AFTER);

                Enumerable.Range(0, 5).ToList().ForEach(i => ResultListBox.Items.Add(""));

                Scan(beforeFileList.Concat(afterFileList).ToList());


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

        private async void Scan(List<FileInfo> mergedList)
        {

            if (ResultListBox.InvokeRequired)
            {
                ResultListBox.Invoke(() => Scan(mergedList));
                return;
            }

            var startTime = Stopwatch.GetTimestamp();

            for (var i = 0; i < mergedList.Count; ++i)
            {
                ResultListBox.Items[0] = (i + 1) + " items were scanned!";
                ResultListBox.Items[2] = mergedList[i];
                await Task.Delay(45);
            }
                
            var finishTime = Stopwatch.GetTimestamp();
            
            ResultListBox.Items[4] = "Scan was completed! Total Elapsed Time: " + String.Format("{0}", TimeSpan.FromMilliseconds(finishTime - startTime).ToString(@"hh\:mm\:ss"));
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
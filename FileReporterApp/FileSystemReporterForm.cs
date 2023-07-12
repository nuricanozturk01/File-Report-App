using FileAccessProject.ServiceApp;
using FileReporterApp.exception;
using FileReporterApp.ServiceApp;
using FileReporterApp.ServiceApp.options;
using System.Diagnostics;

namespace FileReporterApp
{
    public partial class FileSystemReporterForm : Form
    {
        private FileReporterSystemApp _fileReporterSystemService;
        private List<FileInfo> _scannedMergedFiles;
        public FileSystemReporterForm() => InitializeComponent();

        private void MoveRadioButton_CheckedChanged(object sender, EventArgs e) => browseTargetButton.Enabled = MoveRadioButton.Checked;
        private void CopyRadioButton_CheckedChanged(object sender, EventArgs e) => browseTargetButton.Enabled = CopyRadioButton.Checked;
        private void MoveNewFilesToTarget(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders) => _fileReporterSystemService.MoveFiles(afterFileList, targetPath, overwrite, ntfsPermission, emptyFolders);
        private void CopyNewFilesToTarget(IEnumerable<FileInfo> afterFileList, string targetPath, bool overwrite, bool ntfsPermission, bool emptyFolders) => _fileReporterSystemService.CopyFiles(afterFileList, targetPath, overwrite, ntfsPermission, emptyFolders);
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
                await Task.Delay(1);
            }
            
            var finishTime = Stopwatch.GetTimestamp();
            TargetPathTextBox.Text = "FINISH!";
            ResultListBox.Items[4] = "Scan was completed! Total Elapsed Time: " + String.Format("{0}", TimeSpan.FromMilliseconds(finishTime - startTime).ToString("hh\\:mm\\:ss"));
        }
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                PathTextBox.Text = folderBrowser.SelectedPath;
        }
        private void BrowseTargetButton_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                TargetPathTextBox.Text = folderBrowser.SelectedPath;
        }
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

                _fileReporterSystemService = FileReporterFactory.CreateReporterService(destinationPath, targetPath, dateOpt.Name, threadCount, 
                    fileOpt.Name, otherOpts.Select(fi => fi.Name).ToList());

                createOperation(targetPath);

                TargetPathTextBox.ResetText();
                PathTextBox.ResetText();
                OtherOptionsGroup.ResetText();
            }
            catch (RadioButtonNotSelectedException ex)
            {
                ex.CreateMessage();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Please select the path before run");
            }
        }
        private void createOperation(string targetPath)
        {
            Enumerable.Range(0, 5).ToList().ForEach(i => ResultListBox.Items.Add(""));

            var newFileList = _fileReporterSystemService.GetFiles(DateTimePicker.Value, TimeEnum.AFTER);
            _scannedMergedFiles = _fileReporterSystemService.GetFiles(DateTimePicker.Value, TimeEnum.BEFORE).Concat(newFileList).ToList();

            _fileReporterSystemService.SetScannedMergedList(_scannedMergedFiles);

            if (ScanRadioButton.Checked)
                Scan(_scannedMergedFiles);

            else Scan(newFileList.ToList());

            if (CopyRadioButton.Checked)
                CopyNewFilesToTarget(newFileList, targetPath, OverwriteChoiceBox.Checked, EmptyFoldersChoiceBox.Checked, NtfsChoiceBox.Checked);

            if (MoveRadioButton.Checked)
                MoveNewFilesToTarget(newFileList, targetPath, OverwriteChoiceBox.Checked, EmptyFoldersChoiceBox.Checked, NtfsChoiceBox.Checked);
        }
        private void ReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                Stream myStream;

                SaveDialog.Filter = "txt files (*.txt)|*.txt|Excel Files (*.xlsx)|*.xlsx";
                SaveDialog.FilterIndex = 2;
                SaveDialog.RestoreDirectory = true;

                if (SaveDialog.ShowDialog() == DialogResult.OK && (myStream = SaveDialog.OpenFile()) != null)
                    myStream.Close();

                RunButton_Click(sender, e);

                _fileReporterSystemService.ReportByFileFormat(EnumConverter.ToFileType(SaveDialog.FilterIndex), SaveDialog.FileName);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Please select the file!");
            }
            catch
            {
                MessageBox.Show("Please run the scan!");
            }
        }
    }
}

using FileAccessProject.ServiceApp;
using FileReporterApp.exception;
using FileReporterApp.ServiceApp;
using FileReporterApp.ServiceApp.options;
using System.Collections;
using System.Diagnostics;

namespace FileReporterApp
{
    public partial class FileSystemReporterForm : Form
    {
        private FileReporterSystemApp _fileReporterSystemService;
        private List<FileInfo> _scannedMergedFiles;
        private int scannedFileCounter;
        public FileSystemReporterForm() => InitializeComponent();

        private void StartApp(Action<List<FileInfo>>? reportAction)
        {
            try
            {
                var destinationPath = PathTextBox.Text;
                var targetPath = TargetPathTextBox.Text;
                var dateOpt = DateOptionGroup.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
                var threadCount = Decimal.ToInt32(ThreadCounter.Value);
                var fileOpt = OptionsGroup.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
                var otherOpts = OtherOptionsGroup.Controls.OfType<CheckBox>().Where(rb => rb.Checked).ToList();

                if (dateOpt is null)
                    throw new RadioButtonNotSelectedException("Please Select the Date option!");
                if (fileOpt is null)
                    throw new RadioButtonNotSelectedException("Please Select the File option!");

                _fileReporterSystemService = FileReporterFactory.CreateReporterService(destinationPath, targetPath, DateTimePicker.Value, dateOpt.Name, threadCount,
                    fileOpt.Name, otherOpts.Select(fi => fi.Name).ToList());

                CreateOperation(targetPath, reportAction);

                /* TargetPathTextBox.ResetText();
                 PathTextBox.ResetText();
                 OtherOptionsGroup.ResetText();*/
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
        private void RunButton_Click(object sender, EventArgs e) => StartApp(null);
        private void ReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                Stream myStream;

                SaveDialog.Filter = "txt files (*.txt)|*.txt|Excel Files (*.xlsx)|*.xlsx";
                SaveDialog.FilterIndex = 2;
                SaveDialog.RestoreDirectory = true;

                if (SaveDialog.ShowDialog() == DialogResult.OK && (myStream = SaveDialog.OpenFile()) != null)
                {
                    myStream.Close();
                    StartApp(fileList => _fileReporterSystemService.ReportByFileFormat(EnumConverter.ToFileType(SaveDialog.FilterIndex), SaveDialog.FileName, fileList));
                }
               
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
        private void MoveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            browseTargetButton.Enabled = MoveRadioButton.Checked;
            EmptyFoldersChoiceBox.Enabled = true;
            OverwriteChoiceBox.Enabled = true;
            NtfsChoiceBox.Enabled = false;
        }
        private void CopyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            browseTargetButton.Enabled = CopyRadioButton.Checked;
            EmptyFoldersChoiceBox.Enabled = true;
            OverwriteChoiceBox.Enabled = true;
            NtfsChoiceBox.Enabled = true;
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
        private void ScanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EmptyFoldersChoiceBox.Enabled = false;
            OverwriteChoiceBox.Enabled = false;
            NtfsChoiceBox.Enabled = false;
        }

        private void CreateOperation(string targetPath, Action<List<FileInfo>>? reportAction)
        {
            /*TimeLabel.ResetText();
            ScannedSizeLabel.ResetText();
            ScannigLabel.ResetText();*/

            if (ScanRadioButton.Checked)
                ScanStartAsync(false, null);

            if (reportAction is not null)
                ScanStartAsync(false, reportAction);

            if (CopyRadioButton.Checked)
                ScanAndCopyAsync(targetPath, OverwriteChoiceBox.Checked, EmptyFoldersChoiceBox.Checked, NtfsChoiceBox.Checked);

            if (MoveRadioButton.Checked)
                ScanAndMoveAsync(targetPath, OverwriteChoiceBox.Checked, EmptyFoldersChoiceBox.Checked, NtfsChoiceBox.Checked);
        }

        private async void ScanStartAsync(bool isBefore, Action<List<FileInfo>>? reportAction)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var dirInfo = new DirectoryInfo(PathTextBox.Text);

            await Scan(dirInfo, TimeEnum.AFTER, scannedFileCounter, reportAction);

            if (isBefore)
                await Scan(dirInfo, TimeEnum.BEFORE, scannedFileCounter, reportAction);

            stopWatch.Stop();

            TimeLabel.Text = "Scan was completed! Total Elapsed Time: " + stopWatch.Elapsed;
        }
        private async Task Scan(DirectoryInfo directoryInfo, TimeEnum timeEnum, int counter, Action<List<FileInfo>> reportAction)
        {
            var list = new List<FileInfo>();

            var directories = new Stack<string>();
            var totalFileCount = Directory.GetFiles(directoryInfo.FullName, "*.*", SearchOption.AllDirectories).Length;

            directories.Push(directoryInfo.FullName);

            while (directories.Count > 0)
            {
                var currentDirectory = directories.Pop();
                var di = new DirectoryInfo(currentDirectory);

                foreach (var file in di.GetFiles())
                {
                    if (_fileReporterSystemService.Filter(file, DateTimePicker.Value, timeEnum))
                    {
                        if (reportAction != null)
                            list.Add(file);
                        if (++counter % 1000 == 0)
                        {
                            ScannedSizeLabel.Text = counter + " items were scanned!";
                            ScannigLabel.Text = file.FullName;
                            ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)counter / (double)totalFileCount) * 100.0);
                            await Task.Delay(1);
                        }
                    }
                }


                foreach (var directory in di.GetDirectories())
                    directories.Push(directory.FullName);


            }

            if (counter < 1000)
            {
                ScannedSizeLabel.Text = counter + " items were scanned!";
                ScannigLabel.Text = directoryInfo.FullName;
            }
            ScanProgressBar.Value = ScanProgressBar.Maximum;
            reportAction?.Invoke(list);
        }

        private void MoveNewFilesToTarget(string targetPath, bool overwrite, bool emptyFolders, bool ntfsPermission)
        {
            _fileReporterSystemService.MoveFilesAnother(targetPath, overwrite, ntfsPermission, emptyFolders);
        }

        private void CopyNewFilesToTarget(string targetPath, bool overwrite, bool emptyFolders, bool ntfsPermission)
        {
            _fileReporterSystemService.CopyFilesAnother(targetPath, overwrite, ntfsPermission, emptyFolders);
        }

        private void ScanAndMoveAsync(string targetPath, bool overwrite, bool emptyFolders, bool ntfsPermissions)
        {
            throw new NotImplementedException();
        }

        private void ScanAndCopyAsync(string targetPath, bool overwrite, bool emptyFolders, bool ntfsPermissions)
        {
            throw new NotImplementedException();
        }

    }
}
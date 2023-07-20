using DocumentFormat.OpenXml.Drawing;
using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.options;
using FileReportServiceLib.ServiceApp.filter;
using System.Collections.Concurrent;
using System.Diagnostics;
using static FileReportServiceLib.Util.OptionCreator;

namespace FileReporterApp
{
    internal class COUNTER_LOCK
    {
        public int COUNTER = 0;
        public COUNTER_LOCK() { }
    }

    public partial class FileSystemReporterForm : Form
    {
        private ConcurrentBag<string> _newFileList;
        private ConcurrentBag<string> _oldFileList;
        private ConcurrentBag<string> _directoryList;
        private static COUNTER_LOCK _locker = new();

        public FileSystemReporterForm()
        {
            InitializeComponent();
            _newFileList = new();
            _oldFileList = new();
            _directoryList = new();
        }

        private async Task Scan(Action<List<FileInfo>, List<FileInfo>, FileType, string>? reportAction, Action<int, string>? copyAction, Action<int, string>? moveAction)
        {
            try
            {
                _newFileList = new();
                _oldFileList = new();
                _directoryList = new();

                _locker.COUNTER = 0;

                var dateOption = GetSelectedDateOption(CreatedDateRadioButton.Checked, ModifiedDateRadioButton.Checked);

                var totalFileCount = new DirectoryInfo(PathTextBox.Text).GetFiles("*.*", SearchOption.AllDirectories).Length;

                var stopWatch = new Stopwatch();

                stopWatch.Start();

                await Task.Run(() => ScanFileSystemAndClassifyFiles(PathTextBox.Text, dateOption, totalFileCount));

                stopWatch.Stop();

                RequireInvoke(() =>
                {
                    ScanProgressBar.Value = ScanProgressBar.Maximum;
                    ScannedSizeLabel.Text = _locker.COUNTER + " items were scanned!";
                    TimeLabel.Text = "Scan was completed! Total Elapsed Time: " + stopWatch.Elapsed;
                });

                await CreateOperation(totalFileCount, copyAction, reportAction, moveAction);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("You do not have access to Directory!");
            }
        }
        private async Task CreateOperation(int totalFileCount, Action<int, string>? copyAction, Action<List<FileInfo>, List<FileInfo>, FileType, string>? reportAction, Action<int, string>? moveAction)
        {
            if (copyAction != null)
                await Task.Run(() => copyAction.Invoke(totalFileCount, TargetPathTextBox.Text));

            if (reportAction != null)
            {
                var newFiles = _newFileList.Select(nf => new FileInfo(nf)).ToList();
                var oldFiles = _oldFileList.Select(of => new FileInfo(of)).ToList();

                await Task.Run(() => reportAction.Invoke(newFiles, oldFiles, GetFileFormat(SaveDialog.FilterIndex), SaveDialog.FileName));
            }
            if (moveAction != null)
                await Task.Run(() => moveAction.Invoke(totalFileCount, TargetPathTextBox.Text));
        }
        public void ScanFileSystemAndClassifyFiles(string path, IDateOption dateOption, int totalFileCount)
        {

            if (EmptyFoldersChoiceBox.Checked && dateOption.setDate(new DirectoryInfo(path), DateTimePicker.Value))
                _directoryList.Add(path);

            foreach (string file in Directory.GetFiles(path))
            {
                ClassifyBySelectedDate(file, dateOption, totalFileCount);

                lock (_locker)
                {
                    _locker.COUNTER++;
                }
            }

            Parallel.ForEach(Directory.GetDirectories(path), new ParallelOptions { MaxDegreeOfParallelism = (int)ThreadCounter.Value }, subDir =>
            {
                var name = new DirectoryInfo(subDir);

                if (name.GetFiles().Length != 0 && dateOption.setDate(new DirectoryInfo(path), DateTimePicker.Value))
                    _directoryList.Add(name.FullName);

                if (String.IsNullOrWhiteSpace(name.Name) || name.Name is null)
                    return;

                ScanFileSystemAndClassifyFiles(subDir, dateOption, totalFileCount);
            });
        }
        private void ClassifyBySelectedDate(string file, IDateOption dateOption, int totalFileCount)
        {
            if (_locker.COUNTER % 100 == 0)
            {
                RequireInvoke(() =>
                {
                    ScannedSizeLabel.Text = _locker.COUNTER + " items were scanned!";
                    ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)_locker.COUNTER / (double)totalFileCount) * 100.0);
                    ScannigLabel.Text = file;
                });
            }
            if (file == "C:\\Users\\hp\\Desktop\\New folder\\ARM-Assembly.pdf")
                RequireInvoke(() => ReportButton.Text = new FileInfo(file).CreationTime.Date.ToString());

            if (dateOption.setDate(new FileInfo(file), DateTimePicker.Value))
                _newFileList.Add(file);

            else _oldFileList.Add(file);
        }

        private async void RunButton_Click(object sender, EventArgs e)
        {
            _locker.COUNTER = 0;

            if (CopyRadioButton.Checked)
                await Scan(null, async (totalFile, targetPath) => await CopyFile(totalFile, targetPath), null);

            if (MoveRadioButton.Checked)
                await Scan(null, null, async (totalFile, targetPath) => await MoveFile(totalFile, targetPath));

            else await Task.Run(() => Scan(null, null, null));
        }
        private async void ReportButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                ThreadCounter.Value = 4;
                Stream myStream;

                SaveDialog.Filter = "txt files (*.txt)|*.txt|Excel Files (*.xlsx)|*.xlsx";
                SaveDialog.FilterIndex = 2;
                SaveDialog.RestoreDirectory = true;

                if (SaveDialog.ShowDialog() == DialogResult.OK && (myStream = SaveDialog.OpenFile()) != null)
                {
                    myStream.Close();
                    await Scan((newFiles, oldFiles, fileFormat, targetPath) => FileWriter.WriteFile(newFiles, oldFiles, fileFormat, targetPath), null, null);
                    ScanProgressBar.Value = ScanProgressBar.Maximum;
                    TimeLabel.Text = "Report is ready!";
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

        private async Task CopyFile(int totalFileCount, string targetPath)
        {
            _directoryList.ToList().ForEach(d => Directory.CreateDirectory(d.Replace(PathTextBox.Text, TargetPathTextBox.Text)));

            RequireInvoke(() => ScanProgressBar.Value = ScanProgressBar.Minimum);

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(() => CopyFileCallback(totalFileCount));

            stopWatch.Stop();

            RequireInvoke(() => TimeLabel.Text = "Copy Operation was completed! Total Elapsed Time: " + stopWatch.Elapsed);
        }
        private void CopyFileCallback(int totalFileCount)
        {
            var isConflict = false;
            var conflictFileList = new ConcurrentBag<string>();
            var _newLocker = new COUNTER_LOCK();

            Parallel.ForEach(_newFileList, new ParallelOptions { MaxDegreeOfParallelism = (int)ThreadCounter.Value }, file =>
            {
                lock (_newLocker)
                {
                    _newLocker.COUNTER++;
                }
                ShowOnScreenProgress(file, totalFileCount, _newLocker.COUNTER);

                var targetFile = file.Replace(PathTextBox.Text, TargetPathTextBox.Text);
                try
                {
                    File.Copy(file, targetFile, OverwriteChoiceBox.Checked);

                    if (NtfsChoiceBox.Checked)
                        CopyNtfsPermissions(new FileInfo(file), new FileInfo(file.Replace(PathTextBox.Text, TargetPathTextBox.Text)));

                }
                catch (IOException ex)
                {
                    isConflict = true;
                    conflictFileList.Add(file);
                }
            });

            if (isConflict)
                MessageBox.Show($"{conflictFileList.Count} files are conflicted!", "Conflicted Files", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);

        }
        private void CopyNtfsPermissions(FileInfo sourceFile, FileInfo targetFile)
        {
            var security = sourceFile.GetAccessControl();
            security.SetAccessRuleProtection(true, true);
            targetFile.SetAccessControl(security);
        }

        private async Task MoveFile(int totalFileCount, string targetPath)
        {
            _directoryList.ToList().ForEach(d => Directory.CreateDirectory(d.Replace(PathTextBox.Text, TargetPathTextBox.Text)));
            File.WriteAllLines("C:\\Users\\hp\\Desktop\\Deneme.txt", _newFileList);
            RequireInvoke(() => ScanProgressBar.Value = ScanProgressBar.Minimum);

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(() => MoveFileCallback(totalFileCount));

            stopWatch.Stop();

            RequireInvoke(() => TimeLabel.Text = "Copy Operation was completed! Total Elapsed Time: " + stopWatch.Elapsed);
        }
        private void MoveFileCallback(int totalFileCount)
        {
            
            var _moveCounter = new COUNTER_LOCK();
            Parallel.ForEach(_newFileList, new ParallelOptions { MaxDegreeOfParallelism = (int)ThreadCounter.Value}, file =>
            {
                try
                {
                    ShowOnScreenProgress(file, totalFileCount, _moveCounter.COUNTER);
                    File.Move(file, file.Replace(PathTextBox.Text, TargetPathTextBox.Text), OverwriteChoiceBox.Checked);
                    lock (_moveCounter)
                    {
                        _moveCounter.COUNTER++;
                    }
                }
                catch (Exception ex) { }
            });


            Parallel.ForEach(_directoryList.Select(d => new DirectoryInfo(d)), new ParallelOptions { MaxDegreeOfParallelism = (int)ThreadCounter.Value },
                dir =>
                    {
                        try
                        {
                            if (dir.GetFiles().Length == 0)
                                dir.Delete(true);
                        }
                        catch (Exception ex) { }
                    });
        }
        private void ShowOnScreenProgress(string file, int totalFileCount)
        {
            if (_locker.COUNTER % 100 == 0)
            {
                RequireInvoke(() =>
                {
                    ScannedSizeLabel.Text = _locker.COUNTER + " items were scanned!";
                    ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)_locker.COUNTER / (double)totalFileCount) * 100.0);
                    ScannigLabel.Text = file;
                });
            }
        }
        private void ShowOnScreenProgress(string file, int totalFileCount, int counter)
        {
            if (counter % 100 == 0)
            {
                RequireInvoke(() =>
                {
                    ScannedSizeLabel.Text = _locker.COUNTER + " items were scanned!";
                    ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)_locker.COUNTER / (double)totalFileCount) * 100.0);
                    ScannigLabel.Text = file;
                });
            }
        }
        private void RequireInvoke(Action invoke)
        {
            if (InvokeRequired)
            {
                Invoke(invoke);
                return;
            }
        }
    }
}
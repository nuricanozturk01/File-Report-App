using FileReporterApp.ServiceApp.FileWriter;
using FileReporterApp.ServiceApp.options;
using FileReportServiceLib.ServiceApp.filter;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace FileReporterApp
{
    internal class LOCK_COUNTER
    {
        public int COUNTER = 0;
        public LOCK_COUNTER() { }
    }
    public partial class FileSystemReporterForm : Form
    {

        private readonly ConcurrentBag<string> newFiles;
        private readonly ConcurrentBag<string> oldFiles;
        private readonly ConcurrentBag<string> directories;
        private static LOCK_COUNTER _locker = new LOCK_COUNTER();

        public FileSystemReporterForm()
        {
            InitializeComponent();
            newFiles = new ConcurrentBag<string>();
            oldFiles = new ConcurrentBag<string>();
            directories = new ConcurrentBag<string>();
        }

        private async Task Scan(Action<List<FileInfo>, List<FileInfo>, FileType, string>? reportAction,
            Action<int, string>? copyAction, Action<int, string>? moveAction)
        {
            _locker.COUNTER = 0;

            var dateOption = GetSelectedDateOption();

            var totalFileCount = new DirectoryInfo(PathTextBox.Text).GetFiles("*.*", SearchOption.AllDirectories).Length;

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(() => ScanFileSystemAndClassifyFiles(PathTextBox.Text, dateOption, totalFileCount));

            stopWatch.Stop();

            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    ScanProgressBar.Value = ScanProgressBar.Maximum;
                    ScannedSizeLabel.Text = _locker.COUNTER + " items were scanned!";
                    TimeLabel.Text = "Scan was completed! Total Elapsed Time: " + stopWatch.Elapsed;
                });
                return;
            }
            if (copyAction != null)
                await Task.Run(() => copyAction.Invoke(totalFileCount, TargetPathTextBox.Text));

            if (reportAction != null)
                await Task.Run(() => reportAction.Invoke(newFiles.Select(nf => new FileInfo(nf)).ToList(), oldFiles.Select(of => new FileInfo(of)).ToList(), SaveDialog.FilterIndex == 1 ? FileType.TEXT : FileType.EXCEL,
                    SaveDialog.FileName));
            if (moveAction != null)
                await Task.Run(() => moveAction.Invoke(totalFileCount, TargetPathTextBox.Text));
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
                    await Scan((nf, of, ft, path) => FileWriter.WriteFile(nf, of, ft, path), null, null);
                    ScanProgressBar.Value = ScanProgressBar.Maximum;
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
        private async Task CopyFile(int totalFileCount, string targetPath)
        {
            directories.ToList().ForEach(d => Directory.CreateDirectory(d.Replace(PathTextBox.Text, TargetPathTextBox.Text)));
            requireInvoke(() => ScanProgressBar.Value = ScanProgressBar.Minimum);

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(() => CopyFileCallback(totalFileCount));

            stopWatch.Stop();

            requireInvoke(() => TimeLabel.Text = "Copy Operation was completed! Total Elapsed Time: " + stopWatch.Elapsed);
        }

        private void requireInvoke(Action invoke)
        {
            if (InvokeRequired)
            {
                Invoke(invoke);
                return;
            }
        }
        private void CopyFileCallback(int totalFileCount)
        {
            Parallel.ForEach(newFiles, new ParallelOptions { MaxDegreeOfParallelism = (int)ThreadCounter.Value }, file =>
            {
                showOnScreenProgress(file, totalFileCount);
                File.Copy(file, file.Replace(PathTextBox.Text, TargetPathTextBox.Text), OverwriteChoiceBox.Checked);
                lock (_locker)
                {
                    _locker.COUNTER++;
                }
            });
        }
        private void showOnScreenProgress(string file, int totalFileCount)
        {
            if (_locker.COUNTER % 100 == 0)
            {
                requireInvoke(() =>
                {
                    ScannedSizeLabel.Text = _locker.COUNTER + " items were scanned!";
                    ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)_locker.COUNTER / (double)totalFileCount) * 100.0);
                    ScannigLabel.Text = file;
                });
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
        private async Task MoveFile(int totalFileCount, string targetPath)
        {
            directories.ToList().ForEach(d => Directory.CreateDirectory(d.Replace(PathTextBox.Text, TargetPathTextBox.Text)));
            requireInvoke(() => ScanProgressBar.Value = ScanProgressBar.Minimum);

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(() => MoveFileCallback(totalFileCount));

            stopWatch.Stop();

            requireInvoke(() => TimeLabel.Text = "Copy Operation was completed! Total Elapsed Time: " + stopWatch.Elapsed);
        }


        public void ScanFileSystemAndClassifyFiles(string path, IDateOption dateOption, int totalFileCount)
        {

            if (EmptyFoldersChoiceBox.Checked && dateOption.setDate(new DirectoryInfo(path), DateTimePicker.Value))
                directories.Add(path);

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

                if (name.GetFiles().Length != 0)
                    directories.Add(name.FullName);

                if (String.IsNullOrWhiteSpace(name.FullName) || name.FullName is null)
                    return;

                ScanFileSystemAndClassifyFiles(subDir, dateOption, totalFileCount);
            });
        }
        private IDateOption GetSelectedDateOption()
        {
            if (CreatedDateRadioButton.Checked)
                return new CreationDateOption();

            else if (ModifiedDateRadioButton.Checked)
                return new ModifiedDateOption();

            return new LastAccessDateOption();
        }

        private void ClassifyBySelectedDate(string file, IDateOption dateOption, int totalFileCount)
        {
            if (_locker.COUNTER % 100 == 0)
            {
                if (InvokeRequired)
                {
                    Invoke(() =>
                    {
                        ScannedSizeLabel.Text = _locker.COUNTER + " items were scanned!";
                        ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)_locker.COUNTER / (double)totalFileCount) * 100.0);
                        ScannigLabel.Text = file;
                    });
                    return;
                }
            }
            if (dateOption.setDate(new FileInfo(file), DateTimePicker.Value))
                newFiles.Add(file);

            else oldFiles.Add(file);
        }

        private async void MoveFileCallback(int totalFileCount)
        {
            Parallel.ForEach(newFiles, new ParallelOptions { MaxDegreeOfParallelism = (int)ThreadCounter.Value }, file =>
            {
                try
                {
                    showOnScreenProgress(file, totalFileCount);
                    File.Move(file, file.Replace(PathTextBox.Text, TargetPathTextBox.Text), OverwriteChoiceBox.Checked);
                    lock (_locker)
                    {
                        _locker.COUNTER++;
                    }
                }
                catch (Exception ex) { }
            });

            Parallel.ForEach(directories.Select(d => new DirectoryInfo(d)), new ParallelOptions { MaxDegreeOfParallelism = (int)ThreadCounter.Value },
                dir =>
                    {
                        if (dir.GetFiles().Length == 0)
                            dir.Delete(true);
                    });
        }
    }


}


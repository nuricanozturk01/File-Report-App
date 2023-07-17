using DocumentFormat.OpenXml.Spreadsheet;
using FileAccessProject.ServiceApp;
using FileReporterApp.exception;
using FileReporterApp.ServiceApp;
using FileReporterApp.ServiceApp.options;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using FileReportServiceLib.Util;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileReporterApp
{
    public partial class FileSystemReporterForm : Form
    {
        private FileReporterSystemApp _fileReporterSystemService;
        private int scannedFileCounter;
        public FileSystemReporterForm()
        {
            InitializeComponent();
        }
        private void StartApp(Action<List<FileInfo>, List<FileInfo>>? reportAction)
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
                ReportButton.Text = dateOpt.Name;
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
                    StartApp((newFileList, oldFileList) => _fileReporterSystemService.ReportByFileFormat(EnumConverter.ToFileType(SaveDialog.FilterIndex), SaveDialog.FileName, newFileList, oldFileList));
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

        private void CreateOperation(string targetPath, Action<List<FileInfo>, List<FileInfo>>? reportAction)
        {
            if (ScanRadioButton.Checked)
                ScanStartAsync(null, null, null);

            if (reportAction is not null)
                ScanStartAsync(reportAction, null, null);

            if (CopyRadioButton.Checked)
                startCopy();

             if (MoveRadioButton.Checked)
                StartMove();
        }

        private async void StartMove()
        {
            await Scan(new DirectoryInfo(PathTextBox.Text), TimeEnum.AFTER, 0, null, null,
                async (map, totalFileCount, directoryInfo, targetPath, totalByte, dirs) =>
                    await MoveFile(map, totalFileCount, directoryInfo, TargetPathTextBox.Text, Convert.ToDouble(totalByte), dirs));
        }

        private async void startCopy()
        {
            await Scan(new DirectoryInfo(PathTextBox.Text), TimeEnum.AFTER, 0, null,
                async (map, totalFileCount, directoryInfo, targetPath, totalByte) =>
                    await CopyFile(map, totalFileCount, directoryInfo, TargetPathTextBox.Text, Convert.ToDouble(totalByte)), null);
        }
        private async void ScanStartAsync(Action<List<FileInfo>, List<FileInfo>>? reportAction, Action<Dictionary<string, FileInfo>, int, DirectoryInfo, string, double>? copyAction,
            Action<Dictionary<string, FileInfo>, int, DirectoryInfo, string, double, HashSet<string>>? moveAction)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            var dirInfo = new DirectoryInfo(PathTextBox.Text);

            await Scan(dirInfo, TimeEnum.AFTER, scannedFileCounter, reportAction, copyAction, moveAction);

            stopWatch.Stop();

            TimeLabel.Text = "Scan was completed! Total Elapsed Time: " + stopWatch.Elapsed;
        }
        private async Task Scan(DirectoryInfo directoryInfo, TimeEnum timeEnum, int counter, 
            Action<List<FileInfo>, List<FileInfo>>? reportAction,
            Action<Dictionary<string, FileInfo>, int, DirectoryInfo, string, double>? copyAction, 
            Action<Dictionary<string, FileInfo>, int, DirectoryInfo, string, double, HashSet<string>>? moveAction)
        {
            long totalByte = 0;
            Dictionary<string, FileInfo>? map = null;

            HashSet<string> directoryInfos = null;
            var directories = new Stack<string>();
            var totalFileCount = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).Length;

            if (copyAction != null || moveAction != null)
            {
                totalByte = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
                map = new Dictionary<string, FileInfo>();
                directoryInfos = new HashSet<string>();
            }
            directories.Push(directoryInfo.FullName);

            List<FileInfo>? newFileList = null;
            List<FileInfo>? oldFileList = null;

            if (reportAction != null)
            {
                newFileList = new List<FileInfo>();
                oldFileList = new List<FileInfo>();
            }

            while (directories.Count > 0)
            {
                var currentDirectory = directories.Pop();
                var di = new DirectoryInfo(currentDirectory);

                //Empty File
                if ((copyAction != null || moveAction != null) && di.GetFiles().Count() == 0 && _fileReporterSystemService.FilterDirectoryInfo(di, DateTimePicker.Value, timeEnum) && EmptyFoldersChoiceBox.Checked)
                {
                    Directory.CreateDirectory(TargetPathTextBox.Text);
                    directoryInfos.Add(di.FullName);
                }

                foreach (var file in di.GetFiles())
                {

                    if (_fileReporterSystemService.FilterFileInfo(file, DateTimePicker.Value, timeEnum))
                    {

         
                        if (reportAction is not null)
                            newFileList?.Add(file);

                        if (copyAction != null || moveAction != null)
                        {
                            Directory.CreateDirectory(di.FullName.Replace(directoryInfo.FullName, TargetPathTextBox.Text));
                            map?.Add(file.FullName, file);
                            directoryInfos.Add(di.FullName);
                        }
                       

                        if ((copyAction is null && moveAction is null) && ++counter % 1000 == 0)
                        {
                            ScannedSizeLabel.Text = counter + " items were scanned!";
                            ScannigLabel.Text = file.FullName;
                            ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)counter / (double)totalFileCount) * 100.0);
                            await Task.Delay(1);
                        }
                    }
                    else
                        if (reportAction != null)
                             oldFileList?.Add(file);
                }

                foreach (var directory in di.GetDirectories())
                    directories.Push(directory.FullName);
            }

            if (copyAction is null && moveAction is null)
            {
                if (counter < 1000)
                    ScannigLabel.Text = directoryInfo.FullName;

                ScannedSizeLabel.Text = counter + " items were scanned!";
                ScanProgressBar.Value = ScanProgressBar.Maximum;
            }
            reportAction?.Invoke(newFileList, oldFileList);
           // File.WriteAllLines("C:\\Users\\hp\\Desktop\\dirs2.txt", directoryInfos);
            copyAction?.Invoke(map, totalFileCount, directoryInfo, TargetPathTextBox.Text, Convert.ToDouble(totalByte));
            moveAction?.Invoke(map, totalFileCount, directoryInfo, TargetPathTextBox.Text, Convert.ToDouble(totalByte), directoryInfos);
            
            
        }
        double _toGB(double _byte) => (double)((_byte / 1024f) / 1024f / 1024f);
        

        private async Task CopyFile(Dictionary<string, FileInfo> map, int totalFileCount, DirectoryInfo directoryInfo, string targetPath, double totalByte)
        {
            totalByte = _toGB(totalByte);

            ScanProgressBar.Value = ScanProgressBar.Minimum;

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(() => copyFileCallback(directoryInfo, map, totalByte, targetPath, totalFileCount));

            stopWatch.Stop();
           
            TimeLabel.Text = "Scan was completed! Total Elapsed Time: " + stopWatch.Elapsed;
        }

        private async void copyFileCallback(DirectoryInfo directoryInfo, Dictionary<string, FileInfo> map, double totalByte, string targetPath, int totalFileCount)
        {
            int counter = 0;
            long totalLength = 0;
            map.AsParallel().ForAll(async x =>
            {
                totalLength += x.Value.Length;
                File.Copy(x.Key, x.Value.FullName.Replace(directoryInfo.FullName, targetPath), OverwriteChoiceBox.Checked);

                if (++counter % 15 == 0)
                {
                    if (InvokeRequired)
                    {
                        Invoke(() =>
                        {
                            ScannedSizeLabel.Text = counter + " items were copied! [" + $"{Convert.ToDouble(totalLength >> 30):0.##}" + "/" + $"{totalByte:0.##}" + "] GB";
                            ScannigLabel.Text = x.Value.FullName;
                            ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)counter / (double)totalFileCount) * 100.0);
                        });
                        return;
                    }
                    await Task.Delay(1);
                }
            });

            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    ScannedSizeLabel.Text = counter + " items were copied! [" + $"{Convert.ToDouble(totalLength >> 30):0.##}" + "/" + $"{totalByte:0.##}" + "] GB";
                    ScanProgressBar.Value = ScanProgressBar.Maximum;
                });
            }
        }





        private async Task MoveFile(Dictionary<string, FileInfo> map, int totalFileCount, DirectoryInfo directoryInfo, string targetPath, double totalByte, HashSet<string> dirs)
        {
            totalByte = _toGB(totalByte);

            ScanProgressBar.Value = ScanProgressBar.Minimum;

            var stopWatch = new Stopwatch();

            stopWatch.Start();
            await Task.Run(() => MoveFileCallback(directoryInfo, map, totalByte, targetPath, totalFileCount, dirs));


            stopWatch.Stop();

            TimeLabel.Text = "Scan was completed! Total Elapsed Time: " + stopWatch.Elapsed;
        }

        private async void MoveFileCallback(DirectoryInfo directoryInfo, Dictionary<string, FileInfo> map, double totalByte, string targetPath, int totalFileCount, HashSet<string> dirs)
        {
            int counter = 0;
            long totalLength = 0;
            HashSet<string> list = new HashSet<string>();
            map.AsParallel().ForAll(async x =>
            {
                totalLength += x.Value.Length;
                
                File.Move(x.Key, x.Value.FullName.Replace(directoryInfo.FullName, targetPath), OverwriteChoiceBox.Checked);
               
                list.Add(Regex.Match(new DirectoryInfo(x.Key).FullName, @"^(.*\\).*?$").Groups[1].Value);
                
                if (++counter % 15 == 0)
                {
                    if (InvokeRequired)
                    {
                        Invoke(() =>
                        {
                            ScannedSizeLabel.Text = counter + " items were moved! [" + $"{Convert.ToDouble(totalLength >> 30):0.##}" + "/" + $"{totalByte:0.##}" + "] GB";
                            ScannigLabel.Text = x.Value.FullName;
                            ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)counter / (double)totalFileCount) * 100.0);
                        });
                        return;
                    }
                    await Task.Delay(1);
                }
            });

            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    ScannedSizeLabel.Text = counter + " items were moved! [" + $"{Convert.ToDouble(totalLength >> 30):0.##}" + "/" + $"{totalByte:0.##}" + "] GB";
                    ScanProgressBar.Value = ScanProgressBar.Maximum;
                });
            }
            File.WriteAllLines("C:\\Users\\hp\\Desktop\\dirs.txt", list);
            list.Skip(0).Select(d => new DirectoryInfo(d)).Where(d => d.GetFiles().Length == 0 && d.Exists && d.FullName != directoryInfo.FullName).ToList().ForEach(d =>
            {
                try
                {
                    d.Delete(true);
                } catch (Exception ex) { }
            });
            //File.WriteAllLines("C:\\Users\\hp\\Desktop\\dirs.txt", list);
          
        }
    }

}


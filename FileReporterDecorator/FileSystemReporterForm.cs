using FileReporterDecorator;
using FileReporterDecorator.FileOperation;
using FileReporterDecorator.FileOperation.operations;
using FileReporterLib.Filter.DateFilter;
using FileReporterLib.Util;
using System.Security.AccessControl;
using static FileReporterDecorator.Util.ExceptionUtil;
using static FileReportServiceLib.Util.OptionCreator;

namespace FileReporterApp
{
    public partial class FileSystemReporterForm : Form
    {
        private readonly Action<int, int, string> _showOnScreenProgressCallback;
        private readonly Action<int, TimeSpan> _showMaximizeOnScreenCallback;
        private readonly Action _minimumProgressBarCallback;
        private readonly Action<string> _setTimeLabelCallback;
        private readonly Action<string> _errorLabelTextCallback;

        private int _threadCount;
        private int _totalFileCount;
        private string _destinationPath;
        private string? _targetPath;
        private IDateOption _dateOption;

        public FileSystemReporterForm()
        {
            InitializeComponent();

            _showOnScreenProgressCallback = (counter, fileCount, file) => ShowOnScreenProgress(counter, fileCount, file);
            _showMaximizeOnScreenCallback = (counter, elapsedTime) => ShowMaximizeOnScreen(counter, elapsedTime);
            _minimumProgressBarCallback = () => MinimumProgressBar();
            _setTimeLabelCallback = (text) => SetTimeLabel(text);
            _errorLabelTextCallback = str => SetErrorLabelText(str);
        }

        private void SetErrorLabelText(string msg) => RequireInvoke(() => ErrorLabel.Text = msg);
        private IDateOption GetDateOption() => GetSelectedDateOption(CreatedDateRadioButton.Checked, ModifiedDateRadioButton.Checked);
        private int GetTotalFileCount()
        {
            var fileCount = 0;
            try
            {
                if (AccessControl.HasAccessAllow(_destinationPath, FileSystemRights.FullControl, FileSystemRights.ReadAttributes, FileSystemRights.ListDirectory))
                    fileCount = new DirectoryInfo(PathTextBox.Text).GetFiles("*.*", SearchOption.AllDirectories).Length;

                else throw new UnauthorizedAccessException("You cannot access this directory!");
            }
            catch (UnauthorizedAccessException ex)
            {
                ErrorLabel.Text = ex.Message;
            }
            return fileCount;

        }
        private void MinimumProgressBar() => ScanProgressBar.Value = ScanProgressBar.Minimum;
        private void SetTimeLabel(string str) => TimeLabel.Text = str;


        public FileOperation CreateScanProcess()
        {
            return new ScanDirectoryOperation(null, DateTimePicker.Value, _totalFileCount, _threadCount,
                 _destinationPath, _dateOption, _showOnScreenProgressCallback, _showMaximizeOnScreenCallback);
        }
        public FileOperation CreateTransportProcess(FileOperation process, FileOperation scanProcess)
        {
            if (NtfsChoiceBox.Checked)
                process = new NtfsSecurityOptionDecorator(process, scanProcess);

            if (EmptyFoldersChoiceBox.Checked)
                process = new EmptyOptionDecorator(process, scanProcess);

            if (OverwriteChoiceBox.Checked)
                process = new OverwriteOptionDecorator(process, scanProcess);

            return process;
        }
        public FileOperation CreateOperationProcess(FileOperation scanProcess, bool moveOption, bool copyOption)
        {

            if (moveOption)
                return new MoveFileOperation(scanProcess, _totalFileCount,
                    _threadCount, _destinationPath, _targetPath,
                    _showOnScreenProgressCallback, _minimumProgressBarCallback,
                    _setTimeLabelCallback, _showMaximizeOnScreenCallback, _errorLabelTextCallback);

            else if (copyOption)
                return new CopyFileOperation(scanProcess, _totalFileCount,
                    _threadCount, _destinationPath, _targetPath,
                    _showOnScreenProgressCallback, _minimumProgressBarCallback,
                    _showMaximizeOnScreenCallback, _setTimeLabelCallback, _errorLabelTextCallback);

            return new EmptyOperation();
        }
        private bool InitMembers(bool isReport)
        {
            _threadCount = (int)ThreadCounter.Value;
            _destinationPath = PathTextBox.Text;
            _targetPath = TargetPathTextBox.Text;
            _dateOption = GetDateOption();

            ErrorLabel.ResetText();

            bool isValid = DataValidator.ValidateData(_threadCount, _destinationPath, _targetPath, isReport, _threadCount, ThreadCounter, CopyRadioButton, MoveRadioButton);

            if (isValid)
                _totalFileCount = GetTotalFileCount();

            return isValid;
        }



        private async void RunButton_Click(object sender, EventArgs e)
        {
            if (!InitMembers(false))
                return;

            FileOperation scanProcess = CreateScanProcess();
            await scanProcess.Run();

            FileOperation operationProcess = CreateOperationProcess(scanProcess, MoveRadioButton.Checked, CopyRadioButton.Checked);

            operationProcess = CreateTransportProcess(operationProcess, scanProcess);

            await operationProcess.Run();
        }
        private async void ReportButtonCallback()
        {

            if (!InitMembers(true))
                return;

            ThreadCounter.Value = 4;
            Stream myStream;

            SaveDialog.Filter = "txt files (*.txt)|*.txt|Excel Files (*.xlsx)|*.xlsx";
            SaveDialog.FilterIndex = 2;
            SaveDialog.RestoreDirectory = true;

            if (SaveDialog.ShowDialog() == DialogResult.OK && (myStream = SaveDialog.OpenFile()) != null)
            {
                myStream.Close();

                FileOperation scanProcess = CreateScanProcess();
                await scanProcess.Run();

                var reportProcess = new ExportReportOperation(scanProcess, GetFileFormat(SaveDialog.FilterIndex), SaveDialog.FileName);

                await reportProcess.Run();

                ScanProgressBar.Value = ScanProgressBar.Maximum;
                TimeLabel.Text = "Report is ready!";
            }
        }
        private async void ReportButton_ClickAsync(object sender, EventArgs e)
        {
            ThrowException(ReportButtonCallback, () => MessageBox.Show("Please select the file!"));
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
        private void CleanButton_Click(object sender, EventArgs e)
        {
            ThreadCounter.Value = 1;
            PathTextBox.Clear();
            TargetPathTextBox.Clear();
            ScanRadioButton.Select();
            OverwriteChoiceBox.Checked = false;
            NtfsChoiceBox.Checked = false;
            EmptyFoldersChoiceBox.Checked = false;
            DateTimePicker.Value = DateTime.Now;
            ScanProgressBar.Value = ScanProgressBar.Minimum;
            ScannigLabel.ResetText();
            TimeLabel.ResetText();
            ScannedSizeLabel.ResetText();
        }
        private void ScanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EmptyFoldersChoiceBox.Enabled = false;
            OverwriteChoiceBox.Enabled = false;
            NtfsChoiceBox.Enabled = false;
        }
        private void ShowMaximizeOnScreen(int counter, TimeSpan elapsedTime)
        {
            ScanProgressBar.Value = ScanProgressBar.Maximum;
            ScannedSizeLabel.Text = counter + " items were scanned!";
            TimeLabel.Text = "Scan was completed! Total Elapsed Time: " + elapsedTime;
        }

        private void ShowOnScreenProgress(int counter, int totalFileCount, string file)
        {
            if (counter % 100 == 0)
            {
                RequireInvoke(() =>
                {
                    ThrowGeneralException(() =>
                    {
                        ScannedSizeLabel.Text = counter + " items were scanned";
                        ScanProgressBar.Value = (int)Math.Min(ScanProgressBar.Maximum, ((double)counter / (double)totalFileCount) * 100.0);
                        ScannigLabel.Text = file;
                    }, () => { });

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
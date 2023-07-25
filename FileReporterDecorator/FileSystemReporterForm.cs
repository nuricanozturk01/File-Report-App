using FileReporterDecorator;
using FileReporterDecorator.FileOperation;
using FileReporterDecorator.FileOperation.operations;
using FileReporterLib.Filter.DateFilter;
using static FileReporterDecorator.Util.ExceptionUtil;
using static FileReportServiceLib.Util.OptionCreator;

namespace FileReporterApp
{
    public partial class FileSystemReporterForm : Form
    {
        private readonly Action<int, int, string> _showOnScreenProgressCallback;
        private readonly Action<int, string> _showOnScreenProgressCallbackOverride;
        private readonly Action<int, TimeSpan> _showMaximizeOnScreenCallback;
        private readonly Action<string> _setTimeLabelCallback;
        private readonly Action<string> _errorLabelTextCallback;
        private readonly Action<List<string>> _saveDialogUnAccessAuthorizeCallback;

        private int _threadCount;
        private int _totalFileCount;
        private string _destinationPath;
        private string? _targetPath;
        private IDateOption _dateOption;

        public FileSystemReporterForm()
        {
            InitializeComponent();

            _saveDialogUnAccessAuthorizeCallback = unAuthorizedList => SaveUnAuthorizedFolders(unAuthorizedList);
            _showOnScreenProgressCallback = (counter, fileCount, file) => ShowOnScreenProgress(counter, fileCount, file);
            _showOnScreenProgressCallbackOverride = (counter, file) => ShowOnScreenProgress(counter, file);
            _showMaximizeOnScreenCallback = (counter, elapsedTime) => ShowMaximizeOnScreen(counter, elapsedTime);
            _setTimeLabelCallback = (text) => SetTimeLabel(text);
            _errorLabelTextCallback = str => SetErrorLabelText(str);
        }

        /*
         * 
         * 
         * Set error label. Written for callback. 
         * 
         */
        private void SetErrorLabelText(string msg) => RequireInvoke(() => ErrorLabel.Text = msg);

        /*
         * 
         * 
         * Get Selected Date Option 
         * 
         */
        private IDateOption GetDateOption() => GetSelectedDateOption(CreatedDateRadioButton.Checked, ModifiedDateRadioButton.Checked);


        /*
         * 
         * Set progress bar to minimum value. Written for callback
         *  
         */
        private void MinimumProgressBar() => ScanProgressBar.Value = ScanProgressBar.Minimum;

        /*
         * 
         * Set Scanned, moved or copied time label. Written for callback
         *  
         */
        private void SetTimeLabel(string str) => TimeLabel.Text = str;

        /*
         * 
         * Create Scan Process 
         * 
         */
        public FileOperation CreateScanProcess()
        {
            return new ScanDirectoryOperation(null, DateTimePicker.Value, _totalFileCount, _threadCount,
                 _destinationPath, _dateOption, _showOnScreenProgressCallbackOverride, _showMaximizeOnScreenCallback, _saveDialogUnAccessAuthorizeCallback);
        }




        /*
         * 
         * Create Scan Process For Report
         * 
         */
        public FileOperation CreateScanProcessWithoutSaveUnaccessFolders()
        {
            return new ScanDirectoryOperation(null, DateTimePicker.Value, _totalFileCount, _threadCount,
                 _destinationPath, _dateOption, _showOnScreenProgressCallbackOverride, _showMaximizeOnScreenCallback, empty => { });
        }




        /*
         * 
         * Create Transport Process like (Ntfs, EmptyFolder, Overwrite) and decorate it.
         * 
         */
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
        /*
         * 
         * Create Operation Process like (Move or Copy). If take any error, return the empty Operation 
         * 
         */
        public FileOperation CreateOperationProcess(FileOperation scanProcess, bool moveOption, bool copyOption)
        {
            if (moveOption)
                return new MoveFileOperation(scanProcess, _threadCount, _destinationPath,
                                             _targetPath, _showOnScreenProgressCallbackOverride, _setTimeLabelCallback,
                                             _showMaximizeOnScreenCallback, _errorLabelTextCallback);

            else if (copyOption)
                return new CopyFileOperation(scanProcess, _threadCount, _destinationPath,
                                             _targetPath, _showOnScreenProgressCallbackOverride, _showMaximizeOnScreenCallback,
                                             _setTimeLabelCallback, _errorLabelTextCallback);

            return new EmptyOperation();
        }
        /*
         * 
         * Initialize the input values and validate it. 
         * 
         */
        private bool InitMembers(bool isReport)
        {
            _threadCount = (int)ThreadCounter.Value;
            _destinationPath = PathTextBox.Text;
            _targetPath = TargetPathTextBox.Text;
            _dateOption = GetDateOption();

            ErrorLabel.ResetText();

            bool isValid = DataValidator.ValidateData(_threadCount, _destinationPath, _targetPath, isReport, _threadCount, ThreadCounter, CopyRadioButton, MoveRadioButton);

            return isValid;
        }


        /*
         * 
         * Run this method when user click on the Run Button. This method create the processes. trigger method. 
         * 
         */
        private async void RunButton_Click(object sender, EventArgs e)
        {
            if (!InitMembers(false))
                return;

            FileOperation scanProcess = ScanRadioButton.Checked ? CreateScanProcess() : CreateScanProcessWithoutSaveUnaccessFolders();

            ScanProgressBar.Style = ProgressBarStyle.Marquee;
            ScanProgressBar.MarqueeAnimationSpeed = 30;

            await scanProcess.Run();

            FileOperation operationProcess = CreateOperationProcess(scanProcess, MoveRadioButton.Checked, CopyRadioButton.Checked);

            operationProcess = CreateTransportProcess(operationProcess, scanProcess);

            await operationProcess.Run();

            ScanProgressBar.Value = ScanProgressBar.Maximum;
        }


        /*
         * 
         * Save the unauthorizated files. 
         * 
         */
        private void SaveUnAuthorizedFolders(List<string> unAuthorizedFileList)
        {
            Stream myStream;
            SaveUnAccessFileDialog.Title = "Save As Unaccess Folders";
            SaveUnAccessFileDialog.Filter = "txt files (*.txt)|*.txt";
            SaveUnAccessFileDialog.FilterIndex = 1;
            SaveUnAccessFileDialog.RestoreDirectory = true;

            if (SaveUnAccessFileDialog.ShowDialog() == DialogResult.OK && (myStream = SaveUnAccessFileDialog.OpenFile()) != null)
            {
                myStream.Close();

                File.WriteAllLines(SaveUnAccessFileDialog.FileName, unAuthorizedFileList);
                ErrorLabel.Text = "Report is loading...";
                ScanProgressBar.Value = ScanProgressBar.Maximum;
                ErrorLabel.Text = "Report is ready!";
            }
        }


        /*
         * 
         * Callback for Report Button.
         * 
         */
        private async void ReportButtonCallback()
        {

            if (!InitMembers(true))
                return;

            Stream myStream;

            SaveDialog.Filter = "txt files (*.txt)|*.txt|Excel Files (*.xlsx)|*.xlsx";
            SaveDialog.FilterIndex = 2;
            SaveDialog.RestoreDirectory = true;

            if (SaveDialog.ShowDialog() == DialogResult.OK && (myStream = SaveDialog.OpenFile()) != null)
            {
                myStream.Close();

                FileOperation scanProcess = CreateScanProcessWithoutSaveUnaccessFolders();
                await scanProcess.Run();

                ErrorLabel.Text = "Report is loading...";

                var reportProcess = new ExportReportOperation(scanProcess, GetFileFormat(SaveDialog.FilterIndex), SaveDialog.FileName);

                await reportProcess.Run();

                ScanProgressBar.Value = ScanProgressBar.Maximum;
                ErrorLabel.Text = "Report is ready!";
            }
        }




        /*
         * 
         *  Run this method when user click on the Report Button. This method create the report process. trigger method. 
         * 
         */
        private async void ReportButton_ClickAsync(object sender, EventArgs e)
        {
            ThrowException(ReportButtonCallback, () => MessageBox.Show("Please select the file!"));
        }

        /*
         * 
         *  Change and reset the other options on GUI when user select the Move Radio Button
         * 
         */
        private void MoveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            browseTargetButton.Enabled = MoveRadioButton.Checked;
            EmptyFoldersChoiceBox.Enabled = true;
            OverwriteChoiceBox.Enabled = true;
            NtfsChoiceBox.Enabled = false;
        }

        /**
         * 
         *
         * This method written for when user selected the copy radio button, program will set automatically 
         *
         */
        private void CopyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            browseTargetButton.Enabled = CopyRadioButton.Checked;
            EmptyFoldersChoiceBox.Enabled = true;
            OverwriteChoiceBox.Enabled = true;
            NtfsChoiceBox.Enabled = true;
        }

        /*
        * 
        * This method written for selecting destination path
        * 
        */
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                PathTextBox.Text = folderBrowser.SelectedPath;
        }
        /*
         * This method for Copy and Move operations. You can select the Target Path.  
         */
        private void BrowseTargetButton_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                TargetPathTextBox.Text = folderBrowser.SelectedPath;
        }

        /*
         * 
         * Cleaning inputs 
         * 
         */
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

        /*
         * 
         *  Change and reset the other options on GUI when user select the Scan Radio Button
         * 
         */
        private void ScanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            OverwriteChoiceBox.Checked = false;
            NtfsChoiceBox.Checked = false;
            EmptyFoldersChoiceBox.Checked = false;

            EmptyFoldersChoiceBox.Enabled = false;
            OverwriteChoiceBox.Enabled = false;
            NtfsChoiceBox.Enabled = false;
        }

        /*
         * 
         * 
         * Set Progress Bar Max value and scannedSizeLabel  
         * 
         */
        private void ShowMaximizeOnScreen(int counter, TimeSpan elapsedTime)
        {
            ScanProgressBar.Style = ProgressBarStyle.Continuous;
            ScanProgressBar.Value = ScanProgressBar.Maximum;

            ScannedSizeLabel.Text = counter + " items were scanned!";
            TimeLabel.Text = "Scan was completed! Total Elapsed Time: " + elapsedTime;
        }

        /*
         * 
         * Modify progress bar and show on screen.
         * 
         * Used with callback
         * 
         */
        private void ShowOnScreenProgress(int counter, int totalFileCount, string file)
        {
            if (counter % 100 == 0)
            {
                RequireInvoke(() =>
                {

                    ScannedSizeLabel.Text = counter + " items were scanned";

                    ScannigLabel.Text = file;

                });
            }
        }

        private void ShowOnScreenProgress(int counter, string file)
        {
            RequireInvoke(() =>
            {
                ScannedSizeLabel.Text = counter + " items were scanned";
                ScannigLabel.Text = file;
            });
        }



        /*
         * 
         * RequireInvoke method written for nested if clauses like 
         * if (counter % 10 == 0)
         *    if (RequireInvoke) // Necessary for escape the Cross Thread error
         *        .....
         */
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
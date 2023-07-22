using System.Drawing;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using SizeF = System.Drawing.SizeF;

namespace FileReporterApp
{
    partial class FileSystemReporterForm
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileSystemReporterForm));
            panel1 = new Panel();
            ErrorLabel = new Label();
            CleanButton = new Button();
            TimeLabel = new Label();
            ScannigLabel = new Label();
            ScannedSizeLabel = new Label();
            ScanProgressBar = new ProgressBar();
            ReportButton = new Button();
            RunButton = new Button();
            label3 = new Label();
            ExceptionTextBox = new TextBox();
            ExceptionBox = new ComboBox();
            label2 = new Label();
            browseTargetButton = new Button();
            TargetPathTextBox = new TextBox();
            OtherOptionsGroup = new GroupBox();
            OverwriteChoiceBox = new CheckBox();
            NtfsChoiceBox = new CheckBox();
            EmptyFoldersChoiceBox = new CheckBox();
            OptionsGroup = new GroupBox();
            CopyRadioButton = new RadioButton();
            MoveRadioButton = new RadioButton();
            ScanRadioButton = new RadioButton();
            ThreadCounter = new NumericUpDown();
            DateTimePicker = new DateTimePicker();
            label1 = new Label();
            DateOptionGroup = new GroupBox();
            AccessedDateRadioButton = new RadioButton();
            ModifiedDateRadioButton = new RadioButton();
            CreatedDateRadioButton = new RadioButton();
            BrowseButton = new Button();
            PathTextBox = new TextBox();
            fileSystemWatcher1 = new FileSystemWatcher();
            SaveDialog = new SaveFileDialog();
            label4 = new Label();
            panel1.SuspendLayout();
            OtherOptionsGroup.SuspendLayout();
            OptionsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThreadCounter).BeginInit();
            DateOptionGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Controls.Add(ErrorLabel);
            panel1.Controls.Add(CleanButton);
            panel1.Controls.Add(TimeLabel);
            panel1.Controls.Add(ScannigLabel);
            panel1.Controls.Add(ScannedSizeLabel);
            panel1.Controls.Add(ScanProgressBar);
            panel1.Controls.Add(ReportButton);
            panel1.Controls.Add(RunButton);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(ExceptionTextBox);
            panel1.Controls.Add(ExceptionBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(browseTargetButton);
            panel1.Controls.Add(TargetPathTextBox);
            panel1.Controls.Add(OtherOptionsGroup);
            panel1.Controls.Add(OptionsGroup);
            panel1.Controls.Add(ThreadCounter);
            panel1.Controls.Add(DateTimePicker);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(DateOptionGroup);
            panel1.Controls.Add(BrowseButton);
            panel1.Controls.Add(PathTextBox);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(931, 625);
            panel1.TabIndex = 0;
            // 
            // ErrorLabel
            // 
            ErrorLabel.AutoSize = true;
            ErrorLabel.Location = new Point(24, 592);
            ErrorLabel.MinimumSize = new Size(438, 0);
            ErrorLabel.Name = "ErrorLabel";
            ErrorLabel.Size = new Size(438, 15);
            ErrorLabel.TabIndex = 21;
            // 
            // CleanButton
            // 
            CleanButton.Location = new Point(639, 372);
            CleanButton.Name = "CleanButton";
            CleanButton.Size = new Size(277, 31);
            CleanButton.TabIndex = 3;
            CleanButton.Text = "Clean";
            CleanButton.UseVisualStyleBackColor = true;
            CleanButton.Click += CleanButton_Click;
            // 
            // TimeLabel
            // 
            TimeLabel.AutoSize = true;
            TimeLabel.Location = new Point(24, 558);
            TimeLabel.MinimumSize = new Size(438, 0);
            TimeLabel.Name = "TimeLabel";
            TimeLabel.Size = new Size(438, 15);
            TimeLabel.TabIndex = 20;
            TimeLabel.Text = "-------------";
            // 
            // ScannigLabel
            // 
            ScannigLabel.AutoSize = true;
            ScannigLabel.Location = new Point(24, 518);
            ScannigLabel.MinimumSize = new Size(875, 0);
            ScannigLabel.Name = "ScannigLabel";
            ScannigLabel.Size = new Size(875, 15);
            ScannigLabel.TabIndex = 19;
            ScannigLabel.Text = "-------------";
            // 
            // ScannedSizeLabel
            // 
            ScannedSizeLabel.AutoSize = true;
            ScannedSizeLabel.Location = new Point(24, 484);
            ScannedSizeLabel.MinimumSize = new Size(438, 0);
            ScannedSizeLabel.Name = "ScannedSizeLabel";
            ScannedSizeLabel.Size = new Size(438, 15);
            ScannedSizeLabel.TabIndex = 18;
            ScannedSizeLabel.Text = "0 items were scanned";
            // 
            // ScanProgressBar
            // 
            ScanProgressBar.Location = new Point(24, 418);
            ScanProgressBar.Margin = new Padding(4);
            ScanProgressBar.Name = "ScanProgressBar";
            ScanProgressBar.Size = new Size(892, 37);
            ScanProgressBar.TabIndex = 17;
            // 
            // ReportButton
            // 
            ReportButton.Location = new Point(315, 372);
            ReportButton.Margin = new Padding(4);
            ReportButton.Name = "ReportButton";
            ReportButton.Size = new Size(280, 31);
            ReportButton.TabIndex = 16;
            ReportButton.Text = "Report";
            ReportButton.UseVisualStyleBackColor = true;
            ReportButton.Click += ReportButton_ClickAsync;
            // 
            // RunButton
            // 
            RunButton.Location = new Point(24, 373);
            RunButton.Margin = new Padding(4);
            RunButton.Name = "RunButton";
            RunButton.Size = new Size(249, 30);
            RunButton.TabIndex = 15;
            RunButton.Text = "Run";
            RunButton.UseVisualStyleBackColor = true;
            RunButton.Click += RunButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 305);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 14;
            label3.Text = "Folder Exceptions";
            // 
            // ExceptionTextBox
            // 
            ExceptionTextBox.Enabled = false;
            ExceptionTextBox.Location = new Point(214, 324);
            ExceptionTextBox.Margin = new Padding(4);
            ExceptionTextBox.Name = "ExceptionTextBox";
            ExceptionTextBox.Size = new Size(693, 23);
            ExceptionTextBox.TabIndex = 13;
            ExceptionTextBox.Text = "NOT SUPPORTED YET!";
            // 
            // ExceptionBox
            // 
            ExceptionBox.Enabled = false;
            ExceptionBox.FormattingEnabled = true;
            ExceptionBox.Location = new Point(24, 323);
            ExceptionBox.Margin = new Padding(4);
            ExceptionBox.Name = "ExceptionBox";
            ExceptionBox.Size = new Size(182, 23);
            ExceptionBox.TabIndex = 12;
            ExceptionBox.Text = "NOT SUPPORTED YET!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 253);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 11;
            label2.Text = "Target Path";
            // 
            // browseTargetButton
            // 
            browseTargetButton.Enabled = false;
            browseTargetButton.Location = new Point(819, 268);
            browseTargetButton.Margin = new Padding(4);
            browseTargetButton.Name = "browseTargetButton";
            browseTargetButton.Size = new Size(88, 26);
            browseTargetButton.TabIndex = 10;
            browseTargetButton.Text = "Browse";
            browseTargetButton.UseVisualStyleBackColor = true;
            browseTargetButton.Click += BrowseTargetButton_Click;
            // 
            // TargetPathTextBox
            // 
            TargetPathTextBox.BackColor = Color.White;
            TargetPathTextBox.Enabled = false;
            TargetPathTextBox.Location = new Point(24, 271);
            TargetPathTextBox.Margin = new Padding(4);
            TargetPathTextBox.Name = "TargetPathTextBox";
            TargetPathTextBox.PlaceholderText = "Please Select the Target Path (Copy or Move)";
            TargetPathTextBox.ReadOnly = true;
            TargetPathTextBox.Size = new Size(787, 23);
            TargetPathTextBox.TabIndex = 9;
            // 
            // OtherOptionsGroup
            // 
            OtherOptionsGroup.Controls.Add(OverwriteChoiceBox);
            OtherOptionsGroup.Controls.Add(NtfsChoiceBox);
            OtherOptionsGroup.Controls.Add(EmptyFoldersChoiceBox);
            OtherOptionsGroup.Location = new Point(486, 171);
            OtherOptionsGroup.Margin = new Padding(4);
            OtherOptionsGroup.Name = "OtherOptionsGroup";
            OtherOptionsGroup.Padding = new Padding(4);
            OtherOptionsGroup.Size = new Size(433, 58);
            OtherOptionsGroup.TabIndex = 8;
            OtherOptionsGroup.TabStop = false;
            OtherOptionsGroup.Text = "Other Options";
            // 
            // OverwriteChoiceBox
            // 
            OverwriteChoiceBox.AutoSize = true;
            OverwriteChoiceBox.Enabled = false;
            OverwriteChoiceBox.Location = new Point(317, 22);
            OverwriteChoiceBox.Margin = new Padding(4);
            OverwriteChoiceBox.Name = "OverwriteChoiceBox";
            OverwriteChoiceBox.Size = new Size(77, 19);
            OverwriteChoiceBox.TabIndex = 2;
            OverwriteChoiceBox.Text = "Overwrite";
            OverwriteChoiceBox.UseVisualStyleBackColor = true;
            // 
            // NtfsChoiceBox
            // 
            NtfsChoiceBox.AutoSize = true;
            NtfsChoiceBox.Enabled = false;
            NtfsChoiceBox.Location = new Point(153, 22);
            NtfsChoiceBox.Margin = new Padding(4);
            NtfsChoiceBox.Name = "NtfsChoiceBox";
            NtfsChoiceBox.Size = new Size(119, 19);
            NtfsChoiceBox.TabIndex = 1;
            NtfsChoiceBox.Text = "NTFS Permissions";
            NtfsChoiceBox.UseVisualStyleBackColor = true;
            // 
            // EmptyFoldersChoiceBox
            // 
            EmptyFoldersChoiceBox.AutoSize = true;
            EmptyFoldersChoiceBox.Enabled = false;
            EmptyFoldersChoiceBox.Location = new Point(8, 23);
            EmptyFoldersChoiceBox.Margin = new Padding(4);
            EmptyFoldersChoiceBox.Name = "EmptyFoldersChoiceBox";
            EmptyFoldersChoiceBox.Size = new Size(101, 19);
            EmptyFoldersChoiceBox.TabIndex = 0;
            EmptyFoldersChoiceBox.Text = "Empty Folders";
            EmptyFoldersChoiceBox.UseVisualStyleBackColor = true;
            // 
            // OptionsGroup
            // 
            OptionsGroup.Controls.Add(CopyRadioButton);
            OptionsGroup.Controls.Add(MoveRadioButton);
            OptionsGroup.Controls.Add(ScanRadioButton);
            OptionsGroup.Location = new Point(24, 171);
            OptionsGroup.Margin = new Padding(4);
            OptionsGroup.Name = "OptionsGroup";
            OptionsGroup.Padding = new Padding(4);
            OptionsGroup.Size = new Size(440, 58);
            OptionsGroup.TabIndex = 7;
            OptionsGroup.TabStop = false;
            OptionsGroup.Text = "Operations";
            // 
            // CopyRadioButton
            // 
            CopyRadioButton.AutoSize = true;
            CopyRadioButton.Location = new Point(314, 22);
            CopyRadioButton.Margin = new Padding(4);
            CopyRadioButton.Name = "CopyRadioButton";
            CopyRadioButton.Size = new Size(53, 19);
            CopyRadioButton.TabIndex = 2;
            CopyRadioButton.Text = "Copy";
            CopyRadioButton.UseVisualStyleBackColor = true;
            CopyRadioButton.CheckedChanged += CopyRadioButton_CheckedChanged;
            // 
            // MoveRadioButton
            // 
            MoveRadioButton.AutoSize = true;
            MoveRadioButton.Location = new Point(165, 22);
            MoveRadioButton.Margin = new Padding(4);
            MoveRadioButton.Name = "MoveRadioButton";
            MoveRadioButton.Size = new Size(55, 19);
            MoveRadioButton.TabIndex = 1;
            MoveRadioButton.Text = "Move";
            MoveRadioButton.UseVisualStyleBackColor = true;
            MoveRadioButton.CheckedChanged += MoveRadioButton_CheckedChanged;
            // 
            // ScanRadioButton
            // 
            ScanRadioButton.AutoSize = true;
            ScanRadioButton.Checked = true;
            ScanRadioButton.Location = new Point(8, 22);
            ScanRadioButton.Margin = new Padding(4);
            ScanRadioButton.Name = "ScanRadioButton";
            ScanRadioButton.Size = new Size(50, 19);
            ScanRadioButton.TabIndex = 0;
            ScanRadioButton.TabStop = true;
            ScanRadioButton.Text = "Scan";
            ScanRadioButton.UseVisualStyleBackColor = true;
            ScanRadioButton.CheckedChanged += ScanRadioButton_CheckedChanged;
            // 
            // ThreadCounter
            // 
            ThreadCounter.Location = new Point(807, 110);
            ThreadCounter.Margin = new Padding(4);
            ThreadCounter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThreadCounter.Name = "ThreadCounter";
            ThreadCounter.Size = new Size(100, 23);
            ThreadCounter.TabIndex = 6;
            ThreadCounter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DateTimePicker
            // 
            DateTimePicker.Location = new Point(486, 110);
            DateTimePicker.Margin = new Padding(4);
            DateTimePicker.Name = "DateTimePicker";
            DateTimePicker.Size = new Size(208, 23);
            DateTimePicker.TabIndex = 5;
            DateTimePicker.Value = new DateTime(2023, 7, 22, 10, 4, 1, 668);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 10);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 4;
            label1.Text = "Path";
            // 
            // DateOptionGroup
            // 
            DateOptionGroup.Controls.Add(AccessedDateRadioButton);
            DateOptionGroup.Controls.Add(ModifiedDateRadioButton);
            DateOptionGroup.Controls.Add(CreatedDateRadioButton);
            DateOptionGroup.Location = new Point(24, 91);
            DateOptionGroup.Margin = new Padding(4);
            DateOptionGroup.Name = "DateOptionGroup";
            DateOptionGroup.Padding = new Padding(4);
            DateOptionGroup.Size = new Size(440, 53);
            DateOptionGroup.TabIndex = 3;
            DateOptionGroup.TabStop = false;
            DateOptionGroup.Text = "Date Option";
            // 
            // AccessedDateRadioButton
            // 
            AccessedDateRadioButton.AutoSize = true;
            AccessedDateRadioButton.Location = new Point(314, 23);
            AccessedDateRadioButton.Margin = new Padding(4);
            AccessedDateRadioButton.Name = "AccessedDateRadioButton";
            AccessedDateRadioButton.Size = new Size(101, 19);
            AccessedDateRadioButton.TabIndex = 2;
            AccessedDateRadioButton.Text = "Accessed Date";
            AccessedDateRadioButton.UseVisualStyleBackColor = true;
            // 
            // ModifiedDateRadioButton
            // 
            ModifiedDateRadioButton.AutoSize = true;
            ModifiedDateRadioButton.Location = new Point(165, 23);
            ModifiedDateRadioButton.Margin = new Padding(4);
            ModifiedDateRadioButton.Name = "ModifiedDateRadioButton";
            ModifiedDateRadioButton.Size = new Size(97, 19);
            ModifiedDateRadioButton.TabIndex = 1;
            ModifiedDateRadioButton.Text = "ModifiedDate";
            ModifiedDateRadioButton.UseVisualStyleBackColor = true;
            // 
            // CreatedDateRadioButton
            // 
            CreatedDateRadioButton.AutoSize = true;
            CreatedDateRadioButton.Checked = true;
            CreatedDateRadioButton.Location = new Point(8, 23);
            CreatedDateRadioButton.Margin = new Padding(4);
            CreatedDateRadioButton.Name = "CreatedDateRadioButton";
            CreatedDateRadioButton.Size = new Size(90, 19);
            CreatedDateRadioButton.TabIndex = 0;
            CreatedDateRadioButton.TabStop = true;
            CreatedDateRadioButton.Text = "CreatedDate";
            CreatedDateRadioButton.UseVisualStyleBackColor = true;
            // 
            // BrowseButton
            // 
            BrowseButton.Location = new Point(819, 32);
            BrowseButton.Margin = new Padding(4);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(88, 26);
            BrowseButton.TabIndex = 1;
            BrowseButton.Text = "Browse";
            BrowseButton.UseVisualStyleBackColor = true;
            BrowseButton.Click += BrowseButton_Click;
            // 
            // PathTextBox
            // 
            PathTextBox.BackColor = Color.White;
            PathTextBox.Enabled = false;
            PathTextBox.Location = new Point(24, 32);
            PathTextBox.Margin = new Padding(4);
            PathTextBox.Name = "PathTextBox";
            PathTextBox.PlaceholderText = "Please Select the Destination Path";
            PathTextBox.ReadOnly = true;
            PathTextBox.Size = new Size(787, 23);
            PathTextBox.TabIndex = 0;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(710, 114);
            label4.Name = "label4";
            label4.Size = new Size(79, 15);
            label4.TabIndex = 22;
            label4.Text = "Thread Count";
            // 
            // FileSystemReporterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(931, 625);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "FileSystemReporterForm";
            Text = "File Reporter App";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            OtherOptionsGroup.ResumeLayout(false);
            OtherOptionsGroup.PerformLayout();
            OptionsGroup.ResumeLayout(false);
            OptionsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThreadCounter).EndInit();
            DateOptionGroup.ResumeLayout(false);
            DateOptionGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox PathTextBox;
        private Button BrowseButton;
        private GroupBox DateOptionGroup;
        private RadioButton AccessedDateRadioButton;
        private RadioButton ModifiedDateRadioButton;
        private RadioButton CreatedDateRadioButton;
        private Label label1;
        private DateTimePicker DateTimePicker;
        private NumericUpDown ThreadCounter;
        private GroupBox OtherOptionsGroup;
        private GroupBox OptionsGroup;
        private RadioButton CopyRadioButton;
        private RadioButton MoveRadioButton;
        private RadioButton ScanRadioButton;
        private CheckBox OverwriteChoiceBox;
        private CheckBox NtfsChoiceBox;
        private CheckBox EmptyFoldersChoiceBox;
        private Label label3;
        private TextBox ExceptionTextBox;
        private ComboBox ExceptionBox;
        private Label label2;
        private Button browseTargetButton;
        private TextBox TargetPathTextBox;
        private ProgressBar ScanProgressBar;
        private Button ReportButton;
        private Button RunButton;
        private FileSystemWatcher fileSystemWatcher1;
        private Label TimeLabel;
        private Label ScannigLabel;
        private Label ScannedSizeLabel;
        private SaveFileDialog SaveDialog;
        private Button CleanButton;
        private Label ErrorLabel;
        private Label label4;
    }
}


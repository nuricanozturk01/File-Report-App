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
            panel1 = new Panel();
            ResultListBox = new ListBox();
            ProgressBar = new ProgressBar();
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
            panel1.Controls.Add(ResultListBox);
            panel1.Controls.Add(ProgressBar);
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
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1064, 833);
            panel1.TabIndex = 0;
            // 
            // ResultListBox
            // 
            ResultListBox.FormattingEnabled = true;
            ResultListBox.ItemHeight = 20;
            ResultListBox.Location = new Point(27, 631);
            ResultListBox.Margin = new Padding(5);
            ResultListBox.Name = "ResultListBox";
            ResultListBox.Size = new Size(1021, 184);
            ResultListBox.TabIndex = 18;
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(27, 557);
            ProgressBar.Margin = new Padding(5);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(1019, 49);
            ProgressBar.TabIndex = 17;
            // 
            // ReportButton
            // 
            ReportButton.Location = new Point(555, 495);
            ReportButton.Margin = new Padding(5);
            ReportButton.Name = "ReportButton";
            ReportButton.Size = new Size(485, 41);
            ReportButton.TabIndex = 16;
            ReportButton.Text = "Report";
            ReportButton.UseVisualStyleBackColor = true;
            ReportButton.Click += ReportButton_Click;
            // 
            // RunButton
            // 
            RunButton.Location = new Point(27, 497);
            RunButton.Margin = new Padding(5);
            RunButton.Name = "RunButton";
            RunButton.Size = new Size(503, 40);
            RunButton.TabIndex = 15;
            RunButton.Text = "Run";
            RunButton.UseVisualStyleBackColor = true;
            RunButton.Click += RunButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 407);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(126, 20);
            label3.TabIndex = 14;
            label3.Text = "Folder Exceptions";
            // 
            // ExceptionTextBox
            // 
            ExceptionTextBox.Location = new Point(245, 432);
            ExceptionTextBox.Margin = new Padding(5);
            ExceptionTextBox.Name = "ExceptionTextBox";
            ExceptionTextBox.Size = new Size(791, 27);
            ExceptionTextBox.TabIndex = 13;
            // 
            // ExceptionBox
            // 
            ExceptionBox.FormattingEnabled = true;
            ExceptionBox.Location = new Point(27, 431);
            ExceptionBox.Margin = new Padding(5);
            ExceptionBox.Name = "ExceptionBox";
            ExceptionBox.Size = new Size(207, 28);
            ExceptionBox.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 337);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(82, 20);
            label2.TabIndex = 11;
            label2.Text = "Target Path";
            // 
            // browseTargetButton
            // 
            browseTargetButton.Enabled = false;
            browseTargetButton.Location = new Point(936, 357);
            browseTargetButton.Margin = new Padding(5);
            browseTargetButton.Name = "browseTargetButton";
            browseTargetButton.Size = new Size(101, 35);
            browseTargetButton.TabIndex = 10;
            browseTargetButton.Text = "Browse";
            browseTargetButton.UseVisualStyleBackColor = true;
            browseTargetButton.Click += BrowseTargetButton_Click;
            // 
            // TargetPathTextBox
            // 
            TargetPathTextBox.BackColor = Color.White;
            TargetPathTextBox.Enabled = false;
            TargetPathTextBox.Location = new Point(27, 361);
            TargetPathTextBox.Margin = new Padding(5);
            TargetPathTextBox.Name = "TargetPathTextBox";
            TargetPathTextBox.Size = new Size(899, 27);
            TargetPathTextBox.TabIndex = 9;
            // 
            // OtherOptionsGroup
            // 
            OtherOptionsGroup.Controls.Add(OverwriteChoiceBox);
            OtherOptionsGroup.Controls.Add(NtfsChoiceBox);
            OtherOptionsGroup.Controls.Add(EmptyFoldersChoiceBox);
            OtherOptionsGroup.Location = new Point(555, 228);
            OtherOptionsGroup.Margin = new Padding(5);
            OtherOptionsGroup.Name = "OtherOptionsGroup";
            OtherOptionsGroup.Padding = new Padding(5);
            OtherOptionsGroup.Size = new Size(495, 77);
            OtherOptionsGroup.TabIndex = 8;
            OtherOptionsGroup.TabStop = false;
            OtherOptionsGroup.Text = "Other Options";
            // 
            // OverwriteChoiceBox
            // 
            OverwriteChoiceBox.AutoSize = true;
            OverwriteChoiceBox.Location = new Point(335, 31);
            OverwriteChoiceBox.Margin = new Padding(5);
            OverwriteChoiceBox.Name = "OverwriteChoiceBox";
            OverwriteChoiceBox.Size = new Size(95, 24);
            OverwriteChoiceBox.TabIndex = 2;
            OverwriteChoiceBox.Text = "Overwrite";
            OverwriteChoiceBox.UseVisualStyleBackColor = true;
            // 
            // NtfsChoiceBox
            // 
            NtfsChoiceBox.AutoSize = true;
            NtfsChoiceBox.Location = new Point(168, 31);
            NtfsChoiceBox.Margin = new Padding(5);
            NtfsChoiceBox.Name = "NtfsChoiceBox";
            NtfsChoiceBox.Size = new Size(145, 24);
            NtfsChoiceBox.TabIndex = 1;
            NtfsChoiceBox.Text = "NTFS Permissions";
            NtfsChoiceBox.UseVisualStyleBackColor = true;
            // 
            // EmptyFoldersChoiceBox
            // 
            EmptyFoldersChoiceBox.AutoSize = true;
            EmptyFoldersChoiceBox.Location = new Point(9, 31);
            EmptyFoldersChoiceBox.Margin = new Padding(5);
            EmptyFoldersChoiceBox.Name = "EmptyFoldersChoiceBox";
            EmptyFoldersChoiceBox.Size = new Size(125, 24);
            EmptyFoldersChoiceBox.TabIndex = 0;
            EmptyFoldersChoiceBox.Text = "Empty Folders";
            EmptyFoldersChoiceBox.UseVisualStyleBackColor = true;
            // 
            // OptionsGroup
            // 
            OptionsGroup.Controls.Add(CopyRadioButton);
            OptionsGroup.Controls.Add(MoveRadioButton);
            OptionsGroup.Controls.Add(ScanRadioButton);
            OptionsGroup.Location = new Point(27, 228);
            OptionsGroup.Margin = new Padding(5);
            OptionsGroup.Name = "OptionsGroup";
            OptionsGroup.Padding = new Padding(5);
            OptionsGroup.Size = new Size(503, 77);
            OptionsGroup.TabIndex = 7;
            OptionsGroup.TabStop = false;
            OptionsGroup.Text = "Options";
            // 
            // CopyRadioButton
            // 
            CopyRadioButton.AutoSize = true;
            CopyRadioButton.Location = new Point(359, 29);
            CopyRadioButton.Margin = new Padding(5);
            CopyRadioButton.Name = "CopyRadioButton";
            CopyRadioButton.Size = new Size(64, 24);
            CopyRadioButton.TabIndex = 2;
            CopyRadioButton.Text = "Copy";
            CopyRadioButton.UseVisualStyleBackColor = true;
            CopyRadioButton.CheckedChanged += CopyRadioButton_CheckedChanged;
            // 
            // MoveRadioButton
            // 
            MoveRadioButton.AutoSize = true;
            MoveRadioButton.Location = new Point(189, 29);
            MoveRadioButton.Margin = new Padding(5);
            MoveRadioButton.Name = "MoveRadioButton";
            MoveRadioButton.Size = new Size(67, 24);
            MoveRadioButton.TabIndex = 1;
            MoveRadioButton.Text = "Move";
            MoveRadioButton.UseVisualStyleBackColor = true;
            MoveRadioButton.CheckedChanged += MoveRadioButton_CheckedChanged;
            // 
            // ScanRadioButton
            // 
            ScanRadioButton.AutoSize = true;
            ScanRadioButton.Checked = true;
            ScanRadioButton.Location = new Point(9, 29);
            ScanRadioButton.Margin = new Padding(5);
            ScanRadioButton.Name = "ScanRadioButton";
            ScanRadioButton.Size = new Size(61, 24);
            ScanRadioButton.TabIndex = 0;
            ScanRadioButton.TabStop = true;
            ScanRadioButton.Text = "Scan";
            ScanRadioButton.UseVisualStyleBackColor = true;
            // 
            // ThreadCounter
            // 
            ThreadCounter.Location = new Point(891, 147);
            ThreadCounter.Margin = new Padding(5);
            ThreadCounter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThreadCounter.Name = "ThreadCounter";
            ThreadCounter.Size = new Size(160, 27);
            ThreadCounter.TabIndex = 6;
            ThreadCounter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DateTimePicker
            // 
            DateTimePicker.Location = new Point(555, 147);
            DateTimePicker.Margin = new Padding(5);
            DateTimePicker.Name = "DateTimePicker";
            DateTimePicker.Size = new Size(317, 27);
            DateTimePicker.TabIndex = 5;
            DateTimePicker.Value = new DateTime(2023, 7, 10, 11, 2, 11, 0);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 13);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(37, 20);
            label1.TabIndex = 4;
            label1.Text = "Path";
            // 
            // DateOptionGroup
            // 
            DateOptionGroup.Controls.Add(AccessedDateRadioButton);
            DateOptionGroup.Controls.Add(ModifiedDateRadioButton);
            DateOptionGroup.Controls.Add(CreatedDateRadioButton);
            DateOptionGroup.Location = new Point(27, 121);
            DateOptionGroup.Margin = new Padding(5);
            DateOptionGroup.Name = "DateOptionGroup";
            DateOptionGroup.Padding = new Padding(5);
            DateOptionGroup.Size = new Size(503, 71);
            DateOptionGroup.TabIndex = 3;
            DateOptionGroup.TabStop = false;
            DateOptionGroup.Text = "Date Option";
            // 
            // AccessedDateRadioButton
            // 
            AccessedDateRadioButton.AutoSize = true;
            AccessedDateRadioButton.Location = new Point(359, 31);
            AccessedDateRadioButton.Margin = new Padding(5);
            AccessedDateRadioButton.Name = "AccessedDateRadioButton";
            AccessedDateRadioButton.Size = new Size(127, 24);
            AccessedDateRadioButton.TabIndex = 2;
            AccessedDateRadioButton.Text = "Accessed Date";
            AccessedDateRadioButton.UseVisualStyleBackColor = true;
            // 
            // ModifiedDateRadioButton
            // 
            ModifiedDateRadioButton.AutoSize = true;
            ModifiedDateRadioButton.Location = new Point(189, 31);
            ModifiedDateRadioButton.Margin = new Padding(5);
            ModifiedDateRadioButton.Name = "ModifiedDateRadioButton";
            ModifiedDateRadioButton.Size = new Size(123, 24);
            ModifiedDateRadioButton.TabIndex = 1;
            ModifiedDateRadioButton.Text = "ModifiedDate";
            ModifiedDateRadioButton.UseVisualStyleBackColor = true;
            // 
            // CreatedDateRadioButton
            // 
            CreatedDateRadioButton.AutoSize = true;
            CreatedDateRadioButton.Checked = true;
            CreatedDateRadioButton.Location = new Point(9, 31);
            CreatedDateRadioButton.Margin = new Padding(5);
            CreatedDateRadioButton.Name = "CreatedDateRadioButton";
            CreatedDateRadioButton.Size = new Size(114, 24);
            CreatedDateRadioButton.TabIndex = 0;
            CreatedDateRadioButton.TabStop = true;
            CreatedDateRadioButton.Text = "CreatedDate";
            CreatedDateRadioButton.UseVisualStyleBackColor = true;
            // 
            // BrowseButton
            // 
            BrowseButton.Location = new Point(936, 43);
            BrowseButton.Margin = new Padding(5);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(101, 35);
            BrowseButton.TabIndex = 1;
            BrowseButton.Text = "Browse";
            BrowseButton.UseVisualStyleBackColor = true;
            BrowseButton.Click += BrowseButton_Click;
            // 
            // PathTextBox
            // 
            PathTextBox.BackColor = Color.White;
            PathTextBox.Enabled = false;
            PathTextBox.Location = new Point(27, 43);
            PathTextBox.Margin = new Padding(5);
            PathTextBox.Name = "PathTextBox";
            PathTextBox.Size = new Size(899, 27);
            PathTextBox.TabIndex = 0;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // FileSystemReporterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 833);
            Controls.Add(panel1);
            Margin = new Padding(5);
            Name = "FileSystemReporterForm";
            Text = "FileSystemReporterForm";
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
        private ListBox ResultListBox;
        private ProgressBar ProgressBar;
        private Button ReportButton;
        private Button RunButton;
        private FileSystemWatcher fileSystemWatcher1;
        private SaveFileDialog SaveDialog;
    }
}


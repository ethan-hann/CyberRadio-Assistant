using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms
{
    partial class AudioConverterForm
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
                AudioConverter.Instance.ConversionStarted -= OnConversionStarted;
                AudioConverter.Instance.ConversionProgress -= OnConversionProgress;
                AudioConverter.Instance.ConversionCompleted -= OnConversionCompleted;
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioConverterForm));
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnCancel = new Button();
            btnStartConversion = new Button();
            btnAddFiles = new Button();
            grpConversionLog = new GroupBox();
            rtbConversionLog = new RichTextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnUncheckAll = new Button();
            btnCheckAll = new Button();
            fdlgOpenSongs = new OpenFileDialog();
            fdlgChangeOutput = new FolderBrowserDialog();
            splitContainer1 = new SplitContainer();
            lbCandidates = new CheckedListBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            lblTotalConversions = new Label();
            pgConvertCandidate = new AetherUtils.Core.WinForms.Controls.AuPropertyGrid(components);
            splitContainer2 = new SplitContainer();
            btnRemoveFiles = new Button();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            grpConversionLog.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, toolStripStatusLabel1, progressBar });
            statusStrip1.Location = new Point(0, 502);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(945, 25);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 8;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.status__16x16;
            lblStatus.Margin = new Padding(5, 3, 0, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(2);
            lblStatus.Size = new Size(59, 20);
            lblStatus.Text = "Ready";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(714, 20);
            toolStripStatusLabel1.Spring = true;
            // 
            // progressBar
            // 
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(150, 19);
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.5306129F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.4693871F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 391F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96F));
            tableLayoutPanel1.Controls.Add(btnRemoveFiles, 1, 0);
            tableLayoutPanel1.Controls.Add(btnAddFiles, 0, 0);
            tableLayoutPanel1.Controls.Add(btnCancel, 3, 0);
            tableLayoutPanel1.Controls.Add(btnStartConversion, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 469);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(945, 33);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Yellow;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.Image = Properties.Resources.cancel_16x16;
            btnCancel.Location = new Point(851, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(91, 27);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.TextAlign = ContentAlignment.MiddleRight;
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnStartConversion
            // 
            btnStartConversion.BackColor = Color.Yellow;
            btnStartConversion.Dock = DockStyle.Fill;
            btnStartConversion.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnStartConversion.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnStartConversion.FlatStyle = FlatStyle.Flat;
            btnStartConversion.Font = new Font("Segoe UI", 9F);
            btnStartConversion.Image = Properties.Resources.sound_waves__16x16;
            btnStartConversion.Location = new Point(460, 3);
            btnStartConversion.Name = "btnStartConversion";
            btnStartConversion.Size = new Size(385, 27);
            btnStartConversion.TabIndex = 8;
            btnStartConversion.Text = "Start Conversion";
            btnStartConversion.TextAlign = ContentAlignment.MiddleRight;
            btnStartConversion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStartConversion.UseVisualStyleBackColor = false;
            btnStartConversion.Click += btnStartConversion_Click;
            // 
            // btnAddFiles
            // 
            btnAddFiles.BackColor = Color.Yellow;
            btnAddFiles.Dock = DockStyle.Fill;
            btnAddFiles.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddFiles.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddFiles.FlatStyle = FlatStyle.Flat;
            btnAddFiles.Font = new Font("Segoe UI", 9F);
            btnAddFiles.Image = Properties.Resources.add__16x16;
            btnAddFiles.Location = new Point(3, 3);
            btnAddFiles.Name = "btnAddFiles";
            btnAddFiles.Size = new Size(230, 27);
            btnAddFiles.TabIndex = 7;
            btnAddFiles.Text = "Add Files...";
            btnAddFiles.TextAlign = ContentAlignment.MiddleRight;
            btnAddFiles.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddFiles.UseVisualStyleBackColor = false;
            btnAddFiles.Click += btnAddFiles_Click;
            // 
            // grpConversionLog
            // 
            grpConversionLog.Controls.Add(rtbConversionLog);
            grpConversionLog.Dock = DockStyle.Fill;
            grpConversionLog.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpConversionLog.Location = new Point(0, 0);
            grpConversionLog.Name = "grpConversionLog";
            grpConversionLog.Size = new Size(945, 161);
            grpConversionLog.TabIndex = 10;
            grpConversionLog.TabStop = false;
            grpConversionLog.Text = "Conversion Log";
            // 
            // rtbConversionLog
            // 
            rtbConversionLog.BackColor = Color.White;
            rtbConversionLog.Dock = DockStyle.Fill;
            rtbConversionLog.Location = new Point(3, 21);
            rtbConversionLog.Name = "rtbConversionLog";
            rtbConversionLog.ReadOnly = true;
            rtbConversionLog.Size = new Size(939, 137);
            rtbConversionLog.TabIndex = 0;
            rtbConversionLog.Text = "";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnUncheckAll, 1, 0);
            tableLayoutPanel2.Controls.Add(btnCheckAll, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(312, 32);
            tableLayoutPanel2.TabIndex = 11;
            // 
            // btnUncheckAll
            // 
            btnUncheckAll.BackColor = Color.Yellow;
            btnUncheckAll.Dock = DockStyle.Fill;
            btnUncheckAll.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnUncheckAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnUncheckAll.FlatStyle = FlatStyle.Flat;
            btnUncheckAll.Font = new Font("Segoe UI", 9F);
            btnUncheckAll.Image = Properties.Resources.disabled__16x16;
            btnUncheckAll.Location = new Point(159, 3);
            btnUncheckAll.Name = "btnUncheckAll";
            btnUncheckAll.Size = new Size(150, 26);
            btnUncheckAll.TabIndex = 9;
            btnUncheckAll.Text = "Uncheck All";
            btnUncheckAll.TextAlign = ContentAlignment.MiddleRight;
            btnUncheckAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUncheckAll.UseVisualStyleBackColor = false;
            btnUncheckAll.Click += btnUncheckAll_Click;
            // 
            // btnCheckAll
            // 
            btnCheckAll.BackColor = Color.Yellow;
            btnCheckAll.Dock = DockStyle.Fill;
            btnCheckAll.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCheckAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCheckAll.FlatStyle = FlatStyle.Flat;
            btnCheckAll.Font = new Font("Segoe UI", 9F);
            btnCheckAll.Image = Properties.Resources.enabled__16x16;
            btnCheckAll.Location = new Point(3, 3);
            btnCheckAll.Name = "btnCheckAll";
            btnCheckAll.Size = new Size(150, 26);
            btnCheckAll.TabIndex = 8;
            btnCheckAll.Text = "Check All";
            btnCheckAll.TextAlign = ContentAlignment.MiddleRight;
            btnCheckAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCheckAll.UseVisualStyleBackColor = false;
            btnCheckAll.Click += btnCheckAll_Click;
            // 
            // fdlgOpenSongs
            // 
            fdlgOpenSongs.Multiselect = true;
            // 
            // fdlgChangeOutput
            // 
            fdlgChangeOutput.UseDescriptionForTitle = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lbCandidates);
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel2);
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel4);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pgConvertCandidate);
            splitContainer1.Size = new Size(945, 304);
            splitContainer1.SplitterDistance = 312;
            splitContainer1.TabIndex = 13;
            // 
            // lbCandidates
            // 
            lbCandidates.BackColor = Color.White;
            lbCandidates.Dock = DockStyle.Fill;
            lbCandidates.FormattingEnabled = true;
            lbCandidates.Location = new Point(0, 32);
            lbCandidates.Name = "lbCandidates";
            lbCandidates.Size = new Size(312, 244);
            lbCandidates.TabIndex = 12;
            lbCandidates.ThreeDCheckBoxes = true;
            lbCandidates.ItemCheck += lbCandidates_ItemCheck;
            lbCandidates.SelectedIndexChanged += lbCandidates_SelectedIndexChanged;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(lblTotalConversions, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Bottom;
            tableLayoutPanel4.Location = new Point(0, 276);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(312, 28);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // lblTotalConversions
            // 
            lblTotalConversions.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblTotalConversions.AutoSize = true;
            lblTotalConversions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalConversions.Location = new Point(3, 4);
            lblTotalConversions.Name = "lblTotalConversions";
            lblTotalConversions.Size = new Size(306, 19);
            lblTotalConversions.TabIndex = 0;
            lblTotalConversions.Text = "Converting: {0} / {1}";
            lblTotalConversions.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pgConvertCandidate
            // 
            pgConvertCandidate.Dock = DockStyle.Fill;
            pgConvertCandidate.Location = new Point(0, 0);
            pgConvertCandidate.Name = "pgConvertCandidate";
            pgConvertCandidate.Size = new Size(629, 304);
            pgConvertCandidate.TabIndex = 0;
            pgConvertCandidate.PropertyValueChanged += pgConvertCandidate_PropertyValueChanged;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(grpConversionLog);
            splitContainer2.Size = new Size(945, 469);
            splitContainer2.SplitterDistance = 304;
            splitContainer2.TabIndex = 14;
            // 
            // btnRemoveFiles
            // 
            btnRemoveFiles.BackColor = Color.Yellow;
            btnRemoveFiles.Dock = DockStyle.Fill;
            btnRemoveFiles.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemoveFiles.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemoveFiles.FlatStyle = FlatStyle.Flat;
            btnRemoveFiles.Font = new Font("Segoe UI", 9F);
            btnRemoveFiles.Image = Properties.Resources.delete__16x16;
            btnRemoveFiles.Location = new Point(239, 3);
            btnRemoveFiles.Name = "btnRemoveFiles";
            btnRemoveFiles.Size = new Size(215, 27);
            btnRemoveFiles.TabIndex = 10;
            btnRemoveFiles.Text = "Remove Selected Files";
            btnRemoveFiles.TextAlign = ContentAlignment.MiddleRight;
            btnRemoveFiles.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemoveFiles.UseVisualStyleBackColor = false;
            btnRemoveFiles.Click += btnRemoveFiles_Click;
            // 
            // AudioConverterForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(945, 527);
            Controls.Add(splitContainer2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AudioConverterForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Convert Audio";
            FormClosing += AudioConverterForm_FormClosing;
            Load += AudioConverterForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            grpConversionLog.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnAddFiles;
        private GroupBox grpConversionLog;
        private RichTextBox rtbConversionLog;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnUncheckAll;
        private Button btnCheckAll;
        private Button btnStartConversion;
        private OpenFileDialog fdlgOpenSongs;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripProgressBar progressBar;
        private FolderBrowserDialog fdlgChangeOutput;
        private Button btnCancel;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lblTotalConversions;
        private AetherUtils.Core.WinForms.Controls.AuPropertyGrid pgConvertCandidate;
        private CheckedListBox lbCandidates;
        private Button btnRemoveFiles;
    }
}
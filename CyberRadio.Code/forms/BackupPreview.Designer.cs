namespace RadioExt_Helper.forms
{
    partial class BackupPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupPreview));
            lvFilePreviews = new ListView();
            colFileName = new ColumnHeader();
            colFileSize = new ColumnHeader();
            lblTotalSizeLbl = new Label();
            lblEstimatedSizeLbl = new Label();
            lblTotalSize = new Label();
            lblEstimatedSize = new Label();
            tvFiles = new TreeView();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblCompressionLevel = new Label();
            btnStartBackup = new Button();
            lblEstimatedDisclaimer = new Label();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            pgProgress = new ToolStripProgressBar();
            splitContainer1 = new SplitContainer();
            tableLayoutPanel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // lvFilePreviews
            // 
            lvFilePreviews.Columns.AddRange(new ColumnHeader[] { colFileName, colFileSize });
            lvFilePreviews.Dock = DockStyle.Fill;
            lvFilePreviews.Location = new Point(0, 0);
            lvFilePreviews.Name = "lvFilePreviews";
            lvFilePreviews.Size = new Size(743, 573);
            lvFilePreviews.TabIndex = 0;
            lvFilePreviews.UseCompatibleStateImageBehavior = false;
            lvFilePreviews.View = View.Details;
            lvFilePreviews.ColumnClick += LvFilePreviews_ColumnClick;
            // 
            // colFileName
            // 
            colFileName.Text = "File Name";
            // 
            // colFileSize
            // 
            colFileSize.Text = "File Size";
            // 
            // lblTotalSizeLbl
            // 
            lblTotalSizeLbl.Anchor = AnchorStyles.Right;
            lblTotalSizeLbl.AutoSize = true;
            lblTotalSizeLbl.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            lblTotalSizeLbl.Location = new Point(57, 0);
            lblTotalSizeLbl.Name = "lblTotalSizeLbl";
            lblTotalSizeLbl.Size = new Size(97, 17);
            lblTotalSizeLbl.TabIndex = 1;
            lblTotalSizeLbl.Text = "Total File Size: ";
            // 
            // lblEstimatedSizeLbl
            // 
            lblEstimatedSizeLbl.Anchor = AnchorStyles.Right;
            lblEstimatedSizeLbl.AutoSize = true;
            lblEstimatedSizeLbl.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            lblEstimatedSizeLbl.Location = new Point(3, 24);
            lblEstimatedSizeLbl.Name = "lblEstimatedSizeLbl";
            lblEstimatedSizeLbl.Size = new Size(151, 17);
            lblEstimatedSizeLbl.TabIndex = 2;
            lblEstimatedSizeLbl.Text = "Estimated Backup Size: ";
            // 
            // lblTotalSize
            // 
            lblTotalSize.Anchor = AnchorStyles.Left;
            lblTotalSize.AutoSize = true;
            lblTotalSize.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            lblTotalSize.ForeColor = Color.Green;
            lblTotalSize.Location = new Point(160, 0);
            lblTotalSize.Name = "lblTotalSize";
            lblTotalSize.Size = new Size(83, 17);
            lblTotalSize.TabIndex = 3;
            lblTotalSize.Text = "<total Size>";
            // 
            // lblEstimatedSize
            // 
            lblEstimatedSize.Anchor = AnchorStyles.Left;
            lblEstimatedSize.AutoSize = true;
            lblEstimatedSize.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            lblEstimatedSize.ForeColor = Color.Green;
            lblEstimatedSize.Location = new Point(160, 24);
            lblEstimatedSize.Name = "lblEstimatedSize";
            lblEstimatedSize.Size = new Size(115, 17);
            lblEstimatedSize.TabIndex = 4;
            lblEstimatedSize.Text = "<estimated Size>";
            // 
            // tvFiles
            // 
            tvFiles.Dock = DockStyle.Fill;
            tvFiles.Location = new Point(0, 0);
            tvFiles.Name = "tvFiles";
            tvFiles.Size = new Size(369, 573);
            tvFiles.TabIndex = 5;
            tvFiles.AfterSelect += TvFiles_AfterSelect;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(lblCompressionLevel, 3, 0);
            tableLayoutPanel1.Controls.Add(btnStartBackup, 3, 1);
            tableLayoutPanel1.Controls.Add(lblTotalSizeLbl, 0, 0);
            tableLayoutPanel1.Controls.Add(lblTotalSize, 1, 0);
            tableLayoutPanel1.Controls.Add(lblEstimatedSize, 1, 1);
            tableLayoutPanel1.Controls.Add(lblEstimatedSizeLbl, 0, 1);
            tableLayoutPanel1.Controls.Add(lblEstimatedDisclaimer, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1124, 48);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // lblCompressionLevel
            // 
            lblCompressionLevel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblCompressionLevel.AutoSize = true;
            lblCompressionLevel.Location = new Point(745, 1);
            lblCompressionLevel.Name = "lblCompressionLevel";
            lblCompressionLevel.Size = new Size(376, 15);
            lblCompressionLevel.TabIndex = 8;
            lblCompressionLevel.Text = "<compression Level>";
            lblCompressionLevel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnStartBackup
            // 
            btnStartBackup.AutoSize = true;
            btnStartBackup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnStartBackup.BackColor = Color.Yellow;
            btnStartBackup.Dock = DockStyle.Fill;
            btnStartBackup.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnStartBackup.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnStartBackup.FlatStyle = FlatStyle.Flat;
            btnStartBackup.Image = Properties.Resources.zip_file_16x16;
            btnStartBackup.Location = new Point(745, 19);
            btnStartBackup.Margin = new Padding(3, 2, 3, 2);
            btnStartBackup.Name = "btnStartBackup";
            btnStartBackup.Size = new Size(376, 27);
            btnStartBackup.TabIndex = 6;
            btnStartBackup.Text = "Start Backup";
            btnStartBackup.TextAlign = ContentAlignment.MiddleRight;
            btnStartBackup.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStartBackup.UseVisualStyleBackColor = false;
            btnStartBackup.Click += BtnStartBackup_Click;
            // 
            // lblEstimatedDisclaimer
            // 
            lblEstimatedDisclaimer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblEstimatedDisclaimer.AutoSize = true;
            lblEstimatedDisclaimer.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblEstimatedDisclaimer.Location = new Point(281, 7);
            lblEstimatedDisclaimer.Name = "lblEstimatedDisclaimer";
            tableLayoutPanel1.SetRowSpan(lblEstimatedDisclaimer, 2);
            lblEstimatedDisclaimer.Size = new Size(458, 34);
            lblEstimatedDisclaimer.TabIndex = 5;
            lblEstimatedDisclaimer.Text = "* This is an estimate based on the selected compression level in the configuration.\r\nActual size may be different.";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, toolStripStatusLabel2, pgProgress });
            statusStrip1.Location = new Point(0, 625);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1124, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.status__16x16;
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(121, 17);
            lblStatus.Text = "Creating preview...";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(686, 17);
            toolStripStatusLabel2.Spring = true;
            // 
            // pgProgress
            // 
            pgProgress.Name = "pgProgress";
            pgProgress.Size = new Size(300, 16);
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 48);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tvFiles);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lvFilePreviews);
            splitContainer1.Size = new Size(1124, 577);
            splitContainer1.SplitterDistance = 373;
            splitContainer1.TabIndex = 6;
            // 
            // BackupPreview
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1124, 647);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BackupPreview";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Backup Staging Folder";
            FormClosing += BackupPreview_FormClosing;
            Load += BackupPreview_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvFilePreviews;
        private Label lblTotalSizeLbl;
        private Label lblEstimatedSizeLbl;
        private Label lblTotalSize;
        private Label lblEstimatedSize;
        private TreeView tvFiles;
        private ColumnHeader colFileName;
        private ColumnHeader colFileSize;
        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripProgressBar pgProgress;
        private Button btnStartBackup;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblEstimatedDisclaimer;
        private Label lblCompressionLevel;
    }
}
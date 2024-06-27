namespace RadioExt_Helper.forms
{
    partial class UpdateBox
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateBox));
            btnDownload = new Button();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            pgDownloadProgress = new ToolStripProgressBar();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblCurrentVerText = new Label();
            lblCurrentVersion = new Label();
            lblNewVersionTxt = new Label();
            lblNewVersion = new Label();
            lblChangelogTxt = new Label();
            lnkChangelog = new LinkLabel();
            bgDownloadUpdate = new System.ComponentModel.BackgroundWorker();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnDownload
            // 
            btnDownload.BackColor = Color.Yellow;
            tableLayoutPanel1.SetColumnSpan(btnDownload, 2);
            btnDownload.Dock = DockStyle.Fill;
            btnDownload.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnDownload.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnDownload.FlatStyle = FlatStyle.Flat;
            btnDownload.Location = new Point(4, 129);
            btnDownload.Margin = new Padding(4, 3, 4, 3);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(635, 36);
            btnDownload.TabIndex = 1;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = false;
            btnDownload.Click += btnDownload_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, toolStripStatusLabel2, pgDownloadProgress });
            statusStrip1.Location = new Point(0, 168);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(643, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.info;
            lblStatus.Margin = new Padding(5, 3, 0, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(55, 17);
            lblStatus.Text = "Ready";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(316, 17);
            toolStripStatusLabel2.Spring = true;
            // 
            // pgDownloadProgress
            // 
            pgDownloadProgress.Name = "pgDownloadProgress";
            pgDownloadProgress.Size = new Size(250, 16);
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.12987F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.87013F));
            tableLayoutPanel1.Controls.Add(lblCurrentVerText, 0, 0);
            tableLayoutPanel1.Controls.Add(lblCurrentVersion, 1, 0);
            tableLayoutPanel1.Controls.Add(lblNewVersionTxt, 0, 1);
            tableLayoutPanel1.Controls.Add(lblNewVersion, 1, 1);
            tableLayoutPanel1.Controls.Add(lblChangelogTxt, 0, 2);
            tableLayoutPanel1.Controls.Add(lnkChangelog, 1, 2);
            tableLayoutPanel1.Controls.Add(btnDownload, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(643, 168);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // lblCurrentVerText
            // 
            lblCurrentVerText.Anchor = AnchorStyles.Right;
            lblCurrentVerText.AutoSize = true;
            lblCurrentVerText.Location = new Point(21, 12);
            lblCurrentVerText.Name = "lblCurrentVerText";
            lblCurrentVerText.Size = new Size(105, 17);
            lblCurrentVerText.TabIndex = 0;
            lblCurrentVerText.Text = "Current Version: ";
            // 
            // lblCurrentVersion
            // 
            lblCurrentVersion.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblCurrentVersion.AutoSize = true;
            lblCurrentVersion.Font = new Font("Segoe UI Variable Text", 9.75F, FontStyle.Bold);
            lblCurrentVersion.Location = new Point(132, 12);
            lblCurrentVersion.Name = "lblCurrentVersion";
            lblCurrentVersion.Size = new Size(508, 17);
            lblCurrentVersion.TabIndex = 2;
            lblCurrentVersion.Text = "label1";
            // 
            // lblNewVersionTxt
            // 
            lblNewVersionTxt.Anchor = AnchorStyles.Right;
            lblNewVersionTxt.AutoSize = true;
            lblNewVersionTxt.Location = new Point(39, 54);
            lblNewVersionTxt.Name = "lblNewVersionTxt";
            lblNewVersionTxt.Size = new Size(87, 17);
            lblNewVersionTxt.TabIndex = 3;
            lblNewVersionTxt.Text = "New Version: ";
            // 
            // lblNewVersion
            // 
            lblNewVersion.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblNewVersion.AutoSize = true;
            lblNewVersion.Font = new Font("Segoe UI Variable Text", 9.75F, FontStyle.Bold);
            lblNewVersion.Location = new Point(132, 54);
            lblNewVersion.Name = "lblNewVersion";
            lblNewVersion.Size = new Size(508, 17);
            lblNewVersion.TabIndex = 4;
            lblNewVersion.Text = "label3";
            // 
            // lblChangelogTxt
            // 
            lblChangelogTxt.Anchor = AnchorStyles.Right;
            lblChangelogTxt.AutoSize = true;
            lblChangelogTxt.Location = new Point(51, 96);
            lblChangelogTxt.Name = "lblChangelogTxt";
            lblChangelogTxt.Size = new Size(75, 17);
            lblChangelogTxt.TabIndex = 5;
            lblChangelogTxt.Text = "Changelog:";
            // 
            // lnkChangelog
            // 
            lnkChangelog.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lnkChangelog.AutoSize = true;
            lnkChangelog.Font = new Font("Segoe UI Variable Text", 9.75F, FontStyle.Bold);
            lnkChangelog.Location = new Point(132, 96);
            lnkChangelog.Name = "lnkChangelog";
            lnkChangelog.Size = new Size(508, 17);
            lnkChangelog.TabIndex = 6;
            lnkChangelog.TabStop = true;
            lnkChangelog.Text = "linkLabel1";
            lnkChangelog.LinkClicked += lnkChangelog_LinkClicked;
            // 
            // bgDownloadUpdate
            // 
            bgDownloadUpdate.WorkerReportsProgress = true;
            bgDownloadUpdate.DoWork += bgDownloadUpdate_DoWork;
            bgDownloadUpdate.RunWorkerCompleted += bgDownloadUpdate_RunWorkerCompleted;
            // 
            // UpdateBox
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(643, 190);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UpdateBox";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Update Available";
            TopMost = true;
            Load += UpdateBox_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.ProgressBar progressBar;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripProgressBar pgDownloadProgress;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblCurrentVerText;
        private Label lblCurrentVersion;
        private Label lblNewVersionTxt;
        private Label lblNewVersion;
        private Label lblChangelogTxt;
        private LinkLabel lnkChangelog;
        private System.ComponentModel.BackgroundWorker bgDownloadUpdate;
    }
}
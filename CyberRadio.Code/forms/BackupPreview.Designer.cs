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
            lvFilePreviews = new ListView();
            colFileName = new ColumnHeader();
            colFileSize = new ColumnHeader();
            label1 = new Label();
            label2 = new Label();
            lblTotalSize = new Label();
            lblEstimatedSize = new Label();
            tvFiles = new TreeView();
            panel1 = new Panel();
            panel2 = new Panel();
            statusStrip1 = new StatusStrip();
            splitContainer1 = new SplitContainer();
            lblPreviewStatusLabel = new ToolStripStatusLabel();
            pgPreviewProgress = new ToolStripProgressBar();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
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
            lvFilePreviews.Size = new Size(530, 371);
            lvFilePreviews.TabIndex = 0;
            lvFilePreviews.UseCompatibleStateImageBehavior = false;
            lvFilePreviews.View = View.Details;
            lvFilePreviews.ColumnClick += lvFilePreviews_ColumnClick;
            // 
            // colFileName
            // 
            colFileName.Text = "File Name";
            // 
            // colFileSize
            // 
            colFileSize.Text = "File Size";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(82, 16);
            label1.TabIndex = 1;
            label1.Text = "Total File Size: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 25);
            label2.Name = "label2";
            label2.Size = new Size(129, 16);
            label2.TabIndex = 2;
            label2.Text = "Estimated Backup Size: ";
            // 
            // lblTotalSize
            // 
            lblTotalSize.AutoSize = true;
            lblTotalSize.Location = new Point(138, 9);
            lblTotalSize.Name = "lblTotalSize";
            lblTotalSize.Size = new Size(38, 16);
            lblTotalSize.TabIndex = 3;
            lblTotalSize.Text = "label3";
            // 
            // lblEstimatedSize
            // 
            lblEstimatedSize.AutoSize = true;
            lblEstimatedSize.Location = new Point(138, 25);
            lblEstimatedSize.Name = "lblEstimatedSize";
            lblEstimatedSize.Size = new Size(38, 16);
            lblEstimatedSize.TabIndex = 4;
            lblEstimatedSize.Text = "label4";
            // 
            // tvFiles
            // 
            tvFiles.Dock = DockStyle.Fill;
            tvFiles.Location = new Point(0, 0);
            tvFiles.Name = "tvFiles";
            tvFiles.Size = new Size(266, 371);
            tvFiles.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblEstimatedSize);
            panel1.Controls.Add(statusStrip1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblTotalSize);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 371);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 79);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.Controls.Add(splitContainer1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 371);
            panel2.TabIndex = 7;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblPreviewStatusLabel, toolStripStatusLabel2, pgPreviewProgress });
            statusStrip1.Location = new Point(0, 57);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tvFiles);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lvFilePreviews);
            splitContainer1.Size = new Size(800, 371);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 6;
            // 
            // lblPreviewStatusLabel
            // 
            lblPreviewStatusLabel.Name = "lblPreviewStatusLabel";
            lblPreviewStatusLabel.Size = new Size(104, 17);
            lblPreviewStatusLabel.Text = "Creating preview...";
            // 
            // pgPreviewProgress
            // 
            pgPreviewProgress.Name = "pgPreviewProgress";
            pgPreviewProgress.Size = new Size(300, 16);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(348, 17);
            toolStripStatusLabel2.Spring = true;
            // 
            // BackupPreview
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "BackupPreview";
            Text = "BackupPreview";
            Load += BackupPreview_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView lvFilePreviews;
        private Label label1;
        private Label label2;
        private Label lblTotalSize;
        private Label lblEstimatedSize;
        private TreeView tvFiles;
        private Panel panel1;
        private Panel panel2;
        private ColumnHeader colFileName;
        private ColumnHeader colFileSize;
        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private ToolStripStatusLabel lblPreviewStatusLabel;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripProgressBar pgPreviewProgress;
    }
}
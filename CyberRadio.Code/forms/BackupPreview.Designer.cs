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
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // lvFilePreviews
            // 
            lvFilePreviews.Columns.AddRange(new ColumnHeader[] { colFileName, colFileSize });
            lvFilePreviews.Dock = DockStyle.Fill;
            lvFilePreviews.Location = new Point(242, 0);
            lvFilePreviews.Name = "lvFilePreviews";
            lvFilePreviews.Size = new Size(558, 371);
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
            label1.Location = new Point(14, 11);
            label1.Name = "label1";
            label1.Size = new Size(82, 16);
            label1.TabIndex = 1;
            label1.Text = "Total File Size: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 41);
            label2.Name = "label2";
            label2.Size = new Size(129, 16);
            label2.TabIndex = 2;
            label2.Text = "Estimated Backup Size: ";
            // 
            // lblTotalSize
            // 
            lblTotalSize.AutoSize = true;
            lblTotalSize.Location = new Point(102, 11);
            lblTotalSize.Name = "lblTotalSize";
            lblTotalSize.Size = new Size(38, 16);
            lblTotalSize.TabIndex = 3;
            lblTotalSize.Text = "label3";
            // 
            // lblEstimatedSize
            // 
            lblEstimatedSize.AutoSize = true;
            lblEstimatedSize.Location = new Point(148, 41);
            lblEstimatedSize.Name = "lblEstimatedSize";
            lblEstimatedSize.Size = new Size(38, 16);
            lblEstimatedSize.TabIndex = 4;
            lblEstimatedSize.Text = "label4";
            // 
            // tvFiles
            // 
            tvFiles.Dock = DockStyle.Left;
            tvFiles.Location = new Point(0, 0);
            tvFiles.Name = "tvFiles";
            tvFiles.Size = new Size(242, 371);
            tvFiles.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(lblEstimatedSize);
            panel1.Controls.Add(lblTotalSize);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 371);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 79);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.Controls.Add(lvFilePreviews);
            panel2.Controls.Add(tvFiles);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 371);
            panel2.TabIndex = 7;
            // 
            // BackupPreview
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "BackupPreview";
            Text = "BackupPreview";
            Load += BackupPreview_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
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
    }
}
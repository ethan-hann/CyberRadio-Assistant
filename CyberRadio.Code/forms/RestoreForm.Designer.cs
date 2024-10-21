namespace RadioExt_Helper.forms
{
    partial class RestoreForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreForm));
            btnCancel = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblRestoreSize = new Label();
            btnStartRestore = new Button();
            lblDescription = new Label();
            splitContainer1 = new SplitContainer();
            tvFiles = new TreeView();
            lvFilePreviews = new ListView();
            colFileName = new ColumnHeader();
            colFileSize = new ColumnHeader();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            pgProgress = new ToolStripProgressBar();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCancel.BackColor = Color.Yellow;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Image = Properties.Resources.cancel_16x16;
            btnCancel.Location = new Point(807, 41);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(226, 31);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.TextAlign = ContentAlignment.MiddleRight;
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 232F));
            tableLayoutPanel2.Controls.Add(lblRestoreSize, 0, 1);
            tableLayoutPanel2.Controls.Add(btnCancel, 1, 1);
            tableLayoutPanel2.Controls.Add(btnStartRestore, 1, 0);
            tableLayoutPanel2.Controls.Add(lblDescription, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanel2.Size = new Size(1036, 75);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // lblRestoreSize
            // 
            lblRestoreSize.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblRestoreSize.AutoSize = true;
            lblRestoreSize.Font = new Font("Segoe UI Variable Small Semibol", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRestoreSize.Location = new Point(3, 48);
            lblRestoreSize.Name = "lblRestoreSize";
            lblRestoreSize.Size = new Size(798, 17);
            lblRestoreSize.TabIndex = 12;
            lblRestoreSize.Text = "Restored Size: {0}";
            // 
            // btnStartRestore
            // 
            btnStartRestore.AutoSize = true;
            btnStartRestore.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnStartRestore.BackColor = Color.Yellow;
            btnStartRestore.Dock = DockStyle.Fill;
            btnStartRestore.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnStartRestore.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnStartRestore.FlatStyle = FlatStyle.Flat;
            btnStartRestore.Image = Properties.Resources.export__16x16;
            btnStartRestore.Location = new Point(807, 2);
            btnStartRestore.Margin = new Padding(3, 2, 3, 2);
            btnStartRestore.Name = "btnStartRestore";
            btnStartRestore.Size = new Size(226, 34);
            btnStartRestore.TabIndex = 8;
            btnStartRestore.Text = "Start Restore";
            btnStartRestore.TextAlign = ContentAlignment.MiddleRight;
            btnStartRestore.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStartRestore.UseVisualStyleBackColor = false;
            btnStartRestore.Click += btnStartRestore_Click;
            // 
            // lblDescription
            // 
            lblDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI Variable Display", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescription.Location = new Point(3, 9);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(798, 20);
            lblDescription.TabIndex = 11;
            lblDescription.Text = "Review the files to be restored below. When ready, click \"Start Restore\".";
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 75);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tvFiles);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lvFilePreviews);
            splitContainer1.Size = new Size(1036, 675);
            splitContainer1.SplitterDistance = 339;
            splitContainer1.TabIndex = 7;
            // 
            // tvFiles
            // 
            tvFiles.Dock = DockStyle.Fill;
            tvFiles.Location = new Point(0, 0);
            tvFiles.Name = "tvFiles";
            tvFiles.Size = new Size(335, 671);
            tvFiles.TabIndex = 5;
            tvFiles.AfterSelect += tvFiles_AfterSelect;
            // 
            // lvFilePreviews
            // 
            lvFilePreviews.Columns.AddRange(new ColumnHeader[] { colFileName, colFileSize });
            lvFilePreviews.Dock = DockStyle.Fill;
            lvFilePreviews.Location = new Point(0, 0);
            lvFilePreviews.Name = "lvFilePreviews";
            lvFilePreviews.Size = new Size(689, 671);
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
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, toolStripStatusLabel2, pgProgress });
            statusStrip1.Location = new Point(0, 750);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1036, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.status__16x16;
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(55, 17);
            lblStatus.Text = "Ready";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(664, 17);
            toolStripStatusLabel2.Spring = true;
            // 
            // pgProgress
            // 
            pgProgress.Name = "pgProgress";
            pgProgress.Size = new Size(300, 16);
            // 
            // RestoreForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1036, 772);
            Controls.Add(splitContainer1);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(statusStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RestoreForm";
            ShowInTaskbar = false;
            Text = "Restore Backup - {0}";
            TopMost = true;
            FormClosing += RestoreForm_FormClosing;
            Load += RestoreForm_Load;
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private TableLayoutPanel tableLayoutPanel2;
        private SplitContainer splitContainer1;
        private TreeView tvFiles;
        private ListView lvFilePreviews;
        private ColumnHeader colFileName;
        private ColumnHeader colFileSize;
        private Button btnStartRestore;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripProgressBar pgProgress;
        private Label lblDescription;
        private Label lblRestoreSize;
    }
}
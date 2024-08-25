namespace RadioExt_Helper.forms
{
    partial class IconImportForm
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
            bgImportWorker = new System.ComponentModel.BackgroundWorker();
            rtbImportProgress = new RichTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnImportIcon = new Button();
            lblAtlasName = new Label();
            txtAtlasName = new TextBox();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            pgProgress = new ToolStripProgressBar();
            picIconPreview = new PictureBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnCancel = new Button();
            tableLayoutPanel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picIconPreview).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // bgImportWorker
            // 
            bgImportWorker.WorkerReportsProgress = true;
            bgImportWorker.WorkerSupportsCancellation = true;
            bgImportWorker.DoWork += bgImportWorker_DoWork;
            bgImportWorker.ProgressChanged += bgImportWorker_ProgressChanged;
            bgImportWorker.RunWorkerCompleted += bgImportWorker_RunWorkerCompleted;
            // 
            // rtbImportProgress
            // 
            rtbImportProgress.Dock = DockStyle.Fill;
            rtbImportProgress.Location = new Point(140, 3);
            rtbImportProgress.Name = "rtbImportProgress";
            rtbImportProgress.ReadOnly = true;
            rtbImportProgress.Size = new Size(588, 210);
            rtbImportProgress.TabIndex = 0;
            rtbImportProgress.Text = "";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.3503647F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.6496353F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 183F));
            tableLayoutPanel1.Controls.Add(btnImportIcon, 2, 0);
            tableLayoutPanel1.Controls.Add(lblAtlasName, 0, 0);
            tableLayoutPanel1.Controls.Add(txtAtlasName, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(731, 29);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnImportIcon
            // 
            btnImportIcon.BackColor = Color.Yellow;
            btnImportIcon.Dock = DockStyle.Fill;
            btnImportIcon.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnImportIcon.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnImportIcon.FlatStyle = FlatStyle.Flat;
            btnImportIcon.Image = Properties.Resources.magic_wand_16x16;
            btnImportIcon.Location = new Point(551, 2);
            btnImportIcon.Margin = new Padding(3, 2, 3, 2);
            btnImportIcon.Name = "btnImportIcon";
            btnImportIcon.Size = new Size(177, 25);
            btnImportIcon.TabIndex = 2;
            btnImportIcon.Text = "Import";
            btnImportIcon.TextAlign = ContentAlignment.MiddleRight;
            btnImportIcon.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnImportIcon.UseVisualStyleBackColor = false;
            btnImportIcon.Click += BtnImportIcon_Click;
            // 
            // lblAtlasName
            // 
            lblAtlasName.Anchor = AnchorStyles.Right;
            lblAtlasName.AutoSize = true;
            lblAtlasName.Location = new Point(40, 7);
            lblAtlasName.Name = "lblAtlasName";
            lblAtlasName.Size = new Size(74, 15);
            lblAtlasName.TabIndex = 0;
            lblAtlasName.Text = "Atlas Name: ";
            // 
            // txtAtlasName
            // 
            txtAtlasName.Dock = DockStyle.Fill;
            txtAtlasName.Location = new Point(120, 3);
            txtAtlasName.Name = "txtAtlasName";
            txtAtlasName.Size = new Size(425, 23);
            txtAtlasName.TabIndex = 1;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, toolStripStatusLabel2, pgProgress });
            statusStrip1.Location = new Point(0, 275);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(731, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 2;
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
            toolStripStatusLabel2.Size = new Size(428, 17);
            toolStripStatusLabel2.Spring = true;
            // 
            // pgProgress
            // 
            pgProgress.Name = "pgProgress";
            pgProgress.Size = new Size(200, 16);
            pgProgress.Visible = false;
            // 
            // picIconPreview
            // 
            picIconPreview.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            picIconPreview.Location = new Point(3, 48);
            picIconPreview.Name = "picIconPreview";
            picIconPreview.Size = new Size(131, 120);
            picIconPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picIconPreview.TabIndex = 3;
            picIconPreview.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.8782482F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 81.12175F));
            tableLayoutPanel2.Controls.Add(btnCancel, 1, 1);
            tableLayoutPanel2.Controls.Add(picIconPreview, 0, 0);
            tableLayoutPanel2.Controls.Add(rtbImportProgress, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 29);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.Size = new Size(731, 246);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Right;
            btnCancel.BackColor = Color.Yellow;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Image = Properties.Resources.cancel_16x16;
            btnCancel.Location = new Point(551, 218);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(177, 25);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.TextAlign = ContentAlignment.MiddleRight;
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Visible = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // IconImportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(731, 297);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(statusStrip1);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IconImportForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Importing Icon: {0}";
            TopMost = true;
            Load += IconImportForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picIconPreview).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgImportWorker;
        private RichTextBox rtbImportProgress;
        private TableLayoutPanel tableLayoutPanel1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripProgressBar pgProgress;
        private Label lblAtlasName;
        private TextBox txtAtlasName;
        private Button btnImportIcon;
        private PictureBox picIconPreview;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnCancel;
    }
}
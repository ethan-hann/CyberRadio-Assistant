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
            lvFiles = new ListView();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnStartConversion = new Button();
            btnAddFiles = new Button();
            grpConversionLog = new GroupBox();
            rtbConversionLog = new RichTextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnUncheckAll = new Button();
            btnCheckAll = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            statusStrip2 = new StatusStrip();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            lblTotalConversions = new ToolStripStatusLabel();
            fdlgOpenSongs = new OpenFileDialog();
            cmsFiles = new ContextMenuStrip(components);
            changeOutputToolStripMenuItem = new ToolStripMenuItem();
            fdlgChangeOutput = new FolderBrowserDialog();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            grpConversionLog.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            statusStrip2.SuspendLayout();
            cmsFiles.SuspendLayout();
            SuspendLayout();
            // 
            // lvFiles
            // 
            lvFiles.CheckBoxes = true;
            lvFiles.Dock = DockStyle.Top;
            lvFiles.Location = new Point(0, 38);
            lvFiles.Name = "lvFiles";
            lvFiles.ShowGroups = false;
            lvFiles.Size = new Size(612, 204);
            lvFiles.TabIndex = 0;
            lvFiles.UseCompatibleStateImageBehavior = false;
            lvFiles.View = View.Details;
            lvFiles.ItemChecked += lvFiles_ItemChecked;
            lvFiles.MouseDown += lvFiles_MouseDown;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, toolStripStatusLabel1, progressBar });
            statusStrip1.Location = new Point(0, 409);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(612, 25);
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
            toolStripStatusLabel1.Size = new Size(381, 20);
            toolStripStatusLabel1.Spring = true;
            // 
            // progressBar
            // 
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(150, 19);
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.5306129F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.4693871F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 219F));
            tableLayoutPanel1.Controls.Add(btnStartConversion, 1, 0);
            tableLayoutPanel1.Controls.Add(btnAddFiles, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 376);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(612, 33);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // btnStartConversion
            // 
            btnStartConversion.BackColor = Color.Yellow;
            tableLayoutPanel1.SetColumnSpan(btnStartConversion, 2);
            btnStartConversion.Dock = DockStyle.Fill;
            btnStartConversion.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnStartConversion.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnStartConversion.FlatStyle = FlatStyle.Flat;
            btnStartConversion.Font = new Font("Segoe UI", 9F);
            btnStartConversion.Image = Properties.Resources.sound_waves__16x16;
            btnStartConversion.Location = new Point(205, 3);
            btnStartConversion.Name = "btnStartConversion";
            btnStartConversion.Size = new Size(404, 27);
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
            btnAddFiles.Size = new Size(196, 27);
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
            grpConversionLog.Location = new Point(0, 242);
            grpConversionLog.Name = "grpConversionLog";
            grpConversionLog.Size = new Size(612, 134);
            grpConversionLog.TabIndex = 10;
            grpConversionLog.TabStop = false;
            grpConversionLog.Text = "Conversion Log";
            // 
            // rtbConversionLog
            // 
            rtbConversionLog.BackColor = Color.White;
            rtbConversionLog.Dock = DockStyle.Fill;
            rtbConversionLog.Location = new Point(3, 19);
            rtbConversionLog.Name = "rtbConversionLog";
            rtbConversionLog.ReadOnly = true;
            rtbConversionLog.Size = new Size(606, 112);
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
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(245, 32);
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
            btnUncheckAll.Location = new Point(125, 3);
            btnUncheckAll.Name = "btnUncheckAll";
            btnUncheckAll.Size = new Size(117, 26);
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
            btnCheckAll.Size = new Size(116, 26);
            btnCheckAll.TabIndex = 8;
            btnCheckAll.Text = "Check All";
            btnCheckAll.TextAlign = ContentAlignment.MiddleRight;
            btnCheckAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCheckAll.UseVisualStyleBackColor = false;
            btnCheckAll.Click += btnCheckAll_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.17647F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.0196075F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.96732F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel3.Controls.Add(statusStrip2, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(612, 38);
            tableLayoutPanel3.TabIndex = 12;
            // 
            // statusStrip2
            // 
            statusStrip2.BackColor = Color.Transparent;
            statusStrip2.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel3, lblTotalConversions });
            statusStrip2.Location = new Point(397, 16);
            statusStrip2.Name = "statusStrip2";
            statusStrip2.Size = new Size(215, 22);
            statusStrip2.SizingGrip = false;
            statusStrip2.TabIndex = 12;
            statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(53, 17);
            toolStripStatusLabel3.Spring = true;
            // 
            // lblTotalConversions
            // 
            lblTotalConversions.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalConversions.Name = "lblTotalConversions";
            lblTotalConversions.Size = new Size(116, 17);
            lblTotalConversions.Text = "Converting: {0} / {1}";
            lblTotalConversions.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fdlgOpenSongs
            // 
            fdlgOpenSongs.Multiselect = true;
            // 
            // cmsFiles
            // 
            cmsFiles.Items.AddRange(new ToolStripItem[] { changeOutputToolStripMenuItem });
            cmsFiles.Name = "cmsFiles";
            cmsFiles.Size = new Size(193, 26);
            // 
            // changeOutputToolStripMenuItem
            // 
            changeOutputToolStripMenuItem.Image = Properties.Resources.folder__16x16;
            changeOutputToolStripMenuItem.Name = "changeOutputToolStripMenuItem";
            changeOutputToolStripMenuItem.Size = new Size(192, 22);
            changeOutputToolStripMenuItem.Text = "Change Output Path...";
            changeOutputToolStripMenuItem.Click += changeOutputToolStripMenuItem_Click;
            // 
            // fdlgChangeOutput
            // 
            fdlgChangeOutput.UseDescriptionForTitle = true;
            // 
            // AudioConverterForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(612, 434);
            Controls.Add(grpConversionLog);
            Controls.Add(lvFiles);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
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
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            statusStrip2.ResumeLayout(false);
            statusStrip2.PerformLayout();
            cmsFiles.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvFiles;
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
        private TableLayoutPanel tableLayoutPanel3;
        private OpenFileDialog fdlgOpenSongs;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripProgressBar progressBar;
        private ContextMenuStrip cmsFiles;
        private ToolStripMenuItem changeOutputToolStripMenuItem;
        private FolderBrowserDialog fdlgChangeOutput;
        private StatusStrip statusStrip2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel lblTotalConversions;
    }
}
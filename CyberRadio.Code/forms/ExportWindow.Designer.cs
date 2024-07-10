namespace RadioExt_Helper.forms
{
    partial class ExportWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportWindow));
            lvStations = new ListView();
            colIsActive = new ColumnHeader();
            colStationName = new ColumnHeader();
            colIcon = new ColumnHeader();
            colSongCount = new ColumnHeader();
            colStreamURL = new ColumnHeader();
            colProposedPath = new ColumnHeader();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            pgExportProgress = new ToolStripProgressBar();
            bgWorkerExport = new System.ComponentModel.BackgroundWorker();
            btnExportToStaging = new Button();
            btnExportToGame = new Button();
            btnOpenStagingFolder = new Button();
            btnOpenGameFolder = new Button();
            bgWorkerExportGame = new System.ComponentModel.BackgroundWorker();
            splitContainer1 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            lblTip = new Label();
            btnCancel = new Button();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lvStations
            // 
            lvStations.Columns.AddRange(new ColumnHeader[] { colIsActive, colStationName, colIcon, colSongCount, colStreamURL, colProposedPath });
            lvStations.Dock = DockStyle.Fill;
            lvStations.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lvStations.Location = new Point(0, 0);
            lvStations.MultiSelect = false;
            lvStations.Name = "lvStations";
            lvStations.Size = new Size(927, 420);
            lvStations.Sorting = SortOrder.Ascending;
            lvStations.TabIndex = 0;
            lvStations.UseCompatibleStateImageBehavior = false;
            lvStations.View = View.Details;
            // 
            // colIsActive
            // 
            colIsActive.Text = "Enabled In Game?";
            // 
            // colStationName
            // 
            colStationName.Text = "Display Name";
            colStationName.Width = 120;
            // 
            // colIcon
            // 
            colIcon.Text = "Icon";
            colIcon.Width = 120;
            // 
            // colSongCount
            // 
            colSongCount.Text = "Song Count";
            colSongCount.Width = 120;
            // 
            // colStreamURL
            // 
            colStreamURL.Text = "Stream URL";
            colStreamURL.Width = 120;
            // 
            // colProposedPath
            // 
            colProposedPath.Text = "Proposed Path";
            colProposedPath.Width = 200;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, toolStripStatusLabel2, pgExportProgress });
            statusStrip1.Location = new Point(0, 420);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1204, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.status__16x16;
            lblStatus.Margin = new Padding(5, 3, 0, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(55, 17);
            lblStatus.Text = "Ready";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(727, 17);
            toolStripStatusLabel2.Spring = true;
            // 
            // pgExportProgress
            // 
            pgExportProgress.Name = "pgExportProgress";
            pgExportProgress.Size = new Size(400, 16);
            // 
            // bgWorkerExport
            // 
            bgWorkerExport.WorkerReportsProgress = true;
            bgWorkerExport.WorkerSupportsCancellation = true;
            bgWorkerExport.DoWork += BgWorkerExport_DoWork;
            bgWorkerExport.ProgressChanged += BgWorkerExport_ProgressChanged;
            bgWorkerExport.RunWorkerCompleted += BgWorkerExport_RunWorkerCompleted;
            // 
            // btnExportToStaging
            // 
            btnExportToStaging.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnExportToStaging.BackColor = Color.Yellow;
            btnExportToStaging.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnExportToStaging.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnExportToStaging.FlatStyle = FlatStyle.Flat;
            btnExportToStaging.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnExportToStaging.Image = Properties.Resources.export__16x16;
            btnExportToStaging.Location = new Point(3, 143);
            btnExportToStaging.Name = "btnExportToStaging";
            btnExportToStaging.Size = new Size(267, 43);
            btnExportToStaging.TabIndex = 4;
            btnExportToStaging.Text = "Export to Staging";
            btnExportToStaging.TextAlign = ContentAlignment.MiddleRight;
            btnExportToStaging.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExportToStaging.UseVisualStyleBackColor = false;
            btnExportToStaging.Click += BtnExportToStaging_Click;
            // 
            // btnExportToGame
            // 
            btnExportToGame.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnExportToGame.BackColor = Color.Yellow;
            btnExportToGame.Enabled = false;
            btnExportToGame.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnExportToGame.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnExportToGame.FlatStyle = FlatStyle.Flat;
            btnExportToGame.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnExportToGame.Image = Properties.Resources.game_16x16;
            btnExportToGame.Location = new Point(3, 209);
            btnExportToGame.Name = "btnExportToGame";
            btnExportToGame.Size = new Size(267, 43);
            btnExportToGame.TabIndex = 5;
            btnExportToGame.Text = "Export to Game";
            btnExportToGame.TextAlign = ContentAlignment.MiddleRight;
            btnExportToGame.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExportToGame.UseVisualStyleBackColor = false;
            btnExportToGame.Click += BtnExportToGame_Click;
            // 
            // btnOpenStagingFolder
            // 
            btnOpenStagingFolder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnOpenStagingFolder.BackColor = Color.Yellow;
            btnOpenStagingFolder.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnOpenStagingFolder.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnOpenStagingFolder.FlatStyle = FlatStyle.Flat;
            btnOpenStagingFolder.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnOpenStagingFolder.Image = Properties.Resources.folder_no_fill__16x16;
            btnOpenStagingFolder.Location = new Point(3, 11);
            btnOpenStagingFolder.Name = "btnOpenStagingFolder";
            btnOpenStagingFolder.Size = new Size(267, 43);
            btnOpenStagingFolder.TabIndex = 7;
            btnOpenStagingFolder.Text = "Open Staging Folder";
            btnOpenStagingFolder.TextAlign = ContentAlignment.MiddleRight;
            btnOpenStagingFolder.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnOpenStagingFolder.UseVisualStyleBackColor = false;
            btnOpenStagingFolder.Click += BtnOpenStagingFolder_Click;
            // 
            // btnOpenGameFolder
            // 
            btnOpenGameFolder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnOpenGameFolder.BackColor = Color.Yellow;
            btnOpenGameFolder.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnOpenGameFolder.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnOpenGameFolder.FlatStyle = FlatStyle.Flat;
            btnOpenGameFolder.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnOpenGameFolder.Image = Properties.Resources.folder_no_fill__16x16;
            btnOpenGameFolder.Location = new Point(3, 77);
            btnOpenGameFolder.Name = "btnOpenGameFolder";
            btnOpenGameFolder.Size = new Size(267, 43);
            btnOpenGameFolder.TabIndex = 9;
            btnOpenGameFolder.Text = "Open Game Radios Folder";
            btnOpenGameFolder.TextAlign = ContentAlignment.MiddleRight;
            btnOpenGameFolder.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnOpenGameFolder.UseVisualStyleBackColor = false;
            btnOpenGameFolder.Click += BtnOpenGameFolder_Click;
            // 
            // bgWorkerExportGame
            // 
            bgWorkerExportGame.WorkerReportsProgress = true;
            bgWorkerExportGame.WorkerSupportsCancellation = true;
            bgWorkerExportGame.DoWork += BgWorkerExportGame_DoWork;
            bgWorkerExportGame.ProgressChanged += BgWorkerExportGame_ProgressChanged;
            bgWorkerExportGame.RunWorkerCompleted += BgWorkerExportGame_RunWorkerCompleted;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lvStations);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel1);
            splitContainer1.Size = new Size(1204, 420);
            splitContainer1.SplitterDistance = 927;
            splitContainer1.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 4);
            tableLayoutPanel1.Controls.Add(btnOpenStagingFolder, 0, 0);
            tableLayoutPanel1.Controls.Add(btnOpenGameFolder, 0, 1);
            tableLayoutPanel1.Controls.Add(btnExportToGame, 0, 3);
            tableLayoutPanel1.Controls.Add(btnExportToStaging, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(273, 420);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblTip);
            panel1.Controls.Add(btnCancel);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 267);
            panel1.Name = "panel1";
            panel1.Size = new Size(267, 150);
            panel1.TabIndex = 11;
            // 
            // lblTip
            // 
            lblTip.AutoSize = true;
            lblTip.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTip.Image = Properties.Resources.enabled__16x16;
            lblTip.ImageAlign = ContentAlignment.BottomLeft;
            lblTip.Location = new Point(3, 10);
            lblTip.Name = "lblTip";
            lblTip.RightToLeft = RightToLeft.No;
            lblTip.Size = new Size(173, 34);
            lblTip.TabIndex = 7;
            lblTip.Text = "     Only Enabled stations are \r\nexported to the game.";
            lblTip.TextAlign = ContentAlignment.BottomRight;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Yellow;
            btnCancel.Dock = DockStyle.Bottom;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnCancel.Image = Properties.Resources.cancel_16x16;
            btnCancel.Location = new Point(0, 114);
            btnCancel.Margin = new Padding(3, 3, 3, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(267, 36);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.TextAlign = ContentAlignment.MiddleRight;
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Visible = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // ExportWindow
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1204, 442);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI Variable Display Semib", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExportWindow";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export";
            TopMost = true;
            HelpButtonClicked += ExportWindow_HelpButtonClicked;
            Load += ExportWindow_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView lvStations;
        private ColumnHeader colStationName;
        private ColumnHeader colIcon;
        private ColumnHeader colSongCount;
        private ColumnHeader colStreamURL;
        private ColumnHeader colProposedPath;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripProgressBar pgExportProgress;
        private System.ComponentModel.BackgroundWorker bgWorkerExport;
        private Button btnExportToStaging;
        private Button btnExportToGame;
        private Button btnOpenStagingFolder;
        private Button btnOpenGameFolder;
        private System.ComponentModel.BackgroundWorker bgWorkerExportGame;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private ColumnHeader colIsActive;
        private Panel panel1;
        private Button btnCancel;
        private Label lblTip;
    }
}
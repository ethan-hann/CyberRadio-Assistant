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
            colStationName = new ColumnHeader();
            colIcon = new ColumnHeader();
            colSongCount = new ColumnHeader();
            colStreamURL = new ColumnHeader();
            colProposedPath = new ColumnHeader();
            lblConfirm = new Label();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            pgExportProgress = new ToolStripProgressBar();
            bgWorkerExport = new System.ComponentModel.BackgroundWorker();
            btnExportToStaging = new Button();
            btnExportToGame = new Button();
            btnCancel = new Button();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lvStations
            // 
            lvStations.Columns.AddRange(new ColumnHeader[] { colStationName, colIcon, colSongCount, colStreamURL, colProposedPath });
            lvStations.Dock = DockStyle.Top;
            lvStations.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lvStations.Location = new Point(0, 0);
            lvStations.MultiSelect = false;
            lvStations.Name = "lvStations";
            lvStations.Size = new Size(1190, 296);
            lvStations.Sorting = SortOrder.Ascending;
            lvStations.TabIndex = 0;
            lvStations.UseCompatibleStateImageBehavior = false;
            lvStations.View = View.Details;
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
            // lblConfirm
            // 
            lblConfirm.AutoSize = true;
            lblConfirm.Font = new Font("CF Notche Demo", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConfirm.Location = new Point(12, 309);
            lblConfirm.Name = "lblConfirm";
            lblConfirm.Size = new Size(701, 60);
            lblConfirm.TabIndex = 1;
            lblConfirm.Text = resources.GetString("lblConfirm.Text");
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, toolStripStatusLabel2, pgExportProgress });
            statusStrip1.Location = new Point(0, 499);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1190, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.status;
            lblStatus.Margin = new Padding(5, 3, 0, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(55, 17);
            lblStatus.Text = "Ready";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(713, 17);
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
            bgWorkerExport.DoWork += bgWorkerExport_DoWork;
            bgWorkerExport.ProgressChanged += bgWorkerExport_ProgressChanged;
            bgWorkerExport.RunWorkerCompleted += bgWorkerExport_RunWorkerCompleted;
            // 
            // btnExportToStaging
            // 
            btnExportToStaging.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExportToStaging.Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportToStaging.Location = new Point(666, 453);
            btnExportToStaging.Name = "btnExportToStaging";
            btnExportToStaging.Size = new Size(247, 43);
            btnExportToStaging.TabIndex = 4;
            btnExportToStaging.Text = "Export to Staging";
            btnExportToStaging.UseVisualStyleBackColor = true;
            btnExportToStaging.Click += btnExportToStaging_Click;
            // 
            // btnExportToGame
            // 
            btnExportToGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExportToGame.Enabled = false;
            btnExportToGame.Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportToGame.Location = new Point(919, 453);
            btnExportToGame.Name = "btnExportToGame";
            btnExportToGame.Size = new Size(259, 43);
            btnExportToGame.TabIndex = 5;
            btnExportToGame.Text = "Export to Game";
            btnExportToGame.UseVisualStyleBackColor = true;
            btnExportToGame.Click += btnExportToGame_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancel.Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(12, 460);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(138, 36);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Visible = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // ExportWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1190, 521);
            Controls.Add(btnCancel);
            Controls.Add(btnExportToGame);
            Controls.Add(btnExportToStaging);
            Controls.Add(statusStrip1);
            Controls.Add(lvStations);
            Controls.Add(lblConfirm);
            Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExportWindow";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export";
            Load += ExportWindow_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView lvStations;
        private Label lblConfirm;
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
        private Button btnCancel;
    }
}
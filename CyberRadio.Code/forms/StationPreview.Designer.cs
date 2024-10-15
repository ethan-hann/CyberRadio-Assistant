namespace RadioExt_Helper.forms
{
    partial class StationPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationPreview));
            picStationIcon = new PictureBox();
            panStationPreview = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblNowPlaying = new Label();
            lblStationName = new Label();
            grpGamePreview = new GroupBox();
            grpTrackList = new GroupBox();
            lbTracks = new ListBox();
            mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnNormalize = new Button();
            bgLoader = new System.ComponentModel.BackgroundWorker();
            btnPlayStation = new Button();
            btnResetStationPreview = new Button();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).BeginInit();
            panStationPreview.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            grpGamePreview.SuspendLayout();
            grpTrackList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mediaPlayer).BeginInit();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // picStationIcon
            // 
            picStationIcon.BackColor = Color.Transparent;
            picStationIcon.Location = new Point(27, 131);
            picStationIcon.Name = "picStationIcon";
            picStationIcon.Size = new Size(272, 205);
            picStationIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picStationIcon.TabIndex = 0;
            picStationIcon.TabStop = false;
            picStationIcon.Paint += picStationIcon_Paint;
            // 
            // panStationPreview
            // 
            panStationPreview.BackColor = Color.Transparent;
            panStationPreview.BackgroundImage = Properties.Resources.cra_radio_preview_bg;
            panStationPreview.BackgroundImageLayout = ImageLayout.Stretch;
            panStationPreview.Controls.Add(tableLayoutPanel2);
            panStationPreview.Controls.Add(lblStationName);
            panStationPreview.Controls.Add(picStationIcon);
            panStationPreview.Dock = DockStyle.Fill;
            panStationPreview.Location = new Point(3, 21);
            panStationPreview.Name = "panStationPreview";
            panStationPreview.Size = new Size(794, 486);
            panStationPreview.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(lblNowPlaying, 0, 0);
            tableLayoutPanel2.Location = new Point(330, 131);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(452, 185);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // lblNowPlaying
            // 
            lblNowPlaying.AutoSize = true;
            lblNowPlaying.Font = new Font("Microsoft Sans Serif", 20.9999962F, FontStyle.Bold);
            lblNowPlaying.ForeColor = Color.FromArgb(111, 230, 239);
            lblNowPlaying.Location = new Point(3, 0);
            lblNowPlaying.Name = "lblNowPlaying";
            lblNowPlaying.Size = new Size(114, 32);
            lblNowPlaying.TabIndex = 0;
            lblNowPlaying.Text = "Track 1";
            // 
            // lblStationName
            // 
            lblStationName.AutoSize = true;
            lblStationName.Font = new Font("Microsoft Sans Serif", 23.9999962F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStationName.ForeColor = Color.FromArgb(111, 230, 239);
            lblStationName.Location = new Point(49, 426);
            lblStationName.Name = "lblStationName";
            lblStationName.Size = new Size(358, 37);
            lblStationName.TabIndex = 1;
            lblStationName.Text = "69.9 Awesome Station";
            // 
            // grpGamePreview
            // 
            grpGamePreview.Controls.Add(panStationPreview);
            grpGamePreview.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpGamePreview.Location = new Point(12, 12);
            grpGamePreview.Name = "grpGamePreview";
            grpGamePreview.Size = new Size(800, 510);
            grpGamePreview.TabIndex = 2;
            grpGamePreview.TabStop = false;
            grpGamePreview.Text = "Game Preview";
            // 
            // grpTrackList
            // 
            grpTrackList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grpTrackList.Controls.Add(lbTracks);
            grpTrackList.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpTrackList.Location = new Point(818, 12);
            grpTrackList.Name = "grpTrackList";
            grpTrackList.Size = new Size(311, 510);
            grpTrackList.TabIndex = 3;
            grpTrackList.TabStop = false;
            grpTrackList.Text = "Track List";
            // 
            // lbTracks
            // 
            lbTracks.Dock = DockStyle.Fill;
            lbTracks.FormattingEnabled = true;
            lbTracks.ItemHeight = 17;
            lbTracks.Location = new Point(3, 21);
            lbTracks.Name = "lbTracks";
            lbTracks.Size = new Size(305, 486);
            lbTracks.TabIndex = 0;
            lbTracks.SelectedIndexChanged += lbTracks_SelectedIndexChanged;
            // 
            // mediaPlayer
            // 
            mediaPlayer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            mediaPlayer.Enabled = true;
            mediaPlayer.Location = new Point(15, 528);
            mediaPlayer.Name = "mediaPlayer";
            mediaPlayer.OcxState = (AxHost.State)resources.GetObject("mediaPlayer.OcxState");
            mediaPlayer.Size = new Size(797, 167);
            mediaPlayer.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            groupBox1.Location = new Point(818, 528);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(311, 167);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Actions";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(btnResetStationPreview, 0, 1);
            tableLayoutPanel1.Controls.Add(btnPlayStation, 0, 0);
            tableLayoutPanel1.Controls.Add(btnNormalize, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 21);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(305, 143);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnNormalize
            // 
            btnNormalize.BackColor = Color.Yellow;
            btnNormalize.Dock = DockStyle.Fill;
            btnNormalize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnNormalize.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnNormalize.FlatStyle = FlatStyle.Flat;
            btnNormalize.Location = new Point(3, 97);
            btnNormalize.Name = "btnNormalize";
            btnNormalize.Size = new Size(299, 43);
            btnNormalize.TabIndex = 0;
            btnNormalize.Text = "Normalize Volumes";
            btnNormalize.UseVisualStyleBackColor = false;
            btnNormalize.Click += btnNormalize_Click;
            // 
            // bgLoader
            // 
            bgLoader.WorkerReportsProgress = true;
            bgLoader.WorkerSupportsCancellation = true;
            bgLoader.DoWork += bgLoader_DoWork;
            bgLoader.ProgressChanged += bgLoader_ProgressChanged;
            bgLoader.RunWorkerCompleted += bgLoader_RunWorkerCompleted;
            // 
            // btnPlayStation
            // 
            btnPlayStation.BackColor = Color.Yellow;
            btnPlayStation.Dock = DockStyle.Fill;
            btnPlayStation.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnPlayStation.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnPlayStation.FlatStyle = FlatStyle.Flat;
            btnPlayStation.Location = new Point(3, 3);
            btnPlayStation.Name = "btnPlayStation";
            btnPlayStation.Size = new Size(299, 41);
            btnPlayStation.TabIndex = 1;
            btnPlayStation.Text = "Play Station Preview";
            btnPlayStation.UseVisualStyleBackColor = false;
            btnPlayStation.Click += btnPlayStation_Click;
            // 
            // btnResetStationPreview
            // 
            btnResetStationPreview.BackColor = Color.Yellow;
            btnResetStationPreview.Dock = DockStyle.Fill;
            btnResetStationPreview.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnResetStationPreview.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnResetStationPreview.FlatStyle = FlatStyle.Flat;
            btnResetStationPreview.Location = new Point(3, 50);
            btnResetStationPreview.Name = "btnResetStationPreview";
            btnResetStationPreview.Size = new Size(299, 41);
            btnResetStationPreview.TabIndex = 2;
            btnResetStationPreview.Text = "Reset Station Preview";
            btnResetStationPreview.UseVisualStyleBackColor = false;
            btnResetStationPreview.Click += btnResetStationPreview_Click;
            // 
            // StationPreview
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1141, 707);
            Controls.Add(groupBox1);
            Controls.Add(mediaPlayer);
            Controls.Add(grpTrackList);
            Controls.Add(grpGamePreview);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StationPreview";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Station Preview: {0}";
            TopMost = true;
            FormClosing += StationPreview_FormClosing;
            Load += StationPreview_Load;
            ((System.ComponentModel.ISupportInitialize)picStationIcon).EndInit();
            panStationPreview.ResumeLayout(false);
            panStationPreview.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            grpGamePreview.ResumeLayout(false);
            grpTrackList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mediaPlayer).EndInit();
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picStationIcon;
        private Panel panStationPreview;
        private Label lblStationName;
        private GroupBox grpGamePreview;
        private GroupBox grpTrackList;
        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnNormalize;
        private ListBox lbTracks;
        private System.ComponentModel.BackgroundWorker bgLoader;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblNowPlaying;
        private Button btnResetStationPreview;
        private Button btnPlayStation;
    }
}
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
            ListViewGroup listViewGroup1 = new ListViewGroup("Unordered", HorizontalAlignment.Left);
            ListViewGroup listViewGroup2 = new ListViewGroup("Ordered", HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationPreview));
            picStationIcon = new PictureBox();
            panStationPreview = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblNowPlaying = new Label();
            lblStationName = new Label();
            grpGamePreview = new GroupBox();
            grpTrackList = new GroupBox();
            lvSongs = new ListView();
            colTitle = new ColumnHeader();
            colDuration = new ColumnHeader();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnStopStation = new Button();
            btnResetStationPreview = new Button();
            btnPlayStation = new Button();
            btnNormalize = new Button();
            audioController = new user_controls.AudioControllerCtl();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).BeginInit();
            panStationPreview.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            grpGamePreview.SuspendLayout();
            grpTrackList.SuspendLayout();
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
            grpGamePreview.Location = new Point(3, 0);
            grpGamePreview.Name = "grpGamePreview";
            grpGamePreview.Size = new Size(800, 510);
            grpGamePreview.TabIndex = 2;
            grpGamePreview.TabStop = false;
            grpGamePreview.Text = "Game Preview";
            // 
            // grpTrackList
            // 
            grpTrackList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grpTrackList.Controls.Add(lvSongs);
            grpTrackList.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpTrackList.Location = new Point(806, 0);
            grpTrackList.Name = "grpTrackList";
            grpTrackList.Size = new Size(332, 510);
            grpTrackList.TabIndex = 3;
            grpTrackList.TabStop = false;
            grpTrackList.Text = "Song List";
            // 
            // lvSongs
            // 
            lvSongs.Columns.AddRange(new ColumnHeader[] { colTitle, colDuration });
            lvSongs.Dock = DockStyle.Fill;
            listViewGroup1.Header = "Unordered";
            listViewGroup1.Name = "lvGrpUnordered";
            listViewGroup1.Subtitle = "All of the songs in the station not present in the defined order.";
            listViewGroup2.Header = "Ordered";
            listViewGroup2.Name = "lvGrpOrdered";
            listViewGroup2.Subtitle = "All of the ordered songs in the station in the order they will be played.";
            lvSongs.Groups.AddRange(new ListViewGroup[] { listViewGroup1, listViewGroup2 });
            lvSongs.Location = new Point(3, 21);
            lvSongs.Name = "lvSongs";
            lvSongs.Size = new Size(326, 486);
            lvSongs.TabIndex = 1;
            lvSongs.UseCompatibleStateImageBehavior = false;
            lvSongs.View = View.Details;
            // 
            // colTitle
            // 
            colTitle.Text = "Title";
            // 
            // colDuration
            // 
            colDuration.Text = "Duration";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            groupBox1.Location = new Point(806, 513);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(332, 191);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Actions";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(btnStopStation, 0, 3);
            tableLayoutPanel1.Controls.Add(btnResetStationPreview, 0, 1);
            tableLayoutPanel1.Controls.Add(btnPlayStation, 0, 0);
            tableLayoutPanel1.Controls.Add(btnNormalize, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 21);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(326, 167);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnStopStation
            // 
            btnStopStation.BackColor = Color.FromArgb(255, 128, 128);
            btnStopStation.Dock = DockStyle.Fill;
            btnStopStation.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnStopStation.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnStopStation.FlatStyle = FlatStyle.Flat;
            btnStopStation.Location = new Point(3, 126);
            btnStopStation.Name = "btnStopStation";
            btnStopStation.Size = new Size(320, 38);
            btnStopStation.TabIndex = 3;
            btnStopStation.Text = "Stop Station";
            btnStopStation.UseVisualStyleBackColor = false;
            btnStopStation.Click += btnStopStation_Click;
            // 
            // btnResetStationPreview
            // 
            btnResetStationPreview.BackColor = Color.Yellow;
            btnResetStationPreview.Dock = DockStyle.Fill;
            btnResetStationPreview.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnResetStationPreview.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnResetStationPreview.FlatStyle = FlatStyle.Flat;
            btnResetStationPreview.Location = new Point(3, 44);
            btnResetStationPreview.Name = "btnResetStationPreview";
            btnResetStationPreview.Size = new Size(320, 35);
            btnResetStationPreview.TabIndex = 2;
            btnResetStationPreview.Text = "Re-shuffle Station";
            btnResetStationPreview.UseVisualStyleBackColor = false;
            btnResetStationPreview.Click += btnResetStationPreview_Click;
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
            btnPlayStation.Size = new Size(320, 35);
            btnPlayStation.TabIndex = 1;
            btnPlayStation.Text = "Play Station";
            btnPlayStation.UseVisualStyleBackColor = false;
            btnPlayStation.Click += btnPlayStation_Click;
            // 
            // btnNormalize
            // 
            btnNormalize.BackColor = Color.Yellow;
            btnNormalize.Dock = DockStyle.Fill;
            btnNormalize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnNormalize.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnNormalize.FlatStyle = FlatStyle.Flat;
            btnNormalize.Location = new Point(3, 85);
            btnNormalize.Name = "btnNormalize";
            btnNormalize.Size = new Size(320, 35);
            btnNormalize.TabIndex = 0;
            btnNormalize.Text = "Normalize Song Volumes";
            btnNormalize.UseVisualStyleBackColor = false;
            btnNormalize.Click += btnNormalize_Click;
            // 
            // audioController
            // 
            audioController.BackColor = Color.Transparent;
            audioController.Location = new Point(6, 513);
            audioController.Name = "audioController";
            audioController.Size = new Size(797, 191);
            audioController.TabIndex = 6;
            audioController.SongEnded += audioController_SongEnded;
            audioController.PlaylistEnded += audioController_PlaylistEnded;
            // 
            // StationPreview
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1141, 707);
            Controls.Add(audioController);
            Controls.Add(groupBox1);
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
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnNormalize;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblNowPlaying;
        private Button btnResetStationPreview;
        private Button btnPlayStation;
        private ListView lvSongs;
        private ColumnHeader colTitle;
        private ColumnHeader colDuration;
        private user_controls.AudioControllerCtl audioController;
        private Button btnStopStation;
    }
}
namespace RadioExt_Helper.forms
{
    partial class VanillaStationSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VanillaStationSelector));
            tableLayoutPanel1 = new TableLayoutPanel();
            btnSelectStation = new Button();
            splitContainer1 = new SplitContainer();
            lbVanillaStations = new ListBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblHelp = new Label();
            lvTracks = new ListView();
            colTrackName = new ColumnHeader();
            colTrackArtist = new ColumnHeader();
            colTrackDuration = new ColumnHeader();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnSelectStation, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 513);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(999, 47);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnSelectStation
            // 
            btnSelectStation.BackColor = Color.Yellow;
            btnSelectStation.Dock = DockStyle.Fill;
            btnSelectStation.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnSelectStation.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnSelectStation.FlatStyle = FlatStyle.Flat;
            btnSelectStation.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnSelectStation.Image = Properties.Resources.add__16x16;
            btnSelectStation.Location = new Point(3, 3);
            btnSelectStation.Name = "btnSelectStation";
            btnSelectStation.Size = new Size(993, 41);
            btnSelectStation.TabIndex = 0;
            btnSelectStation.Text = "Replace Selected Station";
            btnSelectStation.TextAlign = ContentAlignment.MiddleRight;
            btnSelectStation.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSelectStation.UseVisualStyleBackColor = false;
            btnSelectStation.Click += btnSelectStation_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lbVanillaStations);
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lvTracks);
            splitContainer1.Size = new Size(999, 513);
            splitContainer1.SplitterDistance = 381;
            splitContainer1.TabIndex = 1;
            // 
            // lbVanillaStations
            // 
            lbVanillaStations.Dock = DockStyle.Fill;
            lbVanillaStations.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lbVanillaStations.FormattingEnabled = true;
            lbVanillaStations.ItemHeight = 17;
            lbVanillaStations.Location = new Point(0, 55);
            lbVanillaStations.Name = "lbVanillaStations";
            lbVanillaStations.Size = new Size(381, 458);
            lbVanillaStations.TabIndex = 0;
            lbVanillaStations.SelectedIndexChanged += lbVanillaStations_SelectedIndexChanged;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(lblHelp, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(381, 55);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // lblHelp
            // 
            lblHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblHelp.AutoSize = true;
            lblHelp.Location = new Point(3, 12);
            lblHelp.Name = "lblHelp";
            lblHelp.Size = new Size(375, 30);
            lblHelp.TabIndex = 0;
            lblHelp.Text = "Click a station name below to view tracks and then click the button to add it to CRA.";
            lblHelp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lvTracks
            // 
            lvTracks.Columns.AddRange(new ColumnHeader[] { colTrackName, colTrackArtist, colTrackDuration });
            lvTracks.Dock = DockStyle.Fill;
            lvTracks.FullRowSelect = true;
            lvTracks.GridLines = true;
            lvTracks.Location = new Point(0, 0);
            lvTracks.MultiSelect = false;
            lvTracks.Name = "lvTracks";
            lvTracks.ShowGroups = false;
            lvTracks.Size = new Size(614, 513);
            lvTracks.TabIndex = 0;
            lvTracks.UseCompatibleStateImageBehavior = false;
            lvTracks.View = View.Details;
            // 
            // colTrackName
            // 
            colTrackName.Text = "Track Title";
            colTrackName.Width = 120;
            // 
            // colTrackArtist
            // 
            colTrackArtist.Text = "Track Artist";
            colTrackArtist.Width = 120;
            // 
            // colTrackDuration
            // 
            colTrackDuration.Text = "Track Duration";
            colTrackDuration.Width = 120;
            // 
            // VanillaStationSelector
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(999, 560);
            Controls.Add(splitContainer1);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "VanillaStationSelector";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Vanilla Station";
            TopMost = true;
            Load += VanillaStationSelector_Load;
            tableLayoutPanel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button btnSelectStation;
        private SplitContainer splitContainer1;
        private ListBox lbVanillaStations;
        private ListView lvTracks;
        private ColumnHeader colTrackName;
        private ColumnHeader colTrackArtist;
        private ColumnHeader colTrackDuration;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblHelp;
    }
}
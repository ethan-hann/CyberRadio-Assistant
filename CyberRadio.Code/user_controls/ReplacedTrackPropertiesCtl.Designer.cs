namespace RadioExt_Helper.user_controls
{
    partial class ReplacedTrackPropertiesCtl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRemove = new Button();
            lblWEMIdHelp = new Label();
            grpReplacedTrack = new GroupBox();
            lvTracks = new ListView();
            colWemId = new ColumnHeader();
            colReplacedFile = new ColumnHeader();
            btnRemoveAll = new Button();
            btnReplace = new Button();
            btnReplaceAll = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            splitContainer1 = new SplitContainer();
            tableLayoutPanel3 = new TableLayoutPanel();
            lblWemIdDisclaimer = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            fdlgSelectFile = new OpenFileDialog();
            grpReplacedTrack.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.Yellow;
            btnRemove.Dock = DockStyle.Fill;
            btnRemove.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemove.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnRemove.Image = Properties.Resources.delete__16x16;
            btnRemove.ImageAlign = ContentAlignment.MiddleRight;
            btnRemove.Location = new Point(3, 97);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(322, 41);
            btnRemove.TabIndex = 7;
            btnRemove.Text = "Remove Replacement";
            btnRemove.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // lblWEMIdHelp
            // 
            lblWEMIdHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblWEMIdHelp.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(lblWEMIdHelp, 2);
            lblWEMIdHelp.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWEMIdHelp.Location = new Point(3, 11);
            lblWEMIdHelp.Name = "lblWEMIdHelp";
            lblWEMIdHelp.Size = new Size(1077, 34);
            lblWEMIdHelp.TabIndex = 4;
            lblWEMIdHelp.Text = "Select WEM IDs below and click \"Replace\" to select new audio file.\r\nYou can replace all IDs for this track or only a specific one.";
            lblWEMIdHelp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpReplacedTrack
            // 
            grpReplacedTrack.Controls.Add(lvTracks);
            grpReplacedTrack.Dock = DockStyle.Fill;
            grpReplacedTrack.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpReplacedTrack.Location = new Point(0, 0);
            grpReplacedTrack.Name = "grpReplacedTrack";
            grpReplacedTrack.Size = new Size(751, 383);
            grpReplacedTrack.TabIndex = 5;
            grpReplacedTrack.TabStop = false;
            grpReplacedTrack.Text = "Replaced Ids";
            // 
            // lvTracks
            // 
            lvTracks.Columns.AddRange(new ColumnHeader[] { colWemId, colReplacedFile });
            lvTracks.Dock = DockStyle.Fill;
            lvTracks.FullRowSelect = true;
            lvTracks.GridLines = true;
            lvTracks.Location = new Point(3, 21);
            lvTracks.MultiSelect = false;
            lvTracks.Name = "lvTracks";
            lvTracks.ShowGroups = false;
            lvTracks.Size = new Size(745, 359);
            lvTracks.TabIndex = 2;
            lvTracks.UseCompatibleStateImageBehavior = false;
            lvTracks.View = View.Details;
            // 
            // colWemId
            // 
            colWemId.Text = "WEM ID";
            colWemId.Width = 120;
            // 
            // colReplacedFile
            // 
            colReplacedFile.Text = "Replaced With File";
            colReplacedFile.Width = 150;
            // 
            // btnRemoveAll
            // 
            btnRemoveAll.BackColor = Color.LightYellow;
            btnRemoveAll.Dock = DockStyle.Fill;
            btnRemoveAll.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemoveAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemoveAll.FlatStyle = FlatStyle.Flat;
            btnRemoveAll.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnRemoveAll.Image = Properties.Resources.de_select_32x32;
            btnRemoveAll.ImageAlign = ContentAlignment.MiddleRight;
            btnRemoveAll.Location = new Point(3, 144);
            btnRemoveAll.Name = "btnRemoveAll";
            btnRemoveAll.Size = new Size(322, 43);
            btnRemoveAll.TabIndex = 4;
            btnRemoveAll.Text = "Remove All Replacements";
            btnRemoveAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemoveAll.UseVisualStyleBackColor = false;
            btnRemoveAll.Click += btnRemoveAll_Click;
            // 
            // btnReplace
            // 
            btnReplace.BackColor = Color.Yellow;
            btnReplace.Dock = DockStyle.Fill;
            btnReplace.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnReplace.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnReplace.FlatStyle = FlatStyle.Flat;
            btnReplace.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnReplace.Image = Properties.Resources.change__16x16;
            btnReplace.ImageAlign = ContentAlignment.MiddleRight;
            btnReplace.Location = new Point(3, 3);
            btnReplace.Name = "btnReplace";
            btnReplace.Size = new Size(322, 41);
            btnReplace.TabIndex = 6;
            btnReplace.Text = "Replace";
            btnReplace.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReplace.UseVisualStyleBackColor = false;
            btnReplace.Click += btnReplace_Click;
            // 
            // btnReplaceAll
            // 
            btnReplaceAll.BackColor = Color.LightYellow;
            btnReplaceAll.Dock = DockStyle.Fill;
            btnReplaceAll.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnReplaceAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnReplaceAll.FlatStyle = FlatStyle.Flat;
            btnReplaceAll.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnReplaceAll.Image = Properties.Resources.select_32x32;
            btnReplaceAll.ImageAlign = ContentAlignment.MiddleRight;
            btnReplaceAll.Location = new Point(3, 50);
            btnReplaceAll.Name = "btnReplaceAll";
            btnReplaceAll.Size = new Size(322, 41);
            btnReplaceAll.TabIndex = 3;
            btnReplaceAll.Text = "Replace All with Same Song";
            btnReplaceAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReplaceAll.UseVisualStyleBackColor = false;
            btnReplaceAll.Click += btnReplaceAll_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lblWEMIdHelp, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1083, 57);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 57);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(grpReplacedTrack);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel3);
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel2);
            splitContainer1.Size = new Size(1083, 383);
            splitContainer1.SplitterDistance = 751;
            splitContainer1.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(lblWemIdDisclaimer, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 190);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(328, 193);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // lblWemIdDisclaimer
            // 
            lblWemIdDisclaimer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblWemIdDisclaimer.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(lblWemIdDisclaimer, 2);
            lblWemIdDisclaimer.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblWemIdDisclaimer.Location = new Point(3, 62);
            lblWemIdDisclaimer.Name = "lblWemIdDisclaimer";
            lblWemIdDisclaimer.Size = new Size(322, 68);
            lblWemIdDisclaimer.TabIndex = 5;
            lblWemIdDisclaimer.Text = "It is recommended to replace all WEM IDs for a selected station.\r\nIt is unknown where exactly all the WEM IDs are used in-game.";
            lblWemIdDisclaimer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(btnReplace, 0, 0);
            tableLayoutPanel2.Controls.Add(btnReplaceAll, 0, 1);
            tableLayoutPanel2.Controls.Add(btnRemove, 0, 2);
            tableLayoutPanel2.Controls.Add(btnRemoveAll, 0, 3);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Size = new Size(328, 190);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // ReplacedTrackPropertiesCtl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            Controls.Add(splitContainer1);
            Controls.Add(tableLayoutPanel1);
            Name = "ReplacedTrackPropertiesCtl";
            Size = new Size(1083, 440);
            Load += ReplacedTrackPropertiesCtl_Load;
            grpReplacedTrack.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btnRemoveAll;
        private Button btnReplaceAll;
        private Label lblWEMIdHelp;
        private GroupBox grpReplacedTrack;
        private ListView lvTracks;
        private ColumnHeader colWemId;
        private ColumnHeader colReplacedFile;
        private Button btnReplace;
        private Button btnRemove;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lblWemIdDisclaimer;
        private OpenFileDialog fdlgSelectFile;
    }
}

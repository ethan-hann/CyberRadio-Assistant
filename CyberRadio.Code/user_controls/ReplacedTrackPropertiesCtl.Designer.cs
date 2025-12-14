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
            tableLayoutPanel2 = new TableLayoutPanel();
            btnRemove = new Button();
            lblWEMIdHelp = new Label();
            grpReplacedTrack = new GroupBox();
            lvTracks = new ListView();
            colWemId = new ColumnHeader();
            colReplacedFile = new ColumnHeader();
            btnRemoveAll = new Button();
            btnReplace = new Button();
            btnReplaceAll = new Button();
            tableLayoutPanel2.SuspendLayout();
            grpReplacedTrack.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.White;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnRemove, 1, 3);
            tableLayoutPanel2.Controls.Add(lblWEMIdHelp, 0, 0);
            tableLayoutPanel2.Controls.Add(grpReplacedTrack, 0, 1);
            tableLayoutPanel2.Controls.Add(btnRemoveAll, 1, 4);
            tableLayoutPanel2.Controls.Add(btnReplace, 0, 3);
            tableLayoutPanel2.Controls.Add(btnReplaceAll, 0, 4);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Font = new Font("Segoe UI Variable Text", 9.75F);
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 43.51145F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 56.48855F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 316F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel2.Size = new Size(798, 532);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.Yellow;
            btnRemove.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemove.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnRemove.Image = Properties.Resources.delete__16x16;
            btnRemove.ImageAlign = ContentAlignment.MiddleRight;
            btnRemove.Location = new Point(402, 450);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(393, 36);
            btnRemove.TabIndex = 7;
            btnRemove.Text = "Remove Replacement";
            btnRemove.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // lblWEMIdHelp
            // 
            lblWEMIdHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblWEMIdHelp.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(lblWEMIdHelp, 2);
            lblWEMIdHelp.Location = new Point(3, 11);
            lblWEMIdHelp.Name = "lblWEMIdHelp";
            lblWEMIdHelp.Size = new Size(792, 34);
            lblWEMIdHelp.TabIndex = 4;
            lblWEMIdHelp.Text = "Select WEM IDs below and click \"Replace\" to select new audio file.\r\nYou can replace all IDs for this track or only a specific one.";
            lblWEMIdHelp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpReplacedTrack
            // 
            tableLayoutPanel2.SetColumnSpan(grpReplacedTrack, 2);
            grpReplacedTrack.Controls.Add(lvTracks);
            grpReplacedTrack.Dock = DockStyle.Fill;
            grpReplacedTrack.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpReplacedTrack.Location = new Point(3, 60);
            grpReplacedTrack.Name = "grpReplacedTrack";
            tableLayoutPanel2.SetRowSpan(grpReplacedTrack, 2);
            grpReplacedTrack.Size = new Size(792, 384);
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
            lvTracks.Size = new Size(786, 360);
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
            btnRemoveAll.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemoveAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemoveAll.FlatStyle = FlatStyle.Flat;
            btnRemoveAll.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnRemoveAll.Image = Properties.Resources.de_select_32x32;
            btnRemoveAll.ImageAlign = ContentAlignment.MiddleRight;
            btnRemoveAll.Location = new Point(402, 492);
            btnRemoveAll.Name = "btnRemoveAll";
            btnRemoveAll.Size = new Size(393, 37);
            btnRemoveAll.TabIndex = 4;
            btnRemoveAll.Text = "Remove All Replacements";
            btnRemoveAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemoveAll.UseVisualStyleBackColor = false;
            // 
            // btnReplace
            // 
            btnReplace.BackColor = Color.Yellow;
            btnReplace.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnReplace.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnReplace.FlatStyle = FlatStyle.Flat;
            btnReplace.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnReplace.Image = Properties.Resources.change__16x16;
            btnReplace.ImageAlign = ContentAlignment.MiddleRight;
            btnReplace.Location = new Point(3, 450);
            btnReplace.Name = "btnReplace";
            btnReplace.Size = new Size(393, 36);
            btnReplace.TabIndex = 6;
            btnReplace.Text = "Replace";
            btnReplace.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReplace.UseVisualStyleBackColor = false;
            // 
            // btnReplaceAll
            // 
            btnReplaceAll.BackColor = Color.LightYellow;
            btnReplaceAll.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnReplaceAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnReplaceAll.FlatStyle = FlatStyle.Flat;
            btnReplaceAll.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnReplaceAll.Image = Properties.Resources.select_32x32;
            btnReplaceAll.ImageAlign = ContentAlignment.MiddleRight;
            btnReplaceAll.Location = new Point(3, 492);
            btnReplaceAll.Name = "btnReplaceAll";
            btnReplaceAll.Size = new Size(393, 37);
            btnReplaceAll.TabIndex = 3;
            btnReplaceAll.Text = "Replace All with Same Song";
            btnReplaceAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReplaceAll.UseVisualStyleBackColor = false;
            // 
            // ReplacedTrackPropertiesCtl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel2);
            Name = "ReplacedTrackPropertiesCtl";
            Size = new Size(798, 532);
            Load += ReplacedTrackPropertiesCtl_Load;
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            grpReplacedTrack.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel2;
        private Button btnRemoveAll;
        private Button btnReplaceAll;
        private Label lblWEMIdHelp;
        private GroupBox grpReplacedTrack;
        private ListView lvTracks;
        private ColumnHeader colWemId;
        private ColumnHeader colReplacedFile;
        private Button btnReplace;
        private Button btnRemove;
    }
}

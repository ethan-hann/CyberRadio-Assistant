using AetherUtils.Core.WinForms.Controls;

namespace RadioExt_Helper.user_controls
{
    sealed partial class ReplacementStationEditor
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
            components = new System.ComponentModel.Container();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            tabControl = new TabControl();
            tabMainInfo = new TabPage();
            grpNotes = new GroupBox();
            tinyEditor = new RadioExt_Helper.custom_controls.TinyMce();
            grpDisplay = new GroupBox();
            tlpDisplayTable = new TableLayoutPanel();
            lblIcon = new Label();
            txtDisplayName = new TextBox();
            lblDisplayName = new Label();
            lblVanillaName = new Label();
            txtVanillaStationName = new TextBox();
            pbStationIcon = new PictureBox();
            tabMusic = new TabPage();
            splitContainer1 = new SplitContainer();
            grpVanillaTracks = new GroupBox();
            lvTracks = new ListView();
            colReplaced = new ColumnHeader();
            colTrackName = new ColumnHeader();
            colTrackArtist = new ColumnHeader();
            colTrackDuration = new ColumnHeader();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnRemoveReplacedTrack = new SplitButton();
            ctxRemoveMenu = new ContextMenuStrip(components);
            btnRemoveAllTracks = new ToolStripMenuItem();
            btnReplaceTrack = new SplitButton();
            ctxReplaceMenu = new ContextMenuStrip(components);
            btnReplaceAllTracks = new ToolStripMenuItem();
            splitContainer2 = new SplitContainer();
            grpReplacedTracks = new GroupBox();
            lbReplacedTracks = new ListBox();
            grpTrackProperties = new GroupBox();
            lvReplacementTracks = new ListView();
            colWemId = new ColumnHeader();
            colReplacedFilePath = new ColumnHeader();
            pnlTrackProperties = new Panel();
            statusStrip1.SuspendLayout();
            tabControl.SuspendLayout();
            tabMainInfo.SuspendLayout();
            grpNotes.SuspendLayout();
            grpDisplay.SuspendLayout();
            tlpDisplayTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbStationIcon).BeginInit();
            tabMusic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            grpVanillaTracks.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ctxRemoveMenu.SuspendLayout();
            ctxReplaceMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            grpReplacedTracks.SuspendLayout();
            grpTrackProperties.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 807);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1234, 25);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 8;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.info__16x16;
            lblStatus.Margin = new Padding(5, 3, 0, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(2);
            lblStatus.Size = new Size(59, 20);
            lblStatus.Text = "Ready";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabMainInfo);
            tabControl.Controls.Add(tabMusic);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1234, 807);
            tabControl.TabIndex = 10;
            // 
            // tabMainInfo
            // 
            tabMainInfo.BackColor = Color.White;
            tabMainInfo.BorderStyle = BorderStyle.FixedSingle;
            tabMainInfo.Controls.Add(grpNotes);
            tabMainInfo.Controls.Add(grpDisplay);
            tabMainInfo.Font = new Font("Microsoft Sans Serif", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabMainInfo.ImageIndex = 0;
            tabMainInfo.Location = new Point(4, 29);
            tabMainInfo.Name = "tabMainInfo";
            tabMainInfo.Padding = new Padding(3);
            tabMainInfo.Size = new Size(1226, 774);
            tabMainInfo.TabIndex = 0;
            tabMainInfo.Text = "Main Info";
            // 
            // grpNotes
            // 
            grpNotes.BackColor = Color.White;
            grpNotes.Controls.Add(tinyEditor);
            grpNotes.Dock = DockStyle.Fill;
            grpNotes.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpNotes.Location = new Point(3, 208);
            grpNotes.Name = "grpNotes";
            grpNotes.Size = new Size(1218, 561);
            grpNotes.TabIndex = 4;
            grpNotes.TabStop = false;
            grpNotes.Text = "Notes";
            // 
            // tinyEditor
            // 
            tinyEditor.BackColor = Color.White;
            tinyEditor.Dock = DockStyle.Fill;
            tinyEditor.Language = "en";
            tinyEditor.Location = new Point(3, 21);
            tinyEditor.Margin = new Padding(0);
            tinyEditor.Name = "tinyEditor";
            tinyEditor.Size = new Size(1212, 537);
            tinyEditor.TabIndex = 0;
            // 
            // grpDisplay
            // 
            grpDisplay.BackColor = Color.White;
            grpDisplay.Controls.Add(tlpDisplayTable);
            grpDisplay.Dock = DockStyle.Top;
            grpDisplay.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpDisplay.Location = new Point(3, 3);
            grpDisplay.Name = "grpDisplay";
            grpDisplay.Size = new Size(1218, 205);
            grpDisplay.TabIndex = 3;
            grpDisplay.TabStop = false;
            grpDisplay.Text = "Display";
            // 
            // tlpDisplayTable
            // 
            tlpDisplayTable.ColumnCount = 2;
            tlpDisplayTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.9896374F));
            tlpDisplayTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 86.01036F));
            tlpDisplayTable.Controls.Add(lblIcon, 0, 2);
            tlpDisplayTable.Controls.Add(txtDisplayName, 1, 1);
            tlpDisplayTable.Controls.Add(lblDisplayName, 0, 1);
            tlpDisplayTable.Controls.Add(lblVanillaName, 0, 0);
            tlpDisplayTable.Controls.Add(txtVanillaStationName, 1, 0);
            tlpDisplayTable.Controls.Add(pbStationIcon, 1, 2);
            tlpDisplayTable.Dock = DockStyle.Fill;
            tlpDisplayTable.Location = new Point(3, 21);
            tlpDisplayTable.Name = "tlpDisplayTable";
            tlpDisplayTable.RowCount = 3;
            tlpDisplayTable.RowStyles.Add(new RowStyle(SizeType.Percent, 44.3038F));
            tlpDisplayTable.RowStyles.Add(new RowStyle(SizeType.Percent, 55.6962F));
            tlpDisplayTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 111F));
            tlpDisplayTable.Size = new Size(1212, 181);
            tlpDisplayTable.TabIndex = 0;
            // 
            // lblIcon
            // 
            lblIcon.Anchor = AnchorStyles.Right;
            lblIcon.AutoSize = true;
            lblIcon.Font = new Font("Segoe UI Variable Text", 9F);
            lblIcon.Location = new Point(93, 117);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(73, 16);
            lblIcon.TabIndex = 4;
            lblIcon.Text = "Station Icon:";
            lblIcon.MouseEnter += lblIcon_MouseEnter;
            lblIcon.MouseLeave += Lbl_MouseLeave;
            // 
            // txtDisplayName
            // 
            txtDisplayName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDisplayName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtDisplayName.Location = new Point(172, 38);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(1037, 23);
            txtDisplayName.TabIndex = 3;
            txtDisplayName.TextChanged += txtDisplayName_TextChanged;
            // 
            // lblDisplayName
            // 
            lblDisplayName.Anchor = AnchorStyles.Right;
            lblDisplayName.AutoSize = true;
            lblDisplayName.Font = new Font("Segoe UI Variable Text", 9F);
            lblDisplayName.Location = new Point(84, 42);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(82, 16);
            lblDisplayName.TabIndex = 2;
            lblDisplayName.Text = "Display Name:";
            lblDisplayName.MouseEnter += lblDisplayName_MouseEnter;
            lblDisplayName.MouseLeave += Lbl_MouseLeave;
            // 
            // lblVanillaName
            // 
            lblVanillaName.Anchor = AnchorStyles.Right;
            lblVanillaName.AutoSize = true;
            lblVanillaName.Font = new Font("Segoe UI Variable Text", 9F);
            lblVanillaName.Location = new Point(48, 7);
            lblVanillaName.Name = "lblVanillaName";
            lblVanillaName.Size = new Size(118, 16);
            lblVanillaName.TabIndex = 0;
            lblVanillaName.Text = "Vanilla Station Name:";
            lblVanillaName.MouseEnter += lblVanillaName_MouseEnter;
            lblVanillaName.MouseLeave += Lbl_MouseLeave;
            // 
            // txtVanillaStationName
            // 
            txtVanillaStationName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtVanillaStationName.Enabled = false;
            txtVanillaStationName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtVanillaStationName.Location = new Point(172, 4);
            txtVanillaStationName.Name = "txtVanillaStationName";
            txtVanillaStationName.ReadOnly = true;
            txtVanillaStationName.Size = new Size(1037, 23);
            txtVanillaStationName.TabIndex = 1;
            // 
            // pbStationIcon
            // 
            pbStationIcon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pbStationIcon.Location = new Point(172, 72);
            pbStationIcon.Name = "pbStationIcon";
            pbStationIcon.Size = new Size(163, 106);
            pbStationIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pbStationIcon.TabIndex = 5;
            pbStationIcon.TabStop = false;
            // 
            // tabMusic
            // 
            tabMusic.BackColor = Color.White;
            tabMusic.BorderStyle = BorderStyle.FixedSingle;
            tabMusic.Controls.Add(splitContainer1);
            tabMusic.ImageIndex = 1;
            tabMusic.Location = new Point(4, 29);
            tabMusic.Name = "tabMusic";
            tabMusic.Padding = new Padding(3);
            tabMusic.Size = new Size(1226, 774);
            tabMusic.TabIndex = 1;
            tabMusic.Text = "Tracks";
            tabMusic.ToolTipText = "Change the music this radio station will play.";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Font = new Font("Segoe UI", 9F);
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(grpVanillaTracks);
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1218, 766);
            splitContainer1.SplitterDistance = 303;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 0;
            // 
            // grpVanillaTracks
            // 
            grpVanillaTracks.Controls.Add(lvTracks);
            grpVanillaTracks.Dock = DockStyle.Fill;
            grpVanillaTracks.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpVanillaTracks.Location = new Point(0, 0);
            grpVanillaTracks.Name = "grpVanillaTracks";
            grpVanillaTracks.Size = new Size(1218, 266);
            grpVanillaTracks.TabIndex = 2;
            grpVanillaTracks.TabStop = false;
            grpVanillaTracks.Text = "Vanilla Tracks";
            // 
            // lvTracks
            // 
            lvTracks.Columns.AddRange(new ColumnHeader[] { colReplaced, colTrackName, colTrackArtist, colTrackDuration });
            lvTracks.Dock = DockStyle.Fill;
            lvTracks.FullRowSelect = true;
            lvTracks.GridLines = true;
            lvTracks.Location = new Point(3, 21);
            lvTracks.MultiSelect = false;
            lvTracks.Name = "lvTracks";
            lvTracks.ShowGroups = false;
            lvTracks.Size = new Size(1212, 242);
            lvTracks.TabIndex = 1;
            lvTracks.UseCompatibleStateImageBehavior = false;
            lvTracks.View = View.Details;
            // 
            // colReplaced
            // 
            colReplaced.Text = "Is Replaced?";
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnRemoveReplacedTrack, 1, 0);
            tableLayoutPanel1.Controls.Add(btnReplaceTrack, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 266);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1218, 37);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnRemoveReplacedTrack
            // 
            btnRemoveReplacedTrack.BackColor = Color.Yellow;
            btnRemoveReplacedTrack.Dock = DockStyle.Fill;
            btnRemoveReplacedTrack.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemoveReplacedTrack.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemoveReplacedTrack.FlatStyle = FlatStyle.Flat;
            btnRemoveReplacedTrack.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnRemoveReplacedTrack.Image = Properties.Resources.up__16x16;
            btnRemoveReplacedTrack.Location = new Point(612, 3);
            btnRemoveReplacedTrack.Menu = ctxRemoveMenu;
            btnRemoveReplacedTrack.Name = "btnRemoveReplacedTrack";
            btnRemoveReplacedTrack.Size = new Size(603, 31);
            btnRemoveReplacedTrack.SplitWidth = 35;
            btnRemoveReplacedTrack.TabIndex = 2;
            btnRemoveReplacedTrack.Text = "Remove Replaced Track";
            btnRemoveReplacedTrack.TextAlign = ContentAlignment.MiddleRight;
            btnRemoveReplacedTrack.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemoveReplacedTrack.UseVisualStyleBackColor = false;
            btnRemoveReplacedTrack.Click += btnRemoveReplacedTrack_Click;
            btnRemoveReplacedTrack.MouseEnter += btnRemoveReplacedTrack_MouseEnter;
            btnRemoveReplacedTrack.MouseLeave += Lbl_MouseLeave;
            // 
            // ctxRemoveMenu
            // 
            ctxRemoveMenu.Items.AddRange(new ToolStripItem[] { btnRemoveAllTracks });
            ctxRemoveMenu.Name = "ctxReplaceMenu";
            ctxRemoveMenu.Size = new Size(170, 26);
            // 
            // btnRemoveAllTracks
            // 
            btnRemoveAllTracks.Image = Properties.Resources.up__16x16;
            btnRemoveAllTracks.Name = "btnRemoveAllTracks";
            btnRemoveAllTracks.Size = new Size(169, 22);
            btnRemoveAllTracks.Text = "Remove All Tracks";
            btnRemoveAllTracks.Click += btnRemoveAllTracks_Click;
            btnRemoveAllTracks.MouseEnter += btnRemoveAllTracks_MouseEnter;
            btnRemoveAllTracks.MouseLeave += Lbl_MouseLeave;
            // 
            // btnReplaceTrack
            // 
            btnReplaceTrack.BackColor = Color.Yellow;
            btnReplaceTrack.Dock = DockStyle.Fill;
            btnReplaceTrack.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnReplaceTrack.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnReplaceTrack.FlatStyle = FlatStyle.Flat;
            btnReplaceTrack.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnReplaceTrack.Image = Properties.Resources.down__16x16;
            btnReplaceTrack.Location = new Point(3, 3);
            btnReplaceTrack.Menu = ctxReplaceMenu;
            btnReplaceTrack.Name = "btnReplaceTrack";
            btnReplaceTrack.Size = new Size(603, 31);
            btnReplaceTrack.SplitWidth = 35;
            btnReplaceTrack.TabIndex = 1;
            btnReplaceTrack.Text = "Replace Selected Track";
            btnReplaceTrack.TextAlign = ContentAlignment.MiddleRight;
            btnReplaceTrack.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReplaceTrack.UseVisualStyleBackColor = false;
            btnReplaceTrack.Click += btnReplaceTrack_Click;
            btnReplaceTrack.MouseLeave += Lbl_MouseLeave;
            // 
            // ctxReplaceMenu
            // 
            ctxReplaceMenu.Items.AddRange(new ToolStripItem[] { btnReplaceAllTracks });
            ctxReplaceMenu.Name = "ctxReplaceMenu";
            ctxReplaceMenu.Size = new Size(168, 26);
            // 
            // btnReplaceAllTracks
            // 
            btnReplaceAllTracks.Image = Properties.Resources.down__16x16;
            btnReplaceAllTracks.Name = "btnReplaceAllTracks";
            btnReplaceAllTracks.Size = new Size(167, 22);
            btnReplaceAllTracks.Text = "Replace All Tracks";
            btnReplaceAllTracks.Click += btnReplaceAllTracks_Click;
            btnReplaceAllTracks.MouseEnter += btnReplaceAllTracks_MouseEnter;
            btnReplaceAllTracks.MouseLeave += Lbl_MouseLeave;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(grpReplacedTracks);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(grpTrackProperties);
            splitContainer2.Size = new Size(1218, 455);
            splitContainer2.SplitterDistance = 242;
            splitContainer2.TabIndex = 1;
            // 
            // grpReplacedTracks
            // 
            grpReplacedTracks.Controls.Add(lbReplacedTracks);
            grpReplacedTracks.Dock = DockStyle.Fill;
            grpReplacedTracks.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpReplacedTracks.Location = new Point(0, 0);
            grpReplacedTracks.Name = "grpReplacedTracks";
            grpReplacedTracks.Size = new Size(242, 455);
            grpReplacedTracks.TabIndex = 1;
            grpReplacedTracks.TabStop = false;
            grpReplacedTracks.Text = "Tracks to Replace";
            // 
            // lbReplacedTracks
            // 
            lbReplacedTracks.Dock = DockStyle.Fill;
            lbReplacedTracks.Font = new Font("Segoe UI Variable Text", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbReplacedTracks.FormattingEnabled = true;
            lbReplacedTracks.ItemHeight = 17;
            lbReplacedTracks.Location = new Point(3, 21);
            lbReplacedTracks.Name = "lbReplacedTracks";
            lbReplacedTracks.Size = new Size(236, 431);
            lbReplacedTracks.TabIndex = 0;
            lbReplacedTracks.SelectedIndexChanged += lbReplacedTracks_SelectedIndexChanged;
            lbReplacedTracks.MouseEnter += lbReplacedTracks_MouseEnter;
            lbReplacedTracks.MouseLeave += Lbl_MouseLeave;
            // 
            // grpTrackProperties
            // 
            grpTrackProperties.Controls.Add(pnlTrackProperties);
            grpTrackProperties.Dock = DockStyle.Fill;
            grpTrackProperties.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpTrackProperties.Location = new Point(0, 0);
            grpTrackProperties.Name = "grpTrackProperties";
            grpTrackProperties.Size = new Size(972, 455);
            grpTrackProperties.TabIndex = 0;
            grpTrackProperties.TabStop = false;
            grpTrackProperties.Text = "Properties";
            // 
            // lvReplacementTracks
            // 
            lvReplacementTracks.Columns.AddRange(new ColumnHeader[] { colWemId, colReplacedFilePath });
            lvReplacementTracks.Dock = DockStyle.Fill;
            lvReplacementTracks.FullRowSelect = true;
            lvReplacementTracks.GridLines = true;
            lvReplacementTracks.Location = new Point(3, 21);
            lvReplacementTracks.MultiSelect = false;
            lvReplacementTracks.Name = "lvReplacementTracks";
            lvReplacementTracks.ShowGroups = false;
            lvReplacementTracks.Size = new Size(954, 226);
            lvReplacementTracks.TabIndex = 5;
            lvReplacementTracks.UseCompatibleStateImageBehavior = false;
            lvReplacementTracks.View = View.Details;
            // 
            // colWemId
            // 
            colWemId.Text = "WEM ID";
            colWemId.Width = 120;
            // 
            // colReplacedFilePath
            // 
            colReplacedFilePath.Text = "Replaced With File";
            colReplacedFilePath.Width = 150;
            // 
            // pnlTrackProperties
            // 
            pnlTrackProperties.Dock = DockStyle.Fill;
            pnlTrackProperties.Location = new Point(3, 21);
            pnlTrackProperties.Name = "pnlTrackProperties";
            pnlTrackProperties.Size = new Size(966, 431);
            pnlTrackProperties.TabIndex = 0;
            // 
            // ReplacementStationEditor
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            Controls.Add(tabControl);
            Controls.Add(statusStrip1);
            Name = "ReplacementStationEditor";
            Size = new Size(1234, 832);
            Load += ReplacementStationEditor_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabControl.ResumeLayout(false);
            tabMainInfo.ResumeLayout(false);
            grpNotes.ResumeLayout(false);
            grpDisplay.ResumeLayout(false);
            tlpDisplayTable.ResumeLayout(false);
            tlpDisplayTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbStationIcon).EndInit();
            tabMusic.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            grpVanillaTracks.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ctxRemoveMenu.ResumeLayout(false);
            ctxReplaceMenu.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            grpReplacedTracks.ResumeLayout(false);
            grpTrackProperties.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private TabControl tabControl;
        private TabPage tabMainInfo;
        private TabPage tabMusic;
        private GroupBox grpDisplay;
        private TableLayoutPanel tlpDisplayTable;
        private Label lblDisplayName;
        private Label lblVanillaName;
        private TextBox txtVanillaStationName;
        private TextBox txtDisplayName;
        private Label lblIcon;
        private PictureBox pbStationIcon;
        private GroupBox grpNotes;
        private custom_controls.TinyMce tinyEditor;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitButton btnRemoveReplacedTrack;
        private SplitButton btnReplaceTrack;
        private SplitContainer splitContainer2;
        private ListBox lbReplacedTracks;
        private ListView lvTracks;
        private ColumnHeader colTrackName;
        private ColumnHeader colTrackArtist;
        private ColumnHeader colTrackDuration;
        private ColumnHeader colReplaced;
        private ContextMenuStrip ctxReplaceMenu;
        private ToolStripMenuItem btnReplaceAllTracks;
        private ContextMenuStrip ctxRemoveMenu;
        private ToolStripMenuItem btnRemoveAllTracks;
        private GroupBox grpReplacedTracks;
        private GroupBox grpTrackProperties;
        private GroupBox grpVanillaTracks;
        private GroupBox groupBox1;
        private ListView lvReplacementTracks;
        private ColumnHeader colWemId;
        private ColumnHeader colReplacedFilePath;
        private Panel pnlTrackProperties;
    }
}

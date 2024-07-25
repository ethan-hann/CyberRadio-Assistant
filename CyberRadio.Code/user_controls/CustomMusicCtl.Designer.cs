namespace RadioExt_Helper.user_controls
{
    sealed partial class CustomMusicCtl
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
            lvSongs = new ListView();
            colFileExists = new ColumnHeader();
            colSongNames = new ColumnHeader();
            colArtist = new ColumnHeader();
            colSongLength = new ColumnHeader();
            colSongFileSize = new ColumnHeader();
            colFilePath = new ColumnHeader();
            fdlgOpenSongs = new OpenFileDialog();
            tabControl = new TabControl();
            tabSongs = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            btnRemoveAllSongs = new Button();
            btnRemoveSongs = new Button();
            tableLayoutPanel5 = new TableLayoutPanel();
            btnAddSongs = new Button();
            tabSongOrder = new TabPage();
            splitContainer1 = new SplitContainer();
            lbSongs = new ListBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnRemoveFromOrder = new Button();
            btnAddToOrder = new Button();
            lvSongOrder = new ListView();
            colOrderNum = new ColumnHeader();
            colSongName = new ColumnHeader();
            songBindingSource = new BindingSource(components);
            tableLayoutPanel4 = new TableLayoutPanel();
            button1 = new Button();
            label2 = new Label();
            cmsSongRightClick = new ContextMenuStrip(components);
            locateToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblTotalSongsLabel = new ToolStripStatusLabel();
            lblTotalSongsVal = new ToolStripStatusLabel();
            lblStationSizeLabel = new ToolStripStatusLabel();
            lblStationSizeVal = new ToolStripStatusLabel();
            toolStripStatusLabel5 = new ToolStripStatusLabel();
            toolStripStatusLabel6 = new ToolStripStatusLabel();
            lblStatSeperator = new ToolStripStatusLabel();
            tabControl.SuspendLayout();
            tabSongs.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tabSongOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)songBindingSource).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            cmsSongRightClick.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lvSongs
            // 
            lvSongs.Columns.AddRange(new ColumnHeader[] { colFileExists, colSongNames, colArtist, colSongLength, colSongFileSize, colFilePath });
            lvSongs.Dock = DockStyle.Fill;
            lvSongs.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lvSongs.FullRowSelect = true;
            lvSongs.GridLines = true;
            lvSongs.Location = new Point(3, 48);
            lvSongs.Name = "lvSongs";
            lvSongs.Size = new Size(905, 638);
            lvSongs.TabIndex = 1;
            lvSongs.UseCompatibleStateImageBehavior = false;
            lvSongs.View = View.Details;
            lvSongs.ColumnClick += LvSongs_ColumnClick;
            lvSongs.KeyDown += LvSongs_KeyDown;
            lvSongs.MouseDoubleClick += LvSongs_MouseDoubleClick;
            lvSongs.MouseDown += LvSongs_MouseDown;
            // 
            // colFileExists
            // 
            colFileExists.Text = "File Exists?";
            // 
            // colSongNames
            // 
            colSongNames.Text = "Name";
            colSongNames.Width = 150;
            // 
            // colArtist
            // 
            colArtist.Text = "Artist";
            colArtist.Width = 150;
            // 
            // colSongLength
            // 
            colSongLength.Text = "Length";
            colSongLength.Width = 150;
            // 
            // colSongFileSize
            // 
            colSongFileSize.Text = "File Size";
            colSongFileSize.Width = 150;
            // 
            // colFilePath
            // 
            colFilePath.Text = "Original File Path";
            colFilePath.Width = 150;
            // 
            // fdlgOpenSongs
            // 
            fdlgOpenSongs.Filter = "Music Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma";
            fdlgOpenSongs.Multiselect = true;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabSongs);
            tabControl.Controls.Add(tabSongOrder);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(919, 745);
            tabControl.TabIndex = 3;
            // 
            // tabSongs
            // 
            tabSongs.BackColor = Color.White;
            tabSongs.Controls.Add(lvSongs);
            tabSongs.Controls.Add(statusStrip1);
            tabSongs.Controls.Add(tableLayoutPanel2);
            tabSongs.ImageIndex = 0;
            tabSongs.Location = new Point(4, 30);
            tabSongs.Name = "tabSongs";
            tabSongs.Padding = new Padding(3);
            tabSongs.Size = new Size(911, 711);
            tabSongs.TabIndex = 0;
            tabSongs.Text = "Song Listing";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.19337F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.80663F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(905, 45);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65.5480957F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.4519F));
            tableLayoutPanel3.Controls.Add(btnRemoveAllSongs, 1, 0);
            tableLayoutPanel3.Controls.Add(btnRemoveSongs, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(412, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(490, 39);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // btnRemoveAllSongs
            // 
            btnRemoveAllSongs.BackColor = Color.Yellow;
            btnRemoveAllSongs.Dock = DockStyle.Fill;
            btnRemoveAllSongs.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemoveAllSongs.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemoveAllSongs.FlatStyle = FlatStyle.Flat;
            btnRemoveAllSongs.Image = Properties.Resources.delete__16x16;
            btnRemoveAllSongs.Location = new Point(324, 2);
            btnRemoveAllSongs.Margin = new Padding(3, 2, 3, 2);
            btnRemoveAllSongs.Name = "btnRemoveAllSongs";
            btnRemoveAllSongs.Size = new Size(163, 35);
            btnRemoveAllSongs.TabIndex = 3;
            btnRemoveAllSongs.Text = "Clear All!";
            btnRemoveAllSongs.TextAlign = ContentAlignment.MiddleRight;
            btnRemoveAllSongs.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemoveAllSongs.UseVisualStyleBackColor = false;
            btnRemoveAllSongs.Click += BtnRemoveAllSongs_Click;
            // 
            // btnRemoveSongs
            // 
            btnRemoveSongs.BackColor = Color.Yellow;
            btnRemoveSongs.Dock = DockStyle.Fill;
            btnRemoveSongs.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemoveSongs.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemoveSongs.FlatStyle = FlatStyle.Flat;
            btnRemoveSongs.Image = Properties.Resources.delete__16x16;
            btnRemoveSongs.Location = new Point(3, 2);
            btnRemoveSongs.Margin = new Padding(3, 2, 3, 2);
            btnRemoveSongs.Name = "btnRemoveSongs";
            btnRemoveSongs.Size = new Size(315, 35);
            btnRemoveSongs.TabIndex = 2;
            btnRemoveSongs.Text = "Remove Selected Song(s)";
            btnRemoveSongs.TextAlign = ContentAlignment.MiddleRight;
            btnRemoveSongs.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemoveSongs.UseVisualStyleBackColor = false;
            btnRemoveSongs.Click += BtnRemoveSongs_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(btnAddSongs, 0, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(403, 39);
            tableLayoutPanel5.TabIndex = 3;
            // 
            // btnAddSongs
            // 
            btnAddSongs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnAddSongs.BackColor = Color.Yellow;
            btnAddSongs.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddSongs.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddSongs.FlatStyle = FlatStyle.Flat;
            btnAddSongs.Image = Properties.Resources.add__16x16;
            btnAddSongs.Location = new Point(3, 2);
            btnAddSongs.Margin = new Padding(3, 2, 3, 2);
            btnAddSongs.Name = "btnAddSongs";
            btnAddSongs.Size = new Size(397, 35);
            btnAddSongs.TabIndex = 1;
            btnAddSongs.Text = "Add Song(s)";
            btnAddSongs.TextAlign = ContentAlignment.MiddleRight;
            btnAddSongs.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddSongs.UseVisualStyleBackColor = false;
            btnAddSongs.Click += BtnAddSongs_Click;
            // 
            // tabSongOrder
            // 
            tabSongOrder.BackColor = Color.White;
            tabSongOrder.Controls.Add(splitContainer1);
            tabSongOrder.ImageIndex = 1;
            tabSongOrder.Location = new Point(4, 30);
            tabSongOrder.Name = "tabSongOrder";
            tabSongOrder.Padding = new Padding(3);
            tabSongOrder.Size = new Size(911, 711);
            tabSongOrder.TabIndex = 1;
            tabSongOrder.Text = "Song Order";
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.White;
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lbSongs);
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lvSongOrder);
            splitContainer1.Size = new Size(905, 705);
            splitContainer1.SplitterDistance = 365;
            splitContainer1.SplitterWidth = 9;
            splitContainer1.TabIndex = 0;
            // 
            // lbSongs
            // 
            lbSongs.Dock = DockStyle.Fill;
            lbSongs.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbSongs.FormattingEnabled = true;
            lbSongs.ItemHeight = 20;
            lbSongs.Location = new Point(0, 0);
            lbSongs.Name = "lbSongs";
            lbSongs.SelectionMode = SelectionMode.MultiExtended;
            lbSongs.Size = new Size(901, 325);
            lbSongs.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnRemoveFromOrder, 1, 0);
            tableLayoutPanel1.Controls.Add(btnAddToOrder, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 325);
            tableLayoutPanel1.MaximumSize = new Size(0, 36);
            tableLayoutPanel1.MinimumSize = new Size(0, 36);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(901, 36);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnRemoveFromOrder
            // 
            btnRemoveFromOrder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnRemoveFromOrder.BackColor = Color.Yellow;
            btnRemoveFromOrder.BackgroundImage = Properties.Resources.up__32x32;
            btnRemoveFromOrder.BackgroundImageLayout = ImageLayout.Zoom;
            btnRemoveFromOrder.FlatAppearance.BorderSize = 0;
            btnRemoveFromOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemoveFromOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemoveFromOrder.FlatStyle = FlatStyle.Flat;
            btnRemoveFromOrder.ForeColor = Color.Black;
            btnRemoveFromOrder.Location = new Point(453, 3);
            btnRemoveFromOrder.Name = "btnRemoveFromOrder";
            btnRemoveFromOrder.Size = new Size(445, 30);
            btnRemoveFromOrder.TabIndex = 0;
            btnRemoveFromOrder.UseVisualStyleBackColor = false;
            btnRemoveFromOrder.Click += BtnRemoveFromOrder_Click;
            // 
            // btnAddToOrder
            // 
            btnAddToOrder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnAddToOrder.BackColor = Color.Yellow;
            btnAddToOrder.BackgroundImage = Properties.Resources.down__32x32;
            btnAddToOrder.BackgroundImageLayout = ImageLayout.Zoom;
            btnAddToOrder.FlatAppearance.BorderSize = 0;
            btnAddToOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddToOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddToOrder.FlatStyle = FlatStyle.Flat;
            btnAddToOrder.ForeColor = Color.Black;
            btnAddToOrder.Location = new Point(3, 3);
            btnAddToOrder.Name = "btnAddToOrder";
            btnAddToOrder.Size = new Size(444, 30);
            btnAddToOrder.TabIndex = 1;
            btnAddToOrder.UseVisualStyleBackColor = false;
            btnAddToOrder.Click += BtnAddToOrder_Click;
            // 
            // lvSongOrder
            // 
            lvSongOrder.AllowDrop = true;
            lvSongOrder.Columns.AddRange(new ColumnHeader[] { colOrderNum, colSongName });
            lvSongOrder.Dock = DockStyle.Fill;
            lvSongOrder.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lvSongOrder.FullRowSelect = true;
            lvSongOrder.GridLines = true;
            lvSongOrder.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvSongOrder.Location = new Point(0, 0);
            lvSongOrder.MultiSelect = false;
            lvSongOrder.Name = "lvSongOrder";
            lvSongOrder.Size = new Size(901, 327);
            lvSongOrder.TabIndex = 0;
            lvSongOrder.UseCompatibleStateImageBehavior = false;
            lvSongOrder.View = View.Details;
            lvSongOrder.ItemDrag += LvSongOrder_ItemDrag;
            lvSongOrder.DragDrop += LvSongOrder_DragDrop;
            lvSongOrder.DragEnter += LvSongOrder_DragEnter;
            lvSongOrder.DragOver += LvSongOrder_DragOver;
            // 
            // colOrderNum
            // 
            colOrderNum.Text = "Order";
            // 
            // colSongName
            // 
            colSongName.Text = "Name";
            colSongName.Width = 200;
            // 
            // songBindingSource
            // 
            songBindingSource.DataSource = typeof(models.Song);
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.4976959F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.5023041F));
            tableLayoutPanel4.Controls.Add(button1, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(200, 100);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Right;
            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(155, 32);
            button1.Margin = new Padding(3, 3, 10, 3);
            button1.Name = "button1";
            button1.Size = new Size(35, 35);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(3, 42);
            label2.Name = "label2";
            label2.Size = new Size(146, 15);
            label2.TabIndex = 0;
            label2.Text = "Add to Order";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmsSongRightClick
            // 
            cmsSongRightClick.Items.AddRange(new ToolStripItem[] { locateToolStripMenuItem });
            cmsSongRightClick.Name = "cmsSongRightClick";
            cmsSongRightClick.Size = new Size(193, 26);
            // 
            // locateToolStripMenuItem
            // 
            locateToolStripMenuItem.Image = Properties.Resources.search_16x16;
            locateToolStripMenuItem.Name = "locateToolStripMenuItem";
            locateToolStripMenuItem.Size = new Size(192, 22);
            locateToolStripMenuItem.Text = "Locate Missing Song...";
            locateToolStripMenuItem.Click += LocateToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(192, 255, 192);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel5, lblTotalSongsLabel, lblTotalSongsVal, lblStatSeperator, lblStationSizeLabel, lblStationSizeVal, toolStripStatusLabel6 });
            statusStrip1.Location = new Point(3, 686);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(905, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblTotalSongsLabel
            // 
            lblTotalSongsLabel.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            lblTotalSongsLabel.Image = Properties.Resources.music;
            lblTotalSongsLabel.Name = "lblTotalSongsLabel";
            lblTotalSongsLabel.Size = new Size(101, 17);
            lblTotalSongsLabel.Text = "Total Songs: ";
            // 
            // lblTotalSongsVal
            // 
            lblTotalSongsVal.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalSongsVal.Name = "lblTotalSongsVal";
            lblTotalSongsVal.Size = new Size(92, 17);
            lblTotalSongsVal.Text = "<song count>";
            // 
            // lblStationSizeLabel
            // 
            lblStationSizeLabel.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            lblStationSizeLabel.Image = Properties.Resources.station_size;
            lblStationSizeLabel.Name = "lblStationSizeLabel";
            lblStationSizeLabel.Size = new Size(103, 17);
            lblStationSizeLabel.Text = "Station Size: ";
            // 
            // lblStationSizeVal
            // 
            lblStationSizeVal.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStationSizeVal.Name = "lblStationSizeVal";
            lblStationSizeVal.Size = new Size(90, 17);
            lblStationSizeVal.Text = "<station size>";
            // 
            // toolStripStatusLabel5
            // 
            toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            toolStripStatusLabel5.Size = new Size(246, 17);
            toolStripStatusLabel5.Spring = true;
            // 
            // toolStripStatusLabel6
            // 
            toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            toolStripStatusLabel6.Size = new Size(246, 17);
            toolStripStatusLabel6.Spring = true;
            // 
            // lblStatSeperator
            // 
            lblStatSeperator.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            lblStatSeperator.Image = Properties.Resources.vertical_line;
            lblStatSeperator.Margin = new Padding(0, 3, 5, 2);
            lblStatSeperator.Name = "lblStatSeperator";
            lblStatSeperator.Size = new Size(16, 17);
            // 
            // CustomMusicCtl
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tabControl);
            Name = "CustomMusicCtl";
            Size = new Size(919, 745);
            Load += CustomMusicCtl_Load;
            tabControl.ResumeLayout(false);
            tabSongs.ResumeLayout(false);
            tabSongs.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tabSongOrder.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)songBindingSource).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            cmsSongRightClick.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ListView lvSongs;
        private ColumnHeader colSongNames;
        private ColumnHeader colSongLength;
        private ColumnHeader colSongFileSize;
        private OpenFileDialog fdlgOpenSongs;
        private ColumnHeader colArtist;
        private TabControl tabControl;
        private TabPage tabSongs;
        private TabPage tabSongOrder;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private ListBox lbSongs;
        private BindingSource songBindingSource;
        private Button btnRemoveFromOrder;
        private Button btnAddToOrder;
        private TableLayoutPanel tableLayoutPanel4;
        private Button button1;
        private Label label2;
        private ListView lvSongOrder;
        private ColumnHeader colOrderNum;
        private ColumnHeader colSongName;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnRemoveSongs;
        private Button btnAddSongs;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnRemoveAllSongs;
        private TableLayoutPanel tableLayoutPanel5;
        private ColumnHeader colFileExists;
        private ContextMenuStrip cmsSongRightClick;
        private ToolStripMenuItem locateToolStripMenuItem;
        private ColumnHeader colFilePath;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ToolStripStatusLabel lblTotalSongsLabel;
        private ToolStripStatusLabel lblTotalSongsVal;
        private ToolStripStatusLabel lblStatSeperator;
        private ToolStripStatusLabel lblStationSizeLabel;
        private ToolStripStatusLabel lblStationSizeVal;
        private ToolStripStatusLabel toolStripStatusLabel6;
    }
}

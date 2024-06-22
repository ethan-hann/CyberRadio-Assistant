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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomMusicCtl));
            menuStrip1 = new MenuStrip();
            addSongsToolStripMenuItem = new ToolStripMenuItem();
            removeSongsToolStripMenuItem = new ToolStripMenuItem();
            lvSongs = new ListView();
            colSongNames = new ColumnHeader();
            colArtist = new ColumnHeader();
            colSongLength = new ColumnHeader();
            colSongFileSize = new ColumnHeader();
            fdlgOpenSongs = new OpenFileDialog();
            tabControl1 = new TabControl();
            pgSongs = new TabPage();
            pgSongOrder = new TabPage();
            splitContainer1 = new SplitContainer();
            lbSongs = new ListBox();
            songBindingSource = new BindingSource(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            btnRemoveFromOrder = new Button();
            btnAddToOrder = new Button();
            lvSongOrder = new ListView();
            colOrderNum = new ColumnHeader();
            colSongName = new ColumnHeader();
            tabImages = new ImageList(components);
            tableLayoutPanel4 = new TableLayoutPanel();
            button1 = new Button();
            label2 = new Label();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            pgSongs.SuspendLayout();
            pgSongOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)songBindingSource).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Yellow;
            menuStrip1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { addSongsToolStripMenuItem, removeSongsToolStripMenuItem });
            menuStrip1.Location = new Point(3, 3);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(905, 29);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // addSongsToolStripMenuItem
            // 
            addSongsToolStripMenuItem.Name = "addSongsToolStripMenuItem";
            addSongsToolStripMenuItem.Size = new Size(112, 25);
            addSongsToolStripMenuItem.Text = "Add Song(s)";
            addSongsToolStripMenuItem.Click += addSongsToolStripMenuItem_Click;
            // 
            // removeSongsToolStripMenuItem
            // 
            removeSongsToolStripMenuItem.Name = "removeSongsToolStripMenuItem";
            removeSongsToolStripMenuItem.Size = new Size(209, 25);
            removeSongsToolStripMenuItem.Text = "Remove Selected Song(s)";
            removeSongsToolStripMenuItem.Click += removeSongsToolStripMenuItem_Click;
            // 
            // lvSongs
            // 
            lvSongs.Columns.AddRange(new ColumnHeader[] { colSongNames, colArtist, colSongLength, colSongFileSize });
            lvSongs.Dock = DockStyle.Fill;
            lvSongs.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lvSongs.FullRowSelect = true;
            lvSongs.GridLines = true;
            lvSongs.Location = new Point(3, 32);
            lvSongs.Name = "lvSongs";
            lvSongs.Size = new Size(905, 629);
            lvSongs.TabIndex = 1;
            lvSongs.UseCompatibleStateImageBehavior = false;
            lvSongs.View = View.Details;
            lvSongs.ColumnClick += lvSongs_ColumnClick;
            lvSongs.MouseDoubleClick += lvSongs_MouseDoubleClick;
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
            // fdlgOpenSongs
            // 
            fdlgOpenSongs.Filter = "Music Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma";
            fdlgOpenSongs.Multiselect = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(pgSongs);
            tabControl1.Controls.Add(pgSongOrder);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl1.ImageList = tabImages;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(919, 698);
            tabControl1.TabIndex = 3;
            // 
            // pgSongs
            // 
            pgSongs.BackColor = Color.White;
            pgSongs.Controls.Add(lvSongs);
            pgSongs.Controls.Add(menuStrip1);
            pgSongs.ImageIndex = 0;
            pgSongs.Location = new Point(4, 30);
            pgSongs.Name = "pgSongs";
            pgSongs.Padding = new Padding(3);
            pgSongs.Size = new Size(911, 664);
            pgSongs.TabIndex = 0;
            pgSongs.Text = "Song Listing";
            // 
            // pgSongOrder
            // 
            pgSongOrder.BackColor = Color.White;
            pgSongOrder.Controls.Add(splitContainer1);
            pgSongOrder.ImageIndex = 1;
            pgSongOrder.Location = new Point(4, 30);
            pgSongOrder.Name = "pgSongOrder";
            pgSongOrder.Padding = new Padding(3);
            pgSongOrder.Size = new Size(911, 664);
            pgSongOrder.TabIndex = 1;
            pgSongOrder.Text = "Song Order";
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
            splitContainer1.Size = new Size(905, 658);
            splitContainer1.SplitterDistance = 341;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 0;
            // 
            // lbSongs
            // 
            lbSongs.DataSource = songBindingSource;
            lbSongs.DisplayMember = "Name";
            lbSongs.Dock = DockStyle.Fill;
            lbSongs.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbSongs.FormattingEnabled = true;
            lbSongs.ItemHeight = 20;
            lbSongs.Location = new Point(0, 0);
            lbSongs.Name = "lbSongs";
            lbSongs.SelectionMode = SelectionMode.MultiExtended;
            lbSongs.Size = new Size(901, 303);
            lbSongs.TabIndex = 0;
            // 
            // songBindingSource
            // 
            songBindingSource.DataSource = typeof(models.Song);
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
            tableLayoutPanel1.Location = new Point(0, 303);
            tableLayoutPanel1.MaximumSize = new Size(0, 34);
            tableLayoutPanel1.MinimumSize = new Size(0, 34);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(901, 34);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnRemoveFromOrder
            // 
            btnRemoveFromOrder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnRemoveFromOrder.BackColor = Color.Yellow;
            btnRemoveFromOrder.BackgroundImage = Properties.Resources.upload_32x32;
            btnRemoveFromOrder.BackgroundImageLayout = ImageLayout.Zoom;
            btnRemoveFromOrder.FlatAppearance.BorderSize = 0;
            btnRemoveFromOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemoveFromOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemoveFromOrder.FlatStyle = FlatStyle.Flat;
            btnRemoveFromOrder.ForeColor = Color.Black;
            btnRemoveFromOrder.Location = new Point(453, 3);
            btnRemoveFromOrder.Name = "btnRemoveFromOrder";
            btnRemoveFromOrder.Size = new Size(445, 28);
            btnRemoveFromOrder.TabIndex = 0;
            btnRemoveFromOrder.UseVisualStyleBackColor = false;
            btnRemoveFromOrder.Click += btnRemoveFromOrder_Click;
            // 
            // btnAddToOrder
            // 
            btnAddToOrder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnAddToOrder.BackColor = Color.Yellow;
            btnAddToOrder.BackgroundImage = Properties.Resources.download_32x32;
            btnAddToOrder.BackgroundImageLayout = ImageLayout.Zoom;
            btnAddToOrder.FlatAppearance.BorderSize = 0;
            btnAddToOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddToOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddToOrder.FlatStyle = FlatStyle.Flat;
            btnAddToOrder.ForeColor = Color.Black;
            btnAddToOrder.Location = new Point(3, 3);
            btnAddToOrder.Name = "btnAddToOrder";
            btnAddToOrder.Size = new Size(444, 28);
            btnAddToOrder.TabIndex = 1;
            btnAddToOrder.UseVisualStyleBackColor = false;
            btnAddToOrder.Click += btnAddToOrder_Click;
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
            lvSongOrder.Size = new Size(901, 305);
            lvSongOrder.TabIndex = 0;
            lvSongOrder.UseCompatibleStateImageBehavior = false;
            lvSongOrder.View = View.Details;
            lvSongOrder.ItemDrag += lvSongOrder_ItemDrag;
            lvSongOrder.DragDrop += lvSongOrder_DragDrop;
            lvSongOrder.DragEnter += lvSongOrder_DragEnter;
            lvSongOrder.DragOver += lvSongOrder_DragOver;
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
            // tabImages
            // 
            tabImages.ColorDepth = ColorDepth.Depth32Bit;
            tabImages.ImageStream = (ImageListStreamer)resources.GetObject("tabImages.ImageStream");
            tabImages.TransparentColor = Color.Transparent;
            tabImages.Images.SetKeyName(0, "list.png");
            tabImages.Images.SetKeyName(1, "shuffle.png");
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
            // CustomMusicCtl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tabControl1);
            Name = "CustomMusicCtl";
            Size = new Size(919, 698);
            Load += CustomMusicCtl_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            pgSongs.ResumeLayout(false);
            pgSongs.PerformLayout();
            pgSongOrder.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)songBindingSource).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem addSongsToolStripMenuItem;
        private ToolStripMenuItem removeSongsToolStripMenuItem;
        private ListView lvSongs;
        private ColumnHeader colSongNames;
        private ColumnHeader colSongLength;
        private ColumnHeader colSongFileSize;
        private OpenFileDialog fdlgOpenSongs;
        private ColumnHeader colArtist;
        private TabControl tabControl1;
        private TabPage pgSongs;
        private TabPage pgSongOrder;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private ListBox lbSongs;
        private BindingSource songBindingSource;
        private Button btnRemoveFromOrder;
        private Button btnAddToOrder;
        private TableLayoutPanel tableLayoutPanel4;
        private Button button1;
        private Label label2;
        private ImageList tabImages;
        private ListView lvSongOrder;
        private ColumnHeader colOrderNum;
        private ColumnHeader colSongName;
    }
}

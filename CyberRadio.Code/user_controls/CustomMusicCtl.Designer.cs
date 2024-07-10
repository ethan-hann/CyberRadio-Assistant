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
            colSongNames = new ColumnHeader();
            colArtist = new ColumnHeader();
            colSongLength = new ColumnHeader();
            colSongFileSize = new ColumnHeader();
            fdlgOpenSongs = new OpenFileDialog();
            tabControl = new TabControl();
            tabSongs = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnAddSongs = new Button();
            btnRemoveSongs = new Button();
            tabSongOrder = new TabPage();
            splitContainer1 = new SplitContainer();
            lbSongs = new ListBox();
            songBindingSource = new BindingSource(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            btnRemoveFromOrder = new Button();
            btnAddToOrder = new Button();
            lvSongOrder = new ListView();
            colOrderNum = new ColumnHeader();
            colSongName = new ColumnHeader();
            tableLayoutPanel4 = new TableLayoutPanel();
            button1 = new Button();
            label2 = new Label();
            tabControl.SuspendLayout();
            tabSongs.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tabSongOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)songBindingSource).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // lvSongs
            // 
            lvSongs.Columns.AddRange(new ColumnHeader[] { colSongNames, colArtist, colSongLength, colSongFileSize });
            lvSongs.Dock = DockStyle.Fill;
            lvSongs.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lvSongs.FullRowSelect = true;
            lvSongs.GridLines = true;
            lvSongs.Location = new Point(3, 39);
            lvSongs.Name = "lvSongs";
            lvSongs.Size = new Size(905, 622);
            lvSongs.TabIndex = 1;
            lvSongs.UseCompatibleStateImageBehavior = false;
            lvSongs.View = View.Details;
            lvSongs.ColumnClick += LvSongs_ColumnClick;
            lvSongs.MouseDoubleClick += LvSongs_MouseDoubleClick;
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
            // tabControl
            // 
            tabControl.Controls.Add(tabSongs);
            tabControl.Controls.Add(tabSongOrder);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(919, 698);
            tabControl.TabIndex = 3;
            // 
            // tabSongs
            // 
            tabSongs.BackColor = Color.White;
            tabSongs.Controls.Add(lvSongs);
            tabSongs.Controls.Add(tableLayoutPanel2);
            tabSongs.ImageIndex = 0;
            tabSongs.Location = new Point(4, 30);
            tabSongs.Name = "tabSongs";
            tabSongs.Padding = new Padding(3);
            tabSongs.Size = new Size(911, 664);
            tabSongs.TabIndex = 0;
            tabSongs.Text = "Song Listing";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnAddSongs, 0, 0);
            tableLayoutPanel2.Controls.Add(btnRemoveSongs, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(905, 36);
            tableLayoutPanel2.TabIndex = 2;
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
            btnAddSongs.Size = new Size(446, 32);
            btnAddSongs.TabIndex = 1;
            btnAddSongs.Text = "Add Song(s)";
            btnAddSongs.TextAlign = ContentAlignment.MiddleRight;
            btnAddSongs.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddSongs.UseVisualStyleBackColor = false;
            btnAddSongs.Click += BtnAddSongs_Click;
            // 
            // btnRemoveSongs
            // 
            btnRemoveSongs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnRemoveSongs.BackColor = Color.Yellow;
            btnRemoveSongs.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnRemoveSongs.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnRemoveSongs.FlatStyle = FlatStyle.Flat;
            btnRemoveSongs.Image = Properties.Resources.delete__16x16;
            btnRemoveSongs.Location = new Point(455, 2);
            btnRemoveSongs.Margin = new Padding(3, 2, 3, 2);
            btnRemoveSongs.Name = "btnRemoveSongs";
            btnRemoveSongs.Size = new Size(447, 32);
            btnRemoveSongs.TabIndex = 2;
            btnRemoveSongs.Text = "Remove Selected Song(s)";
            btnRemoveSongs.TextAlign = ContentAlignment.MiddleRight;
            btnRemoveSongs.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemoveSongs.UseVisualStyleBackColor = false;
            btnRemoveSongs.Click += BtnRemoveSongs_Click;
            // 
            // tabSongOrder
            // 
            tabSongOrder.BackColor = Color.White;
            tabSongOrder.Controls.Add(splitContainer1);
            tabSongOrder.ImageIndex = 1;
            tabSongOrder.Location = new Point(4, 30);
            tabSongOrder.Name = "tabSongOrder";
            tabSongOrder.Padding = new Padding(3);
            tabSongOrder.Size = new Size(911, 664);
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
            splitContainer1.Size = new Size(905, 658);
            splitContainer1.SplitterDistance = 341;
            splitContainer1.SplitterWidth = 8;
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
            btnRemoveFromOrder.BackgroundImage = Properties.Resources.up__32x32;
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
            btnAddToOrder.Size = new Size(444, 28);
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
            lvSongOrder.Size = new Size(901, 305);
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
            Controls.Add(tabControl);
            Name = "CustomMusicCtl";
            Size = new Size(919, 698);
            Load += CustomMusicCtl_Load;
            tabControl.ResumeLayout(false);
            tabSongs.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tabSongOrder.ResumeLayout(false);
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
    }
}

namespace RadioExt_Helper.user_controls
{
    partial class CustomMusicCtl
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
            menuStrip1 = new MenuStrip();
            addSongsToolStripMenuItem = new ToolStripMenuItem();
            removeSongsToolStripMenuItem = new ToolStripMenuItem();
            lvSongs = new ListView();
            colSongNames = new ColumnHeader();
            colArtist = new ColumnHeader();
            colSongLength = new ColumnHeader();
            colSongFileSize = new ColumnHeader();
            fdlgOpenSongs = new OpenFileDialog();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnSongOrder = new Button();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Yellow;
            menuStrip1.Font = new Font("CF Notche Demo", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { addSongsToolStripMenuItem, removeSongsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(919, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // addSongsToolStripMenuItem
            // 
            addSongsToolStripMenuItem.Name = "addSongsToolStripMenuItem";
            addSongsToolStripMenuItem.Size = new Size(108, 20);
            addSongsToolStripMenuItem.Text = "Add Song(s)";
            addSongsToolStripMenuItem.Click += addSongsToolStripMenuItem_Click;
            // 
            // removeSongsToolStripMenuItem
            // 
            removeSongsToolStripMenuItem.Name = "removeSongsToolStripMenuItem";
            removeSongsToolStripMenuItem.Size = new Size(209, 20);
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
            lvSongs.Location = new Point(0, 24);
            lvSongs.Name = "lvSongs";
            lvSongs.Size = new Size(919, 625);
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 77.6931458F));
            tableLayoutPanel1.Controls.Add(btnSongOrder, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 649);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(919, 49);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // btnSongOrder
            // 
            btnSongOrder.Dock = DockStyle.Fill;
            btnSongOrder.Font = new Font("CF Notche Demo", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSongOrder.Location = new Point(8, 8);
            btnSongOrder.Margin = new Padding(8);
            btnSongOrder.Name = "btnSongOrder";
            btnSongOrder.Size = new Size(903, 33);
            btnSongOrder.TabIndex = 0;
            btnSongOrder.Text = "Edit Song Order";
            btnSongOrder.UseVisualStyleBackColor = true;
            // 
            // CustomMusicCtl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lvSongs);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            Name = "CustomMusicCtl";
            Size = new Size(919, 698);
            Load += CustomMusicCtl_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnSongOrder;
        private ColumnHeader colArtist;
    }
}

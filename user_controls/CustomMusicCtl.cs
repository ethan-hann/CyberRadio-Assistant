using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RadioExt_Helper.user_controls
{
    public partial class CustomMusicCtl : UserControl
    {
        public EventHandler? SongListUpdated;

        private SongList _songList = [];

        public CustomMusicCtl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }

        private void CustomMusicCtl_Load(object sender, EventArgs e)
        {
            Translate();
            PopulateListView();
        }

        public void Translate()
        {
            addSongsToolStripMenuItem.Text = GlobalData.Strings.GetString("AddSongsToolStrip");
            removeSongsToolStripMenuItem.Text = GlobalData.Strings.GetString("RemoveSongsToolStrip");

            lvSongs.Columns[0].Text = GlobalData.Strings.GetString("SongNameHeader");
            lvSongs.Columns[1].Text = GlobalData.Strings.GetString("SongArtistHeader");
            lvSongs.Columns[2].Text = GlobalData.Strings.GetString("SongLengthHeader");
            lvSongs.Columns[3].Text = GlobalData.Strings.GetString("SongFileSizeHeader");

            fdlgOpenSongs.Title = GlobalData.Strings.GetString("Open");
            btnSongOrder.Text = GlobalData.Strings.GetString("EditSongOrder");
        }

        public void SetSongList(SongList songList)
        {
            _songList = songList;
            PopulateListView();
        }

        private void PopulateListView()
        {
            lvSongs.SuspendLayout();
            lvSongs.Items.Clear();

            foreach (Song song in _songList)
            {
                //TimeSpan length = TimeSpan.FromSeconds(song.Duration);
                //string timeString = string.Format("{0}:{1:02}:{2:02}",
                //    (int)length.TotalHours, length.Minutes, length.Seconds);

                ListViewItem lvItem = new(new string[]
                {
                    song.Name,
                    song.Artist,
                    string.Format("{0:hh\\:mm\\:ss}", song.Duration),
                    song.Size.FormatSize()
                })
                { Tag = song };

                lvSongs.Items.Add(lvItem);
            }

            lvSongs.ResizeColumns();
            lvSongs.ResumeLayout();
        }

        private void addSongsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fdlgOpenSongs.ShowDialog() != DialogResult.OK) return;

            foreach (string path in fdlgOpenSongs.FileNames)
            {
                Song song = new();
                var file = TagLib.File.Create(path);
                song.OriginalFilePath = path;
                song.Name = file.Tag.Title ?? file.Name;
                song.Artist = file.Tag.FirstPerformer ?? string.Empty;
                song.Size = (ulong)new FileInfo(path).Length;
                song.Duration = file.Properties.Duration;

                _songList.Add(song);
            }

            PopulateListView();
        }

        private void removeSongsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvSongs.SelectedItems.Count <= 0) return;

            if (lvSongs.SelectedItems.Count == lvSongs.Items.Count)
            {
                if (MessageBox.Show(this, GlobalData.Strings.GetString("DeleteAllSongsConfirm"), 
                    GlobalData.Strings.GetString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            }

            for (int i = 0; i < lvSongs.SelectedItems.Count; i++)
            {
                if (lvSongs.SelectedItems[i].Tag is Song song)
                    _songList.Remove(song);
            }
            PopulateListView();
        }

        private void lvSongs_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if the clicked column is already the column that is being sorted.
            if (lvSongs.ListViewItemSorter is ListViewItemComparer sorter && sorter.Column == e.Column)
            {
                // Reverse the current sort direction for this column.
                sorter.Order = sorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvSongs.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
            }

            // Perform the sort with these new sort options.
            lvSongs.Sort();
        }

        private void lvSongs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvSongs.SelectedItems.Count != 1) return;

            if (lvSongs.SelectedItems[0].Tag is Song song)
                if (Directory.GetParent(song.OriginalFilePath) is DirectoryInfo parentDir)
                    Process.Start("explorer.exe", parentDir.FullName);
        }
    }
}

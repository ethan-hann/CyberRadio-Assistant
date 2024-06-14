using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            lvSongs.Columns[1].Text = GlobalData.Strings.GetString("SongLengthHeader");
            lvSongs.Columns[2].Text = GlobalData.Strings.GetString("SongFileSizeHeader");

            fdlgOpenSongs.Title = GlobalData.Strings.GetString("Open");
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
                TimeSpan length = TimeSpan.FromSeconds(song.Length);
                string timeString = string.Format("{0}:{1:02}:{2:02}",
                    (int)length.TotalHours, length.Minutes, length.Seconds);

                ListViewItem lvItem = new(new string[]
                {
                    song.Name,
                    timeString,
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

        }

        private void removeSongsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

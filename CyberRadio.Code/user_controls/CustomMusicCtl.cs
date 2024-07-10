// CustomMusicCtl.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.ComponentModel;
using System.Diagnostics;
using AetherUtils.Core.Extensions;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using ListView = System.Windows.Forms.ListView;

namespace RadioExt_Helper.user_controls;

/// <summary>
/// Represents a custom music control with support for adding/removing songs and setting the song order.
/// </summary>
public sealed partial class CustomMusicCtl : UserControl, IUserControl
{
    /// <summary>
    /// Image list for the tab images.
    /// </summary>
    private readonly ImageList _tabImages = new();

    /// <summary>
    /// Binding list of songs for data binding.
    /// </summary>
    private BindingList<Song> _bindingSongList = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomMusicCtl"/> class.
    /// </summary>
    /// <param name="station">The trackable station object.</param>
    public CustomMusicCtl(TrackableObject<Station> station)
    {
        InitializeComponent();
        Dock = DockStyle.Fill;

        SetTabImages();

        Station = station;
        SetSongList(Station.TrackedObject.SongsAsList);
    }

    /// <summary>
    /// Gets the trackable station object associated with the control.
    /// </summary>
    public TrackableObject<Station> Station { get; }

    /// <summary>
    /// Translates the control's text to the appropriate language.
    /// </summary>
    public void Translate()
    {
        btnAddSongs.Text = GlobalData.Strings.GetString("AddSongsToolStrip");
        btnRemoveSongs.Text = GlobalData.Strings.GetString("RemoveSongsToolStrip");

        lvSongs.Columns[0].Text = GlobalData.Strings.GetString("SongNameHeader");
        lvSongs.Columns[1].Text = GlobalData.Strings.GetString("SongArtistHeader");
        lvSongs.Columns[2].Text = GlobalData.Strings.GetString("SongLengthHeader");
        lvSongs.Columns[3].Text = GlobalData.Strings.GetString("SongFileSizeHeader");

        fdlgOpenSongs.Title = GlobalData.Strings.GetString("AddSongs");
        tabSongs.Text = GlobalData.Strings.GetString("SongListing");
        tabSongOrder.Text = GlobalData.Strings.GetString("SongOrder");

        lvSongOrder.Columns[0].Text = GlobalData.Strings.GetString("Order");
        lvSongOrder.Columns[1].Text = GlobalData.Strings.GetString("SongNameHeader");
    }

    /// <summary>
    /// Event that is triggered when the station is updated.
    /// </summary>
    public event EventHandler? StationUpdated;

    private void CustomMusicCtl_Load(object sender, EventArgs e)
    {
        Translate();
        UpdateListsAndViews();
        SetOrderedList();
    }

    /// <summary>
    /// Sets the images for the tabs.
    /// </summary>
    private void SetTabImages()
    {
        _tabImages.Images.Add("listing", Resources.music_list);
        _tabImages.Images.Add("order", Resources.order);
        tabControl.ImageList = _tabImages;
        tabSongs.ImageKey = @"listing";
        tabSongOrder.ImageKey = @"order";
    }

    /// <summary>
    /// Sets the song list and updates the binding list.
    /// </summary>
    /// <param name="songList">The list of songs.</param>
    private void SetSongList(List<Song> songList)
    {
        Station.TrackedObject.SongsAsList = songList;
        _bindingSongList = new BindingList<Song>(songList);

        PopulateListView();
    }

    /// <summary>
    /// Populates the song list view with the songs from the station.
    /// </summary>
    private void PopulateListView()
    {
        lvSongs.SuspendLayout();
        lvSongs.Items.Clear();

        foreach (var lvItem in Station.TrackedObject.SongsAsList
                     .Select(song => new ListViewItem(new[]
                     {
                         song.Title,
                         song.Artist,
                         $"{song.Duration:hh\\:mm\\:ss}",
                         song.FileSize.FormatSize()
                     })
                     {
                         Tag = song
                     }))
        {
            lvSongs.Items.Add(lvItem);
        }

        lvSongs.ResizeColumns();
        lvSongs.ResumeLayout();
    }

    /// <summary>
    /// Determines whether a song can be added to the station. Checks if the song is already in the station.
    /// </summary>
    /// <param name="song">The song to check.</param>
    /// <returns>True if the song can be added; otherwise, false.</returns>
    private bool CanSongBeAdded(Song song)
    {
        return !Station.TrackedObject.SongsAsList.Any(s => s.Equals(song));
    }

    private void BtnAddSongs_Click(object sender, EventArgs e)
    {
        if (fdlgOpenSongs.ShowDialog() != DialogResult.OK) return;

        foreach (var path in fdlgOpenSongs.FileNames)
        {
            var song = Song.FromFile(path);
            if (song == null) continue;

            if (!CanSongBeAdded(song)) continue;
            _bindingSongList.Add(song);
        }

        UpdateListsAndViews();
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void BtnRemoveSongs_Click(object sender, EventArgs e)
    {
        if (lvSongs.SelectedItems.Count <= 0) return;

        if (lvSongs.SelectedItems.Count == lvSongs.Items.Count)
        {
            if (MessageBox.Show(this, GlobalData.Strings.GetString("DeleteAllSongsConfirm"),
                    GlobalData.Strings.GetString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.No)
            {
                return;
            }
        }

        for (var i = 0; i < lvSongs.SelectedItems.Count; i++)
        {
            if (lvSongs.SelectedItems[i].Tag is not Song song) continue;

            _bindingSongList.Remove(song);
            var fileName = Path.GetFileName(song.FilePath);
            Station.TrackedObject.MetaData.SongOrder.Remove(fileName);
        }

        UpdateListsAndViews();
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Updates the song list view, the order list view, and synchronizes the song order.
    /// </summary>
    private void UpdateListsAndViews()
    {
        PopulateListView();
        PopulateSongListBox();
        SynchronizeSongOrder();
    }

    /// <summary>
    /// Synchronizes the song order list view with the station's song list.
    /// </summary>
    private void SynchronizeSongOrder()
    {
        lvSongOrder.BeginUpdate();
        for (var i = lvSongOrder.Items.Count - 1; i >= 0; i--)
        {
            var item = lvSongOrder.Items[i];
            if (item.Tag is not Song song) continue;

            if (!Station.TrackedObject.SongsAsList.Contains(song))
                lvSongOrder.Items.Remove(item);
        }

        UpdateOrderColumn();
        lvSongOrder.EndUpdate();
    }

    private void LvSongs_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        if (lvSongs.ListViewItemSorter is ListViewItemComparer sorter && sorter.Column == e.Column)
        {
            sorter.Order = sorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
        }
        else
        {
            lvSongs.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
        }

        lvSongs.Sort();
    }

    private void LvSongs_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if (lvSongs.SelectedItems.Count != 1) return;

        if (lvSongs.SelectedItems[0].Tag is not Song song) return;

        if (Directory.GetParent(song.FilePath) is { } parentDir)
        {
            Process.Start("explorer.exe", parentDir.FullName);
        }
    }

    #region Song Order

    /// <summary>
    /// Populates the song order list box with the songs from the binding list.
    /// </summary>
    private void PopulateSongListBox()
    {
        lbSongs.BeginUpdate();
        lbSongs.DataSource = null;

        lbSongs.DataSource = _bindingSongList;
        lbSongs.EndUpdate();
    }

    private void BtnAddToOrder_Click(object sender, EventArgs e)
    {
        if (lbSongs.SelectedItems.Count <= 0) return;

        var selectedSongs = lbSongs.SelectedItems.Cast<Song>().ToList();
        foreach (var item in selectedSongs.OfType<Song>())
        {
            AddSongToOrderListView(item);
            _bindingSongList.Remove(item);

            UpdateListsAndViews();
        }

        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void BtnRemoveFromOrder_Click(object sender, EventArgs e)
    {
        if (lvSongOrder.SelectedItems.Count <= 0) return;

        foreach (var item in lvSongOrder.SelectedItems)
        {
            if (item is not ListViewItem lvItem) continue;
            if (lvItem.Tag is not Song song) continue;

            lvSongOrder.Items.Remove(lvItem);
            _bindingSongList.Add(song);
        }

        UpdateListsAndViews();

        if (lvSongOrder.Items.Count <= 0) return;

        lvSongOrder.Items[0].Selected = true;
        lvSongOrder.EnsureVisible(0);

        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void LvSongOrder_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data != null && !e.Data.GetDataPresent(typeof(Song))) return;

        e.Effect = DragDropEffects.Move;
    }

    private void LvSongOrder_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data == null || !e.Data.GetDataPresent(typeof(ListViewItem))) return;

        var cp = lvSongOrder.PointToClient(new Point(e.X, e.Y));
        var hoverItem = lvSongOrder.GetItemAt(cp.X, cp.Y);
        var dragItem = e.Data.GetData(typeof(ListViewItem));

        if (hoverItem == null || dragItem == hoverItem || dragItem?.GetType() != typeof(ListViewItem)) return;

        var item = (ListViewItem)dragItem;
        var hoverIndex = hoverItem.Index;
        lvSongOrder.Items.Remove(item);
        lvSongOrder.Items.Insert(hoverIndex, item);
        UpdateOrderColumn();
        UpdateOrderedList();
    }

    private void LvSongOrder_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Move;

        // Auto-scroll
        if (sender is not ListView lv) return;

        var pt = lv.PointToClient(new Point(e.X, e.Y));
        var hoverItem = lv.GetItemAt(pt.X, pt.Y);
        hoverItem?.EnsureVisible();
    }

    private void LvSongOrder_ItemDrag(object sender, ItemDragEventArgs e)
    {
        if (e.Item != null)
            lvSongOrder.DoDragDrop(e.Item, DragDropEffects.Move);
    }

    /// <summary>
    /// Adds a song to the order list view.
    /// </summary>
    /// <param name="song">The song to add.</param>
    /// <param name="updateOrderedList">Indicates whether to update the ordered list.</param>
    private void AddSongToOrderListView(Song song, bool updateOrderedList = true)
    {
        var item = new ListViewItem([(lvSongOrder.Items.Count + 1).ToString(), song.Title])
        {
            Name = song.Title,
            Tag = song
        };

        lvSongOrder.Items.Add(item);
        UpdateOrderColumn();

        if (updateOrderedList)
        {
            UpdateOrderedList();
        }

        lvSongOrder.ResizeColumns();
    }

    /// <summary>
    /// Updates the order column numbers in the song order list view.
    /// </summary>
    private void UpdateOrderColumn()
    {
        for (var i = 0; i < lvSongOrder.Items.Count; i++)
        {
            lvSongOrder.Items[i].SubItems[0].Text = (i + 1).ToString();
        }
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Sets the ordered list of songs based on the station's metadata.
    /// </summary>
    private void SetOrderedList()
    {
        var tempSongs = lbSongs.Items.Cast<Song>().ToList();

        foreach (var song in Station.TrackedObject.MetaData.SongOrder
                     .Select(item => tempSongs.Find(x => x.FilePath.EndsWith(item)))
                     .OfType<Song>())
        {
            AddSongToOrderListView(song, false);
            _bindingSongList.Remove(song);
        }

        UpdateListsAndViews();
    }

    /// <summary>
    /// Updates the ordered list of songs in the station with the items in the list view.
    /// </summary>
    private void UpdateOrderedList()
    {
        Station.TrackedObject.MetaData.SongOrder.Clear();
        foreach (ListViewItem item in lvSongOrder.Items)
        {
            if (item.Tag is Song song)
            {
                Station.TrackedObject.MetaData.SongOrder.Add(Path.GetFileName(song.FilePath));
            }
        }
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}
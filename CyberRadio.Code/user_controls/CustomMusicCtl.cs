using System.ComponentModel;
using System.Diagnostics;
using AetherUtils.Core.Extensions;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using ListView = System.Windows.Forms.ListView;

namespace RadioExt_Helper.user_controls;

public sealed partial class CustomMusicCtl : UserControl, IUserControl
{
    private BindingList<Song> _bindingSongList = [];

    public CustomMusicCtl(Station station)
    {
        InitializeComponent();

        Dock = DockStyle.Fill;
        Station = station;
        SetSongList(Station.SongsAsList);
    }

    public Station Station { get; }

    public void Translate()
    {
        addSongsToolStripMenuItem.Text = GlobalData.Strings.GetString("AddSongsToolStrip");
        removeSongsToolStripMenuItem.Text = GlobalData.Strings.GetString("RemoveSongsToolStrip");

        lvSongs.Columns[0].Text = GlobalData.Strings.GetString("SongNameHeader");
        lvSongs.Columns[1].Text = GlobalData.Strings.GetString("SongArtistHeader");
        lvSongs.Columns[2].Text = GlobalData.Strings.GetString("SongLengthHeader");
        lvSongs.Columns[3].Text = GlobalData.Strings.GetString("SongFileSizeHeader");

        fdlgOpenSongs.Title = GlobalData.Strings.GetString("Open");
        pgSongs.Text = GlobalData.Strings.GetString("SongListing");
        pgSongOrder.Text = GlobalData.Strings.GetString("SongOrder");

        lvSongOrder.Columns[0].Text = GlobalData.Strings.GetString("Order");
        lvSongOrder.Columns[1].Text = GlobalData.Strings.GetString("SongNameHeader");
    }

    private void CustomMusicCtl_Load(object sender, EventArgs e)
    {
        Translate();
        UpdateListsAndViews();
        SetOrderedList();
    }

    private void SetSongList(List<Song> songList)
    {
        Station.SongsAsList = songList;
        _bindingSongList = [.. songList];

        PopulateListView();
    }

    private void PopulateListView()
    {
        lvSongs.SuspendLayout();
        lvSongs.Items.Clear();

        foreach (var lvItem in Station.SongsAsList
                     .Select(song => new ListViewItem([
                         song.Name,
                         song.Artist,
                         $"{song.Duration:hh\\:mm\\:ss}",
                         song.Size.FormatSize()
                     ])
                     { Tag = song }))
        {
            lvSongs.Items.Add(lvItem);
        }

        lvSongs.ResizeColumns();
        lvSongs.ResumeLayout();
    }

    private bool CanSongBeAdded(Song song)
    {
        return !Station.SongsAsList.Any(s => s.Name.Equals(song.Name) && s.Artist.Equals(song.Artist)
                                                                      && s.OriginalFilePath.Equals(
                                                                          song.OriginalFilePath));
    }

    private void addSongsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (fdlgOpenSongs.ShowDialog() != DialogResult.OK) return;

        foreach (var path in fdlgOpenSongs.FileNames)
        {
            var song = Song.ParseFromFile(path);
            if (song == null) continue;
            
            if (!CanSongBeAdded(song)) continue;
            
            Station.SongsAsList.Add(song);
            _bindingSongList.Add(song);
        }

        UpdateListsAndViews();
    }

    private void removeSongsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lvSongs.SelectedItems.Count <= 0) return;

        if (lvSongs.SelectedItems.Count == lvSongs.Items.Count)
            if (MessageBox.Show(this, GlobalData.Strings.GetString("DeleteAllSongsConfirm"),
                    GlobalData.Strings.GetString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.No)
                return;

        for (var i = 0; i < lvSongs.SelectedItems.Count; i++)
            if (lvSongs.SelectedItems[i].Tag is Song song)
            {
                Station.SongsAsList.Remove(song);
                _bindingSongList.Remove(song);
                var fileName = Path.GetFileName(song.OriginalFilePath);
                Station.MetaData.SongOrder.Remove(fileName);
            }

        UpdateListsAndViews();
    }

    private void UpdateListsAndViews()
    {
        PopulateListView();
        PopulateSongListBox();
        SynchronizeSongOrder();
    }

    private void SynchronizeSongOrder()
    {
        lvSongOrder.BeginUpdate();
        for (var i = lvSongOrder.Items.Count - 1; i >= 0; i--)
        {
            var item = lvSongOrder.Items[i];
            if (item.Tag is not Song song) continue;
            
            if (!Station.SongsAsList.Contains(song))
                lvSongOrder.Items.Remove(item);
        }

        UpdateOrderColumn();
        lvSongOrder.EndUpdate();
    }

    private void lvSongs_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        // Determine if the clicked column is already the column that is being sorted.
        if (lvSongs.ListViewItemSorter is ListViewItemComparer sorter && sorter.Column == e.Column)
            // Reverse the current sort direction for this column.
            sorter.Order = sorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
        else
            // Set the column number that is to be sorted; default to ascending.
            lvSongs.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);

        // Perform the sort with these new sort options.
        lvSongs.Sort();
    }

    private void lvSongs_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if (lvSongs.SelectedItems.Count != 1) return;

        if (lvSongs.SelectedItems[0].Tag is not Song song) return;
        
        if (Directory.GetParent(song.OriginalFilePath) is { } parentDir)
            Process.Start("explorer.exe", parentDir.FullName);
    }

    #region Song Order

    private void PopulateSongListBox()
    {
        lbSongs.BeginUpdate();
        lbSongs.DataSource = null;

        lbSongs.DataSource = _bindingSongList;
        lbSongs.EndUpdate();
    }

    private void btnAddToOrder_Click(object sender, EventArgs e)
    {
        if (lbSongs.SelectedItems.Count <= 0) return;

        var selectedSongs = lbSongs.SelectedItems.Cast<Song>().ToList();
        foreach (var item in selectedSongs.OfType<Song>())
        {
            AddSongToOrderListView(item);
            _bindingSongList.Remove(item);

            UpdateListsAndViews();
        }
    }

    private void btnRemoveFromOrder_Click(object sender, EventArgs e)
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
    }

    private void lvSongOrder_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data != null && !e.Data.GetDataPresent(typeof(Song))) return;

        e.Effect = DragDropEffects.Move;
    }

    private void lvSongOrder_DragDrop(object sender, DragEventArgs e)
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

    private void lvSongOrder_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Move;

        // Auto-scroll
        if (sender is not ListView lv) return;
        
        var pt = lv.PointToClient(new Point(e.X, e.Y));
        var hoverItem = lv.GetItemAt(pt.X, pt.Y);
        hoverItem?.EnsureVisible();
    }

    private void lvSongOrder_ItemDrag(object sender, ItemDragEventArgs e)
    {
        if (e.Item != null) 
            lvSongOrder.DoDragDrop(e.Item, DragDropEffects.Move);
    }

    private void AddSongToOrderListView(Song song, bool updateOrderedList = true)
    {
        ListViewItem item = new([(lvSongOrder.Items.Count + 1).ToString(), song.Name])
        {
            Name = song.Name,
            Tag = song
        };

        lvSongOrder.Items.Add(item);
        UpdateOrderColumn();

        if (updateOrderedList)
            UpdateOrderedList();

        lvSongOrder.ResizeColumns();
    }

    private void UpdateOrderColumn()
    {
        for (var i = 0; i < lvSongOrder.Items.Count; i++)
            lvSongOrder.Items[i].SubItems[0].Text = (i + 1).ToString();
    }

    private void SetOrderedList()
    {
        var tempSongs = lbSongs.Items.Cast<Song>().ToList();

        foreach (var song in Station.MetaData.SongOrder
                     .Select(item => tempSongs.Find(x => x.OriginalFilePath.EndsWith(item)))
                     .OfType<Song>())
        {
            AddSongToOrderListView(song, false);
            _bindingSongList.Remove(song);
        }

        UpdateListsAndViews();
    }

    private void UpdateOrderedList()
    {
        Station.MetaData.SongOrder.Clear();
        foreach (ListViewItem item in lvSongOrder.Items)
            if (item.Tag is Song song)
                Station.MetaData.SongOrder.Add(Path.GetFileName(song.OriginalFilePath));
    }

    #endregion
}
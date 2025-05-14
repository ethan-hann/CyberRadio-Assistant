// CustomMusicCtl.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
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

using System.Diagnostics;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using RadioExt_Helper.forms;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using ListView = System.Windows.Forms.ListView;

namespace RadioExt_Helper.user_controls;

public sealed partial class CustomMusicCtl : UserControl, IUserControl
{
    private readonly ImageList _songListViewImages = new();

    /// <summary>
    /// Image list for the tab images.
    /// </summary>
    private readonly ImageList _tabImages = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomMusicCtl"/> class.
    /// </summary>
    /// <param name="station">The trackable station object.</param>
    public CustomMusicCtl(TrackableObject<Station> station)
    {
        Station = station;

        InitializeComponent();
        Dock = DockStyle.Fill;

        SetTabImages();
        SetSongListImages();
        PopulateListView();
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
        btnAddSongs.Text = Strings.AddSongsToolStrip;
        btnRemoveSongs.Text = Strings.RemoveSongsToolStrip;
        btnRemoveAllSongs.Text = Strings.ClearAllSongs;

        lvSongs.Columns[0].Text = Strings.SongExistsHeader;
        lvSongs.Columns[1].Text = Strings.SongNameHeader;
        lvSongs.Columns[2].Text = Strings.SongArtistHeader;
        lvSongs.Columns[3].Text = Strings.SongLengthHeader;
        lvSongs.Columns[4].Text = Strings.SongFileSizeHeader;
        lvSongs.Columns[5].Text = Strings.SongFilePathHeader;

        lblTotalSongsLabel.Text = Strings.TotalSongsLabel;
        lblStationSizeLabel.Text = Strings.TotalStationSizeLabel;

        fdlgOpenSongs.Title = Strings.AddSongsFileBrowserTitle;
        fdlgOpenSongs.Filter = @"Audio/Video Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma;*.aac;*.m4a;*.aiff;*.alac;*.opus;*.amr;*.ac3;*.mp4;*.m4v;*.mov;*.avi;*.wmv;*.flv;*.mkv;*.webm;*.mpeg;*.mpg;*.3gp;*.3g2;*.ts;*.mts;*.m2ts";
        tabSongs.Text = Strings.SongListing;
        tabSongOrder.Text = Strings.SongOrder;

        lvSongOrder.Columns[0].Text = Strings.Order;
        lvSongOrder.Columns[1].Text = Strings.SongNameHeader;

        locateToolStripMenuItem.Text = Strings.LocateSong;
        locateAllMissingSongsToolStripMenuItem.Text = Strings.LocateAllMissingSongs;
    }

    /// <summary>
    /// Event that is triggered when the station is updated.
    /// </summary>
    public event EventHandler? StationUpdated;

    public event EventHandler<string>? StatusChanged;

    public event EventHandler? StatusReset;

    private void CustomMusicCtl_Load(object sender, EventArgs e)
    {
        Translate();

        //Enable owner drawing
        lvSongs.OwnerDraw = true;
        lvSongs.DrawColumnHeader += (_, args) => args.DrawDefault = true;
        lvSongs.DrawSubItem += LvSongs_DrawSubItem;

        UpdateListsAndViews();
        SetOrderedList();
    }

    /// <summary>
    /// Reset the UI values to the defaults for the station.
    /// </summary>
    public void ResetUi()
    {
        UpdateListsAndViews();
        SetOrderedList();
    }

    private void LvSongs_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
    {
        if (e.ColumnIndex == 0) // Assuming the icon is in the first column
        {
            if (e.Item == null || lvSongs.SmallImageList == null ||
                e.Item.Tag is not Song song) return;

            var imageKey = FileHelper.DoesFileExist(song.FilePath, false) ? "enabled" : "disabled";
            var image = lvSongs.SmallImageList.Images[imageKey];
            if (image == null) return;

            // Calculate the position to center the image in the cell
            var iconX = e.Bounds.Left + (e.Bounds.Width - image.Width) / 2;
            var iconY = e.Bounds.Top + (e.Bounds.Height - image.Height) / 2;
            e.Graphics.DrawImage(image, iconX, iconY);
        }
        else
        {
            e.DrawDefault = true;
        }
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

    private void SetSongListImages()
    {
        _songListViewImages.Images.Add("enabled", Resources.enabled__16x16);
        _songListViewImages.Images.Add("disabled", Resources.disabled__16x16);
        lvSongs.SmallImageList = _songListViewImages;
    }

    /// <summary>
    /// Populates the song list view with the songs from the station.
    /// </summary>
    private void PopulateListView()
    {
        lvSongs.SuspendLayout();
        lvSongs.Items.Clear();

        foreach (var lvItem in Station.TrackedObject.Songs
                     .Select(song => new ListViewItem([
                             string.Empty, // This is required for the icon to show up in the first column
                             song.Title,
                             song.Artist,
                             $"{song.Duration:hh\\:mm\\:ss}",
                             song.FileSize.FormatSize(),
                             song.FilePath
                         ])
                         { Tag = song }))
            lvSongs.Items.Add(lvItem);

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
        return !Station.TrackedObject.Songs.Any(s => s.Equals(song));
    }

    private void BtnAddSongs_Click(object sender, EventArgs e)
    {
        if (fdlgOpenSongs.ShowDialog() != DialogResult.OK) return;

        var config = GlobalData.ConfigManager.GetConfig();
        if (config == null) return;

        // Check if the selected files are valid audio files
        List<string> needConversion = [];
        List<string> noConversionNeeded = [];
        List<string> alreadyInStation = [];
        needConversion.AddRange(fdlgOpenSongs.FileNames.Where(AudioConverter.NeedsConversion));
        noConversionNeeded.AddRange(fdlgOpenSongs.FileNames.Where(f => !needConversion.Contains(f)));
        
        //Check if the proposed MP3 (that would be the final converted file) is already in the station
        alreadyInStation.AddRange(from file in needConversion 
            let proposedMp3 = $"{Path.GetFileNameWithoutExtension(file)}.mp3"
            where Station.TrackedObject.Songs.Any(s => s.FilePath.Contains(proposedMp3)) 
            select file);

        // Remove files that are already in the station from needConversion
        needConversion.RemoveAll(f => alreadyInStation.Contains(f));
        
        // Add songs that don't require conversion first
        foreach (var song in noConversionNeeded.Select(Song.FromFile).OfType<Song>().Where(CanSongBeAdded))
            Station.TrackedObject.Songs.Add(song);

        if (needConversion.Count > 0)
        {
            //Ask the user if they want to convert the files
            var result = MessageBox.Show(this, 
                string.Format(Strings.AudioConverterPrompt, needConversion.Count), Strings.Confirm,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Show the audio converter form
                var audioConverterForm = new AudioConverterForm(needConversion, Station);
                audioConverterForm.ConversionCompleted += AudioConverterForm_ConversionCompleted;
                audioConverterForm.ShowDialog(this);
            }
        }

        UpdateListsAndViews();
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void AudioConverterForm_ConversionCompleted(object? sender, List<string> e)
    {
        foreach (var song in e.Select(Song.FromFile).OfType<Song>().Where(CanSongBeAdded))
            Station.TrackedObject.Songs.Add(song);

        UpdateListsAndViews();
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void BtnRemoveSongs_Click(object sender, EventArgs e)
    {
        if (lvSongs.SelectedItems.Count <= 0) return;

        if (lvSongs.SelectedItems.Count == lvSongs.Items.Count)
            if (ConfirmRemoveAllSongs() == DialogResult.No)
                return;

        for (var i = 0; i < lvSongs.SelectedItems.Count; i++)
            if (lvSongs.SelectedItems[i].Tag is Song song)
            {
                Station.TrackedObject.Songs.Remove(song);

                var fileName = Path.GetFileName(song.FilePath);
                Station.TrackedObject.MetaData.SongOrder.Remove(fileName);
            }

        UpdateListsAndViews();
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void BtnRemoveAllSongs_Click(object sender, EventArgs e)
    {
        if (lvSongs.Items.Count == 0) return;

        if (ConfirmRemoveAllSongs() == DialogResult.No)
            return;

        lvSongs.Items.Clear();
        Station.TrackedObject.Songs.Clear();
        Station.TrackedObject.MetaData.SongOrder.Clear();
        UpdateListsAndViews();
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private DialogResult ConfirmRemoveAllSongs()
    {
        return MessageBox.Show(this, Strings.DeleteAllSongsConfirm, Strings.Confirm, MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);
    }

    private void LocateToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lvSongs.SelectedItems[0].Tag is not Song song) return;

        var fileName = Path.GetFileName(song.FilePath);
        if (string.IsNullOrEmpty(fileName)) return;

        fdlgOpenSongs.Multiselect = false;
        fdlgOpenSongs.FileName = Directory.GetParent(song.FilePath)?.FullName;
        fdlgOpenSongs.Title = Strings.LocateSong;
        fdlgOpenSongs.Filter = @$"{fileName}|{fileName}"; // Show only the specific file name

        if (fdlgOpenSongs.ShowDialog(this) == DialogResult.OK)
        {
            lvSongs.BeginUpdate();
            // Ensure the selected file has the same name as song.FilePath
            if (Path.GetFileName(fdlgOpenSongs.FileName).Equals(fileName, StringComparison.OrdinalIgnoreCase))
            {
                song.FilePath = fdlgOpenSongs.FileName;
                lvSongs.SelectedItems[0].SubItems[5].Text = song.FilePath;
                lvSongs.Invalidate();
            }
            else
            {
                MessageBox.Show(Strings.InvalidFile, Strings.InvalidFileCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            lvSongs.EndUpdate();
        }

        // Reset the file browser to its original state
        fdlgOpenSongs.Multiselect = true;
        fdlgOpenSongs.Title = Strings.AddSongsFileBrowserTitle;
        fdlgOpenSongs.Filter = @"Audio/Video Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma;*.aac;*.m4a;
                                    *.aiff;*.alac;*.opus;*.amr;*.ac3;*.mp4;*.m4v;*.mov;*.avi;*.wmv;*.flv;*.mkv;
                                    *.webm;*.mpeg;*.mpg;*.3gp;*.3g2;*.ts;*.mts;*.m2ts";
        fdlgOpenSongs.FileName = string.Empty;
    }

    private void locateAllMissingSongsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var folderDialog = new FolderBrowserDialog();
        folderDialog.Description = Strings.LocateAllMissingSongsDesc;
        folderDialog.UseDescriptionForTitle = true;

        if (folderDialog.ShowDialog(this) != DialogResult.OK) return;
        var selectedFolder = folderDialog.SelectedPath;

        // Get the list of files in the selected folder
        var filesInFolder = FileHelper.SafeEnumerateFiles(selectedFolder, "*.*", SearchOption.AllDirectories)
            .Select(f => new FileInfo(f))
            .ToList();

        lvSongs.BeginUpdate();
        foreach (ListViewItem item in lvSongs.SelectedItems)
        {
            if (item.Tag is not Song song) continue;

            // Try to find a matching file in the selected folder
            var matchingFile = filesInFolder.FirstOrDefault(f =>
                Path.GetFileName(f.FullName).Equals(Path.GetFileName(song.FilePath), StringComparison.OrdinalIgnoreCase)
                && (ulong)f.Length == song.FileSize);

            // Skip to the next song if no matching file was found
            if (matchingFile == null) continue;

            // Update the song's file path
            song.FilePath = matchingFile.FullName;
            item.SubItems[5].Text = song.FilePath; // Update the file path in the ListView
        }

        lvSongs.EndUpdate();
        lvSongs.Invalidate(); // Refresh the ListView to show updated paths
    }

    private void LvSongs_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right) return;

        var hasMissingSongs = false;

        if (lvSongs.SelectedItems.Count > 1)
        {
            // Multiple items selected, check if any of the selected songs are missing
            foreach (ListViewItem item in lvSongs.SelectedItems)
            {
                if (item?.Tag is not Song song) continue;

                // If at least one song is missing, set hasMissingSongs to true
                if (FileHelper.DoesFileExist(song.FilePath)) continue;

                hasMissingSongs = true;
                break;
            }

            // Show the "Locate All Missing Songs..." option only if any missing songs are found
            locateAllMissingSongsToolStripMenuItem.Visible = hasMissingSongs;
            locateToolStripMenuItem.Visible = false;
        }
        else
        {
            var hitTestInfo = lvSongs.HitTest(e.Location);
            var item = hitTestInfo.Item;

            if (item?.Tag is not Song song) return;

            // Show the context menu only if the song is missing
            hasMissingSongs = !FileHelper.DoesFileExist(song.FilePath);

            locateToolStripMenuItem.Visible =
                hasMissingSongs; // Show the single "Locate Missing Song..." option if the song is missing
            locateAllMissingSongsToolStripMenuItem.Visible = false;
        }

        // Show the context menu only if there are missing songs
        if (hasMissingSongs) cmsSongRightClick.Show(Cursor.Position);
    }

    /// <summary>
    /// Updates the song list view, the order list view, and synchronizes the song order.
    /// </summary>
    private void UpdateListsAndViews()
    {
        PopulateListView();
        PopulateSongListBox();
        SynchronizeSongOrder();
        UpdateStatLabels();
    }

    private void UpdateStatLabels()
    {
        lblTotalSongsVal.Text = Station.TrackedObject.Songs.Count.ToString();
        lblStationSizeVal.Text = ((ulong)Station.TrackedObject.Songs.Sum(s => (long)s.FileSize)).FormatSize();
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

            if (!Station.TrackedObject.Songs.Contains(song))
                lvSongOrder.Items.Remove(item);
        }

        UpdateOrderColumn();
        lvSongOrder.EndUpdate();
    }

    private void LvSongs_ColumnClick(object sender, ColumnClickEventArgs e)
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

    private void LvSongs_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if (lvSongs.SelectedItems.Count != 1) return;

        if (lvSongs.SelectedItems[0].Tag is not Song song) return;

        if (Directory.GetParent(song.FilePath) is { } parentDir)
            Process.Start("explorer.exe", parentDir.FullName);
    }

    private void btnAddSongs_MouseEnter(object sender, EventArgs e)
    {
        StatusChanged?.Invoke(this, Strings.AddSongsHelp);
    }

    private void btnRemoveFromOrder_MouseEnter(object sender, EventArgs e)
    {
        StatusChanged?.Invoke(this, Strings.RemoveFromOrderHelp);
    }

    private void btnAddToOrder_MouseEnter(object sender, EventArgs e)
    {
        StatusChanged?.Invoke(this, Strings.AddToOrderHelp);
    }

    private void btnRemoveSongs_MouseEnter(object sender, EventArgs e)
    {
        StatusChanged?.Invoke(this, Strings.RemoveSongsHelp);
    }

    private void btnRemoveAllSongs_MouseEnter(object sender, EventArgs e)
    {
        StatusChanged?.Invoke(this, Strings.RemoveAllSongsHelp);
    }

    private void lblTotalSongsVal_MouseEnter(object sender, EventArgs e)
    {
        StatusChanged?.Invoke(this, Strings.TotalSongsHelp);
    }

    private void lblStationSizeVal_MouseEnter(object sender, EventArgs e)
    {
        StatusChanged?.Invoke(this, Strings.TotalStationSizeHelp);
    }

    private void MouseLeaveControl(object sender, EventArgs e)
    {
        StatusReset?.Invoke(this, EventArgs.Empty);
    }

    #region Song Order

    /// <summary>
    /// Populates the song order list box with the songs from the binding list.
    /// </summary>
    private void PopulateSongListBox()
    {
        lbSongs.BeginUpdate();
        lbSongs.Items.Clear();
        foreach (var s in Station.TrackedObject.Songs
                     .Where(s => !lvSongOrder.Items
                         .Cast<ListViewItem>()
                         .Any(item => item.Tag != null && item.Tag.Equals(s))))
            lbSongs.Items.Add(s);
        lbSongs.EndUpdate();
    }

    private void BtnAddToOrder_Click(object sender, EventArgs e)
    {
        if (lbSongs.SelectedItems.Count <= 0) return;

        var selectedSongs = lbSongs.SelectedItems.Cast<Song>().ToList();
        foreach (var item in selectedSongs.OfType<Song>())
        {
            AddSongToOrderListView(item);
            lbSongs.Items.Remove(item);
        }

        UpdateListsAndViews();

        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void BtnRemoveFromOrder_Click(object sender, EventArgs e)
    {
        if (lvSongOrder.SelectedItems.Count <= 0) return;
        RemoveSongsFromOrderListView();

        UpdateListsAndViews();
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Adds a song to the order list view.
    /// </summary>
    /// <param name="song">The song to add.</param>
    /// <param name="updateOrderedList">Indicates whether to update the ordered list.</param>
    private void AddSongToOrderListView(Song song, bool updateOrderedList = true)
    {
        ListViewItem item = new([(lvSongOrder.Items.Count + 1).ToString(), song.Title])
        {
            Name = song.Title,
            Tag = song
        };

        lvSongOrder.Items.Add(item);
        UpdateOrderColumn();

        if (updateOrderedList)
            UpdateOrderedList();

        lvSongOrder.ResizeColumns();
    }

    private void RemoveSongsFromOrderListView()
    {
        foreach (var item in lvSongOrder.SelectedItems)
        {
            if (item is not ListViewItem lvItem) continue;
            if (lvItem.Tag is not Song song) continue;

            lvSongOrder.Items.Remove(lvItem);
            lbSongs.Items.Add(song);
        }

        if (lvSongOrder.Items.Count > 0)
        {
            lvSongOrder.Items[0].Selected = true;
            lvSongOrder.EnsureVisible(0);
        }

        UpdateOrderColumn();
        UpdateOrderedList();
    }

    /// <summary>
    /// Updates the order column numbers in the song order list view.
    /// </summary>
    private void UpdateOrderColumn()
    {
        for (var i = 0; i < lvSongOrder.Items.Count; i++)
            lvSongOrder.Items[i].SubItems[0].Text = (i + 1).ToString();
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
            AddSongToOrderListView(song, false);

        UpdateListsAndViews();
    }

    /// <summary>
    /// Updates the ordered list of songs in the station with the items in the list view.
    /// </summary>
    private void UpdateOrderedList()
    {
        Station.TrackedObject.MetaData.SongOrder.Clear();
        foreach (ListViewItem item in lvSongOrder.Items)
            if (item.Tag is Song song)
                Station.TrackedObject.MetaData.SongOrder.Add(Path.GetFileName(song.FilePath));
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

    private void LvSongs_KeyDown(object sender, KeyEventArgs e)
    {
        if (!e.Control || e.KeyCode != Keys.A) return;

        SelectAllItems(lvSongs);
        e.SuppressKeyPress = true;
    }

    /// <summary>
    /// Selects all the items in a <see cref="ListView"/>.
    /// </summary>
    /// <param name="listView">The <see cref="ListView"/> to select items of.</param>
    private static void SelectAllItems(ListView listView)
    {
        listView.BeginUpdate();
        foreach (ListViewItem item in listView.Items)
            item.Selected = true;
        listView.EndUpdate();
    }

    #endregion
}
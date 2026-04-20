// ReplacementStationEditor.cs : RadioExt-Helper
// Copyright (C) 2026  Ethan Hann
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

#region

using System.ComponentModel;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.custom_controls;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using WIG.Lib.Models.Audio;

#endregion

namespace RadioExt_Helper.user_controls;

/// <summary>
/// Represents an editor control for modifying replacement radio stations.
/// </summary>
public sealed partial class ReplacementStationEditor : UserControl, IEditor
{
    private readonly ImageList _imageList = new();

    private readonly BindingList<AudioTrack> _replacedTracks = [];

    private readonly Dictionary<AudioTrack, ReplacedTrackPropertiesCtl?> _replacementTrackMap = [];
    private readonly ImageList _tabImages = new();

    /// <summary>
    ///     Create a new ReplacementStationEditor for the specified replacement station.
    /// </summary>
    /// <param name="station"></param>
    public ReplacementStationEditor(TrackableObject<ReplacementStation> station)
    {
        InitializeComponent();
        Dock = DockStyle.Fill;

        SetTabImages();
        SetImageList();

        ReplacedStation = station;
    }

    /// <summary>
    /// Finalizes an instance of the ReplacementStationEditor class and performs cleanup operations before the object is
    /// reclaimed by garbage collection.
    /// </summary>
    /// <remarks>This destructor unsubscribes from the ContentChanged event to help prevent memory leaks. It
    /// is called automatically by the garbage collector and should not be invoked directly.</remarks>
    ~ReplacementStationEditor() => tinyEditor.ContentChanged -= TinyEditor_ContentChanged;

    /// <inheritdoc />
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <inheritdoc />
    public EditorType Type { get; set; } = EditorType.StationEditor;

    /// <summary>
    ///     Null for this editor type.
    /// </summary>
    public TrackableObject<AdditionalStation>? Station => null;

    /// <summary>
    ///     Gets the tracked replacement station associated with this control.
    /// </summary>
    public TrackableObject<ReplacementStation>? ReplacedStation { get; }

    /// <inheritdoc />
    public void Translate()
    {
        //todo: implement translation
        lblVanillaName.Text = Strings.VanillaStationName;
        lblDisplayName.Text = Strings.VanillaStationDisplayName;
        lblIcon.Text = Strings.VanillaStationIcon;

        btnRemoveReplacedTrack.Text = Strings.RemoveReplacedTrack;
        btnReplaceTrack.Text = Strings.ReplaceTrack;
        btnReplaceAllTracks.Text = Strings.ReplaceAllTracks;
        btnRemoveAllTracks.Text = Strings.RemoveAllReplacedTracks;

        grpDisplay.Text = Strings.DisplaySettings;
        grpNotes.Text = Strings.Notes;
        grpReplacedTracks.Text = Strings.ReplacedTracksGroupBox;
        grpVanillaTracks.Text = Strings.VanillaTracksGroupBox;
        grpTrackProperties.Text = Strings.ReplacedTrackProperties;

        tabMainInfo.Text = Strings.VanillaStationMainInfoTab;
        tabMusic.Text = Strings.VanillaStationTracksTab;

        colReplaced.Text = Strings.ListView_Column_IsReplaced;
        colTrackArtist.Text = Strings.VanillaStationSelector_Column_TrackArtist;
        colTrackDuration.Text = Strings.VanillaStationSelector_Column_TrackDuration;
        colTrackName.Text = Strings.VanillaStationSelector_Column_TrackName;

        lblStatus.Text = Strings.Ready;
        tinyEditor.Translate();

        //Translate track properties editors
        foreach (var editor in _replacementTrackMap.Values)
            editor?.Translate();
    }

    /// <summary>
    ///     Event that is raised when the station is updated.
    /// </summary>
    public event EventHandler? StationUpdated;

    private void ReplacementStationEditor_Load(object sender, EventArgs e)
    {
        SuspendLayout();

        Translate();

        lvTracks.OwnerDraw = true;
        lvTracks.DrawColumnHeader += (_, args) => args.DrawDefault = true;
        lvTracks.DrawSubItem += LvTracks_DrawSubItem;

        SetDisplayTabValues();
        SetMusicTabValues();

        ResumeLayout();
    }

    private void SetImageList()
    {
        _imageList.Images.Add("enabled", Resources.enabled);
        _imageList.Images.Add("disabled", Resources.disabled);
        _imageList.ImageSize = new Size(16, 16);
        lvTracks.SmallImageList = _imageList;
    }

    private void LvTracks_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
    {
        if (e.ColumnIndex == 0) // Assuming the icon is in the first column
        {
            if (e.Item == null || lvTracks.SmallImageList == null ||
                e.Item.Tag is not AudioTrack track) return;

            var replacedTracks = lbReplacedTracks.Items.Cast<AudioTrack>().ToList();

            var image = lvTracks.SmallImageList.Images[
                replacedTracks.Find(t => t.TrackName.Equals(track.TrackName)) != null ? "enabled" : "disabled"];
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
    ///     Sets the images for the tabs.
    /// </summary>
    private void SetTabImages()
    {
        _tabImages.Images.Add("display", Resources.display_frame);
        _tabImages.Images.Add("music", Resources.sound_waves);
        tabControl.ImageList = _tabImages;
        tabMainInfo.ImageKey = @"display";
        tabMusic.ImageKey = @"music";
    }

    private void SetDisplayTabValues()
    {
        if (ReplacedStation == null) return;

        txtVanillaStationName.Text = ReplacedStation.TrackedObject.VanillaStation?.StationName ?? "Unknown Station";
        txtDisplayName.Text = ReplacedStation.TrackedObject.DisplayName;

        var vanillaStation = ReplacedStation.TrackedObject.VanillaStation;
        if (vanillaStation != null)
            pbStationIcon.Image = ReplacementStationListBox.GetOrCreateThumb(vanillaStation);

        // Set the notes in the TinyMCE editor
        _ = tinyEditor.SetHtmlAsync(ReplacedStation.TrackedObject.Notes);
        tinyEditor.ContentChanged += TinyEditor_ContentChanged;
    }

    private void TinyEditor_ContentChanged(object? sender, string e)
    {
        if (ReplacedStation == null) return;
        ReplacedStation.TrackedObject.Notes = e;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void SetMusicTabValues()
    {
        lvTracks.BeginUpdate();
        lbReplacedTracks.BeginUpdate();

        try
        {
            _replacedTracks.Clear();
            _replacementTrackMap.Clear();

            if (ReplacedStation == null) return;

            var vanillaTracks = ReplacedStation.TrackedObject.VanillaStation?.Tracks;
            if (vanillaTracks == null)
                return;

            // A replacement station can have multiple replacement entries for the same vanilla track
            // (one per WEM ID). The editor is track-name based, so we only add one UI entry per track name.
            var replacedTrackNames = ReplacedStation.TrackedObject.Tracks
                .Select(track => track.VanillaTrackName)
                .Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (var replacedTrackName in replacedTrackNames)
            {
                var matchingVanillaTrack = vanillaTracks
                    .FirstOrDefault(t => t.TrackName.Equals(replacedTrackName, StringComparison.OrdinalIgnoreCase));
                if (matchingVanillaTrack == null)
                    continue;

                _replacedTracks.Add(matchingVanillaTrack);

                if (_replacementTrackMap.Keys.Any(t =>
                        t.TrackName.Equals(matchingVanillaTrack.TrackName, StringComparison.OrdinalIgnoreCase)))
                    continue;

                _replacementTrackMap.Add(matchingVanillaTrack,
                    CreateTrackPropertiesControl(matchingVanillaTrack.TrackName));
            }

            PopulateListView();

            lbReplacedTracks.DataSource = null;
            lbReplacedTracks.DataSource = _replacedTracks;
            lbReplacedTracks.DisplayMember = "ToString";

            lvTracks.Invalidate();
        }
        finally
        {
            lvTracks.EndUpdate();
            lbReplacedTracks.EndUpdate();
        }
    }

    private void PopulateListView()
    {
        lvTracks.SuspendLayout();
        lvTracks.Items.Clear();

        var vanillaTracks = ReplacedStation?.TrackedObject.VanillaStation?.Tracks;
        if (vanillaTracks == null) return;

        foreach (var song in vanillaTracks) //TODO: Error here because the track durations are null for the vanilla station tracks on replaced stations
        {
            var durations = song.TrackDuration.Select(d => TimeSpan.FromSeconds(d).ToString("g")).ToList();
            string durationString;
            if (durations.Count > 1)
                durationString = string.Join(", ", durations);
            else
                durationString = durations.FirstOrDefault() ?? "Unknown";

            ListViewItem lvItem = new([
                    string.Empty, // Placeholder for icon
                    song.TrackName,
                    song.TrackArtist,
                    durationString
                ])
            { Tag = song };

            lvTracks.Items.Add(lvItem);
        }

        lvTracks.ResizeColumns();
        lvTracks.ResumeLayout();
    }

    /// <summary>
    ///     Resets the UI values to the defaults for the station.
    /// </summary>
    public void ResetUi()
    {
        SetDisplayTabValues();
        SetMusicTabValues();
    }

    private void SelectTrackInListBox(AudioTrack track)
    {
        if (!_replacedTracks.Contains(track)) return;

        lbReplacedTracks.SelectedItem = track;
        SwapPropertiesControl(track);
    }

    private void btnReplaceTrack_Click(object sender, EventArgs e)
    {
        if (lvTracks.SelectedItems.Count <= 0) return;
        if (ReplacedStation == null) return;

        // Add the selected track to the replaced tracks list if it does not already exist
        if (lvTracks.SelectedItems[0].Tag is not AudioTrack track) return;
        if (_replacedTracks.Contains(track)) return;

        _replacedTracks.Add(track);
        TryAddPropertiesControl(track);
        SelectTrackInListBox(track);

        lvTracks.BeginUpdate();
        lvTracks.Invalidate(); // Refresh the ListView to update the icon
        lvTracks.EndUpdate();

        ReplacedStation?.TrackedObject.SyncStagingFolder(GlobalData.ConfigManager.CurrentConfig.StagingPath);
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void btnReplaceAllTracks_Click(object sender, EventArgs e)
    {
        try
        {
            lbReplacedTracks.BeginUpdate();

            lbReplacedTracks.DataSource = null;
            _replacedTracks.Clear();

            if (ReplacedStation == null) return;

            var tracks = lvTracks.Items.Cast<ListViewItem>().ToList();
            foreach (var item in tracks)
            {
                if (item.Tag is not AudioTrack track) continue;
                _replacedTracks.Add(track);
                TryAddPropertiesControl(track);
            }

            lbReplacedTracks.DataSource = _replacedTracks;
            lbReplacedTracks.DisplayMember = "ToString";
            lbReplacedTracks.EndUpdate();

            SelectTrackInListBox(_replacedTracks.Last());

            lvTracks.BeginUpdate();
            lvTracks.Invalidate(); // Refresh the ListView to update the icon
            lvTracks.EndUpdate();

            ReplacedStation?.TrackedObject.SyncStagingFolder(GlobalData.ConfigManager.CurrentConfig.StagingPath);
            StationUpdated?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<ReplacementStationEditor>("btnReplaceAllTracks_Click")
                .Error("An error occurred while replacing all tracks.", ex);
        }
    }

    private void btnRemoveReplacedTrack_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbReplacedTracks.SelectedItems.Count <= 0) return;

            // Remove the selected track from the replaced tracks list if it exists and re-enable it in the main list
            if (lbReplacedTracks.SelectedItems[0] is not AudioTrack track) return;

            _replacedTracks.Remove(track);
            _replacementTrackMap.Remove(track);

            if (_replacedTracks.Count > 0)
                SelectTrackInListBox(_replacedTracks.Last()); // Select the last track in the list after removal
            else
                ResetPropertiesUi(); //No tracks in the listbox, remove properties UI

            lvTracks.BeginUpdate();
            lvTracks.Invalidate(); // Refresh the ListView to update the icon
            lvTracks.EndUpdate();

            //Remove the replacement track from the station as well
            if (ReplacedStation?.TrackedObject.Tracks.Count <= 0)
                return;

            ReplacedStation?.TrackedObject.Tracks.Remove(ReplacedStation.TrackedObject.Tracks.First(t => t.VanillaTrackName.Equals(track.TrackName)));

            ReplacedStation?.TrackedObject.SyncStagingFolder(GlobalData.ConfigManager.CurrentConfig.StagingPath);
            StationUpdated?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<ReplacementStationEditor>("btnRemoveReplacedTrack_Click")
                .Error("An error occurred while removing the replaced track.", ex);
        }
    }

    private void btnRemoveAllTracks_Click(object sender, EventArgs e)
    {
        //Confirm with dialog before removing all tracks
        var caption = Strings.Confirm;
        var text = Strings.ConfirmRemoveAllReplacementTracks;

        if (MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            return;

        lbReplacedTracks.BeginUpdate();
        lbReplacedTracks.DataSource = null;
        _replacedTracks.Clear();
        _replacementTrackMap.Clear();

        //Clear all replacement tracks from the station.
        ReplacedStation?.TrackedObject.Tracks.Clear();

        ResetPropertiesUi();

        lbReplacedTracks.DataSource = _replacedTracks;
        lbReplacedTracks.DisplayMember = "ToString";

        lbReplacedTracks.EndUpdate();

        lvTracks.BeginUpdate();
        lvTracks.Invalidate(); // Refresh the ListView to update the icon
        lvTracks.EndUpdate();

        ReplacedStation?.TrackedObject.SyncStagingFolder(GlobalData.ConfigManager.CurrentConfig.StagingPath);
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void lvTracks_DoubleClick(object sender, EventArgs e) => btnReplaceTrack.PerformClick();

    private void ResetPropertiesUi()
    {
        pnlTrackProperties.Controls.Clear();
    }

    /// <summary>
    ///     Updates the station's display name. Does not affect the in-game name. Mainly used when the main form detects a
    ///     duplication.
    /// </summary>
    /// <param name="newName"></param>
    public void UpdateStationName(string newName)
    {
        txtDisplayName.Text = newName;
        ReplacedStation!.TrackedObject.DisplayName = newName;
    }

    private void txtDisplayName_TextChanged(object sender, EventArgs e)
    {
        ReplacedStation!.TrackedObject.DisplayName = txtDisplayName.Text;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void lbReplacedTracks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lbReplacedTracks.SelectedIndex < 0) return;

        if (lbReplacedTracks.SelectedItem is not AudioTrack selectedTrack) return;

        if (SwapPropertiesControl(selectedTrack)) return;

        // This should never happen, but just in case, add a new control and swap again
        TryAddPropertiesControl(selectedTrack);
        SwapPropertiesControl(selectedTrack);
    }

    private void TryAddPropertiesControl(AudioTrack track)
    {
        if (ReplacedStation == null) return;

        if (_replacementTrackMap.ContainsKey(track)) return;
        if (_replacementTrackMap.Keys.Any(t => t.TrackName.Equals(track.TrackName, StringComparison.OrdinalIgnoreCase)))
            return;

        var propertiesCtl = CreateTrackPropertiesControl(track.TrackName);
        _replacementTrackMap.Add(track, propertiesCtl);
    }

    private ReplacedTrackPropertiesCtl CreateTrackPropertiesControl(string trackName)
    {
        var propertiesCtl = new ReplacedTrackPropertiesCtl(ReplacedStation!, trackName);
        propertiesCtl.TrackChanged += OnTrackChanged;
        return propertiesCtl;
    }

    private void OnTrackChanged(object? sender, string e)
    {
        if (ReplacedStation == null) return;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private bool SwapPropertiesControl(AudioTrack track)
    {
        ResetPropertiesUi();

        if (!_replacementTrackMap.TryGetValue(track, out var trackPropertiesCtl))
            trackPropertiesCtl = _replacementTrackMap
                .FirstOrDefault(kvp => kvp.Key.TrackName.Equals(track.TrackName, StringComparison.OrdinalIgnoreCase))
                .Value;

        if (trackPropertiesCtl == null)
            return false;

        trackPropertiesCtl.Dock = DockStyle.Fill;
        pnlTrackProperties.Controls.Add(trackPropertiesCtl);
        return true;
    }

    #region Hover Help

    private void lblVanillaName_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.VanillaStationNameHelp;
    }

    private void lblDisplayName_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.VanillaStationDisplayNameHelp;
    }

    private void lblIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.VanillaStationIconHelp;
    }

    private void btnRemoveReplacedTrack_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.RemoveReplacedTrackHelp;
    }

    private void lbReplacedTracks_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.ReplacedTracksListHelp;
    }

    private void btnReplaceAllTracks_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.ReplaceAllTracksHelp;
    }

    private void btnRemoveAllTracks_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.RemoveAllReplacedTracksHelp;
    }

    /// <summary>
    ///     Occurs when the mouse leaves a label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Lbl_MouseLeave(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.Ready;
    }

    #endregion
}
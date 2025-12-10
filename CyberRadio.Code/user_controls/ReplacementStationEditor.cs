// // ReplacementStationEditor.cs : RadioExt-Helper
// // Copyright (C) 2025  Ethan Hann
// //
// // This program is free software: you can redistribute it and/or modify
// // it under the terms of the GNU General Public License as published by
// // the Free Software Foundation, either version 3 of the License, or
// // (at your option) any later version.
// //
// // This program is distributed in the hope that it will be useful,
// // but WITHOUT ANY WARRANTY; without even the implied warranty of
// // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// // GNU General Public License for more details.
// //
// // You should have received a copy of the GNU General Public License
// // along with this program.  If not, see <https://www.gnu.org/licenses/>.

#region

using System.ComponentModel;
using RadioExt_Helper.custom_controls;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using System.Windows.Forms;
using AetherUtils.Core.Extensions;
using WIG.Lib.Models.Audio;

#endregion

namespace RadioExt_Helper.user_controls;

/// <summary>
/// Represents an editor control for modifying replacement radio stations.
/// </summary>
public sealed partial class ReplacementStationEditor : UserControl, IEditor
{
    private readonly ImageList _tabImages = new();
    private readonly ImageList _imageList = new();

    private readonly BindingList<AudioTrack> _replacedTracks = [];

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

    public Guid Id { get; set; } = Guid.NewGuid();
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

        tabMainInfo.Text = Strings.VanillaStationMainInfoTab;
        tabMusic.Text = Strings.VanillaStationTracksTab;

        colReplaced.Text = Strings.ListView_Column_IsReplaced;
        colTrackArtist.Text = Strings.VanillaStationSelector_Column_TrackArtist;
        colTrackDuration.Text = Strings.VanillaStationSelector_Column_TrackDuration;
        colTrackName.Text = Strings.VanillaStationSelector_Column_TrackName;

        lblStatus.Text = Strings.Ready;
        tinyEditor.Translate();
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

            var image = lvTracks.SmallImageList.Images[replacedTracks.Find(t => t.TrackName.Equals(track.TrackName)) != null ? "enabled" : "disabled"];
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
        txtVanillaStationName.Text = ReplacedStation?.TrackedObject?.VanillaStation?.StationName ?? "Unknown Station";
        txtDisplayName.Text = ReplacedStation?.TrackedObject?.DisplayName ?? "New Replacement Station";

        pbStationIcon.Image = ReplacementStationListBox.GetOrCreateThumb(ReplacedStation.TrackedObject.VanillaStation);
    }

    private void SetMusicTabValues()
    {
        lvTracks.BeginUpdate();
        lbReplacedTracks.BeginUpdate();

        _replacedTracks.Clear();
        if (ReplacedStation == null) return;

        var vanillaTracks = ReplacedStation.TrackedObject.VanillaStation.Tracks;
        foreach (var matchingVanillaTrack in ReplacedStation.TrackedObject.Tracks
                     .Select(track => vanillaTracks
                         .FirstOrDefault(t => t.TrackName.Equals(track.VanillaTrackName))).OfType<AudioTrack>())
        {
            _replacedTracks.Add(matchingVanillaTrack);
        }

        PopulateListView();

        lbReplacedTracks.DataSource = null;
        lbReplacedTracks.DataSource = _replacedTracks;
        lbReplacedTracks.DisplayMember = "TrackName";

        lvTracks.Invalidate();
        lvTracks.EndUpdate();
        lbReplacedTracks.EndUpdate();
    }

    private void PopulateListView()
    {
        lvTracks.SuspendLayout();
        lvTracks.Items.Clear();
        if (ReplacedStation == null) return;

        var vanillaTracks = ReplacedStation.TrackedObject.VanillaStation.Tracks;
        foreach (var song in vanillaTracks)
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

    private void btnReplaceTrack_Click(object sender, EventArgs e)
    {
        if (lvTracks.SelectedItems.Count <= 0) return;

        // Add the selected track to the replaced tracks list if it does not already exist
        if (lvTracks.SelectedItems[0].Tag is not AudioTrack track) return;
        if (_replacedTracks.Contains(track)) return;

        _replacedTracks.Add(track);

        lvTracks.BeginUpdate();
        lvTracks.Invalidate(); // Refresh the ListView to update the icon
        lvTracks.EndUpdate();
    }

    private void btnRemoveReplacedTrack_Click(object sender, EventArgs e)
    {
        if (lbReplacedTracks.SelectedItems.Count <= 0) return;

        // Remove the selected track from the replaced tracks list if it exists and re-enable it in the main list
        if (lvTracks.SelectedItems[0].Tag is not AudioTrack track) return;

        _replacedTracks.Remove(track);

        lvTracks.BeginUpdate();
        lvTracks.Invalidate(); // Refresh the ListView to update the icon
        lvTracks.EndUpdate();
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
        //TODO: add track editing functionality
        //TODO: add replace all button
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

    private void btnReplaceTrack_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.ReplaceTrackHelp;
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
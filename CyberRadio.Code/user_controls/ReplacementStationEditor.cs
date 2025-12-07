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

using RadioExt_Helper.custom_controls;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

#endregion

namespace RadioExt_Helper.user_controls;

public sealed partial class ReplacementStationEditor : UserControl, IEditor
{
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

        grpDisplay.Text = Strings.DisplaySettings;
        grpNotes.Text = Strings.Notes;

        tabMainInfo.Text = Strings.VanillaStationMainInfoTab;
        tabMusic.Text = Strings.VanillaStationTracksTab;

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

        SetDisplayTabValues();
        SetMusicTabValues();
        Translate();

        ResumeLayout();
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
    }

    /// <summary>
    ///     Resets the UI values to the defaults for the station.
    /// </summary>
    public void ResetUi()
    {
        SetDisplayTabValues();
        SetMusicTabValues();
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

    /// <summary>
    ///     Set the status text to the default text.
    /// </summary>
    private void ResetStatusText()
    {
        lblStatus.Text = Strings.Ready;
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
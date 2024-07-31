// StationEditor.cs : RadioExt-Helper
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

using AetherUtils.Core.WinForms.Controls;
using AetherUtils.Core.WinForms.CustomArgs;
using RadioExt_Helper.custom_controls;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using System.Globalization;

namespace RadioExt_Helper.user_controls;

/// <summary>
/// Represents a user control for editing a station.
/// </summary>
public sealed partial class StationEditor : UserControl, IUserControl
{
    private readonly ComboBox _cmbUiIcons;
    private readonly CustomMusicCtl _musicCtl;
    private readonly ImageList _tabImages = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="StationEditor"/> class.
    /// </summary>
    /// <param name="station">The trackable station object.</param>
    public StationEditor(TrackableObject<Station> station)
    {
        InitializeComponent();
        Dock = DockStyle.Fill;

        SetTabImages();

        Station = station;
        _musicCtl = new CustomMusicCtl(Station);
        _musicCtl.StationUpdated += (_, _) => StationUpdated?.Invoke(this, EventArgs.Empty);

        _cmbUiIcons = GlobalData.CloneTemplateComboBox();
        _cmbUiIcons.SelectedIndexChanged += CmbUIIcons_SelectedIndexChanged;

        // Initialize DataGridView columns
        InitializeDataGridViewColumns();
    }

    /// <summary>
    /// Gets the trackable station object.
    /// </summary>
    public TrackableObject<Station> Station { get; }

    /// <summary>
    /// Translates the user control to the current language.
    /// </summary>
    public void Translate()
    {
        tabDisplayAndIcon.Text = GlobalData.Strings.GetString("DisplayAndIcon");
        lblName.Text = GlobalData.Strings.GetString("DisplayName");
        lblIcon.Text = GlobalData.Strings.GetString("Icon");
        lblUsingCustomIcon.Text = GlobalData.Strings.GetString("Using?");
        lblInkPath.Text = GlobalData.Strings.GetString("InkAtlasPath");
        lblInkPart.Text = GlobalData.Strings.GetString("InkAtlasPart");
        lblFM.Text = GlobalData.Strings.GetString("FM");
        lblVolume.Text = GlobalData.Strings.GetString("Volume");
        lblVolumeVal.Text = GlobalData.Strings.GetString("Value");
        radUseCustomYes.Text = GlobalData.Strings.GetString("Yes");
        radUseCustomNo.Text = GlobalData.Strings.GetString("No");
        grpDisplay.Text = GlobalData.Strings.GetString("Display");
        grpCustomIcon.Text = GlobalData.Strings.GetString("CustomIcon");
        grpSettings.Text = GlobalData.Strings.GetString("Settings");
        grpNotes.Text = GlobalData.Strings.GetString("CustomDataGroup");
        dgvMetadata.Columns[0].HeaderText = GlobalData.Strings.GetString("CustomDataKey");
        dgvMetadata.Columns[1].HeaderText = GlobalData.Strings.GetString("CustomDataValue");

        tabMusic.Text = GlobalData.Strings.GetString("Music");
        lblUseStream.Text = GlobalData.Strings.GetString("UseStream");
        lblStreamURL.Text = GlobalData.Strings.GetString("StreamURL");
        btnGetFromRadioGarden.Text = GlobalData.Strings.GetString("ParseFromRadioGarden");
        grpStreamSettings.Text = GlobalData.Strings.GetString("StreamSettings");
        grpSongs.Text = GlobalData.Strings.GetString("Songs");
        radUseStreamYes.Text = GlobalData.Strings.GetString("Yes");
        radUseStreamNo.Text = GlobalData.Strings.GetString("No");

        lblStatus.Text = GlobalData.Strings.GetString("Ready");

        _musicCtl.Translate();
    }

    /// <summary>
    /// Event that is raised when the station is updated.
    /// </summary>
    public event EventHandler? StationUpdated;

    private void InitializeDataGridViewColumns()
    {
        dgvMetadata.Columns.Add("colKey", GlobalData.Strings.GetString("MetaDataKey") ?? "Key");
        dgvMetadata.Columns.Add("colValue", GlobalData.Strings.GetString("MetaDataValue") ?? "Value");
        dgvMetadata.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dgvMetadata.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }

    /// <summary>
    /// Gets the music player associated with the station.
    /// </summary>
    /// <returns>The music player.</returns>
    public MusicPlayer GetMusicPlayer()
    {
        return mpStreamPlayer;
    }

    /// <summary>
    /// Occurs when the control is loaded.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StationEditor_Load(object sender, EventArgs e)
    {
        SuspendLayout();
        tlpDisplayTable.Controls.Add(_cmbUiIcons, 1, 1);
        grpSongs.Controls.Add(_musicCtl);

        SetDisplayTabValues();
        SetMusicTabValues();
        Translate();

        ResumeLayout();
    }

    /// <summary>
    /// Sets the images for the tabs.
    /// </summary>
    private void SetTabImages()
    {
        _tabImages.Images.Add("display", Resources.display_frame);
        _tabImages.Images.Add("music", Resources.sound_waves);
        tabControl.ImageList = _tabImages;
        tabDisplayAndIcon.ImageKey = @"display";
        tabMusic.ImageKey = @"music";
    }

    #region Display and Icon Tab

    /// <summary>
    /// Set the values for the display tab based on the station's data.
    /// </summary>
    private void SetDisplayTabValues()
    {
        txtDisplayName.Text = Station.TrackedObject.MetaData.DisplayName;
        _cmbUiIcons.SelectedIndex = _cmbUiIcons.Items.IndexOf(Station.TrackedObject.MetaData.Icon);

        if (Station.TrackedObject.CustomIcon.UseCustom)
        {
            radUseCustomYes.Checked = true;
            radUseCustomNo.Checked = false;
        }
        else
        {
            radUseCustomNo.Checked = true;
            radUseCustomYes.Checked = false;
            txtInkAtlasPart.Visible = !radUseCustomNo.Checked;
            txtInkAtlasPath.Visible = !radUseCustomNo.Checked;
            lblInkPart.Visible = !radUseCustomNo.Checked;
            lblInkPath.Visible = !radUseCustomNo.Checked;
        }

        txtInkAtlasPath.Text = Station.TrackedObject.CustomIcon.InkAtlasPath;
        txtInkAtlasPart.Text = Station.TrackedObject.CustomIcon.InkAtlasPart;
        nudFM.Value = (decimal)Station.TrackedObject.MetaData.Fm;
        volumeSlider.Value = (int)(Station.TrackedObject.MetaData.Volume / 0.1f);
        lblSelectedVolume.Text = $@"{Station.TrackedObject.MetaData.Volume:F1}";

        dgvMetadata.Rows.Clear();
        foreach (var (key, value) in Station.TrackedObject.MetaData.CustomData)
            dgvMetadata.Rows.Add(key, value);
    }

    /// <summary>
    /// Updates the station's display name. Mainly used when the main form detects a duplication.
    /// </summary>
    /// <param name="newName">The new station name.</param>
    public void UpdateStationName(string newName)
    {
        txtDisplayName.Text = newName;
        Station.TrackedObject.MetaData.DisplayName = newName;
    }

    /// <summary>
    /// Occurs when the display name text box is changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtDisplayName_TextChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.MetaData.DisplayName = txtDisplayName.Text;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the display name text box loses focus.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtDisplayName_Leave(object sender, EventArgs e)
    {
        StationManager.Instance.EnsureDisplayNameFormat(Station);
        nudFM.Value = (decimal)Station.TrackedObject.MetaData.Fm;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the UI icon combo box selection is changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CmbUIIcons_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_cmbUiIcons.SelectedItem is string iconStr)
            Station.TrackedObject.MetaData.Icon = iconStr;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the custom icon radio button "Yes" is checked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RadUseCustomYes_CheckedChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.CustomIcon.UseCustom = radUseCustomYes.Checked;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the custom icon radio button "No" is checked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RadUseCustomNo_CheckedChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.CustomIcon.UseCustom = !radUseCustomNo.Checked;
        txtInkAtlasPart.Visible = !radUseCustomNo.Checked;
        txtInkAtlasPath.Visible = !radUseCustomNo.Checked;
        lblInkPart.Visible = !radUseCustomNo.Checked;
        lblInkPath.Visible = !radUseCustomNo.Checked;
    }

    /// <summary>
    /// Occurs when the ink atlas path text is changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtInkAtlasPath_TextChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.CustomIcon.InkAtlasPath = txtInkAtlasPath.Text;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the ink atlas part text is changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtInkAtlasPart_TextChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.CustomIcon.InkAtlasPart = txtInkAtlasPart.Text;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the FM number is changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NudFM_ValueChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.MetaData.Fm = StationManager.Instance.EnsureDisplayNameFormat(Station, (float)nudFM.Value);
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the volume slider is scrolled.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void VolumeSlider_Scroll(object sender, EventArgs e)
    {
        Station.TrackedObject.MetaData.Volume = volumeSlider.Value * 0.1f;
        lblSelectedVolume.Text = $@"{Station.TrackedObject.MetaData.Volume:F1}";
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the volume value label is double-clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblVolumeVal_DoubleClick(object sender, EventArgs e)
    {
        LblSelectedVolume_DoubleClick(sender, e);
    }

    /// <summary>
    /// Occurs when the volume label is double-clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblSelectedVolume_DoubleClick(object sender, EventArgs e)
    {
        txtVolumeEdit.Text = lblSelectedVolume.Text;
        txtVolumeEdit.Size = lblSelectedVolume.Size;
        txtVolumeEdit.Visible = true;
        volumeSlider.Enabled = false;
        txtVolumeEdit.Focus();
        txtVolumeEdit.SelectAll();
    }

    /// <summary>
    /// Occurs when a key is held down in the volume text box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtVolumeEdit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;

        var maxVol = volumeSlider.Maximum * 0.1f;
        var minVol = volumeSlider.Minimum * 0.1f;

        if (!float.TryParse(txtVolumeEdit.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                out var newVolume)) return;

        if (newVolume > maxVol || newVolume < minVol)
        {
            e.Handled = true;
        }
        else
        {
            lblSelectedVolume.Text = txtVolumeEdit.Text;
            txtVolumeEdit.Visible = false;
            volumeSlider.Enabled = true;
            volumeSlider.Value = (int)(newVolume / 0.1f);
            VolumeSlider_Scroll(sender, e);
            e.Handled = true;
        }
    }

    /// <summary>
    /// Occurs when a key is pressed in the volume text box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtVolumeEdit_KeyPress(object sender, KeyPressEventArgs e)
    {
        // Allow control characters, digits, decimal point, and minus sign
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            e.Handled = true;

        // Allow only one decimal point
        if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
            e.Handled = true;
    }

    private void DgvMetadata_UserAddedRow(object sender, DataGridViewRowEventArgs e)
    {
        // New row added by the user
        var row = dgvMetadata.Rows[e.Row.Index - 1];
        var key = row.Cells[0].Value?.ToString();
        var value = row.Cells[1].Value?.ToString();
        if (key != null && value != null && Station.TrackedObject.MetaData.CustomData.TryAdd(key, value))
            StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void DgvMetadata_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        // Row deleted by the user
        foreach (DataGridViewCell cell in e.Row.Cells)
        {
            var key = cell.Value?.ToString();
            if (key == null || !Station.TrackedObject.MetaData.CustomData.ContainsKey(key)) continue;

            Station.TrackedObject.MetaData.CustomData.Remove(key);
            StationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    private void DgvMetadata_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        // Cell value changed by the user
        if (e is not { RowIndex: >= 0, ColumnIndex: >= 0 }) return;

        var row = dgvMetadata.Rows[e.RowIndex];
        var key = row.Cells[0].Value?.ToString();
        var value = row.Cells[1].Value?.ToString();
        if (key == null || value == null) return;

        Station.TrackedObject.MetaData.CustomData[key] = value;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    #endregion

    #region Music Tab

    /// <summary>
    /// Sets the values for the music tab based on the station's data.
    /// </summary>
    private void SetMusicTabValues()
    {
        radUseStreamYes.Checked = Station.TrackedObject.StreamInfo.IsStream;
        radUseStreamNo.Checked = !radUseStreamYes.Checked;

        ToggleStreamControls(Station.TrackedObject.StreamInfo.IsStream);

        txtStreamURL.Text = Station.TrackedObject.StreamInfo.StreamUrl;
    }

    /// <summary>
    /// Occurs when the use stream radio button "Yes" is checked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RadUseStreamYes_CheckedChanged(object sender, EventArgs e)
    {
        ToggleStreamControls(true);
        Station.TrackedObject.StreamInfo.IsStream = radUseStreamYes.Checked;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the use stream radio button "No" is checked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RadUseStreamNo_CheckedChanged(object sender, EventArgs e)
    {
        ToggleStreamControls(false);
        Station.TrackedObject.StreamInfo.IsStream = !radUseStreamNo.Checked;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the stream URL text box is changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtStreamURL_TextChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.StreamInfo.StreamUrl = txtStreamURL.Text;
        mpStreamPlayer.StreamUrl = Station.TrackedObject.StreamInfo.StreamUrl;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the "Get from Radio Garden" button is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnGetFromRadioGarden_Click(object sender, EventArgs e)
    {
        var text = GlobalData.Strings.GetString("RadioGardenInput") ??
                   "Enter the radio.garden URL from the web: ";
        var caption = GlobalData.Strings.GetString("RadioGardenURLCaption") ?? "Radio Garden URL";

        InputBox input = new(caption, text, RetrieveUrlFromInputBox);
        input.ShowDialog();
    }

    /// <summary>
    /// Retrieve the stream URL from the input box and validate it.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void RetrieveUrlFromInputBox(object? sender, EventArgs e)
    {
        var streamChecker = new AudioStreamChecker(TimeSpan.FromSeconds(5));
        if (e is not InputBoxEventArgs args) return;

        var streamUrl = AudioStreamChecker.ConvertRadioGardenUrl(args.InputText);
        var isValid = await streamChecker.IsAudioStreamValidAsync(streamUrl);
        if (isValid)
            txtStreamURL.Text = streamUrl;
        else
            txtStreamURL.Text = GlobalData.Strings.GetString("InvalidStream") ??
                                "Invalid stream - Will not work in game";
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Toggle the stream controls on or off.
    /// </summary>
    /// <param name="onOff"></param>
    private void ToggleStreamControls(bool onOff)
    {
        lblStreamURL.Visible = onOff;
        txtStreamURL.Visible = onOff;
        mpStreamPlayer.Visible = onOff;
        btnGetFromRadioGarden.Visible = onOff;

        if (!txtStreamURL.Visible)
            mpStreamPlayer.StopStream();

        grpSongs.Visible = !onOff;
    }

    #endregion

    #region Hover Help

    /// <summary>
    /// Occurs when the mouse enters the display name label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblName_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("StationNameHelp");
    }

    /// <summary>
    /// Occurs when the mouse enters the icon label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("IconHelp");
    }

    /// <summary>
    /// Occurs when the mouse enters the using custom icon label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblUsingCustomIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("CustomIconHelp");
    }

    /// <summary>
    /// Occurs when the mouse enters the ink path label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblInkPath_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("InkPathHelp");
    }

    /// <summary>
    /// Occurs when the mouse enters the ink part label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblInkPart_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("InkPartHelp");
    }

    /// <summary>
    /// Occurs when the mouse enters the FM label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblFM_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("FMHelp");
    }

    /// <summary>
    /// Occurs when the mouse enters the volume label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblVolume_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("VolumeHelp");
    }

    /// <summary>
    /// Occurs when the mouse enters the volume value label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblVolumeVal_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("VolumeValHelp");
    }

    /// <summary>
    /// Occurs when the mouse enters the use stream label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblUseStream_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("UseStreamHelp");
    }

    /// <summary>
    /// Occurs when the mouse enters the stream URL label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblStreamURL_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("StreamURLHelp");
    }

    /// <summary>
    /// Occurs when the mouse leaves a label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Lbl_MouseLeave(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("Ready");
    }

    #endregion
}
﻿// StationEditor.cs : RadioExt-Helper
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

using System.Globalization;
using AetherUtils.Core.Logging;
using RadioExt_Helper.custom_controls;
using RadioExt_Helper.forms;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using WIG.Lib.Models;
using ApplicationContext = RadioExt_Helper.utility.ApplicationContext;

namespace RadioExt_Helper.user_controls;

/// <summary>
/// Represents a user control for editing a station.
/// </summary>
public sealed partial class StationEditor : UserControl, IEditor
{
    private readonly ComboBox _cmbUiIcons;

    private readonly List<string?> _customDataKeys = ["Icon Name", "Image Path", "Archive Path", "SHA256 Archive Hash"];
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
        _musicCtl.StatusChanged += (_, text) => SetStatusText(text);
        _musicCtl.StatusReset += (_, _) => ResetStatusText();

        _cmbUiIcons = GlobalData.CloneTemplateComboBox();
        _cmbUiIcons.SelectedIndexChanged += CmbUIIcons_SelectedIndexChanged;

        // Initialize DataGridView columns
        InitializeDataGridViewColumns();
    }

    /// <summary>
    /// Gets the trackable station object.
    /// </summary>
    public TrackableObject<Station> Station { get; }

    public Guid Id { get; set; } = Guid.NewGuid();
    public EditorType Type { get; set; } = EditorType.StationEditor;

    /// <summary>
    /// Translates the user control to the current language.
    /// </summary>
    public void Translate()
    {
        tabDisplayAndIcon.Text = Strings.DisplayAndIcon;
        lblName.Text = Strings.DisplayName;
        lblIcon.Text = Strings.Icon;
        lblUsingCustomIcon.Text = Strings.UsingQuestion;
        lblInkPath.Text = Strings.InkAtlasPath;
        lblInkPart.Text = Strings.InkAtlasPart;
        lblFM.Text = Strings.FM;
        lblVolume.Text = Strings.Volume;
        lblVolumeVal.Text = Strings.Value;
        radUseCustomYes.Text = Strings.Yes;
        radUseCustomNo.Text = Strings.No;
        grpDisplay.Text = Strings.Display;
        grpCustomIcon.Text = Strings.CustomIcon;
        grpSettings.Text = Strings.Settings;
        grpCustomData.Text = Strings.CustomDataGroup;
        dgvMetadata.Columns[0].HeaderText = Strings.CustomDataKey;
        dgvMetadata.Columns[1].HeaderText = Strings.CustomDataValue;

        tabMusic.Text = Strings.Music;
        lblUseStream.Text = Strings.UseStream;
        lblStreamURL.Text = Strings.StreamURL;
        btnGetFromRadioGarden.Text = Strings.ParseFromRadioGarden;
        grpStreamSettings.Text = Strings.StreamSettings;
        grpSongs.Text = Strings.Songs;
        radUseStreamYes.Text = Strings.Yes;
        radUseStreamNo.Text = Strings.No;

        btnOpenIconManager.Text = Strings.IconManager;

        lblStatus.Text = Strings.Ready;

        _musicCtl.Translate();
    }

    /// <summary>
    /// Event that is raised when the station is updated.
    /// </summary>
    public event EventHandler? StationUpdated;

    private void InitializeDataGridViewColumns()
    {
        dgvMetadata.Columns.Add("colKey", Strings.MetaDataKey);
        dgvMetadata.Columns.Add("colValue", Strings.MetaDataValue);
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

        if (Station.TrackedObject.CustomIcon.UseCustom)
        {
            radUseCustomYes.Checked = true;
            radUseCustomNo.Checked = false;
            _cmbUiIcons.SelectionLength = 0;
            _cmbUiIcons.Enabled = false;
        }
        else
        {
            radUseCustomNo.Checked = true;
            radUseCustomYes.Checked = false;
            _cmbUiIcons.Enabled = true;
            _cmbUiIcons.SelectedIndex = _cmbUiIcons.Items.IndexOf(Station.TrackedObject.MetaData.Icon);
            _cmbUiIcons.SelectionLength = 0;
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

        var stationIcon = Station.TrackedObject.GetActiveIcon();
        if (stationIcon != null)
        {
            var imagePath = stationIcon.TrackedObject.ImagePath;
            if (Path.Exists(imagePath))
            {
                picStationIcon.SetImage(stationIcon.TrackedObject.ImagePath);
            }
            else
            {
                if (stationIcon.TrackedObject.IsFromArchive)
                    picStationIcon.SetImageFromBitmap(Resources.pending_extraction);
                else
                    picStationIcon.SetImage(null);
            }
        }

        UpdateCustomDataView();
    }

    /// <summary>
    /// Reset the UI values to the defaults for the station.
    /// </summary>
    public void ResetUi()
    {
        SetDisplayTabValues();
        SetMusicTabValues();
        _musicCtl.ResetUi();
    }

    /// <summary>
    /// Forces an update to the station's custom data grid view.
    /// </summary>
    public void UpdateCustomDataView()
    {
        dgvMetadata.Rows.Clear();
        foreach (var (key, value) in Station.TrackedObject.MetaData.CustomData)
            dgvMetadata.Rows.Add(key, value);

        //Find the indices of the custom data rows that pertain to the custom icon
        var indices = GetCustomIconDataRowIndices();

        //Set the custom data rows to be read-only
        SetReadOnlyDataViewRows(indices);
    }

    private void RemoveCustomIconDataRows()
    {
        try
        {
            var indices = GetCustomIconDataRowIndices();
            foreach (var index in indices)
            {
                var row = dgvMetadata.Rows[index];
                dgvMetadata.Rows.Remove(row);
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationEditor>("RemoveCustomIconDataRows").Error(ex,
                "Something went wrong while removing the custom icon data rows!");
        }
    }

    /// <summary>
    /// Get the array of indices in the data grid view pertaining to the icon's custom data. This data is always non-editable for the user.
    /// </summary>
    /// <returns>The array of indices or an empty array if an exception occured or no icon custom data.</returns>
    private int[] GetCustomIconDataRowIndices()
    {
        try
        {
            return dgvMetadata.Rows.Cast<DataGridViewRow>()
                .Select((row, index) => new { Row = row, Index = index })
                .Where(x => _customDataKeys.Contains(x.Row.Cells[0].Value?.ToString()))
                .Select(x => x.Index)
                .ToArray();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationEditor>("GetCustomIconDataRowIndices")
                .Error(ex, "Something went wrong looking for the custom icon data rows!");
        }

        return [];
    }

    /// <summary>
    /// Sets the custom data view rows to be readonly.
    /// </summary>
    /// <param name="rowIndices">The indices of the rows to set to read-only.</param>
    private void SetReadOnlyDataViewRows(params int[] rowIndices)
    {
        foreach (var rowIndex in rowIndices)
        {
            dgvMetadata.Rows[rowIndex].ReadOnly = true;
            dgvMetadata.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
        }
    }

    /// <summary>
    /// Sets the custom data view rows to be editable.
    /// </summary>
    /// <param name="rowIndices">The indices of the rows to set to editable.</param>
    private void SetEditableDataViewRows(params int[] rowIndices)
    {
        foreach (var rowIndex in rowIndices)
        {
            dgvMetadata.Rows[rowIndex].ReadOnly = false;
            dgvMetadata.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
        }
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

    private void RemoveCustomIconData()
    {
        Station.TrackedObject.RemoveCustomData(_customDataKeys[0]);
        Station.TrackedObject.RemoveCustomData(_customDataKeys[1]);
        Station.TrackedObject.RemoveCustomData(_customDataKeys[2]);
        Station.TrackedObject.RemoveCustomData(_customDataKeys[3]);
        UpdateCustomDataView();
    }

    private void AddCustomIconData(TrackableObject<WolvenIcon>? icon)
    {
        if (icon == null)
        {
            RemoveCustomIconData();
            return;
        }

        Station.TrackedObject.AddCustomData(_customDataKeys[0], icon.TrackedObject.IconName ?? string.Empty);
        Station.TrackedObject.AddCustomData(_customDataKeys[1], icon.TrackedObject.ImagePath ?? string.Empty);
        Station.TrackedObject.AddCustomData(_customDataKeys[2], icon.TrackedObject.ArchivePath ?? string.Empty);
        Station.TrackedObject.AddCustomData(_customDataKeys[3],
            icon.TrackedObject.Sha256HashOfArchiveFile ?? string.Empty);
        UpdateCustomDataView();
    }

    public void UpdateIcon(TrackableObject<WolvenIcon>? icon)
    {
        if (icon == null)
        {
            radUseCustomYes.Checked = false;
            radUseCustomNo.Checked = true;
        }
        else
        {
            radUseCustomYes.Checked = true;
            radUseCustomNo.Checked = false;
        }

        UpdateIconPrivate(icon);
    }

    private void UpdateIconPrivate(TrackableObject<WolvenIcon>? icon)
    {
        if (icon == null)
        {
            Station.TrackedObject.CustomIcon.UseCustom = false;
            Station.TrackedObject.CustomIcon = new CustomIcon();
            txtInkAtlasPath.Text = Station.TrackedObject.CustomIcon.InkAtlasPath;
            txtInkAtlasPart.Text = Station.TrackedObject.CustomIcon.InkAtlasPart;

            picStationIcon.SetImageFromBitmap(Resources.drag_and_drop);
            RemoveCustomIconData();
            Station.CheckPendingSaveStatus();
        }
        else
        {
            Station.TrackedObject.CustomIcon = new CustomIcon
            {
                UseCustom = true,
                InkAtlasPath = icon.TrackedObject.CustomIcon.InkAtlasPath,
                InkAtlasPart = icon.TrackedObject.CustomIcon.InkAtlasPart
            };

            txtInkAtlasPath.Text = Station.TrackedObject.CustomIcon.InkAtlasPath;
            txtInkAtlasPart.Text = Station.TrackedObject.CustomIcon.InkAtlasPart;

            picStationIcon.SetImage(Path.Exists(icon.TrackedObject.ImagePath)
                ? icon.TrackedObject.ImagePath
                : string.Empty);

            //Update custom data pertaining to the active icon
            AddCustomIconData(icon);

            StationUpdated?.Invoke(this, EventArgs.Empty);
        }
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
        _cmbUiIcons.SelectionLength = 0;

        try
        {
            Station.TrackedObject.Icons.ForEach(i => i.TrackedObject.IsActive = false);

            //Find the matching wolven icon in the station's list of icons
            var matchingIcon = Station.TrackedObject.Icons.FirstOrDefault(i =>
                i.TrackedObject.CustomIcon.InkAtlasPath.Equals(txtInkAtlasPath.Text));
            if (matchingIcon != null)
            {
                matchingIcon.TrackedObject.IsActive = true;
                UpdateIconPrivate(matchingIcon);
            }
            else
            {
                UpdateIconPrivate(null);
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationEditor>("UseCustom_YesChanged")
                .Error(ex, "An error occured while trying to set the active icon!");
        }

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
        picStationIcon.Visible = !radUseCustomNo.Checked;
        btnOpenIconManager.Visible = !radUseCustomNo.Checked;
        _cmbUiIcons.Enabled = radUseCustomNo.Checked;
        _cmbUiIcons.SelectionLength = 0;

        Station.TrackedObject.Icons.ForEach(i => i.TrackedObject.IsActive = false);
        UpdateIconPrivate(null);
    }

    private void PicStationIcon_DragDrop(object sender, DragEventArgs e)
    {
        ApplicationContext.MainFormInstance?.ShowIconManagerForm(Station, picStationIcon.ImagePath);
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

    private void txtInkAtlasPath_Leave(object sender, EventArgs e)
    {
        try
        {
            //Find the matching wolven icon in the station's list of icons
            var matchingIcon = Station.TrackedObject.Icons.FirstOrDefault(i =>
                i.TrackedObject.CustomIcon.InkAtlasPath.Equals(txtInkAtlasPath.Text));
            if (matchingIcon == null) return;

            matchingIcon.TrackedObject.IsActive = true;
            txtInkAtlasPart.Text = matchingIcon.TrackedObject.CustomIcon.InkAtlasPart;
            picStationIcon.SetImage(matchingIcon.TrackedObject.ImagePath);
            UpdateIcon(matchingIcon);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationEditor>("InkAtlasPath_Leave")
                .Error(ex, "An error occured while trying to set the active icon!");
        }
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

    private void btnOpenIconManager_Click(object sender, EventArgs e)
    {
        ApplicationContext.MainFormInstance?.ShowIconManagerForm(Station);
    }

    /// <summary>
    /// Occurs when the FM number is changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NudFM_ValueChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.MetaData.Fm =
            StationManager.Instance.EnsureDisplayNameFormat(Station, (float)nudFM.Value);
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
        if (key == null || value == null) return;

        Station.TrackedObject.AddCustomData(key, value);
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void dgvMetadata_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        var readOnlyIndices = GetCustomIconDataRowIndices();

        //Don't allow deleting custom icon data rows or null rows
        if (e.Row == null) return;

        if (readOnlyIndices.Contains(dgvMetadata.Rows.IndexOf(e.Row)))
            e.Cancel = true;
    }

    private void DgvMetadata_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        // Row deleted by the user
        foreach (DataGridViewCell cell in e.Row.Cells)
        {
            var key = cell.Value?.ToString();
            if (key == null || !Station.TrackedObject.ContainsCustomData(key)) continue;

            Station.TrackedObject.RemoveCustomData(key);
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

        Station.TrackedObject.AddCustomData(key, value);
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
        var radioGardenForm = new RadioGardenInput();
        radioGardenForm.UrlParsed += RadioGardenFormOnUrlParsed;
        radioGardenForm.ShowDialog();
        radioGardenForm.UrlParsed -= RadioGardenFormOnUrlParsed;
    }

    /// <summary>
    /// Retrieve the stream URL from the input box and validate it.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void RadioGardenFormOnUrlParsed(object? sender, string e)
    {
        var streamChecker = new AudioStreamChecker(TimeSpan.FromSeconds(5));

        var streamUrl = AudioStreamChecker.ConvertRadioGardenUrl(e);
        var isValid = await streamChecker.IsAudioStreamValidAsync(streamUrl);

        txtStreamURL.Text = isValid ? streamUrl : Strings.InvalidStream;
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
        lblStatus.Text = Strings.StationNameHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the icon label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the custom picture box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void picStationIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.CustomIconPictureHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the using custom icon label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblUsingCustomIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.CustomIconHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the open icon manager button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnOpenIconManager_Enter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconManagerHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the ink path label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblInkPath_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.InkPathHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the ink part label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblInkPart_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.InkPartHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the FM label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblFM_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.FMHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the volume label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblVolume_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.VolumeHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the volume value label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblVolumeVal_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.VolumeValHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the use stream label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblUseStream_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.UseStreamHelp;
    }

    /// <summary>
    /// Occurs when the mouse enters the stream URL label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LblStreamURL_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.StreamURLHelp;
    }

    private void btnGetFromRadioGarden_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.ParseFromRadioGardenHelp;
    }

    private void mpStreamPlayer_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.StreamPlayerHelp;
    }

    /// <summary>
    /// Set the status text to the given text.
    /// </summary>
    /// <param name="text">The text to set.</param>
    private void SetStatusText(string text)
    {
        lblStatus.Text = text;
    }

    /// <summary>
    /// Set the status text to the default text.
    /// </summary>
    private void ResetStatusText()
    {
        lblStatus.Text = Strings.Ready;
    }

    /// <summary>
    /// Occurs when the mouse leaves a label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Lbl_MouseLeave(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.Ready;
    }

    #endregion
}
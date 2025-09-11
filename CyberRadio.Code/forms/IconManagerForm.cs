// IconManagerForm.cs : RadioExt-Helper
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

using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;
using WIG.Lib.Models;

namespace RadioExt_Helper.forms;

public partial class IconManagerForm : Form
{
    private readonly bool _addedFromImagePath;

    //Need this to prevent constant tooltip flickering when the mouse is over the listbox.
    private readonly string _createdFromArchiveString = Strings.CreatedFromArchive;
    private readonly string _createdFromPngString = Strings.CreatedFromPng;
    private readonly TrackableObject<WolvenIcon>? _iconFromImagePath;
    private readonly ToolTip _iconToolTip = new();
    private readonly TrackableObject<AdditionalStation> _station;

    private readonly ImageList _stationImageList = new();
    private IconEditor? _currentEditor;
    private bool _ignoreSelectedIndexChanged;
    private bool _isExportingIcon;
    private bool _isImportingIcon;

    public IconManagerForm(TrackableObject<AdditionalStation> station)
    {
        InitializeComponent();

        _station = station;
    }

    public IconManagerForm(TrackableObject<AdditionalStation> station, string imagePath)
    {
        InitializeComponent();

        _station = station;
        _iconFromImagePath = new TrackableObject<WolvenIcon>(WolvenIcon.FromPath(imagePath));
        _addedFromImagePath = true;
    }

    public event EventHandler<TrackableObject<WolvenIcon>>? IconAdded;
    public event EventHandler<TrackableObject<WolvenIcon>>? IconUpdated;
    public event EventHandler<TrackableObject<WolvenIcon>?>? IconDeleted;

    private void IconManagerForm_Load(object sender, EventArgs e)
    {
        Translate();

        ResetListBox();
        SetImageList();

        if (_addedFromImagePath && _iconFromImagePath != null) AddNewIcon(_iconFromImagePath, true, false);
    }

    private void Translate()
    {
        Text = string.Format(Strings.IconManagerFormTitle, _station.TrackedObject.MetaData.DisplayName);

        btnAddIcon.Text = Strings.NewIcon;
        btnDeleteIcon.Text = Strings.DeleteIcon;
        btnDeleteAllIcons.Text = Strings.DeleteAllIcons;
        btnEnableIcon.Text = Strings.EnableSelected;
        btnDisableIcon.Text = Strings.DisableSelected;
        fromArchiveFileToolStripMenuItem.Text = Strings.FromArchiveFile;
        grpIcons.Text = Strings.Icons;
        fdlgOpenArchive.Title = Strings.OpenArchiveFile;
        fdlgOpenArchive.Filter = Strings.ArchiveFiles + @"|*.archive";
    }

    /// <summary>
    ///     Sets up the image list for the station list.
    /// </summary>
    private void SetImageList()
    {
        _stationImageList.Images.Add("disabled", Resources.disabled);
        _stationImageList.Images.Add("enabled", Resources.enabled);
        _stationImageList.Images.Add("fromPng", Resources.png_file_16x16);
        _stationImageList.Images.Add("fromArchive", Resources.box_16x16);
        _stationImageList.ImageSize = new Size(16, 16);
        lbIcons.ImageList = _stationImageList;
    }

    private void lbIcons_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SafeInvoke(() => SelectListBoxItem(lbIcons.SelectedIndex, true));
    }

    private void SelectListBoxItem(int index, bool userDriven)
    {
        if (index < 0 || index >= lbIcons.Items.Count) return;

        if (userDriven)
        {
            if (_ignoreSelectedIndexChanged)
            {
                _ignoreSelectedIndexChanged = false;
                return;
            }
        }
        else
        {
            lbIcons.SelectedIndex = index;
        }

        if (lbIcons.SelectedItem is not TrackableObject<WolvenIcon> icon) return;
        SelectIconEditor(icon);
    }

    /// <summary>
    /// Gets the correct icon editor from the station manager based on the station's ID and the icon selected.
    /// </summary>
    /// <param name="icon">The icon associated with the editor.</param>
    private void SelectIconEditor(TrackableObject<WolvenIcon>? icon)
    {
        if (icon == null)
        {
            UpdateIconEditor(null);
        }
        else
        {
            var editor = StationManager.Instance.GetStationIconEditor(_station.Id, icon.Id);
            editor?.SetLogIdentifier();
            UpdateIconEditor(editor);
        }
    }

    /// <summary>
    ///     Updates the currently displayed icon editor.
    ///     <param name="editor">The editor to display in the split panel.</param>
    /// </summary>
    private void UpdateIconEditor(IconEditor? editor)
    {
        try
        {
            if (_currentEditor == editor) return;

            //Remove the subscribed event if the current editor is not null.
            if (_currentEditor != null && editor != null)
            {
                _currentEditor.IconUpdated -= _currentEditor_IconUpdated;
                _currentEditor.IconImportStarted -= _currentEditor_IconImportStarted;
                _currentEditor.IconImportFinished -= _currentEditor_IconImportFinished;
                _currentEditor.IconExtractStarted -= _currentEditor_IconExtractStarted;
                _currentEditor.IconExtractFinished -= _currentEditor_IconExtractFinished;

                editor.IconUpdated -= _currentEditor_IconUpdated;
                editor.IconImportStarted -= _currentEditor_IconImportStarted;
                editor.IconImportFinished -= _currentEditor_IconImportFinished;
                editor.IconExtractStarted -= _currentEditor_IconExtractStarted;
                editor.IconExtractFinished -= _currentEditor_IconExtractFinished;
            }

            if (editor == null)
            {
                splitContainer1.Panel2.SuspendLayout();
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.ResumeLayout();
            }
            else
            {
                _currentEditor = editor;

                splitContainer1.Panel2.SuspendLayout();
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(_currentEditor);
                splitContainer1.Panel2.ResumeLayout();

                //Resubscribe to the event for the icon updating
                _currentEditor.IconUpdated += _currentEditor_IconUpdated;
                _currentEditor.IconImportStarted += _currentEditor_IconImportStarted;
                _currentEditor.IconImportFinished += _currentEditor_IconImportFinished;
                _currentEditor.IconExtractStarted += _currentEditor_IconExtractStarted;
                _currentEditor.IconExtractFinished += _currentEditor_IconExtractFinished;
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<IconManagerForm>("UpdateIconEditor")
                .Error(ex, "An error occurred while updating the icon editor.");
        }
    }

    private void _currentEditor_IconImportStarted(object? sender, EventArgs e)
    {
        _isImportingIcon = true;
        SetManagerReadOnly();
    }

    private void _currentEditor_IconImportFinished(object? sender, EventArgs e)
    {
        _isImportingIcon = false;
        SetManagerEditable();
    }

    private void _currentEditor_IconExtractStarted(object? sender, EventArgs e)
    {
        _isExportingIcon = true;
        SetManagerReadOnly();
    }

    private void _currentEditor_IconExtractFinished(object? sender, EventArgs e)
    {
        _isExportingIcon = false;
        SetManagerEditable();
    }

    private void SetManagerReadOnly()
    {
        this.SafeInvoke(() =>
        {
            lbIcons.Enabled = false;
            btnAddIcon.Enabled = false;
            btnDeleteIcon.Enabled = false;
            btnEnableIcon.Enabled = false;
            btnDisableIcon.Enabled = false;
        });
    }

    private void SetManagerEditable()
    {
        this.SafeInvoke(() =>
        {
            lbIcons.Enabled = true;
            btnAddIcon.Enabled = true;
            btnDeleteIcon.Enabled = true;
            btnEnableIcon.Enabled = true;
            btnDisableIcon.Enabled = true;
        });
    }

    private void _currentEditor_IconUpdated(object? sender, TrackableObject<WolvenIcon> icon)
    {
        IconUpdated?.Invoke(this, icon);
    }

    private void AddNewIcon(TrackableObject<WolvenIcon> icon, bool makeActive, bool isExistingArchive)
    {
        var added = StationManager.Instance.AddStationIcon(_station.Id, icon, makeActive, isExistingArchive);
        if (!added) return;

        ResetListBox();
        lbIcons.SelectedItem = icon;
        SelectIconEditor(icon);

        IconAdded?.Invoke(this, icon);
    }

    /// <summary>
    /// Ensures the icon that was copied or linked is added to the listbox and the editor is displayed in the control.
    /// </summary>
    /// <param name="icon">The icon that was copied or linked from another station.</param>
    private void AddNewIconFromCopy(TrackableObject<WolvenIcon> icon)
    {
        ResetListBox();
        lbIcons.SelectedItem = icon;
        SelectIconEditor(icon);

        IconAdded?.Invoke(this, icon);
    }

    private void RemoveIcon(TrackableObject<WolvenIcon> icon)
    {
        var firstResult = MessageBox.Show(Strings.IconManagerForm_RemoveIcon_Are_you_sure_you_want_to_delete_this_icon_,
            Strings.IconManagerForm_RemoveIcon_Confirm_Delete,
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (firstResult != DialogResult.Yes) return;

        var isLinkedToOtherStations =
            StationManager.Instance.IsIconLinkedToOtherStations(icon.TrackedObject.IconId, _station.Id);
        var isValidIcon =
            icon.TrackedObject
                .CheckIconValid(); //a valid icon would have files on disk; otherwise, no need to ask to delete non-existent files ;)

        if (isValidIcon)
        {
            if (!isLinkedToOtherStations)
            {
                var secondResult = MessageBox.Show(
                    Strings
                        .IconManagerForm_RemoveIcon_Do_you_want_to_delete_associated_icon_files_from_staging__This_will_delete_the_copied__archive_file_and_the_associated_PNG_,
                    Strings.IconManagerForm_RemoveIcon_Confirm_Delete, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (secondResult == DialogResult.Yes)
                    StationManager.Instance.RemoveStationIcon(_station.Id, icon, true);
                else
                    StationManager.Instance.RemoveStationIcon(_station.Id, icon);
            }
            else
            {
                StationManager.Instance.RemoveStationIcon(_station.Id, icon);
                AuLogger.GetCurrentLogger<IconManagerForm>("RemoveIcon").Warn(
                    $"An icon was removed from the station '{_station.TrackedObject.MetaData.DisplayName}' but no files were deleted; they are in use by another station.");
            }
        }
        else
        {
            StationManager.Instance.RemoveStationIcon(_station.Id,
                icon); //No files, no conflicts with other stations, and not a valid icon yet so simply remove the icon from the station.
        }

        ResetListBox();

        if (lbIcons.Items.Count > 0)
        {
            lbIcons.SelectedIndex = 0;
            SelectListBoxItem(0, false);
        }
        else
        {
            SelectIconEditor(null);
        }

        IconDeleted?.Invoke(this, icon);
    }

    private void ResetListBox()
    {
        lbIcons.BeginUpdate();
        lbIcons.DataSource = null;
        lbIcons.DataSource = _station.TrackedObject.Icons.ToBindingList();
        lbIcons.DisplayMember = "TrackedObject.AtlasName";

        // Set tooltips for each item when the mouse hovers over them.
        lbIcons.MouseMove += lbIcons_MouseMove;

        lbIcons.Invalidate();
        lbIcons.EndUpdate();
    }

    private void lbIcons_MouseMove(object? sender, MouseEventArgs e)
    {
        // Get the index of the item under the mouse cursor
        var index = lbIcons.IndexFromPoint(e.Location);

        // Ensure index is within bounds and valid
        if (index >= 0 && index < lbIcons.Items.Count)
        {
            if (lbIcons.Items[index] is not TrackableObject<WolvenIcon> icon) return;

            // Determine the tooltip text based on whether the icon is from an archive or PNG
            var tooltipText = icon.TrackedObject.IsFromArchive ? _createdFromArchiveString : _createdFromPngString;

            // Show the tooltip for the specific item
            _iconToolTip.SetToolTip(lbIcons, tooltipText);
        }
        else
        {
            // If the mouse is not over a valid item, clear the tooltip
            _iconToolTip.SetToolTip(lbIcons, string.Empty);
        }
    }

    private void fromArchiveFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (fdlgOpenArchive.ShowDialog() == DialogResult.OK)
            {
                //Check if the archive file already exists in the staging folder. If it does, display option to copy it.
                var result = CheckForCopy(fdlgOpenArchive.FileName);

                if (result.shouldBreak)
                    return;

                //If the icon was copied successfully, display the editor for it and don't add a duplicate icon!
                if (result is { performedCopy: true, icon: not null })
                {
                    AddNewIconFromCopy(result.icon);
                }
                else //otherwise, proceed with adding the template icon from an archive
                {
                    TrackableObject<WolvenIcon> icon = new(WolvenIcon.FromArchive(fdlgOpenArchive.FileName))
                    {
                        TrackedObject =
                        {
                            IsFromArchive = true
                        }
                    };

                    AddNewIcon(icon, false, true);
                }
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<IconManagerForm>("fromArchiveFile_Click")
                .Error(ex, "Failed to load icon from archive file.");
        }
    }

    /// <summary>
    /// Checks if the archive already exists in the staging's icons folder and whether it is already associated with one or more stations.
    /// If it exists, prompts the user to either reuse the icon or create a new copy.
    /// </summary>
    /// <param name="path">The path to the icon to check.</param>
    /// <returns>A reference to the newly copied or linked icon, whether the copy was performed, and whether the caller should break their function flow; or <c>null</c> if the user did not copy or link the icon or an error occurred.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private (TrackableObject<WolvenIcon>? icon, bool performedCopy, bool shouldBreak) CheckForCopy(string path)
    {
        Guid? newIconId = null;
        var fileName = Path.GetFileName(path);
        var stagingPath = GlobalData.ConfigManager.GetConfig()?.StagingPath;

        if (stagingPath == null)
            throw new InvalidOperationException("The staging path should already be set at this point!");

        var iconPath = Path.Combine(stagingPath, "icons", fileName);

        // Check if the archive already exists in the staging folder
        var existsInStaging = File.Exists(iconPath);

        // Check if the icon already exists in the current station
        var currentStationHasIcon = _station.TrackedObject.Icons
            .Any(icon => string.Equals(icon.TrackedObject.ArchivePath, iconPath, StringComparison.OrdinalIgnoreCase));

        if (currentStationHasIcon)
        {
            MessageBox.Show(
                Strings.IconManagerForm_CheckForCopy_This_icon_is_already_associated_with_the_current_station_,
                Strings.IconManagerForm_CheckForCopy_Duplicate_Icon_Detected,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return (null, false, true); // Cancel the import since the icon is already present
        }

        // Check if the archive is already associated with one or more other stations
        var existingStations = StationManager.Instance.StationsAsList
            .Where(station => station.TrackedObject.Icons
                .Any(icon =>
                    string.Equals(icon.TrackedObject.ArchivePath, iconPath, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (existsInStaging && existingStations.Count > 0 && !existingStations.Contains(_station))
        {
            // Inform the user that multiple associations were found
            var associatedStationNames = string.Join(", ",
                existingStations.Select(station => station.TrackedObject.MetaData.DisplayName));
            var dialogResult = MessageBox.Show(
                string.Format(Strings.IconManagerForm_CheckForCopy_, associatedStationNames),
                Strings.IconManagerForm_CheckForCopy_Duplicate_Icon_Detected,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            switch (dialogResult)
            {
                case DialogResult.OK:
                {
                    // Create a new copy of the existing icon for the current station
                    var existingIcon = existingStations
                        .SelectMany(station => station.TrackedObject.Icons)
                        .First(icon => string.Equals(icon.TrackedObject.ArchivePath, iconPath,
                            StringComparison.OrdinalIgnoreCase));

                    // Copy the .archive and .png files and update paths
                    newIconId = StationManager.Instance.CopyStationIcon(_station.Id, existingStations.First().Id,
                        existingIcon);

                    if (newIconId == null)
                        throw new Exception(
                            $"Failed to copy existing icon to the station: {_station.TrackedObject.MetaData.DisplayName}");
                    break;
                }
                case DialogResult.Cancel:
                default:
                    // Cancel the import
                    return (null, true, true);
            }
        }

        // If the icon was copied successfully, return the icon and true. Otherwise, return null and false.
        return newIconId != null
            ? (StationManager.Instance.GetStationIcon(_station.Id, (Guid)newIconId), true, false)
            : (null, false, false);
    }

    private void btnAddIcon_Click(object sender, EventArgs e)
    {
        var icon = new TrackableObject<WolvenIcon>(new WolvenIcon());
        AddNewIcon(icon, false, false);
    }

    private void btnDeleteIcon_Click(object sender, EventArgs e)
    {
        if (lbIcons.SelectedItem is not TrackableObject<WolvenIcon> icon) return;
        RemoveIcon(icon);
    }

    private void btnDeleteAllIcons_Click(object sender, EventArgs e)
    {
        var firstResult = MessageBox.Show(
            Strings
                .IconManagerForm_btnDeleteAllIcons_Click_Are_you_sure_you_want_to_remove_ALL_icons_from_this_station_,
            Strings.IconManagerForm_RemoveIcon_Confirm_Delete,
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (firstResult != DialogResult.Yes) return;

        var secondResult = MessageBox.Show(
            Strings
                .IconManagerForm_btnDeleteAllIcons_Click_Do_you_want_to_also_delete_associated_icon_s_files_from_staging,
            Strings.IconManagerForm_RemoveIcon_Confirm_Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        var isDeletingFiles = secondResult == DialogResult.Yes;

        StationManager.Instance.RemoveAllStationIcons(_station.Id, isDeletingFiles);
        IconDeleted?.Invoke(this, null);

        ResetListBox();

        if (lbIcons.Items.Count > 0)
        {
            lbIcons.SelectedIndex = 0;
            SelectListBoxItem(0, false);
        }
        else
        {
            SelectIconEditor(null);
        }
    }

    private void btnEnableIcon_Click(object sender, EventArgs e)
    {
        if (lbIcons.SelectedItem is not TrackableObject<WolvenIcon> icon) return;
        EnableIcon(icon);
    }

    private void btnDisableIcon_Click(object sender, EventArgs e)
    {
        if (lbIcons.SelectedItem is not TrackableObject<WolvenIcon> icon) return;
        DisableIcon(icon);
    }

    private void EnableIcon(TrackableObject<WolvenIcon> icon)
    {
        lbIcons.BeginUpdate();
        StationManager.Instance.EnableStationIcon(_station.Id, icon.Id);
        lbIcons.Invalidate();
        lbIcons.EndUpdate();

        IconUpdated?.Invoke(this, icon);
    }

    private void DisableIcon(TrackableObject<WolvenIcon> icon)
    {
        lbIcons.BeginUpdate();
        StationManager.Instance.DisableStationIcon(_station.Id, icon.Id);
        lbIcons.Invalidate();
        lbIcons.EndUpdate();

        IconUpdated?.Invoke(this, icon);
    }

    private void IconManagerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason is CloseReason.TaskManagerClosing or CloseReason.WindowsShutDown) return;

        if (_isImportingIcon)
        {
            MessageBox.Show(Strings.IconImportInProgress, Strings.IconImportInProgressCaption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            e.Cancel = true;
            return;
        }

        if (_isExportingIcon)
        {
            MessageBox.Show(Strings.IconExportInProgress, Strings.IconExportInProgressCaption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            e.Cancel = true;
            return;
        }

        if (_currentEditor != null)
        {
            _currentEditor.IconUpdated -= _currentEditor_IconUpdated;
            _currentEditor.IconImportStarted -= _currentEditor_IconImportStarted;
            _currentEditor.IconImportFinished -= _currentEditor_IconImportFinished;
            _currentEditor.IconExtractStarted -= _currentEditor_IconExtractStarted;
            _currentEditor.IconExtractFinished -= _currentEditor_IconExtractFinished;
        }

        if (_station.TrackedObject.GetActiveIcon() == null) return;
        if (_station.TrackedObject.CheckActiveIconValid()) return;

        MessageBox.Show(Strings.InvalidActiveIcon, Strings.InvalidActiveIconCaption, MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        e.Cancel = true;
    }
}
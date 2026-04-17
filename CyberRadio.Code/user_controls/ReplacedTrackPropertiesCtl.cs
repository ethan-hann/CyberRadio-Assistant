// ReplacedTrackPropertiesCtl.cs : RadioExt-Helper
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

using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using Org.BouncyCastle.Utilities;
using RadioExt_Helper.forms;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using WIG.Lib.Models.Audio;

namespace RadioExt_Helper.user_controls;

/// <summary>
/// A UserControl for displaying and editing properties of a replaced track associated with a replacement station.
/// </summary>
public partial class ReplacedTrackPropertiesCtl : UserControl, IEditor
{
    private readonly string _trackName;

    /// <summary>
    /// Event triggered when the track's properties are changed. Event data contains the vanilla track name.
    /// </summary>
    public EventHandler<string>? TrackChanged;

    /// <summary>
    /// Initializes a new instance of the ReplacedTrackPropertiesCtl class with the specified replacement station.
    /// </summary>
    /// <param name="station">A TrackableObject containing the replacement station to associate with this control. Cannot be null.</param>
    /// <param name="trackName">The name of the track this property window is editing.</param>
    public ReplacedTrackPropertiesCtl(TrackableObject<ReplacementStation> station, string trackName)
    {
        InitializeComponent();

        ReplacedStation = station;
        _trackName = trackName;
    }

    /// <inheritdoc />
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <inheritdoc />
    public EditorType Type { get; set; }

    /// <inheritdoc />
    public TrackableObject<AdditionalStation>? Station => null; // Not applicable for this control

    /// <inheritdoc />
    public TrackableObject<ReplacementStation>? ReplacedStation { get; } // The replacement station being edited

    /// <inheritdoc />
    public void Translate()
    {
        lblWEMIdHelp.Text = Strings.WEMIdHelp;
        lblWemIdDisclaimer.Text = Strings.WEMIdDisclaimer;
        grpReplacedTrack.Text = Strings.ReplacedWemIdsGroupBox;
        colWemId.Text = Strings.WemId;
        colReplacedFile.Text = Strings.ReplacedWithFile;

        btnReplace.Text = Strings.ReplaceWemId;
        btnRemove.Text = Strings.RemoveReplacedWemId;
        btnReplaceAll.Text = Strings.ReplaceAllWemIds;
        btnRemoveAll.Text = Strings.RemoveAllWemIds;

        copyWemIDToolStripMenuItem.Text = Strings.CopyWemIdContextMenu;
        openPathToReplacementFileToolStripMenuItem.Text = Strings.OpenPathToReplacementFileContextMenu;

        fdlgSelectFile.Title = Strings.AddReplacementTrackTitle;
        fdlgSelectFile.Filter =
            @"Audio Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma;*.wem";
    }

    private void ReplacedTrackPropertiesCtl_Load(object sender, EventArgs e)
    {
        fdlgSelectFile.Title = Strings.AddReplacementTrackTitle;
        fdlgSelectFile.Filter =
            @"Audio Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma;*.wem";

        PopulateListView();
    }

    private void PopulateListView()
    {
        if (ReplacedStation is null) return;

        lvTracks.SuspendLayout();
        lvTracks.Items.Clear();

        //Get list of wem ids from replaced station and vanilla station name
        var wemIds = ReplacedStation.TrackedObject.VanillaStation?.Tracks
            .FirstOrDefault(t => t.TrackName.Equals(_trackName))?.WemIds;

        if (wemIds is null)
        {
            AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>("PopulateListView")
                .Error(
                    $"No WEM IDs found for track '{_trackName}' in vanilla station associated with replacement station '{ReplacedStation.TrackedObject.DisplayName}'. This indicates that the Google sheet was not parsed correctly!");
            return;
        }

        //Each WEM ID should be treated as its own entry in the list view and display the replaced file for the WEM ID and track combo

        foreach (var lvItem in from wemId in wemIds
                               let replacedFile = ReplacedStation.TrackedObject.Tracks
                                   .FirstOrDefault(rt => rt.VanillaTrackName.Equals(_trackName) && rt.WemId.Equals(wemId))?
                                   .ReplacementFilePath ?? Strings.TrackNotReplacedYet
                               select new ListViewItem([
                                       wemId,
                         replacedFile
                                   ])
                               { Tag = wemId })
            lvTracks.Items.Add(lvItem);

        lvTracks.ResizeColumns();
        lvTracks.ResumeLayout();
    }

    private void btnReplace_Click(object sender, EventArgs e)
    {
        if (ReplacedStation is null) return;
        if (lvTracks.SelectedItems.Count <= 0) return;
        if (lvTracks.SelectedItems[0].Tag is not string selectedWemId) return;
        if (!TryGetReplacementFilePath(out var filePath)) return;

        if (!ReplacedStation.TrackedObject.ReplaceTrack(_trackName, selectedWemId, filePath)) return;

        PopulateListView();
        TrackChanged?.Invoke(this, _trackName);
    }

    private void btnReplaceAll_Click(object sender, EventArgs e)
    {
        if (ReplacedStation is null) return;
        if (!TryGetTrackWemIds(out var wemIds)) return;
        if (!TryGetReplacementFilePath(out var filePath)) return;

        if (wemIds == null)
        {
            AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>("btnReplaceAll_Click")
                .Error(
                    $"No WEM IDs found for track '{_trackName}' in vanilla station associated with replacement station '{ReplacedStation.TrackedObject.DisplayName}'. This indicates that the Google sheet was not parsed correctly!");
            return;
        }

        foreach (var wemId in wemIds)
        {
            ReplacedStation.TrackedObject.ReplaceTrack(_trackName, wemId, filePath);
        }

        PopulateListView();
        TrackChanged?.Invoke(this, _trackName);
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        if (ReplacedStation is null) return;

        if (lvTracks.SelectedItems.Count <= 0) return;
        if (lvTracks.SelectedItems[0].Tag is not string selectedWemId) return;
        if (!ReplacedStation.TrackedObject.RemoveReplacedTrack(
                _trackName,
                selectedWemId)) return;

        PopulateListView();
        TrackChanged?.Invoke(this, _trackName);
    }

    private void btnRemoveAll_Click(object sender, EventArgs e)
    {
        if (ReplacedStation is null) return;
        var wemIds = ReplacedStation.TrackedObject.VanillaStation?.Tracks
            .FirstOrDefault(t => t.TrackName.Equals(_trackName))?.WemIds;
        if (wemIds is null)
        {
            AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>("btnRemoveAll_Click")
                .Error(
                    $"No WEM IDs found for track '{_trackName}' in vanilla station associated with replacement station '{ReplacedStation.TrackedObject.DisplayName}'. This indicates that the Google sheet was not parsed correctly!");
            return;
        }

        foreach (var wemId in wemIds)
            ReplacedStation.TrackedObject.RemoveReplacedTrack(
                _trackName,
                wemId);

        PopulateListView();
        TrackChanged?.Invoke(this, _trackName);
    }

    private void lvTracks_DoubleClick(object sender, EventArgs e) => btnReplace.PerformClick();

    private bool TryGetTrackWemIds(out HashSet<string>? wemIds)
    {
        wemIds = [];

        if (ReplacedStation is null) return false;

        var ids = ReplacedStation.TrackedObject.VanillaStation?.Tracks
            .FirstOrDefault(t => t.TrackName.Equals(_trackName))?.WemIds;

        if (ids is null)
        {
            AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>(nameof(TryGetTrackWemIds))
                .Error(
                    $"No WEM IDs found for track '{_trackName}' in vanilla station associated with replacement station '{ReplacedStation.TrackedObject.DisplayName}'. This indicates that the Google sheet was not parsed correctly!");
            return false;
        }

        wemIds = ids;
        return true;
    }

    private bool TryGetReplacementFilePath(out string filePath)
    {
        filePath = string.Empty;

        if (fdlgSelectFile.ShowDialog() != DialogResult.OK) return false;

        filePath = fdlgSelectFile.FileName;
        return TryConvertToWavIfNeeded(ref filePath);
    }

    private bool TryConvertToWavIfNeeded(ref string filePath)
    {
        if (Path.GetExtension(filePath)?.ToLowerInvariant() == ".wav") return true;

        var title = Strings.ReplaceTrackConvertToWavTitle;
        var message = string.Format(Strings.ReplaceTrackConvertToWavMessage, Path.GetFileName(filePath));

        if (MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
            return false;

        try
        {
            var outputPath = AudioConverter.Instance.ConvertedDirectory
                ?? Directory.GetParent(filePath)?.FullName ?? Path.GetDirectoryName(filePath);

            if (outputPath == null)
            {
                AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>(nameof(TryConvertToWavIfNeeded))
                    .Error($"Could not determine the output path for audio conversion. Aborting...: {filePath}");
                ToastNotification.Show(this,
                    $"{Strings.ReplaceTrackConvertFailedTitle}: {string.Format(Strings.ReplaceTrackConvertFailedMessage, Path.GetFileName(filePath))}",
                    ToastType.Error);
                return false;
            }

            var convertCandidate = new ConvertCandidate(filePath, ValidAudioFiles.Wav, outputPath);
            AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>(nameof(TryConvertToWavIfNeeded))
                .Info($"Converting file '{filePath}' to WAV format for track replacement...");

            var convertedFilePath = Task.Run(() => AudioConverter.Instance.ConvertAsync(convertCandidate, true))
                .GetAwaiter()
                .GetResult();

            if (convertedFilePath is null)
            {
                AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>(nameof(TryConvertToWavIfNeeded))
                    .Error($"Audio conversion failed for file '{filePath}'. No output file was generated.");
                ToastNotification.Show(this,
                    $"{Strings.ReplaceTrackConvertFailedTitle}: {string.Format(Strings.ReplaceTrackConvertFailedMessage, Path.GetFileName(filePath))}",
                    ToastType.Error);
                return false;
            }

            AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>(nameof(TryConvertToWavIfNeeded))
                .Info($"Successfully converted file '{filePath}' to WAV format at '{convertedFilePath}'.");

            filePath = convertedFilePath;
            ToastNotification.Show(this, $"Converted to WAV: {Path.GetFileName(filePath)}", ToastType.Success);
            return true;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>(nameof(TryConvertToWavIfNeeded))
                .Error($"Error converting audio file '{filePath}' to WAV: {ex.Message}");
            ToastNotification.Show(this,
                $"{Strings.ReplaceTrackConvertFailedTitle}: {string.Format(Strings.ReplaceTrackConvertFailedMessage, Path.GetFileName(filePath))}",
                ToastType.Error);
            return false;
        }
    }

    private void copyWemIDToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lvTracks.SelectedItems.Count <= 0) return;
        var selectedTrack = lvTracks.SelectedItems[0];
        if (selectedTrack.Tag is not string selectedWemId) return;

        Clipboard.SetText(selectedWemId);
        ToastNotification.Show(this, $"{Strings.CopyWemIdContextMenu}: {selectedWemId}", ToastType.Success);
    }

    private void openPathToReplacementFileToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void lvTracks_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right) return;
        if (lvTracks.SelectedItems is [{ Tag: string }])
            cmsTracksRightClick.Show(Cursor.Position);
    }
}
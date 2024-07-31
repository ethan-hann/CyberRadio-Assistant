// StationManager.cs : RadioExt-Helper
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
using System.Globalization;
using System.Text.RegularExpressions;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using AetherUtils.Core.Structs;
using RadioExt_Helper.models;
using RadioExt_Helper.user_controls;

namespace RadioExt_Helper.utility;

/// <summary>
/// This class is responsible for managing the radio stations. It provides methods for adding and removing stations as well as their station editors.
/// Everything to do with stations is managed here.
/// <para>This class cannot be instantiated. It is a singleton.</para>
/// </summary>
public partial class StationManager : IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StationManager"/> class.
    /// </summary>
    private StationManager()
    {
    }

    /// <summary>
    /// Disposes of the station manager and clears all stations.
    /// </summary>
    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
        ClearStations();
    }

    /// <summary>
    /// Regular expression that matches a display name with an optional FM number at the start.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^\d+(\.\d+)?\s*")]
    private static partial Regex DisplayNameRegex();

    /// <summary>
    /// Loads stations from the specified directory into the manager, clearing any existing stations.
    /// </summary>
    /// <param name="directory">The directory to load stations from.</param>
    public void LoadStations(string directory)
    {
        try
        {
            ClearStations();
            foreach (var d in FileHelper.SafeEnumerateDirectories(directory))
                ProcessDirectory(d, false);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error loading stations from directory.");
        }
    }

    /// <summary>
    /// Loads a station from the specified directory into the manager.
    /// This method will not clear existing stations, unlike <see cref="LoadStations"/> 
    /// and is used to load a single station from a single directory, not necessarily from the staging directory.
    /// </summary>
    /// <param name="directory">The directory to load the station from.</param>
    /// <param name="treatAsNewStation">Indicates whether the station should be treated as a new station (i.e., it is not present in the staging directory already.</param>
    /// <returns>The <see cref="Guid"/> of the processed station or <c>null</c> if the directory couldn't be processed.</returns>
    public Guid? LoadStationFromDirectory(string? directory, bool treatAsNewStation)
    {
        try
        {
            if (string.IsNullOrEmpty(directory)) return null;
            if (!PathHelper.IsSubPath(GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty, directory))
                return ProcessDirectory(directory, treatAsNewStation);
            AuLogger.GetCurrentLogger<StationManager>()
                .Warn($"Attempted to load station from staging directory: {directory}");
            return null;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error loading station from directory.");
            return null;
        }
    }

    /// <summary>
    /// Adds a new station and editor to the manager.
    /// </summary>
    /// <param name="station">The station to add.</param>
    /// <param name="isOnDisk">Indicates whether the station is an in-memory addition or was added from .json files on disk.</param>
    /// <param name="pathOnDisk">Specifies the path to the station folder relative to the staging folder.</param>
    /// <returns>The <see cref="Guid"/> of the newly added station.</returns>
    public Guid AddStation(TrackableObject<Station> station, bool isOnDisk, string pathOnDisk = "\\")
    {
        try
        {
            lock (_stations)
            {
                CheckForDuplicateStation(station.Id);

                if (!isOnDisk)
                    _newStations.Add(station.Id);

                var editor = new StationEditor(station);
                _stations[station.Id] = new Pair<TrackableObject<Station>, StationEditor>(station, editor);
                editor.StationUpdated += Editor_StationUpdated;

                StationsAsBindingList.Add(station);

                if (pathOnDisk.Equals("\\"))
                {
                    StationPaths[station.Id] = Path.Combine(pathOnDisk, station.TrackedObject.MetaData.DisplayName);
                }
                else
                {
                    var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
                    var relativePath = PathHelper.GetRelativePath(pathOnDisk, stagingPath);

                    if (relativePath.Equals(pathOnDisk))
                        StationPaths[station.Id] = Path.Combine("\\", station.TrackedObject.MetaData.DisplayName);
                    else
                        StationPaths[station.Id] = relativePath;
                }

                StationAdded?.Invoke(this, station.Id);
                return station.Id;
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error adding station to manager.");
            return Guid.Empty;
        }
    }

    /// <summary>
    /// Add a new, blank station to the manager. The station will have the default metadata and no songs.
    /// The station will be added to the manager and the ID will be returned.
    /// </summary>
    /// <returns>The <see cref="Guid"/> of the newly added station.</returns>
    public Guid AddStation()
    {
        return AddBlankStation();
    }

    /// <summary>
    /// Removes a station, and it's editor from the manager.
    /// </summary>
    /// <param name="stationId">The station to remove, by ID.</param>
    public void RemoveStation(Guid stationId)
    {
        try
        {
            lock (_stations)
            {
                if (!_stations.TryGetValue(stationId, out var pair)) return;
                pair.Value.Dispose();
                _stations.Remove(stationId);
                _newStations.Remove(stationId);
                StationPaths.Remove(stationId);

                StationsAsBindingList.Remove(StationsAsBindingList.First(s => s.Id == stationId));

                StationRemoved?.Invoke(this, stationId);
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error removing station from manager.");
        }
    }

    /// <summary>
    /// Gets the station count string, formatted with the enabled station count and localized.
    /// </summary>
    /// <returns>A formatted string showing enabled and disabled station counts: <c>Enabled Stations: {0} / {1}</c></returns>
    public string GetStationCount()
    {
        var enabledCount = _stations.Select(pair => pair.Value.Key.TrackedObject.MetaData.IsActive)
            .Count(isActive => isActive);
        return string.Format(StationCountFormat, enabledCount, _stations.Count);
    }

    /// <summary>
    /// Clears all stations and their editors from the manager.
    /// </summary>
    public void ClearStations()
    {
        try
        {
            lock (_stations)
            {
                foreach (var pair in _stations.Values)
                    pair.Value.Dispose();
                _stations.Clear();
                StationsAsBindingList.Clear();
                StationPaths.Clear();
                _newStations.Clear();

                StationsCleared?.Invoke(this, EventArgs.Empty);
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error clearing stations from manager.");
        }
    }

    /// <summary>
    /// Get the station and editor for the specified station ID.
    /// </summary>
    /// <param name="stationId">The ID of the station to get.</param>
    /// <returns>A pair where the key is the <see cref="TrackableObject{T}"/> and the value is the <see cref="StationEditor"/>; 
    /// or <c>null</c> if the <paramref name="stationId"/> did not exist in the manager.</returns>
    public Pair<TrackableObject<Station>, StationEditor>? GetStation(Guid? stationId)
    {
        try
        {
            if (stationId == null) return null;
            var id = (Guid)stationId;
            lock (_stations)
            {
                return _stations.TryGetValue(id, out var pair) ? pair : null;
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error getting station from manager.");
            return null;
        }
    }

    /// <summary>
    /// Stops all music players for all stations in the manager.
    /// </summary>
    public void StopAllMusicPlayers()
    {
        try
        {
            foreach (var editor in _stations.Values.Select(pair => pair.Value))
                editor.GetMusicPlayer().StopStream();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error stopping all music players.");
        }
    }

    /// <summary>
    /// Set the station's active status to the new status and raise the <see cref="StationUpdated"/> event.
    /// </summary>
    /// <param name="stationId">The ID of the station to change the status of.</param>
    /// <param name="newStatus">The new status of the station: <c>true</c> = enabled; <c>false</c> = disabled.</param>
    public void ChangeStationStatus(Guid stationId, bool newStatus)
    {
        try
        {
            if (!_stations.TryGetValue(stationId, out var pair)) return;

            pair.Key.TrackedObject.MetaData.IsActive = newStatus;
            StationsAsBindingList.First(s => s.Id == stationId).TrackedObject.MetaData.IsActive = newStatus;

            StationUpdated?.Invoke(this, stationId);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error changing station status.");
        }
    }

    /// <summary>
    /// Check the pending save status of the station with the specified ID.
    /// </summary>
    /// <param name="stationId">The ID of the station to check.</param>
    /// <returns><c>true</c> if there are pending changes; <c>false</c> otherwise.</returns>
    public bool CheckStatus(Guid stationId)
    {
        try
        {
            return _stations[stationId].Key.CheckPendingSaveStatus() &
                   StationsAsBindingList.First(s => s.Id == stationId).CheckPendingSaveStatus();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking station status.");
            return false;
        }
    }

    /// <summary>
    /// Occurs when a station is updated.
    /// </summary>
    /// <param name="stationId">The <see cref="Guid"/> of the station that was updated.</param>
    public void OnStationUpdated(Guid stationId)
    {
        var newName = CheckForDuplicateStation(stationId);
        CheckStatus(stationId);

        _stations[stationId].Value.UpdateStationName(newName);
    }

    /// <summary>
    /// Checks for duplicate station names and gets an updated name if a duplicate is found.
    /// </summary>
    /// <param name="stationId">The ID of the station to check.</param>
    /// <returns>The updated station name.</returns>
    public string CheckForDuplicateStation(Guid stationId)
    {
        if (!_stations.TryGetValue(stationId, out var pair)) return string.Empty;

        var station = pair.Key.TrackedObject;
        var originalName = station.MetaData.DisplayName;
        var updatedName = originalName;
        var duplicateCount = 0;

        foreach (var existingStation in _stations.Values.Select(p => p.Key))
        {
            if (existingStation.Id == stationId) continue;

            if (!existingStation.TrackedObject.MetaData.DisplayName.Equals(updatedName,
                    StringComparison.OrdinalIgnoreCase)) continue;

            duplicateCount++;
            updatedName = $"{originalName} ({duplicateCount})";
        }

        if (duplicateCount <= 0) return updatedName;

        station.MetaData.DisplayName = updatedName;
        StationsAsBindingList.First(s => s.Id == stationId).TrackedObject.MetaData.DisplayName = updatedName;

        StationUpdated?.Invoke(this, stationId);
        StationNameDuplicate?.Invoke(this, (stationId, updatedName));

        return updatedName;
    }

    /// <summary>
    /// Translates all station editors in the manager to the current language.
    /// </summary>
    public void TranslateEditors()
    {
        try
        {
            foreach (var editor in _stations.Values.Select(pair => pair.Value))
                editor.Translate();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error translating station editors.");
        }
    }

    /// <summary>
    /// Get a dictionary of stations with a pair indicating if the station has missing songs and the count of missing songs.
    /// </summary>
    /// <returns>A dictionary where the key is the station's ID and the value is a pair containing whether the station is missing songs and the count of missing songs.</returns>
    public Dictionary<Guid, Pair<bool, int>> CheckForMissingSongs()
    {
        try
        {
            var missingSongs = new Dictionary<Guid, Pair<bool, int>>();
            foreach (var pair in _stations)
            {
                var station = pair.Value.Key.TrackedObject;
                var missingCount = station.Songs.Count(song => !FileHelper.DoesFileExist(song.FilePath, false));
                missingSongs[pair.Key] = new Pair<bool, int>(missingCount > 0, missingCount);
            }

            return missingSongs;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking for missing songs.");
            return [];
        }
    }

    /// <summary>
    /// Get a dictionary of station ids with a value indicating if the station has pending saves.
    /// </summary>
    /// <returns>A dictionary where the key is the station's ID and the value indicates if the station is pending save.</returns>
    public Dictionary<Guid, bool> CheckPendingSave()
    {
        try
        {
            var pendingSave = new Dictionary<Guid, bool>();
            foreach (var pair in _stations)
                pendingSave[pair.Key] = pair.Value.Key.IsPendingSave
                                        & StationsAsBindingList.First(s => s.Id == pair.Key).IsPendingSave;
            return pendingSave;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking for pending saves.");
            return [];
        }
    }

    /// <summary>
    /// Clear the list of new stations added during the current session.
    /// </summary>
    public void ResetNewStations()
    {
        _newStations.Clear();
    }

    /// <summary>
    /// Get a value indicating if the station with the specified ID is a new station added during the current session.
    /// </summary>
    /// <param name="stationId">The ID of the station to check.</param>
    /// <returns><c>true</c> if the station is new; <c>false</c> otherwise.</returns>
    public bool IsNewStation(Guid stationId)
    {
        return _newStations.Contains(stationId);
    }

    /// <summary>
    /// Get the count of stations that exist in the game's radios folder but do not exist in the staging folder.
    /// </summary>
    /// <param name="stagingPath">The path to the staging directory.</param>
    /// <param name="gameBasePath">The path to the game's base path where the <c>radios</c> directory is derived from.</param>
    /// <returns>A non-zero integer if there are station's in the game's radios folder that are not in the staging folder; <c>0</c> otherwise.</returns>
    public int CheckGameForExistingStations(string stagingPath, string gameBasePath)
    {
        if (string.IsNullOrEmpty(stagingPath) || string.IsNullOrEmpty(gameBasePath)) return 0;

        try
        {
            var radiosDir = PathHelper.GetRadiosPath(gameBasePath);
            if (!Directory.Exists(radiosDir)) return 0;

            var gameDirectories = FileHelper.SafeEnumerateDirectories(radiosDir);
            var stagingDirectories =
                FileHelper.SafeEnumerateDirectories(stagingPath).Select(Path.GetFileName).ToHashSet();

            var count = 0;

            foreach (var gameDir in gameDirectories)
            {
                var dirName = Path.GetFileName(gameDir);
                if (!stagingDirectories.Contains(dirName))
                    count++;
            }

            return count;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking for existing stations.");
            return 0;
        }
    }

    /// <summary>
    /// Sync stations from the game's radios folder to the staging folder.
    /// This will copy all stations and songs from the game's radios folder to the staging folder 
    /// if they do not exist and update them if they are newer.
    /// </summary>
    /// <param name="stagingPath">The path to the staging directory.</param>
    /// <param name="gameBasePath">The path to the game's base path where the <c>radios</c> directory is derived from.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SynchronizeStationsAsync(string stagingPath, string gameBasePath)
    {
        if (string.IsNullOrEmpty(stagingPath) || string.IsNullOrEmpty(gameBasePath)) return;

        try
        {
            var radiosDir = PathHelper.GetRadiosPath(gameBasePath);
            if (!Directory.Exists(radiosDir)) return;

            var stagingDirectories = FileHelper.SafeEnumerateDirectories(stagingPath, "*", SearchOption.AllDirectories)
                .ToList();
            var gameDirectories = FileHelper.SafeEnumerateDirectories(radiosDir, "*", SearchOption.AllDirectories)
                .ToList();

            var tasks = new List<Task>();

            // Synchronize directories
            foreach (var gameDir in gameDirectories)
            {
                var dirName = Path.GetFileName(gameDir);
                var stagingDir = Path.Combine(stagingPath, dirName);

                if (!stagingDirectories.Contains(stagingDir))
                    // Directory does not exist in the staging path, so copy it entirely
                    tasks.Add(Task.Run(() => CopyDirectoryAsync(gameDir, stagingDir)));
                else
                    // Directory exists, synchronize files
                    tasks.Add(Task.Run(() => SynchronizeFilesAsync(gameDir, stagingDir)));
            }

            await Task.WhenAll(tasks);
            SyncStatusChanged?.Invoke(GlobalData.Strings.GetString("SyncStatusComplete") ??
                                      "Synchronization complete.");
            StationsSynchronized?.Invoke(true);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error synchronizing stations.");
            SyncStatusChanged?.Invoke(
                GlobalData.Strings.GetString("SyncStatusError") ?? "Error synchronizing stations.");
            StationsSynchronized?.Invoke(false);
        }
    }

    /// <summary>
    /// Get the relative path in the staging folder to the station with the specified ID.
    /// </summary>
    /// <param name="stationId">The ID of the station to get the relative path of.</param>
    /// <returns>The relative path to the station's folder; or <see cref="string.Empty"/> if the station ID was invalid.</returns>
    public string GetStationPath(Guid? stationId)
    {
        if (stationId == null) return string.Empty;
        return StationPaths.TryGetValue((Guid)stationId, out var path) ? path : string.Empty;
    }

    private async Task SynchronizeFilesAsync(string sourceDir, string targetDir)
    {
        var sourceFiles = FileHelper.SafeEnumerateFiles(sourceDir);
        var targetFiles = FileHelper.SafeEnumerateFiles(targetDir).ToDictionary(Path.GetFileName, f => f);

        var fileTasks = new List<Task>();
        var fileCount = 0;

        foreach (var sourceFile in sourceFiles)
        {
            var fileName = Path.GetFileName(sourceFile);
            var targetFilePath = Path.Combine(targetDir, fileName);

            if (!targetFiles.ContainsKey(fileName) ||
                File.GetLastWriteTime(sourceFile) > File.GetLastWriteTime(targetFilePath))
            {
                fileTasks.Add(Task.Run(() => File.Copy(sourceFile, targetFilePath, true)));
                fileCount++;
            }

            var progress = (int)((float)fileCount / sourceFiles.Count() * 100);
            SyncProgressChanged?.Invoke(progress);
            SyncStatusChanged?.Invoke(string.Format(
                GlobalData.Strings.GetString("SyncProgressChanged") ?? "Synchronizing Files... {0}%", progress));
        }

        await Task.WhenAll(fileTasks);

        var sourceDirectories = FileHelper.SafeEnumerateDirectories(sourceDir);
        var targetDirectories = FileHelper.SafeEnumerateDirectories(targetDir).ToDictionary(Path.GetFileName, d => d);

        var dirTasks = new List<Task>();
        var dirCount = 0;

        foreach (var sourceSubDir in sourceDirectories)
        {
            var dirName = Path.GetFileName(sourceSubDir);
            var targetSubDir = Path.Combine(targetDir, dirName);

            if (!targetDirectories.ContainsKey(dirName))
            {
                // Directory does not exist in the target path, so copy it entirely
                FileHelper.CreateDirectories(targetSubDir);
                dirTasks.Add(Task.Run(() => CopyDirectoryAsync(sourceSubDir, targetSubDir)));
            }
            else
            {
                // Directory exists, synchronize files recursively
                dirTasks.Add(Task.Run(() => SynchronizeFilesAsync(sourceSubDir, targetSubDir)));
            }

            dirCount++;

            var progress = (int)((float)dirCount / sourceDirectories.Count() * 100);
            SyncProgressChanged?.Invoke(progress);
            SyncStatusChanged?.Invoke(string.Format(
                GlobalData.Strings.GetString("SyncProgressChanged") ?? "Synchronizing Directories... {0}%", progress));
        }

        await Task.WhenAll(dirTasks);
    }

    private async Task CopyDirectoryAsync(string sourceDir, string targetDir)
    {
        var files = FileHelper.SafeEnumerateFiles(sourceDir);
        var directories = FileHelper.SafeEnumerateDirectories(sourceDir);

        FileHelper.CreateDirectories(targetDir);

        var copyTasks = new List<Task>();

        foreach (var file in files)
        {
            var targetFilePath = Path.Combine(targetDir, Path.GetFileName(file));
            copyTasks.Add(Task.Run(() =>
            {
                FileHelper.CreateDirectories(targetFilePath);
                File.Copy(file, targetFilePath, true);
            }));
        }

        foreach (var directory in directories)
        {
            var targetDirectoryPath = Path.Combine(targetDir, Path.GetFileName(directory));
            copyTasks.Add(Task.Run(() => CopyDirectoryAsync(directory, targetDirectoryPath)));
        }

        await Task.WhenAll(copyTasks);
    }

    private void Editor_StationUpdated(object? sender, EventArgs e)
    {
        if (sender is StationEditor editor)
            StationUpdated?.Invoke(this, editor.Station.Id);
    }

    /// <summary>
    /// Add a new, blank station to the manager. The station will have the default metadata and no songs.
    /// The station will be added to the manager and the ID will be returned.
    /// </summary>
    /// <returns>The <see cref="Guid"/> of the newly added station.</returns>
    private Guid AddBlankStation()
    {
        var station = new Station
        {
            MetaData =
            {
                DisplayName = $"{GlobalData.Strings.GetString("NewStationListBoxEntry")}"
            }
        };
        var trackedStation = new TrackableObject<Station>(station);
        return AddStation(trackedStation, false);
    }

    /// <summary>
    /// Processes a directory by loading the metadata and songs from the files in the directory and adding them to the manager.
    /// </summary>
    /// <param name="directory">The directory to process.</param>
    /// <param name="treatAsNewStation">Indicates if this directory should be treated as a new station (i.e., not in the staging directory already) or not.</param>
    /// <returns>The newly processed Station ID; or <c>null</c> if the directory couldn't be processed.</returns>
    private Guid? ProcessDirectory(string directory, bool treatAsNewStation)
    {
        try
        {
            var files = FileHelper.SafeEnumerateFiles(directory).ToList();
            var metadata = files.Where(file => file.EndsWith("metadata.json")).Select(_metaDataJson.LoadJson)
                .FirstOrDefault();
            var songList = files.Where(file => file.EndsWith("songs.sgls")).Select(_songListJson.LoadJson)
                .FirstOrDefault() ?? [];
            var songFiles = files.Where(file => ValidAudioExtensions.Contains(Path.GetExtension(file).ToLower()))
                .ToList();

            if (metadata == null) return null;

            if (songList.Count == 0)
                songFiles.ForEach(path =>
                {
                    var song = Song.FromFile(path);
                    if (song != null)
                        songList.Add(song);
                });

            var station = new Station { MetaData = metadata, Songs = songList };
            var trackedStation = new TrackableObject<Station>(station);
            EnsureDisplayNameFormat(trackedStation);

            return AddStation(trackedStation, !treatAsNewStation, directory);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error processing directory.");
            return null;
        }
    }

    /// <summary>
    /// Ensure's the display name contains the station's FM number at the beginning of its name like so:
    /// <c>00.00 Station Name</c>
    /// </summary>
    /// <param name="station">The station to ensure the display name of.</param>
    /// <param name="optionalFMVal">The FM number to ensure is in front of the station name. If null, the default FM value will be used instead.</param>
    /// <returns>The parsed FM number from the original display name string; or, the original FM number if the same.</returns>
    public float EnsureDisplayNameFormat(TrackableObject<Station> station, float? optionalFMVal = null)
    {
        var currentName = station.TrackedObject.MetaData.DisplayName;

        var regex = DisplayNameRegex();
        var match = regex.Match(currentName);
        var fmNumber = optionalFMVal ?? station.TrackedObject.MetaData.Fm;

        if (match.Success)
        {
            currentName = currentName[match.Length..].TrimStart();
            if (float.TryParse(match.Value, CultureInfo.InvariantCulture, out var fmNumberParsed) &
                (optionalFMVal == null))
                fmNumber = fmNumberParsed;
        }

        station.TrackedObject.MetaData.DisplayName = @$"{fmNumber} {currentName}";
        station.TrackedObject.MetaData.Fm = fmNumber;
        return fmNumber;
    }

    #region Events

    /// <summary>
    /// Event that is raised when a station is added to the manager. Event data is the station ID.
    /// </summary>
    public event EventHandler<Guid>? StationAdded;

    /// <summary>
    /// Event that is raised when a station is removed from the manager. Event data is the station ID.
    /// </summary>
    public event EventHandler<Guid>? StationRemoved;

    /// <summary>
    /// Event that is raised when a station is updated in the manager. Event data is the station ID.
    /// </summary>
    public event EventHandler<Guid>? StationUpdated;

    /// <summary>
    /// Event that is raised when all stations are cleared from the manager.
    /// </summary>
    public event EventHandler? StationsCleared;

    /// <summary>
    /// Event that is raised when a station name is found to be a duplicate. Event data is a tuple with the station ID and the updated name.
    /// </summary>
    public event EventHandler<(Guid stationId, string updatedName)>? StationNameDuplicate;

    /// <summary>
    /// Event that is raised when the synchronization progress changes. Event data is the current progress percentage.
    /// </summary>
    public event Action<int>? SyncProgressChanged;

    /// <summary>
    /// Event that is raised when the synchronization status changes. Event data is a message describing the status.
    /// </summary>
    public event Action<string>? SyncStatusChanged;

    /// <summary>
    /// Event that is raised when stations have finished synchronizing from the game's radios folder to staging.
    /// Event data is a flag indicating success.
    /// </summary>
    public event Action<bool>? StationsSynchronized;

    #endregion

    #region Static Members

    /// <summary>
    /// The singleton instance of the <see cref="StationManager"/> class.
    /// </summary>
    private static StationManager? _instance;

    /// <summary>
    /// The lock object used to synchronize access to the singleton instance.
    /// </summary>
    private static readonly object Lock = new();

    /// <summary>
    /// The format string for the station count.
    /// </summary>
    private static string StationCountFormat =>
        GlobalData.Strings.GetString("EnabledStationsCount") ?? "Enabled Stations: {0} / {1}";

    #endregion

    #region Private, Readonly Members

    /// <summary>
    /// The dictionary of stations and editors managed by the manager. Each station has a unique ID which is used as the key.
    /// <para>Key: <c>Unique station ID</c></para>
    /// <para>Value: Pair with following:</para>
    ///     <list type="bullet">
    ///         <item>Key: <c>Trackable station</c></item>
    ///         <item>Value: <c>Station's editor control</c></item>
    ///     </list>
    /// </summary>
    private readonly Dictionary<Guid, Pair<TrackableObject<Station>, StationEditor>> _stations = [];

    /// <summary>
    /// The JSON object used to serialize and deserialize the metadata of a station.
    /// </summary>
    private readonly Json<MetaData> _metaDataJson = new();

    /// <summary>
    /// The JSON object used to serialize and deserialize the list of songs.
    /// </summary>
    private readonly Json<List<Song>> _songListJson = new();

    /// <summary>
    /// List of new station IDs added to the manager during the current session. These stations should not be allowed to revert changes unless saved to disk.
    /// Upon exporting, the station IDs in this list should be removed by <see cref="ResetNewStations"/>
    /// </summary>
    private readonly List<Guid> _newStations = [];

    #endregion

    #region Properties

    /// <summary>
    /// Get the singleton instance of the <see cref="StationManager"/> class.
    /// </summary>
    public static StationManager Instance
    {
        get
        {
            lock (Lock)
            {
                return _instance ??= new StationManager();
            }
        }
    }

    /// <summary>
    /// Get a list of valid audio file extensions for song files.
    /// </summary>
    public string?[] ValidAudioExtensions { get; } = EnumHelper<ValidAudioFiles>.GetEnumDescriptions() as string[] ??
                                                     EnumHelper<ValidAudioFiles>.GetEnumDescriptions().ToArray();

    /// <summary>
    /// The current list of stations managed by the manager as a binding list. Auto-updates when stations are added or removed.
    /// </summary>
    public BindingList<TrackableObject<Station>> StationsAsBindingList { get; } =
        [];

    /// <summary>
    /// The current list of stations managed by the manager as a list.
    /// </summary>
    public List<TrackableObject<Station>> StationsAsList => _stations.Values.Select(pair => pair.Key).ToList();

    /// <summary>
    /// A dictionary of station IDs and their relative paths in the staging directory.
    /// </summary>
    private Dictionary<Guid, string> StationPaths { get; } = [];

    /// <summary>
    /// Get a value indicating whether the station manager is empty (i.e., has no stations).
    /// </summary>
    public bool IsEmpty => _stations.Count == 0;

    #endregion
}
// StationManager.cs : RadioExt-Helper
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
using System.Globalization;
using System.Text.RegularExpressions;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using AetherUtils.Core.Structs;
using RadioExt_Helper.models;
using RadioExt_Helper.user_controls;
using SharpCompress.Archives;
using WIG.Lib.Models;

#endregion

namespace RadioExt_Helper.utility;

/// <summary>
///     This class is responsible for managing the radio stations. It provides methods for adding and removing stations as
///     well as their various editors and icons.
///     Everything to do with stations is managed here.
///     <para>This class cannot be instantiated. It is a singleton.</para>
/// </summary>
public partial class StationManager : IDisposable
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StationManager" /> class.
    /// </summary>
    private StationManager()
    {
    }

    /// <summary>
    ///     Disposes of the station manager and clears all stations.
    /// </summary>
    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
        ClearStations();
    }

    /// <summary>
    ///     Loads stations from the specified directory into the manager, clearing any existing stations.
    /// </summary>
    /// <param name="directory">The directory to load stations from.</param>
    public void LoadStations(string directory)
    {
        try
        {
            ClearStations();
            foreach (var d in FileHelper.SafeEnumerateDirectories(directory))
            {
                // Check if the `.vanilla` file is present in the directory indicating it's a vanilla replacement station
                var vanillaFilePath = Path.Combine(d, ".vanilla");
                if (FileHelper.DoesFileExist(vanillaFilePath))
                    ProcessVanillaDirectory(d, true);
                else
                    ProcessDirectory(d, d, false);
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error loading stations from directory.");
        }
    }

    /// <summary>
    ///     Loads a station from the specified directory into the manager.
    ///     This method will not clear existing stations, unlike <see cref="LoadStations" />
    ///     and is used to load a single station from a single directory, not from the staging directory.
    /// </summary>
    /// <param name="directory">The directory to load the station from.</param>
    /// <param name="songDirectory">The song directory to load the station's songs from.</param>
    /// <param name="treatAsNewStation">
    ///     Indicates whether the station should be treated as a new station (i.e., it is not
    ///     present in the staging directory already.
    /// </param>
    /// <returns>The <see cref="Guid" /> of the processed station or <c>null</c> if the directory couldn't be processed.</returns>
    public Guid? LoadStationFromDirectory(string? directory, string? songDirectory, bool treatAsNewStation)
    {
        try
        {
            if (string.IsNullOrEmpty(directory)) return null;
            if (string.IsNullOrEmpty(songDirectory)) return null;

            if (!PathHelper.IsSubPath(GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty, directory))
            {
                // Check if the `.vanilla` file is present in the directory indicating it's a vanilla replacement station
                var vanillaFilePath = Path.Combine(directory, ".vanilla");
                return FileHelper.DoesFileExist(vanillaFilePath)
                    ? ProcessVanillaDirectory(directory, treatAsNewStation)
                    : ProcessDirectory(directory, songDirectory, treatAsNewStation);
            }

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
    ///     Adds a new station and editor to the manager.
    /// </summary>
    /// <param name="station">The station to add.</param>
    /// <param name="isOnDisk">Indicates whether the station is an in-memory addition or was added from .json files on disk.</param>
    /// <returns>The <see cref="Guid" /> of the newly added station.</returns>
    public Guid AddStation(TrackableObject<AdditionalStation> station, bool isOnDisk)
    {
        return AddStation(station, isOnDisk, "\\");
    }

    /// <summary>
    ///     Adds a new replacement station and editor to the manager.
    /// </summary>
    /// <param name="station">The replacement station to add.</param>
    /// <param name="isOnDisk">Indicates whether the station is an in-memory addition or was added from .json files on disk.</param>
    /// <returns>The <see cref="Guid" /> of the newly added replacement station.</returns>
    public Guid AddVanillaStation(TrackableObject<ReplacementStation> station, bool isOnDisk)
    {
        return AddVanillaStation(station, isOnDisk, "\\");
    }

    /// <summary>
    ///     Adds a new station and editor to the manager. Optionally, specify the path to the station folder relative to the
    ///     staging folder.
    /// </summary>
    /// <param name="station">The station to add.</param>
    /// <param name="isOnDisk">Indicates whether the station is an in-memory addition or was added from .json files on disk.</param>
    /// <param name="pathOnDisk">Specifies the path to the station folder relative to the staging folder.</param>
    /// <returns>The <see cref="Guid" /> of the newly added station.</returns>
    public Guid AddStation(TrackableObject<AdditionalStation> station, bool isOnDisk, string pathOnDisk)
    {
        try
        {
            lock (_stations)
            {
                CheckForDuplicateStation(station.Id);

                if (!isOnDisk)
                    _newStations.Add(station.Id);

                StationEditor editor = new(station);
                _stations[station.Id] = new Pair<TrackableObject<AdditionalStation>, List<IEditor>>(station, [editor]);
                editor.StationUpdated += Editor_StationUpdated;

                StationsAsBindingList.Add(station);

                var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

                if (pathOnDisk.Equals("\\"))
                {
                    StationPaths[station.Id] = Path.Combine(pathOnDisk, station.TrackedObject.MetaData.DisplayName);
                }
                else
                {
                    var relativePath = PathHelper.GetRelativePath(pathOnDisk, stagingPath);

                    if (relativePath.Equals(pathOnDisk))
                        StationPaths[station.Id] = Path.Combine("\\", station.TrackedObject.MetaData.DisplayName);
                    else
                        StationPaths[station.Id] = relativePath;
                }

                //Make sure the .archive file(s) are copied to the staging directory icons folder and set the icon's archive path.
                foreach (var icon in station.TrackedObject.Icons)
                {
                    if (icon.TrackedObject.ArchivePath == null) continue; //if no archive path, skip
                    if (PathHelper.IsSubPath(stagingPath,
                            icon.TrackedObject
                                .ArchivePath)) //if the archive path is already in the staging directory, skip
                        continue;

                    //Otherwise, copy the archive file to the staging directory's icons folder and set the icon's archive path.
                    if (icon.TrackedObject.ArchivePath != null &&
                        FileHelper.DoesFileExist(icon.TrackedObject.ArchivePath))
                    {
                        var filename = Path.GetFileName(icon.TrackedObject.ArchivePath);
                        var iconArchivePath = Path.Combine(stagingPath, "icons", filename);
                        File.Copy(icon.TrackedObject.ArchivePath, iconArchivePath, true);
                        icon.TrackedObject.ArchivePath = iconArchivePath;
                        icon.AcceptChanges();
                    }
                }

                //Add custom icon data to station if the active icon is not null
                var activeIcon = station.TrackedObject.GetActiveIcon();
                if (activeIcon != null)
                {
                    station.TrackedObject.AddCustomData(_customDataKeys[0],
                        activeIcon.TrackedObject.IconName ?? string.Empty);
                    station.TrackedObject.AddCustomData(_customDataKeys[1],
                        activeIcon.TrackedObject.ImagePath ?? string.Empty);
                    station.TrackedObject.AddCustomData(_customDataKeys[2],
                        activeIcon.TrackedObject.ArchivePath ?? string.Empty);
                    station.TrackedObject.AddCustomData(_customDataKeys[3],
                        activeIcon.TrackedObject.Sha256HashOfArchiveFile ?? string.Empty);
                }

                //Add the station's icon editors as well and find the active icon to display.
                foreach (var icon in station.TrackedObject.Icons)
                    AddStationIconEditor(station.Id, icon.Id, icon.TrackedObject.IsFromArchive);

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
    ///     Adds a new replacement station and editor to the manager. Optionally, specify the path to the station folder
    ///     relative to the staging folder.
    /// </summary>
    /// <param name="station">The replacement station to add.</param>
    /// <param name="isOnDisk">
    ///     Indicates whether the replacement station is an in-memory addition or was added from .json files
    ///     on disk.
    /// </param>
    /// <param name="pathOnDisk">Specifies the path to the station folder relative to the staging folder.</param>
    /// <returns>The <see cref="Guid" /> of the newly added station.</returns>
    public Guid AddVanillaStation(TrackableObject<ReplacementStation> station, bool isOnDisk, string pathOnDisk)
    {
        try
        {
            lock (_replacementStations)
            {
                CheckForDuplicateVanillaStation(station.Id);

                if (!isOnDisk)
                    _newStations.Add(station.Id);

                ReplacementStationEditor editor = new(station);
                _replacementStations[station.Id] =
                    new Pair<TrackableObject<ReplacementStation>, List<IEditor>>(station, [editor]);

                editor.StationUpdated += Editor_StationUpdated;

                ReplacementStationsAsBindingList.Add(station);

                var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
                if (pathOnDisk.Equals("\\"))
                {
                    StationPaths[station.Id] = Path.Combine(pathOnDisk, station.TrackedObject.DisplayName);
                }
                else
                {
                    var relativePath = PathHelper.GetRelativePath(pathOnDisk, stagingPath);
                    if (relativePath.Equals(pathOnDisk))
                        StationPaths[station.Id] = Path.Combine("\\", station.TrackedObject.DisplayName);
                    else
                        StationPaths[station.Id] = relativePath;
                }

                VanillaStationAdded?.Invoke(this, station.Id);
                return station.Id;
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error adding vanilla station to manager.");
            return Guid.Empty;
        }
    }

    /// <summary>
    ///     Add an icon to a station. Also, adds an IconEditor the internal list of editors for the station.
    /// </summary>
    /// <param name="stationId">The station ID to add the icon to.</param>
    /// <param name="icon">The <see cref="Icon" /> to add.</param>
    /// <returns><c>true</c> if the icon was added successfully; <c>false</c> otherwise.</returns>
    public bool AddStationIcon(Guid stationId, TrackableObject<WolvenIcon> icon)
    {
        return AddStationIcon(stationId, icon, false, false);
    }

    /// <summary>
    ///     Add an icon to a station. Also, adds an IconEditor the internal list of editors for the station. Optionally, make
    ///     the icon active and specify whether the icon is an existing icon.
    /// </summary>
    /// <param name="stationId">The station ID to add the icon to.</param>
    /// <param name="icon">The <see cref="Icon" /> to add.</param>
    /// <param name="makeActive">
    ///     Whether to immediately make this icon the active one for the station, de-activating other
    ///     icons.
    /// </param>
    /// <param name="isExistingIcon">Whether the icon editor should be initialized with an existing .archive file.</param>
    /// <returns><c>true</c> if the icon was added successfully; <c>false</c> otherwise.</returns>
    public bool AddStationIcon(Guid stationId, TrackableObject<WolvenIcon> icon, bool makeActive, bool isExistingIcon)
    {
        try
        {
            if (!_stations.TryGetValue(stationId, out var pair)) return false;

            if (makeActive)
            {
                //First, de-active other icons.
                foreach (var i in pair.Key.TrackedObject.Icons)
                    i.TrackedObject.IsActive = false;

                //Make this new icon the active one.
                icon.TrackedObject.IsActive = true;
            }

            pair.Key.TrackedObject.AddIcon(icon);
            StationsAsBindingList.First(s => s.Id == pair.Key.Id).TrackedObject.AddIcon(icon);

            IconEditor iconEditor = new(pair.Key, icon,
                isExistingIcon ? IconEditorType.FromArchive : IconEditorType.FromPng);
            iconEditor.Translate();
            pair.Value.Add(iconEditor);

            StationUpdated?.Invoke(this, stationId);

            return pair.Key.TrackedObject.Icons.Contains(icon);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error adding icon to station.");
            return false;
        }
    }

    /// <summary>
    ///     Import a station from an archive file. The station will be extracted and added to the manager.
    /// </summary>
    /// <param name="filePath">The path to the .zip file containing the radio station.</param>
    /// <returns>The <see cref="Guid" /> of the imported station; <c>null</c> if the station couldn't be imported.</returns>
    public Guid? ImportStationFromArchive(string filePath)
    {
        try
        {
            var directories = ExtractStationArchive(filePath);
            var stationId = LoadStationFromDirectory(directories.tempDir, directories.songDir, true);
            var station = GetStation(stationId)?.Key;

            if (station != null) return station.Id;

            AuLogger.GetCurrentLogger<StationManager>("HandleStationArchive")
                .Warn($"Station not found in directory: {directories.tempDir}");
            return null;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error importing station from archive.");
        }

        return null;
    }

    /// <summary>
    ///     Import a vanilla station from an archive file. The station will be extracted and added to the manager.
    /// </summary>
    /// <param name="filePath">The path to the .zip file containing the radio station.</param>
    /// <returns>The <see cref="Guid" /> of the imported station; <c>null</c> if the station couldn't be imported.</returns>
    public Guid? ImportVanillaStationFromArchive(string filePath)
    {
        try
        {
            var directories = ExtractStationArchive(filePath);
            var stationId = LoadStationFromDirectory(directories.tempDir, directories.songDir, true);
            var station = GetVanillaStation(stationId)?.Key;

            if (station != null) return station.Id;

            AuLogger.GetCurrentLogger<StationManager>("HandleVanillaStationArchive")
                .Warn($"Vanilla Station not found in directory: {directories.tempDir}");
            return null;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error importing vanilla station from archive.");
        }

        return null;
    }

    /// <summary>
    ///     Copy an existing icon from a reference station to another station.
    /// </summary>
    /// <param name="stationId">The station ID to which the icon should be copied.</param>
    /// <param name="referenceStationId">The station ID from which the icon is being copied.</param>
    /// <param name="icon">The <see cref="WolvenIcon" /> to copy.</param>
    /// <returns>The internal id of the new copied icon; <c>null</c> if the icon couldn't be copied.</returns>
    public Guid? CopyStationIcon(Guid stationId, Guid referenceStationId, TrackableObject<WolvenIcon> icon)
    {
        return CopyStationIcon(stationId, referenceStationId, icon, false);
    }

    /// <summary>
    ///     Copies an existing icon from a reference station to another station. Optionally, make the copied icon active.
    /// </summary>
    /// <param name="stationId">The station ID to which the icon should be copied.</param>
    /// <param name="referenceStationId">The station ID from which the icon is being copied.</param>
    /// <param name="icon">The <see cref="WolvenIcon" /> to copy.</param>
    /// <param name="makeActive">Whether to make this copied icon active for the current station.</param>
    /// <returns>The internal id of the new copied icon; <c>null</c> if the icon couldn't be copied.</returns>
    public Guid? CopyStationIcon(Guid stationId, Guid referenceStationId, TrackableObject<WolvenIcon> icon,
        bool makeActive)
    {
        try
        {
            // Verify both stations exist in the manager
            if (!_stations.TryGetValue(stationId, out var currentStationPair) ||
                !_stations.TryGetValue(referenceStationId, out var referenceStationPair))
                return null;

            // Create a deep copy of the WolvenIcon object and update paths
            TrackableObject<WolvenIcon> copiedIcon = new((WolvenIcon)icon.TrackedObject.Clone());

            // Add the copied icon to the target station
            var currentStation = currentStationPair.Key;
            currentStation.TrackedObject.AddIcon(copiedIcon);
            StationsAsBindingList.First(s => s.Id == currentStationPair.Key.Id).TrackedObject.AddIcon(copiedIcon);

            if (makeActive)
            {
                // Deactivate all other icons for the current station
                foreach (var i in currentStation.TrackedObject.Icons) i.TrackedObject.IsActive = false;
                copiedIcon.TrackedObject.IsActive = true;
            }

            // Add an IconEditor for the copied icon
            IconEditor copiedIconEditor = new(currentStation, copiedIcon, IconEditorType.FromArchive);
            copiedIconEditor.Translate();
            currentStationPair.Value.Add(copiedIconEditor);

            // Notify listeners that the station has been updated
            StationUpdated?.Invoke(this, stationId);

            return copiedIcon.Id;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>("CopyStationIcon").Error(ex, "Error copying icon from station.");
            return null;
        }
    }

    /// <summary>
    ///     Remove an icon from a station, including the associated IconEditor.
    /// </summary>
    /// <param name="stationId">The station ID to remove the icon from.</param>
    /// <param name="icon">The <see cref="WolvenIcon" /> to remove.</param>
    /// <returns><c>true</c> if the icon was removed successfully; <c>false</c> otherwise.</returns>
    public bool RemoveStationIcon(Guid stationId, TrackableObject<WolvenIcon> icon)
    {
        return RemoveStationIcon(stationId, icon, false);
    }

    /// <summary>
    ///     Remove an icon from a station, including the associated IconEditor. Optionally, delete the icon files from disk.
    /// </summary>
    /// <param name="stationId">The station ID to remove the icon from.</param>
    /// <param name="icon">The <see cref="WolvenIcon" /> to remove.</param>
    /// <param name="deleteFiles">Indicates whether to delete the icon files from disk.</param>
    /// <returns><c>true</c> if the icon was removed successfully; <c>false</c> otherwise.</returns>
    public bool RemoveStationIcon(Guid stationId, TrackableObject<WolvenIcon> icon, bool deleteFiles)
    {
        try
        {
            if (!_stations.TryGetValue(stationId, out var pair)) return false;

            // Remove the icon from the station
            pair.Key.TrackedObject.RemoveIcon(icon);
            StationsAsBindingList.First(s => s.Id == pair.Key.Id).TrackedObject.RemoveIcon(icon);

            // Remove the associated IconEditor if it exists
            var iconEditor = GetStationIconEditor(stationId, icon.Id);
            if (iconEditor != null)
                pair.Value.Remove(iconEditor);

            if (deleteFiles)
            {
                if (!IsIconLinkedToOtherStations(icon.TrackedObject.IconId, stationId))
                {
                    if (icon.TrackedObject.ArchivePath != null &&
                        FileHelper.DoesFileExist(icon.TrackedObject.ArchivePath))
                        FileHelper.DeleteFile(icon.TrackedObject.ArchivePath);
                    if (icon.TrackedObject.ImagePath != null && FileHelper.DoesFileExist(icon.TrackedObject.ImagePath))
                        FileHelper.DeleteFile(icon.TrackedObject.ImagePath);

                    // Delete the icon's folder in the appdata directory
                    var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    var foldersInAppData = FileHelper
                        .SafeEnumerateDirectories(Path.Combine(appData, "Wolven Icon Generator", "tools")).ToList();
                    var foldersToDelete = foldersInAppData.Where(f => f.Contains(icon.Id.ToString())).ToList();
                    foreach (var folder in foldersToDelete.Where(Directory.Exists))
                        Directory.Delete(folder, true);
                }
                else
                {
                    AuLogger.GetCurrentLogger<StationManager>("RemoveStationIcon")
                        .Warn("Could not delete files for the icon as they are linked to another station's icon.");
                }
            }

            // Notify listeners that the station has been updated
            StationUpdated?.Invoke(this, stationId);

            return pair.Key.TrackedObject.Icons.All(i => i.Id != icon.Id);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>("RemoveStationIcon")
                .Error(ex, "Error removing icon from station.");
            return false;
        }
    }

    /// <summary>
    ///     Checks if an icon is linked to other stations.
    /// </summary>
    /// <param name="iconId">The ID of the icon to check.</param>
    /// <param name="excludingStationId">The station ID to exclude from the check.</param>
    /// <returns><c>true</c> if the icon is linked to other stations; <c>false</c> otherwise.</returns>
    public bool IsIconLinkedToOtherStations(Guid? iconId, Guid excludingStationId)
    {
        // Check if any other station references the icon (excluding the provided station ID)
        return _stations.Values.Any(pair => pair.Key.Id != excludingStationId &&
                                            pair.Key.TrackedObject.Icons.Any(icon =>
                                                icon.TrackedObject.IconId == iconId));
    }

    /// <summary>
    ///     Get the icon associated with the station by id.
    /// </summary>
    /// <param name="stationId">The id of the station to get the icon of.</param>
    /// <param name="iconId">The id of the icon to retrieve from the station.</param>
    /// <returns>
    ///     The <see cref="TrackableObject{WolvenIcon}" /> corresponding to the id or <c>null</c> if no icon existed with
    ///     the id.
    /// </returns>
    public TrackableObject<WolvenIcon>? GetStationIcon(Guid stationId, Guid iconId)
    {
        return !_stations.TryGetValue(stationId, out var pair)
            ? null
            : pair.Key.TrackedObject.Icons.FirstOrDefault(i => i.Id == iconId);
    }

    /// <summary>
    ///     Enable a specific icon for a station. An icon can only have one active icon at a time.
    /// </summary>
    /// <param name="stationId">The id of the station.</param>
    /// <param name="iconId">The id of the icon to enable.</param>
    public void EnableStationIcon(Guid stationId, Guid iconId)
    {
        try
        {
            if (!_stations.TryGetValue(stationId, out var pair)) return;

            foreach (var icon in pair.Key.TrackedObject.Icons) icon.TrackedObject.IsActive = icon.Id == iconId;

            StationUpdated?.Invoke(this, stationId);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error enabling station icon.");
        }
    }

    /// <summary>
    ///     Disable a specific icon for a station.
    /// </summary>
    /// <param name="stationId">The id of the station.</param>
    /// <param name="iconId">The id of the icon to disable.</param>
    public void DisableStationIcon(Guid stationId, Guid iconId)
    {
        try
        {
            if (!_stations.TryGetValue(stationId, out var pair)) return;

            // Find the specific icon by ID and disable it.
            var targetIcon = pair.Key.TrackedObject.Icons.FirstOrDefault(icon => icon.Id == iconId);
            if (targetIcon == null) return;

            targetIcon.TrackedObject.IsActive = false;

            StationUpdated?.Invoke(this, stationId);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error disabling station icon.");
        }
    }

    /// <summary>
    ///     Remove all icons from a station, including the associated IconEditors. Does not delete the icon files from disk.
    /// </summary>
    /// <param name="stationId">The station ID to remove all icons from.</param>
    /// <returns><c>true</c> if all icons were removed successfully; <c>false</c> otherwise.</returns>
    public bool RemoveAllStationIcons(Guid stationId)
    {
        return RemoveAllStationIcons(stationId, false);
    }

    /// <summary>
    ///     Remove all icons from a station, including the associated IconEditors. Optionally, delete the icon files from disk.
    /// </summary>
    /// <param name="stationId">The station ID to remove all icons from.</param>
    /// <param name="deleteFiles">Indicates whether to delete the Icon files from disk.</param>
    /// <returns><c>true</c> if all icons were removed successfully; <c>false</c> otherwise.</returns>
    public bool RemoveAllStationIcons(Guid stationId, bool deleteFiles)
    {
        try
        {
            if (!_stations.TryGetValue(stationId, out var pair)) return false;

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var foldersInAppData = FileHelper
                .SafeEnumerateDirectories(Path.Combine(appData, "Wolven Icon Generator", "tools")).ToList();
            foreach (var icon in pair.Key.TrackedObject.Icons)
            {
                var iconEditor = GetStationIconEditor(stationId, icon.Id);
                if (iconEditor != null)
                    pair.Value.Remove(iconEditor);

                if (deleteFiles)
                {
                    if (icon.TrackedObject.ArchivePath != null &&
                        FileHelper.DoesFileExist(icon.TrackedObject.ArchivePath))
                        FileHelper.DeleteFile(icon.TrackedObject.ArchivePath);
                    if (icon.TrackedObject.ImagePath != null && FileHelper.DoesFileExist(icon.TrackedObject.ImagePath))
                        FileHelper.DeleteFile(icon.TrackedObject.ImagePath);

                    //Delete the icon's folder in the appdata directory.
                    var foldersToDelete = foldersInAppData.Where(f => f.Contains(icon.Id.ToString())).ToList();
                    foreach (var folder in foldersToDelete.Where(Directory.Exists))
                        Directory.Delete(folder, true);
                }
            }

            pair.Key.TrackedObject.RemoveAllIcons();
            StationsAsBindingList.First(s => s.Id == pair.Key.Id).TrackedObject.RemoveAllIcons();

            StationUpdated?.Invoke(this, stationId);

            return pair.Key.TrackedObject.Icons.Count == 0;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error removing all icons from station.");
            return false;
        }
    }

    /// <summary>
    ///     Add a new, blank station to the manager. The station will have the default metadata and no songs.
    ///     The station will be added to the manager and the ID will be returned.
    /// </summary>
    /// <returns>The <see cref="Guid" /> of the newly added station.</returns>
    public Guid AddStation()
    {
        return AddBlankStation();
    }

    /// <summary>
    ///     Removes a station, and its editors from the manager.
    /// </summary>
    /// <param name="stationId">The station to remove, by ID.</param>
    public void RemoveStation(Guid stationId)
    {
        try
        {
            lock (_stations)
            {
                if (!_stations.TryGetValue(stationId, out var pair)) return;

                foreach (var editor in pair.Value.Where(e => e.Type == EditorType.StationEditor).Cast<StationEditor>())
                    editor.Dispose();

                foreach (var editor in pair.Value.Where(e => e.Type == EditorType.IconEditor).Cast<IconEditor>())
                    editor.Dispose();

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
    ///     Remove a vanilla station, and its editor from the manager.
    /// </summary>
    /// <param name="stationId">The station to remove, by ID.</param>
    public void RemoveVanillaStation(Guid stationId)
    {
        try
        {
            lock (_replacementStations)
            {
                if (!_replacementStations.TryGetValue(stationId, out var pair)) return;

                foreach (var editor in pair.Value.Where(e => e.Type == EditorType.StationEditor)
                             .Cast<ReplacementStationEditor>())
                    editor.Dispose();

                _replacementStations.Remove(stationId);
                _newStations.Remove(stationId);
                StationPaths.Remove(stationId);

                ReplacementStationsAsBindingList.Remove(ReplacementStationsAsBindingList.First(s => s.Id == stationId));

                VanillaStationRemoved?.Invoke(this, stationId);
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error removing vanilla station from manager.");
        }
    }

    /// <summary>
    ///     Gets the station count string, formatted with the enabled station count and localized.
    /// </summary>
    /// <returns>A formatted string showing enabled and disabled station counts: <c>Enabled Stations: {0} / {1}</c></returns>
    public string GetStationCount()
    {
        var enabledCount = _stations.Select(pair => pair.Value.Key.TrackedObject.MetaData.IsActive)
            .Count(isActive => isActive);
        return string.Format(StationCountFormat, enabledCount, _stations.Count);
    }

    /// <summary>
    ///     Gets the vanilla station count string, formatted with the enabled vanilla station count and localized.
    /// </summary>
    /// <returns>
    ///     A formatted string showing enabled and disabled station counts: <c>Enabled Vanilla Stations: {0} / {1}</c>
    /// </returns>
    public string GetVanillaStationCount()
    {
        var enabledCount = _replacementStations.Select(pair => pair.Value.Key.TrackedObject.IsActive)
            .Count(isActive => isActive);
        return string.Format(VanillaStationCountFormat, enabledCount, _replacementStations.Count);
    }

    /// <summary>
    ///     Gets the station icon for the specified station ID.
    /// </summary>
    /// <param name="stationId">The station to get the icon of, by id.</param>
    /// <returns>The active <see cref="Icon" /> of the station or <c>null</c> if no active icons.</returns>
    public TrackableObject<WolvenIcon>? GetStationActiveIcon(Guid? stationId)
    {
        try
        {
            lock (_stations)
            {
                if (stationId == null) return null;

                return _stations.TryGetValue((Guid)stationId, out var pair)
                    ? pair.Key.TrackedObject.GetActiveIcon()
                    : null;
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error getting active icon from station.");
            return null;
        }
    }

    /// <summary>
    ///     Gets the icon editor for the specified station ID and icon ID.
    /// </summary>
    /// <param name="stationId">The station to get the editor of, by id.</param>
    /// <param name="iconId">The icon associated with the editor, by id.</param>
    /// <returns>
    ///     The <see cref="IconEditor" /> for the specified station and icon or <c>null</c> if the editor could not be
    ///     found in the manager.
    /// </returns>
    public IconEditor? GetStationIconEditor(Guid? stationId, Guid? iconId)
    {
        try
        {
            lock (_stations)
            {
                if (stationId == null || iconId == null) return null;

                return _stations.TryGetValue((Guid)stationId, out var pair)
                    ? pair.Value.FirstOrDefault(e =>
                            e.Type == EditorType.IconEditor && ((IconEditor)e).Icon.Id.Equals(iconId))
                        as IconEditor
                    : null;
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error getting icon editor from station.");
            return null;
        }
    }

    /// <summary>
    ///     Add an icon editor the station based on the station ID and icon ID.
    /// </summary>
    /// <param name="stationId">The station to add an editor of, by id.</param>
    /// <param name="iconId">The icon to associate with the editor, by id.</param>
    /// <param name="isExistingArchive">Whether the editor should be initialized with an existing .archive file.</param>
    public void AddStationIconEditor(Guid? stationId, Guid? iconId, bool isExistingArchive)
    {
        try
        {
            lock (_stations)
            {
                if (stationId == null || iconId == null) return;

                if (!_stations.TryGetValue((Guid)stationId, out var pair)) return;

                var wolvenIcon = pair.Key.TrackedObject.Icons.FirstOrDefault(i => i.Id.Equals(iconId));
                if (wolvenIcon == null) return;

                IconEditor editor = new(pair.Key, wolvenIcon,
                    isExistingArchive ? IconEditorType.FromArchive : IconEditorType.FromPng);
                editor.Translate();
                pair.Value.Add(editor);
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error adding icon editor to station.");
        }
    }

    /// <summary>
    ///     Remove the icon editor for the specified station ID and icon ID.
    /// </summary>
    /// <param name="stationId">The station to remove the editor from, by id.</param>
    /// <param name="iconId">The icon to remove the editor of, by id.</param>
    public void RemoveStationIconEditor(Guid? stationId, Guid? iconId)
    {
        try
        {
            lock (_stations)
            {
                if (stationId == null || iconId == null) return;

                if (!_stations.TryGetValue((Guid)stationId, out var pair)) return;

                var editor = pair.Value.FirstOrDefault(e =>
                    e.Type == EditorType.IconEditor && ((IconEditor)e).Icon.Id.Equals(iconId));
                if (editor == null) return;

                pair.Value.Remove(editor);
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error removing icon editor from station.");
        }
    }

    /// <summary>
    ///     Clear all stations and their editors from the manager. Does not delete the folders in the staging directory.
    /// </summary>
    public void ClearStations()
    {
        ClearStations(false);
    }

    /// <summary>
    ///     Clears all stations and their editors from the manager. Optionally, delete the folders in the staging directory.
    /// </summary>
    /// <param name="deleteFoldersFromStaging">Indicates if the folders in the staging directory should also be deleted.</param>
    public void ClearStations(bool deleteFoldersFromStaging)
    {
        try
        {
            lock (_stations)
            {
                foreach (var editor in _stations.Values
                             .SelectMany(p => p.Value.Where(e => e.Type == EditorType.StationEditor))
                             .Cast<StationEditor>())
                    editor.Dispose();

                _stations.Clear();
                StationsAsBindingList.Clear();

                StationsCleared?.Invoke(this, EventArgs.Empty);
            }

            lock (_replacementStations)
            {
                foreach (var editor in _replacementStations.Values
                             .SelectMany(p => p.Value.Where(e => e.Type == EditorType.StationEditor))
                             .Cast<ReplacementStationEditor>())
                    editor.Dispose();

                _replacementStations.Clear();
                ReplacementStationsAsBindingList.Clear();

                VanillaStationsCleared?.Invoke(this, EventArgs.Empty);
            }

            StationPaths.Clear();
            _newStations.Clear();

            if (deleteFoldersFromStaging)
            {
                var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
                foreach (var folder in FileHelper.SafeEnumerateDirectories(stagingPath))
                    if (IsProtectedFolder(folder))
                        PathHelper.ClearDirectory(
                            folder); //Don't remove protected folders; just remove their contents
                    else
                        Directory.Delete(folder, true);
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error clearing stations from manager.");
        }
    }

    /// <summary>
    ///     Get the station and editors for the specified station ID.
    /// </summary>
    /// <param name="stationId">The ID of the station to get.</param>
    /// <returns>
    ///     A pair where the key is the <see cref="TrackableObject{T}" /> and the value is
    ///     the list of <see cref="IEditor" />s associated with the station;
    ///     or <c>null</c> if the <paramref name="stationId" /> did not exist in the manager.
    /// </returns>
    public Pair<TrackableObject<AdditionalStation>, List<IEditor>>? GetStation(Guid? stationId)
    {
        try
        {
            lock (_stations)
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
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error getting station from manager.");
            return null;
        }
    }

    /// <summary>
    ///     Get the vanilla station and editors for the specified station ID.
    /// </summary>
    /// <param name="stationId">The ID of the station to get.</param>
    /// <returns>
    ///     A pair where the key is the <see cref="TrackableObject{T}" /> and the value is
    ///     the list of <see cref="IEditor" />s associated with the station;
    ///     or <c>null</c> if the <paramref name="stationId" /> did not exist in the manager.
    /// </returns>
    public Pair<TrackableObject<ReplacementStation>, List<IEditor>>? GetVanillaStation(Guid? stationId)
    {
        try
        {
            lock (_replacementStations)
            {
                try
                {
                    if (stationId == null) return null;
                    var id = (Guid)stationId;
                    lock (_replacementStations)
                    {
                        return _replacementStations.TryGetValue(id, out var pair) ? pair : null;
                    }
                }
                catch (Exception ex)
                {
                    AuLogger.GetCurrentLogger<StationManager>()
                        .Error(ex, "Error getting vanilla station from manager.");
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error getting vanilla station from manager.");
            return null;
        }
    }

    /// <summary>
    ///     Get the station editor for the specified station ID.
    /// </summary>
    /// <param name="stationId">The station to get the editor of, by id.</param>
    /// <returns>The station editor for the specified station id or <c>null</c> if no editor exists in the manager.</returns>
    public StationEditor? GetStationEditor(Guid? stationId)
    {
        try
        {
            lock (_stations)
            {
                try
                {
                    if (stationId == null) return null;
                    var id = (Guid)stationId;
                    lock (_stations)
                    {
                        return _stations.TryGetValue(id, out var pair)
                            ? pair.Value.FirstOrDefault(e => e.Type == EditorType.StationEditor) as StationEditor
                            : null;
                    }
                }
                catch (Exception ex)
                {
                    AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error getting station editor from manager.");
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error getting station editor from manager.");
            return null;
        }
    }

    /// <summary>
    ///     Get the vanilla station editor for the specified station ID.
    /// </summary>
    /// <param name="stationId">The station to get the editor of, by id.</param>
    /// <returns>The station editor for the specified station id or <c>null</c> if no editor exists in the manager.</returns>
    public ReplacementStationEditor? GetVanillaStationEditor(Guid? stationId)
    {
        try
        {
            lock (_replacementStations)
            {
                try
                {
                    if (stationId == null) return null;
                    var id = (Guid)stationId;
                    lock (_replacementStations)
                    {
                        return _replacementStations.TryGetValue(id, out var pair)
                            ? pair.Value.FirstOrDefault(e => e.Type == EditorType.StationEditor) as
                                ReplacementStationEditor
                            : null;
                    }
                }
                catch (Exception ex)
                {
                    AuLogger.GetCurrentLogger<StationManager>("GetVanillaStationEditor")
                        .Error(ex, "Error getting vanilla station editor from manager.");
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>("GetVanillaStationEditor")
                .Error(ex, "Error getting vanilla station editor from manager.");
            return null;
        }
    }

    /// <summary>
    ///     Stops all music players for all stations in the manager.
    /// </summary>
    public void StopAllMusicPlayers()
    {
        try
        {
            foreach (var editor in _stations.Values.SelectMany(pair => pair.Value
                         .Where(e => e.Type == EditorType.StationEditor)).Cast<StationEditor>())
                editor.GetMusicPlayer().StopStream();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error stopping all music players.");
        }
    }

    /// <summary>
    ///     Set the station's active status to the new status and raise the <see cref="StationUpdated" /> event.
    /// </summary>
    /// <param name="stationId">The ID of the station to change the status of.</param>
    /// <param name="newStatus">The new status of the station: <c>true</c> = enabled; <c>false</c> = disabled.</param>
    public void ChangeStationActiveStatus(Guid stationId, bool newStatus)
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
    ///     Set the vanilla station's active status to the new status and raise the <see cref="VanillaStationUpdated" /> event.
    /// </summary>
    /// <param name="stationId">The ID of the station to change the status of.</param>
    /// <param name="newStatus">The new status of the vanilla station: <c>true</c> = enabled; <c>false</c> = disabled.</param>
    public void ChangeVanillaStationActiveStatus(Guid stationId, bool newStatus)
    {
        try
        {
            if (!_replacementStations.TryGetValue(stationId, out var pair)) return;

            pair.Key.TrackedObject.IsActive = newStatus;
            ReplacementStationsAsBindingList.First(s => s.Id == stationId).TrackedObject.IsActive = newStatus;

            VanillaStationUpdated?.Invoke(this, stationId);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error changing vanilla station status.");
        }
    }

    /// <summary>
    ///     Check the pending save status of the station with the specified ID.
    /// </summary>
    /// <param name="stationId">The ID of the station to check.</param>
    /// <returns><c>true</c> if there are pending changes; <c>false</c> otherwise.</returns>
    public bool CheckSaveStatus(Guid stationId)
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
    ///     Check the pending save status of the vanilla station with the specified ID.
    /// </summary>
    /// <param name="stationId">The ID of the station to check.</param>
    /// <returns><c>true</c> if there are pending changes; <c>false</c> otherwise.</returns>
    public bool CheckVanillaSaveStatus(Guid stationId)
    {
        try
        {
            return _replacementStations[stationId].Key.CheckPendingSaveStatus() &
                   ReplacementStationsAsBindingList.First(s => s.Id == stationId).CheckPendingSaveStatus();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking vanilla station status.");
            return false;
        }
    }

    /// <summary>
    ///     Occurs when a station is updated.
    /// </summary>
    /// <param name="stationId">The <see cref="Guid" /> of the station that was updated.</param>
    public void OnStationUpdated(Guid stationId)
    {
        var newName = CheckForDuplicateStation(stationId);
        CheckSaveStatus(stationId);

        ((StationEditor)_stations[stationId].Value.First(e => e.Type == EditorType.StationEditor))
            .UpdateStationName(newName);
    }

    /// <summary>
    ///     Occurs when a vanilla station is updated.
    /// </summary>
    /// <param name="stationId">The <see cref="Guid" /> of the replacement station that was updated.</param>
    public void OnVanillaStationUpdated(Guid stationId)
    {
        var newName = CheckForDuplicateVanillaStation(stationId);
        CheckVanillaSaveStatus(stationId);

        ((ReplacementStationEditor)_replacementStations[stationId].Value
            .First(e => e.Type == EditorType.StationEditor)).UpdateStationName(newName);
    }

    /// <summary>
    ///     Checks for duplicate station names and gets an updated name if a duplicate is found.
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

        if (duplicateCount <= 0) return originalName;

        station.MetaData.DisplayName = updatedName;
        StationsAsBindingList.First(s => s.Id == stationId).TrackedObject.MetaData.DisplayName = updatedName;

        StationUpdated?.Invoke(this, stationId);
        StationNameDuplicate?.Invoke(this, (stationId, updatedName));

        return updatedName;
    }

    /// <summary>
    ///     Checks for duplicate vanilla station names and gets an updated name if a duplicate is found.
    ///     This method checks the display name of the replacement station, not the original vanilla station name.
    /// </summary>
    /// <param name="stationId">The ID of the station to check.</param>
    /// <returns>The updated station name.</returns>
    public string CheckForDuplicateVanillaStation(Guid stationId)
    {
        if (!_replacementStations.TryGetValue(stationId, out var pair)) return string.Empty;

        var station = pair.Key.TrackedObject;
        var originalName = station.DisplayName;
        var updatedName = originalName;
        var duplicateCount = 0;

        foreach (var existingStation in _replacementStations.Values.Select(p => p.Key))
        {
            if (existingStation.Id == stationId) continue;

            if (!existingStation.TrackedObject.DisplayName.Equals(updatedName,
                    StringComparison.OrdinalIgnoreCase)) continue;

            duplicateCount++;
            updatedName = $"{originalName} ({duplicateCount})";
        }

        if (duplicateCount <= 0) return originalName;

        station.DisplayName = updatedName;
        ReplacementStationsAsBindingList.First(s => s.Id == stationId).TrackedObject.DisplayName = updatedName;

        VanillaStationUpdated?.Invoke(this, stationId);
        VanillaStationNameDuplicate?.Invoke(this, (stationId, updatedName));

        return updatedName;
    }

    /// <summary>
    ///     Translates all editors in the manager to the current language.
    /// </summary>
    public void TranslateEditors()
    {
        try
        {
            foreach (var editorList in _stations.Values.Select(pair => pair.Value))
            foreach (var editor in editorList)
                editor.Translate();

            foreach (var editorList in _replacementStations.Values.Select(pair => pair.Value))
            foreach (var editor in editorList)
                editor.Translate();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error translating station's editors.");
        }
    }

    /// <summary>
    ///     Get a dictionary of stations with a pair indicating if the station has missing songs and the count of missing
    ///     songs.
    /// </summary>
    /// <returns>
    ///     A dictionary where the key is the station's ID and the value is a pair containing whether the station is
    ///     missing songs and the count of missing songs.
    /// </returns>
    public Dictionary<Guid, Pair<bool, int>> CheckForMissingSongs()
    {
        try
        {
            Dictionary<Guid, Pair<bool, int>> missingSongs = new();
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
    ///     Get a dictionary of vanilla stations with a pair indicating if the station has missing songs and the count of
    ///     missing songs.
    /// </summary>
    /// <returns>
    ///     A dictionary where the key is the vanilla station's ID and the value is a pair containing whether the station
    ///     is missing songs and the count of missing songs.
    /// </returns>
    public Dictionary<Guid, Pair<bool, int>> CheckForVanillaMissingSongs()
    {
        try
        {
            Dictionary<Guid, Pair<bool, int>> missingSongs = new();
            foreach (var pair in _replacementStations)
            {
                var station = pair.Value.Key.TrackedObject;
                var missingCount = station.Tracks.Count(t => !FileHelper.DoesFileExist(t.ReplacementFilePath, false));
                missingSongs[pair.Key] = new Pair<bool, int>(missingCount > 0, missingCount);
            }

            return missingSongs;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking for vanilla missing songs.");
            return [];
        }
    }

    /// <summary>
    ///     Get a dictionary of station ids with a value indicating if the station has pending saves.
    /// </summary>
    /// <returns>A dictionary where the key is the station's ID and the value indicates if the station is pending save.</returns>
    public Dictionary<Guid, bool> CheckPendingSave()
    {
        try
        {
            Dictionary<Guid, bool> pendingSave = new();
            foreach (var pair in _stations)
            {
                pendingSave[pair.Key] = pair.Value.Key.IsPendingSave
                                        & StationsAsBindingList
                                            .First(s => s.Id == pair.Key).IsPendingSave;
                pendingSave[pair.Key] |= IsNewStation(pair.Key);
            }

            return pendingSave;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking for pending saves.");
            return [];
        }
    }

    /// <summary>
    ///     Get a dictionary of vanilla station ids with a value indicating if the station has pending saves.
    /// </summary>
    /// <returns>A dictionary where the key is the station's ID and the value indicates if the station is pending save.</returns>
    public Dictionary<Guid, bool> CheckVanillaPendingSave()
    {
        try
        {
            Dictionary<Guid, bool> pendingSave = new();
            foreach (var pair in _replacementStations)
            {
                pendingSave[pair.Key] = pair.Value.Key.IsPendingSave
                                        & ReplacementStationsAsBindingList
                                            .First(s => s.Id == pair.Key).IsPendingSave;
                pendingSave[pair.Key] |= IsNewStation(pair.Key);
            }

            return pendingSave;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking for vanilla pending saves.");
            return [];
        }
    }

    /// <summary>
    ///     Clear the list of new stations added during the current session.
    /// </summary>
    public void ResetNewStations()
    {
        _newStations.Clear();
    }

    /// <summary>
    ///     Get a value indicating if the station with the specified ID is a new station added during the current session.
    /// </summary>
    /// <param name="stationId">The ID of the station to check.</param>
    /// <returns><c>true</c> if the station is new; <c>false</c> otherwise.</returns>
    public bool IsNewStation(Guid stationId)
    {
        return _newStations.Contains(stationId);
    }

    /// <summary>
    ///     Get the count of stations that exist in the game's radios folder but do not exist in the staging folder.
    /// </summary>
    /// <param name="stagingPath">The path to the staging directory.</param>
    /// <param name="gameBasePath">The path to the game's base path where the <c>radios</c> directory is derived from.</param>
    /// <returns>
    ///     A non-zero integer if there are station's in the game's radios folder that are not in the staging folder;
    ///     <c>0</c> otherwise.
    /// </returns>
    public static int CheckGameForExistingStations(string stagingPath, string gameBasePath)
    {
        if (string.IsNullOrEmpty(stagingPath) || string.IsNullOrEmpty(gameBasePath)) return 0;

        try
        {
            var radiosDir = PathHelper.GetRadiosPath(gameBasePath);
            if (!Directory.Exists(radiosDir)) return 0;

            var gameDirectories = FileHelper.SafeEnumerateDirectories(radiosDir);
            var stagingDirectories =
                FileHelper.SafeEnumerateDirectories(stagingPath).Select(Path.GetFileName).ToHashSet();

            return gameDirectories.Select(Path.GetFileName).Count(dirName => !stagingDirectories.Contains(dirName));
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking for existing stations.");
            return 0;
        }
    }

    /// <summary>
    ///     Sync stations from the game's radios folder to the staging folder.
    ///     This will copy all stations and songs from the game's radios folder to the staging folder
    ///     if they do not exist and update them if they are newer.
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
                .ToList().Where(folder => !IsProtectedFolder(folder)); //Do not synchronize protected folders
            var gameDirectories = FileHelper.SafeEnumerateDirectories(radiosDir, "*", SearchOption.AllDirectories)
                .ToList();

            // Synchronize directories
            List<Task> tasks = (from gameDir in gameDirectories
                let dirName = Path.GetFileName(gameDir)
                let stagingDir = Path.Combine(stagingPath, dirName)
                select !stagingDirectories.Contains(stagingDir)
                    ? Task.Run(() => CopyDirectoryAsync(gameDir, stagingDir))
                    // Directory exists, synchronize files
                    : Task.Run(() => SynchronizeFilesAsync(gameDir, stagingDir))).ToList();

            await Task.WhenAll(tasks);
            SyncStatusChanged?.Invoke(Strings.SyncStatusComplete);
            StationsSynchronized?.Invoke(true);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error synchronizing stations.");
            SyncStatusChanged?.Invoke(Strings.SyncStatusError);
            StationsSynchronized?.Invoke(false);
        }
    }

    /// <summary>
    ///     Get the relative path in the staging folder to the station with the specified ID.
    /// </summary>
    /// <param name="stationId">The ID of the station to get the relative path of.</param>
    /// <returns>The relative path to the station's folder; or <see cref="string.Empty" /> if the station ID was invalid.</returns>
    public string GetStationPath(Guid? stationId)
    {
        if (stationId == null) return string.Empty;
        return StationPaths.TryGetValue((Guid)stationId, out var path) ? path : string.Empty;
    }

    /// <summary>
    ///     Synchronize files from the source directory to the target directory.
    /// </summary>
    /// <param name="sourceDir">The directory to synchronize from.</param>
    /// <param name="targetDir">The directory to synchronize to.</param>
    /// <returns></returns>
    private async Task SynchronizeFilesAsync(string sourceDir, string targetDir)
    {
        var sourceFiles = FileHelper.SafeEnumerateFiles(sourceDir);
        var targetFiles = FileHelper.SafeEnumerateFiles(targetDir)
            .Where(f => Path.GetFileName(f) != null)
            .ToDictionary(f => Path.GetFileName(f)!, f => f);

        List<Task> fileTasks = new();
        var fileCount = 0;

        var files = sourceFiles.ToList();
        foreach (var sourceFile in files)
        {
            var fileName = Path.GetFileName(sourceFile);
            var targetFilePath = Path.Combine(targetDir, fileName);

            if (!targetFiles.ContainsKey(fileName) ||
                File.GetLastWriteTime(sourceFile) > File.GetLastWriteTime(targetFilePath))
            {
                fileTasks.Add(Task.Run(() => File.Copy(sourceFile, targetFilePath, true)));
                fileCount++;
            }

            var progress = (int)((float)fileCount / files.Count * 100);
            SyncProgressChanged?.Invoke(progress);
            SyncStatusChanged?.Invoke(string.Format(Strings.SyncProgressChangedFormat, progress));
        }

        await Task.WhenAll(fileTasks);

        var sourceDirectories = FileHelper.SafeEnumerateDirectories(sourceDir);
        var targetDirectories = FileHelper.SafeEnumerateDirectories(targetDir)
            .ToDictionary(d => Path.GetFileName(d)!, d => d);

        List<Task> dirTasks = new();
        var dirCount = 0;

        var sourceSubDirs = sourceDirectories.ToList();
        foreach (var sourceSubDir in sourceSubDirs)
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

            var progress = (int)((float)dirCount / sourceSubDirs.Count * 100);
            SyncProgressChanged?.Invoke(progress);
            SyncStatusChanged?.Invoke(string.Format(Strings.SyncProgressDirectoriesChangedFormat, progress));
        }

        await Task.WhenAll(dirTasks);
    }

    /// <summary>
    ///     Copy a directory and all its contents to the target directory.
    /// </summary>
    /// <param name="sourceDir">The directory to copy from.</param>
    /// <param name="targetDir">The directory to copy to.</param>
    /// <returns></returns>
    private async Task CopyDirectoryAsync(string sourceDir, string targetDir)
    {
        var files = FileHelper.SafeEnumerateFiles(sourceDir);
        var directories = FileHelper.SafeEnumerateDirectories(sourceDir);

        FileHelper.CreateDirectories(targetDir);

        List<Task> copyTasks = (from file in files
            let targetFilePath = Path.Combine(targetDir, Path.GetFileName(file))
            select Task.Run(() =>
            {
                FileHelper.CreateDirectories(targetFilePath);
                File.Copy(file, targetFilePath, true);
            })).ToList();

        copyTasks.AddRange(from directory in directories
            let targetDirectoryPath = Path.Combine(targetDir, Path.GetFileName(directory))
            select Task.Run(() => CopyDirectoryAsync(directory, targetDirectoryPath)));

        await Task.WhenAll(copyTasks);
    }

    private void Editor_StationUpdated(object? sender, EventArgs e)
    {
        switch (sender)
        {
            case StationEditor editor:
                StationUpdated?.Invoke(this, editor.Station.Id);
                break;
            case IconEditor iconEditor:
                StationUpdated?.Invoke(this, iconEditor.Station.Id);
                break;
            case ReplacementStationEditor { ReplacedStation: not null } replEditor:
                VanillaStationUpdated?.Invoke(this, replEditor.ReplacedStation.Id);
                break;
        }
    }

    /// <summary>
    ///     Add a new, blank station to the manager. The station will have the default metadata and no songs.
    ///     The station will be added to the manager and the ID will be returned.
    /// </summary>
    /// <returns>The <see cref="Guid" /> of the newly added station.</returns>
    private Guid AddBlankStation()
    {
        AdditionalStation station = new()
        {
            MetaData =
            {
                DisplayName = Strings.NewStationListBoxEntry
            }
        };
        TrackableObject<AdditionalStation> trackedStation = new(station);
        return AddStation(trackedStation, false);
    }

    /// <summary>
    ///     Processes a directory by loading the metadata, songs, and icons from the files in the directory and adding them to
    ///     the manager.
    /// </summary>
    /// <param name="directory">The directory to process.</param>
    /// <param name="songDirectory">The directory to load songs from.</param>
    /// <param name="treatAsNewStation">
    ///     Indicates if this directory should be treated as a new station (i.e., not in the
    ///     staging directory already) or not.
    /// </param>
    /// <returns>The newly processed Station ID; or <c>null</c> if the directory couldn't be processed.</returns>
    private Guid? ProcessDirectory(string directory, string songDirectory, bool treatAsNewStation)
    {
        try
        {
            var files = FileHelper.SafeEnumerateFiles(directory, "*.*", SearchOption.AllDirectories).ToList();
            var songDirFiles = !directory.Equals(songDirectory)
                ? FileHelper.SafeEnumerateFiles(songDirectory, "*.*", SearchOption.AllDirectories).ToList()
                : files;

            var metadata = files.Where(file => file.EndsWith("metadata.json")).Select(_metaDataJson.LoadJson)
                .FirstOrDefault();
            var songList = files.Where(file => file.EndsWith("songs.sgls")).Select(_songListJson.LoadJson)
                .FirstOrDefault() ?? [];
            var iconList = files.Where(file => file.EndsWith("icons.icls")).Select(_iconListJson.LoadJson)
                .FirstOrDefault() ?? [];
            var iconFiles = files.Where(file => file.EndsWith(".archive")).ToList();
            var songFiles = songDirFiles.Where(file => ValidAudioExtensions.Contains(Path.GetExtension(file).ToLower()))
                .ToList();

            if (metadata == null) return null;

            if (songList.Count == 0)
                songFiles.ForEach(path =>
                {
                    var song = Song.FromFile(path);
                    if (song != null)
                        songList.Add(song);
                });

            AdditionalStation station = new() { MetaData = metadata, Songs = songList };

            if (iconFiles.Count > 0)
            {
                foreach (var icon in iconFiles)
                {
                    TrackableObject<WolvenIcon> trackedIcon = new(new WolvenIcon(string.Empty, icon))
                    {
                        TrackedObject =
                        {
                            IsFromArchive = true,
                            IconId = Guid.NewGuid(),
                            CustomIcon = new RadioExtCustomIcon
                            {
                                InkAtlasPart = metadata.CustomIcon.InkAtlasPart,
                                InkAtlasPath = metadata.CustomIcon.InkAtlasPath
                            }
                        }
                    };

                    trackedIcon.AcceptChanges();
                    station.AddIcon(trackedIcon);
                }

                station.Icons.First().TrackedObject.IsActive = true; //Set only the first icon as active
            }

            if (iconList.Count > 0 &&
                iconFiles.Count <=
                0) //we only want to add the icons if there are no .archive files in the directory (indicating an imported station)
                foreach (var icon in iconList)
                {
                    TrackableObject<WolvenIcon> trackedIcon = new(icon);
                    trackedIcon.AcceptChanges();
                    station.AddIcon(trackedIcon, icon.IsActive);
                }

            TrackableObject<AdditionalStation> trackedStation = new(station);

            EnsureDisplayNameFormat(trackedStation);
            trackedStation.AcceptChanges();

            return AddStation(trackedStation, !treatAsNewStation, directory);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error processing directory.");
            return null;
        }
    }

    /// <summary>
    ///     Processes a vanilla station directory by loading the data from the files in the directory and adding them to the
    ///     manager.
    /// </summary>
    /// <param name="directory">The directory to process.</param>
    /// <param name="treatAsNewStation">
    ///     Indicates if this directory should be treated as a new station (i.e., not in the
    ///     staging directory already) or not.
    /// </param>
    /// <returns>The newly processed Station ID; or <c>null</c> if the directory couldn't be processed.</returns>
    private Guid? ProcessVanillaDirectory(string directory, bool treatAsNewStation)
    {
        try
        {
            var files = FileHelper.SafeEnumerateFiles(directory, "*.*", SearchOption.AllDirectories).ToList();
            var stationData = files.Where(file => file.EndsWith("replaced.json"))
                .Select(_replacementStationJson.LoadJson)
                .FirstOrDefault();

            if (stationData == null) return null;

            ReplacementStation station = new()
            {
                VanillaStation = stationData.VanillaStation,
                Tracks = stationData.Tracks,
                IsActive = stationData.IsActive,
                Notes = stationData.Notes,
                DisplayName = stationData.DisplayName
            };

            TrackableObject<ReplacementStation> trackedStation = new(station);
            trackedStation.AcceptChanges();

            return AddVanillaStation(trackedStation, !treatAsNewStation, directory);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>("ProcessVanillaDirectory")
                .Error(ex, "Error processing vanilla directory.");
            return null;
        }
    }

    /// <summary>
    ///     Extracts the contents of a station archive file (.zip or .rar) to a temporary directory.
    /// </summary>
    /// <param name="filePath">The path to a .zip or .rar file containing a station's files.</param>
    /// <returns>A tuple containing the temporary station directory and the song directory for the station.</returns>
    public static (string tempDir, string songDir) ExtractStationArchive(string filePath)
    {
        try
        {
            var tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var songDir = GlobalData.ConfigManager.Get("defaultSongLocation") as string ??
                          Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            songDir = Path.Combine(songDir, Path.GetFileNameWithoutExtension(filePath));

            Directory.CreateDirectory(tempDir);

            using var archive = ArchiveFactory.Open(filePath);

            foreach (var entry in archive.Entries)
            {
                if (entry.IsDirectory) continue;

                if (entry.Key == null)
                {
                    AuLogger.GetCurrentLogger<StationManager>("ExtractStationArchive").Error("Entry key is null.");
                    continue;
                }

                var entryFileName = PathHelper.SanitizePath(entry.Key);

                if (string.IsNullOrEmpty(entryFileName)) continue;

                //If the file is a song file, extract it to the song directory, otherwise extract it to the temp directory
                var fullPath =
                    Path.Combine(
                        Instance.ValidAudioExtensions.Contains(Path.GetExtension(entryFileName)) ? songDir : tempDir,
                        Path.GetFileName(entryFileName));

                var directoryName = Path.GetDirectoryName(fullPath);

                if (directoryName?.Length > 0)
                {
                    Directory.CreateDirectory(directoryName);
                    AuLogger.GetCurrentLogger<StationManager>("ExtractStationArchive")
                        .Info($"Created directory: {directoryName}");
                }

                if (!Directory.Exists(directoryName))
                {
                    AuLogger.GetCurrentLogger<StationManager>("ExtractStationArchive")
                        .Error($"Directory does not exist after creation: {directoryName}");
                    continue;
                }

                try
                {
                    // Extract the file
                    using var entryStream = entry.OpenEntryStream();
                    using var outputStream = File.Create(fullPath);
                    entryStream.CopyTo(outputStream);
                    AuLogger.GetCurrentLogger<StationManager>("ExtractStationArchive")
                        .Info($"Extracted file: {fullPath}");
                }
                catch (Exception ex)
                {
                    AuLogger.GetCurrentLogger<StationManager>("ExtractStationArchive")
                        .Error(ex, $"Failed to extract file: {fullPath}");
                }
            }

            return (tempDir, songDir);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationManager>("ExtractStationArchive")
                .Error(ex, "Error extracting station archive.");
            return (string.Empty, string.Empty);
        }
    }

    /// <summary>
    ///     Ensures the display name contains the station's FM number at the beginning of its name like so:
    ///     <c>00.00 Station Name</c>
    /// </summary>
    /// <param name="station">The station to ensure the display name of.</param>
    /// <returns>The parsed FM number from the original display name string; or, the original FM number if the same.</returns>
    public float EnsureDisplayNameFormat(TrackableObject<AdditionalStation> station)
    {
        return EnsureDisplayNameFormat(station, null);
    }

    /// <summary>
    ///     Ensures the display name contains the station's FM number at the beginning of its name like so:
    ///     <c>00.00 Station Name</c>
    /// </summary>
    /// <param name="station">The station to ensure the display name of.</param>
    /// <param name="optionalFmVal">
    ///     The FM number to ensure is in front of the station name. If null, the default FM value will
    ///     be used instead.
    /// </param>
    /// <returns>The parsed FM number from the original display name string; or, the original FM number if the same.</returns>
    public float EnsureDisplayNameFormat(TrackableObject<AdditionalStation> station, float? optionalFmVal)
    {
        var currentName = station.TrackedObject.MetaData.DisplayName;

        var regex = DisplayNameRegex();
        var match = regex.Match(currentName);
        var fmNumber = optionalFmVal ?? station.TrackedObject.MetaData.Fm;

        if (match.Success)
        {
            currentName = currentName[match.Length..].TrimStart();
            if (float.TryParse(match.Value, CultureInfo.InvariantCulture, out var fmNumberParsed) &&
                optionalFmVal == null)
                fmNumber = fmNumberParsed;
        }

        station.TrackedObject.MetaData.DisplayName = @$"{fmNumber} {currentName}";
        station.TrackedObject.MetaData.Fm = fmNumber;
        return fmNumber;
    }

    /// <summary>
    ///     Add a protected folder to the list of folders that should not be deleted during station synchronization.
    /// </summary>
    /// <param name="folder">The path to a folder to protect.</param>
    public void AddProtectedFolder(string folder)
    {
        if (!ProtectedStagingFolders.Contains(folder))
            ProtectedStagingFolders.Add(folder);

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
    }

    /// <summary>
    ///     Remove a protected folder from the list of folders that should not be deleted during station synchronization.
    /// </summary>
    /// <param name="folder">The path to a folder to remove from protection.</param>
    public void RemoveProtectedFolder(string folder)
    {
        if (ProtectedStagingFolders.Contains(folder))
            ProtectedStagingFolders.Remove(folder);
    }

    /// <summary>
    ///     Clear all protected folders from the list of folders that should not be deleted during station synchronization.
    /// </summary>
    public void ClearProtectedFolders()
    {
        ProtectedStagingFolders.Clear();
    }

    /// <summary>
    ///     Get a value indicating if the specified folder is protected from deletion during station synchronization.
    /// </summary>
    /// <param name="folder">The path to a folder.</param>
    /// <returns><c>true</c> if the folder is protected. <c>false</c>, otherwise.</returns>
    public bool IsProtectedFolder(string folder)
    {
        return ProtectedStagingFolders.Contains(folder);
    }

    #region Events

    /// <summary>
    ///     Event that is raised when a station is added to the manager. Event data is the station ID.
    /// </summary>
    public event EventHandler<Guid>? StationAdded;

    /// <summary>
    ///     Event that is raised when a vanilla station is updated in the manager. Event data is the station ID.
    /// </summary>
    public event EventHandler<Guid>? VanillaStationAdded;

    /// <summary>
    ///     Event that is raised when a station is removed from the manager. Event data is the station ID.
    /// </summary>
    public event EventHandler<Guid>? StationRemoved;

    /// <summary>
    ///     Event that is raised when a vanilla station is removed from the manager. Event data is the station ID.
    /// </summary>
    public event EventHandler<Guid>? VanillaStationRemoved;

    /// <summary>
    ///     Event that is raised when a station is updated in the manager. Event data is the station ID.
    /// </summary>
    public event EventHandler<Guid>? StationUpdated;

    /// <summary>
    ///     Event that is raised when a vanilla station is updated in the manager. Event data is the station ID.
    /// </summary>
    public event EventHandler<Guid>? VanillaStationUpdated;

    /// <summary>
    ///     Event that is raised when all stations are cleared from the manager.
    /// </summary>
    public event EventHandler? StationsCleared;

    /// <summary>
    ///     Event that is raised when all vanilla stations are cleared from the manager.
    /// </summary>
    public event EventHandler? VanillaStationsCleared;

    /// <summary>
    ///     Event that is raised when stations have finished importing from a directory or archive.
    /// </summary>
    public event EventHandler<List<Guid?>>? StationImported;

    /// <summary>
    ///     Event that is raised when a station name is found to be a duplicate. Event data is a tuple with the station ID and
    ///     the updated name.
    /// </summary>
    public event EventHandler<(Guid stationId, string updatedName)>? StationNameDuplicate;

    /// <summary>
    ///     Event that is raised when a vanilla station name is found to be a duplicate. Event data is a tuple with the station
    ///     ID and the updated name.
    /// </summary>
    public event EventHandler<(Guid stationId, string updatedName)>? VanillaStationNameDuplicate;

    /// <summary>
    ///     Event that is raised when the synchronization progress changes. Event data is the current progress percentage.
    /// </summary>
    public event Action<int>? SyncProgressChanged;

    /// <summary>
    ///     Event that is raised when the synchronization status changes. Event data is a message describing the status.
    /// </summary>
    public event Action<string>? SyncStatusChanged;

    /// <summary>
    ///     Event that is raised when stations have finished synchronizing from the game's radios folder to staging.
    ///     Event data is a flag indicating success.
    /// </summary>
    public event Action<bool>? StationsSynchronized;

    #endregion

    #region Static Members

    /// <summary>
    ///     The singleton instance of the <see cref="StationManager" /> class.
    /// </summary>
    private static StationManager? _instance;

    /// <summary>
    ///     The lock object used to synchronize access to the singleton instance.
    /// </summary>
    private static readonly object Lock = new();

    /// <summary>
    ///     The format string for the station count.
    /// </summary>
    private static string StationCountFormat => Strings.EnabledStationsCount;

    /// <summary>
    ///     The format string for the vanilla station count.
    /// </summary>
    private static string VanillaStationCountFormat => Strings.EnabledVanillaStationsCount;

    #endregion

    #region Private, Readonly Members

    /// <summary>
    ///     The dictionary of stations and editors managed by the manager. Each station has a unique ID which is used as the
    ///     key.
    ///     <para>Key: <c>Unique station ID</c></para>
    ///     <para>Value: Pair with following:</para>
    ///     <list type="bullet">
    ///         <item>Key: <c>Trackable station</c></item>
    ///         <item>Value: <c>List of all editor's associated with the station.</c></item>
    ///     </list>
    /// </summary>
    private readonly Dictionary<Guid, Pair<TrackableObject<AdditionalStation>, List<IEditor>>> _stations = [];

    /// <summary>
    ///     The dictionary of replacement stations and editors managed by the manager. Each replacement station has a unique ID
    ///     which is used as the key.
    ///     <para>Key: <c>Unique station ID</c></para>
    ///     <para>Value: Pair with following:</para>
    ///     <list type="bullet">
    ///         <item>Key: <c>Trackable replacement station</c></item>
    ///         <item>Value: <c>List of all editor's associated with the replacement station.</c></item>
    ///     </list>
    /// </summary>
    private readonly Dictionary<Guid, Pair<TrackableObject<ReplacementStation>, List<IEditor>>> _replacementStations =
        [];

    /// <summary>
    ///     The JSON object used to serialize and deserialize the metadata of a station.
    /// </summary>
    private readonly Json<MetaData> _metaDataJson = new();

    /// <summary>
    ///     The JSON object used to serialize and deserialize the list of songs.
    /// </summary>
    private readonly Json<List<Song>> _songListJson = new();

    /// <summary>
    ///     The JSON object used to serialize and deserialize the list of icons.
    /// </summary>
    private readonly Json<List<WolvenIcon>> _iconListJson = new();

    /// <summary>
    ///     The JSON object used to serialize and deserialize a replacement station.
    /// </summary>
    private readonly Json<ReplacementStation> _replacementStationJson = new();

    /// <summary>
    ///     List of new station IDs added to the manager during the current session. These stations should not be allowed to
    ///     revert changes unless saved to disk.
    ///     Upon exporting, the station IDs in this list should be removed by <see cref="ResetNewStations" />
    /// </summary>
    private readonly List<Guid> _newStations = [];

    private readonly List<string?> _customDataKeys = ["Icon Name", "Image Path", "Archive Path", "SHA256 Archive Hash"];

    #endregion

    #region Properties

    /// <summary>
    ///     Get the singleton instance of the <see cref="StationManager" /> class.
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
    ///     Get a list of valid audio file extensions for song files.
    /// </summary>
    public string?[] ValidAudioExtensions { get; } = EnumHelper<ValidAudioFiles>.GetEnumDescriptions() as string[] ??
                                                     EnumHelper<ValidAudioFiles>.GetEnumDescriptions().ToArray();

    /// <summary>
    ///     Get a list of valid archive file extensions for station icons.
    /// </summary>
    public string?[] ValidArchiveExtensions { get; } =
        EnumHelper<ValidArchiveFiles>.GetEnumDescriptions() as string[] ??
        EnumHelper<ValidArchiveFiles>.GetEnumDescriptions().ToArray();

    /// <summary>
    ///     Get a list of valid image file extensions for station icons.
    /// </summary>
    public string?[] ValidImageExtensions { get; } = EnumHelper<ValidImageFiles>.GetEnumDescriptions() as string[] ??
                                                     EnumHelper<ValidImageFiles>.GetEnumDescriptions().ToArray();

    /// <summary>
    ///     The current list of stations managed by the manager as a binding list. Auto-updates when stations are added or
    ///     removed.
    /// </summary>
    public BindingList<TrackableObject<AdditionalStation>> StationsAsBindingList { get; } = [];

    /// <summary>
    ///     The current list of stations managed by the manager as a list.
    /// </summary>
    public List<TrackableObject<AdditionalStation>> StationsAsList =>
        _stations.Values.Select(pair => pair.Key).ToList();

    /// <summary>
    ///     The current list of replacement stations managed by the manager as a binding list. Auto-updates when replacement
    ///     stations are added or removed.
    /// </summary>
    public BindingList<TrackableObject<ReplacementStation>> ReplacementStationsAsBindingList { get; } = [];

    /// <summary>
    ///     The current list of replacement stations managed by the manager as a list.
    /// </summary>
    public List<TrackableObject<ReplacementStation>> ReplacementStationsAsList =>
        _replacementStations.Values.Select(pair => pair.Key).ToList();

    /// <summary>
    ///     The list of protected staging folders that should not be deleted. Normally, this contains at least the path to the
    ///     "icons" folder.
    /// </summary>
    private List<string> ProtectedStagingFolders { get; } = [];

    /// <summary>
    ///     A dictionary of station IDs and their relative paths in the staging directory.
    /// </summary>
    private Dictionary<Guid, string> StationPaths { get; } = [];

    /// <summary>
    ///     Get a value indicating whether the station manager is empty (i.e., has no stations).
    /// </summary>
    public bool IsEmpty => (_stations.Count == 0) & (_replacementStations.Count == 0);

    /// <summary>
    ///     Regular expression that matches a display name with an optional FM number at the start.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^\d+(\.\d+)?\s*")]
    private static partial Regex DisplayNameRegex();

    #endregion
}
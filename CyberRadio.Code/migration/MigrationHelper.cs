// MigrationHelper.cs : RadioExt-Helper
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

using System.Xml;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using Newtonsoft.Json.Linq;
using RadioExt_Helper.config;
using RadioExt_Helper.models;

namespace RadioExt_Helper.migration;

/// <summary>
/// This class helps with migrating from the old user.config file to the new config.yml file. The previous application version used the
/// built-in .NET settings file to handle application settings but is too limited for our needs. The new version uses a custom YAML file that
/// can be extended more easily across application versions.
/// </summary>
public static class MigrationHelper
{
    private static readonly string BaseDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "RadioExt-Helper");

    #region Settings Migration

    /// <summary>
    /// Migrate the settings from the old user.config file to the new config.yml file. This method will look for all user.config files in the
    /// old application data directory and parse the most recent one. The settings will then be migrated to a new <see cref="ApplicationConfig"/> object
    /// and returned. If no user.config files are found, the method will return null.
    /// </summary>
    /// <returns>The migrated configuration; or <c>null</c> if no previous setting files were found.</returns>
    public static ApplicationConfig? MigrateSettings()
    {
        ApplicationConfig? appConfig = null;

        var userConfigPaths = FindAllUserConfigFiles(BaseDirectory);

        if (userConfigPaths.Count == 0) //No settings need to be migrated
        {
            AuLogger.GetCurrentLogger("MigrationHelper.MigrateSettings")
                .Info("No user.config files found. No migration necessary.");
            return appConfig;
        }

        AuLogger.GetCurrentLogger("MigrationHelper.FindAllUserConfigFiles")
            .Info($"{userConfigPaths.Count} user.config files have been found.");

        var mostRecentUserConfigPath = GetMostRecentUserConfigFile(userConfigPaths);

        if (!string.IsNullOrEmpty(mostRecentUserConfigPath))
        {
            appConfig = ParseUserConfig(mostRecentUserConfigPath);
            AuLogger.GetCurrentLogger("MigrationHelper.MigrateSettings")
                .Info("Old settings have been parsed and migrated.");
        }
        else
        {
            AuLogger.GetCurrentLogger("MigrationHelper.MigrateSettings").Info("No recent user.config files found.");
            return appConfig;
        }

        CleanOldSettings();
        return appConfig;
    }

    /// <summary>
    /// Recursively finds all user.config files in the specified directory and its subdirectories.
    /// </summary>
    /// <param name="baseDirectory">The base directory to search for user.config files.</param>
    /// <returns>A list of paths to user.config files.</returns>
    private static List<string> FindAllUserConfigFiles(string baseDirectory)
    {
        List<string> userConfigPaths = [];

        foreach (var directory in FileHelper.SafeEnumerateDirectories(baseDirectory))
        {
            var userConfigPath = Path.Combine(directory, "user.config");
            if (File.Exists(userConfigPath))
                userConfigPaths.Add(userConfigPath);
            else
                userConfigPaths.AddRange(FindAllUserConfigFiles(directory));
        }

        return userConfigPaths;
    }

    /// <summary>
    /// Gets the most recent user.config file from the list of user.config paths.
    /// </summary>
    /// <param name="userConfigPaths">The list of user.config paths.</param>
    /// <returns>The path to the most recent user.config file; or <c>null</c> if an error occurred.</returns>
    private static string? GetMostRecentUserConfigFile(List<string> userConfigPaths)
    {
        try
        {
            return userConfigPaths.MaxBy(
                path => Directory.GetLastWriteTime(Path.GetDirectoryName(path) ?? string.Empty));
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger("MigrationHelper.GetMostRecentUserConfigFile")
                .Error(e, "Error while getting the most recent user.config file.");
            return null;
        }
    }

    /// <summary>
    /// Parses the user.config file and creates an <see cref="ApplicationConfig"/> object.
    /// </summary>
    /// <param name="userConfigPath">The path to the user.config file.</param>
    /// <returns>The parsed <see cref="ApplicationConfig"/> object; or <c>null</c> if an error occurred.</returns>
    private static ApplicationConfig? ParseUserConfig(string userConfigPath)
    {
        try
        {
            XmlDocument xmlDoc = new();
            xmlDoc.Load(userConfigPath);

            var config = new ApplicationConfig
            {
                Language = xmlDoc.SelectSingleNode("//setting[@name='SelectedLanguage']/value")?.InnerText ??
                           "English (en)",
                GameBasePath = xmlDoc.SelectSingleNode("//setting[@name='GameBasePath']/value")?.InnerText ??
                               string.Empty,
                StagingPath = xmlDoc.SelectSingleNode("//setting[@name='StagingPath']/value")?.InnerText ?? string.Empty
            };

            var windowSize = xmlDoc.SelectSingleNode("//setting[@name='WindowSize']/value")?.InnerText.Split(',');

            if (int.TryParse(windowSize?[0], out var width) && int.TryParse(windowSize[1], out var height))
                config.WindowSize = new WindowSize(width, height);
            else
                throw new Exception("Error while parsing the window size.");

            return config;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("MigrationHelper.ParseUserConfig")
                .Error(ex, "Error while parsing the user.config file.");
            return null;
        }
    }

    /// <summary>
    /// Cleans up the old settings by deleting unnecessary directories and files.
    /// </summary>
    private static void CleanOldSettings()
    {
        var baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RadioExt-Helper");

        var configFilePath = Path.Combine(baseDirectory, "config.yml");
        var logsFolderPath = Path.Combine(baseDirectory, "logs");

        var directoriesToKeep = new[] { configFilePath, logsFolderPath };

        // Get all directories under the base directory
        var allDirectories = FileHelper.SafeEnumerateDirectories(baseDirectory);

        foreach (var directory in allDirectories)
        {
            // Skip the directories to keep
            if (directoriesToKeep.Any(dir => directory.StartsWith(dir)))
                continue;

            // Recursively delete the directory
            Directory.Delete(directory, true);
        }

        AuLogger.GetCurrentLogger("MigrationHelper.CleanOldSettings")
            .Info("Old setting directories and files have been removed.");
    }

    #endregion

    #region Song JSON Migration

    /// <summary>
    /// Migrate the songs.sgls files from the old JSON format to the new <see cref="Song"/> object format.
    /// </summary>
    /// <param name="stagingPath">The path to the staging folder.</param>
    /// <returns>A list of status messages.</returns>
    public static List<string> MigrateSongs(string stagingPath)
    {
        var statusMessages = new List<string>();

        if (stagingPath.Equals(string.Empty))
        {
            const string warningMessage = "Staging path was empty! No songs can be migrated.";
            statusMessages.Add(warningMessage);
            return statusMessages;
        }

        Json<List<Song>> jsonSerializer = new();
        Json<List<dynamic>> oldSongDeserializer = new();

        var songsFiles = FindAllSongsFiles(stagingPath);

        foreach (var songsFile in songsFiles)
            try
            {
                if (IsSongNewFormat(songsFile))
                {
                    var infoMessage = $"File {songsFile} is already in the new format. Skipping migration...";
                    statusMessages.Add(infoMessage);
                    continue;
                }

                // Read old JSON file
                var oldSongs = oldSongDeserializer.LoadJson(songsFile);

                // Transform to new format
                var newSongs = oldSongs?.Select(oldSong => new Song
                {
                    Title = oldSong.name,
                    Artist = oldSong.artist,
                    Duration = TimeSpan.Parse(oldSong.duration.ToString()),
                    FileSize = (ulong)oldSong.size,
                    FilePath = oldSong.original_path
                }).ToList();

                if (newSongs != null)
                {
                    if (jsonSerializer.SaveJson(songsFile, newSongs))
                    {
                        var successMessage = $"Songs in {songsFile} have been migrated to the new format.";
                        statusMessages.Add(successMessage);
                    }
                    else
                    {
                        var errorMessage =
                            $"Error while migrating songs in {songsFile}. Could not save new song object to disk.";
                        statusMessages.Add(errorMessage);
                    }
                }
                else
                {
                    var errorMessage =
                        $"Error while migrating songs in {songsFile}. Could not parse old song's format.";
                    statusMessages.Add(errorMessage);
                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = $"Error while migrating songs in {songsFile}: {ex.Message}";
                statusMessages.Add(exceptionMessage);
            }

        return statusMessages;
    }

    /// <summary>
    /// Find all <c>songs.sgls</c> files in the specified directory and its subdirectories.
    /// </summary>
    /// <param name="baseDirectory">The base directory to search.</param>
    /// <returns>A list of paths to <c>songs.sgls</c> files.</returns>
    private static List<string> FindAllSongsFiles(string baseDirectory)
    {
        return FileHelper.SafeEnumerateFiles(baseDirectory, "songs.sgls", SearchOption.AllDirectories).ToList();
    }

    /// <summary>
    /// Check if the song file is already in the new format.
    /// </summary>
    /// <param name="jsonPath">The path to the songs.sgls JSON file to check.</param>
    /// <returns><c>true</c> if the file is already in new format; <c>false</c> otherwise.</returns>
    private static bool IsSongNewFormat(string jsonPath)
    {
        try
        {
            var jsonArray = JArray.Parse(FileHelper.OpenFile(jsonPath, false));

            // Check if the first object contains the key "title"
            if (jsonArray.Count > 0 && jsonArray[0]["title"] != null) return true;
        }
        catch
        {
            // If there's an exception, it means the content is not a valid JSON array in the new format
            return false;
        }

        return false;
    }

    #endregion
}
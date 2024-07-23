// PathHelper.cs : RadioExt-Helper
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

using AetherUtils.Core.Logging;
using RadioExt_Helper.forms;

namespace RadioExt_Helper.utility;

/// <summary>
///     Helper class to get the various paths associated with the game.
/// </summary>
public static class PathHelper
{
    /// <summary>
    ///     Retrieves the base game path (the folder containing <c>bin</c>) from the Cyberpunk 2077 executable.
    /// </summary>
    /// <param name="shouldLoop">Indicates whether the file dialog should continue showing until a valid file is selected.</param>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string? GetGamePath(bool shouldLoop = false)
    {
        OpenFileDialog dialog = new()
        {
            Filter = @"Game Executable|Cyberpunk2077.exe",
            Title = GlobalData.Strings.GetString("Open") ?? "Open Game Executable"
        };

        try
        {
            var gamePath = string.Empty;
            do
            {
                if (dialog.ShowDialog() != DialogResult.OK) continue;

                if (dialog.FileName.Contains("Cyberpunk2077"))
                    gamePath = dialog.FileName;
                else
                    MessageBox.Show(GlobalData.Strings.GetString("NonCyberpunkExe"));
            } while (gamePath.Equals(string.Empty) && shouldLoop);

            var name = Directory.GetParent(gamePath)?.FullName;
            if (name != null)
            {
                var fullName = Directory.GetParent(name)?.FullName;
                if (fullName != null)
                {
                    var basePath = Directory.GetParent(fullName)?.FullName;
                    return basePath;
                }
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetGamePath")
                .Error(ex, "Error retrieving base game path.");
            return string.Empty;
        }

        return string.Empty;
    }

    /// <summary>
    ///     Retrieves the staging path (the folder containing radio stations before copied to the game).
    /// </summary>
    /// <param name="shouldLoop">Indicates whether the file dialog should continue showing until a valid file is selected.</param>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string GetStagingPath(bool shouldLoop = false)
    {
        FolderBrowserDialog dialog = new()
        {
            Description = GlobalData.Strings.GetString("StagingPathHelp") ?? "Select the radio station staging path.",
            UseDescriptionForTitle = true
        };

        try
        {
            var stagingPath = string.Empty;
            do
            {
                if (dialog.ShowDialog() != DialogResult.OK) continue;

                stagingPath = dialog.SelectedPath;
            } while (stagingPath.Equals(string.Empty) && shouldLoop);

            return stagingPath;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetStagingPath")
                .Error(ex, "Error retrieving staging path.");
            return string.Empty;
        }
    }

    /// <summary>
    ///     Checks for and retrieves (if it exists) the path to the <c>radioExt</c> folder where custom radios are placed.
    /// </summary>
    /// <returns>The path to the radioExt folder, or <see cref="string.Empty" /> if the path couldn't be determined.</returns>
    public static string GetRadioExtPath(string gameBasePath)
    {
        var path = Path.Combine(gameBasePath, "bin", "x64", "plugins", "cyber_engine_tweaks", "mods", "radioExt");
        try
        {
            return Path.Exists(path) ? path : string.Empty;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetRadioExtPath")
                .Error(ex, "Error retrieving path to radioExt mod's folder.");
            return string.Empty;
        }
    }

    /// <summary>
    ///     Get the path to the <c>radios</c> folder under the radioExt mod folder. This method does not check if the path
    ///     exists.
    /// </summary>
    /// <param name="gameBasePath">The base path of the game.</param>
    /// <returns>The path to the <c>radios</c> folder, based on the base path of the game.</returns>
    public static string GetRadiosPath(string gameBasePath)
    {
        return Path.Combine(GetRadioExtPath(gameBasePath), "radios");
    }

    /// <summary>
    /// Determines if a path is a sub-path (i.e., starts with) of another path.
    /// </summary>
    /// <param name="basePath">The base path to check against.</param>
    /// <param name="subPath">The path to check against the base path.</param>
    /// <returns><c>true</c> if the sub path is part of the base path; <c>false</c> otherwise or if an error occured.</returns>
    public static bool IsSubPath(string basePath, string subPath)
    {
        try
        {
            // Check if the paths are valid
            if (string.IsNullOrEmpty(basePath) || string.IsNullOrEmpty(subPath) || !Directory.Exists(basePath) ||
                !Directory.Exists(subPath))
                return false;

            // Get the full paths
            var fullBasePath = Path.GetFullPath(basePath);
            var fullSubPath = Path.GetFullPath(subPath);

            // Normalize directory separators
            fullBasePath = fullBasePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) +
                           Path.DirectorySeparatorChar;
            fullSubPath = fullSubPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) +
                          Path.DirectorySeparatorChar;

            // Check if the fullSubPath starts with fullBasePath
            return fullSubPath.StartsWith(fullBasePath, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.IsSubPath")
                .Error(ex, "An error occurred while checking if the path is a subpath.");
            return false;
        }
    }
}
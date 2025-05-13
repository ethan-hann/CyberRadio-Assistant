// ApplicationConfig.cs : RadioExt-Helper
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

using System.ComponentModel;
using System.Security;
using AetherUtils.Core.Attributes;
using AetherUtils.Core.Configuration;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.config;

/// <summary>
///     Represents the configuration for the application; replaces the need for a settings file.
///     All present and future configuration options will be managed via this class.
/// </summary>
public sealed class ApplicationConfig
{
    /// <summary>
    ///   Specifies whether the application should automatically check for updates on startup.
    /// </summary>
    [Config("autoCheckForUpdates", "Specifies whether the application should automatically check for updates on startup.", true)]
    [Description("CheckForUpdatesOptionHelp")]
    public bool AutoCheckForUpdates { get; set; }

    /// <summary>
    ///   Specifies whether the application is running for the first time.
    /// </summary>
    [Config("isFirstRun", "Specifies whether the application is running for the first time.", true)]
    [Description("IsFirstRunOptionHelp")]
    public bool IsFirstRun { get; set; }

    /// <summary>
    ///  Specifies whether the application should automatically export the stations to the game directory after exporting to staging.
    /// </summary>
    [Config("autoExportToGame", "Specifies whether the application should automatically export the stations to the game directory after exporting to staging.")]
    [Description("AutoExportOptionHelp")]
    public bool AutoExportToGame { get; set; }

    /// <summary>
    ///  Specifies whether the application should automatically watch for changes in the game's radios directory.
    /// </summary>
    [Config("watchForGameChanges", "Specifies whether the application should automatically watch for changes in the game's radios directory.", true)]
    [Description("WatchForChangesHelp")]
    public bool WatchForGameChanges { get; set; }

    /// <summary>
    ///  Specifies whether the application should automatically copy the song files when creating a backup of the staging folder.
    /// </summary>
    [Config("copySongFilesToBackup", "Specifies whether the application should copy the song files when creating a backup of the staging folder.", true)]
    [Description("CopySongFilesToBackupHelp")]
    public bool CopySongFilesToBackup { get; set; }

    /// <summary>
    ///  Specifies the default location for song files that have been imported from a station .zip or .rar file.
    /// </summary>
    [Config("defaultSongLocation", "Specifies the default location for song files that have been imported from a station .zip or .rar file.")]
    [Description("DefaultSongLocationHelp")]
    public string DefaultSongLocation { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

    /// <summary>
    /// Specifies the backup compression level to use when zipping the staging folder.
    /// </summary>
    [Config("backupCompressionLevel", "Specifies the backup compression level to use when zipping the staging folder.",
        CompressionLevel.Normal)]
    [Description("BackupCompressionLevelHelp")]
    public CompressionLevel BackupCompressionLevel { get; set; } = CompressionLevel.Normal;

    /// <summary>
    /// Specifies the path to the staging directory where the radio stations are temporarily stored.
    /// </summary>
    [Config("stagingPath", "The path to the staging directory where the radio stations are temporarily stored.", "")]
    public string StagingPath { get; set; } = string.Empty;

    /// <summary>
    /// Specifies the path to the game directory where the radio stations are exported.
    /// </summary>
    [Config("gameBasePath", "The path to the game directory where the radio stations are exported.", "")]
    public string GameBasePath { get; set; } = string.Empty;

    /// <summary>
    /// The currently selected language for the application.
    /// </summary>
    [Config("language", "The currently selected language for the application.", "English (en)")]
    public string Language { get; set; } = "English (en)";

    /// <summary>
    /// The window size of the application.
    /// </summary>
    [Config("windowSize", "The window size of the application.")]
    public WindowSize WindowSize { get; set; } = new();

    /// <summary>
    /// The encrypted API key for accessing the Nexus API.
    /// </summary>
    [Config("nexusApiKey", "The encrypted API key for accessing the Nexus API.", "")]
    public string NexusApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The list of forbidden keywords and whether they are enabled or not.
    /// </summary>
    [Config("forbiddenKeywords", "The list of forbidden keywords and whether they are enabled or not.")]
    [Description("ForbiddenPathsHelp")]
    public List<ForbiddenKeyword> ForbiddenKeywords { get; set; } =
    [
        new("Game Launchers", "steam", true), new("Game Launchers", "steamapps", true),
        new("Game Launchers", "common", true), new("Game Launchers", "userdata", true),
        new("Game Launchers", "gog", true), new("Game Launchers", "gog galaxy", true),
        new("Game Launchers", "galaxyclient", true), new("Game Launchers", "epic", true),
        new("Game Launchers", "epic games", true), new("Game Launchers", "epicgames", true),
        new("Game Launchers", "origin", true), new("Game Launchers", "electronic arts", true),
        new("Game Launchers", "ea games", true), new("Game Launchers", "ubisoft", true),
        new("Game Launchers", "ubisoft connect", true), new("Game Launchers", "uplay", true),
        new("Game Launchers", "battlenet", true), new("Game Launchers", "blizzard", true),
        new("Game Launchers", "warcraft", true), new("Game Launchers", "starcraft", true),
        new("Game Launchers", "overwatch", true), new("Game Launchers", "riot games", true),
        new("Game Launchers", "league of legends", true), new("Game Launchers", "valorant", true),
        new("Game Launchers", "riotclient", true), new("Game Launchers", "rockstar games", true),
        new("Game Launchers", "rockstar launcher", true), new("Game Launchers", "bethesda", true),
        new("Game Launchers", "bethesda.net", true), new("Mod Managers", "vortex", true),
        new("Mod Managers", "nexusmods", true), new("Mod Managers", "vortex staging", true),
        new("Mod Managers", "modorganizer", true), new("Mod Managers", "mo2", true),
        new("Windows Related", "windowsapps", true), new("Windows Related", "microsoft games", true),
        new("Windows Related", "xbox", true), new("Windows Related", "xbox games", true)
    ];

    /// <summary>
    /// The log options for the application.
    /// </summary>
    [Config("logOptions", "The log options for the application.")]
    public LogOptions LogOptions { get; set; } = new()
    {
        AppName = "CyberRadioAssistant",
        WriteLogToConsole = false,
        NewFileEveryLaunch = true,
        LogFileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RadioExt-Helper", "logs"),
        LogLayout = "${longdate}|${level:uppercase=true}|${logger}|${message:withexception=true}",
        LogHeader = SystemInfo.GetLogFileHeader(),
        IncludeDateTime = true,
        IncludeDateOnly = false
    };
}
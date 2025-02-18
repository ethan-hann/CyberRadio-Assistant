// ApplicationConfig.cs : RadioExt-Helper
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

using AetherUtils.Core.Attributes;
using AetherUtils.Core.Configuration;
using RadioExt_Helper.utility;
using System.ComponentModel;

namespace RadioExt_Helper.config;

/// <summary>
///     Represents the configuration for the application; replaces the need for a settings file.
///     All present and future configuration options will be managed via this class.
///     <para>
///         <remarks>
///             The <see cref="ConfigAttribute"/> is used to specify the key for each property in the configuration file.
///             The <see cref="DescriptionAttribute"/> is used to provide a localizable help description for each property.
///         </remarks>    
///     </para>
/// </summary>
public sealed class ApplicationConfig
{
    // Constants for config keys
    private const string AutoCheckForUpdatesKey = "autoCheckForUpdates";
    private const string IsFirstRunKey = "isFirstRun";
    private const string AutoExportToGameKey = "autoExportToGame";
    private const string WatchForGameChangesKey = "watchForGameChanges";
    private const string CopySongFilesToBackupKey = "copySongFilesToBackup";
    private const string DefaultSongLocationKey = "defaultSongLocation";
    private const string BackupCompressionLevelKey = "backupCompressionLevel";
    private const string StagingPathKey = "stagingPath";
    private const string GameBasePathKey = "gameBasePath";
    private const string LanguageKey = "language";
    private const string WindowSizeKey = "windowSize";
    private const string NexusApiKeyKey = "nexusApiKey";
    private const string ForbiddenKeywordsKey = "forbiddenKeywords";
    private const string LogOptionsKey = "logOptions";

    /// <summary>
    ///     Specifies whether the application should automatically check for updates on startup.
    /// </summary>
    [Config(AutoCheckForUpdatesKey)]
    [Description("CheckForUpdatesOptionHelp")]
    public bool AutoCheckForUpdates { get; set; } = true;

    /// <summary>
    /// Specifies whether the application is running for the first time.
    /// </summary>
    [Config(IsFirstRunKey)]
    public bool IsFirstRun { get; set; } = true;

    /// <summary>
    ///     Specifies whether the application should automatically export the stations to the game
    ///     directory after exporting to staging.
    /// </summary>
    [Config(AutoExportToGameKey)]
    [Description("AutoExportOptionHelp")]
    public bool AutoExportToGame { get; set; }

    /// <summary>
    ///     Specifies whether the application should automatically watch for changes in the game's radios directory.
    /// </summary>
    [Config(WatchForGameChangesKey)]
    [Description("WatchForChangesHelp")]
    public bool WatchForGameChanges { get; set; } = true;

    /// <summary>
    ///    Specifies whether the application should copy the song files when creating a backup of the staging folder.
    /// </summary>
    [Config(CopySongFilesToBackupKey)]
    [Description("CopySongFilesToBackupHelp")]
    public bool CopySongFilesToBackup { get; set; } = true;

    /// <summary>
    /// Specifies the default location for song files that have been imported from a station .zip or .rar file.
    /// </summary>
    [Config(DefaultSongLocationKey)]
    [Description("DefaultSongLocationHelp")]
    public string DefaultSongLocation { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

    /// <summary>
    ///     Specifies the backup compression level to use when zipping the staging folder.
    /// </summary>
    [Config(BackupCompressionLevelKey)]
    [Description("BackupCompressionLevelHelp")]
    public CompressionLevel BackupCompressionLevel { get; set; } = CompressionLevel.Normal;

    /// <summary>
    ///     The path to the staging directory.
    /// </summary>
    [Config(StagingPathKey)]
    public string StagingPath { get; set; } = string.Empty;

    /// <summary>
    ///     The path to the game directory.
    /// </summary>
    [Config(GameBasePathKey)]
    public string GameBasePath { get; set; } = string.Empty;

    /// <summary>
    ///     The selected language for the application.
    /// </summary>
    [Config(LanguageKey)]
    public string Language { get; set; } = "English (en)";

    /// <summary>
    ///     The window size of the application.
    /// </summary>
    [Config(WindowSizeKey)]
    public WindowSize WindowSize { get; set; } = new();

    /// <summary>
    /// The API key the user has entered for accessing the Nexus API.
    /// </summary>
    [Config(NexusApiKeyKey)]
    public string NexusApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The list of forbidden keywords and whether they are enabled or not.
    /// </summary>
    [Config(ForbiddenKeywordsKey)]
    [Description("ForbiddenPathsHelp")]
    public List<ForbiddenKeyword> ForbiddenKeywords { get; set; } =
    [
        new("Game Launchers", "steam", true), new("Game Launchers", "steamapps", true), new("Game Launchers","common", true),
        new("Game Launchers", "userdata", true), new("Game Launchers","gog", true), new("Game Launchers","gog galaxy", true),
        new("Game Launchers","galaxyclient", true), new("Game Launchers", "epic", true), new("Game Launchers", "epic games", true),
        new("Game Launchers","epicgames", true), new("Game Launchers","origin", true), new("Game Launchers","electronic arts", true),
        new("Game Launchers","ea games", true), new("Game Launchers", "ubisoft", true), new("Game Launchers","ubisoft connect", true),
        new("Game Launchers", "uplay", true), new("Game Launchers","battlenet", true), new("Game Launchers","blizzard", true),
        new("Game Launchers","warcraft", true), new("Game Launchers","starcraft", true), new("Game Launchers","overwatch", true),
        new("Game Launchers","riot games", true), new("Game Launchers","league of legends", true), new("Game Launchers","valorant", true),
        new("Game Launchers","riotclient", true), new("Game Launchers","rockstar games", true), new("Game Launchers","rockstar launcher", true),
        new("Game Launchers","bethesda", true), new("Game Launchers","bethesda.net", true),
        new("Mod Managers", "vortex", true), new("Mod Managers","nexusmods", true), new("Mod Managers","vortex staging", true),
        new("Mod Managers","modorganizer", true), new("Mod Managers","mo2", true),
        new("Windows Related", "windowsapps", true), new("Windows Related","microsoft games", true), new("Windows Related","xbox", true),
        new("Windows Related","xbox games", true)
    ];

    /// <summary>
    ///     The log options for the application.
    /// </summary>
    [Config(LogOptionsKey)]
    public LogOptions LogOptions { get; set; } = new()
    {
        AppName = "CyberRadioAssistant",
        WriteLogToConsole = false,
        NewFileEveryLaunch = true,
        LogFileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RadioExt-Helper", "logs"),
        LogHeader = SystemInfo.GetLogFileHeader(),
        IncludeDateTime = true,
        IncludeDateOnly = false
    };
}
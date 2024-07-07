using AetherUtils.Core.Attributes;
using AetherUtils.Core.Configuration;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.config;

/// <summary>
///     A DTO representing the configuration for the application; replaces the need for a settings file. All present and future configuration options
///     will be done via this class.
/// </summary>
public sealed class CyberConfig
{
    /// <summary>
    ///     Specifies whether the application should automatically check for updates on startup.
    /// </summary>
    [Config("autoCheckForUpdates")]
    public bool AutoCheckForUpdates { get; set; } = true;

    /// <summary>
    ///     Specifies whether the application should automatically export the stations to the game directory after exporting to
    ///     staging.
    /// </summary>
    [Config("autoExportToGame")]
    public bool AutoExportToGame { get; set; }

    /// <summary>
    ///     The path to the staging directory.
    /// </summary>
    [Config("stagingPath")]
    public string StagingPath { get; set; } = string.Empty;

    /// <summary>
    ///     The path to the game directory.
    /// </summary>
    [Config("gameBasePath")]
    public string GameBasePath { get; set; } = string.Empty;

    /// <summary>
    ///     The selected language for the application.
    /// </summary>
    [Config("language")]
    public string Language { get; set; } = "English (en)";

    /// <summary>
    ///     The window size of the application.
    /// </summary>
    [Config("windowSize")]
    public WindowSize WindowSize { get; set; } = new();

    /// <summary>
    ///     The log options for the application.
    /// </summary>
    [Config("logOptions")]
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

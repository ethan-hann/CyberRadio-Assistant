// CyberConfig.cs : RadioExt-Helper
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

namespace RadioExt_Helper.config;

/// <summary>
///     A DTO representing the configuration for the application; replaces the need for a settings file. All present and
///     future configuration options
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
    public bool AutoExportToGame { get; set; } = false;

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
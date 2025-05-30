﻿// GlobalData.cs : RadioExt-Helper
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
using System.Globalization;
using System.Reflection;
using System.Resources;
using AetherUtils.Core.Configuration;
using AetherUtils.Core.Logging;
using RadioExt_Helper.config;
using RadioExt_Helper.forms;

namespace RadioExt_Helper.utility;

/// <summary>
///     The `GlobalData` class is a utility class that provides global data and functionality used throughout the
///     application.
///     It contains static properties and methods for managing resources, configuration, logging, and culture settings.
/// </summary>
public static class GlobalData
{
    private const string ConfigFileName = "config.yml";
    private const string DefaultLanguage = "English (en)";

    /// <summary>
    /// The version of the application.
    /// </summary>
    public static readonly Version AppVersion =
        Assembly.GetExecutingAssembly().GetName().Version is { } v
            ? new Version(v.Major, v.Minor, v.Build)
            : new Version(0, 0, 0); //This should never happen, but just in case!

    /// <summary>
    /// Get the resource manager for accessing string resources.
    /// </summary>
    public static readonly ResourceManager Strings = new("RadioExt_Helper.Strings", typeof(MainForm).Assembly);

    private static readonly string ConfigFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "RadioExt-Helper", ConfigFileName);

    /// <summary>
    ///     Indicates whether the global data has been initialized.
    /// </summary>
    private static bool _globalDataInitialized;

    private static bool _uiIconsInitialized;

    /// <summary>
    /// Get the configuration manager responsible for managing the application configuration.
    /// </summary>
    public static ConfigManager<ApplicationConfig> ConfigManager { get; private set; } = null!;

    private static BindingList<string> UiIcons { get; set; } = [];
    private static ComboBox? UiIconsComboTemplate { get; set; }
    private static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();

    /// <summary>
    ///     Initializes the global data. This method is responsible for initializing the global data used throughout the
    ///     application.
    ///     It performs the following tasks:
    ///     <list type="bullet">
    ///         <item>Loads UI icons</item>
    ///         <item>Initializes the configuration</item>
    ///         <item>Initializes logging</item>
    ///         <item>Sets the culture based on the language configuration</item>
    ///     </list>
    /// </summary>
    public static void Initialize()
    {
        if (_globalDataInitialized) return;
        ConfigManager = ConfigManagerFactory.CreateAndLoad<ApplicationConfig>(ConfigFilePath);

        LoadUiIcons();

        Exception? e = null;
        try
        {
            // Always update the log header and save
            ConfigManager.Set("logHeader", SystemInfo.GetLogFileHeader());
            ConfigManager.Save();
        }
        catch (Exception ex)
        {
            e = ex;
            MessageBox.Show(Strings.GetString("ConfigError"), Strings.GetString("Error"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        InitializeLogging();

        if (e != null)
            AuLogger.GetCurrentLogger(nameof(GlobalData)).Error(e, "Error initializing configuration.");

        SetCulture(ConfigManager.Get("language") as string ?? DefaultLanguage);
        _globalDataInitialized = true;
    }

    /// <summary>
    /// Initializes the ComboBox template with default properties.
    /// </summary>
    public static void InitializeComboBoxTemplate()
    {
        UiIconsComboTemplate = CreateComboBoxTemplate();
    }

    /// <summary>
    ///     Retrieves the path to the containing log folder.
    /// </summary>
    /// <returns>The log folder path. If log folder path is not found in the config, the default log folder path is returned.</returns>
    public static string GetLogFolderPath()
    {
        var options = ConfigManager.Get("logOptions") as LogOptions;
        return options?.LogFileDirectory ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RadioExt-Helper", "logs");
    }

    /// <summary>
    /// Retrieves the full path to the most recent log file.
    /// </summary>
    /// <returns>The full path to the most recent log file, or null if no log files are found.</returns>
    public static string GetLogFilePath()
    {
        var options = ConfigManager.Get("logOptions") as LogOptions;
        var folderPath = GetLogFolderPath();

        // Get all log files in the directory
        var logFiles = Directory.GetFiles(folderPath, $"{options?.AppName}_*.log", SearchOption.TopDirectoryOnly);

        // Check if there are no log files
        if (logFiles.Length == 0)
            throw new Exception("No log file found in `logs` directory. There should be one at this point!");

        // Return the file with the most recent last write time
        var mostRecentLogFile = logFiles.OrderByDescending(File.GetLastWriteTime).FirstOrDefault();

        return mostRecentLogFile ?? string.Empty;
    }

    /// <summary>
    ///     Initializes the logging for the application. This method is responsible for configuring and initializing the
    ///     logging system used throughout the application.
    ///     It performs the following tasks:
    ///     <list type="bullet">
    ///         <item>Gets the log options from the configuration</item>
    ///         <item>Initializes the logging system using the log options</item>
    ///     </list>
    /// </summary>
    private static void InitializeLogging()
    {
        if (ConfigManager.Get("logOptions") is not LogOptions options) return;

        AuLogger.Initialize(options);

        //Grant full access to the application to the log file and directory
        PathHelper.GrantAccess(options.LogFileDirectory);
        PathHelper.GrantAccess(GetLogFolderPath());

        AuLogger.GetCurrentLogger(nameof(GlobalData)).Info("Logging initialized!");
    }

    /// <summary>
    /// Creates a ComboBox template with default properties. Data source is set to
    /// <see cref="GlobalData.UiIcons" />
    /// </summary>
    /// <returns>
    ///     A ComboBox instance with the following properties:
    ///     <list type="bullet">
    ///         <item>Font: Segoe UI, 9pt, Bold</item>
    ///         <item>BackColor: White</item>
    ///         <item>Font: Segoe UI, 9pt, Bold</item>
    ///         <item>DropDownStyle: DropDown</item>
    ///         <item>Name: UiIconsComboTemplate</item>
    ///         <item>Anchor: Left | Right</item>
    ///         <item>Dock: None</item>
    ///         <item>AutoCompleteMode: Suggest</item>
    ///         <item>AutoCompleteSource: ListItems</item>
    ///         <item>DataSource: UiIcons</item>
    ///         <item>DisplayMember: "Name"</item>
    ///         <item>MaxDropDownItems: 24</item>
    ///     </list>
    /// </returns>
    private static ComboBox CreateComboBoxTemplate()
    {
        return new ComboBox
        {
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.White,
            DropDownStyle = ComboBoxStyle.DropDown,
            Name = "UiIconsComboTemplate",
            Anchor = AnchorStyles.Left | AnchorStyles.Right,
            Dock = DockStyle.None,
            AutoCompleteMode = AutoCompleteMode.Suggest,
            AutoCompleteSource = AutoCompleteSource.ListItems,
            DataSource = UiIcons,
            DisplayMember = "Name",
            MaxDropDownItems = 24
        };
    }

    /// <summary>
    ///     Creates a clone of the template ComboBox object.
    /// </summary>
    /// <returns>A new instance of ComboBox with the same properties as the template ComboBox object.</returns>
    public static ComboBox CloneTemplateComboBox()
    {
        if (UiIconsComboTemplate == null) return new ComboBox();

        return new ComboBox
        {
            Location = UiIconsComboTemplate.Location,
            Size = UiIconsComboTemplate.Size,
            DropDownStyle = UiIconsComboTemplate.DropDownStyle,
            FormattingEnabled = UiIconsComboTemplate.FormattingEnabled,
            MaxDropDownItems = UiIconsComboTemplate.MaxDropDownItems,
            IntegralHeight = UiIconsComboTemplate.IntegralHeight,
            ItemHeight = UiIconsComboTemplate.ItemHeight,
            MaxLength = UiIconsComboTemplate.MaxLength,
            Sorted = UiIconsComboTemplate.Sorted,
            TabIndex = UiIconsComboTemplate.TabIndex,
            Enabled = UiIconsComboTemplate.Enabled,
            Visible = UiIconsComboTemplate.Visible,
            Anchor = UiIconsComboTemplate.Anchor,
            Dock = UiIconsComboTemplate.Dock,
            Margin = UiIconsComboTemplate.Margin,
            Padding = UiIconsComboTemplate.Padding,
            RightToLeft = UiIconsComboTemplate.RightToLeft,
            Font = UiIconsComboTemplate.Font,
            ForeColor = UiIconsComboTemplate.ForeColor,
            BackColor = UiIconsComboTemplate.BackColor,
            DropDownWidth = UiIconsComboTemplate.DropDownWidth,
            DropDownHeight = UiIconsComboTemplate.DropDownHeight,
            FlatStyle = UiIconsComboTemplate.FlatStyle,
            DataSource = UiIconsComboTemplate.DataSource,
            DisplayMember = UiIconsComboTemplate.DisplayMember,
            ValueMember = UiIconsComboTemplate.ValueMember,
            BindingContext = UiIconsComboTemplate.BindingContext,
            SelectedIndex = UiIconsComboTemplate.SelectedIndex,
            SelectedItem = UiIconsComboTemplate.SelectedItem,
            SelectedValue = UiIconsComboTemplate.SelectedValue,
            AutoCompleteMode = UiIconsComboTemplate.AutoCompleteMode,
            AutoCompleteSource = UiIconsComboTemplate.AutoCompleteSource
        };
    }

    /// <summary>
    ///     Sets the culture of the application user interface.
    /// </summary>
    /// <param name="culture">The culture to set, in the format "[Language] ([Culture])", e.g., "English (en)".</param>
    public static void SetCulture(string culture)
    {
        var cultureCode = ExtractCultureCode(culture);
        CultureInfo.CurrentUICulture = new CultureInfo(cultureCode);
    }

    /// <summary>
    ///     Loads the list UI icons from an embedded text file resource.
    /// </summary>
    private static void LoadUiIcons()
    {
        if (_uiIconsInitialized) return;

        try
        {
            var iconsText = ExecutingAssembly.ReadResource("RadioExt_Helper.resources.final_ui_icon_strings.txt");
            var distinctUiIcons = iconsText.Split('\n')
                .Select(line => line.Trim())
                .Distinct()
                .ToList();

            UiIcons = new BindingList<string>(distinctUiIcons);
            _uiIconsInitialized = true;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger(nameof(GlobalData)).Error(ex, "Error initializing UI Icons.");
        }
    }

    /// <summary>
    ///     Extracts the .NET culture code from a language string.
    /// </summary>
    /// <param name="language">The language string to extract the language from.</param>
    /// <returns></returns>
    private static string ExtractCultureCode(string language)
    {
        var startIndex = language.IndexOf('(') + 1;
        var length = language.LastIndexOf(')') - startIndex;
        return language.Substring(startIndex, length);
    }
}
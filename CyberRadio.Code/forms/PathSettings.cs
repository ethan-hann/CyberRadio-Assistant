// PathSettings.cs : RadioExt-Helper
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
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents the settings form for managing game and staging paths.
/// </summary>
public partial class PathSettings : Form
{
    /// <summary>
    /// Occurs whenever the game base path is changed.
    /// </summary>
    public event EventHandler? GameBasePathChanged;

    /// <summary>
    /// Occurs whenever the staging path is changed.
    /// </summary>
    public event EventHandler? StagingPathChanged;

    private bool _gameFolderChanged;
    private bool _stageFolderChanged;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PathSettings" /> class.
    /// </summary>
    public PathSettings()
    {
        InitializeComponent();
    }

    /// <summary>
    ///     Gets the game base path from the configuration.
    /// </summary>
    private static string GameBasePath => GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;

    /// <summary>
    ///     Gets the staging path from the configuration.
    /// </summary>
    private static string StagingPath => GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

    /// <summary>
    ///     Handles the Load event of the form. Sets up initial label values and translations.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void PathSettings_Load(object sender, EventArgs e)
    {
        SetLabels();
        Translate();
    }

    /// <summary>
    ///     Translates UI components based on the current language settings.
    /// </summary>
    private void Translate()
    {
        Text = GlobalData.Strings.GetString("GamePaths");

        label1.Text = GlobalData.Strings.GetString("GameBasePath");
        label2.Text = GlobalData.Strings.GetString("RadioStationPath");
        label4.Text = GlobalData.Strings.GetString("StagingPath");

        btnChangeGameBasePath.Text = GlobalData.Strings.GetString("Change") +
                                     GlobalData.Strings.GetString("DotDotDot");
        btnChangeBackUpPath.Text = GlobalData.Strings.GetString("Change") +
                                   GlobalData.Strings.GetString("DotDotDot");

        SetLabels();
    }

    /// <summary>
    ///     Sets the labels for the current paths.
    /// </summary>
    private void SetLabels()
    {
        lblGameBasePath.Text = !GameBasePath.Equals(string.Empty)
            ? GameBasePath
            : GlobalData.Strings.GetString("GameBasePathPlaceholder");

        lblBackupPath.Text = !StagingPath.Equals(string.Empty)
            ? StagingPath
            : GlobalData.Strings.GetString("StagingPathPlaceholder");

        var radioPath = PathHelper.GetRadioExtPath(GameBasePath);
        lblRadioPath.Text = radioPath.Equals(string.Empty)
            ? GlobalData.Strings.GetString("RadioExtPathPlaceholder")
            : radioPath;
    }

    /// <summary>
    ///     Handles the click event of the Change Game Base Path button. Updates the game base path.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnChangeGameBasePath_Click(object sender, EventArgs e)
    {
        var basePath = PathHelper.GetGamePath();
        if (basePath == null || basePath.Equals(string.Empty)) return;

        if (!GameBasePath.Equals(basePath))
            _gameFolderChanged = true;

        if (GlobalData.ConfigManager.Set("gameBasePath", basePath))
        {
            if (GlobalData.ConfigManager.Save())
            {
                if (_gameFolderChanged)
                {
                    AuLogger.GetCurrentLogger<PathSettings>("ChangeGameBasePath")
                    .Info($"Updated game base path: {basePath}");
                    GameBasePathChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            else
                AuLogger.GetCurrentLogger<PathSettings>("ChangeGameBasePath")
                    .Warn("Couldn't save updated configuration after changing base path.");
        }

        SetLabels();
    }

    /// <summary>
    ///     Handles the click event of the Change Backup Path button. Updates the staging path.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnChangeBackUpPath_Click(object sender, EventArgs e)
    {
        var stagingPath = PathHelper.GetStagingPath();
        if (stagingPath.Equals(string.Empty) || stagingPath.Equals(StagingPath)) return;

        if (!StagingPath.Equals(stagingPath))
            _stageFolderChanged = true;

        if (GlobalData.ConfigManager.Set("stagingPath", stagingPath))
        {
            if (GlobalData.ConfigManager.Save())
            {
                if (_stageFolderChanged)
                {
                    AuLogger.GetCurrentLogger<PathSettings>("ChangeStagingPath")
                    .Info($"Updated staging path: {stagingPath}");
                    StagingPathChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            else
                AuLogger.GetCurrentLogger<PathSettings>("ChangeStagingPath")
                    .Warn("Couldn't save updated configuration after changing staging path.");
        }

        SetLabels();
    }
}
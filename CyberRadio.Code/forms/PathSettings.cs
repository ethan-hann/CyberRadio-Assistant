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

using System.Text;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents the settings form for managing game and staging paths.
/// </summary>
public partial class PathSettings : Form
{
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
    /// Occurs whenever the game base path is changed.
    /// </summary>
    public event EventHandler? GameBasePathChanged;

    /// <summary>
    /// Occurs whenever the staging path is changed.
    /// </summary>
    public event EventHandler? StagingPathChanged;

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
        Text = Strings.GamePaths;

        label1.Text = Strings.GameBasePath;
        label2.Text = Strings.RadioStationPath;
        label4.Text = Strings.StagingPath;

        btnChangeGameBasePath.Text = Strings.Change + @"...";
        btnChangeBackUpPath.Text = Strings.Change + @"...";

        SetLabels();
    }

    /// <summary>
    ///     Sets the labels for the current paths.
    /// </summary>
    private void SetLabels()
    {
        lblGameBasePath.Text = !GameBasePath.Equals(string.Empty)
            ? GameBasePath
            : Strings.GameBasePathPlaceholder;

        lblBackupPath.Text = !StagingPath.Equals(string.Empty)
            ? StagingPath
            : Strings.StagingPathPlaceholder;

        var radioPath = PathHelper.GetRadioExtPath(GameBasePath);
        lblRadioPath.Text = radioPath.Equals(string.Empty)
            ? Strings.RadioExtPathPlaceholder
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
        if (basePath.Equals(string.Empty)) return;
        var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

        if (PathHelper.IsSubPath(stagingPath, basePath))
        {
            MessageBox.Show(this, Strings.GamePathWithinStagingPath, Strings.Error, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<PathSettings>("ChangeGameBasePath").Warn("Game path is within the staging path.");
            return;
        }

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
            {
                AuLogger.GetCurrentLogger<PathSettings>("ChangeGameBasePath")
                    .Warn("Couldn't save updated configuration after changing base path.");
            }
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
        var gamePath = GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;

        if (PathHelper.IsSubPath(gamePath, stagingPath))
        {
            MessageBox.Show(this, Strings.StagingPathWithinGamePath, Strings.Error, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<PathSettings>("ChangeStagingPath").Warn("Staging path is within the game path.");
            return;
        }

        // Check if the staging path is within any forbidden paths
        var forbiddenPathResult = PathHelper.IsForbiddenPath(stagingPath);
        if (forbiddenPathResult.IsForbidden)
        {
            var reason = Strings.ResourceManager.GetString(forbiddenPathResult.Reason.ToDescriptionString());
            if (reason == null)
            {
                MessageBox.Show(this, string.Format(Strings.StagingPathForbidden, stagingPath), Strings.Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                AuLogger.GetCurrentLogger<PathSettings>("ChangeStagingPath").Warn("Staging path is within a forbidden path.");
                return;
            }

            var text = new StringBuilder();
            text.AppendLine(string.Format(Strings.StagingPathForbidden, stagingPath));
            text.AppendLine();
            text.AppendLine(reason);
            MessageBox.Show(this, text.ToString(), Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<PathSettings>("ChangeStagingPath").Warn("Staging path is within a forbidden path.");
            return;
        }

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
            {
                AuLogger.GetCurrentLogger<PathSettings>("ChangeStagingPath")
                    .Warn("Couldn't save updated configuration after changing staging path.");
            }
        }

        SetLabels();
    }
}
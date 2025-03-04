// NoStationsCtl.cs : RadioExt-Helper
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

using RadioExt_Helper.forms;
using RadioExt_Helper.models;
using RadioExt_Helper.theming;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls;

/// <summary>
/// Represents the user control for displaying "No Stations" information.
/// </summary>
public sealed partial class NoStationsCtl : UserControl, IUserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoStationsCtl"/> class.
    /// </summary>
    public NoStationsCtl()
    {
        InitializeComponent();
        Dock = DockStyle.Fill;
    }

    /// <summary>
    /// Gets the trackable object for the station.
    /// </summary>
    public TrackableObject<Station> Station => new(new Station());

    /// <summary>
    /// Translates the text of the control to the current language.
    /// </summary>
    public void Translate()
    {
        lblNoStations.Text = Strings.NoStationsYet;
        lblNoGamePath.Text = Strings.NoExeFound;
        lblNoStagingPath.Text = Strings.NoStagingPathFound;

        btnPaths.Text = Strings.Paths;
        btnPaths.Text = Strings.Paths;
        btnRestoreFromBackup.Text = Strings.RestoreFromBackupButton;
    }

    /// <summary>
    /// Event that is raised when the paths are set.
    /// </summary>
    public event EventHandler? PathsSet;

    /// <summary>
    /// Event that is raised when the user wants to restore from a backup.
    /// </summary>
    public event EventHandler<string>? RestoringFromBackup;

    /// <summary>
    /// Occurs when the control is loaded.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NoStationsCtl_Load(object sender, EventArgs e)
    {
        Translate();
        CheckPaths();

        ThemeManager.Instance.ApplyThemeToUserControl(this);
    }

    /// <summary>
    /// Checks the staging and game paths and displays the appropriate message.
    /// </summary>
    private void CheckPaths()
    {
        var showNoGamePath = false;
        var showNoStagePath = false;

        var gameBasePath = GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;
        var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

        if (gameBasePath.Equals(string.Empty))
            showNoGamePath = true;

        if (stagingPath.Equals(string.Empty))
            showNoStagePath = true;

        ToggleControls(showNoGamePath, showNoStagePath);
    }

    /// <summary>
    /// Toggles the visibility of the controls based on the availability of the game and staging paths.
    /// </summary>
    /// <param name="gamePath">Whether the game path is set.</param>
    /// <param name="stagePath">Whether the staging path is set.</param>
    private void ToggleControls(bool gamePath, bool stagePath)
    {
        tlpNoGamePath.Visible = gamePath;
        tlpNoStagingPath.Visible = stagePath;

        btnRestoreFromBackup.Visible = !gamePath && !stagePath;

        //Trigger population of stations
        if (!tlpNoGamePath.Visible && !tlpNoStagingPath.Visible)
            PathsSet?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the paths button is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnPaths_Click(object sender, EventArgs e)
    {
        var pathDialog = new PathSettings();
        pathDialog.GameBasePathChanged += (_, _) => CheckPaths();
        pathDialog.StagingPathChanged += (_, _) => CheckPaths();
        pathDialog.ShowDialog(this);
    }

    private void BtnRestoreFromBackup_Click(object sender, EventArgs e)
    {
        var fileBrowser = new OpenFileDialog
        {
            Filter = Strings.MainForm_RestoreFileBrowserFilter + @"|*.zip",
            Title = Strings.MainForm_RestoreFileBrowserTitle
        };

        if (fileBrowser.ShowDialog(this) != DialogResult.OK) return;

        RestoringFromBackup?.Invoke(this, fileBrowser.FileName);
    }
}
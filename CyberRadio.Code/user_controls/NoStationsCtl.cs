// NoStationsCtl.cs : RadioExt-Helper
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

using RadioExt_Helper.forms;
using RadioExt_Helper.models;
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
        lblNoStations.Text = GlobalData.Strings.GetString("NoStationsYet");
        lblNoGamePath.Text = GlobalData.Strings.GetString("NoExeFound");
        lblNoStagingPath.Text = GlobalData.Strings.GetString("NoStagingPathFound");

        btnPaths.Text = GlobalData.Strings.GetString("Paths");
        btnPaths.Text = GlobalData.Strings.GetString("Paths");
    }

    /// <summary>
    /// Event that is raised when the paths are set.
    /// </summary>
    public event EventHandler? PathsSet;

    /// <summary>
    /// Occurs when the control is loaded.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NoStationsCtl_Load(object sender, EventArgs e)
    {
        Translate();
        CheckPaths();
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
        pathDialog.GameBasePathChanged += (s, e) => CheckPaths();
        pathDialog.StagingPathChanged += (s, e) => CheckPaths();
        pathDialog.ShowDialog(this);
    }
}
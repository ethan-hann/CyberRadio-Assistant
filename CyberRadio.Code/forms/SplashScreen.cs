// SplashScreen.cs : RadioExt-Helper
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

using System.Reflection;
using AetherUtils.Core.Logging;
using RadioExt_Helper.migration;
using RadioExt_Helper.utility;
using WIG.Lib.Utility;

namespace RadioExt_Helper.forms;

/// <summary>
/// Represents the splash screen for the application.
/// </summary>
public partial class SplashScreen : Form
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SplashScreen"/> class.
    /// </summary>
    public SplashScreen()
    {
        InitializeComponent();

        var version = Assembly.GetExecutingAssembly().GetName()?.Version;

        SetVersionLabel(version);
    }

    /// <summary>
    /// Handles the Load event of the splash screen. Starts the background tasks.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private async void SplashScreen_Load(object sender, EventArgs e)
    {
        UpdateStatus(Strings.SplashScreen_Starting);
        var statusMessages = await PerformBackgroundTasks();
        statusMessages.ForEach(msg =>
        {
            if (msg.Contains("Error", StringComparison.CurrentCultureIgnoreCase))
                AuLogger.GetCurrentLogger<SplashScreen>().Error(msg);
            else
                AuLogger.GetCurrentLogger<SplashScreen>().Info(msg);
        });
        Close();
    }

    /// <summary>
    /// Performs background tasks such as migrating settings and checking for updates.
    /// </summary>
    /// <returns>A list of status messages generated during the tasks.</returns>
    private async Task<List<string>> PerformBackgroundTasks()
    {
        var statusMessages = new List<string>();

        // Migrate settings (if needed)
        UpdateStatus(Strings.SplashScreen_CheckingSettings);

        await Task.Delay(300);
        var config = MigrationHelper.MigrateSettings();
        if (config != null)
        {
            GlobalData.ConfigManager.SetConfig(config);
            await GlobalData.ConfigManager.SaveAsync();

            UpdateStatus(Strings.SplashScreen_MigratedSettings);
            await Task.Delay(300);
            statusMessages.Add("Settings migrated successfully.");
        }
        else
        {
            UpdateStatus(Strings.SplashScreen_MigratedSettingsNo);
            await Task.Delay(300);
            statusMessages.Add("Settings migration not needed.");
        }

        // Migrate songs (if needed)
        UpdateStatus(Strings.SplashScreen_CheckingSongs);
        await Task.Delay(300);

        var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
        var songMigrationStatus = MigrationHelper.MigrateSongs(stagingPath);
        statusMessages.AddRange(songMigrationStatus);

        // Check for updates (if needed)
        if (GlobalData.ConfigManager.Get("autoCheckForUpdates") as bool? ?? true)
        {
            UpdateStatus(Strings.SplashScreen_UpdateCheck);
            await Task.Run(Updater.CheckForUpdates);
        }
        else
        {
            UpdateStatus(Strings.SplashScreen_UpdateCheckNo);
            await Task.Delay(300);
        }

        //Setup Icon Manager
        UpdateStatus(Strings.SplashScreen_SetupIconManager);
        await IconManager.Instance.InitializeAsync();
        await Task.Delay(200);
        statusMessages.Add(IconManager.Instance.IsInitialized
            ? "Icon Manager initialized successfully."
            : "Icon Manager initialization failed.");

        //TODO: Add Nexus API key authentication when feature is implemented
        //var nexusApiKey = GlobalData.ConfigManager.Get("nexusApiKey") as string ?? string.Empty;
        //if (!nexusApiKey.Equals(string.Empty))
        //{
        //    UpdateStatus(Strings.SplashScreen_CheckApiAccess ?? "Checking Nexus API Key...");
        //    await NexusApi.AuthenticateApiKey(nexusApiKey);
        //    statusMessages.Add(NexusApi.IsAuthenticated
        //        ? "Nexus API key authenticated successfully."
        //        : "Nexus API key authentication failed.");
        //    await Task.Delay(500); // Simulate delay
        //}
        //------------------------------------------------------------------------------------------------------------

        UpdateStatus(Strings.SplashScreen_Finalizing ?? "Finalizing...");
        await Task.Delay(500);

        return statusMessages;
    }

    /// <summary>
    /// Updates the status message on the splash screen.
    /// </summary>
    /// <param name="message">The status message to display.</param>
    public void UpdateStatus(string message)
    {
        if (InvokeRequired)
            Invoke(new Action<string>(UpdateStatus), message);
        else
            lblSplashStatus.Text = message;
    }

    /// <summary>
    /// Sets the version label on the splash screen.
    /// </summary>
    /// <param name="version">The version to display.</param>
    private void SetVersionLabel(Version? version)
    {
        if (InvokeRequired)
            Invoke(new Action<Version?>(SetVersionLabel), version);
        else
            lblVersion.Text =
                version != null ? @$"{version.Major}.{version.Minor}.{version.Build}" : @"Version Unknown";
    }
}
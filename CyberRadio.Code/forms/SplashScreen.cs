// SplashScreen.cs : RadioExt-Helper
// Copyright (C) 2026  Ethan Hann
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

#region

using System.Reflection;
using System.Text;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.migration;
using RadioExt_Helper.utility;
using WIG.Lib.Utility;
using PathHelper = RadioExt_Helper.utility.PathHelper;

#endregion

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents the splash screen for the application.
/// </summary>
public partial class SplashScreen : Form
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SplashScreen" /> class.
    /// </summary>
    public SplashScreen()
    {
        InitializeComponent();

        var version = Assembly.GetExecutingAssembly().GetName().Version;

        SetVersionLabel(version);
    }

    /// <summary>
    ///     Handles the Load event of the splash screen. Starts the background tasks.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private async void SplashScreen_Load(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<SplashScreen>()
                .Fatal("An unhandled exception occurred during splash screen initialization.", ex);
            ToastNotification.Show(this, Strings.SplashScreen_InitializationError, ToastType.Error);
        }
    }

    /// <summary>
    ///     Performs background tasks such as migrating settings and checking for updates.
    /// </summary>
    /// <returns>A list of status messages generated during the tasks.</returns>
    private async Task<List<string>> PerformBackgroundTasks()
    {
        List<string> statusMessages = [];

        // Migrate settings (if needed)
        UpdateStatus(Strings.SplashScreen_CheckingSettings);

        await Task.Delay(150);
        var config = MigrationHelper.MigrateSettings();
        if (config != null)
        {
            GlobalData.ConfigManager.SetConfig(config);
            await GlobalData.ConfigManager.SaveAsync();

            UpdateStatus(Strings.SplashScreen_MigratedSettings);
            await Task.Delay(150);
            statusMessages.Add("Settings migrated successfully.");
        }
        else
        {
            UpdateStatus(Strings.SplashScreen_MigratedSettingsNo);
            await Task.Delay(150);
            statusMessages.Add("Settings migration not needed.");
        }

        //Check staging path for forbidden paths
        bool isStagingPathValid;
        UpdateStatus(Strings.SplashScreen_CheckingStagingPath);
        await Task.Delay(150);

        var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
        var result = PathHelper.IsForbiddenPath(stagingPath);
        if (result.IsForbidden) //If the staging path is a forbidden path, reset it to an empty string before continuing.
        {
            GlobalData.ConfigManager.Set("stagingPath", string.Empty);
            await GlobalData.ConfigManager.SaveAsync();

            UpdateStatus(Strings.SplashScreen_StagingPathForbidden);
            var reason = Strings.ResourceManager.GetString(result.Reason.ToDescriptionString());
            statusMessages.Add(reason ?? "Staging path was invalid. Reset it to an empty string.");
            isStagingPathValid = false;

            StringBuilder text = new();
            text.AppendLine(string.Format(Strings.StagingPathForbidden, stagingPath));
            text.AppendLine();
            text.AppendLine(reason);
            Invoke(() =>
                MessageBox.Show(this, text.ToString(), Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error));
        }
        else
        {
            UpdateStatus(Strings.SplashScreen_StagingPathValid);
            statusMessages.Add("Staging path is valid!");
            isStagingPathValid = true;
        }

        await Task.Delay(300);

        // Migrate songs (if needed)
        if (isStagingPathValid)
        {
            UpdateStatus(Strings.SplashScreen_CheckingSongs);
            await Task.Delay(150);

            var songMigrationStatus = MigrationHelper.MigrateSongs(stagingPath);
            statusMessages.AddRange(songMigrationStatus);
        }

        // Check for updates (if needed)
        if (GlobalData.ConfigManager.Get("autoCheckForUpdates") as bool? ?? true)
        {
            UpdateStatus(Strings.SplashScreen_UpdateCheck);
            await Task.Run(Updater.CheckForUpdates);
        }
        else
        {
            UpdateStatus(Strings.SplashScreen_UpdateCheckNo);
            await Task.Delay(150);
        }

        //Setup Icon Manager
        UpdateStatus(Strings.SplashScreen_SetupIconManager);
        await IconManager.Instance.InitializeAsync();
        await Task.Delay(100);
        statusMessages.Add(IconManager.Instance.IsInitialized
            ? "Icon Manager initialized successfully."
            : "Icon Manager initialization failed.");

        //Setup Audio Converter
        UpdateStatus(Strings.SplashScreen_SetupAudioConverter);
        statusMessages.AddRange(await AudioConverter.Instance.InitializeAsync());
        await Task.Delay(100);
        statusMessages.Add(AudioConverter.Instance.IsInitialized
            ? "Audio Converter initialized successfully."
            : "Audio Converter initialization failed.");

        //Setup Audio Manager
        UpdateStatus(Strings.SplashScreen_SetupAudioManager);
        await AudioManager.Instance.InitializeAsync();
        await Task.Delay(100);
        statusMessages.Add(AudioManager.Instance.IsInitialized
            ? "Audio Manager initialized successfully."
            : "Audio Manager initialization failed.");

        //Setup TinyMCE
        UpdateStatus(Strings.SplashScreen_DownloadingTinyMCE);
        await TinyMceInstaller.EnsureInstalledAsync("https://tortal.xyz/RNlB9");
        await Task.Delay(100);
        statusMessages.Add(!TinyMceInstaller.IsInstalled()
            ? "Failed to install TinyMCE or already installed."
            : "TinyMCE installation successful.");

        UpdateStatus(Strings.SplashScreen_DownloadingTinyMCELanguages);
        await TinyMceInstaller.EnsureLanguagesInstalled("https://tortal.xyz/jf9dJ");
        await Task.Delay(500);
        statusMessages.Add(!TinyMceInstaller.IsLanguagesInstalled()
            ? "Failed to install TinyMCE Languages or already installed."
            : "TinyMCE Languages installation successful.");

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

        //Log all final paths
        var finalStagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
        var finalGamePath = GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;
        var iconManagerWorkingDirectory = IconManager.Instance.WorkingDirectory;
        var iconManagerWolvenKitTempDirectory = IconManager.Instance.WolvenKitTempDirectory;
        var iconManagerImageImportDirectory = IconManager.Instance.ImageImportDirectory;
        var iconManagerImageExportDirectory = IconManager.Instance.ImageExportDirectory;
        var iconManagerImportedWorkingDirectory = IconManager.Instance.ImportedWorkingDirectory;
        var iconManagerExtractedWorkingDirectory = IconManager.Instance.ExtractedWorkingDirectory;
        var audioManagerWorkingDirectory = AudioManager.Instance.WorkingDirectory;
        var audioManagerSound2WemDirectory = AudioManager.Instance.Sound2WemDirectory;
        var audioManagerWwiseToolsDirectory = AudioManager.Instance.WwiseToolsDirectory;
        var audioManagerFfmpegDirectory = AudioManager.Instance.FfmpegDirectory;
        var audioManagerAudioImportDirectory = AudioManager.Instance.AudioImportDirectory;
        var audioManagerImportedWorkingDirectory = AudioManager.Instance.ImportedWorkingDirectory;
        var audioConvertorWorkingDirectory = AudioConverter.Instance.WorkingDirectory;
        var audioConvertorConvertedDirectory = AudioConverter.Instance.ConvertedDirectory;

        statusMessages.Add("================== Initialization Complete! ==================");
        statusMessages.Add($"Staging Path: {finalStagingPath}");
        statusMessages.Add($"Game Path: {finalGamePath}");
        statusMessages.Add($"Icon Manager WolvenKit Tools Directory: {iconManagerWolvenKitTempDirectory}");
        statusMessages.Add($"Icon Manager Working Directory: {iconManagerWorkingDirectory}");
        statusMessages.Add($"Icon Manager Image Import Directory: {iconManagerImageImportDirectory}");
        statusMessages.Add($"Icon Manager Image Export Directory: {iconManagerImageExportDirectory}");
        statusMessages.Add($"Icon Manager Imported Working Directory: {iconManagerImportedWorkingDirectory}");
        statusMessages.Add($"Icon Manager Extracted Working Directory: {iconManagerExtractedWorkingDirectory}");
        statusMessages.Add($"Audio Manager Working Directory: {audioManagerWorkingDirectory}");
        statusMessages.Add($"Audio Manager Sound2Wem Directory: {audioManagerSound2WemDirectory}");
        statusMessages.Add($"Audio Manager Wwise Tools Directory: {audioManagerWwiseToolsDirectory}");
        statusMessages.Add($"Audio Manager Audio Import Directory: {audioManagerAudioImportDirectory}");
        statusMessages.Add($"Audio Manager Imported Working Directory: {audioManagerImportedWorkingDirectory}");
        statusMessages.Add($"Audio Manager Ffmpeg Directory: {audioManagerFfmpegDirectory}");
        statusMessages.Add($"Audio Convertor Working Directory: {audioConvertorWorkingDirectory}");
        statusMessages.Add($"Audio Convertor Converted Directory: {audioConvertorConvertedDirectory}");

        statusMessages.Add("If any paths above are empty, you may have issues with some functionality of CRA!");
        statusMessages.Add("================== Initialization Complete! ==================");

        UpdateStatus(Strings.SplashScreen_Finalizing);
        GlobalData.ConfigManager.Set("isFirstRun", false);
        await GlobalData.ConfigManager.SaveAsync();

        await Task.Delay(100);

        return statusMessages;
    }

    /// <summary>
    ///     Updates the status message on the splash screen.
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
    ///     Sets the version label on the splash screen.
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
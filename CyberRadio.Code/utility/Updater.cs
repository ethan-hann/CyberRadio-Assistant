﻿using System.Reflection;
using AetherUtils.Core.Logging;
using Newtonsoft.Json.Linq;
using RadioExt_Helper.forms;

namespace RadioExt_Helper.utility;

/// <summary>
///     Facilitates checking for application updates based on a <c>version.json</c> file on the main branch of the GitHub.
/// </summary>
public static class Updater
{
    private const string VersionUrl =
        "https://raw.githubusercontent.com/ethan-hann/CyberRadio-Assistant/main/version.json";

    /// <summary>
    ///     Checks for a new application update and displays a dialog allowing the user to choose if they want to update now or
    ///     not.
    /// </summary>
    public static async Task CheckForUpdates()
    {
        try
        {
            var (version, url) = await GetLatestVersionInfoAsync();

            if (version.Equals(string.Empty) || url.Equals(string.Empty))
            {
                var text = GlobalData.Strings.GetString("NoInternetMsg");
                var caption = GlobalData.Strings.GetString("NoInternetCaption");
                MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var latestVersion = new Version(version);
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

            if (currentVersion != null)
            {
                //Strip the build from the version number
                currentVersion = new Version(currentVersion.Major, currentVersion.Minor, currentVersion.Build);

                if (latestVersion > currentVersion)
                {
                    var text = GlobalData.Strings.GetString("UpdateAvailableNotice");
                    var caption = GlobalData.Strings.GetString("UpdateAvailable");
                    if (MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                        DialogResult.Yes)
                    {
                        var vInfo = new VersionInfo(latestVersion, url);
                        new UpdateBox(vInfo).ShowDialog();
                    }
                }
                else
                {
                    var text = GlobalData.Strings.GetString("NoUpdateAvailable");
                    var caption = GlobalData.Strings.GetString("NoUpdateCaption");
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                throw new InvalidOperationException("No current version found.");
            }
        }
        catch (Exception ex)
        {
            var text = string.Format(GlobalData.Strings.GetString("UpdateCheckError") ??
                                     "Error checking for updates: {0}", ex.Message);
            var caption = GlobalData.Strings.GetString("Error") ?? "Error";
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger("Updater.CheckForUpdates").Error(ex, "Error checking for updates");
        }
    }

    private static async Task<(string version, string url)> GetLatestVersionInfoAsync()
    {
        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(5);

        var response = await client.GetStringAsync(VersionUrl);

        var json = JObject.Parse(response);
        var version = json["version"]?.ToString();
        var url = json["url"]?.ToString();

        if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(url))
            return (string.Empty, string.Empty);

        return (version, url);
    }
}
using Newtonsoft.Json.Linq;
using RadioExt_Helper.forms;
using System.Reflection;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Facilitates checking for application updates based on a <c>version.json</c> file on the main branch of the GitHub.
    /// </summary>
    public sealed class Updater
    {
        private const string VersionUrl = "https://raw.githubusercontent.com/ethan-hann/CyberRadio-Assistant/main/version.json";

        /// <summary>
        /// Checks for a new application update and displays a dialog allowing the user to choose if they want to update now or not.
        /// </summary>
        public static async void CheckForUpdates()
        {
            try
            {
                var (version, url) = await GetLatestVersionInfoAsync();
                var latestVersion = new Version(version);
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

                //Strip the build from the version number
                currentVersion = new Version(currentVersion.Major, currentVersion.Minor, currentVersion.Build);

                if (latestVersion > currentVersion)
                {
                    var text = GlobalData.Strings.GetString("UpdateAvailableNotice");
                    var caption = GlobalData.Strings.GetString("UpdateAvailable");
                    if (MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(GlobalData.Strings.GetString("UpdateCheckError") ?? "Error checking for updates: {0}", ex.Message), 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static async Task<(string version, string url)> GetLatestVersionInfoAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(VersionUrl);
            var json = JObject.Parse(response);
            var version = json["version"]?.ToString();
            var url = json["url"]?.ToString();

            if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(url))
                return (string.Empty, string.Empty);

            return (version, url);
        }
    }
}

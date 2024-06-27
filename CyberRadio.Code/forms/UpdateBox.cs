using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using RadioExt_Helper.utility;
using System.Diagnostics;
using System.Reflection;

namespace RadioExt_Helper.forms
{
    public partial class UpdateBox : Form
    {
        private readonly VersionInfo _versionInfo;
        private readonly HttpClient _httpClient;
        private readonly string _newFileName;

        public UpdateBox(VersionInfo info)
        {
            InitializeComponent();

            _versionInfo = info;
            _newFileName = $"CyberRadioAssistant-{_versionInfo.LatestVersion}.exe";

            _httpClient = new HttpClient();
        }

        private void UpdateBox_Load(object sender, EventArgs e)
        {
            Translate();
            SetValues();
        }

        private void Translate()
        {
            Text = GlobalData.Strings.GetString("UpdateAvailable") ?? "Update Available";
            lblCurrentVerText.Text = GlobalData.Strings.GetString("UpdateCurrentVersion") ?? "Current Version:";
            lblNewVersionTxt.Text = GlobalData.Strings.GetString("UpdateNewVersion") ?? "New Version:";
            lblChangelogTxt.Text = GlobalData.Strings.GetString("UpdateChangelog") ?? "Changelog:";
            btnDownload.Text = GlobalData.Strings.GetString("UpdateDownloadButton") ?? "Download";
            lblStatus.Text = GlobalData.Strings.GetString("Ready") ?? "Ready";
        }

        private void SetValues()
        {
            Version? v = Assembly.GetExecutingAssembly().GetName().Version;
            var currentVersion = new Version(v.Major, v.Minor, v.Build);

            lblCurrentVersion.Text = currentVersion.ToString() ?? "Unknown";
            lblNewVersion.Text = _versionInfo.LatestVersion.ToString();
            lnkChangelog.Text = "https://github.com/ethan-hann/CyberRadio-Assistant";
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            var tempFilePath = Path.Combine(Path.GetTempPath(), _newFileName);
            var progress = new Progress<int>(percent =>
            {
                if (InvokeRequired)
                {
                    Invoke(() =>
                    {
                        pgDownloadProgress.Value = percent;
                        lblStatus.Text = string.Format(GlobalData.Strings.GetString("UpdateDownloadPercent") ?? "Downloaded {0}%", percent);
                    });
                }
                else
                {
                    pgDownloadProgress.Value = percent;
                    lblStatus.Text = string.Format(GlobalData.Strings.GetString("UpdateDownloadPercent") ?? "Downloaded {0}%", percent);
                }
                
            });

            try
            {
                await DownloadFileAsync(_versionInfo.DownloadUrl, tempFilePath, progress);
                lblStatus.Text = GlobalData.Strings.GetString("UpdateDownloadComplete") ?? "Download completed! Starting the update...";
                SaveSettingsBeforeExit();

                //Start the updated application and close the current instance
                var newFilePath = CopyToOriginalLocation(tempFilePath);
                StartUpdatedApplication(newFilePath);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(GlobalData.Strings.GetString("UpdateDownloadError") ?? "Download error: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DownloadFileAsync(string url, string destinationPath, IProgress<int> progress)
        {
            using var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault(-1L);
            var canReportProgress = totalBytes != -1L;

            using var contentStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

            var totalBytesRead = 0L;
            var buffer = new byte[8192];
            var isMoreToRead = true;

            while (isMoreToRead)
            {
                var bytesRead = await contentStream.ReadAsync(buffer);
                if (bytesRead == 0)
                {
                    isMoreToRead = false;
                    continue;
                }

                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));

                totalBytesRead += bytesRead;
                if (canReportProgress)
                {
                    var percentComplete = (int)((totalBytesRead * 1.0 / totalBytes) * 100);
                    progress.Report(percentComplete);
                }
            }
        }

        private void SaveSettingsBeforeExit()
        {
            // Save any settings changes before exit
            Properties.Settings.Default.Save();
        }

        private string CopyToOriginalLocation(string tempFilePath)
        {
            try
            {
                var executingPath = Application.StartupPath;
                var newPath = Path.Combine(executingPath, _newFileName);
                File.Copy(tempFilePath, newPath, true);

                return newPath;
            }
            catch (Exception)
            {
                return tempFilePath;
            }
        }

        private void StartUpdatedApplication(string filePath)
        {
            if (!File.Exists(filePath)) { return; }
            if (!FileHelper.GetExtension(filePath).Equals(".exe")) { return; }

            var startInfo = new ProcessStartInfo(filePath);
            startInfo.UseShellExecute = true;

            Process.Start(startInfo);
        }

        private void lnkChangelog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkChangelog.Text.OpenUrl();
        }
    }
}

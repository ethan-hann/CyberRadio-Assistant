// UpdateBox.cs : RadioExt-Helper
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

using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using RadioExt_Helper.utility;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace RadioExt_Helper.forms;

public partial class UpdateBox : Form
{
    private readonly HttpClient _httpClient;
    private readonly string _newFileName;
    private readonly Progress<int> _progressReporter = new();
    private readonly VersionInfo _versionInfo;

    public UpdateBox(VersionInfo info)
    {
        InitializeComponent();

        _versionInfo = info;
        _newFileName = $"CyberRadioAssistant-{_versionInfo.LatestVersion}.exe";

        _httpClient = new HttpClient();

        _progressReporter.ProgressChanged += _progressReporter_ProgressChanged;
    }

    private void _progressReporter_ProgressChanged(object? sender, int e)
    {
        pgDownloadProgress.Value = e;
        SetStatus(string.Format(Strings.UpdateDownloadPercent, e));
    }

    private void UpdateBox_Load(object sender, EventArgs e)
    {
        Translate();
        SetValues();
    }

    private void Translate()
    {
        Text = Strings.UpdateAvailable;
        lblCurrentVerText.Text = Strings.UpdateCurrentVersion;
        lblNewVersionTxt.Text = Strings.UpdateNewVersion;
        lblChangelogTxt.Text = Strings.UpdateChangelog;
        btnDownload.Text = Strings.UpdateDownloadButton;
        lblStatus.Text = Strings.Ready;
    }

    private void SetValues()
    {
        var v = Assembly.GetExecutingAssembly().GetName().Version;
        Version? currentVersion = null;

        if (v != null)
            currentVersion = new Version(v.Major, v.Minor, v.Build);

        lblCurrentVersion.Text = currentVersion?.ToString() ?? "Unknown";
        lblNewVersion.Text = _versionInfo.LatestVersion.ToString();
        lnkChangelog.Text = $@"https://github.com/ethan-hann/CyberRadio-Assistant/CHANGELOG.md#{_versionInfo.LatestVersion}";
    }

    private void btnDownload_Click(object sender, EventArgs e)
    {
        if (bgDownloadUpdate.IsBusy || bgDownloadUpdate.CancellationPending)
            return;

        btnDownload.Enabled = false; // Disable the button
        bgDownloadUpdate.RunWorkerAsync(_progressReporter);
    }

    private void bgDownloadUpdate_DoWork(object sender, DoWorkEventArgs e)
    {
        if (e.Argument is IProgress<int> progressReporter)
            Task.Run(async () => { await DownloadFileAsync(_versionInfo.DownloadUrl, _newFileName, progressReporter); })
                .GetAwaiter().GetResult();
    }

    private void bgDownloadUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            MessageBox.Show(string.Format(Strings.UpdateDownloadError, e.Error.Message), Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        SetStatus(Strings.UpdateDownloadComplete);
        SaveSettingsBeforeExit();
        var newFilePath = CopyToOriginalLocation(Path.Combine(Path.GetTempPath(), _newFileName));

        MessageBox.Show(this, Strings.UpdateFolderOpening, Strings.UpdateFolderOpeningCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        StartUpdatedApplication(newFilePath);
        Application.Exit();
    }

    private async Task DownloadFileAsync(string url, string destinationFileName, IProgress<int> progress)
    {
        var tempFilePath = Path.Combine(Path.GetTempPath(), destinationFileName);

        using var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        var totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault(-1L);
        var canReportProgress = totalBytes != -1L;

        await using var contentStream = await response.Content.ReadAsStreamAsync();
        await using var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

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
            if (!canReportProgress) continue;

            var percentComplete = (int)(totalBytesRead * 1.0 / totalBytes * 100);
            progress?.Report(percentComplete);
        }
    }

    private void SetStatus(string status)
    {
        if (InvokeRequired)
            Invoke(() => { lblStatus.Text = status; });
        else
            lblStatus.Text = status;
    }

    private static void SaveSettingsBeforeExit()
    {
        GlobalData.ConfigManager.Save();
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
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<UpdateBox>("CopyToOriginalLocation").Error(ex, "Couldn't copy the updated file to original .exe location.");
            return tempFilePath;
        }
    }

    private static void StartUpdatedApplication(string filePath)
    {
        if (!File.Exists(filePath)) return;
        if (!FileHelper.GetExtension(filePath).Equals(".exe")) return;

        if (Directory.GetParent(filePath) is not { } parent) return;

        var startInfo = new ProcessStartInfo("explorer.exe")
        {
            Arguments = parent.FullName,
            UseShellExecute = true
        };

        Process.Start(startInfo);
    }

    private void lnkChangelog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        lnkChangelog.Text.OpenUrl();
    }
}
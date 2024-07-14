using System.ComponentModel;
using AetherUtils.Core.Extensions;
using Microsoft.Web.WebView2.Core;
using Pathoschild.FluentNexus.Models;
using RadioExt_Helper.nexus_api;

namespace RadioExt_Helper.forms
{
    public partial class ModDownloader : Form
    {
        private readonly string _gameDomain = "cyberpunk2077";
        private readonly string _baseDomain = $"https://nexusmods.com/cyberpunk2077";
        private readonly BindingList<Mod> _modsDownloadQueue = [];

        public ModDownloader()
        {
            InitializeComponent();
            InitializeWebView2Async();
        }

        private void ModDownloader_Load(object sender, EventArgs e)
        {
            lbModQueue.DataSource = _modsDownloadQueue;
            lbModQueue.DisplayMember = "Name";
        }

        private async void InitializeWebView2Async()
        {
            try
            {
                await wvNexusWebBrowser.EnsureCoreWebView2Async(null);
                wvNexusWebBrowser.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;
                wvNexusWebBrowser.CoreWebView2.Settings.IsScriptEnabled = false;
                wvNexusWebBrowser.CoreWebView2.Settings.IsWebMessageEnabled = false;
                wvNexusWebBrowser.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
                wvNexusWebBrowser.Source = new Uri($"https://nexusmods.com/{_gameDomain}/mods/");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"WebView2 initialization failed: {ex.Message}");
            }
        }

        private void CoreWebView2_NewWindowRequested(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Uri.OpenUrl();
            e.Handled = true;
        }

        private void WvNexusWebBrowser_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            txtModId.Text = wvNexusWebBrowser.Source.AbsoluteUri;
        }

        private void WvNexusWebBrowser_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            txtModId.Text = wvNexusWebBrowser.Source.AbsoluteUri;
        }

        private void WvNexusWebBrowser_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            txtModId.Text = wvNexusWebBrowser.Source.AbsoluteUri;
        }

        private async void BtnAddToQueue_Click(object sender, EventArgs e)
        {
            if (txtModId.Text.Length == 0) return;

            if (!NexusApi.IsAuthenticated || NexusApi.NexusClient == null) return;

            var modId = ParseModId(txtModId.Text);
            if (modId == -1)
            {
                MessageBox.Show("Invalid mod ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_modsDownloadQueue.Any(m => m.ModID == modId))
            {
                MessageBox.Show("Mod is already in the download queue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_modsDownloadQueue.Count >= 10)
            {
                MessageBox.Show("Download queue is full. You can only add up to 10 mods.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var mod = await NexusApi.NexusClient.Mods.GetMod(_gameDomain, modId);
            _modsDownloadQueue.Add(mod);
            //_modsDownloadQueue.Add(mod);
            MessageBox.Show($"Mod '{mod.Name}' added to the download queue.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int ParseModId(string url)
        {
            var uri = new Uri(url);
            var segments = uri.Segments;
            if (segments.Length < 4) return -1;

            var modId = segments[3].Trim('/');
            if (int.TryParse(modId, out var id)) return id;

            return -1;
        }

        private void LbModQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbModQueue.SelectedItem is Mod mod)
            {
                modDetails.SetMod(mod);
            }
        }

        private void WvNexusWebBrowser_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            var url = e.Uri;
            var uri = new Uri(url);

            // Check if the URL is part of the original domain and path
            if (IsInternalUrl(uri)) return;

            // Open the link in the default browser
            url.OpenUrl();
            e.Cancel = true;
        }

        private bool IsInternalUrl(Uri uri)
        {
            return uri.Host.Equals(new Uri(_baseDomain).Host, StringComparison.OrdinalIgnoreCase) &&
                   uri.AbsolutePath.StartsWith(_gameDomain, StringComparison.OrdinalIgnoreCase);
        }
    }
}

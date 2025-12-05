// // TinyMCE.cs : RadioExt-Helper
// // Copyright (C) 2025  Ethan Hann
// //
// // This program is free software: you can redistribute it and/or modify
// // it under the terms of the GNU General Public License as published by
// // the Free Software Foundation, either version 3 of the License, or
// // (at your option) any later version.
// //
// // This program is distributed in the hope that it will be useful,
// // but WITHOUT ANY WARRANTY; without even the implied warranty of
// // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// // GNU General Public License for more details.
// //
// // You should have received a copy of the GNU General Public License
// // along with this program.  If not, see <https://www.gnu.org/licenses/>.

#region

using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using AetherUtils.Core.Logging;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;

#endregion

namespace RadioExt_Helper.custom_controls;

/// <summary>
///     A Windows Forms control that hosts a TinyMCE rich text editor using WebView2.
///     Supports offline TinyMCE and runtime language switching.
/// </summary>
[DefaultEvent(nameof(EditorReady))]
public partial class TinyMce : UserControl, IUserControl
{
    private const string VirtualHostName = "tinymce.cra";

    private string _language = "en";
    private string? _pendingHtml;
    private WebView2? _webView;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TinyMce" /> control.
    /// </summary>
    public TinyMce()
    {
        InitializeComponent();

        DoubleBuffered = true;
        InitializeWebViewControl();

        HandleCreated += TinyMCE_HandleCreated;
    }

    /// <summary>
    ///     True once TinyMCE has initialized and is ready to accept commands.
    /// </summary>
    [Browsable(false)]
    public bool IsEditorReady { get; private set; }

    /// <summary>
    ///     TinyMCE UI language code (e.g., "en_US", "fr_FR").
    ///     This is used for the initial editor language; to change language at runtime,
    ///     call <see cref="SetLanguageAsync" />.
    /// </summary>
    [Category("TinyMCE")]
    [Description("TinyMCE UI language code, e.g. 'en', 'de', 'fr_FR'.")]
    public string Language
    {
        get => _language;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                value = "en";
            _language = value;
        }
    }

    /// <inheritdoc />
    public void Translate()
    {
        var currentCulture = CultureInfo.CurrentUICulture;
        var langCode = ResolveTinyMceLanguageCode(currentCulture.Name);

        // fire and forget; you already don't await it
        _ = SetLanguageAsync(langCode);
    }

    /// <summary>
    ///     Raised when TinyMCE has finished initializing and is ready for use.
    /// </summary>
    public event EventHandler? EditorReady;

    private void InitializeWebViewControl()
    {
        _webView = new WebView2
        {
            Dock = DockStyle.Fill
        };
        Controls.Add(_webView);
    }

    private void TinyMCE_HandleCreated(object? sender, EventArgs e)
    {
        if (IsInDesignMode())
            return;

        _ = InitializeWebViewAsync();
    }

    private bool IsInDesignMode()
    {
        return DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }

    private async Task InitializeWebViewAsync()
    {
        try
        {
            // Sanity check: make sure TinyMCE is actually present on disk
            if (!File.Exists(TinyMceInstaller.TinyMceScriptPath))
                throw new FileNotFoundException(
                    $"TinyMCE not found at expected path: {TinyMceInstaller.TinyMceScriptPath}\n" +
                    "Make sure your splash screen downloaded and extracted the TinyMCE .zip " +
                    "to TinyMceRootFolder before this control is created.");

            var webViewDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "RadioExt-Helper",
                "webview2");

            Directory.CreateDirectory(webViewDataFolder);

            if (_webView == null)
                throw new InvalidOperationException("WebView2 control is not initialized.");

            var env = await CoreWebView2Environment.CreateAsync(userDataFolder: webViewDataFolder);
            await _webView.EnsureCoreWebView2Async(env);

            _webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                VirtualHostName,
                TinyMceInstaller.TinyMceRootFolder,
                CoreWebView2HostResourceAccessKind.Allow);

            _webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

            var html = BuildTinyMceHtmlPage();
            _webView.CoreWebView2.NavigateToString(html);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<TinyMce>().Error(ex,
                "Failed to initialize TinyMCE WebView2 control.");
        }
    }

    private void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        try
        {
            var message = e.TryGetWebMessageAsString();
            if (!string.Equals(message, "editor-ready", StringComparison.OrdinalIgnoreCase)) return;

            IsEditorReady = true;

            if (!string.IsNullOrEmpty(_pendingHtml))
                _ = SendSetContentMessageAsync(_pendingHtml);

            EditorReady?.Invoke(this, EventArgs.Empty);
        }
        catch
        {
            // Ignore; worst case, editor ready event is lost
        }
    }

    private string BuildTinyMceHtmlPage()
    {
        const string scriptUrl = $"https://{VirtualHostName}/tinymce/js/tinymce/tinymce.min.js";

        // Resolve whatever we have in _language to an actual TinyMCE code/file
        var resolved = ResolveTinyMceLanguageCode(_language);
        var initialLanguage = string.IsNullOrWhiteSpace(resolved) ? "en" : resolved;

        var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <title>TinyMCE Editor</title>
    <script src=""{scriptUrl}"" referrerpolicy=""origin""></script>
    <style>
        html, body {{
            height: 100%;
            margin: 0;
            padding: 0;
            overflow: hidden;
            font-family: Arial, sans-serif;
        }}
        #editor-container {{
            height: 100%;
            box-sizing: border-box;
            padding: 4px;
        }}
        textarea {{
            height: calc(100% - 8px);
        }}
    </style>
</head>
<body>
<div id=""editor-container"">
    <textarea id=""editor""></textarea>
</div>

<script>
    var defaultLanguage = '{initialLanguage}';
    var currentLanguage = defaultLanguage;

    function isEnglish(lang) {{
        if (!lang) return true;
            lang = lang.toLowerCase();
        if (lang === 'en') return true;
        return lang.startsWith('en_') || lang.startsWith('en-');
    }}

    function notifyHostEditorReady() {{
        if (window.chrome && window.chrome.webview && window.chrome.webview.postMessage) {{
            window.chrome.webview.postMessage('editor-ready');
        }}
    }}

    function initTinyMCE(initialContent, lang) {{
        currentLanguage = lang || defaultLanguage;

        var config = {{
            selector: '#editor',
            menubar: true,
            plugins: 'lists link image table code',
            toolbar: 'undo redo | formatselect | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            license_key: 'gpl',
            height: '100%',
            resize: false,
            promotion: false,
            setup: function (editor) {{
                editor.on('init', function () {{
                    if (initialContent) {{
                        editor.setContent(initialContent);
                    }}
                    notifyHostEditorReady();
                }});
            }}
        }};

        // Only set language + language_url for non-English; English is the built-in default
        if (!isEnglish(currentLanguage)) {{
            config.language = currentLanguage;
            config.language_url = 'https://{VirtualHostName}/tinymce/js/tinymce/langs/' + currentLanguage + '.js';
        }}

        tinymce.init(config);
    }}

    // Initial editor startup
    initTinyMCE('', defaultLanguage);

    if (window.chrome && window.chrome.webview) {{
        window.chrome.webview.addEventListener('message', function (event) {{
            var message = event.data;
            if (!message || typeof message !== 'object') return;

            if (message.type === 'set-content') {{
                if (tinymce.activeEditor) {{
                    tinymce.activeEditor.setContent(message.html || '');
                }}
            }} else if (message.type === 'set-language') {{
                var newLang = message.language || defaultLanguage;
                var content = '';
                if (tinymce.activeEditor) {{
                    content = tinymce.activeEditor.getContent({{ format: 'html' }});
                    tinymce.activeEditor.remove();
                }}
                initTinyMCE(content, newLang);
            }}
        }});
    }}

    window.__tinyHostApi = {{
        getContent: function() {{
            if (!tinymce.activeEditor) return '';
            return tinymce.activeEditor.getContent({{ format: 'html' }});
        }}
    }};
</script>
</body>
</html>";
        return html;
    }

    private Task SendSetContentMessageAsync(string htmlContent)
    {
        if (_webView?.CoreWebView2 == null)
            return Task.CompletedTask;

        var payload = new
        {
            type = "set-content",
            html = htmlContent
        };

        var json = JsonSerializer.Serialize(payload);

        _webView.CoreWebView2.PostWebMessageAsJson(json);
        return Task.CompletedTask;
    }

    private Task SendSetLanguageMessageAsync(string languageCode)
    {
        if (_webView?.CoreWebView2 == null)
            return Task.CompletedTask;

        var payload = new
        {
            type = "set-language",
            language = languageCode
        };

        var json = JsonSerializer.Serialize(payload);

        _webView.CoreWebView2.PostWebMessageAsJson(json);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Resolves a .js language file name for TinyMCE based on the given culture name.
    /// </summary>
    /// <param name="cultureName">The culture name to resolve</param>
    /// <returns>A .js language file name or "en" for English</returns>
    private static string ResolveTinyMceLanguageCode(string cultureName)
    {
        if (string.IsNullOrWhiteSpace(cultureName))
            return "en";

        CultureInfo culture;
        try
        {
            culture = new CultureInfo(cultureName);
        }
        catch
        {
            return "en";
        }

        var iso2 = culture.TwoLetterISOLanguageName.ToLowerInvariant();

        // English uses TinyMCE's built-in language; no file
        if (iso2 == "en")
            return "en";

        var langsFolder = TinyMceInstaller.TinyMceLangsPath;
        if (!Directory.Exists(langsFolder))
            return "en";

        var files = Directory
            .GetFiles(langsFolder, "*.js")
            .Select(Path.GetFileNameWithoutExtension)
            .Where(f => !string.IsNullOrWhiteSpace(f))
            .ToList();

        // 1) Exact two-letter match: es.js, pt.js, zh.js, etc.
        var exact = files.FirstOrDefault(f =>
            string.Equals(f, iso2, StringComparison.OrdinalIgnoreCase));
        if (exact != null)
            return exact;

        // 2) First variant starting with "xx_": zh_CN, zh_TW, pt_BR, etc.
        var variant = files.FirstOrDefault(f =>
            f != null && f.StartsWith(iso2 + "_", StringComparison.OrdinalIgnoreCase));
        if (variant != null)
            return variant;

        // 3) Fallback: variant with dash ("xx-YY") if any
        variant = files.FirstOrDefault(f =>
            f != null && f.StartsWith(iso2 + "-", StringComparison.OrdinalIgnoreCase));
        return variant ?? "en";
    }

    /// <summary>
    ///     Sets the HTML content of the TinyMCE editor.
    ///     If the editor is not yet ready, this is applied once initialization completes.
    /// </summary>
    public async Task SetHtmlAsync(string htmlContent)
    {
        _pendingHtml = htmlContent;

        if (!IsEditorReady || _webView?.CoreWebView2 == null)
            return;

        await SendSetContentMessageAsync(_pendingHtml);
    }

    /// <summary>
    ///     Gets the current HTML content from the TinyMCE editor.
    /// </summary>
    public async Task<string> GetHtmlAsync()
    {
        if (!IsEditorReady || _webView?.CoreWebView2 == null)
            return _pendingHtml ?? string.Empty;

        var resultJson = await _webView.CoreWebView2.ExecuteScriptAsync(
            "window.__tinyHostApi.getContent();");

        string? html;
        try
        {
            html = JsonSerializer.Deserialize<string>(resultJson);
        }
        catch
        {
            return _pendingHtml ?? string.Empty;
        }

        _pendingHtml = html ?? string.Empty;
        return _pendingHtml;
    }

    /// <summary>
    ///     Changes the TinyMCE UI language at runtime.
    ///     This reinitializes the editor with the specified language and preserves the current content.
    /// </summary>
    /// <param name="languageCode">Language code, e.g., ""en_US"", ""fr_FR"" (must match a .js file in the langs folder).</param>
    public async Task SetLanguageAsync(string languageCode)
    {
        if (string.IsNullOrWhiteSpace(languageCode))
            throw new ArgumentException(@"Language code is required.", nameof(languageCode));

        var resolved = ResolveTinyMceLanguageCode(languageCode);
        _language = resolved;

        if (!IsEditorReady || _webView?.CoreWebView2 == null)
            return;

        await SendSetLanguageMessageAsync(resolved);
    }
}
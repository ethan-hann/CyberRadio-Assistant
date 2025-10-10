// ReplacementStationListBox.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
//
// GPL-3.0-or-later

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.custom_controls;

/// <summary>
/// Represents a custom ListBox control for displaying stations.
/// Original mode: status icons + text (replacement stations).
/// Extended mode: image from Resources.resx + text (vanilla stations).
/// </summary>
public sealed partial class ReplacementStationListBox : ListBox
{
    // ------------------- NEW: Rendering modes -------------------
    public enum ItemRenderMode
    {
        /// <summary>Original behavior: enabled/disabled icon (left), text, saved/edited icon (right).</summary>
        ReplacementStatusIcons = 0,

        /// <summary>Image (from Resources.resx) + text. Ideal for vanilla stations with logos.</summary>
        ImageAndText = 1
    }

    private ItemRenderMode _renderMode = ItemRenderMode.ReplacementStatusIcons;

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Selects how items are rendered.")]
    [DefaultValue(ItemRenderMode.ReplacementStatusIcons)]
    public ItemRenderMode RenderModeEx
    {
        get => _renderMode;
        set
        {
            if (_renderMode == value) return;
            _renderMode = value;
            // Switch draw modes appropriately
            if (_renderMode == ItemRenderMode.ImageAndText)
            {
                base.DrawMode = DrawMode.OwnerDrawVariable;
                if (_imageEdge < 16) _imageEdge = 40;
            }
            else
            {
                base.DrawMode = DrawMode.OwnerDrawFixed;
                ItemHeight = 16;
            }
            Invalidate();
        }
    }

    // ------------------- NEW: Delegates for vanilla rendering -------------------
    /// <summary>Returns the display text for the item (vanilla mode). If null, ToString() is used.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static Func<object, string>? TextSelector { get; set; }

    /// <summary>Returns the Resources.resx key for the item's image (vanilla mode). If null, sanitation fallback is used.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static Func<object, string>? ResourceKeySelector { get; set; }

    // ------------------- NEW: Image sizing for vanilla mode -------------------
    private static int _imageEdge = 48;

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Edge length (px) for item images when using ImageAndText mode.")]
    [DefaultValue(48)]
    public int ImageEdge
    {
        get => _imageEdge;
        set
        {
            _imageEdge = Math.Clamp(value, 16, 256);
            if (_renderMode == ItemRenderMode.ImageAndText)
                Invalidate();
        }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Space (px) between image and text in ImageAndText mode.")]
    [DefaultValue(10)]
    public int ImageTextGap { get; set; } = 10;

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Additional padding around each item (ImageAndText mode only).")]
    public new Padding Padding
    {
        get => base.Padding;
        set
        {
            base.Padding = value;
            if (_renderMode == ItemRenderMode.ImageAndText)
                Invalidate();
        }
    }

    // ------------------- ORIGINAL fields (kept) -------------------
    private string _disabledIconKey = "disabled";
    private Color _duplicateStationsColor = Color.Red;
    private Font _duplicateStationsFont = new(DefaultFont, FontStyle.Italic);
    private string _editedStationIconKey = "edited_station";
    private string _enabledIconKey = "enabled";
    private ImageList _imageList;
    private Color _newStationColor = Color.Green;
    private Font _newStationFont = new(DefaultFont, FontStyle.Bold);
    private string _savedStationIconKey = "saved_station";
    private Color _songsMissingColor = Color.Orange;
    private Font _songsMissingFont = new(DefaultFont, FontStyle.Bold);

    // ------------------- NEW: thumbnail cache (vanilla mode) -------------------
    private static readonly Dictionary<object, Image> _thumbCache = new(ReferenceEqualityComparer.Instance);

    /// <summary>
    /// Initializes a new instance of the <see cref="ReplacementStationListBox"/> class.
    /// </summary>
    public ReplacementStationListBox()
    {
        SetValues();
        _imageList ??= new ImageList();

        // Defaults for vanilla mode
        base.Padding = new Padding(8, 6, 8, 6);

        // Enable drag and drop (original replacement flow)
        AllowDrop = true;
        DragEnter += ReplacementStationListBox_DragEnter;
        DragDrop += ReplacementStationListBox_DragDrop;
    }

    // ------------------- ORIGINAL public properties (kept) -------------------
    [Browsable(true)]
    [Category("Icons")]
    [Description("Image list containing the icons for the list box (ReplacementStatusIcons mode).")]
    public ImageList ImageList
    {
        get => _imageList;
        set
        {
            _imageList = value;
            Invalidate();
        }
    }

    [Browsable(true)]
    [Category("Icons")]
    [Description("The key for the enabled icon in the ImageList.")]
    public string EnabledIconKey
    {
        get => _enabledIconKey;
        set { _enabledIconKey = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Icons")]
    [Description("The key for the disabled icon in the ImageList.")]
    public string DisabledIconKey
    {
        get => _disabledIconKey;
        set { _disabledIconKey = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Icons")]
    [Description("The key for the edited station icon in the ImageList.")]
    public string EditedStationIconKey
    {
        get => _editedStationIconKey;
        set { _editedStationIconKey = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Icons")]
    [Description("The key for the saved station icon in the ImageList.")]
    public string SavedStationIconKey
    {
        get => _savedStationIconKey;
        set { _savedStationIconKey = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Colors")]
    [Description("The color used to highlight stations with missing songs.")]
    public Color SongsMissingColor
    {
        get => _songsMissingColor;
        set { _songsMissingColor = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Colors")]
    [Description("The color used to highlight duplicate station names")]
    public Color DuplicateColor
    {
        get => _duplicateStationsColor;
        set { _duplicateStationsColor = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Colors")]
    [Description("The color used to highlight new stations.")]
    public Color NewStationColor
    {
        get => _newStationColor;
        set { _newStationColor = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Fonts")]
    [Description("The font used to highlight stations with missing songs.")]
    public Font SongsMissingFont
    {
        get => _songsMissingFont;
        set { _songsMissingFont = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Fonts")]
    [Description("The font used to highlight duplicate station names.")]
    public Font DuplicateFont
    {
        get => _duplicateStationsFont;
        set { _duplicateStationsFont = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Fonts")]
    [Description("The font used to highlight new stations.")]
    public Font NewStationFont
    {
        get => _newStationFont;
        set { _newStationFont = value; Invalidate(); }
    }

    /// <summary>
    /// Occurs whenever the station is imported from a .zip or .rar file.
    /// (Replacement flow only; irrelevant but harmless in vanilla mode.)
    /// </summary>
    public event EventHandler<List<Guid?>>? ReplacementStationsImported;

    private void ReplacementStationListBox_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data == null || !e.Data.GetDataPresent(DataFormats.FileDrop)) return;
        var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);
        if (files is not { Length: > 0 }) return;
        if (files.Any(file => file.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) ||
                              file.EndsWith(".rar", StringComparison.OrdinalIgnoreCase)))
            e.Effect = DragDropEffects.Copy;
    }

    private void ReplacementStationListBox_DragDrop(object? sender, DragEventArgs e)
    {
        if (e.Data == null || !e.Data.GetDataPresent(DataFormats.FileDrop)) return;
        var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);
        if (files is not { Length: > 0 }) return;

        List<Guid?> importedStationIds = [];

        foreach (var file in files)
            if (file.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) ||
                file.EndsWith(".rar", StringComparison.OrdinalIgnoreCase))
            {
                var stationId = StationManager.Instance.ImportVanillaStationFromArchive(file);
                if (stationId != null)
                    importedStationIds.Add(stationId);
            }

        ReplacementStationsImported?.Invoke(this, importedStationIds);
    }

    /// <summary>Sets default values for the control.</summary>
    private void SetValues()
    {
        base.DrawMode = DrawMode.OwnerDrawFixed;
        DoubleBuffered = true;
        ItemHeight = 16;
        BorderStyle = BorderStyle.FixedSingle;
    }

    // ------------------- ORIGINAL measurement kept; extended for vanilla -------------------
    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
        if (_renderMode == ItemRenderMode.ReplacementStatusIcons)
        {
            e.ItemHeight = 16; // fixed
            return;
        }

        // Image + text (variable height)
        if (e.Index < 0 || e.Index >= Items.Count)
        {
            e.ItemHeight = Math.Max(_imageEdge + Padding.Vertical, Font.Height + Padding.Vertical);
            return;
        }

        var item = Items[e.Index];
        string text = (TextSelector?.Invoke(item)) ?? item?.ToString() ?? string.Empty;

        int left = Padding.Left + _imageEdge + ImageTextGap;
        int right = Math.Max(0, e.ItemWidth - Padding.Right);
        int textWidth = Math.Max(10, right - left);

        using var g = CreateGraphics();
        var textSize = TextRenderer.MeasureText(g, text, Font,
            new Size(textWidth, int.MaxValue),
            TextFormatFlags.WordBreak | TextFormatFlags.NoPadding);

        int contentHeight = Math.Max(_imageEdge, textSize.Height);
        e.ItemHeight = Math.Max(contentHeight + Padding.Vertical, 20);
    }

    // ------------------- DRAW -------------------
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        try
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            if (_renderMode == ItemRenderMode.ReplacementStatusIcons)
            {
                DrawReplacementStatusItem(e);
            }
            else
            {
                DrawImageAndTextItem(e);
            }

            e.DrawFocusRectangle();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<ReplacementStationListBox>("OnDrawItem")
                .Error(ex, "An error occurred while drawing the item.");
        }
    }

    // ------------------- ORIGINAL DRAW -------------------
    private void DrawReplacementStatusItem(DrawItemEventArgs e)
    {
        if (Items[e.Index] is TrackableObject<ReplacementStation> station)
        {
            // Primary icon (enabled/disabled)
            var primaryIconKey = station.TrackedObject.IsActive ? _enabledIconKey : _disabledIconKey;

            if (_imageList.Images.ContainsKey(primaryIconKey))
            {
                var primaryIcon = _imageList.Images[primaryIconKey];
                if (primaryIcon != null) e.Graphics.DrawImage(primaryIcon, e.Bounds.Left, e.Bounds.Top, 16, 16);
            }

            // Secondary icon (edited/new vs saved)
            var secondaryIconKey = (station.IsPendingSave | StationManager.Instance.IsNewStation(station.Id))
                ? _editedStationIconKey : _savedStationIconKey;

            var iconX = e.Bounds.Right - 16 - 4;
            if (_imageList.Images.ContainsKey(secondaryIconKey))
            {
                var secondaryIcon = _imageList.Images[secondaryIconKey];
                if (secondaryIcon != null) e.Graphics.DrawImage(secondaryIcon, iconX, e.Bounds.Top, 16, 16);
            }

            // Text
            var textRect = new Rectangle(e.Bounds.Left + 20, e.Bounds.Top,
                e.Bounds.Width - 40 - 4, e.Bounds.Height);

            TextRenderer.DrawText(e.Graphics, station.TrackedObject.DisplayName, GetItemFont(station),
                textRect, GetItemColor(station), TextFormatFlags.Left);
        }
    }

    // ------------------- NEW DRAW: image (Resources) + text -------------------
    private void DrawImageAndTextItem(DrawItemEventArgs e)
    {
        var item = Items[e.Index];

        // Colors
        bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
        Color back = selected ? SystemColors.Highlight : e.BackColor;
        using (var backBrush = new SolidBrush(back))
            e.Graphics.FillRectangle(backBrush, e.Bounds);
        Color fore = selected ? SystemColors.HighlightText : e.ForeColor;

        // Layout
        Rectangle bounds = Rectangle.Inflate(e.Bounds, -1, -1);

        var imgRect = new Rectangle(
            bounds.Left + Padding.Left,
            bounds.Top + Padding.Top,
            _imageEdge, _imageEdge);

        var textRect = new Rectangle(
            imgRect.Right + ImageTextGap,
            bounds.Top + Padding.Top,
            Math.Max(10, bounds.Right - Padding.Right - (imgRect.Right + ImageTextGap)),
            Math.Max(10, bounds.Bottom - Padding.Bottom - (bounds.Top + Padding.Top)));

        // Resolve image from Resources
        var img = GetOrCreateThumb(item);

        // Draw image
        if (img != null)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            DrawImageContained(e.Graphics, img, imgRect);
        }

        // Draw text
        string text = (TextSelector?.Invoke(item)) ?? item?.ToString() ?? string.Empty;
        TextRenderer.DrawText(e.Graphics, text, Font, textRect, fore,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis);
    }

    // ------------------- Helpers: replacement mode coloring -------------------
    private Color GetItemColor(TrackableObject<ReplacementStation> station)
    {
        var returnColor = ForeColor;

        if (station.TrackedObject.Tracks.Any(s => !FileHelper.DoesFileExist(s.ReplacementFilePath)))
            returnColor = CombineColors(returnColor, _songsMissingColor);

        if (Items.OfType<TrackableObject<ReplacementStation>>().Count(s =>
                s.TrackedObject.DisplayName.Equals(station.TrackedObject.DisplayName, StringComparison.OrdinalIgnoreCase)) > 1)
            returnColor = CombineColors(returnColor, _duplicateStationsColor);

        if (StationManager.Instance.IsNewStation(station.Id))
            returnColor = CombineColors(returnColor, _newStationColor);

        return returnColor;
    }

    private Font GetItemFont(TrackableObject<ReplacementStation> station)
    {
        var font = Font;

        if (station.TrackedObject.Tracks.Any(s => !FileHelper.DoesFileExist(s.ReplacementFilePath)))
            font = _songsMissingFont;

        else if (Items.OfType<TrackableObject<ReplacementStation>>().Count(s =>
                     s.TrackedObject.DisplayName.Equals(station.TrackedObject.DisplayName, StringComparison.OrdinalIgnoreCase)) > 1)
            font = _duplicateStationsFont;

        else if (StationManager.Instance.IsNewStation(station.Id))
            font = _newStationFont;

        return font;
    }

    private static Color CombineColors(Color color1, Color color2)
    {
        var r = (color1.R + color2.R) / 2;
        var g = (color1.G + color2.G) / 2;
        var b = (color1.B + color2.B) / 2;
        return Color.FromArgb(r, g, b);
    }

    // ------------------- Helpers: vanilla thumbnails -------------------
    public static Image? GetOrCreateThumb(object item)
    {
        if (_thumbCache.TryGetValue(item, out var cached))
            return cached;

        try
        {
            var key = ResourceKeySelector?.Invoke(item) ?? DefaultResourceKeyFromItem(item);
            var img = ResolveResourceImage(key) ??
                      // smart fallbacks
                      ResolveResourceImage(ToUnderscoreKey(key)) ??
                      ResolveResourceImage(key.ToLowerInvariant());

            if (img == null)
            {
                // No logo found: create initials avatar from text (nice fallback).
                var text = (TextSelector?.Invoke(item)) ?? item?.ToString() ?? "?";
                img = CreateAvatar(text, _imageEdge);
            }

            // Cache a scaled copy to the configured edge
            var thumb = new Bitmap(_imageEdge, _imageEdge, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (var g = Graphics.FromImage(thumb))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.Clear(Color.Transparent);
                var fit = GetContainedRect(img.Size, new Rectangle(0, 0, _imageEdge, _imageEdge), 0.08f);
                g.DrawImage(img, fit);
            }

            _thumbCache[item] = thumb;
            return thumb;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Image resolution error: {ex}");
            return null;
        }
    }

    private static string DefaultResourceKeyFromItem(object item)
    {
        // Try to use a "StationName" or "DisplayName" property if available; else ToString().
        var t = item.GetType();
        var prop = t.GetProperty("StationName") ?? t.GetProperty("DisplayName") ?? t.GetProperty("Name");
        var val = prop?.GetValue(item) as string ?? item.ToString() ?? "unknown";
        return val.Trim();
    }

    private static string ToUnderscoreKey(string s)
    {
        var sb = new System.Text.StringBuilder(s.Length);
        foreach (var ch in s.Trim())
        {
            if (char.IsWhiteSpace(ch) || ch is '-' or '.' or '/')
                sb.Append('_');
            else if (char.IsLetterOrDigit(ch))
                sb.Append(ch);
            else
                sb.Append('_');
        }
        return sb.ToString();
    }

    private static Rectangle GetContainedRect(Size content, Rectangle dest, float paddingRatio)
    {
        int pad = (int)(Math.Min(dest.Width, dest.Height) * paddingRatio);
        var inner = Rectangle.Inflate(dest, -pad, -pad);

        float wr = (float)inner.Width / content.Width;
        float hr = (float)inner.Height / content.Height;
        float scale = Math.Min(wr, hr);

        int w = (int)Math.Round(content.Width * scale);
        int h = (int)Math.Round(content.Height * scale);

        int x = inner.Left + (inner.Width - w) / 2;
        int y = inner.Top + (inner.Height - h) / 2;
        return new Rectangle(x, y, w, h);
    }

    private static void DrawImageContained(Graphics g, Image img, Rectangle target)
    {
        var fit = GetContainedRect(img.Size, target, 0.08f);
        using var gp = new GraphicsPath();
        gp.AddRectangle(target);
        using var clip = new Region(gp);
        var old = g.Clip;
        g.Clip = clip;
        g.DrawImage(img, fit);
        g.Clip = old;
    }

    private static Image CreateAvatar(string text, int edge)
    {
        string initials = GetInitials(text, 2);
        var bmp = new Bitmap(edge, edge, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using var bg = new SolidBrush(AvatarColorFor(text));
        using var fg = new SolidBrush(Color.White);
        g.FillEllipse(bg, 0, 0, edge - 1, edge - 1);

        float fontSize = edge * 0.42f;
        using var f = new Font("Segoe UI", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
        var sz = g.MeasureString(initials, f);
        var pt = new PointF((edge - sz.Width) / 2f, (edge - sz.Height) / 2f - 1);
        g.DrawString(initials, f, fg, pt);
        g.DrawEllipse(Pens.Transparent, 0, 0, edge - 1, edge - 1);
        return bmp;
    }

    private static string GetInitials(string s, int max)
    {
        if (string.IsNullOrWhiteSpace(s)) return "?";
        var parts = s.Trim().Split(new[] { ' ', '\t', '-', '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
        var buf = new List<char>(max);
        foreach (var p in parts)
        {
            var c = char.ToUpperInvariant(p[0]);
            if (char.IsLetterOrDigit(c)) buf.Add(c);
            if (buf.Count == max) break;
        }
        if (buf.Count == 0) buf.Add(char.ToUpperInvariant(s[0]));
        return new string(buf.ToArray());
    }

    private static Color AvatarColorFor(string key)
    {
        unchecked
        {
            int h = 17;
            foreach (char c in key) h = h * 31 + c;
            int[] hues = { 200, 15, 260, 120, 340, 45, 180, 300, 95, 25 };
            int hue = hues[Math.Abs(h) % hues.Length];
            return HsvToColor(hue, 0.55f, 0.75f);
        }
    }

    private static Color HsvToColor(int h, float s, float v)
    {
        float C = v * s;
        float X = C * (1 - Math.Abs(((h / 60f) % 2) - 1));
        float m = v - C;
        float r = 0, g = 0, b = 0;
        if (h < 60) { r = C; g = X; b = 0; }
        else if (h < 120) { r = X; g = C; b = 0; }
        else if (h < 180) { r = 0; g = C; b = X; }
        else if (h < 240) { r = 0; g = X; b = C; }
        else if (h < 300) { r = X; g = 0; b = C; }
        else { r = C; g = 0; b = X; }
        return Color.FromArgb(
            255,
            (int)((r + m) * 255),
            (int)((g + m) * 255),
            (int)((b + m) * 255));
    }

    private static Image? ResolveResourceImage(string key)
    {
        // Looks up an image in Properties.Resources by key (object must be Image).
        // NOTE: ResourceManager returns object; cast to Image if compatible.
        object? obj = null;
        try
        {
            obj = global::RadioExt_Helper.Properties.Resources.ResourceManager.GetObject(key);
        }
        catch { /* ignore */ }

        return obj as Image;
    }

    /// <summary>
    /// Reference equality comparer for cache keys (items).
    /// </summary>
    private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceEqualityComparer Instance = new();
        public new bool Equals(object x, object y) => ReferenceEquals(x, y);
        public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
    }
}

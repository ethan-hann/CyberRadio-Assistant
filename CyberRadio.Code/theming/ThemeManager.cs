using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using AetherUtils.Core.Logging;
using RadioExt_Helper.utility;
using YamlDotNet.Serialization;

namespace RadioExt_Helper.theming;

/// <summary>
/// Responsible for managing themes for the application. Themes are stored as YAML files in the theme directory.
/// <para>This class is a singleton and should be accessed via the <see cref="Instance"/> property.</para>
/// </summary>
public class ThemeManager
{
    private static readonly object Lock = new();
    private static ThemeManager? _instance;

    /// <summary>
    /// Get the current theme.
    /// </summary>
    public Theme CurrentTheme { get; private set; } = new();

    /// <summary>
    /// Get the font used for the main application.
    /// </summary>
    public Font? MainFont { get; private set; }

    /// <summary>
    /// Get the font used for menu strips.
    /// </summary>
    public Font? MenuStripFont { get; private set; }

    /// <summary>
    /// Get a value indicating whether the theme manager has been initialized.
    /// </summary>
    public bool IsInitialized { get; private set; }

    /// <summary>
    /// Get a value indicating whether the application is using themes. If false, the default, designed theme will be used.
    /// </summary>
    public bool IsUsingThemes { get; set; }

    private readonly string _themeDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RadioExt-Helper", "themes");
    private readonly Assembly _executingAssembly = Assembly.GetExecutingAssembly();
    private readonly Dictionary<ToolStripItem, Dictionary<string, string>> _menuItemMetadata = new();
    private const string ResourceFolderPrefix = "RadioExt_Helper.resources";

    /// <summary>
    /// Get the singleton instance of the ThemeManager.
    /// </summary>
    public static ThemeManager Instance
    {
        get
        {
            lock (Lock)
            {
                return _instance ??= new ThemeManager();
            }
        }
    }

    private ThemeManager() { }

    /// <summary>
    /// Initialize the theme manager. This should be called once at application startup.
    /// </summary>
    public void Initialize()
    {
        try
        {
            if (!Directory.Exists(_themeDirectory))
                Directory.CreateDirectory(_themeDirectory);

            ExtractDefaultTheme("Default.yaml");
            ExtractDefaultTheme("Dark.yaml");
            ExtractDefaultTheme("Light.yaml");

            IsInitialized = true;
            IsUsingThemes = true;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("ThemeManager.Initialize").Error(ex, "Could not initialize theme engine!");
            IsInitialized = false;
            IsUsingThemes = false;
        }
    }

    private void ExtractDefaultTheme(string fileName)
    {
        var filePath = Path.Combine(_themeDirectory, fileName);
        var yamlText = _executingAssembly.ReadResource($"{ResourceFolderPrefix}.{fileName}");
        File.WriteAllText(filePath, yamlText);
    }

    /// <summary>
    /// Load a theme from the specified theme name and optionally apply it. This theme should exist as a YAML file in the theme directory.
    /// </summary>
    /// <param name="themeName">The name of the theme to load.</param>
    /// <param name="applyTheme">Indicate whether to immediately apply the theme.</param>
    public void LoadTheme(string themeName, bool applyTheme = true)
    {
        try
        {
            var themePath = Path.Combine(_themeDirectory, $"{themeName}.yaml");

            if (!File.Exists(themePath))
            {
                AuLogger.GetCurrentLogger("ThemeManager.LoadTheme").Error($"Theme file not found: {themePath}");
                return;
            }

            var yaml = File.ReadAllText(themePath);

            if (string.IsNullOrEmpty(yaml))
            {
                AuLogger.GetCurrentLogger("ThemeManager.LoadTheme").Error($"Theme file is empty: {themePath}");
                AuLogger.GetCurrentLogger("ThemeManager.LoadTheme").Info($"Loading default theme...");
                CurrentTheme = new Theme();
            }
            else
            {
                CurrentTheme = new DeserializerBuilder()
                    .IgnoreUnmatchedProperties()
                    .Build()
                    .Deserialize<Theme>(yaml);
            }

            if (applyTheme)
                ApplyTheme();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("ThemeManager.LoadTheme").Error(ex, $"Could not load theme: {themeName}.");
        }
    }

    /// <summary>
    /// Apply the current theme to all controls and forms in the application. Will dynamically apply the theme to new forms and controls as they are created.
    /// </summary>
    public void ApplyTheme()
    {
        if (!IsInitialized || !IsUsingThemes) return;


        foreach (Form form in Application.OpenForms)
        {
            // Prevent duplicate event handlers
            form.ControlAdded -= OnControlAdded;
            form.ControlAdded += OnControlAdded;

            ApplyThemeToControl(form);
            form.Invalidate(); // Force UI refresh
        }
    }

    /// <summary>
    /// Apply the current theme to the specified user control and all of its children.
    /// </summary>
    public void ApplyThemeToUserControl(Control baseControl)
    {
        if (!IsInitialized || !IsUsingThemes) return;

        if (baseControl is not UserControl userControl) return;

        foreach (Control control in baseControl.Controls)
        {
            if (control is UserControl nestedControl)
                ApplyThemeToUserControl(nestedControl);

            ApplyThemeToControl(userControl);
            userControl.Refresh();  // Force UI refresh
        }
    }

    /// <summary>
    /// Event handler for when a new control is added to a form. This will apply the current theme to the control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnControlAdded(object? sender, ControlEventArgs e)
    {
        if (!IsInitialized || !IsUsingThemes) return;

        if (e.Control is UserControl)
            ApplyThemeToUserControl(e.Control);
        else
            ApplyThemeToControl(e.Control);
    }

    /// <summary>
    /// Apply the current theme to the specified control and all of its children.
    /// </summary>
    /// <param name="control"></param>
    public void ApplyThemeToControl(Control? control)
    {
        if (!IsInitialized || !IsUsingThemes) return;
        if (control == null || control.IsDisposed) return;

        if (MenuStripFont == null || MainFont == null)
        {
            // Validate font size
            var validatedFontSize = CurrentTheme.FontSize > 0 ? CurrentTheme.FontSize : 9.0f;
            var validatedMenuFontSize = CurrentTheme.MenuStripFontSize > 0 ? CurrentTheme.MenuStripFontSize : 9.0f;

            // Validate font family
            var themeFontFamily = FontExists(CurrentTheme.FontFamily) ? new FontFamily(CurrentTheme.FontFamily) : SystemFonts.DefaultFont.FontFamily;
            var menuFontFamily = FontExists(CurrentTheme.MenuStripFontFamily) ? new FontFamily(CurrentTheme.MenuStripFontFamily) : SystemFonts.DefaultFont.FontFamily;

            // Handle SemiBold manually for Segoe UI
            if (CurrentTheme.FontFamily.Equals("Segoe UI", StringComparison.OrdinalIgnoreCase) &&
                CurrentTheme.FontStyle.Equals("semibold", StringComparison.OrdinalIgnoreCase))
            {
                themeFontFamily = new FontFamily("Segoe UI Semibold");
            }

            if (CurrentTheme.MenuStripFontFamily.Equals("Segoe UI", StringComparison.OrdinalIgnoreCase) &&
                CurrentTheme.MenuStripFontStyle.Equals("semibold", StringComparison.OrdinalIgnoreCase))
            {
                menuFontFamily = new FontFamily("Segoe UI Semibold");
            }

            // Validate font style
            var themeFontStyle = ParseFontStyle(CurrentTheme.FontStyle);
            var menuFontStyle = ParseFontStyle(CurrentTheme.MenuStripFontStyle);

            // Create final fonts
            MainFont = new Font(themeFontFamily, validatedFontSize, themeFontStyle);
            MenuStripFont = new Font(menuFontFamily, validatedMenuFontSize, menuFontStyle);
        }

        // Apply high contrast settings if enabled
        if (CurrentTheme.HighContrast)
        {
            control.BackColor = SystemColors.Control;
            control.ForeColor = SystemColors.ControlText;
            control.Font = SystemFonts.DefaultFont;
        }
        else
        {
            control.Font = MainFont;
        }

        // Apply theme to different control types
        switch (control)
        {
            case Form form:
                form.BackColor = CurrentTheme.BackgroundColor;
                form.ForeColor = CurrentTheme.ForegroundColor;
                break;
            case Button btn:
                ApplyButtonStyle(btn);
                break;
            case TextBox txt:
                txt.BackColor = CurrentTheme.TextBoxBackground;
                txt.ForeColor = CurrentTheme.TextBoxTextColor;
                break;
            case GroupBox gb:
                gb.ForeColor = CurrentTheme.ForegroundColor;
                gb.BackColor = CurrentTheme.BackgroundColor;
                break;
            case RadioButton:
            case CheckBox:
            case Label:
                control.ForeColor = CurrentTheme.LabelTextColor;
                break;
            case ListView lv:
                lv.BackColor = CurrentTheme.ListViewBackground;
                lv.ForeColor = CurrentTheme.ListViewTextColor;
                break;
            case ListBox:
            case ComboBox:
            case TrackBar:
                control.BackColor = CurrentTheme.BackgroundColor;
                control.ForeColor = CurrentTheme.LabelTextColor;
                break;
            case DataGridView dgv:
                dgv.BackgroundColor = CurrentTheme.BackgroundColor;
                dgv.GridColor = CurrentTheme.BackgroundColor;
                dgv.ForeColor = CurrentTheme.TextBoxTextColor;
                break;
            case MenuStrip:
            case ToolStrip:
                control.BackColor = CurrentTheme.MenuStripBackground;
                control.ForeColor = CurrentTheme.MenuStripTextColor;
                control.Font = MenuStripFont;
                break;
            case Panel:
            case TabControl:
                control.BackColor = CurrentTheme.BackgroundColor;
                control.ForeColor = CurrentTheme.ForegroundColor;
                break;
        }

        // Apply icons if using a custom icon set
        var iconSet = ParseIconSet(CurrentTheme.IconSet);
        if (iconSet != IconTheme.Default)
            ApplyIconTheme(control);

        // Recursively apply theme to child controls
        foreach (Control child in control.Controls)
        {
            if (child is UserControl)
                ApplyThemeToUserControl(child);
            else
                ApplyThemeToControl(child);
        }
    }

    /// <summary>
    /// Checks if a font exists on the system.
    /// </summary>
    /// <param name="fontName"></param>
    /// <returns></returns>
    private static bool FontExists(string fontName)
    {
        using var fontsCollection = new InstalledFontCollection();
        return fontsCollection.Families.Any(f => f.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Parses a string to a valid FontStyle. Defaults to Regular if invalid.
    /// </summary>
    /// <param name="fontStyle"></param>
    /// <returns></returns>
    private FontStyle ParseFontStyle(string fontStyle)
    {
        return fontStyle.ToLower() switch
        {
            "bold" => FontStyle.Bold,
            "italic" => FontStyle.Italic,
            "underline" => FontStyle.Underline,
            "strikeout" => FontStyle.Strikeout,
            "semibold" => FontStyle.Regular, // Handle separately for specific font families
            _ => FontStyle.Regular
        };
    }

    /// <summary>
    /// Parses a string to a valid IconTheme. Defaults to Default if invalid.
    /// </summary>
    /// <param name="iconSet"></param>
    /// <returns></returns>
    private static IconTheme ParseIconSet(string iconSet)
    {
        return iconSet.ToLower() switch
        {
            "default" => IconTheme.Default,
            "light" => IconTheme.Light,
            "dark" => IconTheme.Dark,
            _ => IconTheme.Default
        };
    }

    /// <summary>
    /// Apply the current theme to the specified button.
    /// </summary>
    /// <param name="btn"></param>
    private void ApplyButtonStyle(Button btn)
    {
        // Extract metadata
        var metadata = btn.GetMetadata();
    
        // Determine button style based on metadata
        var styleType = metadata.GetValueOrDefault("style", "primary");
    
        var style = styleType switch
        {
            "secondary" => CurrentTheme.SecondaryButton,
            "danger" => CurrentTheme.DangerButton,
            _ => CurrentTheme.PrimaryButton
        };

        // Apply styles
        btn.BackColor = style.BackgroundColor;
        btn.ForeColor = style.TextColor;

        btn.Region = style.BorderRadius > 0 ? new Region(CreateRoundedRectanglePath(btn.ClientRectangle, style.BorderRadius)) : null;

        btn.FlatStyle = style.IsFlat ? FlatStyle.Flat : FlatStyle.Standard;
        btn.FlatAppearance.MouseDownBackColor = style.MouseDownBackColor;
        btn.FlatAppearance.MouseOverBackColor = style.MouseOverBackColor;
        btn.FlatAppearance.BorderColor = style.BorderColor;
        btn.FlatAppearance.BorderSize = style.BorderThickness;

        if (style is { UseShadow: true, IsFlat: false })
        {
            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Dark(style.BackgroundColor);
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.DarkDark(style.BackgroundColor);
        }

        // Validate font size
        var validatedFontSize = style.FontSize > 0 ? style.FontSize : 9.0f;

        // Validate font family
        var themeFontFamily = FontExists(style.FontFamily) ? new FontFamily(style.FontFamily) : SystemFonts.DefaultFont.FontFamily;

        // Handle SemiBold manually for Segoe UI
        if (style.FontFamily.Equals("Segoe UI", StringComparison.OrdinalIgnoreCase) &&
            style.FontStyle.Equals("semibold", StringComparison.OrdinalIgnoreCase))
        {
            themeFontFamily = new FontFamily("Segoe UI Semibold");
        }

        // Validate font style
        var themeFontStyle = ParseFontStyle(style.FontStyle);

        btn.Font = new Font(themeFontFamily, validatedFontSize, themeFontStyle);
    }

    /// <summary>
    /// Apply the current icon theme to the specified control.
    /// </summary>
    /// <param name="control"></param>
    private void ApplyIconTheme(Control control)
    {
        switch (control)
        {
            case Button btn:
            {
                var metadata = btn.GetMetadata();
        
                var iconName = metadata.GetValueOrDefault("icon", string.Empty);
                if (string.IsNullOrEmpty(iconName)) return;

                var fullIconName = $"{iconName}_{CurrentTheme.IconSet.ToLower()}";
                var image = _executingAssembly.GetEmbeddedIcon($"{fullIconName}");
                btn.Image = image;
                break;
            }
            case MenuStrip ms:
            {
                foreach (ToolStripMenuItem item in ms.Items)
                {
                    ApplyIconToMenuItem(item);
                }

                break;
            }
            case ToolStrip ts:
            {
                foreach (ToolStripItem item in ts.Items)
                {
                    if (item is ToolStripMenuItem menuItem)
                    {
                        ApplyIconToMenuItem(menuItem);
                    }
                }

                break;
            }
            default:
            {
                if (control is StatusStrip st)
                {
                    foreach (ToolStripItem item in st.Items)
                    {
                        if (item is ToolStripMenuItem menuItem)
                        {
                            ApplyIconToMenuItem(menuItem);
                        }
                    }
                }
                break;
            }
        }
    }

    /// <summary>
    /// Apply the current theme to the specified <see cref="ToolStripControlHost"/>.
    /// </summary>
    /// <param name="host"></param>
    public void ApplyThemeToCustomMenuControls(ToolStripControlHost host)
    {
        host.Font = MenuStripFont ?? SystemFonts.DefaultFont;
        host.BackColor = CurrentTheme.MenuStripBackground;
        host.ForeColor = CurrentTheme.MenuStripTextColor;
    }

    /// <summary>
    /// Apply the current icon theme to the specified menu item and all of its children.
    /// </summary>
    /// <param name="item">The <see cref="ToolStripMenuItem"/> to apply the icon theme to.</param>
    private void ApplyIconToMenuItem(ToolStripMenuItem item)
    {
        //Always apply the colors to the menu item regardless of the icon
        item.BackColor = CurrentTheme.MenuStripBackground;
        item.ForeColor = CurrentTheme.MenuStripTextColor;

        var metadata = GetMenuItemMetadata(item);

        var iconName = metadata.GetValueOrDefault("icon", string.Empty);

        if (string.IsNullOrEmpty(iconName)) return;

        var fullIconName = $"{iconName}_{CurrentTheme.IconSet}";
        var image = _executingAssembly.GetEmbeddedIcon($"{fullIconName}");
        item.Image = image;
        
        foreach (ToolStripItem subItem in item.DropDownItems)
        {
            if (subItem is ToolStripMenuItem subMenuItem)
            {
                ApplyIconToMenuItem(subMenuItem);
            }
        }
    }

    /// <summary>
    /// Create a rounded rectangle path with the specified radius.
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
    {
        GraphicsPath path = new();
        var diameter = Math.Min(radius * 2, Math.Min(rect.Width, rect.Height));

        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }

    /// <summary>
    /// Sets metadata for a ToolStripItem (like a ToolStripMenuItem).
    /// </summary>
    public void SetMenuItemMetadata(ToolStripItem item, string key, string value)
    {
        if (!_menuItemMetadata.ContainsKey(item))
            _menuItemMetadata[item] = new Dictionary<string, string>();

        _menuItemMetadata[item][key] = value;
    }

    /// <summary>
    /// Retrieves metadata for a ToolStripItem.
    /// </summary>
    public Dictionary<string, string> GetMenuItemMetadata(ToolStripItem item)
    {
        return _menuItemMetadata.TryGetValue(item, out var metadata) ? metadata : new Dictionary<string, string>();
    }

    /// <summary>
    /// Save the selected theme to the configuration file.
    /// </summary>
    /// <param name="themeName">The name of the theme selected.</param>
    public void SaveSelectedTheme(string themeName)
    {
        //Check if theme exists before saving
        if (!File.Exists(Path.Combine(_themeDirectory, $"{themeName}.yaml")))
        {
            AuLogger.GetCurrentLogger("ThemeManager.SaveSelectedTheme").Error($"Theme file not found: {themeName}");
            return;
        }

        GlobalData.ConfigManager.Set("themeName", themeName);
        GlobalData.ConfigManager.Save();
    }

    /// <summary>
    /// Get the saved theme name from the configuration file.
    /// </summary>
    /// <returns></returns>
    public string GetSavedTheme()
    {
        var themeName = GlobalData.ConfigManager.Get("themeName") as string;
        return themeName ?? "Light";
    }
}
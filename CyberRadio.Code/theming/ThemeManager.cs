using System.Drawing.Drawing2D;
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
    /// Get a value indicating whether the theme manager has been initialized.
    /// </summary>
    public bool IsInitialized { get; private set; }

    private readonly string _themeDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RadioExt-Helper", "themes");
    private readonly Assembly _executingAssembly = Assembly.GetExecutingAssembly();
    private readonly Dictionary<ToolStripItem, Dictionary<string, string>> _menuItemMetadata = new();
    private const string ResourcePrefix = "RadioExt_Helper.resources";

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

            ExtractDefaultTheme("Dark.yaml");
            ExtractDefaultTheme("Light.yaml");

            IsInitialized = true;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("ThemeManager.Initialize").Error(ex, "Could not initialize theme engine!");
            IsInitialized = false;
        }
    }

    private void ExtractDefaultTheme(string fileName)
    {
        var filePath = Path.Combine(_themeDirectory, fileName);
        if (File.Exists(filePath)) return;

        var yamlText = _executingAssembly.ReadResource($"{ResourcePrefix}.{fileName}");
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
            CurrentTheme = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .Build()
                .Deserialize<Theme>(yaml);

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
        ApplyThemeToControl(e.Control);
    }

    /// <summary>
    /// Apply the current theme to the specified control and all of its children.
    /// </summary>
    /// <param name="control"></param>
    public void ApplyThemeToControl(Control? control)
    {
        if (control == null) return;

        var themeFont = new Font(CurrentTheme.FontFamily, CurrentTheme.FontSize);

        if (CurrentTheme.HighContrast)
        {
            control.BackColor = Color.Black;
            control.ForeColor = Color.Yellow;
            control.Font = new Font(CurrentTheme.FontFamily, CurrentTheme.FontSize, FontStyle.Bold);
        }
        else
        {
            control.Font = themeFont;
        }

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
            case ListBox lb:
                lb.BackColor = CurrentTheme.BackgroundColor;
                lb.ForeColor = CurrentTheme.LabelTextColor;
                break;
            case MenuStrip:
            case ToolStrip:
                control.BackColor = CurrentTheme.MenuStripBackground;
                control.ForeColor = CurrentTheme.MenuStripTextColor;
                break;
            case Panel:
            case TabControl:
                control.BackColor = CurrentTheme.BackgroundColor;
                control.ForeColor = CurrentTheme.ForegroundColor;
                break;
        }

        ApplyIconTheme(control);

        foreach (Control child in control.Controls)
        {
            ApplyThemeToControl(child);
        }
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
        btn.FlatAppearance.BorderColor = style.BorderColor;
        btn.FlatAppearance.BorderSize = style.BorderThickness;

        if (style.BorderRadius > 0)
        {
            btn.FlatStyle = FlatStyle.Standard;
            btn.Region = new Region(CreateRoundedRectanglePath(btn.ClientRectangle, style.BorderRadius));
        }
        else
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.Region = null;
        }

        if (style.UseShadow)
        {
            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Dark(style.BackgroundColor);
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.DarkDark(style.BackgroundColor);
        }

        btn.Font = new Font(CurrentTheme.FontFamily, CurrentTheme.FontSize);
    }

    /// <summary>
    /// Apply the current icon theme to the specified control.
    /// </summary>
    /// <param name="control"></param>
    private void ApplyIconTheme(Control control)
    {
        if (control is Button btn)
        {
            var metadata = btn.GetMetadata();
        
            var iconName = metadata.GetValueOrDefault("icon", string.Empty);
            if (string.IsNullOrEmpty(iconName)) return;

            var fullIconName = $"{iconName}_{CurrentTheme.IconSet}";
            btn.Image = _executingAssembly.GetEmbeddedIcon($"{ResourcePrefix}.{fullIconName}");
            btn.ImageAlign = ContentAlignment.MiddleLeft;
        }
        else if (control is MenuStrip ms)
        {
            foreach (ToolStripMenuItem item in ms.Items)
            {
                ApplyIconToMenuItem(item);
            }
        }
    }

    /// <summary>
    /// Apply the current icon theme to the specified menu item and all of its children.
    /// </summary>
    /// <param name="item">The <see cref="ToolStripMenuItem"/> to apply the icon theme to.</param>
    private void ApplyIconToMenuItem(ToolStripMenuItem item)
    {
        var metadata = GetMenuItemMetadata(item);

        var iconName = metadata.GetValueOrDefault("icon", string.Empty);

        if (string.IsNullOrEmpty(iconName)) return;

        var fullIconName = $"{iconName}_{CurrentTheme.IconSet}";
        item.Image = _executingAssembly.GetEmbeddedIcon($"{ResourcePrefix}.{fullIconName}");

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
        {
            _menuItemMetadata[item] = new Dictionary<string, string>();
        }

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
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using AetherUtils.Core.Configuration;
using AetherUtils.Core.Logging;
using RadioExt_Helper.config;
using RadioExt_Helper.forms;
using RadioExt_Helper.models;

namespace RadioExt_Helper.utility;

public static class GlobalData
{
    /// <summary>
    ///     The resource manager responsible for keeping translations of strings.
    /// </summary>
    public static readonly ResourceManager Strings = new("RadioExt_Helper.Strings", typeof(MainForm).Assembly);

    private static bool _uiIconsInitialized;
    private static bool _globalDataInitialized;

    public static CyberConfigManager ConfigManager { get; private set; } = new("");

    /// <summary>
    ///     A list of strings containing all UIIcon records in the game. This list is populated from an embedded text file.
    /// </summary>
    private static BindingList<string> UiIcons { get; set; } = [];

    private static ComboBox UiIconsComboTemplate { get; set; } = new();

    private static Assembly ExecAssembly { get; } = Assembly.GetExecutingAssembly();

    public static List<TrackableObject<Station>> TrackedStations = [];

    /// <summary>
    ///     <para>
    ///         Initializes the global data for the application. This includes getting all the embedded resources,
    ///         setting the initial application culture, and creating the combo box template for the UIIcons.
    ///     </para>
    ///     <para>This method should only be called once. Subsequent calls will have no effect.</para>
    /// </summary>
    public static void Initialize()
    {
        if (_globalDataInitialized) return;

        GetUiIcons();
        CreateComboBoxTemplate();

        InitializeConfig();
        InitializeLogging();

        SetCulture(ConfigManager.Get("language") as string ?? "English (en)");

        _globalDataInitialized = true;
    }

    /// <summary>
    /// Get the path to the log file directory.
    /// </summary>
    /// <returns>The path to the log file directory or <see cref="string.Empty"/> if log options not defined in configuration.</returns>
    public static string GetLogPath()
    {
        if (ConfigManager.Get("logOptions") is not LogOptions options) return string.Empty;
        return options.LogFileDirectory;
    }

    private static void InitializeConfig()
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RadioExt-Helper", "config.yml");
        ConfigManager = new CyberConfigManager(path);
        if (ConfigManager.ConfigExists)
        {
            ConfigManager.Load();
        }
        else
        {
            ConfigManager.CreateDefaultConfig();
            ConfigManager.Save();
        }
    }

    private static void InitializeLogging()
    {
        if (ConfigManager.Get("logOptions") is not LogOptions options) return;

        AuLogger.Initialize(options);
        AuLogger.GetCurrentLogger("GlobalData.InitializeLogging").Info("Logging initialized!");
    }

    private static void CreateComboBoxTemplate()
    {
        UiIconsComboTemplate = new ComboBox
        {
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.White,
            DropDownStyle = ComboBoxStyle.DropDown,
            Name = "cmbUIIconsTemplate",
            Anchor = AnchorStyles.Left | AnchorStyles.Right,
            Dock = DockStyle.None,
            AutoCompleteMode = AutoCompleteMode.Suggest,
            AutoCompleteSource = AutoCompleteSource.ListItems,
            DataSource = UiIcons,
            DisplayMember = "Name",
            MaxDropDownItems = 24
        };
    }

    /// <summary>
    ///     Get a clone of the combo box holding all the UIIcons. This is faster than creating new combo boxes and
    ///     manually setting the data source.
    /// </summary>
    /// <returns>A ComboBox that is a clone of the template combo box. The data source is already set.</returns>
    //public static ComboBox? CloneTemplateComboBox()
    //{
    //    if (UiIconsComboTemplate.IsHandleCreated)
    //        return UiIconsComboTemplate.Invoke(new Func<ComboBox?>(CloneTemplateComboBox));
    //    else
    //        return null;
    //}

    public static ComboBox CloneTemplateComboBox()
    {
        return new ComboBox
        {
            // Copy basic properties
            Location = UiIconsComboTemplate.Location,
            Size = UiIconsComboTemplate.Size,
            DropDownStyle = UiIconsComboTemplate.DropDownStyle,
            FormattingEnabled = UiIconsComboTemplate.FormattingEnabled,
            MaxDropDownItems = UiIconsComboTemplate.MaxDropDownItems,
            IntegralHeight = UiIconsComboTemplate.IntegralHeight,
            ItemHeight = UiIconsComboTemplate.ItemHeight,
            MaxLength = UiIconsComboTemplate.MaxLength,
            Sorted = UiIconsComboTemplate.Sorted,
            TabIndex = UiIconsComboTemplate.TabIndex,
            Enabled = UiIconsComboTemplate.Enabled,
            Visible = UiIconsComboTemplate.Visible,
            Anchor = UiIconsComboTemplate.Anchor,
            Dock = UiIconsComboTemplate.Dock,
            Margin = UiIconsComboTemplate.Margin,
            Padding = UiIconsComboTemplate.Padding,
            RightToLeft = UiIconsComboTemplate.RightToLeft,
            Font = UiIconsComboTemplate.Font,
            ForeColor = UiIconsComboTemplate.ForeColor,
            BackColor = UiIconsComboTemplate.BackColor,
            DropDownWidth = UiIconsComboTemplate.DropDownWidth,
            DropDownHeight = UiIconsComboTemplate.DropDownHeight,
            FlatStyle = UiIconsComboTemplate.FlatStyle,
            DataSource = UiIconsComboTemplate.DataSource,
            DisplayMember = UiIconsComboTemplate.DisplayMember,
            ValueMember = UiIconsComboTemplate.ValueMember,
            BindingContext = UiIconsComboTemplate.BindingContext,
            SelectedIndex = UiIconsComboTemplate.SelectedIndex,
            SelectedItem = UiIconsComboTemplate.SelectedItem,
            SelectedValue = UiIconsComboTemplate.SelectedValue,
            AutoCompleteMode = UiIconsComboTemplate.AutoCompleteMode,
            AutoCompleteSource = UiIconsComboTemplate.AutoCompleteSource
        };
    }

    /// <summary>
    ///     Sets the UI culture to the value passed in. This method expects the culture to be formatted like so:
    ///     <c>English (en)</c>, where the culture code is in parentheses at the end of the string.
    /// </summary>
    /// <param name="culture">The culture to change the UI to.</param>
    public static void SetCulture(string culture)
    {
        var parsedCulture = culture.Substring(culture.IndexOf('(') + 1,
            culture.Length - culture.LastIndexOf(')') + 1);
        CultureInfo.CurrentUICulture = new CultureInfo(parsedCulture);
    }

    private static void GetUiIcons()
    {
        if (_uiIconsInitialized) return;
        try
        {
            List<string> strings = [];

            var txtData = ExecAssembly.ReadResource("RadioExt_Helper.resources.final_ui_icon_strings.txt");
            strings.AddRange(txtData.Split('\n').Select(line => line.TrimEnd().TrimStart()));
            UiIcons = new BindingList<string>(strings.Distinct().ToList());

            _uiIconsInitialized = true;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("GlobalData.GetUiIcons")
                .Error(ex, "Couldn't initialize list of UIIcons.");
        }
    }
}
using System.ComponentModel;
using RadioExt_Helper.models;

namespace RadioExt_Helper.user_controls;

public sealed partial class StationListBox : ListBox
{
    private string _disabledIconKey = "disabled";
    private string _enabledIconKey = "enabled";
    private string _editedStationIconKey = "edited_station";
    private string _savedStationIconKey = "saved_station";

    private ImageList _imageList;

    /// <summary>
    ///     Initializes a new instance of the <see cref="StationListBox" /> class.
    /// </summary>
    public StationListBox()
    {
        InitializeComponent();

        SetValues();
        _imageList ??= new ImageList();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StationListBox" /> class.
    /// </summary>
    public StationListBox(IContainer container)
    {
        container.Add(this);

        InitializeComponent();

        SetValues();
        _imageList ??= new ImageList();
    }

    /// <summary>
    ///     Gets or sets the ImageList containing the icons for the list box.
    /// </summary>
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Image list containing the icons for the list box.")]
    public ImageList ImageList
    {
        get => _imageList;
        set
        {
            _imageList = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     Gets or sets the key for the enabled icon in the ImageList.
    /// </summary>
    [Browsable(true)]
    [Category("Appearance")]
    [Description("The key for the enabled icon in the ImageList.")]
    public string EnabledIconKey
    {
        get => _enabledIconKey;
        set
        {
            _enabledIconKey = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     Gets or sets the key for the disabled icon in the ImageList.
    /// </summary>
    [Browsable(true)]
    [Category("Appearance")]
    [Description("The key for the disabled icon in the ImageList.")]
    public string DisabledIconKey
    {
        get => _disabledIconKey;
        set
        {
            _disabledIconKey = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     Gets or sets the key for the edited station icon in the ImageList.
    /// </summary>
    [Browsable(true)]
    [Category("Appearance")]
    [Description("The key for the edited station icon in the ImageList.")]
    public string EditedStationIconKey
    {
        get => _editedStationIconKey;
        set
        {
            _editedStationIconKey = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     Gets or sets the key for the saved station icon in the ImageList.
    /// </summary>
    [Browsable(true)]
    [Category("Appearance")]
    [Description("The key for the saved station icon in the ImageList.")]
    public string SavedStationIconKey
    {
        get => _savedStationIconKey;
        set
        {
            _savedStationIconKey = value;
            Invalidate();
        }
    }

    private void SetValues()
    {
        DrawMode = DrawMode.OwnerDrawFixed;
        DoubleBuffered = true;
        ItemHeight = 16;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        if (e.Index < 0) return;

        e.DrawBackground();

        if (Items[e.Index] is Station station)
        {
            // Determine the primary icon (enabled/disabled)
            var primaryIconKey = station.GetStatus() ? _enabledIconKey : _disabledIconKey;

            // Draw the primary icon
            if (_imageList.Images.ContainsKey(primaryIconKey))
            {
                var primaryIcon = _imageList.Images[primaryIconKey];
                if (primaryIcon != null)
                {
                    e.Graphics.DrawImage(primaryIcon, e.Bounds.Left, e.Bounds.Top, 16, 16);
                }
            }

            // Determine the secondary icon (changes pending/saved)
            var secondaryIconKey = station.PendingSave ? _editedStationIconKey : _savedStationIconKey;

            // Calculate the position for the secondary icon at the right edge
            var iconX = e.Bounds.Right - 16 - 4; // 16 is the icon width, 4 is some padding from the edge

            // Draw the secondary icon
            if (_imageList.Images.ContainsKey(secondaryIconKey))
            {
                var secondaryIcon = _imageList.Images[secondaryIconKey];
                if (secondaryIcon != null)
                {
                    e.Graphics.DrawImage(secondaryIcon, iconX, e.Bounds.Top, 16, 16);
                }
            }

            // Draw the text
            var textRect = new Rectangle(e.Bounds.Left + 20, e.Bounds.Top, e.Bounds.Width - 40 - 4, e.Bounds.Height); // Adjust width to leave space for the secondary icon
            TextRenderer.DrawText(e.Graphics, station.MetaData.DisplayName, e.Font, textRect, e.ForeColor, TextFormatFlags.Left);
        }

        e.DrawFocusRectangle();
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
        e.ItemHeight = 16; // Adjust height based on icon size
    }
}
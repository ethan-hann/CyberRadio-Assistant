using System.ComponentModel;
using RadioExt_Helper.models;

namespace RadioExt_Helper.user_controls;

public sealed partial class StationListBox : ListBox
{
    private string _disabledIconKey = "disabled";
    private string _enabledIconKey = "enabled";
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

    private void SetValues()
    {
        DrawMode = DrawMode.OwnerDrawFixed;
        DoubleBuffered = true;
        ItemHeight = 16;
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
    
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        if (e.Index < 0) return;

        e.DrawBackground();

        if (Items[e.Index] is Station station)
        {
            var iconKey = station.GetStatus() ? _enabledIconKey : _disabledIconKey;

            //Draw the icon
            if (_imageList.Images.ContainsKey(iconKey))
            {
                var icon = _imageList.Images[iconKey];
                if (icon != null)
                    e.Graphics.DrawImage(icon, e.Bounds.Left, e.Bounds.Top, 16, 16);
            }

            //Draw the text
            var textRect = new Rectangle(e.Bounds.Left + 20, e.Bounds.Top, e.Bounds.Width - 20, e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, station.MetaData.DisplayName, e.Font, textRect, e.ForeColor,
                TextFormatFlags.Left);
        }

        e.DrawFocusRectangle();
    }
    
    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
        e.ItemHeight = 16; // Adjust height based on icon size
    }
}
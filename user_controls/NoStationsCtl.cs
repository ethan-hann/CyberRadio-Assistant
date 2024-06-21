using AetherUtils.Core.Reflection;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls;

public sealed partial class NoStationsCtl : UserControl, IUserControl
{
    public NoStationsCtl()
    {
        InitializeComponent();
        Dock = DockStyle.Fill;
    }

    public Station Station => new();

    public void Translate()
    {
        lblNoStations.Text = GlobalData.Strings.GetString("NoStationsYet");
    }

    public void ApplyFonts()
    {
        ApplyFontsToControls(this);
    }

    private void NoStationsCtl_Load(object sender, EventArgs e)
    {
        Translate();
    }

    private static void ApplyFontsToControls(Control control)
    {
        switch (control)
        {
            case MenuStrip:
            case GroupBox:
            case Button:
                FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 9, FontStyle.Bold);
                break;
            case TabControl:
                FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 12, FontStyle.Bold);
                break;
            case Label:
                FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 28, FontStyle.Bold);
                break;
        }

        foreach (Control child in control.Controls)
            ApplyFontsToControls(child);
    }
}
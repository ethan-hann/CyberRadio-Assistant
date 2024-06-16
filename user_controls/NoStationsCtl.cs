using AetherUtils.Core.Reflection;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadioExt_Helper.user_controls
{
    public partial class NoStationsCtl : UserControl, IUserControl
    {
        public Station Station => new();

        public NoStationsCtl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private void NoStationsCtl_Load(object sender, EventArgs e)
        {
            Translate();
        }

        public void Translate()
        {
            lblNoStations.Text = GlobalData.Strings.GetString("NoStationsYet");
        }

        public void ApplyFonts()
        {
            ApplyFontsToControls(this);
        }

        private void ApplyFontsToControls(Control control)
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
}

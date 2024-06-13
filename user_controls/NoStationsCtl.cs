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
    public partial class NoStationsCtl : UserControl
    {
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
    }
}

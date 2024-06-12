using RadioExt_Helper.models;
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
    public partial class StationEditor : UserControl
    {
        public EventHandler? StationUpdated;

        private MetaData _metaData;

        public StationEditor(MetaData metaData)
        {
            InitializeComponent();

            _metaData = metaData;
        }
    }
}

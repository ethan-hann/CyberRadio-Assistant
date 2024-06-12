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
    public partial class FloatTrackBar : TrackBar
    {
        private float precision = 0.1f;

        public float Precision
        {
            get { return precision; }
            set
            {
                precision = value;
                // todo: update the 5 properties below
            }
        }
        public new float LargeChange
        { get { return base.LargeChange * precision; } set { base.LargeChange = (int)(value / precision); } }
        public new float Maximum
        { get { return base.Maximum * precision; } set { base.Maximum = (int)(value / precision); } }
        public new float Minimum
        { get { return base.Minimum * precision; } set { base.Minimum = (int)(value / precision); } }
        public new float SmallChange
        { get { return base.SmallChange * precision; } set { base.SmallChange = (int)(value / precision); } }
        public new float Value
        { get { return base.Value * precision; } set { base.Value = (int)(value / precision); } }

        public FloatTrackBar()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}

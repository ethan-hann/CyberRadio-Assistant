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
using Icon = RadioExt_Helper.models.Icon;

namespace RadioExt_Helper.user_controls
{
    public partial class IconEditor : UserControl, IEditor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public EditorType Type { get; set; } = EditorType.IconEditor;
        public TrackableObject<Station> Station { get; }

        public Icon Icon { get; }
        private string _iconPath = string.Empty;

        public IconEditor(TrackableObject<Station> station, Icon icon)
        {
            InitializeComponent();

            Station = station;
            Icon = icon;
        }

        public void Translate()
        {
            
        }

        private void IconEditor_Load(object sender, EventArgs e)
        {
            Icon.EnsureImage();
            picStationIcon.Image = Icon.IconImage ?? Properties.Resources.drag_and_drop_128x128;
            lblEditingText.Text = $"Editing Icon: {Icon.IconId}";
        }

        private void picStationIcon_DragDrop(object sender, DragEventArgs e)
        {
            _iconPath = picStationIcon.ImagePath;
        }
    }
}

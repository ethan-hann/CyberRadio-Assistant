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
        private readonly TrackableObject<Station> _station;

        public Guid Id { get; set; } = Guid.NewGuid();
        public EditorType Type { get; set; } = EditorType.IconEditor;
        public TrackableObject<Station> Station => _station;

        private Icon? _activeIcon;
        private string _iconPath = string.Empty;

        public IconEditor(TrackableObject<Station> station)
        {
            InitializeComponent();

            _station = station;
            _activeIcon = _station.TrackedObject.GetActiveIcon();
        }

        public void Translate()
        {
            throw new NotImplementedException();
        }

        private void IconEditor_Load(object sender, EventArgs e)
        {
            if (_activeIcon?.IconImage != null)
                picStationIcon.Image = _activeIcon.IconImage;
            else
                picStationIcon.Image = Properties.Resources.drag_and_drop_128x128;
        }

        private void picStationIcon_DragDrop(object sender, DragEventArgs e)
        {
            _iconPath = picStationIcon.ImagePath;
        }
    }
}

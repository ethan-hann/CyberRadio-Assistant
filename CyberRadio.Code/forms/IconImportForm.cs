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

namespace RadioExt_Helper.forms
{
    public partial class IconImportForm : Form
    {
        private readonly TrackableObject<Station> _station;
        private readonly DragEventArgs _dragEvent;

        public IconImportForm(TrackableObject<Station> station, DragEventArgs dragEvent)
        {
            InitializeComponent();

            _station = station;
            _dragEvent = dragEvent;
        }

        private void IconImportForm_Load(object sender, EventArgs e)
        {
            Translate();
            LoadPreviewIcon();
        }

        private void Translate()
        {
            //TODO: translations
            Text = string.Format(GlobalData.Strings.GetString("IconImportForm_Title") ?? "Importing Icon: {0}", _station.TrackedObject.MetaData.DisplayName);

        }

        private void LoadPreviewIcon()
        {
            if (_station == null) return;

            var data = _dragEvent.Data;
            if (data != null && data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = data.GetData(DataFormats.FileDrop) as string[];
                if (files?.Length > 0)
                {
                    var file = files[0];
                    if (IconManager.Instance.IsPngFile(file))
                    {
                        var image = IconManager.Instance.LoadImage(file);
                        if (image != null)
                        {
                            picIconPreview.Image = image;
                        }
                    }
                }
            }
        }
    }
}

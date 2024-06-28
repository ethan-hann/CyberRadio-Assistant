using RadioExt_Helper.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.user_controls
{
    public partial class StationListBox : ListBox
    {
        private ImageList _imageList;
        private string _enabledIconKey = "enabled";
        private string _disabledIconKey = "disabled";

        /// <summary>
        /// Gets or sets the ImageList containing the icons for the list box.
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
        /// Gets or sets the key for the enabled icon in the ImageList.
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
        /// Gets or sets the key for the disabled icon in the ImageList.
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
        /// Initializes a new instance of the <see cref="StationListBox"/> class.
        /// </summary>
        public StationListBox()
        {
            InitializeComponent();

            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 16;

            _imageList ??= new ImageList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StationListBox"/> class.
        /// </summary>
        public StationListBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 16;

            _imageList ??= new ImageList();
        }

        /// <summary>
        /// Raises the <see cref="System.Windows.Forms.Control.DrawItem"/> event.
        /// </summary>
        /// <param name="e">A <see cref="DrawItemEventArgs"/> that contains the event data.</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            if (Items[e.Index] is Station station)
            {
                string iconKey = station.GetStatus() ? _enabledIconKey : _disabledIconKey;

                //Draw the icon
                if (_imageList != null && _imageList.Images.ContainsKey(iconKey))
                {
                    var icon = _imageList.Images[iconKey];
                    if (icon != null)
                        e.Graphics.DrawImage(icon, e.Bounds.Left, e.Bounds.Top, 16, 16);
                }

                //Draw the text
                var textRect = new Rectangle(e.Bounds.Left + 20, e.Bounds.Top, e.Bounds.Width - 20, e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, station.MetaData.DisplayName, e.Font, textRect, e.ForeColor, TextFormatFlags.Left);
            }

            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Raises the <see cref="System.Windows.Forms.Control.MeasureItem"/> event.
        /// </summary>
        /// <param name="e">A <see cref="MeasureItemEventArgs"/> that contains the event data.</param>
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            e.ItemHeight = 16; // Adjust height based on icon size
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using static System.Collections.Specialized.BitVector32;
using Icon = RadioExt_Helper.models.Icon;

namespace RadioExt_Helper.custom_controls
{
    public sealed partial class IconListBox : ListBox
    {
        public event EventHandler<Icon>? IconAddedViaDragDrop;

        private string _disabledIconKey = "disabled";
        private string _enabledIconKey = "enabled";
        private ImageList _imageList;

        private TrackableObject<Station> _station = new(new Station());

        [Browsable(true)]
        [Category("Data")]
        [Description("The station that the icon list is associated with.")]
        public TrackableObject<Station> Station
        {
            get => _station;
            set
            {
                _station = value;
                Items.Clear();

                foreach (var icon in _station.TrackedObject.Icons)
                {
                    Items.Add(icon);
                }
            }
        }

        [Browsable(true)]
        [Category("Icons")]
        [Description("The ImageList to use for the enabled/disabled icons.")]
        public ImageList ImageList
        {
            get => _imageList;
            set
            {
                _imageList = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Icons")]
        [Description("The key for the enabled icon.")]
        public string EnabledIconKey
        {
            get => _enabledIconKey;
            set
            {
                _enabledIconKey = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Icons")]
        [Description("The key for the disabled icon.")]
        public string DisabledIconKey
        {
            get => _disabledIconKey;
            set
            {
                _disabledIconKey = value;
                Invalidate();
            }
        }

        public IconListBox()
        {
            InitializeComponent();

            _imageList ??= new ImageList();

            AllowDrop = true;
            DragEnter += IconListBox_DragEnter;
            DragDrop += IconListBox_DragDrop;
            SetValues();
        }

        private void IconListBox_DragEnter(object? sender, DragEventArgs e)
        {
            try
            {
                if (e.Data == null) return;
                if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
                if (e.Data.GetData(DataFormats.FileDrop) is not string[] files) return;

                if (files.Any(file => file.EndsWith(".png"))) 
                    e.Effect = DragDropEffects.Copy;
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconListBox>("IconListBox_DragEnter").Error(ex, "An error occured while dragging the icon.");
            }
        }

        private void IconListBox_DragDrop(object? sender, DragEventArgs e)
        {
            try
            {
                if (e.Data == null) return;
                if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
                if (e.Data.GetData(DataFormats.FileDrop) is not string[] files) return;
                if (files.Length <= 0) return;

                var file = files[0];
                var icon = Icon.FromPath(file);
                StationManager.Instance.AddStationIcon(_station.Id, icon);

                IconAddedViaDragDrop?.Invoke(this, icon);
            } 
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconListBox>("IconListBox_DragDrop").Error(ex, "An error occured while dropping the icon.");
            }
        }

        private void SetValues()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DoubleBuffered = true;
            ItemHeight = 16;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0) return;

                e.DrawBackground();

                if (Items[e.Index] is Icon icon)
                {
                    var iconKey = icon.IsActive ? _enabledIconKey : _disabledIconKey;
                    if (_imageList.Images.ContainsKey(iconKey))
                    {
                        var primaryIcon = _imageList.Images[iconKey];
                        if (primaryIcon != null)
                        {
                            e.Graphics.DrawImage(primaryIcon, e.Bounds.Left, e.Bounds.Top, 16, 16);
                        }
                    }

                    // Draw the text
                    var textRect = new Rectangle(e.Bounds.Left + 20, e.Bounds.Top, e.Bounds.Width - 40 - 4,
                        e.Bounds.Height);

                    TextRenderer.DrawText(e.Graphics, icon.ToString(), Font, textRect, ForeColor, TextFormatFlags.Left);
                }

                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconListBox>("OnDrawItem").Error(ex, "An error occured while drawing the item.");
            }
        }

        /// <summary>
        /// Handles the measurement of an item in the ListBox.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            e.ItemHeight = 16; // Adjust height based on icon size
        }
    }
}

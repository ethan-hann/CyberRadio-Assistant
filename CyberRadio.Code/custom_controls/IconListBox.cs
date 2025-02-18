// IconListBox.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using System.ComponentModel;
using WIG.Lib.Models;

namespace RadioExt_Helper.custom_controls;

public sealed partial class IconListBox : ListBox
{
    private string _disabledIconKey = "disabled";
    private string _enabledIconKey = "enabled";
    private string _fromArchiveKey = "fromArchive";
    private string _fromPngKey = "fromPng";

    private ImageList _imageList;

    public IconListBox()
    {
        InitializeComponent();

        _imageList ??= new ImageList();
        SetValues();
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

    [Browsable(true)]
    [Category("Icons")]
    [Description("The key for an icon from a PNG.")]
    public string FromPngKey
    {
        get => _fromPngKey;
        set
        {
            _fromPngKey = value;
            Invalidate();
        }
    }

    [Browsable(true)]
    [Category("Icons")]
    [Description("The key for an icon from an archive.")]
    public string FromArchiveKey
    {
        get => _fromArchiveKey;
        set
        {
            _fromArchiveKey = value;
            Invalidate();
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

            if (Items[e.Index] is TrackableObject<WolvenIcon> icon)
            {
                var iconKey = icon.TrackedObject.IsActive ? _enabledIconKey : _disabledIconKey;
                if (_imageList.Images.ContainsKey(iconKey))
                {
                    var primaryIcon = _imageList.Images[iconKey];
                    if (primaryIcon != null) e.Graphics.DrawImage(primaryIcon, e.Bounds.Left, e.Bounds.Top, 16, 16);
                }

                //Determine the secondary icon (if from a PNG or an archive)
                var secondaryIcon = icon.TrackedObject.IsFromArchive ? _fromArchiveKey : _fromPngKey;
                var iconX = e.Bounds.Right - 16 - 4; //16 is the width of the icon, 4 is the padding from the edge

                if (_imageList.Images.ContainsKey(secondaryIcon))
                {
                    var secondaryImage = _imageList.Images[secondaryIcon];
                    if (secondaryImage != null)
                        e.Graphics.DrawImage(secondaryImage, iconX, e.Bounds.Top, 16, 16);
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
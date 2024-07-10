// StationListBox.cs : RadioExt-Helper
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

using System.ComponentModel;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;

namespace RadioExt_Helper.user_controls;

/// <summary>
/// Represents a custom ListBox control for displaying stations.
/// This listbox has support for displaying icons; one for the station's active status (enabled/disabled)
/// and one for the station's save status (saved/edited).
/// </summary>
public sealed partial class StationListBox : ListBox
{
    private string _disabledIconKey = "disabled";
    private string _editedStationIconKey = "edited_station";
    private string _enabledIconKey = "enabled";

    private ImageList _imageList;
    private string _savedStationIconKey = "saved_station";

    /// <summary>
    /// Gets or sets the ImageList containing the icons for the list box.
    /// </summary>
    [Browsable(true)]
    [Category("Icons")]
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
    ///     Gets or sets the key for the enabled icon in the ImageList.
    /// </summary>
    [Browsable(true)]
    [Category("Icons")]
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
    ///     Gets or sets the key for the disabled icon in the ImageList.
    /// </summary>
    [Browsable(true)]
    [Category("Icons")]
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
    ///     Gets or sets the key for the edited station icon in the ImageList.
    /// </summary>
    [Browsable(true)]
    [Category("Icons")]
    [Description("The key for the edited station icon in the ImageList.")]
    public string EditedStationIconKey
    {
        get => _editedStationIconKey;
        set
        {
            _editedStationIconKey = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     Gets or sets the key for the saved station icon in the ImageList.
    /// </summary>
    [Browsable(true)]
    [Category("Icons")]
    [Description("The key for the saved station icon in the ImageList.")]
    public string SavedStationIconKey
    {
        get => _savedStationIconKey;
        set
        {
            _savedStationIconKey = value;
            Invalidate();
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StationListBox"/> class.
    /// </summary>
    public StationListBox()
    {
        SetValues();
        _imageList ??= new ImageList();
    }

    /// <summary>
    /// Sets the default values for the control.
    /// </summary>
    private void SetValues()
    {
        DrawMode = DrawMode.OwnerDrawFixed;
        DoubleBuffered = true;
        ItemHeight = 16;
    }

    /// <summary>
    /// Handles the drawing of an item in the ListBox.
    /// <para>An item is drawn like so: <c>(left-aligned){active status icon} {Station Name} {save status icon}(right-aligned)</c></para>
    /// </summary>
    /// <param name="e">The event data.</param>
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        try
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            if (Items[e.Index] is TrackableObject<Station> station)
            {
                // Determine the primary icon (enabled/disabled)
                var primaryIconKey = station.TrackedObject.GetStatus() ? _enabledIconKey : _disabledIconKey;

                // Draw the primary icon
                if (_imageList.Images.ContainsKey(primaryIconKey))
                {
                    var primaryIcon = _imageList.Images[primaryIconKey];
                    if (primaryIcon != null) e.Graphics.DrawImage(primaryIcon, e.Bounds.Left, e.Bounds.Top, 16, 16);
                }

                // Determine the secondary icon (changes pending/saved)
                var secondaryIconKey = station.IsPendingSave ? _editedStationIconKey : _savedStationIconKey;

                // Calculate the position for the secondary icon at the right edge
                var iconX = e.Bounds.Right - 16 - 4; // 16 is the icon width, 4 is some padding from the edge

                // Draw the secondary icon
                if (_imageList.Images.ContainsKey(secondaryIconKey))
                {
                    var secondaryIcon = _imageList.Images[secondaryIconKey];
                    if (secondaryIcon != null) e.Graphics.DrawImage(secondaryIcon, iconX, e.Bounds.Top, 16, 16);
                }

                // Draw the text
                var textRect = new Rectangle(e.Bounds.Left + 20, e.Bounds.Top, e.Bounds.Width - 40 - 4,
                    e.Bounds.Height); // Adjust width to leave space for the secondary icon
                TextRenderer.DrawText(e.Graphics, station.TrackedObject.MetaData.DisplayName, e.Font, textRect,
                    e.ForeColor, TextFormatFlags.Left);
            }

            e.DrawFocusRectangle();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationListBox>("OnDrawItem")
                .Error(ex, "An error occurred while drawing the item.");
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
// StationListBox.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
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
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.custom_controls;

/// <summary>
/// Represents a custom ListBox control for displaying stations.
/// This listbox has support for displaying icons; one for the station's active status (enabled/disabled)
/// and one for the station's save status (saved/edited).
/// </summary>
public sealed partial class StationListBox : ListBox
{
    private string _disabledIconKey = "disabled";
    private Color _duplicateStationsColor = Color.Red;
    private Font _duplicateStationsFont = new(DefaultFont, FontStyle.Italic);
    private string _editedStationIconKey = "edited_station";
    private string _enabledIconKey = "enabled";

    private ImageList _imageList;

    private Color _newStationColor = Color.Green;

    private Font _newStationFont = new(DefaultFont, FontStyle.Bold);
    private string _savedStationIconKey = "saved_station";

    private Color _songsMissingColor = Color.Orange;

    private Font _songsMissingFont = new(DefaultFont, FontStyle.Bold);

    /// <summary>
    /// Initializes a new instance of the <see cref="StationListBox"/> class.
    /// </summary>
    public StationListBox()
    {
        SetValues();
        _imageList ??= new ImageList();

        // Enable drag and drop
        AllowDrop = true;
        DragEnter += StationListBox_DragEnter;
        DragDrop += StationListBox_DragDrop;
    }

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
    /// Gets or sets the color used to highlight stations with missing songs.
    /// </summary>
    [Browsable(true)]
    [Category("Colors")]
    [Description("The color used to highlight stations with missing songs.")]
    public Color SongsMissingColor
    {
        get => _songsMissingColor;
        set
        {
            _songsMissingColor = value;
            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the color used to highlight duplicate station names.
    /// </summary>
    [Browsable(true)]
    [Category("Colors")]
    [Description("The color used to highlight duplicate station names")]
    public Color DuplicateColor
    {
        get => _duplicateStationsColor;
        set
        {
            _duplicateStationsColor = value;
            Invalidate();
        }
    }

    [Browsable(true)]
    [Category("Colors")]
    [Description("The color used to highlight new stations.")]
    public Color NewStationColor
    {
        get => _newStationColor;
        set
        {
            _newStationColor = value;
            Invalidate();
        }
    }

    [Browsable(true)]
    [Category("Fonts")]
    [Description("The font used to highlight stations with missing songs.")]
    public Font SongsMissingFont
    {
        get => _songsMissingFont;
        set
        {
            _songsMissingFont = value;
            Invalidate();
        }
    }

    [Browsable(true)]
    [Category("Fonts")]
    [Description("The font used to highlight duplicate station names.")]
    public Font DuplicateFont
    {
        get => _duplicateStationsFont;
        set
        {
            _duplicateStationsFont = value;
            Invalidate();
        }
    }

    [Browsable(true)]
    [Category("Fonts")]
    [Description("The font used to highlight new stations.")]
    public Font NewStationFont
    {
        get => _newStationFont;
        set
        {
            _newStationFont = value;
            Invalidate();
        }
    }

    /// <summary>
    /// Occurs whenever the station is imported from a .zip or .rar file.
    /// </summary>
    public event EventHandler<List<Guid?>>? StationsImported;

    private void StationListBox_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data == null || !e.Data.GetDataPresent(DataFormats.FileDrop)) return;

        var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);
        if (files is not { Length: > 0 }) return;

        if (files.Any(file => file.EndsWith(".zip") || file.EndsWith(".rar")))
            e.Effect = DragDropEffects.Copy;
    }

    private void StationListBox_DragDrop(object? sender, DragEventArgs e)
    {
        if (e.Data == null || !e.Data.GetDataPresent(DataFormats.FileDrop)) return;

        var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);
        if (files is not { Length: > 0 }) return;

        List<Guid?> importedStationIds = [];

        foreach (var file in files)
            if (file.EndsWith(".zip") || file.EndsWith(".rar"))
            {
                var stationId = StationManager.Instance.ImportStationFromArchive(file);
                if (stationId != null)
                    importedStationIds.Add(stationId);
            }

        StationsImported?.Invoke(this, importedStationIds);
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
    /// <para>It's font and color are styled appropriately depending on whether it's a duplicate station or has missing songs.</para>
    /// </summary>
    /// <param name="e">The event data.</param>
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        try
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            if (Items[e.Index] is TrackableObject<AdditionalStation> station)
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

                TextRenderer.DrawText(e.Graphics, station.TrackedObject.MetaData.DisplayName, GetItemFont(station),
                    textRect,
                    GetItemColor(station), TextFormatFlags.Left);
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
    /// Retrieves the appropriate color for the item based on its properties.
    /// </summary>
    /// <param name="station">A station to get the color of.</param>
    /// <returns>If the station has no missing songs, is not a duplicate name, and is not a new station, returns the original fore color. Otherwise, returns a
    /// blend of colors depending on whether the station is missing songs, is a duplicate, and/or is a new station.</returns>
    private Color GetItemColor(TrackableObject<AdditionalStation> station)
    {
        var returnColor = ForeColor;

        if (station.TrackedObject.Songs.Any(s => !FileHelper.DoesFileExist(s.FilePath)))
            returnColor = CombineColors(returnColor, _songsMissingColor);

        if (Items.OfType<TrackableObject<AdditionalStation>>().Count(s =>
                s.TrackedObject.MetaData.DisplayName.Equals(station.TrackedObject.MetaData.DisplayName)) > 1)
            returnColor = CombineColors(returnColor, _duplicateStationsColor);

        if (StationManager.Instance.IsNewStation(station.Id))
            returnColor = CombineColors(returnColor, _newStationColor);

        return returnColor;
    }

    private Font GetItemFont(TrackableObject<AdditionalStation> station)
    {
        var font = Font;

        if (station.TrackedObject.Songs.Any(s => !FileHelper.DoesFileExist(s.FilePath)))
            font = _songsMissingFont;

        else if (Items.OfType<TrackableObject<AdditionalStation>>().Count(s =>
                     s.TrackedObject.MetaData.DisplayName.Equals(station.TrackedObject.MetaData.DisplayName)) > 1)
            font = _duplicateStationsFont;

        else if (StationManager.Instance.IsNewStation(station.Id))
            font = _newStationFont;

        return font;
    }

    private Color CombineColors(Color color1, Color color2)
    {
        var r = (color1.R + color2.R) / 2;
        var g = (color1.G + color2.G) / 2;
        var b = (color1.B + color2.B) / 2;
        return Color.FromArgb(r, g, b);
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
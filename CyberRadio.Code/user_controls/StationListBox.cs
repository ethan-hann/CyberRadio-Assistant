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
using System.Security.Cryptography;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls;

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

    /// <summary>
    /// Occurs whenever the station is imported from a .zip or .rar file.
    /// </summary>
    public event EventHandler<TrackableObject<Station>>? StationImported;

    private void StationListBox_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Any(file => file.EndsWith(".zip") || file.EndsWith(".rar"))) 
                e.Effect = DragDropEffects.Copy;
        }
    }

    private void StationListBox_DragDrop(object? sender, DragEventArgs e)
    {
        var files = (string[])e.Data.GetData(DataFormats.FileDrop);
        foreach (var file in files)
            if (file.EndsWith(".zip") || file.EndsWith(".rar"))
                // Handle file extraction and preview
                HandleStationArchive(file);
    }

    private void HandleStationArchive(string filePath)
    {
        var tempDir = StationManager.ExtractStationArchive(filePath);
        var stationId = StationManager.Instance.LoadStationFromDirectory(tempDir, true);
        var station = StationManager.Instance.GetStation(stationId)?.Key;

        if (station == null)
        {
            AuLogger.GetCurrentLogger<StationListBox>("HandleStationArchive").Warn($"Station not found in directory: {tempDir}");
            return;
        }

        StationImported?.Invoke(this, station);
        //try
        //{
        //    var tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        //    Directory.CreateDirectory(tempDir);
        //    AuLogger.GetCurrentLogger<StationListBox>("HandleStationArchive").Info($"Temporary directory created: {tempDir}");



        //    // Extract radio station files and custom icon .archive file, if it exists.
        //    //var radiosPath = PathHelper.GetRadiosPath(tempDir);
        //    //var iconPath = Path.Combine(tempDir, "archive", "pc", "mod");

        //    //AuLogger.GetCurrentLogger<StationListBox>("HandleStationArchive").Info($"Checking directories: {radiosPath} and {iconPath}");

        //    //if (!Directory.Exists(radiosPath))
        //    //    AuLogger.GetCurrentLogger<StationListBox>("HandleStationArchive").Warn($"Radios path does not exist: {radiosPath}");

        //    //if (!Directory.Exists(iconPath))
        //    //    AuLogger.GetCurrentLogger<StationListBox>("HandleStationArchive").Warn($"Icon path does not exist: {iconPath}");


        //    var stationDirs = Directory.GetDirectories(tempDir, "*", SearchOption.AllDirectories);

        //    foreach (var stationDir in stationDirs)
        //    {
        //        Guid? stationId = null;
        //        if (Directory.GetFiles(stationDir, "metadata.json", SearchOption.TopDirectoryOnly).Length != 0)
        //        {
        //            // Load the station using StationManager if a metadata.json file is found
        //            stationId = StationManager.Instance.LoadStationFromDirectory(stationDir, true);
        //        }

        //        TrackableObject<Station>? station = null;
        //        if (stationId != null && stationId != Guid.Empty)
        //            station = StationManager.Instance.GetStation(stationId)?.Key;

        //        if (station == null) // Skip if the station is not found
        //        {
        //            AuLogger.GetCurrentLogger<StationListBox>("HandleStationArchive").Warn($"Station not found in directory: {stationDir}");
        //            continue;
        //        }

        //        // Load custom icon, if it exists
        //        var iconFilePath = Directory.GetFiles(stationDir, "*.archive", SearchOption.AllDirectories).FirstOrDefault();
        //        if (iconFilePath == null) //Skip loading the icon if it doesn't exist
        //            StationImported?.Invoke(this, new StationImportedEventArgs(station, null, null));
        //        else
        //        {
        //            var stagingIconsFolder = 
        //                GlobalData.ConfigManager.Get("stagingPath") is string stagingPath ? 
        //                    Path.Combine(stagingPath, "icons") : null;
        //            LoadCustomIcon(station, iconFilePath, stagingIconsFolder);

        //            // Raise event
        //            var eventArgs = new StationImportedEventArgs(station, iconFilePath, Path.GetFileName(iconFilePath));
        //            StationImported?.Invoke(this, eventArgs);
        //        }
        //    }

        //    // Clean up temporary directory
        //    Directory.Delete(tempDir, true);
        //}
        //catch (Exception ex)
        //{
        //    AuLogger.GetCurrentLogger<StationListBox>("HandleStationArchive").Error(ex, "An error occurred while importing the station archive.");
        //}
    }

    //private void LoadCustomIcon(TrackableObject<Station> station, string? iconFilePath, string? stagingIconsFolder)
    //{
    //    try
    //    {
    //        if (!File.Exists(iconFilePath))
    //        {
    //            AuLogger.GetCurrentLogger<StationListBox>("LoadCustomIcon").Error($"Icon file does not exist: {iconFilePath}");
    //            return;
    //        }

    //        if (stagingIconsFolder == null)
    //        {
    //            AuLogger.GetCurrentLogger<StationListBox>("LoadCustomIcon").Error($"Staging icons folder is null.");
    //            return;
    //        }

    //        // Ensure the icons folder exists
    //        Directory.CreateDirectory(stagingIconsFolder);

    //        // Copy the icon to the icons folder
    //        var iconFileName = Path.GetFileName(iconFilePath);
    //        var destIconPath = Path.Combine(stagingIconsFolder, iconFileName);
    //        File.Copy(iconFilePath, destIconPath, true);

    //        // Calculate the hash of the icon file
    //        string fileHash;
    //        using (var sha256 = SHA256.Create())
    //        using (var fileStream = File.OpenRead(destIconPath))
    //        {
    //            var hashBytes = sha256.ComputeHash(fileStream);
    //            fileHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    //        }

    //        // Store the file name and hash in the CustomData dictionary
    //        station.TrackedObject.MetaData.CustomData["IconFileName"] = iconFileName;
    //        station.TrackedObject.MetaData.CustomData["IconFileHash"] = fileHash;

    //        AuLogger.GetCurrentLogger<StationListBox>("LoadCustomIcon").Info($"Custom icon loaded for station: {station.TrackedObject.MetaData.DisplayName}");
    //    }
    //    catch (Exception ex)
    //    {
    //        AuLogger.GetCurrentLogger<StationListBox>("LoadCustomIcon").Error(ex, $"Failed to load custom icon: {iconFilePath}");
    //    }
    //}

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
    /// <returns>If the station has no missing songs and is not a duplicate name, returns the original forecolor. Otherwise, returns a
    /// blend of colors depending on whether the station is missing songs or is a duplicate or both.</returns>
    private Color GetItemColor(TrackableObject<Station> station)
    {
        var returnColor = ForeColor;

        if (station.TrackedObject.Songs.Any(s => !FileHelper.DoesFileExist(s.FilePath)))
            returnColor = CombineColors(returnColor, _songsMissingColor);

        if (Items.OfType<TrackableObject<Station>>().Count(s =>
                s.TrackedObject.MetaData.DisplayName.Equals(station.TrackedObject.MetaData.DisplayName)) > 1)
            returnColor = CombineColors(returnColor, _duplicateStationsColor);

        return returnColor;
    }

    private Font GetItemFont(TrackableObject<Station> station)
    {
        var font = Font;

        if (station.TrackedObject.Songs.Any(s => !FileHelper.DoesFileExist(s.FilePath)))
            font = _songsMissingFont;

        if (Items.OfType<TrackableObject<Station>>().Count(s =>
                s.TrackedObject.MetaData.DisplayName.Equals(station.TrackedObject.MetaData.DisplayName)) > 1)
            font = _duplicateStationsFont;

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
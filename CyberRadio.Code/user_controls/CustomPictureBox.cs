// CustomPictureBox.cs : RadioExt-Helper
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
using RadioExt_Helper.Properties;
using WIG.Lib.Utility;

namespace RadioExt_Helper.custom_controls;

internal class CustomPictureBox : PictureBox
{
    private Image? _downSampledImage; // Store the down-sampled version of the image

    public CustomPictureBox()
    {
        AllowDrop = true;
        DragEnter += CustomPictureBox_DragEnter;
        DragDrop += CustomPictureBox_DragDrop;

        // Set control styles for smooth rendering
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint,
            true);

        ResetView(); // Ensure it's ready with a default view
    }

    public string ImagePath { get; private set; } = string.Empty;
    public ImageProperties ImageProperties { get; set; } = new();

    public sealed override bool AllowDrop
    {
        get => base.AllowDrop;
        set => base.AllowDrop = value;
    }

    public void ResetView()
    {
        // Ensure we're using the appropriate size mode based on aspect ratio
        SetSizeMode();

        Invalidate(); // Redraw to reflect the reset view
    }

    // Override OnPaintBackground to draw the checkered background based on the image lightness
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        base.OnPaintBackground(pevent);

        // Determine if the image is light or dark
        var isImageLight = IsImageLight();

        // Set colors for the checkered pattern based on image lightness
        var lightColor = isImageLight ? Color.DarkGray : Color.White;
        var darkColor = isImageLight ? Color.Gray : Color.LightGray;

        // Create the checkered background pattern
        const int tileSize = 10; // Size of each checkered tile
        using var lightBrush = new SolidBrush(lightColor);
        using var darkBrush = new SolidBrush(darkColor);
        for (var y = 0; y < Height; y += tileSize)
        for (var x = 0; x < Width; x += tileSize)
        {
            var isLightTile = (x / tileSize + y / tileSize) % 2 == 0;
            pevent.Graphics.FillRectangle(isLightTile ? lightBrush : darkBrush, x, y, tileSize, tileSize);
        }
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
        if (_downSampledImage != null)
        {
            var imageRect = GetScaledImageRect(_downSampledImage);
            pe.Graphics.DrawImage(_downSampledImage, imageRect);
        }
        else if (Image != null)
        {
            var imageRect = GetScaledImageRect(Image);
            pe.Graphics.DrawImage(Image, imageRect);
        }
        else
        {
            base.OnPaint(pe);
        }
    }

    // Helper method to get the scaled and centered image rectangle
    private Rectangle GetScaledImageRect(Image? image)
    {
        if (image == null) return new Rectangle(0, 0, Width, Height);

        var imageAspect = (float)image.Width / image.Height;
        var controlAspect = (float)Width / Height;

        int drawWidth, drawHeight;
        if (imageAspect > controlAspect)
        {
            drawWidth = Width;
            drawHeight = (int)(Width / imageAspect);
        }
        else
        {
            drawHeight = Height;
            drawWidth = (int)(Height * imageAspect);
        }

        var drawX = (Width - drawWidth) / 2; // Center X
        var drawY = (Height - drawHeight) / 2; // Center Y

        return new Rectangle(drawX, drawY, drawWidth, drawHeight);
    }

    // Method to check if the image is predominantly light
    private bool IsImageLight()
    {
        if (Image == null) return false;

        var bmp = new Bitmap(Image);
        var lightPixelCount = 0;
        var pixelCount = 0;

        for (var y = 0; y < bmp.Height; y++)
        for (var x = 0; x < bmp.Width; x++)
        {
            var pixelColor = bmp.GetPixel(x, y);

            // Skip transparent pixels
            if (pixelColor.A < 128) continue;

            // Calculate the brightness (lightness) of the pixel
            var brightness =
                (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B); // Standard luminance formula

            // Count as light if brightness is higher than a threshold (e.g., 180)
            if (brightness > 180)
                lightPixelCount++;

            pixelCount++;
        }

        // Determine if the image is predominantly light (> 60% light pixels)
        return pixelCount > 0 && (double)lightPixelCount / pixelCount > 0.6;
    }

    private void CustomPictureBox_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string?[] { Length: > 0 } files &&
                Path.GetExtension(files[0])?.ToLower() == ".png")
                e.Effect = DragDropEffects.Copy; // Allow only .png files
            else
                e.Effect = DragDropEffects.None; // Disallow non-.png files
        }
        else
        {
            e.Effect = DragDropEffects.None; // Disallow other data types
        }
    }

    private void CustomPictureBox_DragDrop(object? sender, DragEventArgs e)
    {
        try
        {
            var data = e.Data;
            if (data == null || !data.GetDataPresent(DataFormats.FileDrop)) return;

            if (data.GetData(DataFormats.FileDrop) is not string?[] files || files.Length == 0 ||
                Path.GetExtension(files[0])?.ToLower() != ".png") return;
            var file = files[0];
            if (file == null) return;

            ClearImage();
            SetImage(file);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<CustomPictureBox>().Error(ex.Message);
        }
    }

    /// <summary>
    /// Clear the image and image path from the picture box.
    /// </summary>
    public void ClearImage()
    {
        try
        {
            Image?.Dispose();
            _downSampledImage?.Dispose();
            Image = null;
            _downSampledImage = null;
            ImagePath = string.Empty;
            ImageProperties = new ImageProperties();
            Invalidate(); // Trigger repaint to show that the image has been cleared
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<CustomPictureBox>().Error(ex.Message);
        }
    }

    /// <summary>
    /// Set the image to display in the picture box from the given file path using lazy loading (background thread).
    /// </summary>
    /// <param name="imagePath">The path to the image to load.</param>
    public void SetImage(string? imagePath)
    {
        try
        {
            Image = ImageUtils.LoadAndOptimizeImage(imagePath ?? string.Empty) ?? Resources.drag_and_drop;
            ImagePath = imagePath ?? string.Empty;
            _downSampledImage = ImageUtils.DownsampleImage(Image, Width, Height);
            UpdateImageProperties();
            ResetView();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<CustomPictureBox>().Error(ex.Message);
        }
    }

    /// <summary>
    /// Set the image to display in the picture box from the given bitmap resource using lazy loading (background thread).
    /// </summary>
    /// <param name="image">The image to load into the picture box.</param>
    public void SetImageFromBitmap(Bitmap image)
    {
        try
        {
            Image = image;
            ImagePath = string.Empty;
            _downSampledImage = ImageUtils.DownsampleImage(Image, Width, Height);
            UpdateImageProperties();
            ResetView();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<CustomPictureBox>().Error(ex.Message);
        }
    }

    /// <summary>
    /// Set the size mode based on the aspect ratio of the image and the control.
    /// </summary>
    private void SetSizeMode()
    {
        if (Image == null) return;

        var imageAspect = (float)Image.Width / Image.Height;
        var controlAspect = (float)Width / Height;

        SizeMode = imageAspect > controlAspect ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
    }

    private void UpdateImageProperties()
    {
        if (Image == null) return;
        ImageProperties = new ImageProperties
        {
            Width = Image.Width,
            Height = Image.Height,
            ImageFormat = Image.RawFormat,
            PixelFormat = Image.PixelFormat
        };
    }
}
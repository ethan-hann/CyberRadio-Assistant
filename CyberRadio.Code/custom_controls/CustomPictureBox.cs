using AetherUtils.Core.Logging;
using RadioExt_Helper.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIG.Lib.Utility;

namespace RadioExt_Helper.custom_controls
{
    internal class CustomPictureBox : PictureBox
    {
        public string ImagePath { get; private set; } = string.Empty;

        public CustomPictureBox()
        {
            AllowDrop = true;
            DragEnter += CustomPictureBox_DragEnter;
            DragDrop += CustomPictureBox_DragDrop;

            // Set this to true so we can handle painting manually
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        // Override OnPaintBackground to draw the checkered background based on the image lightness
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

            // Determine if the image is light or dark
            bool isImageLight = IsImageLight();

            // Set colors for the checkered pattern based on image lightness
            Color lightColor = isImageLight ? Color.DarkGray : Color.White;
            Color darkColor = isImageLight ? Color.Gray : Color.LightGray;

            // Create the checkered background pattern
            int tileSize = 10; // Size of each checkered tile
            using (var lightBrush = new SolidBrush(lightColor))
            using (var darkBrush = new SolidBrush(darkColor))
            {
                for (int y = 0; y < Height; y += tileSize)
                {
                    for (int x = 0; x < Width; x += tileSize)
                    {
                        bool isLightTile = (x / tileSize + y / tileSize) % 2 == 0;
                        pevent.Graphics.FillRectangle(isLightTile ? lightBrush : darkBrush, x, y, tileSize, tileSize);
                    }
                }
            }
        }

        // Override OnPaint to draw the image on top of the background
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Draw the checkered background first (handled by OnPaintBackground)

            // Now draw the image, if it exists
            if (Image != null)
            {
                var imageRect = GetScaledImageRect();
                pe.Graphics.DrawImage(Image, imageRect);
            }
            else
            {
                base.OnPaint(pe);
            }
        }

        // Helper method to get the scaled and centered image rectangle
        private Rectangle GetScaledImageRect()
        {
            if (Image == null) return new Rectangle(0, 0, Width, Height);

            float imageAspect = (float)Image.Width / Image.Height;
            float controlAspect = (float)Width / Height;

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

            int drawX = (Width - drawWidth) / 2;
            int drawY = (Height - drawHeight) / 2;

            return new Rectangle(drawX, drawY, drawWidth, drawHeight);
        }

        // Method to check if the image is predominantly light
        private bool IsImageLight()
        {
            if (Image == null) return false;

            Bitmap bmp = new Bitmap(Image);
            int lightPixelCount = 0;
            int pixelCount = 0;

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    
                    // Skip transparent pixels
                    if (pixelColor.A < 128) continue;

                    // Calculate the brightness (lightness) of the pixel
                    int brightness = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B); // Standard luminance formula

                    // Count as light if brightness is higher than a threshold (e.g., 180)
                    if (brightness > 180)
                    {
                        lightPixelCount++;
                    }

                    pixelCount++;
                }
            }

            // Determine if the image is predominantly light (> 60% light pixels)
            return pixelCount > 0 && (double)lightPixelCount / pixelCount > 0.6;
        }

        private void CustomPictureBox_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) is true)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void CustomPictureBox_DragDrop(object? sender, DragEventArgs e)
        {
            try
            {
                var data = e.Data;
                if (data != null && data.GetDataPresent(DataFormats.FileDrop))
                {
                    var files = data.GetData(DataFormats.FileDrop) as string[];
                    if (files?.Length > 0)
                    {
                        var file = files[0];
                        var image = ImageUtils.LoadImage(file);
                        if (image != null)
                        {
                            Image = image;
                            ImagePath = file;

                            // Redraw the picture box after setting the image
                            Invalidate(); // Force the control to repaint with the new image
                        }
                    }
                }
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<CustomPictureBox>().Error(ex.Message);
            }
        }
    }
}

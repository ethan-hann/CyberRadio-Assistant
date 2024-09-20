using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using WIG.Lib.Models;
using WIG.Lib.Utility;

namespace RadioExt_Helper.custom_controls
{
    internal class CustomPictureBox : PictureBox
    {
        public string ImagePath { get; private set; } = string.Empty;
        public ImageProperties ImageProperties { get; set; } = new();

        public CustomPictureBox()
        {
            AllowDrop = true;
            DragEnter += CustomPictureBox_DragEnter;
            DragDrop += CustomPictureBox_DragDrop;

            // Set this to true so we can handle painting manually
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public sealed override bool AllowDrop
        {
            get => base.AllowDrop;
            set => base.AllowDrop = value;
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
            {
                for (var x = 0; x < Width; x += tileSize)
                {
                    var isLightTile = (x / tileSize + y / tileSize) % 2 == 0;
                    pevent.Graphics.FillRectangle(isLightTile ? lightBrush : darkBrush, x, y, tileSize, tileSize);
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

            var imageAspect = (float)Image.Width / Image.Height;
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

            var drawX = (Width - drawWidth) / 2;
            var drawY = (Height - drawHeight) / 2;

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
            {
                for (var x = 0; x < bmp.Width; x++)
                {
                    var pixelColor = bmp.GetPixel(x, y);

                    // Skip transparent pixels
                    if (pixelColor.A < 128) continue;

                    // Calculate the brightness (lightness) of the pixel
                    var brightness = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B); // Standard luminance formula

                    // Count as light if brightness is higher than a threshold (e.g., 180)
                    if (brightness > 180)
                        lightPixelCount++;

                    pixelCount++;
                }
            }

            // Determine if the image is predominantly light (> 60% light pixels)
            return pixelCount > 0 && (double)lightPixelCount / pixelCount > 0.6;
        }

        private void CustomPictureBox_DragEnter(object? sender, DragEventArgs e)
        {
            e.Effect = e.Data?.GetDataPresent(DataFormats.FileDrop) is true ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void CustomPictureBox_DragDrop(object? sender, DragEventArgs e)
        {
            try
            {
                var data = e.Data;
                if (data == null || !data.GetDataPresent(DataFormats.FileDrop)) return;

                var files = data.GetData(DataFormats.FileDrop) as string[];
                if (!(files?.Length > 0)) return;

                var file = files[0];
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
                Image = null;
                ImagePath = string.Empty;
                ImageProperties = new ImageProperties();
                Invalidate();
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<CustomPictureBox>().Error(ex.Message);
            }
        }

        /// <summary>
        /// Set the image to display in the picture box from the given <see cref="Image"/> object.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> to load.</param>
        public void SetImage(Image? image)
        {
            try
            {
                Image = image ?? Resources.drag_and_drop;
                UpdateImageProperties();
                SetSizeMode();
                Invalidate();
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<CustomPictureBox>().Error(ex.Message);
            }
        }

        /// <summary>
        /// Set the image to display in the picture box from the given file path.
        /// </summary>
        /// <param name="imagePath">The path to the image to load.</param>
        public void SetImage(string? imagePath)
        {
            try
            {
                Image = ImageUtils.LoadImage(imagePath ?? string.Empty) ?? Resources.drag_and_drop;
                ImagePath = imagePath ?? string.Empty;
                UpdateImageProperties();

                SetSizeMode();
                Invalidate();
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
}

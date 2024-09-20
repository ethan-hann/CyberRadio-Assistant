using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using SixLabors.ImageSharp.Processing;
using WIG.Lib.Models;
using WIG.Lib.Utility;

namespace RadioExt_Helper.custom_controls
{
    internal class CustomPictureBox : PictureBox
    {
        private bool _isManualMode = false; // Track whether zoom/pan is in manual mode
        private float _zoomFactor = 1.0f; // Track the zoom level
        private Point _imageOffset = Point.Empty; // Track the offset for panning
        private Point _lastMousePosition = Point.Empty; // Last mouse position during panning
        private bool _isZooming = false; // Track whether the user is zooming
        private bool _isPanning = false; // Track whether the user is panning
        private Rectangle _initialImageRect; // Initial rectangle of the image
        private Image? _downsampledImage; // Store the downsampled version of the image

        public string ImagePath { get; private set; } = string.Empty;
        public ImageProperties ImageProperties { get; set; } = new();

        public CustomPictureBox()
        {
            AllowDrop = true;
            DragEnter += CustomPictureBox_DragEnter;
            DragDrop += CustomPictureBox_DragDrop;

            // Set control styles for smooth rendering
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            // Set the initial size mode to Zoom (so it handles aspect ratio correctly)
            SizeMode = PictureBoxSizeMode.Zoom;

            // Handle mouse events for zoom and pan
            MouseWheel += CustomPictureBox_MouseWheel;
            MouseDown += CustomPictureBox_MouseDown;
            MouseMove += CustomPictureBox_MouseMove;
            MouseUp += CustomPictureBox_MouseUp;

            ResetView(); // Ensure it's ready with a default view
        }

        public sealed override bool AllowDrop
        {
            get => base.AllowDrop;
            set => base.AllowDrop = value;
        }

        public void ResetView()
        {
            // Reset manual mode and revert to native PictureBox behavior
            _isManualMode = false;
            _zoomFactor = 1.0f;
            _imageOffset = Point.Empty;

            // Ensure we're using the appropriate size mode based on aspect ratio
            SetSizeMode();

            Invalidate(); // Redraw to reflect the reset view
        }

        private void CustomPictureBox_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (!_isManualMode)
            {
                // Switch to manual mode if the user starts zooming
                _isManualMode = true;

                // Set the initial zoom factor and image offset based on the current PictureBox view
                var scaledRect = GetScaledImageRect(Image);
                _imageOffset = new Point(scaledRect.X, scaledRect.Y);
            }

            _isZooming = true;

            const float zoomStep = 0.1f;
            var oldZoomFactor = _zoomFactor;

            if (e.Delta > 0)
            {
                _zoomFactor += zoomStep;
            }
            else if (e.Delta < 0 && _zoomFactor > zoomStep)
            {
                _zoomFactor -= zoomStep;
            }

            // Use the image offset and cursor position to zoom like Google Maps
            var mousePosRelativeToImage = new Point(e.X - _imageOffset.X, e.Y - _imageOffset.Y);
            var zoomFactorChange = _zoomFactor / oldZoomFactor;

            _imageOffset.X = (int)(e.X - mousePosRelativeToImage.X * zoomFactorChange);
            _imageOffset.Y = (int)(e.Y - mousePosRelativeToImage.Y * zoomFactorChange);

            _isZooming = false;
            //_zoomFactor = zoomFactorChange;
            Invalidate(); // Redraw with the updated zoom and offset
        }

        // Handle mouse down for panning
        private void CustomPictureBox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (!_isManualMode && e.Button == MouseButtons.Left)
            {
                // Switch to manual mode if the user starts panning
                _isManualMode = true;

                // Set the initial image offset based on the current PictureBox view
                var scaledRect = GetScaledImageRect(Image);
                _imageOffset = new Point(scaledRect.X, scaledRect.Y);
            }

            if (e.Button == MouseButtons.Left)
            {
                _isPanning = true;
                _lastMousePosition = e.Location;
                Cursor = Cursors.SizeAll; // Change the cursor to indicate panning
            }
        }

        // Handle mouse move for panning
        private void CustomPictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isPanning)
            {
                var deltaX = e.X - _lastMousePosition.X;
                var deltaY = e.Y - _lastMousePosition.Y;

                // Update the image offset for panning
                _imageOffset.X += deltaX;
                _imageOffset.Y += deltaY;

                _lastMousePosition = e.Location;

                Invalidate(); // Redraw the image with the updated offset
            }
        }

        // Handle mouse up to stop panning
        private void CustomPictureBox_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isPanning = false;
                _isManualMode = false;
                Cursor = Cursors.Default; // Reset the cursor

                Invalidate();
            }
        }

        private Rectangle GetImageRectangle(Image? image)
        {
            if (image == null) return Rectangle.Empty;

            var zoomedWidth = (int)(image.Width * _zoomFactor);
            var zoomedHeight = (int)(image.Height * _zoomFactor);

            return new Rectangle(_imageOffset.X, _imageOffset.Y, zoomedWidth, zoomedHeight);
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

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Adjust graphics quality based on user interaction
            if (_isManualMode)
            {
                // Lower quality during active interaction (e.g., zooming/panning) for performance
                if (_isPanning || _isZooming)  // Add a flag to track zooming/panning
                {
                    pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    pe.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

                    // Custom rendering logic for manual zoom/pan. Use the downsampled image for performance.
                    if (_downsampledImage != null)
                    {
                        var imageRect = GetScaledImageRect(_downsampledImage);
                        pe.Graphics.DrawImage(_downsampledImage, imageRect);
                    }
                    else
                    {
                        base.OnPaint(pe);
                    }
                }
                else
                {
                    // High quality when not interacting
                    pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    pe.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    if (Image != null)
                    {
                        var imageRect = GetImageRectangle(_downsampledImage);
                        pe.Graphics.DrawImage(Image, imageRect);
                    }
                    else
                    {
                        base.OnPaint(pe);
                    }
                }
            }
            else
            {
                if (Image != null)
                {
                    var imageRect = GetImageRectangle(_downsampledImage);
                    pe.Graphics.DrawImage(Image, imageRect);
                }
                else
                {
                    base.OnPaint(pe);
                }
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
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                if (e.Data.GetData(DataFormats.FileDrop) is string?[] { Length: > 0 } files && 
                    Path.GetExtension(files[0])?.ToLower() == ".png")
                {
                    e.Effect = DragDropEffects.Copy; // Allow only .png files
                }
                else
                {
                    e.Effect = DragDropEffects.None; // Disallow non-.png files
                }
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

                if (data.GetData(DataFormats.FileDrop) is not string?[] files || files.Length == 0 || Path.GetExtension(files[0])?.ToLower() != ".png") return;
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
                Image = null;
                ImagePath = string.Empty;
                ImageProperties = new ImageProperties();
                ResetView();
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
                _downsampledImage = ImageUtils.DownsampleImage(Image, Width, Height);
                UpdateImageProperties();
                ResetView();
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
                Image = ImageUtils.LoadAndOptimizeImage(imagePath ?? string.Empty) ?? Resources.drag_and_drop;
                ImagePath = imagePath ?? string.Empty;
                _downsampledImage = ImageUtils.DownsampleImage(Image, Width, Height);
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

            SizeMode = imageAspect > controlAspect ? PictureBoxSizeMode.CenterImage : PictureBoxSizeMode.Zoom;
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

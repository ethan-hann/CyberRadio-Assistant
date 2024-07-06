using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RadioExt_Helper.user_controls
{
    public class RoundedPictureBox : PictureBox
    {
        private ImageList? _imageList;
        private string _imageKey = string.Empty;

        [Browsable(true)]
        [Category("Rounded Image")]
        [Description("The radius of the rounded corners. Set to 0 to disable rounded corners.")]
        public int BorderRadius { get; set; } = 20;

        [Browsable(true)]
        [Category("Rounded Image")]
        [Description("Whether to include a border around the button.")]
        public bool IncludeBorder { get; set; } = true;

        [Browsable(true)]
        [Category("Rounded Image")]
        [Description("The color of the border.")]
        public Color BorderColor { get; set; } = Color.Transparent;

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The width of the border.")]
        public int BorderWidth { get; set; } = 2;

        [Browsable(true)]
        [Category("Rounded Image")]
        [Description("The image list containing the images.")]
        public ImageList? ImageList
        {
            get => _imageList;
            set
            {
                _imageList = value;
                UpdateImage();
            }
        }

        [Browsable(true)]
        [Category("Rounded Image")]
        [Description("The key of the image to display.")]
        public string ImageKey
        {
            get => _imageKey;
            set
            {
                _imageKey = value;
                UpdateImage();
            }
        }

        public RoundedPictureBox()
        {
            // Ensure the PictureBox supports transparency
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
        }

        private void UpdateImage()
        {
            if (_imageList != null && !string.IsNullOrEmpty(_imageKey) && _imageList.Images.ContainsKey(_imageKey))
            {
                Image = _imageList.Images[_imageKey];
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (_imageList != null)
            {
                ImageKey = _imageKey + "_over";
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_imageList != null)
            {
                ImageKey = _imageKey;
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (_imageList != null)
            {
                ImageKey = _imageKey + "_down";
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (_imageList != null)
            {
                ImageKey = _imageKey + "_over"; // Change this to ImageKey if you want to revert to hover state
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                if (BorderRadius > 0)
                {
                    path.AddArc(0, 0, BorderRadius, BorderRadius, 180, 90);
                    path.AddArc(Width - BorderRadius, 0, BorderRadius, BorderRadius, 270, 90);
                    path.AddArc(Width - BorderRadius, Height - BorderRadius, BorderRadius, BorderRadius, 0, 90);
                    path.AddArc(0, Height - BorderRadius, BorderRadius, BorderRadius, 90, 90);
                    path.CloseFigure();

                    pe.Graphics.SetClip(path);
                }
                else
                {
                    pe.Graphics.SetClip(ClientRectangle);
                }

                // Clear the previous drawing
                pe.Graphics.Clear(Color.White);

                // Draw the image within the clipped region
                if (Image != null)
                {
                    pe.Graphics.DrawImage(Image, ClientRectangle);
                }

                if (IncludeBorder && BorderRadius > 0)
                {
                    // Draw the border
                    using (Pen pen = new Pen(BorderColor, BorderWidth))
                    {
                        pe.Graphics.DrawPath(pen, path);
                    }
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Do not paint the background to keep it transparent
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate(); // Invalidate to repaint with the updated parent background
        }
    }
}

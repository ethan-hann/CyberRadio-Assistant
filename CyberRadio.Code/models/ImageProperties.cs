using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Represents the properties of an image. Used when displaying images in the Icon Manager.
    /// </summary>
    public class ImageProperties
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public ImageFormat ImageFormat { get; set; } = ImageFormat.Png;
        public PixelFormat PixelFormat { get; set; } = PixelFormat.DontCare;
    }
}

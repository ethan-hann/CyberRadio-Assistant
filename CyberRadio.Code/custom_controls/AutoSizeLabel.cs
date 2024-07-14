using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.custom_controls
{
    public class AutoSizeLabel : Label
    {
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            ResizeFontToFit();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ResizeFontToFit();
        }

        private void ResizeFontToFit()
        {
            if (string.IsNullOrEmpty(Text))
                return;

            // Initial font size
            float fontSize = 10;
            Font font = new Font(Font.FontFamily, fontSize, Font.Style);

            // Calculate the size of the text with the initial font
            Size textSize;
            using (Graphics g = CreateGraphics())
            {
                textSize = g.MeasureString(Text, font).ToSize();
            }

            // Adjust the font size
            while (textSize.Width < ClientSize.Width && textSize.Height < ClientSize.Height)
            {
                fontSize++;
                font = new Font(Font.FontFamily, fontSize, Font.Style);
                using (Graphics g = CreateGraphics())
                {
                    textSize = g.MeasureString(Text, font).ToSize();
                }
            }

            while ((textSize.Width > ClientSize.Width || textSize.Height > ClientSize.Height) && fontSize > 1)
            {
                fontSize--;
                font = new Font(Font.FontFamily, fontSize, Font.Style);
                using (Graphics g = CreateGraphics())
                {
                    textSize = g.MeasureString(Text, font).ToSize();
                }
            }

            // Apply the adjusted font size
            Font = new Font(Font.FontFamily, fontSize, Font.Style);
        }
    }
}

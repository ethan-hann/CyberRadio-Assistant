using RadioExt_Helper.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.user_controls
{
    public class ImageComboBox : ComboBox
    {
        public ImageComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            e.DrawFocusRectangle();

            if (Items[e.Index] is LanguageItem item)
            {
                var bounds = e.Bounds;
                var flagSize = new Size(16, 16);
                if (item.Flag != null)
                    e.Graphics.DrawImage(item.Flag, bounds.Left, bounds.Top, flagSize.Width, flagSize.Height);

                using var brush = new SolidBrush(e.ForeColor);
                e.Graphics.DrawString(item.Language, e.Font, brush, bounds.Left + flagSize.Width + 5, bounds.Top + (bounds.Height - e.Font.Height) / 2);
            }
            else
                base.OnDrawItem(e);
        }
    }
}

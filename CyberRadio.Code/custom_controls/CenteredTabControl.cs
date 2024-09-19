using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.custom_controls
{
    public class CenteredTabControl : TabControl
    {
        public CenteredTabControl()
        {
            // Set the TabControl to allow custom drawing
            DrawMode = TabDrawMode.OwnerDrawFixed;

            // Optionally, disable the top-level border
            Appearance = TabAppearance.FlatButtons;

            // Handle the DrawItem event for custom tab drawing
            DrawItem += CustomTabControl_DrawItem;
        }

        // Override the default drawing of the tab items
        private void CustomTabControl_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (sender is not TabControl tabControl)
            {
                return;
            }

            // Set custom font for the tabs
            var customFont = new Font("Segoue UI", 24, FontStyle.Bold);
            var textColor = Color.Black;

            // Get the bounds of the tab
            var tabBounds = e.Bounds;

            // Fill the background of the tab (white or any other color)
            e.Graphics.FillRectangle(Brushes.White, tabBounds);

            // Center the text in the middle of the tab
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,  // Center horizontally
                LineAlignment = StringAlignment.Center // Center vertically
            };

            // Draw the text for the tab using large font size
            var textBrush = new SolidBrush(textColor);
            e.Graphics.DrawString(tabControl.TabPages[e.Index].Text, customFont, textBrush, tabBounds, stringFormat);

            // Hide the tab border by skipping the default border drawing
            e.DrawBackground();  // This ensures the background is drawn without the standard border
        }

        // Prevent the tab borders from being drawn
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Hide tab borders by skipping their default painting behavior
            foreach (TabPage tabPage in TabPages)
            {
                tabPage.BorderStyle = BorderStyle.None; // Ensure no borders are drawn on the tab page itself
            }
        }

        // Center the tabs within the parent
        protected override void CreateHandle()
        {
            base.CreateHandle();

            // Ensure the tab headers are centered
            Alignment = TabAlignment.Top;
        }
    }
}

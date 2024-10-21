// AutoSizeLabel.cs : RadioExt-Helper
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

namespace RadioExt_Helper.custom_controls;

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
        var font = new Font(Font.FontFamily, fontSize, Font.Style);

        // Calculate the size of the text with the initial font
        Size textSize;
        using (var g = CreateGraphics())
        {
            textSize = g.MeasureString(Text, font).ToSize();
        }

        // Adjust the font size
        while (textSize.Width < ClientSize.Width && textSize.Height < ClientSize.Height)
        {
            fontSize++;
            font = new Font(Font.FontFamily, fontSize, Font.Style);
            using (var g = CreateGraphics())
            {
                textSize = g.MeasureString(Text, font).ToSize();
            }
        }

        while ((textSize.Width > ClientSize.Width || textSize.Height > ClientSize.Height) && fontSize > 1)
        {
            fontSize--;
            font = new Font(Font.FontFamily, fontSize, Font.Style);
            using (var g = CreateGraphics())
            {
                textSize = g.MeasureString(Text, font).ToSize();
            }
        }

        // Apply the adjusted font size
        Font = new Font(Font.FontFamily, fontSize, Font.Style);
    }
}
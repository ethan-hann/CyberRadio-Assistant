// WrapLabel.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
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

using System.ComponentModel;

namespace RadioExt_Helper.custom_controls;

public partial class WrapLabel : Label
{
    /// <summary>
    /// Defines the possible text alignment options within the <see cref="WrapLabel"/>.
    /// </summary>
    public enum InnerTextAlign
    {
        Left,
        Center,
        Right
    }

    private InnerTextAlign _innerTextAlign = InnerTextAlign.Left;

    [Browsable(true)]
    [DefaultValue(InnerTextAlign.Left)]
    [Category("Appearance")]
    [Description("Sets the text alignment within the label.")]
    public InnerTextAlign InnerTextAlignment
    {
        get => _innerTextAlign;
        set
        {
            _innerTextAlign = value;
            Invalidate(); // Redraw the control when the alignment changes
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        // Define the text format
        var format = new StringFormat
        {
            FormatFlags = StringFormatFlags.LineLimit,
            Trimming = StringTrimming.Word
        };

        // Set alignment based on the InnerTextAlignment property
        format.Alignment = InnerTextAlignment switch
        {
            InnerTextAlign.Left => StringAlignment.Near,
            InnerTextAlign.Center => StringAlignment.Center,
            InnerTextAlign.Right => StringAlignment.Far,
            _ => format.Alignment
        };

        // Measure the required height for the wrapped text
        var size = e.Graphics.MeasureString(Text, Font, Width, format);

        // Adjust the height of the label to fit the text
        Height = (int)Math.Ceiling(size.Height);

        // Draw the text
        e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new RectangleF(0, 0, Width, Height), format);
    }
}
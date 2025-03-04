// RoundedPictureBox.cs : RadioExt-Helper
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
using System.Drawing.Drawing2D;

namespace RadioExt_Helper.custom_controls;

/// <summary>
/// Represents a picture box control with rounded corners and customizable border.
/// </summary>
public sealed class RoundedPictureBox : PictureBox
{
    private string _imageKey = string.Empty;

    /// <summary>
    /// The image list containing the images.
    /// </summary>
    private ImageList? _imageList;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoundedPictureBox"/> class.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the radius of the rounded corners. Set to 0 to disable rounded corners.
    /// </summary>
    [Browsable(true)]
    [Category("Rounded Image")]
    [Description("The radius of the rounded corners. Set to 0 to disable rounded corners.")]
    public int BorderRadius { get; set; } = 20;

    /// <summary>
    /// Gets or sets whether to include a border around the button.
    /// </summary>
    [Browsable(true)]
    [Category("Rounded Image")]
    [Description("Whether to include a border around the button.")]
    public bool IncludeBorder { get; set; } = true;

    /// <summary>
    /// Gets or sets the color of the border.
    /// </summary>
    [Browsable(true)]
    [Category("Rounded Image")]
    [Description("The color of the border.")]
    public Color BorderColor { get; set; } = Color.Transparent;

    /// <summary>
    /// Gets or sets the width of the border.
    /// </summary>
    [Browsable(true)]
    [Category("Appearance")]
    [Description("The width of the border.")]
    public int BorderWidth { get; set; } = 2;

    /// <summary>
    /// Gets or sets the image list containing the images.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the key of the image to display.
    /// </summary>
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

    /// <summary>
    /// Updates the image displayed in the picture box.
    /// </summary>
    private void UpdateImage()
    {
        if (_imageList != null && !string.IsNullOrEmpty(_imageKey) && _imageList.Images.ContainsKey(_imageKey))
            Image = _imageList.Images[_imageKey];
    }

    /// <summary>
    /// Event handler for the MouseEnter event.
    /// </summary>
    /// <param name="e">The event data that contains the event specifics.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        if (_imageList != null) ImageKey = _imageKey + "_over";
    }

    /// <summary>
    /// Event handler for the MouseLeave event.
    /// </summary>
    /// <param name="e">The event data that contains the event specifics.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        if (_imageList != null) ImageKey = _imageKey;
    }

    /// <summary>
    /// Event handler for the MouseDown event.
    /// </summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
        base.OnMouseDown(mevent);
        if (_imageList != null) ImageKey = _imageKey + "_down";
    }

    /// <summary>
    /// Event handler for the MouseUp event.
    /// </summary>
    /// <param name="mevent">The MouseEventArgs that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
        base.OnMouseUp(mevent);
        if (_imageList != null)
            ImageKey = _imageKey + "_over";
    }

    /// <summary>
    /// Paints the control.
    /// </summary>
    /// <param name="pe">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs pe)
    {
        pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        using var path = new GraphicsPath();

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
        if (Image != null) pe.Graphics.DrawImage(Image, ClientRectangle);

        if (!IncludeBorder || BorderRadius <= 0) return;

        // Draw the border
        using var pen = new Pen(BorderColor, BorderWidth);
        pe.Graphics.DrawPath(pen, path);
    }

    /// <summary>
    /// Overrides the OnPaintBackground method to prevent painting the background.
    /// </summary>
    /// <param name="pevent">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        // Do not paint the background to keep it transparent
    }

    /// <summary>
    /// Handles the event when the parent control's back color changes.
    /// </summary>
    /// <param name="e">The event data that contains the event specifics.</param>
    protected override void OnParentBackColorChanged(EventArgs e)
    {
        base.OnParentBackColorChanged(e);
        Invalidate(); // Invalidate to repaint with the updated parent background
    }
}
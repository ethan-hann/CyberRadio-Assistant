using AetherUtils.Core.Logging;
using RadioExt_Helper.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.custom_controls
{
    internal class CustomPictureBox : PictureBox
    {
        public string ImagePath { get; private set; } = string.Empty;

        public CustomPictureBox()
        {
            AllowDrop = true;
            DragEnter += CustomPictureBox_DragEnter;
            DragDrop += CustomPictureBox_DragDrop;
        }

        private void CustomPictureBox_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) is true)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void CustomPictureBox_DragDrop(object? sender, DragEventArgs e)
        {
            try
            {
                var data = e.Data;
                if (data != null && data.GetDataPresent(DataFormats.FileDrop))
                {
                    var files = data.GetData(DataFormats.FileDrop) as string[];
                    if (files?.Length > 0)
                    {
                        var file = files[0];
                        var image = IconManager.Instance.LoadImage(file);
                        if (image != null)
                        {
                            Image = image;
                            ImagePath = file;
                        }
                    }
                }
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<CustomPictureBox>().Error(ex.Message);
            }
        }
    }
}

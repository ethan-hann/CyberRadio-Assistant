using RadioExt_Helper.user_controls;

namespace RadioExt_Helper.forms
{
    partial class LogWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindow));
            liveLogViewer = new LiveLogViewer();
            SuspendLayout();
            // 
            // liveLogViewer
            // 
            liveLogViewer.Dock = DockStyle.Fill;
            liveLogViewer.Location = new Point(0, 0);
            liveLogViewer.Name = "liveLogViewer";
            liveLogViewer.Size = new Size(992, 558);
            liveLogViewer.TabIndex = 0;
            // 
            // LogWindow
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(992, 558);
            Controls.Add(liveLogViewer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LogWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Live Log";
            FormClosing += LogWindow_FormClosing;
            Load += LogWindow_Load;
            ResumeLayout(false);
        }

        #endregion
        private LiveLogViewer liveLogViewer;
    }
}
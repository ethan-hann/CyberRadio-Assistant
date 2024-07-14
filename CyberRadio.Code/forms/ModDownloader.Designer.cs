namespace RadioExt_Helper.forms
{
    partial class ModDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModDownloader));
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnAddToQueue = new Button();
            lblModId = new Label();
            txtModId = new TextBox();
            tabsDownload = new TabControl();
            tabBrowser = new TabPage();
            wvNexusWebBrowser = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabQueue = new TabPage();
            lbModQueue = new ListBox();
            modDetails = new user_controls.ModDetails();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabsDownload.SuspendLayout();
            tabBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)wvNexusWebBrowser).BeginInit();
            tabQueue.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 600);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1053, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.status__16x16;
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(55, 17);
            lblStatus.Text = "Ready";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.971751F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95.02825F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 167F));
            tableLayoutPanel1.Controls.Add(btnAddToQueue, 2, 0);
            tableLayoutPanel1.Controls.Add(lblModId, 0, 0);
            tableLayoutPanel1.Controls.Add(txtModId, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1053, 41);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnAddToQueue
            // 
            btnAddToQueue.Anchor = AnchorStyles.Left;
            btnAddToQueue.BackColor = Color.Yellow;
            btnAddToQueue.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddToQueue.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddToQueue.FlatStyle = FlatStyle.Flat;
            btnAddToQueue.Image = Properties.Resources.add__16x16;
            btnAddToQueue.Location = new Point(888, 4);
            btnAddToQueue.Margin = new Padding(3, 2, 3, 2);
            btnAddToQueue.Name = "btnAddToQueue";
            btnAddToQueue.Size = new Size(151, 33);
            btnAddToQueue.TabIndex = 3;
            btnAddToQueue.Text = "Add To Queue";
            btnAddToQueue.TextAlign = ContentAlignment.MiddleRight;
            btnAddToQueue.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddToQueue.UseVisualStyleBackColor = false;
            btnAddToQueue.Click += BtnAddToQueue_Click;
            // 
            // lblModId
            // 
            lblModId.Anchor = AnchorStyles.Right;
            lblModId.AutoSize = true;
            lblModId.Location = new Point(10, 13);
            lblModId.Name = "lblModId";
            lblModId.Size = new Size(31, 15);
            lblModId.TabIndex = 0;
            lblModId.Text = "URL:";
            // 
            // txtModId
            // 
            txtModId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtModId.Location = new Point(47, 9);
            txtModId.Name = "txtModId";
            txtModId.ReadOnly = true;
            txtModId.Size = new Size(835, 23);
            txtModId.TabIndex = 4;
            // 
            // tabsDownload
            // 
            tabsDownload.Controls.Add(tabBrowser);
            tabsDownload.Controls.Add(tabQueue);
            tabsDownload.Dock = DockStyle.Fill;
            tabsDownload.Location = new Point(0, 41);
            tabsDownload.Name = "tabsDownload";
            tabsDownload.SelectedIndex = 0;
            tabsDownload.Size = new Size(1053, 559);
            tabsDownload.TabIndex = 2;
            // 
            // tabBrowser
            // 
            tabBrowser.BackColor = Color.White;
            tabBrowser.Controls.Add(wvNexusWebBrowser);
            tabBrowser.Location = new Point(4, 24);
            tabBrowser.Name = "tabBrowser";
            tabBrowser.Padding = new Padding(3);
            tabBrowser.Size = new Size(1045, 531);
            tabBrowser.TabIndex = 0;
            tabBrowser.Text = "Browser";
            // 
            // wvNexusWebBrowser
            // 
            wvNexusWebBrowser.AllowExternalDrop = true;
            wvNexusWebBrowser.CreationProperties = null;
            wvNexusWebBrowser.DefaultBackgroundColor = Color.White;
            wvNexusWebBrowser.Dock = DockStyle.Fill;
            wvNexusWebBrowser.Location = new Point(3, 3);
            wvNexusWebBrowser.Name = "wvNexusWebBrowser";
            wvNexusWebBrowser.Size = new Size(1039, 525);
            wvNexusWebBrowser.TabIndex = 0;
            wvNexusWebBrowser.ZoomFactor = 1D;
            wvNexusWebBrowser.CoreWebView2InitializationCompleted += WvNexusWebBrowser_CoreWebView2InitializationCompleted;
            wvNexusWebBrowser.NavigationStarting += WvNexusWebBrowser_NavigationStarting;
            wvNexusWebBrowser.NavigationCompleted += WvNexusWebBrowser_NavigationCompleted;
            wvNexusWebBrowser.SourceChanged += WvNexusWebBrowser_SourceChanged;
            // 
            // tabQueue
            // 
            tabQueue.BackColor = Color.White;
            tabQueue.Controls.Add(lbModQueue);
            tabQueue.Controls.Add(modDetails);
            tabQueue.Location = new Point(4, 24);
            tabQueue.Name = "tabQueue";
            tabQueue.Padding = new Padding(3);
            tabQueue.Size = new Size(1045, 531);
            tabQueue.TabIndex = 1;
            tabQueue.Text = "Download Queue";
            // 
            // lbModQueue
            // 
            lbModQueue.Dock = DockStyle.Fill;
            lbModQueue.FormattingEnabled = true;
            lbModQueue.ItemHeight = 15;
            lbModQueue.Location = new Point(3, 120);
            lbModQueue.Name = "lbModQueue";
            lbModQueue.Size = new Size(1039, 408);
            lbModQueue.TabIndex = 2;
            lbModQueue.SelectedIndexChanged += LbModQueue_SelectedIndexChanged;
            // 
            // modDetails
            // 
            modDetails.BackColor = Color.White;
            modDetails.Dock = DockStyle.Top;
            modDetails.Location = new Point(3, 3);
            modDetails.Name = "modDetails";
            modDetails.Size = new Size(1039, 117);
            modDetails.TabIndex = 1;
            // 
            // ModDownloader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1053, 622);
            Controls.Add(tabsDownload);
            Controls.Add(statusStrip1);
            Controls.Add(tableLayoutPanel1);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModDownloader";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Download Mods";
            TopMost = true;
            Load += ModDownloader_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabsDownload.ResumeLayout(false);
            tabBrowser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)wvNexusWebBrowser).EndInit();
            tabQueue.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblModId;
        private TextBox txtModId;
        private TabControl tabsDownload;
        private TabPage tabBrowser;
        private TabPage tabQueue;
        private Button btnAddToQueue;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvNexusWebBrowser;
        private user_controls.ModDetails modDetails;
        private ListBox lbModQueue;
    }
}
using RadioExt_Helper.user_controls;

namespace RadioExt_Helper.forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exportToGameToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            pathsToolStripMenuItem = new ToolStripMenuItem();
            refreshStationsToolStripMenuItem = new ToolStripMenuItem();
            languageToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            howToUseToolStripMenuItem = new ToolStripMenuItem();
            radioExtOnNexusModsToolStripMenuItem = new ToolStripMenuItem();
            radioExtGitHubToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            checkForUpdatesToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            grpStations = new GroupBox();
            lbStations = new StationListBox(components);
            stationBindingSource = new BindingSource(components);
            stationImageList = new ImageList(components);
            tableLayoutPanel2 = new TableLayoutPanel();
            btnDisableSelected = new AetherUtils.Core.WinForms.Controls.SplitButton();
            cmsDisable = new ContextMenuStrip(components);
            btnDisableAll = new ToolStripMenuItem();
            btnEnableSelected = new AetherUtils.Core.WinForms.Controls.SplitButton();
            cmsEnable = new ContextMenuStrip(components);
            btnEnableAll = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnDeleteStation = new Button();
            btnAddStation = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            grpStations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stationBindingSource).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            cmsDisable.SuspendLayout();
            cmsEnable.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Transparent;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, languageToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1380, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportToGameToolStripMenuItem, toolStripSeparator2, pathsToolStripMenuItem, refreshStationsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exportToGameToolStripMenuItem
            // 
            exportToGameToolStripMenuItem.Image = Properties.Resources.export_16x16;
            exportToGameToolStripMenuItem.Name = "exportToGameToolStripMenuItem";
            exportToGameToolStripMenuItem.Size = new Size(158, 22);
            exportToGameToolStripMenuItem.Text = "Export Stations";
            exportToGameToolStripMenuItem.Click += exportToGameToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(155, 6);
            // 
            // pathsToolStripMenuItem
            // 
            pathsToolStripMenuItem.Image = Properties.Resources.folder;
            pathsToolStripMenuItem.Name = "pathsToolStripMenuItem";
            pathsToolStripMenuItem.Size = new Size(158, 22);
            pathsToolStripMenuItem.Text = "Game Paths";
            pathsToolStripMenuItem.Click += pathsToolStripMenuItem_Click;
            // 
            // refreshStationsToolStripMenuItem
            // 
            refreshStationsToolStripMenuItem.Image = Properties.Resources.refresh;
            refreshStationsToolStripMenuItem.Name = "refreshStationsToolStripMenuItem";
            refreshStationsToolStripMenuItem.Size = new Size(158, 22);
            refreshStationsToolStripMenuItem.Text = "Refresh Stations";
            refreshStationsToolStripMenuItem.Click += refreshStationsToolStripMenuItem_Click;
            // 
            // languageToolStripMenuItem
            // 
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            languageToolStripMenuItem.Size = new Size(71, 20);
            languageToolStripMenuItem.Text = "Language";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { howToUseToolStripMenuItem, radioExtOnNexusModsToolStripMenuItem, radioExtGitHubToolStripMenuItem, toolStripSeparator1, aboutToolStripMenuItem, checkForUpdatesToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // howToUseToolStripMenuItem
            // 
            howToUseToolStripMenuItem.Image = Properties.Resources.guide;
            howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
            howToUseToolStripMenuItem.Size = new Size(200, 22);
            howToUseToolStripMenuItem.Text = "How To Use";
            howToUseToolStripMenuItem.Click += howToUseToolStripMenuItem_Click;
            // 
            // radioExtOnNexusModsToolStripMenuItem
            // 
            radioExtOnNexusModsToolStripMenuItem.Image = Properties.Resources.external_link;
            radioExtOnNexusModsToolStripMenuItem.Name = "radioExtOnNexusModsToolStripMenuItem";
            radioExtOnNexusModsToolStripMenuItem.Size = new Size(200, 22);
            radioExtOnNexusModsToolStripMenuItem.Text = "radioExt on NexusMods";
            radioExtOnNexusModsToolStripMenuItem.Click += radioExtOnNexusModsToolStripMenuItem_Click;
            // 
            // radioExtGitHubToolStripMenuItem
            // 
            radioExtGitHubToolStripMenuItem.Image = Properties.Resources.external_link;
            radioExtGitHubToolStripMenuItem.Name = "radioExtGitHubToolStripMenuItem";
            radioExtGitHubToolStripMenuItem.Size = new Size(200, 22);
            radioExtGitHubToolStripMenuItem.Text = "radioExt GitHub";
            radioExtGitHubToolStripMenuItem.Click += radioExtHelpToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(197, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Image = Properties.Resources.info;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(200, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            checkForUpdatesToolStripMenuItem.Image = Properties.Resources.updated;
            checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            checkForUpdatesToolStripMenuItem.Size = new Size(200, 22);
            checkForUpdatesToolStripMenuItem.Text = "Check For Updates";
            checkForUpdatesToolStripMenuItem.Click += checkForUpdatesToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.Transparent;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Margin = new Padding(3, 2, 3, 2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(grpStations);
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.Transparent;
            splitContainer1.Size = new Size(1380, 856);
            splitContainer1.SplitterDistance = 328;
            splitContainer1.TabIndex = 1;
            // 
            // grpStations
            // 
            grpStations.Controls.Add(lbStations);
            grpStations.Controls.Add(tableLayoutPanel2);
            grpStations.Dock = DockStyle.Fill;
            grpStations.Location = new Point(0, 0);
            grpStations.Name = "grpStations";
            grpStations.Size = new Size(328, 819);
            grpStations.TabIndex = 2;
            grpStations.TabStop = false;
            grpStations.Text = "Stations";
            // 
            // lbStations
            // 
            lbStations.DataSource = stationBindingSource;
            lbStations.DisabledIconKey = "disabled";
            lbStations.DisplayMember = "MetaData";
            lbStations.Dock = DockStyle.Fill;
            lbStations.DrawMode = DrawMode.OwnerDrawFixed;
            lbStations.EnabledIconKey = "enabled";
            lbStations.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbStations.FormattingEnabled = true;
            lbStations.ImageList = stationImageList;
            lbStations.ItemHeight = 17;
            lbStations.Location = new Point(3, 53);
            lbStations.Margin = new Padding(3, 2, 3, 2);
            lbStations.Name = "lbStations";
            lbStations.Size = new Size(322, 763);
            lbStations.TabIndex = 0;
            lbStations.SelectedIndexChanged += lbStations_SelectedIndexChanged;
            lbStations.MouseDown += lbStations_MouseDown;
            // 
            // stationBindingSource
            // 
            stationBindingSource.DataSource = typeof(models.Station);
            // 
            // stationImageList
            // 
            stationImageList.ColorDepth = ColorDepth.Depth32Bit;
            stationImageList.ImageStream = (ImageListStreamer)resources.GetObject("stationImageList.ImageStream");
            stationImageList.TransparentColor = Color.Transparent;
            stationImageList.Images.SetKeyName(0, "enabled");
            stationImageList.Images.SetKeyName(1, "disabled");
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnDisableSelected, 1, 0);
            tableLayoutPanel2.Controls.Add(btnEnableSelected, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(322, 34);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // btnDisableSelected
            // 
            btnDisableSelected.BackColor = Color.Yellow;
            btnDisableSelected.Dock = DockStyle.Fill;
            btnDisableSelected.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnDisableSelected.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnDisableSelected.FlatStyle = FlatStyle.Flat;
            btnDisableSelected.Location = new Point(164, 3);
            btnDisableSelected.Menu = cmsDisable;
            btnDisableSelected.Name = "btnDisableSelected";
            btnDisableSelected.Size = new Size(155, 28);
            btnDisableSelected.TabIndex = 1;
            btnDisableSelected.Text = "Disable Selected";
            btnDisableSelected.UseVisualStyleBackColor = false;
            btnDisableSelected.Click += btnDisableStation_Click;
            // 
            // cmsDisable
            // 
            cmsDisable.BackColor = Color.White;
            cmsDisable.Items.AddRange(new ToolStripItem[] { btnDisableAll });
            cmsDisable.Name = "cmsDisable";
            cmsDisable.Size = new Size(130, 26);
            // 
            // btnDisableAll
            // 
            btnDisableAll.BackColor = Color.White;
            btnDisableAll.Name = "btnDisableAll";
            btnDisableAll.Size = new Size(129, 22);
            btnDisableAll.Text = "Disable All";
            btnDisableAll.Click += btnDisableAll_Click;
            // 
            // btnEnableSelected
            // 
            btnEnableSelected.BackColor = Color.Yellow;
            btnEnableSelected.Dock = DockStyle.Fill;
            btnEnableSelected.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnEnableSelected.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnEnableSelected.FlatStyle = FlatStyle.Flat;
            btnEnableSelected.Location = new Point(3, 3);
            btnEnableSelected.Menu = cmsEnable;
            btnEnableSelected.Name = "btnEnableSelected";
            btnEnableSelected.Size = new Size(155, 28);
            btnEnableSelected.TabIndex = 0;
            btnEnableSelected.Text = "Enable Selected";
            btnEnableSelected.UseVisualStyleBackColor = false;
            btnEnableSelected.Click += btnEnableStation_Click;
            // 
            // cmsEnable
            // 
            cmsEnable.BackColor = Color.White;
            cmsEnable.Items.AddRange(new ToolStripItem[] { btnEnableAll });
            cmsEnable.Name = "cmsDisable";
            cmsEnable.Size = new Size(127, 26);
            // 
            // btnEnableAll
            // 
            btnEnableAll.Name = "btnEnableAll";
            btnEnableAll.Size = new Size(126, 22);
            btnEnableAll.Text = "Enable All";
            btnEnableAll.Click += btnEnableAll_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnDeleteStation, 1, 0);
            tableLayoutPanel1.Controls.Add(btnAddStation, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 819);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(328, 37);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnDeleteStation
            // 
            btnDeleteStation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDeleteStation.BackColor = Color.Yellow;
            btnDeleteStation.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnDeleteStation.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnDeleteStation.FlatStyle = FlatStyle.Flat;
            btnDeleteStation.Image = Properties.Resources.cancel_16x16;
            btnDeleteStation.Location = new Point(167, 2);
            btnDeleteStation.Margin = new Padding(3, 2, 3, 2);
            btnDeleteStation.Name = "btnDeleteStation";
            btnDeleteStation.Size = new Size(158, 33);
            btnDeleteStation.TabIndex = 1;
            btnDeleteStation.Text = "Delete Station";
            btnDeleteStation.TextAlign = ContentAlignment.MiddleRight;
            btnDeleteStation.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDeleteStation.UseVisualStyleBackColor = false;
            btnDeleteStation.Click += btnDeleteStation_Click;
            // 
            // btnAddStation
            // 
            btnAddStation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnAddStation.BackColor = Color.Yellow;
            btnAddStation.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddStation.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddStation.FlatStyle = FlatStyle.Flat;
            btnAddStation.Image = Properties.Resources.plus_16x16;
            btnAddStation.Location = new Point(3, 2);
            btnAddStation.Margin = new Padding(3, 2, 3, 2);
            btnAddStation.Name = "btnAddStation";
            btnAddStation.Size = new Size(158, 33);
            btnAddStation.TabIndex = 0;
            btnAddStation.Text = "New Station";
            btnAddStation.TextAlign = ContentAlignment.MiddleRight;
            btnAddStation.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddStation.UseVisualStyleBackColor = false;
            btnAddStation.Click += btnAddStation_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1380, 880);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cyber Radio Assistant";
            HelpButtonClicked += MainForm_HelpButtonClicked;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            Resize += MainForm_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            grpStations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)stationBindingSource).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            cmsDisable.ResumeLayout(false);
            cmsEnable.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem pathsToolStripMenuItem;
        private ToolStripMenuItem howToUseToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private StationListBox lbStations;
        private Button btnDeleteStation;
        private Button btnAddStation;
        private ToolStripMenuItem refreshStationsToolStripMenuItem;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem radioExtOnNexusModsToolStripMenuItem;
        private ToolStripMenuItem radioExtGitHubToolStripMenuItem;
        private BindingSource stationBindingSource;
        private ToolStripSeparator toolStripSeparator2;
        private GroupBox grpStations;
        private ToolStripMenuItem exportToGameToolStripMenuItem;
        private ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private ImageList stationImageList;
        private TableLayoutPanel tableLayoutPanel2;
        private AetherUtils.Core.WinForms.Controls.SplitButton btnEnableSelected;
        private AetherUtils.Core.WinForms.Controls.SplitButton btnDisableSelected;
        private ContextMenuStrip cmsDisable;
        private ToolStripMenuItem btnDisableAll;
        private ContextMenuStrip cmsEnable;
        private ToolStripMenuItem btnEnableAll;
    }
}

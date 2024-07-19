using RadioExt_Helper.custom_controls;
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
            refreshStationsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            backupStagingFolderToolStripMenuItem = new ToolStripMenuItem();
            openStagingPathToolStripMenuItem = new ToolStripMenuItem();
            openGamePathToolStripMenuItem = new ToolStripMenuItem();
            openLogFolderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            configurationToolStripMenuItem = new ToolStripMenuItem();
            pathsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            modsToolStripMenuItem = new ToolStripMenuItem();
            downloadRadioModsToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            howToUseToolStripMenuItem = new ToolStripMenuItem();
            radioExtOnNexusModsToolStripMenuItem = new ToolStripMenuItem();
            radioExtGitHubToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            checkForUpdatesToolStripMenuItem = new ToolStripMenuItem();
            languageToolStripMenuItem = new ToolStripMenuItem();
            apiStatusToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            grpStations = new GroupBox();
            lbStations = new StationListBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            lblStationCount = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
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
            stationBindingSource = new BindingSource(components);
            cmsRevertStationChanges = new ContextMenuStrip(components);
            revertChangesToolStripMenuItem = new ToolStripMenuItem();
            statusStripBackup = new StatusStrip();
            lblBackupStatus = new ToolStripStatusLabel();
            pgBackupProgress = new ToolStripProgressBar();
            lblSpring2 = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            grpStations.SuspendLayout();
            statusStrip1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            cmsDisable.SuspendLayout();
            cmsEnable.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stationBindingSource).BeginInit();
            cmsRevertStationChanges.SuspendLayout();
            statusStripBackup.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Transparent;
            menuStrip1.Font = new Font("Segoe UI Variable Display Semib", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, modsToolStripMenuItem, helpToolStripMenuItem, languageToolStripMenuItem, apiStatusToolStripMenuItem });
            menuStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1224, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportToGameToolStripMenuItem, refreshStationsToolStripMenuItem, toolStripSeparator2, backupStagingFolderToolStripMenuItem, openStagingPathToolStripMenuItem, openGamePathToolStripMenuItem, openLogFolderToolStripMenuItem, toolStripSeparator3, configurationToolStripMenuItem, pathsToolStripMenuItem, toolStripSeparator4, exitToolStripMenuItem });
            fileToolStripMenuItem.Image = Properties.Resources.file;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(53, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exportToGameToolStripMenuItem
            // 
            exportToGameToolStripMenuItem.Image = Properties.Resources.export;
            exportToGameToolStripMenuItem.Name = "exportToGameToolStripMenuItem";
            exportToGameToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            exportToGameToolStripMenuItem.Size = new Size(273, 22);
            exportToGameToolStripMenuItem.Text = "Export Stations";
            exportToGameToolStripMenuItem.Click += ExportToGameToolStripMenuItem_Click;
            // 
            // refreshStationsToolStripMenuItem
            // 
            refreshStationsToolStripMenuItem.Image = Properties.Resources.refresh;
            refreshStationsToolStripMenuItem.Name = "refreshStationsToolStripMenuItem";
            refreshStationsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            refreshStationsToolStripMenuItem.Size = new Size(273, 22);
            refreshStationsToolStripMenuItem.Text = "Refresh Stations";
            refreshStationsToolStripMenuItem.Click += RefreshStationsToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(270, 6);
            // 
            // backupStagingFolderToolStripMenuItem
            // 
            backupStagingFolderToolStripMenuItem.Image = Properties.Resources.zip_file_16x16;
            backupStagingFolderToolStripMenuItem.Name = "backupStagingFolderToolStripMenuItem";
            backupStagingFolderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.B;
            backupStagingFolderToolStripMenuItem.Size = new Size(273, 22);
            backupStagingFolderToolStripMenuItem.Text = "Backup Staging Folder";
            backupStagingFolderToolStripMenuItem.Click += BackupStagingFolderToolStripMenuItem_Click;
            // 
            // openStagingPathToolStripMenuItem
            // 
            openStagingPathToolStripMenuItem.Image = Properties.Resources.link;
            openStagingPathToolStripMenuItem.Name = "openStagingPathToolStripMenuItem";
            openStagingPathToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.S;
            openStagingPathToolStripMenuItem.Size = new Size(273, 22);
            openStagingPathToolStripMenuItem.Text = "Open Staging Folder";
            openStagingPathToolStripMenuItem.Click += OpenStagingPathToolStripMenuItem_Click;
            // 
            // openGamePathToolStripMenuItem
            // 
            openGamePathToolStripMenuItem.Image = Properties.Resources.link;
            openGamePathToolStripMenuItem.Name = "openGamePathToolStripMenuItem";
            openGamePathToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.G;
            openGamePathToolStripMenuItem.Size = new Size(273, 22);
            openGamePathToolStripMenuItem.Text = "Open Game Radios Folder";
            openGamePathToolStripMenuItem.Click += OpenGamePathToolStripMenuItem_Click;
            // 
            // openLogFolderToolStripMenuItem
            // 
            openLogFolderToolStripMenuItem.Image = Properties.Resources.link;
            openLogFolderToolStripMenuItem.Name = "openLogFolderToolStripMenuItem";
            openLogFolderToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.L;
            openLogFolderToolStripMenuItem.Size = new Size(273, 22);
            openLogFolderToolStripMenuItem.Text = "Open Log Folder";
            openLogFolderToolStripMenuItem.Click += OpenLogFolderToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(270, 6);
            // 
            // configurationToolStripMenuItem
            // 
            configurationToolStripMenuItem.Image = Properties.Resources.settings;
            configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            configurationToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.C;
            configurationToolStripMenuItem.Size = new Size(273, 22);
            configurationToolStripMenuItem.Text = "Configuration";
            configurationToolStripMenuItem.Click += ConfigurationToolStripMenuItem_Click;
            // 
            // pathsToolStripMenuItem
            // 
            pathsToolStripMenuItem.Image = Properties.Resources.folder;
            pathsToolStripMenuItem.Name = "pathsToolStripMenuItem";
            pathsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            pathsToolStripMenuItem.Size = new Size(273, 22);
            pathsToolStripMenuItem.Text = "Game Paths";
            pathsToolStripMenuItem.Click += PathsToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(270, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Properties.Resources.exit_16x16;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            exitToolStripMenuItem.Size = new Size(273, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // modsToolStripMenuItem
            // 
            modsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { downloadRadioModsToolStripMenuItem });
            modsToolStripMenuItem.Image = Properties.Resources.game_16x16;
            modsToolStripMenuItem.Name = "modsToolStripMenuItem";
            modsToolStripMenuItem.Size = new Size(65, 20);
            modsToolStripMenuItem.Text = "Mods";
            modsToolStripMenuItem.Visible = false;
            // 
            // downloadRadioModsToolStripMenuItem
            // 
            downloadRadioModsToolStripMenuItem.Image = Properties.Resources.download_16x16;
            downloadRadioModsToolStripMenuItem.Name = "downloadRadioModsToolStripMenuItem";
            downloadRadioModsToolStripMenuItem.Size = new Size(193, 22);
            downloadRadioModsToolStripMenuItem.Text = "Download Radio Mods";
            downloadRadioModsToolStripMenuItem.Click += DownloadRadioModsToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { howToUseToolStripMenuItem, radioExtOnNexusModsToolStripMenuItem, radioExtGitHubToolStripMenuItem, toolStripSeparator1, aboutToolStripMenuItem, checkForUpdatesToolStripMenuItem });
            helpToolStripMenuItem.Image = Properties.Resources.info;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(60, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // howToUseToolStripMenuItem
            // 
            howToUseToolStripMenuItem.Image = Properties.Resources.guide;
            howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
            howToUseToolStripMenuItem.ShortcutKeys = Keys.F1;
            howToUseToolStripMenuItem.Size = new Size(202, 22);
            howToUseToolStripMenuItem.Text = "How To Use";
            howToUseToolStripMenuItem.Click += HowToUseToolStripMenuItem_Click;
            // 
            // radioExtOnNexusModsToolStripMenuItem
            // 
            radioExtOnNexusModsToolStripMenuItem.Image = Properties.Resources.link;
            radioExtOnNexusModsToolStripMenuItem.Name = "radioExtOnNexusModsToolStripMenuItem";
            radioExtOnNexusModsToolStripMenuItem.Size = new Size(202, 22);
            radioExtOnNexusModsToolStripMenuItem.Text = "radioExt on NexusMods";
            radioExtOnNexusModsToolStripMenuItem.Click += RadioExtOnNexusModsToolStripMenuItem_Click;
            // 
            // radioExtGitHubToolStripMenuItem
            // 
            radioExtGitHubToolStripMenuItem.Image = Properties.Resources.link;
            radioExtGitHubToolStripMenuItem.Name = "radioExtGitHubToolStripMenuItem";
            radioExtGitHubToolStripMenuItem.Size = new Size(202, 22);
            radioExtGitHubToolStripMenuItem.Text = "radioExt GitHub";
            radioExtGitHubToolStripMenuItem.Click += RadioExtHelpToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(199, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Image = Properties.Resources.info;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.ShortcutKeys = Keys.F2;
            aboutToolStripMenuItem.Size = new Size(202, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            checkForUpdatesToolStripMenuItem.Image = Properties.Resources.update;
            checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            checkForUpdatesToolStripMenuItem.ShortcutKeys = Keys.F3;
            checkForUpdatesToolStripMenuItem.Size = new Size(202, 22);
            checkForUpdatesToolStripMenuItem.Text = "Check For Updates";
            checkForUpdatesToolStripMenuItem.Click += CheckForUpdatesToolStripMenuItem_Click;
            // 
            // languageToolStripMenuItem
            // 
            languageToolStripMenuItem.Image = Properties.Resources.language;
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            languageToolStripMenuItem.RightToLeft = RightToLeft.No;
            languageToolStripMenuItem.Size = new Size(87, 20);
            languageToolStripMenuItem.Text = "Language";
            // 
            // apiStatusToolStripMenuItem
            // 
            apiStatusToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            apiStatusToolStripMenuItem.ImageAlign = ContentAlignment.MiddleLeft;
            apiStatusToolStripMenuItem.Name = "apiStatusToolStripMenuItem";
            apiStatusToolStripMenuItem.Size = new Size(124, 20);
            apiStatusToolStripMenuItem.Text = "API Not Connected";
            apiStatusToolStripMenuItem.Visible = false;
            apiStatusToolStripMenuItem.Click += ApiStatusToolStripMenuItem_Click;
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
            splitContainer1.Size = new Size(1224, 534);
            splitContainer1.SplitterDistance = 315;
            splitContainer1.TabIndex = 1;
            // 
            // grpStations
            // 
            grpStations.Controls.Add(lbStations);
            grpStations.Controls.Add(statusStrip1);
            grpStations.Controls.Add(tableLayoutPanel2);
            grpStations.Dock = DockStyle.Fill;
            grpStations.Location = new Point(0, 0);
            grpStations.Name = "grpStations";
            grpStations.Size = new Size(315, 497);
            grpStations.TabIndex = 2;
            grpStations.TabStop = false;
            grpStations.Text = "Stations";
            // 
            // lbStations
            // 
            lbStations.CausesValidation = false;
            lbStations.DisabledIconKey = "disabled";
            lbStations.Dock = DockStyle.Fill;
            lbStations.DrawMode = DrawMode.OwnerDrawFixed;
            lbStations.DuplicateColor = Color.FromArgb(255, 128, 0);
            lbStations.DuplicateFont = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbStations.EditedStationIconKey = "edited_station";
            lbStations.EnabledIconKey = "enabled";
            lbStations.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbStations.FormattingEnabled = true;
            lbStations.ItemHeight = 17;
            lbStations.Location = new Point(3, 53);
            lbStations.Margin = new Padding(3, 2, 3, 2);
            lbStations.Name = "lbStations";
            lbStations.SavedStationIconKey = "saved_station";
            lbStations.Size = new Size(309, 419);
            lbStations.SongsMissingColor = Color.FromArgb(192, 0, 0);
            lbStations.SongsMissingFont = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbStations.TabIndex = 0;
            lbStations.SelectedIndexChanged += LbStations_SelectedIndexChanged;
            lbStations.MouseDown += LbStations_MouseDown;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.White;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, lblStationCount, toolStripStatusLabel3 });
            statusStrip1.Location = new Point(3, 472);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(309, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(63, 17);
            toolStripStatusLabel1.Spring = true;
            // 
            // lblStationCount
            // 
            lblStationCount.Font = new Font("Segoe UI Variable Small", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStationCount.Name = "lblStationCount";
            lblStationCount.Size = new Size(167, 17);
            lblStationCount.Text = "Enabled Stations: {0} / {1}";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(63, 17);
            toolStripStatusLabel3.Spring = true;
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
            tableLayoutPanel2.Size = new Size(309, 34);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // btnDisableSelected
            // 
            btnDisableSelected.BackColor = Color.Yellow;
            btnDisableSelected.Dock = DockStyle.Fill;
            btnDisableSelected.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnDisableSelected.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnDisableSelected.FlatStyle = FlatStyle.Flat;
            btnDisableSelected.Image = Properties.Resources.disabled__16x16;
            btnDisableSelected.ImageAlign = ContentAlignment.MiddleLeft;
            btnDisableSelected.Location = new Point(157, 3);
            btnDisableSelected.Menu = cmsDisable;
            btnDisableSelected.Name = "btnDisableSelected";
            btnDisableSelected.Size = new Size(149, 28);
            btnDisableSelected.TabIndex = 1;
            btnDisableSelected.Text = "Disable Selected";
            btnDisableSelected.UseVisualStyleBackColor = false;
            btnDisableSelected.Click += BtnDisableStation_Click;
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
            btnDisableAll.Click += BtnDisableAll_Click;
            // 
            // btnEnableSelected
            // 
            btnEnableSelected.BackColor = Color.Yellow;
            btnEnableSelected.Dock = DockStyle.Fill;
            btnEnableSelected.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnEnableSelected.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnEnableSelected.FlatStyle = FlatStyle.Flat;
            btnEnableSelected.Image = Properties.Resources.enabled__16x16;
            btnEnableSelected.ImageAlign = ContentAlignment.MiddleLeft;
            btnEnableSelected.Location = new Point(3, 3);
            btnEnableSelected.Menu = cmsEnable;
            btnEnableSelected.Name = "btnEnableSelected";
            btnEnableSelected.Size = new Size(148, 28);
            btnEnableSelected.TabIndex = 0;
            btnEnableSelected.Text = "Enable Selected";
            btnEnableSelected.UseVisualStyleBackColor = false;
            btnEnableSelected.Click += BtnEnableStation_Click;
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
            btnEnableAll.Click += BtnEnableAll_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnDeleteStation, 1, 0);
            tableLayoutPanel1.Controls.Add(btnAddStation, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 497);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(315, 37);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnDeleteStation
            // 
            btnDeleteStation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDeleteStation.BackColor = Color.Yellow;
            btnDeleteStation.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnDeleteStation.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnDeleteStation.FlatStyle = FlatStyle.Flat;
            btnDeleteStation.Image = Properties.Resources.delete__16x16;
            btnDeleteStation.Location = new Point(160, 2);
            btnDeleteStation.Margin = new Padding(3, 2, 3, 2);
            btnDeleteStation.Name = "btnDeleteStation";
            btnDeleteStation.Size = new Size(152, 33);
            btnDeleteStation.TabIndex = 1;
            btnDeleteStation.Text = "Delete Station";
            btnDeleteStation.TextAlign = ContentAlignment.MiddleRight;
            btnDeleteStation.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDeleteStation.UseVisualStyleBackColor = false;
            btnDeleteStation.Click += BtnDeleteStation_Click;
            // 
            // btnAddStation
            // 
            btnAddStation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnAddStation.BackColor = Color.Yellow;
            btnAddStation.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddStation.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddStation.FlatStyle = FlatStyle.Flat;
            btnAddStation.Image = Properties.Resources.add__16x16;
            btnAddStation.Location = new Point(3, 2);
            btnAddStation.Margin = new Padding(3, 2, 3, 2);
            btnAddStation.Name = "btnAddStation";
            btnAddStation.Size = new Size(151, 33);
            btnAddStation.TabIndex = 0;
            btnAddStation.Text = "New Station";
            btnAddStation.TextAlign = ContentAlignment.MiddleRight;
            btnAddStation.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddStation.UseVisualStyleBackColor = false;
            btnAddStation.Click += BtnAddStation_Click;
            // 
            // stationBindingSource
            // 
            stationBindingSource.DataSource = typeof(models.Station);
            // 
            // cmsRevertStationChanges
            // 
            cmsRevertStationChanges.Items.AddRange(new ToolStripItem[] { revertChangesToolStripMenuItem });
            cmsRevertStationChanges.Name = "cmsRevertStationChanges";
            cmsRevertStationChanges.Size = new Size(157, 26);
            // 
            // revertChangesToolStripMenuItem
            // 
            revertChangesToolStripMenuItem.Image = Properties.Resources.refresh__16x16;
            revertChangesToolStripMenuItem.Name = "revertChangesToolStripMenuItem";
            revertChangesToolStripMenuItem.Size = new Size(156, 22);
            revertChangesToolStripMenuItem.Text = "Revert Changes";
            revertChangesToolStripMenuItem.Click += RevertChangesToolStripMenuItem_Click;
            // 
            // statusStripBackup
            // 
            statusStripBackup.BackColor = Color.Transparent;
            statusStripBackup.Items.AddRange(new ToolStripItem[] { lblBackupStatus, pgBackupProgress, lblSpring2 });
            statusStripBackup.Location = new Point(0, 539);
            statusStripBackup.Name = "statusStripBackup";
            statusStripBackup.Size = new Size(1224, 22);
            statusStripBackup.SizingGrip = false;
            statusStripBackup.TabIndex = 3;
            statusStripBackup.Text = "statusStrip2";
            statusStripBackup.Visible = false;
            // 
            // lblBackupStatus
            // 
            lblBackupStatus.Image = Properties.Resources.status__16x16;
            lblBackupStatus.Name = "lblBackupStatus";
            lblBackupStatus.Size = new Size(97, 17);
            lblBackupStatus.Text = "Backup Ready";
            // 
            // pgBackupProgress
            // 
            pgBackupProgress.Margin = new Padding(5, 3, 1, 3);
            pgBackupProgress.Name = "pgBackupProgress";
            pgBackupProgress.Size = new Size(300, 16);
            // 
            // lblSpring2
            // 
            lblSpring2.Name = "lblSpring2";
            lblSpring2.Size = new Size(806, 17);
            lblSpring2.Spring = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1224, 558);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Controls.Add(statusStripBackup);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cyber Radio Assistant";
            HelpButtonClicked += MainForm_HelpButtonClicked;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            Resize += MainForm_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            grpStations.ResumeLayout(false);
            grpStations.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            cmsDisable.ResumeLayout(false);
            cmsEnable.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)stationBindingSource).EndInit();
            cmsRevertStationChanges.ResumeLayout(false);
            statusStripBackup.ResumeLayout(false);
            statusStripBackup.PerformLayout();
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
        private TableLayoutPanel tableLayoutPanel2;
        private AetherUtils.Core.WinForms.Controls.SplitButton btnEnableSelected;
        private AetherUtils.Core.WinForms.Controls.SplitButton btnDisableSelected;
        private ContextMenuStrip cmsDisable;
        private ToolStripMenuItem btnDisableAll;
        private ContextMenuStrip cmsEnable;
        private ToolStripMenuItem btnEnableAll;
        private ToolStripMenuItem openStagingPathToolStripMenuItem;
        private ToolStripMenuItem openGamePathToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel lblStationCount;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripMenuItem configurationToolStripMenuItem;
        private ToolStripMenuItem openLogFolderToolStripMenuItem;
        private ContextMenuStrip cmsRevertStationChanges;
        private ToolStripMenuItem revertChangesToolStripMenuItem;
        private ToolStripMenuItem modsToolStripMenuItem;
        private ToolStripMenuItem apiStatusToolStripMenuItem;
        private ToolStripMenuItem downloadRadioModsToolStripMenuItem;
        private ToolStripMenuItem backupStagingFolderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem exitToolStripMenuItem;
        private StatusStrip statusStripBackup;
        private ToolStripStatusLabel lblBackupStatus;
        private ToolStripProgressBar pgBackupProgress;
        private ToolStripStatusLabel lblSpring2;
    }
}

namespace RadioExt_Helper.forms
{
    partial class ConfigForm
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
            ListViewGroup listViewGroup1 = new ListViewGroup("Game Launchers", HorizontalAlignment.Left);
            ListViewGroup listViewGroup2 = new ListViewGroup("Mod Managers", HorizontalAlignment.Left);
            ListViewGroup listViewGroup3 = new ListViewGroup("Windows Related", HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            chkCheckForUpdates = new CheckBox();
            tableLayoutPanel7 = new TableLayoutPanel();
            lblBackupCompressionLvl = new Label();
            cmbCompressionLevels = new ComboBox();
            chkCopySongFilesToBackup = new CheckBox();
            chkAutoExportToGame = new CheckBox();
            chkWatchForChanges = new CheckBox();
            tableLayoutPanel6 = new TableLayoutPanel();
            btnEditDefaultSongLocation = new Button();
            lblDefaultSongLocation = new Label();
            tabConfigs = new TabControl();
            tabGeneral = new TabPage();
            tabLogging = new TabPage();
            tableLayoutPanel3 = new TableLayoutPanel();
            chkNewFileEveryLaunch = new CheckBox();
            btnEditLogsPath = new Button();
            lblLogPathLabel = new Label();
            lblCurrentLogPath = new Label();
            tabPathSetup = new TabPage();
            splitContainer1 = new SplitContainer();
            lvForbiddenPaths = new ListView();
            panel3 = new Panel();
            tableLayoutPanel5 = new TableLayoutPanel();
            btnEditPaths = new Button();
            btnDeleteSelectedKeyword = new Button();
            btnAddKeyword = new Button();
            tabNexus = new TabPage();
            tableLayoutPanel4 = new TableLayoutPanel();
            panel2 = new Panel();
            btnClearApiKey = new Button();
            btnAuthenticate = new Button();
            picApiStatus = new PictureBox();
            lblAuthenticatedStatus = new Label();
            lblApiHelp = new Label();
            panel1 = new Panel();
            lblApiKeyUnlocks = new Label();
            lnkApiAcceptableUse = new LinkLabel();
            lnkNexusApiKeyPage = new LinkLabel();
            lblApiInputHelp = new Label();
            txtApiKey = new MaskedTextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnReloadFromFile = new Button();
            btnSaveAndClose = new Button();
            btnCancel = new Button();
            btnResetToDefault = new Button();
            fldrOpenLogPath = new FolderBrowserDialog();
            statusStrip1 = new StatusStrip();
            lblHelpText = new ToolStripStatusLabel();
            fldrOpenDefaultMusicPath = new FolderBrowserDialog();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tabConfigs.SuspendLayout();
            tabGeneral.SuspendLayout();
            tabLogging.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tabPathSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel3.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tabNexus.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picApiStatus).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(chkCheckForUpdates, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel7, 0, 3);
            tableLayoutPanel1.Controls.Add(chkAutoExportToGame, 0, 1);
            tableLayoutPanel1.Controls.Add(chkWatchForChanges, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel6, 0, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(779, 220);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // chkCheckForUpdates
            // 
            chkCheckForUpdates.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkCheckForUpdates.AutoSize = true;
            chkCheckForUpdates.Location = new Point(10, 3);
            chkCheckForUpdates.Margin = new Padding(10, 3, 3, 3);
            chkCheckForUpdates.Name = "chkCheckForUpdates";
            chkCheckForUpdates.Size = new Size(766, 19);
            chkCheckForUpdates.TabIndex = 1;
            chkCheckForUpdates.Tag = "CheckForUpdatesOptionHelp";
            chkCheckForUpdates.Text = "Check for Updates at Startup?";
            chkCheckForUpdates.UseVisualStyleBackColor = true;
            chkCheckForUpdates.MouseEnter += ControlMouseEnter;
            chkCheckForUpdates.MouseLeave += ControlMouseLeave;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 3;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56.6607475F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.3392525F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 209F));
            tableLayoutPanel7.Controls.Add(lblBackupCompressionLvl, 1, 0);
            tableLayoutPanel7.Controls.Add(cmbCompressionLevels, 2, 0);
            tableLayoutPanel7.Controls.Add(chkCopySongFilesToBackup, 0, 0);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(3, 78);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Size = new Size(773, 33);
            tableLayoutPanel7.TabIndex = 2;
            // 
            // lblBackupCompressionLvl
            // 
            lblBackupCompressionLvl.Anchor = AnchorStyles.Right;
            lblBackupCompressionLvl.AutoSize = true;
            lblBackupCompressionLvl.Location = new Point(405, 9);
            lblBackupCompressionLvl.Name = "lblBackupCompressionLvl";
            lblBackupCompressionLvl.Size = new Size(155, 15);
            lblBackupCompressionLvl.TabIndex = 2;
            lblBackupCompressionLvl.Text = "Backup Compression Level: ";
            lblBackupCompressionLvl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbCompressionLevels
            // 
            cmbCompressionLevels.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCompressionLevels.FormattingEnabled = true;
            cmbCompressionLevels.Location = new Point(566, 3);
            cmbCompressionLevels.Name = "cmbCompressionLevels";
            cmbCompressionLevels.Size = new Size(204, 23);
            cmbCompressionLevels.TabIndex = 1;
            cmbCompressionLevels.Tag = "BackupCompressionLevelHelp";
            cmbCompressionLevels.MouseEnter += ControlMouseEnter;
            cmbCompressionLevels.MouseLeave += ControlMouseLeave;
            // 
            // chkCopySongFilesToBackup
            // 
            chkCopySongFilesToBackup.Anchor = AnchorStyles.Left;
            chkCopySongFilesToBackup.AutoSize = true;
            chkCopySongFilesToBackup.Location = new Point(7, 7);
            chkCopySongFilesToBackup.Margin = new Padding(7, 3, 3, 3);
            chkCopySongFilesToBackup.Name = "chkCopySongFilesToBackup";
            chkCopySongFilesToBackup.Size = new Size(171, 19);
            chkCopySongFilesToBackup.TabIndex = 3;
            chkCopySongFilesToBackup.Tag = "CopySongFilesToBackupHelp";
            chkCopySongFilesToBackup.Text = "Copy Song Files to Backup?";
            chkCopySongFilesToBackup.UseVisualStyleBackColor = true;
            chkCopySongFilesToBackup.MouseEnter += ControlMouseEnter;
            chkCopySongFilesToBackup.MouseLeave += ControlMouseLeave;
            // 
            // chkAutoExportToGame
            // 
            chkAutoExportToGame.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkAutoExportToGame.AutoSize = true;
            chkAutoExportToGame.Location = new Point(10, 28);
            chkAutoExportToGame.Margin = new Padding(10, 3, 3, 3);
            chkAutoExportToGame.Name = "chkAutoExportToGame";
            chkAutoExportToGame.Size = new Size(766, 19);
            chkAutoExportToGame.TabIndex = 2;
            chkAutoExportToGame.Tag = "AutoExportOptionHelp";
            chkAutoExportToGame.Text = "Auto-Export to Game?";
            chkAutoExportToGame.UseVisualStyleBackColor = true;
            chkAutoExportToGame.MouseEnter += ControlMouseEnter;
            chkAutoExportToGame.MouseLeave += ControlMouseLeave;
            // 
            // chkWatchForChanges
            // 
            chkWatchForChanges.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkWatchForChanges.AutoSize = true;
            chkWatchForChanges.Location = new Point(10, 53);
            chkWatchForChanges.Margin = new Padding(10, 3, 3, 3);
            chkWatchForChanges.Name = "chkWatchForChanges";
            chkWatchForChanges.Size = new Size(766, 19);
            chkWatchForChanges.TabIndex = 7;
            chkWatchForChanges.Tag = "WatchForChangesHelp";
            chkWatchForChanges.Text = "Watch For Station Changes?";
            chkWatchForChanges.UseVisualStyleBackColor = true;
            chkWatchForChanges.MouseEnter += ControlMouseEnter;
            chkWatchForChanges.MouseLeave += ControlMouseLeave;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.86934F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.13066F));
            tableLayoutPanel6.Controls.Add(btnEditDefaultSongLocation, 0, 0);
            tableLayoutPanel6.Controls.Add(lblDefaultSongLocation, 1, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(3, 117);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(773, 37);
            tableLayoutPanel6.TabIndex = 9;
            // 
            // btnEditDefaultSongLocation
            // 
            btnEditDefaultSongLocation.BackColor = Color.Yellow;
            btnEditDefaultSongLocation.Dock = DockStyle.Fill;
            btnEditDefaultSongLocation.FlatStyle = FlatStyle.Flat;
            btnEditDefaultSongLocation.Image = Properties.Resources.folder__16x16;
            btnEditDefaultSongLocation.Location = new Point(3, 3);
            btnEditDefaultSongLocation.Name = "btnEditDefaultSongLocation";
            btnEditDefaultSongLocation.Size = new Size(279, 31);
            btnEditDefaultSongLocation.TabIndex = 1;
            btnEditDefaultSongLocation.Tag = "DefaultSongLocationHelp";
            btnEditDefaultSongLocation.Text = "Default Song Location";
            btnEditDefaultSongLocation.TextAlign = ContentAlignment.MiddleRight;
            btnEditDefaultSongLocation.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditDefaultSongLocation.UseVisualStyleBackColor = false;
            btnEditDefaultSongLocation.Click += BtnEditDefaultSongLocation_Click;
            btnEditDefaultSongLocation.MouseEnter += ControlMouseEnter;
            btnEditDefaultSongLocation.MouseLeave += ControlMouseLeave;
            // 
            // lblDefaultSongLocation
            // 
            lblDefaultSongLocation.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblDefaultSongLocation.AutoSize = true;
            lblDefaultSongLocation.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            lblDefaultSongLocation.ForeColor = Color.Green;
            lblDefaultSongLocation.Location = new Point(288, 10);
            lblDefaultSongLocation.Name = "lblDefaultSongLocation";
            lblDefaultSongLocation.Size = new Size(482, 17);
            lblDefaultSongLocation.TabIndex = 2;
            lblDefaultSongLocation.Text = "<default_song_location>";
            // 
            // tabConfigs
            // 
            tabConfigs.Controls.Add(tabGeneral);
            tabConfigs.Controls.Add(tabLogging);
            tabConfigs.Controls.Add(tabPathSetup);
            tabConfigs.Controls.Add(tabNexus);
            tabConfigs.Dock = DockStyle.Fill;
            tabConfigs.Location = new Point(0, 0);
            tabConfigs.Name = "tabConfigs";
            tabConfigs.SelectedIndex = 0;
            tabConfigs.Size = new Size(793, 254);
            tabConfigs.TabIndex = 1;
            // 
            // tabGeneral
            // 
            tabGeneral.BackColor = Color.White;
            tabGeneral.Controls.Add(tableLayoutPanel1);
            tabGeneral.ImageKey = "config";
            tabGeneral.Location = new Point(4, 24);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new Padding(3);
            tabGeneral.Size = new Size(785, 226);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            // 
            // tabLogging
            // 
            tabLogging.BackColor = Color.White;
            tabLogging.Controls.Add(tableLayoutPanel3);
            tabLogging.ImageKey = "logging";
            tabLogging.Location = new Point(4, 24);
            tabLogging.Name = "tabLogging";
            tabLogging.Padding = new Padding(3);
            tabLogging.Size = new Size(785, 226);
            tabLogging.TabIndex = 1;
            tabLogging.Text = "Logging";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.42362F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69.57638F));
            tableLayoutPanel3.Controls.Add(chkNewFileEveryLaunch, 0, 0);
            tableLayoutPanel3.Controls.Add(btnEditLogsPath, 0, 5);
            tableLayoutPanel3.Controls.Add(lblLogPathLabel, 0, 6);
            tableLayoutPanel3.Controls.Add(lblCurrentLogPath, 1, 6);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 8;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(779, 220);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // chkNewFileEveryLaunch
            // 
            chkNewFileEveryLaunch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkNewFileEveryLaunch.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(chkNewFileEveryLaunch, 2);
            chkNewFileEveryLaunch.Location = new Point(10, 3);
            chkNewFileEveryLaunch.Margin = new Padding(10, 3, 3, 3);
            chkNewFileEveryLaunch.Name = "chkNewFileEveryLaunch";
            chkNewFileEveryLaunch.Size = new Size(766, 19);
            chkNewFileEveryLaunch.TabIndex = 1;
            chkNewFileEveryLaunch.Tag = "NewLogFileOptionHelp";
            chkNewFileEveryLaunch.Text = "New File Every Launch?";
            chkNewFileEveryLaunch.UseVisualStyleBackColor = true;
            chkNewFileEveryLaunch.MouseEnter += ControlMouseEnter;
            chkNewFileEveryLaunch.MouseLeave += ControlMouseLeave;
            // 
            // btnEditLogsPath
            // 
            btnEditLogsPath.BackColor = Color.Yellow;
            tableLayoutPanel3.SetColumnSpan(btnEditLogsPath, 2);
            btnEditLogsPath.Dock = DockStyle.Fill;
            btnEditLogsPath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnEditLogsPath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnEditLogsPath.FlatStyle = FlatStyle.Flat;
            btnEditLogsPath.Image = Properties.Resources.folder__16x16;
            btnEditLogsPath.Location = new Point(3, 28);
            btnEditLogsPath.Name = "btnEditLogsPath";
            btnEditLogsPath.Size = new Size(773, 31);
            btnEditLogsPath.TabIndex = 7;
            btnEditLogsPath.Tag = "EditLogsPathOptionHelp";
            btnEditLogsPath.Text = "Edit Logs Path...";
            btnEditLogsPath.TextAlign = ContentAlignment.MiddleRight;
            btnEditLogsPath.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditLogsPath.UseVisualStyleBackColor = false;
            btnEditLogsPath.Click += BtnEditLogsPath_Click;
            btnEditLogsPath.MouseEnter += ControlMouseEnter;
            btnEditLogsPath.MouseLeave += ControlMouseLeave;
            // 
            // lblLogPathLabel
            // 
            lblLogPathLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblLogPathLabel.AutoSize = true;
            lblLogPathLabel.Location = new Point(3, 64);
            lblLogPathLabel.Name = "lblLogPathLabel";
            lblLogPathLabel.Size = new Size(231, 15);
            lblLogPathLabel.TabIndex = 9;
            lblLogPathLabel.Text = "Log Path:";
            lblLogPathLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblCurrentLogPath
            // 
            lblCurrentLogPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblCurrentLogPath.AutoSize = true;
            lblCurrentLogPath.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCurrentLogPath.ForeColor = Color.Green;
            lblCurrentLogPath.Location = new Point(240, 63);
            lblCurrentLogPath.Name = "lblCurrentLogPath";
            lblCurrentLogPath.Size = new Size(536, 17);
            lblCurrentLogPath.TabIndex = 10;
            lblCurrentLogPath.Text = "<no path>";
            // 
            // tabPathSetup
            // 
            tabPathSetup.BackColor = Color.White;
            tabPathSetup.Controls.Add(splitContainer1);
            tabPathSetup.Location = new Point(4, 24);
            tabPathSetup.Name = "tabPathSetup";
            tabPathSetup.Padding = new Padding(3);
            tabPathSetup.Size = new Size(785, 226);
            tabPathSetup.TabIndex = 3;
            tabPathSetup.Text = "Paths Setup";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lvForbiddenPaths);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel3);
            splitContainer1.Size = new Size(779, 220);
            splitContainer1.SplitterDistance = 562;
            splitContainer1.TabIndex = 1;
            // 
            // lvForbiddenPaths
            // 
            lvForbiddenPaths.CheckBoxes = true;
            lvForbiddenPaths.Dock = DockStyle.Fill;
            listViewGroup1.Header = "Game Launchers";
            listViewGroup1.Name = "lvgGameLaunchers";
            listViewGroup2.Header = "Mod Managers";
            listViewGroup2.Name = "lvgModManagers";
            listViewGroup3.Header = "Windows Related";
            listViewGroup3.Name = "lvgWindowsRelated";
            lvForbiddenPaths.Groups.AddRange(new ListViewGroup[] { listViewGroup1, listViewGroup2, listViewGroup3 });
            lvForbiddenPaths.Location = new Point(0, 0);
            lvForbiddenPaths.Name = "lvForbiddenPaths";
            lvForbiddenPaths.Size = new Size(562, 220);
            lvForbiddenPaths.TabIndex = 1;
            lvForbiddenPaths.Tag = "ForbiddenPathsHelp";
            lvForbiddenPaths.UseCompatibleStateImageBehavior = false;
            lvForbiddenPaths.View = View.Details;
            lvForbiddenPaths.ItemChecked += lvForbiddenPaths_ItemChecked;
            lvForbiddenPaths.MouseEnter += ControlMouseEnter;
            lvForbiddenPaths.MouseLeave += ControlMouseLeave;
            // 
            // panel3
            // 
            panel3.Controls.Add(tableLayoutPanel5);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(213, 220);
            panel3.TabIndex = 2;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(btnEditPaths, 0, 2);
            tableLayoutPanel5.Controls.Add(btnDeleteSelectedKeyword, 0, 1);
            tableLayoutPanel5.Controls.Add(btnAddKeyword, 0, 0);
            tableLayoutPanel5.Dock = DockStyle.Top;
            tableLayoutPanel5.Location = new Point(0, 0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel5.Size = new Size(213, 120);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // btnEditPaths
            // 
            btnEditPaths.BackColor = Color.Yellow;
            btnEditPaths.Dock = DockStyle.Fill;
            btnEditPaths.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnEditPaths.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnEditPaths.FlatStyle = FlatStyle.Flat;
            btnEditPaths.Image = Properties.Resources.folder__16x16;
            btnEditPaths.Location = new Point(3, 83);
            btnEditPaths.Name = "btnEditPaths";
            btnEditPaths.Size = new Size(207, 34);
            btnEditPaths.TabIndex = 11;
            btnEditPaths.Tag = "EditPathsOptionHelp";
            btnEditPaths.Text = "Edit Paths...";
            btnEditPaths.TextAlign = ContentAlignment.MiddleRight;
            btnEditPaths.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditPaths.UseVisualStyleBackColor = false;
            btnEditPaths.Click += BtnEditPaths_Click;
            btnEditPaths.MouseEnter += ControlMouseEnter;
            btnEditPaths.MouseLeave += ControlMouseLeave;
            // 
            // btnDeleteSelectedKeyword
            // 
            btnDeleteSelectedKeyword.BackColor = Color.Yellow;
            btnDeleteSelectedKeyword.Dock = DockStyle.Fill;
            btnDeleteSelectedKeyword.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnDeleteSelectedKeyword.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnDeleteSelectedKeyword.FlatStyle = FlatStyle.Flat;
            btnDeleteSelectedKeyword.Image = Properties.Resources.delete__16x16;
            btnDeleteSelectedKeyword.Location = new Point(3, 43);
            btnDeleteSelectedKeyword.Name = "btnDeleteSelectedKeyword";
            btnDeleteSelectedKeyword.Size = new Size(207, 34);
            btnDeleteSelectedKeyword.TabIndex = 10;
            btnDeleteSelectedKeyword.Tag = "DeleteSelectedKeywordHelp";
            btnDeleteSelectedKeyword.Text = "Delete Selected Keyword(s)";
            btnDeleteSelectedKeyword.TextAlign = ContentAlignment.MiddleRight;
            btnDeleteSelectedKeyword.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDeleteSelectedKeyword.UseVisualStyleBackColor = false;
            btnDeleteSelectedKeyword.Click += btnDeleteSelectedKeyword_Click;
            btnDeleteSelectedKeyword.MouseEnter += ControlMouseEnter;
            btnDeleteSelectedKeyword.MouseLeave += ControlMouseLeave;
            // 
            // btnAddKeyword
            // 
            btnAddKeyword.BackColor = Color.Yellow;
            btnAddKeyword.Dock = DockStyle.Fill;
            btnAddKeyword.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddKeyword.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddKeyword.FlatStyle = FlatStyle.Flat;
            btnAddKeyword.Image = Properties.Resources.add__16x16;
            btnAddKeyword.Location = new Point(3, 3);
            btnAddKeyword.Name = "btnAddKeyword";
            btnAddKeyword.Size = new Size(207, 34);
            btnAddKeyword.TabIndex = 9;
            btnAddKeyword.Tag = "NewKeywordHelp";
            btnAddKeyword.Text = "New Keyword";
            btnAddKeyword.TextAlign = ContentAlignment.MiddleRight;
            btnAddKeyword.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddKeyword.UseVisualStyleBackColor = false;
            btnAddKeyword.Click += btnAddKeyword_Click;
            btnAddKeyword.MouseEnter += ControlMouseEnter;
            btnAddKeyword.MouseLeave += ControlMouseLeave;
            // 
            // tabNexus
            // 
            tabNexus.Controls.Add(tableLayoutPanel4);
            tabNexus.Location = new Point(4, 24);
            tabNexus.Name = "tabNexus";
            tabNexus.Padding = new Padding(3);
            tabNexus.Size = new Size(785, 226);
            tabNexus.TabIndex = 2;
            tabNexus.Text = "Nexus API";
            tabNexus.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.10398F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.89602F));
            tableLayoutPanel4.Controls.Add(panel2, 0, 5);
            tableLayoutPanel4.Controls.Add(panel1, 1, 5);
            tableLayoutPanel4.Controls.Add(lblApiInputHelp, 1, 0);
            tableLayoutPanel4.Controls.Add(txtApiKey, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 6;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanel4.Size = new Size(779, 220);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnClearApiKey);
            panel2.Controls.Add(btnAuthenticate);
            panel2.Controls.Add(picApiStatus);
            panel2.Controls.Add(lblAuthenticatedStatus);
            panel2.Controls.Add(lblApiHelp);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 32);
            panel2.Name = "panel2";
            panel2.Size = new Size(470, 185);
            panel2.TabIndex = 3;
            // 
            // btnClearApiKey
            // 
            btnClearApiKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearApiKey.BackColor = Color.Yellow;
            btnClearApiKey.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnClearApiKey.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnClearApiKey.FlatStyle = FlatStyle.Flat;
            btnClearApiKey.Image = Properties.Resources.cancel_16x16;
            btnClearApiKey.Location = new Point(352, 5);
            btnClearApiKey.Name = "btnClearApiKey";
            btnClearApiKey.Size = new Size(115, 25);
            btnClearApiKey.TabIndex = 16;
            btnClearApiKey.Text = "Clear Key";
            btnClearApiKey.TextAlign = ContentAlignment.MiddleRight;
            btnClearApiKey.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClearApiKey.UseVisualStyleBackColor = false;
            btnClearApiKey.Click += BtnClearApiKey_Click;
            // 
            // btnAuthenticate
            // 
            btnAuthenticate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAuthenticate.BackColor = Color.Yellow;
            btnAuthenticate.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAuthenticate.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAuthenticate.FlatStyle = FlatStyle.Flat;
            btnAuthenticate.Image = Properties.Resources.auth_16x16;
            btnAuthenticate.Location = new Point(231, 5);
            btnAuthenticate.Name = "btnAuthenticate";
            btnAuthenticate.Size = new Size(115, 25);
            btnAuthenticate.TabIndex = 15;
            btnAuthenticate.Text = "Test Key";
            btnAuthenticate.TextAlign = ContentAlignment.MiddleRight;
            btnAuthenticate.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAuthenticate.UseVisualStyleBackColor = false;
            btnAuthenticate.Click += BtnAuthenticate_Click;
            // 
            // picApiStatus
            // 
            picApiStatus.Image = Properties.Resources.disabled__16x16;
            picApiStatus.Location = new Point(3, 3);
            picApiStatus.Name = "picApiStatus";
            picApiStatus.Size = new Size(16, 16);
            picApiStatus.TabIndex = 14;
            picApiStatus.TabStop = false;
            // 
            // lblAuthenticatedStatus
            // 
            lblAuthenticatedStatus.AutoSize = true;
            lblAuthenticatedStatus.Font = new Font("Segoe UI Variable Text", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAuthenticatedStatus.ForeColor = Color.Red;
            lblAuthenticatedStatus.Location = new Point(25, 3);
            lblAuthenticatedStatus.Name = "lblAuthenticatedStatus";
            lblAuthenticatedStatus.Size = new Size(122, 17);
            lblAuthenticatedStatus.TabIndex = 13;
            lblAuthenticatedStatus.Text = "Not Authenticated";
            // 
            // lblApiHelp
            // 
            lblApiHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblApiHelp.AutoSize = true;
            lblApiHelp.Location = new Point(25, 124);
            lblApiHelp.Name = "lblApiHelp";
            lblApiHelp.Size = new Size(416, 45);
            lblApiHelp.TabIndex = 12;
            lblApiHelp.Text = resources.GetString("lblApiHelp.Text");
            // 
            // panel1
            // 
            panel1.Controls.Add(lblApiKeyUnlocks);
            panel1.Controls.Add(lnkApiAcceptableUse);
            panel1.Controls.Add(lnkNexusApiKeyPage);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(479, 32);
            panel1.Name = "panel1";
            panel1.Size = new Size(297, 185);
            panel1.TabIndex = 3;
            // 
            // lblApiKeyUnlocks
            // 
            lblApiKeyUnlocks.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblApiKeyUnlocks.AutoSize = true;
            lblApiKeyUnlocks.Font = new Font("Segoe UI Variable Text", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblApiKeyUnlocks.Location = new Point(3, 86);
            lblApiKeyUnlocks.MaximumSize = new Size(200, 0);
            lblApiKeyUnlocks.Name = "lblApiKeyUnlocks";
            lblApiKeyUnlocks.Size = new Size(176, 48);
            lblApiKeyUnlocks.TabIndex = 15;
            lblApiKeyUnlocks.Text = "A valid API key will unlock the ability to download radio mods from within CRA.";
            lblApiKeyUnlocks.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lnkApiAcceptableUse
            // 
            lnkApiAcceptableUse.AutoSize = true;
            lnkApiAcceptableUse.Location = new Point(3, 25);
            lnkApiAcceptableUse.Name = "lnkApiAcceptableUse";
            lnkApiAcceptableUse.Size = new Size(188, 15);
            lnkApiAcceptableUse.TabIndex = 14;
            lnkApiAcceptableUse.TabStop = true;
            lnkApiAcceptableUse.Text = "API Acceptable Use Policy (Nexus)";
            lnkApiAcceptableUse.LinkClicked += LnkApiAcceptableUse_LinkClicked;
            // 
            // lnkNexusApiKeyPage
            // 
            lnkNexusApiKeyPage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lnkNexusApiKeyPage.AutoSize = true;
            lnkNexusApiKeyPage.Location = new Point(3, 45);
            lnkNexusApiKeyPage.Name = "lnkNexusApiKeyPage";
            lnkNexusApiKeyPage.Size = new Size(98, 15);
            lnkNexusApiKeyPage.TabIndex = 13;
            lnkNexusApiKeyPage.TabStop = true;
            lnkNexusApiKeyPage.Text = "Get Your API Key!";
            lnkNexusApiKeyPage.LinkClicked += LnkNexusApiKeyPage_LinkClicked;
            // 
            // lblApiInputHelp
            // 
            lblApiInputHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblApiInputHelp.AutoSize = true;
            lblApiInputHelp.Location = new Point(479, 7);
            lblApiInputHelp.Name = "lblApiInputHelp";
            lblApiInputHelp.Size = new Size(297, 15);
            lblApiInputHelp.TabIndex = 3;
            lblApiInputHelp.Text = "Specify your Nexus Mods API key.";
            // 
            // txtApiKey
            // 
            txtApiKey.Dock = DockStyle.Fill;
            txtApiKey.Location = new Point(3, 3);
            txtApiKey.Name = "txtApiKey";
            txtApiKey.Size = new Size(470, 23);
            txtApiKey.TabIndex = 11;
            txtApiKey.UseSystemPasswordChar = true;
            txtApiKey.TextChanged += TxtApiKey_TextChanged;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(btnReloadFromFile, 1, 0);
            tableLayoutPanel2.Controls.Add(btnSaveAndClose, 0, 0);
            tableLayoutPanel2.Controls.Add(btnCancel, 3, 0);
            tableLayoutPanel2.Controls.Add(btnResetToDefault, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Location = new Point(0, 254);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(793, 41);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // btnReloadFromFile
            // 
            btnReloadFromFile.BackColor = Color.Yellow;
            btnReloadFromFile.Dock = DockStyle.Fill;
            btnReloadFromFile.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnReloadFromFile.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnReloadFromFile.FlatStyle = FlatStyle.Flat;
            btnReloadFromFile.Image = Properties.Resources.file__16x16;
            btnReloadFromFile.Location = new Point(201, 3);
            btnReloadFromFile.Name = "btnReloadFromFile";
            btnReloadFromFile.Size = new Size(192, 35);
            btnReloadFromFile.TabIndex = 9;
            btnReloadFromFile.Text = "Reload From File";
            btnReloadFromFile.TextAlign = ContentAlignment.MiddleRight;
            btnReloadFromFile.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReloadFromFile.UseVisualStyleBackColor = false;
            btnReloadFromFile.Click += BtnReloadFromFile_Click;
            // 
            // btnSaveAndClose
            // 
            btnSaveAndClose.BackColor = Color.Yellow;
            btnSaveAndClose.Dock = DockStyle.Fill;
            btnSaveAndClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnSaveAndClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnSaveAndClose.FlatStyle = FlatStyle.Flat;
            btnSaveAndClose.Image = Properties.Resources.disk__16x16;
            btnSaveAndClose.Location = new Point(3, 3);
            btnSaveAndClose.Name = "btnSaveAndClose";
            btnSaveAndClose.Size = new Size(192, 35);
            btnSaveAndClose.TabIndex = 6;
            btnSaveAndClose.Text = "Save and Close";
            btnSaveAndClose.TextAlign = ContentAlignment.MiddleRight;
            btnSaveAndClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSaveAndClose.UseVisualStyleBackColor = false;
            btnSaveAndClose.Click += BtnSaveAndClose_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Yellow;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Image = Properties.Resources.cancel_16x16;
            btnCancel.Location = new Point(597, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(193, 35);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.TextAlign = ContentAlignment.MiddleRight;
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnResetToDefault
            // 
            btnResetToDefault.BackColor = Color.Yellow;
            btnResetToDefault.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnResetToDefault.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnResetToDefault.FlatStyle = FlatStyle.Flat;
            btnResetToDefault.Image = Properties.Resources.refresh__16x16;
            btnResetToDefault.Location = new Point(399, 3);
            btnResetToDefault.Name = "btnResetToDefault";
            btnResetToDefault.Size = new Size(192, 35);
            btnResetToDefault.TabIndex = 7;
            btnResetToDefault.Text = "Reset to Defaults";
            btnResetToDefault.TextAlign = ContentAlignment.MiddleRight;
            btnResetToDefault.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnResetToDefault.UseVisualStyleBackColor = false;
            btnResetToDefault.Click += BtnResetToDefault_Click;
            // 
            // fldrOpenLogPath
            // 
            fldrOpenLogPath.Description = "Select the location to store log files";
            fldrOpenLogPath.UseDescriptionForTitle = true;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblHelpText });
            statusStrip1.Location = new Point(0, 295);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(793, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblHelpText
            // 
            lblHelpText.Image = Properties.Resources.info__16x16;
            lblHelpText.Name = "lblHelpText";
            lblHelpText.Size = new Size(55, 17);
            lblHelpText.Text = "Ready";
            // 
            // fldrOpenDefaultMusicPath
            // 
            fldrOpenDefaultMusicPath.Description = "Select the default path for song files when importing stations";
            fldrOpenDefaultMusicPath.UseDescriptionForTitle = true;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(793, 317);
            Controls.Add(tabConfigs);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(statusStrip1);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Configuration";
            TopMost = true;
            FormClosing += ConfigForm_FormClosing;
            Load += ConfigForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tabConfigs.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tabLogging.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tabPathSetup.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tabNexus.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picApiStatus).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox chkCheckForUpdates;
        private CheckBox chkAutoExportToGame;
        private TabControl tabConfigs;
        private TabPage tabGeneral;
        private TabPage tabLogging;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnCancel;
        private Button btnResetToDefault;
        private Button btnSaveAndClose;
        private TableLayoutPanel tableLayoutPanel3;
        private CheckBox chkNewFileEveryLaunch;
        private Button btnEditLogsPath;
        private Label lblLogPathLabel;
        private Label lblCurrentLogPath;
        private FolderBrowserDialog fldrOpenLogPath;
        private TabPage tabNexus;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lblApiInputHelp;
        private MaskedTextBox txtApiKey;
        private Label lblApiHelp;
        private LinkLabel lnkNexusApiKeyPage;
        private Panel panel1;
        private LinkLabel lnkApiAcceptableUse;
        private Label lblApiKeyUnlocks;
        private Panel panel2;
        private Button btnAuthenticate;
        private PictureBox picApiStatus;
        private Label lblAuthenticatedStatus;
        private Button btnClearApiKey;
        private CheckBox chkWatchForChanges;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblHelpText;
        private ComboBox cmbCompressionLevels;
        private Label lblBackupCompressionLvl;
        private CheckBox chkCopySongFilesToBackup;
        private TableLayoutPanel tableLayoutPanel6;
        private Button btnEditDefaultSongLocation;
        private TableLayoutPanel tableLayoutPanel7;
        private Label lblDefaultSongLocation;
        private FolderBrowserDialog fldrOpenDefaultMusicPath;
        private TabPage tabPathSetup;
        private ListView lvForbiddenPaths;
        private Panel panel3;
        private TableLayoutPanel tableLayoutPanel5;
        private Button btnAddKeyword;
        private Button btnDeleteSelectedKeyword;
        private Button btnEditPaths;
        private SplitContainer splitContainer1;
        private Button btnReloadFromFile;
    }
}
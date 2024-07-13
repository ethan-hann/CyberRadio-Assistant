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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            lblEditPathsHelp = new Label();
            lblAutoExportHelp = new Label();
            chkCheckForUpdates = new CheckBox();
            chkAutoExportToGame = new CheckBox();
            lblUpdatesHelp = new Label();
            btnEditPaths = new Button();
            tabConfigs = new TabControl();
            tabGeneral = new TabPage();
            tabLogging = new TabPage();
            tableLayoutPanel3 = new TableLayoutPanel();
            chkNewFileEveryLaunch = new CheckBox();
            lblNewFileEveryLaunchHelp = new Label();
            btnEditLogsPath = new Button();
            lblEditLogPathHelp = new Label();
            lblLogPathLabel = new Label();
            lblCurrentLogPath = new Label();
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
            btnCancel = new Button();
            btnResetToDefault = new Button();
            btnSaveAndClose = new Button();
            fldrOpenLogPath = new FolderBrowserDialog();
            tableLayoutPanel1.SuspendLayout();
            tabConfigs.SuspendLayout();
            tabGeneral.SuspendLayout();
            tabLogging.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tabNexus.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picApiStatus).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.94142F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.05858F));
            tableLayoutPanel1.Controls.Add(lblEditPathsHelp, 1, 2);
            tableLayoutPanel1.Controls.Add(lblAutoExportHelp, 1, 1);
            tableLayoutPanel1.Controls.Add(chkCheckForUpdates, 0, 0);
            tableLayoutPanel1.Controls.Add(chkAutoExportToGame, 0, 1);
            tableLayoutPanel1.Controls.Add(lblUpdatesHelp, 1, 0);
            tableLayoutPanel1.Controls.Add(btnEditPaths, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 28.125F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.875F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(779, 150);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblEditPathsHelp
            // 
            lblEditPathsHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblEditPathsHelp.AutoSize = true;
            lblEditPathsHelp.Location = new Point(205, 74);
            lblEditPathsHelp.Name = "lblEditPathsHelp";
            lblEditPathsHelp.Size = new Size(571, 15);
            lblEditPathsHelp.TabIndex = 6;
            lblEditPathsHelp.Text = "Opens the path's dialog to edit the staging and game base path.";
            // 
            // lblAutoExportHelp
            // 
            lblAutoExportHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblAutoExportHelp.AutoSize = true;
            lblAutoExportHelp.Location = new Point(205, 40);
            lblAutoExportHelp.Name = "lblAutoExportHelp";
            lblAutoExportHelp.Size = new Size(571, 15);
            lblAutoExportHelp.TabIndex = 4;
            lblAutoExportHelp.Text = "If checked, your stations will immediately export to the game after successfully exporting to staging.";
            // 
            // chkCheckForUpdates
            // 
            chkCheckForUpdates.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkCheckForUpdates.AutoSize = true;
            chkCheckForUpdates.Location = new Point(10, 6);
            chkCheckForUpdates.Margin = new Padding(10, 3, 3, 3);
            chkCheckForUpdates.Name = "chkCheckForUpdates";
            chkCheckForUpdates.Size = new Size(189, 19);
            chkCheckForUpdates.TabIndex = 1;
            chkCheckForUpdates.Text = "Check for Updates at Startup?";
            chkCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // chkAutoExportToGame
            // 
            chkAutoExportToGame.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkAutoExportToGame.AutoSize = true;
            chkAutoExportToGame.Location = new Point(10, 38);
            chkAutoExportToGame.Margin = new Padding(10, 3, 3, 3);
            chkAutoExportToGame.Name = "chkAutoExportToGame";
            chkAutoExportToGame.Size = new Size(189, 19);
            chkAutoExportToGame.TabIndex = 2;
            chkAutoExportToGame.Text = "Auto-Export to Game?";
            chkAutoExportToGame.UseVisualStyleBackColor = true;
            // 
            // lblUpdatesHelp
            // 
            lblUpdatesHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblUpdatesHelp.AutoSize = true;
            lblUpdatesHelp.Location = new Point(205, 8);
            lblUpdatesHelp.Name = "lblUpdatesHelp";
            lblUpdatesHelp.Size = new Size(571, 15);
            lblUpdatesHelp.TabIndex = 3;
            lblUpdatesHelp.Text = "If checked, enables the application to check for updates immediately after started.";
            // 
            // btnEditPaths
            // 
            btnEditPaths.BackColor = Color.Yellow;
            btnEditPaths.Dock = DockStyle.Fill;
            btnEditPaths.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnEditPaths.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnEditPaths.FlatStyle = FlatStyle.Flat;
            btnEditPaths.Image = Properties.Resources.folder__16x16;
            btnEditPaths.Location = new Point(3, 67);
            btnEditPaths.Name = "btnEditPaths";
            btnEditPaths.Size = new Size(196, 30);
            btnEditPaths.TabIndex = 5;
            btnEditPaths.Text = "Edit Paths...";
            btnEditPaths.TextAlign = ContentAlignment.MiddleRight;
            btnEditPaths.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditPaths.UseVisualStyleBackColor = false;
            btnEditPaths.Click += BtnEditPaths_Click;
            // 
            // tabConfigs
            // 
            tabConfigs.Controls.Add(tabGeneral);
            tabConfigs.Controls.Add(tabLogging);
            tabConfigs.Controls.Add(tabNexus);
            tabConfigs.Dock = DockStyle.Fill;
            tabConfigs.Location = new Point(0, 0);
            tabConfigs.Name = "tabConfigs";
            tabConfigs.SelectedIndex = 0;
            tabConfigs.Size = new Size(793, 184);
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
            tabGeneral.Size = new Size(785, 156);
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
            tabLogging.Size = new Size(785, 156);
            tabLogging.TabIndex = 1;
            tabLogging.Text = "Logging";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.94142F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.05858F));
            tableLayoutPanel3.Controls.Add(chkNewFileEveryLaunch, 0, 0);
            tableLayoutPanel3.Controls.Add(lblNewFileEveryLaunchHelp, 1, 0);
            tableLayoutPanel3.Controls.Add(btnEditLogsPath, 0, 5);
            tableLayoutPanel3.Controls.Add(lblEditLogPathHelp, 1, 5);
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
            tableLayoutPanel3.Size = new Size(779, 150);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // chkNewFileEveryLaunch
            // 
            chkNewFileEveryLaunch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkNewFileEveryLaunch.AutoSize = true;
            chkNewFileEveryLaunch.Location = new Point(10, 3);
            chkNewFileEveryLaunch.Margin = new Padding(10, 3, 3, 3);
            chkNewFileEveryLaunch.Name = "chkNewFileEveryLaunch";
            chkNewFileEveryLaunch.Size = new Size(189, 19);
            chkNewFileEveryLaunch.TabIndex = 1;
            chkNewFileEveryLaunch.Text = "New File Every Launch?";
            chkNewFileEveryLaunch.UseVisualStyleBackColor = true;
            // 
            // lblNewFileEveryLaunchHelp
            // 
            lblNewFileEveryLaunchHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblNewFileEveryLaunchHelp.AutoSize = true;
            lblNewFileEveryLaunchHelp.Location = new Point(205, 5);
            lblNewFileEveryLaunchHelp.Name = "lblNewFileEveryLaunchHelp";
            lblNewFileEveryLaunchHelp.Size = new Size(571, 15);
            lblNewFileEveryLaunchHelp.TabIndex = 3;
            lblNewFileEveryLaunchHelp.Text = "If checked, specifies that a new log file will be created on every run of the app.";
            // 
            // btnEditLogsPath
            // 
            btnEditLogsPath.BackColor = Color.Yellow;
            btnEditLogsPath.Dock = DockStyle.Fill;
            btnEditLogsPath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnEditLogsPath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnEditLogsPath.FlatStyle = FlatStyle.Flat;
            btnEditLogsPath.Image = Properties.Resources.folder__16x16;
            btnEditLogsPath.Location = new Point(3, 28);
            btnEditLogsPath.Name = "btnEditLogsPath";
            btnEditLogsPath.Size = new Size(196, 31);
            btnEditLogsPath.TabIndex = 7;
            btnEditLogsPath.Text = "Edit Logs Path...";
            btnEditLogsPath.TextAlign = ContentAlignment.MiddleRight;
            btnEditLogsPath.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditLogsPath.UseVisualStyleBackColor = false;
            btnEditLogsPath.Click += BtnEditLogsPath_Click;
            // 
            // lblEditLogPathHelp
            // 
            lblEditLogPathHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblEditLogPathHelp.AutoSize = true;
            lblEditLogPathHelp.Location = new Point(205, 36);
            lblEditLogPathHelp.Name = "lblEditLogPathHelp";
            lblEditLogPathHelp.Size = new Size(571, 15);
            lblEditLogPathHelp.TabIndex = 8;
            lblEditLogPathHelp.Text = "Opens a folder dialog where you can choose a new path to save log files.";
            // 
            // lblLogPathLabel
            // 
            lblLogPathLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblLogPathLabel.AutoSize = true;
            lblLogPathLabel.Location = new Point(3, 64);
            lblLogPathLabel.Name = "lblLogPathLabel";
            lblLogPathLabel.Size = new Size(196, 15);
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
            lblCurrentLogPath.Location = new Point(205, 63);
            lblCurrentLogPath.Name = "lblCurrentLogPath";
            lblCurrentLogPath.Size = new Size(571, 17);
            lblCurrentLogPath.TabIndex = 10;
            lblCurrentLogPath.Text = "<no path>";
            // 
            // tabNexus
            // 
            tabNexus.Controls.Add(tableLayoutPanel4);
            tabNexus.Location = new Point(4, 24);
            tabNexus.Name = "tabNexus";
            tabNexus.Padding = new Padding(3);
            tabNexus.Size = new Size(785, 156);
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
            tableLayoutPanel4.Size = new Size(779, 150);
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
            panel2.Size = new Size(470, 115);
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
            lblApiHelp.Location = new Point(25, 54);
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
            panel1.Size = new Size(297, 115);
            panel1.TabIndex = 3;
            // 
            // lblApiKeyUnlocks
            // 
            lblApiKeyUnlocks.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblApiKeyUnlocks.AutoSize = true;
            lblApiKeyUnlocks.Font = new Font("Segoe UI Variable Text", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblApiKeyUnlocks.Location = new Point(3, 51);
            lblApiKeyUnlocks.MaximumSize = new Size(200, 0);
            lblApiKeyUnlocks.Name = "lblApiKeyUnlocks";
            lblApiKeyUnlocks.Size = new Size(176, 48);
            lblApiKeyUnlocks.TabIndex = 15;
            lblApiKeyUnlocks.Text = "A valid API key will unlock the ability to download radio mods directly from CRA.";
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
            lnkNexusApiKeyPage.Location = new Point(3, 10);
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
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56.6153831F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.3846169F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 142F));
            tableLayoutPanel2.Controls.Add(btnCancel, 0, 0);
            tableLayoutPanel2.Controls.Add(btnResetToDefault, 0, 0);
            tableLayoutPanel2.Controls.Add(btnSaveAndClose, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Location = new Point(0, 184);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(793, 41);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Yellow;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Image = Properties.Resources.cancel_16x16;
            btnCancel.Location = new Point(653, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(137, 35);
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
            btnResetToDefault.Dock = DockStyle.Fill;
            btnResetToDefault.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnResetToDefault.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnResetToDefault.FlatStyle = FlatStyle.Flat;
            btnResetToDefault.Image = Properties.Resources.refresh__16x16;
            btnResetToDefault.Location = new Point(371, 3);
            btnResetToDefault.Name = "btnResetToDefault";
            btnResetToDefault.Size = new Size(276, 35);
            btnResetToDefault.TabIndex = 7;
            btnResetToDefault.Text = "Reset to Defaults";
            btnResetToDefault.TextAlign = ContentAlignment.MiddleRight;
            btnResetToDefault.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnResetToDefault.UseVisualStyleBackColor = false;
            btnResetToDefault.Click += BtnResetToDefault_Click;
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
            btnSaveAndClose.Size = new Size(362, 35);
            btnSaveAndClose.TabIndex = 6;
            btnSaveAndClose.Text = "Save and Close";
            btnSaveAndClose.TextAlign = ContentAlignment.MiddleRight;
            btnSaveAndClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSaveAndClose.UseVisualStyleBackColor = false;
            btnSaveAndClose.Click += BtnSaveAndClose_Click;
            // 
            // fldrOpenLogPath
            // 
            fldrOpenLogPath.Description = "Select the location to store log files";
            fldrOpenLogPath.UseDescriptionForTitle = true;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(793, 225);
            Controls.Add(tabConfigs);
            Controls.Add(tableLayoutPanel2);
            FormBorderStyle = FormBorderStyle.FixedDialog;
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
            tabConfigs.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tabLogging.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tabNexus.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picApiStatus).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox chkCheckForUpdates;
        private CheckBox chkAutoExportToGame;
        private Label lblUpdatesHelp;
        private Label lblEditPathsHelp;
        private Label lblAutoExportHelp;
        private Button btnEditPaths;
        private TabControl tabConfigs;
        private TabPage tabGeneral;
        private TabPage tabLogging;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnCancel;
        private Button btnResetToDefault;
        private Button btnSaveAndClose;
        private TableLayoutPanel tableLayoutPanel3;
        private CheckBox chkNewFileEveryLaunch;
        private Label lblNewFileEveryLaunchHelp;
        private Button btnEditLogsPath;
        private Label lblEditLogPathHelp;
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
    }
}
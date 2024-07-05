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
            components = new System.ComponentModel.Container();
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
            tabImages = new ImageList(components);
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
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(779, 150);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblEditPathsHelp
            // 
            lblEditPathsHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblEditPathsHelp.AutoSize = true;
            lblEditPathsHelp.Location = new Point(205, 57);
            lblEditPathsHelp.Name = "lblEditPathsHelp";
            lblEditPathsHelp.Size = new Size(571, 15);
            lblEditPathsHelp.TabIndex = 6;
            lblEditPathsHelp.Text = "Opens the path's dialog to edit the staging and game base path.";
            // 
            // lblAutoExportHelp
            // 
            lblAutoExportHelp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblAutoExportHelp.AutoSize = true;
            lblAutoExportHelp.Location = new Point(205, 30);
            lblAutoExportHelp.Name = "lblAutoExportHelp";
            lblAutoExportHelp.Size = new Size(571, 15);
            lblAutoExportHelp.TabIndex = 4;
            lblAutoExportHelp.Text = "If checked, your stations will immediately export to the game after successfully exporting to staging.";
            // 
            // chkCheckForUpdates
            // 
            chkCheckForUpdates.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkCheckForUpdates.AutoSize = true;
            chkCheckForUpdates.Location = new Point(10, 3);
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
            chkAutoExportToGame.Location = new Point(10, 28);
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
            lblUpdatesHelp.Location = new Point(205, 5);
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
            btnEditPaths.Location = new Point(3, 53);
            btnEditPaths.Name = "btnEditPaths";
            btnEditPaths.Size = new Size(196, 23);
            btnEditPaths.TabIndex = 5;
            btnEditPaths.Text = "Edit Paths...";
            btnEditPaths.UseVisualStyleBackColor = false;
            btnEditPaths.Click += btnEditPaths_Click;
            // 
            // tabConfigs
            // 
            tabConfigs.Controls.Add(tabGeneral);
            tabConfigs.Controls.Add(tabLogging);
            tabConfigs.Dock = DockStyle.Fill;
            tabConfigs.ImageList = tabImages;
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
            btnEditLogsPath.Location = new Point(3, 28);
            btnEditLogsPath.Name = "btnEditLogsPath";
            btnEditLogsPath.Size = new Size(196, 31);
            btnEditLogsPath.TabIndex = 7;
            btnEditLogsPath.Text = "Edit Logs Path...";
            btnEditLogsPath.UseVisualStyleBackColor = false;
            btnEditLogsPath.Click += btnEditLogsPath_Click;
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
            lblCurrentLogPath.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblCurrentLogPath.ForeColor = Color.Green;
            lblCurrentLogPath.Location = new Point(205, 64);
            lblCurrentLogPath.Name = "lblCurrentLogPath";
            lblCurrentLogPath.Size = new Size(571, 15);
            lblCurrentLogPath.TabIndex = 10;
            lblCurrentLogPath.Text = "<no path>";
            // 
            // tabImages
            // 
            tabImages.ColorDepth = ColorDepth.Depth32Bit;
            tabImages.ImageStream = (ImageListStreamer)resources.GetObject("tabImages.ImageStream");
            tabImages.TransparentColor = Color.Transparent;
            tabImages.Images.SetKeyName(0, "config");
            tabImages.Images.SetKeyName(1, "logging");
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
            btnCancel.Location = new Point(653, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(137, 35);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnResetToDefault
            // 
            btnResetToDefault.BackColor = Color.Yellow;
            btnResetToDefault.Dock = DockStyle.Fill;
            btnResetToDefault.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnResetToDefault.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnResetToDefault.FlatStyle = FlatStyle.Flat;
            btnResetToDefault.Location = new Point(371, 3);
            btnResetToDefault.Name = "btnResetToDefault";
            btnResetToDefault.Size = new Size(276, 35);
            btnResetToDefault.TabIndex = 7;
            btnResetToDefault.Text = "Reset to Defaults";
            btnResetToDefault.UseVisualStyleBackColor = false;
            btnResetToDefault.Click += btnResetToDefault_Click;
            // 
            // btnSaveAndClose
            // 
            btnSaveAndClose.BackColor = Color.Yellow;
            btnSaveAndClose.Dock = DockStyle.Fill;
            btnSaveAndClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnSaveAndClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnSaveAndClose.FlatStyle = FlatStyle.Flat;
            btnSaveAndClose.Location = new Point(3, 3);
            btnSaveAndClose.Name = "btnSaveAndClose";
            btnSaveAndClose.Size = new Size(362, 35);
            btnSaveAndClose.TabIndex = 6;
            btnSaveAndClose.Text = "Save and Close";
            btnSaveAndClose.UseVisualStyleBackColor = false;
            btnSaveAndClose.Click += btnSaveAndClose_Click;
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
            Load += ConfigForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabConfigs.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tabLogging.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
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
        private ImageList tabImages;
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
    }
}
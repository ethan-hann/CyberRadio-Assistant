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
            pathsToolStripMenuItem = new ToolStripMenuItem();
            refreshStationsToolStripMenuItem = new ToolStripMenuItem();
            languageToolStripMenuItem = new ToolStripMenuItem();
            cmbLanguageSelect = new ToolStripComboBox();
            helpToolStripMenuItem = new ToolStripMenuItem();
            howToUseToolStripMenuItem = new ToolStripMenuItem();
            radioExtOnNexusModsToolStripMenuItem = new ToolStripMenuItem();
            radioExtGitHubToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            fdlgOpenGameExe = new OpenFileDialog();
            splitContainer1 = new SplitContainer();
            lbStations = new ListBox();
            metaDataBindingSource = new BindingSource(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            btnDeleteStation = new Button();
            btnAddStation = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)metaDataBindingSource).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Transparent;
            menuStrip1.Font = new Font("CF Notche Demo", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, languageToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1053, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pathsToolStripMenuItem, refreshStationsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // pathsToolStripMenuItem
            // 
            pathsToolStripMenuItem.Image = Properties.Resources.folder;
            pathsToolStripMenuItem.Name = "pathsToolStripMenuItem";
            pathsToolStripMenuItem.Size = new Size(164, 22);
            pathsToolStripMenuItem.Text = "Game Paths";
            pathsToolStripMenuItem.Click += pathsToolStripMenuItem_Click;
            // 
            // refreshStationsToolStripMenuItem
            // 
            refreshStationsToolStripMenuItem.Image = Properties.Resources.refresh;
            refreshStationsToolStripMenuItem.Name = "refreshStationsToolStripMenuItem";
            refreshStationsToolStripMenuItem.Size = new Size(164, 22);
            refreshStationsToolStripMenuItem.Text = "Refresh Stations";
            refreshStationsToolStripMenuItem.Click += refreshStationsToolStripMenuItem_Click;
            // 
            // languageToolStripMenuItem
            // 
            languageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cmbLanguageSelect });
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            languageToolStripMenuItem.Size = new Size(73, 20);
            languageToolStripMenuItem.Text = "Language";
            // 
            // cmbLanguageSelect
            // 
            cmbLanguageSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguageSelect.FlatStyle = FlatStyle.System;
            cmbLanguageSelect.Items.AddRange(new object[] { "English (en)", "Español (es)", "Français (fr)" });
            cmbLanguageSelect.Name = "cmbLanguageSelect";
            cmbLanguageSelect.Size = new Size(121, 23);
            cmbLanguageSelect.Sorted = true;
            cmbLanguageSelect.SelectedIndexChanged += cmbLanguageSelect_SelectedIndexChanged;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { howToUseToolStripMenuItem, radioExtOnNexusModsToolStripMenuItem, radioExtGitHubToolStripMenuItem, toolStripSeparator1, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(42, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // howToUseToolStripMenuItem
            // 
            howToUseToolStripMenuItem.Image = Properties.Resources.guide;
            howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
            howToUseToolStripMenuItem.Size = new Size(202, 22);
            howToUseToolStripMenuItem.Text = "How To Use";
            // 
            // radioExtOnNexusModsToolStripMenuItem
            // 
            radioExtOnNexusModsToolStripMenuItem.Name = "radioExtOnNexusModsToolStripMenuItem";
            radioExtOnNexusModsToolStripMenuItem.Size = new Size(202, 22);
            radioExtOnNexusModsToolStripMenuItem.Text = "radioExt on NexusMods";
            radioExtOnNexusModsToolStripMenuItem.Click += radioExtOnNexusModsToolStripMenuItem_Click;
            // 
            // radioExtGitHubToolStripMenuItem
            // 
            radioExtGitHubToolStripMenuItem.Name = "radioExtGitHubToolStripMenuItem";
            radioExtGitHubToolStripMenuItem.Size = new Size(202, 22);
            radioExtGitHubToolStripMenuItem.Text = "radioExt GitHub";
            radioExtGitHubToolStripMenuItem.Click += radioExtHelpToolStripMenuItem_Click;
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
            aboutToolStripMenuItem.Size = new Size(202, 22);
            aboutToolStripMenuItem.Text = "About";
            // 
            // fdlgOpenGameExe
            // 
            fdlgOpenGameExe.Filter = "Game Executable|Cyberpunk2077.exe";
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
            splitContainer1.Panel1.Controls.Add(lbStations);
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.Transparent;
            splitContainer1.Size = new Size(1053, 592);
            splitContainer1.SplitterDistance = 251;
            splitContainer1.TabIndex = 1;
            // 
            // lbStations
            // 
            lbStations.DataSource = metaDataBindingSource;
            lbStations.DisplayMember = "DisplayName";
            lbStations.Dock = DockStyle.Fill;
            lbStations.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbStations.FormattingEnabled = true;
            lbStations.ItemHeight = 17;
            lbStations.Location = new Point(0, 0);
            lbStations.Margin = new Padding(3, 2, 3, 2);
            lbStations.Name = "lbStations";
            lbStations.Size = new Size(251, 555);
            lbStations.TabIndex = 0;
            lbStations.SelectedIndexChanged += lbStations_SelectedIndexChanged;
            // 
            // metaDataBindingSource
            // 
            metaDataBindingSource.DataSource = typeof(models.MetaData);
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnDeleteStation, 1, 0);
            tableLayoutPanel1.Controls.Add(btnAddStation, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 555);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(251, 37);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnDeleteStation
            // 
            btnDeleteStation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDeleteStation.Location = new Point(128, 2);
            btnDeleteStation.Margin = new Padding(3, 2, 3, 2);
            btnDeleteStation.Name = "btnDeleteStation";
            btnDeleteStation.Size = new Size(120, 33);
            btnDeleteStation.TabIndex = 1;
            btnDeleteStation.Text = "Delete Station";
            btnDeleteStation.UseVisualStyleBackColor = true;
            btnDeleteStation.Click += btnDeleteStation_Click;
            // 
            // btnAddStation
            // 
            btnAddStation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnAddStation.Location = new Point(3, 2);
            btnAddStation.Margin = new Padding(3, 2, 3, 2);
            btnAddStation.Name = "btnAddStation";
            btnAddStation.Size = new Size(119, 33);
            btnAddStation.TabIndex = 0;
            btnAddStation.Text = "New Station";
            btnAddStation.UseVisualStyleBackColor = true;
            btnAddStation.Click += btnAddStation_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1053, 616);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            Font = new Font("CF Notche Demo", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cyber Radio Assistant";
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)metaDataBindingSource).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem pathsToolStripMenuItem;
        private OpenFileDialog fdlgOpenGameExe;
        private ToolStripMenuItem howToUseToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private ListBox lbStations;
        private Button btnDeleteStation;
        private Button btnAddStation;
        private ToolStripMenuItem refreshStationsToolStripMenuItem;
        private BindingSource metaDataBindingSource;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripComboBox cmbLanguageSelect;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem radioExtOnNexusModsToolStripMenuItem;
        private ToolStripMenuItem radioExtGitHubToolStripMenuItem;
    }
}

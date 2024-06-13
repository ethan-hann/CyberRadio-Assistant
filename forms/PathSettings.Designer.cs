namespace RadioExt_Helper.forms
{
    partial class PathSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathSettings));
            tableLayoutPanel1 = new TableLayoutPanel();
            label4 = new Label();
            lblRadioPath = new Label();
            label2 = new Label();
            lblGameBasePath = new Label();
            label1 = new Label();
            btnChangeGameBasePath = new Button();
            btnChangeBackUpPath = new Button();
            btnClearBackupPath = new Button();
            lblBackupPath = new Label();
            fdlgBackupPath = new FolderBrowserDialog();
            fdlgOpenGameExe = new OpenFileDialog();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.9863014F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 83.0137F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.Controls.Add(label4, 0, 2);
            tableLayoutPanel1.Controls.Add(lblRadioPath, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(lblGameBasePath, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(btnChangeGameBasePath, 2, 0);
            tableLayoutPanel1.Controls.Add(btnChangeBackUpPath, 2, 2);
            tableLayoutPanel1.Controls.Add(btnClearBackupPath, 2, 2);
            tableLayoutPanel1.Controls.Add(lblBackupPath, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Size = new Size(947, 131);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(33, 100);
            label4.Name = "label4";
            label4.Size = new Size(91, 17);
            label4.TabIndex = 6;
            label4.Text = "Back Up Path: ";
            // 
            // lblRadioPath
            // 
            lblRadioPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblRadioPath.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(lblRadioPath, 3);
            lblRadioPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lblRadioPath.ForeColor = Color.Green;
            lblRadioPath.Location = new Point(130, 56);
            lblRadioPath.Name = "lblRadioPath";
            lblRadioPath.Size = new Size(814, 17);
            lblRadioPath.TabIndex = 4;
            lblRadioPath.Text = "<no path set; radioExt is not installed>";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(6, 56);
            label2.Name = "label2";
            label2.Size = new Size(118, 17);
            label2.TabIndex = 3;
            label2.Text = "Radio Station Path: ";
            // 
            // lblGameBasePath
            // 
            lblGameBasePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblGameBasePath.AutoSize = true;
            lblGameBasePath.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lblGameBasePath.ForeColor = Color.Green;
            lblGameBasePath.Location = new Point(130, 13);
            lblGameBasePath.Name = "lblGameBasePath";
            lblGameBasePath.Size = new Size(618, 17);
            lblGameBasePath.TabIndex = 2;
            lblGameBasePath.Text = "<no path set; game executable missing>";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(15, 13);
            label1.Name = "label1";
            label1.Size = new Size(109, 17);
            label1.TabIndex = 0;
            label1.Text = "Game Base Path: ";
            // 
            // btnChangeGameBasePath
            // 
            btnChangeGameBasePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(btnChangeGameBasePath, 2);
            btnChangeGameBasePath.Location = new Point(754, 9);
            btnChangeGameBasePath.Name = "btnChangeGameBasePath";
            btnChangeGameBasePath.Size = new Size(190, 25);
            btnChangeGameBasePath.TabIndex = 1;
            btnChangeGameBasePath.Text = "Change...";
            btnChangeGameBasePath.UseVisualStyleBackColor = true;
            btnChangeGameBasePath.Click += btnChangeGameBasePath_Click;
            // 
            // btnChangeBackUpPath
            // 
            btnChangeBackUpPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnChangeBackUpPath.Location = new Point(754, 95);
            btnChangeBackUpPath.Name = "btnChangeBackUpPath";
            btnChangeBackUpPath.Size = new Size(114, 26);
            btnChangeBackUpPath.TabIndex = 9;
            btnChangeBackUpPath.Text = "Change...";
            btnChangeBackUpPath.UseVisualStyleBackColor = true;
            btnChangeBackUpPath.Click += btnChangeBackUpPath_Click;
            // 
            // btnClearBackupPath
            // 
            btnClearBackupPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnClearBackupPath.Location = new Point(874, 95);
            btnClearBackupPath.Name = "btnClearBackupPath";
            btnClearBackupPath.Size = new Size(70, 26);
            btnClearBackupPath.TabIndex = 7;
            btnClearBackupPath.Text = "Clear";
            btnClearBackupPath.UseVisualStyleBackColor = true;
            btnClearBackupPath.Click += btnClearBackupPath_Click;
            // 
            // lblBackupPath
            // 
            lblBackupPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblBackupPath.AutoSize = true;
            lblBackupPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lblBackupPath.ForeColor = Color.Green;
            lblBackupPath.Location = new Point(130, 100);
            lblBackupPath.Name = "lblBackupPath";
            lblBackupPath.Size = new Size(618, 17);
            lblBackupPath.TabIndex = 8;
            lblBackupPath.Text = "<no path set; radio stations will NOT be backed up>";
            // 
            // fdlgBackupPath
            // 
            fdlgBackupPath.Description = "Select the path to backup custom radio stations to.";
            fdlgBackupPath.UseDescriptionForTitle = true;
            // 
            // fdlgOpenGameExe
            // 
            fdlgOpenGameExe.Filter = "Game Executable|Cyberpunk2077.exe";
            // 
            // PathSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(947, 126);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PathSettings";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Game Paths";
            TopMost = true;
            Load += PathSettings_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Button btnChangeGameBasePath;
        private Label lblGameBasePath;
        private Label label4;
        private Label lblRadioPath;
        private Label label2;
        private Button btnClearBackupPath;
        private Label lblBackupPath;
        private FolderBrowserDialog fdlgBackupPath;
        private OpenFileDialog fdlgOpenGameExe;
        private Button btnChangeBackUpPath;
    }
}
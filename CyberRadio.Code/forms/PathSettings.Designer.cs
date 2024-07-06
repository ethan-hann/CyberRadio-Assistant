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
            label4 = new Label();
            lblRadioPath = new Label();
            label2 = new Label();
            lblGameBasePath = new Label();
            label1 = new Label();
            btnChangeGameBasePath = new Button();
            btnChangeBackUpPath = new Button();
            lblBackupPath = new Label();
            splitContainer1 = new SplitContainer();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(25, 108);
            label4.Name = "label4";
            label4.Size = new Size(84, 17);
            label4.TabIndex = 6;
            label4.Text = "Staging Path:";
            // 
            // lblRadioPath
            // 
            lblRadioPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblRadioPath.AutoSize = true;
            lblRadioPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lblRadioPath.ForeColor = Color.Green;
            lblRadioPath.Location = new Point(115, 61);
            lblRadioPath.Name = "lblRadioPath";
            lblRadioPath.Size = new Size(444, 17);
            lblRadioPath.TabIndex = 4;
            lblRadioPath.Text = "<no path set; radioExt is not installed>";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(19, 52);
            label2.Name = "label2";
            label2.Size = new Size(90, 34);
            label2.TabIndex = 3;
            label2.Text = "Radio Station Path: ";
            // 
            // lblGameBasePath
            // 
            lblGameBasePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblGameBasePath.AutoSize = true;
            lblGameBasePath.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lblGameBasePath.ForeColor = Color.Green;
            lblGameBasePath.Location = new Point(115, 14);
            lblGameBasePath.Name = "lblGameBasePath";
            lblGameBasePath.Size = new Size(444, 17);
            lblGameBasePath.TabIndex = 2;
            lblGameBasePath.Text = "<no path set; game executable missing>";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(4, 14);
            label1.Name = "label1";
            label1.Size = new Size(105, 17);
            label1.TabIndex = 0;
            label1.Text = "Game Base Path: ";
            // 
            // btnChangeGameBasePath
            // 
            btnChangeGameBasePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnChangeGameBasePath.BackColor = Color.Yellow;
            tableLayoutPanel3.SetColumnSpan(btnChangeGameBasePath, 2);
            btnChangeGameBasePath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnChangeGameBasePath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnChangeGameBasePath.FlatStyle = FlatStyle.Flat;
            btnChangeGameBasePath.Font = new Font("Segoe UI Variable Display Semib", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChangeGameBasePath.Image = Properties.Resources.change__16x16;
            btnChangeGameBasePath.Location = new Point(3, 8);
            btnChangeGameBasePath.Name = "btnChangeGameBasePath";
            btnChangeGameBasePath.Size = new Size(172, 30);
            btnChangeGameBasePath.TabIndex = 1;
            btnChangeGameBasePath.Text = "Change...";
            btnChangeGameBasePath.TextAlign = ContentAlignment.MiddleRight;
            btnChangeGameBasePath.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnChangeGameBasePath.UseVisualStyleBackColor = false;
            btnChangeGameBasePath.Click += btnChangeGameBasePath_Click;
            // 
            // btnChangeBackUpPath
            // 
            btnChangeBackUpPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnChangeBackUpPath.BackColor = Color.Yellow;
            tableLayoutPanel3.SetColumnSpan(btnChangeBackUpPath, 2);
            btnChangeBackUpPath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnChangeBackUpPath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnChangeBackUpPath.FlatStyle = FlatStyle.Flat;
            btnChangeBackUpPath.Font = new Font("Segoe UI Variable Display Semib", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChangeBackUpPath.Image = Properties.Resources.change__16x16;
            btnChangeBackUpPath.Location = new Point(3, 102);
            btnChangeBackUpPath.Name = "btnChangeBackUpPath";
            btnChangeBackUpPath.Size = new Size(172, 30);
            btnChangeBackUpPath.TabIndex = 9;
            btnChangeBackUpPath.Text = "Change...";
            btnChangeBackUpPath.TextAlign = ContentAlignment.MiddleRight;
            btnChangeBackUpPath.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnChangeBackUpPath.UseVisualStyleBackColor = false;
            btnChangeBackUpPath.Click += btnChangeBackUpPath_Click;
            // 
            // lblBackupPath
            // 
            lblBackupPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblBackupPath.AutoSize = true;
            lblBackupPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lblBackupPath.ForeColor = Color.Green;
            lblBackupPath.Location = new Point(115, 108);
            lblBackupPath.Name = "lblBackupPath";
            lblBackupPath.Size = new Size(444, 17);
            lblBackupPath.TabIndex = 8;
            lblBackupPath.Text = "<no path set; radio stations need to be staged first>";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel3);
            splitContainer1.Size = new Size(744, 141);
            splitContainer1.SplitterDistance = 562;
            splitContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.Transparent;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel2.Controls.Add(lblBackupPath, 1, 2);
            tableLayoutPanel2.Controls.Add(lblRadioPath, 1, 1);
            tableLayoutPanel2.Controls.Add(label4, 0, 2);
            tableLayoutPanel2.Controls.Add(lblGameBasePath, 1, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(label2, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.Size = new Size(562, 141);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(btnChangeGameBasePath, 0, 0);
            tableLayoutPanel3.Controls.Add(btnChangeBackUpPath, 0, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.Size = new Size(178, 141);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // PathSettings
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(744, 141);
            Controls.Add(splitContainer1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1920, 180);
            MinimizeBox = false;
            MinimumSize = new Size(0, 180);
            Name = "PathSettings";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Game Paths";
            TopMost = true;
            FormClosed += PathSettings_FormClosed;
            Load += PathSettings_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Button btnChangeGameBasePath;
        private Label lblGameBasePath;
        private Label label4;
        private Label lblRadioPath;
        private Label label2;
        private Label lblBackupPath;
        private Button btnChangeBackUpPath;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
    }
}
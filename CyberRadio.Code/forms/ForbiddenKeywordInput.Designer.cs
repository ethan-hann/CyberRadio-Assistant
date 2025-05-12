namespace RadioExt_Helper.forms
{
    partial class ForbiddenKeywordInput
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
            tableLayoutPanel1 = new TableLayoutPanel();
            lblGroup = new Label();
            lblKeyword = new Label();
            txtKeyword = new TextBox();
            cmbGroups = new ComboBox();
            chkIsForbidden = new CheckBox();
            btnAddAndClose = new Button();
            btnAddAndNew = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.73077F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.26923F));
            tableLayoutPanel1.Controls.Add(lblGroup, 0, 1);
            tableLayoutPanel1.Controls.Add(lblKeyword, 0, 0);
            tableLayoutPanel1.Controls.Add(txtKeyword, 1, 0);
            tableLayoutPanel1.Controls.Add(cmbGroups, 1, 1);
            tableLayoutPanel1.Controls.Add(chkIsForbidden, 1, 2);
            tableLayoutPanel1.Controls.Add(btnAddAndClose, 1, 3);
            tableLayoutPanel1.Controls.Add(btnAddAndNew, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 48.57143F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 51.42857F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.Size = new Size(416, 122);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblGroup
            // 
            lblGroup.Anchor = AnchorStyles.Right;
            lblGroup.AutoSize = true;
            lblGroup.Location = new Point(86, 36);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(43, 15);
            lblGroup.TabIndex = 10;
            lblGroup.Text = "Group:";
            // 
            // lblKeyword
            // 
            lblKeyword.Anchor = AnchorStyles.Right;
            lblKeyword.AutoSize = true;
            lblKeyword.Location = new Point(73, 7);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(56, 15);
            lblKeyword.TabIndex = 8;
            lblKeyword.Text = "Keyword:";
            // 
            // txtKeyword
            // 
            txtKeyword.Dock = DockStyle.Fill;
            txtKeyword.Location = new Point(135, 3);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(278, 23);
            txtKeyword.TabIndex = 0;
            // 
            // cmbGroups
            // 
            cmbGroups.Dock = DockStyle.Fill;
            cmbGroups.FormattingEnabled = true;
            cmbGroups.Location = new Point(135, 32);
            cmbGroups.Name = "cmbGroups";
            cmbGroups.Size = new Size(278, 23);
            cmbGroups.TabIndex = 1;
            // 
            // chkIsForbidden
            // 
            chkIsForbidden.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkIsForbidden.AutoSize = true;
            chkIsForbidden.Location = new Point(135, 63);
            chkIsForbidden.Name = "chkIsForbidden";
            chkIsForbidden.Size = new Size(278, 19);
            chkIsForbidden.TabIndex = 2;
            chkIsForbidden.Text = "Is Forbidden?";
            chkIsForbidden.UseVisualStyleBackColor = true;
            // 
            // btnAddAndClose
            // 
            btnAddAndClose.BackColor = Color.Yellow;
            btnAddAndClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddAndClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddAndClose.FlatStyle = FlatStyle.Flat;
            btnAddAndClose.Image = Properties.Resources.add__16x16;
            btnAddAndClose.Location = new Point(135, 89);
            btnAddAndClose.Name = "btnAddAndClose";
            btnAddAndClose.Size = new Size(278, 30);
            btnAddAndClose.TabIndex = 3;
            btnAddAndClose.Text = "Add and Close";
            btnAddAndClose.TextAlign = ContentAlignment.MiddleRight;
            btnAddAndClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddAndClose.UseVisualStyleBackColor = false;
            btnAddAndClose.Click += btnAddKeyword_Click;
            // 
            // btnAddAndNew
            // 
            btnAddAndNew.BackColor = Color.Yellow;
            btnAddAndNew.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddAndNew.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddAndNew.FlatStyle = FlatStyle.Flat;
            btnAddAndNew.Image = Properties.Resources.add__16x16;
            btnAddAndNew.Location = new Point(3, 89);
            btnAddAndNew.Name = "btnAddAndNew";
            btnAddAndNew.Size = new Size(126, 30);
            btnAddAndNew.TabIndex = 11;
            btnAddAndNew.Text = "Add and New";
            btnAddAndNew.TextAlign = ContentAlignment.MiddleRight;
            btnAddAndNew.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddAndNew.UseVisualStyleBackColor = false;
            btnAddAndNew.Click += btnAddAndNew_Click;
            // 
            // ForbiddenKeywordInput
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(416, 122);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ForbiddenKeywordInput";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "New Keyword";
            TopMost = true;
            Load += ForbiddenKeywordInput_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button btnAddAndClose;
        private Label lblKeyword;
        private Label lblGroup;
        private TextBox txtKeyword;
        private ComboBox cmbGroups;
        private CheckBox chkIsForbidden;
        private Button btnAddAndNew;
    }
}
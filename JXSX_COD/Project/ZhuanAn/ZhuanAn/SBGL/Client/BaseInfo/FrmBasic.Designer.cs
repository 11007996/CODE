namespace Basic
{
    partial class FrmBasic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBasic));
            this.dgcFactoryCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetMainNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetSubNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcEntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCostCenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcDurableYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcDurableMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMadeFactory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcControlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiContact = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileManage = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgcFactoryCode
            // 
            this.dgcFactoryCode.MinimumWidth = 6;
            this.dgcFactoryCode.Name = "dgcFactoryCode";
            this.dgcFactoryCode.Width = 125;
            // 
            // dgcAssetMainNo
            // 
            this.dgcAssetMainNo.MinimumWidth = 6;
            this.dgcAssetMainNo.Name = "dgcAssetMainNo";
            this.dgcAssetMainNo.Width = 125;
            // 
            // dgcAssetSubNo
            // 
            this.dgcAssetSubNo.MinimumWidth = 6;
            this.dgcAssetSubNo.Name = "dgcAssetSubNo";
            this.dgcAssetSubNo.Width = 125;
            // 
            // dgcAssetName
            // 
            this.dgcAssetName.MinimumWidth = 6;
            this.dgcAssetName.Name = "dgcAssetName";
            this.dgcAssetName.Width = 125;
            // 
            // dgcAssetClass
            // 
            this.dgcAssetClass.MinimumWidth = 6;
            this.dgcAssetClass.Name = "dgcAssetClass";
            this.dgcAssetClass.Width = 125;
            // 
            // dgcModel
            // 
            this.dgcModel.MinimumWidth = 6;
            this.dgcModel.Name = "dgcModel";
            this.dgcModel.Width = 125;
            // 
            // dgcEntryDate
            // 
            this.dgcEntryDate.MinimumWidth = 6;
            this.dgcEntryDate.Name = "dgcEntryDate";
            this.dgcEntryDate.Width = 125;
            // 
            // dgcCostCenter
            // 
            this.dgcCostCenter.MinimumWidth = 6;
            this.dgcCostCenter.Name = "dgcCostCenter";
            this.dgcCostCenter.Width = 125;
            // 
            // dgcDurableYear
            // 
            this.dgcDurableYear.MinimumWidth = 6;
            this.dgcDurableYear.Name = "dgcDurableYear";
            this.dgcDurableYear.Width = 125;
            // 
            // dgcDurableMonth
            // 
            this.dgcDurableMonth.MinimumWidth = 6;
            this.dgcDurableMonth.Name = "dgcDurableMonth";
            this.dgcDurableMonth.Width = 125;
            // 
            // dgcMadeFactory
            // 
            this.dgcMadeFactory.MinimumWidth = 6;
            this.dgcMadeFactory.Name = "dgcMadeFactory";
            this.dgcMadeFactory.Width = 125;
            // 
            // dgcControlNo
            // 
            this.dgcControlNo.MinimumWidth = 6;
            this.dgcControlNo.Name = "dgcControlNo";
            this.dgcControlNo.Width = 125;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Honeydew;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLine,
            this.tsmiContact,
            this.tsmiFileManage});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(134, 753);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiLine
            // 
            this.tsmiLine.Image = ((System.Drawing.Image)(resources.GetObject("tsmiLine.Image")));
            this.tsmiLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiLine.Name = "tsmiLine";
            this.tsmiLine.Size = new System.Drawing.Size(125, 31);
            this.tsmiLine.Text = "产线";
            this.tsmiLine.Click += new System.EventHandler(this.tsmiLine_Click);
            // 
            // tsmiContact
            // 
            this.tsmiContact.Image = ((System.Drawing.Image)(resources.GetObject("tsmiContact.Image")));
            this.tsmiContact.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiContact.Name = "tsmiContact";
            this.tsmiContact.Size = new System.Drawing.Size(125, 31);
            this.tsmiContact.Text = "联系人";
            this.tsmiContact.Click += new System.EventHandler(this.tsmiContact_Click);
            // 
            // tsmiFileManage
            // 
            this.tsmiFileManage.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileManage.Image")));
            this.tsmiFileManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiFileManage.Name = "tsmiFileManage";
            this.tsmiFileManage.Size = new System.Drawing.Size(125, 31);
            this.tsmiFileManage.Text = "文件管理";
            this.tsmiFileManage.Click += new System.EventHandler(this.tsmiFileManage_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelContent, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1582, 753);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(137, 3);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1442, 747);
            this.panelContent.TabIndex = 2;
            // 
            // FrmBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1582, 753);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmBasic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基础信息";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseBasic_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFactoryCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetMainNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetSubNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcEntryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCostCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcDurableYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcDurableMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMadeFactory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcControlNo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiLine;
        private System.Windows.Forms.ToolStripMenuItem tsmiContact;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileManage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelContent;
    }
}
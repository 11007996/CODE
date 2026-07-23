namespace Machine
{
    partial class FrmMaintenanceItem
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
            this.labAssetNo = new System.Windows.Forms.Label();
            this.labTimeMark = new System.Windows.Forms.Label();
            this.cmbAssetNo = new System.Windows.Forms.ComboBox();
            this.gbMaintenanceItem = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.nudSortNo = new System.Windows.Forms.NumericUpDown();
            this.labSortNo = new System.Windows.Forms.Label();
            this.txbItemName = new System.Windows.Forms.TextBox();
            this.labItemName = new System.Windows.Forms.Label();
            this.cmbTimeMark = new System.Windows.Forms.ComboBox();
            this.dgvMaintenanceItem = new System.Windows.Forms.DataGridView();
            this.dgcAssetNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTimeMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcSortNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.gbMaintenanceItem.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSortNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenanceItem)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labAssetNo
            // 
            this.labAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAssetNo.AutoSize = true;
            this.labAssetNo.Location = new System.Drawing.Point(10, 18);
            this.labAssetNo.Name = "labAssetNo";
            this.labAssetNo.Size = new System.Drawing.Size(67, 15);
            this.labAssetNo.TabIndex = 0;
            this.labAssetNo.Text = "资产编号";
            // 
            // labTimeMark
            // 
            this.labTimeMark.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTimeMark.AutoSize = true;
            this.labTimeMark.Location = new System.Drawing.Point(320, 18);
            this.labTimeMark.Name = "labTimeMark";
            this.labTimeMark.Size = new System.Drawing.Size(67, 15);
            this.labTimeMark.TabIndex = 1;
            this.labTimeMark.Text = "时间周期";
            // 
            // cmbAssetNo
            // 
            this.cmbAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbAssetNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAssetNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAssetNo.FormattingEnabled = true;
            this.cmbAssetNo.Location = new System.Drawing.Point(83, 14);
            this.cmbAssetNo.Name = "cmbAssetNo";
            this.cmbAssetNo.Size = new System.Drawing.Size(220, 23);
            this.cmbAssetNo.TabIndex = 3;
            this.cmbAssetNo.SelectedIndexChanged += new System.EventHandler(this.cmbAssetNo_SelectedIndexChanged);
            // 
            // gbMaintenanceItem
            // 
            this.gbMaintenanceItem.Controls.Add(this.tableLayoutPanel1);
            this.gbMaintenanceItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMaintenanceItem.Location = new System.Drawing.Point(0, 30);
            this.gbMaintenanceItem.Name = "gbMaintenanceItem";
            this.gbMaintenanceItem.Size = new System.Drawing.Size(1007, 75);
            this.gbMaintenanceItem.TabIndex = 6;
            this.gbMaintenanceItem.TabStop = false;
            this.gbMaintenanceItem.Text = "维护项目信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labAssetNo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudSortNo, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbAssetNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labSortNo, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.labTimeMark, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbItemName, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.labItemName, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbTimeMark, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1001, 51);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // nudSortNo
            // 
            this.nudSortNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudSortNo.Location = new System.Drawing.Point(903, 13);
            this.nudSortNo.Name = "nudSortNo";
            this.nudSortNo.Size = new System.Drawing.Size(66, 25);
            this.nudSortNo.TabIndex = 8;
            // 
            // labSortNo
            // 
            this.labSortNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labSortNo.AutoSize = true;
            this.labSortNo.Location = new System.Drawing.Point(860, 18);
            this.labSortNo.Name = "labSortNo";
            this.labSortNo.Size = new System.Drawing.Size(37, 15);
            this.labSortNo.TabIndex = 7;
            this.labSortNo.Text = "排序";
            // 
            // txbItemName
            // 
            this.txbItemName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbItemName.Location = new System.Drawing.Point(613, 13);
            this.txbItemName.Name = "txbItemName";
            this.txbItemName.Size = new System.Drawing.Size(220, 25);
            this.txbItemName.TabIndex = 4;
            // 
            // labItemName
            // 
            this.labItemName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labItemName.AutoSize = true;
            this.labItemName.Location = new System.Drawing.Point(510, 18);
            this.labItemName.Name = "labItemName";
            this.labItemName.Size = new System.Drawing.Size(97, 15);
            this.labItemName.TabIndex = 6;
            this.labItemName.Text = "维护项目名称";
            // 
            // cmbTimeMark
            // 
            this.cmbTimeMark.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbTimeMark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeMark.FormattingEnabled = true;
            this.cmbTimeMark.Location = new System.Drawing.Point(393, 14);
            this.cmbTimeMark.Name = "cmbTimeMark";
            this.cmbTimeMark.Size = new System.Drawing.Size(83, 23);
            this.cmbTimeMark.TabIndex = 5;
            this.cmbTimeMark.SelectedIndexChanged += new System.EventHandler(this.cmbTimeMark_SelectedIndexChanged);
            // 
            // dgvMaintenanceItem
            // 
            this.dgvMaintenanceItem.AllowUserToAddRows = false;
            this.dgvMaintenanceItem.AllowUserToDeleteRows = false;
            this.dgvMaintenanceItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaintenanceItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaintenanceItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcAssetNo,
            this.dgcTimeMark,
            this.dgcItemName,
            this.dgcSortNo});
            this.dgvMaintenanceItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaintenanceItem.Location = new System.Drawing.Point(0, 105);
            this.dgvMaintenanceItem.MultiSelect = false;
            this.dgvMaintenanceItem.Name = "dgvMaintenanceItem";
            this.dgvMaintenanceItem.ReadOnly = true;
            this.dgvMaintenanceItem.RowHeadersVisible = false;
            this.dgvMaintenanceItem.RowHeadersWidth = 51;
            this.dgvMaintenanceItem.RowTemplate.Height = 27;
            this.dgvMaintenanceItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaintenanceItem.Size = new System.Drawing.Size(1007, 385);
            this.dgvMaintenanceItem.TabIndex = 7;
            // 
            // dgcAssetNo
            // 
            this.dgcAssetNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetNo.DataPropertyName = "AssetNo";
            this.dgcAssetNo.HeaderText = "资产编号";
            this.dgcAssetNo.MinimumWidth = 6;
            this.dgcAssetNo.Name = "dgcAssetNo";
            this.dgcAssetNo.ReadOnly = true;
            this.dgcAssetNo.Width = 150;
            // 
            // dgcTimeMark
            // 
            this.dgcTimeMark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcTimeMark.DataPropertyName = "TimeMark";
            this.dgcTimeMark.HeaderText = "时间周期";
            this.dgcTimeMark.MinimumWidth = 6;
            this.dgcTimeMark.Name = "dgcTimeMark";
            this.dgcTimeMark.ReadOnly = true;
            this.dgcTimeMark.Width = 125;
            // 
            // dgcItemName
            // 
            this.dgcItemName.DataPropertyName = "ItemName";
            this.dgcItemName.HeaderText = "项目名称";
            this.dgcItemName.MinimumWidth = 6;
            this.dgcItemName.Name = "dgcItemName";
            this.dgcItemName.ReadOnly = true;
            // 
            // dgcSortNo
            // 
            this.dgcSortNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcSortNo.DataPropertyName = "SortNo";
            this.dgcSortNo.HeaderText = "排序编号";
            this.dgcSortNo.MinimumWidth = 6;
            this.dgcSortNo.Name = "dgcSortNo";
            this.dgcSortNo.ReadOnly = true;
            this.dgcSortNo.Width = 125;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSave,
            this.tsmiDel,
            this.tsmiImport,
            this.tsmiExport});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1007, 30);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(53, 26);
            this.tsmiSave.Text = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiDel
            // 
            this.tsmiDel.Name = "tsmiDel";
            this.tsmiDel.Size = new System.Drawing.Size(53, 26);
            this.tsmiDel.Text = "删除";
            this.tsmiDel.Click += new System.EventHandler(this.tsmiDel_Click);
            // 
            // tsmiImport
            // 
            this.tsmiImport.Name = "tsmiImport";
            this.tsmiImport.Size = new System.Drawing.Size(53, 26);
            this.tsmiImport.Text = "导入";
            this.tsmiImport.Click += new System.EventHandler(this.tsmiImport_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(53, 26);
            this.tsmiExport.Text = "导出";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // FrmMaintenanceItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1007, 490);
            this.Controls.Add(this.dgvMaintenanceItem);
            this.Controls.Add(this.gbMaintenanceItem);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMaintenanceItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备保养项目";
            this.Load += new System.EventHandler(this.FrmMaintenanceItem_Load);
            this.gbMaintenanceItem.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSortNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenanceItem)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labAssetNo;
        private System.Windows.Forms.Label labTimeMark;
        private System.Windows.Forms.ComboBox cmbAssetNo;
        private System.Windows.Forms.GroupBox gbMaintenanceItem;
        private System.Windows.Forms.DataGridView dgvMaintenanceItem;
        private System.Windows.Forms.Label labItemName;
        private System.Windows.Forms.ComboBox cmbTimeMark;
        private System.Windows.Forms.TextBox txbItemName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.NumericUpDown nudSortNo;
        private System.Windows.Forms.Label labSortNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiDel;
        private System.Windows.Forms.ToolStripMenuItem tsmiImport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTimeMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcSortNo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
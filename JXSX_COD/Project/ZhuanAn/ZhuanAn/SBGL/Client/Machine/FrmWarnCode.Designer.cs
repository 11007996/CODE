namespace Machine
{
    partial class FrmWarnCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWarnCode));
            this.dgvWarnCode = new System.Windows.Forms.DataGridView();
            this.dgcWarnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWarnDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbWarn = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labMessage = new System.Windows.Forms.Label();
            this.labMachineCode = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.labWarnCode = new System.Windows.Forms.Label();
            this.txbMachineCode = new System.Windows.Forms.TextBox();
            this.txbWarnDesc = new System.Windows.Forms.TextBox();
            this.labWarnDetail = new System.Windows.Forms.Label();
            this.txbWarnCode = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tmisImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnCode)).BeginInit();
            this.gbWarn.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvWarnCode
            // 
            this.dgvWarnCode.AllowUserToAddRows = false;
            this.dgvWarnCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWarnCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWarnCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcWarnCode,
            this.dgcWarnDesc});
            this.dgvWarnCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWarnCode.Location = new System.Drawing.Point(0, 147);
            this.dgvWarnCode.Name = "dgvWarnCode";
            this.dgvWarnCode.ReadOnly = true;
            this.dgvWarnCode.RowHeadersWidth = 51;
            this.dgvWarnCode.RowTemplate.Height = 27;
            this.dgvWarnCode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWarnCode.Size = new System.Drawing.Size(667, 306);
            this.dgvWarnCode.TabIndex = 0;
            this.dgvWarnCode.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvWarnCode_UserDeletingRow);
            // 
            // dgcWarnCode
            // 
            this.dgcWarnCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcWarnCode.DataPropertyName = "WarnCode";
            this.dgcWarnCode.HeaderText = "报警代码";
            this.dgcWarnCode.MinimumWidth = 6;
            this.dgcWarnCode.Name = "dgcWarnCode";
            this.dgcWarnCode.ReadOnly = true;
            this.dgcWarnCode.Width = 120;
            // 
            // dgcWarnDesc
            // 
            this.dgcWarnDesc.DataPropertyName = "WarnDesc";
            this.dgcWarnDesc.HeaderText = "报警详情";
            this.dgcWarnDesc.MinimumWidth = 6;
            this.dgcWarnDesc.Name = "dgcWarnDesc";
            this.dgcWarnDesc.ReadOnly = true;
            // 
            // gbWarn
            // 
            this.gbWarn.AutoSize = true;
            this.gbWarn.Controls.Add(this.tableLayoutPanel1);
            this.gbWarn.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbWarn.Location = new System.Drawing.Point(0, 28);
            this.gbWarn.Name = "gbWarn";
            this.gbWarn.Size = new System.Drawing.Size(667, 119);
            this.gbWarn.TabIndex = 1;
            this.gbWarn.TabStop = false;
            this.gbWarn.Text = "报警编码";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labMachineCode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labWarnCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbMachineCode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbWarnDesc, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labWarnDetail, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbWarnCode, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(661, 95);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labMessage, 4);
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(3, 72);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 8;
            this.labMessage.Text = "提示";
            // 
            // labMachineCode
            // 
            this.labMachineCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMachineCode.AutoSize = true;
            this.labMachineCode.Location = new System.Drawing.Point(3, 7);
            this.labMachineCode.Name = "labMachineCode";
            this.labMachineCode.Size = new System.Drawing.Size(67, 15);
            this.labMachineCode.TabIndex = 6;
            this.labMachineCode.Text = "设备编码";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.Location = new System.Drawing.Point(553, 33);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 29);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "增加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labWarnCode
            // 
            this.labWarnCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labWarnCode.AutoSize = true;
            this.labWarnCode.Location = new System.Drawing.Point(153, 7);
            this.labWarnCode.Name = "labWarnCode";
            this.labWarnCode.Size = new System.Drawing.Size(67, 15);
            this.labWarnCode.TabIndex = 1;
            this.labWarnCode.Text = "报警代码";
            // 
            // txbMachineCode
            // 
            this.txbMachineCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbMachineCode.Location = new System.Drawing.Point(3, 35);
            this.txbMachineCode.MaxLength = 20;
            this.txbMachineCode.Name = "txbMachineCode";
            this.txbMachineCode.ReadOnly = true;
            this.txbMachineCode.Size = new System.Drawing.Size(100, 25);
            this.txbMachineCode.TabIndex = 7;
            // 
            // txbWarnDesc
            // 
            this.txbWarnDesc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbWarnDesc.Location = new System.Drawing.Point(303, 35);
            this.txbWarnDesc.MaxLength = 50;
            this.txbWarnDesc.Name = "txbWarnDesc";
            this.txbWarnDesc.Size = new System.Drawing.Size(230, 25);
            this.txbWarnDesc.TabIndex = 3;
            // 
            // labWarnDetail
            // 
            this.labWarnDetail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labWarnDetail.AutoSize = true;
            this.labWarnDetail.Location = new System.Drawing.Point(303, 7);
            this.labWarnDetail.Name = "labWarnDetail";
            this.labWarnDetail.Size = new System.Drawing.Size(67, 15);
            this.labWarnDetail.TabIndex = 0;
            this.labWarnDetail.Text = "报警详情";
            // 
            // txbWarnCode
            // 
            this.txbWarnCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbWarnCode.Location = new System.Drawing.Point(153, 35);
            this.txbWarnCode.MaxLength = 2;
            this.txbWarnCode.Name = "txbWarnCode";
            this.txbWarnCode.Size = new System.Drawing.Size(100, 25);
            this.txbWarnCode.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmisImport,
            this.tsmiExport});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(667, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tmisImport
            // 
            this.tmisImport.Image = ((System.Drawing.Image)(resources.GetObject("tmisImport.Image")));
            this.tmisImport.Name = "tmisImport";
            this.tmisImport.Size = new System.Drawing.Size(73, 24);
            this.tmisImport.Text = "导入";
            this.tmisImport.Click += new System.EventHandler(this.tmisImport_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExport.Image")));
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(73, 24);
            this.tsmiExport.Text = "导出";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // FrmWarnCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(667, 453);
            this.Controls.Add(this.dgvWarnCode);
            this.Controls.Add(this.gbWarn);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmWarnCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报警代码维护";
            this.Load += new System.EventHandler(this.FrmWarnCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnCode)).EndInit();
            this.gbWarn.ResumeLayout(false);
            this.gbWarn.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWarnCode;
        private System.Windows.Forms.GroupBox gbWarn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tmisImport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txbWarnDesc;
        private System.Windows.Forms.TextBox txbWarnCode;
        private System.Windows.Forms.Label labWarnCode;
        private System.Windows.Forms.Label labWarnDetail;
        private System.Windows.Forms.Label labMachineCode;
        private System.Windows.Forms.TextBox txbMachineCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWarnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWarnDesc;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
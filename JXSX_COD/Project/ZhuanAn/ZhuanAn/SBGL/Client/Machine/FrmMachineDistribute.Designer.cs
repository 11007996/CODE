namespace Machine
{
    partial class FrmMachineDistribute
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
            this.dgvMachineDistribute = new System.Windows.Forms.DataGridView();
            this.dgcPointName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbPointInfo = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labMessage = new System.Windows.Forms.Label();
            this.labPointName = new System.Windows.Forms.Label();
            this.tbxPointName = new System.Windows.Forms.TextBox();
            this.labCount = new System.Windows.Forms.Label();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineDistribute)).BeginInit();
            this.gbPointInfo.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMachineDistribute
            // 
            this.dgvMachineDistribute.AllowUserToAddRows = false;
            this.dgvMachineDistribute.AllowUserToDeleteRows = false;
            this.dgvMachineDistribute.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMachineDistribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMachineDistribute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcPointName,
            this.dgcCount});
            this.dgvMachineDistribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMachineDistribute.Location = new System.Drawing.Point(0, 113);
            this.dgvMachineDistribute.Name = "dgvMachineDistribute";
            this.dgvMachineDistribute.ReadOnly = true;
            this.dgvMachineDistribute.RowHeadersWidth = 51;
            this.dgvMachineDistribute.RowTemplate.Height = 27;
            this.dgvMachineDistribute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMachineDistribute.Size = new System.Drawing.Size(492, 195);
            this.dgvMachineDistribute.TabIndex = 0;
            this.dgvMachineDistribute.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMachineDistribute_CellClick);
            // 
            // dgcPointName
            // 
            this.dgcPointName.DataPropertyName = "PointName";
            this.dgcPointName.HeaderText = "点位名称";
            this.dgcPointName.MinimumWidth = 6;
            this.dgcPointName.Name = "dgcPointName";
            this.dgcPointName.ReadOnly = true;
            // 
            // dgcCount
            // 
            this.dgcCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcCount.DataPropertyName = "Count";
            this.dgcCount.HeaderText = "设备数量";
            this.dgcCount.MinimumWidth = 6;
            this.dgcCount.Name = "dgcCount";
            this.dgcCount.ReadOnly = true;
            this.dgcCount.Width = 125;
            // 
            // gbPointInfo
            // 
            this.gbPointInfo.BackColor = System.Drawing.Color.Transparent;
            this.gbPointInfo.Controls.Add(this.tableLayoutPanel1);
            this.gbPointInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPointInfo.Location = new System.Drawing.Point(0, 28);
            this.gbPointInfo.Name = "gbPointInfo";
            this.gbPointInfo.Size = new System.Drawing.Size(492, 85);
            this.gbPointInfo.TabIndex = 1;
            this.gbPointInfo.TabStop = false;
            this.gbPointInfo.Text = "点位信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labPointName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbxPointName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labCount, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudCount, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(486, 61);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labMessage, 4);
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(3, 42);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 8;
            this.labMessage.Text = "提示";
            // 
            // labPointName
            // 
            this.labPointName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPointName.AutoSize = true;
            this.labPointName.Location = new System.Drawing.Point(10, 10);
            this.labPointName.Name = "labPointName";
            this.labPointName.Size = new System.Drawing.Size(67, 15);
            this.labPointName.TabIndex = 1;
            this.labPointName.Text = "点位名称";
            // 
            // tbxPointName
            // 
            this.tbxPointName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxPointName.Location = new System.Drawing.Point(83, 5);
            this.tbxPointName.MaxLength = 10;
            this.tbxPointName.Name = "tbxPointName";
            this.tbxPointName.Size = new System.Drawing.Size(150, 25);
            this.tbxPointName.TabIndex = 2;
            // 
            // labCount
            // 
            this.labCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labCount.AutoSize = true;
            this.labCount.Location = new System.Drawing.Point(253, 10);
            this.labCount.Name = "labCount";
            this.labCount.Size = new System.Drawing.Size(67, 15);
            this.labCount.TabIndex = 0;
            this.labCount.Text = "设备数量";
            // 
            // nudCount
            // 
            this.nudCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudCount.Location = new System.Drawing.Point(326, 5);
            this.nudCount.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(150, 25);
            this.nudCount.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSave,
            this.tsmiDel});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(492, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(53, 24);
            this.tsmiSave.Text = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiDel
            // 
            this.tsmiDel.Name = "tsmiDel";
            this.tsmiDel.Size = new System.Drawing.Size(53, 24);
            this.tsmiDel.Text = "删除";
            this.tsmiDel.Click += new System.EventHandler(this.tsmiDel_Click);
            // 
            // FrmMachineDistribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(492, 308);
            this.Controls.Add(this.dgvMachineDistribute);
            this.Controls.Add(this.gbPointInfo);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMachineDistribute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备分布维护";
            this.Load += new System.EventHandler(this.FrmMachineDistribute_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineDistribute)).EndInit();
            this.gbPointInfo.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMachineDistribute;
        private System.Windows.Forms.GroupBox gbPointInfo;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.TextBox tbxPointName;
        private System.Windows.Forms.Label labPointName;
        private System.Windows.Forms.Label labCount;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPointName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
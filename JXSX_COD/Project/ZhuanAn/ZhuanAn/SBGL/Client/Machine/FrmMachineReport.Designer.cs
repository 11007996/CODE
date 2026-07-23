namespace Machine
{
    partial class FrmMachineReport
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
            this.components = new System.ComponentModel.Container();
            this.labMachineCode = new System.Windows.Forms.Label();
            this.labStartTime = new System.Windows.Forms.Label();
            this.labEndTime = new System.Windows.Forms.Label();
            this.cmbMachineCode = new System.Windows.Forms.ComboBox();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.gbMachineReport = new System.Windows.Forms.GroupBox();
            this.dgvMachineReport = new System.Windows.Forms.DataGridView();
            this.dgcMachineCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcOperateCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRunState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcProductCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFailedCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWarnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWarnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLineCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRemoteEndPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcReturnHexCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsDel = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbMachineReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineReport)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labMachineCode
            // 
            this.labMachineCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachineCode.AutoSize = true;
            this.labMachineCode.Location = new System.Drawing.Point(20, 10);
            this.labMachineCode.Name = "labMachineCode";
            this.labMachineCode.Size = new System.Drawing.Size(37, 15);
            this.labMachineCode.TabIndex = 0;
            this.labMachineCode.Text = "设备";
            // 
            // labStartTime
            // 
            this.labStartTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labStartTime.AutoSize = true;
            this.labStartTime.Location = new System.Drawing.Point(270, 10);
            this.labStartTime.Name = "labStartTime";
            this.labStartTime.Size = new System.Drawing.Size(67, 15);
            this.labStartTime.TabIndex = 1;
            this.labStartTime.Text = "上报时间";
            // 
            // labEndTime
            // 
            this.labEndTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labEndTime.AutoSize = true;
            this.labEndTime.Location = new System.Drawing.Point(495, 10);
            this.labEndTime.Name = "labEndTime";
            this.labEndTime.Size = new System.Drawing.Size(22, 15);
            this.labEndTime.TabIndex = 2;
            this.labEndTime.Text = "至";
            // 
            // cmbMachineCode
            // 
            this.cmbMachineCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMachineCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMachineCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMachineCode.FormattingEnabled = true;
            this.cmbMachineCode.Location = new System.Drawing.Point(63, 6);
            this.cmbMachineCode.Name = "cmbMachineCode";
            this.cmbMachineCode.Size = new System.Drawing.Size(194, 23);
            this.cmbMachineCode.TabIndex = 3;
            this.cmbMachineCode.SelectedIndexChanged += new System.EventHandler(this.cmbMachineCode_SelectedIndexChanged);
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpStartTime.Location = new System.Drawing.Point(343, 5);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(144, 25);
            this.dtpStartTime.TabIndex = 4;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpEndTime.Location = new System.Drawing.Point(523, 5);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(144, 25);
            this.dtpEndTime.TabIndex = 5;
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);
            // 
            // gbMachineReport
            // 
            this.gbMachineReport.Controls.Add(this.tableLayoutPanel1);
            this.gbMachineReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMachineReport.Location = new System.Drawing.Point(0, 0);
            this.gbMachineReport.Name = "gbMachineReport";
            this.gbMachineReport.Size = new System.Drawing.Size(1482, 60);
            this.gbMachineReport.TabIndex = 6;
            this.gbMachineReport.TabStop = false;
            this.gbMachineReport.Text = "条件";
            // 
            // dgvMachineReport
            // 
            this.dgvMachineReport.AllowUserToAddRows = false;
            this.dgvMachineReport.AllowUserToDeleteRows = false;
            this.dgvMachineReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMachineReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMachineReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcMachineCode,
            this.dgcOperateCode,
            this.dgcRunState,
            this.dgcProductCount,
            this.dgcFailedCount,
            this.dgcWarnState,
            this.dgcWarnCode,
            this.dgcCreateTime,
            this.dgcLineCode,
            this.dgcRemoteEndPoint,
            this.dgcReturnHexCode});
            this.dgvMachineReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMachineReport.Location = new System.Drawing.Point(0, 60);
            this.dgvMachineReport.Name = "dgvMachineReport";
            this.dgvMachineReport.ReadOnly = true;
            this.dgvMachineReport.RowHeadersVisible = false;
            this.dgvMachineReport.RowHeadersWidth = 51;
            this.dgvMachineReport.RowTemplate.Height = 27;
            this.dgvMachineReport.Size = new System.Drawing.Size(1482, 430);
            this.dgvMachineReport.TabIndex = 7;
            this.dgvMachineReport.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMachineReport_CellMouseClick);
            // 
            // dgcMachineCode
            // 
            this.dgcMachineCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcMachineCode.DataPropertyName = "MachineCode";
            this.dgcMachineCode.HeaderText = "设备编码";
            this.dgcMachineCode.MinimumWidth = 6;
            this.dgcMachineCode.Name = "dgcMachineCode";
            this.dgcMachineCode.ReadOnly = true;
            this.dgcMachineCode.Width = 80;
            // 
            // dgcOperateCode
            // 
            this.dgcOperateCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcOperateCode.DataPropertyName = "OperateCode";
            this.dgcOperateCode.HeaderText = "操作编码";
            this.dgcOperateCode.MinimumWidth = 6;
            this.dgcOperateCode.Name = "dgcOperateCode";
            this.dgcOperateCode.ReadOnly = true;
            this.dgcOperateCode.Width = 80;
            // 
            // dgcRunState
            // 
            this.dgcRunState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcRunState.DataPropertyName = "RunState";
            this.dgcRunState.HeaderText = "运行状态";
            this.dgcRunState.MinimumWidth = 6;
            this.dgcRunState.Name = "dgcRunState";
            this.dgcRunState.ReadOnly = true;
            this.dgcRunState.Width = 80;
            // 
            // dgcProductCount
            // 
            this.dgcProductCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcProductCount.DataPropertyName = "ProductCount";
            this.dgcProductCount.HeaderText = "产能";
            this.dgcProductCount.MinimumWidth = 6;
            this.dgcProductCount.Name = "dgcProductCount";
            this.dgcProductCount.ReadOnly = true;
            this.dgcProductCount.Width = 80;
            // 
            // dgcFailedCount
            // 
            this.dgcFailedCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFailedCount.DataPropertyName = "FailedCount";
            this.dgcFailedCount.HeaderText = "不良";
            this.dgcFailedCount.MinimumWidth = 6;
            this.dgcFailedCount.Name = "dgcFailedCount";
            this.dgcFailedCount.ReadOnly = true;
            this.dgcFailedCount.Width = 80;
            // 
            // dgcWarnState
            // 
            this.dgcWarnState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcWarnState.DataPropertyName = "WarnState";
            this.dgcWarnState.HeaderText = "报警状态";
            this.dgcWarnState.MinimumWidth = 6;
            this.dgcWarnState.Name = "dgcWarnState";
            this.dgcWarnState.ReadOnly = true;
            this.dgcWarnState.Width = 80;
            // 
            // dgcWarnCode
            // 
            this.dgcWarnCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcWarnCode.DataPropertyName = "WarnCode";
            this.dgcWarnCode.HeaderText = "报警代码";
            this.dgcWarnCode.MinimumWidth = 6;
            this.dgcWarnCode.Name = "dgcWarnCode";
            this.dgcWarnCode.ReadOnly = true;
            this.dgcWarnCode.Width = 80;
            // 
            // dgcCreateTime
            // 
            this.dgcCreateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcCreateTime.DataPropertyName = "CreateTime";
            this.dgcCreateTime.HeaderText = "上报时间";
            this.dgcCreateTime.MinimumWidth = 6;
            this.dgcCreateTime.Name = "dgcCreateTime";
            this.dgcCreateTime.ReadOnly = true;
            this.dgcCreateTime.Width = 110;
            // 
            // dgcLineCode
            // 
            this.dgcLineCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcLineCode.DataPropertyName = "LineCode";
            this.dgcLineCode.HeaderText = "产线编码";
            this.dgcLineCode.MinimumWidth = 6;
            this.dgcLineCode.Name = "dgcLineCode";
            this.dgcLineCode.ReadOnly = true;
            this.dgcLineCode.Width = 80;
            // 
            // dgcRemoteEndPoint
            // 
            this.dgcRemoteEndPoint.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcRemoteEndPoint.DataPropertyName = "RemoteEndPoint";
            this.dgcRemoteEndPoint.HeaderText = "远端节点";
            this.dgcRemoteEndPoint.MinimumWidth = 6;
            this.dgcRemoteEndPoint.Name = "dgcRemoteEndPoint";
            this.dgcRemoteEndPoint.ReadOnly = true;
            this.dgcRemoteEndPoint.Width = 140;
            // 
            // dgcReturnHexCode
            // 
            this.dgcReturnHexCode.DataPropertyName = "ReturnHexCode";
            this.dgcReturnHexCode.HeaderText = "返回编码";
            this.dgcReturnHexCode.MinimumWidth = 6;
            this.dgcReturnHexCode.Name = "dgcReturnHexCode";
            this.dgcReturnHexCode.ReadOnly = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsDel});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(109, 28);
            // 
            // tmsDel
            // 
            this.tmsDel.Name = "tmsDel";
            this.tmsDel.Size = new System.Drawing.Size(108, 24);
            this.tmsDel.Text = "删除";
            this.tmsDel.Click += new System.EventHandler(this.tmsDel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labMachineCode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpEndTime, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbMachineCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labEndTime, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpStartTime, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labStartTime, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1476, 36);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // FrmMachineReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1482, 490);
            this.Controls.Add(this.dgvMachineReport);
            this.Controls.Add(this.gbMachineReport);
            this.Name = "FrmMachineReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上报记录";
            this.Load += new System.EventHandler(this.FrmMachineReport_Load);
            this.gbMachineReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineReport)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labMachineCode;
        private System.Windows.Forms.Label labStartTime;
        private System.Windows.Forms.Label labEndTime;
        private System.Windows.Forms.ComboBox cmbMachineCode;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.GroupBox gbMachineReport;
        private System.Windows.Forms.DataGridView dgvMachineReport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tmsDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMachineCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcOperateCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRunState;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcProductCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFailedCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWarnState;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWarnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLineCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRemoteEndPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcReturnHexCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
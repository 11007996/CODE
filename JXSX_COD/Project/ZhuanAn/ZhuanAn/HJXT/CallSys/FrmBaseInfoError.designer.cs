namespace CallSys
{
    partial class FrmBaseInfoError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseInfoError));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStripError = new System.Windows.Forms.MenuStrip();
            this.tsmErrorRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmErrorSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmErrorUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmErrorSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmErrorDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmErrorBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmErrorExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmErrorReset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMsgPushSet = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvError = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcErrorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMachineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWaitedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcSolvedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcErrorHandlerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHelperComeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHelpTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHelperName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcErrorReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFaultContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcSolutionContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcProdCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcComeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcSolverName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFaultType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcErrorStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbError = new System.Windows.Forms.GroupBox();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.labArea = new System.Windows.Forms.Label();
            this.labErrorStatus = new System.Windows.Forms.Label();
            this.cmbErrorStatus = new System.Windows.Forms.ComboBox();
            this.tbErrorId = new System.Windows.Forms.TextBox();
            this.cmbMachineType = new System.Windows.Forms.ComboBox();
            this.labMachineType = new System.Windows.Forms.Label();
            this.cmbErrorHelper = new System.Windows.Forms.ComboBox();
            this.cmbErrorHandler = new System.Windows.Forms.ComboBox();
            this.labHandler = new System.Windows.Forms.Label();
            this.labHelper = new System.Windows.Forms.Label();
            this.labErrorMessage = new System.Windows.Forms.Label();
            this.nudProdCount = new System.Windows.Forms.NumericUpDown();
            this.labProdCount = new System.Windows.Forms.Label();
            this.tbSolutionContent = new System.Windows.Forms.TextBox();
            this.labSolutionContent = new System.Windows.Forms.Label();
            this.cmbErrorReason = new System.Windows.Forms.ComboBox();
            this.labErrorReason = new System.Windows.Forms.Label();
            this.tbFaultContent = new System.Windows.Forms.TextBox();
            this.labFaultContent = new System.Windows.Forms.Label();
            this.tbMachineNo = new System.Windows.Forms.TextBox();
            this.dtpComeTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.labComeTimeEnd = new System.Windows.Forms.Label();
            this.dtpComeTimeStart = new System.Windows.Forms.DateTimePicker();
            this.labComeTimeStart = new System.Windows.Forms.Label();
            this.labMachine = new System.Windows.Forms.Label();
            this.menuStripError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).BeginInit();
            this.gbError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProdCount)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripError
            // 
            this.menuStripError.BackColor = System.Drawing.Color.Transparent;
            this.menuStripError.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripError.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmErrorRefresh,
            this.tsmErrorSelect,
            this.tsmErrorUpdate,
            this.tsmErrorSave,
            this.tsmErrorDelete,
            this.tsmErrorBack,
            this.tsmErrorExport,
            this.tsmErrorReset,
            this.tsmMsgPushSet});
            this.menuStripError.Location = new System.Drawing.Point(15, 0);
            this.menuStripError.Name = "menuStripError";
            this.menuStripError.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStripError.Size = new System.Drawing.Size(1570, 35);
            this.menuStripError.TabIndex = 4;
            this.menuStripError.Text = "menuStrip2";
            // 
            // tsmErrorRefresh
            // 
            this.tsmErrorRefresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmErrorRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmErrorRefresh.Image")));
            this.tsmErrorRefresh.Name = "tsmErrorRefresh";
            this.tsmErrorRefresh.Size = new System.Drawing.Size(84, 31);
            this.tsmErrorRefresh.Text = "刷新";
            this.tsmErrorRefresh.Click += new System.EventHandler(this.tsmErrorRefresh_Click);
            // 
            // tsmErrorSelect
            // 
            this.tsmErrorSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmErrorSelect.Image = ((System.Drawing.Image)(resources.GetObject("tsmErrorSelect.Image")));
            this.tsmErrorSelect.Name = "tsmErrorSelect";
            this.tsmErrorSelect.Size = new System.Drawing.Size(84, 31);
            this.tsmErrorSelect.Text = "查询";
            this.tsmErrorSelect.Click += new System.EventHandler(this.tsmErrorSelect_Click);
            // 
            // tsmErrorUpdate
            // 
            this.tsmErrorUpdate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmErrorUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsmErrorUpdate.Image")));
            this.tsmErrorUpdate.Name = "tsmErrorUpdate";
            this.tsmErrorUpdate.Size = new System.Drawing.Size(84, 31);
            this.tsmErrorUpdate.Text = "维护";
            this.tsmErrorUpdate.Click += new System.EventHandler(this.tsmErrorUpdate_Click);
            // 
            // tsmErrorSave
            // 
            this.tsmErrorSave.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmErrorSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmErrorSave.Image")));
            this.tsmErrorSave.Name = "tsmErrorSave";
            this.tsmErrorSave.Size = new System.Drawing.Size(84, 31);
            this.tsmErrorSave.Text = "保存";
            this.tsmErrorSave.Click += new System.EventHandler(this.tsmErrorSave_Click);
            // 
            // tsmErrorDelete
            // 
            this.tsmErrorDelete.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmErrorDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsmErrorDelete.Image")));
            this.tsmErrorDelete.Name = "tsmErrorDelete";
            this.tsmErrorDelete.Size = new System.Drawing.Size(84, 31);
            this.tsmErrorDelete.Text = "删除";
            this.tsmErrorDelete.Click += new System.EventHandler(this.tsmErrorDelete_Click);
            // 
            // tsmErrorBack
            // 
            this.tsmErrorBack.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmErrorBack.Image = ((System.Drawing.Image)(resources.GetObject("tsmErrorBack.Image")));
            this.tsmErrorBack.Name = "tsmErrorBack";
            this.tsmErrorBack.Size = new System.Drawing.Size(84, 31);
            this.tsmErrorBack.Text = "返回";
            this.tsmErrorBack.Click += new System.EventHandler(this.tsmErrorBack_Click);
            // 
            // tsmErrorExport
            // 
            this.tsmErrorExport.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmErrorExport.Image = ((System.Drawing.Image)(resources.GetObject("tsmErrorExport.Image")));
            this.tsmErrorExport.Name = "tsmErrorExport";
            this.tsmErrorExport.Size = new System.Drawing.Size(84, 31);
            this.tsmErrorExport.Text = "导出";
            this.tsmErrorExport.Click += new System.EventHandler(this.tsmErrorExport_Click);
            // 
            // tsmErrorReset
            // 
            this.tsmErrorReset.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmErrorReset.Image = ((System.Drawing.Image)(resources.GetObject("tsmErrorReset.Image")));
            this.tsmErrorReset.Name = "tsmErrorReset";
            this.tsmErrorReset.Size = new System.Drawing.Size(124, 31);
            this.tsmErrorReset.Text = "重置看板";
            this.tsmErrorReset.Click += new System.EventHandler(this.tsmErrorReset_Click);
            // 
            // tsmMsgPushSet
            // 
            this.tsmMsgPushSet.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmMsgPushSet.Image = ((System.Drawing.Image)(resources.GetObject("tsmMsgPushSet.Image")));
            this.tsmMsgPushSet.Name = "tsmMsgPushSet";
            this.tsmMsgPushSet.Size = new System.Drawing.Size(124, 31);
            this.tsmMsgPushSet.Text = "消息设置";
            this.tsmMsgPushSet.Click += new System.EventHandler(this.tsmMsgPushSet_Click);
            // 
            // dgvError
            // 
            this.dgvError.AllowUserToAddRows = false;
            this.dgvError.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.GhostWhite;
            this.dgvError.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvError.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvError.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvError.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvError.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcErrorId,
            this.dgcMachineNo,
            this.dgcWaitedTime,
            this.dgcSolvedTime,
            this.dgcErrorHandlerName,
            this.dgcHelperComeTime,
            this.dgcHelpTimes,
            this.dgcHelperName,
            this.dgcErrorReason,
            this.dgcFaultContent,
            this.dgcSolutionContent,
            this.dgcProdCount,
            this.dgcComeTime,
            this.dgcSolverName,
            this.dgcFaultType,
            this.dgcErrorStatus});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvError.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvError.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvError.Location = new System.Drawing.Point(15, 315);
            this.dgvError.Margin = new System.Windows.Forms.Padding(4);
            this.dgvError.Name = "dgvError";
            this.dgvError.ReadOnly = true;
            this.dgvError.RowTemplate.Height = 23;
            this.dgvError.Size = new System.Drawing.Size(1570, 470);
            this.dgvError.TabIndex = 3;
            this.dgvError.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvError_CellClick);
            this.dgvError.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvError_CellFormatting);
            // 
            // dgcErrorId
            // 
            this.dgcErrorId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcErrorId.DataPropertyName = "Id";
            this.dgcErrorId.HeaderText = "索引";
            this.dgcErrorId.Name = "dgcErrorId";
            this.dgcErrorId.ReadOnly = true;
            this.dgcErrorId.Width = 60;
            // 
            // dgcMachineNo
            // 
            this.dgcMachineNo.DataPropertyName = "MachineNo";
            this.dgcMachineNo.HeaderText = "机台编号";
            this.dgcMachineNo.Name = "dgcMachineNo";
            this.dgcMachineNo.ReadOnly = true;
            // 
            // dgcWaitedTime
            // 
            this.dgcWaitedTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcWaitedTime.DataPropertyName = "WaitedTime";
            this.dgcWaitedTime.HeaderText = "等待时间";
            this.dgcWaitedTime.Name = "dgcWaitedTime";
            this.dgcWaitedTime.ReadOnly = true;
            this.dgcWaitedTime.Width = 80;
            // 
            // dgcSolvedTime
            // 
            this.dgcSolvedTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcSolvedTime.DataPropertyName = "SolvedTime";
            this.dgcSolvedTime.HeaderText = "处理时间";
            this.dgcSolvedTime.Name = "dgcSolvedTime";
            this.dgcSolvedTime.ReadOnly = true;
            this.dgcSolvedTime.Width = 80;
            // 
            // dgcErrorHandlerName
            // 
            this.dgcErrorHandlerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcErrorHandlerName.DataPropertyName = "HandlerName";
            this.dgcErrorHandlerName.HeaderText = "处理者";
            this.dgcErrorHandlerName.Name = "dgcErrorHandlerName";
            this.dgcErrorHandlerName.ReadOnly = true;
            this.dgcErrorHandlerName.Width = 80;
            // 
            // dgcHelperComeTime
            // 
            this.dgcHelperComeTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHelperComeTime.HeaderText = "支援到时";
            this.dgcHelperComeTime.Name = "dgcHelperComeTime";
            this.dgcHelperComeTime.ReadOnly = true;
            this.dgcHelperComeTime.Width = 80;
            // 
            // dgcHelpTimes
            // 
            this.dgcHelpTimes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHelpTimes.HeaderText = "支援时间";
            this.dgcHelpTimes.Name = "dgcHelpTimes";
            this.dgcHelpTimes.ReadOnly = true;
            this.dgcHelpTimes.Width = 80;
            // 
            // dgcHelperName
            // 
            this.dgcHelperName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHelperName.DataPropertyName = "HelperName";
            this.dgcHelperName.HeaderText = "支援者";
            this.dgcHelperName.Name = "dgcHelperName";
            this.dgcHelperName.ReadOnly = true;
            this.dgcHelperName.Width = 80;
            // 
            // dgcErrorReason
            // 
            this.dgcErrorReason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcErrorReason.DataPropertyName = "ErrorReason";
            this.dgcErrorReason.HeaderText = "故障原因";
            this.dgcErrorReason.Name = "dgcErrorReason";
            this.dgcErrorReason.ReadOnly = true;
            this.dgcErrorReason.Width = 80;
            // 
            // dgcFaultContent
            // 
            this.dgcFaultContent.DataPropertyName = "FaultContent";
            this.dgcFaultContent.HeaderText = "故障内容";
            this.dgcFaultContent.Name = "dgcFaultContent";
            this.dgcFaultContent.ReadOnly = true;
            // 
            // dgcSolutionContent
            // 
            this.dgcSolutionContent.DataPropertyName = "SolutionContent";
            this.dgcSolutionContent.HeaderText = "解决方案";
            this.dgcSolutionContent.Name = "dgcSolutionContent";
            this.dgcSolutionContent.ReadOnly = true;
            // 
            // dgcProdCount
            // 
            this.dgcProdCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcProdCount.DataPropertyName = "ProdCount";
            this.dgcProdCount.HeaderText = "制品数";
            this.dgcProdCount.Name = "dgcProdCount";
            this.dgcProdCount.ReadOnly = true;
            this.dgcProdCount.Width = 65;
            // 
            // dgcComeTime
            // 
            this.dgcComeTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcComeTime.DataPropertyName = "ComeTime";
            dataGridViewCellStyle3.Format = "yyyy-MM-dd HH:mm:ss";
            this.dgcComeTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgcComeTime.HeaderText = "到场时间";
            this.dgcComeTime.Name = "dgcComeTime";
            this.dgcComeTime.ReadOnly = true;
            this.dgcComeTime.Width = 130;
            // 
            // dgcSolverName
            // 
            this.dgcSolverName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcSolverName.DataPropertyName = "SolverName";
            this.dgcSolverName.HeaderText = "维护者";
            this.dgcSolverName.Name = "dgcSolverName";
            this.dgcSolverName.ReadOnly = true;
            this.dgcSolverName.Width = 80;
            // 
            // dgcFaultType
            // 
            this.dgcFaultType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFaultType.DataPropertyName = "FaultType";
            this.dgcFaultType.HeaderText = "故障类别";
            this.dgcFaultType.Name = "dgcFaultType";
            this.dgcFaultType.ReadOnly = true;
            this.dgcFaultType.Width = 130;
            // 
            // dgcErrorStatus
            // 
            this.dgcErrorStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcErrorStatus.DataPropertyName = "Status";
            this.dgcErrorStatus.HeaderText = "状态";
            this.dgcErrorStatus.Name = "dgcErrorStatus";
            this.dgcErrorStatus.ReadOnly = true;
            this.dgcErrorStatus.Width = 60;
            // 
            // gbError
            // 
            this.gbError.BackColor = System.Drawing.Color.White;
            this.gbError.Controls.Add(this.cmbArea);
            this.gbError.Controls.Add(this.labArea);
            this.gbError.Controls.Add(this.labErrorStatus);
            this.gbError.Controls.Add(this.cmbErrorStatus);
            this.gbError.Controls.Add(this.tbErrorId);
            this.gbError.Controls.Add(this.cmbMachineType);
            this.gbError.Controls.Add(this.labMachineType);
            this.gbError.Controls.Add(this.cmbErrorHelper);
            this.gbError.Controls.Add(this.cmbErrorHandler);
            this.gbError.Controls.Add(this.labHandler);
            this.gbError.Controls.Add(this.labHelper);
            this.gbError.Controls.Add(this.labErrorMessage);
            this.gbError.Controls.Add(this.nudProdCount);
            this.gbError.Controls.Add(this.labProdCount);
            this.gbError.Controls.Add(this.tbSolutionContent);
            this.gbError.Controls.Add(this.labSolutionContent);
            this.gbError.Controls.Add(this.cmbErrorReason);
            this.gbError.Controls.Add(this.labErrorReason);
            this.gbError.Controls.Add(this.tbFaultContent);
            this.gbError.Controls.Add(this.labFaultContent);
            this.gbError.Controls.Add(this.tbMachineNo);
            this.gbError.Controls.Add(this.dtpComeTimeEnd);
            this.gbError.Controls.Add(this.labComeTimeEnd);
            this.gbError.Controls.Add(this.dtpComeTimeStart);
            this.gbError.Controls.Add(this.labComeTimeStart);
            this.gbError.Controls.Add(this.labMachine);
            this.gbError.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbError.Location = new System.Drawing.Point(15, 35);
            this.gbError.Margin = new System.Windows.Forms.Padding(4);
            this.gbError.Name = "gbError";
            this.gbError.Padding = new System.Windows.Forms.Padding(4);
            this.gbError.Size = new System.Drawing.Size(1570, 280);
            this.gbError.TabIndex = 1;
            this.gbError.TabStop = false;
            this.gbError.Text = "故障信息";
            // 
            // cmbArea
            // 
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Location = new System.Drawing.Point(132, 243);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(257, 23);
            this.cmbArea.TabIndex = 28;
            // 
            // labArea
            // 
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(72, 248);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(52, 15);
            this.labArea.TabIndex = 27;
            this.labArea.Text = "区域：";
            // 
            // labErrorStatus
            // 
            this.labErrorStatus.AutoSize = true;
            this.labErrorStatus.Location = new System.Drawing.Point(72, 209);
            this.labErrorStatus.Name = "labErrorStatus";
            this.labErrorStatus.Size = new System.Drawing.Size(52, 15);
            this.labErrorStatus.TabIndex = 26;
            this.labErrorStatus.Text = "状态：";
            // 
            // cmbErrorStatus
            // 
            this.cmbErrorStatus.FormattingEnabled = true;
            this.cmbErrorStatus.Location = new System.Drawing.Point(132, 205);
            this.cmbErrorStatus.Name = "cmbErrorStatus";
            this.cmbErrorStatus.Size = new System.Drawing.Size(257, 23);
            this.cmbErrorStatus.TabIndex = 25;
            // 
            // tbErrorId
            // 
            this.tbErrorId.Enabled = false;
            this.tbErrorId.Location = new System.Drawing.Point(132, 60);
            this.tbErrorId.Name = "tbErrorId";
            this.tbErrorId.Size = new System.Drawing.Size(257, 25);
            this.tbErrorId.TabIndex = 24;
            this.tbErrorId.Visible = false;
            // 
            // cmbMachineType
            // 
            this.cmbMachineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineType.FormattingEnabled = true;
            this.cmbMachineType.Items.AddRange(new object[] {
            ""});
            this.cmbMachineType.Location = new System.Drawing.Point(132, 147);
            this.cmbMachineType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMachineType.Name = "cmbMachineType";
            this.cmbMachineType.Size = new System.Drawing.Size(257, 23);
            this.cmbMachineType.TabIndex = 23;
            // 
            // labMachineType
            // 
            this.labMachineType.AutoSize = true;
            this.labMachineType.Location = new System.Drawing.Point(42, 151);
            this.labMachineType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMachineType.Name = "labMachineType";
            this.labMachineType.Size = new System.Drawing.Size(82, 15);
            this.labMachineType.TabIndex = 22;
            this.labMachineType.Text = "机台类别：";
            // 
            // cmbErrorHelper
            // 
            this.cmbErrorHelper.FormattingEnabled = true;
            this.cmbErrorHelper.Items.AddRange(new object[] {
            ""});
            this.cmbErrorHelper.Location = new System.Drawing.Point(937, 37);
            this.cmbErrorHelper.Margin = new System.Windows.Forms.Padding(4);
            this.cmbErrorHelper.Name = "cmbErrorHelper";
            this.cmbErrorHelper.Size = new System.Drawing.Size(257, 23);
            this.cmbErrorHelper.TabIndex = 21;
            // 
            // cmbErrorHandler
            // 
            this.cmbErrorHandler.FormattingEnabled = true;
            this.cmbErrorHandler.Items.AddRange(new object[] {
            ""});
            this.cmbErrorHandler.Location = new System.Drawing.Point(541, 37);
            this.cmbErrorHandler.Margin = new System.Windows.Forms.Padding(4);
            this.cmbErrorHandler.Name = "cmbErrorHandler";
            this.cmbErrorHandler.Size = new System.Drawing.Size(257, 23);
            this.cmbErrorHandler.TabIndex = 20;
            // 
            // labHandler
            // 
            this.labHandler.AutoSize = true;
            this.labHandler.Location = new System.Drawing.Point(462, 41);
            this.labHandler.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandler.Name = "labHandler";
            this.labHandler.Size = new System.Drawing.Size(67, 15);
            this.labHandler.TabIndex = 17;
            this.labHandler.Text = "处理者：";
            // 
            // labHelper
            // 
            this.labHelper.AutoSize = true;
            this.labHelper.Location = new System.Drawing.Point(843, 41);
            this.labHelper.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHelper.Name = "labHelper";
            this.labHelper.Size = new System.Drawing.Size(67, 15);
            this.labHelper.TabIndex = 15;
            this.labHelper.Text = "支援者：";
            // 
            // labErrorMessage
            // 
            this.labErrorMessage.AutoSize = true;
            this.labErrorMessage.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labErrorMessage.ForeColor = System.Drawing.Color.Green;
            this.labErrorMessage.Location = new System.Drawing.Point(1304, 43);
            this.labErrorMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labErrorMessage.Name = "labErrorMessage";
            this.labErrorMessage.Size = new System.Drawing.Size(0, 18);
            this.labErrorMessage.TabIndex = 14;
            // 
            // nudProdCount
            // 
            this.nudProdCount.Location = new System.Drawing.Point(541, 91);
            this.nudProdCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudProdCount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudProdCount.Name = "nudProdCount";
            this.nudProdCount.Size = new System.Drawing.Size(257, 25);
            this.nudProdCount.TabIndex = 13;
            // 
            // labProdCount
            // 
            this.labProdCount.AutoSize = true;
            this.labProdCount.Location = new System.Drawing.Point(432, 96);
            this.labProdCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labProdCount.Name = "labProdCount";
            this.labProdCount.Size = new System.Drawing.Size(97, 15);
            this.labProdCount.TabIndex = 12;
            this.labProdCount.Text = "制品跟踪数：";
            // 
            // tbSolutionContent
            // 
            this.tbSolutionContent.Location = new System.Drawing.Point(937, 151);
            this.tbSolutionContent.Margin = new System.Windows.Forms.Padding(4);
            this.tbSolutionContent.Multiline = true;
            this.tbSolutionContent.Name = "tbSolutionContent";
            this.tbSolutionContent.Size = new System.Drawing.Size(257, 115);
            this.tbSolutionContent.TabIndex = 11;
            // 
            // labSolutionContent
            // 
            this.labSolutionContent.AutoSize = true;
            this.labSolutionContent.Location = new System.Drawing.Point(843, 151);
            this.labSolutionContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labSolutionContent.Name = "labSolutionContent";
            this.labSolutionContent.Size = new System.Drawing.Size(82, 15);
            this.labSolutionContent.TabIndex = 10;
            this.labSolutionContent.Text = "解决方案：";
            // 
            // cmbErrorReason
            // 
            this.cmbErrorReason.Location = new System.Drawing.Point(132, 92);
            this.cmbErrorReason.Margin = new System.Windows.Forms.Padding(4);
            this.cmbErrorReason.Name = "cmbErrorReason";
            this.cmbErrorReason.Size = new System.Drawing.Size(257, 23);
            this.cmbErrorReason.TabIndex = 9;
            // 
            // labErrorReason
            // 
            this.labErrorReason.AutoSize = true;
            this.labErrorReason.Location = new System.Drawing.Point(42, 96);
            this.labErrorReason.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labErrorReason.Name = "labErrorReason";
            this.labErrorReason.Size = new System.Drawing.Size(82, 15);
            this.labErrorReason.TabIndex = 8;
            this.labErrorReason.Text = "故障原因：";
            // 
            // tbFaultContent
            // 
            this.tbFaultContent.Location = new System.Drawing.Point(541, 151);
            this.tbFaultContent.Margin = new System.Windows.Forms.Padding(4);
            this.tbFaultContent.Multiline = true;
            this.tbFaultContent.Name = "tbFaultContent";
            this.tbFaultContent.Size = new System.Drawing.Size(257, 115);
            this.tbFaultContent.TabIndex = 7;
            // 
            // labFaultContent
            // 
            this.labFaultContent.AutoSize = true;
            this.labFaultContent.Location = new System.Drawing.Point(447, 151);
            this.labFaultContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFaultContent.Name = "labFaultContent";
            this.labFaultContent.Size = new System.Drawing.Size(82, 15);
            this.labFaultContent.TabIndex = 6;
            this.labFaultContent.Text = "故障内容：";
            // 
            // tbMachineNo
            // 
            this.tbMachineNo.Location = new System.Drawing.Point(132, 36);
            this.tbMachineNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbMachineNo.Name = "tbMachineNo";
            this.tbMachineNo.Size = new System.Drawing.Size(257, 25);
            this.tbMachineNo.TabIndex = 5;
            // 
            // dtpComeTimeEnd
            // 
            this.dtpComeTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpComeTimeEnd.Location = new System.Drawing.Point(1077, 92);
            this.dtpComeTimeEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dtpComeTimeEnd.Name = "dtpComeTimeEnd";
            this.dtpComeTimeEnd.Size = new System.Drawing.Size(115, 25);
            this.dtpComeTimeEnd.TabIndex = 4;
            // 
            // labComeTimeEnd
            // 
            this.labComeTimeEnd.AutoSize = true;
            this.labComeTimeEnd.Location = new System.Drawing.Point(1057, 96);
            this.labComeTimeEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labComeTimeEnd.Name = "labComeTimeEnd";
            this.labComeTimeEnd.Size = new System.Drawing.Size(22, 15);
            this.labComeTimeEnd.TabIndex = 3;
            this.labComeTimeEnd.Text = "至";
            // 
            // dtpComeTimeStart
            // 
            this.dtpComeTimeStart.CustomFormat = "";
            this.dtpComeTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpComeTimeStart.Location = new System.Drawing.Point(937, 91);
            this.dtpComeTimeStart.Margin = new System.Windows.Forms.Padding(4);
            this.dtpComeTimeStart.Name = "dtpComeTimeStart";
            this.dtpComeTimeStart.Size = new System.Drawing.Size(115, 25);
            this.dtpComeTimeStart.TabIndex = 2;
            // 
            // labComeTimeStart
            // 
            this.labComeTimeStart.AutoSize = true;
            this.labComeTimeStart.Location = new System.Drawing.Point(843, 96);
            this.labComeTimeStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labComeTimeStart.Name = "labComeTimeStart";
            this.labComeTimeStart.Size = new System.Drawing.Size(82, 15);
            this.labComeTimeStart.TabIndex = 1;
            this.labComeTimeStart.Text = "到场时间：";
            // 
            // labMachine
            // 
            this.labMachine.AutoSize = true;
            this.labMachine.Location = new System.Drawing.Point(42, 41);
            this.labMachine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMachine.Name = "labMachine";
            this.labMachine.Size = new System.Drawing.Size(82, 15);
            this.labMachine.TabIndex = 0;
            this.labMachine.Text = "机台编号：";
            // 
            // FrmBaseInfoError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.Controls.Add(this.dgvError);
            this.Controls.Add(this.gbError);
            this.Controls.Add(this.menuStripError);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBaseInfoError";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.Text = "信息维护";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseInfoError_Load);
            this.menuStripError.ResumeLayout(false);
            this.menuStripError.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).EndInit();
            this.gbError.ResumeLayout(false);
            this.gbError.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProdCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbError;
        private System.Windows.Forms.Label labComeTimeStart;
        private System.Windows.Forms.Label labMachine;
        private System.Windows.Forms.TextBox tbMachineNo;
        private System.Windows.Forms.DateTimePicker dtpComeTimeEnd;
        private System.Windows.Forms.Label labComeTimeEnd;
        private System.Windows.Forms.DateTimePicker dtpComeTimeStart;
        private System.Windows.Forms.TextBox tbFaultContent;
        private System.Windows.Forms.Label labFaultContent;
        private System.Windows.Forms.ComboBox cmbErrorReason;
        private System.Windows.Forms.Label labErrorReason;
        private System.Windows.Forms.TextBox tbSolutionContent;
        private System.Windows.Forms.Label labSolutionContent;
        private System.Windows.Forms.Label labProdCount;
        private System.Windows.Forms.NumericUpDown nudProdCount;
        private System.Windows.Forms.Label labErrorMessage;
        private System.Windows.Forms.Label labHandler;
        private System.Windows.Forms.Label labHelper;
        private System.Windows.Forms.ComboBox cmbErrorHandler;
        private System.Windows.Forms.ComboBox cmbErrorHelper;
        private System.Windows.Forms.ComboBox cmbMachineType;
        private System.Windows.Forms.Label labMachineType;
        private System.Windows.Forms.MenuStrip menuStripError;
        private System.Windows.Forms.ToolStripMenuItem tsmErrorSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmErrorRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmErrorUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmErrorSave;
        private System.Windows.Forms.ToolStripMenuItem tsmErrorBack;
        private System.Windows.Forms.ToolStripMenuItem tsmErrorExport;
        private System.Windows.Forms.ToolStripMenuItem tsmErrorReset;
        private System.Windows.Forms.TextBox tbErrorId;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvError;
        private System.Windows.Forms.Label labErrorStatus;
        private System.Windows.Forms.ComboBox cmbErrorStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmMsgPushSet;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.ToolStripMenuItem tsmErrorDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcErrorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMachineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWaitedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcSolvedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcErrorHandlerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHelperComeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHelpTimes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHelperName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcErrorReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFaultContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcSolutionContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcProdCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcComeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcSolverName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFaultType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcErrorStatus;
    }
}
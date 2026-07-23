namespace Call
{
    partial class FrmBaseError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseError));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStripError = new System.Windows.Forms.MenuStrip();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMsgPushSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPushSetForTarger = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvError = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMachineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTargetHandlerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWaitedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcSolvedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHelperComeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHelpTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHelperName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcErrorReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFaultContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcSolutionContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcProdCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcPassCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcNGCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcQCName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcSolverName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcComeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFaultType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbError = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labMachine = new System.Windows.Forms.Label();
            this.tbId = new System.Windows.Forms.TextBox();
            this.cmbErrorStatus = new System.Windows.Forms.ComboBox();
            this.dtpComeTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.labComeTimeEnd = new System.Windows.Forms.Label();
            this.labErrorStatus = new System.Windows.Forms.Label();
            this.dtpComeTimeStart = new System.Windows.Forms.DateTimePicker();
            this.tbxTargetHandler = new System.Windows.Forms.TextBox();
            this.labComeTimeStart = new System.Windows.Forms.Label();
            this.labArea = new System.Windows.Forms.Label();
            this.cmbCallType = new System.Windows.Forms.ComboBox();
            this.labTargetHandler = new System.Windows.Forms.Label();
            this.tbMachineNo = new System.Windows.Forms.TextBox();
            this.labCallType = new System.Windows.Forms.Label();
            this.cmbHelper = new System.Windows.Forms.ComboBox();
            this.labHandler = new System.Windows.Forms.Label();
            this.labHelper = new System.Windows.Forms.Label();
            this.cmbHandler = new System.Windows.Forms.ComboBox();
            this.labMachineType = new System.Windows.Forms.Label();
            this.cmbMachineType = new System.Windows.Forms.ComboBox();
            this.labMessage = new System.Windows.Forms.Label();
            this.gbSolution = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labErrorReason = new System.Windows.Forms.Label();
            this.txbSolutionContent = new System.Windows.Forms.TextBox();
            this.nudNGCount = new System.Windows.Forms.NumericUpDown();
            this.labSolutionContent = new System.Windows.Forms.Label();
            this.txbQCName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbErrorReason = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbSolverName = new System.Windows.Forms.TextBox();
            this.labProdCount = new System.Windows.Forms.Label();
            this.nudProdCount = new System.Windows.Forms.NumericUpDown();
            this.nudPassCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labFaultContent = new System.Windows.Forms.Label();
            this.txbFaultContent = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStripError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).BeginInit();
            this.gbError.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbSolution.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNGCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudProdCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassCount)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripError
            // 
            this.menuStripError.BackColor = System.Drawing.Color.Transparent;
            this.menuStripError.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.menuStripError.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripError.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh,
            this.tsmiSelect,
            this.tsmiUpdate,
            this.tsmiDelete,
            this.tsmiSave,
            this.tsmiBack,
            this.tsmiExport,
            this.tsmiReset,
            this.tsmiMsgPushSet,
            this.tsmiPushSetForTarger});
            this.menuStripError.Location = new System.Drawing.Point(15, 0);
            this.menuStripError.Name = "menuStripError";
            this.menuStripError.Size = new System.Drawing.Size(1570, 35);
            this.menuStripError.TabIndex = 4;
            this.menuStripError.Text = "menuStrip2";
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmiRefresh.Image")));
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(86, 31);
            this.tsmiRefresh.Text = "刷新";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // tsmiSelect
            // 
            this.tsmiSelect.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSelect.Image")));
            this.tsmiSelect.Name = "tsmiSelect";
            this.tsmiSelect.Size = new System.Drawing.Size(86, 31);
            this.tsmiSelect.Text = "查询";
            this.tsmiSelect.Click += new System.EventHandler(this.tsmiSelect_Click);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUpdate.Image")));
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(86, 31);
            this.tsmiUpdate.Text = "维护";
            this.tsmiUpdate.Click += new System.EventHandler(this.tsmiUpdate_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDelete.Image")));
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(86, 31);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSave.Image")));
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(86, 31);
            this.tsmiSave.Text = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiBack
            // 
            this.tsmiBack.Image = ((System.Drawing.Image)(resources.GetObject("tsmiBack.Image")));
            this.tsmiBack.Name = "tsmiBack";
            this.tsmiBack.Size = new System.Drawing.Size(86, 31);
            this.tsmiBack.Text = "返回";
            this.tsmiBack.Click += new System.EventHandler(this.tsmiBack_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExport.Image")));
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(86, 31);
            this.tsmiExport.Text = "导出";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // tsmiReset
            // 
            this.tsmiReset.Image = ((System.Drawing.Image)(resources.GetObject("tsmiReset.Image")));
            this.tsmiReset.Name = "tsmiReset";
            this.tsmiReset.Size = new System.Drawing.Size(126, 31);
            this.tsmiReset.Text = "重置看板";
            this.tsmiReset.Click += new System.EventHandler(this.tsmiReset_Click);
            // 
            // tsmiMsgPushSet
            // 
            this.tsmiMsgPushSet.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMsgPushSet.Image")));
            this.tsmiMsgPushSet.Name = "tsmiMsgPushSet";
            this.tsmiMsgPushSet.Size = new System.Drawing.Size(166, 31);
            this.tsmiMsgPushSet.Text = "故障消息设置";
            this.tsmiMsgPushSet.Click += new System.EventHandler(this.tsmiMsgPushSet_Click);
            // 
            // tsmiPushSetForTarger
            // 
            this.tsmiPushSetForTarger.Image = ((System.Drawing.Image)(resources.GetObject("tsmiPushSetForTarger.Image")));
            this.tsmiPushSetForTarger.Name = "tsmiPushSetForTarger";
            this.tsmiPushSetForTarger.Size = new System.Drawing.Size(166, 31);
            this.tsmiPushSetForTarger.Text = "人员消息设置";
            this.tsmiPushSetForTarger.Click += new System.EventHandler(this.tsmiPushSetForTarger_Click);
            // 
            // dgvError
            // 
            this.dgvError.AllowUserToAddRows = false;
            this.dgvError.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.GhostWhite;
            this.dgvError.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvError.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
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
            this.dgcId,
            this.dgcLine,
            this.dgcMachineNo,
            this.dgcTargetHandlerName,
            this.dgcWaitedTime,
            this.dgcSolvedTime,
            this.dgcHandlerName,
            this.dgcHelperComeTime,
            this.dgcHelpTimes,
            this.dgcHelperName,
            this.dgcErrorReason,
            this.dgcFaultContent,
            this.dgcSolutionContent,
            this.dgcProdCount,
            this.dgcPassCount,
            this.dgcNGCount,
            this.dgcQCName,
            this.dgcSolverName,
            this.dgcComeTime,
            this.dgcFaultType,
            this.dgcStatus});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvError.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvError.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvError.Location = new System.Drawing.Point(15, 235);
            this.dgvError.Margin = new System.Windows.Forms.Padding(4);
            this.dgvError.Name = "dgvError";
            this.dgvError.ReadOnly = true;
            this.dgvError.RowHeadersWidth = 51;
            this.dgvError.RowTemplate.Height = 23;
            this.dgvError.Size = new System.Drawing.Size(1570, 550);
            this.dgvError.TabIndex = 3;
            this.dgvError.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvError_CellClick);
            this.dgvError.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvError_CellFormatting);
            // 
            // dgcId
            // 
            this.dgcId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcId.DataPropertyName = "Id";
            this.dgcId.HeaderText = "索引";
            this.dgcId.MinimumWidth = 6;
            this.dgcId.Name = "dgcId";
            this.dgcId.ReadOnly = true;
            this.dgcId.Visible = false;
            this.dgcId.Width = 60;
            // 
            // dgcLine
            // 
            this.dgcLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcLine.DataPropertyName = "Line";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcLine.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgcLine.HeaderText = "产线";
            this.dgcLine.MinimumWidth = 6;
            this.dgcLine.Name = "dgcLine";
            this.dgcLine.ReadOnly = true;
            this.dgcLine.Width = 80;
            // 
            // dgcMachineNo
            // 
            this.dgcMachineNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcMachineNo.DataPropertyName = "Machine";
            this.dgcMachineNo.HeaderText = "机台编号";
            this.dgcMachineNo.MinimumWidth = 6;
            this.dgcMachineNo.Name = "dgcMachineNo";
            this.dgcMachineNo.ReadOnly = true;
            this.dgcMachineNo.Width = 96;
            // 
            // dgcTargetHandlerName
            // 
            this.dgcTargetHandlerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcTargetHandlerName.DataPropertyName = "TargetHandlerName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcTargetHandlerName.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgcTargetHandlerName.HeaderText = "指定人员";
            this.dgcTargetHandlerName.MinimumWidth = 6;
            this.dgcTargetHandlerName.Name = "dgcTargetHandlerName";
            this.dgcTargetHandlerName.ReadOnly = true;
            this.dgcTargetHandlerName.Width = 80;
            // 
            // dgcWaitedTime
            // 
            this.dgcWaitedTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcWaitedTime.DataPropertyName = "WaitedTime";
            this.dgcWaitedTime.HeaderText = "等待时间";
            this.dgcWaitedTime.MinimumWidth = 6;
            this.dgcWaitedTime.Name = "dgcWaitedTime";
            this.dgcWaitedTime.ReadOnly = true;
            this.dgcWaitedTime.Width = 80;
            // 
            // dgcSolvedTime
            // 
            this.dgcSolvedTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcSolvedTime.DataPropertyName = "SolvedTime";
            this.dgcSolvedTime.HeaderText = "处理时间";
            this.dgcSolvedTime.MinimumWidth = 6;
            this.dgcSolvedTime.Name = "dgcSolvedTime";
            this.dgcSolvedTime.ReadOnly = true;
            this.dgcSolvedTime.Width = 80;
            // 
            // dgcHandlerName
            // 
            this.dgcHandlerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHandlerName.DataPropertyName = "HandlerName";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcHandlerName.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgcHandlerName.HeaderText = "处理者";
            this.dgcHandlerName.MinimumWidth = 6;
            this.dgcHandlerName.Name = "dgcHandlerName";
            this.dgcHandlerName.ReadOnly = true;
            this.dgcHandlerName.Width = 80;
            // 
            // dgcHelperComeTime
            // 
            this.dgcHelperComeTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHelperComeTime.HeaderText = "支援到时";
            this.dgcHelperComeTime.MinimumWidth = 6;
            this.dgcHelperComeTime.Name = "dgcHelperComeTime";
            this.dgcHelperComeTime.ReadOnly = true;
            this.dgcHelperComeTime.Width = 80;
            // 
            // dgcHelpTimes
            // 
            this.dgcHelpTimes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHelpTimes.HeaderText = "支援时间";
            this.dgcHelpTimes.MinimumWidth = 6;
            this.dgcHelpTimes.Name = "dgcHelpTimes";
            this.dgcHelpTimes.ReadOnly = true;
            this.dgcHelpTimes.Width = 80;
            // 
            // dgcHelperName
            // 
            this.dgcHelperName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHelperName.DataPropertyName = "HelperName";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcHelperName.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgcHelperName.HeaderText = "支援者";
            this.dgcHelperName.MinimumWidth = 6;
            this.dgcHelperName.Name = "dgcHelperName";
            this.dgcHelperName.ReadOnly = true;
            this.dgcHelperName.Width = 80;
            // 
            // dgcErrorReason
            // 
            this.dgcErrorReason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcErrorReason.DataPropertyName = "ErrorReason";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcErrorReason.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgcErrorReason.HeaderText = "故障原因";
            this.dgcErrorReason.MinimumWidth = 6;
            this.dgcErrorReason.Name = "dgcErrorReason";
            this.dgcErrorReason.ReadOnly = true;
            this.dgcErrorReason.Width = 80;
            // 
            // dgcFaultContent
            // 
            this.dgcFaultContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFaultContent.DataPropertyName = "FaultContent";
            this.dgcFaultContent.HeaderText = "故障内容";
            this.dgcFaultContent.MinimumWidth = 6;
            this.dgcFaultContent.Name = "dgcFaultContent";
            this.dgcFaultContent.ReadOnly = true;
            this.dgcFaultContent.Width = 120;
            // 
            // dgcSolutionContent
            // 
            this.dgcSolutionContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcSolutionContent.DataPropertyName = "SolutionContent";
            this.dgcSolutionContent.HeaderText = "解决方案";
            this.dgcSolutionContent.MinimumWidth = 6;
            this.dgcSolutionContent.Name = "dgcSolutionContent";
            this.dgcSolutionContent.ReadOnly = true;
            this.dgcSolutionContent.Width = 200;
            // 
            // dgcProdCount
            // 
            this.dgcProdCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcProdCount.DataPropertyName = "ProdCount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgcProdCount.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgcProdCount.HeaderText = "调机品数";
            this.dgcProdCount.MinimumWidth = 6;
            this.dgcProdCount.Name = "dgcProdCount";
            this.dgcProdCount.ReadOnly = true;
            this.dgcProdCount.Width = 65;
            // 
            // dgcPassCount
            // 
            this.dgcPassCount.DataPropertyName = "PassCount";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgcPassCount.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgcPassCount.HeaderText = "良品";
            this.dgcPassCount.MinimumWidth = 6;
            this.dgcPassCount.Name = "dgcPassCount";
            this.dgcPassCount.ReadOnly = true;
            this.dgcPassCount.Width = 66;
            // 
            // dgcNGCount
            // 
            this.dgcNGCount.DataPropertyName = "NGCount";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgcNGCount.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgcNGCount.HeaderText = "不良品";
            this.dgcNGCount.MinimumWidth = 6;
            this.dgcNGCount.Name = "dgcNGCount";
            this.dgcNGCount.ReadOnly = true;
            this.dgcNGCount.Width = 81;
            // 
            // dgcQCName
            // 
            this.dgcQCName.DataPropertyName = "QCName";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcQCName.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgcQCName.HeaderText = "品管";
            this.dgcQCName.MinimumWidth = 6;
            this.dgcQCName.Name = "dgcQCName";
            this.dgcQCName.ReadOnly = true;
            this.dgcQCName.Width = 66;
            // 
            // dgcSolverName
            // 
            this.dgcSolverName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcSolverName.DataPropertyName = "SolverName";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcSolverName.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgcSolverName.HeaderText = "维护者";
            this.dgcSolverName.MinimumWidth = 6;
            this.dgcSolverName.Name = "dgcSolverName";
            this.dgcSolverName.ReadOnly = true;
            this.dgcSolverName.Width = 80;
            // 
            // dgcComeTime
            // 
            this.dgcComeTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcComeTime.DataPropertyName = "ComeTime";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Format = "yyyy-MM-dd HH:mm:ss";
            this.dgcComeTime.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgcComeTime.HeaderText = "到场时间";
            this.dgcComeTime.MinimumWidth = 6;
            this.dgcComeTime.Name = "dgcComeTime";
            this.dgcComeTime.ReadOnly = true;
            this.dgcComeTime.Width = 130;
            // 
            // dgcFaultType
            // 
            this.dgcFaultType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFaultType.DataPropertyName = "FaultType";
            this.dgcFaultType.HeaderText = "故障类别";
            this.dgcFaultType.MinimumWidth = 6;
            this.dgcFaultType.Name = "dgcFaultType";
            this.dgcFaultType.ReadOnly = true;
            this.dgcFaultType.Width = 80;
            // 
            // dgcStatus
            // 
            this.dgcStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcStatus.DataPropertyName = "Status";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcStatus.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgcStatus.HeaderText = "状态";
            this.dgcStatus.MinimumWidth = 6;
            this.dgcStatus.Name = "dgcStatus";
            this.dgcStatus.ReadOnly = true;
            this.dgcStatus.Width = 60;
            // 
            // gbError
            // 
            this.gbError.AutoSize = true;
            this.gbError.BackColor = System.Drawing.Color.Snow;
            this.gbError.Controls.Add(this.tableLayoutPanel1);
            this.gbError.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbError.Location = new System.Drawing.Point(3, 3);
            this.gbError.Name = "gbError";
            this.gbError.Size = new System.Drawing.Size(724, 164);
            this.gbError.TabIndex = 1;
            this.gbError.TabStop = false;
            this.gbError.Text = "故障信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.Controls.Add(this.labMachine, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbId, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbErrorStatus, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpComeTimeEnd, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbArea, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labComeTimeEnd, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.labErrorStatus, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpComeTimeStart, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbxTargetHandler, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.labComeTimeStart, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labArea, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbCallType, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labTargetHandler, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbMachineNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labCallType, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbHelper, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.labHandler, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.labHelper, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbHandler, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMachineType, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbMachineType, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(718, 140);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // labMachine
            // 
            this.labMachine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachine.AutoSize = true;
            this.labMachine.Location = new System.Drawing.Point(9, 10);
            this.labMachine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMachine.Name = "labMachine";
            this.labMachine.Size = new System.Drawing.Size(67, 15);
            this.labMachine.TabIndex = 0;
            this.labMachine.Text = "机台编号";
            // 
            // tbId
            // 
            this.tbId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbId.Enabled = false;
            this.tbId.Location = new System.Drawing.Point(563, 110);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(144, 25);
            this.tbId.TabIndex = 24;
            this.tbId.Visible = false;
            // 
            // cmbErrorStatus
            // 
            this.cmbErrorStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbErrorStatus.FormattingEnabled = true;
            this.cmbErrorStatus.Location = new System.Drawing.Point(323, 76);
            this.cmbErrorStatus.Name = "cmbErrorStatus";
            this.cmbErrorStatus.Size = new System.Drawing.Size(144, 23);
            this.cmbErrorStatus.TabIndex = 25;
            // 
            // dtpComeTimeEnd
            // 
            this.dtpComeTimeEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpComeTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpComeTimeEnd.Location = new System.Drawing.Point(324, 110);
            this.dtpComeTimeEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dtpComeTimeEnd.Name = "dtpComeTimeEnd";
            this.dtpComeTimeEnd.Size = new System.Drawing.Size(142, 25);
            this.dtpComeTimeEnd.TabIndex = 4;
            // 
            // cmbArea
            // 
            this.cmbArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Location = new System.Drawing.Point(83, 76);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(144, 23);
            this.cmbArea.TabIndex = 28;
            // 
            // labComeTimeEnd
            // 
            this.labComeTimeEnd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labComeTimeEnd.AutoSize = true;
            this.labComeTimeEnd.Location = new System.Drawing.Point(294, 115);
            this.labComeTimeEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labComeTimeEnd.Name = "labComeTimeEnd";
            this.labComeTimeEnd.Size = new System.Drawing.Size(22, 15);
            this.labComeTimeEnd.TabIndex = 3;
            this.labComeTimeEnd.Text = "至";
            // 
            // labErrorStatus
            // 
            this.labErrorStatus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labErrorStatus.AutoSize = true;
            this.labErrorStatus.Location = new System.Drawing.Point(280, 80);
            this.labErrorStatus.Name = "labErrorStatus";
            this.labErrorStatus.Size = new System.Drawing.Size(37, 15);
            this.labErrorStatus.TabIndex = 26;
            this.labErrorStatus.Text = "状态";
            // 
            // dtpComeTimeStart
            // 
            this.dtpComeTimeStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpComeTimeStart.CustomFormat = "";
            this.dtpComeTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpComeTimeStart.Location = new System.Drawing.Point(84, 110);
            this.dtpComeTimeStart.Margin = new System.Windows.Forms.Padding(4);
            this.dtpComeTimeStart.Name = "dtpComeTimeStart";
            this.dtpComeTimeStart.Size = new System.Drawing.Size(142, 25);
            this.dtpComeTimeStart.TabIndex = 2;
            // 
            // tbxTargetHandler
            // 
            this.tbxTargetHandler.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxTargetHandler.Location = new System.Drawing.Point(323, 40);
            this.tbxTargetHandler.Name = "tbxTargetHandler";
            this.tbxTargetHandler.Size = new System.Drawing.Size(144, 25);
            this.tbxTargetHandler.TabIndex = 31;
            // 
            // labComeTimeStart
            // 
            this.labComeTimeStart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labComeTimeStart.AutoSize = true;
            this.labComeTimeStart.Location = new System.Drawing.Point(9, 115);
            this.labComeTimeStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labComeTimeStart.Name = "labComeTimeStart";
            this.labComeTimeStart.Size = new System.Drawing.Size(67, 15);
            this.labComeTimeStart.TabIndex = 1;
            this.labComeTimeStart.Text = "到场时间";
            // 
            // labArea
            // 
            this.labArea.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(40, 80);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(37, 15);
            this.labArea.TabIndex = 27;
            this.labArea.Text = "区域";
            // 
            // cmbCallType
            // 
            this.cmbCallType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbCallType.FormattingEnabled = true;
            this.cmbCallType.Items.AddRange(new object[] {
            "",
            "机台模组",
            "部门人员"});
            this.cmbCallType.Location = new System.Drawing.Point(323, 6);
            this.cmbCallType.Name = "cmbCallType";
            this.cmbCallType.Size = new System.Drawing.Size(144, 23);
            this.cmbCallType.TabIndex = 33;
            // 
            // labTargetHandler
            // 
            this.labTargetHandler.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTargetHandler.AutoSize = true;
            this.labTargetHandler.Location = new System.Drawing.Point(250, 45);
            this.labTargetHandler.Name = "labTargetHandler";
            this.labTargetHandler.Size = new System.Drawing.Size(67, 15);
            this.labTargetHandler.TabIndex = 30;
            this.labTargetHandler.Text = "指定人员";
            // 
            // tbMachineNo
            // 
            this.tbMachineNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbMachineNo.Location = new System.Drawing.Point(84, 5);
            this.tbMachineNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbMachineNo.Name = "tbMachineNo";
            this.tbMachineNo.Size = new System.Drawing.Size(142, 25);
            this.tbMachineNo.TabIndex = 5;
            // 
            // labCallType
            // 
            this.labCallType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labCallType.AutoSize = true;
            this.labCallType.Location = new System.Drawing.Point(250, 10);
            this.labCallType.Name = "labCallType";
            this.labCallType.Size = new System.Drawing.Size(67, 15);
            this.labCallType.TabIndex = 32;
            this.labCallType.Text = "呼叫类型";
            // 
            // cmbHelper
            // 
            this.cmbHelper.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbHelper.FormattingEnabled = true;
            this.cmbHelper.Items.AddRange(new object[] {
            ""});
            this.cmbHelper.Location = new System.Drawing.Point(564, 41);
            this.cmbHelper.Margin = new System.Windows.Forms.Padding(4);
            this.cmbHelper.Name = "cmbHelper";
            this.cmbHelper.Size = new System.Drawing.Size(142, 23);
            this.cmbHelper.TabIndex = 21;
            // 
            // labHandler
            // 
            this.labHandler.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labHandler.AutoSize = true;
            this.labHandler.Location = new System.Drawing.Point(504, 10);
            this.labHandler.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandler.Name = "labHandler";
            this.labHandler.Size = new System.Drawing.Size(52, 15);
            this.labHandler.TabIndex = 17;
            this.labHandler.Text = "处理者";
            // 
            // labHelper
            // 
            this.labHelper.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labHelper.AutoSize = true;
            this.labHelper.Location = new System.Drawing.Point(504, 45);
            this.labHelper.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHelper.Name = "labHelper";
            this.labHelper.Size = new System.Drawing.Size(52, 15);
            this.labHelper.TabIndex = 15;
            this.labHelper.Text = "支援者";
            // 
            // cmbHandler
            // 
            this.cmbHandler.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbHandler.FormattingEnabled = true;
            this.cmbHandler.Items.AddRange(new object[] {
            ""});
            this.cmbHandler.Location = new System.Drawing.Point(564, 6);
            this.cmbHandler.Margin = new System.Windows.Forms.Padding(4);
            this.cmbHandler.Name = "cmbHandler";
            this.cmbHandler.Size = new System.Drawing.Size(142, 23);
            this.cmbHandler.TabIndex = 20;
            // 
            // labMachineType
            // 
            this.labMachineType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachineType.AutoSize = true;
            this.labMachineType.Location = new System.Drawing.Point(9, 45);
            this.labMachineType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMachineType.Name = "labMachineType";
            this.labMachineType.Size = new System.Drawing.Size(67, 15);
            this.labMachineType.TabIndex = 22;
            this.labMachineType.Text = "机台类别";
            // 
            // cmbMachineType
            // 
            this.cmbMachineType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMachineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineType.FormattingEnabled = true;
            this.cmbMachineType.Items.AddRange(new object[] {
            ""});
            this.cmbMachineType.Location = new System.Drawing.Point(84, 41);
            this.cmbMachineType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMachineType.Name = "cmbMachineType";
            this.cmbMachineType.Size = new System.Drawing.Size(142, 23);
            this.cmbMachineType.TabIndex = 23;
            // 
            // labMessage
            // 
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(15, 205);
            this.labMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(1570, 30);
            this.labMessage.TabIndex = 14;
            this.labMessage.Text = "提示:";
            // 
            // gbSolution
            // 
            this.gbSolution.AutoSize = true;
            this.gbSolution.BackColor = System.Drawing.Color.Honeydew;
            this.gbSolution.Controls.Add(this.tableLayoutPanel2);
            this.gbSolution.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSolution.Location = new System.Drawing.Point(733, 3);
            this.gbSolution.Name = "gbSolution";
            this.gbSolution.Size = new System.Drawing.Size(834, 164);
            this.gbSolution.TabIndex = 29;
            this.gbSolution.TabStop = false;
            this.gbSolution.Text = "解决方案";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.labErrorReason, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txbSolutionContent, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.nudNGCount, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.labSolutionContent, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.txbQCName, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmbErrorReason, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txbSolverName, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labProdCount, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.nudProdCount, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.nudPassCount, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.labFaultContent, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.txbFaultContent, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(828, 140);
            this.tableLayoutPanel2.TabIndex = 36;
            // 
            // labErrorReason
            // 
            this.labErrorReason.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labErrorReason.AutoSize = true;
            this.labErrorReason.Location = new System.Drawing.Point(9, 10);
            this.labErrorReason.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labErrorReason.Name = "labErrorReason";
            this.labErrorReason.Size = new System.Drawing.Size(67, 15);
            this.labErrorReason.TabIndex = 8;
            this.labErrorReason.Text = "故障原因";
            // 
            // txbSolutionContent
            // 
            this.txbSolutionContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbSolutionContent.Location = new System.Drawing.Point(484, 74);
            this.txbSolutionContent.Margin = new System.Windows.Forms.Padding(4);
            this.txbSolutionContent.Multiline = true;
            this.txbSolutionContent.Name = "txbSolutionContent";
            this.tableLayoutPanel2.SetRowSpan(this.txbSolutionContent, 2);
            this.txbSolutionContent.Size = new System.Drawing.Size(340, 62);
            this.txbSolutionContent.TabIndex = 11;
            // 
            // nudNGCount
            // 
            this.nudNGCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudNGCount.Location = new System.Drawing.Point(324, 75);
            this.nudNGCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudNGCount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudNGCount.Name = "nudNGCount";
            this.nudNGCount.Size = new System.Drawing.Size(60, 25);
            this.nudNGCount.TabIndex = 17;
            // 
            // labSolutionContent
            // 
            this.labSolutionContent.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labSolutionContent.AutoSize = true;
            this.labSolutionContent.Location = new System.Drawing.Point(409, 80);
            this.labSolutionContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labSolutionContent.Name = "labSolutionContent";
            this.labSolutionContent.Size = new System.Drawing.Size(67, 15);
            this.labSolutionContent.TabIndex = 10;
            this.labSolutionContent.Text = "解决方案";
            // 
            // txbQCName
            // 
            this.txbQCName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbQCName.Location = new System.Drawing.Point(83, 75);
            this.txbQCName.Name = "txbQCName";
            this.txbQCName.Size = new System.Drawing.Size(150, 25);
            this.txbQCName.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "不良品数";
            // 
            // cmbErrorReason
            // 
            this.cmbErrorReason.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbErrorReason.Location = new System.Drawing.Point(84, 6);
            this.cmbErrorReason.Margin = new System.Windows.Forms.Padding(4);
            this.cmbErrorReason.Name = "cmbErrorReason";
            this.cmbErrorReason.Size = new System.Drawing.Size(149, 23);
            this.cmbErrorReason.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 80);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "确认品管";
            // 
            // txbSolverName
            // 
            this.txbSolverName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbSolverName.Location = new System.Drawing.Point(83, 40);
            this.txbSolverName.Name = "txbSolverName";
            this.txbSolverName.Size = new System.Drawing.Size(150, 25);
            this.txbSolverName.TabIndex = 34;
            // 
            // labProdCount
            // 
            this.labProdCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labProdCount.AutoSize = true;
            this.labProdCount.Location = new System.Drawing.Point(249, 10);
            this.labProdCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labProdCount.Name = "labProdCount";
            this.labProdCount.Size = new System.Drawing.Size(67, 15);
            this.labProdCount.TabIndex = 12;
            this.labProdCount.Text = "调机品数";
            // 
            // nudProdCount
            // 
            this.nudProdCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudProdCount.Location = new System.Drawing.Point(324, 5);
            this.nudProdCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudProdCount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudProdCount.Name = "nudProdCount";
            this.nudProdCount.Size = new System.Drawing.Size(60, 25);
            this.nudProdCount.TabIndex = 13;
            // 
            // nudPassCount
            // 
            this.nudPassCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudPassCount.Location = new System.Drawing.Point(324, 40);
            this.nudPassCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudPassCount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudPassCount.Name = "nudPassCount";
            this.nudPassCount.Size = new System.Drawing.Size(60, 25);
            this.nudPassCount.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "维护人";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "良品数";
            // 
            // labFaultContent
            // 
            this.labFaultContent.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labFaultContent.AutoSize = true;
            this.labFaultContent.Location = new System.Drawing.Point(409, 10);
            this.labFaultContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFaultContent.Name = "labFaultContent";
            this.labFaultContent.Size = new System.Drawing.Size(67, 15);
            this.labFaultContent.TabIndex = 6;
            this.labFaultContent.Text = "故障内容";
            // 
            // txbFaultContent
            // 
            this.txbFaultContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbFaultContent.Location = new System.Drawing.Point(484, 4);
            this.txbFaultContent.Margin = new System.Windows.Forms.Padding(4);
            this.txbFaultContent.Multiline = true;
            this.txbFaultContent.Name = "txbFaultContent";
            this.tableLayoutPanel2.SetRowSpan(this.txbFaultContent, 2);
            this.txbFaultContent.Size = new System.Drawing.Size(340, 62);
            this.txbFaultContent.TabIndex = 7;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 730F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.gbError, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.gbSolution, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(15, 35);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1570, 170);
            this.tableLayoutPanel3.TabIndex = 15;
            // 
            // FrmBaseError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.Controls.Add(this.dgvError);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.menuStripError);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBaseError";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.Text = "异常信息记录";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseError_Load);
            this.menuStripError.ResumeLayout(false);
            this.menuStripError.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).EndInit();
            this.gbError.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbSolution.ResumeLayout(false);
            this.gbSolution.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNGCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudProdCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassCount)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
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
        private System.Windows.Forms.TextBox txbFaultContent;
        private System.Windows.Forms.Label labFaultContent;
        private System.Windows.Forms.ComboBox cmbErrorReason;
        private System.Windows.Forms.Label labErrorReason;
        private System.Windows.Forms.TextBox txbSolutionContent;
        private System.Windows.Forms.Label labSolutionContent;
        private System.Windows.Forms.Label labProdCount;
        private System.Windows.Forms.NumericUpDown nudProdCount;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.Label labHandler;
        private System.Windows.Forms.Label labHelper;
        private System.Windows.Forms.ComboBox cmbHandler;
        private System.Windows.Forms.ComboBox cmbHelper;
        private System.Windows.Forms.ComboBox cmbMachineType;
        private System.Windows.Forms.Label labMachineType;
        private System.Windows.Forms.MenuStrip menuStripError;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiReset;
        private System.Windows.Forms.TextBox tbId;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvError;
        private System.Windows.Forms.Label labErrorStatus;
        private System.Windows.Forms.ComboBox cmbErrorStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiMsgPushSet;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.GroupBox gbSolution;
        private System.Windows.Forms.Label labTargetHandler;
        private System.Windows.Forms.TextBox tbxTargetHandler;
        private System.Windows.Forms.ComboBox cmbCallType;
        private System.Windows.Forms.Label labCallType;
        private System.Windows.Forms.ToolStripMenuItem tsmiPushSetForTarger;
        private System.Windows.Forms.NumericUpDown nudNGCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPassCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbQCName;
        private System.Windows.Forms.TextBox txbSolverName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMachineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTargetHandlerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWaitedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcSolvedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHelperComeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHelpTimes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHelperName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcErrorReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFaultContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcSolutionContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcProdCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPassCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcNGCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcQCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcSolverName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcComeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFaultType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}
namespace CallSys
{
    partial class FrmBaseInfoSolution
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseInfoSolution));
            this.dgvFaultSolution = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcMachineType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFaultType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMaxHandleTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMaxHelpTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAutoHelpFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFaultItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcSolutionItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcValidity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcImitateSound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbSolution = new System.Windows.Forms.GroupBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.tbImitateSound = new System.Windows.Forms.TextBox();
            this.labImitateSound = new System.Windows.Forms.Label();
            this.cmbFaultType = new System.Windows.Forms.ComboBox();
            this.nudMaxHelpTimes = new System.Windows.Forms.NumericUpDown();
            this.nudMaxHandleTimes = new System.Windows.Forms.NumericUpDown();
            this.btnSolutionItemsDel = new System.Windows.Forms.Button();
            this.btnSolutionItemsAdd = new System.Windows.Forms.Button();
            this.btnFaultItemsDel = new System.Windows.Forms.Button();
            this.btnFaultItemsAdd = new System.Windows.Forms.Button();
            this.chkAutoHelpFlag = new System.Windows.Forms.CheckBox();
            this.lsbSolutionItems = new System.Windows.Forms.ListBox();
            this.lsbFaultItems = new System.Windows.Forms.ListBox();
            this.tbMachineType = new System.Windows.Forms.TextBox();
            this.labMessage = new System.Windows.Forms.Label();
            this.labFaultCount = new System.Windows.Forms.Label();
            this.labSolutionCount = new System.Windows.Forms.Label();
            this.labSolutionItems = new System.Windows.Forms.Label();
            this.labFaultType = new System.Windows.Forms.Label();
            this.labFaultItems = new System.Windows.Forms.Label();
            this.labMaxHelpTime = new System.Windows.Forms.Label();
            this.labMaxHandleTime = new System.Windows.Forms.Label();
            this.labMachineType = new System.Windows.Forms.Label();
            this.menuStripSolution = new System.Windows.Forms.MenuStrip();
            this.tsmSolutionRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSolutionAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSolutionUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSolutionSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSolutionDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSolutionBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSolutionImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSolutionExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLineMachine = new System.Windows.Forms.ToolStripMenuItem();
            this.tbFaultItem = new System.Windows.Forms.TextBox();
            this.tbSolutionItem = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultSolution)).BeginInit();
            this.gbSolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHelpTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHandleTimes)).BeginInit();
            this.menuStripSolution.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFaultSolution
            // 
            this.dgvFaultSolution.AllowUserToAddRows = false;
            this.dgvFaultSolution.AllowUserToDeleteRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.GhostWhite;
            this.dgvFaultSolution.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvFaultSolution.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFaultSolution.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvFaultSolution.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFaultSolution.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcMachineType,
            this.dgcFaultType,
            this.dgcMaxHandleTimes,
            this.dgcMaxHelpTimes,
            this.dgcAutoHelpFlag,
            this.dgcFaultItems,
            this.dgcSolutionItems,
            this.dgcValidity,
            this.dgcImitateSound});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFaultSolution.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvFaultSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFaultSolution.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvFaultSolution.Location = new System.Drawing.Point(15, 217);
            this.dgvFaultSolution.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFaultSolution.Name = "dgvFaultSolution";
            this.dgvFaultSolution.ReadOnly = true;
            this.dgvFaultSolution.RowTemplate.Height = 27;
            this.dgvFaultSolution.Size = new System.Drawing.Size(1570, 568);
            this.dgvFaultSolution.TabIndex = 2;
            this.dgvFaultSolution.VirtualMode = true;
            this.dgvFaultSolution.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFaultSolution_CellClick);
            this.dgvFaultSolution.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvFaultSolution_CellFormatting);
            // 
            // dgcMachineType
            // 
            this.dgcMachineType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcMachineType.DataPropertyName = "MachineType";
            this.dgcMachineType.HeaderText = "机台类型";
            this.dgcMachineType.Name = "dgcMachineType";
            this.dgcMachineType.ReadOnly = true;
            this.dgcMachineType.Width = 224;
            // 
            // dgcFaultType
            // 
            this.dgcFaultType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFaultType.DataPropertyName = "FaultType";
            this.dgcFaultType.HeaderText = "故障类型";
            this.dgcFaultType.Name = "dgcFaultType";
            this.dgcFaultType.ReadOnly = true;
            this.dgcFaultType.Width = 150;
            // 
            // dgcMaxHandleTimes
            // 
            this.dgcMaxHandleTimes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcMaxHandleTimes.DataPropertyName = "MaxHandleTimes";
            this.dgcMaxHandleTimes.HeaderText = "最大处理时间(分钟)";
            this.dgcMaxHandleTimes.Name = "dgcMaxHandleTimes";
            this.dgcMaxHandleTimes.ReadOnly = true;
            this.dgcMaxHandleTimes.Width = 180;
            // 
            // dgcMaxHelpTimes
            // 
            this.dgcMaxHelpTimes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcMaxHelpTimes.DataPropertyName = "MaxHelpTimes";
            this.dgcMaxHelpTimes.HeaderText = "最大支援时间(分钟)";
            this.dgcMaxHelpTimes.Name = "dgcMaxHelpTimes";
            this.dgcMaxHelpTimes.ReadOnly = true;
            this.dgcMaxHelpTimes.Width = 180;
            // 
            // dgcAutoHelpFlag
            // 
            this.dgcAutoHelpFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAutoHelpFlag.DataPropertyName = "AutoHelpFlag";
            this.dgcAutoHelpFlag.HeaderText = "自动支援";
            this.dgcAutoHelpFlag.Name = "dgcAutoHelpFlag";
            this.dgcAutoHelpFlag.ReadOnly = true;
            // 
            // dgcFaultItems
            // 
            this.dgcFaultItems.DataPropertyName = "FaultItems";
            this.dgcFaultItems.HeaderText = "故障内容";
            this.dgcFaultItems.Name = "dgcFaultItems";
            this.dgcFaultItems.ReadOnly = true;
            // 
            // dgcSolutionItems
            // 
            this.dgcSolutionItems.DataPropertyName = "SolutionItems";
            this.dgcSolutionItems.HeaderText = "解决方案";
            this.dgcSolutionItems.Name = "dgcSolutionItems";
            this.dgcSolutionItems.ReadOnly = true;
            // 
            // dgcValidity
            // 
            this.dgcValidity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcValidity.DataPropertyName = "Validity";
            this.dgcValidity.HeaderText = "校验状态";
            this.dgcValidity.Name = "dgcValidity";
            this.dgcValidity.ReadOnly = true;
            // 
            // dgcImitateSound
            // 
            this.dgcImitateSound.DataPropertyName = "ImitateSound";
            this.dgcImitateSound.HeaderText = "模拟声音";
            this.dgcImitateSound.Name = "dgcImitateSound";
            this.dgcImitateSound.ReadOnly = true;
            // 
            // gbSolution
            // 
            this.gbSolution.BackColor = System.Drawing.Color.White;
            this.gbSolution.Controls.Add(this.tbSolutionItem);
            this.gbSolution.Controls.Add(this.tbFaultItem);
            this.gbSolution.Controls.Add(this.btnPlay);
            this.gbSolution.Controls.Add(this.tbImitateSound);
            this.gbSolution.Controls.Add(this.labImitateSound);
            this.gbSolution.Controls.Add(this.cmbFaultType);
            this.gbSolution.Controls.Add(this.nudMaxHelpTimes);
            this.gbSolution.Controls.Add(this.nudMaxHandleTimes);
            this.gbSolution.Controls.Add(this.btnSolutionItemsDel);
            this.gbSolution.Controls.Add(this.btnSolutionItemsAdd);
            this.gbSolution.Controls.Add(this.btnFaultItemsDel);
            this.gbSolution.Controls.Add(this.btnFaultItemsAdd);
            this.gbSolution.Controls.Add(this.chkAutoHelpFlag);
            this.gbSolution.Controls.Add(this.lsbSolutionItems);
            this.gbSolution.Controls.Add(this.lsbFaultItems);
            this.gbSolution.Controls.Add(this.tbMachineType);
            this.gbSolution.Controls.Add(this.labMessage);
            this.gbSolution.Controls.Add(this.labFaultCount);
            this.gbSolution.Controls.Add(this.labSolutionCount);
            this.gbSolution.Controls.Add(this.labSolutionItems);
            this.gbSolution.Controls.Add(this.labFaultType);
            this.gbSolution.Controls.Add(this.labFaultItems);
            this.gbSolution.Controls.Add(this.labMaxHelpTime);
            this.gbSolution.Controls.Add(this.labMaxHandleTime);
            this.gbSolution.Controls.Add(this.labMachineType);
            this.gbSolution.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSolution.Location = new System.Drawing.Point(15, 35);
            this.gbSolution.Name = "gbSolution";
            this.gbSolution.Size = new System.Drawing.Size(1570, 182);
            this.gbSolution.TabIndex = 1;
            this.gbSolution.TabStop = false;
            this.gbSolution.Text = "方案信息";
            // 
            // btnPlay
            // 
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(306, 106);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(30, 30);
            this.btnPlay.TabIndex = 25;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // tbImitateSound
            // 
            this.tbImitateSound.Location = new System.Drawing.Point(119, 109);
            this.tbImitateSound.Name = "tbImitateSound";
            this.tbImitateSound.Size = new System.Drawing.Size(180, 25);
            this.tbImitateSound.TabIndex = 24;
            // 
            // labImitateSound
            // 
            this.labImitateSound.AutoSize = true;
            this.labImitateSound.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labImitateSound.Location = new System.Drawing.Point(46, 114);
            this.labImitateSound.Name = "labImitateSound";
            this.labImitateSound.Size = new System.Drawing.Size(67, 15);
            this.labImitateSound.TabIndex = 23;
            this.labImitateSound.Text = "模拟声音";
            // 
            // cmbFaultType
            // 
            this.cmbFaultType.Location = new System.Drawing.Point(119, 28);
            this.cmbFaultType.Name = "cmbFaultType";
            this.cmbFaultType.Size = new System.Drawing.Size(180, 23);
            this.cmbFaultType.TabIndex = 22;
            // 
            // nudMaxHelpTimes
            // 
            this.nudMaxHelpTimes.Location = new System.Drawing.Point(451, 68);
            this.nudMaxHelpTimes.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudMaxHelpTimes.Name = "nudMaxHelpTimes";
            this.nudMaxHelpTimes.Size = new System.Drawing.Size(100, 25);
            this.nudMaxHelpTimes.TabIndex = 20;
            // 
            // nudMaxHandleTimes
            // 
            this.nudMaxHandleTimes.Location = new System.Drawing.Point(451, 27);
            this.nudMaxHandleTimes.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudMaxHandleTimes.Name = "nudMaxHandleTimes";
            this.nudMaxHandleTimes.Size = new System.Drawing.Size(100, 25);
            this.nudMaxHandleTimes.TabIndex = 19;
            // 
            // btnSolutionItemsDel
            // 
            this.btnSolutionItemsDel.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSolutionItemsDel.Location = new System.Drawing.Point(1489, 60);
            this.btnSolutionItemsDel.Name = "btnSolutionItemsDel";
            this.btnSolutionItemsDel.Size = new System.Drawing.Size(30, 30);
            this.btnSolutionItemsDel.TabIndex = 18;
            this.btnSolutionItemsDel.Text = "－";
            this.btnSolutionItemsDel.UseVisualStyleBackColor = true;
            this.btnSolutionItemsDel.Click += new System.EventHandler(this.btnSolutionItemsDel_Click);
            // 
            // btnSolutionItemsAdd
            // 
            this.btnSolutionItemsAdd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSolutionItemsAdd.Location = new System.Drawing.Point(1489, 25);
            this.btnSolutionItemsAdd.Name = "btnSolutionItemsAdd";
            this.btnSolutionItemsAdd.Size = new System.Drawing.Size(30, 30);
            this.btnSolutionItemsAdd.TabIndex = 17;
            this.btnSolutionItemsAdd.Text = "＋";
            this.btnSolutionItemsAdd.UseVisualStyleBackColor = true;
            this.btnSolutionItemsAdd.Click += new System.EventHandler(this.btnSolutionItemsAdd_Click);
            // 
            // btnFaultItemsDel
            // 
            this.btnFaultItemsDel.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFaultItemsDel.Location = new System.Drawing.Point(1022, 60);
            this.btnFaultItemsDel.Name = "btnFaultItemsDel";
            this.btnFaultItemsDel.Size = new System.Drawing.Size(30, 30);
            this.btnFaultItemsDel.TabIndex = 16;
            this.btnFaultItemsDel.Text = "－";
            this.btnFaultItemsDel.UseVisualStyleBackColor = true;
            this.btnFaultItemsDel.Click += new System.EventHandler(this.btnFaultItemsDel_Click);
            // 
            // btnFaultItemsAdd
            // 
            this.btnFaultItemsAdd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFaultItemsAdd.Location = new System.Drawing.Point(1022, 25);
            this.btnFaultItemsAdd.Name = "btnFaultItemsAdd";
            this.btnFaultItemsAdd.Size = new System.Drawing.Size(30, 30);
            this.btnFaultItemsAdd.TabIndex = 15;
            this.btnFaultItemsAdd.Text = "＋";
            this.btnFaultItemsAdd.UseVisualStyleBackColor = true;
            this.btnFaultItemsAdd.Click += new System.EventHandler(this.btnFaultItemsAdd_Click);
            // 
            // chkAutoHelpFlag
            // 
            this.chkAutoHelpFlag.AutoSize = true;
            this.chkAutoHelpFlag.Location = new System.Drawing.Point(378, 112);
            this.chkAutoHelpFlag.Name = "chkAutoHelpFlag";
            this.chkAutoHelpFlag.Size = new System.Drawing.Size(89, 19);
            this.chkAutoHelpFlag.TabIndex = 12;
            this.chkAutoHelpFlag.Text = "自动支援";
            this.chkAutoHelpFlag.UseVisualStyleBackColor = true;
            // 
            // lsbSolutionItems
            // 
            this.lsbSolutionItems.FormattingEnabled = true;
            this.lsbSolutionItems.ItemHeight = 15;
            this.lsbSolutionItems.Location = new System.Drawing.Point(1140, 60);
            this.lsbSolutionItems.Name = "lsbSolutionItems";
            this.lsbSolutionItems.Size = new System.Drawing.Size(343, 109);
            this.lsbSolutionItems.TabIndex = 7;
            // 
            // lsbFaultItems
            // 
            this.lsbFaultItems.AllowDrop = true;
            this.lsbFaultItems.FormattingEnabled = true;
            this.lsbFaultItems.ItemHeight = 15;
            this.lsbFaultItems.Location = new System.Drawing.Point(673, 60);
            this.lsbFaultItems.Name = "lsbFaultItems";
            this.lsbFaultItems.Size = new System.Drawing.Size(343, 109);
            this.lsbFaultItems.TabIndex = 6;
            // 
            // tbMachineType
            // 
            this.tbMachineType.Location = new System.Drawing.Point(119, 68);
            this.tbMachineType.Name = "tbMachineType";
            this.tbMachineType.Size = new System.Drawing.Size(180, 25);
            this.tbMachineType.TabIndex = 1;
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.ForeColor = System.Drawing.Color.Green;
            this.labMessage.Location = new System.Drawing.Point(1533, 151);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 10;
            this.labMessage.Text = "提示";
            // 
            // labFaultCount
            // 
            this.labFaultCount.AutoSize = true;
            this.labFaultCount.Location = new System.Drawing.Point(1020, 151);
            this.labFaultCount.Name = "labFaultCount";
            this.labFaultCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labFaultCount.Size = new System.Drawing.Size(15, 15);
            this.labFaultCount.TabIndex = 13;
            this.labFaultCount.Text = "0";
            // 
            // labSolutionCount
            // 
            this.labSolutionCount.AutoSize = true;
            this.labSolutionCount.Location = new System.Drawing.Point(1487, 151);
            this.labSolutionCount.Name = "labSolutionCount";
            this.labSolutionCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labSolutionCount.Size = new System.Drawing.Size(15, 15);
            this.labSolutionCount.TabIndex = 14;
            this.labSolutionCount.Text = "0";
            // 
            // labSolutionItems
            // 
            this.labSolutionItems.AutoSize = true;
            this.labSolutionItems.Location = new System.Drawing.Point(1070, 33);
            this.labSolutionItems.Name = "labSolutionItems";
            this.labSolutionItems.Size = new System.Drawing.Size(67, 15);
            this.labSolutionItems.TabIndex = 5;
            this.labSolutionItems.Text = "解决方案";
            // 
            // labFaultType
            // 
            this.labFaultType.AutoSize = true;
            this.labFaultType.Location = new System.Drawing.Point(46, 32);
            this.labFaultType.Name = "labFaultType";
            this.labFaultType.Size = new System.Drawing.Size(67, 15);
            this.labFaultType.TabIndex = 21;
            this.labFaultType.Text = "故障类型";
            // 
            // labFaultItems
            // 
            this.labFaultItems.AutoSize = true;
            this.labFaultItems.Location = new System.Drawing.Point(603, 33);
            this.labFaultItems.Name = "labFaultItems";
            this.labFaultItems.Size = new System.Drawing.Size(67, 15);
            this.labFaultItems.TabIndex = 4;
            this.labFaultItems.Text = "故障内容";
            // 
            // labMaxHelpTime
            // 
            this.labMaxHelpTime.AutoSize = true;
            this.labMaxHelpTime.Location = new System.Drawing.Point(378, 73);
            this.labMaxHelpTime.Name = "labMaxHelpTime";
            this.labMaxHelpTime.Size = new System.Drawing.Size(67, 15);
            this.labMaxHelpTime.TabIndex = 3;
            this.labMaxHelpTime.Text = "支援时间";
            // 
            // labMaxHandleTime
            // 
            this.labMaxHandleTime.AutoSize = true;
            this.labMaxHandleTime.Location = new System.Drawing.Point(378, 32);
            this.labMaxHandleTime.Name = "labMaxHandleTime";
            this.labMaxHandleTime.Size = new System.Drawing.Size(67, 15);
            this.labMaxHandleTime.TabIndex = 2;
            this.labMaxHandleTime.Text = "处理时间";
            // 
            // labMachineType
            // 
            this.labMachineType.AutoSize = true;
            this.labMachineType.Location = new System.Drawing.Point(46, 73);
            this.labMachineType.Name = "labMachineType";
            this.labMachineType.Size = new System.Drawing.Size(67, 15);
            this.labMachineType.TabIndex = 0;
            this.labMachineType.Text = "机台类型";
            // 
            // menuStripSolution
            // 
            this.menuStripSolution.BackColor = System.Drawing.Color.Transparent;
            this.menuStripSolution.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripSolution.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSolutionRefresh,
            this.tsmSolutionAdd,
            this.tsmSolutionUpdate,
            this.tsmSolutionSave,
            this.tsmSolutionDelete,
            this.tsmSolutionBack,
            this.tsmiSolutionImport,
            this.tsmiSolutionExport,
            this.tsmLineMachine});
            this.menuStripSolution.Location = new System.Drawing.Point(15, 0);
            this.menuStripSolution.Name = "menuStripSolution";
            this.menuStripSolution.Size = new System.Drawing.Size(1570, 35);
            this.menuStripSolution.TabIndex = 0;
            this.menuStripSolution.Text = "menuStrip1";
            // 
            // tsmSolutionRefresh
            // 
            this.tsmSolutionRefresh.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmSolutionRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmSolutionRefresh.Image")));
            this.tsmSolutionRefresh.Name = "tsmSolutionRefresh";
            this.tsmSolutionRefresh.Size = new System.Drawing.Size(84, 31);
            this.tsmSolutionRefresh.Text = "刷新";
            this.tsmSolutionRefresh.Click += new System.EventHandler(this.tsmSolutionRefresh_Click);
            // 
            // tsmSolutionAdd
            // 
            this.tsmSolutionAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmSolutionAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsmSolutionAdd.Image")));
            this.tsmSolutionAdd.Name = "tsmSolutionAdd";
            this.tsmSolutionAdd.Size = new System.Drawing.Size(84, 31);
            this.tsmSolutionAdd.Text = "添加";
            this.tsmSolutionAdd.Click += new System.EventHandler(this.tsmSolutionAdd_Click);
            // 
            // tsmSolutionUpdate
            // 
            this.tsmSolutionUpdate.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmSolutionUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsmSolutionUpdate.Image")));
            this.tsmSolutionUpdate.Name = "tsmSolutionUpdate";
            this.tsmSolutionUpdate.Size = new System.Drawing.Size(84, 31);
            this.tsmSolutionUpdate.Text = "修改";
            this.tsmSolutionUpdate.Click += new System.EventHandler(this.tsmSolutionUpdate_Click);
            // 
            // tsmSolutionSave
            // 
            this.tsmSolutionSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmSolutionSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmSolutionSave.Image")));
            this.tsmSolutionSave.Name = "tsmSolutionSave";
            this.tsmSolutionSave.Size = new System.Drawing.Size(84, 31);
            this.tsmSolutionSave.Text = "保存";
            this.tsmSolutionSave.Click += new System.EventHandler(this.tsmSolutionSave_Click);
            // 
            // tsmSolutionDelete
            // 
            this.tsmSolutionDelete.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmSolutionDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsmSolutionDelete.Image")));
            this.tsmSolutionDelete.Name = "tsmSolutionDelete";
            this.tsmSolutionDelete.Size = new System.Drawing.Size(84, 31);
            this.tsmSolutionDelete.Text = "删除";
            this.tsmSolutionDelete.Click += new System.EventHandler(this.tsmSolutionDelete_Click);
            // 
            // tsmSolutionBack
            // 
            this.tsmSolutionBack.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmSolutionBack.Image = ((System.Drawing.Image)(resources.GetObject("tsmSolutionBack.Image")));
            this.tsmSolutionBack.Name = "tsmSolutionBack";
            this.tsmSolutionBack.Size = new System.Drawing.Size(84, 31);
            this.tsmSolutionBack.Text = "返回";
            this.tsmSolutionBack.Click += new System.EventHandler(this.tsmSolutionBack_Click);
            // 
            // tsmiSolutionImport
            // 
            this.tsmiSolutionImport.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiSolutionImport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSolutionImport.Image")));
            this.tsmiSolutionImport.Name = "tsmiSolutionImport";
            this.tsmiSolutionImport.Size = new System.Drawing.Size(84, 31);
            this.tsmiSolutionImport.Text = "导入";
            this.tsmiSolutionImport.Click += new System.EventHandler(this.tsmiSolutionImport_Click);
            // 
            // tsmiSolutionExport
            // 
            this.tsmiSolutionExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiSolutionExport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSolutionExport.Image")));
            this.tsmiSolutionExport.Name = "tsmiSolutionExport";
            this.tsmiSolutionExport.Size = new System.Drawing.Size(84, 31);
            this.tsmiSolutionExport.Text = "导出";
            this.tsmiSolutionExport.Click += new System.EventHandler(this.tsmiSolutionExport_Click);
            // 
            // tsmLineMachine
            // 
            this.tsmLineMachine.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmLineMachine.Image = ((System.Drawing.Image)(resources.GetObject("tsmLineMachine.Image")));
            this.tsmLineMachine.Name = "tsmLineMachine";
            this.tsmLineMachine.Size = new System.Drawing.Size(124, 31);
            this.tsmLineMachine.Text = "线体机台";
            this.tsmLineMachine.Click += new System.EventHandler(this.tsmLineMachine_Click);
            // 
            // tbFaultItem
            // 
            this.tbFaultItem.Location = new System.Drawing.Point(673, 28);
            this.tbFaultItem.Name = "tbFaultItem";
            this.tbFaultItem.Size = new System.Drawing.Size(343, 25);
            this.tbFaultItem.TabIndex = 26;
            // 
            // tbSolutionItem
            // 
            this.tbSolutionItem.Location = new System.Drawing.Point(1140, 28);
            this.tbSolutionItem.Name = "tbSolutionItem";
            this.tbSolutionItem.Size = new System.Drawing.Size(343, 25);
            this.tbSolutionItem.TabIndex = 27;
            // 
            // FrmBaseInfoSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.Controls.Add(this.dgvFaultSolution);
            this.Controls.Add(this.gbSolution);
            this.Controls.Add(this.menuStripSolution);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBaseInfoSolution";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.Text = "信息维护";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseInfoSolution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultSolution)).EndInit();
            this.gbSolution.ResumeLayout(false);
            this.gbSolution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHelpTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHandleTimes)).EndInit();
            this.menuStripSolution.ResumeLayout(false);
            this.menuStripSolution.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem tsmLineMachine;
        private System.Windows.Forms.GroupBox gbSolution;
        private System.Windows.Forms.MenuStrip menuStripSolution;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvFaultSolution;
        private System.Windows.Forms.ListBox lsbSolutionItems;
        private System.Windows.Forms.ListBox lsbFaultItems;
        private System.Windows.Forms.Label labSolutionItems;
        private System.Windows.Forms.Label labFaultItems;
        private System.Windows.Forms.Label labMaxHelpTime;
        private System.Windows.Forms.Label labMaxHandleTime;
        private System.Windows.Forms.TextBox tbMachineType;
        private System.Windows.Forms.Label labMachineType;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.CheckBox chkAutoHelpFlag;
        private System.Windows.Forms.ToolStripMenuItem tsmSolutionRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmSolutionAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmSolutionUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmSolutionDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmSolutionBack;
        private System.Windows.Forms.Label labSolutionCount;
        private System.Windows.Forms.Label labFaultCount;
        private System.Windows.Forms.Button btnSolutionItemsDel;
        private System.Windows.Forms.Button btnSolutionItemsAdd;
        private System.Windows.Forms.Button btnFaultItemsDel;
        private System.Windows.Forms.Button btnFaultItemsAdd;
        private System.Windows.Forms.NumericUpDown nudMaxHandleTimes;
        private System.Windows.Forms.NumericUpDown nudMaxHelpTimes;
        private System.Windows.Forms.ToolStripMenuItem tsmSolutionSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiSolutionImport;
        private System.Windows.Forms.ToolStripMenuItem tsmiSolutionExport;
        private System.Windows.Forms.ComboBox cmbFaultType;
        private System.Windows.Forms.Label labFaultType;
        private System.Windows.Forms.TextBox tbImitateSound;
        private System.Windows.Forms.Label labImitateSound;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMachineType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFaultType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMaxHandleTimes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMaxHelpTimes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAutoHelpFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFaultItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcSolutionItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcValidity;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcImitateSound;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.TextBox tbSolutionItem;
        private System.Windows.Forms.TextBox tbFaultItem;
    }
}
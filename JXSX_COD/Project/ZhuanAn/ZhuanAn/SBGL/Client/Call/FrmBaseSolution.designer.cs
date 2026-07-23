namespace Call
{
    partial class FrmBaseSolution
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseSolution));
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labFaultType = new System.Windows.Forms.Label();
            this.chkAutoHelpFlag = new System.Windows.Forms.CheckBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.tbSolutionItem = new System.Windows.Forms.TextBox();
            this.tbImitateSound = new System.Windows.Forms.TextBox();
            this.cmbFaultType = new System.Windows.Forms.ComboBox();
            this.labImitateSound = new System.Windows.Forms.Label();
            this.tbFaultItem = new System.Windows.Forms.TextBox();
            this.nudMaxHelpTimes = new System.Windows.Forms.NumericUpDown();
            this.labMaxHandleTime = new System.Windows.Forms.Label();
            this.btnSolutionItemsDel = new System.Windows.Forms.Button();
            this.tbMachineType = new System.Windows.Forms.TextBox();
            this.nudMaxHandleTimes = new System.Windows.Forms.NumericUpDown();
            this.btnSolutionItemsAdd = new System.Windows.Forms.Button();
            this.labMaxHelpTime = new System.Windows.Forms.Label();
            this.labFaultItems = new System.Windows.Forms.Label();
            this.lsbFaultItems = new System.Windows.Forms.ListBox();
            this.lsbSolutionItems = new System.Windows.Forms.ListBox();
            this.labMachineType = new System.Windows.Forms.Label();
            this.btnFaultItemsAdd = new System.Windows.Forms.Button();
            this.btnFaultItemsDel = new System.Windows.Forms.Button();
            this.labSolutionItems = new System.Windows.Forms.Label();
            this.labSolutionCount = new System.Windows.Forms.Label();
            this.labFaultCount = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            this.menuStripSolution = new System.Windows.Forms.MenuStrip();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLineMachine = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLineQC = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultSolution)).BeginInit();
            this.gbSolution.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHelpTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHandleTimes)).BeginInit();
            this.menuStripSolution.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFaultSolution
            // 
            this.dgvFaultSolution.AllowUserToAddRows = false;
            this.dgvFaultSolution.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.GhostWhite;
            this.dgvFaultSolution.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFaultSolution.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFaultSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFaultSolution.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvFaultSolution.Location = new System.Drawing.Point(15, 238);
            this.dgvFaultSolution.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFaultSolution.Name = "dgvFaultSolution";
            this.dgvFaultSolution.ReadOnly = true;
            this.dgvFaultSolution.RowHeadersWidth = 51;
            this.dgvFaultSolution.RowTemplate.Height = 27;
            this.dgvFaultSolution.Size = new System.Drawing.Size(1570, 547);
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
            this.dgcMachineType.MinimumWidth = 6;
            this.dgcMachineType.Name = "dgcMachineType";
            this.dgcMachineType.ReadOnly = true;
            this.dgcMachineType.Width = 224;
            // 
            // dgcFaultType
            // 
            this.dgcFaultType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFaultType.DataPropertyName = "FaultType";
            this.dgcFaultType.HeaderText = "故障类型";
            this.dgcFaultType.MinimumWidth = 6;
            this.dgcFaultType.Name = "dgcFaultType";
            this.dgcFaultType.ReadOnly = true;
            this.dgcFaultType.Width = 150;
            // 
            // dgcMaxHandleTimes
            // 
            this.dgcMaxHandleTimes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcMaxHandleTimes.DataPropertyName = "MaxHandleTimes";
            this.dgcMaxHandleTimes.HeaderText = "最大处理时间(分钟)";
            this.dgcMaxHandleTimes.MinimumWidth = 6;
            this.dgcMaxHandleTimes.Name = "dgcMaxHandleTimes";
            this.dgcMaxHandleTimes.ReadOnly = true;
            this.dgcMaxHandleTimes.Width = 180;
            // 
            // dgcMaxHelpTimes
            // 
            this.dgcMaxHelpTimes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcMaxHelpTimes.DataPropertyName = "MaxHelpTimes";
            this.dgcMaxHelpTimes.HeaderText = "最大支援时间(分钟)";
            this.dgcMaxHelpTimes.MinimumWidth = 6;
            this.dgcMaxHelpTimes.Name = "dgcMaxHelpTimes";
            this.dgcMaxHelpTimes.ReadOnly = true;
            this.dgcMaxHelpTimes.Width = 180;
            // 
            // dgcAutoHelpFlag
            // 
            this.dgcAutoHelpFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAutoHelpFlag.DataPropertyName = "AutoHelpFlag";
            this.dgcAutoHelpFlag.HeaderText = "自动支援";
            this.dgcAutoHelpFlag.MinimumWidth = 6;
            this.dgcAutoHelpFlag.Name = "dgcAutoHelpFlag";
            this.dgcAutoHelpFlag.ReadOnly = true;
            this.dgcAutoHelpFlag.Width = 125;
            // 
            // dgcFaultItems
            // 
            this.dgcFaultItems.DataPropertyName = "FaultItems";
            this.dgcFaultItems.HeaderText = "故障内容";
            this.dgcFaultItems.MinimumWidth = 6;
            this.dgcFaultItems.Name = "dgcFaultItems";
            this.dgcFaultItems.ReadOnly = true;
            // 
            // dgcSolutionItems
            // 
            this.dgcSolutionItems.DataPropertyName = "SolutionItems";
            this.dgcSolutionItems.HeaderText = "解决方案";
            this.dgcSolutionItems.MinimumWidth = 6;
            this.dgcSolutionItems.Name = "dgcSolutionItems";
            this.dgcSolutionItems.ReadOnly = true;
            // 
            // dgcValidity
            // 
            this.dgcValidity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcValidity.DataPropertyName = "Validity";
            this.dgcValidity.HeaderText = "校验状态";
            this.dgcValidity.MinimumWidth = 6;
            this.dgcValidity.Name = "dgcValidity";
            this.dgcValidity.ReadOnly = true;
            this.dgcValidity.Width = 125;
            // 
            // dgcImitateSound
            // 
            this.dgcImitateSound.DataPropertyName = "ImitateSound";
            this.dgcImitateSound.HeaderText = "模拟声音";
            this.dgcImitateSound.MinimumWidth = 6;
            this.dgcImitateSound.Name = "dgcImitateSound";
            this.dgcImitateSound.ReadOnly = true;
            // 
            // gbSolution
            // 
            this.gbSolution.AutoSize = true;
            this.gbSolution.BackColor = System.Drawing.Color.Transparent;
            this.gbSolution.Controls.Add(this.tableLayoutPanel1);
            this.gbSolution.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSolution.Location = new System.Drawing.Point(15, 35);
            this.gbSolution.Name = "gbSolution";
            this.gbSolution.Size = new System.Drawing.Size(1570, 164);
            this.gbSolution.TabIndex = 1;
            this.gbSolution.TabStop = false;
            this.gbSolution.Text = "方案信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 11;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labFaultType, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkAutoHelpFlag, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnPlay, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbSolutionItem, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbImitateSound, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbFaultType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labImitateSound, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbFaultItem, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudMaxHelpTimes, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.labMaxHandleTime, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSolutionItemsDel, 9, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbMachineType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudMaxHandleTimes, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSolutionItemsAdd, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMaxHelpTime, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labFaultItems, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lsbFaultItems, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.lsbSolutionItems, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.labMachineType, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnFaultItemsAdd, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnFaultItemsDel, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.labSolutionItems, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.labSolutionCount, 9, 3);
            this.tableLayoutPanel1.Controls.Add(this.labFaultCount, 6, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1564, 140);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // labFaultType
            // 
            this.labFaultType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labFaultType.AutoSize = true;
            this.labFaultType.Location = new System.Drawing.Point(10, 10);
            this.labFaultType.Name = "labFaultType";
            this.labFaultType.Size = new System.Drawing.Size(67, 15);
            this.labFaultType.TabIndex = 21;
            this.labFaultType.Text = "故障类型";
            // 
            // chkAutoHelpFlag
            // 
            this.chkAutoHelpFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAutoHelpFlag.AutoSize = true;
            this.chkAutoHelpFlag.Location = new System.Drawing.Point(323, 78);
            this.chkAutoHelpFlag.Name = "chkAutoHelpFlag";
            this.chkAutoHelpFlag.Size = new System.Drawing.Size(89, 19);
            this.chkAutoHelpFlag.TabIndex = 12;
            this.chkAutoHelpFlag.Text = "自动支援";
            this.chkAutoHelpFlag.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(243, 73);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(30, 29);
            this.btnPlay.TabIndex = 25;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // tbSolutionItem
            // 
            this.tbSolutionItem.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbSolutionItem.Location = new System.Drawing.Point(983, 5);
            this.tbSolutionItem.Name = "tbSolutionItem";
            this.tbSolutionItem.Size = new System.Drawing.Size(294, 25);
            this.tbSolutionItem.TabIndex = 27;
            // 
            // tbImitateSound
            // 
            this.tbImitateSound.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbImitateSound.Location = new System.Drawing.Point(83, 75);
            this.tbImitateSound.Name = "tbImitateSound";
            this.tbImitateSound.Size = new System.Drawing.Size(150, 25);
            this.tbImitateSound.TabIndex = 24;
            // 
            // cmbFaultType
            // 
            this.cmbFaultType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbFaultType.Location = new System.Drawing.Point(83, 6);
            this.cmbFaultType.Name = "cmbFaultType";
            this.cmbFaultType.Size = new System.Drawing.Size(150, 23);
            this.cmbFaultType.TabIndex = 22;
            // 
            // labImitateSound
            // 
            this.labImitateSound.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labImitateSound.AutoSize = true;
            this.labImitateSound.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labImitateSound.Location = new System.Drawing.Point(10, 80);
            this.labImitateSound.Name = "labImitateSound";
            this.labImitateSound.Size = new System.Drawing.Size(67, 15);
            this.labImitateSound.TabIndex = 23;
            this.labImitateSound.Text = "模拟声音";
            // 
            // tbFaultItem
            // 
            this.tbFaultItem.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbFaultItem.Location = new System.Drawing.Point(563, 5);
            this.tbFaultItem.Name = "tbFaultItem";
            this.tbFaultItem.Size = new System.Drawing.Size(294, 25);
            this.tbFaultItem.TabIndex = 26;
            // 
            // nudMaxHelpTimes
            // 
            this.nudMaxHelpTimes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudMaxHelpTimes.Location = new System.Drawing.Point(323, 40);
            this.nudMaxHelpTimes.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudMaxHelpTimes.Name = "nudMaxHelpTimes";
            this.nudMaxHelpTimes.Size = new System.Drawing.Size(150, 25);
            this.nudMaxHelpTimes.TabIndex = 20;
            // 
            // labMaxHandleTime
            // 
            this.labMaxHandleTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMaxHandleTime.AutoSize = true;
            this.labMaxHandleTime.Location = new System.Drawing.Point(250, 10);
            this.labMaxHandleTime.Name = "labMaxHandleTime";
            this.labMaxHandleTime.Size = new System.Drawing.Size(67, 15);
            this.labMaxHandleTime.TabIndex = 2;
            this.labMaxHandleTime.Text = "处理时间";
            // 
            // btnSolutionItemsDel
            // 
            this.btnSolutionItemsDel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSolutionItemsDel.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSolutionItemsDel.Location = new System.Drawing.Point(1283, 38);
            this.btnSolutionItemsDel.Name = "btnSolutionItemsDel";
            this.btnSolutionItemsDel.Size = new System.Drawing.Size(30, 29);
            this.btnSolutionItemsDel.TabIndex = 18;
            this.btnSolutionItemsDel.Text = "－";
            this.btnSolutionItemsDel.UseVisualStyleBackColor = true;
            this.btnSolutionItemsDel.Click += new System.EventHandler(this.btnSolutionItemsDel_Click);
            // 
            // tbMachineType
            // 
            this.tbMachineType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbMachineType.Location = new System.Drawing.Point(83, 40);
            this.tbMachineType.Name = "tbMachineType";
            this.tbMachineType.Size = new System.Drawing.Size(150, 25);
            this.tbMachineType.TabIndex = 1;
            // 
            // nudMaxHandleTimes
            // 
            this.nudMaxHandleTimes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudMaxHandleTimes.Location = new System.Drawing.Point(323, 5);
            this.nudMaxHandleTimes.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudMaxHandleTimes.Name = "nudMaxHandleTimes";
            this.nudMaxHandleTimes.Size = new System.Drawing.Size(150, 25);
            this.nudMaxHandleTimes.TabIndex = 19;
            // 
            // btnSolutionItemsAdd
            // 
            this.btnSolutionItemsAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSolutionItemsAdd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSolutionItemsAdd.Location = new System.Drawing.Point(1283, 3);
            this.btnSolutionItemsAdd.Name = "btnSolutionItemsAdd";
            this.btnSolutionItemsAdd.Size = new System.Drawing.Size(30, 29);
            this.btnSolutionItemsAdd.TabIndex = 17;
            this.btnSolutionItemsAdd.Text = "＋";
            this.btnSolutionItemsAdd.UseVisualStyleBackColor = true;
            this.btnSolutionItemsAdd.Click += new System.EventHandler(this.btnSolutionItemsAdd_Click);
            // 
            // labMaxHelpTime
            // 
            this.labMaxHelpTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMaxHelpTime.AutoSize = true;
            this.labMaxHelpTime.Location = new System.Drawing.Point(250, 45);
            this.labMaxHelpTime.Name = "labMaxHelpTime";
            this.labMaxHelpTime.Size = new System.Drawing.Size(67, 15);
            this.labMaxHelpTime.TabIndex = 3;
            this.labMaxHelpTime.Text = "支援时间";
            // 
            // labFaultItems
            // 
            this.labFaultItems.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labFaultItems.AutoSize = true;
            this.labFaultItems.Location = new System.Drawing.Point(490, 10);
            this.labFaultItems.Name = "labFaultItems";
            this.labFaultItems.Size = new System.Drawing.Size(67, 15);
            this.labFaultItems.TabIndex = 4;
            this.labFaultItems.Text = "故障内容";
            // 
            // lsbFaultItems
            // 
            this.lsbFaultItems.AllowDrop = true;
            this.lsbFaultItems.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lsbFaultItems.FormattingEnabled = true;
            this.lsbFaultItems.ItemHeight = 15;
            this.lsbFaultItems.Location = new System.Drawing.Point(563, 40);
            this.lsbFaultItems.Name = "lsbFaultItems";
            this.tableLayoutPanel1.SetRowSpan(this.lsbFaultItems, 3);
            this.lsbFaultItems.Size = new System.Drawing.Size(294, 94);
            this.lsbFaultItems.TabIndex = 6;
            // 
            // lsbSolutionItems
            // 
            this.lsbSolutionItems.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lsbSolutionItems.FormattingEnabled = true;
            this.lsbSolutionItems.ItemHeight = 15;
            this.lsbSolutionItems.Location = new System.Drawing.Point(983, 40);
            this.lsbSolutionItems.Name = "lsbSolutionItems";
            this.tableLayoutPanel1.SetRowSpan(this.lsbSolutionItems, 3);
            this.lsbSolutionItems.Size = new System.Drawing.Size(294, 94);
            this.lsbSolutionItems.TabIndex = 7;
            // 
            // labMachineType
            // 
            this.labMachineType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachineType.AutoSize = true;
            this.labMachineType.Location = new System.Drawing.Point(10, 45);
            this.labMachineType.Name = "labMachineType";
            this.labMachineType.Size = new System.Drawing.Size(67, 15);
            this.labMachineType.TabIndex = 0;
            this.labMachineType.Text = "机台类型";
            // 
            // btnFaultItemsAdd
            // 
            this.btnFaultItemsAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFaultItemsAdd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFaultItemsAdd.Location = new System.Drawing.Point(863, 3);
            this.btnFaultItemsAdd.Name = "btnFaultItemsAdd";
            this.btnFaultItemsAdd.Size = new System.Drawing.Size(30, 29);
            this.btnFaultItemsAdd.TabIndex = 15;
            this.btnFaultItemsAdd.Text = "＋";
            this.btnFaultItemsAdd.UseVisualStyleBackColor = true;
            this.btnFaultItemsAdd.Click += new System.EventHandler(this.btnFaultItemsAdd_Click);
            // 
            // btnFaultItemsDel
            // 
            this.btnFaultItemsDel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFaultItemsDel.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFaultItemsDel.Location = new System.Drawing.Point(863, 38);
            this.btnFaultItemsDel.Name = "btnFaultItemsDel";
            this.btnFaultItemsDel.Size = new System.Drawing.Size(30, 29);
            this.btnFaultItemsDel.TabIndex = 16;
            this.btnFaultItemsDel.Text = "－";
            this.btnFaultItemsDel.UseVisualStyleBackColor = true;
            this.btnFaultItemsDel.Click += new System.EventHandler(this.btnFaultItemsDel_Click);
            // 
            // labSolutionItems
            // 
            this.labSolutionItems.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labSolutionItems.AutoSize = true;
            this.labSolutionItems.Location = new System.Drawing.Point(910, 10);
            this.labSolutionItems.Name = "labSolutionItems";
            this.labSolutionItems.Size = new System.Drawing.Size(67, 15);
            this.labSolutionItems.TabIndex = 5;
            this.labSolutionItems.Text = "解决方案";
            // 
            // labSolutionCount
            // 
            this.labSolutionCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labSolutionCount.AutoSize = true;
            this.labSolutionCount.Location = new System.Drawing.Point(1283, 115);
            this.labSolutionCount.Name = "labSolutionCount";
            this.labSolutionCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labSolutionCount.Size = new System.Drawing.Size(15, 15);
            this.labSolutionCount.TabIndex = 14;
            this.labSolutionCount.Text = "0";
            // 
            // labFaultCount
            // 
            this.labFaultCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFaultCount.AutoSize = true;
            this.labFaultCount.Location = new System.Drawing.Point(863, 115);
            this.labFaultCount.Name = "labFaultCount";
            this.labFaultCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labFaultCount.Size = new System.Drawing.Size(15, 15);
            this.labFaultCount.TabIndex = 13;
            this.labFaultCount.Text = "0";
            // 
            // labMessage
            // 
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labMessage.ForeColor = System.Drawing.Color.Green;
            this.labMessage.Location = new System.Drawing.Point(15, 199);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(1570, 39);
            this.labMessage.TabIndex = 10;
            this.labMessage.Text = "提示";
            // 
            // menuStripSolution
            // 
            this.menuStripSolution.BackColor = System.Drawing.Color.Transparent;
            this.menuStripSolution.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.menuStripSolution.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripSolution.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh,
            this.tsmiAdd,
            this.tsmiUpdate,
            this.tsmiDelete,
            this.tsmiSave,
            this.tsmiBack,
            this.tsmiImport,
            this.tsmiExport,
            this.tsmLineMachine,
            this.tsmiLineQC});
            this.menuStripSolution.Location = new System.Drawing.Point(15, 0);
            this.menuStripSolution.Name = "menuStripSolution";
            this.menuStripSolution.Size = new System.Drawing.Size(1570, 35);
            this.menuStripSolution.TabIndex = 0;
            this.menuStripSolution.Text = "menuStrip1";
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmiRefresh.Image")));
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(86, 31);
            this.tsmiRefresh.Text = "刷新";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAdd.Image")));
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(86, 31);
            this.tsmiAdd.Text = "添加";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUpdate.Image")));
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(86, 31);
            this.tsmiUpdate.Text = "修改";
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
            // tsmiImport
            // 
            this.tsmiImport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiImport.Image")));
            this.tsmiImport.Name = "tsmiImport";
            this.tsmiImport.Size = new System.Drawing.Size(86, 31);
            this.tsmiImport.Text = "导入";
            this.tsmiImport.Click += new System.EventHandler(this.tsmiImport_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExport.Image")));
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(86, 31);
            this.tsmiExport.Text = "导出";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // tsmLineMachine
            // 
            this.tsmLineMachine.Image = ((System.Drawing.Image)(resources.GetObject("tsmLineMachine.Image")));
            this.tsmLineMachine.Name = "tsmLineMachine";
            this.tsmLineMachine.Size = new System.Drawing.Size(126, 31);
            this.tsmLineMachine.Text = "产线机台";
            this.tsmLineMachine.Click += new System.EventHandler(this.tsmLineMachine_Click);
            // 
            // tsmiLineQC
            // 
            this.tsmiLineQC.Image = ((System.Drawing.Image)(resources.GetObject("tsmiLineQC.Image")));
            this.tsmiLineQC.Name = "tsmiLineQC";
            this.tsmiLineQC.Size = new System.Drawing.Size(126, 31);
            this.tsmiLineQC.Text = "产线品管";
            this.tsmiLineQC.Click += new System.EventHandler(this.tsmiLineQC_Click);
            // 
            // FrmBaseSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.Controls.Add(this.dgvFaultSolution);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.gbSolution);
            this.Controls.Add(this.menuStripSolution);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBaseSolution";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.Text = "故障方案";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseSolution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultSolution)).EndInit();
            this.gbSolution.ResumeLayout(false);
            this.gbSolution.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.Label labSolutionCount;
        private System.Windows.Forms.Label labFaultCount;
        private System.Windows.Forms.Button btnSolutionItemsDel;
        private System.Windows.Forms.Button btnSolutionItemsAdd;
        private System.Windows.Forms.Button btnFaultItemsDel;
        private System.Windows.Forms.Button btnFaultItemsAdd;
        private System.Windows.Forms.NumericUpDown nudMaxHandleTimes;
        private System.Windows.Forms.NumericUpDown nudMaxHelpTimes;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiImport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
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
        private System.Windows.Forms.ToolStripMenuItem tsmiLineQC;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
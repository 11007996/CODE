namespace Machine
{
    partial class FrmBaseMachine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseMachine));
            DevComponents.DotNetBar.SuperGrid.Style.Background background1 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            this.gbMachine = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labAsset = new System.Windows.Forms.Label();
            this.chkIsLink = new System.Windows.Forms.CheckBox();
            this.labMachineName = new System.Windows.Forms.Label();
            this.cmbAssetNo = new System.Windows.Forms.ComboBox();
            this.tbMachineCategory = new System.Windows.Forms.TextBox();
            this.cmbLine = new System.Windows.Forms.ComboBox();
            this.labMachineCategory = new System.Windows.Forms.Label();
            this.tbMachineName = new System.Windows.Forms.TextBox();
            this.labLine = new System.Windows.Forms.Label();
            this.txbAssetName = new System.Windows.Forms.TextBox();
            this.tbMachineNo = new System.Windows.Forms.TextBox();
            this.labPower = new System.Windows.Forms.Label();
            this.nudPower = new System.Windows.Forms.NumericUpDown();
            this.labTheoryCT = new System.Windows.Forms.Label();
            this.labMahineNo = new System.Windows.Forms.Label();
            this.tbMachineCode = new System.Windows.Forms.TextBox();
            this.nudTheoryCT = new System.Windows.Forms.NumericUpDown();
            this.labMachineCode = new System.Windows.Forms.Label();
            this.swbtnShow = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labMessage = new System.Windows.Forms.Label();
            this.menuStripMachine = new System.Windows.Forms.MenuStrip();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMachineReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWarnCode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMachinePoint = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiMachineState = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sgridMachine = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gcAssetNo = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gcMachineCode = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gcMachineName = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gcTheoryCT = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gcPower = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gcMachineCategory = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gcIsLink = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gcLine = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.dgvWarnCode = new System.Windows.Forms.DataGridView();
            this.dgcWarnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWarnDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbMachine.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTheoryCT)).BeginInit();
            this.menuStripMachine.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnCode)).BeginInit();
            this.SuspendLayout();
            // 
            // gbMachine
            // 
            this.gbMachine.AutoSize = true;
            this.gbMachine.BackColor = System.Drawing.Color.Transparent;
            this.gbMachine.Controls.Add(this.tableLayoutPanel1);
            this.gbMachine.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMachine.Location = new System.Drawing.Point(15, 35);
            this.gbMachine.Name = "gbMachine";
            this.gbMachine.Size = new System.Drawing.Size(1570, 94);
            this.gbMachine.TabIndex = 1;
            this.gbMachine.TabStop = false;
            this.gbMachine.Text = "设备信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 11;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labAsset, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkIsLink, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMachineName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbAssetNo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbMachineCategory, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbLine, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMachineCategory, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbMachineName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labLine, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbMachineNo, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.labPower, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudPower, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.labTheoryCT, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMahineNo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbMachineCode, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudTheoryCT, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMachineCode, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.swbtnShow, 10, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1564, 70);
            this.tableLayoutPanel1.TabIndex = 33;
            // 
            // labAsset
            // 
            this.labAsset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAsset.AutoSize = true;
            this.labAsset.Location = new System.Drawing.Point(10, 10);
            this.labAsset.Name = "labAsset";
            this.labAsset.Size = new System.Drawing.Size(67, 15);
            this.labAsset.TabIndex = 28;
            this.labAsset.Text = "资产名称";
            // 
            // chkIsLink
            // 
            this.chkIsLink.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkIsLink.AutoSize = true;
            this.chkIsLink.Location = new System.Drawing.Point(1168, 8);
            this.chkIsLink.Name = "chkIsLink";
            this.chkIsLink.Size = new System.Drawing.Size(89, 19);
            this.chkIsLink.TabIndex = 31;
            this.chkIsLink.Text = "是否连接";
            this.chkIsLink.UseVisualStyleBackColor = true;
            // 
            // labMachineName
            // 
            this.labMachineName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachineName.AutoSize = true;
            this.labMachineName.Location = new System.Drawing.Point(10, 45);
            this.labMachineName.Name = "labMachineName";
            this.labMachineName.Size = new System.Drawing.Size(67, 15);
            this.labMachineName.TabIndex = 0;
            this.labMachineName.Text = "设备名称";
            // 
            // cmbAssetNo
            // 
            this.cmbAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbAssetNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAssetNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAssetNo.Location = new System.Drawing.Point(313, 6);
            this.cmbAssetNo.MaxLength = 50;
            this.cmbAssetNo.Name = "cmbAssetNo";
            this.cmbAssetNo.Size = new System.Drawing.Size(224, 23);
            this.cmbAssetNo.TabIndex = 29;
            this.cmbAssetNo.SelectedIndexChanged += new System.EventHandler(this.cmbAssetNo_SelectedIndexChanged);
            // 
            // tbMachineCategory
            // 
            this.tbMachineCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbMachineCategory.Location = new System.Drawing.Point(983, 40);
            this.tbMachineCategory.MaxLength = 20;
            this.tbMachineCategory.Name = "tbMachineCategory";
            this.tbMachineCategory.Size = new System.Drawing.Size(150, 25);
            this.tbMachineCategory.TabIndex = 24;
            // 
            // cmbLine
            // 
            this.cmbLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLine.FormattingEnabled = true;
            this.cmbLine.Location = new System.Drawing.Point(983, 6);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(150, 23);
            this.cmbLine.TabIndex = 17;
            // 
            // labMachineCategory
            // 
            this.labMachineCategory.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachineCategory.AutoSize = true;
            this.labMachineCategory.Location = new System.Drawing.Point(940, 45);
            this.labMachineCategory.Name = "labMachineCategory";
            this.labMachineCategory.Size = new System.Drawing.Size(37, 15);
            this.labMachineCategory.TabIndex = 23;
            this.labMachineCategory.Text = "类型";
            // 
            // tbMachineName
            // 
            this.tbMachineName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbMachineName.Location = new System.Drawing.Point(83, 40);
            this.tbMachineName.MaxLength = 20;
            this.tbMachineName.Name = "tbMachineName";
            this.tbMachineName.Size = new System.Drawing.Size(224, 25);
            this.tbMachineName.TabIndex = 1;
            // 
            // labLine
            // 
            this.labLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labLine.AutoSize = true;
            this.labLine.Location = new System.Drawing.Point(940, 10);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(37, 15);
            this.labLine.TabIndex = 4;
            this.labLine.Text = "线体";
            // 
            // txbAssetName
            // 
            this.txbAssetName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAssetName.Location = new System.Drawing.Point(83, 5);
            this.txbAssetName.Name = "txbAssetName";
            this.txbAssetName.ReadOnly = true;
            this.txbAssetName.Size = new System.Drawing.Size(224, 25);
            this.txbAssetName.TabIndex = 32;
            // 
            // tbMachineNo
            // 
            this.tbMachineNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbMachineNo.Location = new System.Drawing.Point(623, 40);
            this.tbMachineNo.MaxLength = 2;
            this.tbMachineNo.Name = "tbMachineNo";
            this.tbMachineNo.Size = new System.Drawing.Size(80, 25);
            this.tbMachineNo.TabIndex = 25;
            // 
            // labPower
            // 
            this.labPower.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPower.AutoSize = true;
            this.labPower.Location = new System.Drawing.Point(760, 45);
            this.labPower.Name = "labPower";
            this.labPower.Size = new System.Drawing.Size(37, 15);
            this.labPower.TabIndex = 27;
            this.labPower.Text = "功率";
            // 
            // nudPower
            // 
            this.nudPower.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudPower.DecimalPlaces = 3;
            this.nudPower.Location = new System.Drawing.Point(803, 40);
            this.nudPower.Name = "nudPower";
            this.nudPower.Size = new System.Drawing.Size(80, 25);
            this.nudPower.TabIndex = 26;
            // 
            // labTheoryCT
            // 
            this.labTheoryCT.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTheoryCT.AutoSize = true;
            this.labTheoryCT.Location = new System.Drawing.Point(728, 10);
            this.labTheoryCT.Name = "labTheoryCT";
            this.labTheoryCT.Size = new System.Drawing.Size(69, 15);
            this.labTheoryCT.TabIndex = 21;
            this.labTheoryCT.Text = "理论CT/S";
            // 
            // labMahineNo
            // 
            this.labMahineNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMahineNo.AutoSize = true;
            this.labMahineNo.Location = new System.Drawing.Point(580, 45);
            this.labMahineNo.Name = "labMahineNo";
            this.labMahineNo.Size = new System.Drawing.Size(37, 15);
            this.labMahineNo.TabIndex = 3;
            this.labMahineNo.Text = "编号";
            // 
            // tbMachineCode
            // 
            this.tbMachineCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbMachineCode.Location = new System.Drawing.Point(623, 5);
            this.tbMachineCode.MaxLength = 20;
            this.tbMachineCode.Name = "tbMachineCode";
            this.tbMachineCode.Size = new System.Drawing.Size(79, 25);
            this.tbMachineCode.TabIndex = 15;
            // 
            // nudTheoryCT
            // 
            this.nudTheoryCT.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudTheoryCT.DecimalPlaces = 3;
            this.nudTheoryCT.Location = new System.Drawing.Point(803, 5);
            this.nudTheoryCT.Name = "nudTheoryCT";
            this.nudTheoryCT.Size = new System.Drawing.Size(80, 25);
            this.nudTheoryCT.TabIndex = 22;
            // 
            // labMachineCode
            // 
            this.labMachineCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachineCode.AutoSize = true;
            this.labMachineCode.Location = new System.Drawing.Point(580, 10);
            this.labMachineCode.Name = "labMachineCode";
            this.labMachineCode.Size = new System.Drawing.Size(37, 15);
            this.labMachineCode.TabIndex = 2;
            this.labMachineCode.Text = "编码";
            // 
            // swbtnShow
            // 
            this.swbtnShow.Anchor = System.Windows.Forms.AnchorStyles.Right;
            // 
            // 
            // 
            this.swbtnShow.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.swbtnShow.Location = new System.Drawing.Point(1486, 38);
            this.swbtnShow.Name = "swbtnShow";
            this.swbtnShow.OffText = "产线";
            this.swbtnShow.OnText = "设备";
            this.swbtnShow.Size = new System.Drawing.Size(75, 29);
            this.swbtnShow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.swbtnShow.TabIndex = 30;
            this.swbtnShow.Value = true;
            this.swbtnShow.ValueObject = "Y";
            this.swbtnShow.ValueChanged += new System.EventHandler(this.swbtnShow_ValueChanged);
            // 
            // labMessage
            // 
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(15, 129);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(1570, 35);
            this.labMessage.TabIndex = 10;
            this.labMessage.Text = "提示";
            // 
            // menuStripMachine
            // 
            this.menuStripMachine.BackColor = System.Drawing.Color.Transparent;
            this.menuStripMachine.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMachine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh,
            this.tsmiAdd,
            this.tsmiUpdate,
            this.tsmiDelete,
            this.tsmiSave,
            this.tsmiBack,
            this.tsmiImport,
            this.tsmiExport,
            this.tsmiMachineReport,
            this.tsmiWarnCode,
            this.tsmiMachinePoint,
            this.tmsiMachineState});
            this.menuStripMachine.Location = new System.Drawing.Point(15, 0);
            this.menuStripMachine.Name = "menuStripMachine";
            this.menuStripMachine.Size = new System.Drawing.Size(1570, 35);
            this.menuStripMachine.TabIndex = 0;
            this.menuStripMachine.Text = "menuStrip1";
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmiRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmiRefresh.Image")));
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(86, 31);
            this.tsmiRefresh.Text = "刷新";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAdd.Image")));
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(86, 31);
            this.tsmiAdd.Text = "添加";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUpdate.Image")));
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(86, 31);
            this.tsmiUpdate.Text = "修改";
            this.tsmiUpdate.Click += new System.EventHandler(this.tsmiUpdate_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDelete.Image")));
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(86, 31);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSave.Image")));
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(86, 31);
            this.tsmiSave.Text = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiBack
            // 
            this.tsmiBack.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiBack.Image = ((System.Drawing.Image)(resources.GetObject("tsmiBack.Image")));
            this.tsmiBack.Name = "tsmiBack";
            this.tsmiBack.Size = new System.Drawing.Size(86, 31);
            this.tsmiBack.Text = "返回";
            this.tsmiBack.Click += new System.EventHandler(this.tsmiBack_Click);
            // 
            // tsmiImport
            // 
            this.tsmiImport.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiImport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiImport.Image")));
            this.tsmiImport.Name = "tsmiImport";
            this.tsmiImport.Size = new System.Drawing.Size(86, 31);
            this.tsmiImport.Text = "导入";
            this.tsmiImport.Click += new System.EventHandler(this.tsmiImport_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiExport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExport.Image")));
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(86, 31);
            this.tsmiExport.Text = "导出";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // tsmiMachineReport
            // 
            this.tsmiMachineReport.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiMachineReport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMachineReport.Image")));
            this.tsmiMachineReport.Name = "tsmiMachineReport";
            this.tsmiMachineReport.Size = new System.Drawing.Size(126, 31);
            this.tsmiMachineReport.Text = "上报记录";
            this.tsmiMachineReport.Click += new System.EventHandler(this.tsmiMachineReport_Click);
            // 
            // tsmiWarnCode
            // 
            this.tsmiWarnCode.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiWarnCode.Image = ((System.Drawing.Image)(resources.GetObject("tsmiWarnCode.Image")));
            this.tsmiWarnCode.Name = "tsmiWarnCode";
            this.tsmiWarnCode.Size = new System.Drawing.Size(126, 31);
            this.tsmiWarnCode.Text = "报警代码";
            this.tsmiWarnCode.Click += new System.EventHandler(this.tsmiWarnCode_Click);
            // 
            // tsmiMachinePoint
            // 
            this.tsmiMachinePoint.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiMachinePoint.Name = "tsmiMachinePoint";
            this.tsmiMachinePoint.Size = new System.Drawing.Size(106, 31);
            this.tsmiMachinePoint.Text = "设备分布";
            this.tsmiMachinePoint.Click += new System.EventHandler(this.tsmiMachinePoint_Click);
            // 
            // tmsiMachineState
            // 
            this.tmsiMachineState.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tmsiMachineState.Name = "tmsiMachineState";
            this.tmsiMachineState.Size = new System.Drawing.Size(106, 31);
            this.tmsiMachineState.Text = "设备状态";
            this.tmsiMachineState.Click += new System.EventHandler(this.tmsiMachineState_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sgridMachine);
            this.panel1.Controls.Add(this.dgvWarnCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(15, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1570, 621);
            this.panel1.TabIndex = 3;
            // 
            // sgridMachine
            // 
            this.sgridMachine.BackColor = System.Drawing.Color.White;
            this.sgridMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgridMachine.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.sgridMachine.ForeColor = System.Drawing.Color.Black;
            this.sgridMachine.Location = new System.Drawing.Point(0, 0);
            this.sgridMachine.Name = "sgridMachine";
            // 
            // 
            // 
            this.sgridMachine.PrimaryGrid.AutoGenerateColumns = false;
            this.sgridMachine.PrimaryGrid.ColumnAutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.sgridMachine.PrimaryGrid.Columns.Add(this.gcAssetNo);
            this.sgridMachine.PrimaryGrid.Columns.Add(this.gcMachineCode);
            this.sgridMachine.PrimaryGrid.Columns.Add(this.gcMachineName);
            this.sgridMachine.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.sgridMachine.PrimaryGrid.Columns.Add(this.gcTheoryCT);
            this.sgridMachine.PrimaryGrid.Columns.Add(this.gcPower);
            this.sgridMachine.PrimaryGrid.Columns.Add(this.gcMachineCategory);
            this.sgridMachine.PrimaryGrid.Columns.Add(this.gcIsLink);
            this.sgridMachine.PrimaryGrid.Columns.Add(this.gcLine);
            background1.Color1 = System.Drawing.Color.Moccasin;
            this.sgridMachine.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Background = background1;
            this.sgridMachine.PrimaryGrid.InitialActiveRow = DevComponents.DotNetBar.SuperGrid.RelativeRow.None;
            this.sgridMachine.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.None;
            this.sgridMachine.PrimaryGrid.MultiSelect = false;
            this.sgridMachine.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.sgridMachine.PrimaryGrid.ShowTreeButtons = true;
            this.sgridMachine.PrimaryGrid.ShowTreeLines = true;
            this.sgridMachine.Size = new System.Drawing.Size(1302, 621);
            this.sgridMachine.TabIndex = 4;
            this.sgridMachine.SelectionChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEventArgs>(this.sgridMachine_SelectionChanged);
            // 
            // gcAssetNo
            // 
            this.gcAssetNo.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.None;
            this.gcAssetNo.DataPropertyName = "AssetNo";
            this.gcAssetNo.HeaderText = "资产编号";
            this.gcAssetNo.Name = "gcAssetNo";
            this.gcAssetNo.Width = 180;
            // 
            // gcMachineCode
            // 
            this.gcMachineCode.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.None;
            this.gcMachineCode.DataPropertyName = "MachineCode";
            this.gcMachineCode.HeaderText = "设备编码";
            this.gcMachineCode.Name = "gcMachineCode";
            this.gcMachineCode.Width = 80;
            // 
            // gcMachineName
            // 
            this.gcMachineName.DataPropertyName = "MachineName";
            this.gcMachineName.HeaderText = "设备名称";
            this.gcMachineName.Name = "gcMachineName";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.None;
            this.gridColumn2.DataPropertyName = "MachineNo";
            this.gridColumn2.HeaderText = "编号";
            this.gridColumn2.Name = "gcMachineNo";
            this.gridColumn2.Width = 80;
            // 
            // gcTheoryCT
            // 
            this.gcTheoryCT.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.None;
            this.gcTheoryCT.DataPropertyName = "TheoryCT";
            this.gcTheoryCT.HeaderText = "理论CT/S";
            this.gcTheoryCT.Name = "gcTheoryCT";
            this.gcTheoryCT.Width = 80;
            // 
            // gcPower
            // 
            this.gcPower.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.None;
            this.gcPower.DataPropertyName = "Power";
            this.gcPower.HeaderText = "功率(KW)";
            this.gcPower.Name = "gcPower";
            this.gcPower.Width = 80;
            // 
            // gcMachineCategory
            // 
            this.gcMachineCategory.DataPropertyName = "MachineCategory";
            this.gcMachineCategory.HeaderText = "设备分类";
            this.gcMachineCategory.Name = "gcMachineCategory";
            // 
            // gcIsLink
            // 
            this.gcIsLink.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.None;
            this.gcIsLink.DataPropertyName = "IsLink";
            this.gcIsLink.HeaderText = "是否连接";
            this.gcIsLink.Name = "gcIsLink";
            this.gcIsLink.Width = 80;
            // 
            // gcLine
            // 
            this.gcLine.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.None;
            this.gcLine.DataPropertyName = "Line";
            this.gcLine.HeaderText = "关联产线";
            this.gcLine.Name = "gcLine";
            this.gcLine.Width = 80;
            // 
            // dgvWarnCode
            // 
            this.dgvWarnCode.AllowUserToAddRows = false;
            this.dgvWarnCode.AllowUserToDeleteRows = false;
            this.dgvWarnCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWarnCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWarnCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcWarnCode,
            this.dgcWarnDesc});
            this.dgvWarnCode.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvWarnCode.Location = new System.Drawing.Point(1302, 0);
            this.dgvWarnCode.Name = "dgvWarnCode";
            this.dgvWarnCode.ReadOnly = true;
            this.dgvWarnCode.RowHeadersVisible = false;
            this.dgvWarnCode.RowHeadersWidth = 51;
            this.dgvWarnCode.RowTemplate.Height = 27;
            this.dgvWarnCode.Size = new System.Drawing.Size(268, 621);
            this.dgvWarnCode.TabIndex = 3;
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
            // dgcWarnDesc
            // 
            this.dgcWarnDesc.DataPropertyName = "WarnDesc";
            this.dgcWarnDesc.HeaderText = "报警详情";
            this.dgcWarnDesc.MinimumWidth = 6;
            this.dgcWarnDesc.Name = "dgcWarnDesc";
            this.dgcWarnDesc.ReadOnly = true;
            // 
            // FrmBaseMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.gbMachine);
            this.Controls.Add(this.menuStripMachine);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBaseMachine";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.Text = "设备管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseMachine_Load);
            this.gbMachine.ResumeLayout(false);
            this.gbMachine.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTheoryCT)).EndInit();
            this.menuStripMachine.ResumeLayout(false);
            this.menuStripMachine.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMachine;
        private System.Windows.Forms.MenuStrip menuStripMachine;
        private System.Windows.Forms.Label labMahineNo;
        private System.Windows.Forms.Label labMachineCode;
        private System.Windows.Forms.TextBox tbMachineName;
        private System.Windows.Forms.Label labMachineName;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiImport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ComboBox cmbLine;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.TextBox tbMachineCode;
        private System.Windows.Forms.ToolStripMenuItem tsmiMachineReport;
        private System.Windows.Forms.Label labTheoryCT;
        private System.Windows.Forms.NumericUpDown nudTheoryCT;
        private System.Windows.Forms.Label labMachineCategory;
        private System.Windows.Forms.TextBox tbMachineCategory;
        private System.Windows.Forms.TextBox tbMachineNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiWarnCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvWarnCode;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl sgridMachine;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gcMachineCode;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gcMachineName;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gcTheoryCT;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gcMachineCategory;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gcLine;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gcIsLink;
        private System.Windows.Forms.NumericUpDown nudPower;
        private System.Windows.Forms.Label labPower;
        private System.Windows.Forms.ComboBox cmbAssetNo;
        private System.Windows.Forms.Label labAsset;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gcPower;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gcAssetNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiMachinePoint;
        private System.Windows.Forms.ToolStripMenuItem tmsiMachineState;
        private DevComponents.DotNetBar.Controls.SwitchButton swbtnShow;
        private System.Windows.Forms.CheckBox chkIsLink;
        private System.Windows.Forms.TextBox txbAssetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWarnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWarnDesc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
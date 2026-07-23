namespace Machine
{
    partial class FrmResumeReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResumeReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStripMachine = new System.Windows.Forms.MenuStrip();
            this.tsmRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelFloor = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labMessage = new System.Windows.Forms.Label();
            this.labPageNo = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.nudPageNo = new System.Windows.Forms.NumericUpDown();
            this.tabPanelRoot = new System.Windows.Forms.TableLayoutPanel();
            this.labTitle = new System.Windows.Forms.Label();
            this.labFloor = new System.Windows.Forms.Label();
            this.tabPanelCategory = new System.Windows.Forms.TableLayoutPanel();
            this.chkA = new System.Windows.Forms.CheckBox();
            this.chkB = new System.Windows.Forms.CheckBox();
            this.chkC = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.labEntryDate = new System.Windows.Forms.Label();
            this.labAssetName = new System.Windows.Forms.Label();
            this.txbAssetName = new System.Windows.Forms.TextBox();
            this.labAssetNo = new System.Windows.Forms.Label();
            this.txbAssetNo = new System.Windows.Forms.TextBox();
            this.labMadeFactory = new System.Windows.Forms.Label();
            this.txbMadeFactory = new System.Windows.Forms.TextBox();
            this.labModel = new System.Windows.Forms.Label();
            this.txbModel = new System.Windows.Forms.TextBox();
            this.labControlNo = new System.Windows.Forms.Label();
            this.txbControlNo = new System.Windows.Forms.TextBox();
            this.txbEntryDate = new System.Windows.Forms.TextBox();
            this.tabPanelGrid = new System.Windows.Forms.TableLayoutPanel();
            this.label18 = new System.Windows.Forms.Label();
            this.dgvMaintenance = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcExecuteCategory = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.dgcExecuteDate = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.dgcTakenDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcExecuteState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcExecuteUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcNextExecuteDate = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.dgcRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label19 = new System.Windows.Forms.Label();
            this.dgvRepair = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcRepairDate = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.dgcAbnormalDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRepairReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRepairUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCheckResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRepairRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRepairMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dgvRepairMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvRepairMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvMaintenanceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dgvMaintenanceMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvMaintenanceMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPage = new System.Windows.Forms.Panel();
            this.menuStripMachine.SuspendLayout();
            this.panelFloor.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageNo)).BeginInit();
            this.tabPanelRoot.SuspendLayout();
            this.tabPanelCategory.SuspendLayout();
            this.tabPanelInfo.SuspendLayout();
            this.tabPanelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepair)).BeginInit();
            this.dgvRepairMenu.SuspendLayout();
            this.dgvMaintenanceMenu.SuspendLayout();
            this.panelPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMachine
            // 
            this.menuStripMachine.BackColor = System.Drawing.Color.Transparent;
            this.menuStripMachine.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMachine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmRefresh,
            this.tsmiCreate,
            this.tsmiExport,
            this.tsmiExportAll});
            this.menuStripMachine.Location = new System.Drawing.Point(5, 0);
            this.menuStripMachine.Name = "menuStripMachine";
            this.menuStripMachine.Size = new System.Drawing.Size(1172, 35);
            this.menuStripMachine.TabIndex = 0;
            this.menuStripMachine.Text = "menuStrip1";
            // 
            // tsmRefresh
            // 
            this.tsmRefresh.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmRefresh.Image")));
            this.tsmRefresh.Name = "tsmRefresh";
            this.tsmRefresh.Size = new System.Drawing.Size(86, 31);
            this.tsmRefresh.Text = "刷新";
            this.tsmRefresh.Click += new System.EventHandler(this.tsmRefresh_Click);
            // 
            // tsmiCreate
            // 
            this.tsmiCreate.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiCreate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCreate.Image")));
            this.tsmiCreate.Name = "tsmiCreate";
            this.tsmiCreate.Size = new System.Drawing.Size(126, 31);
            this.tsmiCreate.Text = "创建新页";
            this.tsmiCreate.Click += new System.EventHandler(this.tsmiCreate_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiExport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExport.Image")));
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(126, 31);
            this.tsmiExport.Text = "导出本页";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // tsmiExportAll
            // 
            this.tsmiExportAll.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiExportAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExportAll.Image")));
            this.tsmiExportAll.Name = "tsmiExportAll";
            this.tsmiExportAll.Size = new System.Drawing.Size(126, 31);
            this.tsmiExportAll.Text = "全部导出";
            this.tsmiExportAll.Click += new System.EventHandler(this.tsmiExportAll_Click);
            // 
            // panelFloor
            // 
            this.panelFloor.Controls.Add(this.tableLayoutPanel1);
            this.panelFloor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFloor.Location = new System.Drawing.Point(5, 1018);
            this.panelFloor.Name = "panelFloor";
            this.panelFloor.Size = new System.Drawing.Size(1172, 32);
            this.panelFloor.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labPageNo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label15, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudPageNo, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1172, 32);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.labMessage.ForeColor = System.Drawing.Color.Green;
            this.labMessage.Location = new System.Drawing.Point(3, 8);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 10;
            this.labMessage.Text = "提示";
            // 
            // labPageNo
            // 
            this.labPageNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labPageNo.AutoSize = true;
            this.labPageNo.Location = new System.Drawing.Point(1015, 8);
            this.labPageNo.Name = "labPageNo";
            this.labPageNo.Size = new System.Drawing.Size(75, 15);
            this.labPageNo.TabIndex = 7;
            this.labPageNo.Text = "页，共0页";
            this.labPageNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(907, 8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 15);
            this.label15.TabIndex = 5;
            this.label15.Text = "第";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudPageNo
            // 
            this.nudPageNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudPageNo.Location = new System.Drawing.Point(935, 3);
            this.nudPageNo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPageNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPageNo.Name = "nudPageNo";
            this.nudPageNo.Size = new System.Drawing.Size(74, 25);
            this.nudPageNo.TabIndex = 4;
            this.nudPageNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPageNo.ValueChanged += new System.EventHandler(this.nudPageNo_ValueChanged);
            // 
            // tabPanelRoot
            // 
            this.tabPanelRoot.AutoScroll = true;
            this.tabPanelRoot.BackColor = System.Drawing.Color.Transparent;
            this.tabPanelRoot.ColumnCount = 1;
            this.tabPanelRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabPanelRoot.Controls.Add(this.labTitle, 0, 0);
            this.tabPanelRoot.Controls.Add(this.labFloor, 0, 4);
            this.tabPanelRoot.Controls.Add(this.tabPanelCategory, 0, 1);
            this.tabPanelRoot.Controls.Add(this.tabPanelInfo, 0, 2);
            this.tabPanelRoot.Controls.Add(this.tabPanelGrid, 0, 3);
            this.tabPanelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelRoot.Location = new System.Drawing.Point(0, 0);
            this.tabPanelRoot.Name = "tabPanelRoot";
            this.tabPanelRoot.RowCount = 5;
            this.tabPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tabPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tabPanelRoot.Size = new System.Drawing.Size(1172, 983);
            this.tabPanelRoot.TabIndex = 61;
            // 
            // labTitle
            // 
            this.labTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTitle.Font = new System.Drawing.Font("微软雅黑", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.Location = new System.Drawing.Point(3, 0);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(1166, 100);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "固 定 资 产 履 历 表";
            this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labFloor
            // 
            this.labFloor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFloor.AutoSize = true;
            this.labFloor.Location = new System.Drawing.Point(3, 991);
            this.labFloor.Name = "labFloor";
            this.labFloor.Size = new System.Drawing.Size(279, 15);
            this.labFloor.TabIndex = 5;
            this.labFloor.Text = "备注：a. A类固定资产维修后需再校正；";
            this.labFloor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPanelCategory
            // 
            this.tabPanelCategory.AutoSize = true;
            this.tabPanelCategory.ColumnCount = 4;
            this.tabPanelCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabPanelCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabPanelCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabPanelCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabPanelCategory.Controls.Add(this.chkA, 1, 0);
            this.tabPanelCategory.Controls.Add(this.chkB, 2, 0);
            this.tabPanelCategory.Controls.Add(this.chkC, 3, 0);
            this.tabPanelCategory.Controls.Add(this.label17, 0, 0);
            this.tabPanelCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelCategory.Location = new System.Drawing.Point(3, 103);
            this.tabPanelCategory.Name = "tabPanelCategory";
            this.tabPanelCategory.RowCount = 1;
            this.tabPanelCategory.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelCategory.Size = new System.Drawing.Size(1166, 27);
            this.tabPanelCategory.TabIndex = 1;
            // 
            // chkA
            // 
            this.chkA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkA.AutoSize = true;
            this.chkA.Enabled = false;
            this.chkA.Location = new System.Drawing.Point(127, 5);
            this.chkA.Name = "chkA";
            this.chkA.Size = new System.Drawing.Size(112, 19);
            this.chkA.TabIndex = 2;
            this.chkA.Text = "A类检测仪器";
            this.chkA.UseVisualStyleBackColor = true;
            // 
            // chkB
            // 
            this.chkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkB.AutoSize = true;
            this.chkB.Enabled = false;
            this.chkB.Location = new System.Drawing.Point(245, 5);
            this.chkB.Name = "chkB";
            this.chkB.Size = new System.Drawing.Size(112, 19);
            this.chkB.TabIndex = 4;
            this.chkB.Text = "B类生产设备";
            this.chkB.UseVisualStyleBackColor = true;
            // 
            // chkC
            // 
            this.chkC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkC.AutoSize = true;
            this.chkC.Enabled = false;
            this.chkC.Location = new System.Drawing.Point(363, 5);
            this.chkC.Name = "chkC";
            this.chkC.Size = new System.Drawing.Size(112, 19);
            this.chkC.TabIndex = 3;
            this.chkC.Text = "C类基础设施";
            this.chkC.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 3);
            this.label17.Margin = new System.Windows.Forms.Padding(3);
            this.label17.Name = "label17";
            this.label17.Padding = new System.Windows.Forms.Padding(3);
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label17.Size = new System.Drawing.Size(118, 21);
            this.label17.TabIndex = 0;
            this.label17.Text = "固定资产种类：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPanelInfo
            // 
            this.tabPanelInfo.AutoSize = true;
            this.tabPanelInfo.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tabPanelInfo.ColumnCount = 4;
            this.tabPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tabPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tabPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabPanelInfo.Controls.Add(this.labEntryDate, 0, 2);
            this.tabPanelInfo.Controls.Add(this.labAssetName, 0, 0);
            this.tabPanelInfo.Controls.Add(this.txbAssetName, 1, 0);
            this.tabPanelInfo.Controls.Add(this.labAssetNo, 2, 0);
            this.tabPanelInfo.Controls.Add(this.txbAssetNo, 3, 0);
            this.tabPanelInfo.Controls.Add(this.labMadeFactory, 0, 1);
            this.tabPanelInfo.Controls.Add(this.txbMadeFactory, 1, 1);
            this.tabPanelInfo.Controls.Add(this.labModel, 2, 1);
            this.tabPanelInfo.Controls.Add(this.txbModel, 3, 1);
            this.tabPanelInfo.Controls.Add(this.labControlNo, 2, 2);
            this.tabPanelInfo.Controls.Add(this.txbControlNo, 3, 2);
            this.tabPanelInfo.Controls.Add(this.txbEntryDate, 1, 2);
            this.tabPanelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelInfo.Location = new System.Drawing.Point(3, 136);
            this.tabPanelInfo.Name = "tabPanelInfo";
            this.tabPanelInfo.RowCount = 3;
            this.tabPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelInfo.Size = new System.Drawing.Size(1166, 91);
            this.tabPanelInfo.TabIndex = 2;
            // 
            // labEntryDate
            // 
            this.labEntryDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labEntryDate.AutoSize = true;
            this.labEntryDate.Location = new System.Drawing.Point(81, 68);
            this.labEntryDate.Name = "labEntryDate";
            this.labEntryDate.Size = new System.Drawing.Size(67, 15);
            this.labEntryDate.TabIndex = 60;
            this.labEntryDate.Text = "进厂日期";
            this.labEntryDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labAssetName
            // 
            this.labAssetName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAssetName.AutoSize = true;
            this.labAssetName.Location = new System.Drawing.Point(81, 8);
            this.labAssetName.Name = "labAssetName";
            this.labAssetName.Size = new System.Drawing.Size(67, 15);
            this.labAssetName.TabIndex = 50;
            this.labAssetName.Text = "资产名称";
            this.labAssetName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbAssetName
            // 
            this.txbAssetName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbAssetName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbAssetName.Font = new System.Drawing.Font("宋体", 12F);
            this.txbAssetName.Location = new System.Drawing.Point(155, 4);
            this.txbAssetName.Name = "txbAssetName";
            this.txbAssetName.ReadOnly = true;
            this.txbAssetName.Size = new System.Drawing.Size(424, 23);
            this.txbAssetName.TabIndex = 51;
            // 
            // labAssetNo
            // 
            this.labAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAssetNo.AutoSize = true;
            this.labAssetNo.Location = new System.Drawing.Point(663, 8);
            this.labAssetNo.Name = "labAssetNo";
            this.labAssetNo.Size = new System.Drawing.Size(67, 15);
            this.labAssetNo.TabIndex = 52;
            this.labAssetNo.Text = "资产编号";
            this.labAssetNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbAssetNo
            // 
            this.txbAssetNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbAssetNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbAssetNo.Font = new System.Drawing.Font("宋体", 12F);
            this.txbAssetNo.Location = new System.Drawing.Point(737, 4);
            this.txbAssetNo.Name = "txbAssetNo";
            this.txbAssetNo.ReadOnly = true;
            this.txbAssetNo.Size = new System.Drawing.Size(425, 23);
            this.txbAssetNo.TabIndex = 53;
            // 
            // labMadeFactory
            // 
            this.labMadeFactory.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMadeFactory.AutoSize = true;
            this.labMadeFactory.Location = new System.Drawing.Point(81, 38);
            this.labMadeFactory.Name = "labMadeFactory";
            this.labMadeFactory.Size = new System.Drawing.Size(67, 15);
            this.labMadeFactory.TabIndex = 54;
            this.labMadeFactory.Text = "制造厂商";
            this.labMadeFactory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbMadeFactory
            // 
            this.txbMadeFactory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbMadeFactory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbMadeFactory.Font = new System.Drawing.Font("宋体", 12F);
            this.txbMadeFactory.Location = new System.Drawing.Point(155, 34);
            this.txbMadeFactory.Name = "txbMadeFactory";
            this.txbMadeFactory.ReadOnly = true;
            this.txbMadeFactory.Size = new System.Drawing.Size(424, 23);
            this.txbMadeFactory.TabIndex = 56;
            // 
            // labModel
            // 
            this.labModel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labModel.AutoSize = true;
            this.labModel.Location = new System.Drawing.Point(693, 38);
            this.labModel.Name = "labModel";
            this.labModel.Size = new System.Drawing.Size(37, 15);
            this.labModel.TabIndex = 57;
            this.labModel.Text = "型号";
            this.labModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbModel
            // 
            this.txbModel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbModel.Font = new System.Drawing.Font("宋体", 12F);
            this.txbModel.Location = new System.Drawing.Point(737, 34);
            this.txbModel.Name = "txbModel";
            this.txbModel.ReadOnly = true;
            this.txbModel.Size = new System.Drawing.Size(425, 23);
            this.txbModel.TabIndex = 59;
            // 
            // labControlNo
            // 
            this.labControlNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labControlNo.AutoSize = true;
            this.labControlNo.Location = new System.Drawing.Point(663, 68);
            this.labControlNo.Name = "labControlNo";
            this.labControlNo.Size = new System.Drawing.Size(67, 15);
            this.labControlNo.TabIndex = 62;
            this.labControlNo.Text = "管制编号";
            this.labControlNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbControlNo
            // 
            this.txbControlNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbControlNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbControlNo.Font = new System.Drawing.Font("宋体", 12F);
            this.txbControlNo.Location = new System.Drawing.Point(737, 64);
            this.txbControlNo.Name = "txbControlNo";
            this.txbControlNo.ReadOnly = true;
            this.txbControlNo.Size = new System.Drawing.Size(425, 23);
            this.txbControlNo.TabIndex = 63;
            // 
            // txbEntryDate
            // 
            this.txbEntryDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbEntryDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEntryDate.Font = new System.Drawing.Font("宋体", 12F);
            this.txbEntryDate.Location = new System.Drawing.Point(155, 64);
            this.txbEntryDate.Name = "txbEntryDate";
            this.txbEntryDate.ReadOnly = true;
            this.txbEntryDate.Size = new System.Drawing.Size(424, 23);
            this.txbEntryDate.TabIndex = 64;
            // 
            // tabPanelGrid
            // 
            this.tabPanelGrid.AutoSize = true;
            this.tabPanelGrid.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tabPanelGrid.ColumnCount = 2;
            this.tabPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tabPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabPanelGrid.Controls.Add(this.label18, 0, 0);
            this.tabPanelGrid.Controls.Add(this.dgvMaintenance, 1, 0);
            this.tabPanelGrid.Controls.Add(this.label19, 0, 1);
            this.tabPanelGrid.Controls.Add(this.dgvRepair, 1, 1);
            this.tabPanelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelGrid.Location = new System.Drawing.Point(3, 233);
            this.tabPanelGrid.Name = "tabPanelGrid";
            this.tabPanelGrid.RowCount = 2;
            this.tabPanelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tabPanelGrid.Size = new System.Drawing.Size(1166, 750);
            this.tabPanelGrid.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("宋体", 15F);
            this.label18.Location = new System.Drawing.Point(4, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 511);
            this.label18.TabIndex = 3;
            this.label18.Text = "定期保养／  校验     实施记录";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvMaintenance
            // 
            this.dgvMaintenance.AllowUserToAddRows = false;
            this.dgvMaintenance.AllowUserToDeleteRows = false;
            this.dgvMaintenance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaintenance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaintenance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcExecuteCategory,
            this.dgcExecuteDate,
            this.dgcTakenDept,
            this.dgcExecuteState,
            this.dgcExecuteUser,
            this.dgcNextExecuteDate,
            this.dgcRemark});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaintenance.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaintenance.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvMaintenance.Location = new System.Drawing.Point(85, 4);
            this.dgvMaintenance.MultiSelect = false;
            this.dgvMaintenance.Name = "dgvMaintenance";
            this.dgvMaintenance.RowHeadersVisible = false;
            this.dgvMaintenance.RowHeadersWidth = 51;
            this.dgvMaintenance.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaintenance.Size = new System.Drawing.Size(1077, 505);
            this.dgvMaintenance.TabIndex = 5;
            this.dgvMaintenance.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseUp);
            this.dgvMaintenance.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // dgcExecuteCategory
            // 
            this.dgcExecuteCategory.DataPropertyName = "ExecuteCategory";
            this.dgcExecuteCategory.DisplayMember = "Text";
            this.dgcExecuteCategory.DropDownHeight = 106;
            this.dgcExecuteCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dgcExecuteCategory.DropDownWidth = 121;
            this.dgcExecuteCategory.FillWeight = 68.62745F;
            this.dgcExecuteCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dgcExecuteCategory.HeaderText = "实施类别";
            this.dgcExecuteCategory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgcExecuteCategory.IntegralHeight = false;
            this.dgcExecuteCategory.ItemHeight = 15;
            this.dgcExecuteCategory.Items.AddRange(new object[] {
            "保养",
            "校验"});
            this.dgcExecuteCategory.MinimumWidth = 75;
            this.dgcExecuteCategory.Name = "dgcExecuteCategory";
            this.dgcExecuteCategory.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcExecuteCategory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // dgcExecuteDate
            // 
            this.dgcExecuteDate.AutoSelectDate = true;
            // 
            // 
            // 
            this.dgcExecuteDate.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dgcExecuteDate.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.dgcExecuteDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcExecuteDate.BackgroundStyle.TextColor = System.Drawing.SystemColors.ControlText;
            this.dgcExecuteDate.CustomFormat = "yyyy-MM-dd";
            this.dgcExecuteDate.DataPropertyName = "ExecuteDate";
            this.dgcExecuteDate.FillWeight = 68.62745F;
            this.dgcExecuteDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dgcExecuteDate.HeaderText = "实施日期";
            this.dgcExecuteDate.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.dgcExecuteDate.MinimumWidth = 80;
            // 
            // 
            // 
            // 
            // 
            // 
            this.dgcExecuteDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcExecuteDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            // 
            // 
            // 
            this.dgcExecuteDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcExecuteDate.MonthCalendar.DisplayMonth = new System.DateTime(2024, 2, 1, 0, 0, 0, 0);
            this.dgcExecuteDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            // 
            // 
            // 
            this.dgcExecuteDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcExecuteDate.Name = "dgcExecuteDate";
            this.dgcExecuteDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgcTakenDept
            // 
            this.dgcTakenDept.DataPropertyName = "TakenDept";
            this.dgcTakenDept.FillWeight = 68.62745F;
            this.dgcTakenDept.HeaderText = "保管部门";
            this.dgcTakenDept.MinimumWidth = 80;
            this.dgcTakenDept.Name = "dgcTakenDept";
            this.dgcTakenDept.ReadOnly = true;
            // 
            // dgcExecuteState
            // 
            this.dgcExecuteState.DataPropertyName = "ExecuteState";
            this.dgcExecuteState.FillWeight = 68.62745F;
            this.dgcExecuteState.HeaderText = "实施状况";
            this.dgcExecuteState.MinimumWidth = 20;
            this.dgcExecuteState.Name = "dgcExecuteState";
            // 
            // dgcExecuteUser
            // 
            this.dgcExecuteUser.DataPropertyName = "ExecuteUser";
            this.dgcExecuteUser.FillWeight = 68.62745F;
            this.dgcExecuteUser.HeaderText = "实施人员";
            this.dgcExecuteUser.MinimumWidth = 80;
            this.dgcExecuteUser.Name = "dgcExecuteUser";
            // 
            // dgcNextExecuteDate
            // 
            // 
            // 
            // 
            this.dgcNextExecuteDate.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dgcNextExecuteDate.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.dgcNextExecuteDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcNextExecuteDate.BackgroundStyle.TextColor = System.Drawing.SystemColors.ControlText;
            this.dgcNextExecuteDate.CustomFormat = "yyyy-MM-dd";
            this.dgcNextExecuteDate.DataPropertyName = "NextExecuteDate";
            this.dgcNextExecuteDate.FillWeight = 68.62745F;
            this.dgcNextExecuteDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dgcNextExecuteDate.HeaderText = "下次实施日期";
            this.dgcNextExecuteDate.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.dgcNextExecuteDate.MinimumWidth = 80;
            // 
            // 
            // 
            // 
            // 
            // 
            this.dgcNextExecuteDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcNextExecuteDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            // 
            // 
            // 
            this.dgcNextExecuteDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcNextExecuteDate.MonthCalendar.DisplayMonth = new System.DateTime(2024, 2, 1, 0, 0, 0, 0);
            this.dgcNextExecuteDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            // 
            // 
            // 
            this.dgcNextExecuteDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcNextExecuteDate.Name = "dgcNextExecuteDate";
            this.dgcNextExecuteDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgcRemark
            // 
            this.dgcRemark.DataPropertyName = "Remark";
            this.dgcRemark.FillWeight = 68.62745F;
            this.dgcRemark.HeaderText = "备注(校验报告编号)";
            this.dgcRemark.MinimumWidth = 20;
            this.dgcRemark.Name = "dgcRemark";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("宋体", 15F);
            this.label19.Location = new System.Drawing.Point(4, 513);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 236);
            this.label19.TabIndex = 6;
            this.label19.Text = "维修记录";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvRepair
            // 
            this.dgvRepair.AllowUserToAddRows = false;
            this.dgvRepair.AllowUserToDeleteRows = false;
            this.dgvRepair.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRepair.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRepair.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcRepairDate,
            this.dgcAbnormalDesc,
            this.dgcRepairReason,
            this.dgcRepairUser,
            this.dgcCheckResult,
            this.dgcRepairRemark});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRepair.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRepair.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRepair.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvRepair.Location = new System.Drawing.Point(85, 516);
            this.dgvRepair.Name = "dgvRepair";
            this.dgvRepair.RowHeadersVisible = false;
            this.dgvRepair.RowHeadersWidth = 51;
            this.dgvRepair.RowTemplate.ContextMenuStrip = this.dgvRepairMenu;
            this.dgvRepair.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRepair.Size = new System.Drawing.Size(1077, 230);
            this.dgvRepair.TabIndex = 7;
            this.dgvRepair.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseUp);
            this.dgvRepair.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // dgcRepairDate
            // 
            // 
            // 
            // 
            this.dgcRepairDate.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dgcRepairDate.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.dgcRepairDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcRepairDate.BackgroundStyle.TextColor = System.Drawing.SystemColors.ControlText;
            this.dgcRepairDate.CustomFormat = "yyyy-MM-dd";
            this.dgcRepairDate.DataPropertyName = "RepairDate";
            this.dgcRepairDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dgcRepairDate.HeaderText = "维修日期";
            this.dgcRepairDate.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.dgcRepairDate.MinimumWidth = 6;
            // 
            // 
            // 
            // 
            // 
            // 
            this.dgcRepairDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcRepairDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            // 
            // 
            // 
            this.dgcRepairDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcRepairDate.MonthCalendar.DisplayMonth = new System.DateTime(2024, 2, 1, 0, 0, 0, 0);
            this.dgcRepairDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            // 
            // 
            // 
            this.dgcRepairDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dgcRepairDate.Name = "dgcRepairDate";
            this.dgcRepairDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgcAbnormalDesc
            // 
            this.dgcAbnormalDesc.DataPropertyName = "AbnormalDesc";
            this.dgcAbnormalDesc.HeaderText = "异常描述";
            this.dgcAbnormalDesc.MinimumWidth = 6;
            this.dgcAbnormalDesc.Name = "dgcAbnormalDesc";
            // 
            // dgcRepairReason
            // 
            this.dgcRepairReason.DataPropertyName = "RepairReason";
            this.dgcRepairReason.HeaderText = "维修原因";
            this.dgcRepairReason.MinimumWidth = 6;
            this.dgcRepairReason.Name = "dgcRepairReason";
            // 
            // dgcRepairUser
            // 
            this.dgcRepairUser.DataPropertyName = "RepairUser";
            this.dgcRepairUser.HeaderText = "维修人员";
            this.dgcRepairUser.MinimumWidth = 6;
            this.dgcRepairUser.Name = "dgcRepairUser";
            // 
            // dgcCheckResult
            // 
            this.dgcCheckResult.DataPropertyName = "CheckResult";
            this.dgcCheckResult.HeaderText = "验收结果";
            this.dgcCheckResult.MinimumWidth = 6;
            this.dgcCheckResult.Name = "dgcCheckResult";
            // 
            // dgcRepairRemark
            // 
            this.dgcRepairRemark.DataPropertyName = "Remark";
            this.dgcRepairRemark.HeaderText = "备注/维修单号";
            this.dgcRepairRemark.MinimumWidth = 6;
            this.dgcRepairRemark.Name = "dgcRepairRemark";
            // 
            // dgvRepairMenu
            // 
            this.dgvRepairMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.dgvRepairMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dgvRepairMenuSave,
            this.dgvRepairMenuDelete});
            this.dgvRepairMenu.Name = "dgvRepairMenu";
            this.dgvRepairMenu.Size = new System.Drawing.Size(109, 52);
            // 
            // dgvRepairMenuSave
            // 
            this.dgvRepairMenuSave.Name = "dgvRepairMenuSave";
            this.dgvRepairMenuSave.Size = new System.Drawing.Size(108, 24);
            this.dgvRepairMenuSave.Text = "保存";
            this.dgvRepairMenuSave.Click += new System.EventHandler(this.dgvRepairMenuSave_Click);
            // 
            // dgvRepairMenuDelete
            // 
            this.dgvRepairMenuDelete.Name = "dgvRepairMenuDelete";
            this.dgvRepairMenuDelete.Size = new System.Drawing.Size(108, 24);
            this.dgvRepairMenuDelete.Text = "删除";
            this.dgvRepairMenuDelete.Click += new System.EventHandler(this.dgvRepairMenuDelete_Click);
            // 
            // dgvMaintenanceMenu
            // 
            this.dgvMaintenanceMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.dgvMaintenanceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dgvMaintenanceMenuSave,
            this.dgvMaintenanceMenuDelete});
            this.dgvMaintenanceMenu.Name = "dgvResumeMenu";
            this.dgvMaintenanceMenu.Size = new System.Drawing.Size(109, 52);
            // 
            // dgvMaintenanceMenuSave
            // 
            this.dgvMaintenanceMenuSave.Name = "dgvMaintenanceMenuSave";
            this.dgvMaintenanceMenuSave.Size = new System.Drawing.Size(108, 24);
            this.dgvMaintenanceMenuSave.Text = "保存";
            this.dgvMaintenanceMenuSave.Click += new System.EventHandler(this.dgvMaintenanceMenuSave_Click);
            // 
            // dgvMaintenanceMenuDelete
            // 
            this.dgvMaintenanceMenuDelete.Name = "dgvMaintenanceMenuDelete";
            this.dgvMaintenanceMenuDelete.Size = new System.Drawing.Size(108, 24);
            this.dgvMaintenanceMenuDelete.Text = "删除";
            this.dgvMaintenanceMenuDelete.Click += new System.EventHandler(this.dgvMaintenanceMenuDelete_Click);
            // 
            // panelPage
            // 
            this.panelPage.BackColor = System.Drawing.Color.Azure;
            this.panelPage.Controls.Add(this.tabPanelRoot);
            this.panelPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPage.Location = new System.Drawing.Point(5, 35);
            this.panelPage.Name = "panelPage";
            this.panelPage.Size = new System.Drawing.Size(1172, 983);
            this.panelPage.TabIndex = 7;
            // 
            // FrmResumeReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1182, 1055);
            this.Controls.Add(this.panelPage);
            this.Controls.Add(this.menuStripMachine);
            this.Controls.Add(this.panelFloor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmResumeReport";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "资产履历";
            this.Load += new System.EventHandler(this.FrmResumeReport_Load);
            this.menuStripMachine.ResumeLayout(false);
            this.menuStripMachine.PerformLayout();
            this.panelFloor.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageNo)).EndInit();
            this.tabPanelRoot.ResumeLayout(false);
            this.tabPanelRoot.PerformLayout();
            this.tabPanelCategory.ResumeLayout(false);
            this.tabPanelCategory.PerformLayout();
            this.tabPanelInfo.ResumeLayout(false);
            this.tabPanelInfo.PerformLayout();
            this.tabPanelGrid.ResumeLayout(false);
            this.tabPanelGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepair)).EndInit();
            this.dgvRepairMenu.ResumeLayout(false);
            this.dgvMaintenanceMenu.ResumeLayout(false);
            this.panelPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStripMachine;
        private System.Windows.Forms.ToolStripMenuItem tsmRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreate;
        private System.Windows.Forms.Panel panelFloor;
        private System.Windows.Forms.NumericUpDown nudPageNo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labPageNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportAll;
        private System.Windows.Forms.TableLayoutPanel tabPanelRoot;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.TableLayoutPanel tabPanelCategory;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chkA;
        private System.Windows.Forms.CheckBox chkB;
        private System.Windows.Forms.CheckBox chkC;
        private System.Windows.Forms.TableLayoutPanel tabPanelInfo;
        private System.Windows.Forms.Label labEntryDate;
        private System.Windows.Forms.Label labAssetName;
        private System.Windows.Forms.TextBox txbAssetName;
        private System.Windows.Forms.Label labAssetNo;
        private System.Windows.Forms.TextBox txbAssetNo;
        private System.Windows.Forms.Label labMadeFactory;
        private System.Windows.Forms.TextBox txbMadeFactory;
        private System.Windows.Forms.Label labModel;
        private System.Windows.Forms.TextBox txbModel;
        private System.Windows.Forms.Label labControlNo;
        private System.Windows.Forms.TextBox txbControlNo;
        private System.Windows.Forms.Panel panelPage;
        private System.Windows.Forms.TextBox txbEntryDate;
        private System.Windows.Forms.TableLayoutPanel tabPanelGrid;
        private System.Windows.Forms.Label label18;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvRepair;
        private System.Windows.Forms.Label label19;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvMaintenance;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn dgcExecuteCategory;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn dgcExecuteDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTakenDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcExecuteState;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcExecuteUser;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn dgcNextExecuteDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRemark;
        private System.Windows.Forms.ContextMenuStrip dgvMaintenanceMenu;
        private System.Windows.Forms.ToolStripMenuItem dgvMaintenanceMenuSave;
        private System.Windows.Forms.ToolStripMenuItem dgvMaintenanceMenuDelete;
        private System.Windows.Forms.ContextMenuStrip dgvRepairMenu;
        private System.Windows.Forms.ToolStripMenuItem dgvRepairMenuSave;
        private System.Windows.Forms.ToolStripMenuItem dgvRepairMenuDelete;
        private System.Windows.Forms.Label labFloor;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn dgcRepairDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAbnormalDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRepairReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRepairUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCheckResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRepairRemark;
    }
}
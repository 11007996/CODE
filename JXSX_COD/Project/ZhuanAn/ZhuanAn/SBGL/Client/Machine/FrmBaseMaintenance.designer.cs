namespace Machine
{
    partial class FrmBaseMaintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseMaintenance));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.cmbAssetNo = new System.Windows.Forms.ComboBox();
            this.labAssetNo = new System.Windows.Forms.Label();
            this.menuStripMachine = new System.Windows.Forms.MenuStrip();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMaintenanceItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMaintenanceItems = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTipDicData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHoliday = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBatchCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGlobalUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSyncItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labMessage = new System.Windows.Forms.Label();
            this.gbMaintenance = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labAssetName = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbAssetName = new System.Windows.Forms.ComboBox();
            this.labYear = new System.Windows.Forms.Label();
            this.dgvMaintenance = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcAssetNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMonth12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcResume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripMachine.SuspendLayout();
            this.gbMaintenance.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenance)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbAssetNo
            // 
            this.cmbAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbAssetNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAssetNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAssetNo.Location = new System.Drawing.Point(413, 6);
            this.cmbAssetNo.Name = "cmbAssetNo";
            this.cmbAssetNo.Size = new System.Drawing.Size(244, 23);
            this.cmbAssetNo.TabIndex = 31;
            // 
            // labAssetNo
            // 
            this.labAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAssetNo.AutoSize = true;
            this.labAssetNo.Location = new System.Drawing.Point(340, 10);
            this.labAssetNo.Name = "labAssetNo";
            this.labAssetNo.Size = new System.Drawing.Size(67, 15);
            this.labAssetNo.TabIndex = 28;
            this.labAssetNo.Text = "资产编号";
            // 
            // menuStripMachine
            // 
            this.menuStripMachine.BackColor = System.Drawing.Color.Transparent;
            this.menuStripMachine.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMachine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh,
            this.tsmiMaintenanceItem,
            this.tsmiMaintenanceItems,
            this.tsmiTipDicData,
            this.tsmiHoliday,
            this.tsmiBatchCreate,
            this.tsmiStat,
            this.tsmiGlobalUpdate,
            this.tsmiSyncItem});
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
            // tsmiMaintenanceItem
            // 
            this.tsmiMaintenanceItem.Name = "tsmiMaintenanceItem";
            this.tsmiMaintenanceItem.Size = new System.Drawing.Size(14, 31);
            // 
            // tsmiMaintenanceItems
            // 
            this.tsmiMaintenanceItems.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiMaintenanceItems.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMaintenanceItems.Image")));
            this.tsmiMaintenanceItems.Name = "tsmiMaintenanceItems";
            this.tsmiMaintenanceItems.Size = new System.Drawing.Size(126, 31);
            this.tsmiMaintenanceItems.Text = "保养项目";
            this.tsmiMaintenanceItems.Click += new System.EventHandler(this.tsmiMaintenanceItems_Click);
            // 
            // tsmiTipDicData
            // 
            this.tsmiTipDicData.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiTipDicData.Image = ((System.Drawing.Image)(resources.GetObject("tsmiTipDicData.Image")));
            this.tsmiTipDicData.Name = "tsmiTipDicData";
            this.tsmiTipDicData.Size = new System.Drawing.Size(126, 31);
            this.tsmiTipDicData.Text = "提示维护";
            this.tsmiTipDicData.Click += new System.EventHandler(this.tsmiTipDicData_Click);
            // 
            // tsmiHoliday
            // 
            this.tsmiHoliday.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiHoliday.Image = ((System.Drawing.Image)(resources.GetObject("tsmiHoliday.Image")));
            this.tsmiHoliday.Name = "tsmiHoliday";
            this.tsmiHoliday.Size = new System.Drawing.Size(126, 31);
            this.tsmiHoliday.Text = "节日维护";
            this.tsmiHoliday.Click += new System.EventHandler(this.tsmiHoliday_Click);
            // 
            // tsmiBatchCreate
            // 
            this.tsmiBatchCreate.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiBatchCreate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiBatchCreate.Image")));
            this.tsmiBatchCreate.Name = "tsmiBatchCreate";
            this.tsmiBatchCreate.Size = new System.Drawing.Size(126, 31);
            this.tsmiBatchCreate.Text = "批量创建";
            this.tsmiBatchCreate.Click += new System.EventHandler(this.tsmiBatchCreate_Click);
            // 
            // tsmiStat
            // 
            this.tsmiStat.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiStat.Image = ((System.Drawing.Image)(resources.GetObject("tsmiStat.Image")));
            this.tsmiStat.Name = "tsmiStat";
            this.tsmiStat.Size = new System.Drawing.Size(126, 31);
            this.tsmiStat.Text = "更新状态";
            this.tsmiStat.Click += new System.EventHandler(this.tsmiStat_Click);
            // 
            // tsmiGlobalUpdate
            // 
            this.tsmiGlobalUpdate.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiGlobalUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiGlobalUpdate.Image")));
            this.tsmiGlobalUpdate.Name = "tsmiGlobalUpdate";
            this.tsmiGlobalUpdate.Size = new System.Drawing.Size(126, 31);
            this.tsmiGlobalUpdate.Text = "全局维护";
            this.tsmiGlobalUpdate.Click += new System.EventHandler(this.tsmiGlobalUpdate_Click);
            // 
            // tsmiSyncItem
            // 
            this.tsmiSyncItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiSyncItem.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSyncItem.Image")));
            this.tsmiSyncItem.Name = "tsmiSyncItem";
            this.tsmiSyncItem.Size = new System.Drawing.Size(126, 31);
            this.tsmiSyncItem.Text = "项目同步";
            this.tsmiSyncItem.Click += new System.EventHandler(this.tsmiSyncItem_Click);
            // 
            // labMessage
            // 
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(15, 94);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(1570, 35);
            this.labMessage.TabIndex = 10;
            this.labMessage.Text = "提示";
            // 
            // gbMaintenance
            // 
            this.gbMaintenance.AutoSize = true;
            this.gbMaintenance.BackColor = System.Drawing.Color.Transparent;
            this.gbMaintenance.Controls.Add(this.tableLayoutPanel1);
            this.gbMaintenance.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMaintenance.Location = new System.Drawing.Point(15, 35);
            this.gbMaintenance.Name = "gbMaintenance";
            this.gbMaintenance.Size = new System.Drawing.Size(1570, 59);
            this.gbMaintenance.TabIndex = 1;
            this.gbMaintenance.TabStop = false;
            this.gbMaintenance.Text = "设备保养信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labAssetName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbYear, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbAssetName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labYear, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbAssetNo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labAssetNo, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1564, 35);
            this.tableLayoutPanel1.TabIndex = 47;
            // 
            // labAssetName
            // 
            this.labAssetName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAssetName.AutoSize = true;
            this.labAssetName.Location = new System.Drawing.Point(10, 10);
            this.labAssetName.Name = "labAssetName";
            this.labAssetName.Size = new System.Drawing.Size(67, 15);
            this.labAssetName.TabIndex = 46;
            this.labAssetName.Text = "资产名称";
            // 
            // cmbYear
            // 
            this.cmbYear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(743, 6);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(150, 23);
            this.cmbYear.TabIndex = 44;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // cmbAssetName
            // 
            this.cmbAssetName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbAssetName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAssetName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAssetName.FormattingEnabled = true;
            this.cmbAssetName.Location = new System.Drawing.Point(83, 6);
            this.cmbAssetName.Name = "cmbAssetName";
            this.cmbAssetName.Size = new System.Drawing.Size(244, 23);
            this.cmbAssetName.TabIndex = 45;
            this.cmbAssetName.SelectedIndexChanged += new System.EventHandler(this.cmbAssetName_SelectedIndexChanged);
            // 
            // labYear
            // 
            this.labYear.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labYear.AutoSize = true;
            this.labYear.Location = new System.Drawing.Point(700, 10);
            this.labYear.Name = "labYear";
            this.labYear.Size = new System.Drawing.Size(37, 15);
            this.labYear.TabIndex = 43;
            this.labYear.Text = "年份";
            // 
            // dgvMaintenance
            // 
            this.dgvMaintenance.AllowUserToAddRows = false;
            this.dgvMaintenance.AllowUserToDeleteRows = false;
            this.dgvMaintenance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaintenance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaintenance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcAssetNo,
            this.dgcAssetName,
            this.dgcMonth1,
            this.dgcMonth2,
            this.dgcMonth3,
            this.dgcMonth4,
            this.dgcMonth5,
            this.dgcMonth6,
            this.dgcMonth7,
            this.dgcMonth8,
            this.dgcMonth9,
            this.dgcMonth10,
            this.dgcMonth11,
            this.dgcMonth12,
            this.dgcYear,
            this.dgcResume});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaintenance.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvMaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaintenance.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvMaintenance.Location = new System.Drawing.Point(15, 129);
            this.dgvMaintenance.Name = "dgvMaintenance";
            this.dgvMaintenance.ReadOnly = true;
            this.dgvMaintenance.RowHeadersWidth = 51;
            this.dgvMaintenance.RowTemplate.Height = 27;
            this.dgvMaintenance.Size = new System.Drawing.Size(1570, 656);
            this.dgvMaintenance.TabIndex = 2;
            this.dgvMaintenance.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaintenance_CellClick);
            this.dgvMaintenance.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMaintenance_CellFormatting);
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
            // dgcAssetName
            // 
            this.dgcAssetName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetName.DataPropertyName = "AssetName";
            this.dgcAssetName.HeaderText = "资产名称";
            this.dgcAssetName.MinimumWidth = 6;
            this.dgcAssetName.Name = "dgcAssetName";
            this.dgcAssetName.ReadOnly = true;
            this.dgcAssetName.Width = 300;
            // 
            // dgcMonth1
            // 
            this.dgcMonth1.DataPropertyName = "1";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgcMonth1.HeaderText = "1月";
            this.dgcMonth1.MinimumWidth = 6;
            this.dgcMonth1.Name = "dgcMonth1";
            this.dgcMonth1.ReadOnly = true;
            this.dgcMonth1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth2
            // 
            this.dgcMonth2.DataPropertyName = "2";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgcMonth2.HeaderText = "2月";
            this.dgcMonth2.MinimumWidth = 6;
            this.dgcMonth2.Name = "dgcMonth2";
            this.dgcMonth2.ReadOnly = true;
            this.dgcMonth2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth3
            // 
            this.dgcMonth3.DataPropertyName = "3";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgcMonth3.HeaderText = "3月";
            this.dgcMonth3.MinimumWidth = 6;
            this.dgcMonth3.Name = "dgcMonth3";
            this.dgcMonth3.ReadOnly = true;
            this.dgcMonth3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth4
            // 
            this.dgcMonth4.DataPropertyName = "4";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth4.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgcMonth4.HeaderText = "4月";
            this.dgcMonth4.MinimumWidth = 6;
            this.dgcMonth4.Name = "dgcMonth4";
            this.dgcMonth4.ReadOnly = true;
            this.dgcMonth4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth5
            // 
            this.dgcMonth5.DataPropertyName = "5";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth5.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgcMonth5.HeaderText = "5月";
            this.dgcMonth5.MinimumWidth = 6;
            this.dgcMonth5.Name = "dgcMonth5";
            this.dgcMonth5.ReadOnly = true;
            this.dgcMonth5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth6
            // 
            this.dgcMonth6.DataPropertyName = "6";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth6.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgcMonth6.HeaderText = "6月";
            this.dgcMonth6.MinimumWidth = 6;
            this.dgcMonth6.Name = "dgcMonth6";
            this.dgcMonth6.ReadOnly = true;
            this.dgcMonth6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth7
            // 
            this.dgcMonth7.DataPropertyName = "7";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth7.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgcMonth7.HeaderText = "7月";
            this.dgcMonth7.MinimumWidth = 6;
            this.dgcMonth7.Name = "dgcMonth7";
            this.dgcMonth7.ReadOnly = true;
            this.dgcMonth7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth8
            // 
            this.dgcMonth8.DataPropertyName = "8";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth8.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgcMonth8.HeaderText = "8月";
            this.dgcMonth8.MinimumWidth = 6;
            this.dgcMonth8.Name = "dgcMonth8";
            this.dgcMonth8.ReadOnly = true;
            this.dgcMonth8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth9
            // 
            this.dgcMonth9.DataPropertyName = "9";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth9.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgcMonth9.HeaderText = "9月";
            this.dgcMonth9.MinimumWidth = 6;
            this.dgcMonth9.Name = "dgcMonth9";
            this.dgcMonth9.ReadOnly = true;
            this.dgcMonth9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth10
            // 
            this.dgcMonth10.DataPropertyName = "10";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth10.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgcMonth10.HeaderText = "10月";
            this.dgcMonth10.MinimumWidth = 6;
            this.dgcMonth10.Name = "dgcMonth10";
            this.dgcMonth10.ReadOnly = true;
            this.dgcMonth10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth11
            // 
            this.dgcMonth11.DataPropertyName = "11";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth11.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgcMonth11.HeaderText = "11月";
            this.dgcMonth11.MinimumWidth = 6;
            this.dgcMonth11.Name = "dgcMonth11";
            this.dgcMonth11.ReadOnly = true;
            this.dgcMonth11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcMonth12
            // 
            this.dgcMonth12.DataPropertyName = "12";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcMonth12.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgcMonth12.HeaderText = "12月";
            this.dgcMonth12.MinimumWidth = 6;
            this.dgcMonth12.Name = "dgcMonth12";
            this.dgcMonth12.ReadOnly = true;
            this.dgcMonth12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcMonth12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcYear
            // 
            this.dgcYear.DataPropertyName = "0";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcYear.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgcYear.HeaderText = "年表";
            this.dgcYear.MinimumWidth = 6;
            this.dgcYear.Name = "dgcYear";
            this.dgcYear.ReadOnly = true;
            this.dgcYear.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcYear.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcResume
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcResume.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgcResume.HeaderText = "履历";
            this.dgcResume.MinimumWidth = 6;
            this.dgcResume.Name = "dgcResume";
            this.dgcResume.ReadOnly = true;
            this.dgcResume.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcResume.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmBaseMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.Controls.Add(this.dgvMaintenance);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.gbMaintenance);
            this.Controls.Add(this.menuStripMachine);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBaseMaintenance";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.Text = "资产保养与履历";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseMaintenance_Load);
            this.menuStripMachine.ResumeLayout(false);
            this.menuStripMachine.PerformLayout();
            this.gbMaintenance.ResumeLayout(false);
            this.gbMaintenance.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMachine;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.Label labAssetNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiMaintenanceItem;
        private System.Windows.Forms.ComboBox cmbAssetNo;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.GroupBox gbMaintenance;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label labYear;
        private System.Windows.Forms.ToolStripMenuItem tsmiMaintenanceItems;
        private System.Windows.Forms.ToolStripMenuItem tsmiBatchCreate;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvMaintenance;
        private System.Windows.Forms.ComboBox cmbAssetName;
        private System.Windows.Forms.Label labAssetName;
        private System.Windows.Forms.ToolStripMenuItem tsmiTipDicData;
        private System.Windows.Forms.ToolStripMenuItem tsmiStat;
        private System.Windows.Forms.ToolStripMenuItem tsmiGlobalUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiSyncItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiHoliday;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMonth12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcResume;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
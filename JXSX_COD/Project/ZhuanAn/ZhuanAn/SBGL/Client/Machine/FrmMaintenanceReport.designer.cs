namespace Machine
{
    partial class FrmMaintenanceReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaintenanceReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txbAssetNo = new System.Windows.Forms.TextBox();
            this.labAssetNo = new System.Windows.Forms.Label();
            this.menuStripMachine = new System.Windows.Forms.MenuStrip();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMaintenanceItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtxbYearTip = new System.Windows.Forms.RichTextBox();
            this.dgvYear = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgvYear_Item = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.labYearTitle = new System.Windows.Forms.Label();
            this.dgvQuarter = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgvQuarter_Item = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.labQuarterTitle = new System.Windows.Forms.Label();
            this.dgvYearMonth = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgvYearMonth_Item = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.labYearMonthTitle = new System.Windows.Forms.Label();
            this.rtxbMonthTip = new System.Windows.Forms.RichTextBox();
            this.dgvMonth = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgvMonth_Item = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.labMonthTitle = new System.Windows.Forms.Label();
            this.dgvWeek = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgvWeek_Item = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.labWeekTitle = new System.Windows.Forms.Label();
            this.dgvDay = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgvDay_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labDayTitile = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            this.gbMaintenance = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.txbCostCenter = new System.Windows.Forms.TextBox();
            this.labMonth = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.txbModel = new System.Windows.Forms.TextBox();
            this.labYear = new System.Windows.Forms.Label();
            this.txbAssetName = new System.Windows.Forms.TextBox();
            this.labCostCenter = new System.Windows.Forms.Label();
            this.labAssetName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPanelMonth = new System.Windows.Forms.TableLayoutPanel();
            this.tabPanelYear = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStripMachine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuarter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYearMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDay)).BeginInit();
            this.gbMaintenance.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPanelMonth.SuspendLayout();
            this.tabPanelYear.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbAssetNo
            // 
            this.txbAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.txbAssetNo, 2);
            this.txbAssetNo.Location = new System.Drawing.Point(83, 5);
            this.txbAssetNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbAssetNo.Name = "txbAssetNo";
            this.txbAssetNo.ReadOnly = true;
            this.txbAssetNo.Size = new System.Drawing.Size(194, 25);
            this.txbAssetNo.TabIndex = 31;
            // 
            // labAssetNo
            // 
            this.labAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAssetNo.AutoSize = true;
            this.labAssetNo.Location = new System.Drawing.Point(10, 10);
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
            this.tsmiAdd,
            this.tsmiUpdate,
            this.tsmiDelete,
            this.tsmiSave,
            this.tsmiBack,
            this.tsmiImport,
            this.tsmiExport,
            this.tsmiMaintenanceItem});
            this.menuStripMachine.Location = new System.Drawing.Point(5, 0);
            this.menuStripMachine.Name = "menuStripMachine";
            this.menuStripMachine.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStripMachine.Size = new System.Drawing.Size(1473, 35);
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
            this.tsmiAdd.Text = "创建";
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
            // tsmiMaintenanceItem
            // 
            this.tsmiMaintenanceItem.Name = "tsmiMaintenanceItem";
            this.tsmiMaintenanceItem.Size = new System.Drawing.Size(14, 31);
            // 
            // rtxbYearTip
            // 
            this.rtxbYearTip.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtxbYearTip.ForeColor = System.Drawing.Color.Red;
            this.rtxbYearTip.Location = new System.Drawing.Point(39, 446);
            this.rtxbYearTip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtxbYearTip.Name = "rtxbYearTip";
            this.rtxbYearTip.Size = new System.Drawing.Size(1407, 52);
            this.rtxbYearTip.TabIndex = 58;
            this.rtxbYearTip.Text = "注:“V”表示“OK”,“X”表示“NG”,发现异常时应立即处理更换.\n   3月定义为一季度，保养时间为每季度后两天保养，最后不足一季度的天数按一季度最后一天保" +
    "养。年保养为每年最后一周。 逢节假日时间需提前完成。";
            // 
            // dgvYear
            // 
            this.dgvYear.AllowUserToAddRows = false;
            this.dgvYear.AllowUserToDeleteRows = false;
            this.dgvYear.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvYear.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYear.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvYear_Item});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvYear.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvYear.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvYear.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvYear.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvYear.Location = new System.Drawing.Point(39, 299);
            this.dgvYear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvYear.Name = "dgvYear";
            this.dgvYear.RowHeadersWidth = 51;
            this.dgvYear.RowTemplate.Height = 27;
            this.dgvYear.Size = new System.Drawing.Size(1407, 52);
            this.dgvYear.TabIndex = 8;
            this.dgvYear.Tag = "Y";
            this.dgvYear.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvYear.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // dgvYear_Item
            // 
            this.dgvYear_Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvYear_Item.DataPropertyName = "ItemName";
            this.dgvYear_Item.DisplayMember = "Text";
            this.dgvYear_Item.DropDownHeight = 106;
            this.dgvYear_Item.DropDownWidth = 121;
            this.dgvYear_Item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dgvYear_Item.HeaderText = "保养项目";
            this.dgvYear_Item.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvYear_Item.IntegralHeight = false;
            this.dgvYear_Item.ItemHeight = 20;
            this.dgvYear_Item.MinimumWidth = 6;
            this.dgvYear_Item.Name = "dgvYear_Item";
            this.dgvYear_Item.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvYear_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvYear_Item.Width = 300;
            // 
            // labYearTitle
            // 
            this.labYearTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labYearTitle.AutoSize = true;
            this.labYearTitle.Font = new System.Drawing.Font("宋体", 14F);
            this.labYearTitle.ForeColor = System.Drawing.Color.Blue;
            this.labYearTitle.Location = new System.Drawing.Point(6, 297);
            this.labYearTitle.Name = "labYearTitle";
            this.labYearTitle.Size = new System.Drawing.Size(24, 144);
            this.labYearTitle.TabIndex = 57;
            this.labYearTitle.Text = "年保养项目  ";
            this.labYearTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvQuarter
            // 
            this.dgvQuarter.AllowUserToAddRows = false;
            this.dgvQuarter.AllowUserToDeleteRows = false;
            this.dgvQuarter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuarter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuarter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvQuarter_Item});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvQuarter.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvQuarter.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvQuarter.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvQuarter.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvQuarter.Location = new System.Drawing.Point(39, 152);
            this.dgvQuarter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvQuarter.Name = "dgvQuarter";
            this.dgvQuarter.RowHeadersWidth = 51;
            this.dgvQuarter.RowTemplate.Height = 27;
            this.dgvQuarter.Size = new System.Drawing.Size(1407, 51);
            this.dgvQuarter.TabIndex = 9;
            this.dgvQuarter.Tag = "Q";
            this.dgvQuarter.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvQuarter.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // dgvQuarter_Item
            // 
            this.dgvQuarter_Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvQuarter_Item.DataPropertyName = "ItemName";
            this.dgvQuarter_Item.DisplayMember = "Text";
            this.dgvQuarter_Item.DropDownHeight = 106;
            this.dgvQuarter_Item.DropDownWidth = 121;
            this.dgvQuarter_Item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dgvQuarter_Item.HeaderText = "保养项目";
            this.dgvQuarter_Item.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvQuarter_Item.IntegralHeight = false;
            this.dgvQuarter_Item.ItemHeight = 20;
            this.dgvQuarter_Item.MinimumWidth = 6;
            this.dgvQuarter_Item.Name = "dgvQuarter_Item";
            this.dgvQuarter_Item.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvQuarter_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvQuarter_Item.Width = 300;
            // 
            // labQuarterTitle
            // 
            this.labQuarterTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labQuarterTitle.AutoSize = true;
            this.labQuarterTitle.Font = new System.Drawing.Font("宋体", 14F);
            this.labQuarterTitle.ForeColor = System.Drawing.Color.Blue;
            this.labQuarterTitle.Location = new System.Drawing.Point(6, 150);
            this.labQuarterTitle.Name = "labQuarterTitle";
            this.labQuarterTitle.Size = new System.Drawing.Size(24, 144);
            this.labQuarterTitle.TabIndex = 57;
            this.labQuarterTitle.Text = "季保养项目  ";
            this.labQuarterTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvYearMonth
            // 
            this.dgvYearMonth.AllowUserToAddRows = false;
            this.dgvYearMonth.AllowUserToDeleteRows = false;
            this.dgvYearMonth.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvYearMonth.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYearMonth.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvYearMonth_Item});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvYearMonth.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvYearMonth.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvYearMonth.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvYearMonth.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvYearMonth.Location = new System.Drawing.Point(39, 5);
            this.dgvYearMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvYearMonth.Name = "dgvYearMonth";
            this.dgvYearMonth.RowHeadersWidth = 51;
            this.dgvYearMonth.RowTemplate.Height = 27;
            this.dgvYearMonth.Size = new System.Drawing.Size(1407, 52);
            this.dgvYearMonth.TabIndex = 11;
            this.dgvYearMonth.Tag = "M";
            this.dgvYearMonth.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvYearMonth.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // dgvYearMonth_Item
            // 
            this.dgvYearMonth_Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvYearMonth_Item.DataPropertyName = "ItemName";
            this.dgvYearMonth_Item.DisplayMember = "Text";
            this.dgvYearMonth_Item.DropDownHeight = 106;
            this.dgvYearMonth_Item.DropDownWidth = 121;
            this.dgvYearMonth_Item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dgvYearMonth_Item.HeaderText = "保养项目";
            this.dgvYearMonth_Item.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvYearMonth_Item.IntegralHeight = false;
            this.dgvYearMonth_Item.ItemHeight = 20;
            this.dgvYearMonth_Item.MinimumWidth = 6;
            this.dgvYearMonth_Item.Name = "dgvYearMonth_Item";
            this.dgvYearMonth_Item.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvYearMonth_Item.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvYearMonth_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvYearMonth_Item.Width = 300;
            // 
            // labYearMonthTitle
            // 
            this.labYearMonthTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labYearMonthTitle.AutoSize = true;
            this.labYearMonthTitle.Font = new System.Drawing.Font("宋体", 14F);
            this.labYearMonthTitle.ForeColor = System.Drawing.Color.Blue;
            this.labYearMonthTitle.Location = new System.Drawing.Point(6, 3);
            this.labYearMonthTitle.Name = "labYearMonthTitle";
            this.labYearMonthTitle.Size = new System.Drawing.Size(24, 144);
            this.labYearMonthTitle.TabIndex = 56;
            this.labYearMonthTitle.Text = "月保养项目  ";
            this.labYearMonthTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtxbMonthTip
            // 
            this.rtxbMonthTip.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtxbMonthTip.ForeColor = System.Drawing.Color.Red;
            this.rtxbMonthTip.Location = new System.Drawing.Point(39, 446);
            this.rtxbMonthTip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtxbMonthTip.Name = "rtxbMonthTip";
            this.rtxbMonthTip.Size = new System.Drawing.Size(1407, 59);
            this.rtxbMonthTip.TabIndex = 57;
            this.rtxbMonthTip.Text = "注:“V”表示“OK”,“X”表示“NG”,发现异常时应立即处理更换.\n    7天定义为一周，保养时间为每周后两天保养，最后不足一周的天数按一周最后一天保养。月" +
    "保养为每月25号-30号。 逢节假日时间需提前完成。";
            // 
            // dgvMonth
            // 
            this.dgvMonth.AllowUserToAddRows = false;
            this.dgvMonth.AllowUserToDeleteRows = false;
            this.dgvMonth.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMonth.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonth.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvMonth_Item});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMonth.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvMonth.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvMonth.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvMonth.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvMonth.Location = new System.Drawing.Point(39, 299);
            this.dgvMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvMonth.Name = "dgvMonth";
            this.dgvMonth.RowHeadersWidth = 51;
            this.dgvMonth.RowTemplate.Height = 27;
            this.dgvMonth.Size = new System.Drawing.Size(1407, 55);
            this.dgvMonth.TabIndex = 6;
            this.dgvMonth.Tag = "M";
            this.dgvMonth.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvMonth.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // dgvMonth_Item
            // 
            this.dgvMonth_Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvMonth_Item.DataPropertyName = "ItemName";
            this.dgvMonth_Item.DisplayMember = "Text";
            this.dgvMonth_Item.DropDownHeight = 106;
            this.dgvMonth_Item.DropDownWidth = 121;
            this.dgvMonth_Item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dgvMonth_Item.HeaderText = "保养项目";
            this.dgvMonth_Item.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvMonth_Item.IntegralHeight = false;
            this.dgvMonth_Item.ItemHeight = 20;
            this.dgvMonth_Item.MinimumWidth = 6;
            this.dgvMonth_Item.Name = "dgvMonth_Item";
            this.dgvMonth_Item.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMonth_Item.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvMonth_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvMonth_Item.Width = 300;
            // 
            // labMonthTitle
            // 
            this.labMonthTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labMonthTitle.AutoSize = true;
            this.labMonthTitle.Font = new System.Drawing.Font("宋体", 14F);
            this.labMonthTitle.ForeColor = System.Drawing.Color.Blue;
            this.labMonthTitle.Location = new System.Drawing.Point(6, 297);
            this.labMonthTitle.Name = "labMonthTitle";
            this.labMonthTitle.Size = new System.Drawing.Size(24, 144);
            this.labMonthTitle.TabIndex = 55;
            this.labMonthTitle.Text = "月保养项目  ";
            this.labMonthTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvWeek
            // 
            this.dgvWeek.AllowUserToAddRows = false;
            this.dgvWeek.AllowUserToDeleteRows = false;
            this.dgvWeek.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWeek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeek.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvWeek_Item});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWeek.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvWeek.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvWeek.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvWeek.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvWeek.Location = new System.Drawing.Point(39, 152);
            this.dgvWeek.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvWeek.Name = "dgvWeek";
            this.dgvWeek.RowHeadersWidth = 51;
            this.dgvWeek.RowTemplate.Height = 27;
            this.dgvWeek.Size = new System.Drawing.Size(1407, 56);
            this.dgvWeek.TabIndex = 7;
            this.dgvWeek.Tag = "W";
            this.dgvWeek.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvWeek.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // dgvWeek_Item
            // 
            this.dgvWeek_Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvWeek_Item.DataPropertyName = "ItemName";
            this.dgvWeek_Item.DisplayMember = "Text";
            this.dgvWeek_Item.DropDownHeight = 106;
            this.dgvWeek_Item.DropDownWidth = 121;
            this.dgvWeek_Item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dgvWeek_Item.HeaderText = "保养项目";
            this.dgvWeek_Item.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvWeek_Item.IntegralHeight = false;
            this.dgvWeek_Item.ItemHeight = 20;
            this.dgvWeek_Item.MinimumWidth = 6;
            this.dgvWeek_Item.Name = "dgvWeek_Item";
            this.dgvWeek_Item.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvWeek_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvWeek_Item.Width = 300;
            // 
            // labWeekTitle
            // 
            this.labWeekTitle.AutoSize = true;
            this.labWeekTitle.Font = new System.Drawing.Font("宋体", 14F);
            this.labWeekTitle.ForeColor = System.Drawing.Color.Blue;
            this.labWeekTitle.Location = new System.Drawing.Point(6, 150);
            this.labWeekTitle.Name = "labWeekTitle";
            this.labWeekTitle.Size = new System.Drawing.Size(24, 144);
            this.labWeekTitle.TabIndex = 54;
            this.labWeekTitle.Text = "周保养项目  ";
            this.labWeekTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvDay
            // 
            this.dgvDay.AllowUserToAddRows = false;
            this.dgvDay.AllowUserToDeleteRows = false;
            this.dgvDay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDay_Item});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDay.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvDay.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvDay.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDay.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDay.Location = new System.Drawing.Point(39, 5);
            this.dgvDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDay.Name = "dgvDay";
            this.dgvDay.RowHeadersWidth = 51;
            this.dgvDay.RowTemplate.Height = 27;
            this.dgvDay.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvDay.Size = new System.Drawing.Size(1407, 63);
            this.dgvDay.TabIndex = 5;
            this.dgvDay.Tag = "D";
            this.dgvDay.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvDay.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // dgvDay_Item
            // 
            this.dgvDay_Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvDay_Item.DataPropertyName = "ItemName";
            this.dgvDay_Item.HeaderText = "保养项目";
            this.dgvDay_Item.MinimumWidth = 6;
            this.dgvDay_Item.Name = "dgvDay_Item";
            this.dgvDay_Item.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDay_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvDay_Item.Width = 300;
            // 
            // labDayTitile
            // 
            this.labDayTitile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labDayTitile.AutoSize = true;
            this.labDayTitile.Font = new System.Drawing.Font("宋体", 14F);
            this.labDayTitile.ForeColor = System.Drawing.Color.Blue;
            this.labDayTitile.Location = new System.Drawing.Point(6, 3);
            this.labDayTitile.Name = "labDayTitile";
            this.labDayTitile.Size = new System.Drawing.Size(24, 144);
            this.labDayTitile.TabIndex = 53;
            this.labDayTitile.Text = "日保养项目  ";
            this.labDayTitile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMessage
            // 
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labMessage.ForeColor = System.Drawing.Color.Green;
            this.labMessage.Location = new System.Drawing.Point(5, 127);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(1473, 28);
            this.labMessage.TabIndex = 10;
            this.labMessage.Text = "提示";
            // 
            // gbMaintenance
            // 
            this.gbMaintenance.AutoSize = true;
            this.gbMaintenance.BackColor = System.Drawing.Color.Transparent;
            this.gbMaintenance.Controls.Add(this.tableLayoutPanel1);
            this.gbMaintenance.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMaintenance.Location = new System.Drawing.Point(5, 35);
            this.gbMaintenance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbMaintenance.Name = "gbMaintenance";
            this.gbMaintenance.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbMaintenance.Size = new System.Drawing.Size(1473, 92);
            this.gbMaintenance.TabIndex = 1;
            this.gbMaintenance.TabStop = false;
            this.gbMaintenance.Text = "设备保养信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labAssetNo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbMonth, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbCostCenter, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMonth, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbYear, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbModel, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.labYear, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetName, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.labCostCenter, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.labAssetName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1467, 70);
            this.tableLayoutPanel1.TabIndex = 53;
            // 
            // cmbMonth
            // 
            this.cmbMonth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbMonth.Location = new System.Drawing.Point(283, 41);
            this.cmbMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(74, 23);
            this.cmbMonth.TabIndex = 45;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // txbCostCenter
            // 
            this.txbCostCenter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbCostCenter.Location = new System.Drawing.Point(923, 5);
            this.txbCostCenter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbCostCenter.Name = "txbCostCenter";
            this.txbCostCenter.ReadOnly = true;
            this.txbCostCenter.Size = new System.Drawing.Size(194, 25);
            this.txbCostCenter.TabIndex = 52;
            // 
            // labMonth
            // 
            this.labMonth.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMonth.AutoSize = true;
            this.labMonth.Location = new System.Drawing.Point(240, 45);
            this.labMonth.Name = "labMonth";
            this.labMonth.Size = new System.Drawing.Size(37, 15);
            this.labMonth.TabIndex = 46;
            this.labMonth.Text = "月份";
            // 
            // cmbYear
            // 
            this.cmbYear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(83, 41);
            this.cmbYear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(94, 23);
            this.cmbYear.TabIndex = 44;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // txbModel
            // 
            this.txbModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbModel.Location = new System.Drawing.Point(643, 5);
            this.txbModel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbModel.Name = "txbModel";
            this.txbModel.ReadOnly = true;
            this.txbModel.Size = new System.Drawing.Size(194, 25);
            this.txbModel.TabIndex = 48;
            // 
            // labYear
            // 
            this.labYear.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labYear.AutoSize = true;
            this.labYear.Location = new System.Drawing.Point(40, 45);
            this.labYear.Name = "labYear";
            this.labYear.Size = new System.Drawing.Size(37, 15);
            this.labYear.TabIndex = 43;
            this.labYear.Text = "年份";
            // 
            // txbAssetName
            // 
            this.txbAssetName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAssetName.Location = new System.Drawing.Point(363, 5);
            this.txbAssetName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbAssetName.Name = "txbAssetName";
            this.txbAssetName.ReadOnly = true;
            this.txbAssetName.Size = new System.Drawing.Size(194, 25);
            this.txbAssetName.TabIndex = 49;
            // 
            // labCostCenter
            // 
            this.labCostCenter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labCostCenter.AutoSize = true;
            this.labCostCenter.Location = new System.Drawing.Point(850, 10);
            this.labCostCenter.Name = "labCostCenter";
            this.labCostCenter.Size = new System.Drawing.Size(67, 15);
            this.labCostCenter.TabIndex = 51;
            this.labCostCenter.Text = "资产单位";
            // 
            // labAssetName
            // 
            this.labAssetName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAssetName.AutoSize = true;
            this.labAssetName.Location = new System.Drawing.Point(290, 10);
            this.labAssetName.Name = "labAssetName";
            this.labAssetName.Size = new System.Drawing.Size(67, 15);
            this.labAssetName.TabIndex = 50;
            this.labAssetName.Text = "资产名称";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(600, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "机型";
            // 
            // tabPanelMonth
            // 
            this.tabPanelMonth.AutoSize = true;
            this.tabPanelMonth.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tabPanelMonth.ColumnCount = 2;
            this.tabPanelMonth.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabPanelMonth.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabPanelMonth.Controls.Add(this.labDayTitile, 0, 0);
            this.tabPanelMonth.Controls.Add(this.dgvDay, 1, 0);
            this.tabPanelMonth.Controls.Add(this.labWeekTitle, 0, 1);
            this.tabPanelMonth.Controls.Add(this.dgvWeek, 1, 1);
            this.tabPanelMonth.Controls.Add(this.labMonthTitle, 0, 2);
            this.tabPanelMonth.Controls.Add(this.dgvMonth, 1, 2);
            this.tabPanelMonth.Controls.Add(this.rtxbMonthTip, 1, 3);
            this.tabPanelMonth.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabPanelMonth.Location = new System.Drawing.Point(0, 0);
            this.tabPanelMonth.Name = "tabPanelMonth";
            this.tabPanelMonth.RowCount = 4;
            this.tabPanelMonth.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelMonth.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelMonth.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelMonth.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelMonth.Size = new System.Drawing.Size(1452, 510);
            this.tabPanelMonth.TabIndex = 0;
            // 
            // tabPanelYear
            // 
            this.tabPanelYear.AutoSize = true;
            this.tabPanelYear.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tabPanelYear.ColumnCount = 2;
            this.tabPanelYear.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabPanelYear.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabPanelYear.Controls.Add(this.labYearMonthTitle, 0, 0);
            this.tabPanelYear.Controls.Add(this.dgvYearMonth, 1, 0);
            this.tabPanelYear.Controls.Add(this.labQuarterTitle, 0, 1);
            this.tabPanelYear.Controls.Add(this.dgvQuarter, 1, 1);
            this.tabPanelYear.Controls.Add(this.labYearTitle, 0, 2);
            this.tabPanelYear.Controls.Add(this.dgvYear, 1, 2);
            this.tabPanelYear.Controls.Add(this.rtxbYearTip, 1, 3);
            this.tabPanelYear.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabPanelYear.Location = new System.Drawing.Point(0, 510);
            this.tabPanelYear.Name = "tabPanelYear";
            this.tabPanelYear.RowCount = 4;
            this.tabPanelYear.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelYear.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelYear.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelYear.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelYear.Size = new System.Drawing.Size(1452, 503);
            this.tabPanelYear.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tabPanelYear);
            this.panel1.Controls.Add(this.tabPanelMonth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 155);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1473, 593);
            this.panel1.TabIndex = 58;
            // 
            // FrmMaintenanceReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1483, 753);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.gbMaintenance);
            this.Controls.Add(this.menuStripMachine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMaintenanceReport";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "资产保养报表";
            this.Load += new System.EventHandler(this.FrmBaseInfoMaintenance_Load);
            this.menuStripMachine.ResumeLayout(false);
            this.menuStripMachine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuarter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYearMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDay)).EndInit();
            this.gbMaintenance.ResumeLayout(false);
            this.gbMaintenance.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPanelMonth.ResumeLayout(false);
            this.tabPanelMonth.PerformLayout();
            this.tabPanelYear.ResumeLayout(false);
            this.tabPanelYear.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMachine;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiImport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.Label labAssetNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiMaintenanceItem;
        private System.Windows.Forms.TextBox txbAssetNo;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvWeek;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvMonth;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDay;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvYear;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvQuarter;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvYearMonth;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.GroupBox gbMaintenance;
        private System.Windows.Forms.Label labMonth;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label labYear;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn dgvMonth_Item;
        private System.Windows.Forms.TextBox txbModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labAssetName;
        private System.Windows.Forms.TextBox txbAssetName;
        private System.Windows.Forms.TextBox txbCostCenter;
        private System.Windows.Forms.Label labCostCenter;
        private System.Windows.Forms.Label labDayTitile;
        private System.Windows.Forms.Label labWeekTitle;
        private System.Windows.Forms.Label labYearMonthTitle;
        private System.Windows.Forms.Label labMonthTitle;
        private System.Windows.Forms.Label labYearTitle;
        private System.Windows.Forms.Label labQuarterTitle;
        private System.Windows.Forms.RichTextBox rtxbMonthTip;
        private System.Windows.Forms.RichTextBox rtxbYearTip;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn dgvYear_Item;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn dgvQuarter_Item;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn dgvYearMonth_Item;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn dgvWeek_Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDay_Item;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tabPanelYear;
        private System.Windows.Forms.TableLayoutPanel tabPanelMonth;
        private System.Windows.Forms.Panel panel1;
    }
}
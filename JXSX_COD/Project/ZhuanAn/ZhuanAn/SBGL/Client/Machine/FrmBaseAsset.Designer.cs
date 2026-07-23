namespace Machine
{
    partial class FrmBaseAsset
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseAsset));
            this.dgvAsset = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcFactoryCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetMainNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetSubNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcEntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCostCenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcDurableYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcDurableMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMadeFactory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcControlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpAssetInfo = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.nudDurableMonth = new System.Windows.Forms.NumericUpDown();
            this.txbControlNo = new System.Windows.Forms.TextBox();
            this.txbFactoryCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txbMadeFactory = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.nudDurableYear = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txbAssetMainNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txbCostCenter = new System.Windows.Forms.TextBox();
            this.txbAssetSubNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbAssetName = new System.Windows.Forms.TextBox();
            this.dtpEntryDate = new System.Windows.Forms.DateTimePicker();
            this.txbAssetClass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbModel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMaintenanceItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiQRCode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileBind = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsset)).BeginInit();
            this.gpAssetInfo.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDurableMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDurableYear)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAsset
            // 
            this.dgvAsset.AllowUserToAddRows = false;
            this.dgvAsset.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.GhostWhite;
            this.dgvAsset.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAsset.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAsset.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAsset.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAsset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcFactoryCode,
            this.dgcAssetMainNo,
            this.dgcAssetSubNo,
            this.dgcAssetName,
            this.dgcAssetClass,
            this.dgcModel,
            this.dgcEntryDate,
            this.dgcCostCenter,
            this.dgcDurableYear,
            this.dgcDurableMonth,
            this.dgcMadeFactory,
            this.dgcControlNo});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAsset.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAsset.EnableHeadersVisualStyles = false;
            this.dgvAsset.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvAsset.Location = new System.Drawing.Point(15, 199);
            this.dgvAsset.Name = "dgvAsset";
            this.dgvAsset.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAsset.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvAsset.RowHeadersWidth = 51;
            this.dgvAsset.RowTemplate.Height = 27;
            this.dgvAsset.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAsset.Size = new System.Drawing.Size(1552, 539);
            this.dgvAsset.TabIndex = 0;
            this.dgvAsset.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAsset_CellClick);
            // 
            // dgcFactoryCode
            // 
            this.dgcFactoryCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFactoryCode.DataPropertyName = "FactoryCode";
            this.dgcFactoryCode.HeaderText = "公司代码";
            this.dgcFactoryCode.MinimumWidth = 6;
            this.dgcFactoryCode.Name = "dgcFactoryCode";
            this.dgcFactoryCode.ReadOnly = true;
            this.dgcFactoryCode.Width = 80;
            // 
            // dgcAssetMainNo
            // 
            this.dgcAssetMainNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetMainNo.DataPropertyName = "AssetMainNo";
            this.dgcAssetMainNo.HeaderText = "资产主编号";
            this.dgcAssetMainNo.MinimumWidth = 6;
            this.dgcAssetMainNo.Name = "dgcAssetMainNo";
            this.dgcAssetMainNo.ReadOnly = true;
            this.dgcAssetMainNo.Width = 150;
            // 
            // dgcAssetSubNo
            // 
            this.dgcAssetSubNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetSubNo.DataPropertyName = "AssetSubNo";
            this.dgcAssetSubNo.HeaderText = "资产子编号";
            this.dgcAssetSubNo.MinimumWidth = 6;
            this.dgcAssetSubNo.Name = "dgcAssetSubNo";
            this.dgcAssetSubNo.ReadOnly = true;
            this.dgcAssetSubNo.Width = 80;
            // 
            // dgcAssetName
            // 
            this.dgcAssetName.DataPropertyName = "AssetName";
            this.dgcAssetName.HeaderText = "资产名称";
            this.dgcAssetName.MinimumWidth = 6;
            this.dgcAssetName.Name = "dgcAssetName";
            this.dgcAssetName.ReadOnly = true;
            // 
            // dgcAssetClass
            // 
            this.dgcAssetClass.DataPropertyName = "AssetClass";
            this.dgcAssetClass.HeaderText = "资产分类";
            this.dgcAssetClass.MinimumWidth = 6;
            this.dgcAssetClass.Name = "dgcAssetClass";
            this.dgcAssetClass.ReadOnly = true;
            // 
            // dgcModel
            // 
            this.dgcModel.DataPropertyName = "Model";
            this.dgcModel.HeaderText = "型号规格";
            this.dgcModel.MinimumWidth = 6;
            this.dgcModel.Name = "dgcModel";
            this.dgcModel.ReadOnly = true;
            // 
            // dgcEntryDate
            // 
            this.dgcEntryDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcEntryDate.DataPropertyName = "EntryDate";
            this.dgcEntryDate.HeaderText = "购置日期";
            this.dgcEntryDate.MinimumWidth = 6;
            this.dgcEntryDate.Name = "dgcEntryDate";
            this.dgcEntryDate.ReadOnly = true;
            this.dgcEntryDate.Width = 125;
            // 
            // dgcCostCenter
            // 
            this.dgcCostCenter.DataPropertyName = "CostCenter";
            this.dgcCostCenter.HeaderText = "成本中心";
            this.dgcCostCenter.MinimumWidth = 6;
            this.dgcCostCenter.Name = "dgcCostCenter";
            this.dgcCostCenter.ReadOnly = true;
            // 
            // dgcDurableYear
            // 
            this.dgcDurableYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcDurableYear.DataPropertyName = "DurableYear";
            this.dgcDurableYear.HeaderText = "耐用年限";
            this.dgcDurableYear.MinimumWidth = 6;
            this.dgcDurableYear.Name = "dgcDurableYear";
            this.dgcDurableYear.ReadOnly = true;
            this.dgcDurableYear.Width = 80;
            // 
            // dgcDurableMonth
            // 
            this.dgcDurableMonth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcDurableMonth.DataPropertyName = "DurableMonth";
            this.dgcDurableMonth.HeaderText = "耐用月数";
            this.dgcDurableMonth.MinimumWidth = 6;
            this.dgcDurableMonth.Name = "dgcDurableMonth";
            this.dgcDurableMonth.ReadOnly = true;
            this.dgcDurableMonth.Width = 80;
            // 
            // dgcMadeFactory
            // 
            this.dgcMadeFactory.DataPropertyName = "MadeFactory";
            this.dgcMadeFactory.HeaderText = "供应商名称";
            this.dgcMadeFactory.MinimumWidth = 6;
            this.dgcMadeFactory.Name = "dgcMadeFactory";
            this.dgcMadeFactory.ReadOnly = true;
            // 
            // dgcControlNo
            // 
            this.dgcControlNo.DataPropertyName = "ControlNo";
            this.dgcControlNo.HeaderText = "管制编号";
            this.dgcControlNo.MinimumWidth = 6;
            this.dgcControlNo.Name = "dgcControlNo";
            this.dgcControlNo.ReadOnly = true;
            // 
            // gpAssetInfo
            // 
            this.gpAssetInfo.AutoSize = true;
            this.gpAssetInfo.BackColor = System.Drawing.Color.Transparent;
            this.gpAssetInfo.Controls.Add(this.tableLayoutPanel1);
            this.gpAssetInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpAssetInfo.Location = new System.Drawing.Point(15, 35);
            this.gpAssetInfo.Name = "gpAssetInfo";
            this.gpAssetInfo.Size = new System.Drawing.Size(1552, 129);
            this.gpAssetInfo.TabIndex = 1;
            this.gpAssetInfo.TabStop = false;
            this.gpAssetInfo.Text = "资产基本信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudDurableMonth, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbControlNo, 7, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbFactoryCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbMadeFactory, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.nudDurableYear, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetMainNo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbCostCenter, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetSubNo, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetName, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpEntryDate, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetClass, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbModel, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1546, 105);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "公司代码";
            // 
            // nudDurableMonth
            // 
            this.nudDurableMonth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudDurableMonth.Location = new System.Drawing.Point(383, 75);
            this.nudDurableMonth.Name = "nudDurableMonth";
            this.nudDurableMonth.Size = new System.Drawing.Size(174, 25);
            this.nudDurableMonth.TabIndex = 27;
            // 
            // txbControlNo
            // 
            this.txbControlNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbControlNo.Location = new System.Drawing.Point(943, 75);
            this.txbControlNo.Name = "txbControlNo";
            this.txbControlNo.Size = new System.Drawing.Size(174, 25);
            this.txbControlNo.TabIndex = 12;
            // 
            // txbFactoryCode
            // 
            this.txbFactoryCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbFactoryCode.Location = new System.Drawing.Point(103, 5);
            this.txbFactoryCode.Name = "txbFactoryCode";
            this.txbFactoryCode.Size = new System.Drawing.Size(174, 25);
            this.txbFactoryCode.TabIndex = 19;
            this.txbFactoryCode.Text = "0210";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(870, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "管制编号";
            // 
            // txbMadeFactory
            // 
            this.txbMadeFactory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbMadeFactory.Location = new System.Drawing.Point(663, 75);
            this.txbMadeFactory.Name = "txbMadeFactory";
            this.txbMadeFactory.Size = new System.Drawing.Size(174, 25);
            this.txbMadeFactory.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(310, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 15);
            this.label11.TabIndex = 24;
            this.label11.Text = "耐用月数";
            // 
            // nudDurableYear
            // 
            this.nudDurableYear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudDurableYear.Location = new System.Drawing.Point(103, 75);
            this.nudDurableYear.Name = "nudDurableYear";
            this.nudDurableYear.Size = new System.Drawing.Size(174, 25);
            this.nudDurableYear.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(590, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "制造厂商";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "资产主编号";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(30, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 15);
            this.label12.TabIndex = 25;
            this.label12.Text = "耐用年限";
            // 
            // txbAssetMainNo
            // 
            this.txbAssetMainNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAssetMainNo.Location = new System.Drawing.Point(383, 5);
            this.txbAssetMainNo.Name = "txbAssetMainNo";
            this.txbAssetMainNo.Size = new System.Drawing.Size(174, 25);
            this.txbAssetMainNo.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(575, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 20;
            this.label9.Text = "资产子编号";
            // 
            // txbCostCenter
            // 
            this.txbCostCenter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbCostCenter.Location = new System.Drawing.Point(943, 40);
            this.txbCostCenter.Name = "txbCostCenter";
            this.txbCostCenter.Size = new System.Drawing.Size(174, 25);
            this.txbCostCenter.TabIndex = 23;
            // 
            // txbAssetSubNo
            // 
            this.txbAssetSubNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAssetSubNo.Location = new System.Drawing.Point(663, 5);
            this.txbAssetSubNo.Name = "txbAssetSubNo";
            this.txbAssetSubNo.Size = new System.Drawing.Size(174, 25);
            this.txbAssetSubNo.TabIndex = 21;
            this.txbAssetSubNo.Text = "0000";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(870, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 22;
            this.label10.Text = "成本中心";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(870, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "资产名称";
            // 
            // txbAssetName
            // 
            this.txbAssetName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAssetName.Location = new System.Drawing.Point(943, 5);
            this.txbAssetName.Name = "txbAssetName";
            this.txbAssetName.Size = new System.Drawing.Size(174, 25);
            this.txbAssetName.TabIndex = 9;
            // 
            // dtpEntryDate
            // 
            this.dtpEntryDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpEntryDate.Location = new System.Drawing.Point(663, 40);
            this.dtpEntryDate.Name = "dtpEntryDate";
            this.dtpEntryDate.Size = new System.Drawing.Size(174, 25);
            this.dtpEntryDate.TabIndex = 13;
            // 
            // txbAssetClass
            // 
            this.txbAssetClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAssetClass.Location = new System.Drawing.Point(103, 40);
            this.txbAssetClass.Name = "txbAssetClass";
            this.txbAssetClass.Size = new System.Drawing.Size(174, 25);
            this.txbAssetClass.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "资产种类";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "型号";
            // 
            // txbModel
            // 
            this.txbModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbModel.Location = new System.Drawing.Point(383, 40);
            this.txbModel.Name = "txbModel";
            this.txbModel.Size = new System.Drawing.Size(174, 25);
            this.txbModel.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(590, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "购置日期";
            // 
            // labMessage
            // 
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(15, 164);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(1552, 35);
            this.labMessage.TabIndex = 17;
            this.labMessage.Text = "提示:";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh,
            this.tsmiAdd,
            this.tsmiUpdate,
            this.tsmiDelete,
            this.tsmiSave,
            this.tsmiBack,
            this.tsmiImport,
            this.tsmiExport,
            this.tsmiMaintenanceItem,
            this.tsmiQRCode,
            this.tsmiFileBind});
            this.menuStrip.Location = new System.Drawing.Point(15, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1552, 35);
            this.menuStrip.TabIndex = 18;
            this.menuStrip.Text = "menuStrip1";
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
            // tsmiMaintenanceItem
            // 
            this.tsmiMaintenanceItem.Name = "tsmiMaintenanceItem";
            this.tsmiMaintenanceItem.Size = new System.Drawing.Size(14, 31);
            // 
            // tsmiQRCode
            // 
            this.tsmiQRCode.Image = ((System.Drawing.Image)(resources.GetObject("tsmiQRCode.Image")));
            this.tsmiQRCode.Name = "tsmiQRCode";
            this.tsmiQRCode.Size = new System.Drawing.Size(106, 31);
            this.tsmiQRCode.Text = "二维码";
            this.tsmiQRCode.Click += new System.EventHandler(this.tsmiQRCode_Click);
            // 
            // tsmiFileBind
            // 
            this.tsmiFileBind.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileBind.Image")));
            this.tsmiFileBind.Name = "tsmiFileBind";
            this.tsmiFileBind.Size = new System.Drawing.Size(126, 31);
            this.tsmiFileBind.Text = "文件绑定";
            this.tsmiFileBind.Click += new System.EventHandler(this.tsmiFileBind_Click);
            // 
            // FrmBaseAsset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1582, 753);
            this.Controls.Add(this.dgvAsset);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.gpAssetInfo);
            this.Controls.Add(this.menuStrip);
            this.Name = "FrmBaseAsset";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "资产清册";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseAsset_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsset)).EndInit();
            this.gpAssetInfo.ResumeLayout(false);
            this.gpAssetInfo.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDurableMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDurableYear)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvAsset;
        private System.Windows.Forms.GroupBox gpAssetInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEntryDate;
        private System.Windows.Forms.TextBox txbControlNo;
        private System.Windows.Forms.TextBox txbMadeFactory;
        private System.Windows.Forms.TextBox txbModel;
        private System.Windows.Forms.TextBox txbAssetName;
        private System.Windows.Forms.TextBox txbAssetMainNo;
        private System.Windows.Forms.TextBox txbAssetClass;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.ToolStripMenuItem tsmiImport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiMaintenanceItem;
        private System.Windows.Forms.TextBox txbAssetSubNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txbFactoryCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudDurableMonth;
        private System.Windows.Forms.NumericUpDown nudDurableYear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txbCostCenter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFactoryCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetMainNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetSubNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcEntryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCostCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcDurableYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcDurableMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMadeFactory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcControlNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiQRCode;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileBind;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
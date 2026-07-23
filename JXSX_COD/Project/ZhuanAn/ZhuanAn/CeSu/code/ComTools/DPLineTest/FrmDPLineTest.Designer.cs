namespace ComTools.DPLineTest
{
    partial class FrmDPLineTest
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDPLineTest));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.chkSerailListenFlag = new System.Windows.Forms.CheckBox();
            this.labSerailListenFlag = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbHexSuffix = new System.Windows.Forms.TextBox();
            this.btnFlag = new System.Windows.Forms.Button();
            this.tlPanelRoot = new System.Windows.Forms.TableLayoutPanel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tlPanelResult = new System.Windows.Forms.TableLayoutPanel();
            this.panelResult = new System.Windows.Forms.Panel();
            this.labResult = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            this.labResultItemName = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTestItem = new System.Windows.Forms.DataGridView();
            this.dgcTestItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWindowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcControlType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAutomationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWhereValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFilterValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRequestCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcResponsePassCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcResponseFailCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTestConfig = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tlPanelSerialPort = new System.Windows.Forms.TableLayoutPanel();
            this.labStopBits = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbStartTestHexCode = new System.Windows.Forms.TextBox();
            this.labPortName = new System.Windows.Forms.Label();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.labParity = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.labBaudRate = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.labDataBits = new System.Windows.Forms.Label();
            this.tbDataBits = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txbEndTestHexCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nudStartTestOvertime = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReset = new System.Windows.Forms.Button();
            this.bgWorkTest = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlPanelRoot.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tlPanelResult.SuspendLayout();
            this.panelResult.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestItem)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tlPanelSerialPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartTestOvertime)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Pink;
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbModel, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(964, 40);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "启动模式";
            // 
            // cmbModel
            // 
            this.cmbModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Location = new System.Drawing.Point(103, 8);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(114, 23);
            this.cmbModel.TabIndex = 19;
            this.cmbModel.SelectedIndexChanged += new System.EventHandler(this.cmbModel_SelectedIndexChanged);
            // 
            // chkSerailListenFlag
            // 
            this.chkSerailListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSerailListenFlag.AutoSize = true;
            this.chkSerailListenFlag.Location = new System.Drawing.Point(103, 6);
            this.chkSerailListenFlag.Name = "chkSerailListenFlag";
            this.chkSerailListenFlag.Size = new System.Drawing.Size(18, 17);
            this.chkSerailListenFlag.TabIndex = 15;
            this.chkSerailListenFlag.CheckedChanged += new System.EventHandler(this.chkSerailListenFlag_CheckedChanged);
            // 
            // labSerailListenFlag
            // 
            this.labSerailListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labSerailListenFlag.AutoSize = true;
            this.labSerailListenFlag.Location = new System.Drawing.Point(30, 7);
            this.labSerailListenFlag.Name = "labSerailListenFlag";
            this.labSerailListenFlag.Size = new System.Drawing.Size(67, 15);
            this.labSerailListenFlag.TabIndex = 11;
            this.labSerailListenFlag.Text = "串口监听";
            this.labSerailListenFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(460, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "后缀(Hex)";
            // 
            // txbHexSuffix
            // 
            this.txbHexSuffix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbHexSuffix.Location = new System.Drawing.Point(540, 62);
            this.txbHexSuffix.Margin = new System.Windows.Forms.Padding(0);
            this.txbHexSuffix.Name = "txbHexSuffix";
            this.txbHexSuffix.Size = new System.Drawing.Size(120, 25);
            this.txbHexSuffix.TabIndex = 20;
            // 
            // btnFlag
            // 
            this.btnFlag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFlag.BackColor = System.Drawing.Color.Green;
            this.btnFlag.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFlag.ForeColor = System.Drawing.Color.White;
            this.btnFlag.Location = new System.Drawing.Point(3, 3);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Size = new System.Drawing.Size(95, 37);
            this.btnFlag.TabIndex = 17;
            this.btnFlag.Text = "Start";
            this.btnFlag.UseVisualStyleBackColor = false;
            this.btnFlag.Click += new System.EventHandler(this.btnFlag_Click);
            // 
            // tlPanelRoot
            // 
            this.tlPanelRoot.ColumnCount = 2;
            this.tlPanelRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tlPanelRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlPanelRoot.Controls.Add(this.rtbLog, 1, 0);
            this.tlPanelRoot.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tlPanelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelRoot.Location = new System.Drawing.Point(20, 20);
            this.tlPanelRoot.Name = "tlPanelRoot";
            this.tlPanelRoot.RowCount = 1;
            this.tlPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelRoot.Size = new System.Drawing.Size(1442, 707);
            this.tlPanelRoot.TabIndex = 1;
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(1195, 3);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(244, 701);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1186, 701);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tlPanelResult, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(4, 398);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1178, 299);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkOrange;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 299);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目结果";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlPanelResult
            // 
            this.tlPanelResult.ColumnCount = 3;
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlPanelResult.Controls.Add(this.panelResult, 0, 0);
            this.tlPanelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelResult.Location = new System.Drawing.Point(43, 3);
            this.tlPanelResult.Name = "tlPanelResult";
            this.tlPanelResult.RowCount = 3;
            this.tlPanelResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.Size = new System.Drawing.Size(1132, 293);
            this.tlPanelResult.TabIndex = 1;
            // 
            // panelResult
            // 
            this.panelResult.Controls.Add(this.labResult);
            this.panelResult.Controls.Add(this.labMessage);
            this.panelResult.Controls.Add(this.labResultItemName);
            this.panelResult.Controls.Add(this.progressBar1);
            this.panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResult.Location = new System.Drawing.Point(3, 3);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(371, 91);
            this.panelResult.TabIndex = 0;
            // 
            // labResult
            // 
            this.labResult.BackColor = System.Drawing.Color.Red;
            this.labResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labResult.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labResult.ForeColor = System.Drawing.Color.White;
            this.labResult.Location = new System.Drawing.Point(0, 25);
            this.labResult.Name = "labResult";
            this.labResult.Size = new System.Drawing.Size(371, 31);
            this.labResult.TabIndex = 1;
            this.labResult.Text = "测试结果";
            this.labResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMessage
            // 
            this.labMessage.BackColor = System.Drawing.Color.MediumBlue;
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labMessage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMessage.ForeColor = System.Drawing.Color.White;
            this.labMessage.Location = new System.Drawing.Point(0, 56);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(371, 25);
            this.labMessage.TabIndex = 2;
            this.labMessage.Text = "消息";
            this.labMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labResultItemName
            // 
            this.labResultItemName.BackColor = System.Drawing.Color.MediumBlue;
            this.labResultItemName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labResultItemName.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labResultItemName.ForeColor = System.Drawing.Color.White;
            this.labResultItemName.Location = new System.Drawing.Point(0, 0);
            this.labResultItemName.Name = "labResultItemName";
            this.labResultItemName.Size = new System.Drawing.Size(371, 25);
            this.labResultItemName.TabIndex = 0;
            this.labResultItemName.Text = "测试项目";
            this.labResultItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 81);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(371, 10);
            this.progressBar1.TabIndex = 3;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.dgvTestItem, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(4, 153);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1178, 238);
            this.tableLayoutPanel8.TabIndex = 18;
            // 
            // dgvTestItem
            // 
            this.dgvTestItem.AllowUserToAddRows = false;
            this.dgvTestItem.AllowUserToDeleteRows = false;
            this.dgvTestItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTestItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcTestItem,
            this.dgcWindowName,
            this.dgcControlType,
            this.dgcAutomationID,
            this.dgcOperator,
            this.dgcWhereValue,
            this.dgcFilterValue,
            this.dgcRequestCode,
            this.dgcResponsePassCode,
            this.dgcResponseFailCode,
            this.dgcTestConfig});
            this.dgvTestItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvTestItem.Location = new System.Drawing.Point(43, 43);
            this.dgvTestItem.Name = "dgvTestItem";
            this.dgvTestItem.ReadOnly = true;
            this.dgvTestItem.RowHeadersVisible = false;
            this.dgvTestItem.RowHeadersWidth = 51;
            this.dgvTestItem.RowTemplate.Height = 27;
            this.dgvTestItem.Size = new System.Drawing.Size(1132, 192);
            this.dgvTestItem.TabIndex = 1;
            this.dgvTestItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTestItem_CellClick);
            this.dgvTestItem.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvTestItem_RowsAdded);
            this.dgvTestItem.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvTestItem_RowsRemoved);
            // 
            // dgcTestItem
            // 
            this.dgcTestItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcTestItem.DataPropertyName = "TestItem";
            this.dgcTestItem.HeaderText = "测试项目";
            this.dgcTestItem.MinimumWidth = 6;
            this.dgcTestItem.Name = "dgcTestItem";
            this.dgcTestItem.ReadOnly = true;
            this.dgcTestItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcTestItem.Width = 96;
            // 
            // dgcWindowName
            // 
            this.dgcWindowName.DataPropertyName = "WindowName";
            this.dgcWindowName.HeaderText = "目标窗口";
            this.dgcWindowName.MinimumWidth = 6;
            this.dgcWindowName.Name = "dgcWindowName";
            this.dgcWindowName.ReadOnly = true;
            this.dgcWindowName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgcControlType
            // 
            this.dgcControlType.DataPropertyName = "ControlType";
            this.dgcControlType.HeaderText = "控件类型";
            this.dgcControlType.MinimumWidth = 6;
            this.dgcControlType.Name = "dgcControlType";
            this.dgcControlType.ReadOnly = true;
            // 
            // dgcAutomationID
            // 
            this.dgcAutomationID.DataPropertyName = "AutomationID";
            this.dgcAutomationID.HeaderText = "自动化ID";
            this.dgcAutomationID.MinimumWidth = 6;
            this.dgcAutomationID.Name = "dgcAutomationID";
            this.dgcAutomationID.ReadOnly = true;
            // 
            // dgcOperator
            // 
            this.dgcOperator.DataPropertyName = "Operator";
            this.dgcOperator.HeaderText = "运算符";
            this.dgcOperator.MinimumWidth = 6;
            this.dgcOperator.Name = "dgcOperator";
            this.dgcOperator.ReadOnly = true;
            this.dgcOperator.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgcWhereValue
            // 
            this.dgcWhereValue.DataPropertyName = "WhereValue";
            this.dgcWhereValue.HeaderText = "条件值";
            this.dgcWhereValue.MinimumWidth = 6;
            this.dgcWhereValue.Name = "dgcWhereValue";
            this.dgcWhereValue.ReadOnly = true;
            // 
            // dgcFilterValue
            // 
            this.dgcFilterValue.DataPropertyName = "FilterValue";
            this.dgcFilterValue.HeaderText = "过滤取值";
            this.dgcFilterValue.MinimumWidth = 6;
            this.dgcFilterValue.Name = "dgcFilterValue";
            this.dgcFilterValue.ReadOnly = true;
            // 
            // dgcRequestCode
            // 
            this.dgcRequestCode.DataPropertyName = "RequestHexCode";
            this.dgcRequestCode.HeaderText = "请求指令";
            this.dgcRequestCode.MinimumWidth = 6;
            this.dgcRequestCode.Name = "dgcRequestCode";
            this.dgcRequestCode.ReadOnly = true;
            // 
            // dgcResponsePassCode
            // 
            this.dgcResponsePassCode.DataPropertyName = "ResponsePassHexCode";
            this.dgcResponsePassCode.HeaderText = "PASS指令";
            this.dgcResponsePassCode.MinimumWidth = 6;
            this.dgcResponsePassCode.Name = "dgcResponsePassCode";
            this.dgcResponsePassCode.ReadOnly = true;
            // 
            // dgcResponseFailCode
            // 
            this.dgcResponseFailCode.DataPropertyName = "ResponseFailHexCode";
            this.dgcResponseFailCode.HeaderText = "FAIL指令";
            this.dgcResponseFailCode.MinimumWidth = 6;
            this.dgcResponseFailCode.Name = "dgcResponseFailCode";
            this.dgcResponseFailCode.ReadOnly = true;
            // 
            // dgcTestConfig
            // 
            this.dgcTestConfig.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcTestConfig.HeaderText = "测试配置";
            this.dgcTestConfig.MinimumWidth = 6;
            this.dgcTestConfig.Name = "dgcTestConfig";
            this.dgcTestConfig.ReadOnly = true;
            this.dgcTestConfig.Text = "配置";
            this.dgcTestConfig.UseColumnTextForButtonValue = true;
            this.dgcTestConfig.Width = 80;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.DarkOrange;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.tableLayoutPanel8.SetRowSpan(this.label5, 2);
            this.label5.Size = new System.Drawing.Size(34, 238);
            this.label5.TabIndex = 0;
            this.label5.Text = "测试项目配置";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddItem, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDeleteItem, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(43, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1132, 34);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.Turquoise;
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Location = new System.Drawing.Point(3, 3);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(34, 28);
            this.btnAddItem.TabIndex = 11;
            this.btnAddItem.Text = "+";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.BackColor = System.Drawing.Color.Tomato;
            this.btnDeleteItem.ForeColor = System.Drawing.Color.White;
            this.btnDeleteItem.Location = new System.Drawing.Point(43, 3);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(34, 28);
            this.btnDeleteItem.TabIndex = 11;
            this.btnDeleteItem.Text = "-";
            this.btnDeleteItem.UseVisualStyleBackColor = false;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.tlPanelSerialPort, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1178, 142);
            this.tableLayoutPanel5.TabIndex = 19;
            // 
            // tlPanelSerialPort
            // 
            this.tlPanelSerialPort.AutoSize = true;
            this.tlPanelSerialPort.BackColor = System.Drawing.Color.Pink;
            this.tlPanelSerialPort.ColumnCount = 9;
            this.tlPanelSerialPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlPanelSerialPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlPanelSerialPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlPanelSerialPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlPanelSerialPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlPanelSerialPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlPanelSerialPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlPanelSerialPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlPanelSerialPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelSerialPort.Controls.Add(this.labSerailListenFlag, 0, 0);
            this.tlPanelSerialPort.Controls.Add(this.chkSerailListenFlag, 1, 0);
            this.tlPanelSerialPort.Controls.Add(this.labStopBits, 0, 1);
            this.tlPanelSerialPort.Controls.Add(this.cmbStopBits, 1, 1);
            this.tlPanelSerialPort.Controls.Add(this.label4, 0, 2);
            this.tlPanelSerialPort.Controls.Add(this.txbStartTestHexCode, 1, 2);
            this.tlPanelSerialPort.Controls.Add(this.labPortName, 2, 0);
            this.tlPanelSerialPort.Controls.Add(this.cmbPortName, 3, 0);
            this.tlPanelSerialPort.Controls.Add(this.labParity, 2, 1);
            this.tlPanelSerialPort.Controls.Add(this.cmbParity, 3, 1);
            this.tlPanelSerialPort.Controls.Add(this.labBaudRate, 4, 0);
            this.tlPanelSerialPort.Controls.Add(this.cmbBaudRate, 5, 0);
            this.tlPanelSerialPort.Controls.Add(this.labDataBits, 4, 1);
            this.tlPanelSerialPort.Controls.Add(this.tbDataBits, 5, 1);
            this.tlPanelSerialPort.Controls.Add(this.label3, 4, 2);
            this.tlPanelSerialPort.Controls.Add(this.txbHexSuffix, 5, 2);
            this.tlPanelSerialPort.Controls.Add(this.label7, 2, 2);
            this.tlPanelSerialPort.Controls.Add(this.txbEndTestHexCode, 3, 2);
            this.tlPanelSerialPort.Controls.Add(this.label6, 6, 2);
            this.tlPanelSerialPort.Controls.Add(this.nudStartTestOvertime, 7, 2);
            this.tlPanelSerialPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlPanelSerialPort.Location = new System.Drawing.Point(3, 49);
            this.tlPanelSerialPort.Name = "tlPanelSerialPort";
            this.tlPanelSerialPort.RowCount = 3;
            this.tlPanelSerialPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelSerialPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelSerialPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelSerialPort.Size = new System.Drawing.Size(964, 90);
            this.tlPanelSerialPort.TabIndex = 24;
            // 
            // labStopBits
            // 
            this.labStopBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labStopBits.AutoSize = true;
            this.labStopBits.Location = new System.Drawing.Point(45, 37);
            this.labStopBits.Name = "labStopBits";
            this.labStopBits.Size = new System.Drawing.Size(52, 15);
            this.labStopBits.TabIndex = 3;
            this.labStopBits.Text = "停止位";
            this.labStopBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Location = new System.Drawing.Point(100, 33);
            this.cmbStopBits.Margin = new System.Windows.Forms.Padding(0);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(120, 23);
            this.cmbStopBits.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "启动(Hex)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbStartTestHexCode
            // 
            this.txbStartTestHexCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbStartTestHexCode.Location = new System.Drawing.Point(100, 62);
            this.txbStartTestHexCode.Margin = new System.Windows.Forms.Padding(0);
            this.txbStartTestHexCode.Name = "txbStartTestHexCode";
            this.txbStartTestHexCode.Size = new System.Drawing.Size(120, 25);
            this.txbStartTestHexCode.TabIndex = 20;
            // 
            // labPortName
            // 
            this.labPortName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPortName.AutoSize = true;
            this.labPortName.Location = new System.Drawing.Point(250, 7);
            this.labPortName.Name = "labPortName";
            this.labPortName.Size = new System.Drawing.Size(67, 15);
            this.labPortName.TabIndex = 0;
            this.labPortName.Text = "串口名称";
            this.labPortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPortName
            // 
            this.cmbPortName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.cmbPortName.Location = new System.Drawing.Point(320, 3);
            this.cmbPortName.Margin = new System.Windows.Forms.Padding(0);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(120, 23);
            this.cmbPortName.TabIndex = 6;
            // 
            // labParity
            // 
            this.labParity.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labParity.AutoSize = true;
            this.labParity.Location = new System.Drawing.Point(250, 37);
            this.labParity.Name = "labParity";
            this.labParity.Size = new System.Drawing.Size(67, 15);
            this.labParity.TabIndex = 4;
            this.labParity.Text = "奇偶校验";
            this.labParity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbParity
            // 
            this.cmbParity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Location = new System.Drawing.Point(320, 33);
            this.cmbParity.Margin = new System.Windows.Forms.Padding(0);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(120, 23);
            this.cmbParity.TabIndex = 10;
            // 
            // labBaudRate
            // 
            this.labBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labBaudRate.AutoSize = true;
            this.labBaudRate.Location = new System.Drawing.Point(485, 7);
            this.labBaudRate.Name = "labBaudRate";
            this.labBaudRate.Size = new System.Drawing.Size(52, 15);
            this.labBaudRate.TabIndex = 1;
            this.labBaudRate.Text = "波特率";
            this.labBaudRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(540, 3);
            this.cmbBaudRate.Margin = new System.Windows.Forms.Padding(0);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(120, 23);
            this.cmbBaudRate.TabIndex = 8;
            // 
            // labDataBits
            // 
            this.labDataBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labDataBits.AutoSize = true;
            this.labDataBits.Location = new System.Drawing.Point(485, 37);
            this.labDataBits.Name = "labDataBits";
            this.labDataBits.Size = new System.Drawing.Size(52, 15);
            this.labDataBits.TabIndex = 2;
            this.labDataBits.Text = "数据位";
            this.labDataBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDataBits
            // 
            this.tbDataBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbDataBits.Location = new System.Drawing.Point(540, 32);
            this.tbDataBits.Margin = new System.Windows.Forms.Padding(0);
            this.tbDataBits.Name = "tbDataBits";
            this.tbDataBits.Size = new System.Drawing.Size(120, 25);
            this.tbDataBits.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "结束(Hex)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbEndTestHexCode
            // 
            this.txbEndTestHexCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbEndTestHexCode.Location = new System.Drawing.Point(320, 62);
            this.txbEndTestHexCode.Margin = new System.Windows.Forms.Padding(0);
            this.txbEndTestHexCode.Name = "txbEndTestHexCode";
            this.txbEndTestHexCode.Size = new System.Drawing.Size(120, 25);
            this.txbEndTestHexCode.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(690, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "启动超时";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudStartTestOvertime
            // 
            this.nudStartTestOvertime.Location = new System.Drawing.Point(763, 63);
            this.nudStartTestOvertime.Name = "nudStartTestOvertime";
            this.nudStartTestOvertime.Size = new System.Drawing.Size(114, 25);
            this.nudStartTestOvertime.TabIndex = 21;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnFlag, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnReset, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(973, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel5.SetRowSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(202, 43);
            this.tableLayoutPanel3.TabIndex = 22;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReset.BackColor = System.Drawing.Color.Green;
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(105, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(93, 37);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // bgWorkTest
            // 
            this.bgWorkTest.WorkerReportsProgress = true;
            this.bgWorkTest.WorkerSupportsCancellation = true;
            this.bgWorkTest.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkTest_DoWork);
            this.bgWorkTest.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkTest_ProgressChanged);
            // 
            // FrmDPLineTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1482, 747);
            this.Controls.Add(this.tlPanelRoot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDPLineTest";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DP线测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDPLineTest_FormClosing);
            this.Load += new System.EventHandler(this.FrmDPLineTest_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlPanelRoot.ResumeLayout(false);
            this.tlPanelRoot.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tlPanelResult.ResumeLayout(false);
            this.panelResult.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestItem)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tlPanelSerialPort.ResumeLayout(false);
            this.tlPanelSerialPort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartTestOvertime)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tlPanelRoot;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button btnFlag;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvTestItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.TableLayoutPanel tlPanelResult;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.Label labResult;
        private System.Windows.Forms.Label labResultItemName;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.TextBox txbHexSuffix;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tlPanelSerialPort;
        private System.Windows.Forms.Label labSerailListenFlag;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.CheckBox chkSerailListenFlag;
        private System.Windows.Forms.Label labParity;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.Label labPortName;
        private System.Windows.Forms.TextBox tbDataBits;
        private System.Windows.Forms.Label labStopBits;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.Label labBaudRate;
        private System.Windows.Forms.Label labDataBits;
        private System.ComponentModel.BackgroundWorker bgWorkTest;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbStartTestHexCode;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTestItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWindowName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcControlType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAutomationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWhereValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFilterValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRequestCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcResponsePassCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcResponseFailCode;
        private System.Windows.Forms.DataGridViewButtonColumn dgcTestConfig;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudStartTestOvertime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txbEndTestHexCode;
    }
}


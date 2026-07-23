namespace ComTools.AgingTest
{
    partial class FrmAgingTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgingTest));
            this.tlPanelConfig = new System.Windows.Forms.TableLayoutPanel();
            this.labBaudRate = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.labStopBits = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.labParity = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.labDataBits = new System.Windows.Forms.Label();
            this.tbDataBits = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudDeviceCount = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudLayoutColCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudTestCount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudPassCount = new System.Windows.Forms.NumericUpDown();
            this.btnFlag = new System.Windows.Forms.Button();
            this.tlPanelRoot = new System.Windows.Forms.TableLayoutPanel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tlPanelResult = new System.Windows.Forms.TableLayoutPanel();
            this.panelResult = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labResult = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tlPanelItemConfig = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTestItem = new System.Windows.Forms.DataGridView();
            this.dgcTestItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRequestByteSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRequestHexCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcResponseByteSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcResponseHexCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcPassHexCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFailHexCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWaitHexCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcOvertime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTestConfig = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.tlPanelConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeviceCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLayoutColCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassCount)).BeginInit();
            this.tlPanelRoot.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tlPanelResult.SuspendLayout();
            this.panelResult.SuspendLayout();
            this.tlPanelItemConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestItem)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlPanelConfig
            // 
            this.tlPanelConfig.AutoSize = true;
            this.tlPanelConfig.BackColor = System.Drawing.Color.Pink;
            this.tlPanelConfig.ColumnCount = 9;
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlPanelConfig.Controls.Add(this.labBaudRate, 0, 1);
            this.tlPanelConfig.Controls.Add(this.cmbBaudRate, 1, 1);
            this.tlPanelConfig.Controls.Add(this.labStopBits, 2, 1);
            this.tlPanelConfig.Controls.Add(this.cmbStopBits, 3, 1);
            this.tlPanelConfig.Controls.Add(this.labParity, 4, 1);
            this.tlPanelConfig.Controls.Add(this.cmbParity, 5, 1);
            this.tlPanelConfig.Controls.Add(this.labDataBits, 6, 1);
            this.tlPanelConfig.Controls.Add(this.tbDataBits, 7, 1);
            this.tlPanelConfig.Controls.Add(this.label7, 0, 0);
            this.tlPanelConfig.Controls.Add(this.nudDeviceCount, 1, 0);
            this.tlPanelConfig.Controls.Add(this.label6, 2, 0);
            this.tlPanelConfig.Controls.Add(this.nudLayoutColCount, 3, 0);
            this.tlPanelConfig.Controls.Add(this.label3, 4, 0);
            this.tlPanelConfig.Controls.Add(this.nudTestCount, 5, 0);
            this.tlPanelConfig.Controls.Add(this.label4, 6, 0);
            this.tlPanelConfig.Controls.Add(this.nudPassCount, 7, 0);
            this.tlPanelConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlPanelConfig.Location = new System.Drawing.Point(3, 3);
            this.tlPanelConfig.Name = "tlPanelConfig";
            this.tlPanelConfig.RowCount = 2;
            this.tlPanelConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlPanelConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlPanelConfig.Size = new System.Drawing.Size(901, 80);
            this.tlPanelConfig.TabIndex = 0;
            // 
            // labBaudRate
            // 
            this.labBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labBaudRate.AutoSize = true;
            this.labBaudRate.Location = new System.Drawing.Point(25, 52);
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
            "115200",
            "512000"});
            this.cmbBaudRate.Location = new System.Drawing.Point(80, 48);
            this.cmbBaudRate.Margin = new System.Windows.Forms.Padding(0);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(100, 23);
            this.cmbBaudRate.TabIndex = 8;
            // 
            // labStopBits
            // 
            this.labStopBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labStopBits.AutoSize = true;
            this.labStopBits.Location = new System.Drawing.Point(205, 52);
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
            this.cmbStopBits.Location = new System.Drawing.Point(260, 48);
            this.cmbStopBits.Margin = new System.Windows.Forms.Padding(0);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(100, 23);
            this.cmbStopBits.TabIndex = 9;
            // 
            // labParity
            // 
            this.labParity.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labParity.AutoSize = true;
            this.labParity.Location = new System.Drawing.Point(370, 52);
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
            this.cmbParity.Location = new System.Drawing.Point(440, 48);
            this.cmbParity.Margin = new System.Windows.Forms.Padding(0);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(100, 23);
            this.cmbParity.TabIndex = 10;
            // 
            // labDataBits
            // 
            this.labDataBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labDataBits.AutoSize = true;
            this.labDataBits.Location = new System.Drawing.Point(565, 52);
            this.labDataBits.Name = "labDataBits";
            this.labDataBits.Size = new System.Drawing.Size(52, 15);
            this.labDataBits.TabIndex = 2;
            this.labDataBits.Text = "数据位";
            this.labDataBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDataBits
            // 
            this.tbDataBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbDataBits.Location = new System.Drawing.Point(620, 47);
            this.tbDataBits.Margin = new System.Windows.Forms.Padding(0);
            this.tbDataBits.Name = "tbDataBits";
            this.tbDataBits.Size = new System.Drawing.Size(100, 25);
            this.tbDataBits.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "设备总数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudDeviceCount
            // 
            this.nudDeviceCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudDeviceCount.Location = new System.Drawing.Point(83, 7);
            this.nudDeviceCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDeviceCount.Name = "nudDeviceCount";
            this.nudDeviceCount.Size = new System.Drawing.Size(94, 25);
            this.nudDeviceCount.TabIndex = 11;
            this.nudDeviceCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDeviceCount.ValueChanged += new System.EventHandler(this.nudDeviceCount_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(190, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "布局列数";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudLayoutColCount
            // 
            this.nudLayoutColCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudLayoutColCount.Location = new System.Drawing.Point(263, 7);
            this.nudLayoutColCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLayoutColCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLayoutColCount.Name = "nudLayoutColCount";
            this.nudLayoutColCount.Size = new System.Drawing.Size(94, 25);
            this.nudLayoutColCount.TabIndex = 11;
            this.nudLayoutColCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLayoutColCount.ValueChanged += new System.EventHandler(this.nudLayoutColCount_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(370, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "测试次数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudTestCount
            // 
            this.nudTestCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudTestCount.Location = new System.Drawing.Point(443, 7);
            this.nudTestCount.Name = "nudTestCount";
            this.nudTestCount.Size = new System.Drawing.Size(94, 25);
            this.nudTestCount.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(550, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "合格次数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudPassCount
            // 
            this.nudPassCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudPassCount.Location = new System.Drawing.Point(623, 7);
            this.nudPassCount.Name = "nudPassCount";
            this.nudPassCount.Size = new System.Drawing.Size(94, 25);
            this.nudPassCount.TabIndex = 11;
            // 
            // btnFlag
            // 
            this.btnFlag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFlag.BackColor = System.Drawing.Color.Green;
            this.btnFlag.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFlag.ForeColor = System.Drawing.Color.White;
            this.btnFlag.Location = new System.Drawing.Point(3, 3);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Size = new System.Drawing.Size(94, 37);
            this.btnFlag.TabIndex = 17;
            this.btnFlag.Text = "Start";
            this.btnFlag.UseVisualStyleBackColor = false;
            this.btnFlag.Click += new System.EventHandler(this.btnFlag_Click);
            // 
            // tlPanelRoot
            // 
            this.tlPanelRoot.ColumnCount = 2;
            this.tlPanelRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlPanelRoot.Controls.Add(this.rtbLog, 1, 0);
            this.tlPanelRoot.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tlPanelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelRoot.Location = new System.Drawing.Point(0, 0);
            this.tlPanelRoot.Name = "tlPanelRoot";
            this.tlPanelRoot.RowCount = 1;
            this.tlPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelRoot.Size = new System.Drawing.Size(1483, 674);
            this.tlPanelRoot.TabIndex = 1;
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(1230, 3);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(250, 668);
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
            this.tableLayoutPanel4.Controls.Add(this.tlPanelItemConfig, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1221, 668);
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
            this.tableLayoutPanel6.Location = new System.Drawing.Point(4, 265);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1213, 399);
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
            this.label1.Size = new System.Drawing.Size(34, 399);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目结果";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlPanelResult
            // 
            this.tlPanelResult.ColumnCount = 3;
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlPanelResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlPanelResult.Controls.Add(this.panelResult, 0, 0);
            this.tlPanelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelResult.Location = new System.Drawing.Point(43, 3);
            this.tlPanelResult.Name = "tlPanelResult";
            this.tlPanelResult.Padding = new System.Windows.Forms.Padding(3);
            this.tlPanelResult.RowCount = 3;
            this.tlPanelResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlPanelResult.Size = new System.Drawing.Size(1167, 393);
            this.tlPanelResult.TabIndex = 1;
            // 
            // panelResult
            // 
            this.panelResult.Controls.Add(this.progressBar1);
            this.panelResult.Controls.Add(this.labResult);
            this.panelResult.Controls.Add(this.comboBox1);
            this.panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResult.Location = new System.Drawing.Point(6, 6);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(381, 123);
            this.panelResult.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 113);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(381, 10);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Value = 50;
            // 
            // labResult
            // 
            this.labResult.BackColor = System.Drawing.Color.Blue;
            this.labResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labResult.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labResult.ForeColor = System.Drawing.Color.White;
            this.labResult.Location = new System.Drawing.Point(0, 23);
            this.labResult.Name = "labResult";
            this.labResult.Size = new System.Drawing.Size(381, 100);
            this.labResult.TabIndex = 1;
            this.labResult.Text = "测试结果\r\nOK\r\n";
            this.labResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.Blue;
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.ForeColor = System.Drawing.Color.White;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(381, 23);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "COM3";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.cmbSerialPort_SelectedIndexChanged);
            // 
            // tlPanelItemConfig
            // 
            this.tlPanelItemConfig.AutoSize = true;
            this.tlPanelItemConfig.ColumnCount = 2;
            this.tlPanelItemConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlPanelItemConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelItemConfig.Controls.Add(this.dgvTestItem, 1, 1);
            this.tlPanelItemConfig.Controls.Add(this.label5, 0, 0);
            this.tlPanelItemConfig.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tlPanelItemConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlPanelItemConfig.Location = new System.Drawing.Point(4, 97);
            this.tlPanelItemConfig.Name = "tlPanelItemConfig";
            this.tlPanelItemConfig.RowCount = 2;
            this.tlPanelItemConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlPanelItemConfig.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlPanelItemConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlPanelItemConfig.Size = new System.Drawing.Size(1213, 161);
            this.tlPanelItemConfig.TabIndex = 18;
            // 
            // dgvTestItem
            // 
            this.dgvTestItem.AllowUserToAddRows = false;
            this.dgvTestItem.AllowUserToDeleteRows = false;
            this.dgvTestItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTestItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcTestItem,
            this.dgcRequestByteSize,
            this.dgcRequestHexCode,
            this.dgcResponseByteSize,
            this.dgcResponseHexCode,
            this.dgcPassHexCode,
            this.dgcFailHexCode,
            this.dgcWaitHexCode,
            this.dgcOvertime,
            this.dgcTestConfig});
            this.dgvTestItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvTestItem.Location = new System.Drawing.Point(43, 43);
            this.dgvTestItem.Name = "dgvTestItem";
            this.dgvTestItem.ReadOnly = true;
            this.dgvTestItem.RowHeadersVisible = false;
            this.dgvTestItem.RowHeadersWidth = 51;
            this.dgvTestItem.RowTemplate.Height = 27;
            this.dgvTestItem.Size = new System.Drawing.Size(1167, 115);
            this.dgvTestItem.TabIndex = 1;
            this.dgvTestItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTestItem_CellClick);
            // 
            // dgcTestItem
            // 
            this.dgcTestItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcTestItem.DataPropertyName = "TestItem";
            this.dgcTestItem.HeaderText = "项目名称";
            this.dgcTestItem.MinimumWidth = 6;
            this.dgcTestItem.Name = "dgcTestItem";
            this.dgcTestItem.ReadOnly = true;
            this.dgcTestItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcTestItem.Width = 75;
            // 
            // dgcRequestByteSize
            // 
            this.dgcRequestByteSize.DataPropertyName = "RequestByteSize";
            this.dgcRequestByteSize.HeaderText = "请求总字节数";
            this.dgcRequestByteSize.MinimumWidth = 6;
            this.dgcRequestByteSize.Name = "dgcRequestByteSize";
            this.dgcRequestByteSize.ReadOnly = true;
            // 
            // dgcRequestHexCode
            // 
            this.dgcRequestHexCode.DataPropertyName = "RequestHexCode";
            this.dgcRequestHexCode.HeaderText = "请求指令";
            this.dgcRequestHexCode.MinimumWidth = 6;
            this.dgcRequestHexCode.Name = "dgcRequestHexCode";
            this.dgcRequestHexCode.ReadOnly = true;
            // 
            // dgcResponseByteSize
            // 
            this.dgcResponseByteSize.DataPropertyName = "ResponseByteSize";
            this.dgcResponseByteSize.HeaderText = "响应总字节数";
            this.dgcResponseByteSize.MinimumWidth = 6;
            this.dgcResponseByteSize.Name = "dgcResponseByteSize";
            this.dgcResponseByteSize.ReadOnly = true;
            this.dgcResponseByteSize.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgcResponseHexCode
            // 
            this.dgcResponseHexCode.DataPropertyName = "ResponseHexCode";
            this.dgcResponseHexCode.HeaderText = "响应指令";
            this.dgcResponseHexCode.MinimumWidth = 6;
            this.dgcResponseHexCode.Name = "dgcResponseHexCode";
            this.dgcResponseHexCode.ReadOnly = true;
            // 
            // dgcPassHexCode
            // 
            this.dgcPassHexCode.DataPropertyName = "PassHexCode";
            this.dgcPassHexCode.HeaderText = "响应成功";
            this.dgcPassHexCode.MinimumWidth = 6;
            this.dgcPassHexCode.Name = "dgcPassHexCode";
            this.dgcPassHexCode.ReadOnly = true;
            this.dgcPassHexCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgcFailHexCode
            // 
            this.dgcFailHexCode.DataPropertyName = "FailHexCode";
            this.dgcFailHexCode.HeaderText = "响应失败";
            this.dgcFailHexCode.MinimumWidth = 6;
            this.dgcFailHexCode.Name = "dgcFailHexCode";
            this.dgcFailHexCode.ReadOnly = true;
            // 
            // dgcWaitHexCode
            // 
            this.dgcWaitHexCode.DataPropertyName = "WaitHexCode";
            this.dgcWaitHexCode.HeaderText = "响应等待";
            this.dgcWaitHexCode.MinimumWidth = 6;
            this.dgcWaitHexCode.Name = "dgcWaitHexCode";
            this.dgcWaitHexCode.ReadOnly = true;
            // 
            // dgcOvertime
            // 
            this.dgcOvertime.DataPropertyName = "Overtime";
            this.dgcOvertime.HeaderText = "超时(秒)";
            this.dgcOvertime.MinimumWidth = 6;
            this.dgcOvertime.Name = "dgcOvertime";
            this.dgcOvertime.ReadOnly = true;
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
            this.tlPanelItemConfig.SetRowSpan(this.label5, 2);
            this.label5.Size = new System.Drawing.Size(34, 161);
            this.label5.TabIndex = 0;
            this.label5.Text = "指令配置";
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1167, 34);
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
            this.tableLayoutPanel5.Controls.Add(this.tlPanelConfig, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1213, 86);
            this.tableLayoutPanel5.TabIndex = 19;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.btnFlag, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnReset, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnMax, 2, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(910, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel5.SetRowSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(300, 43);
            this.tableLayoutPanel3.TabIndex = 22;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReset.BackColor = System.Drawing.Color.Green;
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(103, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(93, 37);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMax.BackColor = System.Drawing.Color.Green;
            this.btnMax.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMax.ForeColor = System.Drawing.Color.White;
            this.btnMax.Location = new System.Drawing.Point(203, 3);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(93, 37);
            this.btnMax.TabIndex = 17;
            this.btnMax.Text = "最大化";
            this.btnMax.UseVisualStyleBackColor = false;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // FrmAgingTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1483, 674);
            this.Controls.Add(this.tlPanelRoot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAgingTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "老化测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDPLineTest_FormClosing);
            this.Load += new System.EventHandler(this.FrmDPLineTest_Load);
            this.tlPanelConfig.ResumeLayout(false);
            this.tlPanelConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeviceCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLayoutColCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassCount)).EndInit();
            this.tlPanelRoot.ResumeLayout(false);
            this.tlPanelRoot.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tlPanelResult.ResumeLayout(false);
            this.panelResult.ResumeLayout(false);
            this.tlPanelItemConfig.ResumeLayout(false);
            this.tlPanelItemConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestItem)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlPanelConfig;
        private System.Windows.Forms.TableLayoutPanel tlPanelRoot;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button btnFlag;
        private System.Windows.Forms.TableLayoutPanel tlPanelItemConfig;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvTestItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.TableLayoutPanel tlPanelResult;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.Label labResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.Label labParity;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.TextBox tbDataBits;
        private System.Windows.Forms.Label labStopBits;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label labBaudRate;
        private System.Windows.Forms.Label labDataBits;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudTestCount;
        private System.Windows.Forms.NumericUpDown nudPassCount;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTestItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRequestByteSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRequestHexCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcResponseByteSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcResponseHexCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPassHexCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFailHexCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWaitHexCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcOvertime;
        private System.Windows.Forms.DataGridViewButtonColumn dgcTestConfig;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudLayoutColCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudDeviceCount;
        private System.Windows.Forms.Button btnMax;
    }
}


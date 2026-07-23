namespace EAM.Listen.UI
{
    partial class FrmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            this.gbSerailPort = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.labSerailListenFlag = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.ckbSerailListenFlag = new System.Windows.Forms.CheckBox();
            this.labParity = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.labPortName = new System.Windows.Forms.Label();
            this.tbDataBits = new System.Windows.Forms.TextBox();
            this.labStopBits = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.labBaudRate = new System.Windows.Forms.Label();
            this.labDataBits = new System.Windows.Forms.Label();
            this.gbTcp = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labTcpListenFlag = new System.Windows.Forms.Label();
            this.labUnit = new System.Windows.Forms.Label();
            this.nudReceiveTimeout = new System.Windows.Forms.NumericUpDown();
            this.cbListenIP = new System.Windows.Forms.ComboBox();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.btnCheckPort = new System.Windows.Forms.Button();
            this.labLocalIPTitle = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.labReceiveTimeout = new System.Windows.Forms.Label();
            this.ckbTcpListenFlag = new System.Windows.Forms.CheckBox();
            this.gpOperate = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkDayMaintenanceFlag = new System.Windows.Forms.CheckBox();
            this.chkMonthMaintenanceFlag = new System.Windows.Forms.CheckBox();
            this.chkWeekMaintenanceFlag = new System.Windows.Forms.CheckBox();
            this.gpCode = new System.Windows.Forms.GroupBox();
            this.btn_SendCodeConfig = new System.Windows.Forms.Button();
            this.btnReceiveCodeConfig = new System.Windows.Forms.Button();
            this.ckbUploadFlag = new System.Windows.Forms.CheckBox();
            this.gbMqtt = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbTopicList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMqttHost = new System.Windows.Forms.ComboBox();
            this.nudMqttPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbMqttSubFlag = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.nudKeepAliveSeconds = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ckbCleanSession = new System.Windows.Forms.CheckBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbClientId = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAddTopic = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.nudHttpPort = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.ckbHttpListenFlag = new System.Windows.Forms.CheckBox();
            this.gbSerailPort.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.gbTcp.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceiveTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.gpOperate.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gpCode.SuspendLayout();
            this.gbMqtt.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMqttPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKeepAliveSeconds)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHttpPort)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSerailPort
            // 
            this.gbSerailPort.AutoSize = true;
            this.gbSerailPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gbSerailPort.Controls.Add(this.tableLayoutPanel5);
            this.gbSerailPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSerailPort.Location = new System.Drawing.Point(3, 3);
            this.gbSerailPort.Name = "gbSerailPort";
            this.gbSerailPort.Size = new System.Drawing.Size(298, 144);
            this.gbSerailPort.TabIndex = 16;
            this.gbSerailPort.TabStop = false;
            this.gbSerailPort.Text = "串口通信";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.10169F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.44068F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.35593F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.10169F));
            this.tableLayoutPanel5.Controls.Add(this.labSerailListenFlag, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cmbParity, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.ckbSerailListenFlag, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.labParity, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.cmbStopBits, 3, 2);
            this.tableLayoutPanel5.Controls.Add(this.labPortName, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tbDataBits, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.labStopBits, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.cmbBaudRate, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.cmbPortName, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.labBaudRate, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.labDataBits, 0, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(292, 120);
            this.tableLayoutPanel5.TabIndex = 16;
            // 
            // labSerailListenFlag
            // 
            this.labSerailListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labSerailListenFlag.AutoSize = true;
            this.labSerailListenFlag.Location = new System.Drawing.Point(6, 7);
            this.labSerailListenFlag.Name = "labSerailListenFlag";
            this.labSerailListenFlag.Size = new System.Drawing.Size(67, 15);
            this.labSerailListenFlag.TabIndex = 11;
            this.labSerailListenFlag.Text = "串口监听";
            this.labSerailListenFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbParity
            // 
            this.cmbParity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Location = new System.Drawing.Point(76, 93);
            this.cmbParity.Margin = new System.Windows.Forms.Padding(0);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(73, 23);
            this.cmbParity.TabIndex = 10;
            // 
            // ckbSerailListenFlag
            // 
            this.ckbSerailListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbSerailListenFlag.AutoSize = true;
            this.ckbSerailListenFlag.Location = new System.Drawing.Point(79, 6);
            this.ckbSerailListenFlag.Name = "ckbSerailListenFlag";
            this.ckbSerailListenFlag.Size = new System.Drawing.Size(18, 17);
            this.ckbSerailListenFlag.TabIndex = 15;
            // 
            // labParity
            // 
            this.labParity.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labParity.AutoSize = true;
            this.labParity.Location = new System.Drawing.Point(6, 97);
            this.labParity.Name = "labParity";
            this.labParity.Size = new System.Drawing.Size(67, 15);
            this.labParity.TabIndex = 4;
            this.labParity.Text = "奇偶校验";
            this.labParity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Location = new System.Drawing.Point(215, 63);
            this.cmbStopBits.Margin = new System.Windows.Forms.Padding(0);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(75, 23);
            this.cmbStopBits.TabIndex = 9;
            // 
            // labPortName
            // 
            this.labPortName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPortName.AutoSize = true;
            this.labPortName.Location = new System.Drawing.Point(6, 37);
            this.labPortName.Name = "labPortName";
            this.labPortName.Size = new System.Drawing.Size(67, 15);
            this.labPortName.TabIndex = 0;
            this.labPortName.Text = "串口名称";
            this.labPortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDataBits
            // 
            this.tbDataBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbDataBits.Location = new System.Drawing.Point(76, 62);
            this.tbDataBits.Margin = new System.Windows.Forms.Padding(0);
            this.tbDataBits.Name = "tbDataBits";
            this.tbDataBits.Size = new System.Drawing.Size(73, 25);
            this.tbDataBits.TabIndex = 5;
            // 
            // labStopBits
            // 
            this.labStopBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labStopBits.AutoSize = true;
            this.labStopBits.Location = new System.Drawing.Point(160, 67);
            this.labStopBits.Name = "labStopBits";
            this.labStopBits.Size = new System.Drawing.Size(52, 15);
            this.labStopBits.TabIndex = 3;
            this.labStopBits.Text = "停止位";
            this.labStopBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.cmbBaudRate.Location = new System.Drawing.Point(215, 33);
            this.cmbBaudRate.Margin = new System.Windows.Forms.Padding(0);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(75, 23);
            this.cmbBaudRate.TabIndex = 8;
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
            this.cmbPortName.Location = new System.Drawing.Point(76, 33);
            this.cmbPortName.Margin = new System.Windows.Forms.Padding(0);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(73, 23);
            this.cmbPortName.TabIndex = 6;
            // 
            // labBaudRate
            // 
            this.labBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labBaudRate.AutoSize = true;
            this.labBaudRate.Location = new System.Drawing.Point(160, 37);
            this.labBaudRate.Name = "labBaudRate";
            this.labBaudRate.Size = new System.Drawing.Size(52, 15);
            this.labBaudRate.TabIndex = 1;
            this.labBaudRate.Text = "波特率";
            this.labBaudRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labDataBits
            // 
            this.labDataBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labDataBits.AutoSize = true;
            this.labDataBits.Location = new System.Drawing.Point(21, 67);
            this.labDataBits.Name = "labDataBits";
            this.labDataBits.Size = new System.Drawing.Size(52, 15);
            this.labDataBits.TabIndex = 2;
            this.labDataBits.Text = "数据位";
            this.labDataBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbTcp
            // 
            this.gbTcp.AutoSize = true;
            this.gbTcp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gbTcp.Controls.Add(this.tableLayoutPanel4);
            this.gbTcp.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTcp.Location = new System.Drawing.Point(3, 3);
            this.gbTcp.Name = "gbTcp";
            this.gbTcp.Size = new System.Drawing.Size(298, 144);
            this.gbTcp.TabIndex = 15;
            this.gbTcp.TabStop = false;
            this.gbTcp.Text = "TCP通信";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.44068F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.22034F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Controls.Add(this.labTcpListenFlag, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.labUnit, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.nudReceiveTimeout, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.cbListenIP, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.nudPort, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.btnCheckPort, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.labLocalIPTitle, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labPort, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.labReceiveTimeout, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.ckbTcpListenFlag, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(292, 120);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // labTcpListenFlag
            // 
            this.labTcpListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTcpListenFlag.AutoSize = true;
            this.labTcpListenFlag.Location = new System.Drawing.Point(13, 7);
            this.labTcpListenFlag.Name = "labTcpListenFlag";
            this.labTcpListenFlag.Size = new System.Drawing.Size(61, 15);
            this.labTcpListenFlag.TabIndex = 0;
            this.labTcpListenFlag.Text = "TCP监听";
            this.labTcpListenFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labUnit
            // 
            this.labUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labUnit.AutoSize = true;
            this.labUnit.Location = new System.Drawing.Point(235, 97);
            this.labUnit.Name = "labUnit";
            this.labUnit.Size = new System.Drawing.Size(38, 15);
            this.labUnit.TabIndex = 6;
            this.labUnit.Text = "(秒)";
            // 
            // nudReceiveTimeout
            // 
            this.nudReceiveTimeout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudReceiveTimeout.Location = new System.Drawing.Point(77, 92);
            this.nudReceiveTimeout.Margin = new System.Windows.Forms.Padding(0);
            this.nudReceiveTimeout.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudReceiveTimeout.Name = "nudReceiveTimeout";
            this.nudReceiveTimeout.Size = new System.Drawing.Size(147, 25);
            this.nudReceiveTimeout.TabIndex = 5;
            this.nudReceiveTimeout.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // cbListenIP
            // 
            this.cbListenIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbListenIP.FormattingEnabled = true;
            this.cbListenIP.Location = new System.Drawing.Point(77, 33);
            this.cbListenIP.Margin = new System.Windows.Forms.Padding(0);
            this.cbListenIP.Name = "cbListenIP";
            this.cbListenIP.Size = new System.Drawing.Size(147, 23);
            this.cbListenIP.TabIndex = 10;
            // 
            // nudPort
            // 
            this.nudPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudPort.Location = new System.Drawing.Point(77, 62);
            this.nudPort.Margin = new System.Windows.Forms.Padding(0);
            this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(147, 25);
            this.nudPort.TabIndex = 10;
            this.nudPort.Value = new decimal(new int[] {
            10409,
            0,
            0,
            0});
            // 
            // btnCheckPort
            // 
            this.btnCheckPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCheckPort.Location = new System.Drawing.Point(232, 63);
            this.btnCheckPort.Margin = new System.Windows.Forms.Padding(0);
            this.btnCheckPort.Name = "btnCheckPort";
            this.btnCheckPort.Size = new System.Drawing.Size(51, 23);
            this.btnCheckPort.TabIndex = 13;
            this.btnCheckPort.Text = "检查";
            this.btnCheckPort.UseVisualStyleBackColor = true;
            this.btnCheckPort.Click += new System.EventHandler(this.btnCheckPort_Click);
            // 
            // labLocalIPTitle
            // 
            this.labLocalIPTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labLocalIPTitle.AutoSize = true;
            this.labLocalIPTitle.Location = new System.Drawing.Point(21, 37);
            this.labLocalIPTitle.Name = "labLocalIPTitle";
            this.labLocalIPTitle.Size = new System.Drawing.Size(53, 15);
            this.labLocalIPTitle.TabIndex = 11;
            this.labLocalIPTitle.Text = "监听IP";
            this.labLocalIPTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labPort
            // 
            this.labPort.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(37, 67);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(37, 15);
            this.labPort.TabIndex = 2;
            this.labPort.Text = "端口";
            this.labPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labReceiveTimeout
            // 
            this.labReceiveTimeout.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labReceiveTimeout.AutoSize = true;
            this.labReceiveTimeout.Location = new System.Drawing.Point(7, 97);
            this.labReceiveTimeout.Name = "labReceiveTimeout";
            this.labReceiveTimeout.Size = new System.Drawing.Size(67, 15);
            this.labReceiveTimeout.TabIndex = 4;
            this.labReceiveTimeout.Text = "接收超时";
            this.labReceiveTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ckbTcpListenFlag
            // 
            this.ckbTcpListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbTcpListenFlag.AutoSize = true;
            this.ckbTcpListenFlag.Location = new System.Drawing.Point(80, 6);
            this.ckbTcpListenFlag.Name = "ckbTcpListenFlag";
            this.ckbTcpListenFlag.Size = new System.Drawing.Size(18, 17);
            this.ckbTcpListenFlag.TabIndex = 14;
            // 
            // gpOperate
            // 
            this.gpOperate.AutoSize = true;
            this.gpOperate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gpOperate.Controls.Add(this.tableLayoutPanel2);
            this.gpOperate.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpOperate.Enabled = false;
            this.gpOperate.Location = new System.Drawing.Point(3, 3);
            this.gpOperate.Name = "gpOperate";
            this.gpOperate.Padding = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.gpOperate.Size = new System.Drawing.Size(298, 114);
            this.gpOperate.TabIndex = 17;
            this.gpOperate.TabStop = false;
            this.gpOperate.Text = "保养操作";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.chkDayMaintenanceFlag, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkMonthMaintenanceFlag, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.chkWeekMaintenanceFlag, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 21);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(285, 90);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // chkDayMaintenanceFlag
            // 
            this.chkDayMaintenanceFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDayMaintenanceFlag.AutoSize = true;
            this.chkDayMaintenanceFlag.Location = new System.Drawing.Point(3, 5);
            this.chkDayMaintenanceFlag.Name = "chkDayMaintenanceFlag";
            this.chkDayMaintenanceFlag.Size = new System.Drawing.Size(164, 19);
            this.chkDayMaintenanceFlag.TabIndex = 5;
            this.chkDayMaintenanceFlag.Text = "【日】保养检查开关";
            this.chkDayMaintenanceFlag.UseVisualStyleBackColor = true;
            // 
            // chkMonthMaintenanceFlag
            // 
            this.chkMonthMaintenanceFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkMonthMaintenanceFlag.AutoSize = true;
            this.chkMonthMaintenanceFlag.Location = new System.Drawing.Point(3, 65);
            this.chkMonthMaintenanceFlag.Name = "chkMonthMaintenanceFlag";
            this.chkMonthMaintenanceFlag.Size = new System.Drawing.Size(164, 19);
            this.chkMonthMaintenanceFlag.TabIndex = 7;
            this.chkMonthMaintenanceFlag.Text = "【月】保养检查开关";
            this.chkMonthMaintenanceFlag.UseVisualStyleBackColor = true;
            // 
            // chkWeekMaintenanceFlag
            // 
            this.chkWeekMaintenanceFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkWeekMaintenanceFlag.AutoSize = true;
            this.chkWeekMaintenanceFlag.Location = new System.Drawing.Point(3, 35);
            this.chkWeekMaintenanceFlag.Name = "chkWeekMaintenanceFlag";
            this.chkWeekMaintenanceFlag.Size = new System.Drawing.Size(164, 19);
            this.chkWeekMaintenanceFlag.TabIndex = 6;
            this.chkWeekMaintenanceFlag.Text = "【周】保养检查开关";
            this.chkWeekMaintenanceFlag.UseVisualStyleBackColor = true;
            // 
            // gpCode
            // 
            this.gpCode.AutoSize = true;
            this.gpCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gpCode.Controls.Add(this.btn_SendCodeConfig);
            this.gpCode.Controls.Add(this.btnReceiveCodeConfig);
            this.gpCode.Controls.Add(this.ckbUploadFlag);
            this.gpCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpCode.Location = new System.Drawing.Point(3, 3);
            this.gpCode.Name = "gpCode";
            this.gpCode.Size = new System.Drawing.Size(298, 115);
            this.gpCode.TabIndex = 19;
            this.gpCode.TabStop = false;
            this.gpCode.Text = "通信设置";
            // 
            // btn_SendCodeConfig
            // 
            this.btn_SendCodeConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SendCodeConfig.Location = new System.Drawing.Point(3, 76);
            this.btn_SendCodeConfig.Name = "btn_SendCodeConfig";
            this.btn_SendCodeConfig.Size = new System.Drawing.Size(292, 36);
            this.btn_SendCodeConfig.TabIndex = 28;
            this.btn_SendCodeConfig.Text = "发送数据编码配置";
            this.btn_SendCodeConfig.UseVisualStyleBackColor = true;
            this.btn_SendCodeConfig.Click += new System.EventHandler(this.btn_SendCodeConfig_Click);
            // 
            // btnReceiveCodeConfig
            // 
            this.btnReceiveCodeConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReceiveCodeConfig.Location = new System.Drawing.Point(3, 40);
            this.btnReceiveCodeConfig.Name = "btnReceiveCodeConfig";
            this.btnReceiveCodeConfig.Size = new System.Drawing.Size(292, 36);
            this.btnReceiveCodeConfig.TabIndex = 27;
            this.btnReceiveCodeConfig.Text = "接收数据编码配置";
            this.btnReceiveCodeConfig.UseVisualStyleBackColor = true;
            this.btnReceiveCodeConfig.Click += new System.EventHandler(this.btnReceiveCodeConfig_Click);
            // 
            // ckbUploadFlag
            // 
            this.ckbUploadFlag.AutoSize = true;
            this.ckbUploadFlag.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbUploadFlag.Enabled = false;
            this.ckbUploadFlag.Location = new System.Drawing.Point(3, 21);
            this.ckbUploadFlag.Name = "ckbUploadFlag";
            this.ckbUploadFlag.Size = new System.Drawing.Size(292, 19);
            this.ckbUploadFlag.TabIndex = 29;
            this.ckbUploadFlag.Text = "是否上传数据";
            this.ckbUploadFlag.UseVisualStyleBackColor = true;
            // 
            // gbMqtt
            // 
            this.gbMqtt.AutoSize = true;
            this.gbMqtt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gbMqtt.Controls.Add(this.tableLayoutPanel1);
            this.gbMqtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMqtt.Location = new System.Drawing.Point(3, 3);
            this.gbMqtt.Name = "gbMqtt";
            this.gbMqtt.Size = new System.Drawing.Size(298, 344);
            this.gbMqtt.TabIndex = 20;
            this.gbMqtt.TabStop = false;
            this.gbMqtt.Text = "MQTT通信";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.cbTopicList, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbMqttHost, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudMqttPort, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ckbMqttSubFlag, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbVersion, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbUsername, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.nudKeepAliveSeconds, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.ckbCleanSession, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.tbPassword, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbClientId, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.btnAddTopic, 2, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(292, 320);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // cbTopicList
            // 
            this.cbTopicList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbTopicList.FormattingEnabled = true;
            this.cbTopicList.Items.AddRange(new object[] {
            "3",
            "4",
            "5"});
            this.cbTopicList.Location = new System.Drawing.Point(87, 292);
            this.cbTopicList.Margin = new System.Windows.Forms.Padding(0);
            this.cbTopicList.Name = "cbTopicList";
            this.cbTopicList.Size = new System.Drawing.Size(142, 23);
            this.cbTopicList.TabIndex = 18;
            this.cbTopicList.SelectedIndexChanged += new System.EventHandler(this.cbTopicList_SelectedIndexChanged);
            this.cbTopicList.TextChanged += new System.EventHandler(this.cbTopicList_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "MQTT订阅";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbMqttHost
            // 
            this.cbMqttHost.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbMqttHost.FormattingEnabled = true;
            this.cbMqttHost.Location = new System.Drawing.Point(87, 36);
            this.cbMqttHost.Margin = new System.Windows.Forms.Padding(0);
            this.cbMqttHost.Name = "cbMqttHost";
            this.cbMqttHost.Size = new System.Drawing.Size(142, 23);
            this.cbMqttHost.TabIndex = 10;
            // 
            // nudMqttPort
            // 
            this.nudMqttPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudMqttPort.Location = new System.Drawing.Point(87, 67);
            this.nudMqttPort.Margin = new System.Windows.Forms.Padding(0);
            this.nudMqttPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudMqttPort.Name = "nudMqttPort";
            this.nudMqttPort.Size = new System.Drawing.Size(142, 25);
            this.nudMqttPort.TabIndex = 10;
            this.nudMqttPort.Value = new decimal(new int[] {
            1883,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "代理主机";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "端口";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ckbMqttSubFlag
            // 
            this.ckbMqttSubFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbMqttSubFlag.AutoSize = true;
            this.ckbMqttSubFlag.Location = new System.Drawing.Point(90, 7);
            this.ckbMqttSubFlag.Name = "ckbMqttSubFlag";
            this.ckbMqttSubFlag.Size = new System.Drawing.Size(18, 17);
            this.ckbMqttSubFlag.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "用户";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Mqtt版本";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbVersion
            // 
            this.cbVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Items.AddRange(new object[] {
            "3",
            "4",
            "5"});
            this.cbVersion.Location = new System.Drawing.Point(87, 100);
            this.cbVersion.Margin = new System.Windows.Forms.Padding(0);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(142, 23);
            this.cbVersion.TabIndex = 10;
            // 
            // tbUsername
            // 
            this.tbUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbUsername.Location = new System.Drawing.Point(90, 131);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(136, 25);
            this.tbUsername.TabIndex = 16;
            // 
            // nudKeepAliveSeconds
            // 
            this.nudKeepAliveSeconds.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudKeepAliveSeconds.Location = new System.Drawing.Point(87, 259);
            this.nudKeepAliveSeconds.Margin = new System.Windows.Forms.Padding(0);
            this.nudKeepAliveSeconds.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudKeepAliveSeconds.Name = "nudKeepAliveSeconds";
            this.nudKeepAliveSeconds.Size = new System.Drawing.Size(142, 25);
            this.nudKeepAliveSeconds.TabIndex = 5;
            this.nudKeepAliveSeconds.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "保活间隔";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "(秒)";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "密码";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 200);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "客户端";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 232);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "清理会话";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ckbCleanSession
            // 
            this.ckbCleanSession.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbCleanSession.AutoSize = true;
            this.ckbCleanSession.Location = new System.Drawing.Point(90, 231);
            this.ckbCleanSession.Name = "ckbCleanSession";
            this.ckbCleanSession.Size = new System.Drawing.Size(18, 17);
            this.ckbCleanSession.TabIndex = 14;
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbPassword.Location = new System.Drawing.Point(90, 163);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(136, 25);
            this.tbPassword.TabIndex = 16;
            // 
            // tbClientId
            // 
            this.tbClientId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbClientId.Location = new System.Drawing.Point(90, 195);
            this.tbClientId.Name = "tbClientId";
            this.tbClientId.Size = new System.Drawing.Size(136, 25);
            this.tbClientId.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 296);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "订阅主题";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddTopic
            // 
            this.btnAddTopic.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAddTopic.Location = new System.Drawing.Point(233, 292);
            this.btnAddTopic.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddTopic.Name = "btnAddTopic";
            this.btnAddTopic.Size = new System.Drawing.Size(51, 23);
            this.btnAddTopic.TabIndex = 13;
            this.btnAddTopic.Text = "添加";
            this.btnAddTopic.UseVisualStyleBackColor = true;
            this.btnAddTopic.Click += new System.EventHandler(this.btnAddTopic_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(312, 392);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbMqtt);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(304, 363);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MQTT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbTcp);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(304, 363);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TCP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gbSerailPort);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(304, 363);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "串口";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gpCode);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(304, 363);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "通信规则";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.gpOperate);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(304, 363);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "保养配置";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox1);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(304, 363);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "HTTP";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 144);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Http通信";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.53061F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.40136F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.06803F));
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.nudHttpPort, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.button1, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label15, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.ckbHttpListenFlag, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(292, 120);
            this.tableLayoutPanel3.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 15);
            this.label12.TabIndex = 0;
            this.label12.Text = "Http监听";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudHttpPort
            // 
            this.nudHttpPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudHttpPort.Location = new System.Drawing.Point(77, 32);
            this.nudHttpPort.Margin = new System.Windows.Forms.Padding(0);
            this.nudHttpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudHttpPort.Name = "nudHttpPort";
            this.nudHttpPort.Size = new System.Drawing.Size(147, 25);
            this.nudHttpPort.TabIndex = 10;
            this.nudHttpPort.Value = new decimal(new int[] {
            8409,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.Location = new System.Drawing.Point(232, 33);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "检查";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(37, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 15);
            this.label15.TabIndex = 2;
            this.label15.Text = "端口";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ckbHttpListenFlag
            // 
            this.ckbHttpListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbHttpListenFlag.AutoSize = true;
            this.ckbHttpListenFlag.Location = new System.Drawing.Point(80, 6);
            this.ckbHttpListenFlag.Name = "ckbHttpListenFlag";
            this.ckbHttpListenFlag.Size = new System.Drawing.Size(18, 17);
            this.ckbHttpListenFlag.TabIndex = 14;
            // 
            // FrmConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(312, 392);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "系统设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.gbSerailPort.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.gbTcp.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceiveTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.gpOperate.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gpCode.ResumeLayout(false);
            this.gpCode.PerformLayout();
            this.gbMqtt.ResumeLayout(false);
            this.gbMqtt.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMqttPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKeepAliveSeconds)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHttpPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labTcpListenFlag;
        private System.Windows.Forms.Label labPort;
        private System.Windows.Forms.Label labUnit;
        private System.Windows.Forms.NumericUpDown nudReceiveTimeout;
        private System.Windows.Forms.Label labReceiveTimeout;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Label labLocalIPTitle;
        private System.Windows.Forms.Button btnCheckPort;
        private System.Windows.Forms.CheckBox ckbTcpListenFlag;
        private System.Windows.Forms.GroupBox gbTcp;
        private System.Windows.Forms.GroupBox gbSerailPort;
        private System.Windows.Forms.Label labStopBits;
        private System.Windows.Forms.Label labDataBits;
        private System.Windows.Forms.Label labBaudRate;
        private System.Windows.Forms.Label labPortName;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.TextBox tbDataBits;
        private System.Windows.Forms.Label labParity;
        private System.Windows.Forms.CheckBox ckbSerailListenFlag;
        private System.Windows.Forms.Label labSerailListenFlag;
        private System.Windows.Forms.GroupBox gpOperate;
        private System.Windows.Forms.ComboBox cbListenIP;
        private System.Windows.Forms.CheckBox chkMonthMaintenanceFlag;
        private System.Windows.Forms.CheckBox chkWeekMaintenanceFlag;
        private System.Windows.Forms.CheckBox chkDayMaintenanceFlag;
        private System.Windows.Forms.GroupBox gpCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnReceiveCodeConfig;
        private System.Windows.Forms.Button btn_SendCodeConfig;
        private System.Windows.Forms.CheckBox ckbUploadFlag;
        private System.Windows.Forms.GroupBox gbMqtt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudKeepAliveSeconds;
        private System.Windows.Forms.ComboBox cbMqttHost;
        private System.Windows.Forms.NumericUpDown nudMqttPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckbMqttSubFlag;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbVersion;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ckbCleanSession;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbClientId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbTopicList;
        private System.Windows.Forms.Button btnAddTopic;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudHttpPort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox ckbHttpListenFlag;
    }
}
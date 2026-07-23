namespace Machine
{
    partial class FrmMachineWatch
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
            System.Windows.Forms.Panel panelMachine;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMachineWatch));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitPanel = new System.Windows.Forms.SplitContainer();
            this.labLineTitle = new System.Windows.Forms.Label();
            this.labProductTitle = new System.Windows.Forms.Label();
            this.labOEETitle = new System.Windows.Forms.Label();
            this.labLine = new System.Windows.Forms.Label();
            this.labProductCount = new System.Windows.Forms.Label();
            this.labOEE = new System.Windows.Forms.Label();
            this.labMachineName = new System.Windows.Forms.Label();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelTopNode = new System.Windows.Forms.Panel();
            this.labMachineCount = new System.Windows.Forms.Label();
            this.labPageNo = new System.Windows.Forms.Label();
            this.pboxNextPage = new System.Windows.Forms.PictureBox();
            this.pboxPrePage = new System.Windows.Forms.PictureBox();
            this.dtpCurrDate = new System.Windows.Forms.DateTimePicker();
            this.labTitle = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labLeftTitle = new System.Windows.Forms.Label();
            this.panelTopLine = new System.Windows.Forms.Panel();
            this.panelFill = new System.Windows.Forms.Panel();
            this.tlpanelMachines = new System.Windows.Forms.TableLayoutPanel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.chartOEE = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMachineStateR = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartUR = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelBottomLine = new System.Windows.Forms.Panel();
            this.timerRoll = new System.Windows.Forms.Timer(this.components);
            panelMachine = new System.Windows.Forms.Panel();
            panelMachine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).BeginInit();
            this.splitPanel.Panel1.SuspendLayout();
            this.splitPanel.Panel2.SuspendLayout();
            this.splitPanel.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelTopNode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxNextPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxPrePage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelFill.SuspendLayout();
            this.tlpanelMachines.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartOEE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMachineStateR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUR)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMachine
            // 
            panelMachine.BackColor = System.Drawing.Color.Gray;
            panelMachine.Controls.Add(this.splitPanel);
            panelMachine.Controls.Add(this.labMachineName);
            panelMachine.Dock = System.Windows.Forms.DockStyle.Top;
            panelMachine.Font = new System.Drawing.Font("宋体", 6F);
            panelMachine.Location = new System.Drawing.Point(3, 3);
            panelMachine.Name = "panelMachine";
            panelMachine.Size = new System.Drawing.Size(71, 60);
            panelMachine.TabIndex = 0;
            // 
            // splitPanel
            // 
            this.splitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPanel.Location = new System.Drawing.Point(0, 21);
            this.splitPanel.Margin = new System.Windows.Forms.Padding(0);
            this.splitPanel.Name = "splitPanel";
            // 
            // splitPanel.Panel1
            // 
            this.splitPanel.Panel1.Controls.Add(this.labLineTitle);
            this.splitPanel.Panel1.Controls.Add(this.labProductTitle);
            this.splitPanel.Panel1.Controls.Add(this.labOEETitle);
            // 
            // splitPanel.Panel2
            // 
            this.splitPanel.Panel2.Controls.Add(this.labLine);
            this.splitPanel.Panel2.Controls.Add(this.labProductCount);
            this.splitPanel.Panel2.Controls.Add(this.labOEE);
            this.splitPanel.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitPanel.Size = new System.Drawing.Size(71, 39);
            this.splitPanel.SplitterDistance = 30;
            this.splitPanel.SplitterWidth = 1;
            this.splitPanel.TabIndex = 4;
            // 
            // labLineTitle
            // 
            this.labLineTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labLineTitle.Location = new System.Drawing.Point(0, 28);
            this.labLineTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labLineTitle.Name = "labLineTitle";
            this.labLineTitle.Size = new System.Drawing.Size(30, 14);
            this.labLineTitle.TabIndex = 4;
            this.labLineTitle.Text = "产线";
            this.labLineTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labProductTitle
            // 
            this.labProductTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labProductTitle.Location = new System.Drawing.Point(0, 14);
            this.labProductTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labProductTitle.Name = "labProductTitle";
            this.labProductTitle.Size = new System.Drawing.Size(30, 14);
            this.labProductTitle.TabIndex = 0;
            this.labProductTitle.Text = "产量";
            this.labProductTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labOEETitle
            // 
            this.labOEETitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labOEETitle.Location = new System.Drawing.Point(0, 0);
            this.labOEETitle.Margin = new System.Windows.Forms.Padding(0);
            this.labOEETitle.Name = "labOEETitle";
            this.labOEETitle.Size = new System.Drawing.Size(30, 14);
            this.labOEETitle.TabIndex = 2;
            this.labOEETitle.Text = "OEE";
            this.labOEETitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labLine
            // 
            this.labLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.labLine.ForeColor = System.Drawing.Color.Aqua;
            this.labLine.Location = new System.Drawing.Point(0, 28);
            this.labLine.Margin = new System.Windows.Forms.Padding(0);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(40, 14);
            this.labLine.TabIndex = 8;
            this.labLine.Text = "未知";
            this.labLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labProductCount
            // 
            this.labProductCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.labProductCount.ForeColor = System.Drawing.Color.Aqua;
            this.labProductCount.Location = new System.Drawing.Point(0, 14);
            this.labProductCount.Margin = new System.Windows.Forms.Padding(0);
            this.labProductCount.Name = "labProductCount";
            this.labProductCount.Size = new System.Drawing.Size(40, 14);
            this.labProductCount.TabIndex = 6;
            this.labProductCount.Text = "0";
            this.labProductCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labOEE
            // 
            this.labOEE.Dock = System.Windows.Forms.DockStyle.Top;
            this.labOEE.ForeColor = System.Drawing.Color.Aqua;
            this.labOEE.Location = new System.Drawing.Point(0, 0);
            this.labOEE.Margin = new System.Windows.Forms.Padding(0);
            this.labOEE.Name = "labOEE";
            this.labOEE.Size = new System.Drawing.Size(40, 14);
            this.labOEE.TabIndex = 5;
            this.labOEE.Text = "0.000%";
            this.labOEE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labMachineName
            // 
            this.labMachineName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labMachineName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labMachineName.Location = new System.Drawing.Point(0, 0);
            this.labMachineName.Name = "labMachineName";
            this.labMachineName.Size = new System.Drawing.Size(71, 21);
            this.labMachineName.TabIndex = 3;
            this.labMachineName.Text = "设备名称";
            this.labMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerRefresh
            // 
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Interval = 60000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Transparent;
            this.panelTop.Controls.Add(this.panelTopNode);
            this.panelTop.Controls.Add(this.panelTopLine);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1280, 60);
            this.panelTop.TabIndex = 10;
            // 
            // panelTopNode
            // 
            this.panelTopNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            this.panelTopNode.Controls.Add(this.labMachineCount);
            this.panelTopNode.Controls.Add(this.labPageNo);
            this.panelTopNode.Controls.Add(this.pboxNextPage);
            this.panelTopNode.Controls.Add(this.pboxPrePage);
            this.panelTopNode.Controls.Add(this.dtpCurrDate);
            this.panelTopNode.Controls.Add(this.labTitle);
            this.panelTopNode.Controls.Add(this.pictureBox2);
            this.panelTopNode.Controls.Add(this.labLeftTitle);
            this.panelTopNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTopNode.Location = new System.Drawing.Point(0, 0);
            this.panelTopNode.Name = "panelTopNode";
            this.panelTopNode.Size = new System.Drawing.Size(1280, 57);
            this.panelTopNode.TabIndex = 7;
            // 
            // labMachineCount
            // 
            this.labMachineCount.AutoSize = true;
            this.labMachineCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            this.labMachineCount.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMachineCount.ForeColor = System.Drawing.Color.White;
            this.labMachineCount.Location = new System.Drawing.Point(805, 34);
            this.labMachineCount.Margin = new System.Windows.Forms.Padding(0);
            this.labMachineCount.Name = "labMachineCount";
            this.labMachineCount.Size = new System.Drawing.Size(105, 19);
            this.labMachineCount.TabIndex = 11;
            this.labMachineCount.Text = "设备总数:0";
            // 
            // labPageNo
            // 
            this.labPageNo.BackColor = System.Drawing.Color.Blue;
            this.labPageNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPageNo.ForeColor = System.Drawing.Color.White;
            this.labPageNo.Location = new System.Drawing.Point(954, 28);
            this.labPageNo.Margin = new System.Windows.Forms.Padding(0);
            this.labPageNo.Name = "labPageNo";
            this.labPageNo.Size = new System.Drawing.Size(35, 25);
            this.labPageNo.TabIndex = 9;
            this.labPageNo.Text = "0";
            this.labPageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pboxNextPage
            // 
            this.pboxNextPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pboxNextPage.BackgroundImage")));
            this.pboxNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pboxNextPage.Location = new System.Drawing.Point(989, 28);
            this.pboxNextPage.Name = "pboxNextPage";
            this.pboxNextPage.Size = new System.Drawing.Size(25, 25);
            this.pboxNextPage.TabIndex = 8;
            this.pboxNextPage.TabStop = false;
            this.pboxNextPage.Click += new System.EventHandler(this.pboxNextPage_Click);
            // 
            // pboxPrePage
            // 
            this.pboxPrePage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pboxPrePage.BackgroundImage")));
            this.pboxPrePage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pboxPrePage.Location = new System.Drawing.Point(929, 28);
            this.pboxPrePage.Name = "pboxPrePage";
            this.pboxPrePage.Size = new System.Drawing.Size(25, 25);
            this.pboxPrePage.TabIndex = 7;
            this.pboxPrePage.TabStop = false;
            this.pboxPrePage.Click += new System.EventHandler(this.pboxPrePage_Click);
            // 
            // dtpCurrDate
            // 
            this.dtpCurrDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpCurrDate.CustomFormat = "yyyy-MM-dd";
            this.dtpCurrDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCurrDate.Location = new System.Drawing.Point(162, 23);
            this.dtpCurrDate.Name = "dtpCurrDate";
            this.dtpCurrDate.Size = new System.Drawing.Size(106, 25);
            this.dtpCurrDate.TabIndex = 6;
            this.dtpCurrDate.ValueChanged += new System.EventHandler(this.dtpCurrDate_ValueChanged);
            // 
            // labTitle
            // 
            this.labTitle.BackColor = System.Drawing.Color.Transparent;
            this.labTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTitle.Font = new System.Drawing.Font("幼圆", 25.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.ForeColor = System.Drawing.Color.White;
            this.labTitle.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.labTitle.Location = new System.Drawing.Point(250, 0);
            this.labTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(780, 57);
            this.labTitle.TabIndex = 2;
            this.labTitle.Text = "设备管理系统";
            this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Location = new System.Drawing.Point(1030, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(250, 57);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // labLeftTitle
            // 
            this.labLeftTitle.BackColor = System.Drawing.Color.Transparent;
            this.labLeftTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.labLeftTitle.Font = new System.Drawing.Font("幼圆", 15.2F);
            this.labLeftTitle.ForeColor = System.Drawing.Color.White;
            this.labLeftTitle.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.labLeftTitle.Location = new System.Drawing.Point(0, 0);
            this.labLeftTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labLeftTitle.Name = "labLeftTitle";
            this.labLeftTitle.Size = new System.Drawing.Size(250, 57);
            this.labLeftTitle.TabIndex = 12;
            this.labLeftTitle.Text = "全体监控";
            this.labLeftTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelTopLine
            // 
            this.panelTopLine.BackColor = System.Drawing.Color.White;
            this.panelTopLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTopLine.Location = new System.Drawing.Point(0, 57);
            this.panelTopLine.Margin = new System.Windows.Forms.Padding(0);
            this.panelTopLine.Name = "panelTopLine";
            this.panelTopLine.Size = new System.Drawing.Size(1280, 3);
            this.panelTopLine.TabIndex = 5;
            // 
            // panelFill
            // 
            this.panelFill.BackColor = System.Drawing.Color.Transparent;
            this.panelFill.Controls.Add(this.tlpanelMachines);
            this.panelFill.Controls.Add(this.panelRight);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.ForeColor = System.Drawing.Color.White;
            this.panelFill.Location = new System.Drawing.Point(0, 60);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(1280, 400);
            this.panelFill.TabIndex = 11;
            // 
            // tlpanelMachines
            // 
            this.tlpanelMachines.ColumnCount = 14;
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142856F));
            this.tlpanelMachines.Controls.Add(panelMachine, 0, 0);
            this.tlpanelMachines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpanelMachines.Location = new System.Drawing.Point(0, 0);
            this.tlpanelMachines.Name = "tlpanelMachines";
            this.tlpanelMachines.RowCount = 6;
            this.tlpanelMachines.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpanelMachines.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpanelMachines.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpanelMachines.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpanelMachines.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpanelMachines.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpanelMachines.Size = new System.Drawing.Size(1080, 400);
            this.tlpanelMachines.TabIndex = 1;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.chartOEE);
            this.panelRight.Controls.Add(this.chartMachineStateR);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(1080, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(200, 400);
            this.panelRight.TabIndex = 1;
            // 
            // chartOEE
            // 
            this.chartOEE.BackColor = System.Drawing.Color.Transparent;
            this.chartOEE.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartAreaOEE";
            this.chartOEE.ChartAreas.Add(chartArea1);
            this.chartOEE.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Enabled = false;
            legend1.Name = "LegendOEE";
            this.chartOEE.Legends.Add(legend1);
            this.chartOEE.Location = new System.Drawing.Point(0, 156);
            this.chartOEE.Name = "chartOEE";
            this.chartOEE.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartOEE.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Cyan,
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))))};
            series1.ChartArea = "ChartAreaOEE";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "LegendOEE";
            series1.Name = "SeriesOEE";
            series1.ToolTip = "#PERCENT";
            this.chartOEE.Series.Add(series1);
            this.chartOEE.Size = new System.Drawing.Size(200, 149);
            this.chartOEE.TabIndex = 2;
            this.chartOEE.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.White;
            title1.Name = "Title1";
            title1.Text = "平均OEE:0%";
            this.chartOEE.Titles.Add(title1);
            // 
            // chartMachineStateR
            // 
            this.chartMachineStateR.BackColor = System.Drawing.Color.Transparent;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartAreaRunState";
            this.chartMachineStateR.ChartAreas.Add(chartArea2);
            this.chartMachineStateR.Dock = System.Windows.Forms.DockStyle.Top;
            legend2.Enabled = false;
            legend2.Name = "LegendRunState";
            this.chartMachineStateR.Legends.Add(legend2);
            this.chartMachineStateR.Location = new System.Drawing.Point(0, 0);
            this.chartMachineStateR.Name = "chartMachineStateR";
            this.chartMachineStateR.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartMachineStateR.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64))))),
        System.Drawing.Color.Green,
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange};
            series2.ChartArea = "ChartAreaRunState";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.Label = "#VAL";
            series2.LabelForeColor = System.Drawing.Color.White;
            series2.Legend = "LegendRunState";
            series2.Name = "SeriesStateR";
            series2.ToolTip = "#VAL";
            this.chartMachineStateR.Series.Add(series2);
            this.chartMachineStateR.Size = new System.Drawing.Size(200, 156);
            this.chartMachineStateR.TabIndex = 0;
            this.chartMachineStateR.Text = "chart1";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.ForeColor = System.Drawing.Color.White;
            title2.Name = "Title1";
            title2.Text = "设备状态占比";
            this.chartMachineStateR.Titles.Add(title2);
            // 
            // chartUR
            // 
            this.chartUR.BackColor = System.Drawing.Color.Transparent;
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None;
            chartArea3.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea3.AxisX.LabelStyle.Interval = 1D;
            chartArea3.AxisX.LineColor = System.Drawing.Color.White;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea3.AxisY.LineColor = System.Drawing.Color.White;
            chartArea3.AxisY.MajorGrid.Enabled = false;
            chartArea3.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea3.AxisY.Maximum = 100D;
            chartArea3.BackColor = System.Drawing.Color.Transparent;
            chartArea3.Name = "ChartAreaUR";
            this.chartUR.ChartAreas.Add(chartArea3);
            this.chartUR.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "LegendUR";
            this.chartUR.Legends.Add(legend3);
            this.chartUR.Location = new System.Drawing.Point(0, 3);
            this.chartUR.Name = "chartUR";
            this.chartUR.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series3.ChartArea = "ChartAreaUR";
            series3.Color = System.Drawing.Color.Blue;
            series3.Label = "#VAL%";
            series3.LabelForeColor = System.Drawing.Color.White;
            series3.Legend = "LegendUR";
            series3.LegendText = "时间稼动率";
            series3.Name = "SeriesTimeUR";
            series3.ToolTip = "#VAL%";
            series4.ChartArea = "ChartAreaUR";
            series4.Color = System.Drawing.Color.Orange;
            series4.Label = "#VAL%";
            series4.LabelForeColor = System.Drawing.Color.White;
            series4.Legend = "LegendUR";
            series4.LegendText = "性能稼动率";
            series4.Name = "SeriesEfficacyUR";
            series4.ToolTip = "#VAL%";
            series5.ChartArea = "ChartAreaUR";
            series5.Color = System.Drawing.Color.Green;
            series5.Label = "#VAL%";
            series5.LabelForeColor = System.Drawing.Color.White;
            series5.Legend = "LegendUR";
            series5.LegendText = "良品率";
            series5.Name = "SeriesPassR";
            series5.ToolTip = "#VAL%";
            this.chartUR.Series.Add(series3);
            this.chartUR.Series.Add(series4);
            this.chartUR.Series.Add(series5);
            this.chartUR.Size = new System.Drawing.Size(1280, 137);
            this.chartUR.TabIndex = 6;
            this.chartUR.Text = "chart1";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.Transparent;
            this.panelBottom.Controls.Add(this.chartUR);
            this.panelBottom.Controls.Add(this.panelBottomLine);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 460);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1280, 140);
            this.panelBottom.TabIndex = 12;
            // 
            // panelBottomLine
            // 
            this.panelBottomLine.BackColor = System.Drawing.Color.White;
            this.panelBottomLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBottomLine.Location = new System.Drawing.Point(0, 0);
            this.panelBottomLine.Margin = new System.Windows.Forms.Padding(0);
            this.panelBottomLine.Name = "panelBottomLine";
            this.panelBottomLine.Size = new System.Drawing.Size(1280, 3);
            this.panelBottomLine.TabIndex = 0;
            // 
            // timerRoll
            // 
            this.timerRoll.Interval = 5000;
            this.timerRoll.Tick += new System.EventHandler(this.timerRoll_Tick);
            // 
            // FrmMoreCurrWatch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 600);
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMoreCurrWatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "全体设备实时看板";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMoreCurrWatch_FormClosing);
            this.Load += new System.EventHandler(this.FrmMachineWatch_Load);
            this.Resize += new System.EventHandler(this.FrmMoreCurrWatch_Resize);
            panelMachine.ResumeLayout(false);
            this.splitPanel.Panel1.ResumeLayout(false);
            this.splitPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).EndInit();
            this.splitPanel.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTopNode.ResumeLayout(false);
            this.panelTopNode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxNextPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxPrePage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelFill.ResumeLayout(false);
            this.tlpanelMachines.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartOEE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMachineStateR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUR)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.Panel panelFill;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelBottomLine;
        private System.Windows.Forms.Panel panelTopLine;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUR;
        private System.Windows.Forms.DateTimePicker dtpCurrDate;
        private System.Windows.Forms.Label labOEETitle;
        private System.Windows.Forms.Label labProductTitle;
        private System.Windows.Forms.Label labMachineName;
        private System.Windows.Forms.SplitContainer splitPanel;
        private System.Windows.Forms.Label labProductCount;
        private System.Windows.Forms.Label labOEE;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelTopNode;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMachineStateR;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOEE;
        private System.Windows.Forms.Label labLineTitle;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.TableLayoutPanel tlpanelMachines;
        private System.Windows.Forms.Timer timerRoll;
        private System.Windows.Forms.PictureBox pboxPrePage;
        private System.Windows.Forms.PictureBox pboxNextPage;
        private System.Windows.Forms.Label labPageNo;
        private System.Windows.Forms.Label labMachineCount;
        private System.Windows.Forms.Label labLeftTitle;
    }
}
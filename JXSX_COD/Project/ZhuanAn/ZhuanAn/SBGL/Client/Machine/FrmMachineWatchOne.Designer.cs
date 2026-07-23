namespace Machine
{
    partial class FrmMachineWatchOne
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMachineWatchOne));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.panelTop = new System.Windows.Forms.Panel();
            this.dtpCurrDate = new System.Windows.Forms.DateTimePicker();
            this.panelTopLine = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labTitle = new System.Windows.Forms.Label();
            this.panelFill = new System.Windows.Forms.Panel();
            this.labRealProductCount = new System.Windows.Forms.Label();
            this.labLastProductCount = new System.Windows.Forms.Label();
            this.dgvWarnData = new System.Windows.Forms.DataGridView();
            this.dgcWarnDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWarnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWarnSeconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartUR = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labTheoryCT = new System.Windows.Forms.Label();
            this.labTheoryCTName = new System.Windows.Forms.Label();
            this.labOEE = new System.Windows.Forms.Label();
            this.labOEETitle = new System.Windows.Forms.Label();
            this.chartOEE = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvRunTime = new System.Windows.Forms.DataGridView();
            this.dgcItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTotalTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.chartRunState = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labWarnState = new System.Windows.Forms.Label();
            this.labStopState = new System.Windows.Forms.Label();
            this.labRunState = new System.Windows.Forms.Label();
            this.pnWarnState = new System.Windows.Forms.Panel();
            this.pnStopState = new System.Windows.Forms.Panel();
            this.pnRunState = new System.Windows.Forms.Panel();
            this.labTitle1 = new System.Windows.Forms.Label();
            this.panelBottomLine = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOEE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunTime)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRunState)).BeginInit();
            this.SuspendLayout();
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
            this.panelTop.Controls.Add(this.dtpCurrDate);
            this.panelTop.Controls.Add(this.panelTopLine);
            this.panelTop.Controls.Add(this.pictureBox2);
            this.panelTop.Controls.Add(this.labTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(990, 50);
            this.panelTop.TabIndex = 10;
            // 
            // dtpCurrDate
            // 
            this.dtpCurrDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpCurrDate.CustomFormat = "yyyy-MM-dd";
            this.dtpCurrDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCurrDate.Location = new System.Drawing.Point(419, 12);
            this.dtpCurrDate.Name = "dtpCurrDate";
            this.dtpCurrDate.Size = new System.Drawing.Size(106, 25);
            this.dtpCurrDate.TabIndex = 6;
            this.dtpCurrDate.ValueChanged += new System.EventHandler(this.dtpCurrDate_ValueChanged);
            // 
            // panelTopLine
            // 
            this.panelTopLine.BackColor = System.Drawing.Color.White;
            this.panelTopLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTopLine.Location = new System.Drawing.Point(0, 47);
            this.panelTopLine.Margin = new System.Windows.Forms.Padding(0);
            this.panelTopLine.Name = "panelTopLine";
            this.panelTopLine.Size = new System.Drawing.Size(990, 3);
            this.panelTopLine.TabIndex = 5;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(737, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(253, 50);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // labTitle
            // 
            this.labTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labTitle.Font = new System.Drawing.Font("幼圆", 25.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.ForeColor = System.Drawing.Color.White;
            this.labTitle.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.labTitle.Location = new System.Drawing.Point(0, 0);
            this.labTitle.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(990, 50);
            this.labTitle.TabIndex = 2;
            this.labTitle.Text = "设备分析(当日汇总)";
            this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFill
            // 
            this.panelFill.BackColor = System.Drawing.Color.Transparent;
            this.panelFill.Controls.Add(this.labRealProductCount);
            this.panelFill.Controls.Add(this.labLastProductCount);
            this.panelFill.Controls.Add(this.dgvWarnData);
            this.panelFill.Controls.Add(this.chartUR);
            this.panelFill.Controls.Add(this.labTheoryCT);
            this.panelFill.Controls.Add(this.labTheoryCTName);
            this.panelFill.Controls.Add(this.labOEE);
            this.panelFill.Controls.Add(this.labOEETitle);
            this.panelFill.Controls.Add(this.chartOEE);
            this.panelFill.Controls.Add(this.dgvRunTime);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.ForeColor = System.Drawing.Color.White;
            this.panelFill.Location = new System.Drawing.Point(0, 50);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(990, 500);
            this.panelFill.TabIndex = 11;
            // 
            // labRealProductCount
            // 
            this.labRealProductCount.AutoSize = true;
            this.labRealProductCount.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRealProductCount.Location = new System.Drawing.Point(645, 374);
            this.labRealProductCount.Name = "labRealProductCount";
            this.labRealProductCount.Size = new System.Drawing.Size(103, 18);
            this.labRealProductCount.TabIndex = 15;
            this.labRealProductCount.Text = "实际产量：";
            // 
            // labLastProductCount
            // 
            this.labLastProductCount.AutoSize = true;
            this.labLastProductCount.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLastProductCount.Location = new System.Drawing.Point(645, 331);
            this.labLastProductCount.Name = "labLastProductCount";
            this.labLastProductCount.Size = new System.Drawing.Size(141, 18);
            this.labLastProductCount.TabIndex = 14;
            this.labLastProductCount.Text = "最后上报产量：";
            // 
            // dgvWarnData
            // 
            this.dgvWarnData.AllowUserToAddRows = false;
            this.dgvWarnData.AllowUserToDeleteRows = false;
            this.dgvWarnData.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvWarnData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWarnData.ColumnHeadersHeight = 38;
            this.dgvWarnData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvWarnData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcWarnDesc,
            this.dgcWarnCount,
            this.dgcWarnSeconds});
            this.dgvWarnData.Location = new System.Drawing.Point(63, 318);
            this.dgvWarnData.Name = "dgvWarnData";
            this.dgvWarnData.ReadOnly = true;
            this.dgvWarnData.RowHeadersVisible = false;
            this.dgvWarnData.RowHeadersWidth = 51;
            this.dgvWarnData.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvWarnData.RowTemplate.Height = 27;
            this.dgvWarnData.Size = new System.Drawing.Size(347, 150);
            this.dgvWarnData.TabIndex = 12;
            // 
            // dgcWarnDesc
            // 
            this.dgcWarnDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcWarnDesc.DataPropertyName = "WarnDesc";
            this.dgcWarnDesc.HeaderText = "报警名称";
            this.dgcWarnDesc.MinimumWidth = 6;
            this.dgcWarnDesc.Name = "dgcWarnDesc";
            this.dgcWarnDesc.ReadOnly = true;
            // 
            // dgcWarnCount
            // 
            this.dgcWarnCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcWarnCount.DataPropertyName = "Count";
            this.dgcWarnCount.HeaderText = "次数";
            this.dgcWarnCount.MinimumWidth = 6;
            this.dgcWarnCount.Name = "dgcWarnCount";
            this.dgcWarnCount.ReadOnly = true;
            this.dgcWarnCount.Width = 80;
            // 
            // dgcWarnSeconds
            // 
            this.dgcWarnSeconds.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcWarnSeconds.DataPropertyName = "Seconds";
            this.dgcWarnSeconds.HeaderText = "时间";
            this.dgcWarnSeconds.MinimumWidth = 6;
            this.dgcWarnSeconds.Name = "dgcWarnSeconds";
            this.dgcWarnSeconds.ReadOnly = true;
            this.dgcWarnSeconds.Width = 80;
            // 
            // chartUR
            // 
            this.chartUR.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX.Interval = 5D;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartAreaUR";
            this.chartUR.ChartAreas.Add(chartArea1);
            legend1.Name = "LegendUR";
            this.chartUR.Legends.Add(legend1);
            this.chartUR.Location = new System.Drawing.Point(591, 54);
            this.chartUR.Name = "chartUR";
            this.chartUR.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartAreaUR";
            series1.Color = System.Drawing.Color.Blue;
            series1.Label = "#VAL%";
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Legend = "LegendUR";
            series1.LegendText = "时间稼动率";
            series1.Name = "SeriesTimeUR";
            series1.ToolTip = "#VAL%";
            series2.ChartArea = "ChartAreaUR";
            series2.Color = System.Drawing.Color.Orange;
            series2.Label = "#VAL%";
            series2.LabelForeColor = System.Drawing.Color.White;
            series2.Legend = "LegendUR";
            series2.LegendText = "性能稼动率";
            series2.Name = "SeriesEfficacyUR";
            series2.ToolTip = "#VAL%";
            series3.ChartArea = "ChartAreaUR";
            series3.Color = System.Drawing.Color.Green;
            series3.Label = "#VAL%";
            series3.LabelForeColor = System.Drawing.Color.White;
            series3.Legend = "LegendUR";
            series3.LegendText = "良品率";
            series3.Name = "SeriesPassR";
            series3.ToolTip = "#VAL%";
            this.chartUR.Series.Add(series1);
            this.chartUR.Series.Add(series2);
            this.chartUR.Series.Add(series3);
            this.chartUR.Size = new System.Drawing.Size(387, 217);
            this.chartUR.TabIndex = 6;
            this.chartUR.Text = "chart1";
            // 
            // labTheoryCT
            // 
            this.labTheoryCT.AutoSize = true;
            this.labTheoryCT.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTheoryCT.Location = new System.Drawing.Point(267, 17);
            this.labTheoryCT.Name = "labTheoryCT";
            this.labTheoryCT.Size = new System.Drawing.Size(41, 20);
            this.labTheoryCT.TabIndex = 5;
            this.labTheoryCT.Text = "0秒";
            // 
            // labTheoryCTName
            // 
            this.labTheoryCTName.AutoSize = true;
            this.labTheoryCTName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTheoryCTName.Location = new System.Drawing.Point(85, 17);
            this.labTheoryCTName.Name = "labTheoryCTName";
            this.labTheoryCTName.Size = new System.Drawing.Size(198, 20);
            this.labTheoryCTName.TabIndex = 4;
            this.labTheoryCTName.Text = "设备计划节拍时间：";
            // 
            // labOEE
            // 
            this.labOEE.AutoSize = true;
            this.labOEE.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOEE.Location = new System.Drawing.Point(494, 225);
            this.labOEE.Name = "labOEE";
            this.labOEE.Size = new System.Drawing.Size(28, 18);
            this.labOEE.TabIndex = 3;
            this.labOEE.Text = "0%";
            // 
            // labOEETitle
            // 
            this.labOEETitle.AutoSize = true;
            this.labOEETitle.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOEETitle.Location = new System.Drawing.Point(445, 225);
            this.labOEETitle.Name = "labOEETitle";
            this.labOEETitle.Size = new System.Drawing.Size(57, 18);
            this.labOEETitle.TabIndex = 2;
            this.labOEETitle.Text = "OEE：";
            // 
            // chartOEE
            // 
            this.chartOEE.BackColor = System.Drawing.Color.Transparent;
            this.chartOEE.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY.Maximum = 100D;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartAreaOEE";
            this.chartOEE.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "LegendOEE";
            this.chartOEE.Legends.Add(legend2);
            this.chartOEE.Location = new System.Drawing.Point(433, 54);
            this.chartOEE.Name = "chartOEE";
            this.chartOEE.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartOEE.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Cyan,
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))))};
            series4.ChartArea = "ChartAreaOEE";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series4.Color = System.Drawing.Color.Red;
            series4.Legend = "LegendOEE";
            series4.Name = "SeriesOEE";
            series4.ToolTip = "#PERCENT";
            this.chartOEE.Series.Add(series4);
            this.chartOEE.Size = new System.Drawing.Size(152, 149);
            this.chartOEE.TabIndex = 1;
            this.chartOEE.Text = "chart1";
            // 
            // dgvRunTime
            // 
            this.dgvRunTime.AllowUserToAddRows = false;
            this.dgvRunTime.AllowUserToDeleteRows = false;
            this.dgvRunTime.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRunTime.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvRunTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRunTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRunTime.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcItem,
            this.dgcTotalTime,
            this.dgcPercent});
            this.dgvRunTime.GridColor = System.Drawing.Color.White;
            this.dgvRunTime.Location = new System.Drawing.Point(63, 54);
            this.dgvRunTime.Name = "dgvRunTime";
            this.dgvRunTime.ReadOnly = true;
            this.dgvRunTime.RowHeadersVisible = false;
            this.dgvRunTime.RowHeadersWidth = 51;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvRunTime.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRunTime.RowTemplate.Height = 27;
            this.dgvRunTime.Size = new System.Drawing.Size(347, 217);
            this.dgvRunTime.TabIndex = 0;
            this.dgvRunTime.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRunTime_CellFormatting);
            // 
            // dgcItem
            // 
            this.dgcItem.DataPropertyName = "Item";
            this.dgcItem.HeaderText = "项目";
            this.dgcItem.MinimumWidth = 6;
            this.dgcItem.Name = "dgcItem";
            this.dgcItem.ReadOnly = true;
            // 
            // dgcTotalTime
            // 
            this.dgcTotalTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcTotalTime.DataPropertyName = "TotalTime";
            this.dgcTotalTime.HeaderText = "时间(分)";
            this.dgcTotalTime.MinimumWidth = 6;
            this.dgcTotalTime.Name = "dgcTotalTime";
            this.dgcTotalTime.ReadOnly = true;
            this.dgcTotalTime.Width = 80;
            // 
            // dgcPercent
            // 
            this.dgcPercent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcPercent.DataPropertyName = "Percent";
            this.dgcPercent.HeaderText = "比例";
            this.dgcPercent.MinimumWidth = 6;
            this.dgcPercent.Name = "dgcPercent";
            this.dgcPercent.ReadOnly = true;
            this.dgcPercent.Width = 80;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.Transparent;
            this.panelBottom.Controls.Add(this.chartRunState);
            this.panelBottom.Controls.Add(this.labWarnState);
            this.panelBottom.Controls.Add(this.labStopState);
            this.panelBottom.Controls.Add(this.labRunState);
            this.panelBottom.Controls.Add(this.pnWarnState);
            this.panelBottom.Controls.Add(this.pnStopState);
            this.panelBottom.Controls.Add(this.pnRunState);
            this.panelBottom.Controls.Add(this.labTitle1);
            this.panelBottom.Controls.Add(this.panelBottomLine);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 550);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(990, 103);
            this.panelBottom.TabIndex = 12;
            // 
            // chartRunState
            // 
            this.chartRunState.BackColor = System.Drawing.Color.Transparent;
            chartArea3.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisY.Interval = 2D;
            chartArea3.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea3.AxisY.LabelStyle.Format = "HH:mm";
            chartArea3.AxisY.LineColor = System.Drawing.Color.Transparent;
            chartArea3.AxisY.MajorGrid.Enabled = false;
            chartArea3.AxisY.Maximum = 86400D;
            chartArea3.AxisY.Minimum = 0D;
            chartArea3.BackColor = System.Drawing.Color.Transparent;
            chartArea3.Name = "ChartAreaRunState";
            this.chartRunState.ChartAreas.Add(chartArea3);
            legend3.Alignment = System.Drawing.StringAlignment.Center;
            legend3.BackColor = System.Drawing.Color.Transparent;
            legend3.DockedToChartArea = "ChartAreaRunState";
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend3.Enabled = false;
            legend3.IsDockedInsideChartArea = false;
            legend3.Name = "LegendRunState";
            this.chartRunState.Legends.Add(legend3);
            this.chartRunState.Location = new System.Drawing.Point(12, 45);
            this.chartRunState.Name = "chartRunState";
            series5.ChartArea = "ChartAreaRunState";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series5.Legend = "LegendRunState";
            series5.Name = "Series1";
            series5.YValuesPerPoint = 2;
            series6.ChartArea = "ChartAreaRunState";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series6.Legend = "LegendRunState";
            series6.Name = "Series2";
            series7.ChartArea = "ChartAreaRunState";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series7.Legend = "LegendRunState";
            series7.Name = "Series3";
            this.chartRunState.Series.Add(series5);
            this.chartRunState.Series.Add(series6);
            this.chartRunState.Series.Add(series7);
            this.chartRunState.Size = new System.Drawing.Size(966, 49);
            this.chartRunState.TabIndex = 11;
            this.chartRunState.Text = "chart1";
            // 
            // labWarnState
            // 
            this.labWarnState.AutoSize = true;
            this.labWarnState.ForeColor = System.Drawing.Color.White;
            this.labWarnState.Location = new System.Drawing.Point(567, 22);
            this.labWarnState.Name = "labWarnState";
            this.labWarnState.Size = new System.Drawing.Size(52, 15);
            this.labWarnState.TabIndex = 7;
            this.labWarnState.Text = "报警中";
            // 
            // labStopState
            // 
            this.labStopState.AutoSize = true;
            this.labStopState.ForeColor = System.Drawing.Color.White;
            this.labStopState.Location = new System.Drawing.Point(455, 22);
            this.labStopState.Name = "labStopState";
            this.labStopState.Size = new System.Drawing.Size(52, 15);
            this.labStopState.TabIndex = 6;
            this.labStopState.Text = "停止中";
            // 
            // labRunState
            // 
            this.labRunState.AutoSize = true;
            this.labRunState.ForeColor = System.Drawing.Color.White;
            this.labRunState.Location = new System.Drawing.Point(343, 22);
            this.labRunState.Name = "labRunState";
            this.labRunState.Size = new System.Drawing.Size(52, 15);
            this.labRunState.TabIndex = 5;
            this.labRunState.Text = "开动中";
            // 
            // pnWarnState
            // 
            this.pnWarnState.BackColor = System.Drawing.Color.Orange;
            this.pnWarnState.Location = new System.Drawing.Point(542, 19);
            this.pnWarnState.Name = "pnWarnState";
            this.pnWarnState.Size = new System.Drawing.Size(20, 20);
            this.pnWarnState.TabIndex = 4;
            // 
            // pnStopState
            // 
            this.pnStopState.BackColor = System.Drawing.Color.Red;
            this.pnStopState.Location = new System.Drawing.Point(430, 19);
            this.pnStopState.Name = "pnStopState";
            this.pnStopState.Size = new System.Drawing.Size(20, 20);
            this.pnStopState.TabIndex = 4;
            // 
            // pnRunState
            // 
            this.pnRunState.BackColor = System.Drawing.Color.Green;
            this.pnRunState.Location = new System.Drawing.Point(318, 19);
            this.pnRunState.Name = "pnRunState";
            this.pnRunState.Size = new System.Drawing.Size(20, 20);
            this.pnRunState.TabIndex = 3;
            // 
            // labTitle1
            // 
            this.labTitle1.AutoSize = true;
            this.labTitle1.Font = new System.Drawing.Font("幼圆", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle1.ForeColor = System.Drawing.Color.White;
            this.labTitle1.Location = new System.Drawing.Point(80, 14);
            this.labTitle1.Name = "labTitle1";
            this.labTitle1.Size = new System.Drawing.Size(228, 25);
            this.labTitle1.TabIndex = 1;
            this.labTitle1.Text = "设备一天运行状态";
            // 
            // panelBottomLine
            // 
            this.panelBottomLine.BackColor = System.Drawing.Color.White;
            this.panelBottomLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBottomLine.Location = new System.Drawing.Point(0, 0);
            this.panelBottomLine.Margin = new System.Windows.Forms.Padding(0);
            this.panelBottomLine.Name = "panelBottomLine";
            this.panelBottomLine.Size = new System.Drawing.Size(990, 3);
            this.panelBottomLine.TabIndex = 0;
            // 
            // FrmMachineWatchOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(990, 653);
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmMachineWatchOne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单个设备当日看板";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSingleCurrWatch_FormClosing);
            this.Load += new System.EventHandler(this.FrmMachineWatchOne_Load);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelFill.ResumeLayout(false);
            this.panelFill.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOEE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunTime)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRunState)).EndInit();
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
        private System.Windows.Forms.Label labTitle1;
        private System.Windows.Forms.Panel pnRunState;
        private System.Windows.Forms.Panel pnWarnState;
        private System.Windows.Forms.Panel pnStopState;
        private System.Windows.Forms.Label labWarnState;
        private System.Windows.Forms.Label labStopState;
        private System.Windows.Forms.Label labRunState;
        private System.Windows.Forms.DataGridView dgvRunTime;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOEE;
        private System.Windows.Forms.Label labOEETitle;
        private System.Windows.Forms.Label labOEE;
        private System.Windows.Forms.Label labTheoryCT;
        private System.Windows.Forms.Label labTheoryCTName;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUR;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRunState;
        private System.Windows.Forms.DateTimePicker dtpCurrDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTotalTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPercent;
        private System.Windows.Forms.DataGridView dgvWarnData;
        private System.Windows.Forms.Label labRealProductCount;
        private System.Windows.Forms.Label labLastProductCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWarnDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWarnCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWarnSeconds;
    }
}
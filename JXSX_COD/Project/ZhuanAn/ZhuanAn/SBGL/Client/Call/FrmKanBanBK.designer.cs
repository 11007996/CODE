namespace Call
{
    partial class FrmKanBanBK
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label labTotalTimes;
            System.Windows.Forms.Label labUntreatedQty;
            System.Windows.Forms.Label labHandleQty;
            System.Windows.Forms.Label labHandlerNo;
            System.Windows.Forms.PictureBox pbHandlePic;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKanBan));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.panelHandlerInfo = new System.Windows.Forms.Panel();
            this.pbBackgroudImg = new System.Windows.Forms.PictureBox();
            this.timerRefreshData = new System.Windows.Forms.Timer(this.components);
            this.labKanBanTitle = new System.Windows.Forms.Label();
            this.labCurrTime = new System.Windows.Forms.Label();
            this.tlpUserInfo = new System.Windows.Forms.TableLayoutPanel();
            this.dgvError = new System.Windows.Forms.DataGridView();
            this.chartError = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timerCurrTime = new System.Windows.Forms.Timer(this.components);
            this.panelTop = new System.Windows.Forms.Panel();
            this.labArea = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.scFill = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgcFormatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTargetObj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcComeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWaitedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHelperName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCallReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandleTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            labTotalTimes = new System.Windows.Forms.Label();
            labUntreatedQty = new System.Windows.Forms.Label();
            labHandleQty = new System.Windows.Forms.Label();
            labHandlerNo = new System.Windows.Forms.Label();
            pbHandlePic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pbHandlePic)).BeginInit();
            this.panelHandlerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackgroudImg)).BeginInit();
            this.tlpUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scFill)).BeginInit();
            this.scFill.Panel1.SuspendLayout();
            this.scFill.Panel2.SuspendLayout();
            this.scFill.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labTotalTimes
            // 
            labTotalTimes.AutoSize = true;
            labTotalTimes.Font = new System.Drawing.Font("宋体", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            labTotalTimes.ForeColor = System.Drawing.Color.White;
            labTotalTimes.Location = new System.Drawing.Point(0, 102);
            labTotalTimes.Name = "labTotalTimes";
            labTotalTimes.Size = new System.Drawing.Size(33, 10);
            labTotalTimes.TabIndex = 4;
            labTotalTimes.Text = "时长:";
            // 
            // labUntreatedQty
            // 
            labUntreatedQty.AutoSize = true;
            labUntreatedQty.Font = new System.Drawing.Font("宋体", 6F, System.Drawing.FontStyle.Bold);
            labUntreatedQty.ForeColor = System.Drawing.Color.White;
            labUntreatedQty.Location = new System.Drawing.Point(0, 111);
            labUntreatedQty.Name = "labUntreatedQty";
            labUntreatedQty.Size = new System.Drawing.Size(33, 10);
            labUntreatedQty.TabIndex = 3;
            labUntreatedQty.Text = "未结:";
            // 
            // labHandleQty
            // 
            labHandleQty.AutoSize = true;
            labHandleQty.Font = new System.Drawing.Font("宋体", 6F, System.Drawing.FontStyle.Bold);
            labHandleQty.ForeColor = System.Drawing.Color.White;
            labHandleQty.Location = new System.Drawing.Point(0, 93);
            labHandleQty.Name = "labHandleQty";
            labHandleQty.Size = new System.Drawing.Size(33, 10);
            labHandleQty.TabIndex = 2;
            labHandleQty.Text = "维护:";
            // 
            // labHandlerNo
            // 
            labHandlerNo.AutoSize = true;
            labHandlerNo.Font = new System.Drawing.Font("宋体", 6F, System.Drawing.FontStyle.Bold);
            labHandlerNo.ForeColor = System.Drawing.Color.White;
            labHandlerNo.Location = new System.Drawing.Point(0, 84);
            labHandlerNo.Name = "labHandlerNo";
            labHandlerNo.Size = new System.Drawing.Size(33, 10);
            labHandlerNo.TabIndex = 1;
            labHandlerNo.Text = "工号:";
            // 
            // pbHandlePic
            // 
            pbHandlePic.Dock = System.Windows.Forms.DockStyle.Top;
            pbHandlePic.Location = new System.Drawing.Point(0, 0);
            pbHandlePic.Margin = new System.Windows.Forms.Padding(0);
            pbHandlePic.Name = "pbHandlePic";
            pbHandlePic.Size = new System.Drawing.Size(79, 84);
            pbHandlePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pbHandlePic.TabIndex = 0;
            pbHandlePic.TabStop = false;
            // 
            // panelHandlerInfo
            // 
            this.panelHandlerInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panelHandlerInfo.Controls.Add(labTotalTimes);
            this.panelHandlerInfo.Controls.Add(labUntreatedQty);
            this.panelHandlerInfo.Controls.Add(labHandleQty);
            this.panelHandlerInfo.Controls.Add(labHandlerNo);
            this.panelHandlerInfo.Controls.Add(pbHandlePic);
            this.panelHandlerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHandlerInfo.Location = new System.Drawing.Point(0, 0);
            this.panelHandlerInfo.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.panelHandlerInfo.Name = "panelHandlerInfo";
            this.panelHandlerInfo.Size = new System.Drawing.Size(79, 118);
            this.panelHandlerInfo.TabIndex = 0;
            // 
            // pbBackgroudImg
            // 
            this.pbBackgroudImg.BackColor = System.Drawing.Color.Transparent;
            this.pbBackgroudImg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbBackgroudImg.BackgroundImage")));
            this.pbBackgroudImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbBackgroudImg.Location = new System.Drawing.Point(0, 50);
            this.pbBackgroudImg.Margin = new System.Windows.Forms.Padding(0);
            this.pbBackgroudImg.Name = "pbBackgroudImg";
            this.pbBackgroudImg.Size = new System.Drawing.Size(1280, 25);
            this.pbBackgroudImg.TabIndex = 1;
            this.pbBackgroudImg.TabStop = false;
            // 
            // timerRefreshData
            // 
            this.timerRefreshData.Enabled = true;
            this.timerRefreshData.Interval = 10000;
            this.timerRefreshData.Tick += new System.EventHandler(this.timerRefreshData_Tick);
            // 
            // labKanBanTitle
            // 
            this.labKanBanTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            this.labKanBanTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labKanBanTitle.Font = new System.Drawing.Font("幼圆", 25.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labKanBanTitle.ForeColor = System.Drawing.Color.White;
            this.labKanBanTitle.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.labKanBanTitle.Location = new System.Drawing.Point(0, 0);
            this.labKanBanTitle.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.labKanBanTitle.Name = "labKanBanTitle";
            this.labKanBanTitle.Size = new System.Drawing.Size(1280, 50);
            this.labKanBanTitle.TabIndex = 2;
            this.labKanBanTitle.Text = "设备管理系统";
            this.labKanBanTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCurrTime
            // 
            this.labCurrTime.AutoSize = true;
            this.labCurrTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            this.labCurrTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCurrTime.ForeColor = System.Drawing.Color.White;
            this.labCurrTime.Location = new System.Drawing.Point(380, 51);
            this.labCurrTime.Name = "labCurrTime";
            this.labCurrTime.Size = new System.Drawing.Size(74, 20);
            this.labCurrTime.TabIndex = 3;
            this.labCurrTime.Text = "2022年";
            this.labCurrTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpHandlerInfo
            // 
            this.tlpUserInfo.BackColor = System.Drawing.Color.Transparent;
            this.tlpUserInfo.BackgroundImage = global::Call.Properties.Resources.bg_repeat_x;
            this.tlpUserInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlpUserInfo.ColumnCount = 4;
            this.tlpUserInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpUserInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpUserInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpUserInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpUserInfo.Controls.Add(this.panelHandlerInfo, 0, 0);
            this.tlpUserInfo.Location = new System.Drawing.Point(10, 0);
            this.tlpUserInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUserInfo.Name = "tlpHandlerInfo";
            this.tlpUserInfo.RowCount = 4;
            this.tlpUserInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpUserInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpUserInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpUserInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpUserInfo.Size = new System.Drawing.Size(324, 480);
            this.tlpUserInfo.TabIndex = 4;
            // 
            // dgvError
            // 
            this.dgvError.AllowUserToAddRows = false;
            this.dgvError.AllowUserToDeleteRows = false;
            this.dgvError.AllowUserToResizeColumns = false;
            this.dgvError.AllowUserToResizeRows = false;
            this.dgvError.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvError.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(139)))), ((int)(((byte)(216)))));
            this.dgvError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvError.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvError.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvError.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvError.ColumnHeadersHeight = 40;
            this.dgvError.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcFormatColumn,
            this.dgcLine,
            this.dgcTargetObj,
            this.dgcDept,
            this.dgcStartTime,
            this.dgcComeTime,
            this.dgcWaitedTime,
            this.dgcHandlerName,
            this.dgcHelperName,
            this.dgcStatus,
            this.dgcCallReason,
            this.dgcHandleTime});
            this.dgvError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvError.Enabled = false;
            this.dgvError.EnableHeadersVisualStyles = false;
            this.dgvError.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(139)))), ((int)(((byte)(216)))));
            this.dgvError.Location = new System.Drawing.Point(0, 0);
            this.dgvError.Name = "dgvError";
            this.dgvError.ReadOnly = true;
            this.dgvError.RowHeadersVisible = false;
            this.dgvError.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(84)))), ((int)(((byte)(220)))));
            this.dgvError.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.dgvError.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvError.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvError.RowTemplate.DividerHeight = 1;
            this.dgvError.RowTemplate.Height = 40;
            this.dgvError.RowTemplate.ReadOnly = true;
            this.dgvError.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvError.Size = new System.Drawing.Size(933, 354);
            this.dgvError.TabIndex = 8;
            this.dgvError.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvError_CellFormatting);
            // 
            // chartError
            // 
            this.chartError.BackColor = System.Drawing.Color.Transparent;
            this.chartError.BackImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chartError.BorderlineColor = System.Drawing.Color.Black;
            this.chartError.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chartError.ChartAreas.Add(chartArea1);
            this.chartError.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Name = "Legend1";
            this.chartError.Legends.Add(legend1);
            this.chartError.Location = new System.Drawing.Point(0, 354);
            this.chartError.Name = "chartError";
            series1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            series1.BackImageTransparentColor = System.Drawing.Color.Transparent;
            series1.BackSecondaryColor = System.Drawing.Color.Transparent;
            series1.BorderColor = System.Drawing.Color.Transparent;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Transparent;
            series1.LabelBackColor = System.Drawing.Color.Transparent;
            series1.LabelBorderColor = System.Drawing.Color.Transparent;
            series1.LabelForeColor = System.Drawing.Color.Transparent;
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = System.Drawing.Color.Transparent;
            series1.MarkerColor = System.Drawing.Color.Transparent;
            series1.MarkerImageTransparentColor = System.Drawing.Color.Transparent;
            series1.Name = " ";
            series1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ShadowColor = System.Drawing.Color.Transparent;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "上周";
            series2.YValuesPerPoint = 4;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "上上周";
            this.chartError.Series.Add(series1);
            this.chartError.Series.Add(series2);
            this.chartError.Series.Add(series3);
            this.chartError.Size = new System.Drawing.Size(933, 148);
            this.chartError.TabIndex = 6;
            this.chartError.Text = "chart1";
            // 
            // timerCurrTime
            // 
            this.timerCurrTime.Interval = 1000;
            this.timerCurrTime.Tick += new System.EventHandler(this.timerCurrTime_Tick);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Transparent;
            this.panelTop.Controls.Add(this.labArea);
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.labCurrTime);
            this.panelTop.Controls.Add(this.labKanBanTitle);
            this.panelTop.Controls.Add(this.pbBackgroudImg);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1280, 98);
            this.panelTop.TabIndex = 9;
            // 
            // labArea
            // 
            this.labArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labArea.AutoSize = true;
            this.labArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            this.labArea.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labArea.ForeColor = System.Drawing.Color.White;
            this.labArea.Location = new System.Drawing.Point(1169, 10);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(99, 40);
            this.labArea.TabIndex = 5;
            this.labArea.Text = "全区";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(44)))), ((int)(((byte)(89)))));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(253, 50);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // scFill
            // 
            this.scFill.BackColor = System.Drawing.Color.Transparent;
            this.scFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFill.Location = new System.Drawing.Point(0, 98);
            this.scFill.Name = "scFill";
            // 
            // scFill.Panel1
            // 
            this.scFill.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.scFill.Panel1.Controls.Add(this.panel1);
            // 
            // scFill.Panel2
            // 
            this.scFill.Panel2.Controls.Add(this.tlpUserInfo);
            this.scFill.Size = new System.Drawing.Size(1280, 502);
            this.scFill.SplitterDistance = 933;
            this.scFill.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvError);
            this.panel1.Controls.Add(this.chartError);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 502);
            this.panel1.TabIndex = 9;
            // 
            // dgcFormatColumn
            // 
            this.dgcFormatColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFormatColumn.DataPropertyName = "FormatColumn";
            this.dgcFormatColumn.HeaderText = "格式化";
            this.dgcFormatColumn.MinimumWidth = 2;
            this.dgcFormatColumn.Name = "dgcFormatColumn";
            this.dgcFormatColumn.ReadOnly = true;
            this.dgcFormatColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcFormatColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcFormatColumn.Width = 2;
            // 
            // dgcLine
            // 
            this.dgcLine.DataPropertyName = "Line";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcLine.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgcLine.FillWeight = 8F;
            this.dgcLine.HeaderText = "产线";
            this.dgcLine.Name = "dgcLine";
            this.dgcLine.ReadOnly = true;
            this.dgcLine.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcTargetObj
            // 
            this.dgcTargetObj.DataPropertyName = "Machine";
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.dgcTargetObj.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgcTargetObj.FillWeight = 16.63452F;
            this.dgcTargetObj.HeaderText = "机台";
            this.dgcTargetObj.Name = "dgcTargetObj";
            this.dgcTargetObj.ReadOnly = true;
            this.dgcTargetObj.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcTargetObj.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcDept
            // 
            this.dgcDept.DataPropertyName = "Dept";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcDept.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgcDept.FillWeight = 5.908629F;
            this.dgcDept.HeaderText = "部门";
            this.dgcDept.Name = "dgcDept";
            this.dgcDept.ReadOnly = true;
            this.dgcDept.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcStartTime
            // 
            this.dgcStartTime.DataPropertyName = "StartTime";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcStartTime.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgcStartTime.FillWeight = 7.847715F;
            this.dgcStartTime.HeaderText = "开始时间";
            this.dgcStartTime.Name = "dgcStartTime";
            this.dgcStartTime.ReadOnly = true;
            this.dgcStartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcComeTime
            // 
            this.dgcComeTime.DataPropertyName = "ComeTime";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcComeTime.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgcComeTime.FillWeight = 7.847715F;
            this.dgcComeTime.HeaderText = "到位时间";
            this.dgcComeTime.Name = "dgcComeTime";
            this.dgcComeTime.ReadOnly = true;
            this.dgcComeTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcWaitedTime
            // 
            this.dgcWaitedTime.DataPropertyName = "WaitedTime";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcWaitedTime.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgcWaitedTime.FillWeight = 7.878172F;
            this.dgcWaitedTime.HeaderText = "等待时长";
            this.dgcWaitedTime.Name = "dgcWaitedTime";
            this.dgcWaitedTime.ReadOnly = true;
            this.dgcWaitedTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcHandlerName
            // 
            this.dgcHandlerName.DataPropertyName = "HandlerName";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcHandlerName.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgcHandlerName.FillWeight = 7.878172F;
            this.dgcHandlerName.HeaderText = "对应者";
            this.dgcHandlerName.Name = "dgcHandlerName";
            this.dgcHandlerName.ReadOnly = true;
            this.dgcHandlerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcHelperName
            // 
            this.dgcHelperName.DataPropertyName = "HelperName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcHelperName.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgcHelperName.FillWeight = 7.878172F;
            this.dgcHelperName.HeaderText = "支援者";
            this.dgcHelperName.Name = "dgcHelperName";
            this.dgcHelperName.ReadOnly = true;
            this.dgcHelperName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcStatus
            // 
            this.dgcStatus.DataPropertyName = "Status";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcStatus.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgcStatus.FillWeight = 7.878172F;
            this.dgcStatus.HeaderText = "状态";
            this.dgcStatus.Name = "dgcStatus";
            this.dgcStatus.ReadOnly = true;
            this.dgcStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcCallReason
            // 
            this.dgcCallReason.DataPropertyName = "CallReason";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcCallReason.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgcCallReason.FillWeight = 9.847715F;
            this.dgcCallReason.HeaderText = "呼叫原因";
            this.dgcCallReason.Name = "dgcCallReason";
            this.dgcCallReason.ReadOnly = true;
            this.dgcCallReason.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcHandleTime
            // 
            this.dgcHandleTime.DataPropertyName = "HandleTime";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcHandleTime.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgcHandleTime.FillWeight = 7.878172F;
            this.dgcHandleTime.HeaderText = "用时";
            this.dgcHandleTime.Name = "dgcHandleTime";
            this.dgcHandleTime.ReadOnly = true;
            this.dgcHandleTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmKanBan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Call.Properties.Resources.bg_repeat_x;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 600);
            this.Controls.Add(this.scFill);
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmKanBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "1280*600";
            this.Text = "呼叫看板";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmKanBan_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmKanBan_FormClosed);
            this.Load += new System.EventHandler(this.FrmKanBan_Load);
            this.Resize += new System.EventHandler(this.FrmKanBan_Resize);
            ((System.ComponentModel.ISupportInitialize)(pbHandlePic)).EndInit();
            this.panelHandlerInfo.ResumeLayout(false);
            this.panelHandlerInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackgroudImg)).EndInit();
            this.tlpUserInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.scFill.Panel1.ResumeLayout(false);
            this.scFill.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scFill)).EndInit();
            this.scFill.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBackgroudImg;
        private System.Windows.Forms.Timer timerRefreshData;
        private System.Windows.Forms.Label labKanBanTitle;
        private System.Windows.Forms.Label labCurrTime;
        private System.Windows.Forms.TableLayoutPanel tlpUserInfo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartError;
        private System.Windows.Forms.Timer timerCurrTime;
        private System.Windows.Forms.DataGridView dgvError;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.SplitContainer scFill;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.Panel panelHandlerInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFormatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTargetObj;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcComeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWaitedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHelperName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCallReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandleTime;
    }
}


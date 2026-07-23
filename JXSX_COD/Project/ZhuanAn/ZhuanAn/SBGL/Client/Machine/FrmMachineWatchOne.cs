using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class FrmMachineWatchOne : Form
    {
        //设备信息
        private DataTable Machines;

        //统计时间
        private DateTime StatStartTime;//统计开时时间
        private DateTime StatEndTime;//统计结束时间
        private DataTable PlanRestTimes = null;//计划停机时间
        private String StartTimeMark = "07:30:00";//开时时间标记

        //统计分析数据
        private DataTable WatchData = new DataTable();//监听报告的数据
        private DataTable TimeDT;//时间分析统计表
        private DataTable WarnCodeDT;//报警代码参照表
        //private decimal OEE = 0;//OEE(综合稼动率)
        //private decimal TimeUR = 0;//时间稼动率
        //private decimal EfficacyUR = 0;//性能稼动率
        //private decimal PassR = 0;//良品率

        public FrmMachineWatchOne(int machineCode,  DateTime date)
        {
            InitializeComponent();
            dgvWarnData.AutoGenerateColumns = false;

            StatStartTime = Convert.ToDateTime(date.Date.ToString("yyyy-MM-dd") + " " + StartTimeMark);
            StatEndTime = StatStartTime.AddDays(1);

            //设备信息
            string sql = string.Format(@"SELECT  MachineCode,MachineName+'('+CONVERT(varchar, MachineNo)  +')' AS MachineName,Line, TheoryCT,
                                       0.0 AS [OEE],0 AS [ErrorCount],0 AS[ProductCount],0 AS [LastProductCount],0 AS [FailedCount],0 AS [RunState],
                                       0 AS [WarnState],0.0 AS [TimeUR],0.0 AS [EfficacyUR],0.0 AS [PassR] FROM M_Machine_T  
                                        WHERE MachineCode='{0}';", machineCode);
            Machines = DBUtil.GetDataTable(sql);
            if (Machines != null && Machines.Rows.Count > 0)
            {
                labTheoryCT.Text = Machines.Rows[0]["TheoryCT"].ToString() + "CT/S";
                this.Text += "【" + Machines.Rows[0]["MachineName"].ToString() + "】";
            }
            //报警代码
            sql = string.Format("SELECT *,0 Count,0 Seconds FROM M_MachineWarnCode_T  WHERE MachineCode='{0}' ;", machineCode);
            WarnCodeDT = DBUtil.GetDataTable(sql);


            //WarnDT = WarnCodeDT.Clone();
            //WarnDT.Columns.Add("Count");
            //WarnDT.Columns.Add("Seconds");
        }


        #region 窗体加载事件
        private void FrmMachineWatchOne_Load(object sender, EventArgs e)
        {
            try
            {
                //屏幕分辨率(全屏)
                //System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                //int h = rect.Height; //高（像素）
                //int w = rect.Width;  //宽（像素）
                //this.Size = new Size(w, h);

                //计划停机时间
                string sql = "SELECT * FROM M_PlanTime_T";
                PlanRestTimes = DBUtil.GetDataTable(sql);

                //时间分析表
                TimeDT = new DataTable();
                TimeDT.Columns.Add("Item");
                TimeDT.Columns.Add("TotalTime");
                TimeDT.Columns.Add("Percent");

                dtpCurrDate.Text = StatStartTime.ToString("yyyy-MM-dd");
                dgvRunTime.ClearSelection();//清除选中状态
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMachineWatchOne), null, ex);
                return;
            }
        }
        #endregion

        #region  解决刷新时groupbox闪烁
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        #region 刷新数据
        private void RefreshWatchData()
        {
            try
            {
                //实际统计结束时间
                DateTime realStatEndTime = StatEndTime > TimeUtil.Now ? TimeUtil.Now : StatEndTime;
                string sql = string.Format(@"WITH cte AS (
	                                        SELECT *,
		                                        LAG ( RunState,1,0 ) OVER ( ORDER BY  CreateTime  ) AS PreRunState,
		                                        LAG ( WarnState,1,0 ) OVER ( ORDER BY  CreateTime  ) AS PreWarnState,
                                                LEAD ( RunState,1,0 ) OVER ( ORDER BY  CreateTime  ) AS NextRunState,
                                                LEAD ( ProductCount,1,0 ) OVER ( ORDER BY  CreateTime  ) AS NextProductCount,
                                                LEAD ( FailedCount,1,0 ) OVER (PARTITION BY MachineCode  ORDER BY  CreateTime  ) AS NextFailedCount
	                                        FROM
		                                        dbo.M_MachineReport_T WHERE MachineCode='{0}'  AND CreateTime>='{1}' AND CreateTime<='{2}'
	                                    )
                                        SELECT
	                                        *,
	                                        LEAD ( CreateTime,1,NULL ) OVER ( ORDER BY  CreateTime  ) AS EndTime
                                        FROM
	                                        cte WHERE RunState!=PreRunState OR WarnState!=PreWarnState OR  NextRunState=0  OR NextProductCount<ProductCount OR NextFailedCount<FailedCount ORDER BY CreateTime asc", Machines.Rows[0]["MachineCode"], StatStartTime, realStatEndTime);
                WatchData = DBUtil.GetDataTable(sql);
                ResetTimeDTRow();//重置时间占比表
                ParseWatchData(Machines.Rows[0], WatchData.Select());
                RefreshUtilizeRate();
                RefreshRunStateChart();
            }
            catch (Exception e)
            {
                LogHelper.Error(typeof(FrmMachineWatchOne), e.Message);
            }

        }


        //数据解析
        private void ParseWatchData(DataRow machine, DataRow[] watchData)
        {
            //稼动率
            decimal oee = 0;//OEE(综合稼动率)
            decimal timeUR = 0;//时间稼动率
            decimal efficacyUR = 0;//性能稼动率
            decimal passR = 0;//良品率
            //基础数据:产量、不良
            int productCount = 0;//产量（实际）
            int failedCount = 0;//不良
            int lastProductCount = 0;//最后上报的产量
            //基础数据:时间
            int totalSeconds = 0;//开机时间(秒)
            int runSeconds = 0;//运行时间
            int planRestSeconds = 0;//计划停机时间(秒)
            int failedSeconds = 0;////计划外停机时间（秒）=(小停顿时间+故障时间)
            int stopSeconds = 0;//小停顿时间(运行状态1，报警状态1)
            int warnSeconds = 0;//故障时间(运行状态1，报警状态为2)
            //基础数据:故障次数、状态
            int errorCount = 0;//故障次数
            int runState = 0;//设备最后的运行状态，0:未监听，1：停机中，2：运行中
            int wartnState = 0;//设备最后的报警状态，0：未监听，1：正常，2：报警

            //遍历上报数据
            if (watchData != null && watchData.Length > 0)
            {
                //用于计算运行时间、停机时间、产量
                int tempPreProCount = 0;
                int tempProCount = 0;
                int tempPreFailedCount = 0;
                int tempFailedCount = 0;
                bool findFirstDataFlag = false;//存在合规的第一条记录标记
                foreach (DataRow row in watchData)
                {
                    //只有状态为2的记录才是合规的第一条记录。
                    if (!findFirstDataFlag)
                    {
                        if (row["RunState"].ToString() == "2")
                        {
                            //开机总时间
                            DateTime runStart = Convert.ToDateTime(row["CreateTime"]);
                            DateTime runEnd = Convert.ToDateTime(watchData[watchData.Length - 1]["CreateTime"]);
                            totalSeconds = (int)(new TimeSpan(runEnd.Ticks - runStart.Ticks).TotalSeconds);
                            //初始化：上一个产量，当前产量、上一个不良、当前不良
                            tempPreProCount = int.Parse(row["ProductCount"].ToString());
                            tempProCount = int.Parse(row["ProductCount"].ToString());
                            tempPreFailedCount = int.Parse(row["FailedCount"].ToString());
                            tempFailedCount = int.Parse(row["FailedCount"].ToString());
                            findFirstDataFlag = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //最后一个记录的结束时间是其本身的开始时间
                    if (row["EndTime"] == DBNull.Value) row["EndTime"] = watchData[watchData.Length - 1]["CreateTime"];
                    //根据状态得到不同时间
                    if (row["RunState"].ToString() == "1")
                    {
                        DateTime startTime = Convert.ToDateTime(row["CreateTime"]);
                        DateTime endTime = Convert.ToDateTime(row["EndTime"]);
                        int tempFailedSeconds = 0;
                        int tempPlanRestSeconds = 0;
                        //解析停机时间
                        ParseStopTime(startTime, endTime, ref tempFailedSeconds, ref tempPlanRestSeconds);
                        failedSeconds += tempFailedSeconds;
                        planRestSeconds += tempPlanRestSeconds;
                        if (row["WarnState"].ToString() == "2")
                        {
                            warnSeconds += tempFailedSeconds;
                        }
                        else
                        {
                            stopSeconds += tempFailedSeconds;
                        }
                    }
                    else if (row["RunState"].ToString() == "2")
                    {
                        DateTime startTime = Convert.ToDateTime(row["CreateTime"]);
                        DateTime endTime = Convert.ToDateTime(row["EndTime"]);
                        runSeconds += (int)(new TimeSpan(endTime.Ticks - startTime.Ticks).TotalSeconds);
                    }
                    //故障次数统计
                    if (row["RunState"].ToString() == "1" && row["WarnState"].ToString() == "2")
                    {
                        errorCount++;
                    }
                    //产量
                    tempProCount = int.Parse(row["ProductCount"].ToString());
                    if (tempPreProCount <= tempProCount)
                    {
                        productCount += (tempProCount - tempPreProCount);
                    }
                    else
                    { //设备重置
                        productCount += tempProCount;
                    }
                    tempPreProCount = int.Parse(row["ProductCount"].ToString());
                    //不良
                    tempFailedCount = int.Parse(row["FailedCount"].ToString());
                    if (tempPreFailedCount <= tempFailedCount)
                    {
                        failedCount += (tempFailedCount - tempPreFailedCount);
                    }
                    else
                    { //设备重置
                        failedCount += tempFailedCount;
                    }
                    tempPreFailedCount = int.Parse(row["FailedCount"].ToString());
                }

                //--------计算稼动率--------
                //时间稼动率= (（开机时间-异常时间-计划停机时间)/（开机时间-计划停机时间))*100%
                if ((totalSeconds - planRestSeconds) > 0)
                    timeUR = Math.Round(((decimal)(totalSeconds - failedSeconds - planRestSeconds) / (totalSeconds - planRestSeconds)) * 100, 2);
                //性能稼动率=  ((产能 * 理论CT) / (开机时间—异常时间-计划停机时间))*100%
                if ((totalSeconds - failedSeconds - planRestSeconds) > 0)
                    efficacyUR = Math.Round(((decimal)(productCount * decimal.Parse(machine["TheoryCT"].ToString())) / (totalSeconds - failedSeconds - planRestSeconds)) * 100, 2);
                //良品率=(1-(不良品数/产量))*100
                if (productCount > 0)
                    passR = Math.Round((1 - ((decimal)failedCount / productCount)) * 100, 2);
                //OEE
                oee = Math.Round((timeUR * efficacyUR * passR) / 10000, 2);

                //--------最后运行数据-----------
                DateTime createTime = Convert.ToDateTime(watchData[watchData.Length - 1]["CreateTime"]);
                int minutes = (int)(new TimeSpan(TimeUtil.Now.Ticks - createTime.Ticks).TotalMinutes);
                if (minutes > 10)
                { //如果最后一条记录创建时间超过当前时间指定的监听超时时间，则表示连接断开了。
                    runState = 0;
                    wartnState = 0;
                }
                else
                {
                    runState = Convert.ToInt32(watchData[watchData.Length - 1]["RunState"]);
                    wartnState = Convert.ToInt32(watchData[watchData.Length - 1]["WarnState"]);
                }
                lastProductCount = Convert.ToInt32(watchData[watchData.Length - 1]["ProductCount"]);
            }
            //将统计结果插入设备数据行
            machine["OEE"] = oee;
            machine["ErrorCount"] = errorCount;
            machine["ProductCount"] = productCount;
            machine["LastProductCount"] = lastProductCount;
            machine["FailedCount"] = failedCount;
            machine["RunState"] = runState;
            machine["WarnState"] = wartnState;
            machine["TimeUR"] = timeUR;
            machine["EfficacyUR"] = efficacyUR;
            machine["PassR"] = passR;

            //时间比例计算
            if (totalSeconds > 0)
            {
                TimeDT.Rows[0]["TotalTime"] = runSeconds / 60;
                TimeDT.Rows[0]["Percent"] = Math.Round(((decimal)runSeconds / totalSeconds) * 100, 2);
                TimeDT.Rows[1]["TotalTime"] = planRestSeconds / 60;
                TimeDT.Rows[1]["Percent"] = Math.Round(((decimal)planRestSeconds / totalSeconds) * 100, 2);
                TimeDT.Rows[2]["TotalTime"] = failedSeconds / 60;
                TimeDT.Rows[2]["Percent"] = Math.Round(((decimal)failedSeconds / totalSeconds) * 100, 2);
                TimeDT.Rows[3]["TotalTime"] = stopSeconds / 60;
                TimeDT.Rows[3]["Percent"] = Math.Round(((decimal)stopSeconds / totalSeconds) * 100, 2);
                TimeDT.Rows[4]["TotalTime"] = warnSeconds / 60;
                TimeDT.Rows[4]["Percent"] = Math.Round(((decimal)warnSeconds / totalSeconds) * 100, 2);
            }
            labLastProductCount.Text = "最后上报产量：" + lastProductCount;
            labRealProductCount.Text = "实际产量：" + productCount;
            ParseWarnData();
        }

        //解析报警数据
        private void ParseWarnData()
        {
            //重置原来的数据
            ResetWarnCodeDT();
            //筛选报警的数据并遍历
            DataRow[] drs = WatchData.Select(" WarnState=2");
            foreach (DataRow row in drs)
            {
                DataRow[] warnRows = WarnCodeDT.Select("WarnCode='" + row["WarnCode"].ToString() + "'");
                if (warnRows != null && warnRows.Length > 0)
                {
                    int count = int.Parse(warnRows[0]["Count"].ToString());
                    int seconds = int.Parse(warnRows[0]["Seconds"].ToString());
                    DateTime startTime = DateTime.Parse(row["CreateTime"].ToString());
                    DateTime endTime = DateTime.Parse(row["EndTime"].ToString());
                    int times = Convert.ToInt32(new TimeSpan(endTime.Ticks - startTime.Ticks).TotalSeconds);
                    warnRows[0]["Count"] = count + 1;
                    warnRows[0]["Seconds"] = seconds + times;
                }
            }
            //筛选次数不为0不数据并绑定到数据表视图
            DataTable tempDt = WarnCodeDT.Clone();
            DataRow[] tempR = WarnCodeDT.Select(" Count>0");
            foreach (var row in tempR)
            {
                tempDt.ImportRow(row);
            }
            dgvWarnData.DataSource = tempDt;
        }
        #endregion

        #region 刷新图表 [运行状态时间]
        private void RefreshRunStateChart()
        {
            chartRunState.Series.Clear();
            double startMark = StatStartTime.ToOADate();
            chartRunState.ChartAreas[0].AxisY.Minimum = StatStartTime.ToOADate();
            chartRunState.ChartAreas[0].AxisY.Maximum = StatStartTime.AddDays(1).ToOADate();
            chartRunState.ChartAreas[0].AxisY.LabelStyle.Format = "HH:mm";
            chartRunState.ChartAreas[0].AxisY.Interval = 2;
            chartRunState.ChartAreas[0].AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;

            if (WatchData != null && WatchData.Rows.Count > 0)
            {
                //开机前
                System.Windows.Forms.DataVisualization.Charting.Series preSeries = new System.Windows.Forms.DataVisualization.Charting.Series();
                preSeries.ChartArea = "ChartAreaRunState";
                preSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
                preSeries.Legend = "LegendRunState";
                preSeries.Color = Color.Gray;
                preSeries["PointWidth"] = "2";
                preSeries.Points.AddXY(1, Convert.ToDateTime(WatchData.Rows[0]["CreateTime"]).ToOADate());
                chartRunState.Series.Add(preSeries);
                //中间的运行状态
                foreach (DataRow row in WatchData.Rows)
                {
                    System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                    series.ChartArea = "ChartAreaRunState";
                    series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
                    series.Legend = "LegendRunState";
                    series["PointWidth"] = "2";
                    if (row["RunState"].ToString() == "1" && row["WarnState"].ToString() == "2")
                    {
                        series.Color = Color.Orange;
                    }
                    else if (row["RunState"].ToString() == "1")
                    {
                        series.Color = Color.Red;
                    }
                    else if (row["RunState"].ToString() == "2")
                    {
                        series.Color = Color.Green;
                    }
                    series.ToolTip = row["CreateTime"].ToString() + " To " + row["EndTime"].ToString();
                    series.Points.AddXY(1, Convert.ToDateTime(row["EndTime"]).ToOADate() - Convert.ToDateTime(row["CreateTime"]).ToOADate());
                    chartRunState.Series.Add(series);
                }
                //关机后
                if (StatStartTime.AddDays(1).ToOADate() - Convert.ToDateTime(WatchData.Rows[WatchData.Rows.Count - 1]["EndTime"]).ToOADate() > 0)
                {
                    System.Windows.Forms.DataVisualization.Charting.Series latSeries = new System.Windows.Forms.DataVisualization.Charting.Series();
                    latSeries.ChartArea = "ChartAreaRunState";
                    latSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
                    latSeries.Legend = "LegendRunState";
                    latSeries.Color = Color.Gray;
                    latSeries["PointWidth"] = "2";
                    latSeries.Points.AddXY(1, StatStartTime.AddDays(1).ToOADate() - Convert.ToDateTime(WatchData.Rows[WatchData.Rows.Count - 1]["EndTime"]).ToOADate());
                    chartRunState.Series.Add(latSeries);
                }
            }
            else
            {
                System.Windows.Forms.DataVisualization.Charting.Series defSeries = new System.Windows.Forms.DataVisualization.Charting.Series();
                defSeries.ChartArea = "ChartAreaRunState";
                defSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
                defSeries.Legend = "LegendRunState";
                defSeries.Points.AddXY(1, StatEndTime.ToOADate());
                defSeries.Color = Color.Gray;
                chartRunState.Series.Add(defSeries);
            }
        }
        #endregion

        #region 刷新图表 [稼动率与OEE]
        private void RefreshUtilizeRate()
        {
            //加载数据
            decimal oee = decimal.Parse(Machines.Rows[0]["OEE"].ToString());
            labOEE.Text = oee + "%";
            decimal other = 100 - oee;
            chartOEE.Series["SeriesOEE"].Points.DataBindY(new decimal[] { oee, other });

            chartUR.Series["SeriesTimeUR"].Points.DataBindY(new decimal[] { decimal.Parse(Machines.Rows[0]["TimeUR"].ToString()) });
            chartUR.Series["SeriesEfficacyUR"].Points.DataBindY(new decimal[] { decimal.Parse(Machines.Rows[0]["EfficacyUR"].ToString()) });
            chartUR.Series["SeriesPassR"].Points.DataBindY(new decimal[] { decimal.Parse(Machines.Rows[0]["PassR"].ToString()) });
            chartUR.Series["SeriesTimeUR"]["PointWidth"] = "2";
            chartUR.Series["SeriesEfficacyUR"]["PointWidth"] = "2";
            chartUR.Series["SeriesPassR"]["PointWidth"] = "2";
        }
        #endregion

        #region 表格格式化
        private void dgvRunTime_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRunTime.Columns[e.ColumnIndex].HeaderText == "项目")
            {
                if ("运行时间".Equals(dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                {
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                }
                if ("计划停机时间".Equals(dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                {
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Blue;
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                }
                if ("计划外停机时间".Equals(dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                {
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                }
                if ("小停顿时间".Equals(dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                {
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Coral;
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                }
                if ("故障时间".Equals(dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                {
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                    dgvRunTime.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region [定时任务] 实时刷新
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (StatStartTime.AddDays(1) < TimeUtil.Now)
            { //前一天,向后推一天
                dtpCurrDate.Value = StatStartTime.AddDays(1);
                return;
            }
            RefreshWatchData();
            dgvRunTime.ClearSelection();
        }
        #endregion

        #region 其他
        /// <summary>
        /// 停机时间解析： 
        //      返回：生效的故障时间 与 生效的计划停机时间
        /// </summary>
        /// <param name="failedStart">停机开始时间</param>
        /// <param name="failedEnd">停机结束时间</param>
        /// <param name="failedSeconds">生效的故障时间</param>
        /// <param name="planRestSeconds">生效的停机时间</param>
        private void ParseStopTime(DateTime failedStart, DateTime failedEnd, ref int failedSeconds, ref int planRestSeconds)
        {
            failedSeconds = (int)(new TimeSpan(failedEnd.Ticks - failedStart.Ticks).TotalSeconds);
            planRestSeconds = 0;
            if (PlanRestTimes != null && PlanRestTimes.Rows.Count > 0)
            {
                DateTime tempStart;
                DateTime tempEnd;
                foreach (DataRow row in PlanRestTimes.Rows)
                {

                    DateTime startTime = Convert.ToDateTime(StatStartTime.Date.ToString("yyyy-MM-dd") + " " + row["startTime"]);
                    DateTime endTime = Convert.ToDateTime(StatStartTime.Date.ToString("yyyy-MM-dd") + " " + row["EndTime"]);
                    if (endTime < startTime)
                    {//次日
                        endTime.AddDays(1);
                    }
                    //故障时间为计划停机时间内
                    if (failedStart > startTime && failedEnd < endTime)
                    {
                        planRestSeconds = (int)(new TimeSpan(failedEnd.Ticks - failedStart.Ticks).TotalSeconds);
                        break;
                    }
                    //故障时间不在计划时间内
                    else if (failedEnd < startTime || failedStart > endTime)
                    {
                        continue;
                    }
                    //交集处理  
                    if (failedStart < startTime)
                    {
                        tempStart = startTime;
                    }
                    else
                    {
                        tempStart = failedStart;
                    }
                    if (failedEnd > endTime)
                    {
                        tempEnd = endTime;
                    }
                    else
                    {
                        tempEnd = failedEnd;
                    }
                    //是否超过最大计划停机时间
                    int maxSeconds = int.Parse(row["MaxSeconds"].ToString());
                    planRestSeconds += (int)(new TimeSpan(tempEnd.Ticks - tempStart.Ticks).TotalSeconds);
                    if (planRestSeconds > maxSeconds) planRestSeconds = maxSeconds;
                }
            }
            failedSeconds = failedSeconds - planRestSeconds;
        }

        //重置时间分析表行数据
        private void ResetTimeDTRow()
        {
            TimeDT.Rows.Clear();
            DataRow row1 = TimeDT.NewRow();
            row1["Item"] = "运行时间";
            TimeDT.Rows.Add(row1);
            DataRow row2 = TimeDT.NewRow();
            row2["Item"] = "计划停机时间";
            TimeDT.Rows.Add(row2);
            DataRow row3 = TimeDT.NewRow();
            row3["Item"] = "计划外停机时间";
            TimeDT.Rows.Add(row3);
            DataRow row4 = TimeDT.NewRow();
            row4["Item"] = "小停顿时间";
            TimeDT.Rows.Add(row4);
            DataRow row5 = TimeDT.NewRow();
            row5["Item"] = "故障时间";
            TimeDT.Rows.Add(row5);
            dgvRunTime.DataSource = TimeDT;
        }

        //重置报警分析表行数据
        private void ResetWarnCodeDT()
        {
            foreach (DataRow row in WarnCodeDT.Rows)
            {
                row["Count"] = 0;
                row["Seconds"] = 0;
            }
        }

        //日期改变事件
        private void dtpCurrDate_ValueChanged(object sender, EventArgs e)
        {
            StatStartTime = Convert.ToDateTime(dtpCurrDate.Value.Date.ToString("yyyy-MM-dd") + " " + StartTimeMark);
            StatEndTime = StatStartTime.AddDays(1);
            RefreshWatchData();

            //判断是否开启定时任务
            if (TimeUtil.Now >= StatStartTime && TimeUtil.Now <= StatEndTime)
            {
                timerRefresh.Enabled = true;
            }
            else
            {
                timerRefresh.Enabled = false;
            }
        }


        /// 窗体关闭事件
        private void FrmSingleCurrWatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRefresh.Enabled = false;
        }
        #endregion

    }
}

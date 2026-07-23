using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class FrmMachineWatch : Form
    {
        //窗体信息
        private int _Width = 1280;
        private int _Height = 600;
        private bool isLoaded = false;
        //设备信息
        private DataTable Machines;

        //统计时间
        private DateTime StatStartTime;//统计开时时间
        private DateTime StatEndTime;//统计结束时间
        private DataTable PlanRestTimes = null;//计划停机时间
        private String StartTimeMark = "07:30:00";//开时时间标记

        //统计分析数据
        private DataTable WatchData = new DataTable();//监听报告的数据
        //分页设置
        private int PageNo = 1;//当前页码序号。
        private int TotalPage = 0;//总页数。
        private int PageSize = 0;//页面大小
        public FrmMachineWatch()
        {
            InitializeComponent();
            _Width = this.Width;
            _Height = this.Height;
            SetTag(this);

            StatStartTime = Convert.ToDateTime(TimeUtil.Now.Date.ToString("yyyy-MM-dd") + " " + StartTimeMark);
            if (StatStartTime > TimeUtil.Now)
            { //次日,向前推一天
                StatStartTime = StatStartTime.AddDays(-1);
            }
            StatEndTime = StatStartTime.AddDays(1);
        }

        #region 窗体加载事件

        private void FrmMachineWatch_Load(object sender, EventArgs e)
        {
            try
            {
                //屏幕分辨率(全屏)
                System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                int h = rect.Height; //高（像素）
                int w = rect.Width;  //宽（像素）
                this.Size = new Size(w, h);

                //计划停机时间
                string sql = "SELECT * FROM M_PlanTime_T";
                PlanRestTimes = DBUtil.GetDataTable(sql);

                dtpCurrDate.Text = StatStartTime.ToString("yyyy-MM-dd");
                RefreshMachineData();
                isLoaded = true;
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
        private void RefreshMachineData()
        {
            if (!string.IsNullOrWhiteSpace(BaseInfo.Area))
            {
                labLeftTitle.Text = "（" + BaseInfo.Area + "区）监控";
            }
            else
            {
                labLeftTitle.Text = "全体监控";
            }
            string sql = "SELECT  MachineCode,MachineName+'('+CONVERT(varchar, MachineNo)  +')' AS MachineName,Line,TheoryCT,0.0 AS [OEE],0 AS [ErrorCount],0 AS[ProductCount],0 AS [LastProductCount],0 AS [FailedCount],0 AS [RunState],0 AS [WarnState],0.0 AS [TimeUR],0.0 AS [EfficacyUR],0.0 AS [PassR] FROM M_Machine_T  WHERE IsLink='Y' ";
            if (!string.IsNullOrWhiteSpace(BaseInfo.Area))
                sql += string.Format(" AND Line IN(SELECT Line FROM S_LineInfo_T WHERE Area='{0}')", BaseInfo.Area);
            Machines = DBUtil.GetDataTable(sql);
            RefreshWatchData();
        }

        private void RefreshWatchData()
        {
            //实际统计结束时间
            DateTime realStatEndTime = StatEndTime > TimeUtil.Now ? TimeUtil.Now : StatEndTime;
            string sql = string.Format(@"WITH cte AS (
	                                        SELECT *,
		                                        LAG ( RunState,1,0 ) OVER (PARTITION BY MachineCode ORDER BY  CreateTime  ) AS PreRunState,
		                                        LAG ( WarnState,1,0 ) OVER (PARTITION BY MachineCode ORDER BY  CreateTime  ) AS PreWarnState,
                                                LEAD ( RunState,1,0 ) OVER (PARTITION BY MachineCode  ORDER BY  CreateTime  ) AS NextRunState,
                                                LEAD ( ProductCount,1,0 ) OVER (PARTITION BY MachineCode  ORDER BY  CreateTime  ) AS NextProductCount,
                                                LEAD ( FailedCount,1,0 ) OVER (PARTITION BY MachineCode  ORDER BY  CreateTime  ) AS NextFailedCount
	                                        FROM
		                                        dbo.M_MachineReport_T WHERE  CreateTime>='{0}' AND CreateTime<='{1}'
	                                    )
                                        SELECT
	                                        *,
	                                        LEAD ( CreateTime,1,NULL ) OVER (PARTITION BY MachineCode  ORDER BY  CreateTime  ) AS EndTime
                                        FROM
	                                        cte WHERE RunState!=PreRunState OR WarnState!=PreWarnState OR NextRunState=0 OR NextProductCount<ProductCount OR NextFailedCount<FailedCount   ORDER BY MachineCode,CreateTime ", StatStartTime, realStatEndTime);
            WatchData = DBUtil.GetDataTable(sql);

            //设备遍历数据分析
            if (Machines != null && Machines.Rows.Count > 0)
            {
                for (int i = 0; i < Machines.Rows.Count; i++)
                {
                    string whereSql = string.Format("MachineCode='{0}'", Machines.Rows[i]["MachineCode"].ToString());
                    DataRow[] watchRows = WatchData.Select(whereSql);
                    ParseWatchData(Machines.Rows[i], watchRows);
                }
                //排序
                Machines.DefaultView.Sort = "RunState DESC";
                Machines = Machines.DefaultView.ToTable();
            }

            //-------------------------刷新控件---------------------------------
            labMachineCount.Text = "设备总数:" + Machines.Rows.Count.ToString();
            //设备分页
            PageSize = tlpanelMachines.RowCount * tlpanelMachines.ColumnCount; //页面大小(可显示的设备个数)
            TotalPage = (int)Math.Ceiling((decimal)Machines.Rows.Count / PageSize); //总页数
            PageNo = 0;//页码
            //轮播显示设备
            timerRoll_Tick(null, null);
            //刷新图表
            RefreshUtilizeRateChart();
            RefreshStateRateChart();
            RefreshOEEChart();
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
        }
        #endregion

        #region 刷新控件

        #region [设备:控件]

        //创建新的设备控件
        private void MountMachine(int index, string machineCode, string machineName, string line, string oee, string errorCount, string lastProductCount, string failedCount, string runState, string warnState)
        {
            float xScale = (this.Width * 1.0F) / _Width;
            float yScale = (this.Height * 1.0F) / _Height;

            System.Windows.Forms.Panel panelMachine = new System.Windows.Forms.Panel();
            System.Windows.Forms.SplitContainer splitPanel = new System.Windows.Forms.SplitContainer();
            System.Windows.Forms.Label labLineTitle = new System.Windows.Forms.Label();
            System.Windows.Forms.Label labProductTitle = new System.Windows.Forms.Label();
            System.Windows.Forms.Label labOEETitle = new System.Windows.Forms.Label();
            System.Windows.Forms.Label labLine = new System.Windows.Forms.Label();
            System.Windows.Forms.Label labProductCount = new System.Windows.Forms.Label();
            System.Windows.Forms.Label labOEE = new System.Windows.Forms.Label();
            System.Windows.Forms.Label labMachineName = new System.Windows.Forms.Label();

            // 
            // panelMachine
            // 
            panelMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMachine.BackColor = System.Drawing.Color.Gray;
            panelMachine.Controls.Add(splitPanel);
            panelMachine.Controls.Add(labMachineName);
            panelMachine.Name = "panelMachine" + index;
            //panelMachine.Size = new System.Drawing.Size((int)(70 * xScale), (int)(98 * yScale));
            panelMachine.Font = new System.Drawing.Font("宋体", 7F * xScale);
            // 
            // splitPanel
            // 
            splitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            splitPanel.Margin = new System.Windows.Forms.Padding(0);
            splitPanel.Name = "splitPanel" + index;
            splitPanel.SplitterWidth = 1;
            splitPanel.IsSplitterFixed = true;
            // 
            // splitPanel.Panel1
            // 
            splitPanel.Panel1.Controls.Add(labLineTitle);
            splitPanel.Panel1.Controls.Add(labProductTitle);
            splitPanel.Panel1.Controls.Add(labOEETitle);
            // 
            // splitPanel.Panel2
            // 
            splitPanel.Panel2.Controls.Add(labLine);
            splitPanel.Panel2.Controls.Add(labProductCount);
            splitPanel.Panel2.Controls.Add(labOEE);
            splitPanel.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            splitPanel.SplitterDistance = (int)(60 * xScale);
            // 
            // labLineTitle
            // 
            labLineTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labLineTitle.Margin = new System.Windows.Forms.Padding(0);
            labLineTitle.Name = "labLineTitle" + index;
            labLineTitle.Size = new System.Drawing.Size(41, (int)(14 * yScale));
            labLineTitle.Text = "产线";
            labLineTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labProductTitle
            // 
            labProductTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labProductTitle.Margin = new System.Windows.Forms.Padding(0);
            labProductTitle.Name = "labProductTitle" + index;
            labProductTitle.Size = new System.Drawing.Size(41, (int)(14 * yScale));
            labProductTitle.Text = "产量";
            labProductTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labOEETitle
            // 
            labOEETitle.Dock = System.Windows.Forms.DockStyle.Top;
            labOEETitle.Margin = new System.Windows.Forms.Padding(0);
            labOEETitle.Name = "labOEETitle" + index;
            labOEETitle.Size = new System.Drawing.Size(41, (int)(14 * yScale));
            labOEETitle.Text = "OEE";
            labOEETitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labLine
            // 
            labLine.Dock = System.Windows.Forms.DockStyle.Top;
            labLine.ForeColor = System.Drawing.Color.Aqua;
            labLine.Margin = new System.Windows.Forms.Padding(0);
            labLine.Name = "labLine" + index;
            labLine.Size = new System.Drawing.Size(75, (int)(14 * yScale));
            labLine.Text = line;
            labLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labProductCount
            // 
            labProductCount.Dock = System.Windows.Forms.DockStyle.Top;
            labProductCount.ForeColor = System.Drawing.Color.Aqua;
            labProductCount.Margin = new System.Windows.Forms.Padding(0);
            labProductCount.Name = "labProductCount" + index;
            labProductCount.Size = new System.Drawing.Size(75, (int)(14 * yScale));
            labProductCount.Text = lastProductCount;
            labProductCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labOEE
            // 
            labOEE.Dock = System.Windows.Forms.DockStyle.Top;
            labOEE.ForeColor = System.Drawing.Color.Aqua;
            labOEE.Margin = new System.Windows.Forms.Padding(0);
            labOEE.Name = "labOEE" + index;
            labOEE.Size = new System.Drawing.Size(75, (int)(14 * yScale));
            labOEE.Text = oee + "%";
            labOEE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labMachineName
            // 
            Color bkColor = Color.Blue;//默认背景颜色
            if (TimeUtil.Now.Date == StatStartTime.Date)
            {
                if (runState == "2")
                {
                    bkColor = Color.Green;
                }
                else if (warnState == "2")
                {
                    bkColor = Color.Orange;
                }
                else if (runState == "1")
                {
                    bkColor = Color.Red;
                }
                else
                {
                    bkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                }
            }
            labMachineName.BackColor = bkColor;
            labMachineName.Dock = System.Windows.Forms.DockStyle.Top;
            labMachineName.Name = "labMachineName" + index;
            labMachineName.Size = new System.Drawing.Size(120, (int)(21 * yScale));
            labMachineName.Text = machineName;
            labMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labMachineName.DoubleClick += Machine_DoubleClick;
            labMachineName.Tag = machineCode;

            tlpanelMachines.Controls.Add(panelMachine);
        }
        #endregion

        #region [设备状态:环形饼图]
        private void RefreshStateRateChart()
        {
            int runCount = Machines.Select("RunState=2").Length;//运行中设备数量
            int stopCount = Machines.Select("RunState=1 AND WarnState<>2").Length;//停机中设备数量
            int warnCount = Machines.Select("RunState=1  AND WarnState=2").Length;//报警中设备数量
            int noneCount = Machines.Rows.Count - runCount - stopCount - warnCount;//未监听的设备数量

            List<int> statVal = new List<int>();
            List<Color> cols = new List<Color>();
            if (noneCount > 0)
            {
                cols.Add(System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64))))));
                statVal.Add(noneCount);
            }
            if (runCount > 0)
            {
                cols.Add(System.Drawing.Color.Green);
                statVal.Add(runCount);
            }
            if (stopCount > 0)
            {
                cols.Add(System.Drawing.Color.Red);
                statVal.Add(stopCount);
            }
            if (warnCount > 0)
            {
                cols.Add(System.Drawing.Color.Orange);
                statVal.Add(warnCount);
            }
            this.chartMachineStateR.PaletteCustomColors = cols.ToArray();
            //加载数据
            chartMachineStateR.Series["SeriesStateR"].Points.DataBindY(statVal);
        }
        #endregion

        #region [OEE:环形饼图]
        private void RefreshOEEChart()
        {
            DataRow[] rows = Machines.Select("OEE>0");//运行中设备数量
            decimal oee = 0;
            foreach (DataRow row in rows)
            {
                oee += decimal.Parse(row["OEE"].ToString());
            }
            if (rows.Length > 0)
                oee = Math.Round(oee / rows.Length, 2);
            decimal other = 100 - oee;
            //加载数据
            if (other <= 0)
            {
                chartOEE.Series["SeriesOEE"].Points.DataBindY(new decimal[] { oee });
            }
            else
            {
                chartOEE.Series["SeriesOEE"].Points.DataBindY(new decimal[] { oee, other });
            }
            chartOEE.Titles[0].Text = "平均OEE:" + oee + "%";
        }
        #endregion

        #region [稼动率:柱状图]
        private void RefreshUtilizeRateChart()
        {
            DataRow[] rows = Machines.Select("OEE>0").Take(30).ToArray();
            //加载数据
            chartUR.Series["SeriesTimeUR"].Points.DataBindXY(rows, "MachineName", rows, "TimeUR");
            chartUR.Series["SeriesEfficacyUR"].Points.DataBindXY(rows, "MachineName", rows, "EfficacyUR");
            chartUR.Series["SeriesPassR"].Points.DataBindXY(rows, "MachineName", rows, "PassR");
        }
        #endregion

        #endregion

        #region [定时任务] 看板实时刷新
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (StatStartTime.AddDays(1) < TimeUtil.Now)
            { //前一天,向后推一天
                dtpCurrDate.Value = StatStartTime.AddDays(1);
                return;
            }
            RefreshMachineData();
        }
        #endregion

        #region [定时任务] 切换设备页面
        private void timerRoll_Tick(object sender, EventArgs e)
        {
            timerRoll.Enabled = false;
            PageNo++;
            SwitchMachinePage();
            if (TotalPage > 1)
            {
                //滚动时间间隔(毫秒)
                int rollInterval = (int)Math.Floor((decimal)timerRefresh.Interval / TotalPage);
                timerRoll.Interval = rollInterval;
                timerRoll.Enabled = true;
            }
        }
        #endregion

        #region 切换设备页面
        //前一页
        private void pboxPrePage_Click(object sender, EventArgs e)
        {
            PageNo--;
            SwitchMachinePage();
        }
        //后一页
        private void pboxNextPage_Click(object sender, EventArgs e)
        {
            PageNo++;
            SwitchMachinePage();
        }
        //切换设备显示页面
        private void SwitchMachinePage()
        {
            if (PageNo > TotalPage || PageNo <= 0) PageNo = 1;
            labPageNo.Text = PageNo.ToString();

            tlpanelMachines.SuspendLayout();
            tlpanelMachines.Controls.Clear();
            for (int i = (PageNo - 1) * PageSize; i < PageNo * PageSize && i < Machines.Rows.Count; i++)
            {
                DataRow row = Machines.Rows[i];
                MountMachine(i, row["MachineCode"].ToString(), row["MachineName"].ToString(), row["Line"].ToString(), row["OEE"].ToString(), row["ErrorCount"].ToString(),
                    row["LastProductCount"].ToString(), row["FailedCount"].ToString(), row["RunState"].ToString(), row["WarnState"].ToString());
            }
            tlpanelMachines.ResumeLayout();
        }
        #endregion

        #region 缩放
        private void FrmMoreCurrWatch_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) return;
            float xScale = (this.Width * 1.0F) / _Width;
            float yScale = (this.Height * 1.0F) / _Height;
            SetControl(xScale, yScale, panelTop);
            SetControls(xScale, yScale, panelTop, true);
            SetControl(xScale, yScale, panelBottom);
            SetControl(xScale, yScale, panelRight);
            SetControls(xScale, yScale, panelRight, false);
            this.Refresh();
            panelTop.Refresh();
            panelFill.Refresh();
            panelBottom.Refresh();
        }

        //将控件的原始宽、高、X、Y、字体大小设置到控件的Tag属性中。
        private void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    SetTag(con);
            }
        }

        private void SetControl(float xScale, float yScale, Control con)
        {
            string[] mytag = con.Tag.ToString().Split(':');
            con.Width = (int)(Convert.ToSingle(mytag[0]) * xScale);
            con.Height = (int)(Convert.ToSingle(mytag[1]) * yScale);
            con.Left = (int)(Convert.ToSingle(mytag[2]) * xScale);
            con.Top = (int)(Convert.ToSingle(mytag[3]) * yScale);
            Single currentSize = Convert.ToSingle(mytag[4]) * xScale;

            //改变字体大小
            con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
        }

        //设置控件的新宽、高、X、Y、字体大小设。
        private void SetControls(float xScale, float yScale, Control cons, bool isRecursion)
        {
            foreach (Control con in cons.Controls)
            {
                //Width + ":" + Height + ":" + Left + ":" + Top + ":" + Font.Size;
                string[] mytag = con.Tag.ToString().Split(':');
                con.Width = (int)(Convert.ToSingle(mytag[0]) * xScale);
                con.Height = (int)(Convert.ToSingle(mytag[1]) * yScale);
                con.Left = (int)(Convert.ToSingle(mytag[2]) * xScale);
                con.Top = (int)(Convert.ToSingle(mytag[3]) * yScale);
                Single currentSize = Convert.ToSingle(mytag[4]) * xScale;

                //改变字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);

                if (isRecursion)
                {
                    if (con.Controls.Count > 0)
                    {
                        try
                        {
                            SetControls(xScale, yScale, con, true);
                        }
                        catch
                        { }
                    }
                }

            }
        }
        #endregion

        #region 其他
        //停机时间解析
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
                    planRestSeconds += (int)(new TimeSpan(tempEnd.Ticks - tempStart.Ticks).TotalSeconds);
                    //是否超过最大计划停机时间
                    int maxSeconds = int.Parse(row["MaxSeconds"].ToString());
                    planRestSeconds += (int)(new TimeSpan(tempEnd.Ticks - tempStart.Ticks).TotalSeconds);
                    if (planRestSeconds > maxSeconds) planRestSeconds = maxSeconds;
                }
            }
            failedSeconds = failedSeconds - planRestSeconds;
        }


        //日期改变事件
        private void dtpCurrDate_ValueChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
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
        private void FrmMoreCurrWatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRefresh.Enabled = false;
        }

        //机台名称Label双击事件
        private void Machine_DoubleClick(object sender, EventArgs e)
        {
            Label lab = sender as Label;
            int machineCode = Convert.ToInt32(lab.Tag.ToString());
            FrmMachineWatchOne frm = new FrmMachineWatchOne(machineCode, dtpCurrDate.Value);
            frm.ShowDialog();
        }
        #endregion


    }
}
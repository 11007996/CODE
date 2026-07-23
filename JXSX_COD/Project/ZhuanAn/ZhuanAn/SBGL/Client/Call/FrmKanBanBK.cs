using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Linq;
using Common.Util;
using Common;
using System.Runtime.InteropServices;
using Call.Base;


namespace Call
{
    public partial class FrmKanBanBK : Form
    {
        int _Width = 1280;
        int _Height = 600;
        int MachineNum = 15;//看板显示机台数量
        int PreWeekNum = 2;//显示上几周折线图数值
        private string Area;//看板显示的区域，不设置表显全区域
        private DataTable MachineSoundDT;//设备名称仿音表

        //播报计数：无人处理的故障
        Dictionary<int, int> NoHandleSpeechCount = new Dictionary<int, int>();//key：故障id,value当前刷新次数，用来判断时间间隔
        //播报计数：需要支援的故障
        Dictionary<int, int> NeedHelpSpeechCount = new Dictionary<int, int>();//key:故障id；value当前刷新次数，用来判断时间间隔
        //语音合成器
        SpeechSynthesizer Speech = new SpeechSynthesizer();
        //故障信息
        private DataTable ErrorInfo = new DataTable();

        public FrmKanBanBK()
        {
            InitializeComponent();
            MachineNum = BaseInfo.MachineNum > 30 ? 30 : BaseInfo.MachineNum;//最大30
            PreWeekNum = BaseInfo.PreWeekNum > 5 ? 5 : BaseInfo.PreWeekNum;//最大5周
            Area = BaseInfo.Area.Trim();
            BaseInfo.ClockFlag = true;
            BaseInfo.ClockStartTime = BaseInfo.AppStartTime;
            _Width = this.Width;
            _Height = this.Height;
            SetTag(this);
        }

        #region 窗体加载事件
        private void FrmKanBan_Load(object sender, EventArgs e)
        {
            try
            {
                //屏幕分辨率小于1378时，截取groupbox2宽度
                System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                int h = rect.Height; //高（像素）
                int w = rect.Width;  //宽（像素）
                this.Size = new Size(w, h);
                //设置语速及音量
                //waveOutSetVolume(0, 0xFFFF);
                // Speech.Volume = BaseInfo.Volume;
                Speech.Rate = BaseInfo.SpeechRate;
                //关闭表格列自动生成
                dgvError.AutoGenerateColumns = false;
                //显示人员故障。
                if (BaseInfo.CallHandlerShowFlag)
                {
                    dgvError.Columns["dgcTargetObj"].HeaderText = "机台/指定人员";
                    dgvError.Columns["dgcTargetObj"].DataPropertyName = "TargetObj";
                }
                //日期动态显示
                timerCurrTime.Enabled = true;
                //设备类型模仿音
                MachineSoundDT = DBUtil.GetDataTable("SELECT MachineType,ImitateSound FROM M_FaultSolution_T");
                //刷新界面数据
                timerRefreshData_Tick(null, null);
                //区域
                if (!string.IsNullOrWhiteSpace(Area))
                {
                    labArea.Text = Area + "区";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmKanBan), null, ex);
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
        //声音大小
        [DllImport("winmm.dll")]   //引用winmm.dll    
        public static extern long waveOutSetVolume(long deviceID, long Volume);
        #endregion


        #region 定时器任务
        /// <summary>
        /// 每秒钟更新一次看板的时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCurrTime_Tick(object sender, EventArgs e)
        {
            DateTime now = TimeUtil.Now;
            string week = now.DayOfWeek.ToString();//星期
            string YMD = now.ToString("D");//yyyy年MM月dd日
            string HMS = now.ToString("T");//HH:mm:ss
            switch (week)
            {
                case "Monday": week = "星期一";
                    break;
                case "Tuesday": week = "星期二";
                    break;
                case "Wednesday": week = "星期三";
                    break;
                case "Thursday": week = "星期四";
                    break;
                case "Friday": week = "星期五";
                    break;
                case "Saturday": week = "星期六";
                    break;
                default: week = "星期日";
                    break;
            }
            labCurrTime.Text = string.Format("{0} {1}  {2}", YMD, week, HMS);
        }

        //刷新看板数据
        private void timerRefreshData_Tick(object sender, EventArgs e)
        {
            timerRefreshData.Enabled = false;
            try
            {
                //刷新人员信息
                RefreshUserInfo();
                //刷新故障信息
                if (ErrorInfo.Rows.Count > 8)
                {   //当故障信息大于8条时,显示剩余数据
                    for (int i = 0; i < 8; i++)
                    {
                        ErrorInfo.Rows.RemoveAt(0);
                    }
                    dgvError.DataSource = ErrorInfo;
                }
                else
                {
                    RefreshErrorInfo();
                }
                //刷新图表
                RefreshStatisticsChartInfo();
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmKanBan), "看板数据刷新异常", ex);
            }
            finally
            {
                timerRefreshData.Enabled = true;
            }
        }
        #endregion


        #region 刷新人员信息
        /// <summary>
        /// 刷新工程师信息
        /// </summary>
        private void RefreshUserInfo()
        {
            //清除之前在gb_HanderInfo中自动生成的控件
            tlpUserInfo.Controls.Clear();
            //获取新的人员信息
            DataTable userDT = SelectCurrWeekData();
            if (userDT == null || userDT.Rows.Count <= 0)
            {
                tlpUserInfo.Visible = false;
                return;
            }
            //挂载人员信息组件
            for (int i = 0; i < userDT.Rows.Count; i++)
            {
                int colIndex = i % 4;
                int rowIndex = Int32.Parse(Math.Floor((double)(i / 4)).ToString());
                MountUserInfoComponent(i, userDT, rowIndex, colIndex);
            }
            tlpUserInfo.Refresh();
        }

        /// <summary>
        /// 查出本周所有工程师（非管理员）的基础信息、本周维护总数，本周未解决数，本周维护时长
        /// </summary>
        private DataTable SelectCurrWeekData()
        {
            string sql = string.Empty;
            if (string.IsNullOrWhiteSpace(Area))
            {//全区
                //【UserNo工号、UserName姓名，UserState状态，HandleQty本周维护总计，UntreatedQty本周未解决总计，TotalTimes本周维护时长】
                sql = string.Format(@"SELECT UserNo,UserName,UserState,'0' AS HandleQty,'0' AS UntreatedQty,'0' TotalTimes
                    FROM S_User_T WHERE  Dept= N'{0}' AND UserLevel ='1' AND UseFlag='Y'
                    ORDER BY UserState DESC;", BaseInfo.Dept);

                //本周每个工程师所有的维护次数总计
                sql += string.Format(@"SELECT HandlerNo,COUNT(HandlerNo)as HandleQty
                    FROM  M_ErrorRecord_T
					WHERE Starttime >= (SELECT CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7) AS StartOfWeek)
					AND Starttime <= (SELECT CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7)+6 AS EndOfWeek)
                    GROUP BY HandlerNo;");

                //本周每个工程师未处理解决方案次数
                sql += string.Format(@"SELECT HandlerNo,COUNT(HandlerNo)as UntreatedQty
                    FROM  M_ErrorRecord_T 
					WHERE (ErrorReason  is null OR ErrorReason='')
                    AND (FaultContent  is null OR FaultContent='')
                    AND (SolutionContent  is null OR SolutionContent='')
					AND Starttime >=(SELECT CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7) AS StartOfWeek)
					AND Starttime <= (select CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7)+6 AS EndOfWeek)
                    GROUP BY HandlerNo;");

                //本周每个工程师的维护时长
                sql += string.Format(@"SELECT HandlerNo,SUM(DATEDIFF(SECOND,Cometime,Endtime))as TotalTimes
                    FROM  M_ErrorRecord_T  
                    WHERE  Endtime is not null  AND Cometime is not null
				    AND Starttime >=(SELECT CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7) AS StartOfWeek)
				    AND Starttime <= (select CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7)+6 AS EndOfWeek)
                    GROUP BY HandlerNo;");
            }
            else
            { //指定区域
                //【UserNo工号、UserName姓名，UserState状态，HandleQty本周维护总计，UntreatedQty本周未解决总计，TotalTimes本周维护时长】
                sql = string.Format(@"SELECT UserNo,UserName,UserState,'0' AS HandleQty,'0' AS UntreatedQty,'0' TotalTimes
                    FROM S_User_T WHERE Area like '%{0}%' AND Dept= N'{1}' AND UserLevel ='1' AND UseFlag='Y'
                    ORDER BY UserState DESC;", Area, BaseInfo.Dept);

                //本周每个工程师所有的维护次数总计
                sql += string.Format(@"SELECT HandlerNo,COUNT(HandlerNo)as HandleQty
                    FROM  M_ErrorRecord_T  WHERE Area='{0}' 
					AND Starttime >= (SELECT CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7) AS StartOfWeek)
					AND Starttime <= (SELECT CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7)+6 AS EndOfWeek)
                    GROUP BY HandlerNo;", Area);

                //本周每个工程师未处理解决方案次数
                sql += string.Format(@"SELECT HandlerNo,COUNT(HandlerNo)as UntreatedQty
                    FROM  M_ErrorRecord_T  WHERE Area='{0}' 
					AND (ErrorReason  is null OR ErrorReason='')
                    AND (FaultContent  is null OR FaultContent='')
                    AND (SolutionContent  is null OR SolutionContent='')
					AND Starttime >=(SELECT CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7) AS StartOfWeek)
					AND Starttime <= (select CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7)+6 AS EndOfWeek)
                    GROUP BY HandlerNo;", Area);

                //本周每个工程师的维护时长
                sql += string.Format(@"SELECT HandlerNo,SUM(DATEDIFF(SECOND,Cometime,Endtime))as TotalTimes
                    FROM  M_ErrorRecord_T  
                    WHERE Area='{0}' 
                    AND  Endtime is not null  AND Cometime is not null
				    AND Starttime >=(SELECT CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7) AS StartOfWeek)
				    AND Starttime <= (select CONVERT(datetime, FLOOR(CONVERT(float,GETDATE())/7)*7)+6 AS EndOfWeek)
                    GROUP BY HandlerNo;", Area);
            }

            DataSet userInfoDS = DBUtil.GetDataSet(sql, new string[] { "userInfoDT", "handleQtyDT", "untreatedQtyDT", "totalTimesDT" });
            if (userInfoDS == null) return null;
            DataTable userInfoDT = userInfoDS.Tables["userInfoDT"];
            DataTable handleQtyDT = userInfoDS.Tables["HandleQtyDT"];
            DataTable untreatedQtyDT = userInfoDS.Tables["untreatedQtyDT"];
            DataTable totalTimesDT = userInfoDS.Tables["totalTimesDT"];

            //遍历每个工程师，并将 【HandleQty本周维护总计，UntreatedQty本周未解决总计，TotalTimes本周维护时长】插入工程师信息表中
            foreach (DataRow userR in userInfoDT.Rows)
            {
                string userNo = userR["UserNo"].ToString();
                string filterWhere = string.Format("HandlerNo = '{0}'", userNo);
                if (handleQtyDT.Rows.Count > 0)
                {
                    DataRow[] rows = handleQtyDT.Select(filterWhere);
                    if (rows != null && rows.Length > 0 && rows[0]["HandleQty"] != null)
                    {
                        userR["HandleQty"] = rows[0]["HandleQty"];
                    }
                }
                if (untreatedQtyDT.Rows.Count > 0)
                {
                    DataRow[] rows = untreatedQtyDT.Select(filterWhere);
                    if (rows != null && rows.Length > 0 && rows[0]["UntreatedQty"] != null)
                    {
                        userR["UntreatedQty"] = rows[0]["UntreatedQty"];
                    }
                }
                if (totalTimesDT.Rows.Count > 0)
                {
                    DataRow[] rows = totalTimesDT.Select(filterWhere);
                    if (rows != null && rows.Length > 0 && rows[0]["TotalTimes"] != null)
                    {
                        userR["TotalTimes"] = rows[0]["TotalTimes"];
                    }
                }
            }
            return userInfoDT;
        }

        /// <summary>
        ///  挂载一个工程师信息组件
        /// </summary>
        /// <param name="i"></param>
        /// <param name="dt"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        private void MountUserInfoComponent(int i, DataTable dt, int rowIndex, int colIndex)
        {
            float xScale = (this.Width * 1.0F) / _Width;
            float yScale = (this.Height * 1.0F) / _Height;
            // 
            // panelUserInfo
            // 
            Panel panel = new Panel();
            panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            panel.ForeColor = Color.White;
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.Location = new System.Drawing.Point(0, 0);
            panel.Margin = new System.Windows.Forms.Padding(0, 0, (int)(2 * xScale), (int)(2 * xScale));
            panel.Name = "panelUserInfo" + i;
            panel.Size = new System.Drawing.Size(79, 128);
            panel.Enabled = true;
            // 
            // pbHandlePic
            // 
            PictureBox pbUserImage = new PictureBox();
            pbUserImage.Dock = System.Windows.Forms.DockStyle.Top;
            pbUserImage.Location = new System.Drawing.Point(0, 0);
            pbUserImage.Margin = new System.Windows.Forms.Padding(0);
            pbUserImage.Name = "pbUserImage" + i;
            pbUserImage.Size = new System.Drawing.Size(79, (int)(84 * yScale));
            pbUserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pbUserImage.TabStop = false;
            //获取照片路径
            pbUserImage.Image = UserImageUtil.GetCacheFile(dt.Rows[i]["UserNo"].ToString());
            // 
            // labUserNo
            // 
            Label labUserNo = new Label();
            labUserNo.AutoSize = true;
            labUserNo.Font = new System.Drawing.Font("宋体", (float)(6F * xScale), System.Drawing.FontStyle.Bold);
            labUserNo.ForeColor = Color.White;
            labUserNo.Location = new System.Drawing.Point(0, (int)(84 * yScale));
            labUserNo.Name = "labUserNo" + i;
            labUserNo.Size = new System.Drawing.Size(48, 15);
            labUserNo.Text = "工号:" + dt.Rows[i]["UserNo"].ToString();
            // 
            // labHandleQty
            // 
            Label labHandleQty = new Label();
            labHandleQty.AutoSize = true;
            labHandleQty.Font = new System.Drawing.Font("宋体", (float)(6F * xScale), System.Drawing.FontStyle.Bold);
            labHandleQty.ForeColor = System.Drawing.Color.White;
            labHandleQty.Location = new System.Drawing.Point(0, (int)(93 * yScale));
            labHandleQty.Name = "labHandleQty" + i;
            labHandleQty.Size = new System.Drawing.Size(48, 15);
            string handleQty = dt.Rows[i]["HandleQty"].ToString();
            labHandleQty.Text = "维护:" + handleQty;
            // 
            // labUntreatedQty
            // 
            Label labUntreatedQty = new Label();
            labUntreatedQty.AutoSize = true;
            labUntreatedQty.Font = new System.Drawing.Font("宋体", (float)(6F * xScale), System.Drawing.FontStyle.Bold);
            labUntreatedQty.ForeColor = System.Drawing.Color.White;
            labUntreatedQty.Location = new System.Drawing.Point(0, (int)(102 * yScale));
            labUntreatedQty.Name = "labUntreatedQty" + i;
            labUntreatedQty.Size = new System.Drawing.Size(48, 15);
            string untreatedQty = dt.Rows[i]["UntreatedQty"].ToString();
            labUntreatedQty.Text = "未结:" + untreatedQty;
            if (int.Parse(untreatedQty) > 0)
            {
                labUntreatedQty.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                labUntreatedQty.ForeColor = System.Drawing.Color.White;
            }
            // 
            // labTotalTimes
            // 
            Label labTotalTimes = new Label();
            labTotalTimes.AutoSize = true;
            labTotalTimes.Font = new System.Drawing.Font("宋体", (float)(6F * xScale), System.Drawing.FontStyle.Bold);
            labTotalTimes.ForeColor = System.Drawing.Color.White;
            labTotalTimes.Location = new System.Drawing.Point(0, (int)(111 * yScale));
            labTotalTimes.Name = "labTotalTimes" + i;
            labTotalTimes.Size = new System.Drawing.Size(48, 15);
            string totalTimes = "时长:";
            if (!string.IsNullOrWhiteSpace(dt.Rows[i]["TotalTimes"].ToString()))
            {
                totalTimes += TimeUtil.ConvertSecondsToDesc(Int32.Parse(dt.Rows[i]["TotalTimes"].ToString()));
            }
            labTotalTimes.Text = totalTimes;

            panel.Controls.Add(pbUserImage);
            panel.Controls.Add(labUserNo);
            panel.Controls.Add(labHandleQty);
            panel.Controls.Add(labUntreatedQty);
            panel.Controls.Add(labTotalTimes);

            tlpUserInfo.Controls.Add(panel, colIndex, rowIndex);
        }

        #endregion


        #region 刷新故障信息
        private void RefreshErrorInfo()
        {
            try
            {
                //根据部门获取还未处理的故障(前一百100),清除之前在gb_Table_Data自动生成的控件
                string sql = string.Format(@"SELECT  TOP 100 '' FormatColumn,e.Line,e.Machine,h3.UserName TargetHandlerName,CONCAT(e.Machine,h3.UserName) TargetObj,e.Dept,e.StartTime,e.ComeTime,'' WaitedTime,
                                        h1.UserName HandlerName,h2.UserName HelperName,e.Status,e.CallReason,'' HandleTime,e.EndTime,e.Id
                                        FROM	M_ErrorRecord_T e 
                                        LEFT JOIN S_User_T h1 ON e.HandlerNo =h1.UserNo 
                                        LEFT JOIN S_User_T h2 ON e.HelperNo=h2.UserNo
                                        LEFT JOIN S_User_T h3 ON e.TargetHandler=h3.UserNo
                                        WHERE  Status<>'Y' AND Status<>'N' ", Area);
                //是否指定区域
                if (!string.IsNullOrWhiteSpace(Area))
                {
                    sql += string.Format(" AND e.Area='{0}' ", Area);
                }
                //是否包含呼叫人员
                if (!BaseInfo.CallHandlerShowFlag)
                    sql += "AND e.TargetHandler is null";
                sql += " ORDER BY Status ASC,StartTime DESC";

                DataTable dt = DBUtil.GetDataTable(sql);
                if (dt == null) return;
                ErrorInfo = dt;
                dgvError.DataSource = dt;
                dgvError.ClearSelection();

                if (dt.Rows.Count <= 0)
                {
                    NoHandleSpeechCount.Clear();
                    NeedHelpSpeechCount.Clear();
                    BaseInfo.CallFlag = false;
                    return;
                }
                BaseInfo.KanBanErrorCount = dgvError.Rows.Count;
                //广播
                SendVoiceBroadcast(dt);
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmKanBan), null, ex);
            }
        }


        /// <summary>
        /// 发送语音广播
        /// </summary>
        /// <param name="dt"></param>
        private void SendVoiceBroadcast(DataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }

                List<int> tempErrors = new List<int>();//故障列表
                foreach (DataRow row in dt.Rows)
                {
                    //未开启呼叫人员广播,则指定人员的故障不广播。
                    if (row["TargetHandlerName"] != DBNull.Value && !BaseInfo.CallHandlerSpeechFlag)
                    {
                        continue;
                    }

                    int id = Convert.ToInt32(row["Id"]);
                    tempErrors.Add(id);
                    string targetHandler = row["TargetHandlerName"] == DBNull.Value ? "人员" : row["TargetHandlerName"].ToString();
                    string dept = row["Dept"].ToString();
                    string state = row["Status"].ToString();
                    string line = row["Line"].ToString();
                    string callReason = row["CallReason"].ToString();
                    string machine = string.Empty;
                    //检查机台名称是否设置了[模仿声音]
                    string[] machineInfo = row["Machine"].ToString().Split('*');
                    DataRow machineTypeR = MachineSoundDT.Select(string.Format("MachineType='{0}'", machineInfo[0])).FirstOrDefault();
                    if (machineTypeR != null && machineTypeR["ImitateSound"] != DBNull.Value && !string.IsNullOrWhiteSpace(machineTypeR["ImitateSound"].ToString()))
                    {
                        machineInfo[0] = machineTypeR["ImitateSound"].ToString();
                        machine = string.Join("", machineInfo);
                    }
                    else
                    {
                        machine = row["Machine"].ToString().Replace("*", "");
                    }
                    if (!string.IsNullOrWhiteSpace(machine))
                    {
                        machine += "机台";
                    }

                    //机台、部门、呼叫原因、状态都不为空（参数合法）
                    if (!string.IsNullOrEmpty(state))
                    {
                        string speakStr = string.Format("{0},请{1}{2}到{3}{4}处理", callReason, dept, targetHandler, line, machine);
                        //新出现的故障
                        if (!NoHandleSpeechCount.ContainsKey(id))
                        {
                            NoHandleSpeechCount.Add(id, 1);
                            Speech.SpeakAsync(speakStr);
                            Speech.SpeakAsync(speakStr);
                        }
                        else if ("A".Equals(state))
                        {//未接单，每分钟再播一次

                            if (NoHandleSpeechCount[id] % (6 * BaseInfo.SpeechSpanMinute) == 0)
                            {
                                Speech.SpeakAsync(speakStr);
                                Speech.SpeakAsync(speakStr);
                            }
                            NoHandleSpeechCount[id] += 1;
                        }
                        else if ("C".Equals(state))
                        {//呼叫支援
                            if (!NeedHelpSpeechCount.ContainsKey(id)) NeedHelpSpeechCount.Add(id, 0);
                            if (NeedHelpSpeechCount[id] % (6 * BaseInfo.SpeechSpanMinute) == 0)
                            {
                                Speech.SpeakAsync(line + machine + ",设备故障，请高级工程师尽快到现场处理");
                            }
                            NeedHelpSpeechCount[id] += 1;
                        }
                    }
                }

                //清除不存在的故障
                //待处理的故障
                List<int> tempHandleKeys = NoHandleSpeechCount.Keys.ToList();
                for (int i = 0; i < tempHandleKeys.Count; i++)
                {
                    if (!tempErrors.Contains(tempHandleKeys[i])) NoHandleSpeechCount.Remove(tempHandleKeys[i]);
                }
                //待支援的故障
                List<int> tempHelpKeys = NeedHelpSpeechCount.Keys.ToList();
                for (int i = 0; i < tempHelpKeys.Count; i++)
                {
                    if (!tempErrors.Contains(tempHelpKeys[i])) NeedHelpSpeechCount.Remove(tempHelpKeys[i]);
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmKanBan), "语音广播出现异常", ex);
            }
        }
        #endregion


        #region 刷新统计分析图表信息
        /// <summary>
        /// 刷新统计图表
        /// </summary>
        private void RefreshStatisticsChartInfo()
        {
            try
            {
                chartError.Series.Clear();
                //图表X轴的label文本值：机台名称
                List<string> machines = null;
                //图表Y轴数据数组:机台出现故障次数
                List<int>[] pointArr = new List<int>[PreWeekNum + 1];

                //机台故障次数的数据
                DataTable dtMachineEC = SelectMachineErrorToChartData();
                if (dtMachineEC == null || dtMachineEC.Rows.Count <= 0) return;

                //将DataTable数据转换成符合图表的数据类型
                machines = dtMachineEC.AsEnumerable().Select(t => t.Field<string>("MachineNo")).ToList();
                for (int i = 0; i < pointArr.Length; i++)
                {
                    string columnName = "Week" + i;
                    pointArr[i] = dtMachineEC.AsEnumerable().Select(t => t.Field<int>(columnName)).ToList();
                }
                //挂载
                MountStatisticsChartComponent(machines, pointArr);
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmKanBan), "看板图表数据刷新异常", ex);
            }
        }

        /// <summary>
        /// 根据周期查询机台出现故障次数
        /// </summary>
        /// <param name="preWeek">前几周</param>
        /// <param name="machines">机台名称</param>
        /// <returns></returns>
        private DataTable SelectMachineErrorToChartData()
        {
            //本周开始结束时间
            DateTime currWeekStart;
            DateTime end;
            TimeUtil.WeekStartToEnd(0, out currWeekStart, out end);

            //故障机台
            string sql = string.Format(@"SELECT TOP {0} Line+Machine MachineNo  FROM M_ErrorRecord_T WHERE  StartTime>='{1}' AND Machine is not null ", MachineNum, currWeekStart);
            //是否指定区域
            if (!string.IsNullOrWhiteSpace(Area))
            {
                sql += string.Format(" AND Area='{0}' ", Area);
            }
            sql += " GROUP BY Line,Machine  ORDER BY Count(Line+Machine) DESC";
            DataTable dtMachineEC = DBUtil.GetDataTable(sql);
            if (dtMachineEC == null) return null;
            //机台编号清单
            List<string> machines = dtMachineEC.AsEnumerable().Select(t => t.Field<string>("MachineNo")).ToList();

            //机台总数不足时补充
            if (machines.Count < MachineNum)
            {
                DateTime preWeekStart;
                preWeekStart = currWeekStart.AddDays(-1 * PreWeekNum * 7);//前几周开始日期
                sql = string.Format(@"SELECT TOP {0} Line+Machine MachineNo FROM M_ErrorRecord_T WHERE  StartTime>='{1}' AND StartTime<'{2}'  AND Machine is not null
                                    AND Line+Machine NOT IN('{3}') ", MachineNum - machines.Count, preWeekStart, currWeekStart, string.Join("','", machines));
                //指定区域
                if (!string.IsNullOrWhiteSpace(Area))
                {
                    sql += string.Format(" And Area=N'{0}'", Area);
                }
                sql += " GROUP BY Line,Machine   ORDER BY Count(*) DESC";
                DataTable dtMachineECMore = DBUtil.GetDataTable(sql);
                if (dtMachineECMore != null)
                {
                    dtMachineEC.Merge(dtMachineECMore);
                    machines = dtMachineEC.AsEnumerable().Select(t => t.Field<string>("MachineNo")).ToList();
                }
            }

            //没有数据返回null
            if (dtMachineEC.Rows.Count <= 0) return null;

            //循环获取指定周数的机台故障数据，并将故障数插入到机台故障机台表的新列中
            for (int i = 0; i <= PreWeekNum; i++)
            {
                string columnName = "Week" + i;
                dtMachineEC.Columns.Add(columnName, typeof(int));
                DataTable dtMEC = SelectMachineErrorCountWithWeek(i, machines);
                if (dtMEC != null)
                {
                    foreach (DataRow row in dtMachineEC.Rows)
                    {
                        DataRow[] drs = dtMEC.Select(string.Format(" MachineNo='{0}'", row["MachineNo"].ToString()));
                        row[columnName] = drs.Length > 0 ? drs[0]["ErrorCount"] : 0;
                    }
                }
            }
            return dtMachineEC;
        }

        /// <summary>
        /// 根据星期查询要统计的机台故障数
        /// </summary>
        /// <param name="preWeek">前面第几个星期</param>
        /// <param name="machineNo">机台编号</param>
        /// <returns></returns>
        private DataTable SelectMachineErrorCountWithWeek(int preWeek, IList<string> machineNo)
        {
            DateTime start;
            DateTime end;
            TimeUtil.WeekStartToEnd(-1 * preWeek, out start, out end);
            string sql = string.Format("SELECT Line+Machine AS MachineNo,Count(*) ErrorCount FROM M_ErrorRecord_T WHERE  StartTime>='{0}' AND StartTime<'{1}'  AND Machine is not null AND Line+Machine in('{2}')  ",
                                         start, end, string.Join("','", machineNo));
            //是否指定区域
            if (!string.IsNullOrWhiteSpace(Area))
            {
                sql += string.Format(" AND Area='{0}' ", Area);
            }
            sql += "  GROUP BY Line,Machine";
            return DBUtil.GetDataTable(sql);
        }

        /// <summary>
        /// 挂载统计图表信息
        /// </summary>
        /// <param name="dataX"></param>
        /// <param name="dataY"></param>
        private void MountStatisticsChartComponent(List<string> dataX, List<int>[] dataY)
        {
            if (dataX == null || dataX.Count <= 0) return;
            for (int i = 0; i < dataY.Length; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                series.ChartArea = "ChartArea1";
                series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                series.Legend = "Legend1";
                series.Name = i == 0 ? "本周" : "前" + i + "周";
                series.Label = "#VAL";                //设置显示X Y的值    
                series.LabelForeColor = Color.Black;
                series.ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值
                series.Points.DataBindXY(dataX, dataY[i]);

                if (i == 0) series.Color = Color.Black;
                if (i == 1) series.Color = Color.Red;
                if (i == 2) series.Color = Color.DarkOrange;
                if (i == 3) series.Color = Color.Lime;
                if (i == 4) series.Color = Color.Cyan;
                if (i == 5) series.Color = Color.DarkViolet;
                chartError.Series.Add(series);
            }
            chartError.Legends[0].Position.Auto = true;
            chartError.Legends[0].BackColor = Color.Transparent;
            chartError.ChartAreas[0].BackColor = Color.Transparent;
            chartError.ChartAreas[0].BorderColor = Color.Transparent;
            chartError.ChartAreas[0].AxisX.Interval = 1;//X轴数据显示间隔
            chartError.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartError.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chartError.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
            chartError.ChartAreas[0].AxisX2.LabelStyle.IsStaggered = false;
        }

        #endregion


        #region 格式化单元格
        private void dgvError_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string headerText = dgvError.Columns[e.ColumnIndex].HeaderText;
                //防止后面通过状态更改行背景色时导致的闪烁问题
                if (headerText == "格式化")
                {
                    DataRow row = ((DataRowView)dgvError.Rows[e.RowIndex].DataBoundItem).Row;
                    TimeSpan ts = new TimeSpan(TimeUtil.Now.Ticks - Convert.ToDateTime(row["StartTime"].ToString()).Ticks);
                    //大于5分钟无人处理
                    if (ts.TotalMinutes >= 5 && "A".Equals(row["Status"].ToString()))
                    {
                        dgvError.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
                if (headerText == "开始时间")
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("HH:mm");
                }
                else if (headerText == "到位时间")
                {
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        e.Value = Convert.ToDateTime(e.Value).ToString("HH:mm");
                    }
                }
                else if (headerText == "等待时长")
                {
                    DataRow row = ((DataRowView)dgvError.Rows[e.RowIndex].DataBoundItem).Row;
                    if (!string.IsNullOrWhiteSpace(row["ComeTime"].ToString()))
                    {
                        e.Value = TimeUtil.ConvertDiffTime(Convert.ToDateTime(row["StartTime"].ToString()), Convert.ToDateTime(row["ComeTime"].ToString()));
                    }
                }
                else if (headerText == "状态")
                {
                    switch (e.Value.ToString())
                    {
                        case "A":
                            e.Value = "待处理";
                            e.CellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
                            break;
                        case "B":
                            e.CellStyle.BackColor = System.Drawing.Color.ForestGreen;
                            e.Value = "处理中";
                            break;
                        case "C":
                            e.CellStyle.BackColor = System.Drawing.Color.Red;
                            e.Value = "待支援";
                            break;
                        case "D":
                            e.CellStyle.BackColor = System.Drawing.Color.ForestGreen;
                            e.Value = "支援中";
                            break;
                        case "E":
                            e.Value = "待完成";
                            break;
                        case "N":
                            e.Value = "呼叫解除";
                            break;
                        case "Y":
                            e.Value = "已完成";
                            break;
                    }
                }
                else if (headerText == "用时")
                {
                    DataRow row = ((DataRowView)dgvError.Rows[e.RowIndex].DataBoundItem).Row;
                    if (!string.IsNullOrWhiteSpace(row["EndTime"].ToString()))
                    {
                        e.Value = TimeUtil.ConvertDiffTime(Convert.ToDateTime(row["ComeTime"].ToString()), Convert.ToDateTime(row["EndTime"].ToString()));
                    }
                    else if (!string.IsNullOrWhiteSpace(row["ComeTime"].ToString()))
                    {
                        e.Value = TimeUtil.ConvertDiffTime(Convert.ToDateTime(row["ComeTime"].ToString()), TimeUtil.Now);
                    }
                }
            }
        }
        #endregion


        #region 缩放
        private void FrmKanBan_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) return;
            this.SuspendLayout();
            float xScale = (this.Width * 1.0F) / _Width;
            float yScale = (this.Height * 1.0F) / _Height;
            SetControl(xScale, yScale, panelTop);
            SetControls(xScale, yScale, this.panelTop, true);
            SetControl(xScale, yScale, tlpUserInfo);
            SetDataGridScale(xScale, yScale);
            SetControl(xScale, yScale, chartError);

            this.Refresh();
            panelTop.Refresh();
            scFill.Refresh();
            tlpUserInfo.Refresh();
            this.ResumeLayout();
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
            string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
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
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
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

        //数据表格缩放
        private void SetDataGridScale(float xScale, float yScale)
        {
            //列头
            this.dgvError.ColumnHeadersHeight = (int)(40 * yScale) < 4 ? 4 : (int)(40 * yScale);
            this.dgvError.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 8F * xScale, System.Drawing.FontStyle.Bold);
            //行模板
            this.dgvError.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F * xScale, System.Drawing.FontStyle.Bold);
            this.dgvError.RowTemplate.DividerHeight = (int)(1 * yScale);
            this.dgvError.RowTemplate.Height = (int)(40 * yScale);
        }
        #endregion


        #region 其他
        //焦点事件
        private void FrmKanBan_Activated(object sender, EventArgs e)
        {
            BaseInfo.ClockFlag = true;
            BaseInfo.ClockStartTime = BaseInfo.AppStartTime;
            BaseInfo.CurrFrmType = typeof(FrmKanBan);
        }


        /// 窗体关闭事件
        private void FrmKanBan_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerCurrTime.Enabled = false;
            timerRefreshData.Enabled = false;
            BaseInfo.ClockFlag = false;
        }
        #endregion





    }
}

using CallSys;
using CallSys.Base;
using CallSys.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using Common.Utils;
using Common;

namespace CallSys
{
    public partial class FrmMaster : Form
    {
        public string _area = BaseInfo.Area;//厂区
        public string _line = BaseInfo.Line;//线别
        public string _machine = BaseInfo.Machine;//机台

        private DataTable Handlers;//所有处理人信息
        private ErrorInfo _errorInfo;//当前呼叫处理的故障信息

        private FrmBarScan FrmBarScan;//刷卡窗体
        private FrmFaultSolution FrmFaultSolution;//故障维护窗体
        private FrmMasterError FrmErrorList;//故障列表窗体，没有呼叫限制时显示。


        //窗体位置属性
        private int _X;
        private int _Y;

        public FrmMaster()
        {
            InitializeComponent();
            SetWindowRegion();
            //按钮状态初始化
            btnCallHandler.Enabled = true;
            this.btnCallHandler.BackColor = Color.Red;
            btnFinish.Enabled = false;
            btnCallHelper.BackColor = Color.Gray;
            btnCallHelper.Enabled = false;
            btnFinish.BackColor = Color.Gray;
        }

        #region 窗体拖动
        private void FrmMaster_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _X + _Y != 0)
            {
                this.Left += e.X - _X;
                this.Top += e.Y - _Y;
                if (FrmErrorList != null) FrmErrorList.ReLocationForm();
            }
            else
            {
                _X = e.X;
                _Y = e.Y;
            }
        }
        #endregion

        #region 窗体圆角
        public void SetWindowRegion()
        {
            GraphicsPath FormPath;

            FormPath = new System.Drawing.Drawing2D.GraphicsPath();

            Rectangle rect = new Rectangle(-1, -1, this.Width + 1, this.Height);

            FormPath = GetRoundedRectPath(rect, 24);

            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角   
            path.AddArc(arcRect, 185, 90);
            //   右上角   
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 275, 90);
            //   右下角   
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 356, 90);
            //   左下角   
            arcRect.X = rect.Left;
            arcRect.Width += 2;
            arcRect.Height += 2;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion

        #region 窗体加载
        private void FrmMaster_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //数据加载
            if (string.IsNullOrWhiteSpace(_area))
            {
                labMessage.Text = "请在系统设置中设置好区域后重新打开此窗体";
            }
            if (string.IsNullOrWhiteSpace(_line))
            {
                labMessage.Text = "请在系统设置中设置好线名后重新打开此窗体";
            }
            gbMachine.Text = "【" + _area + "区> " + _line + "】机台模组";
            cmbCallReason.SelectedText = "机台故障";

            RefreshHandlerList();
            RefreshMachineList();

            if (!BaseInfo.CallLimit)
            {
                FrmErrorList = new FrmMasterError();
                FrmErrorList.Owner = this;
                FrmErrorList.Show();
            }
            else
            {
                //加载故障信息
                if (!string.IsNullOrWhiteSpace(_area) && !string.IsNullOrWhiteSpace(_line))
                {
                    RefreshErrorInfo();
                }
            }
        }
        #endregion

        private void RefreshHandlerList()
        {
            string sql = "SELECT HandlerNo,HandlerName,HandlerDept,HandlerState,HandlerLevel,Area FROM M_HandlerInfo_T WHERE UseFlag='Y';";
            Handlers = DBUtil.GetDataTable(sql);
            if (Handlers != null)
            {
                List<string> depts = Handlers.AsEnumerable().Select(t => t.Field<string>("HandlerDept")).ToList();
                depts = depts.Distinct().ToList();
            }
        }

        #region 刷新机台列表
        /// <summary>
        /// 刷新机台列表
        /// </summary>
        private void RefreshMachineList()
        {
            try
            {
                lsbMachine.DataSource = null;
                string sqlstr = string.Format("SELECT [Machine]  FROM [dbo].[M_LineMachines_T] where [Line]='{0}'", _line);
                if (!string.IsNullOrWhiteSpace(_machine))
                    sqlstr += string.Format(" AND Machine=N'{0}'", _machine);
                lsbMachine.DataSource = DBUtil.GetDataTable(sqlstr);
                lsbMachine.ValueMember = "Machine";
                lsbMachine.DisplayMember = "Machine";
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMaster), null, ex);
            }
        }
        #endregion

        #region 其他事件
        //切换故障类型
        private void cmbCallReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("换线".Equals(cmbCallReason.Text))
            {
                lsbMachine.DataSource = null;
                DataTable dt = new DataTable();
                DataColumn col = new DataColumn("Machine");
                dt.Columns.Add(col);
                DataRow row = dt.NewRow();
                row[0] = "换线";
                dt.Rows.Add(row);
                lsbMachine.DataSource = dt;
                lsbMachine.DisplayMember = "Machine";
                lsbMachine.ValueMember = "Machine";
                lsbMachine.SelectedIndex = 0;
            }
            else
            {
                RefreshMachineList();
            }
        }

        //焦点事件
        private void FrmMaster_Activated(object sender, EventArgs e)
        {
            GlobalData.CurrFrmType = typeof(FrmMaster);
        }

        //关闭窗体事件
        private void FrmMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_errorInfo == null || _errorInfo.Status == ErrorStatus.Y || _errorInfo.Status == ErrorStatus.N)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("程序有任务在进行中无法关闭", "错误操作", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 按钮事件
        #region 呼叫
        private void btnCall_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            btnCallHandler.Enabled = false;
            try
            {
                //检查系统是否有更新
                if (BaseInfo.AutoUpdate && UpdateHelper.CheckCallSysApp())
                {
                    if (MessageBox.Show("呼叫系统有新的版本，需要现在更新吗?", "更新提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        UpdateHelper.StartUpdateApp(UpdateHelper.START_TYPE_UPDATE);
                    }
                    else
                    {
                        btnCallHandler.Enabled = true;
                        return;
                    }
                }
                //数据检查
                if (!CheckFormData())
                {
                    btnCallHandler.Enabled = true;
                    return;
                }
                //根据是否有限制，检查是否有未完成的故障或是否重复呼叫。
                if (BaseInfo.CallLimit)
                {
                    //检查是否已有未处理的故障
                    string sqlChk = string.Format(@"SELECT TOP 1 Id,Area,Line,Dept,Machine,CallReason,MaxHandleTimes,MaxHelpTimes,StartTime, ComeTime,EndTime,
                                    HandlerNo,HelperNo,Status,ErrorReason,FaultType,FaultContent,SolutionContent,ProdCount,SolverNo,SolverName
                                    FROM M_ErrorRecord_T WHERE  Line='{0}' AND Status<>'N' AND Status<>'Y' ", _line);
                    if (!string.IsNullOrWhiteSpace(_machine))
                        sqlChk += string.Format(" AND (Machine=N'{0}' OR Machine=N'换线')", _machine);
                    sqlChk += " ORDER BY StartTime DESC";
                    DataTable dtErr = DBUtil.GetDataTable(sqlChk);
                    if (dtErr != null && dtErr.Rows.Count > 0)
                    {
                        if (MessageBox.Show("已有故障未处理，是否现在处理?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            RefreshErrorInfo();
                        }
                        else
                        {
                            btnCallHandler.Enabled = true;
                        }
                        return;
                    }
                }
                else
                {
                    //检查机具是否存在未处理故障
                    string sqlChk2 = string.Format(@"SELECT TOP(1) 1  FROM M_ErrorRecord_T WHERE   Line='{0}' AND Machine=N'{1}' AND Status<>'N' AND Status<>'Y'; ", _line, lsbMachine.Text.Trim());
                    DataTable dt = DBUtil.GetDataTable(sqlChk2);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        labMessage.Text = "该机具已存在未完成的呼叫记录";
                        btnCallHandler.Enabled = true;
                        return;
                    }
                }

                //---------------------------呼叫---------------------------------
                //确定呼叫提示
                if (MessageBox.Show("确定要呼叫吗？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                {
                    btnCallHandler.Enabled = true;
                    return;
                }

                //数据处理
                string callReason = cmbCallReason.Text.Trim();
                string machine = lsbMachine.Text.Trim();
                string machineType = lsbMachine.Text.Trim().Split('*')[0];
                //处理时长限制
                string sql = string.Format("SELECT MaxHandleTimes,MaxHelpTimes FROM M_FaultSolution_T WHERE MachineType='{0}' AND AutoHelpFlag='Y'", machineType);
                DataTable dtFHT = DBUtil.GetDataTable(sql);
                int maxHandleTimes = 0;
                int maxHelpTimes = 0;
                if (dtFHT != null && dtFHT.Rows.Count == 1)
                {
                    maxHandleTimes = Int32.Parse(dtFHT.Rows[0]["MaxHandleTimes"].ToString());
                    maxHelpTimes = Int32.Parse(dtFHT.Rows[0]["MaxHelpTimes"].ToString());
                }
                //呼叫电脑信息
                string PCIP = SystemInfoUtil.GetClientLocalIPv4Address();//当前电脑IP
                string PCMac = SystemInfoUtil.getMacAddr_Local();//当前电脑Mac
                string PCUser = SystemInfoUtil.GetUserName();//当前电脑用户

                //增加故障记录
                sql = string.Format(@"INSERT INTO M_ErrorRecord_T
                                    (Area,Line,Dept,Machine,MaxHandleTimes,MaxHelpTimes,CallReason,Status,StartTime,PCIP,PCMac,PCUser,UpdateTime) OUTPUT inserted.Id 
                                     VALUES (N'{0}',N'{1}',N'{2}',N'{3}','{4}','{5}',N'{6}','{7}',GETDATE(),'{8}','{9}','{10}',GETDATE())",
                                     _area, _line, "生技", machine, maxHandleTimes, maxHelpTimes,
                                     callReason, "A", PCIP, PCMac, PCUser);
                //插入并反回插入后生成的自增ID
                DataTable dtId = DBUtil.GetDataTable(sql);
                if (dtId == null || dtId.Rows.Count <= 0)
                {
                    labMessage.Text = "提示：呼叫失败（插入数据失败）";
                    btnCallHandler.Enabled = true;
                    return;
                }
                int errorId = Int32.Parse(dtId.Rows[0][0].ToString());

                //加载数据
                RefreshErrorInfo(errorId);
                if (!BaseInfo.CallLimit)
                {
                    FrmErrorList.SelectedErrorId = errorId;
                    FrmErrorList.RefreshError();
                }
            }
            catch (Exception ex)
            {
                btnCallHandler.Enabled = true;
                LogHelper.Error(typeof(FrmMaster), null, ex);
                return;
            }
        }
        #endregion

        #region 支援
        private void btnCallHelper_Click(object sender, EventArgs e)
        {
            btnCallHelper.Enabled = false;
            try
            {
                //数据检查
                labMessage.Text = "";
                labMessage.ForeColor = Color.Red;
                if (_errorInfo == null)
                {
                    labMessage.Text = "未找到故障信息";
                    btnCallHelper.Enabled = true;
                    return;
                }
                if (string.IsNullOrWhiteSpace(_errorInfo.HandlerNo))
                {
                    labMessage.Text = "还未开始处理。";
                    btnCallHelper.Enabled = true;
                    return;
                }
                //更新状态
                string sql = string.Format("UPDATE M_ErrorRecord_T SET Status='C',HelperNo=null,CallHelpTime=GETDATE(),CallHelpMode='1',UpdateTime=GETDATE()  WHERE Id={0}", _errorInfo.Id);
                DBUtil.ExecSQL(sql);

                //发送微信信息
                SendWxMessage("1", _errorInfo);

                //刷卡显示
                RefreshErrorInfo(_errorInfo.Id);
                if (!BaseInfo.CallLimit)
                {
                    FrmErrorList.RefreshError();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMaster), null, ex);
                return;
            }
        }
        #endregion

        #region 完成
        private void btnFinish_Click(object sender, EventArgs e)
        {
            btnFinish.Enabled = false;
            try
            {
                //更新状态
                string sql = string.Format("UPDATE M_ErrorRecord_T SET Status='E',SolverNo=null,UpdateTime=GETDATE()  WHERE Id={0}", _errorInfo.Id);
                DBUtil.ExecSQL(sql);
                RefreshErrorInfo(_errorInfo.Id);
                if (!BaseInfo.CallLimit)
                {
                    FrmErrorList.RefreshError();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMaster), null, ex);
                return;
            }
        }
        #endregion
        #endregion

        #region 定时任务
        //每分钟刷新一次
        private void timerRefreshErrorInfo_Tick(object sender, EventArgs e)
        {
            if (_errorInfo != null && DBUtil.DBConnState)
            {
                CheckErrorOverTime(_errorInfo);
                RefreshErrorInfo(_errorInfo.Id);
            }
        }
        #endregion

        #region 检查数据
        //新增呼叫时检查数据是否合法
        private bool CheckFormData()
        {
            string msg = "";
            if (_errorInfo != null && _errorInfo.Status != ErrorStatus.N && _errorInfo.Status != ErrorStatus.Y)
            {
                msg = "当前已有存在呼叫记录，请误重复操作。";
            }
            else if (string.IsNullOrWhiteSpace(_area))
            {
                msg = "厂区不能为空";
            }
            else if (string.IsNullOrWhiteSpace(_line))
            {
                msg = "线别不能为空";
            }
            else if (cmbCallReason.Text.Trim() == "")
            {
                msg = "请选择呼叫原因";
            }
            else if (lsbMachine.SelectedItem == null)
            {
                msg = "请选择模组";
            }
            labMessage.Text = msg.Length > 0 ? "提示：" + msg : "";
            return msg.Length == 0;
        }
        #endregion

        #region [回调] 刷卡确认
        /// <summary>
        /// [回调]刷卡确认
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ScanCard_Handle(string cardNo, ref  string msg)
        {
            try
            {
                msg = "";
                if (_errorInfo == null) return true;
                //刷卡人信息
                string strSQL = string.Format("SELECT HandlerNo,HandlerName,HandlerLevel,UseFlag FROM M_HandlerInfo_T WHERE HandlerNo='{0}'", cardNo);
                DataTable dtHandler = DBUtil.GetDataTable(strSQL);
                if (dtHandler == null)
                {
                    msg = "检查网络";
                    return false;
                }
                if (dtHandler.Rows.Count != 1)
                {
                    msg = "警告！此工号不存在,非法进入！";
                    return false;
                }
                if (!"Y".Equals(dtHandler.Rows[0]["UseFlag"]))
                {
                    msg = "警告！此工号不可用！";
                    return false;
                }
                string handlerName = dtHandler.Rows[0]["HandlerName"].ToString();
                string handlerLevel = dtHandler.Rows[0]["HandlerLevel"].ToString();

                //获取最新状态
                strSQL = string.Format("SELECT * FROM M_ErrorRecord_T WHERE Id={0}", _errorInfo.Id);
                DataTable dt = DBUtil.GetDataTable(strSQL);
                if (dt != null && dt.Rows.Count == 1)
                {
                    _errorInfo.Status = (ErrorStatus)Enum.Parse(typeof(ErrorStatus), dt.Rows[0]["Status"].ToString());
                }

                //根据操作状态更新数据
                if (_errorInfo.Status == ErrorStatus.A)
                {
                    strSQL = string.Format(@"INSERT INTO M_ErrorHandleScan_T(ErrorId,HandlerNo,HandlerName,HandlerLevel,ErrorStatus,CreateTime)VALUES('{0}','{1}',N'{2}','{3}',N'B',GETDATE());
                         UPDATE M_ErrorRecord_T SET HandlerNo=N'{1}',ComeTime=GETDATE(), Status=N'B',UpdateUser=N'{1}',UpdateTime=GETDATE() WHERE Id='{0}';
                         UPDATE M_HandlerInfo_T SET HandlerState = 'H',UpdateUser=N'{1}',UpdateTime=GETDATE() WHERE HandlerNo = '{1}'; ", _errorInfo.Id, cardNo, handlerName, handlerLevel);
                    DBUtil.ExecSQL(strSQL);
                }
                else if (_errorInfo.Status == ErrorStatus.C)
                {
                    if (cardNo == _errorInfo.HandlerNo || cardNo == _errorInfo.HelperNo)
                    {
                        msg = "已是处理人，不可再刷卡。";
                    }
                    else
                    {
                        strSQL = string.Format(@"INSERT INTO M_ErrorHandleScan_T(ErrorId,HandlerNo,HandlerName,HandlerLevel,ErrorStatus,CreateTime)VALUES('{0}','{1}',N'{2}','{3}',N'D',GETDATE());
                             UPDATE M_ErrorRecord_T SET HelperNo=N'{1}',Status='D',UpdateUser=N'{1}',UpdateTime=GETDATE() WHERE Id='{0}';
                             UPDATE M_HandlerInfo_T SET HandlerState = N'H',UpdateUser=N'{1}',UpdateTime=GETDATE() WHERE HandlerNo = '{1}';", _errorInfo.Id, cardNo, handlerName, handlerLevel);
                        DBUtil.ExecSQL(strSQL);
                    }
                }
                else if (_errorInfo.Status == ErrorStatus.E)
                {
                    strSQL = string.Format("SELECT TOP 1 * FROM M_ErrorHandleScan_T WHERE ErrorId={0} AND HandlerNo=N'{1}' ORDER BY CreateTime DESC", _errorInfo.Id, cardNo);
                    DataTable dtScan = DBUtil.GetDataTable(strSQL);
                    if (dtScan != null && dtScan.Rows.Count <= 0)
                    {
                        msg = "非处理人或支援人无法确认完成";
                    }
                    else
                    {
                        strSQL = string.Format(@"INSERT INTO M_ErrorHandleScan_T(ErrorId,HandlerNo,HandlerName,HandlerLevel,ErrorStatus,CreateTime)VALUES('{0}','{1}',N'{2}','{3}',N'Y',GETDATE());
                             UPDATE M_ErrorRecord_T SET Status='Y',SolverNo=N'{1}',SolverName=N'{2}',EndTime=GETDATE(),UpdateUser=N'{1}',UpdateTime=GETDATE()  WHERE Id='{0}';
                             UPDATE M_HandlerInfo_T SET HandlerState = 'W' WHERE HandlerNo = '{1}';", _errorInfo.Id, cardNo, handlerName, handlerLevel);
                        DBUtil.ExecSQL(strSQL);
                    }
                }
                //刷卡成功
                if (msg.Length <= 0)
                {
                    RefreshErrorInfo(_errorInfo.Id);
                    if (!BaseInfo.CallLimit)
                    {
                        FrmErrorList.RefreshError();
                    }
                }
                return msg.Length <= 0;
            }
            catch (Exception ex)
            {
                msg = "系统异常，请联系管理员。";
                LogHelper.Error(typeof(FrmMaster), "刷卡处理异常", ex);
                return false;
            }
        }
        #endregion

        #region [回调] 解决故障确认
        public bool SolveError_Handle(int prodCount, string errorReason, string faultType, string faultContent, string solutionContent, ref string msg)
        {
            //返回给呼叫处理窗口
            //校验数据
            if (_errorInfo == null)
            {
                msg = "未故障关联";
            }
            else if (string.IsNullOrWhiteSpace(errorReason))
            {
                msg = "故障原因不能为空";

            }
            else if (string.IsNullOrWhiteSpace(faultType))
            {
                msg = "故障类型不能为空";
            }
            else if (string.IsNullOrWhiteSpace(faultContent))
            {
                msg = "故障内容不能为空";
            }
            else if (string.IsNullOrWhiteSpace(solutionContent))
            {
                msg = "解决方案不能为空";
            }
            //更新故障的维护信息
            if (msg.Length <= 0)
            {
                string sql = string.Format(@"UPDATE M_ErrorRecord_T SET ErrorReason = N'{1}',FaultType=N'{2}',FaultContent = N'{3}',SolutionContent = N'{4}', ProdCount={5},UpdateUser='{6}',UpdateTime=GETDATE() WHERE	Id={0}",
                                           _errorInfo.Id, errorReason, faultType, faultContent, solutionContent, prodCount, _errorInfo.SolverScanInfo.HandlerNo);
                DBUtil.ExecSQL(sql);
            }
            return msg.Length <= 0;
        }
        #endregion

        #region [同步锁] 刷新故障信息
        //同步锁，防止定时任务与用户操作同时发生。
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RefreshErrorInfo(int? errorId = null)
        {
            //多故障模式，传入null不主动查找其他故障。
            if (!BaseInfo.CallLimit && errorId == null)
            {
                LoadErrorInfo(null);
                return;
            }
            //查检数据库连接
            if (DBUtil.DBConnState == false)
            {
                string r = DBUtil.CheckServerConnState();
                if (!string.IsNullOrWhiteSpace(r))
                {
                    new FrmMsgDialog("服务器连接异常", "原因:" + r).Show();
                    return;
                }
            }

            timerRefreshErrorInfo.Enabled = false;

            string sql = string.Empty;
            sql = "DECLARE @Id int;";
            if (errorId == null)
            {
                sql += string.Format(@"SELECT TOP 1 @Id=Id   FROM M_ErrorRecord_T WHERE Area='{0}' AND Line='{1}' AND Status<>'N' AND Status<>'Y'  ", _area, _line);
                if (!string.IsNullOrWhiteSpace(_machine)) sql += string.Format(" AND (Machine=N'{0}' OR Machine=N'换线') ", _machine);
                sql += " ORDER BY StartTime DESC ;";
            }
            else
            {
                sql += string.Format("SET @Id={0};", errorId);
            }
            sql += @"SELECT TOP 1 Id,Area,Line,Dept,Machine,CallReason,MaxHandleTimes,MaxHelpTimes,StartTime,
                                    ComeTime,EndTime,HandlerNo,HelperNo,Status,ErrorReason,FaultType,FaultContent,SolutionContent,
                                    ProdCount,SolverNo,SolverName FROM M_ErrorRecord_T WHERE Id=@Id ;";
            //刷卡人的信息
            sql += @"SELECT  e.ErrorId,e.HandlerNo,e.HandlerName,e.HandlerLevel,e.ErrorStatus,e.CreateTime,h.ImageUpdateTime
                                FROM M_ErrorHandleScan_T e LEFT JOIN M_HandlerInfo_T  h ON e.HandlerNo= h.HandlerNo
                                WHERE e.ErrorId=@Id ORDER BY e.CreateTime ASC";
            DataSet ds = DBUtil.GetDataSet(sql, new string[] { "dtError", "dtScan" });
            if (ds == null)
            {
                timerRefreshErrorInfo.Enabled = true;
                return;
            }
            DataTable dtError = ds.Tables["dtError"];//故障信息
            DataTable dtScan = ds.Tables["dtScan"]; ;//刷卡信息
            //防止因查询失败导致刷新
            ErrorInfo error = null;
            if (dtError.Rows.Count > 0)
            {
                error = MappingErrorInfo(dtError.Rows[0], dtScan);
            }
            LoadErrorInfo(error);
        }
        #endregion

        #region 映射数据到ErrorInfo类
        public ErrorInfo MappingErrorInfo(DataRow eRow, DataTable dtScan)
        {
            ErrorInfo e = null;
            //数据转换
            if (eRow != null)
            {
                e = new ErrorInfo();
                e.Id = Convert.ToInt32(eRow["Id"]);
                e.Area = eRow["Area"].ToString();
                e.Line = eRow["Line"].ToString();
                e.Dept = eRow["Dept"].ToString();
                e.Machine = eRow["Machine"].ToString();
                e.CallReason = eRow["CallReason"].ToString();
                e.MaxHandleTimes = string.IsNullOrWhiteSpace(eRow["MaxHandleTimes"].ToString()) ? 0 : Convert.ToInt32(eRow["MaxHandleTimes"]);
                e.MaxHelpTimes = string.IsNullOrWhiteSpace(eRow["MaxHelpTimes"].ToString()) ? 0 : Convert.ToInt32(eRow["MaxHelpTimes"]);
                e.StartTime = Convert.ToDateTime(eRow["StartTime"]);
                if (string.IsNullOrWhiteSpace(eRow["ComeTime"].ToString()))
                {
                    e.ComeTime = null;
                }
                else
                {
                    e.ComeTime = Convert.ToDateTime(eRow["ComeTime"]);
                }
                if (string.IsNullOrWhiteSpace(eRow["EndTime"].ToString()))
                {
                    e.EndTime = null;
                }
                else
                {
                    e.EndTime = Convert.ToDateTime(eRow["EndTime"]);
                }
                e.HandlerNo = eRow["HandlerNo"].ToString();
                e.HelperNo = eRow["HelperNo"].ToString();
                e.Status = (ErrorStatus)Enum.Parse(typeof(ErrorStatus), eRow["Status"].ToString());
                e.ErrorReason = eRow["ErrorReason"].ToString();
                e.FaultType = eRow["FaultType"].ToString();
                e.FaultContent = eRow["FaultContent"].ToString();
                e.SolutionContent = eRow["SolutionContent"].ToString();
                e.ProdCount = string.IsNullOrWhiteSpace(eRow["ProdCount"].ToString()) ? 0 : Convert.ToInt32(eRow["ProdCount"]);
                e.SolverNo = eRow["SolverNo"].ToString();
                e.SolverName = eRow["SolverName"].ToString();
                //刷卡信息
                if (dtScan != null && dtScan.Rows.Count > 0)
                {
                    foreach (DataRow row in dtScan.Rows)
                    {
                        DateTime? tempImgTimes = null;
                        if (row["ImageUpdateTime"] != DBNull.Value)
                        {
                            tempImgTimes = Convert.ToDateTime(row["ImageUpdateTime"]);
                        }
                        ScanInfo scaninfo = new ScanInfo()
                        {
                            HandlerNo = row["HandlerNo"].ToString(),
                            HandlerName = row["HandlerName"].ToString(),
                            HandlerImageUpdateTime = tempImgTimes,
                            ScanTime = Convert.ToDateTime(row["CreateTime"].ToString())
                        };
                        if ("B".Equals(row["ErrorStatus"]))
                        {
                            e.HandlerScanInfo = scaninfo;
                        }
                        else if ("D".Equals(row["ErrorStatus"]))
                        {
                            e.HelperScanInfo = scaninfo;
                        }
                        else if ("Y".Equals(row["ErrorStatus"]))
                        {
                            e.SolverScanInfo = scaninfo;
                        }
                    }
                }
            }
            return e;
        }

        //加载
        public void LoadErrorInfo(ErrorInfo error)
        {
            _errorInfo = error;
            //刷新控件内容
            RefreshControlText();
            //刷新控件状态
            RefreshControlState();
            //根据故障状态执行相关操作
            ErrorStatusEvent();
        }
        #endregion

        #region 刷新控件文本内容
        private void RefreshControlText()
        {
            if (_errorInfo != null)
            {
                cmbCallReason.Text = _errorInfo.CallReason;
                lsbMachine.SelectedItem = lsbMachine.Items.Cast<DataRowView>().FirstOrDefault(v => v.Row.ItemArray[0].ToString().Equals(_errorInfo.Machine));
                labRequestTime.Text = _errorInfo.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                labComeTime.Text = _errorInfo.ComeTime == null ? "00:00:00" : ((DateTime)_errorInfo.ComeTime).ToString("yyyy-MM-dd HH:mm:ss");
                labFinishTime.Text = _errorInfo.EndTime == null ? "00:00:00" : ((DateTime)_errorInfo.EndTime).ToString("yyyy-MM-dd HH:mm:ss");
                labComeTimes.Text = _errorInfo.ComeTime == null ? "00分钟" : TimeUtil.ConvertDiffTime(_errorInfo.StartTime, (DateTime)_errorInfo.ComeTime);

                try
                {
                    if (_errorInfo.Status == ErrorStatus.A)
                    {//呼叫
                        labHandleTimes.Text = "00分钟";
                        labLimitTimes.Text = "00分钟";
                        labHandler.Text = "";
                        pbHandlerPic.Image = global::CallSys.Properties.Resources.defualt_face;
                    }
                    else if (_errorInfo.Status == ErrorStatus.B || _errorInfo.Status == ErrorStatus.C)
                    {//处理
                        labHandler.Text = _errorInfo.HandlerScanInfo.HandlerName;
                        labHandleTimes.Text = TimeUtil.ConvertDiffTime(_errorInfo.HandlerScanInfo.ScanTime, TimeUtil.Now);
                        labLimitTimes.Text = _errorInfo.MaxHandleTimes <= 0 ? "未限时" : _errorInfo.MaxHandleTimes.ToString() + "分钟";
                        pbHandlerPic.Image = FileUtil.GetCacheFile(_errorInfo.HandlerScanInfo.HandlerNo, (DateTime?)_errorInfo.HandlerScanInfo.HandlerImageUpdateTime);
                    }
                    else if ((_errorInfo.Status == ErrorStatus.D || _errorInfo.Status == ErrorStatus.E) && _errorInfo.HelperScanInfo != null)
                    {//支援
                        labHandler.Text = _errorInfo.HelperScanInfo.HandlerName;
                        labHandleTimes.Text = TimeUtil.ConvertDiffTime(_errorInfo.HelperScanInfo.ScanTime, TimeUtil.Now);
                        labLimitTimes.Text = _errorInfo.MaxHelpTimes <= 0 ? "未限时" : _errorInfo.MaxHelpTimes.ToString() + "分钟";
                        pbHandlerPic.Image = FileUtil.GetCacheFile(_errorInfo.HelperScanInfo.HandlerNo, (DateTime?)_errorInfo.HelperScanInfo.HandlerImageUpdateTime);
                    }
                    else if (_errorInfo.Status == ErrorStatus.N || _errorInfo.Status == ErrorStatus.Y)
                    {//完成
                        labHandler.Text = _errorInfo.SolverScanInfo.HandlerName;
                        labHandleTimes.Text = TimeUtil.ConvertDiffTime(_errorInfo.StartTime, _errorInfo.SolverScanInfo.ScanTime);
                        labLimitTimes.Text = (_errorInfo.MaxHandleTimes + _errorInfo.MaxHelpTimes) <= 0 ? "未限时" : (_errorInfo.MaxHandleTimes + _errorInfo.MaxHelpTimes).ToString() + "分钟";
                        pbHandlerPic.Image = FileUtil.GetCacheFile(_errorInfo.SolverScanInfo.HandlerNo, (DateTime?)_errorInfo.SolverScanInfo.HandlerImageUpdateTime);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error(typeof(FrmMaster), "页面内容数据刷新失败", ex);
                }
            }
        }
        #endregion

        #region 刷新控件状态
        private void RefreshControlState()
        {
            if (_errorInfo != null)
            {
                switch (_errorInfo.Status)
                {
                    case ErrorStatus.A:
                    case ErrorStatus.B:
                        btnCallHandler.Enabled = false;
                        btnCallHandler.BackColor = Color.Gray;
                        btnCallHelper.Enabled = true;
                        btnCallHelper.BackColor = Color.Orange;
                        btnFinish.Enabled = true;
                        btnFinish.BackColor = Color.Green;

                        lsbMachine.Enabled = false;
                        cmbCallReason.Enabled = false;
                        break;

                    case ErrorStatus.C:
                    case ErrorStatus.D:
                        btnCallHandler.Enabled = false;
                        btnCallHandler.BackColor = Color.Gray;
                        btnCallHelper.Enabled = false;
                        btnCallHelper.BackColor = Color.Gray;
                        btnFinish.Enabled = true;
                        btnFinish.BackColor = Color.Green;

                        lsbMachine.Enabled = false;
                        cmbCallReason.Enabled = false;
                        break;

                    case ErrorStatus.E:
                        btnCallHandler.Enabled = false;
                        btnCallHandler.BackColor = Color.Gray;
                        btnCallHelper.Enabled = false;
                        btnCallHelper.BackColor = Color.Gray;
                        btnFinish.Enabled = true;
                        btnFinish.BackColor = Color.Green;

                        lsbMachine.Enabled = false;
                        cmbCallReason.Enabled = false;
                        break;

                    case ErrorStatus.N:
                    case ErrorStatus.Y:
                        btnCallHandler.Enabled = true;
                        btnCallHandler.BackColor = Color.Red;
                        btnCallHelper.Enabled = false;
                        btnCallHelper.BackColor = Color.Gray;
                        btnFinish.Enabled = false;
                        btnFinish.BackColor = Color.Gray;

                        lsbMachine.Enabled = true;
                        cmbCallReason.Enabled = true;
                        labRequestTime.Text = "00:00:00";
                        labComeTime.Text = "00:00:00";
                        labFinishTime.Text = "00:00:00";
                        labComeTimes.Text = "00分钟";
                        labHandleTimes.Text = "00分钟";
                        labLimitTimes.Text = "00分钟";
                        labHandler.Text = "";
                        pbHandlerPic.Image = global::CallSys.Properties.Resources.defualt_face;
                        break;
                }
            }
            else
            {
                //初始化
                btnCallHandler.Enabled = true;
                btnCallHandler.BackColor = Color.Red;
                btnCallHelper.Enabled = false;
                btnCallHelper.BackColor = Color.Gray;
                btnFinish.Enabled = false;
                btnFinish.BackColor = Color.Gray;

                lsbMachine.Enabled = true;
                cmbCallReason.Enabled = true;
                labRequestTime.Text = "00:00:00";
                labComeTime.Text = "00:00:00";
                labFinishTime.Text = "00:00:00";
                labComeTimes.Text = "00分钟";
                labHandleTimes.Text = "00分钟";
                labLimitTimes.Text = "00分钟";
                labHandler.Text = "";
                pbHandlerPic.Image = global::CallSys.Properties.Resources.defualt_face;
            }

        }
        #endregion

        #region 故障状态触发事件
        private void ErrorStatusEvent()
        {
            if (_errorInfo != null)
            {
                //开启定时刷新
                timerRefreshErrorInfo.Enabled = true;
                //开启计时器浮窗
                BaseInfo.CallFlag = true;
                BaseInfo.ClockFlag = true;
                BaseInfo.ClockStartTime = _errorInfo.StartTime;

                //判断是否显示刷卡窗口
                if (_errorInfo.Status == ErrorStatus.A || _errorInfo.Status == ErrorStatus.C || _errorInfo.Status == ErrorStatus.E)
                {//待处理、待支援、待完成
                    ShowScanForm();
                }
                else if (_errorInfo.Status == ErrorStatus.Y && string.IsNullOrWhiteSpace(_errorInfo.ErrorReason))
                {//完成未维护
                    if (FrmBarScan != null && !FrmBarScan.IsDisposed)
                    {
                        FrmBarScan.Close();
                        FrmBarScan.Dispose();
                    }
                    ShowSolutionForm();
                }
                else if (_errorInfo.Status == ErrorStatus.N || _errorInfo.Status == ErrorStatus.Y)
                {//完成
                    //关闭定时器
                    timerRefreshErrorInfo.Enabled = false;
                    //关闭计时浮窗
                    BaseInfo.CallFlag = false;
                    BaseInfo.ClockFlag = false;
                    //清空数据
                    _errorInfo = null;
                    //关闭弹窗
                    if (FrmBarScan != null && !FrmBarScan.IsDisposed)
                    {
                        FrmBarScan.Close();
                    }
                }
            }
            else
            {
                //关闭定时刷新
                timerRefreshErrorInfo.Enabled = false;
                //关闭计时器浮窗
                BaseInfo.CallFlag = false;
                BaseInfo.ClockFlag = false;
                BaseInfo.ClockStartTime = BaseInfo.AppStartTime;

                //关闭弹窗
                if (FrmBarScan != null && !FrmBarScan.IsDisposed)
                {
                    FrmBarScan.Close();
                }
            }
        }
        #endregion

        #region 检查故障是否超时
        public void CheckErrorOverTime(ErrorInfo error)
        {
            if (error != null)
            {
                if (error.Status == ErrorStatus.B && error.MaxHandleTimes > 0 && TimeUtil.Now > error.HandlerScanInfo.ScanTime.AddMinutes(error.MaxHandleTimes))
                {//自动呼叫支援
                    string sql = string.Format("UPDATE M_ErrorRecord_T SET Status='C',HelperNo=null,CallHelpTime=GETDATE(),CallHelpMode='0',UpdateTime=GETDATE()  WHERE Id={0}", error.Id);
                    DBUtil.ExecSQL(sql);
                    //发送微信信息
                    SendWxMessage("1", error);
                }
                else if (error.Status == ErrorStatus.D && error.MaxHelpTimes > 0 && TimeUtil.Now > error.HelperScanInfo.ScanTime.AddMinutes(error.MaxHelpTimes))
                {//支援超时
                    //发送微信信息
                    SendWxMessage("2", error);
                }
            }
        }
        #endregion

        #region 显示刷卡解锁页面
        private void ShowScanForm()
        {
            if (FrmBarScan == null || FrmBarScan.IsDisposed)
            {
                string operate = _errorInfo.Status == ErrorStatus.A ? "呼叫" : _errorInfo.Status == ErrorStatus.C ? "支援" : "完成";
                FrmBarScan = new FrmBarScan();
                FrmBarScan.Owner = this;
                FrmBarScan.SetFormData("阶段:" + operate + "   机台:" + _errorInfo.Machine);
                FrmBarScan.Show();
            }
            else if (!FrmBarScan.Visible)
            {
                FrmBarScan.Show();
            }
            btnCallHandler.Enabled = false;
            btnCallHelper.Enabled = false;
            btnFinish.Enabled = false;
        }
        #endregion

        #region [回调] 关闭刷卡弹窗
        public void ColseScanFrm()
        {
            if (FrmBarScan != null && !FrmBarScan.IsDisposed)
            {
                FrmBarScan.Close();
                FrmBarScan.Dispose();
            }
        }
        #endregion

        #region 显示维护故障弹窗
        private void ShowSolutionForm()
        {
            if (FrmFaultSolution == null || FrmFaultSolution.IsDisposed)
            {
                FrmFaultSolution = new FrmFaultSolution(_errorInfo);
                FrmFaultSolution.Owner = this;
                FrmFaultSolution.ShowDialog();
                RefreshErrorInfo();
            }
            else if (!FrmFaultSolution.Visible)
            {
                FrmFaultSolution.ShowDialog();
                RefreshErrorInfo();
            }
        }
        #endregion

        #region 发送微信消息
        //发送微信消息 ，stageType:触发阶段
        private void SendWxMessage(string stageType, ErrorInfo error)
        {
            string remark = stageType == "1" ? "呼叫支援提醒" : "支援超时提醒";
            //判断是否已发送过消息
            string sql = string.Format("SELECT 1 FROM S_WxMessage_T WHERE ErrorId={0} AND Remark=N'{1}'", error.Id, remark);
            DataTable dtMsg = DBUtil.GetDataTable(sql);
            if (dtMsg == null || dtMsg.Rows.Count > 0) return;

            //是否设置接收人
            sql = string.Format("SELECT WorkCode FROM M_MsgReceiver_T WHERE Area='{0}' AND StageType='{1}'", error.Area, stageType);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt == null || dt.Rows.Count <= 0) return;

            //消息参数
            string workcodes = string.Join(",", dt.AsEnumerable().Select(t => t.Field<string>("WorkCode")).ToList());
            string title = string.Empty;
            string content = string.Empty;

            if (stageType == "1")//呼叫支援
            {
                content = string.Format("<div class='highlight'>机台故障提醒</div><div class='gray'>区域:{0} </div><div class='gray'>线别:{1} </div><div class='gray'>机台:{2} </div><div class='gray'>机故开始时间:{3}</div><div class='gray'>阶段:呼叫支援</div>", error.Area, error.Line, error.Machine, error.StartTime);
            }
            else if (stageType == "2")//支援超时
            {
                content = string.Format("<div class='highlight'>机台故障提醒</div><div class='gray'>区域:{0} </div><div class='gray'>线别:{1} </div><div class='gray'>机台:{2} </div><div class='gray'>机故开始时间:{3}</div><div class='gray'>阶段:支援超时</div>", error.Area, error.Line, error.Machine, error.StartTime);
            }

            //生成发送记录
            sql = string.Format(@"IF NOT EXISTS(SELECT 1 FROM S_WxMessage_T WHERE ErrorId={1} AND Remark=N'{2}')
                                BEGIN 
                                    INSERT INTO S_WxMessage_T([Content],WorkCodes,MsgType,SendTime,ErrorId,Remark) VALUES(@Content,N'{0}','Text',GETDATE(),{1},N'{2}');
                                END", workcodes, error.Id, remark);
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@Content", content);
            int count = DBUtil.ExecSQL(sql, param);
            //生成成功
            if (count > 0)
            {
                //发送消息
                string result = WxMessageHelper.SendTextMessage(workcodes, content);
                //更新发送结果
                sql = string.Format("UPDATE  S_WxMessage_T SET ResultMsg=@ResultMsg WHERE ErrorId={0} AND Remark=N'{1}';", error.Id, remark);
                param.Clear();
                param.Add("@ResultMsg", result);
                DBUtil.ExecSQL(sql, param);
            }
        }
        #endregion

    }
}

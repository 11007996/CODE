using Call;
using Call.Base;
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
using Common.Util;
using Common;
using ApiManager.WxMessage;

namespace Call
{
    public partial class FrmMaster : Form
    {
        public string _Area = BaseInfo.Area;//厂区
        public string _Line = BaseInfo.Line;//线别
        public string _Machine = BaseInfo.Machine;//机台

        private DataTable UserDT;//所有处理人信息
        private DataTable MachineDT;//当前产线关联的机台
        private ErrorInfo _ErrorInfo;//当前呼叫处理的故障信息

        private FrmBarScan FrmBarScan;//刷卡窗体
        private FrmFaultSolution FrmFaultSolution;//故障维护窗体
        private FrmFaultSolutionForTarger FrmFaultSolutionForTarger;//部门人员故障维护窗体
        private FrmMasterError FrmErrorList;//故障列表窗体，没有呼叫限制时显示。


        //窗体位置属性
        private int _X;
        private int _Y;

        public FrmMaster()
        {
            InitializeComponent();
            SetWindowRegion();
            //按钮状态初始化
            btnCall.Enabled = true;
            this.btnCall.BackColor = Color.Red;
            btnFinish.Enabled = false;
            btnHelp.BackColor = Color.Gray;
            btnHelp.Enabled = false;
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
            if (string.IsNullOrWhiteSpace(_Area))
            {
                labMessage.Text = "请在系统设置中设置好区域后重新打开此窗体";
            }
            if (string.IsNullOrWhiteSpace(_Line))
            {
                labMessage.Text = "请在系统设置中设置好线名后重新打开此窗体";
            }
            labAreaLine.Text = "【" + _Area + "区> " + _Line + "】";
            //机台与处理人数据
            try
            {
                string sqlstr = string.Format("SELECT [Machine]  FROM [dbo].[M_LineMachines_T] where [Line]='{0}'", _Line);
                if (!string.IsNullOrWhiteSpace(_Machine))
                    sqlstr += string.Format(" AND Machine=N'{0}'", _Machine);
                sqlstr += ";SELECT UserNo,UserName,Dept,UserState,UserLevel,Area FROM S_User_T WHERE UseFlag='Y';";
                DataSet ds = DBUtil.GetDataSet(sqlstr, new string[] { "MachineDT", "UserDT" });
                if (ds != null)
                {
                    MachineDT = ds.Tables["MachineDT"];
                    UserDT = ds.Tables["UserDT"];
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMaster), null, ex);
            }
            //部门列表
            if (UserDT != null)
            {
                List<string> depts = UserDT.AsEnumerable().Select(t => t.Field<string>("Dept")).ToList();
                depts = depts.Distinct().ToList();
                cmbDept.Items.AddRange(depts.ToArray());
                cmbDept.Text = BaseInfo.Dept;
            }

            cmbCallType.SelectedItem = "机台模组";
            cmbCallReason.SelectedItem = "机台故障";
            //是否有呼叫限制（false:多呼模式）
            if (!BaseInfo.CallLimit)
            {
                FrmErrorList = new FrmMasterError();
                FrmErrorList.Owner = this;
                FrmErrorList.Show();
            }
            else
            {
                //加载故障信息
                if (!string.IsNullOrWhiteSpace(_Area) && !string.IsNullOrWhiteSpace(_Line))
                {
                    RefreshErrorInfo();
                }
            }
        }
       
        #endregion

        #region 呼叫
        private void btnCall_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            btnCall.Enabled = false;
            try
            {
                //检查系统是否有更新(不再检查更新)
                //if (BaseInfo.AutoUpdate && UpdateHelper.CheckMainApp())
                //{
                //    if (MessageBox.Show("呼叫系统有新的版本，需要现在更新吗?", "更新提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                //    {
                //        UpdateHelper.StartUpdateApp(UpdateHelper.START_TYPE_UPDATE);
                //    }
                //    else
                //    {
                //        btnCall.Enabled = true;
                //        return;
                //    }
                //}
                //数据检查
                if (!CheckFormData())
                {
                    btnCall.Enabled = true;
                    return;
                }
                //根据是否有限制，检查是否有未完成的故障或是否重复呼叫。
                if (BaseInfo.CallLimit)
                {
                    //检查是否已有未处理的故障
                    string sqlChk = string.Format(@"SELECT TOP 1 Id,Area,Line,CallType,Dept,Machine,TargetHandler,CallReason,MaxHandleTimes,MaxHelpTimes,StartTime,ComeTime,EndTime,
                                    HandlerNo,HelperNo,Status,ErrorReason,FaultType,FaultContent,SolutionContent,ProdCount,SolverNo,SolverName
                                    FROM M_ErrorRecord_T WHERE  Line='{0}' AND Status<>'N' AND Status<>'Y' ", _Line);
                    if (!string.IsNullOrWhiteSpace(_Machine))
                        sqlChk += string.Format(" AND (Machine=N'{0}' OR Machine=N'换线')", _Machine);
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
                            btnCall.Enabled = true;
                        }
                        return;
                    }
                }
                else
                {
                    if (cmbCallType.Text == "机台模组")
                    {
                        //检查机具是否存在未处理故障
                        string sqlChk2 = string.Format(@"SELECT TOP(1) 1  FROM M_ErrorRecord_T WHERE   Line='{0}' AND Machine=N'{1}' AND Status<>'N' AND Status<>'Y'; ", _Line, lsbTarget.Text.Trim());
                        DataTable dt = DBUtil.GetDataTable(sqlChk2);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            labMessage.Text = "该机具已存在未完成的呼叫记录";
                            btnCall.Enabled = true;
                            return;
                        }
                    }
                    else if (cmbCallType.Text == "部门人员")
                    {
                        //检查机具是否存在未处理故障
                        string sqlChk2 = string.Format(@"SELECT TOP(1) 1  FROM M_ErrorRecord_T WHERE   Line='{0}' AND TargetHandler=N'{1}' AND Status<>'N' AND Status<>'Y'; ", _Line, lsbTarget.SelectedValue.ToString().Trim());
                        DataTable dt = DBUtil.GetDataTable(sqlChk2);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            labMessage.Text = "该人员已存在未完成的呼叫记录";
                            btnCall.Enabled = true;
                            return;
                        }
                    }
                }

                //---------------------------呼叫---------------------------------
                //确定呼叫提示
                if (MessageBox.Show("确定要呼叫吗？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                {
                    btnCall.Enabled = true;
                    return;
                }

                //数据处理
                string callType = cmbCallType.Text.Trim();
                string dept = cmbDept.Text.Trim();
                string callReason = cmbCallReason.Text.Trim();
                string machine = null;
                string targetHandler = null;
                if (callType == "机台模组")
                {
                    machine = lsbTarget.Text.Trim();
                }
                else if (callType == "部门人员")
                {
                    targetHandler = lsbTarget.SelectedValue.ToString().Trim();
                }

                //处理时长限制,0表示不限制。
                int maxHandleTimes = 0;
                int maxHelpTimes = 0;
                if (callType == "机台模组")//机台模组
                {
                    string machineType = lsbTarget.Text.Trim().Split('*')[0];
                    string sqlTimes = string.Format("SELECT MaxHandleTimes,MaxHelpTimes FROM M_FaultSolution_T WHERE MachineType='{0}' AND AutoHelpFlag='Y'", machineType);
                    DataTable dtFHT = DBUtil.GetDataTable(sqlTimes);
                    if (dtFHT != null && dtFHT.Rows.Count == 1)
                    {
                        maxHandleTimes = Int32.Parse(dtFHT.Rows[0]["MaxHandleTimes"].ToString());
                        maxHelpTimes = Int32.Parse(dtFHT.Rows[0]["MaxHelpTimes"].ToString());
                    }
                }

                //呼叫电脑信息
                string PCIP = SystemInfoUtil.GetClientLocalIPv4Address();//当前电脑IP
                string PCMac = SystemInfoUtil.getMacAddr_Local();//当前电脑Mac
                string PCUser = SystemInfoUtil.GetUserName();//当前电脑用户

                string sql = string.Empty;
                //增加故障记录
                if (callType == "机台模组")
                {
                    sql = string.Format(@"INSERT INTO M_ErrorRecord_T
                                    (Area,Line,Dept,Machine,MaxHandleTimes,MaxHelpTimes,CallReason,Status,StartTime,PCIP,PCMac,PCUser,UpdateTime) OUTPUT inserted.Id 
                                     VALUES (N'{0}',N'{1}',N'{2}',N'{3}','{4}','{5}',N'{6}','{7}',GETDATE(),'{8}','{9}','{10}',GETDATE())",
                                    _Area, _Line, dept, machine, maxHandleTimes, maxHelpTimes, callReason, "A", PCIP, PCMac, PCUser);
                }
                else if (callType == "部门人员")
                {
                    if (string.IsNullOrWhiteSpace(_Machine))//是否具体到某个机台
                    {
                        sql = string.Format(@"INSERT INTO M_ErrorRecord_T
                                    (Area,Line,Dept,TargetHandler,MaxHandleTimes,MaxHelpTimes,CallReason,Status,StartTime,PCIP,PCMac,PCUser,UpdateTime) OUTPUT inserted.Id 
                                     VALUES (N'{0}',N'{1}',N'{2}','{3}','{4}','{5}',N'{6}','{7}',GETDATE(),'{8}','{9}','{10}',GETDATE())",
                                    _Area, _Line, dept, targetHandler, maxHandleTimes, maxHelpTimes, callReason, "A", PCIP, PCMac, PCUser);
                    }
                    else
                    {
                        sql = string.Format(@"INSERT INTO M_ErrorRecord_T
                                    (Area,Line,Dept,TargetHandler,Machine,MaxHandleTimes,MaxHelpTimes,CallReason,Status,StartTime,PCIP,PCMac,PCUser,UpdateTime) OUTPUT inserted.Id 
                                     VALUES (N'{0}',N'{1}',N'{2}','{3}',N'{4}','{5}','{6}',N'{7}','{8}',GETDATE(),'{9}','{10}','{11}',GETDATE())",
                                     _Area, _Line, dept, targetHandler, _Machine, maxHandleTimes, maxHelpTimes, callReason, "A", PCIP, PCMac, PCUser);
                    }
                }

                //插入并反回插入后生成的自增ID
                DataTable dtId = DBUtil.GetDataTable(sql);
                if (dtId == null || dtId.Rows.Count <= 0)
                {
                    labMessage.Text = "提示：呼叫失败（插入数据失败）";
                    btnCall.Enabled = true;
                    return;
                }
                int errorId = Int32.Parse(dtId.Rows[0][0].ToString());

                //加载数据
                RefreshErrorInfo(errorId);
                //呼叫指定人员，推送企业微信消息
                if (!string.IsNullOrWhiteSpace(_ErrorInfo.TargetHandler))
                {
                    SendWxMessage("0", _ErrorInfo);
                }
                if (!BaseInfo.CallLimit)
                {
                    FrmErrorList.SelectedErrorId = errorId;
                    FrmErrorList.RefreshError();
                }
            }
            catch (Exception ex)
            {
                btnCall.Enabled = true;
                LogHelper.Error(typeof(FrmMaster), null, ex);
                return;
            }
        }
        #endregion

        #region 支援
        private void btnCallHelper_Click(object sender, EventArgs e)
        {
            btnHelp.Enabled = false;
            try
            {
                //数据检查
                labMessage.Text = "";
                labMessage.ForeColor = Color.Red;
                if (_ErrorInfo == null)
                {
                    labMessage.Text = "未找到故障信息";
                    btnHelp.Enabled = true;
                    return;
                }
                if (string.IsNullOrWhiteSpace(_ErrorInfo.HandlerNo))
                {
                    labMessage.Text = "还未开始处理。";
                    btnHelp.Enabled = true;
                    return;
                }
                //更新状态
                string sql = string.Format("UPDATE M_ErrorRecord_T SET Status='C',HelperNo=null,CallHelpTime=GETDATE(),CallHelpMode='1',UpdateTime=GETDATE()  WHERE Id={0}", _ErrorInfo.Id);
                DBUtil.ExecSQL(sql);

                //发送微信信息
                SendWxMessage("1", _ErrorInfo);

                //刷卡显示
                RefreshErrorInfo(_ErrorInfo.Id);
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
                string sql = string.Format("UPDATE M_ErrorRecord_T SET Status='E',SolverNo=null,UpdateTime=GETDATE()  WHERE Id={0}", _ErrorInfo.Id);
                DBUtil.ExecSQL(sql);
                RefreshErrorInfo(_ErrorInfo.Id);
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

        #region 定时任务
        //每分钟刷新一次
        private void timerRefreshErrorInfo_Tick(object sender, EventArgs e)
        {
            if (_ErrorInfo != null && DBUtil.DBConnState)
            {
                CheckErrorOverTime(_ErrorInfo);
                RefreshErrorInfo(_ErrorInfo.Id);
            }
        }
        #endregion

        #region 检查数据
        //新增呼叫时检查数据是否合法
        private bool CheckFormData()
        {
            string msg = "";
            if (_ErrorInfo != null && _ErrorInfo.Status != ErrorStatus.N && _ErrorInfo.Status != ErrorStatus.Y)
            {
                msg = "当前已有存在呼叫记录，请误重复操作。";
            }
            else if (string.IsNullOrWhiteSpace(_Area))
            {
                msg = "厂区不能为空";
            }
            else if (string.IsNullOrWhiteSpace(_Line))
            {
                msg = "线别不能为空";
            }
            else if (cmbDept.Text.Trim() == "")
            {
                msg = "请选择呼叫部门";
            }
            else if (cmbCallReason.Text.Trim() == "")
            {
                msg = "请选择呼叫原因";
            }
            else if (cmbCallType.Text.Trim() == "")
            {
                msg = "请选择呼叫目标类型";
            }
            else if (lsbTarget.SelectedItem == null)
            {
                msg = "请选择模组或人员";
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
                if (_ErrorInfo == null) return true;
                //刷卡人信息
                string strSQL = string.Format("SELECT UserNo,UserName,UserLevel,UseFlag FROM S_User_T WHERE UserNo='{0}'", cardNo);
                DataTable userDT = DBUtil.GetDataTable(strSQL);
                if (userDT == null)
                {
                    msg = "检查网络";
                    return false;
                }
                if (userDT.Rows.Count != 1)
                {
                    msg = "警告！此工号不存在,非法进入！";
                    return false;
                }
                if (!"Y".Equals(userDT.Rows[0]["UseFlag"]))
                {
                    msg = "警告！此工号不可用！";
                    return false;
                }
                string userName = userDT.Rows[0]["UserName"].ToString();
                string userLevel = userDT.Rows[0]["UserLevel"].ToString();

                //获取最新状态
                strSQL = string.Format("SELECT * FROM M_ErrorRecord_T WHERE Id={0}", _ErrorInfo.Id);
                DataTable dt = DBUtil.GetDataTable(strSQL);
                if (dt != null && dt.Rows.Count == 1)
                {
                    _ErrorInfo.Status = (ErrorStatus)Enum.Parse(typeof(ErrorStatus), dt.Rows[0]["Status"].ToString());
                }
                //如果指定了人员，则只能输入指定人员的工号
                if (dt.Rows[0]["TargetHandler"] != DBNull.Value && cardNo != dt.Rows[0]["TargetHandler"].ToString())
                {
                    msg = "请输入指定人员工号";
                    return false;
                }

                //根据操作状态更新数据
                if (_ErrorInfo.Status == ErrorStatus.A)
                {
                    strSQL = string.Format(@"INSERT INTO M_ErrorHandleScan_T(ErrorId,HandlerNo,HandlerName,HandlerLevel,ErrorStatus,CreateTime)VALUES('{0}','{1}',N'{2}','{3}',N'B',GETDATE());
                         UPDATE M_ErrorRecord_T SET HandlerNo=N'{1}',ComeTime=GETDATE(), Status=N'B',UpdateUser=N'{1}',UpdateTime=GETDATE() WHERE Id='{0}';
                         UPDATE S_User_T SET UserState = 'H',UpdateUser=N'{1}',UpdateTime=GETDATE() WHERE UserNo = '{1}'; ", _ErrorInfo.Id, cardNo, userName, userLevel);
                    DBUtil.ExecSQL(strSQL);
                }
                else if (_ErrorInfo.Status == ErrorStatus.C)
                {
                    if (cardNo == _ErrorInfo.HandlerNo || cardNo == _ErrorInfo.HelperNo)
                    {
                        msg = "已是处理人，不可再刷卡。";
                    }
                    else
                    {
                        strSQL = string.Format(@"INSERT INTO M_ErrorHandleScan_T(ErrorId,HandlerNo,HandlerName,HandlerLevel,ErrorStatus,CreateTime)VALUES('{0}','{1}',N'{2}','{3}',N'D',GETDATE());
                             UPDATE M_ErrorRecord_T SET HelperNo=N'{1}',Status='D',UpdateUser=N'{1}',UpdateTime=GETDATE() WHERE Id='{0}';
                             UPDATE S_User_T SET UserState = N'H',UpdateUser=N'{1}',UpdateTime=GETDATE() WHERE UserNo = '{1}';", _ErrorInfo.Id, cardNo, userName, userLevel);
                        DBUtil.ExecSQL(strSQL);
                    }
                }
                else if (_ErrorInfo.Status == ErrorStatus.E)
                {
                    strSQL = string.Format("SELECT TOP 1 * FROM M_ErrorHandleScan_T WHERE ErrorId={0} AND HandlerNo=N'{1}' ORDER BY CreateTime DESC", _ErrorInfo.Id, cardNo);
                    DataTable scanDT = DBUtil.GetDataTable(strSQL);
                    if (scanDT != null && scanDT.Rows.Count <= 0)
                    {
                        msg = "非处理人或支援人无法确认完成";
                    }
                    else
                    {
                        strSQL = string.Format(@"INSERT INTO M_ErrorHandleScan_T(ErrorId,HandlerNo,HandlerName,HandlerLevel,ErrorStatus,CreateTime)VALUES('{0}','{1}',N'{2}','{3}',N'Y',GETDATE());
                             UPDATE M_ErrorRecord_T SET Status='Y',SolverNo=N'{1}',SolverName=N'{2}',EndTime=GETDATE(),UpdateUser=N'{1}',UpdateTime=GETDATE()  WHERE Id='{0}';
                             UPDATE S_User_T SET UserState = 'W' WHERE UserNo = '{1}';", _ErrorInfo.Id, cardNo, userName, userLevel);
                        DBUtil.ExecSQL(strSQL);
                    }
                }
                //刷卡成功
                if (msg.Length <= 0)
                {
                    RefreshErrorInfo(_ErrorInfo.Id);
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
            // 查检数据库连接
            if (DBUtil.DBConnState == false)
            {
                string r = DBUtil.CheckServerConnState();
                if (!string.IsNullOrWhiteSpace(r))
                {
                    return;
                    //    new FrmMsgDialog("服务器连接异常", "原因:" + r).Show();
                }
            }

            timerRefreshErrorInfo.Enabled = false;

            string sql = string.Empty;
            sql = "DECLARE @Id int;";
            if (errorId == null)
            {
                sql += string.Format(@"SELECT TOP 1 @Id=Id   FROM M_ErrorRecord_T WHERE Area='{0}' AND Line='{1}' AND Status<>'N' AND Status<>'Y'  ", _Area, _Line);
                if (!string.IsNullOrWhiteSpace(_Machine)) sql += string.Format(" AND (Machine=N'{0}' OR Machine=N'换线') ", _Machine);
                sql += " ORDER BY StartTime DESC ;";
            }
            else
            {
                sql += string.Format("SET @Id={0};", errorId);
            }
            sql += @"SELECT TOP 1 Id,e.Area,e.Line,e.Dept,e.TargetHandler,h.UserName TargetHandlerName,Machine,CallReason,MaxHandleTimes,MaxHelpTimes,StartTime,
                                    ComeTime,EndTime,e.HandlerNo,e.HelperNo,Status,ErrorReason,FaultType,FaultContent,SolutionContent,
                                    ProdCount,SolverNo,SolverName FROM M_ErrorRecord_T e
                                    LEFT JOIN S_User_T h ON e.TargetHandler=h.UserNo
                                    WHERE Id=@Id ;";
            //刷卡人的信息
            sql += @"SELECT  ErrorId,HandlerNo,HandlerName,HandlerLevel,ErrorStatus,CreateTime
                                FROM M_ErrorHandleScan_T WHERE ErrorId=@Id ORDER BY CreateTime ASC";
            DataSet ds = DBUtil.GetDataSet(sql, new string[] { "dtError", "dtScan" });
            if (ds == null)
            {
                timerRefreshErrorInfo.Enabled = true;
                return;
            }
            DataTable dtError = ds.Tables["dtError"];//故障信息
            DataTable dtScan = ds.Tables["dtScan"]; //刷卡信息
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
                e.TargetHandler = eRow["TargetHandler"].ToString();
                e.TargetHandlerName = eRow["TargetHandlerName"].ToString();
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
                        ScanInfo scaninfo = new ScanInfo()
                        {
                            UserNo = row["HandlerNo"].ToString(),
                            UserName = row["HandlerName"].ToString(),
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
            _ErrorInfo = error;
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
            if (_ErrorInfo != null)
            {
                cmbCallReason.Text = _ErrorInfo.CallReason;
                cmbDept.Text = _ErrorInfo.Dept;
                if (!string.IsNullOrWhiteSpace(_ErrorInfo.TargetHandler))
                {
                    cmbCallType.SelectedItem = "部门人员";
                    lsbTarget.SelectedValue = _ErrorInfo.TargetHandler;
                }
                else
                {
                    cmbCallType.SelectedItem = "机台模组";
                    lsbTarget.SelectedItem = lsbTarget.Items.Cast<DataRowView>().FirstOrDefault(v => v.Row.ItemArray[0].ToString().Equals(_ErrorInfo.Machine));

                }
                labRequestTime.Text = _ErrorInfo.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                labComeTime.Text = _ErrorInfo.ComeTime == null ? "00:00:00" : ((DateTime)_ErrorInfo.ComeTime).ToString("yyyy-MM-dd HH:mm:ss");
                labFinishTime.Text = _ErrorInfo.EndTime == null ? "00:00:00" : ((DateTime)_ErrorInfo.EndTime).ToString("yyyy-MM-dd HH:mm:ss");
                labComeTimes.Text = _ErrorInfo.ComeTime == null ? "00分钟" : TimeUtil.ConvertDiffTime(_ErrorInfo.StartTime, (DateTime)_ErrorInfo.ComeTime);

                try
                {
                    if (_ErrorInfo.Status == ErrorStatus.A)
                    {//呼叫
                        labHandleTimes.Text = "00分钟";
                        labLimitTimes.Text = "00分钟";
                        labUser.Text = "";
                        pbUserPic.Image = global::Call.Properties.Resources.defualt_face;
                    }
                    else if (_ErrorInfo.Status == ErrorStatus.B || _ErrorInfo.Status == ErrorStatus.C)
                    {//处理
                        labUser.Text = "处理者:" + _ErrorInfo.HandlerScanInfo.UserName;
                        labHandleTimes.Text = TimeUtil.ConvertDiffTime(_ErrorInfo.HandlerScanInfo.ScanTime, TimeUtil.Now);
                        labLimitTimes.Text = _ErrorInfo.MaxHandleTimes <= 0 ? "未限时" : _ErrorInfo.MaxHandleTimes.ToString() + "分钟";
                        pbUserPic.Image = UserImageUtil.GetCacheFile(_ErrorInfo.HandlerScanInfo.UserNo);
                    }
                    else if ((_ErrorInfo.Status == ErrorStatus.D || _ErrorInfo.Status == ErrorStatus.E) && _ErrorInfo.HelperScanInfo != null)
                    {//支援
                        labUser.Text = "支援者:" + _ErrorInfo.HelperScanInfo.UserName;
                        labHandleTimes.Text = TimeUtil.ConvertDiffTime(_ErrorInfo.HelperScanInfo.ScanTime, TimeUtil.Now);
                        labLimitTimes.Text = _ErrorInfo.MaxHelpTimes <= 0 ? "未限时" : _ErrorInfo.MaxHelpTimes.ToString() + "分钟";
                        pbUserPic.Image = UserImageUtil.GetCacheFile(_ErrorInfo.HelperScanInfo.UserNo);
                    }
                    else if (_ErrorInfo.Status == ErrorStatus.N || _ErrorInfo.Status == ErrorStatus.Y)
                    {//完成
                        labUser.Text = "完成者:" + _ErrorInfo.SolverScanInfo.UserName;
                        labHandleTimes.Text = TimeUtil.ConvertDiffTime(_ErrorInfo.StartTime, _ErrorInfo.SolverScanInfo.ScanTime);
                        labLimitTimes.Text = (_ErrorInfo.MaxHandleTimes + _ErrorInfo.MaxHelpTimes) <= 0 ? "未限时" : (_ErrorInfo.MaxHandleTimes + _ErrorInfo.MaxHelpTimes).ToString() + "分钟";
                        pbUserPic.Image = UserImageUtil.GetCacheFile(_ErrorInfo.SolverScanInfo.UserNo);
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
            if (_ErrorInfo != null)
            {
                switch (_ErrorInfo.Status)
                {
                    case ErrorStatus.A:
                    case ErrorStatus.B:
                        btnCall.Enabled = false;
                        btnCall.BackColor = Color.Gray;
                        btnHelp.Enabled = true;
                        btnHelp.BackColor = Color.Orange;
                        if (!string.IsNullOrWhiteSpace(_ErrorInfo.TargetHandler))
                        {
                            btnHelp.Enabled = false;
                            btnHelp.BackColor = Color.Gray;
                        }
                        btnFinish.Enabled = true;
                        btnFinish.BackColor = Color.Green;

                        cmbCallType.Enabled = false;
                        lsbTarget.Enabled = false;
                        cmbCallReason.Enabled = false;
                        cmbDept.Enabled = false;
                        break;

                    case ErrorStatus.C:
                    case ErrorStatus.D:
                        btnCall.Enabled = false;
                        btnCall.BackColor = Color.Gray;
                        btnHelp.Enabled = false;
                        btnHelp.BackColor = Color.Gray;
                        btnFinish.Enabled = true;
                        btnFinish.BackColor = Color.Green;

                        cmbCallType.Enabled = false;
                        lsbTarget.Enabled = false;
                        cmbCallReason.Enabled = false;
                        cmbDept.Enabled = false;
                        break;

                    case ErrorStatus.E:
                        btnCall.Enabled = false;
                        btnCall.BackColor = Color.Gray;
                        btnHelp.Enabled = false;
                        btnHelp.BackColor = Color.Gray;
                        btnFinish.Enabled = true;
                        btnFinish.BackColor = Color.Green;

                        cmbCallType.Enabled = false;
                        lsbTarget.Enabled = false;
                        cmbCallReason.Enabled = false;
                        cmbDept.Enabled = false;
                        break;

                    case ErrorStatus.N:
                    case ErrorStatus.Y:
                        btnCall.Enabled = true;
                        btnCall.BackColor = Color.Red;
                        btnHelp.Enabled = false;
                        btnHelp.BackColor = Color.Gray;
                        btnFinish.Enabled = false;
                        btnFinish.BackColor = Color.Gray;

                        cmbCallType.Enabled = true;
                        lsbTarget.Enabled = true;
                        cmbCallReason.Enabled = true;
                        cmbDept.Enabled = true;
                        labRequestTime.Text = "00:00:00";
                        labComeTime.Text = "00:00:00";
                        labFinishTime.Text = "00:00:00";
                        labComeTimes.Text = "00分钟";
                        labHandleTimes.Text = "00分钟";
                        labLimitTimes.Text = "00分钟";
                        labUser.Text = "";
                        pbUserPic.Image = global::Call.Properties.Resources.defualt_face;
                        break;
                }
            }
            else
            {
                //初始化
                btnCall.Enabled = true;
                btnCall.BackColor = Color.Red;
                btnHelp.Enabled = false;
                btnHelp.BackColor = Color.Gray;
                btnFinish.Enabled = false;
                btnFinish.BackColor = Color.Gray;

                cmbCallType.Enabled = true;
                lsbTarget.Enabled = true;
                cmbCallReason.Enabled = true;
                cmbDept.Enabled = true;
                labRequestTime.Text = "00:00:00";
                labComeTime.Text = "00:00:00";
                labFinishTime.Text = "00:00:00";
                labComeTimes.Text = "00分钟";
                labHandleTimes.Text = "00分钟";
                labLimitTimes.Text = "00分钟";
                labUser.Text = "处理者:";
                pbUserPic.Image = null;
                if (cmbCallType.Text == "机台模组")
                {
                    cmbDept.Text = BaseInfo.Dept;
                    cmbDept.Enabled = false;
                }
                else if (cmbCallType.Text == "部门人员")
                {
                    cmbDept.Enabled = true;
                }
            }

        }
        #endregion

        #region 故障状态触发事件
        private void ErrorStatusEvent()
        {
            if (_ErrorInfo != null)
            {
                //开启定时刷新
                timerRefreshErrorInfo.Enabled = true;
                //开启计时器浮窗
                BaseInfo.CallFlag = true;
                BaseInfo.ClockFlag = true;
                BaseInfo.ClockStartTime = _ErrorInfo.StartTime;

                //判断是否显示刷卡窗口
                if (_ErrorInfo.Status == ErrorStatus.A || _ErrorInfo.Status == ErrorStatus.C || _ErrorInfo.Status == ErrorStatus.E)
                {//待处理、待支援、待完成
                    ShowScanForm();
                }
                else if (_ErrorInfo.Status == ErrorStatus.Y && string.IsNullOrWhiteSpace(_ErrorInfo.ErrorReason))
                {//完成未维护
                    if (FrmBarScan != null && !FrmBarScan.IsDisposed)
                    {
                        FrmBarScan.Close();
                        FrmBarScan.Dispose();
                    }
                    if (!string.IsNullOrWhiteSpace(_ErrorInfo.Machine) && string.IsNullOrWhiteSpace(_ErrorInfo.TargetHandler))
                        ShowSolutionForm();
                    else
                        ShowSolutionFormForTarger();
                }
                else if (_ErrorInfo.Status == ErrorStatus.N || _ErrorInfo.Status == ErrorStatus.Y)
                {//完成
                    //关闭定时器
                    timerRefreshErrorInfo.Enabled = false;
                    //关闭计时浮窗
                    BaseInfo.CallFlag = false;
                    BaseInfo.ClockFlag = false;
                    //清空数据
                    _ErrorInfo = null;
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
                if (!string.IsNullOrWhiteSpace(error.TargetHandler) && error.Status == ErrorStatus.A && TimeUtil.Now > error.StartTime.AddMinutes(10))
                {//呼叫人员时，超过5分钟没人处理，再推送一次微信消息
                    SendWxMessage("3", error);
                }
                else if (error.Status == ErrorStatus.B && error.MaxHandleTimes > 0 && TimeUtil.Now > error.HandlerScanInfo.ScanTime.AddMinutes(error.MaxHandleTimes))
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
                string operate = _ErrorInfo.Status == ErrorStatus.A ? "呼叫" : _ErrorInfo.Status == ErrorStatus.C ? "支援" : "完成";
                string machineTip = string.IsNullOrWhiteSpace(_ErrorInfo.Machine) ? null : "机台:" + _ErrorInfo.Machine;
                string handlerTip = string.IsNullOrWhiteSpace(_ErrorInfo.TargetHandlerName) ? null : "人员:" + _ErrorInfo.TargetHandlerName;
                FrmBarScan = new FrmBarScan();
                FrmBarScan.Owner = this;
                FrmBarScan.SetFormData("阶段:" + operate, machineTip, handlerTip);
                FrmBarScan.Show();
            }
            else if (!FrmBarScan.Visible)
            {
                FrmBarScan.Show();
            }
            btnCall.Enabled = false;
            btnHelp.Enabled = false;
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
                FrmFaultSolution = new FrmFaultSolution(_ErrorInfo);
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

        private void ShowSolutionFormForTarger()
        {
            if (FrmFaultSolutionForTarger == null || FrmFaultSolutionForTarger.IsDisposed)
            {
                FrmFaultSolutionForTarger = new FrmFaultSolutionForTarger(_ErrorInfo);
                FrmFaultSolutionForTarger.Owner = this;
                FrmFaultSolutionForTarger.ShowDialog();
                RefreshErrorInfo();
            }
            else if (!FrmFaultSolutionForTarger.Visible)
            {
                FrmFaultSolutionForTarger.ShowDialog();
                RefreshErrorInfo();
            }
        }
        #endregion

        #region 发送微信消息
        //发送微信消息 ，stageType:触发阶段,0:呼叫，1：支援,2支援超时
        private void SendWxMessage(string stageType, ErrorInfo error)
        {
            string remark = "呼叫提醒";
            switch (stageType)
            {
                case "0": remark = "呼叫提醒";
                    break;
                case "1": remark = "呼叫支援提醒";
                    break;
                case "2": remark = "支援超时提醒";
                    break;
                case "3": remark = "超时未接单";
                    break;
            }
            string sql = string.Format("SELECT 1 FROM S_WxMessage_T WHERE ErrorId={0} AND Remark=N'{1}'", error.Id, remark);
            DataTable dtMsg = DBUtil.GetDataTable(sql);
            if (dtMsg == null || dtMsg.Rows.Count > 0) return;

            //消息接收人
            string workcodes = string.Empty;
            string title = string.Empty;
            string content = string.Empty;
            if (stageType == "0")
            {
                workcodes = error.TargetHandler;
            }
            else
            {
                if (stageType == "1" || stageType == "2")
                {//机台故障
                    sql = string.Format("SELECT WorkCode FROM M_MsgReceiver_T WHERE Area='{0}' AND StageType='{1}'", error.Area, stageType);
                }
                else
                {//指定人员
                    sql = string.Format("SELECT WorkCode FROM M_MsgReceiver_T WHERE Dept='{0}' AND StageType='{1}'", error.Dept, stageType);
                }
                DataTable dtWorkCodes = DBUtil.GetDataTable(sql);
                if (dtWorkCodes == null || dtWorkCodes.Rows.Count <= 0) return;
                workcodes = string.Join(",", dtWorkCodes.AsEnumerable().Select(t => t.Field<string>("WorkCode")).ToList());
            }
            //没有找到推送对象
            if (string.IsNullOrWhiteSpace(workcodes)) return;

            if (stageType == "0")//呼叫指定人员
            {
                content = string.Format("<div class='highlight'>呼叫提醒</div><div class='gray'>区域:{0} </div><div class='gray'>线别:{1} </div><div class='gray'>产品异常</div><div class='gray'>呼叫开始时间:{2}</div><div class='gray'>阶段:呼叫</div>", error.Area, error.Line, error.StartTime);
            }
            if (stageType == "3")//呼叫指定人员超时
            {
                content = string.Format("<div class='highlight'>呼叫人员提醒</div><div class='gray'>区域:{0} </div><div class='gray'>线别:{1} </div><div class='gray'>人员:{2}</div><div class='gray'>呼叫开始时间:{3}</div><div class='gray'>阶段:超时未接单</div>", error.Area, error.Line, error.TargetHandlerName, error.StartTime);
            }
            else if (stageType == "1")//呼叫支援
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

        #region 其他事件
        //切换故障类型
        private void cmbCallReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("机台模组".Equals(cmbCallType.Text))
            {
                if ("换线".Equals(cmbCallReason.Text))
                {
                    lsbTarget.DataSource = null;
                    DataTable dt = new DataTable();
                    DataColumn col = new DataColumn("Machine");
                    dt.Columns.Add(col);
                    DataRow row = dt.NewRow();
                    row[0] = "换线";
                    dt.Rows.Add(row);
                    lsbTarget.DataSource = dt;
                    lsbTarget.DisplayMember = "Machine";
                    lsbTarget.ValueMember = "Machine";
                    lsbTarget.SelectedIndex = 0;
                }
                else
                {
                    lsbTarget.DataSource = MachineDT;
                    lsbTarget.ValueMember = "Machine";
                    lsbTarget.DisplayMember = "Machine";
                }
            }
        }

        //焦点事件
        private void FrmMaster_Activated(object sender, EventArgs e)
        {
              BaseInfo.CurrFrmType = typeof(FrmMaster);

        }

        //关闭窗体事件
        private void FrmMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ErrorInfo == null || _ErrorInfo.Status == ErrorStatus.Y || _ErrorInfo.Status == ErrorStatus.N)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("程序有任务在进行中无法关闭", "错误操作", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //部门切换事件
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCallType.Text == "部门人员")
            {
                lsbTarget.DataSource = null;
                string where = string.Format("Dept='{0}'", cmbDept.Text);
                DataRow[] rows = UserDT.Select(where);
                Dictionary<string, string> handleDic = new Dictionary<string, string>();
                foreach (DataRow row in rows)
                {
                    handleDic.Add(row["UserNo"].ToString(), row["UserName"].ToString());
                }
                lsbTarget.DataSource = handleDic.AsEnumerable<KeyValuePair<string, string>>().ToList();
                lsbTarget.ValueMember = "Key";
                lsbTarget.DisplayMember = "Value";
            }
        }

        //呼叫类型切换事件
        private void cmbCallType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsbTarget.DataSource = null;
            if (cmbCallType.Text == "机台模组")
            {
                cmbCallReason.Text = "机台故障";
                cmbCallReason_SelectedIndexChanged(null, null);
                cmbCallReason.Enabled = true;
                cmbDept.Text = BaseInfo.Dept;
                cmbDept.Enabled = false;
            }
            else if (cmbCallType.Text == "部门人员")
            {
                cmbDept_SelectedIndexChanged(null, null);
                cmbDept.Enabled = true;
                cmbCallReason.Text = "产品异常";
                cmbCallReason.Enabled = false;
            }
            lsbTarget.ClearSelected();
        }
        #endregion

    }
}

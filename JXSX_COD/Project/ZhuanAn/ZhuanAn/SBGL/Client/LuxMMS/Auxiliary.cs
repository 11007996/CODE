using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Machine;
using Common.Util;
using Common;
using Common.Base;
using Call;
using LuxMMS.Base;



namespace LuxMMS
{
    public partial class Auxiliary : Form
    {
        #region 窗体外观属性
        private int currentX;//横坐标      
        private int currentY;//纵坐标      
        private int screenHeight;//屏幕高度      
        private int screenWidth;//屏幕宽度
        private int _X;
        private int _Y;
        #endregion

        private FrmCheckRight FrmCheckRight;//权限验证窗体

        public Auxiliary()
        {
            InitializeComponent();
        }

        #region 窗体加载
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Auxiliary_Load(object sender, EventArgs e)
        {
            //设置通知栏提示文本
            RefreshNotifyIconText();

            //设置成圆形
            System.Drawing.Drawing2D.GraphicsPath myPath = new System.Drawing.Drawing2D.GraphicsPath();
            myPath.AddEllipse(4, 5, 114, 112);
            this.Region = new Region(myPath);

            //设置图片成圆形
            setRectangle(pbCall);

            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            screenHeight = rect.Height;
            screenWidth = rect.Width;
            currentX = screenWidth - this.Width;
            currentY = screenHeight - this.Height;
            this.Location = new System.Drawing.Point(currentX, currentY);

            if (BaseInfo.CurrFrmType != null)
            {
                SwitchFormShow();
            }

            //检查用户头像是否更新
            Call.Base.UserImageUtil.UpdateAllUserImage();
        }

        /// <summary>
        /// 让图片设置成圆形
        /// </summary>
        /// <param name="c"></param>
        private void setRectangle(Control c)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(c.ClientRectangle);
            Region region = new Region(gp);
            c.Region = region;
            gp.Dispose();
            region.Dispose();
        }

        //设置通知栏图标的提示文本
        public void RefreshNotifyIconText()
        {
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                notifyIcon.Text = "设备系统" + Application.ProductVersion + "\r\n服务器IP:" + DBUtil.GetServerIP() + "\r\n用户:未登入";
                return;
            }
            //设置通知栏提示文本
            string rightText = BaseInfo.LoginUserRight == "A" ? "超级管理员" : BaseInfo.LoginUserRight == "B" ? "一般管理员" : "";
            string text = string.Format("设备系统{0}\r\n服务器IP:{1}\r\n用户:{2}({3})\r\n权限:{4}", Application.ProductVersion, DBUtil.GetServerIP(), BaseInfo.LoginUserNo, BaseInfo.LoginUserName, rightText);
            notifyIcon.Text = text;
        }
        #endregion

        #region 移动浮窗
        /// <summary>
        /// 悬浮窗移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Auxiliary_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _X + _Y != 0)
            {
                this.Left += e.X - _X;
                this.Top += e.Y - _Y;
            }
            else
            {
                _X = e.X;
                _Y = e.Y;
            }
        }
        #endregion

        #region 鼠标进入、离开控件事件
        // 看板按钮,鼠标进入事件
        private void btnKanBan_MouseEnter(object sender, EventArgs e)
        {
            this.btnKanBan.BackgroundImage = null;
            //this.btnKanBan.BackColor = System.Drawing.Color.Green;
            //btnKanBan.FlatStyle = FlatStyle.Flat;
        }

        // 看板按钮,鼠标离开事件
        private void btnKanBan_MouseLeave(object sender, EventArgs e)
        {
            this.btnKanBan.BackgroundImage = global::LuxMMS.Properties.Resources.btnLeft;
            //this.btnKanBan.BackColor = System.Drawing.Color.IndianRed;
            //btnKanBan.BackColor = Color.IndianRed;
        }

        // 呼叫图片,鼠标进入事件
        private void pbCall_MouseEnter(object sender, EventArgs e)
        {
            panelClock.BackColor = Color.MidnightBlue;
            pbCall.Image = global::LuxMMS.Properties.Resources.call_enter;
        }

        // 呼叫图片,鼠标离开事件
        private void pbCall_MouseLeave(object sender, EventArgs e)
        {
            panelClock.BackColor = Color.MidnightBlue;
            pbCall.Image = global::LuxMMS.Properties.Resources.call_leave;
        }

        // 基础信息按钮,鼠标进入事件
        private void btnBaseInfo_MouseEnter(object sender, EventArgs e)
        {
            this.btnBaseInfo.BackgroundImage = null;
            //this.btnBaseInfo.BackColor = System.Drawing.Color.Green;
            //btnBaseInfo.FlatStyle = FlatStyle.Flat;
        }

        // 基础信息按钮,鼠标离开事件
        private void btnBaseInfo_MouseLeave(object sender, EventArgs e)
        {
            this.btnBaseInfo.BackgroundImage = global::LuxMMS.Properties.Resources.btnRight;
            //this.btnBaseInfo.BackColor = System.Drawing.Color.Goldenrod;
            //btnBaseInfo.BackColor = Color.Goldenrod;
        }
        #endregion

        #region 控件点击事件
        /// <summary>
        /// 看板按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKanBan_Click(object sender, EventArgs e)
        {
            //未登入，打开登入验证
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                FrmCheckRight = new FrmCheckRight();
                FrmCheckRight.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                if (BaseInfo.KanBanShow == "呼叫看板")
                {
                    BaseInfo.CurrFrmType = typeof(FrmKanBan);
                    SwitchFormShow();
                }
                else if (BaseInfo.KanBanShow == "设备看板")
                {
                    BaseInfo.CurrFrmType = typeof(FrmMachineWatch);
                    SwitchFormShow();
                }
                else if (BaseInfo.KanBanShow == "兼容切换")
                {
                    GlobalData.FrmMachineWatch = new FrmMachineWatch();
                    BaseInfo.CurrFrmType = typeof(FrmKanBan);
                    SwitchFormShow();
                }
            }
        }

        /// <summary>
        /// 信息维护按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseInfo_Click(object sender, EventArgs e)
        {
            //未登入，打开登入验证
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                FrmCheckRight = new FrmCheckRight();
                FrmCheckRight.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                BaseInfo.CurrFrmType = typeof(FrmTabFrame);
                SwitchFormShow();
            }
        }

        /// <summary>
        /// 呼叫图片单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCall_Click(object sender, EventArgs e)
        {
            //右键显示菜单
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Right)
                {
                    contextMenuStrip_Show(sender, e);
                    return;
                }
            }
            BaseInfo.CurrFrmType = typeof(FrmMaster);
            SwitchFormShow();
        }

        /// <summary>
        /// 计时器双击事件：显示或隐藏窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelClock_DoubleClick(object sender, EventArgs e)
        {
            SwitchFormShow();
        }
        #endregion

        #region 右键菜单事件
        //显示菜单，调整菜单显示位置
        private void contextMenuStrip_Show(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Right)
                {
                    Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                    screenHeight = rect.Height;
                    screenWidth = rect.Width;
                    int x = screenWidth - MousePosition.X < contextMenuStrip.Width ? MousePosition.X - contextMenuStrip.Width : MousePosition.X;
                    int y = screenHeight - MousePosition.Y < contextMenuStrip.Height ? MousePosition.Y - contextMenuStrip.Height : MousePosition.Y;
                    contextMenuStrip.Show(x, y);
                }
            }
        }

        //右键菜单打开处理事件
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            //关闭当前窗体
            if (BaseInfo.CurrFrmType != null)
            {
                tsmiCloseCurrFrm.Text = "关闭当前窗体";
                tsmiCloseCurrFrm.Enabled = true;
            }
            else
            {
                tsmiCloseCurrFrm.Text = "关闭当前窗体";
                tsmiCloseCurrFrm.Enabled = false;
            }

            //打开、最小、最大化当前窗体
            if (BaseInfo.CurrFrmType == null)
            {
                this.tsmiSwitchCurrFrmShow.Text = "最小化当前窗体";
                this.tsmiSwitchCurrFrmShow.Enabled = false;
            }
            else
            {
                this.tsmiSwitchCurrFrmShow.Enabled = true;
            }
            if (BaseInfo.CurrFrmType == typeof(FrmMaster))
            {
                //呼叫
                if (GlobalData.FrmMaster == null)
                {
                    tsmiSwitchCurrFrmShow.Text = "打开呼叫面板";
                }
                else if (GlobalData.FrmMaster.WindowState == FormWindowState.Minimized)
                {
                    this.tsmiSwitchCurrFrmShow.Text = "显示呼叫窗体";
                }
                else
                {
                    this.tsmiSwitchCurrFrmShow.Text = "最小化当前窗体";
                }
            }
            else if (BaseInfo.CurrFrmType == typeof(FrmTabFrame))
            {
                //基本信息
                if (GlobalData.FrmBaseInfo == null)
                {
                    this.tsmiSwitchCurrFrmShow.Text = "打开基本信息";
                }
                else if (GlobalData.FrmBaseInfo.WindowState == FormWindowState.Minimized)
                {
                    this.tsmiSwitchCurrFrmShow.Text = "显示信息窗体";
                }
                else
                {
                    this.tsmiSwitchCurrFrmShow.Text = "最小化当前窗体";
                }
            }
            else if (BaseInfo.CurrFrmType == typeof(FrmKanBan) || BaseInfo.CurrFrmType == typeof(FrmMachineWatch))
            {
                //看板
                if (GlobalData.FrmKanBan == null && GlobalData.FrmMachineWatch == null)
                {
                    tsmiSwitchCurrFrmShow.Text = "打开看板";
                }
                else if ((GlobalData.FrmKanBan != null && GlobalData.FrmKanBan.WindowState == FormWindowState.Minimized)
                    || (GlobalData.FrmMachineWatch != null && GlobalData.FrmMachineWatch.WindowState == FormWindowState.Minimized))
                {
                    this.tsmiSwitchCurrFrmShow.Text = "显示看板窗体";
                }
                else
                {
                    this.tsmiSwitchCurrFrmShow.Text = "最小化当前窗体";
                }
            }

            //用户登入、登出
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                tsmiUserLogin.Text = "账号登入";
            }
            else
            {
                tsmiUserLogin.Text = "账号退出";
            }
            //显示或隐藏浮窗
            if (!this.Visible)
            {
                this.tsmsSwitchFloatFrmShow.Text = "显示浮窗";
            }
            else
            {
                this.tsmsSwitchFloatFrmShow.Text = "隐藏浮窗";
            }

            //上传更新
            if (BaseInfo.LoginDept == "资讯" && BaseInfo.LoginUserRight == "A")
            {
                this.tsmiUploadSysFile.Visible = true;
            }
            else
            {
                this.tsmiUploadSysFile.Visible = false;
            }

            //设备监控
            if (BaseInfo.LoginUserNo != null)
            {
                this.tsmiMachineKanBan.Visible = true;
            }
            else
            {
                this.tsmiMachineKanBan.Visible = false;
            }
            if (GlobalData.FrmMachineWatch == null || GlobalData.FrmMachineWatch.IsDisposed)
            {
                tsmiMachineKanBan.Text = "打开设备看板";
            }
            else
            {
                tsmiMachineKanBan.Text = "关闭设备看板";
            }
        }

        #region 关闭窗体
        private void tsmiCloseCurrFrm_Click(object sender, EventArgs e)
        {
            bool r = false;//关闭结果
            if (BaseInfo.CurrFrmType == typeof(FrmMaster))
            {//关闭呼叫面板
                if (GlobalData.FrmMaster != null && !GlobalData.FrmMaster.IsDisposed)
                    GlobalData.FrmMaster.Close();
                if (GlobalData.FrmMaster == null || GlobalData.FrmMaster.IsDisposed)
                    r = true;
            }
            else if (BaseInfo.CurrFrmType == typeof(FrmTabFrame))
            {//关闭信息面板
                if (GlobalData.FrmBaseInfo != null && !GlobalData.FrmBaseInfo.IsDisposed)
                    GlobalData.FrmBaseInfo.Close();
                if (GlobalData.FrmBaseInfo == null || GlobalData.FrmBaseInfo.IsDisposed)
                    r = true;
            }
            else if (BaseInfo.CurrFrmType == typeof(FrmKanBan) || BaseInfo.CurrFrmType == typeof(FrmMachineWatch))
            {//关闭看板
                //关闭呼叫看板
                if (GlobalData.FrmKanBan != null && !GlobalData.FrmKanBan.IsDisposed)
                    GlobalData.FrmKanBan.Close();
                if (GlobalData.FrmKanBan == null || GlobalData.FrmKanBan.IsDisposed)
                    r = true;
                //关闭设备看板
                if (GlobalData.FrmMachineWatch != null && !GlobalData.FrmMachineWatch.IsDisposed)
                    GlobalData.FrmMachineWatch.Close();
                if (GlobalData.FrmMachineWatch == null || GlobalData.FrmMachineWatch.IsDisposed)
                    r = true;
            }
            //关闭窗体成功
            if (r)
            {
                BaseInfo.CurrFrmType = null;
                BaseInfo.ClockFlag = false;
                if (FrmCheckRight != null && !FrmCheckRight.IsDisposed)
                {
                    FrmCheckRight.Close();
                }
            }
        }
        #endregion

        #region 缩放窗体
        private void tsmiSwitchCurrFrmShow_Click(object sender, EventArgs e)
        {
            SwitchFormShow();
        }
        #endregion

        #region 用户登入
        private void tsmiUserLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                if (FrmCheckRight != null && FrmCheckRight.Visible == true) return;
                FrmCheckRight = new FrmCheckRight();
                FrmCheckRight.ShowDialog();
            }
            else
            {
                //清空登入信息
                BaseInfo.LoginUserNo = null;
                BaseInfo.LoginUserName = null;
                BaseInfo.LoginUserRight = null;
                BaseInfo.LoginDept = null;
                RefreshNotifyIconText();
                //关闭需要登入权限的窗体
                if (GlobalData.FrmKanBan != null && !GlobalData.FrmKanBan.IsDisposed) GlobalData.FrmKanBan.Close();
                if (GlobalData.FrmMachineWatch != null && !GlobalData.FrmMachineWatch.IsDisposed) GlobalData.FrmMachineWatch.Close();
                if (GlobalData.FrmBaseInfo != null && !GlobalData.FrmBaseInfo.IsDisposed) GlobalData.FrmBaseInfo.Close();
            }
        }
        #endregion

        #region 浮窗显隐
        private void tsmsSwitchFloatFrmShow_Click(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
            }
        }
        #endregion

        #region 资产保养>领用归还
        private void tsmiAssetRecevie_Click(object sender, EventArgs e)
        {
            FrmAssetReceive frm = new FrmAssetReceive();
            frm.Show();
        }
        #endregion

        #region 资产保养>保养记录
        private void tsmiAssetMaintenance_Click(object sender, EventArgs e)
        {
            FrmLineMaintenance frm = new FrmLineMaintenance();
            frm.Show();
        }
        #endregion

        #region 上传更新
        private void tsmiUploadSysFile_Click(object sender, EventArgs e)
        {
            UpdateHelper.StartUpdateApp(UpdateHelper.START_TYPE_UPLOAD);
        }
        #endregion

        #region 设备监控看板
        private void tsmiMachineKanBan_Click(object sender, EventArgs e)
        {
            if (GlobalData.FrmMachineWatch != null && !GlobalData.FrmMachineWatch.IsDisposed)
            {
                GlobalData.FrmMachineWatch.Close();
            }
            else
            {
                FrmMachineWatch frm = new FrmMachineWatch();
                frm.Show();
                GlobalData.FrmMachineWatch = frm;
            }
        }
        #endregion

        #region 系统设置
        private void tsmiSystemConfig_Click(object sender, EventArgs e)
        {
            //未登入，打开登入验证
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                FrmCheckRight = new FrmCheckRight();
                FrmCheckRight.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                FrmConfig frm = new FrmConfig();
                frm.ShowDialog();
            }
        }
        #endregion

        #region 退出程序
        private void tsmiExitApp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出当前程序\n\nYes:确定退出    No:取消退出", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.notifyIcon.Visible = false;
                Application.Exit();
            }
        }
        #endregion
        #endregion

        #region 定时器任务
        /// 悬浮窗置顶 ，是否显示钟表计时界面
        private void timerFrmTopMost_Tick(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.BringToFront();
            this.TopMost = true;
            if (BaseInfo.ClockFlag == true)
            {
                timerClockTime.Enabled = true;
            }
        }

        // 计时器时间计数
        private void timerClockTime_Tick(object sender, EventArgs e)
        {

            //检查更新(每天8:30)
            DateTime checkTime = UpdateHelper.CHECK_TIME;
            if (BaseInfo.AutoUpdate == true && checkTime.ToString("HH:mm:ss") == DateTime.Now.ToString("HH:mm:ss"))
            {
                //是否符合更新条件，不符合,5分钟后再检查
                if (BaseInfo.CallFlag == true || BaseInfo.KanBanErrorCount > 0 || DBUtil.CheckServerConnState().Length > 0)
                {
                    UpdateHelper.CHECK_TIME.AddMinutes(5);
                }
                else
                {
                    //检查并更新Update.exe文件。
                    UpdateHelper.CheckUpdateApp();
                    //检查并更新主应用
                    UpdateHelper.AutoUpdateApp();
                    //当日没有更新时，重置到次日。
                    UpdateHelper.ResetCheckTime();
                }
            }

            //判断是否显示时钟
            if (BaseInfo.ClockFlag == false)
            {
                panelOperate.Visible = true;
                return;
            }
            else
            {
                panelOperate.Visible = false;
            }
            TimeSpan span = new TimeSpan(TimeUtil.Now.Ticks - BaseInfo.ClockStartTime.Ticks);
            int totalSeconds = Convert.ToInt32(span.TotalSeconds);
            if ((totalSeconds % 60) % 5 == 0)
            {
                switch ((totalSeconds % 60) / 5)
                {
                    case 1:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_01;
                        break;
                    case 2:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_02;
                        break;
                    case 3:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_03;
                        break;
                    case 4:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_04;
                        break;
                    case 5:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_05;
                        break;
                    case 6:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_06;
                        break;
                    case 7:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_07;
                        break;
                    case 8:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_08;
                        break;
                    case 9:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_09;
                        break;
                    case 10:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_10;
                        break;
                    case 11:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_11;
                        break;
                    case 0:
                        this.pbTimer.BackgroundImage = global::LuxMMS.Properties.Resources.clock_12;
                        break;
                }
            }
            int timeVal = 0;
            string timeUnit = "";
            TimeUtil.ConvertTime(totalSeconds, ref timeVal, ref timeUnit);
            labTimeVal.Text = timeVal.ToString();
            labTimeUnit.Text = timeUnit;
        }

        //查检数据库连接
        private void timerCheckDBConn_Tick(object sender, EventArgs e)
        {
            timerCheckDBConn.Enabled = false;

            //数据库连接检查
            string r = DBUtil.CheckServerConnState();
            if (!string.IsNullOrWhiteSpace(r))
            {
                new FrmMsgDialog("连接异常", "原因:" + r).Show();
                timerCheckDBConn.Enabled = true;
                return;
            }

            DBUtil.DBConnState = true;
            timerCheckDBConn.Enabled = true;
        }

        //看板切换
        private void timerSwitchKanBan_Tick(object sender, EventArgs e)
        {
            if (BaseInfo.KanBanShow == "兼容切换" && GlobalData.FrmKanBan != null && !GlobalData.FrmKanBan.IsDisposed &&
                GlobalData.FrmMachineWatch != null && !GlobalData.FrmMachineWatch.IsDisposed)
            {
                if (!GlobalData.FrmKanBan.Visible)
                {//呼叫看板
                    GlobalData.FrmKanBan.Show();
                    //GlobalData.FrmKanBan.Opacity = 1;
                    //GlobalData.FrmMachineKanBan.Opacity = 0;
                    GlobalData.FrmKanBan.Visible = true;
                    GlobalData.FrmMachineWatch.Visible = false;
                }
                else
                {//设备看板
                    GlobalData.FrmMachineWatch.Show();
                    //GlobalData.FrmKanBan.Opacity = 0;
                    //GlobalData.FrmMachineKanBan.Opacity = 1;
                    GlobalData.FrmKanBan.Visible = false;
                    GlobalData.FrmMachineWatch.Visible = true;
                }
            }
        }
        #endregion

        #region 切换显示或隐藏窗体
        /// <summary>
        ///切换显示或隐藏窗体
        /// </summary>
        private void SwitchFormShow()
        {
            try
            {
                if (BaseInfo.CurrFrmType == typeof(FrmMaster))
                {
                    //呼叫
                    if (GlobalData.FrmMaster == null || GlobalData.FrmMaster.IsDisposed)
                    {
                        GlobalData.FrmMaster = new FrmMaster();
                        GlobalData.FrmMaster.Show();
                    }
                    else if (GlobalData.FrmMaster.WindowState == FormWindowState.Minimized)
                    {
                        GlobalData.FrmMaster.WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        GlobalData.FrmMaster.WindowState = FormWindowState.Minimized;
                    }
                }
                else if (BaseInfo.CurrFrmType == typeof(FrmTabFrame))
                {
                    //基本信息
                    if (GlobalData.FrmBaseInfo == null || GlobalData.FrmBaseInfo.IsDisposed)
                    {
                        GlobalData.FrmBaseInfo = new FrmTabFrame();
                        GlobalData.FrmBaseInfo.Show();
                    }
                    else if (GlobalData.FrmBaseInfo.WindowState == FormWindowState.Minimized)
                    {
                        GlobalData.FrmBaseInfo.WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        GlobalData.FrmBaseInfo.WindowState = FormWindowState.Minimized;
                    }
                }
                else if (BaseInfo.CurrFrmType == typeof(FrmKanBan))
                {
                    //呼叫看板
                    if (GlobalData.FrmKanBan == null || GlobalData.FrmKanBan.IsDisposed)
                    {
                        GlobalData.FrmKanBan = new FrmKanBan();
                        GlobalData.FrmKanBan.Show();
                    }
                    else if (GlobalData.FrmKanBan.WindowState == FormWindowState.Minimized)
                    {
                        GlobalData.FrmKanBan.WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        GlobalData.FrmKanBan.WindowState = FormWindowState.Minimized;
                        BaseInfo.ClockFlag = false;
                    }
                }
                else if (BaseInfo.CurrFrmType == typeof(FrmMachineWatch))
                {
                    //设备看板
                    if (GlobalData.FrmMachineWatch == null || GlobalData.FrmMachineWatch.IsDisposed)
                    {
                        GlobalData.FrmMachineWatch = new FrmMachineWatch();
                        GlobalData.FrmMachineWatch.Show();
                    }
                    else if (GlobalData.FrmMachineWatch.WindowState == FormWindowState.Minimized)
                    {
                        GlobalData.FrmMachineWatch.WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        GlobalData.FrmMachineWatch.WindowState = FormWindowState.Minimized;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(this.GetType(), null, ex);
            }
        }
        #endregion

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
        }

    }
}

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
using CallSys.Utils;
using CallSys.Base;
using Common.Utils;
using Common;


namespace CallSys
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

            if (GlobalData.CurrFrmType != null)
            {
                SwitchFormShow();
            }
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
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginHandlerNo))
            {
                notifyIcon.Text = "呼叫系统" + Application.ProductVersion + "\r\n服务器IP:" + DBUtil.GetServerIP() + "\r\n用户:未登入";
                return;
            }
            //设置通知栏提示文本
            string rightText = BaseInfo.LoginHandlerRight == "A" ? "超级管理员" : BaseInfo.LoginHandlerRight == "B" ? "一般管理员" : "";
            string text = string.Format("呼叫系统{0}\r\n服务器IP:{1}\r\n用户:{2}({3})\r\n权限:{4}", Application.ProductVersion, DBUtil.GetServerIP(), BaseInfo.LoginHandlerNo, BaseInfo.LoginHandlerName, rightText);
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
            this.btnKanBan.BackColor = System.Drawing.Color.Green;
            btnKanBan.FlatStyle = FlatStyle.Flat;
        }

        // 看板按钮,鼠标离开事件
        private void btnKanBan_MouseLeave(object sender, EventArgs e)
        {
            this.btnKanBan.BackColor = System.Drawing.Color.IndianRed;
            btnKanBan.BackColor = Color.IndianRed;
        }

        // 呼叫图片,鼠标进入事件
        private void pbCall_MouseEnter(object sender, EventArgs e)
        {
            panelClock.BackColor = Color.MidnightBlue;
            pbCall.Image = global::CallSys.Properties.Resources.call_enter;
        }

        // 呼叫图片,鼠标离开事件
        private void pbCall_MouseLeave(object sender, EventArgs e)
        {
            panelClock.BackColor = Color.MidnightBlue;
            pbCall.Image = global::CallSys.Properties.Resources.call_leave;
        }

        // 基础信息按钮,鼠标进入事件
        private void btnBaseInfo_MouseEnter(object sender, EventArgs e)
        {
            this.btnBaseInfo.BackColor = System.Drawing.Color.Green;
            btnBaseInfo.FlatStyle = FlatStyle.Flat;
        }

        // 基础信息按钮,鼠标离开事件
        private void btnBaseInfo_MouseLeave(object sender, EventArgs e)
        {
            this.btnBaseInfo.BackColor = System.Drawing.Color.Goldenrod;
            btnBaseInfo.BackColor = Color.Goldenrod;
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
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginHandlerNo))
            {
                FrmCheckRight = new FrmCheckRight();
                FrmCheckRight.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(BaseInfo.LoginHandlerNo))
            {
                GlobalData.CurrFrmType = typeof(FrmKanBan);
                SwitchFormShow();
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
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginHandlerNo))
            {
                FrmCheckRight = new FrmCheckRight();
                FrmCheckRight.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(BaseInfo.LoginHandlerNo))
            {
                GlobalData.CurrFrmType = typeof(FrmBaseInfo);
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
            GlobalData.CurrFrmType = typeof(FrmMaster);
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
            if (GlobalData.CurrFrmType != null)
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
            if (GlobalData.CurrFrmType == null)
            {
                this.tsmiSwitchCurrFrmShow.Text = "最小化当前窗体";
                this.tsmiSwitchCurrFrmShow.Enabled = false;
            }
            else
            {
                this.tsmiSwitchCurrFrmShow.Enabled = true;
            }
            if (GlobalData.CurrFrmType == typeof(FrmKanBan))
            {
                //看板
                if (GlobalData.FrmKanBan == null)
                {
                    tsmiSwitchCurrFrmShow.Text = "打开看板";
                }
                else if (GlobalData.FrmKanBan.WindowState == FormWindowState.Minimized)
                {
                    this.tsmiSwitchCurrFrmShow.Text = "显示看板窗体";
                }
                else
                {
                    this.tsmiSwitchCurrFrmShow.Text = "最小化当前窗体";
                }
            }
            else if (GlobalData.CurrFrmType == typeof(FrmMaster))
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
            else if (GlobalData.CurrFrmType == typeof(FrmBaseInfo))
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
            //用户登入、登出
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginHandlerNo))
            {
                tsmiUserLogin.Text = "账号登入";
            }
            else
            {
                tsmiUserLogin.Text = "账号退出";
            }
            //显示或隐藏浮窗
            if (this.Opacity == 0D)
            {
                this.tsmsSwitchFloatFrmShow.Text = "显示浮窗";
            }
            else
            {
                this.tsmsSwitchFloatFrmShow.Text = "隐藏浮窗";
            }

            //上传更新是否可见
            if (BaseInfo.LoginHandlerDept == "资讯" && BaseInfo.LoginHandlerRight == "A")
            {
                this.tsmiUploadSysFile.Visible = true;
            }
            else
            {
                this.tsmiUploadSysFile.Visible = false;
            }
        }

        #region 关闭窗体
        private void tsmiCloseCurrFrm_Click(object sender, EventArgs e)
        {
            bool r = false;//关闭结果
            if (GlobalData.CurrFrmType == typeof(FrmMaster))
            {//关闭呼叫面板
                if (GlobalData.FrmMaster != null && !GlobalData.FrmMaster.IsDisposed)
                    GlobalData.FrmMaster.Close();
                if (GlobalData.FrmMaster == null || GlobalData.FrmMaster.IsDisposed) r = true;
            }
            else if (GlobalData.CurrFrmType == typeof(FrmKanBan))
            {//关闭看板
                if (GlobalData.FrmKanBan != null && !GlobalData.FrmKanBan.IsDisposed)
                    GlobalData.FrmKanBan.Close();
                if (GlobalData.FrmKanBan == null || GlobalData.FrmKanBan.IsDisposed)
                {
                    r = true;
                    FrmCheckRight.Close();
                }

            }
            else if (GlobalData.CurrFrmType == typeof(FrmBaseInfo))
            {//关闭信息面板
                if (GlobalData.FrmBaseInfo != null && !GlobalData.FrmBaseInfo.IsDisposed)
                    GlobalData.FrmBaseInfo.Close();
                if (GlobalData.FrmBaseInfo == null || GlobalData.FrmBaseInfo.IsDisposed)
                {
                    r = true;
                    FrmCheckRight.Close();
                }
            }
            //关闭窗体成功
            if (r)
            {
                GlobalData.CurrFrmType = null;
                BaseInfo.ClockFlag = false;
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
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginHandlerNo))
            {
                if (FrmCheckRight != null && FrmCheckRight.Visible == true) return;
                FrmCheckRight = new FrmCheckRight();
                FrmCheckRight.ShowDialog();
            }
            else
            {
                //清空登入信息
                BaseInfo.LoginHandlerNo = null;
                BaseInfo.LoginHandlerName = null;
                BaseInfo.LoginHandlerRight = null;
                BaseInfo.LoginHandlerDept = null;
                RefreshNotifyIconText();
                //关闭需要登入权限的窗体
                if (GlobalData.FrmKanBan != null && !GlobalData.FrmKanBan.IsDisposed) GlobalData.FrmKanBan.Close();
                if (GlobalData.FrmBaseInfo != null && !GlobalData.FrmBaseInfo.IsDisposed) GlobalData.FrmBaseInfo.Close();
            }
        }
        #endregion

        #region 浮窗显隐
        private void tsmsSwitchFloatFrmShow_Click(object sender, EventArgs e)
        {
            if (this.Opacity == 0D)
            {
                this.Opacity = 1D;
            }
            else
            {
                this.Opacity = 0D;
            }
        }
        #endregion

        #region 上传更新
        private void tsmiUploadSysFile_Click(object sender, EventArgs e)
        {
            UpdateHelper.StartUpdateApp(UpdateHelper.START_TYPE_UPLOAD);
        }
        #endregion

        #region 系统设置
        private void tsmiSystemConfig_Click(object sender, EventArgs e)
        {
            //未登入，打开登入验证
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginHandlerNo))
            {
                FrmCheckRight = new FrmCheckRight();
                FrmCheckRight.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(BaseInfo.LoginHandlerNo))
            {
                FrmSystemConfig frm = new FrmSystemConfig();
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
                System.Environment.Exit(0);
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
                    //检查并更新CallSys.exe应用
                    UpdateHelper.AutoUpdateCallSysApp();
                    //同步服务器时间
                    DBUtil.SyncServerTime();
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
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_01;
                        break;
                    case 2:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_02;
                        break;
                    case 3:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_03;
                        break;
                    case 4:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_04;
                        break;
                    case 5:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_05;
                        break;
                    case 6:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_06;
                        break;
                    case 7:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_07;
                        break;
                    case 8:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_08;
                        break;
                    case 9:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_09;
                        break;
                    case 10:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_10;
                        break;
                    case 11:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_11;
                        break;
                    case 0:
                        this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_12;
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
            //同步服务器时间
            DBUtil.SyncServerTime();
            timerCheckDBConn.Enabled = true;
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
                if (GlobalData.CurrFrmType == typeof(FrmKanBan))
                {
                    //看板
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
                else if (GlobalData.CurrFrmType == typeof(FrmMaster))
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
                else if (GlobalData.CurrFrmType == typeof(FrmBaseInfo))
                {
                    //基本信息
                    if (GlobalData.FrmBaseInfo == null || GlobalData.FrmBaseInfo.IsDisposed)
                    {
                        GlobalData.FrmBaseInfo = new FrmBaseInfo();
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

            }
            catch (Exception ex)
            {
                LogHelper.Error(this.GetType(), null, ex);
            }
        }
        #endregion








    }
}

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
using Listen.Utils;
using Listen.Base;
using Listen;
using Listen.Service;
using System.Threading;

namespace Listen
{
    public partial class Auxiliary : Form
    {
        private FrmCheckRight FrmCheckRight;//权限验证窗体
        private FrmTcpDebug FrmTcpServer;


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
            this.Hide();
            //设置通知栏提示文本
            RefreshNotifyIconText();
            //检查数据库连接
            if (DBUtil.CheckServerConnState().Length > 0)
            {
                new FrmMsgDialog("数据库连接异常", "请检查网络").ShowDialog();
            }
        }

       

        //设置通知栏图标的提示文本
        public void RefreshNotifyIconText()
        {
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                notifyIcon.Text = "设备监听系统" + Application.ProductVersion + "\r\n服务器IP:" + DBUtil.GetServerIP() + "\r\n用户:未登入";
                return;
            }
            //设置通知栏提示文本
            string text = string.Format("设备监听系统{0}\r\n服务器IP:{1}\r\n用户:{2}({3})", Application.ProductVersion, DBUtil.GetServerIP(), BaseInfo.LoginUserNo, BaseInfo.LoginUserName);
            notifyIcon.Text = text;
        }
        #endregion

        //右键菜单打开处理事件
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            //用户登入、登出
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
            {
                tsmiUserLogin.Text = "账号登入";
            }
            else
            {
                tsmiUserLogin.Text = "账号退出";
            }

            //Tcp监听
            string tcpState = TCPServerProxy.TCPListenState ? BaseInfo.Port.ToString() : "未开启";
            tsmiTcpListen.Text = "TCP监听 [" + tcpState + "]";

            //串口监听
            string serialState = SerialPortProxy.SerialListenState ? BaseInfo.PortName : "未开启";
            tsmiSerialPort.Text = "串口监听 [" + serialState + "]";

        }


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
            }
        }
        #endregion

        #region 串口监听
        private void tsmiSerialPort_Click(object sender, EventArgs e)
        {
            FrmSerialDebug s = new FrmSerialDebug();
            s.Show();
        }
        #endregion

        #region TCP监听面板
        private void tsmiTcpListen_Click(object sender, EventArgs e)
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
                if (FrmTcpServer == null || FrmTcpServer.IsDisposed)
                {
                    FrmTcpServer = new FrmTcpDebug();
                }
                if (!FrmTcpServer.Visible) {
                    FrmTcpServer.ShowDialog();
                    FrmTcpServer.Close();
                    FrmTcpServer.Dispose();
                }
                if (FrmTcpServer.WindowState == FormWindowState.Minimized)
                    FrmTcpServer.WindowState = FormWindowState.Normal;
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
                System.Environment.Exit(0);
            }
        }
        #endregion

        #region 定时器任务
        //定时任务，每分钟执行一次，用来初始化保养状态基本信息的
        private void timer1_Tick(object sender, EventArgs e)
        {
            //每天早上九点更新当天的保养记录
            DateTime now = DateTime.Now;
            if (now.Hour == AssetHelper._StartCheckHour && now.Minute == 0)
            {
                AssetHelper.InitAssetStatus();
            }
        }
        #endregion

        #region 系统消息
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM.WM_DEVICE_CHANGE://设备改变
                    switch (m.WParam.ToInt32())
                    {
                        //case WM.DBT_DEVICE_REMOVE_COMPLETE://设备移除
                        case WM.DBT_DEVICEARRIVAL://设备插入
                            //判断是否开启串口监听
                            if (BaseInfo.SerialListenFlag)
                            {
                                SerialPortProxy.Open();
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
        }

       



    }
}

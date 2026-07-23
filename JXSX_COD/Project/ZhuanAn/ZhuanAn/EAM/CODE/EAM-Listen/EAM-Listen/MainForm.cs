using EAM.Listen.Common;
using EAM.Listen.Common.Config;
using EAM.Listen.Communication;
using EAM.Listen.DataProcessing;
using EAM.Listen.Model;
using EAM.Listen.UI;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EAM.Listen
{
    public partial class MainForm : Form
    {
        private FrmLogin FrmLogin;//权限验证窗体
        private FrmTcpDebug FrmTcpDebug;
        private FrmMqttDebug FrmMqttDebug;
        private FrmHttpDebug FrmHttpDebug;

        public MainForm()
        {
            InitializeComponent();
        }

        #region 窗体加载

        //设置通知栏图标的提示文本
        public void RefreshNotifyIconText()
        {
            //设置通知栏提示文本
            string text = $"设备监听系统{Application.ProductVersion}\r\n数据库IP:{SqlSugarUtil.GetServerIP()}\r\n用户:{LoginInfo.LoginUserName}";
            notifyIcon.Text = text;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            //设置通知栏提示文本
            RefreshNotifyIconText();
            //检查数据库连接
            if (SqlSugarUtil.CheckServerConnState().Length > 0)
            {
                new FrmMsgDialog("数据库连接异常", "请检查网络").ShowDialog();
            }
        }

        #endregion 窗体加载

        //右键菜单打开处理事件
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            //用户登入、登出
            if (string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                tsmiUserLogin.Text = "账号登入";
            }
            else
            {
                tsmiUserLogin.Text = "账号退出";
            }

            //MQTT
            string mqttState = Setting.MqttConfig.MqttSubFlag ? Setting.MqttConfig.Port.ToString() : "未订阅";
            tsmiMqttDebug.Text = "MQTT客户端[" + mqttState + "]";

            //http
            string htppState = Setting.HttpConfig.HttpListenFlag ? Setting.HttpConfig.Port.ToString() : "未开启";
            tsmiHttpDebug.Text = "HTTP监听[" + htppState + "]";

            //Tcp监听
            string tcpState = TcpProxy.TCPListenState ? Setting.TCPConfig.Port.ToString() : "未开启";
            tsmiTcpDebug.Text = "TCP监听 [" + tcpState + "]";

            //串口监听
            string serialState = SerialPortProxy.SerialListenState ? Setting.SerialPortConfig.PortName : "未开启";
            tsmiSerialPort.Text = "串口监听 [" + serialState + "]";
        }

        #region 用户登入

        private void tsmiUserLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                if (FrmLogin != null && FrmLogin.Visible == true) return;
                FrmLogin = new FrmLogin();
                FrmLogin.ShowDialog();
            }
            else
            {
                //清空登入信息
                LoginInfo.LoginUserName = null;
                RefreshNotifyIconText();
            }
        }

        #endregion 用户登入

        #region 串口监听

        private void tsmiSerialPort_Click(object sender, EventArgs e)
        {
            FrmSerialDebug s = new FrmSerialDebug();
            s.Show();
        }

        #endregion 串口监听

        #region TCP监听面板

        private void tsmiTcpListen_Click(object sender, EventArgs e)
        {
            //未登入，打开登入验证
            if (string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                FrmLogin = new FrmLogin();
                FrmLogin.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                if (FrmTcpDebug == null || FrmTcpDebug.IsDisposed)
                {
                    FrmTcpDebug = new FrmTcpDebug();
                }
                if (!FrmTcpDebug.Visible)
                {
                    FrmTcpDebug.ShowDialog();
                    FrmTcpDebug.Close();
                    FrmTcpDebug.Dispose();
                }
                if (FrmTcpDebug.WindowState == FormWindowState.Minimized)
                    FrmTcpDebug.WindowState = FormWindowState.Normal;
            }
        }

        #endregion TCP监听面板

        #region 系统设置

        private void tsmiSystemConfig_Click(object sender, EventArgs e)
        {
            //未登入，打开登入验证
            if (string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                FrmLogin = new FrmLogin();
                FrmLogin.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                FrmConfig frm = new FrmConfig();
                frm.ShowDialog();
            }
        }

        #endregion 系统设置

        #region 退出程序

        private void tsmiExitApp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出当前程序\n\nYes:确定退出    No:取消退出", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.notifyIcon.Visible = false;
                System.Environment.Exit(0);
            }
        }

        #endregion 退出程序

        #region 定时器任务

        //定时任务，每分钟执行一次，用来初始化保养状态基本信息的
        private void timer1_Tick(object sender, EventArgs e)
        {
            //每天早上九点更新当天的保养记录
            DateTime now = DateTime.Now;
            if (now.Hour == EquipmentService._StartCheckHour && now.Minute == 0)
            {
                EquipmentService.InitEquipmentStatus();
            }
        }

        /// <summary>
        /// 定时任务：从数据库加载配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerLoadConfig_Tick(object sender, EventArgs e)
        {
            FactoryBaseConfig.LoadConfig();

            IotProductConfig.LoadConfig();
        }

        #endregion 定时器任务

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
                            if (Setting.SerialPortConfig.SerialListenFlag)
                            {
                                SerialPortProxy.Open();
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion 系统消息

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        #region MQTT

        private void tsmiMqttClient_Click(object sender, EventArgs e)
        {
            //未登入，打开登入验证
            if (string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                FrmLogin = new FrmLogin();
                FrmLogin.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                if (FrmMqttDebug == null || FrmMqttDebug.IsDisposed)
                {
                    FrmMqttDebug = new FrmMqttDebug();
                }
                if (!FrmMqttDebug.Visible)
                {
                    FrmMqttDebug.ShowDialog();
                    FrmMqttDebug.Close();
                    FrmMqttDebug.Dispose();
                }
                if (FrmMqttDebug.WindowState == FormWindowState.Minimized)
                    FrmMqttDebug.WindowState = FormWindowState.Normal;
            }
        }

        #endregion MQTT

        #region Http

        private void tsmiHttpDebug_Click(object sender, EventArgs e)
        {
            //未登入，打开登入验证
            if (string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                FrmLogin = new FrmLogin();
                FrmLogin.ShowDialog();
            }
            //已登入
            if (!string.IsNullOrWhiteSpace(LoginInfo.LoginUserName))
            {
                if (FrmHttpDebug == null || FrmHttpDebug.IsDisposed)
                {
                    FrmHttpDebug = new FrmHttpDebug();
                }
                if (!FrmHttpDebug.Visible)
                {
                    FrmHttpDebug.ShowDialog();
                    FrmHttpDebug.Close();
                    FrmHttpDebug.Dispose();
                }
                if (FrmHttpDebug.WindowState == FormWindowState.Minimized)
                    FrmHttpDebug.WindowState = FormWindowState.Normal;
            }
        }

        #endregion Http
    }
}
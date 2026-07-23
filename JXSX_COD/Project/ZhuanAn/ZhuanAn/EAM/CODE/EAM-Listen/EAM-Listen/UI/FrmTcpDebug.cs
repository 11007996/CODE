using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.Communication;
using EAM.Listen.Model;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EAM.Listen.UI
{
    public partial class FrmTcpDebug : Form
    {
        private static SynchronizationContext SyncContext = null;

        public FrmTcpDebug()
        {
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;
        }

        private void FrmTcpDebug_Load(object sender, EventArgs e)
        {
            //刷新控件状态
            RefreshControlState();
            TcpProxy.SetCallBackFunc(SyncContextPost);
        }

        #region 打开监听

        private void btnOpenConn_Click(object sender, EventArgs e)
        {
            try
            {
                Setting.TCPConfig.ListenIP = txbIP.Text;
                Setting.TCPConfig.Port = (int)nudPort.Value;
                Setting.TCPConfig.ReceiveTimeout = (int)nudTimeOut.Value;
                //启动监听服务
                TcpProxy.Start();
                if (TcpProxy.TCPListenState)
                {
                    //刷新控件状态
                    RefreshControlState();
                    //启动线程后 txtMsg文本框显示相应提示
                    tbData.AppendText("启动监听服务成功,IP端口:" + Setting.TCPConfig.ListenIP.ToString() + ":" + Setting.TCPConfig.Port + Environment.NewLine);
                }
                else
                {
                    tbData.AppendText("启动监听服务失败");
                }
            }
            catch (Exception ex)
            {
                tbData.AppendText("服务端启动服务失败!");
                LogHelper.Error(typeof(FrmTcpDebug), ex.Message);
            }
        }

        #endregion 打开监听

        #region 关闭监听

        private void btnCloseConn_Click(object sender, EventArgs e)
        {
            try
            {
                TcpProxy.Stop();
                RefreshControlState();
            }
            catch (Exception)
            {
            }
        }

        #endregion 关闭监听

        #region 发送数据

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbClientIP.Text))
            {
                MessageBox.Show("未设置客户端IP");
                return;
            }
            if (string.IsNullOrWhiteSpace(txbSendMsg.Text))
            {
                MessageBox.Show("未输入要发送的消息");
                return;
            }

            byte[] send = null;
            send = ((!chkHex.Checked) ? Encoding.ASCII.GetBytes(txbSendMsg.Text.Replace("\\n", "\r\n")) : SoftBasic.HexStringToBytes(txbSendMsg.Text));
            if (chkEnter.Checked)
            {
                send = SoftBasic.SpliceTwoByteArray(send, new byte[1] { 10 });
            }

            IPAddress ip = IPAddress.Parse(txbClientIP.Text);
            int port = (int)nudClientPort.Value;
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            TcpProxy.SendMessage(client, send);
        }

        #endregion 发送数据

        #region 清空数据

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbData.Text = "";
        }

        #endregion 清空数据

        #region 显示数据

        private bool SyncContextPost(TcpStateEventArgs args)
        {
            SyncContext.Post(HandleSyncContextMsg, args);
            return true;
        }

        private void HandleSyncContextMsg(object state)
        {
            TcpStateEventArgs param = state as TcpStateEventArgs;
            ShowData(param);
        }

        private void ShowData(TcpStateEventArgs args)
        {
            //数据过多，清理
            if (tbData.Text.Length > 5000)
            {
                tbData.Clear();
            }
            //判断是否显示发送数据
            if (!chkShowSend.Checked && args.eventType == EventType.Send)
            {
                return;
            }
            string point = args.ip.ToString();
            byte[] data = args.buffer;
            string msg = (!chkHex.Checked) ? Encoding.ASCII.GetString(data) : BitConverter.ToString(data);
            string like = txbLike.Text.Trim();
            string mark = args.eventType == EventType.Send ? "发" : "收";
            //过滤数据
            if (!string.IsNullOrWhiteSpace(like) && !point.Contains(like) && !msg.Contains(like))
            {
                return;
            }
            if (chkShowTime.Checked)
            {
                tbData.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{mark}][{point}]{msg}{Environment.NewLine}");
            }
            else
            {
                tbData.AppendText($"[{mark}][{ point}]{msg}{Environment.NewLine}");
            }
        }

        #endregion 显示数据

        #region 其他

        //刷新控件状态
        private void RefreshControlState()
        {
            txbIP.Text = Setting.TCPConfig.ListenIP.ToString();
            nudPort.Value = Setting.TCPConfig.Port;
            nudTimeOut.Value = Setting.TCPConfig.ReceiveTimeout;
            if (TcpProxy.TCPListenState)
            {
                txbIP.Enabled = false;
                nudPort.Enabled = false;
                nudListenLimit.Enabled = false;
                nudTimeOut.Enabled = false;
                btnOpenConn.Enabled = false;
                btnCloseConn.Enabled = true;
                panel2.Enabled = true;
            }
            else
            {
                txbIP.Enabled = false;
                nudPort.Enabled = true;
                nudListenLimit.Enabled = true;
                nudTimeOut.Enabled = true;
                btnOpenConn.Enabled = true;
                btnCloseConn.Enabled = false;

                panel2.Enabled = false;
            }
        }

        //面板关闭
        private void FrmTcpServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            TcpProxy.SetCallBackFunc(null);
        }

        #endregion 其他
    }
}
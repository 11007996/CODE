using Listen;
using Listen.Service;
using Listen.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Listen
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
            TCPServerProxy.SetCallBackFunc(SyncContextPost);
        }

        #region 打开监听
        private void btnOpenConn_Click(object sender, EventArgs e)
        {
            try
            {
                BaseInfo.ListenIP = IPAddress.Parse(txbIP.Text);
                BaseInfo.Port = (int)nudPort.Value;
                BaseInfo.ReceiveTimeout = (int)nudTimeOut.Value;
                //启动监听服务
                TCPServerProxy.Start();
                if (TCPServerProxy.TCPListenState)
                {
                    //刷新控件状态
                    RefreshControlState();
                    //启动线程后 txtMsg文本框显示相应提示
                    tbData.AppendText("启动监听服务成功,IP端口:" + BaseInfo.ListenIP.ToString() + ":" + BaseInfo.Port + Environment.NewLine);
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
        #endregion

        #region 关闭监听
        private void btnCloseConn_Click(object sender, EventArgs e)
        {
            try
            {
                TCPServerProxy.Stop();
                RefreshControlState();
            }
            catch (Exception)
            {

            }

        }
        #endregion

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
            TCPServerProxy.SendMessage(client, send);
        }
        #endregion

        #region 清空数据
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbData.Text = "";
        }
        #endregion

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
            string point = args.remoteEndPoint.ToString();
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
        #endregion

        #region 其他
        //刷新控件状态
        private void RefreshControlState()
        {
            txbIP.Text = BaseInfo.ListenIP.ToString();
            nudPort.Value = BaseInfo.Port;
            nudTimeOut.Value = BaseInfo.ReceiveTimeout;
            if (TCPServerProxy.TCPListenState)
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
            TCPServerProxy.SetCallBackFunc(null);
        }
        #endregion

    }

}

using EAM.Listen.Communication;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EAM.Listen.UI
{
    public partial class FrmHttpDebug : Form
    {
        private static SynchronizationContext SyncContext = null;

        public FrmHttpDebug()
        {
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;
        }

        private void FrmHttpDebug_Load(object sender, EventArgs e)
        {
            HttpProxy.SetCallBackFunc(SyncContextPost);
        }

        #region 显示数据

        private bool SyncContextPost(HttpStateEventArgs args)
        {
            SyncContext.Post(HandleSyncContextMsg, args);
            return true;
        }

        private void HandleSyncContextMsg(object state)
        {
            HttpStateEventArgs param = state as HttpStateEventArgs;
            ShowData(param);
        }

        private void ShowData(HttpStateEventArgs args)
        {
            //数据过多，清理
            if (rtbMsg.Text.Length > 5000)
            {
                rtbMsg.Clear();
            }

            rtbMsg.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{args.method}]{args.url}{Environment.NewLine}请求Body：{Environment.NewLine}{args.postData}{Environment.NewLine}响应Body：{Environment.NewLine}{args.responseText}{Environment.NewLine}");
        }

        #endregion 显示数据

        private void MqttDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            HttpProxy.SetCallBackFunc(null);
        }
    }
}
using EAM.Listen.Communication;
using MQTTnet;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EAM.Listen.UI
{
    public partial class FrmMqttDebug : Form
    {
        private static SynchronizationContext SyncContext = null;

        public FrmMqttDebug()
        {
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;
        }

        private void MqttDebug_Load(object sender, EventArgs e)
        {
            MqttProxy.SetCallBackFunc(SyncContextPost);
        }

        #region 显示数据

        private bool SyncContextPost(MqttApplicationMessage args)
        {
            SyncContext.Post(HandleSyncContextMsg, args);
            return true;
        }

        private void HandleSyncContextMsg(object state)
        {
            MqttApplicationMessage param = state as MqttApplicationMessage;
            ShowData(param);
        }

        private void ShowData(MqttApplicationMessage args)
        {
            //数据过多，清理
            if (rtbMsg.Text.Length > 5000)
            {
                rtbMsg.Clear();
            }

            string topic = args.Topic;
            string content = string.Empty;
            if (topic.Contains("?"))
                content = BitConverter.ToString(args.PayloadSegment.ToArray());
            else
                content = Encoding.UTF8.GetString(args.PayloadSegment.ToArray());

            rtbMsg.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{topic}][retain={args.Retain}][qos={args.QualityOfServiceLevel}]{Environment.NewLine}{content}{Environment.NewLine}");
        }

        #endregion 显示数据

        private void MqttDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            MqttProxy.SetCallBackFunc(null);
        }
    }
}
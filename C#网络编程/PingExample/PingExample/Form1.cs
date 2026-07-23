using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace PingExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPing_Click(object sender, EventArgs e)
        {
            this.listBoxResult.Items.Clear();

            string ipString = this.textBoxRemoteIP.Text.ToString().Trim();
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = false;
            options.Ttl = 128;

            string data = "text data abcabc";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeOut = 120;
            //Ping类Send(string hostNameOrAddress,int timeout,byte[] buffer,PingOptions options)方法返回PingReply类,参数为(timeout指等待答复消息的最大毫秒数，Buffer存和回送消息一起发送的消息（存放为字节数据），Options是一个PingOptions对象，其属性Ttl指可以转发此数据包的路由节点数，默认为128,属性DontFragment指数据包是否可以拆分为多个数据包发送，)
            PingReply reply = pingSender.Send(ipString, timeOut, buffer, options);
            //Status表示消息答复状态，返回值为IPStatus枚举类型
            if (reply.Status == IPStatus.Success)
            {
                listBoxResult.Items.Add("答复的主机地址：" + reply.Address);
                listBoxResult.Items.Add("往返时间：" + reply.RoundtripTime);
                listBoxResult.Items.Add("生存时间：" + reply.Options.Ttl);
                listBoxResult.Items.Add("是否控制数据包的分段：" + reply.Options.DontFragment);
                listBoxResult.Items.Add("缓冲区大小：" + reply.Buffer.Length);
            }
            else
            {
                listBoxResult.Items.Add(reply.Status.ToString());
            }

        }
    }
}

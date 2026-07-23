using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;

namespace NetworkInterfaceExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBoxAdpterInfo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowAdapterInfo();
        }

        private void ShowAdapterInfo()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            listBoxAdpterInfo.Items.Add("适配器个数：" + adapters.Length);
            int index = 0;
            foreach(NetworkInterface adapter in adapters)
            {
                index++;
                listBoxAdpterInfo.Items.Add("----------------------------------第" + index + "个适配器信息-------------------------");
                listBoxAdpterInfo.Items.Add("描述信息：" + adapter.Description);
                listBoxAdpterInfo.Items.Add("名称：" + adapter.Name);
                listBoxAdpterInfo.Items.Add("类型：" + adapter.NetworkInterfaceType);
                listBoxAdpterInfo.Items.Add("速度：" + adapter.Speed);
                listBoxAdpterInfo.Items.Add("MAC地址：" + adapter.GetPhysicalAddress());
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
                if (dnsServers.Count > 0)
                {
                    foreach(IPAddress dns in dnsServers)
                    {
                        listBoxAdpterInfo.Items.Add("DNS服务器IP地址：" + dns + "\n");
                    }
                }
                else
                {
                    listBoxAdpterInfo.Items.Add("DNS服务器IP地址：" + "\n");
                }
            }
        }
    }
}

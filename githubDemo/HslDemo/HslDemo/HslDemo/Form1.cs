using System.Net.Sockets;
using System.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using HslCommunication;
using HslCommunication.LogNet;
using HslCommunication.Profinet.Omron;
using System.Threading;

namespace HslDemo
{
    public partial class Form1 : Form
    {
        private static OmronFinsNet omronFinsNet = null;
        public ILogNet LogNet { get; set; }
        public Form1()
        {
            InitializeComponent();
            omronFinsNet = new OmronFinsNet();
            //omronFinsNet.ConnectTimeOut = 2000;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            omronFinsNet.IpAddress = "172.27.112.1";
            omronFinsNet.Port = 9600;
            omronFinsNet.DA2 = 0;
            //omronFinsNet.ByteTransform.DataFormat = (HslCommunication.Core.DataFormat)2;
            omronFinsNet.ByteTransform.DataFormat = HslCommunication.Core.DataFormat.CDAB;
            omronFinsNet.LogNet = LogNet;

            

            OperateResult connect = omronFinsNet.ConnectServer();

            if (connect.IsSuccess)
            {

            }
            else
            {
                MessageBox.Show(StringResources.Language.ConnectedFailed + " " + connect.ToMessageShowString());
            }
        }
        private void ConnectClose()
        {
            // 断开连接
            omronFinsNet.ConnectClose();
        }

        private void SendMessagePLC()
        {
            //omronFinsNet.Write(寄存器地址, (uint)1);
            omronFinsNet.Write("D3002", (uint)1);
        }

        private void butsend_Click(object sender, EventArgs e)
        {
            Thread d = new Thread(SendMessagePLC);
            d.IsBackground = true;
            d.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectClose();
        }
    }
}

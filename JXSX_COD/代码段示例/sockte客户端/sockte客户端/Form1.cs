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
using System.Net;

namespace sockte客户端
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Socket clientSocket;
        string ip;
        int port;
        private void button1_Click(object sender, EventArgs e)
        {
            string[] ipport = new string[2];
            ipport = txtIP.Text.Split(':');
            ip = ipport[0];
            port = Convert.ToInt32(ipport[1]);
            IPAddress IP = IPAddress.Parse(ip);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IP, port));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string sengMessage = "client send Message Hello" + DateTime.Now;

            byte[] buffer = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x00, 0x00, 0x00, 0x01 };
            clientSocket.Send(buffer);
        }

    }
}

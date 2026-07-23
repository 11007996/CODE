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
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace TCPclient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TcpClient client;
        StreamReader sr;
        StreamWriter sw;
        string ip = string.Empty;
        int port = 0;
        private bool isExit = false;
        private void butconnect_Click(object sender, EventArgs e)
        {
            isExit = false;
            string[] ipport = new string[2];
            ipport = txtIP.Text.Split(':');
            ip = ipport[0];
            port = Convert.ToInt32(ipport[1]);
            IPAddress IP = IPAddress.Parse(ip);
            client = new TcpClient();
            IAsyncResult result = client.BeginConnect(ip, port, null, null);
            while (result.IsCompleted == false)
            {
                Thread.Sleep(100);
            }
            try
            {
                client.EndConnect(result);
                NetworkStream networkstream = client.GetStream();
                sr = new StreamReader(networkstream,Encoding.Default);    //可以读写中英文
                sw = new StreamWriter(networkstream, Encoding.Default);
                sendMsg("hello");
                Thread startRecevie = new Thread(recevie);
                startRecevie.IsBackground = true;
                startRecevie.Start();
                riTxt.Text = "连接成功";
                return;
            }
            catch
            {
                riTxt.Text = "连接失败";
                return;
            }
        }

        private delegate void sendmessDEL(string msg);
        private void sendMsg(string msg)
        {
            byte[] buffer = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x00, 0x00, 0x00, 0x01 };
            sw.Write(buffer);
            //sw.WriteLine(msg);
            sw.Flush();
        }


        private delegate void recevieData(out string recevieMsg);
        private void recevie()
        {
            while (isExit == false)
            {
                //if (client.Available <= 0) continue;
                //if (client.Client == null) break;

                string recevieMsg = null;
                recevieData d = new recevieData(receiveDataMsg);
                IAsyncResult result = d.BeginInvoke(out recevieMsg, null, null);    //异步执行接收消息，BeginInvoke -- EndInvoke
                while (result.IsCompleted == false)
                {
                    Thread.Sleep(50);
                    if (isExit)
                    {
                        sr.Close();
                        sw.Close();
                        client.Close();
                        addriTxt("连接断开");
                        return;
                    }
                }
                d.EndInvoke(out recevieMsg, result);
                if (recevieMsg == null && client.Available <= 0)    //检测服务器主动断开连接
                {
                    isExit = true;
                    sr.Close();
                    sw.Close();
                    client.Close();
                    addriTxt("服务器断开");
                }
                addriTxt(recevieMsg);
            }
            return;
        }
        private void receiveDataMsg(out string recevieMsg)
        {
            recevieMsg = null;
            try
            {
                recevieMsg = sr.ReadLine();
                return;
            }
            catch
            {
                addriTxt("读取错误");
            }
        }

        private delegate void addriTxtDEL(string Msg);
        private void addriTxt(string Msg)    //跨线程访问控件
        {
            if (riTxt.InvokeRequired)
            {
                addriTxtDEL d = new addriTxtDEL(addriTxt);
                riTxt.Invoke(d, new object[] { Msg });
            }
            else
            {
                riTxt.AppendText(Msg + Environment.NewLine);
                //this.Invoke((EventHandler)(delegate { riTxt.AppendText(Msg); }));
            }
        }

        private void butdiscon_Click(object sender, EventArgs e)
        {
            isExit = true;
            ritxtsend.Text = "客户端断开";
            butsend_Click(null, null);
        }

        private void butsend_Click(object sender, EventArgs e)
        {
            string msg = ritxtsend.Text;
            try
            {
                if(!isExit)
                {
                    sendmessDEL d = new sendmessDEL(sendMsg);
                    IAsyncResult result = d.BeginInvoke(msg, null, null);   //异步接收消息
                    while (result.IsCompleted == false)
                    {
                        Thread.Sleep(50);
                    }
                    status st = new status();
                    st.d = d;
                    st.result = result;
                    Thread t = new Thread(endinvoke);
                    t.IsBackground = true;
                    t.Start(st);   //只能有一个参数
                }
            }
            catch
            {
                addriTxt("主动发送失败");
            }
        }

        private struct status
        {
            public sendmessDEL d;
            public IAsyncResult result;
        }
        private void endinvoke(object st)    //Thread.Start只能有一个参数，所有传参结构体
        {
            status sta = (status)st;
            sta.d.EndInvoke(sta.result);   //EndInvoke阻塞线程，所以需要采用异步执行
            return;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.Client != null)
            {
                Thread.Sleep(10);
                sr.Close();
                sw.Close();
                client.Close();
            }
        }
    }
}

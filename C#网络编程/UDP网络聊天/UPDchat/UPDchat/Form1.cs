using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace UPDchat
{
    public partial class Form1 : Form
    {
        private UdpClient receiveUdpClient;
        private UdpClient sendUdpClient;
        private const int port = 18001;
        IPAddress ip;
        IPAddress remoteIp;
        public Form1()
        {
            InitializeComponent();
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            ip = ips[ips.Length - 1];
            remoteIp = ip;
            textboxRemotIp.Text = remoteIp.ToString();
            textBoxSend.Text = "你好！";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread myThread = new Thread(ReceiveData);
            myThread.IsBackground = true;
            myThread.Start();
            textBoxSend.Focus();
        }

        private void ReceiveData()
        {
            IPEndPoint local = new IPEndPoint(ip, port);
            receiveUdpClient = new UdpClient(local);   //本地主机，指定某一端口进行接收消息
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);    //远程IP，接收任何端口发过来的消息
            while(true)
            {
               try
               {
                   byte[] receiveBytes = receiveUdpClient.Receive(ref remote);
                   string receiveMessage = Encoding.Unicode.GetString(receiveBytes, 0, receiveBytes.Length); 
                   AddItem(listBoxReceive,string.Format("来自{0}:{1}", remote,receiveMessage));
               }
                catch
               {
                    break;
                }
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(SendMessage);
            t.IsBackground = true;
            t.Start(textBoxSend.Text);
        }
        private void SendMessage(object obj)
        {
            string message = (string)obj;
            sendUdpClient = new UdpClient(0);
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(message);
            IPEndPoint iep = new IPEndPoint(remoteIp, port);
            try
            {
                sendUdpClient.Send(bytes, bytes.Length, iep);
                AddItem(listBoxStatus,string.Format("向{0}发送：{1}",iep,message));
                ClearTextBox();
            }
            catch(Exception ex)
            {
                AddItem(listBoxStatus,"发送出错：" + ex.Message);
            }
        }
        delegate void AddLisBoxItemDelegate(ListBox listbox, string text);
        private void AddItem(ListBox lixtbox,string text)
        {
            if (listBoxReceive.InvokeRequired)
            {
                AddLisBoxItemDelegate d = new AddLisBoxItemDelegate(AddItem);
                listBoxReceive.Invoke(d,new object[] {listBoxReceive ,text});
            }
            else
            {
                listBoxReceive.Items.Add(text);
                listBoxReceive.SelectedIndex = listBoxReceive.Items.Count -1;
                listBoxReceive.ClearSelected();
            }
        }
        private delegate void ClearTextDelegate();
        private void ClearTextBox()
        {
            if(textBoxSend.InvokeRequired)
            {
                ClearTextDelegate d = ClearTextBox;
                textBoxSend.Invoke(d);
            }
            else
            {
                textBoxSend.Clear();
                textBoxSend.Focus();
            }
        }


    }
}

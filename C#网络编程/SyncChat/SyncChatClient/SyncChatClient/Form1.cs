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

namespace SyncChatClient
{
    public partial class Form1 : Form
    {
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;

        public Form1()
        {
            InitializeComponent();
            Random r = new Random((int)DateTime.Now.Ticks);
            textBoxUserName.Text = "user" + r.Next(100, 999);
            listBoxOnLineStatus.HorizontalScrollbar = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            buttonConnect.Enabled = true;
             try
             {
                 client = new TcpClient(Dns.GetHostName(),51888);
                 AddTalkMessage("连接成功");
             }
            catch
             {
                 AddTalkMessage("连接失败");
                 buttonConnect.Enabled = true;
                 return;
             }

             NetworkStream networkStream = client.GetStream();
             br = new BinaryReader(networkStream);
             bw = new BinaryWriter(networkStream);
             SendMessage("Login," + textBoxUserName.Text);
             Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
             threadReceive.IsBackground = true;
             threadReceive.Start();
        }
        private void ReceiveData()
        {
            string receiveString = null;
            while(isExit == false)
            {
                try
                {
                    receiveString = br.ReadString();
                }
                catch
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("与服务器失去联系");
                    }
                    break;
                }
                string[] splitString = receiveString.Split(',');
                string command = splitString[0].ToLower();
                //通过消息前缀确定消息边界
                switch(command)
                {
                    case "login":
                        AddOnline(splitString[1]);
                        break;
                    case "logout":
                        RemoveUserName(splitString[1]);
                        break;
                    case "talk":
                        AddTalkMessage(splitString[1] + ":");
                        AddTalkMessage(receiveString.Substring(splitString[0].Length + splitString[1].Length + 2));
                        break;
                    default:
                        AddTalkMessage("什么意思啊：" + receiveString);
                        break;
                }
            }
            Application.Exit();
        }

        private void SendMessage(string message)
        {
            try
            {
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddTalkMessage("发送失败");
            }
        }

        private void buttonSeed_Click(object sender, EventArgs e)
        {
            if(listBoxOnLineStatus.SelectedIndex != -1)
            {
                SendMessage("Talk," + listBoxOnLineStatus.SelectedItem  + "," + textBoxSend.Text + "\r\n");
                textBoxSend.Clear();
            }
            else
            {
                MessageBox.Show("请行在[当前在线]中选择一个对话者");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                SendMessage("Logout," + textBoxUserName.Text);
                isExit = true;
                br.Close();
                bw.Close();
                client.Close();
            }
        }

        private void textBoxSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonSeed.PerformClick();
            }
        }
        private delegate void MessageDelegate(string message);
        private void AddTalkMessage(string message)
        {
            if (richTextBoxTalkInfo.InvokeRequired)
            {
                MessageDelegate d = new MessageDelegate(AddTalkMessage);
                richTextBoxTalkInfo.Invoke(d,new object[] {message});
            }
            else
            {
                richTextBoxTalkInfo.AppendText(message + Environment.NewLine);
                richTextBoxTalkInfo.ScrollToCaret();
            }
        }

        private delegate void AddOnlineDelegate(string message);

        private void AddOnline(string userName)
        {
            if (listBoxOnLineStatus.InvokeRequired)
            {
                AddOnlineDelegate d = new AddOnlineDelegate(AddOnline);
                listBoxOnLineStatus.Invoke(d, new object[] { userName });
            }
            else
            {
                listBoxOnLineStatus.Items.Add(userName);
                listBoxOnLineStatus.SelectedIndex = listBoxOnLineStatus.Items.Count - 1;
                listBoxOnLineStatus.ClearSelected();
            }
        }

        private delegate void RemoveUserNameDelegate(string userName);
        private void RemoveUserName(string userName)
        {
            if (listBoxOnLineStatus.InvokeRequired)
            {
                RemoveUserNameDelegate d = RemoveUserName;
                listBoxOnLineStatus.Invoke(d, userName);
            }
            else
            {
                listBoxOnLineStatus.Items.Remove(userName);
                listBoxOnLineStatus.SelectedIndex = listBoxOnLineStatus.Items.Count - 1;
                listBoxOnLineStatus.ClearSelected();
            }
        }
    }
}

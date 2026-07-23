using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace AsyncTcpClient
{
    public partial class FormClient : Form
    {
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        BackgroundWorker connectWork = new BackgroundWorker();

        public FormClient()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Random r = new Random((int)DateTime.Now.Ticks);
            textBoxUserName.Text = "user" + r.Next(100, 999);
            listBoxOnline.HorizontalScrollbar = true;
            connectWork.DoWork += new DoWorkEventHandler(connectWork_DoWork);
            connectWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(connectWork_RunWorkerCompleted);
        }
        private void connectWork_DoWork(object sender,DoWorkEventArgs e)
        {
            //IPAddress apd = Dns.GetHostAddresses(Dns.GetHostName())[0];
            //IPAddress ads = IPAddress.Parse("127.0.0.1");
            //IPAddress ada = new IPAddress(new byte[] { 127, 0, 0, 1 });
            IPAddress ada = new IPAddress(new byte[] { 172, 17, 144, 1 });
            client = new TcpClient();
            IAsyncResult result = client.BeginConnect(Dns.GetHostName(), 51888, null, null);
            while(result.IsCompleted == false)
            {
                Thread.Sleep(100);
                AddStatus(".");
            }
            try
            {
                client.EndConnect(result);
                e.Result = "success";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                return;
            }
        }
        private void  connectWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result.ToString() == "success")
            {
                AddStatus("连接成功");
                NetworkStream networkStream = client.GetStream();
                br = new BinaryReader(networkStream);
                bw = new BinaryWriter(networkStream);
                AsyncSendMessage("Login," + textBoxUserName.Text);
                Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
                threadReceive.IsBackground = true;
                threadReceive.Start();
            }
            else
            {
                AddStatus("连接失败：" + e.Result);
                buttonConnect.Enabled = true;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            buttonConnect.Enabled = false;
            AddStatus("开始连接.");
            connectWork.RunWorkerAsync();
        }
        private void ReceiveData()
        {
            string receiveString = null;
            while(isExit == false)
            {
                ReceiveMessageDelegate d = new ReceiveMessageDelegate(ReceiveMessage);
                IAsyncResult result = d.BeginInvoke(out receiveString,null,null);
                while(result.IsCompleted == false)
                {
                    if(isExit)
                    {
                        break;
                    }
                    Thread.Sleep(250);
                }
                d.EndInvoke(out receiveString, result);
                if (receiveString == null)
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("与服务器失去联系");
                    }
                    break;
                }
                string[] splitString = receiveString.Split(',');
                string command = splitString[0].ToLower();
                switch(command)
                {
                    case "login":
                        AddOnline(splitString[1]);
                        break;
                    case "logout":
                        RemoveUserName(splitString[1]);
                        break;
                    case "talk":
                        AddTalkMessage(splitString[1] + ":\r\n");
                        AddTalkMessage(receiveString.Substring(splitString[0].Length + splitString[1].Length + 2));
                        break;
                }
            }
            Application.Exit();
        }
        private struct SendMessageStatus
        {
            public SendMessageDelegate d;
            public SendbyteDelegate e;
            public IAsyncResult result;
        }
        private void AsyncSendMessage(string message)
        {
            SendMessageDelegate d = new SendMessageDelegate(SendMessage);
            IAsyncResult result = d.BeginInvoke(message, null, null); //通过委托使用BeginInvoke方法实现异步调用同步的SendMessage方法
            while(result.IsCompleted == false)    //不断轮询异步是否完成
            {
                if (isExit)
                {
                    return;
                }
                Thread.Sleep(50);
                SendMessageStatus states = new SendMessageStatus();
                states.d = d;
                states.result = result;
                Thread t = new Thread(FinishAsyncSendMessage);  //因EndInvoke方法会阻塞程序的运行，为避免程序假死，新开一个线程执行EndInvoke方法
                t.IsBackground = true;
                t.Start(states);
            }
        }
        public delegate void SendbyteDelegate(byte[] buffer);

        private void AsyncSendByte(byte[] buffer)
        {
            SendbyteDelegate d = new SendbyteDelegate(SendMessage);
            IAsyncResult result = d.BeginInvoke(buffer, null, null); //通过委托使用BeginInvoke方法实现异步调用同步的SendMessage方法
            while (result.IsCompleted == false)    //不断轮询异步是否完成
            {
                if (isExit)
                {
                    return;
                }
                Thread.Sleep(50);
                SendMessageStatus states = new SendMessageStatus();
                states.e = d;
                states.result = result;
                Thread t = new Thread(FinishAsyncSendMessageByte);  //因EndInvoke方法会阻塞程序的运行，为避免程序假死，新开一个线程执行EndInvoke方法
                t.IsBackground = true;
                t.Start(states);
            }
        }
        private void FinishAsyncSendMessage(object obj)
        {
            SendMessageStatus states = (SendMessageStatus)obj;
            states.d.EndInvoke(states.result);
        }
        private void FinishAsyncSendMessageByte(object obj)
        {
            SendMessageStatus states = (SendMessageStatus)obj;
            states.e.EndInvoke(states.result);
        }
        private delegate void SendMessageDelegate(string message);
        private void SendMessage(string message)
        {
            try
            {
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddStatus("发送失败！");
            }
        }
        private void SendMessage(byte[] buffer)
        {
            try
            {
                bw.Write(buffer);
                bw.Flush();
            }
            catch
            {
                AddStatus("发送失败！");
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (listBoxOnline.SelectedIndex != -1)
            {
                AsyncSendMessage("Talk," + listBoxOnline.SelectedItem + "," + textBoxSend.Text + "\r\n");
                byte[] buffer = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x00, 0x00, 0x00, 0x01 };
                AsyncSendByte(buffer);
                textBoxSend.Clear();
            }
            else
            {
                MessageBox.Show("请先在[当前在线]中选择一个对话者");
            }
        }
        private delegate void ConnectServerDelegate();
        private void ConnectServer()
        {
            client = new TcpClient(Dns.GetHostName(), 51888);
        }
        private delegate void ReceiveMessageDelegate(out string receiveMessage);
        private void ReceiveMessage(out string receiveMessage)
        {
            receiveMessage = null;
            try
            {
                receiveMessage = br.ReadString();
            }
            catch (Exception ex)
            {
                AddStatus(ex.Message);
            }
        }
        private delegate void AddTalkMessageDelegate(string message);

        private void AddTalkMessage(string message)
        {
            if (richTextBoxTalkInfo.InvokeRequired)
            {
                AddTalkMessageDelegate d = new AddTalkMessageDelegate(AddTalkMessage);
                richTextBoxTalkInfo.Invoke(d, new object[] { message });
            }
            else
            {
                richTextBoxTalkInfo.AppendText(message);
                richTextBoxTalkInfo.ScrollToCaret();
            }
        }
        private delegate void AddStatusDelegate(string message);
        private void AddStatus(string message)
        {
            if (richTextBoxStatus.InvokeRequired)
            {
                AddStatusDelegate d = new AddStatusDelegate(AddStatus);
                richTextBoxStatus.Invoke(d, new object[] { message });
            }
            else
            {
                richTextBoxStatus.AppendText(message);
            }
        }
        private delegate void AddOnlineDelegate(string message);
        private void AddOnline(string message)
        {
            if (listBoxOnline.InvokeRequired)
            {
                AddOnlineDelegate d = new AddOnlineDelegate(AddOnline);
                listBoxOnline.Invoke(d, new object[] { message });
            }
            else
            {
                listBoxOnline.Items.Add(message);
                listBoxOnline.SelectedIndex = listBoxOnline.Items.Count-1;
                listBoxOnline.ClearSelected();
            }
        }
        private delegate void RemoveUserNameDelegate(string message);
        private void RemoveUserName(string userName)
        {
            if (listBoxOnline.InvokeRequired)
            {
                RemoveUserNameDelegate d = new RemoveUserNameDelegate(RemoveUserName);
                listBoxOnline.Invoke(d, userName);
            }
            else
            {
                listBoxOnline.Items.Remove(userName);
                listBoxOnline.SelectedIndex = listBoxOnline.Items.Count - 1;
                listBoxOnline.ClearSelected();
            }
        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(client != null)
            {
                AsyncSendMessage("logout," + textBoxUserName.Text);
                isExit = true;
                br.Close();
                bw.Close();
                client.Close();
            }
        }
    }
}

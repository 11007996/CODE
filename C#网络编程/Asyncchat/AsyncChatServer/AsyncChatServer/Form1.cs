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


namespace AsyncChatServer
{
    public partial class FormServer : Form
    {
        private List<User> userList = new List<User>();
        IPAddress localIPAdress;
        private const int port = 51888;
        private TcpListener myLisener;
        bool isExit = false;
        public FormServer()
        {
            InitializeComponent();
            listBoxStatus.HorizontalScrollbar = true;
            IPAddress[] addrIP = Dns.GetHostAddresses(Dns.GetHostName());
            localIPAdress = addrIP[1];
            buttonStop.Enabled = false;

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            IPAddress aps = IPAddress.Parse("172.18.129.110");
            myLisener = new TcpListener(aps, port);
            myLisener.Start();
            AddItemToListBox(string.Format("开始在{0}:{1}监听客户连接", localIPAdress, port));
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }

        private void AddItemToListBox(string str)
        {
            if (listBoxStatus.InvokeRequired)
            {
                AddItemToListBoxDelegate d = AddItemToListBox;
                listBoxStatus.Invoke(d, str);
            }
            else
            {
                listBoxStatus.Items.Add(str);
                listBoxStatus.SelectedIndex = listBoxStatus.Items.Count - 1;
                listBoxStatus.ClearSelected();
            }
        }
        private void ListenClientConnect()
        {
            TcpClient newClient = null;
            while (true)
            {
                ListenClientDelegate d = new ListenClientDelegate(ListenClient);
                IAsyncResult result = d.BeginInvoke(out newClient, null, null);
                while (result.IsCompleted == false)
                {
                    if (isExit)
                    {
                        break;
                    }
                    Thread.Sleep(250);
                }
                d.EndInvoke(out newClient, result);
                if (newClient != null)
                {
                    User user = new User(newClient);
                    Thread threadReceive = new Thread(ReceiveData);
                    threadReceive.Start(user);
                    userList.Add(user);
                    AddItemToListBox(string.Format("[{0}]进入", newClient.Client.RemoteEndPoint));
                    AddItemToListBox(string.Format("当前连接用户数：{0}", userList.Count));
                }
                else
                {
                    break;
                }
            }
        }

        private void ListenClient(out TcpClient newClient)
        {
            try
            {
                newClient = myLisener.AcceptTcpClient();
            }
            catch
            {
                newClient = null;
            }
        }
        private delegate void ListenClientDelegate(out TcpClient client);
        private delegate void ReceiveMessageDelegate(User user, out string receiveMessage);
        private void ReceiveData(object userState)
        {
            User user = (User)userState;
            TcpClient client = user.client;
            while (isExit == false)
            {
                string receiveString = null;
                ReceiveMessageDelegate d = new ReceiveMessageDelegate(ReceiveMessage);
                IAsyncResult result = d.BeginInvoke(user, out receiveString, null, null);
                while (result.IsCompleted == false)
                {
                    if (isExit)
                    {
                        break;
                    }
                    Thread.Sleep(250);
                }
                d.EndInvoke(out receiveString, result);
                if (receiveString == null)
                {
                    if (isExit)
                    {
                        AddItemToListBox(string.Format("与[{0}]失去联系，已终止接收该用户信息", client.Client.RemoteEndPoint));
                        RemoveUser(user);
                    }
                    break;
                }
                AddItemToListBox(string.Format("来自[{0}]:{1}", user.client.Client.RemoteEndPoint, receiveString));
                string[] splitString = receiveString.Split(',');
                switch (splitString[0])
                {
                    case "Login":
                        user.userName = splitString[1];
                        AsyncSendToAllClient(user, receiveString);
                        break;
                    case "Logout":
                        AsyncSendToAllClient(user, receiveString);
                        RemoveUser(user);
                        return;
                    case "Talk":
                        string talkString = receiveString.Substring(splitString[0].Length + splitString[1].Length + 2);
                        AddItemToListBox(string.Format("{0}对{1}说：{2}", user.userName, splitString[1], talkString));
                        AsyncSendToClient(user, "talk," + user.userName + "," + talkString);
                        foreach (User target in userList)
                        {
                            if (target.userName == splitString[1] && user.userName != splitString[1])
                            {
                                AsyncSendToClient(target, "talk," + user.userName + "," + talkString);
                                break;
                            }
                        }
                        break;
                    default:
                        AddItemToListBox("什么意思啊：" + receiveString);
                        break;
                }
            }
        }
        private void ReceiveMessage(User user, out string receiveMessage)
        {
            try
            {
                receiveMessage = user.br.ReadString();
            }
            catch (Exception ex)
            {
                AddItemToListBox(ex.Message);
                receiveMessage = null;
            }
        }
        private void AsyncSendToClient(User user, string message)
        {
            SendToClientDelegate d = new SendToClientDelegate(SendToClient);
            IAsyncResult result = d.BeginInvoke(user, message, null, null);
            while (result.IsCompleted == false)
            {
                if (isExit)
                {
                    break;
                }
                Thread.Sleep(250);
            }
            d.EndInvoke(result);
        }
        private delegate void SendToClientDelegate(User user, string message);
        private void SendToClient(User user, string message)
        {
            try
            {
                user.bw.Write(message);
                user.bw.Flush();
                AddItemToListBox(string.Format("向[{0}]发送:{1}", user.userName,message));
            }
            catch
            {
                AddItemToListBox(string.Format("向[{0}]发送消息失败", user.userName, message));
            }
        }
        private void AsyncSendToAllClient(User user, string message)
        {
            string command = message.Split(',')[0].ToLower();
            if (command == "login")
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    AsyncSendToClient(userList[i], message);
                    if (userList[i].userName != user.userName)
                    {
                        AsyncSendToClient(user, "login," + userList[i].userName);
                    }
                }
            }
            else if (command == "logout")
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    if (userList[i].userName != user.userName)
                    {
                        AsyncSendToClient(userList[i], message);
                    }
                }
            }
        }
        private void RemoveUser(User user)
        {
            userList.Remove(user);
            user.Close();
            AddItemToListBox(string.Format("当前连接用户数：{0}", userList.Count));
        }
        private delegate void AddItemToListBoxDelegate(string str);

        private void buttonStop_Click(object sender, EventArgs e)
        {
            AddItemToListBox("开始停止服务，并依次使用户退出！");
            isExit = true;
            for (int i = userList.Count - 1; i >= 0; i--)
            {
                RemoveUser(userList[i]);
            }
            myLisener.Stop();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myLisener != null)
            {
                buttonStop.PerformClick();
            }
        }

    }
}

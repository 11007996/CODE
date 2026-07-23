using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace SyncChatServer
{
    public partial class Form1 : Form
    {
        private List<User> UserList = new List<User>();
        IPAddress localAddress;
        private const int port = 51888;
        private TcpListener myListener;
        bool isNormalExit = false;
        public Form1()
        {
            InitializeComponent();
            listBoxStatus.HorizontalScrollbar = true;
            IPAddress[] addrIP = Dns.GetHostAddresses(Dns.GetHostName());
            localAddress = addrIP[1];
            buttonStop.Enabled = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            IPEndPoint endPoint = new IPEndPoint(localAddress, port);
            myListener = new TcpListener(endPoint);
            myListener.Start();
            AddItemToListBox(string.Format("开始{0}:{1}监听客户连接", localAddress, port));
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }
  
      

        private void ListenClientConnect()
        {
            TcpClient newClient = null;
            while(true)
            {
                try
                {
                    newClient = myListener.AcceptTcpClient();
                }
                catch 
                {
                    break;
                }
                User user = new User(newClient);
                Thread threadReceive = new Thread(ReceiveData);
                threadReceive.Start(user);
                UserList.Add(user);
                AddItemToListBox(string.Format("[{0}]进入",newClient.Client.RemoteEndPoint));
                AddItemToListBox(string.Format("当前连接用户数：{0}",UserList.Count));
            }
        }

        private void ReceiveData(object UserState)
        {
            User user = (User)UserState;
            TcpClient client = user.client;
            while(isNormalExit == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = user.br.ReadString();
                }
                catch
                {
                    if (isNormalExit == false)
                    {
                        AddItemToListBox(string.Format("与[{0}]失去联系，已终止接收该用户信息",client.Client.RemoteEndPoint));
                        RemoveUser(user);
                    }
                    break;
                }
                AddItemToListBox(string.Format("来自[{0}]：{1}",user.client.Client.RemoteEndPoint,receiveString));
                string[] splitString = receiveString.Split(',');
                switch(splitString[0])
                {
                    case "Login":
                        user.userName = splitString[1];
                        SendToAllClient(user,receiveString);
                        break;
                    case "Logout":
                        SendToAllClient(user,receiveString);
                        RemoveUser(user);
                        return;
                    case "Talk":
                        string talkString = receiveString.Substring(splitString[0].Length + splitString[1].Length + 2);
                        AddItemToListBox(string.Format("{0}对{1}说：{2}",user.userName,splitString[1],talkString));
                        SendToClient(user,"talk," + user.userName + "," + talkString);
                        foreach (User target in UserList)
                        {
                            if (target.userName == splitString[1] && user.userName != splitString[1])
                            {
                                SendToClient(target,"talk," + user.userName + "," + talkString);
                                break;                            }
                        }
                        break;
                    default:
                        AddItemToListBox("什么意思啊：" + receiveString);
                        break;
                }
            }
        }

        private void SendToClient(User user,string message)
        {
            try
            {
                user.bw.Write(message);
                user.bw.Flush();
                AddItemToListBox(string.Format("向[{0}]发送：{1}",user.userName,message));
            }
            catch
            {
                AddItemToListBox(string.Format("向[{0}]发送信息失败",user.userName));
            }
        }

        private void SendToAllClient(User user, string message)
        {
            string command = message.Split(',')[0].ToLower();
            if(command == "login")
            {
                for(int i = 0 ; i <UserList.Count; i++)
                {
                    SendToClient(UserList[i], message);
                        if (UserList[i].userName != user.userName)
                        {
                            SendToClient(user, "login," + UserList[i].userName);
                        }
                }
            }
            else if (command == "logout")
            {
                for (int i = 0; i < UserList.Count; i++)
                {
                    if (UserList[i].userName != user.userName)
                    {
                        SendToClient(UserList[i],message);
                    }
                }
            }
        }

        private void RemoveUser(User user)
        {
            UserList.Remove(user);
            user.Close();
            AddItemToListBox(string.Format("当前用户数：{0}",UserList.Count));
        }
        private delegate void AddItemToListBoxDelegate(string str);

        private void AddItemToListBox(string str)
        {
            if (listBoxStatus.InvokeRequired)
            {
                AddItemToListBoxDelegate d = AddItemToListBox;
                listBoxStatus.Invoke(d,str);
            }
            else
            {
                listBoxStatus.Items.Add(str);
                listBoxStatus.SelectedIndex = listBoxStatus.Items.Count - 1;
                listBoxStatus.ClearSelected();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            AddItemToListBox("停止服务，并依次使用户退出！");
            isNormalExit = true;
            for (int i = UserList.Count - 1; i>= 0; i--)
            {
                RemoveUser(UserList[i]);
            }
            myListener.Stop();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myListener != null)
            {
                buttonStop.PerformClick();
            }
        }

    }
}

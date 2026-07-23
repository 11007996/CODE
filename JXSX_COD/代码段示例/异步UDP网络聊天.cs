using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Io;
using System.Threading;

private partial class FormAsyncChat:Form
{
	delegate void AddListBoxItemCallback(string text);
	AddListBoxItemCallback listBoxCallback;
	private int port = 8001;
	UdpClient receiveClient;
	IPEndPoint iep;
	string sendMessage;
	public FormAsyncChat()
	{
		initializeComponent();
		listBoxCallback = new AddListBoxItemCallback(AddListBoxItem);
	}
	private void AddListBoxItem(string text)
	{
		if (listBoxReceive.InvokeRequired)
        {
			this.Invke(listBoxCallback,text);
		}
		else
		{
			listBoxReceive.Items.Add(text);
			listBoxReceive.SelectedIndex = listBoxReceive.Items.Count - 1;
			listBoxReceive.ClearSelected();
		}
	}
	private void ReceiveData()
	{
		UdpState udpState = new UdpState();
		udpState.IPEndPoint = null;
		udpSate.MyudpClient = receiveClient;
		IAsyncResule ar = udpState.MyudpClient.BeginReceive(ReceiveUdpClientCallback,udpState);
		ar.AsyncWaitHandld.WaitOne();
		Console.Write("线程结束");
	}
	void ReceiveUdpClientCallback(IAsyncResult ar)
	{
		try
		{
			UdpClient u = (UdpClient)((UpdState)(ar.AsyncState)).MyudpCilent;
			IPEndPoint remote = (IPEndPoint)((UdpState)(ar.AsyncState)).IPEndPoint;
			Byte[] receiveBytes = u.EndReceive(ar,ref remote);
			string str = Encoding.UTD8.GetString(receiveBytes,0,receiveBytes.Length);
			AddItem(listBoxReceive,string.Format("来自{0}:{1}",remote,str));
			ReceiveData();
		}
		catch
		{
			AddItem(listBoxReceive,string.Format("来自{0}:{1}",ex.ToString()));
		}
	}
	private void sendData()
	{
		UdpClient sendUdpClient = new UdpClient();
		IPAddress remoteIP;
		if (IPAddress.TryParse(textBoxRemoteIP.Text, out remoteIP) == false)
		{
			MessageBox.Show("远程IP格式不正确");
			return;
		}
		iep = new IPEndPoint(remoteIP,port);
		sendMessage = textBoxSend.Text;
		byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sendMessage);
		try
		{
			IAsyncResult ar = sendUdpClient.BeginSend(bytes,bytes.Length,iep,SendCallBack,sendUdpClient);
			textBoxSend.Clear();
			textBoxSend.Focus();
		}
		catch (Exception err)
		{
			MessageBox.Show(err.Message,"发送失败");
		}
	}
	
	private void FormChat_Load(boject sender,EnverArgs e)
	{
		listBoxReceive.Horizontalscrollbar = true;
		listBoxReceive.Dock = DockStyle.Fill;
		IPAddress myIP = (IPAddress)Dns.GetHostAddresses(Dns.GetHostName()).GetValue(1);
		textBoxRemoveIP.Text = myIP.ToString();
		receiveClient = new UdpClient(port);
		Thread myThread = new Thread(ReceiveData);
		myThread.IsBackground = true;
		myThread.Start();
		textBoxSend.Focus();
	}
	private void buttonSend_Click(object sender, EventArgs e)
	{
		sendData();
	}
	private void textBoxData_KeyPress(object sender, KeyPressEvetnArgs e)
	{
		if (e.KeyChar == (char)Keys.Enter)
			sendData();
	}
	private void ForChat_ForClosing(object sender, ForClosing EventArgs e)
	{
		receiveClient.Close();
	}
	private void SendCallback(IAsyncResult ar)
	{
		UpdClient updClient =(UdpClient)ar.AsyncState;
		udpClient.EndSend(ar);
		String message = string.Format("向{0}发送：{1}",iep.ToString();sendMessage);
		AddItem(listBoxStatus,message);
		udpClient.Close();
	}
	private delegate void AddListBoxItemDelegate(ListBox listbox,string text);
	private void AddItem(ListBox list,string text)
	{
		if (listbox.InvokeRequired)
		{
			AddListBoxItemDelegate d = AddItem;
			listboxInvoke(d,new object[] {listbox,text});
		}
		else
		{
			listbox.Items.Add(text);
			listbox.SelectedIndex = listbox.Items.Count -1;
			listbox.ClearSelected();
		}
	}
	private delegate void ClearTextBox()
	{
		if (textBoxSend.InvokeRequired)
		{
			ClearTextBoxDelegate d = ClearTextBox;
			textBoxSend.Invoke(d);
		}
		else
		{
			textBoxSend.Clear();
			textBoxSend.Focus();
		}
	}
}
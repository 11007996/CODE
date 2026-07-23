//ThreadPool.QueueUserWorkItem(new WaitCallBack(funName));


byte[] ipbyte = new byte[]{171,18,20,255};
IPAddress ip = new IPAddress(ipbyte);

IPAddress ip = IPAddress.Parse("171.18.20.255");
IPEndPoint ipe = new IPEndPoint(ip,8080);
ipe.Address = ip;
ipe.Port = 8080;


IPAddress[] ips = Dns.GetHostEntry("www.baidu.com").AddressList;
IPAddress[] ips = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

IPAddress[] ips = Dns.GetHostAddresses("www.baidu.com");
IPAddress[] ips = Dns.GetHostAddresses(""); //获取本机的所有IP地址

//获取网络适配器对象(网卡)
NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
IPinterfaceProperties adapterProperties = adapters[0].GetIPProperties();


foreach(EncodingInfo ed in Encoding.GetEncodings())
{
	Encoding a = ed.GetEncoding();
	string b = a.EncodingName;
	string c = ed.Name;
}


//unicode转utf8
Encoding unicode = Encoding.Unicode;
Encoding utf8 = Encoding.UTF8;
byte[] aa = unicode.GetBytes("hello world");
byte[] bb = Encoding.Convert(Encoding.Unicode,encod.UTF8,aa);
string cc = utf8.GetString(bb);

//转为UFT8字符串
char st = new char[]{'\u0023','\u0021'};
Encoder ed = Encoding.ASCII.GetEncoder();
byte[] b = new byte[Encoder.GetByteCount(st,0,st.Length,true)];
ed.GetBytes(st,0,st.Length,b,0,ture);
string c = Encoding.UTF8.GetString(b);


//字节转unicode字符串
byte[] by = new byte[]{34,4,34,54,67,23,346,5}
Decoder de = Encoding.Unicode.GetDecoder();
int byCount = de.GetcharCount(by,0,by.Length);
char[] ch = new char[byCount];
in charlen = de.GetChars(by,0.by.Length,ch,0,true);
string text = string.Empty;
foreach(char a in ch)
{
	text += a.ToString();
}



char[] ch = new char[]{'\u0043','\u0054'};
Encoder ec = Encoding.ASCII.GetEncoder();
byte[] by = new byte[Encoding.GetByteCount(ch,0,ch.Length,true)];
ec.GetBytes(ch,0,ch.Length,by,0,true);
string a = Encoding.UTF8.GetString(by);



byte[] by = new byte[]{1,3,4,5,6,4,3};
Decoder dc = new Encoding.Unicode.GetDecoder();
char[] ch = new char[dc.GetcharCount(by,0,by.Length,true);];
int cc = dc.GetChars(by,0,by.Length,ch,0,true);
string bb = string.Empty;
foreach(char aa in ch)
{
	bb += aa.ToString();
}


Socket sk = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
//IPAddress ip = IPAddress.Parse("172.18.32.188");
IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).AddressList[0];
IPEndPoint ipe = new IPEndPoint(ip,8080);
sk.bind(ipe);
sk.Listen(10);


FileStream fs = new FileStream("/dsge/sage/aseg",FileMode.Append,FileAccess.ReadWrite);
FileStream fs = File.Open("/dsge/sage/aseg",FileMode.Append,FileAccess.ReadWrite);
FileStream fs = File.OpenWrite("/dsge/sage/aseg");
fs.Position = fs.Length;


TcpClient tc = new TcpClient();
tc.Connect("www.baidu.com",8080);
NetworkStream myNetworkStream = tc.GetStream();
if(myNetworkStream.CanRead)
{
	byte[] myReadBuffer = new byte[1024];
	StringBuilder myCompleteMessage = new stringBuilder();
	int numberBytesRead=0;
	do{
		numberBytesRead = myNetworkStream.Read(myReadBuffer,0,myReadBuffer.Length);
		myCompleteMessage.AppendFormat("{0}",Encoding.ASCII.GetString(myReadBuffer,0,numberBytesRead));
	}
	while(myNetworkStream.DataAvailable); //获取一个值，该值指示在要读取的 NetworkStream 上是否有可用的数据。
}



Socket sc = new Socket(networkStream.Interetwork,SocketType.Stream,ProtocolType.Tcp);
networkStream ns = sc.GetStream(sc);


private delegate void  messagedele(string aa);
messagedele d = new messagedele(funName);
IAsyncResult ir = d.BeginInvoke(message,null,null);
while(ir.Complete == false)
{
	if(isEixt)
	{
		break;
	}
}
d.EndInvoke(ir);


private delegate void addMessage(string str);
private void txtkpsn(string meassage)
{
	if (txtkpsn.InvokeRequired)
	{
		addMessage d = new addMessage(txtkpsn);
		txtkpsn.Invoke(d,new object{meassage});
	}
	else
	{
		txtkpsn.text = meassage;
	}
}

string uri = "www.baidu.com";
HttpWebRequest wer = (HttpWebRequest)WebRequest.Create(uri);
HttpWebResponse wes = (HttpWebResponse)wer.GetResponse();




private System.Timers.Timer timer;
timer= new System.Timers.Timer();
timer.Interval = 1000;
timeer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
private void timer_Elapsed(object sender,Eventargs e)
{
	Console.WriteLine("定时器方法");
}
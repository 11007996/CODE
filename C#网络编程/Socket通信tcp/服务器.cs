using System;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Net.Sockets;
using System.Net;

class Server
{
	private static byte[] result = new byte[1024];
	private static int myprot = 8889;
	static Socket serverSocket;
	static void Main(string[] args)
	{
		IPAddress ip = IPAddress.Parse("127.0.0.1");
		serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
		serverSocket.Bind(new IPEndPoint(ip,myprot));
		serverSocket.Listen(10);
		Console.WriteLine("启动监听{0}成功",serverSocket.LocalEndPoint.ToString());
		Thread myThread = new Thread(ListenClientConnect);
		myThread.Start();
		Console.ReadKey();
	}
	
	private static void ListenClientConnect()
	{
		while (true)
		{
			Socket clientsocket = serverSocket.Accept();
			clientsocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
			Thread receiveThread = new Thread(ReceiveMessage);
			receiveThread.Start(clientsocket);
		}
	}
	
	private static void ReceiveMessage(Object clientSocket)
	{
		Socket myClientSocket = (Socket)clientSocket;
		while (true)
		{
			try
			{
				int receiveNumber = myClientSocket.Receive(result);
				Console.WriteLine("接收客户端{0}消息{1}",myClientSocket.RemoteEndPoint.ToString(),Encoding.ASCII.GetString(result,0,receiveNumber));

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				myClientSocket.Shutdown(SocketShutdown.Both);
				myClientSocket.Close();
				break;
			}
		}
	}
}
using Listen;
using Listen.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Listen.Service
{
    public class TCPServerProxy
    {
        private static TcpListener TcpListener = null;
        private static Thread ListenThread = null; //负责监听客户端的线程
        private delegate byte[] TcpReceivedEventHandle(TcpStateEventArgs args); //接收信息委托
        private static event TcpReceivedEventHandle TcpMessageReceived; //事件处理

        //回调方法（给外部UI线程调用）
        private static Func<TcpStateEventArgs, bool> CallBackFunc;

        //监听状态
        private static bool _TCPListenState = false;
        public static bool TCPListenState { get { return _TCPListenState; } }


        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Start()
        {
            try
            {

                // 设置最小线程数
                // int minWorkerThreads, minCompletionThreads;
                //ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionThreads);
                //ThreadPool.SetMinThreads(minWorkerThreads, minCompletionThreads);
                // 设置最大线程数
                //int maxWorkerThreads, maxCompletionThreads;
                //ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionThreads);
                //ThreadPool.SetMaxThreads(maxWorkerThreads, maxCompletionThreads);

                //初始化TCP接收事件处理方式
                if (TcpMessageReceived == null)
                    TcpMessageReceived += ReceiveHandle.TCP_HandleReceiveMsg;

                //初始IP地址和端口号
                IPAddress ipaddress = BaseInfo.ListenIP;
                int port = BaseInfo.Port;
                //创建TPC监听服务对象
                TcpListener = new TcpListener(ipaddress, port);
                //设置接收超时
                TcpListener.Server.ReceiveTimeout = BaseInfo.ReceiveTimeout * 1000;
                //启动监听
                TcpListener.Start();
                //----------------因当前线程为UI线程，所以启动一个新的线程监听客户端的连接请求-----------------------
                //创建一个监听线程 
                ListenThread = new Thread(ListenConnecting);
                //将窗体线程设置为与后台同步
                ListenThread.IsBackground = true;
                //启动线程
                ListenThread.Start();
                //启动线程后 txtMsg文本框显示相应提示
                Console.WriteLine("启动TCP监听服务成功,IP端口:" + ipaddress.ToString() + ":" + port.ToString());
                _TCPListenState = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动TCP监听服务异常!原因:" + ex.Message);
                Stop();
            }
        }


        /// <summary>
        /// 监听客户端请求连接
        /// </summary>
        private static void ListenConnecting()
        {
            try
            {
                // 开始监听
                Console.WriteLine("服务器已启动，等待客户端连接...");
                while (true)
                {
                    //监听连接请求(阻塞)
                    TcpClient client = TcpListener.AcceptTcpClient();
                    //交给线程池处理
                    ThreadPool.QueueUserWorkItem(HandleClient, client);
                    //Console.WriteLine("客户端已连接,IP:" + client.Client.RemoteEndPoint.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TCP监听服务器发生异常: " + ex.Message);
            }
            finally
            {  // 停止监听
                Stop();
            }
        }


        private static void HandleClient(object threadParam)
        {
            TcpClient client = threadParam as TcpClient;
            NetworkStream stream = null;
            try
            {
                EndPoint remotePoint = client.Client.RemoteEndPoint;
                //获取客户端的网络流
                stream = client.GetStream();

                // 接收客户端发送的数据
                byte[] buffer = new byte[1024];
                //等待读取数据(阻塞)
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string requestData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //Console.WriteLine("接收到客户端数据: " + requestData);

                //交给指定事件处理
                if (TcpMessageReceived != null)
                {
                    TcpStateEventArgs arg = new TcpStateEventArgs();
                    arg.buffer = buffer.Take(bytesRead).ToArray();
                    arg.remoteEndPoint = remotePoint;
                    arg.eventType = EventType.Received;
                    byte[] response = TcpMessageReceived.Invoke(arg);
                    ExecCallBackFunc(arg);
                    // 发送响应给客户端
                    SendMessage(client, response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("处理客户端请求时发生异常: " + ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                client.Close();
                //Console.WriteLine("客户端已断开连接");
            }
        }

        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        public static void SendMessage(TcpClient client, byte[] data)
        {
            try
            {
                if (client == null || data == null || data.Length <= 0) return;
                client.GetStream().Write(data, 0, data.Length);
                // Console.WriteLine("发送响应给客户端:" + Encoding.ASCII.GetString(data));

                //回调方法，添加数据到指定对象中
                if (CallBackFunc != null)
                {
                    TcpStateEventArgs arg = new TcpStateEventArgs();
                    arg.buffer = data;
                    arg.remoteEndPoint = client.Client.RemoteEndPoint;
                    arg.eventType = EventType.Send;
                    ExecCallBackFunc(arg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("客户端已断开连接,无法发送信息！原因:" + ex.Message);
            }
        }



        public static void Stop()
        {
            if (TcpListener != null)
            {
                TcpListener.Stop();
                TcpListener = null;
            }
            _TCPListenState = false;
        }

        /// <summary>
        /// 设置回调方法
        /// </summary>
        /// <param name="func"></param>
        public static void SetCallBackFunc(Func<TcpStateEventArgs, bool> func)
        {
            CallBackFunc = func;
        }

        private static void ExecCallBackFunc(TcpStateEventArgs args)
        {
            if (CallBackFunc != null)
            {
                try
                {
                    CallBackFunc.Invoke(args);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
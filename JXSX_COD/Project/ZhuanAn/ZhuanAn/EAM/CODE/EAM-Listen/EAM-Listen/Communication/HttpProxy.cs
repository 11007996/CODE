using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.DataProcessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace EAM.Listen.Communication
{
    public class HttpProxy
    {
        private static HttpListener _HttpListener = null;
        private static bool _isListening = false;//监听状态
        private static Thread _ListenThread = null; //负责监听客户端的线程

        //回调方法（给外部UI线程调用）
        private static Func<HttpStateEventArgs, bool> CallBackFunc;

        //新：消息处理
        private delegate string HttpReceivedEventHandle(HttpStateEventArgs args);

        private static event HttpReceivedEventHandle HttpMessageReceived; //事件处理

        public static bool HttpListenState
        { get { return _isListening; } }

        /// <summary>
        /// 启动服务
        /// </summary>
        public static void Start()
        {
            try
            {
                //初始化Http接收事件处理方式
                if (HttpMessageReceived == null)
                    HttpMessageReceived += HttpReceiveHandle.HandleHttpMsg;

                //创建TPC监听服务对象
                _HttpListener = new HttpListener();
                //获取当前IP
                List<string> ips = NetWorkHelper.GetAllLocalIPv4Address();
                foreach (string ip in ips)
                {
                    _HttpListener.Prefixes.Add($"http://{ip}:{Setting.HttpConfig.Port}/");
                }
                _HttpListener.Prefixes.Add($"http://localhost:{Setting.HttpConfig.Port}/");

                //启动监听
                _HttpListener.Start();
                _isListening = true;

                //----------------启动一个新的线程监听客户端的连接请求-----------------------
                //创建一个监听线程
                _ListenThread = new Thread(ListenConnecting);
                //将窗体线程设置为与后台同步
                _ListenThread.IsBackground = true;
                //启动线程
                _ListenThread.Start();

                //启动线程后 txtMsg文本框显示相应提示
                Console.WriteLine("启动HTTP监听服务成功,URL前缀:" + string.Join(",", _HttpListener.Prefixes));
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动HTTP监听服务异常!原因:" + ex.Message);
                Stop();
            }
        }

        public static void Stop()
        {
            if (_HttpListener != null)
            {
                _HttpListener.Stop();
                _HttpListener = null;
            }
            _isListening = false;
        }

        /// <summary>
        /// 设置回调方法
        /// </summary>
        /// <param name="func"></param>
        public static void SetCallBackFunc(Func<HttpStateEventArgs, bool> func)
        {
            CallBackFunc = func;
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
                while (_isListening)
                {
                    //监听连接请求(阻塞)
                    HttpListenerContext context = _HttpListener.GetContext(); // 阻塞等待请求
                    //交给线程池处理
                    ThreadPool.QueueUserWorkItem(HandleHttpContext, context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Http监听服务器发生异常: " + ex.Message);
            }
            finally
            {  // 停止监听
                Stop();
            }
        }

        private static void HandleHttpContext(object threadParam)
        {
            HttpListenerContext context = threadParam as HttpListenerContext;
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            try
            {
                string responseText = string.Empty;
                HttpStateEventArgs arg = new HttpStateEventArgs();
                arg.method = request.HttpMethod;
                arg.url = request.RawUrl;
                
                if (request.QueryString != null && request.QueryString.Count > 0)
                {
                    Dictionary<string, string> query = new Dictionary<string, string>();
                    foreach (var kvp in request.QueryString.AllKeys)
                    {
                        query.Add(kvp, request.QueryString.Get(kvp));
                    }
                    arg.queryDict = query;
                }

                if (request.HttpMethod == "GET")
                {
                }
                else if (request.HttpMethod == "POST")
                {
                    // 读取 POST 数据（示例）
                    string postData;
                    using (var reader = new StreamReader(request.InputStream, Encoding.UTF8))
                    {
                        postData = reader.ReadToEnd();
                    }
                    arg.postData = postData;
                }
                else
                {
                    responseText = "⚠️ 仅支持 GET/POST";
                    response.StatusCode = 405; // Method Not Allowed
                }

                //交给专门的程序处理
                if (HttpMessageReceived != null)
                {
                    arg.responseText = HttpMessageReceived.Invoke(arg);
                }
                if (arg.responseText != null)
                {
                    ResponseMessage(response, arg.responseText);
                }
                ExecCallBackFunc(arg);
            }
            catch (Exception ex)
            {
                // 错误响应
                string errorMsg = $"❌ 处理请求失败: {ex.Message}";
                byte[] errorBuffer = Encoding.UTF8.GetBytes(errorMsg);
                response.StatusCode = 500;
                response.ContentType = "text/plain; charset=utf-8";
                response.ContentLength64 = errorBuffer.Length;

                try
                {
                    using (var output = response.OutputStream)
                    {
                        output.Write(errorBuffer, 0, errorBuffer.Length);
                    }
                }
                catch
                {
                    // 忽略写入错误（如客户端已断开）
                }
            }
            finally
            {
                // 注意：不要手动调用 response.Close() 或 context.Close()
                // HttpListener 会在 OutputStream 关闭后自动完成响应
            }
        }

        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        private static void ResponseMessage(HttpListenerResponse response, string responseText)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                response.ContentType = "text/plain; charset=utf-8";
                response.ContentLength64 = buffer.Length;

                using (var output = response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("客户端已断开连接,无法发送信息！原因:" + ex.Message);
            }
        }

        private static void ExecCallBackFunc(HttpStateEventArgs args)
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

    public class HttpStateEventArgs : EventArgs
    {
        public string method;
        public string url;
        public string queryString;
        public string postData;
        public string responseText;
        public Dictionary<string, string> queryDict;
    }
}
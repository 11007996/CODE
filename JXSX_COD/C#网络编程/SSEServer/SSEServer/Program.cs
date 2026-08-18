using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSEServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();

            Console.WriteLine("Server is running...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                ThreadPool.QueueUserWorkItem((_) =>
                {
                    SendEvent(context);
                });
            }
        }
            static void SendEvent(HttpListenerContext context)
        {
            try
            {
                context.Response.ContentType = "text/event-stream";
                context.Response.StatusCode = 200;
                context.Response.Headers.Add("Cache-Control", "no-cache");
                context.Response.Headers.Add("Connection", "keep-alive");
  
                while (true)
                {
                    string message = string.Format("data: {0}\n\n",DateTime.Now.ToString("HH:mm:ss"));
                    byte[] bytes = Encoding.UTF8.GetBytes(message);
                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    context.Response.OutputStream.Flush();
                    Thread.Sleep(1000); // 每秒发送一次消息
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                context.Response.Close();
            }
        }
    }
}

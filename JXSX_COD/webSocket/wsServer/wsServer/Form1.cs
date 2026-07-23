using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wsServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<WebSocketInfo> webSockets = new List<WebSocketInfo>();
        HttpListener listener;
        private async void button1_Click(object sender, EventArgs e)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();
            textBox2.Text = "WS服务启动 8080";

            while (listener.IsListening)
            {
                try
                {
                    button1.Enabled = false;
                    button3.Enabled = true;
                    var ctx = await listener.GetContextAsync();   //得到客户端上下文
                    if (!ctx.Request.IsWebSocketRequest)
                    {
                        ctx.Response.StatusCode = 400;
                        ctx.Response.Close();
                        continue;
                    }

                    var wsCtx = await ctx.AcceptWebSocketAsync(null);  //从客户端上下文中获取客户端对象
                    var ws = wsCtx.WebSocket;
                    WebSocketInfo socketInfo = new WebSocketInfo(ws);
                    socketInfo.timer = new System.Threading.Timer(_ => SendPingAndCheck(socketInfo), null, 0, 20000);   //心跳机制
                    webSockets.Add(socketInfo);
                    textBox2.Text = "客户端已连接";
                    _ = ReceiveLoop(socketInfo);
                }
                catch (HttpListenerException ex)
                {
                    // 错误码995 = Stop导致IO中止，属于正常关闭，直接跳出
                    if (ex.ErrorCode == 995)
                        break;
                }
                catch
                {

                }
            }
        }

        async Task ReceiveLoop(WebSocketInfo ws)
        {
            byte[] buf = new byte[4096];
            while (ws.Socket.State == WebSocketState.Open)
            {
                var res = await ws.Socket.ReceiveAsync(new ArraySegment<byte>(buf), CancellationToken.None);
                if (res.MessageType == WebSocketMessageType.Close)
                {
                    await ws.Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    textBox2.Text = "收到：" + "客户端断开";
                    break;
                }
                if (res.MessageType == WebSocketMessageType.Text)
                {
                    string text = Encoding.UTF8.GetString(buf, 0, res.Count);
                    if (text == "PONG")   //心跳机制，客户端返回PONG
                    {
                        ws.LostPongCount = 0;
                        continue;
                    }
                    else if(text == "PING")
                    {
                        byte[] rep = Encoding.UTF8.GetBytes("PONG");
                        await ws.Socket.SendAsync(new ArraySegment<byte>(rep), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    textBox2.Text = "收到：" + text;
                    byte[] reply = Encoding.UTF8.GetBytes("服务端收到：" + text);
                    await ws.Socket.SendAsync(new ArraySegment<byte>(reply), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            ws.Socket.Dispose();
        }
        private async void SendPingAndCheck(WebSocketInfo item)
        {
            if (item.Socket.State != WebSocketState.Open)
                return;

            // 丢失次数累加
            item.LostPongCount++;
            if (item.LostPongCount > 3)   //超过3次得不到客户端的心跳回应，则主动断开此连接
            {
                // 心跳超时，强制关闭连接
                await item.Socket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "心跳超时", CancellationToken.None);
                webSockets.Remove(item);
                return;
            }

            // 发送PING
            byte[] pingBuf = System.Text.Encoding.UTF8.GetBytes("PING");   //发送PING信号
            var seg = new ArraySegment<byte>(pingBuf);
            await item.Socket.SendAsync(seg, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        private async void SendMes(string message)
        {
            if(webSockets.Count>0)
            {
                foreach(WebSocketInfo ws in webSockets)
                {
                    if(ws.LostPongCount<=3)
                    {
                        byte[] reply = Encoding.UTF8.GetBytes("服务器发送：" + message);
                        await ws.Socket.SendAsync(new ArraySegment<byte>(reply), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else
                    {
                        webSockets.Remove(ws);   //服务端消除此连接
                    }
                    
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "服务器发送：" + textBox1.Text;
            SendMes("服务器发送：" + textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listener?.Prefixes.Clear();
            listener?.Stop();
            listener?.Close();
            button3.Enabled = false;
            button1.Enabled = true;
        }
    }
}

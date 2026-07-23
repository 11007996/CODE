using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wsServer
{
    class WebSocketInfo
    {
        public WebSocket Socket { set; get; }
        public int LostPongCount { set; get; }
        public Timer timer { set; get; }

        public WebSocketInfo(WebSocket webSocket)
        {
            this.Socket = webSocket;
            LostPongCount = 0;
        }

        private async void SendPingAndCheck(WebSocketInfo item)
        {
            if (item.Socket.State != WebSocketState.Open)
                return;

            // 丢失次数累加
            item.LostPongCount++;
            if (item.LostPongCount > 3)
            {
                // 心跳超时，强制关闭连接
                await item.Socket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "心跳超时", CancellationToken.None);
                return;
            }

            // 发送PING
            byte[] pingBuf = System.Text.Encoding.UTF8.GetBytes("PING");
            var seg = new ArraySegment<byte>(pingBuf);
            await item.Socket.SendAsync(seg, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}

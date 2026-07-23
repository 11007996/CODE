using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketClient
{
    private ClientWebSocket _ws;
    private CancellationTokenSource _cts;
    private readonly string _url;

    // 收到文本消息回调
    public Action<string> OnTextMessage { get; set; }
    // 收到二进制消息回调
    public Action<byte[]> OnBinaryMessage { get; set; }
    // 连接关闭回调
    public Action<string> OnClosed { get; set; }
    // 异常回调
    public Action<Exception> OnError { get; set; }
    private int lostnum = 0;
    private int recon = 0;

    public WebSocketClient(string url)
    {
        _url = url;
        _ws = new ClientWebSocket();
        _cts = new CancellationTokenSource();
    }

    /// <summary>
    /// 建立连接并启动持续接收循环
    /// </summary>
    public async Task ConnectAsync()
    {
        try
        {
            await _ws.ConnectAsync(new Uri(_url), _cts.Token);
            Timer timer = new Timer(_=> SendNumAsync("PING"),null,0,5000);
            // 新开任务持续接收消息（不阻塞主线程）
            _ = ReceiveLoop();
        }
        catch (Exception ex)
        {
            OnError?.Invoke(ex);
        }
    }

    /// <summary>
    /// 核心：循环接收数据
    /// </summary>
    private async Task ReceiveLoop()
    {
        // 缓冲区，按需调整大小
        byte[] buffer = new byte[4096];
        ArraySegment<byte> seg = new ArraySegment<byte>(buffer);

        while (_ws.State == WebSocketState.Open && !_cts.Token.IsCancellationRequested)
        {
            try
            {
                // 阻塞等待服务端下发数据
                WebSocketReceiveResult result = await _ws.ReceiveAsync(seg, _cts.Token);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    // 收到关闭帧，主动回应关闭
                    await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "客户端主动关闭", _cts.Token);
                    OnClosed?.Invoke("服务端下发关闭指令");
                    break;
                }
                else if (result.MessageType == WebSocketMessageType.Text)
                {
                    // 截取有效字节转字符串
                    string text = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    if(text == "PING")
                    {
                        await SendTextAsync("PONG");
                    }
                    else if(text == "PONG")
                    {
                        lostnum = 0;
                    }
                    OnTextMessage?.Invoke(text);
                }
                else if (result.MessageType == WebSocketMessageType.Binary)
                {
                    // 复制有效二进制数据
                    byte[] data = new byte[result.Count];
                    Array.Copy(buffer, data, result.Count);
                    OnBinaryMessage?.Invoke(data);
                }
            }
            catch (OperationCanceledException)
            {
                // 主动取消，正常退出
                break;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
                break;
            }
        }
    }

    /// <summary>
    /// 发送文本
    /// </summary>
    public async Task SendTextAsync(string msg)
    {
        if (_ws.State != WebSocketState.Open) return;
        byte[] buf = Encoding.UTF8.GetBytes(msg);
        ArraySegment<byte> seg = new ArraySegment<byte>(buf);
        await _ws.SendAsync(seg, WebSocketMessageType.Text, endOfMessage: true, _cts.Token);
    }

    public async Task SendNumAsync(string msg)
    {
        await SendTextAsync(msg);
        lostnum++;
        if(lostnum>3)
        {
            recon++;
            if(recon>2)
            {
                throw new WebSocketException("服务器断开");
            }
            await CloseAsync();
            await ConnectAsync();
        }
    }

    /// <summary>
    /// 发送二进制
    /// </summary>
    public async Task SendBinaryAsync(byte[] data)
    {
        if (_ws.State != WebSocketState.Open) return;
        ArraySegment<byte> seg = new ArraySegment<byte>(data);
        await _ws.SendAsync(seg, WebSocketMessageType.Binary, endOfMessage: true, _cts.Token);
    }

    /// <summary>
    /// 断开连接释放资源
    /// </summary>
    public async Task CloseAsync()
    {
        if (_ws.State == WebSocketState.Open)
        {
            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "客户端主动断开", CancellationToken.None);
        }
        _cts.Cancel();
        _ws.Dispose();
        _cts.Dispose();
    }
}
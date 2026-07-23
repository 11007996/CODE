using System.Diagnostics;
using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.VideoSource;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.VideoSource.Rtsp;

public class RtspSource : IVideoSource
{
    private readonly ILogger<RtspSource> _logger;
    private readonly string _rtspUrl;
    private VideoCapture? _capture;
    private int _reconnectAttempts;
    private bool _isConnected;

    private const int MaxReconnectAttempts = 3;
    /// <summary>
    /// Grab() 耗时低于此阈值视为"从缓冲区取出"（即积压帧），
    /// 超过则说明在等待网络上的下一帧，已追赶到实时。
    /// </summary>
    private const double GrabBufferedThresholdMs = 5.0;

    public string SourceType => "RTSP";
    public bool IsOpened => _isConnected && _capture?.IsOpened() == true;
    public bool IsLoop => false;

    public RtspSource(string rtspUrl, ILogger<RtspSource> logger)
    {
        _rtspUrl = rtspUrl;
        _logger = logger;
    }

    public void Open()
    {
        ReleaseCapture();

        try
        {
            _logger.LogInformation("正在连接 RTSP 流: {RtspUrl}", _rtspUrl);

            SetFfmpegLowLatencyEnvironment();

            var lowLatencyUrl = BuildLowLatencyUrl(_rtspUrl);

            _capture = new VideoCapture(lowLatencyUrl, VideoCaptureAPIs.FFMPEG);

            ConfigureLowLatency();

            if (!_capture.IsOpened())
            {
                _logger.LogError("RTSP VideoCapture.IsOpened=false: {RtspUrl}", _rtspUrl);
                ReleaseCapture();
                throw new InvalidOperationException("无法打开 RTSP 流");
            }

            _isConnected = true;

            using (var probe = new Mat())
            {
                if (!_capture.Read(probe) || probe.Empty())
                    _logger.LogWarning(
                        "RTSP 已连接但首帧读取为空，可能尚未出图或需要等待: {RtspUrl}",
                        _rtspUrl);
                else
                    _logger.LogDebug(
                        "RTSP 首帧探测成功: {Width}x{Height}",
                        probe.Width, probe.Height);
            }

            _logger.LogInformation("RTSP 流打开成功 (低延迟模式): {RtspUrl}", _rtspUrl);
        }
        catch (Exception ex)
        {
            _isConnected = false;
            _capture?.Dispose();
            _capture = null;
            _logger.LogError(ex, "RTSP 流连接失败: {RtspUrl}", _rtspUrl);
            throw new InvalidOperationException(
                $"无法打开 RTSP 流: {_rtspUrl}\n\n" +
                $"错误: {ex.Message}\n\n" +
                $"可能的原因：\n" +
                $"1. RTSP URL 不正确\n" +
                $"2. 网络连接问题\n" +
                $"3. 摄像头不在线或需要认证\n" +
                $"4. 防火墙阻止了连接",
                ex);
        }
    }

    /// <summary>
    /// 通过环境变量向 FFmpeg 后端注入低延迟选项（全平台 + Windows 追加套接字超时）。
    /// </summary>
    private void SetFfmpegLowLatencyEnvironment()
    {
        var win = OperatingSystem.IsWindows();
        FfmpegCaptureEnvironment.ApplyRtspOptions(win);
        _logger.LogInformation(
            "已设置 FFmpeg 低延迟环境变量 (RTSP, WindowsExtra={Win}): {Options}",
            win, FfmpegCaptureEnvironment.BuildRtspOptions(win));
    }

    /// <summary>
    /// 在 URL 上追加 <c>rtsp_transport=tcp</c>（若尚未指定），以降低丢包与花屏概率。
    /// </summary>
    private static string BuildLowLatencyUrl(string originalUrl)
    {
        if (string.IsNullOrWhiteSpace(originalUrl))
            return originalUrl;

        if (originalUrl.IndexOf("rtsp_transport=", StringComparison.OrdinalIgnoreCase) >= 0)
            return originalUrl;

        return originalUrl.Contains('?')
            ? originalUrl + "&rtsp_transport=tcp"
            : originalUrl + "?rtsp_transport=tcp";
    }

    private void ConfigureLowLatency()
    {
        if (_capture == null) return;

        try
        {
            _capture.Set(VideoCaptureProperties.BufferSize, 1);
            _logger.LogInformation("已配置低延迟参数: TCP传输 + 最小缓冲区");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "配置低延迟参数时出现警告");
        }
    }

    /// <summary>
    /// 读取最新帧。使用 Grab/Retrieve 分离策略排空 FFmpeg 内部缓冲中的旧帧：
    /// <list type="number">
    ///   <item>先 Grab() 一帧（必须，可能阻塞等待网络）</item>
    ///   <item>再循环 Grab()，只要耗时 &lt; 5ms 就说明是缓冲积压帧，继续排空</item>
    ///   <item>一旦 Grab() 耗时 &gt;= 5ms，说明已追赶到实时，停止排空</item>
    ///   <item>只对最后一次 Grab 的帧执行 Retrieve() 解码</item>
    /// </list>
    /// </summary>
    public Frame? ReadFrame()
    {
        while (true)
        {
            if (!_isConnected || _capture == null || !_capture.IsOpened())
                return null;

            var firstTs = Stopwatch.GetTimestamp();
            if (!_capture.Grab())
            {
                if (_reconnectAttempts >= MaxReconnectAttempts)
                {
                    _reconnectAttempts = 0;
                    return null;
                }

                _reconnectAttempts++;
                _logger.LogWarning("RTSP 流读取失败，尝试重连 ({Attempt}/{Max})",
                    _reconnectAttempts, MaxReconnectAttempts);

                try
                {
                    ReleaseCapture();
                    Thread.Sleep(1000);
                    Open();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "RTSP 流重连失败");
                    _reconnectAttempts = 0;
                    return null;
                }

                continue;
            }

            var firstGrabMs = (Stopwatch.GetTimestamp() - firstTs) * 1000.0 / Stopwatch.Frequency;

            // 仅当第一个 Grab 瞬间返回（< 5ms）时才排空：
            // 瞬间返回 = 帧来自 FFmpeg 内部积压缓冲，后面可能还有更多旧帧；
            // 慢返回（>= 5ms）= 等待了网络帧，缓冲区本身是空的，无需排空。
            if (firstGrabMs < GrabBufferedThresholdMs)
            {
                var drainCount = 0;
                while (drainCount < 30)
                {
                    var ts = Stopwatch.GetTimestamp();
                    if (!_capture.Grab())
                        break;
                    var elapsedMs = (Stopwatch.GetTimestamp() - ts) * 1000.0 / Stopwatch.Frequency;
                    if (elapsedMs >= GrabBufferedThresholdMs)
                        break;
                    drainCount++;
                }

                if (drainCount > 0)
                    _logger.LogDebug("RTSP: 排空 {Count} 帧积压缓冲", drainCount);
            }

            using var mat = new Mat();
            if (!_capture.Retrieve(mat) || mat.Empty())
            {
                _logger.LogDebug("RTSP Retrieve 失败，跳过本帧");
                continue;
            }

            _reconnectAttempts = 0;
            return new Frame(mat.Clone());
        }
    }

    public void Reset()
    {
        // RTSP 流不支持重置
    }

    /// <summary>释放底层捕获，供外部关闭或下一次 <see cref="Open"/> 前清理；会重置重连计数。</summary>
    public void Close()
    {
        _reconnectAttempts = 0;
        ReleaseCapture();
    }

    /// <summary>仅释放捕获与连接标志，不重置重连计数（用于读失败后的重连路径）。</summary>
    private void ReleaseCapture()
    {
        _isConnected = false;
        _capture?.Dispose();
        _capture = null;
    }

    public int GetWidth()
    {
        return (int)(_capture?.Get(VideoCaptureProperties.FrameWidth) ?? 0);
    }

    public int GetHeight()
    {
        return (int)(_capture?.Get(VideoCaptureProperties.FrameHeight) ?? 0);
    }

    public double GetFps()
    {
        var fps = _capture?.Get(VideoCaptureProperties.Fps) ?? 25.0;
        return fps > 0 ? fps : 25.0;
    }

    public void Dispose()
    {
        Close();
        GC.SuppressFinalize(this);
    }
}

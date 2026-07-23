using System.Collections.Concurrent;
using LuxVideoDet.Core.Storage;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Web.Services;

/// <summary>
/// MJPEG 流管理器 — 为每路检测维护最新帧，支持多客户端订阅。
/// <para>
/// 在 <see cref="PushFrame"/> 内同步将 <see cref="Mat"/> 编码为 JPEG（与算法共用同一帧缓冲，不 <see cref="Mat.Clone"/>）。
/// 若改为异步编码，必须在入队前复制整帧，大分辨率下 memcpy 会在检测回调线程上造成严重掉帧，实测可差一个数量级。
/// </para>
/// <para>
/// 默认将宽度超过配置上限的帧先缩小再编码，显著降低 JPEG 编码耗时；与 Desktop 本地预览（不经 JPEG）相比，Web 仍多一道编码，但可接近同一推理帧率。
/// </para>
/// </summary>
public sealed class MjpegStreamManager : IDisposable
{
    private readonly ConcurrentDictionary<string, ChannelState> _channels = new();
    private readonly ILogger<MjpegStreamManager> _logger;
    private readonly int _jpegQuality;
    /// <summary>0 表示不缩放；否则宽大于此值时按比例缩小（仅影响预览流）。</summary>
    private readonly int _maxPreviewWidth;

    public MjpegStreamManager(
        ILogger<MjpegStreamManager> logger,
        int jpegQuality = 72,
        int maxPreviewWidth = 1280)
    {
        _logger = logger;
        _jpegQuality = jpegQuality;
        _maxPreviewWidth = maxPreviewWidth;
    }

    /// <summary>
    /// 推送一帧到指定频道（由 WebDetectionService 在结果回调中调用）。
    /// </summary>
    public void PushFrame(string configId, Mat frame)
    {
        if (frame == null || frame.Empty()) return;

        var channel = _channels.GetOrAdd(configId, _ => new ChannelState());

        Mat? work = null;
        var needDispose = false;
        try
        {
            if (_maxPreviewWidth > 0 && frame.Width > _maxPreviewWidth)
            {
                var w = _maxPreviewWidth;
                var h = Math.Max(1, (int)(frame.Height * (double)w / frame.Width));
                work = new Mat();
                Cv2.Resize(frame, work, new Size(w, h), 0, 0, InterpolationFlags.Nearest);
                needDispose = true;
            }
            else
            {
                work = frame;
            }

            var jpegBytes = CrossPlatformMediaWriter.EncodeMatToJpeg(work, _jpegQuality);
            if (jpegBytes == null || jpegBytes.Length == 0)
            {
                _logger.LogWarning("MJPEG 编码返回空: {ConfigId}", configId);
                return;
            }

            channel.UpdateFrame(jpegBytes);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "MJPEG 编码失败: {ConfigId}", configId);
        }
        finally
        {
            if (needDispose)
                work?.Dispose();
        }
    }

    /// <summary>
    /// 订阅 MJPEG 流 — 持续向 HTTP Response 写入 JPEG 帧。
    /// </summary>
    public async Task SubscribeAsync(string configId, Stream outputStream, CancellationToken ct)
    {
        var channel = _channels.GetOrAdd(configId, _ => new ChannelState());
        var boundary = "--frame";
        long lastVersion = -1;

        _logger.LogInformation("MJPEG 客户端已连接: {ConfigId}", configId);

        try
        {
            while (!ct.IsCancellationRequested)
            {
                var (jpeg, version) = channel.GetLatestFrame();

                if (jpeg != null && version != lastVersion)
                {
                    lastVersion = version;

                    var header = $"{boundary}\r\nContent-Type: image/jpeg\r\nContent-Length: {jpeg.Length}\r\n\r\n";
                    var headerBytes = System.Text.Encoding.ASCII.GetBytes(header);

                    await outputStream.WriteAsync(headerBytes, ct);
                    await outputStream.WriteAsync(jpeg, ct);
                    await outputStream.WriteAsync("\r\n"u8.ToArray(), ct);
                    await outputStream.FlushAsync(ct);
                }
                else
                {
                    await Task.Delay(33, ct); // ~30fps polling
                }
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) when (ex is IOException || ex.InnerException is IOException)
        {
            // 客户端断开连接
        }
        finally
        {
            _logger.LogInformation("MJPEG 客户端已断开: {ConfigId}", configId);
        }
    }

    /// <summary>
    /// 获取指定频道的最新帧快照（用于单帧预览）。
    /// </summary>
    public byte[]? GetSnapshot(string configId)
    {
        if (_channels.TryGetValue(configId, out var channel))
        {
            var (jpeg, _) = channel.GetLatestFrame();
            return jpeg;
        }

        return null;
    }

    public bool HasChannel(string configId) => _channels.ContainsKey(configId);

    public void RemoveChannel(string configId)
    {
        _channels.TryRemove(configId, out _);
    }

    public void Dispose()
    {
        _channels.Clear();
    }

    /// <summary>
    /// 单个频道的状态：存储最新 JPEG 帧和版本号。
    /// </summary>
    private sealed class ChannelState
    {
        private byte[]? _latestJpeg;
        private long _version;
        private readonly object _lock = new();

        public void UpdateFrame(byte[] jpeg)
        {
            lock (_lock)
            {
                _latestJpeg = jpeg;
                _version++;
            }
        }

        public (byte[]? Jpeg, long Version) GetLatestFrame()
        {
            lock (_lock)
            {
                return (_latestJpeg, _version);
            }
        }
    }
}

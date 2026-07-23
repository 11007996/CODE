using System.Diagnostics;
using System.Globalization;
using Microsoft.Extensions.Logging;
using OpenCvSharp;
using SkiaSharp;

namespace LuxVideoDet.Core.Storage;

/// <summary>
/// 跨平台图片/视频写入。
/// 图片：使用 SkiaSharp 编码，避免依赖 OpenCV imgcodecs 模块。
/// 视频：通过 FFmpeg 子进程接收 raw BGR 管道，编码为 H.264 MP4。
/// </summary>
public static class CrossPlatformMediaWriter
{
    /// <summary>
    /// 将 OpenCV Mat (BGR) 保存为 JPEG 文件。
    /// </summary>
    public static bool SaveImage(Mat image, string filePath, int jpegQuality = 90)
    {
        if (image == null || image.Empty())
            return false;

        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);

        var width = image.Width;
        var height = image.Height;

        // 确保是 BGR 3 通道
        using var bgr = image.Channels() == 3 ? image : new Mat();
        if (image.Channels() != 3)
            Cv2.CvtColor(image, bgr, ColorConversionCodes.GRAY2BGR);

        var sourceImage = image.Channels() == 3 ? image : bgr;

        // BGR → BGRA (SkiaSharp 的 Bgra8888 格式)
        using var bgra = new Mat();
        Cv2.CvtColor(sourceImage, bgra, ColorConversionCodes.BGR2BGRA);

        using var skBitmap = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);
        var srcSpan = bgra.AsSpan<byte>();
        var dstSpan = skBitmap.GetPixelSpan();
        srcSpan.CopyTo(dstSpan);

        using var skImage = SKImage.FromBitmap(skBitmap);
        using var data = skImage.Encode(SKEncodedImageFormat.Jpeg, jpegQuality);
        using var fs = File.Create(filePath);
        data.SaveTo(fs);

        return true;
    }

    /// <summary>
    /// 将 Mat 编码为 JPEG 字节。
    /// <para>
    /// 优先使用 OpenCV <see cref="Cv2.ImEncode"/>（通常比 Skia 路径快，且少一次 BGR→BGRA 全图拷贝）；
    /// 在缺少 imgcodecs 等环境下失败时回退到 SkiaSharp（与历史行为一致）。
    /// </para>
    /// </summary>
    public static byte[]? EncodeMatToJpeg(Mat image, int jpegQuality = 90)
    {
        if (image == null || image.Empty())
            return null;

        try
        {
            // 确保 BGR 三通道
            using var bgrOwned = new Mat();
            Mat bgr;
            if (image.Channels() == 3)
            {
                bgr = image;
            }
            else
            {
                switch (image.Channels())
                {
                    case 1:
                        Cv2.CvtColor(image, bgrOwned, ColorConversionCodes.GRAY2BGR);
                        break;
                    case 4:
                        Cv2.CvtColor(image, bgrOwned, ColorConversionCodes.BGRA2BGR);
                        break;
                    default:
                        return null;
                }

                bgr = bgrOwned;
            }

            var q = Math.Clamp(jpegQuality, 1, 100);
            try
            {
                Cv2.ImEncode(".jpg", bgr, out byte[] buf,
                    new[] { (int)ImwriteFlags.JpegQuality, q });
                if (buf is { Length: > 0 })
                    return buf;
            }
            catch
            {
                // 部分环境无 imgcodecs，走下方 Skia
            }

            var width = bgr.Width;
            var height = bgr.Height;

            using var bgra = new Mat();
            Cv2.CvtColor(bgr, bgra, ColorConversionCodes.BGR2BGRA);

            using var skBitmap = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);
            bgra.AsSpan<byte>().CopyTo(skBitmap.GetPixelSpan());

            using var skImage = SKImage.FromBitmap(skBitmap);
            using var data = skImage.Encode(SKEncodedImageFormat.Jpeg, jpegQuality);
            return data.ToArray();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 将帧列表通过 FFmpeg 编码保存为 H.264 MP4 视频。
    /// </summary>
    public static bool SaveVideo(
        IReadOnlyList<Mat> frames, string filePath, double fps,
        ILogger? logger = null)
    {
        if (frames == null || frames.Count == 0)
            return false;

        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);

        var firstValid = frames.FirstOrDefault(f => f != null && !f.Empty());
        if (firstValid == null)
        {
            logger?.LogWarning("所有帧均为空，无法保存视频: {FilePath}", filePath);
            return false;
        }

        var width = firstValid.Width;
        var height = firstValid.Height;
        var pixelFormat = "bgr24";

        var ffmpegPath = FindFFmpeg();
        if (ffmpegPath == null)
        {
            logger?.LogWarning("未找到 ffmpeg，无法保存视频: {FilePath}", filePath);
            return false;
        }

        var rStr = fps.ToString("0.###", CultureInfo.InvariantCulture);
        var args = $"-y -f rawvideo -pix_fmt {pixelFormat} -s {width}x{height} -r {rStr} " +
                   $"-i pipe:0 -c:v libx264 -preset ultrafast -crf 23 -pix_fmt yuv420p " +
                   $"-movflags +faststart \"{filePath}\"";

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        try
        {
            process.Start();

            // 异步消费 stderr 防止缓冲区满导致死锁
            var stderrTask = process.StandardError.ReadToEndAsync();

            var stdinStream = process.StandardInput.BaseStream;
            var expectedBytes = width * height * 3;
            var writtenFrames = 0;

            foreach (var frame in frames)
            {
                if (frame == null || frame.Empty())
                    continue;

                Mat source;
                Mat? convertedBgr = null;
                try
                {
                    if (frame.Channels() == 3)
                    {
                        source = frame;
                    }
                    else
                    {
                        convertedBgr = new Mat();
                        Cv2.CvtColor(frame, convertedBgr, ColorConversionCodes.GRAY2BGR);
                        source = convertedBgr;
                    }

                    using var continuous = source.IsContinuous() ? null : source.Clone();
                    var actualSource = source.IsContinuous() ? source : continuous!;

                    unsafe
                    {
                        var span = new ReadOnlySpan<byte>((void*)actualSource.DataPointer, expectedBytes);
                        stdinStream.Write(span);
                    }

                    writtenFrames++;
                }
                finally
                {
                    convertedBgr?.Dispose();
                }
            }

            stdinStream.Flush();
            stdinStream.Close();

            var exited = process.WaitForExit(30_000);
            var stderr = stderrTask.GetAwaiter().GetResult();

            if (!exited)
            {
                logger?.LogWarning("FFmpeg 编码超时 (30秒)，强制终止: {FilePath}", filePath);
                try { process.Kill(); } catch { }
                return false;
            }

            if (process.ExitCode != 0)
            {
                logger?.LogWarning("FFmpeg 退出码 {ExitCode}: {Stderr}", process.ExitCode, stderr);
                return false;
            }

            logger?.LogDebug("FFmpeg 编码成功: {FilePath}, {FrameCount} 帧", filePath, writtenFrames);
            return true;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "FFmpeg 写入视频失败: {FilePath}", filePath);
            try { if (!process.HasExited) process.Kill(); } catch { }
            return false;
        }
    }

    private static string? FindFFmpeg()
    {
        if (OperatingSystem.IsWindows())
        {
            // PATH 中查找
            var pathDirs = Environment.GetEnvironmentVariable("PATH")?.Split(';') ?? [];
            foreach (var dir in pathDirs)
            {
                var candidate = Path.Combine(dir, "ffmpeg.exe");
                if (File.Exists(candidate)) return candidate;
            }
            return null;
        }

        // macOS / Linux
        string[] candidates = ["/opt/homebrew/bin/ffmpeg", "/usr/local/bin/ffmpeg", "/usr/bin/ffmpeg"];
        foreach (var c in candidates)
        {
            if (File.Exists(c)) return c;
        }

        // which fallback
        try
        {
            using var p = Process.Start(new ProcessStartInfo("which", "ffmpeg")
            {
                UseShellExecute = false, RedirectStandardOutput = true, CreateNoWindow = true
            });
            var output = p?.StandardOutput.ReadToEnd().Trim();
            p?.WaitForExit(3000);
            if (!string.IsNullOrEmpty(output) && File.Exists(output))
                return output;
        }
        catch { }

        return null;
    }
}

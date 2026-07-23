using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Storage;

public class StorageManager
{
    private readonly ILogger _logger;
    private readonly string _machineName;
    private readonly string _catchDir;
    private readonly string _retrainDir;

    public string MachineName => _machineName;

    public StorageManager(
        string machineName,
        ILogger logger,
        string catchDir = "catch",
        string retrainDir = "retrain")
    {
        _machineName = machineName;
        _logger = logger;
        _catchDir = catchDir;
        _retrainDir = retrainDir;
    }

    private string GetDirPath(string baseDir, string? timestamp = null)
    {
        string dirPath;

        if (timestamp != null && timestamp.Length >= 8)
        {
            var year = timestamp[..4];
            var month = timestamp[4..6];
            var day = timestamp[6..8];
            dirPath = Path.Combine(baseDir, _machineName, $"{year}-{month}-{day}");
        }
        else
        {
            var dt = DateTime.Now;
            dirPath = Path.Combine(baseDir, _machineName, dt.ToString("yyyy-MM-dd"));
        }

        Directory.CreateDirectory(dirPath);
        return dirPath;
    }

    private static string GetTimestamp(string? timestamp = null)
    {
        return timestamp ?? DateTime.Now.ToString("yyyyMMddHHmmss");
    }

    public string? SaveErrorImage(Mat image, string? timestamp = null)
    {
        if (image == null || image.Empty())
            return null;

        try
        {
            var ts = GetTimestamp(timestamp);
            var dirPath = GetDirPath(_catchDir, ts);
            var filepath = Path.Combine(dirPath, $"{ts}.jpg");

            CrossPlatformMediaWriter.SaveImage(image, filepath);
            return filepath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{MachineName}] 保存错误图像异常", _machineName);
            return null;
        }
    }

    public bool SaveRetrainImage(Mat image, string? timestamp = null)
    {
        if (image == null || image.Empty())
            return false;

        try
        {
            var ts = GetTimestamp(timestamp);
            var dirPath = GetDirPath(_retrainDir, ts);
            var filepath = Path.Combine(dirPath, $"{ts}.jpg");

            CrossPlatformMediaWriter.SaveImage(image, filepath);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{MachineName}] 保存重训练图像异常", _machineName);
            return false;
        }
    }

    public string? SaveErrorVideo(List<Mat> renderFrames, double fps, string codec, string? timestamp = null)
    {
        if (renderFrames == null || renderFrames.Count == 0)
        {
            _logger.LogWarning("[{MachineName}] 没有帧数据可保存", _machineName);
            return null;
        }

        try
        {
            var ts = GetTimestamp(timestamp);
            var dirPath = GetDirPath(_catchDir, ts);
            var filepath = Path.Combine(dirPath, $"{ts}.mp4");

            var success = CrossPlatformMediaWriter.SaveVideo(renderFrames, filepath, fps, _logger);
            return success ? $"{filepath}|{renderFrames.Count}" : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{MachineName}] 保存错误视频异常", _machineName);
            return null;
        }
    }

    public bool SaveRetrainVideo(List<Mat> rawFrames, double fps, string codec, string? timestamp = null)
    {
        if (rawFrames == null || rawFrames.Count == 0)
        {
            _logger.LogWarning("[{MachineName}] 没有帧数据可保存", _machineName);
            return false;
        }

        try
        {
            var ts = GetTimestamp(timestamp);
            var dirPath = GetDirPath(_retrainDir, ts);
            var filepath = Path.Combine(dirPath, $"{ts}.mp4");

            return CrossPlatformMediaWriter.SaveVideo(rawFrames, filepath, fps, _logger);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{MachineName}] 保存重训练视频异常", _machineName);
            return false;
        }
    }
}

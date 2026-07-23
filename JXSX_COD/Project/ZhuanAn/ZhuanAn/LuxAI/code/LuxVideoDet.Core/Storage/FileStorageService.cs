using LuxVideoDet.Core.Common;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Storage;

/// <summary>
/// 文件存储服务 - 保存图像和视频到本地文件系统
/// </summary>
public class FileStorageService : IStorageService
{
    private readonly string _machineName;
    private readonly string _errorImagePath;
    private readonly string _retrainImagePath;
    private readonly ILogger<FileStorageService> _logger;

    public FileStorageService(
        string machineName,
        string errorImagePath,
        string retrainImagePath,
        ILogger<FileStorageService> logger)
    {
        _machineName = machineName;
        _errorImagePath = errorImagePath;
        _retrainImagePath = retrainImagePath;
        _logger = logger;

        // 确保目录存在
        Directory.CreateDirectory(_errorImagePath);
        Directory.CreateDirectory(_retrainImagePath);
    }

    public async Task<string?> SaveErrorImageAsync(
        Frame frame,
        string? timestamp = null,
        CancellationToken cancellationToken = default)
    {
        if (frame.IsEmpty)
        {
            _logger.LogWarning("帧为空，无法保存错误图像");
            return null;
        }

        try
        {
            var ts = timestamp ?? DateTime.Now.ToString("yyyyMMddHHmmss");
            var dirPath = GetDirPath(_errorImagePath, ts);
            var filePath = Path.Combine(dirPath, $"{ts}.jpg");

            await Task.Run(() => CrossPlatformMediaWriter.SaveImage(frame.Mat, filePath), cancellationToken);

            _logger.LogError("错误图像已保存: MachineName={MachineName}, Path={Path}",
                _machineName, filePath);

            return filePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存错误图像失败: MachineName={MachineName}",
                _machineName);
            return null;
        }
    }

    public async Task<bool> SaveRetrainImageAsync(
        Frame frame,
        string? timestamp = null,
        CancellationToken cancellationToken = default)
    {
        if (frame.IsEmpty)
        {
            _logger.LogWarning("帧为空，无法保存重训练图像");
            return false;
        }

        try
        {
            var ts = timestamp ?? DateTime.Now.ToString("yyyyMMddHHmmss");
            var dirPath = GetDirPath(_retrainImagePath, ts);
            var filePath = Path.Combine(dirPath, $"{ts}.jpg");

            await Task.Run(() => CrossPlatformMediaWriter.SaveImage(frame.Mat, filePath), cancellationToken);

            _logger.LogInformation("重训练图像已保存: MachineName={MachineName}, Path={Path}",
                _machineName, filePath);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存重训练图像失败: MachineName={MachineName}",
                _machineName);
            return false;
        }
    }

    public async Task<string?> SaveVideoAsync(
        List<Frame> frames,
        double fps,
        string? timestamp = null,
        CancellationToken cancellationToken = default)
    {
        if (frames == null || frames.Count == 0)
        {
            _logger.LogWarning("帧列表为空，无法保存视频");
            return null;
        }

        try
        {
            var ts = timestamp ?? DateTime.Now.ToString("yyyyMMddHHmmss");
            var dirPath = GetDirPath(_errorImagePath, ts);
            var filePath = Path.Combine(dirPath, $"{ts}.mp4");

            await Task.Run(() =>
            {
                var mats = frames.Select(f => f.Mat).ToList();
                CrossPlatformMediaWriter.SaveVideo(mats, filePath, fps, _logger);
            }, cancellationToken);

            _logger.LogInformation("视频已保存: MachineName={MachineName}, Path={Path}, FrameCount={Count}",
                _machineName, filePath, frames.Count);

            return filePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存视频失败: MachineName={MachineName}",
                _machineName);
            return null;
        }
    }

    public async Task CleanupOldFilesAsync(
        int retentionDays,
        CancellationToken cancellationToken = default)
    {
        if (retentionDays <= 0)
        {
            _logger.LogInformation("保留天数为 0，跳过清理");
            return;
        }

        try
        {
            var cutoffDate = DateTime.Now.AddDays(-retentionDays);

            await Task.Run(() =>
            {
                CleanupDirectory(_errorImagePath, cutoffDate);
                CleanupDirectory(_retrainImagePath, cutoffDate);
            }, cancellationToken);

            _logger.LogInformation("文件清理完成: RetentionDays={Days}", retentionDays);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "文件清理失败");
        }
    }

    private void CleanupDirectory(string baseDir, DateTime cutoffDate)
    {
        if (!Directory.Exists(baseDir))
            return;

        var machineDir = Path.Combine(baseDir, _machineName);
        if (!Directory.Exists(machineDir))
            return;

        var directories = Directory.GetDirectories(machineDir);

        foreach (var dir in directories)
        {
            var dirInfo = new DirectoryInfo(dir);

            if (dirInfo.CreationTime < cutoffDate)
            {
                try
                {
                    Directory.Delete(dir, true);
                    _logger.LogInformation("已删除过期目录: {Path}", dir);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "删除目录失败: {Path}", dir);
                }
            }
        }
    }

    private string GetDirPath(string baseDir, string timestamp)
    {
        string dirPath;

        if (timestamp.Length >= 8)
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
}

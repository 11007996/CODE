using LuxVideoDet.Core.Configuration.Models;

namespace LuxVideoDet.Core.S3Storage;

/// <summary>
/// S3 兼容对象存储同步服务（插件与模型文件）。
/// </summary>
public interface IS3StorageSyncService
{
    Task<bool> InitializeAsync(CancellationToken cancellationToken = default);

    Task<ConnectionCheckResult> CheckConnectionAsync(CancellationToken cancellationToken = default);

    Task<SyncResult> SyncAllAsync(CancellationToken cancellationToken = default);

    Task<SyncResult> SyncPluginsAsync(CancellationToken cancellationToken = default);

    Task<SyncResult> SyncModelsAsync(CancellationToken cancellationToken = default);

    Task<SyncResult> SyncVideosAsync(CancellationToken cancellationToken = default);

    Task<SyncResult> SyncZipPackagesAsync(CancellationToken cancellationToken = default);

    Task<SyncResult> ForceSyncAllAsync(CancellationToken cancellationToken = default);

    Task<FileSyncStatus> CheckFileSyncStatusAsync(string fileName, string fileType, CancellationToken cancellationToken = default);

    Task<S3StorageSyncStats> GetSyncStatsAsync(CancellationToken cancellationToken = default);

    Task<List<FileSyncStatus>> GetFileListAsync(string fileType, CancellationToken cancellationToken = default);

    Task<FileSyncStatus> DownloadFileAsync(string remotePath, string localPath, CancellationToken cancellationToken = default);

    Task<FileSyncStatus> UploadFileAsync(string localPath, string remotePath, CancellationToken cancellationToken = default);

    Task<bool> DeleteRemoteFileAsync(string remotePath, CancellationToken cancellationToken = default);

    void StartAutoSync();

    void StopAutoSync();

    event EventHandler<SyncProgressEventArgs>? SyncProgress;

    event EventHandler<FileSyncEventArgs>? FileSyncCompleted;

    event EventHandler<SyncCompletedEventArgs>? SyncCompleted;

    event EventHandler<PluginUpdateEventArgs>? PluginUpdating;

    event EventHandler<PluginUpdateEventArgs>? PluginUpdated;
}

public class ConnectionCheckResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? ErrorDetail { get; set; }
    public Exception? Exception { get; set; }

    public static ConnectionCheckResult Ok(string message = "连接成功") => new() { Success = true, Message = message };
    public static ConnectionCheckResult Fail(string message, string? errorDetail = null, Exception? ex = null) =>
        new() { Success = false, Message = message, ErrorDetail = errorDetail, Exception = ex };
}

public class SyncProgressEventArgs : EventArgs
{
    public string CurrentFile { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public int Current { get; set; }
    public int Total { get; set; }
    public double Percentage => Total > 0 ? (Current * 100.0 / Total) : 0;
    public string Message { get; set; } = string.Empty;
}

public class FileSyncEventArgs : EventArgs
{
    public FileSyncStatus FileStatus { get; set; } = null!;
}

public class SyncCompletedEventArgs : EventArgs
{
    public SyncResult SyncResult { get; set; } = null!;
}

public class PluginUpdateEventArgs : EventArgs
{
    public bool ShouldStopDetection { get; set; }
    public List<string> UpdatedFiles { get; set; } = new();
    public bool NeedReloadPlugins { get; set; }
    public string Message { get; set; } = string.Empty;
}

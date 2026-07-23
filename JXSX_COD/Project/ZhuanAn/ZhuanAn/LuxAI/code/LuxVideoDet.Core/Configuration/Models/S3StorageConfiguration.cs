using System.Text.Json.Serialization;

namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 已知的 S3 兼容对象存储实现标识（用于 <see cref="S3StorageConfiguration.Provider"/>）。
/// </summary>
public static class S3StorageProviders
{
    /// <summary>使用 MinIO 官方 .NET SDK（S3 兼容 API，适用于 MinIO、多数自建与云厂商 S3 端点）。</summary>
    public const string MinioSdk = "minio-sdk";

    public static bool IsSupported(string? provider)
    {
        if (string.IsNullOrWhiteSpace(provider))
            return true;
        return string.Equals(provider.Trim(), MinioSdk, StringComparison.OrdinalIgnoreCase);
    }

    public static string NormalizeProvider(string? provider) =>
        string.IsNullOrWhiteSpace(provider) ? MinioSdk : provider.Trim();
}

/// <summary>
/// S3 兼容对象存储配置（远程插件与模型同步）。JSON 中历史字段仍常写作 minIO。
/// </summary>
public class S3StorageConfiguration
{
    /// <summary>
    /// 后端实现，默认 <see cref="S3StorageProviders.MinioSdk"/>。未来可扩展其他 S3 客户端实现。
    /// </summary>
    public string Provider { get; set; } = S3StorageProviders.MinioSdk;

    /// <summary>是否启用从对象存储同步</summary>
    public bool Enabled { get; set; } = false;

    /// <summary>服务端点（主机:端口，勿带协议前缀）</summary>
    public string Endpoint { get; set; } = "localhost:9000";

    /// <summary>登录账号（S3 Access Key）</summary>
    public string AccessKey { get; set; } = "admin";

    /// <summary>登录密码（S3 Secret Key）</summary>
    public string SecretKey { get; set; } = "admin123";

    /// <summary>是否使用 HTTPS</summary>
    public bool UseSsl { get; set; } = false;

    /// <summary>存储桶名称</summary>
    public string BucketName { get; set; } = "luxvideodet";

    /// <summary>远程插件对象键前缀</summary>
    public string RemotePluginPath { get; set; } = "plugins/";

    /// <summary>远程模型对象键前缀</summary>
    public string RemoteModelPath { get; set; } = "models/";

    /// <summary>远程视频对象键前缀</summary>
    public string RemoteVideoPath { get; set; } = "videos/";

    /// <summary>远程压缩包对象键前缀</summary>
    public string RemoteZipPath { get; set; } = "zips/";

    /// <summary>自动同步间隔（分钟），0 表示不自动同步</summary>
    public int AutoSyncIntervalMinutes { get; set; } = 60;

    /// <summary>启动时自动同步</summary>
    public bool SyncOnStartup { get; set; } = true;

    /// <summary>使用 ETag 进行文件校验</summary>
    public bool UseETag { get; set; } = true;

    /// <summary>下载超时时间（秒）</summary>
    public int DownloadTimeoutSeconds { get; set; } = 300;

    /// <summary>最大重试次数</summary>
    public int MaxRetryCount { get; set; } = 3;

    /// <summary>重试延迟（秒）</summary>
    public int RetryDelaySeconds { get; set; } = 5;

    /// <summary>本地插件目录（从 AppConfiguration 注入，不序列化）</summary>
    [JsonIgnore]
    public string PluginDirectory { get; set; } = "plugins";

    /// <summary>本地模型目录（程序根目录下 resources/models，不序列化）</summary>
    [JsonIgnore]
    public string ModelDirectory { get; set; } = "resources/models";

    /// <summary>本地视频目录（程序根目录下 resources/videos，不序列化）</summary>
    [JsonIgnore]
    public string VideoDirectory { get; set; } = "resources/videos";

    /// <summary>本地压缩包目录（程序根目录下 resources/zips，不序列化）</summary>
    [JsonIgnore]
    public string ZipDirectory { get; set; } = "resources/zips";
}

/// <summary>
/// 文件同步状态
/// </summary>
public class FileSyncStatus
{
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public string LocalPath { get; set; } = string.Empty;
    public string RemotePath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime LastModified { get; set; }
    public string ETag { get; set; } = string.Empty;
    public SyncStatus Status { get; set; } = SyncStatus.Pending;
    public string Message { get; set; } = string.Empty;
    public DateTime SyncTime { get; set; }
}

public enum SyncStatus
{
    Pending,
    Syncing,
    Success,
    Failed,
    Skipped,
    Deleted,
    Updated,
    Downloaded
}

public class SyncResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int FileCount { get; set; }
    public int UpdatedCount { get; set; }
    public int DownloadedCount { get; set; }
    public int SkippedCount { get; set; }
    public int FailedCount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
    public List<FileSyncStatus> FileStatuses { get; set; } = new List<FileSyncStatus>();
}

/// <summary>对象存储同步统计</summary>
public class S3StorageSyncStats
{
    public int TotalFiles { get; set; }
    public int PluginFiles { get; set; }
    public int ModelFiles { get; set; }
    public int VideoFiles { get; set; }
    public int ZipFiles { get; set; }
    public long TotalSize { get; set; }
    public DateTime LastSyncTime { get; set; }
    public SyncResult? LastSyncResult { get; set; }
    public List<SyncResult> SyncHistory { get; set; } = new List<SyncResult>();
}

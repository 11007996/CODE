using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.S3Storage;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Collections.Immutable;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace LuxVideoDet.Core.S3Storage.Minio;

/// <summary>
/// S3 兼容对象存储同步（MinIO .NET SDK 实现，适用于 MinIO 及标准 S3 端点）
/// </summary>
public class MinioSdkS3StorageSyncService : IS3StorageSyncService, IDisposable
{
    private readonly IConfigurationStore _configurationStore;
    private readonly ILogger<MinioSdkS3StorageSyncService> _logger;
    private IMinioClient? _minioClient;
    private S3StorageConfiguration? _storageConfig;
    private Timer? _autoSyncTimer;
    private bool _isSyncing = false;
    private readonly object _syncLock = new object();
    private readonly Dictionary<string, string> _localFileETags = new Dictionary<string, string>();
    private S3StorageSyncStats _syncStats = new S3StorageSyncStats();

    public event EventHandler<SyncProgressEventArgs>? SyncProgress;
    public event EventHandler<FileSyncEventArgs>? FileSyncCompleted;
    public event EventHandler<SyncCompletedEventArgs>? SyncCompleted;
    public event EventHandler<PluginUpdateEventArgs>? PluginUpdating;
    public event EventHandler<PluginUpdateEventArgs>? PluginUpdated;

    public MinioSdkS3StorageSyncService(
        IConfigurationStore configurationStore,
        ILogger<MinioSdkS3StorageSyncService> logger)
    {
        _configurationStore = configurationStore;
        _logger = logger;
    }

    /// <summary>
    /// 初始化MinIO客户端
    /// </summary>
    public async Task<bool> InitializeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // 获取应用配置
            var appConfig = await _configurationStore.GetAppConfigurationAsync(cancellationToken);
            _storageConfig = appConfig.S3Storage;
            _storageConfig.Provider = S3StorageProviders.NormalizeProvider(_storageConfig.Provider);
            if (!S3StorageProviders.IsSupported(_storageConfig.Provider))
            {
                _logger.LogError(
                    "不支持的 S3 对象存储 Provider: {Provider}，当前仅实现: {Supported}",
                    _storageConfig.Provider,
                    S3StorageProviders.MinioSdk);
                return false;
            }

            // 固定同步落盘目录到程序根目录，避免不同环境下目录漂移
            var baseDir = AppContext.BaseDirectory;
            _storageConfig.PluginDirectory = Path.Combine(baseDir, "plugins");
            _storageConfig.ModelDirectory = Path.Combine(baseDir, "resources", "models");
            _storageConfig.VideoDirectory = Path.Combine(baseDir, "resources", "videos");
            _storageConfig.ZipDirectory = Path.Combine(baseDir, "resources", "zips");

            // 清理Endpoint格式：移除协议前缀（http://或https://），MinIO客户端只需要host:port
            var endpoint = _storageConfig.Endpoint.Trim();
            if (endpoint.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                endpoint = endpoint["https://".Length..];
                _storageConfig.UseSsl = true;
            }
            else if (endpoint.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                endpoint = endpoint["http://".Length..];
                _storageConfig.UseSsl = false;
            }
            // 移除末尾的斜杠
            endpoint = endpoint.TrimEnd('/');
            _storageConfig.Endpoint = endpoint;

            _logger.LogInformation("MinIO Endpoint: {Endpoint}, UseSSL: {UseSsl}", _storageConfig.Endpoint, _storageConfig.UseSsl);

            // 创建MinIO客户端（即使未启用也创建，方便用户测试连接）
            var minioClient = new MinioClient()
                .WithEndpoint(_storageConfig.Endpoint)
                .WithCredentials(_storageConfig.AccessKey, _storageConfig.SecretKey);

            if (_storageConfig.UseSsl)
            {
                minioClient = minioClient.WithSSL();
            }

            _minioClient = minioClient.Build();

            if (!_storageConfig.Enabled)
            {
                _logger.LogInformation("MinIO同步功能未启用，客户端已创建但跳过连接测试和自动同步");
                return true;
            }

            // 测试连接
            var connectionResult = await CheckConnectionAsync(cancellationToken);
            if (!connectionResult.Success)
            {
                return false;
            }

            _logger.LogInformation("MinIO同步服务初始化成功，连接到: {Endpoint}", _storageConfig.Endpoint);

            // 启动时自动同步
            if (_storageConfig.SyncOnStartup)
            {
                _ = Task.Run(async () =>
                {
                    await Task.Delay(5000, cancellationToken); // 延迟5秒，确保其他服务已启动
                    await SyncAllAsync(cancellationToken);
                }, cancellationToken);
            }

            // 启动自动同步定时器
            if (_storageConfig.AutoSyncIntervalMinutes > 0)
            {
                StartAutoSync();
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MinIO同步服务初始化失败");
            return false;
        }
    }

    /// <summary>
    /// 检查MinIO连接状态
    /// </summary>
    public async Task<ConnectionCheckResult> CheckConnectionAsync(CancellationToken cancellationToken = default)
    {
        if (_minioClient == null || _storageConfig == null)
        {
            var msg = "对象存储未初始化，请先配置并保存设置";
            _logger.LogWarning(msg);
            return ConnectionCheckResult.Fail(msg);
        }

        try
        {
            _logger.LogDebug("正在检查对象存储连接: {Endpoint}, 存储桶: {BucketName}", _storageConfig.Endpoint, _storageConfig.BucketName);

            // 检查存储桶是否存在
            var args = new BucketExistsArgs()
                .WithBucket(_storageConfig.BucketName);

            var bucketExists = await _minioClient.BucketExistsAsync(args, cancellationToken);

            if (!bucketExists)
            {
                _logger.LogWarning("MinIO存储桶不存在: {BucketName}，尝试创建", _storageConfig.BucketName);

                // 尝试创建存储桶
                try
                {
                    var makeArgs = new MakeBucketArgs()
                        .WithBucket(_storageConfig.BucketName);

                    await _minioClient.MakeBucketAsync(makeArgs, cancellationToken);
                    _logger.LogInformation("已创建MinIO存储桶: {BucketName}", _storageConfig.BucketName);
                    return ConnectionCheckResult.Ok($"连接成功，已创建存储桶: {_storageConfig.BucketName}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "创建MinIO存储桶失败: {BucketName}", _storageConfig.BucketName);
                    return ConnectionCheckResult.Fail(
                        $"存储桶 {_storageConfig.BucketName} 不存在且创建失败",
                        $"异常类型: {ex.GetType().Name}\n异常消息: {ex.Message}\n堆栈跟踪: {ex.StackTrace}",
                        ex);
                }
            }

            _logger.LogInformation("MinIO连接检查成功: {Endpoint}, 存储桶: {BucketName}", _storageConfig.Endpoint, _storageConfig.BucketName);
            return ConnectionCheckResult.Ok($"连接成功: {_storageConfig.Endpoint}, 存储桶: {_storageConfig.BucketName}");
        }
        catch (global::Minio.Exceptions.ConnectionException ex)
        {
            _logger.LogWarning("无法连接对象存储 {Endpoint}：{Message}（确认服务已启动、地址/端口/防火墙；无需远程同步可忽略。）", _storageConfig.Endpoint, ex.Message);
            return ConnectionCheckResult.Fail(
                $"无法连接到对象存储: {_storageConfig.Endpoint}（{ex.Message}）",
                "请确认服务已启动，地址、端口、网络与防火墙正确。若不需要远程同步，可在设置中关闭对象存储同步。",
                ex);
        }
        catch (global::Minio.Exceptions.AccessDeniedException ex)
        {
            _logger.LogWarning("对象存储访问被拒绝: {Endpoint}。请检查 AccessKey / SecretKey。详情: {Message}", _storageConfig.Endpoint, ex.Message);
            return ConnectionCheckResult.Fail(
                $"对象存储访问被拒绝: {_storageConfig.Endpoint}",
                "认证失败：请检查 AccessKey 与 SecretKey 是否与服务端一致。",
                ex);
        }
        catch (System.Net.Http.HttpRequestException ex)
        {
            _logger.LogWarning("对象存储请求失败: {Endpoint}。{Message}", _storageConfig.Endpoint, ex.Message);
            return ConnectionCheckResult.Fail(
                $"HTTP 请求失败: {_storageConfig.Endpoint}（{ex.Message}）",
                $"UseSSL: {_storageConfig.UseSsl}。请检查网络、TLS 与端点地址。",
                ex);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogWarning("对象存储连接超时: {Endpoint}。{Message}", _storageConfig.Endpoint, ex.Message);
            return ConnectionCheckResult.Fail(
                $"连接超时: {_storageConfig.Endpoint}",
                "在限定时间内未收到响应，可稍后重试或检查服务负载。",
                ex);
        }
        catch (Exception ex)
        {
            _logger.LogWarning("检查对象存储连接时出错: {Endpoint}。{Message}", _storageConfig.Endpoint, ex.Message);
            return ConnectionCheckResult.Fail(
                $"检查连接失败: {_storageConfig.Endpoint}（{ex.Message}）",
                ex.ToString(),
                ex);
        }
    }

    /// <summary>
    /// 同步所有文件（插件和模型）
    /// </summary>
    public async Task<SyncResult> SyncAllAsync(CancellationToken cancellationToken = default)
    {
        if (!CanSync())
        {
            return CreateFailedResult("同步服务未初始化或正在同步中");
        }

        lock (_syncLock)
        {
            if (_isSyncing)
            {
                return CreateFailedResult("同步正在进行中");
            }
            _isSyncing = true;
        }

        var result = new SyncResult
        {
            StartTime = DateTime.Now,
            Success = false,
            Message = "开始同步所有文件"
        };

        try
        {
            // 同步插件
            var pluginResult = await SyncPluginsAsync(cancellationToken);
            result.FileStatuses.AddRange(pluginResult.FileStatuses);
            result.UpdatedCount += pluginResult.UpdatedCount;
            result.DownloadedCount += pluginResult.DownloadedCount;
            result.SkippedCount += pluginResult.SkippedCount;
            result.FailedCount += pluginResult.FailedCount;

            // 同步模型
            var modelResult = await SyncModelsAsync(cancellationToken);
            result.FileStatuses.AddRange(modelResult.FileStatuses);
            result.UpdatedCount += modelResult.UpdatedCount;
            result.DownloadedCount += modelResult.DownloadedCount;
            result.SkippedCount += modelResult.SkippedCount;
            result.FailedCount += modelResult.FailedCount;

            // 同步视频
            var videoResult = await SyncVideosAsync(cancellationToken);
            result.FileStatuses.AddRange(videoResult.FileStatuses);
            result.UpdatedCount += videoResult.UpdatedCount;
            result.DownloadedCount += videoResult.DownloadedCount;
            result.SkippedCount += videoResult.SkippedCount;
            result.FailedCount += videoResult.FailedCount;

            // 同步压缩包
            var zipResult = await SyncZipPackagesAsync(cancellationToken);
            result.FileStatuses.AddRange(zipResult.FileStatuses);
            result.UpdatedCount += zipResult.UpdatedCount;
            result.DownloadedCount += zipResult.DownloadedCount;
            result.SkippedCount += zipResult.SkippedCount;
            result.FailedCount += zipResult.FailedCount;

            result.FileCount = result.FileStatuses.Count;
            result.Success = result.FailedCount == 0;
            result.Message = $"同步完成，成功: {result.FileCount - result.FailedCount}，失败: {result.FailedCount}";

            // 更新统计信息
            UpdateSyncStats(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "同步所有文件失败");
            result.Success = false;
            result.Message = $"同步失败: {ex.Message}";
        }
        finally
        {
            result.EndTime = DateTime.Now;
            lock (_syncLock)
            {
                _isSyncing = false;
            }

            // 触发同步完成事件
            OnSyncCompleted(result);
        }

        return result;
    }

    /// <summary>
    /// 同步插件文件
    /// </summary>
    public async Task<SyncResult> SyncPluginsAsync(CancellationToken cancellationToken = default)
    {
        return await SyncFilesAsync("plugin", cancellationToken);
    }

    /// <summary>
    /// 同步模型文件
    /// </summary>
    public async Task<SyncResult> SyncModelsAsync(CancellationToken cancellationToken = default)
    {
        return await SyncFilesAsync("model", cancellationToken);
    }

    /// <summary>
    /// 同步视频文件
    /// </summary>
    public async Task<SyncResult> SyncVideosAsync(CancellationToken cancellationToken = default)
    {
        return await SyncFilesAsync("video", cancellationToken);
    }

    /// <summary>
    /// 同步压缩包文件
    /// </summary>
    public async Task<SyncResult> SyncZipPackagesAsync(CancellationToken cancellationToken = default)
    {
        return await SyncFilesAsync("zip", cancellationToken);
    }

    /// <summary>
    /// 强制同步所有文件（忽略ETag检查）
    /// </summary>
    public async Task<SyncResult> ForceSyncAllAsync(CancellationToken cancellationToken = default)
    {
        var originalUseETag = _storageConfig?.UseETag ?? true;

        try
        {
            if (_storageConfig != null)
            {
                _storageConfig.UseETag = false;
            }

            return await SyncAllAsync(cancellationToken);
        }
        finally
        {
            if (_storageConfig != null)
            {
                _storageConfig.UseETag = originalUseETag;
            }
        }
    }

    /// <summary>
    /// 检查单个文件是否需要更新
    /// </summary>
    public async Task<FileSyncStatus> CheckFileSyncStatusAsync(string fileName, string fileType, CancellationToken cancellationToken = default)
    {
        var status = new FileSyncStatus
        {
            FileName = fileName,
            FileType = fileType,
            Status = SyncStatus.Pending
        };

        if (_storageConfig == null || _minioClient == null)
        {
            status.Status = SyncStatus.Failed;
            status.Message = "对象存储服务未初始化";
            return status;
        }

        try
        {
            if (!TryResolveFilePaths(fileType, fileName, out var remotePath, out var localPath))
            {
                status.Status = SyncStatus.Failed;
                status.Message = $"不支持的文件类型: {fileType}";
                return status;
            }

            status.RemotePath = remotePath;
            status.LocalPath = localPath;

            // 检查远程文件是否存在
            var statArgs = new StatObjectArgs()
                .WithBucket(_storageConfig.BucketName)
                .WithObject(remotePath);

            ObjectStat? remoteStat = null;
            try
            {
                remoteStat = await _minioClient.StatObjectAsync(statArgs, cancellationToken);
            }
            catch (ObjectNotFoundException)
            {
                // 远程文件不存在
                if (File.Exists(localPath))
                {
                    status.Status = SyncStatus.Skipped;
                    status.Message = "远程文件不存在，保留本地文件";
                }
                else
                {
                    status.Status = SyncStatus.Failed;
                    status.Message = "远程和本地文件都不存在";
                }
                return status;
            }

            if (remoteStat == null)
            {
                status.Status = SyncStatus.Failed;
                status.Message = "无法获取远程文件信息";
                return status;
            }

            status.FileSize = remoteStat.Size;
            status.LastModified = remoteStat.LastModified;
            status.ETag = remoteStat.ETag?.Trim('"') ?? string.Empty;

            // 检查本地文件是否存在
            if (!File.Exists(localPath))
            {
                status.Status = SyncStatus.Pending;
                status.Message = "本地文件不存在，需要下载";
                return status;
            }

            // 如果禁用ETag检查，总是更新
            if (!_storageConfig.UseETag)
            {
                status.Status = SyncStatus.Updated;
                status.Message = "ETag检查已禁用，需要更新";
                return status;
            }

            // 检查本地文件ETag
            var localETag = await CalculateFileETagAsync(localPath, cancellationToken);

            if (string.IsNullOrEmpty(status.ETag) || status.ETag != localETag)
            {
                status.Status = SyncStatus.Updated;
                status.Message = "文件已更新，需要下载";
            }
            else
            {
                status.Status = SyncStatus.Skipped;
                status.Message = "文件未变化，跳过";
            }

            return status;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "检查文件同步状态失败: {FileName}", fileName);
            status.Status = SyncStatus.Failed;
            status.Message = $"检查失败: {ex.Message}";
            return status;
        }
    }

    /// <summary>
    /// 获取同步统计信息
    /// </summary>
    public Task<S3StorageSyncStats> GetSyncStatsAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_syncStats);
    }

    /// <summary>
    /// 获取文件列表（同时扫描本地目录和远程目录，合并展示完整状态）
    /// </summary>
    public async Task<List<FileSyncStatus>> GetFileListAsync(string fileType, CancellationToken cancellationToken = default)
    {
        var result = new List<FileSyncStatus>();

        if (_storageConfig == null || _minioClient == null)
        {
            return result;
        }

        try
        {
            if (!TryResolveTypeSettings(fileType, out var remotePathPrefix, out var localDirectory, out var searchPattern))
                return result;

            // 1. 获取远程文件列表
            var remoteItems = await ListRemoteObjectsAsync(remotePathPrefix, cancellationToken);

            // 2. 获取本地文件列表（插件目录只扫描DLL文件）
            var localFileNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var localDir = localDirectory; // InitializeAsync中已转为绝对路径
            if (Directory.Exists(localDir))
            {
                foreach (var file in Directory.GetFiles(localDir, searchPattern, SearchOption.TopDirectoryOnly))
                {
                    var name = Path.GetFileName(file);
                    if (!string.IsNullOrEmpty(name) && IsEligibleForFileType(fileType, name))
                        localFileNames.Add(name);
                }
            }

            // 3. 合并：远程文件 + 本地独有文件
            var processedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // 处理远程存在的文件
            foreach (var item in remoteItems)
            {
                if (item.IsDir) continue;

                var fileName = Path.GetFileName(item.Key);
                if (string.IsNullOrEmpty(fileName)) continue;

                if (!IsEligibleForFileType(fileType, fileName))
                    continue;

                // 避免重复（远程可能有同名文件在不同子目录）
                if (processedNames.Contains(fileName)) continue;
                processedNames.Add(fileName);

                var localPath = Path.Combine(localDir, fileName);
                var status = new FileSyncStatus
                {
                    FileName = fileName,
                    FileType = fileType,
                    RemotePath = item.Key,
                    LocalPath = localPath,
                    FileSize = (long)item.Size,
                    LastModified = DateTime.TryParse(item.LastModified, out var lm) ? lm : DateTime.MinValue,
                    Status = SyncStatus.Pending,
                    Message = string.Empty
                };

                // 获取远程文件详细信息
                try
                {
                    var statArgs = new StatObjectArgs()
                        .WithBucket(_storageConfig.BucketName)
                        .WithObject(item.Key);
                    var remoteStat = await _minioClient.StatObjectAsync(statArgs, cancellationToken);
                    status.FileSize = remoteStat.Size;
                    status.LastModified = remoteStat.LastModified;
                    status.ETag = remoteStat.ETag?.Trim('"') ?? string.Empty;
                }
                catch { }

                // 判断本地状态
                if (File.Exists(localPath))
                {
                    if (_storageConfig.UseETag && !string.IsNullOrEmpty(status.ETag))
                    {
                        var localETag = await CalculateFileETagAsync(localPath, cancellationToken);
                        if (string.Equals(status.ETag, localETag, StringComparison.OrdinalIgnoreCase))
                        {
                            status.Status = SyncStatus.Skipped;
                            status.Message = "本地与远程一致";
                        }
                        else
                        {
                            status.Status = SyncStatus.Updated;
                            status.Message = "远程文件已更新，需要下载";
                        }
                    }
                    else
                    {
                        status.Status = SyncStatus.Skipped;
                        status.Message = "本地文件已存在";
                    }
                }
                else
                {
                    status.Status = SyncStatus.Pending;
                    status.Message = "本地文件不存在，需要下载";
                }

                result.Add(status);
            }

            // 处理本地独有的文件（远程不存在）
            foreach (var fileName in localFileNames)
            {
                if (processedNames.Contains(fileName)) continue;

                var localPath = Path.Combine(localDir, fileName);
                var fileInfo = new FileInfo(localPath);

                result.Add(new FileSyncStatus
                {
                    FileName = fileName,
                    FileType = fileType,
                    RemotePath = string.Empty,
                    LocalPath = localPath,
                    FileSize = fileInfo.Length,
                    LastModified = fileInfo.LastWriteTime,
                    Status = SyncStatus.Skipped,
                    Message = "仅本地存在，远程无此文件"
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取文件列表失败: {FileType}", fileType);
        }

        return result;
    }

    /// <summary>
    /// 列出远程对象
    /// </summary>
    private async Task<List<Item>> ListRemoteObjectsAsync(string prefix, CancellationToken cancellationToken)
    {
        var items = new List<Item>();

        if (_minioClient == null || _storageConfig == null) return items;

        var completionSource = new TaskCompletionSource<bool>();

        try
        {
            if (_minioClient is global::Minio.MinioClient minioClient)
            {
                var observable = minioClient.ListObjectsAsync(
                    new ListObjectsArgs()
                        .WithBucket(_storageConfig.BucketName)
                        .WithPrefix(prefix ?? "")
                        .WithRecursive(true),
                    cancellationToken);

                var subscription = observable.Subscribe(
                    item => items.Add(item),
                    ex => completionSource.TrySetException(ex),
                    () => completionSource.TrySetResult(true));

                await completionSource.Task.WaitAsync(cancellationToken);
                subscription.Dispose();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "列出远程对象失败");
            completionSource.TrySetResult(true);
            try { await completionSource.Task.WaitAsync(cancellationToken); } catch { }
        }

        return items;
    }

    /// <summary>
    /// 下载单个文件
    /// </summary>
    public async Task<FileSyncStatus> DownloadFileAsync(string remotePath, string localPath, CancellationToken cancellationToken = default)
    {
        var status = new FileSyncStatus
        {
            RemotePath = remotePath,
            LocalPath = localPath,
            FileName = Path.GetFileName(localPath),
            Status = SyncStatus.Syncing,
            Message = "开始下载文件"
        };

        if (_storageConfig == null || _minioClient == null)
        {
            status.Status = SyncStatus.Failed;
            status.Message = "对象存储服务未初始化";
            return status;
        }

        try
        {
            bool isPluginDll = localPath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) &&
                               localPath.Contains("plugin", StringComparison.OrdinalIgnoreCase);

            // 如果是插件DLL文件，先卸载插件上下文释放文件锁
            if (isPluginDll)
            {
                _logger.LogInformation("下载插件DLL，先卸载插件加载上下文释放文件锁: {LocalPath}", localPath);
                try
                {
                    // 当前仓库按插件目录反射加载；下载前不再显式卸载上下文
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "卸载插件上下文失败，尝试继续下载");
                }
            }

            // 确保本地目录存在
            var localDir = Path.GetDirectoryName(localPath);
            if (!string.IsNullOrEmpty(localDir) && !Directory.Exists(localDir))
            {
                Directory.CreateDirectory(localDir);
            }

            // 判断文件是否被锁定
            bool fileLocked = false;
            if (File.Exists(localPath))
            {
                try
                {
                    using var testStream = File.Open(localPath, FileMode.Open, FileAccess.Write, FileShare.None);
                }
                catch (IOException)
                {
                    fileLocked = true;
                }
            }

            // 始终使用临时文件下载，然后替换，确保原子性
            var tempPath = localPath + ".tmp";
            var backupPath = localPath + ".bak";

            // 清理可能残留的临时文件
            try { if (File.Exists(tempPath)) File.Delete(tempPath); } catch { }
            try { if (File.Exists(backupPath)) File.Delete(backupPath); } catch { }

            // 下载到临时文件
            var getArgs = new GetObjectArgs()
                .WithBucket(_storageConfig.BucketName)
                .WithObject(remotePath)
                .WithCallbackStream(async (stream, ct) =>
                {
                    using var fileStream = File.Create(tempPath);
                    await stream.CopyToAsync(fileStream, ct);
                    return;
                });

            await _minioClient.GetObjectAsync(getArgs, cancellationToken);

            // 替换原文件
            if (File.Exists(localPath))
            {
                if (fileLocked)
                {
                    // 文件被锁定：备份原文件 → 删除原文件 → 重命名临时文件
                    // 如果删除失败，尝试用备份恢复
                    try
                    {
                        File.Move(localPath, backupPath, overwrite: true);
                        _logger.LogInformation("已备份原文件: {BackupPath}", backupPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "备份原文件失败，尝试直接覆盖");
                    }
                }
                else
                {
                    try
                    {
                        File.Delete(localPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "删除原文件失败，尝试备份后替换");
                        try
                        {
                            File.Move(localPath, backupPath, overwrite: true);
                        }
                        catch { }
                    }
                }
            }

            try
            {
                File.Move(tempPath, localPath, overwrite: true);
                _logger.LogInformation("文件已替换: {LocalPath}", localPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "替换文件失败: {TempPath} -> {LocalPath}", tempPath, localPath);
                // 尝试恢复备份
                if (File.Exists(backupPath))
                {
                    try
                    {
                        File.Move(backupPath, localPath, overwrite: true);
                        _logger.LogInformation("已从备份恢复: {LocalPath}", localPath);
                    }
                    catch { }
                }
                // 清理临时文件
                try { if (File.Exists(tempPath)) File.Delete(tempPath); } catch { }
                throw;
            }

            // 清理备份文件
            try { if (File.Exists(backupPath)) File.Delete(backupPath); } catch { }

            // 获取文件信息
            var statArgs = new StatObjectArgs()
                .WithBucket(_storageConfig.BucketName)
                .WithObject(remotePath);

            var remoteStat = await _minioClient.StatObjectAsync(statArgs, cancellationToken);

            status.FileSize = remoteStat.Size;
            status.LastModified = remoteStat.LastModified;
            status.ETag = remoteStat.ETag?.Trim('"') ?? string.Empty;
            status.Status = SyncStatus.Downloaded;
            status.Message = "文件下载成功";
            status.SyncTime = DateTime.Now;

            // 更新本地ETag缓存
            _localFileETags[localPath] = status.ETag;

            // 如果是插件DLL，下载完成后重新加载插件
            if (isPluginDll)
            {
                _logger.LogInformation("插件DLL下载完成，重新加载插件: {LocalPath}", localPath);
                try
                {
                    status.Message = "文件下载成功";
                }
                catch (Exception reloadEx)
                {
                    _logger.LogWarning(reloadEx, "重新加载插件失败");
                    status.Message = $"文件下载成功，但插件重新加载失败: {reloadEx.Message}";
                }
            }

            // 触发文件同步完成事件
            OnFileSyncCompleted(status);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "下载文件失败: {RemotePath} -> {LocalPath}", remotePath, localPath);
            status.Status = SyncStatus.Failed;
            status.Message = $"下载失败: {ex.Message}";
        }

        return status;
    }

    /// <summary>
    /// 上传单个文件到MinIO
    /// </summary>
    public async Task<FileSyncStatus> UploadFileAsync(string localPath, string remotePath, CancellationToken cancellationToken = default)
    {
        var status = new FileSyncStatus
        {
            RemotePath = remotePath,
            LocalPath = localPath,
            FileName = Path.GetFileName(localPath),
            Status = SyncStatus.Syncing,
            Message = "开始上传文件"
        };

        if (_storageConfig == null || _minioClient == null)
        {
            status.Status = SyncStatus.Failed;
            status.Message = "对象存储服务未初始化";
            return status;
        }

        if (!File.Exists(localPath))
        {
            status.Status = SyncStatus.Failed;
            status.Message = "本地文件不存在";
            return status;
        }

        try
        {
            var fileInfo = new FileInfo(localPath);

            using var fileStream = File.OpenRead(localPath);

            var putArgs = new PutObjectArgs()
                .WithBucket(_storageConfig.BucketName)
                .WithObject(remotePath)
                .WithStreamData(fileStream)
                .WithObjectSize(fileInfo.Length)
                .WithContentType(GetContentType(localPath));

            await _minioClient.PutObjectAsync(putArgs, cancellationToken);

            status.FileSize = fileInfo.Length;
            status.LastModified = fileInfo.LastWriteTime;
            status.ETag = await CalculateFileETagAsync(localPath, cancellationToken);
            status.Status = SyncStatus.Updated;
            status.Message = "文件上传成功";
            status.SyncTime = DateTime.Now;

            // 更新本地ETag缓存
            _localFileETags[localPath] = status.ETag;

            // 触发文件同步完成事件
            OnFileSyncCompleted(status);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "上传文件失败: {LocalPath} -> {RemotePath}", localPath, remotePath);
            status.Status = SyncStatus.Failed;
            status.Message = $"上传失败: {ex.Message}";
        }

        return status;
    }

    /// <summary>
    /// 删除MinIO中的文件
    /// </summary>
    public async Task<bool> DeleteRemoteFileAsync(string remotePath, CancellationToken cancellationToken = default)
    {
        if (_storageConfig == null || _minioClient == null)
        {
            return false;
        }

        try
        {
            var removeArgs = new RemoveObjectArgs()
                .WithBucket(_storageConfig.BucketName)
                .WithObject(remotePath);

            await _minioClient.RemoveObjectAsync(removeArgs, cancellationToken);

            _logger.LogInformation("已删除MinIO文件: {RemotePath}", remotePath);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除MinIO文件失败: {RemotePath}", remotePath);
            return false;
        }
    }

    /// <summary>
    /// 开始自动同步（定时任务）
    /// </summary>
    public void StartAutoSync()
    {
        if (_storageConfig == null || _storageConfig.AutoSyncIntervalMinutes <= 0)
        {
            return;
        }

        StopAutoSync();

        var interval = TimeSpan.FromMinutes(_storageConfig.AutoSyncIntervalMinutes);
        _autoSyncTimer = new Timer(async _ =>
        {
            if (!_isSyncing)
            {
                _logger.LogInformation("开始定时同步");
                await SyncAllAsync();
            }
        }, null, interval, interval);

        _logger.LogInformation("已启动自动同步，间隔: {IntervalMinutes}分钟", _storageConfig.AutoSyncIntervalMinutes);
    }

    /// <summary>
    /// 停止自动同步
    /// </summary>
    public void StopAutoSync()
    {
        _autoSyncTimer?.Dispose();
        _autoSyncTimer = null;
        _logger.LogInformation("已停止自动同步");
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        StopAutoSync();
        GC.SuppressFinalize(this);
    }

    #region 私有方法

    private bool CanSync(bool requireNotSyncing = true)
    {
        return _minioClient != null
            && _storageConfig != null
            && _storageConfig.Enabled
            && (!requireNotSyncing || !_isSyncing);
    }

    private SyncResult CreateFailedResult(string message)
    {
        return new SyncResult
        {
            Success = false,
            Message = message,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now
        };
    }

    private async Task<SyncResult> SyncFilesAsync(string fileType, CancellationToken cancellationToken)
    {
        var result = new SyncResult
        {
            StartTime = DateTime.Now,
            Success = false,
            Message = $"开始同步{fileType}文件"
        };

        // SyncAllAsync 内部会调用本方法，此时 _isSyncing=true，不能再次被拦截
        if (!CanSync(requireNotSyncing: false))
        {
            result.Success = false;
            result.Message = "同步服务不可用";
            result.EndTime = DateTime.Now;
            return result;
        }

        // 同步插件文件前，通知应用停止检测
        if (fileType == "plugin")
        {
            _logger.LogInformation("插件同步开始，通知应用停止检测");
            OnPluginUpdating(new PluginUpdateEventArgs
            {
                ShouldStopDetection = true,
                Message = "插件即将更新，请停止检测"
            });
        }

        try
        {
            // 获取远程文件列表
            if (!TryResolveTypeSettings(fileType, out var remotePathPrefix, out var localDirectory, out _))
            {
                result.Success = false;
                result.Message = $"不支持的文件类型: {fileType}";
                return result;
            }

            var remoteItems = await ListRemoteObjectsAsync(remotePathPrefix, cancellationToken);
            var remoteFiles = remoteItems
                .Where(i => !i.IsDir)
                .Where(i => IsEligibleForFileType(fileType, Path.GetFileName(i.Key)))
                .Select(i => i.Key)
                .ToList();

            // 处理每个文件
            int current = 0;
            foreach (var remotePath in remoteFiles)
            {
                current++;

                // 触发进度事件
                OnSyncProgress(new SyncProgressEventArgs
                {
                    CurrentFile = remotePath,
                    FileType = fileType,
                    Current = current,
                    Total = remoteFiles.Count,
                    Message = $"正在处理: {Path.GetFileName(remotePath)}"
                });

                var fileName = Path.GetFileName(remotePath);
                if (string.IsNullOrEmpty(fileName))
                    continue;

                // 确定本地路径
                var localPath = Path.Combine(localDirectory, fileName);

                // 判断是否需要下载
                bool needDownload = false;
                string skipReason = string.Empty;

                if (!File.Exists(localPath))
                {
                    needDownload = true;
                }
                else if (!_storageConfig!.UseETag)
                {
                    needDownload = true;
                }
                else
                {
                    // 比较ETag
                    try
                    {
                        var statArgs = new StatObjectArgs()
                            .WithBucket(_storageConfig.BucketName)
                            .WithObject(remotePath);
                        #pragma warning disable CS8602
                        ObjectStat? remoteStat = await _minioClient.StatObjectAsync(statArgs, cancellationToken);
                        #pragma warning restore CS8602
                        if (remoteStat == null)
                        {
                            needDownload = true;
                            continue;
                        }
                        var remoteETag = remoteStat!.ETag?.Trim('"') ?? string.Empty;
                        var localETag = await CalculateFileETagAsync(localPath, cancellationToken);

                        if (!string.IsNullOrEmpty(remoteETag) && !string.Equals(remoteETag, localETag, StringComparison.OrdinalIgnoreCase))
                        {
                            needDownload = true;
                        }
                        else
                        {
                            skipReason = "本地与远程一致";
                        }
                    }
                    catch
                    {
                        needDownload = true;
                    }
                }

                var fileStatus = new FileSyncStatus
                {
                    FileName = fileName,
                    FileType = fileType,
                    RemotePath = remotePath,
                    LocalPath = localPath
                };

                if (needDownload)
                {
                    var downloadResult = await DownloadFileAsync(remotePath, localPath, cancellationToken);
                    fileStatus = downloadResult;

                    if (downloadResult.Status == SyncStatus.Downloaded)
                    {
                        result.DownloadedCount++;
                    }
                    else if (downloadResult.Status == SyncStatus.Updated)
                    {
                        result.UpdatedCount++;
                    }
                    else
                    {
                        result.FailedCount++;
                    }
                }
                else
                {
                    fileStatus.Status = SyncStatus.Skipped;
                    fileStatus.Message = skipReason;
                    result.SkippedCount++;
                }

                result.FileStatuses.Add(fileStatus);
            }

            result.FileCount = result.FileStatuses.Count;
            result.Success = result.FailedCount == 0;
            result.Message = $"{fileType}同步完成，总计: {result.FileCount}，成功: {result.FileCount - result.FailedCount}，失败: {result.FailedCount}";

            // 插件同步完成后，通知应用重新加载插件
            if (fileType == "plugin" && (result.DownloadedCount > 0 || result.UpdatedCount > 0))
            {
                var updatedFiles = result.FileStatuses
                    .Where(f => f.Status == SyncStatus.Downloaded || f.Status == SyncStatus.Updated)
                    .Select(f => f.FileName)
                    .ToList();

                _logger.LogInformation("插件同步完成，有 {Count} 个文件更新，通知应用重新加载插件", updatedFiles.Count);
                OnPluginUpdated(new PluginUpdateEventArgs
                {
                    ShouldStopDetection = false,
                    NeedReloadPlugins = true,
                    UpdatedFiles = updatedFiles,
                    Message = $"插件更新完成，{updatedFiles.Count} 个文件已更新，请重新加载插件"
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "同步{FileType}文件失败", fileType);
            result.Success = false;
            result.Message = $"{fileType}同步失败: {ex.Message}";
        }
        finally
        {
            result.EndTime = DateTime.Now;
        }

        return result;
    }

    private async Task<string> CalculateFileETagAsync(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            // 使用MD5计算文件哈希作为ETag
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filePath);

            var hash = await md5.ComputeHashAsync(stream, cancellationToken);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "计算文件ETag失败: {FilePath}", filePath);
            return string.Empty;
        }
    }

    private string GetContentType(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();

        return extension switch
        {
            ".dll" => "application/octet-stream",
            ".onnx" => "application/octet-stream",
            ".pt" => "application/octet-stream",
            ".pth" => "application/octet-stream",
            ".zip" => "application/zip",
            ".json" => "application/json",
            ".xml" => "application/xml",
            ".txt" => "text/plain",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".mp4" => "video/mp4",
            ".avi" => "video/x-msvideo",
            ".mov" => "video/quicktime",
            _ => "application/octet-stream"
        };
    }

    private void UpdateSyncStats(SyncResult result)
    {
        _syncStats.TotalFiles = result.FileCount;
        _syncStats.PluginFiles = result.FileStatuses.Count(s => s.FileType == "plugin");
        _syncStats.ModelFiles = result.FileStatuses.Count(s => s.FileType == "model");
        _syncStats.VideoFiles = result.FileStatuses.Count(s => s.FileType == "video");
        _syncStats.ZipFiles = result.FileStatuses.Count(s => s.FileType == "zip");
        _syncStats.TotalSize = result.FileStatuses.Sum(s => s.FileSize);
        _syncStats.LastSyncTime = DateTime.Now;
        _syncStats.LastSyncResult = result;
        _syncStats.SyncHistory.Add(result);

        // 保持历史记录数量
        if (_syncStats.SyncHistory.Count > 100)
        {
            _syncStats.SyncHistory.RemoveAt(0);
        }
    }

    private void OnSyncProgress(SyncProgressEventArgs e)
    {
        SyncProgress?.Invoke(this, e);
    }

    private bool TryResolveTypeSettings(string fileType, out string remotePathPrefix, out string localDirectory, out string searchPattern)
    {
        remotePathPrefix = string.Empty;
        localDirectory = string.Empty;
        searchPattern = "*";
        if (_storageConfig == null) return false;

        switch (fileType)
        {
            case "plugin":
                remotePathPrefix = _storageConfig.RemotePluginPath;
                localDirectory = _storageConfig.PluginDirectory;
                searchPattern = "*";
                return true;
            case "model":
                remotePathPrefix = _storageConfig.RemoteModelPath;
                localDirectory = _storageConfig.ModelDirectory;
                searchPattern = "*";
                return true;
            case "video":
                remotePathPrefix = _storageConfig.RemoteVideoPath;
                localDirectory = _storageConfig.VideoDirectory;
                searchPattern = "*";
                return true;
            case "zip":
                remotePathPrefix = _storageConfig.RemoteZipPath;
                localDirectory = _storageConfig.ZipDirectory;
                searchPattern = "*";
                return true;
            default:
                return false;
        }
    }

    private bool TryResolveFilePaths(string fileType, string fileName, out string remotePath, out string localPath)
    {
        remotePath = string.Empty;
        localPath = string.Empty;
        if (!TryResolveTypeSettings(fileType, out var remotePathPrefix, out var localDirectory, out _))
            return false;
        remotePath = $"{remotePathPrefix}{fileName}";
        localPath = Path.Combine(localDirectory, fileName);
        return true;
    }

    private static bool IsEligibleForFileType(string fileType, string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) return false;
        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        return fileType switch
        {
            "plugin" => extension is ".dll",
            "model" => extension is ".onnx",
            "zip" => IsArchiveFile(fileName),
            "video" => IsVideoFile(fileName),
            _ => true
        };
    }

    private static bool IsVideoFile(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext is ".mp4" or ".avi" or ".mov" or ".mkv" or ".webm" or ".wmv" or ".flv" or ".m4v" or ".mpeg" or ".mpg";
    }

    private static bool IsArchiveFile(string fileName)
    {
        var lowerName = fileName.ToLowerInvariant();
        var ext = Path.GetExtension(fileName).ToLowerInvariant();

        if (lowerName.EndsWith(".tar.gz", StringComparison.Ordinal) || lowerName.EndsWith(".tar.bz2", StringComparison.Ordinal))
            return true;

        return ext is ".zip" or ".7z" or ".rar" or ".tar" or ".gz" or ".bz2" or ".xz" or ".tgz";
    }

    private void OnFileSyncCompleted(FileSyncStatus status)
    {
        FileSyncCompleted?.Invoke(this, new FileSyncEventArgs { FileStatus = status });
    }

    private void OnSyncCompleted(SyncResult result)
    {
        SyncCompleted?.Invoke(this, new SyncCompletedEventArgs { SyncResult = result });
    }

    private void OnPluginUpdating(PluginUpdateEventArgs e)
    {
        PluginUpdating?.Invoke(this, e);
    }

    private void OnPluginUpdated(PluginUpdateEventArgs e)
    {
        PluginUpdated?.Invoke(this, e);
    }

    #endregion
}
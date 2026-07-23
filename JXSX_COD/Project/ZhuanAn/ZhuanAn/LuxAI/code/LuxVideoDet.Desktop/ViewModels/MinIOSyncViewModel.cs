using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.S3Storage;
using LuxVideoDet.Localization;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Desktop.ViewModels;

/// <summary>
/// 对象存储（S3 兼容）同步视图模型
/// </summary>
public partial class MinIOSyncViewModel : ViewModelBase
{
    private readonly IS3StorageSyncService _objectStorageSyncService;
    private readonly ConfigurationService _configService;
    private readonly ILogger<MinIOSyncViewModel> _logger;

    /// <summary>MinIO独立配置文件路径</summary>
    private static readonly string MinioConfigFilePath = Path.Combine(AppContext.BaseDirectory, "config_minio.json");

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    // 配置相关
    [ObservableProperty]
    private S3StorageConfiguration _minioConfig = new S3StorageConfiguration();

    [ObservableProperty]
    private bool _isConfigValid;

    [ObservableProperty]
    private string _configValidationMessage = string.Empty;

    [ObservableProperty]
    private ObservableCollection<S3StorageProviderOption> _availableProviders = new();

    [ObservableProperty]
    private S3StorageProviderOption? _selectedProvider;

    // 同步状态
    [ObservableProperty]
    private bool _isSyncing;

    [ObservableProperty]
    private bool _isConnected;

    [ObservableProperty]
    private string _connectionStatus = "未连接";

    [ObservableProperty]
    private string _syncStatusText = "就绪";

    // 进度信息
    [ObservableProperty]
    private int _currentProgress;

    [ObservableProperty]
    private int _totalProgress;

    [ObservableProperty]
    private double _progressPercentage;

    [ObservableProperty]
    private string _currentFile = string.Empty;

    [ObservableProperty]
    private string _progressMessage = string.Empty;

    // 文件列表
    [ObservableProperty]
    private ObservableCollection<FileSyncStatus> _pluginFiles = new ObservableCollection<FileSyncStatus>();

    [ObservableProperty]
    private ObservableCollection<FileSyncStatus> _modelFiles = new ObservableCollection<FileSyncStatus>();

    [ObservableProperty]
    private ObservableCollection<FileSyncStatus> _videoFiles = new ObservableCollection<FileSyncStatus>();

    [ObservableProperty]
    private ObservableCollection<FileSyncStatus> _zipFiles = new ObservableCollection<FileSyncStatus>();

    [ObservableProperty]
    private FileSyncStatus? _selectedPluginFile;

    [ObservableProperty]
    private FileSyncStatus? _selectedModelFile;

    [ObservableProperty]
    private FileSyncStatus? _selectedVideoFile;

    [ObservableProperty]
    private FileSyncStatus? _selectedZipFile;

    // 统计信息
    [ObservableProperty]
    private S3StorageSyncStats _syncStats = new S3StorageSyncStats();

    [ObservableProperty]
    private int _pendingFileCount;

    [ObservableProperty]
    private int _existingFileCount;

    [ObservableProperty]
    private int _failedFileCount;

    [ObservableProperty]
    private string _fileTypeSummary = "0 / 0 / 0 / 0";

    // 日志信息
    [ObservableProperty]
    private ObservableCollection<UiLogEntry> _syncLogs = new ObservableCollection<UiLogEntry>();

    [ObservableProperty]
    private ObservableCollection<UiLogEntry> _filteredSyncLogs = new ObservableCollection<UiLogEntry>();

    [ObservableProperty]
    private bool _onlyImportantLogs;

    public MinIOSyncViewModel(
        IS3StorageSyncService objectStorageSyncService,
        ConfigurationService configService,
        ILogger<MinIOSyncViewModel> logger)
    {
        _objectStorageSyncService = objectStorageSyncService;
        _configService = configService;
        _logger = logger;

        // 订阅事件
        _objectStorageSyncService.SyncProgress += OnSyncProgress;
        _objectStorageSyncService.FileSyncCompleted += OnFileSyncCompleted;
        _objectStorageSyncService.SyncCompleted += OnSyncCompleted;
        _objectStorageSyncService.PluginUpdating += OnPluginUpdating;
        _objectStorageSyncService.PluginUpdated += OnPluginUpdated;

        LoadAvailableProviders();
        ApplyLogFilter();

        // 加载配置（构造函数中不能await，使用丢弃符号明确表示不等待）
        _ = LoadConfiguration();
    }

    private void LoadAvailableProviders()
    {
        var providers = S3StorageProviderRegistry.DiscoverAvailableProviders();
        AvailableProviders = new ObservableCollection<S3StorageProviderOption>(providers);
    }

    /// <summary>
    /// 从config_minio.json文件加载MinIO配置
    /// </summary>
    private static S3StorageConfiguration? LoadMinioConfigFromFile()
    {
        try
        {
            if (!File.Exists(MinioConfigFilePath))
                return null;

            var json = File.ReadAllText(MinioConfigFilePath);
            return JsonSerializer.Deserialize<S3StorageConfiguration>(json, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 保存MinIO配置到config_minio.json文件
    /// </summary>
    private static void SaveMinioConfigToFile(S3StorageConfiguration config)
    {
        var json = JsonSerializer.Serialize(config, _jsonOptions);
        File.WriteAllText(MinioConfigFilePath, json);
    }

    /// <summary>
    /// 加载配置
    /// </summary>
    [RelayCommand]
    private async Task LoadConfiguration()
    {
        try
        {
            // 优先从独立配置文件加载
            var fileConfig = LoadMinioConfigFromFile();
            if (fileConfig != null)
            {
                MinioConfig = fileConfig;
                AddLog($"已从 {Path.GetFileName(MinioConfigFilePath)} 加载配置");
            }
            else
            {
                // 回退到主配置文件
                var appConfig = await _configService.GetAppConfigurationAsync();
                MinioConfig = appConfig.S3Storage;
                AddLog("已从主配置文件加载 S3 存储配置");
            }

            MinioConfig.Provider = S3StorageProviders.NormalizeProvider(MinioConfig.Provider);
            MinioConfig.Enabled = true;
            SyncSelectedProviderFromConfig();

            ValidateConfiguration();

            if (MinioConfig.Enabled)
            {
                await CheckConnection();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载 S3 存储配置失败");
            AddLog($"加载配置失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 保存配置到config_minio.json文件
    /// </summary>
    [RelayCommand]
    private async Task SaveConfiguration()
    {
        try
        {
            // 保存到独立配置文件
            MinioConfig.Enabled = true;
            SaveMinioConfigToFile(MinioConfig);
            AddLog($"配置已保存到 {Path.GetFileName(MinioConfigFilePath)}");

            // 同步更新主配置文件中的 S3 存储配置
            try
            {
                var appConfig = await _configService.GetAppConfigurationAsync();
                appConfig.S3Storage = MinioConfig;
                await _configService.SetAppConfigurationAsync(appConfig);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "同步更新主配置文件失败，不影响独立配置文件");
            }

            // 重新初始化服务并检查连接
            try
            {
                AddLog("正在重新初始化 S3 存储客户端...");
                var initResult = await _objectStorageSyncService.InitializeAsync();
                if (!initResult)
                {
                    AddLog("S3 存储客户端初始化失败，尝试检查连接...");
                }
                else
                {
                    AddLog("S3 存储客户端初始化成功");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "S3 存储客户端初始化异常");
                AddLog($"S3 存储客户端初始化异常: {ex.Message}");
            }

            await CheckConnection();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存 S3 存储配置失败");
            AddLog($"保存配置失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 检查连接
    /// </summary>
    [RelayCommand]
    private async Task CheckConnection()
    {
        try
        {
            ConnectionStatus = "未连接";
            IsConnected = false;

            // 先尝试初始化 S3 存储客户端（如果尚未初始化）
            var result = await _objectStorageSyncService.CheckConnectionAsync();

            // 如果客户端未初始化，先初始化再检查
            if (!result.Success && result.Message.Contains("未初始化"))
            {
                AddLog("S3 存储客户端未初始化，正在重新初始化...");
                try
                {
                    var initResult = await _objectStorageSyncService.InitializeAsync();
                    if (initResult)
                    {
                        AddLog("S3 存储客户端初始化成功，重新检查连接...");
                        result = await _objectStorageSyncService.CheckConnectionAsync();
                    }
                    else
                    {
                        AddLog("S3 存储客户端初始化失败");
                    }
                }
                catch (Exception initEx)
                {
                    AddLog($"S3 存储客户端初始化异常: {initEx.Message}");
                }
            }

            if (result.Success)
            {
                ConnectionStatus = "已连接";
                IsConnected = true;
                AddLog($"成功连接到 S3 对象存储: {MinioConfig.Endpoint}");
                AddLog(result.Message);

                // 获取文件列表
                await RefreshFileLists();

                // 获取统计信息
                await LoadStats();
            }
            else
            {
                ConnectionStatus = "未连接";
                IsConnected = false;
                AddLog($"连接失败: {result.Message}");

                // 输出详细错误信息到日志
                if (result.ErrorDetail != null)
                {
                    AddLog($"错误详情:\n{result.ErrorDetail}");
                }

                // 同时输出到控制台
                Console.WriteLine($"[S3Storage] 连接失败: {result.Message}");
                if (result.ErrorDetail != null)
                {
                    Console.WriteLine($"[S3Storage] 错误详情:\n{result.ErrorDetail}");
                }
                if (result.Exception != null)
                {
                    Console.WriteLine($"[S3Storage] 异常类型: {result.Exception.GetType().FullName}");
                    Console.WriteLine($"[S3Storage] 异常消息: {result.Exception.Message}");
                    Console.WriteLine($"[S3Storage] 堆栈跟踪:\n{result.Exception.StackTrace}");
                }
            }
        }
        catch (Exception ex)
        {
            ConnectionStatus = "未连接";
            IsConnected = false;
            _logger.LogError(ex, "检查 S3 连接失败");
            AddLog($"连接检查失败: {ex.Message}");

            // 输出到控制台
            Console.WriteLine($"[S3Storage] 连接检查异常: {ex.GetType().FullName}");
            Console.WriteLine($"[S3Storage] 异常消息: {ex.Message}");
            Console.WriteLine($"[S3Storage] 堆栈跟踪:\n{ex.StackTrace}");
        }
    }

    /// <summary>
    /// 同步所有文件
    /// </summary>
    [RelayCommand]
    private async Task SyncAll()
    {
        if (IsSyncing)
        {
            AddLog("同步正在进行中，请等待");
            return;
        }

        try
        {
            IsSyncing = true;
            SyncStatusText = "开始拉取并覆盖本地...";
            AddLog("开始一键同步：拉取远程文件并覆盖本地同名文件");

            var result = await _objectStorageSyncService.ForceSyncAllAsync();

            SyncStatusText = result.Success ? "覆盖同步完成" : "覆盖同步失败";
            AddLog(result.Message);

            // 刷新文件列表
            await RefreshFileLists();

            // 更新统计信息
            await LoadStats();
        }
        catch (Exception ex)
        {
            SyncStatusText = "覆盖同步错误";
            _logger.LogError(ex, "同步所有文件失败");
            AddLog($"同步失败: {ex.Message}");
        }
        finally
        {
            ResetProgressDisplay();
            IsSyncing = false;
        }
    }

    /// <summary>
    /// 强制同步所有文件
    /// </summary>
    [RelayCommand]
    private async Task ForceSyncAll()
    {
        if (IsSyncing)
        {
            AddLog("同步正在进行中，请等待");
            return;
        }

        try
        {
            IsSyncing = true;
            SyncStatusText = "开始强制同步...";
            AddLog("开始强制同步所有文件（忽略ETag检查）");

            var result = await _objectStorageSyncService.ForceSyncAllAsync();

            SyncStatusText = result.Success ? "强制同步完成" : "强制同步失败";
            AddLog(result.Message);

            // 刷新文件列表
            await RefreshFileLists();

            // 更新统计信息
            await LoadStats();
        }
        catch (Exception ex)
        {
            SyncStatusText = "强制同步错误";
            _logger.LogError(ex, "强制同步所有文件失败");
            AddLog($"强制同步失败: {ex.Message}");
        }
        finally
        {
            ResetProgressDisplay();
            IsSyncing = false;
        }
    }

    /// <summary>
    /// 同步插件文件
    /// </summary>
    [RelayCommand]
    private async Task SyncPlugins()
    {
        if (IsSyncing)
        {
            AddLog("同步正在进行中，请等待");
            return;
        }

        try
        {
            IsSyncing = true;
            SyncStatusText = "开始同步插件...";
            AddLog("开始同步插件文件");

            var result = await _objectStorageSyncService.SyncPluginsAsync();

            SyncStatusText = result.Success ? "插件同步完成" : "插件同步失败";
            AddLog(result.Message);

            // 刷新插件文件列表
            await RefreshPluginFiles();
        }
        catch (Exception ex)
        {
            SyncStatusText = "插件同步错误";
            _logger.LogError(ex, "同步插件文件失败");
            AddLog($"插件同步失败: {ex.Message}");
        }
        finally
        {
            ResetProgressDisplay();
            IsSyncing = false;
        }
    }

    /// <summary>
    /// 同步模型文件
    /// </summary>
    [RelayCommand]
    private async Task SyncModels()
    {
        if (IsSyncing)
        {
            AddLog("同步正在进行中，请等待");
            return;
        }

        try
        {
            IsSyncing = true;
            SyncStatusText = "开始同步模型...";
            AddLog("开始同步模型文件");

            var result = await _objectStorageSyncService.SyncModelsAsync();

            SyncStatusText = result.Success ? "模型同步完成" : "模型同步失败";
            AddLog(result.Message);

            // 刷新模型文件列表
            await RefreshModelFiles();
        }
        catch (Exception ex)
        {
            SyncStatusText = "模型同步错误";
            _logger.LogError(ex, "同步模型文件失败");
            AddLog($"模型同步失败: {ex.Message}");
        }
        finally
        {
            ResetProgressDisplay();
            IsSyncing = false;
        }
    }

    /// <summary>
    /// 同步视频文件
    /// </summary>
    [RelayCommand]
    private async Task SyncVideos()
    {
        if (IsSyncing)
        {
            AddLog("同步正在进行中，请等待");
            return;
        }

        try
        {
            IsSyncing = true;
            SyncStatusText = "开始同步视频...";
            AddLog("开始同步视频文件");

            var result = await _objectStorageSyncService.SyncVideosAsync();

            SyncStatusText = result.Success ? "视频同步完成" : "视频同步失败";
            AddLog(result.Message);
            await RefreshVideoFiles();
        }
        catch (Exception ex)
        {
            SyncStatusText = "视频同步错误";
            _logger.LogError(ex, "同步视频文件失败");
            AddLog($"视频同步失败: {ex.Message}");
        }
        finally
        {
            ResetProgressDisplay();
            IsSyncing = false;
        }
    }

    /// <summary>
    /// 同步压缩包文件
    /// </summary>
    [RelayCommand]
    private async Task SyncZips()
    {
        if (IsSyncing)
        {
            AddLog("同步正在进行中，请等待");
            return;
        }

        try
        {
            IsSyncing = true;
            SyncStatusText = "开始同步压缩包...";
            AddLog("开始同步压缩包文件");

            var result = await _objectStorageSyncService.SyncZipPackagesAsync();

            SyncStatusText = result.Success ? "压缩包同步完成" : "压缩包同步失败";
            AddLog(result.Message);
            await RefreshZipFiles();
        }
        catch (Exception ex)
        {
            SyncStatusText = "压缩包同步错误";
            _logger.LogError(ex, "同步压缩包文件失败");
            AddLog($"压缩包同步失败: {ex.Message}");
        }
        finally
        {
            ResetProgressDisplay();
            IsSyncing = false;
        }
    }

    /// <summary>
    /// 刷新文件列表
    /// </summary>
    [RelayCommand]
    private async Task RefreshFileLists()
    {
        await RefreshPluginFiles();
        await RefreshModelFiles();
        await RefreshVideoFiles();
        await RefreshZipFiles();
    }

    /// <summary>
    /// 刷新插件文件列表
    /// </summary>
    [RelayCommand]
    private async Task RefreshPluginFiles()
    {
        try
        {
            var files = await _objectStorageSyncService.GetFileListAsync("plugin");

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                PluginFiles.Clear();
                foreach (var file in files.OrderBy(f => f.FileName))
                {
                    PluginFiles.Add(file);
                }
                RecalculateLocalStats();
            });

            AddLog($"已加载 {files.Count} 个插件文件");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "刷新插件文件列表失败");
            AddLog($"刷新插件文件列表失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 刷新模型文件列表
    /// </summary>
    [RelayCommand]
    private async Task RefreshModelFiles()
    {
        try
        {
            var files = await _objectStorageSyncService.GetFileListAsync("model");

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                ModelFiles.Clear();
                foreach (var file in files.OrderBy(f => f.FileName))
                {
                    ModelFiles.Add(file);
                }
                RecalculateLocalStats();
            });

            AddLog($"已加载 {files.Count} 个模型文件");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "刷新模型文件列表失败");
            AddLog($"刷新模型文件列表失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 刷新视频文件列表
    /// </summary>
    [RelayCommand]
    private async Task RefreshVideoFiles()
    {
        try
        {
            var files = await _objectStorageSyncService.GetFileListAsync("video");
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                VideoFiles.Clear();
                foreach (var file in files.OrderBy(f => f.FileName))
                    VideoFiles.Add(file);
                RecalculateLocalStats();
            });
            AddLog($"已加载 {files.Count} 个视频文件");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "刷新视频文件列表失败");
            AddLog($"刷新视频文件列表失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 刷新压缩包文件列表
    /// </summary>
    [RelayCommand]
    private async Task RefreshZipFiles()
    {
        try
        {
            var files = await _objectStorageSyncService.GetFileListAsync("zip");
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                ZipFiles.Clear();
                foreach (var file in files.OrderBy(f => f.FileName))
                    ZipFiles.Add(file);
                RecalculateLocalStats();
            });
            AddLog($"已加载 {files.Count} 个压缩包文件");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "刷新压缩包文件列表失败");
            AddLog($"刷新压缩包文件列表失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 下载选中的插件文件
    /// </summary>
    [RelayCommand]
    private async Task DownloadSelectedPlugin()
    {
        if (SelectedPluginFile == null)
        {
            AddLog("请先选择一个插件文件");
            return;
        }

        try
        {
            AddLog($"开始下载插件文件: {SelectedPluginFile.FileName}");

            var result = await _objectStorageSyncService.DownloadFileAsync(
                SelectedPluginFile.RemotePath,
                SelectedPluginFile.LocalPath);

            if (result.Status == SyncStatus.Downloaded || result.Status == SyncStatus.Updated)
            {
                AddLog($"插件文件下载成功: {SelectedPluginFile.FileName}");

                // 刷新文件状态
                await RefreshPluginFiles();
            }
            else
            {
                AddLog($"插件文件下载失败: {SelectedPluginFile.FileName} - {result.Message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "下载插件文件失败");
            AddLog($"下载插件文件失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 下载选中的模型文件
    /// </summary>
    [RelayCommand]
    private async Task DownloadSelectedModel()
    {
        if (SelectedModelFile == null)
        {
            AddLog("请先选择一个模型文件");
            return;
        }

        try
        {
            AddLog($"开始下载模型文件: {SelectedModelFile.FileName}");

            var result = await _objectStorageSyncService.DownloadFileAsync(
                SelectedModelFile.RemotePath,
                SelectedModelFile.LocalPath);

            if (result.Status == SyncStatus.Downloaded || result.Status == SyncStatus.Updated)
            {
                AddLog($"模型文件下载成功: {SelectedModelFile.FileName}");

                // 刷新文件状态
                await RefreshModelFiles();
            }
            else
            {
                AddLog($"模型文件下载失败: {SelectedModelFile.FileName} - {result.Message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "下载模型文件失败");
            AddLog($"下载模型文件失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 下载选中的视频文件
    /// </summary>
    [RelayCommand]
    private async Task DownloadSelectedVideo()
    {
        if (SelectedVideoFile == null)
        {
            AddLog("请先选择一个视频文件");
            return;
        }

        try
        {
            AddLog($"开始下载视频文件: {SelectedVideoFile.FileName}");
            var result = await _objectStorageSyncService.DownloadFileAsync(
                SelectedVideoFile.RemotePath,
                SelectedVideoFile.LocalPath);

            if (result.Status == SyncStatus.Downloaded || result.Status == SyncStatus.Updated)
            {
                AddLog($"视频文件下载成功: {SelectedVideoFile.FileName}");
                await RefreshVideoFiles();
            }
            else
            {
                AddLog($"视频文件下载失败: {SelectedVideoFile.FileName} - {result.Message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "下载视频文件失败");
            AddLog($"下载视频文件失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 下载选中的压缩包文件
    /// </summary>
    [RelayCommand]
    private async Task DownloadSelectedZip()
    {
        if (SelectedZipFile == null)
        {
            AddLog("请先选择一个压缩包文件");
            return;
        }

        try
        {
            AddLog($"开始下载压缩包文件: {SelectedZipFile.FileName}");
            var result = await _objectStorageSyncService.DownloadFileAsync(
                SelectedZipFile.RemotePath,
                SelectedZipFile.LocalPath);

            if (result.Status == SyncStatus.Downloaded || result.Status == SyncStatus.Updated)
            {
                AddLog($"压缩包下载成功: {SelectedZipFile.FileName}");
                await RefreshZipFiles();
            }
            else
            {
                AddLog($"压缩包下载失败: {SelectedZipFile.FileName} - {result.Message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "下载压缩包文件失败");
            AddLog($"下载压缩包文件失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 上传本地插件文件
    /// </summary>
    [RelayCommand]
    private Task UploadPluginFile()
    {
        AddLog("上传插件文件功能待实现");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 上传本地模型文件
    /// </summary>
    [RelayCommand]
    private Task UploadModelFile()
    {
        AddLog("上传模型文件功能待实现");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 加载统计信息
    /// </summary>
    [RelayCommand]
    private async Task LoadStats()
    {
        try
        {
            // 先刷新列表，统计基于当前远程+本地真实状态
            await RefreshFileLists();

            var stats = await _objectStorageSyncService.GetSyncStatsAsync();
            var all = PluginFiles.Concat(ModelFiles).Concat(VideoFiles).Concat(ZipFiles).ToList();

            stats.TotalFiles = all.Count;
            stats.PluginFiles = PluginFiles.Count;
            stats.ModelFiles = ModelFiles.Count;
            stats.VideoFiles = VideoFiles.Count;
            stats.ZipFiles = ZipFiles.Count;
            stats.TotalSize = all.Sum(f => f.FileSize);

            // 当服务尚未产生同步历史时，避免显示全 0/默认时间
            if (stats.LastSyncTime == default)
            {
                stats.LastSyncTime = DateTime.Now;
            }

            SyncStats = stats;
            RecalculateLocalStats();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载统计信息失败");
            AddLog($"加载统计信息失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 清空日志
    /// </summary>
    [RelayCommand]
    private void ClearLogs()
    {
        SyncLogs.Clear();
        ApplyLogFilter();
        AddLog("日志已清空");
    }

    /// <summary>
    /// 验证配置
    /// </summary>
    private void ValidateConfiguration()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(MinioConfig.Endpoint))
        {
            errors.Add("S3 服务地址不能为空");
        }

        if (string.IsNullOrWhiteSpace(MinioConfig.AccessKey))
        {
            errors.Add("账号不能为空");
        }

        if (string.IsNullOrWhiteSpace(MinioConfig.SecretKey))
        {
            errors.Add("密码不能为空");
        }

        if (string.IsNullOrWhiteSpace(MinioConfig.BucketName))
        {
            errors.Add("存储桶名称不能为空");
        }

        if (MinioConfig.AutoSyncIntervalMinutes < 0)
        {
            errors.Add("自动同步间隔不能为负数");
        }

        if (MinioConfig.DownloadTimeoutSeconds <= 0)
        {
            errors.Add("下载超时时间必须大于0");
        }

        if (MinioConfig.MaxRetryCount < 0)
        {
            errors.Add("最大重试次数不能为负数");
        }

        if (MinioConfig.RetryDelaySeconds < 0)
        {
            errors.Add("重试延迟时间不能为负数");
        }

        IsConfigValid = errors.Count == 0;
        ConfigValidationMessage = IsConfigValid
            ? "配置有效"
            : $"配置有 {errors.Count} 个错误:\n" + string.Join("\n", errors);
    }

    /// <summary>
    /// 添加日志
    /// </summary>
    private void AddLog(string message)
    {
        var level = InferLogLevel(message);
        var entry = new UiLogEntry
        {
            Time = DateTime.Now,
            Level = level,
            Category = InferLogCategory(message),
            Message = message
        };

        Dispatcher.UIThread.Post(() =>
        {
            SyncLogs.Add(entry);

            // 限制日志数量
            if (SyncLogs.Count > 1000)
            {
                SyncLogs.RemoveAt(0);
            }

            ApplyLogFilter();
        });
    }

    private static UiLogLevel InferLogLevel(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return UiLogLevel.Info;

        var text = message.ToLowerInvariant();
        var normalized = message.Replace(" ", string.Empty);

        // 优先识别“完成且失败为0”的成功场景，避免被“失败”关键词误判
        if ((text.Contains("同步完成") || text.Contains("完成"))
            && (normalized.Contains("失败:0") || normalized.Contains("失败：0")))
            return UiLogLevel.Success;

        if (text.Contains("失败") || text.Contains("错误") || text.Contains("异常"))
            return UiLogLevel.Error;
        if (text.Contains("警告") || text.Contains("忽略") || text.Contains("跳过"))
            return UiLogLevel.Warning;
        if (text.Contains("成功") || text.Contains("完成"))
            return UiLogLevel.Success;
        return UiLogLevel.Info;
    }

    private static string InferLogCategory(string message)
    {
        if (message.Contains("连接")) return "连接";
        if (message.Contains("配置")) return "配置";
        if (message.Contains("插件")) return "插件";
        if (message.Contains("模型")) return "模型";
        if (message.Contains("视频")) return "视频";
        if (message.Contains("压缩包")) return "压缩包";
        if (message.Contains("同步")) return "同步";
        if (message.Contains("下载")) return "下载";
        return "系统";
    }

    #region 事件处理

    private void OnSyncProgress(object? sender, SyncProgressEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            CurrentProgress = e.Current;
            TotalProgress = e.Total;
            ProgressPercentage = e.Percentage;
            CurrentFile = e.CurrentFile;
            ProgressMessage = e.Message;
        });
    }

    private void OnFileSyncCompleted(object? sender, FileSyncEventArgs e)
    {
        var status = e.FileStatus;
        var action = status.Status switch
        {
            SyncStatus.Downloaded => "下载",
            SyncStatus.Updated => "更新",
            SyncStatus.Skipped => "跳过",
            SyncStatus.Failed => "失败",
            _ => "处理"
        };

        AddLog($"{action}文件: {status.FileName} - {status.Message}");
    }

    private void OnSyncCompleted(object? sender, SyncCompletedEventArgs e)
    {
        var result = e.SyncResult;
        AddLog($"同步完成: {result.Message}");

        ResetProgressDisplay();
    }

    private void OnPluginUpdating(object? sender, PluginUpdateEventArgs e)
    {
        AddLog($"⚠ {e.Message}");

        // 通知应用需要停止检测
        Dispatcher.UIThread.Post(() =>
        {
            SyncStatusText = "插件更新中，请停止检测...";
        });
    }

    private void OnPluginUpdated(object? sender, PluginUpdateEventArgs e)
    {
        if (e.NeedReloadPlugins)
        {
            AddLog($"✓ {e.Message}");

            Dispatcher.UIThread.Post(() =>
            {
                SyncStatusText = "插件更新完成，请重新启动检测";
            });
        }
    }

    #endregion

    #region 属性变更处理

    private void ResetProgressDisplay()
    {
        Dispatcher.UIThread.Post(() =>
        {
            CurrentProgress = 0;
            TotalProgress = 0;
            ProgressPercentage = 0;
            CurrentFile = string.Empty;
            ProgressMessage = string.Empty;
        });
    }

    partial void OnMinioConfigChanged(S3StorageConfiguration value)
    {
        ValidateConfiguration();
        SyncSelectedProviderFromConfig();
    }

    partial void OnSelectedProviderChanged(S3StorageProviderOption? value)
    {
        if (value != null)
            MinioConfig.Provider = value.ProviderId;
    }

    partial void OnOnlyImportantLogsChanged(bool value)
    {
        ApplyLogFilter();
    }

    private void SyncSelectedProviderFromConfig()
    {
        if (AvailableProviders.Count == 0)
            return;

        var providerId = S3StorageProviders.NormalizeProvider(MinioConfig.Provider);
        var matched = AvailableProviders.FirstOrDefault(p => string.Equals(p.ProviderId, providerId, StringComparison.OrdinalIgnoreCase))
                      ?? AvailableProviders[0];

        MinioConfig.Provider = matched.ProviderId;
        if (!ReferenceEquals(SelectedProvider, matched))
            SelectedProvider = matched;
    }

    private void RecalculateLocalStats()
    {
        var all = PluginFiles.Concat(ModelFiles).Concat(VideoFiles).Concat(ZipFiles).ToList();

        PendingFileCount = all.Count(f => f.Status == SyncStatus.Pending || f.Status == SyncStatus.Updated);
        ExistingFileCount = all.Count(f => f.Status == SyncStatus.Skipped || f.Status == SyncStatus.Downloaded);
        FailedFileCount = all.Count(f => f.Status == SyncStatus.Failed);
        FileTypeSummary = $"{PluginFiles.Count} / {ModelFiles.Count} / {VideoFiles.Count} / {ZipFiles.Count}";
    }

    private void ApplyLogFilter()
    {
        IEnumerable<UiLogEntry> logs = SyncLogs;
        if (OnlyImportantLogs)
            logs = logs.Where(l => l.Level == UiLogLevel.Warning || l.Level == UiLogLevel.Error);

        FilteredSyncLogs = new ObservableCollection<UiLogEntry>(logs);
    }

    #endregion
}

public enum UiLogLevel
{
    Info,
    Success,
    Warning,
    Error
}

public sealed class UiLogEntry
{
    public DateTime Time { get; set; }
    public UiLogLevel Level { get; set; }
    public string Category { get; set; } = "系统";
    public string Message { get; set; } = string.Empty;
}

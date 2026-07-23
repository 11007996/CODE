using LuxVideoDet.Core.Common;

namespace LuxVideoDet.Core.Storage;

/// <summary>
/// 存储服务接口
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// 保存错误图像
    /// </summary>
    Task<string?> SaveErrorImageAsync(
        Frame frame,
        string? timestamp = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 保存重训练图像
    /// </summary>
    Task<bool> SaveRetrainImageAsync(
        Frame frame,
        string? timestamp = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 保存视频
    /// </summary>
    Task<string?> SaveVideoAsync(
        List<Frame> frames,
        double fps,
        string? timestamp = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 清理过期文件
    /// </summary>
    Task CleanupOldFilesAsync(
        int retentionDays,
        CancellationToken cancellationToken = default);
}

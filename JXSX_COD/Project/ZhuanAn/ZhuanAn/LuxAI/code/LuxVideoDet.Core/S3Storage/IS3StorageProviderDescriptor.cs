namespace LuxVideoDet.Core.S3Storage;

/// <summary>
/// S3 存储实现描述器（用于 UI 自动注册/展示可选 Provider）。
/// </summary>
public interface IS3StorageProviderDescriptor
{
    /// <summary>Provider 唯一标识（写入配置）</summary>
    string ProviderId { get; }

    /// <summary>显示名称（用于 UI 下拉）</summary>
    string DisplayName { get; }

    /// <summary>排序优先级（越小越靠前）</summary>
    int Order => 100;
}

public sealed record S3StorageProviderOption(string ProviderId, string DisplayName);

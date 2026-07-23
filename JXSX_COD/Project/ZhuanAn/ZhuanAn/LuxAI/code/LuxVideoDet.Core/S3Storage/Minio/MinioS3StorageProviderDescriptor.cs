using LuxVideoDet.Core.Configuration.Models;

namespace LuxVideoDet.Core.S3Storage.Minio;

/// <summary>
/// MinIO Provider 描述器（自动注册到 Provider 下拉框）。
/// </summary>
public sealed class MinioS3StorageProviderDescriptor : IS3StorageProviderDescriptor
{
    public string ProviderId => S3StorageProviders.MinioSdk;
    public string DisplayName => "MinIO（S3 兼容）";
    public int Order => 10;
}

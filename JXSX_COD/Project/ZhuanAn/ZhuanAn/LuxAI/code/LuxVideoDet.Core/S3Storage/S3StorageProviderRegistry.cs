using System.Reflection;

namespace LuxVideoDet.Core.S3Storage;

/// <summary>
/// 自动发现并注册 S3 Provider 描述器。
/// </summary>
public static class S3StorageProviderRegistry
{
    public static IReadOnlyList<S3StorageProviderOption> DiscoverAvailableProviders()
    {
        var descriptors = new List<IS3StorageProviderDescriptor>();
        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            if (type.IsAbstract || type.IsInterface)
                continue;
            if (!typeof(IS3StorageProviderDescriptor).IsAssignableFrom(type))
                continue;
            if (type.GetConstructor(Type.EmptyTypes) == null)
                continue;

            if (Activator.CreateInstance(type) is IS3StorageProviderDescriptor descriptor)
                descriptors.Add(descriptor);
        }

        return descriptors
            .OrderBy(d => d.Order)
            .ThenBy(d => d.DisplayName, StringComparer.OrdinalIgnoreCase)
            .Select(d => new S3StorageProviderOption(d.ProviderId, d.DisplayName))
            .ToList();
    }
}

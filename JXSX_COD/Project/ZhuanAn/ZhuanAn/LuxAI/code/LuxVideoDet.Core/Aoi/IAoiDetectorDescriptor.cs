using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Aoi;

/// <summary>
/// AOI 检测器描述符 — 每种实现提供一个 Descriptor 供 <see cref="AoiDetectorFactory"/> 反射注册。
/// </summary>
public interface IAoiDetectorDescriptor
{
    string TypeKey { get; }
    IReadOnlyList<string> Aliases { get; }
    string DisplayName { get; }
    IAoiDetector Create(ILogger logger);
}

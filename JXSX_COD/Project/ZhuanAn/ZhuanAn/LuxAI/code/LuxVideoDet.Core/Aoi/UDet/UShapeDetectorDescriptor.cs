using LuxVideoDet.Core.Aoi;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Aoi.UDet;

public sealed class UShapeDetectorDescriptor : IAoiDetectorDescriptor
{
    public string TypeKey => "u_shape";

    public IReadOnlyList<string> Aliases => new[] { "udet" };

    public string DisplayName => "U 型开口方向检测";

    public IAoiDetector Create(ILogger logger) => new UShapeDetector(logger);
}

using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi;

/// <summary>
/// AOI 检测器接口
/// </summary>
public interface IAoiDetector
{
    /// <summary>检测器名称</summary>
    string Name { get; }

    /// <summary>
    /// 初始化检测器
    /// </summary>
    void Initialize(Dictionary<string, object>? parameters = null);

    /// <summary>
    /// 检测单个 ROI 区域
    /// </summary>
    AoiResult Detect(Mat roi, Dictionary<string, object>? parameters = null);

    /// <summary>
    /// 批量检测多个 ROI 区域
    /// </summary>
    AoiResult[] DetectBatch(Mat[] rois, Dictionary<string, object>? parameters = null);
}

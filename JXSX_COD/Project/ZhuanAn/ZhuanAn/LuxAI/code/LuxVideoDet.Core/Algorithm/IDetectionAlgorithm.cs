using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.Configuration.Models;
using OpenCvSharp;

namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 检测算法接口。
/// </summary>
public interface IDetectionAlgorithm : IDisposable
{
    string AlgorithmType { get; }
    bool IsInitialized { get; }
    RenderOptions RenderOptions { get; }

    List<RegionDefinition> GetRequiredRegions();
    List<string> GetDefaultClasses();
    string GetEngineType();
    string GetDeviceType();

    /// <summary>
    /// 用算法配置初始化（区域、存储、通知等均从配置中读取）。
    /// </summary>
    void Initialize(AlgorithmConfig config);

    /// <summary>
    /// 处理一帧图像，返回检测结果。
    /// </summary>
    Results.DetectionResult Process(Frame frame);

    /// <summary>
    /// 将算法的标注（检测框、状态信息、区域等）重新绘制到指定帧上。
    /// 用于多算法合成场景：每个算法把自己的标注画到同一张画布上，避免闪烁。
    /// </summary>
    /// <param name="frame">要绘制的目标帧</param>
    /// <param name="result">该算法的最新检测结果（作为绘制数据源）</param>
    /// <param name="yOffset">UI 元素（FPS/状态信息）的纵向偏移量，用于多算法竖向错开</param>
    void RenderAnnotations(Mat frame, Results.DetectionResult result, int yOffset = 0);

    void Reset();
    string GetCurrentState();
}

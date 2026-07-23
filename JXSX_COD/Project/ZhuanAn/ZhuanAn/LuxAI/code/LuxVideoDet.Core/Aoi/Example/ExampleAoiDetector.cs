// -----------------------------------------------------------------------------
// 示例 AOI 检测器（传统 CV / OpenCV）
// 作用：演示继承 AoiDetectorBase，在单帧 ROI 上实现检测逻辑并填充 AoiResult。
//       复制为新 AOI 时以本文件为主：实现 OnDetect（可选 OnInitialize），勿忘 Mat 释放与轻量 Data。
// -----------------------------------------------------------------------------

using LuxVideoDet.Core.Aoi;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.Example;

/// <summary>
/// 自定义 AOI 示例：继承 <see cref="AoiDetectorBase"/>。
/// </summary>
/// <remarks>
/// <para><b>继承 AoiDetectorBase 时：</b></para>
/// <list type="bullet">
/// <item><description><b>必须实现（abstract）</b>：<see cref="Name"/>、<see cref="AoiDetectorBase.OnDetect"/>。</description></item>
/// <item><description><b>可选覆盖（virtual）</b>：<see cref="AoiDetectorBase.OnInitialize"/>（加载模型/阈值）、
/// <see cref="DetectBatch"/>（默认逐张调用 <see cref="IAoiDetector.Detect"/>，可改为批处理）。</description></item>
/// <item><description><b>一般由基类完成</b>：<see cref="IAoiDetector.Initialize"/>、参数合并、异常捕获。</description></item>
/// </list>
/// <para>另需配套 <see cref="ExampleAoiDescriptor"/>（<see cref="IAoiDetectorDescriptor"/>）；示例带 <see cref="ExampleTemplateAttribute"/> 不注册。</para>
/// </remarks>
public sealed class ExampleAoiDetector : AoiDetectorBase
{
    // 必须：检测器标识
    public override string Name => "ExampleAoi";

    public ExampleAoiDetector(ILogger logger) : base(logger)
    {
    }

    // 必须：单 ROI 核心算法（可选：另覆盖 OnInitialize 做一次性加载）
    protected override AoiResult OnDetect(Mat roi, Dictionary<string, object> parameters)
    {
        // 示例：在此处接入轮廓/模板/深度学习等逻辑
        return new AoiResult
        {
            Success = false,
            Message = "示例检测器：请替换为实际 ROI 处理逻辑",
            Confidence = 0f
        };
    }
}

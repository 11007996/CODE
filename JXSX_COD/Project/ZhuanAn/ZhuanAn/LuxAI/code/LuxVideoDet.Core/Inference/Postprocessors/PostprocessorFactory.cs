using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Inference.Postprocessors;

/// <summary>
/// 后处理器工厂 - 根据任务类型创建后处理器
/// </summary>
public class PostprocessorFactory
{
    private readonly ILoggerFactory _loggerFactory;

    public PostprocessorFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    /// <summary>
    /// 根据任务类型创建后处理器
    /// </summary>
    public IPostprocessor GetPostprocessor(ModelType taskType)
    {
        return taskType switch
        {
            ModelType.Detection => new DetectionPostprocessor(
                _loggerFactory.CreateLogger<DetectionPostprocessor>()),
            
            ModelType.Segmentation => new SegmentationPostprocessor(
                _loggerFactory.CreateLogger<SegmentationPostprocessor>()),

            // SegmentationTracking：与 Segmentation 同一套实例分割解码；MOT 仅在显式选此项时于算法层启用。
            ModelType.SegmentationTracking => new SegmentationPostprocessor(
                _loggerFactory.CreateLogger<SegmentationPostprocessor>()),

            ModelType.PoseEstimation => new PosePostprocessor(
                _loggerFactory.CreateLogger<PosePostprocessor>()),
            
            ModelType.Obb => new ObbPostprocessor(
                _loggerFactory.CreateLogger<ObbPostprocessor>()),
            
            ModelType.Classification => new ClassificationPostprocessor(
                _loggerFactory.CreateLogger<ClassificationPostprocessor>()),

            // DetectionTracking：与 Detection 解码相同 + 算法层 MOT。
            ModelType.DetectionTracking => new DetectionPostprocessor(
                _loggerFactory.CreateLogger<DetectionPostprocessor>()),

            // Track：预留原生追踪 ONNX；解码暂与 Detection 对齐，算法层不写 MOT。
            ModelType.Track => new DetectionPostprocessor(
                _loggerFactory.CreateLogger<DetectionPostprocessor>()),

            _ => throw new NotSupportedException($"不支持的任务类型: {taskType}")
        };
    }
}

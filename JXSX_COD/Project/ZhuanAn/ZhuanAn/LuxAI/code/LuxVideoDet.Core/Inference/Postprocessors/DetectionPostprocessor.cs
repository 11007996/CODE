using LuxVideoDet.Core.Inference.Results;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Inference.Postprocessors;

/// <summary>
/// 目标检测后处理器 (YOLOv8-det)
/// 输出格式: [1, 4+nc, anchors]
/// </summary>
public class DetectionPostprocessor : IPostprocessor
{
    private readonly ILogger<DetectionPostprocessor> _logger;

    public ModelType TaskType => ModelType.Detection;

    public DetectionPostprocessor(ILogger<DetectionPostprocessor> logger)
    {
        _logger = logger;
    }

    public List<Detection> Process(float[][] outputs, PostprocessContext context)
    {
        var output = outputs[0];
        var detections = new List<Detection>();

        _logger.LogDebug("开始后处理 Anchors={Anchors}, Classes={Classes}",
            context.NumAnchors, context.NumClasses);

        // 遍历所有锚点
        for (int i = 0; i < context.NumAnchors; i++)
        {
            // 获取最大置信度的类别
            var (maxConf, maxClassId) = GetMaxClassConfidence(output, i, context);

            if (maxConf < context.ConfThreshold)
                continue;

            // 获取边界框 (cx, cy, w, h)
            var cx = output[i];
            var cy = output[i + context.NumAnchors];
            var w = output[i + context.NumAnchors * 2];
            var h = output[i + context.NumAnchors * 3];

            // 转换并映射回原始图像
            var detection = CreateDetection(cx, cy, w, h, maxConf, maxClassId, context);

            if (detection != null)
                detections.Add(detection);
        }

        _logger.LogDebug("后处理完成，检测到 {Count} 个候选框", detections.Count);

        // 应用 NMS
        var result = ApplyNMS(detections, context.IouThreshold);

        _logger.LogDebug("NMS 后剩余 {Count} 个检测框", result.Count);

        return result;
    }

    private static (float maxConf, int maxClassId) GetMaxClassConfidence(
        float[] output,
        int anchorIndex,
        PostprocessContext context)
    {
        var maxConf = 0f;
        var maxClassId = 0;

        for (int c = 0; c < context.NumClasses; c++)
        {
            var conf = output[anchorIndex + context.NumAnchors * (4 + c)];
            if (conf > maxConf)
            {
                maxConf = conf;
                maxClassId = c;
            }
        }

        return (maxConf, maxClassId);
    }

    private static Detection? CreateDetection(
        float cx, float cy, float w, float h,
        float confidence, int classId,
        PostprocessContext context)
    {
        // 转换为 (x1, y1, x2, y2) 在 letterbox 图像上
        var x1Letterbox = cx - w / 2;
        var y1Letterbox = cy - h / 2;
        var x2Letterbox = cx + w / 2;
        var y2Letterbox = cy + h / 2;

        // 去除 padding 并缩放回原始图像
        var x1 = (x1Letterbox - context.PadW) / context.Ratio;
        var y1 = (y1Letterbox - context.PadH) / context.Ratio;
        var x2 = (x2Letterbox - context.PadW) / context.Ratio;
        var y2 = (y2Letterbox - context.PadH) / context.Ratio;

        // 边界裁剪
        x1 = Math.Max(0, Math.Min(x1, context.OriginalWidth));
        y1 = Math.Max(0, Math.Min(y1, context.OriginalHeight));
        x2 = Math.Max(0, Math.Min(x2, context.OriginalWidth));
        y2 = Math.Max(0, Math.Min(y2, context.OriginalHeight));

        // 检查有效性
        if (x2 <= x1 || y2 <= y1)
            return null;

        return new Detection
        {
            ClassId = classId,
            ClassName = classId < context.ClassNames.Count
                ? context.ClassNames[classId]
                : $"class{classId}",
            Confidence = confidence,
            BoundingBox = new BoundingBox
            {
                X = x1,
                Y = y1,
                Width = x2 - x1,
                Height = y2 - y1
            }
        };
    }

    private static List<Detection> ApplyNMS(List<Detection> detections, float iouThreshold)
    {
        var result = new List<Detection>();
        var sorted = detections.OrderByDescending(d => d.Confidence).ToList();

        while (sorted.Count > 0)
        {
            var best = sorted[0];
            result.Add(best);
            sorted.RemoveAt(0);

            sorted = sorted.Where(d =>
                d.ClassId != best.ClassId ||
                best.BoundingBox.CalculateIou(d.BoundingBox) < iouThreshold).ToList();
        }

        return result;
    }
}

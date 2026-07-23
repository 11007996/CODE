using LuxVideoDet.Core.Inference.Results;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Inference.Postprocessors;

/// <summary>
/// 旋转目标框后处理器 (YOLOv8-obb)
/// 输出格式: [1, 5+nc, anchors] (cx, cy, w, h, angle + classes)
/// </summary>
public class ObbPostprocessor : IPostprocessor
{
    private readonly ILogger<ObbPostprocessor> _logger;

    public ModelType TaskType => ModelType.Obb;

    public ObbPostprocessor(ILogger<ObbPostprocessor> logger)
    {
        _logger = logger;
    }

    public List<Detection> Process(float[][] outputs, PostprocessContext context)
    {
        var output = outputs[0];
        var detections = new List<Detection>();

        _logger.LogDebug("开始 OBB 后处理 Anchors={Anchors}, Classes={Classes}",
            context.NumAnchors, context.NumClasses);

        for (int i = 0; i < context.NumAnchors; i++)
        {
            var (maxConf, maxClassId) = GetMaxClassConfidence(output, i, context);

            if (maxConf < context.ConfThreshold)
                continue;

            var cx = output[i];
            var cy = output[i + context.NumAnchors];
            var w = output[i + context.NumAnchors * 2];
            var h = output[i + context.NumAnchors * 3];
            var angle = output[i + context.NumAnchors * 4];

            var detection = CreateDetection(cx, cy, w, h, angle, maxConf, maxClassId, context);

            if (detection != null)
                detections.Add(detection);
        }

        _logger.LogDebug("OBB 后处理完成，检测到 {Count} 个候选框", detections.Count);

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
            var conf = output[anchorIndex + context.NumAnchors * (5 + c)];
            if (conf > maxConf)
            {
                maxConf = conf;
                maxClassId = c;
            }
        }

        return (maxConf, maxClassId);
    }

    private static Detection? CreateDetection(
        float cx, float cy, float w, float h, float angle,
        float confidence, int classId,
        PostprocessContext context)
    {
        var centerX = (cx - context.PadW) / context.Ratio;
        var centerY = (cy - context.PadH) / context.Ratio;
        var width = w / context.Ratio;
        var height = h / context.Ratio;

        if (centerX < 0 || centerX > context.OriginalWidth ||
            centerY < 0 || centerY > context.OriginalHeight ||
            width <= 0 || height <= 0)
            return null;

        var halfW = width / 2;
        var halfH = height / 2;
        var x1 = Math.Max(0, centerX - halfW);
        var y1 = Math.Max(0, centerY - halfH);
        var x2 = Math.Min(context.OriginalWidth, centerX + halfW);
        var y2 = Math.Min(context.OriginalHeight, centerY + halfH);

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
            },
            RotationAngle = angle
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
                CalculateRotatedIoU(best, d) < iouThreshold).ToList();
        }

        return result;
    }

    private static float CalculateRotatedIoU(Detection a, Detection b)
    {
        var x1 = Math.Max(a.BoundingBox.X1, b.BoundingBox.X1);
        var y1 = Math.Max(a.BoundingBox.Y1, b.BoundingBox.Y1);
        var x2 = Math.Min(a.BoundingBox.X2, b.BoundingBox.X2);
        var y2 = Math.Min(a.BoundingBox.Y2, b.BoundingBox.Y2);

        var intersection = Math.Max(0, x2 - x1) * Math.Max(0, y2 - y1);
        var areaA = a.BoundingBox.Area;
        var areaB = b.BoundingBox.Area;
        var union = areaA + areaB - intersection;

        return union > 0 ? (float)(intersection / union) : 0;
    }
}

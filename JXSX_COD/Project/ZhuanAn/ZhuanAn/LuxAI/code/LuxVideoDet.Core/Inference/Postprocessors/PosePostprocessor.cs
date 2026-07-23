using LuxVideoDet.Core.Inference.Results;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Inference.Postprocessors;

/// <summary>
/// 姿态估计后处理器 (YOLOv8-pose)
/// 输出格式: [1, 4+nc+51, anchors] (bbox + classes + 17 keypoints * 3)
/// </summary>
public class PosePostprocessor : IPostprocessor
{
    private readonly ILogger<PosePostprocessor> _logger;
    private const int NumKeypoints = 17;
    private const int KeypointDim = 3;

    public ModelType TaskType => ModelType.PoseEstimation;

    public PosePostprocessor(ILogger<PosePostprocessor> logger)
    {
        _logger = logger;
    }

    public List<Detection> Process(float[][] outputs, PostprocessContext context)
    {
        var output = outputs[0];
        var detections = new List<Detection>();

        _logger.LogDebug("开始姿态估计后处理 Anchors={Anchors}, Classes={Classes}",
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

            var keypoints = new KeyPoint[NumKeypoints];
            for (int k = 0; k < NumKeypoints; k++)
            {
                var kpX = output[i + context.NumAnchors * (4 + context.NumClasses + k * KeypointDim)];
                var kpY = output[i + context.NumAnchors * (4 + context.NumClasses + k * KeypointDim + 1)];
                var kpConf = output[i + context.NumAnchors * (4 + context.NumClasses + k * KeypointDim + 2)];

                keypoints[k] = new KeyPoint
                {
                    X = (kpX - context.PadW) / context.Ratio,
                    Y = (kpY - context.PadH) / context.Ratio,
                    Confidence = kpConf,
                    Visible = kpConf > 0.5f
                };
            }

            var detection = CreateDetection(cx, cy, w, h, maxConf, maxClassId, keypoints, context);

            if (detection != null)
                detections.Add(detection);
        }

        _logger.LogDebug("姿态估计后处理完成，检测到 {Count} 个候选框", detections.Count);

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
        float confidence, int classId, KeyPoint[] keypoints,
        PostprocessContext context)
    {
        var x1Letterbox = cx - w / 2;
        var y1Letterbox = cy - h / 2;
        var x2Letterbox = cx + w / 2;
        var y2Letterbox = cy + h / 2;

        var x1 = (x1Letterbox - context.PadW) / context.Ratio;
        var y1 = (y1Letterbox - context.PadH) / context.Ratio;
        var x2 = (x2Letterbox - context.PadW) / context.Ratio;
        var y2 = (y2Letterbox - context.PadH) / context.Ratio;

        x1 = Math.Max(0, Math.Min(x1, context.OriginalWidth));
        y1 = Math.Max(0, Math.Min(y1, context.OriginalHeight));
        x2 = Math.Max(0, Math.Min(x2, context.OriginalWidth));
        y2 = Math.Max(0, Math.Min(y2, context.OriginalHeight));

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
            },
            KeyPoints = keypoints
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

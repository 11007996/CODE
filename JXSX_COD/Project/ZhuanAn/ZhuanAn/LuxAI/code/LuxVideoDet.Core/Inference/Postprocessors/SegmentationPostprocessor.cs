using LuxVideoDet.Core.Inference.Results;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Inference.Postprocessors;

/// <summary>
/// 实例分割后处理器 (YOLOv8-seg)
/// 输出格式: 
///   - output0: [1, 4+nc+32, anchors] (bbox + classes + mask coefficients)
///   - output1: [1, 32, H/4, W/4] (mask prototypes)
/// </summary>
public class SegmentationPostprocessor : IPostprocessor
{
    private readonly ILogger<SegmentationPostprocessor> _logger;
    private const int MaskCoefficients = 32;

    public ModelType TaskType => ModelType.Segmentation;

    public SegmentationPostprocessor(ILogger<SegmentationPostprocessor> logger)
    {
        _logger = logger;
    }

    public List<Detection> Process(float[][] outputs, PostprocessContext context)
    {
        _logger.LogDebug("开始分割后处理，输出数量={OutputCount}", outputs.Length);

        var output0 = outputs[0];
        var output1 = outputs.Length > 1 ? outputs[1] : null;

        if (output1 == null)
        {
            _logger.LogWarning("未找到掩码原型！分割模型应该有 2 个输出，但只有 {Count} 个", outputs.Length);
        }

        var detections = new List<Detection>();

        for (int i = 0; i < context.NumAnchors; i++)
        {
            var (maxConf, maxClassId) = GetMaxClassConfidence(output0, i, context);

            if (maxConf < context.ConfThreshold)
                continue;

            var cx = output0[i];
            var cy = output0[i + context.NumAnchors];
            var w = output0[i + context.NumAnchors * 2];
            var h = output0[i + context.NumAnchors * 3];

            var maskCoeffs = new float[MaskCoefficients];
            for (int m = 0; m < MaskCoefficients; m++)
            {
                maskCoeffs[m] = output0[i + context.NumAnchors * (4 + context.NumClasses + m)];
            }

            var detection = CreateDetection(cx, cy, w, h, maxConf, maxClassId, maskCoeffs, context);

            if (detection != null)
                detections.Add(detection);
        }

        var afterNMS = ApplyNMS(detections, context.IouThreshold);

        if (output1 != null && afterNMS.Count > 0)
        {
            _logger.LogDebug("为 {Count} 个检测生成掩码", afterNMS.Count);
            GenerateMasks(afterNMS, output1, context);

            int masksGenerated = afterNMS.Count(d => d.Mask != null);
            _logger.LogDebug("成功生成 {Generated}/{Total} 个掩码", masksGenerated, afterNMS.Count);
        }

        _logger.LogDebug("分割后处理完成，最终结果 {Count} 个检测", afterNMS.Count);

        return afterNMS;
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
        float confidence, int classId, float[] maskCoeffs,
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
            MaskCoefficients = maskCoeffs
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

    private void GenerateMasks(List<Detection> detections, float[] prototypes, PostprocessContext context)
    {
        int totalPixels = prototypes.Length / MaskCoefficients;
        int maskSize = (int)Math.Sqrt(totalPixels);

        if (maskSize * maskSize * MaskCoefficients != prototypes.Length)
        {
            _logger.LogError("无效的原型尺寸: {Length}，无法确定掩码维度", prototypes.Length);
            return;
        }

        int maskH = maskSize;
        int maskW = maskSize;

        _logger.LogDebug("原型数组大小: {Length}，掩码维度: {H}x{W}", prototypes.Length, maskH, maskW);

        foreach (var detection in detections)
        {
            if (detection.MaskCoefficients == null || detection.MaskCoefficients.Length != MaskCoefficients)
                continue;

            try
            {
                var maskProto = new float[maskH * maskW];

                for (int i = 0; i < maskH * maskW; i++)
                {
                    float sum = 0;
                    for (int c = 0; c < MaskCoefficients; c++)
                    {
                        sum += detection.MaskCoefficients[c] * prototypes[c * maskH * maskW + i];
                    }
                    maskProto[i] = sum;
                }

                for (int i = 0; i < maskProto.Length; i++)
                {
                    maskProto[i] = Sigmoid(maskProto[i]);
                }

                var mask = UpsampleAndCropMask(maskProto, maskH, maskW, detection, context);

                if (mask != null)
                {
                    detection.Mask = mask;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "生成掩码失败");
            }
        }
    }

    private static float Sigmoid(float x)
    {
        return 1.0f / (1.0f + MathF.Exp(-x));
    }

    private static byte[,]? UpsampleAndCropMask(float[] maskProto, int maskH, int maskW,
        Detection detection, PostprocessContext context)
    {
        int x1_orig = (int)detection.BoundingBox.X1;
        int y1_orig = (int)detection.BoundingBox.Y1;
        int x2_orig = (int)detection.BoundingBox.X2;
        int y2_orig = (int)detection.BoundingBox.Y2;

        x1_orig = Math.Max(0, Math.Min(x1_orig, context.OriginalWidth - 1));
        y1_orig = Math.Max(0, Math.Min(y1_orig, context.OriginalHeight - 1));
        x2_orig = Math.Max(0, Math.Min(x2_orig, context.OriginalWidth - 1));
        y2_orig = Math.Max(0, Math.Min(y2_orig, context.OriginalHeight - 1));

        int boxW = x2_orig - x1_orig;
        int boxH = y2_orig - y1_orig;

        if (boxW <= 0 || boxH <= 0)
            return null;

        var mask = new byte[boxH, boxW];

        for (int y = 0; y < boxH; y++)
        {
            for (int x = 0; x < boxW; x++)
            {
                float origX = x1_orig + x;
                float origY = y1_orig + y;

                float letterboxX = origX * context.Ratio + context.PadW;
                float letterboxY = origY * context.Ratio + context.PadH;

                float inputSize = (context.OriginalWidth * context.Ratio + 2 * context.PadW);
                float downscale = inputSize / maskW;

                float srcX = letterboxX / downscale;
                float srcY = letterboxY / downscale;

                if (srcX < 0 || srcY < 0 || srcX >= maskW - 1 || srcY >= maskH - 1)
                {
                    mask[y, x] = 0;
                    continue;
                }

                int x0 = (int)srcX;
                int y0 = (int)srcY;
                int x1 = Math.Min(x0 + 1, maskW - 1);
                int y1 = Math.Min(y0 + 1, maskH - 1);

                float dx = srcX - x0;
                float dy = srcY - y0;

                float v00 = maskProto[y0 * maskW + x0];
                float v10 = maskProto[y0 * maskW + x1];
                float v01 = maskProto[y1 * maskW + x0];
                float v11 = maskProto[y1 * maskW + x1];

                float v0 = v00 * (1 - dx) + v10 * dx;
                float v1 = v01 * (1 - dx) + v11 * dx;
                float value = v0 * (1 - dy) + v1 * dy;

                mask[y, x] = value > 0.5f ? (byte)255 : (byte)0;
            }
        }

        return mask;
    }
}

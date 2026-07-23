using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Inference.Results;
using LuxVideoDet.Core.Utils;
using OpenCvSharp;
using System;

namespace LuxVideoDet.Core.Rendering;

/// <summary>
/// 按 <see cref="ModelType"/> 选择绘制策略，统一支持检测 / 分割 / 姿态 / OBB / 分类等 YOLO 常见输出。
/// 业务算法应通过本类绘制，并传入当前推理对应的 <see cref="ModelType"/>（算法基类在 Initialize 后会缓存为 ModelTaskType）。
/// </summary>
public static class TaskAwareDetectionRenderer
{
    /// <summary>COCO 17 关键点骨架边（0-based 索引）</summary>
    private static readonly (int A, int B)[] Coco17SkeletonEdges =
    {
        (0, 1), (0, 2), (1, 3), (2, 4),
        (5, 6), (5, 7), (6, 8), (7, 9), (8, 10),
        (5, 11), (6, 12), (11, 12), (11, 13), (12, 14), (13, 15), (14, 16)
    };

    public static void Draw(
        Mat image,
        Detection detection,
        Scalar color,
        ModelType taskType,
        DetectionDrawOptions? options = null)
    {
        options ??= new DetectionDrawOptions();

        switch (taskType)
        {
            case ModelType.Classification:
                DrawClassification(image, detection, color, options);
                break;

            case ModelType.PoseEstimation:
                DrawPose(image, detection, color, options);
                break;

            case ModelType.Obb:
                DrawObb(image, detection, color, options);
                break;

            case ModelType.Segmentation:
            case ModelType.SegmentationTracking:
            case ModelType.Detection:
            case ModelType.Track:
            case ModelType.DetectionTracking:
            case ModelType.Auto:
            default:
                DrawingHelper.DrawDetectionAnnotation(
                    image, detection, color,
                    options.ShowBox, options.ShowLabel, options.BoxThickness, options.MaskAlpha,
                    options.LabelFormatter);
                break;
        }
    }

    private static void DrawClassification(Mat image, Detection detection, Scalar color, DetectionDrawOptions options)
    {
        ClassificationOverlayRenderer.Draw(image, detection, color, options);
    }

    private static void DrawPose(Mat image, Detection detection, Scalar color, DetectionDrawOptions options)
    {
        DrawingHelper.DrawDetectionAnnotation(
            image, detection, color,
            options.ShowBox, options.ShowLabel, options.BoxThickness, maskAlpha: 0f,
            options.LabelFormatter);

        var kps = detection.KeyPoints;
        if (kps is not { Length: > 0 }) return;

        const float kptConfMin = 0.25f;
        foreach (var kp in kps)
        {
            if (kp.Confidence < kptConfMin) continue;
            Cv2.Circle(image, new Point((int)kp.X, (int)kp.Y), 3, color, -1);
        }

        if (!options.DrawPoseSkeleton) return;

        foreach (var (a, b) in Coco17SkeletonEdges)
        {
            if (a >= kps.Length || b >= kps.Length) continue;
            var pa = kps[a];
            var pb = kps[b];
            if (pa.Confidence < kptConfMin || pb.Confidence < kptConfMin) continue;
            Cv2.Line(image,
                new Point((int)pa.X, (int)pa.Y),
                new Point((int)pb.X, (int)pb.Y),
                color, 2);
        }
    }

    private static void DrawObb(Mat image, Detection detection, Scalar color, DetectionDrawOptions options)
    {
        var bbox = detection.BoundingBox;
        var angle = detection.RotationAngle;

        if (angle.HasValue)
        {
            var center = new Point2f(bbox.CenterX, bbox.CenterY);
            var size = new Size2f(bbox.Width, bbox.Height);
            // 后处理器输出一般为弧度，OpenCV RotatedRect 使用度
            float angleDeg = angle.Value * 180f / MathF.PI;
            var rr = new RotatedRect(center, size, angleDeg);
            var pts = Cv2.BoxPoints(rr);
            var ip = pts.Select(p => new Point((int)Math.Round(p.X), (int)Math.Round(p.Y))).ToArray();
            Cv2.Polylines(image, new[] { ip }, true, color, options.BoxThickness);
        }
        else
        {
            DrawingHelper.DrawDetectionAnnotation(
                image, detection, color,
                options.ShowBox, showLabel: false, options.BoxThickness, maskAlpha: 0f);
        }

        if (options.ShowLabel)
        {
            var name = options.LabelFormatter?.Invoke(detection) ?? detection.ClassName;
            var label = $"{name} {detection.Confidence:F2}";
            DrawingHelper.DrawLabel(image, label,
                new Point((int)bbox.X1, (int)bbox.Y1 - 5), color);
        }
    }
}

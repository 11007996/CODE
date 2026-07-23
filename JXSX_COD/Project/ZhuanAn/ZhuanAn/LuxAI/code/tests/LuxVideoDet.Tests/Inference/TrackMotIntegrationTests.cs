using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Inference.Onnx;
using LuxVideoDet.Core.Inference.Postprocessors;
using LuxVideoDet.Core.Tracking;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Tests.Inference;

/// <summary>
/// 默认使用仓库根下：<c>resources/models/tearofftab20260326.onnx</c> 与 <c>resources/videos/UCS_TearOffTab_TopView.mp4</c>
///（例如 <c>…/LuxVideoDet/resources/...</c>）。未找到时跳过，避免 CI 无大文件失败。
/// <para>
/// 可选环境变量（绝对路径）：<c>LUXVIDEODET_TEST_ONNX</c>、<c>LUXVIDEODET_TEST_VIDEO</c>；或仅 <c>LUXVIDEODET_REPO</c> 指向仓库根。
/// </para>
/// </summary>
public sealed class TrackMotIntegrationTests
{
    /// <summary>
    /// 自测试输出目录逐级向上，寻找同时存在 ONNX 与视频的仓库根。
    /// </summary>
    private static (string onnx, string mp4)? TryResolveAssetsFromDisk()
    {
        var envOnnx = Environment.GetEnvironmentVariable("LUXVIDEODET_TEST_ONNX");
        var envVideo = Environment.GetEnvironmentVariable("LUXVIDEODET_TEST_VIDEO");
        if (!string.IsNullOrWhiteSpace(envOnnx) && !string.IsNullOrWhiteSpace(envVideo)
            && File.Exists(envOnnx) && File.Exists(envVideo))
            return (envOnnx, envVideo);

        var repo = Environment.GetEnvironmentVariable("LUXVIDEODET_REPO");
        if (!string.IsNullOrWhiteSpace(repo))
        {
            var o = Path.Combine(repo, "resources", "models", "tearofftab20260326.onnx");
            var v = Path.Combine(repo, "resources", "videos", "UCS_TearOffTab_TopView.mp4");
            if (File.Exists(o) && File.Exists(v))
                return (o, v);
        }

        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            var onnx = Path.Combine(dir.FullName, "resources", "models", "tearofftab20260326.onnx");
            var mp4 = Path.Combine(dir.FullName, "resources", "videos", "UCS_TearOffTab_TopView.mp4");
            if (File.Exists(onnx) && File.Exists(mp4))
                return (onnx, mp4);
            dir = dir.Parent;
        }

        return null;
    }

    [Fact]
    public async Task DetectionTrackingOnnx_WithVideo_ProducesTrackIds_And_ModelInfoIsDetectionTracking()
    {
        if (TryResolveAssetsFromDisk() is not (var onnx, var mp4))
            return;

        using var factory = LoggerFactory.Create(b => b.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning));
        var postFactory = new PostprocessorFactory(factory);
        var config = new InferenceConfig
        {
            ModelPath = onnx,
            Device = InferenceDevice.CPU,
            ModelType = ModelType.DetectionTracking,
            ConfidenceThreshold = 0.2f,
            IouThreshold = 0.45f,
            InputSize = new ImageSize { Width = 640, Height = 640 },
            Classes = new List<string> { "a" },
            ThreadCount = 1
        };

        using var engine = new OnnxInferenceEngine(config, factory.CreateLogger<OnnxInferenceEngine>(), postFactory);
        await engine.LoadModelAsync(onnx, CancellationToken.None);

        Assert.Equal(ModelType.DetectionTracking, engine.GetModelInfo().Type);

        var tracker = new MultiObjectTracker(new TrackingConfig { MinHits = 2, TrackIouThreshold = 0.2f, MaxMissedFrames = 50 });
        using var cap = new VideoCapture(mp4);
        Assert.True(cap.IsOpened(), "VideoCapture should open test mp4.");

        var idsWithHits = new HashSet<int>();
        for (var i = 0; i < 120; i++)
        {
            using var mat = new Mat();
            if (!cap.Read(mat) || mat.Empty())
                break;

            var result = engine.Infer(mat);
            tracker.Update(result.Detections);

            foreach (var d in result.Detections)
            {
                if (d.TrackId is int id)
                    idsWithHits.Add(id);
            }
        }

        Assert.NotEmpty(idsWithHits);
    }
}

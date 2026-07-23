using System.Diagnostics;
using LuxVideoDet.Core.Aoi.HandLandmark;
using LuxVideoDet.Core.Utils.PythonNet;
using OpenCvSharp;

namespace LuxVideoDet.Tests.Aoi;

/// <summary>
/// 测量「无 C# 下采样」时手部子进程整段 <see cref="HandLandmarkerSubprocessInferenceSession.Detect"/> 耗时：
/// 输入已为 512×288（与 4K 长边 512 时推理分辨率一致），<c>InferenceMaxLongEdgePixels=512</c> 下不会执行 <c>Cv2.Resize</c>。
/// 仍包含：BGR 拷入 buffer、stdin/stdout、Python MediaPipe、JSON 行、反序列化与坐标映射。
/// <para>
/// 运行前设置 <c>LUXVIDEODET_HAND_TEST_VENV</c>（必填）；可选 <c>LUXVIDEODET_HAND_TEST_SRC</c>（未 pip 安装时用源码路径）。
/// 未设置 venv 时跳过并打印说明（测试标记为通过）。
/// </para>
/// </summary>
public class HandLandmarkerSubprocessPureInferBench
{
    private const string EnvVenv = "LUXVIDEODET_HAND_TEST_VENV";
    private const string EnvSrc = "LUXVIDEODET_HAND_TEST_SRC";

    [Fact]
    public void Detect_at_infer_resolution_no_resize_reports_avg_ms()
    {
        var venv = Environment.GetEnvironmentVariable(EnvVenv);
        if (string.IsNullOrWhiteSpace(venv))
        {
            Console.WriteLine(
                "[HandPureInferBench] SKIP: set {0} to luxvideopyplugin venv root (optional {1} if not pip-installed).",
                EnvVenv,
                EnvSrc);
            return;
        }

        venv = Path.GetFullPath(venv.Trim());
        if (!Directory.Exists(venv))
        {
            Console.WriteLine("[HandPureInferBench] SKIP: venv path does not exist.");
            return;
        }

        var srcRaw = Environment.GetEnvironmentVariable(EnvSrc);
        string packageRoot = "";
        if (!string.IsNullOrWhiteSpace(srcRaw))
        {
            var src = Path.GetFullPath(srcRaw.Trim());
            if (!Directory.Exists(src))
            {
                Console.WriteLine("[HandPureInferBench] SKIP: LUXVIDEODET_HAND_TEST_SRC path does not exist.");
                return;
            }

            packageRoot = src;
        }

        var py = PythonNetRuntimeHost.ResolveVenvPythonExecutable(venv);
        if (string.IsNullOrWhiteSpace(py) || !File.Exists(py))
        {
            Console.WriteLine("[HandPureInferBench] SKIP: venv Python not found.");
            return;
        }

        // 与 3840×2160、长边上限 512 时一致，确保 ComputeInferenceSize 不触发 Resize
        const int iw = 512;
        const int ih = 288;
        using var bgr = new Mat(ih, iw, MatType.CV_8UC3);
        Cv2.Randu(bgr, Scalar.All(0), Scalar.All(255));

        var options = new HandLandmarkerPythonOptions
        {
            VenvRoot = venv,
            ExtraPythonPathRoot = packageRoot,
            FrameDeltaMs = HandLandmarkerInferenceDefaults.NominalFrameDeltaMsAt30FpsCap,
            NumHands = 2,
            InferenceMaxLongEdgePixels = 512,
            MapImageLandmarksToOriginalPixels = true,
            ScaleWorldLandmarksWhenDownsampled = true,
        };

        using var session = new HandLandmarkerSubprocessInferenceSession(options, logger: null);
        session.EnsureInitialized();

        const int warmup = 5;
        for (var i = 0; i < warmup; i++)
            _ = session.Detect(bgr);

        const int n = 40;
        var sw = Stopwatch.StartNew();
        for (var i = 0; i < n; i++)
            _ = session.Detect(bgr);
        sw.Stop();

        var avgMs = sw.Elapsed.TotalMilliseconds / n;
        Console.WriteLine(
            "[HandPureInferBench] Subprocess Detect @ {0}x{1} (no C# Resize): avg {2:F3} ms (n={3}, excl. warmup {4}). " +
            "Includes: copy, pipe, Python MediaPipe, JSON, deserialize.",
            iw,
            ih,
            avgMs,
            n,
            warmup);

        Assert.True(avgMs < 500, $"unexpectedly slow: {avgMs} ms");
    }
}

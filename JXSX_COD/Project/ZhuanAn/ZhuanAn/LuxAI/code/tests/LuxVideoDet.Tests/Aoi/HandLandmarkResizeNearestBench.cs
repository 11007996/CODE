using System.Diagnostics;
using OpenCvSharp;

namespace LuxVideoDet.Tests.Aoi;

/// <summary>
/// 与手部 AOI 一致：4K BGR → 长边 512（512×288）下采样，<see cref="InterpolationFlags.Nearest"/>。
/// 用于回归「单帧 Resize 量级」；数值随 CPU/系统负载波动。
/// </summary>
public class HandLandmarkResizeNearestBench
{
    [Fact]
    public void NearestResize_4K_to_512_long_edge_reports_avg_ms_in_console()
    {
        using var src = new Mat(2160, 3840, MatType.CV_8UC3);
        Cv2.Randu(src, Scalar.All(0), Scalar.All(255));
        using var dst = new Mat();

        for (var w = 0; w < 10; w++)
            Cv2.Resize(src, dst, new Size(512, 288), 0, 0, InterpolationFlags.Nearest);

        const int n = 80;
        var sw = Stopwatch.StartNew();
        for (var i = 0; i < n; i++)
            Cv2.Resize(src, dst, new Size(512, 288), 0, 0, InterpolationFlags.Nearest);
        sw.Stop();

        var avgMs = sw.Elapsed.TotalMilliseconds / n;
        Console.WriteLine(
            "[HandLandmarkResizeBench] Nearest 3840x2160 BGR -> 512x288: avg {0:F3} ms (n={1})",
            avgMs,
            n);

        Assert.True(avgMs < 200, $"unexpectedly slow resize: {avgMs} ms");
    }
}

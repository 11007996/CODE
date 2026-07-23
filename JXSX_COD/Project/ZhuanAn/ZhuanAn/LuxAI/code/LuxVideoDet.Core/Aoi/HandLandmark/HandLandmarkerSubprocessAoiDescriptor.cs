using LuxVideoDet.Core.Aoi;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>
/// 注册名：<c>hand_landmarker_subprocess</c> — 独立 Python 进程跑 MediaPipe，避免与 OpenCvSharp 同进程双 OpenCV。
/// </summary>
public sealed class HandLandmarkerSubprocessAoiDescriptor : IAoiDetectorDescriptor
{
    public string TypeKey => "hand_landmarker_subprocess";

    public IReadOnlyList<string> Aliases { get; } = new[] { "mediapipe_hand_subprocess" };

    public string DisplayName => "MediaPipe 手部 (子进程)";

    public IAoiDetector Create(ILogger logger) => new HandLandmarkerSubprocessAoiDetector(logger);
}

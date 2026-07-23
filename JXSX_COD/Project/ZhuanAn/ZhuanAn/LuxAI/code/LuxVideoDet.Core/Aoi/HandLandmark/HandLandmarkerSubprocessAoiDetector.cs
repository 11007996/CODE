using LuxVideoDet.Core.Aoi;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>
/// 基于独立 Python 子进程的 MediaPipe 手部 AOI（见 <see cref="HandLandmarkerSubprocessInferenceSession"/>）。
/// 初始化参数：<c>mediapipa_venv</c> 必填（须为已安装 <c>luxvideopyplugin</c> 的 venv 根目录；键名历史兼容）。
/// 不需要 <c>python_dll</c>；<c>hand_aoi_per_frame_timeout_seconds</c> 不适用于子进程管道。
/// </summary>
public sealed class HandLandmarkerSubprocessAoiDetector : AoiDetectorBase, IDisposable
{
    private HandLandmarkerSubprocessInferenceSession? _session;

    public override string Name => "HandLandmarkerSubprocess";

    public HandLandmarkerSubprocessAoiDetector(ILogger logger) : base(logger)
    {
    }

    public void Dispose()
    {
        try
        {
            _session?.Dispose();
        }
        catch
        {
            // 忽略
        }

        _session = null;
    }

    protected override void OnInitialize()
    {
        var venv = GetParameter<string>("mediapipa_venv", string.Empty);
        if (string.IsNullOrWhiteSpace(venv))
        {
            throw new InvalidOperationException(
                "缺少 mediapipa_venv：请传入已安装 luxvideopyplugin 的 Python 虚拟环境根目录（内含 bin/python 或 Scripts/python.exe）。");
        }

        var maxLongEdge = GetParameter("inference_max_long_edge", 0);
        var mapToOriginalPixels = _parameters.ContainsKey("map_image_landmarks_to_original_pixels")
            ? GetParameter("map_image_landmarks_to_original_pixels", false)
            : maxLongEdge > 0;

        var modelPathRaw = GetParameter("hand_landmarker_model_path", "");
        var luxRootRaw = GetParameter("luxvideo_root", "");
        var extraPyRaw = GetParameter("extra_python_path", "");

        _session = new HandLandmarkerSubprocessInferenceSession(
            new HandLandmarkerPythonOptions
            {
                VenvRoot = venv.Trim(),
                ExtraPythonPathRoot = string.IsNullOrWhiteSpace(extraPyRaw) ? "" : extraPyRaw.Trim(),
                LuxVideoRoot = string.IsNullOrWhiteSpace(luxRootRaw) ? null : luxRootRaw.Trim(),
                PythonExecutablePath = null,
                FrameDeltaMs = GetParameter(
                    "frame_delta_ms",
                    HandLandmarkerInferenceDefaults.NominalFrameDeltaMsAt30FpsCap),
                NumHands = GetParameter("num_hands", 2),
                InferenceMaxLongEdgePixels = maxLongEdge > 0 ? maxLongEdge : null,
                MapImageLandmarksToOriginalPixels = mapToOriginalPixels,
                ScaleWorldLandmarksWhenDownsampled = GetParameter("scale_world_landmarks_when_downsampled", true),
                HandLandmarkerModelPath = string.IsNullOrWhiteSpace(modelPathRaw) ? null : modelPathRaw.Trim(),
            },
            _logger);

        try
        {
            _session.EnsureInitialized();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "手部子进程 hand_worker 启动失败。虚拟环境: {Venv}。请在该 venv 内安装 luxvideopyplugin（pip install 包或 whl），并确认模块 luxvideopyplugin.cli.hand_worker 可用。",
                venv.Trim());
            throw;
        }

        _logger.LogInformation(
            "HandLandmarker（子进程）已就绪：hand_worker 已启动 (venv={Venv})",
            venv.Trim());
    }

    protected override AoiResult OnDetect(Mat roi, Dictionary<string, object> parameters)
    {
        var session = _session ?? throw new InvalidOperationException("会话未初始化");

        var frame = session.Detect(roi);
        var result = new AoiResult
        {
            Success = frame.HandCount > 0,
            Confidence = frame.HandCount > 0 ? 1f : 0f,
            Message = frame.HandCount > 0 ? $"检测到 {frame.HandCount} 只手" : "未检测到手部",
        };

        result.Set("hand_landmarks", frame);
        result.Set("timestamp_ms", frame.TimestampMs);
        return result;
    }
}

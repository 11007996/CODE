namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>
/// 手部子进程 <c>luxvideopyplugin.cli.hand_worker</c> 的启动与推理选项（由 <see cref="HandLandmarkerSubprocessAoiDetector"/> / <see cref="HandLandmarkerSubprocessInferenceSession"/> 使用）。
/// </summary>
public sealed class HandLandmarkerPythonOptions
{
    public required string VenvRoot { get; init; }

    /// <summary>
    /// 可选。非空时追加到子进程 <c>PYTHONPATH</c>（如开发态源码根）。
    /// 留空表示依赖 venv 内已安装的 <c>luxvideopyplugin</c>（例如 <c>pip install *.whl</c>）。
    /// </summary>
    public string ExtraPythonPathRoot { get; init; } = "";

    /// <summary>可选；非空时设置 <c>LUXVIDEO_ROOT</c>（wheel 安装时解析资源路径等）。</summary>
    public string? LuxVideoRoot { get; init; }

    /// <summary>可选；非空时设置 <c>HAND_LANDMARKER_MODEL_PATH</c>。</summary>
    public string? HandLandmarkerModelPath { get; init; }

    /// <summary>可选；非空时作为子进程 Python 可执行文件路径（默认由 venv 解析）。</summary>
    public string? PythonExecutablePath { get; init; }

    public int FrameDeltaMs { get; init; } = HandLandmarkerInferenceDefaults.NominalFrameDeltaMsAt30FpsCap;

    public int NumHands { get; init; } = 2;

    /// <summary>大于 0 时按长边等比缩小后再推理。</summary>
    public int? InferenceMaxLongEdgePixels { get; init; }

    /// <summary>为 true 时将图像地标转为原图像素坐标。</summary>
    public bool MapImageLandmarksToOriginalPixels { get; init; }

    /// <summary>下采样时是否按比例缩放世界地标。</summary>
    public bool ScaleWorldLandmarksWhenDownsampled { get; init; } = true;
}

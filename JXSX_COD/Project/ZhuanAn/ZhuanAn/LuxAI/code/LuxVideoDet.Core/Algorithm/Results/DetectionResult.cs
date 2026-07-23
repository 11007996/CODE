using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.Inference.Results;

namespace LuxVideoDet.Core.Algorithm.Results;

/// <summary>
/// 算法检测结果
/// </summary>
public class DetectionResult
{
    /// <summary>原始帧</summary>
    public Frame? OriginalFrame { get; set; }

    /// <summary>标注后的帧</summary>
    public Frame? AnnotatedFrame { get; set; }

    /// <summary>检测结果列表</summary>
    public List<Detection> Detections { get; set; } = new();

    /// <summary>
    /// 最近约 60 秒滑动窗口内平均每帧管线耗时（毫秒），与左上角叠加口径一致。
    /// </summary>
    public float InferenceTime { get; set; }

    /// <summary>模型推理耗时（毫秒），仅 <c>Infer</c> 调用段（本帧）。</summary>
    public float ModelInferenceMs { get; set; }

    /// <summary>与 <see cref="ModelInferenceMs"/> 对应的引擎内细分（预处理 / Run / 输出到 CPU / 引擎解码等）。</summary>
    public InferenceTimingBreakdown? InferenceTiming { get; set; }

    /// <summary>后处理耗时（毫秒），<c>ProcessDetections</c> 整段（本帧）。</summary>
    public float PostProcessMs { get; set; }

    /// <summary>渲染耗时（毫秒），含区域与左上角管线叠加等（本帧）。</summary>
    public float RenderMs { get; set; }

    /// <summary>本帧整段管线耗时（毫秒）：模型 + 后处理 + 渲染。</summary>
    public float TotalTime { get; set; }

    /// <summary>最近约 60 秒滑动窗口内的平均 FPS（与画面左上角口径一致）。</summary>
    public double Fps { get; set; }

    /// <summary>当前状态</summary>
    public string CurrentState { get; set; } = string.Empty;

    /// <summary>状态消息</summary>
    public string StateMessage { get; set; } = string.Empty;

    /// <summary>是否触发通知</summary>
    public bool ShouldNotify { get; set; }

    /// <summary>通知消息</summary>
    public string? NotificationMessage { get; set; }

    /// <summary>
    /// 通知标题；<c>null</c> 表示使用算法类型名作为标题（兼容旧行为），空字符串表示仅朗读 <see cref="NotificationMessage"/>（适合 TTS）。
    /// </summary>
    public string? NotificationTitle { get; set; }

    /// <summary>通知级别</summary>
    public NotificationLevel NotificationLevel { get; set; } = NotificationLevel.Info;

    /// <summary>是否保存图像</summary>
    public bool ShouldSaveImage { get; set; }

    /// <summary>是否保存视频</summary>
    public bool ShouldSaveVideo { get; set; }

    /// <summary>
    /// 本帧的产品判定结果。
    /// None 表示本帧未产生判定（如中间帧）；OK/NG 表示算法给出了明确结论。
    /// </summary>
    public ProductionJudgement Judgement { get; set; } = ProductionJudgement.None;

    /// <summary>附加数据</summary>
    public Dictionary<string, object> ExtraData { get; set; } = new();

    /// <summary>时间戳</summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

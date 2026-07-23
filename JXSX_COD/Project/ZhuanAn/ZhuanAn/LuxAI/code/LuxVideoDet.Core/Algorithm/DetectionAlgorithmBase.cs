using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Inference.Results;
using LuxVideoDet.Core.Notification;
using LuxVideoDet.Core.Region;
using LuxVideoDet.Core.Rendering;
using LuxVideoDet.Core.Storage;
using LuxVideoDet.Core.Tracking;
using LuxVideoDet.Core.Utils;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 检测算法基类 — 模板方法模式。
/// <para>
/// 子类实现 <see cref="ProcessDetections"/>；可选覆盖 <see cref="GetDetectionClassBindings"/>，
/// 在 <see cref="Initialize"/> 中于配色前统一完成「业务键 ↔ 模型类名 ↔ ClassId」与 <see cref="DetectionLabelsByClassId"/>。
/// </para>
/// </summary>
public abstract class DetectionAlgorithmBase : IDetectionAlgorithm
{
    protected readonly IInferenceEngine _inferenceEngine;
    protected readonly RegionManager _regionManager;
    protected readonly List<INotificationService> _notifiers;
    protected readonly ILogger _logger;

    /// <summary>当前算法的完整配置（Initialize 后可用）</summary>
    protected AlgorithmConfig _config = null!;

    protected StorageManager? _storageManager;
    protected RecordingManager? _recordingManager;
    protected VideoRecorder? _videoRecorder;

    /// <summary>
    /// 可选：在子类 <see cref="OnInitialize"/> 中赋值。键为 <see cref="Inference.Results.Detection.ClassId"/>（模型输出的类别索引），
    /// 值为画面角标应显示的业务名（与 <see cref="ClassIdsResolution"/> / 常量表一致）。未设置或未命中时，<see cref="FormatDetectionLabel"/> 退回引擎 <see cref="Inference.Results.Detection.ClassName"/>。
    /// 若子类提供 <see cref="GetDetectionClassBindings"/>，基类会在初始化早期赋值本属性。
    /// </summary>
    protected IReadOnlyDictionary<int, string>? DetectionLabelsByClassId { get; set; }

    /// <summary>
    /// 当 <see cref="GetDetectionClassBindings"/> 返回非空时，由基类在 <see cref="InitializeColors"/> 之前填充：
    /// 业务键（<see cref="AlgorithmDetectionClassBinding.LogicalName"/>）→ 模型 <see cref="Inference.Results.Detection.ClassId"/>。
    /// </summary>
    protected IReadOnlyDictionary<string, int>? ResolvedDetectionClassIds { get; private set; }

    private List<string>? _semanticOrderFromBindings;

    /// <summary>约 60 秒滑动窗口：平均 FPS 与平均每帧管线耗时。</summary>
    protected readonly RollingMinutePipelineStats _pipelineMinuteStats = new();

    /// <summary>约 60 秒滑动窗口：推理引擎各分项平均耗时（毫秒）。</summary>
    private readonly RollingMinuteInferenceStepStats _inferenceMinuteStats = new();

    /// <summary><see cref="ModelType.DetectionTracking"/> 或 <see cref="ModelType.SegmentationTracking"/> 时用于跨帧 ID 关联（仅用框与类别，掩膜不参与）。</summary>
    private MultiObjectTracker? _multiObjectTracker;

    /// <summary>视频源标称帧率（由流水线注入）；展示用 fps 取 min(管线吞吐, 该值)，避免「推理很快」时显示超过源帧率。</summary>
    private double? _sourceVideoFpsDisplayCap;

    protected Dictionary<string, CooldownTimer> _cooldownTimers = new();
    protected Dictionary<int, Scalar> _classColors = new();
    protected Scalar _defaultColor = new Scalar(255, 255, 255);

    private int _frameCount;
    private DateTime _lastPerfLogTime = DateTime.MinValue;
    private const int PerfLogIntervalSeconds = 30;

    public abstract string AlgorithmType { get; }
    public bool IsInitialized { get; protected set; }
    public RenderOptions RenderOptions { get; } = new();

    /// <summary>
    /// 当前加载模型对应的视觉任务类型（<see cref="Initialize"/> 成功后可用）。
    /// 用于按任务选择绘制方式（检测 / 分割 / 姿态 / OBB / 分类等）。
    /// </summary>
    public ModelType ModelTaskType { get; protected set; } = ModelType.Detection;

    protected DetectionAlgorithmBase(
        IInferenceEngine inferenceEngine,
        RegionManager regionManager,
        List<INotificationService> notifiers,
        ILogger logger)
    {
        _inferenceEngine = inferenceEngine;
        _regionManager = regionManager;
        _notifiers = notifiers;
        _logger = logger;
    }

    public string GetEngineType() => _inferenceEngine?.EngineType ?? "Unknown";
    public string GetDeviceType() => _inferenceEngine?.DeviceType ?? "Unknown";

    public virtual List<RegionDefinition> GetRequiredRegions() => new();
    public virtual List<string> GetDefaultClasses() => new();

    // ──────────── 初始化 ────────────

    public virtual void Initialize(AlgorithmConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));

        try
        {
            InitializeDetectionClassBindings();
            InitializeColors();
            InitializeStorage();
            OnInitialize();

            var info = _inferenceEngine.GetModelInfo();
            ModelTaskType = info.Type == ModelType.Auto ? ModelType.Detection : info.Type;

            InitializeMultiObjectTracking();

            IsInitialized = true;
            _logger.LogInformation("[算法] {AlgorithmType} Initialize 完成", AlgorithmType);

            if (_notifiers.Count > 0)
            {
                var types = string.Join(", ", _notifiers.Select(n => n.NotificationType));
                _logger.LogInformation("[算法·通知] 本算法已加载 {Count} 个渠道: {Types}", _notifiers.Count, types);
            }
            else
            {
                _logger.LogInformation(
                    "[算法·通知] 未加载任何通知渠道（检查配置 notification.enabled、各 notifier.enabled，或启动日志中的 [配置·通知] 初始化失败）");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[算法] {AlgorithmType} Initialize 失败", AlgorithmType);
            throw;
        }
    }

    /// <summary>子类额外初始化（状态机、冷却计时器等）。</summary>
    protected virtual void OnInitialize() { }

    /// <summary>
    /// 声明本算法<strong>实际使用</strong>的业务类别：每条为「业务键 + 与训练/metadata 一致的模型类名 + 画面展示名」。
    /// 基类在 <see cref="InitializeColors"/> 之前调用 <see cref="ClassIdsResolution.Resolve"/>，填充
    /// <see cref="ResolvedDetectionClassIds"/> 与 <see cref="DetectionLabelsByClassId"/>（ClassId→展示名），
    /// 并将 <see cref="AlgorithmDetectionClassBinding.ModelClassName"/> 顺序用于语义配色（见 <see cref="GetSemanticOrderForClassColors"/>）。
    /// <para>
    /// 模型中可存在<strong>未在此声明</strong>的额外类别或顺序与声明不一致，均不影响；仅当某条声明的
    /// <see cref="AlgorithmDetectionClassBinding.ModelClassName"/> 在引擎类别映射中<strong>找不到</strong>对应 ClassId 时，
    /// <see cref="ClassIdsResolution.Resolve"/> 会在初始化阶段抛错。
    /// </para>
    /// </summary>
    /// <returns>返回 <c>null</c> 或空集合时不解析；帧内请改用 <see cref="RequireResolvedClassId"/> 的子类须先提供非空绑定。</returns>
    protected virtual IReadOnlyList<AlgorithmDetectionClassBinding>? GetDetectionClassBindings() => null;

    private void InitializeDetectionClassBindings()
    {
        ResolvedDetectionClassIds = null;
        _semanticOrderFromBindings = null;

        var bindings = GetDetectionClassBindings();
        if (bindings == null || bindings.Count == 0)
            return;

        var entries = bindings.Select(static b => (b.LogicalName, b.ModelClassName));
        var map = ClassIdsResolution.Resolve(_inferenceEngine, _logger, entries, AlgorithmType);
        ResolvedDetectionClassIds = new Dictionary<string, int>(map, StringComparer.Ordinal);

        var labelById = new Dictionary<int, string>();
        foreach (var b in bindings)
        {
            if (!map.TryGetValue(b.LogicalName, out var id))
                continue;
            if (!labelById.ContainsKey(id))
                labelById[id] = b.DisplayLabel;
        }

        DetectionLabelsByClassId = labelById;

        _semanticOrderFromBindings = new List<string>(bindings.Count);
        foreach (var b in bindings)
            _semanticOrderFromBindings.Add(b.ModelClassName);
    }

    /// <summary>
    /// 在绑定阶段成功后，按业务键取已解析的 ClassId。子类无需再各自实现「字典 → 强类型结构体」。
    /// </summary>
    /// <exception cref="InvalidOperationException">未提供 <see cref="GetDetectionClassBindings"/> 或映射表中无此 <paramref name="logicalName"/>。</exception>
    protected int RequireResolvedClassId(string logicalName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(logicalName);

        if (ResolvedDetectionClassIds == null)
        {
            throw new InvalidOperationException(
                $"算法 \"{AlgorithmType}\" 未建立检测类别映射：请覆盖 {nameof(GetDetectionClassBindings)} 并返回至少一条 {nameof(AlgorithmDetectionClassBinding)}。");
        }

        if (!ResolvedDetectionClassIds.TryGetValue(logicalName, out var id))
        {
            throw new InvalidOperationException(
                $"算法 \"{AlgorithmType}\" 的业务键 \"{logicalName}\" 未在 {nameof(GetDetectionClassBindings)} 中声明，或键名与绑定不一致。");
        }

        return id;
    }

    /// <summary>按业务键尝试取 ClassId；未绑定或未包含该键时返回 <c>false</c>。</summary>
    protected bool TryGetResolvedClassId(string logicalName, out int classId)
    {
        classId = default;
        return ResolvedDetectionClassIds != null
               && !string.IsNullOrWhiteSpace(logicalName)
               && ResolvedDetectionClassIds.TryGetValue(logicalName, out classId);
    }

    /// <summary>
    /// 由多算法流水线在视频源就绪后调用。
    /// <see cref="RollingMinutePipelineStats"/> 按管线耗时换算的 fps 表示推理吞吐，可能与源实际送帧率无关；
    /// 本上限使画面与 API 中的「fps」与源帧率一致（源未知或 0 时不限制）。
    /// </summary>
    public void SetSourceVideoFpsForDisplay(double fps)
    {
        _sourceVideoFpsDisplayCap = fps > 0.01 ? fps : null;
    }

    private double CapDisplayFps(double pipelineThroughputFps) =>
        _sourceVideoFpsDisplayCap is double cap ? Math.Min(pipelineThroughputFps, cap) : pipelineThroughputFps;

    /// <summary>
    /// 在解析类别配色前调用；若语义类名依赖 <c>args</c> 等，请在此解析（如 SafetyPpeWork）。
    /// </summary>
    protected virtual void OnBeforeInitializeColors() { }

    /// <summary>
    /// 语义着色时的模型类名顺序，与 <c>classColors</c> 及 Descriptor <c>DefaultClassColors</c> 下标对齐。
    /// 若已实现 <see cref="GetDetectionClassBindings"/>，默认按绑定中的 <see cref="AlgorithmDetectionClassBinding.ModelClassName"/> 顺序与 Descriptor 语义色下标对齐；
    /// 否则与 <see cref="DetectionAlgorithmFactory.GetDefaultClasses"/>（Descriptor 的 <c>DefaultClasses</c>）一致；
    /// 返回 <c>null</c> 或空集合则按引擎类别**下标**配色（与旧版基类行为一致）。
    /// </summary>
    protected virtual IReadOnlyList<string>? GetSemanticOrderForClassColors()
    {
        if (_semanticOrderFromBindings is { Count: > 0 })
            return _semanticOrderFromBindings;

        var list = DetectionAlgorithmFactory.GetDefaultClasses(AlgorithmType);
        return list.Count > 0 ? list : null;
    }

    /// <summary>
    /// 默认：若 <see cref="GetSemanticOrderForClassColors"/> 非空则按类名匹配语义下标填色，否则按下标填色。
    /// </summary>
    protected virtual void InitializeColors() => ApplyClassColors();

    /// <summary>
    /// 重新应用当前配置下的类别配色（如 Reset 后类名/配置变更）。会先执行 <see cref="OnBeforeInitializeColors"/>。
    /// </summary>
    protected void ReapplyClassColors() => ApplyClassColors();

    private void ApplyClassColors()
    {
        OnBeforeInitializeColors();

        var classNames = _inferenceEngine.GetClassNames();
        if (classNames.Count == 0)
            return;

        var semanticOrder = GetSemanticOrderForClassColors();
        if (semanticOrder == null || semanticOrder.Count == 0)
        {
            FillClassColorsByModelClassIndex(classNames);
            return;
        }

        SemanticOrderClassColors.FillClassColorsBySemanticName(
            _classColors,
            classNames,
            semanticOrder,
            _config.ClassColors,
            DetectionAlgorithmFactory.GetDefaultClassColors(AlgorithmType),
            _logger);
    }

    /// <summary>按下标 <c>i</c> 合并 config / Descriptor / 调色板（模型类别顺序与配置下标对齐）。</summary>
    private void FillClassColorsByModelClassIndex(IReadOnlyList<string> classNames)
    {
        var palette = DrawingHelper.GenerateColorPalette(classNames.Count);
        var descriptorColors = DetectionAlgorithmFactory.GetDefaultClassColors(AlgorithmType);
        var configColors = _config.ClassColors;

        for (int i = 0; i < classNames.Count; i++)
        {
            string? hex = null;
            if (configColors != null && i < configColors.Count)
                hex = configColors[i];

            if (VisualizationColors.TryParseRgbHexToBgrScalar(hex, out var fromConfig))
            {
                _classColors[i] = fromConfig;
                continue;
            }

            if (descriptorColors != null && i < descriptorColors.Count)
                _classColors[i] = descriptorColors[i];
            else
                _classColors[i] = palette[i];
        }
    }

    /// <summary>
    /// 根据 config.Storage 创建 StorageManager / VideoRecorder / RecordingManager。
    /// </summary>
    private void InitializeStorage()
    {
        var storage = _config.Storage;
        if (storage == null) return;

        var machineName = !string.IsNullOrWhiteSpace(_config.ParentConfigName)
            ? _config.ParentConfigName
            : !string.IsNullOrWhiteSpace(_config.DisplayName)
                ? _config.DisplayName
                : AlgorithmType;

        if (storage.SaveErrorImage || storage.SaveRetrainImage || storage.SaveVideo)
        {
            _storageManager = new StorageManager(
                machineName, _logger,
                storage.ErrorImagePath,
                storage.RetrainImagePath);

            if (storage.SaveVideo && _storageManager != null)
            {
                var halfDuration = Math.Max(storage.VideoDuration / 2.0, 1.0);

                _videoRecorder = new VideoRecorder(
                    _logger,
                    bufferSeconds: halfDuration,
                    postBufferSeconds: halfDuration,
                    recordingMaxWidth: storage.RecordingMaxWidth,
                    ngVideoUseSourceResolution: storage.NgVideoUseSourceResolution,
                    maxConcurrentRecordings: storage.MaxConcurrentRecordings);

                _recordingManager = new RecordingManager(
                    _videoRecorder, _storageManager, _logger,
                    storage.VideoCodec);

                _recordingManager.StartBackgroundSaver();

                var recW = storage.NgVideoUseSourceResolution
                    ? "源分辨率"
                    : (storage.RecordingMaxWidth <= 0 ? "自动(源>2560则2K)" : storage.RecordingMaxWidth.ToString());
                _logger.LogInformation(
                    "[算法·存储] {AlgorithmType} | 错误图={SaveError} 重训图={SaveRetrain} 视频=开 | 片段总时长约 {Duration}s 输出帧率=实况 编码={Codec} | 录像宽={RecW} 并发录制≤{RecConcurrent}",
                    AlgorithmType,
                    storage.SaveErrorImage, storage.SaveRetrainImage,
                    storage.VideoDuration, storage.VideoCodec,
                    recW,
                    storage.MaxConcurrentRecordings);
            }
            else
            {
                _logger.LogInformation(
                    "[算法·存储] {AlgorithmType} | 错误图={SaveError} 重训图={SaveRetrain} 视频=关",
                    AlgorithmType, storage.SaveErrorImage, storage.SaveRetrainImage);
            }
        }
    }

    // ──────────── 帧处理 ────────────

    protected virtual Scalar GetDetectionColor(Detection detection, Mat frame)
    {
        return _classColors.GetValueOrDefault(detection.ClassId, _defaultColor);
    }

    /// <summary>
    /// 检测框角标主文案（不含置信度；若存在 <see cref="Detection.TrackId"/>，绘制时在首位显示 <c>#id</c>）。
    /// 优先 <see cref="DetectionLabelsByClassId"/>[<see cref="Detection.ClassId"/>]；
    /// 否则退回引擎 <see cref="Detection.ClassName"/>。子类可覆写以追加规则。
    /// </summary>
    protected virtual string FormatDetectionLabel(Detection detection)
    {
        string label;
        if (DetectionLabelsByClassId != null &&
            DetectionLabelsByClassId.TryGetValue(detection.ClassId, out var text) &&
            !string.IsNullOrEmpty(text))
            label = text;
        else
            label = detection.ClassName;
        return label;
    }

    /// <summary>
    /// 构造单帧绘制检测框的选项。使用 <c>d =&gt; FormatDetectionLabel(d)</c> 以保证虚方法分发。
    /// </summary>
    protected DetectionDrawOptions CreateDetectionDrawOptions(int boxThickness = 2) =>
        new()
        {
            BoxThickness = boxThickness,
            ShowLabel = RenderOptions.ShowLabels,
            LabelFormatter = d => FormatDetectionLabel(d)
        };

    /// <summary>
    /// 在画面上绘制检测结果框：颜色由 <see cref="GetDetectionColor"/>，标签由 <see cref="FormatDetectionLabel"/>。
    /// </summary>
    protected void DrawTaskDetections(Mat frame, IReadOnlyList<Detection> detections, int boxThickness = 2)
    {
        if (!RenderOptions.ShowDetectionBoxes)
            return;

        var opts = CreateDetectionDrawOptions(boxThickness);
        foreach (var detection in detections)
        {
            var color = GetDetectionColor(detection, frame);
            TaskAwareDetectionRenderer.Draw(frame, detection, color, ModelTaskType, opts);
        }
    }

    public Results.DetectionResult Process(Frame frame)
    {
        if (!IsInitialized)
            throw new InvalidOperationException($"算法 {AlgorithmType} 未初始化");

        _frameCount++;

        // 叠加文字使用「本帧入库前」的统计，避免同帧自引用。
        var (displayAvgFps, displayAvgPipelineMs) = _pipelineMinuteStats.GetAverages();
        var displayFpsForOverlay = CapDisplayFps(displayAvgFps);

        // 先推理再 Clone 画板：缩短进入模型前的等待（Clone 与 RTSP/实时预览的跳帧策略无关）。
        var sw = Stopwatch.StartNew();
        var inferenceResult = _inferenceEngine.Infer(frame.Mat);
        if (inferenceResult.Timing != null)
            _inferenceMinuteStats.RecordFrame(inferenceResult.Timing);

        _multiObjectTracker?.Update(inferenceResult.Detections);

        var modelMs = (float)sw.Elapsed.TotalMilliseconds;
        sw.Restart();

        var annotatedFrame = frame.Mat.Clone();
        sw.Restart();

        var result = new Results.DetectionResult
        {
            OriginalFrame = frame,
            Detections = inferenceResult.Detections,
            Timestamp = DateTime.Now
        };

        result = ProcessDetections(frame.Mat, annotatedFrame, inferenceResult.Detections, result);
        var postMs = (float)sw.Elapsed.TotalMilliseconds;
        sw.Restart();

        result.AnnotatedFrame = new Frame(annotatedFrame);
        result.InferenceTiming = inferenceResult.Timing;
        DrawCommonInfo(annotatedFrame, result, displayFpsForOverlay, (float)displayAvgPipelineMs);
        var renderMs = (float)sw.Elapsed.TotalMilliseconds;

        var totalPipelineMs = modelMs + postMs + renderMs;
        result.ModelInferenceMs = modelMs;
        result.PostProcessMs = postMs;
        result.RenderMs = renderMs;
        result.TotalTime = totalPipelineMs;

        _pipelineMinuteStats.RecordFrame(totalPipelineMs);

        var (avgFps, avgPipelineMs) = _pipelineMinuteStats.GetAverages();
        var cappedDisplayFps = CapDisplayFps(avgFps);
        result.Fps = cappedDisplayFps;
        result.InferenceTime = (float)avgPipelineMs;

        if (_videoRecorder != null)
            _videoRecorder.AddFrame(frame.Mat, annotatedFrame);

        // 约 30s 一条：英文、仅 rolling 60s 均值（无单帧分项）。
        if ((DateTime.Now - _lastPerfLogTime).TotalSeconds >= PerfLogIntervalSeconds)
        {
            _lastPerfLogTime = DateTime.Now;
            var extra = BuildMinutePerfLogExtraSuffix();
            var extraPart = string.IsNullOrEmpty(extra) ? string.Empty : " | " + extra;
            var inf = _inferenceMinuteStats.GetAverages();
            if (inf != null)
            {
                var s = inf.Value;
                _logger.LogInformation(
                    "[Algorithm.Perf] {AlgorithmType} | last 60s: {AvgFps:F1} fps, avg end-to-end pipeline {AvgPipelineMs:F1} ms | inference step averages (ms): preprocess {Pre:F2}, input_tensor {InT:F2}, native_run {Run:F2}, output_to_host {Out:F2}, engine_postprocess {Dec:F2}, breakdown_sum {Sum:F2} | inference_samples {InfSamples} | #{FrameCount}{Extra}",
                    AlgorithmType, cappedDisplayFps, avgPipelineMs,
                    s.AvgPreprocessMs, s.AvgInputTensorMs, s.AvgNativeRunMs, s.AvgOutputToCpuMs, s.AvgEnginePostprocessMs, s.AvgBreakdownSumMs,
                    s.SampleCount, _frameCount, extraPart);
            }
            else
            {
                _logger.LogInformation(
                    "[Algorithm.Perf] {AlgorithmType} | last 60s: {AvgFps:F1} fps, avg end-to-end pipeline {AvgPipelineMs:F1} ms | inference step averages (ms): not available | #{FrameCount}{Extra}",
                    AlgorithmType, cappedDisplayFps, avgPipelineMs, _frameCount, extraPart);
            }
        }

        if (result.ShouldNotify && !string.IsNullOrEmpty(result.NotificationMessage))
        {
            var task = SendNotificationAsync(result);
            _ = task.ContinueWith(
                t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                        _logger.LogError(t.Exception.GetBaseException(), "[算法·通知] 发送任务异常（未捕获）");
                },
                TaskContinuationOptions.OnlyOnFaulted);
        }

        return result;
    }

    /// <summary>子类实现具体的检测业务逻辑。</summary>
    protected abstract Results.DetectionResult ProcessDetections(
        Mat rawFrame,
        Mat annotatedFrame,
        List<Detection> detections,
        Results.DetectionResult result);

    // ──────────── 渲染 ────────────

    /// <param name="displayAvgFps">叠加用：上一窗口统计（不含本帧）。</param>
    /// <param name="displayAvgPipelineMs">叠加用：上一窗口统计（不含本帧）。</param>
    protected virtual void DrawCommonInfo(Mat frame, Results.DetectionResult result,
        double displayAvgFps, float displayAvgPipelineMs)
    {
        DrawingHelper.DrawPipelineMinuteStats(frame, displayAvgFps, displayAvgPipelineMs);
        if (RenderOptions.ShowRegions)
        {
            RegionRenderer.DrawRegions(frame, _regionManager.GetAllRegions());
        }
    }

    /// <summary>
    /// 追加到约 30s 一次的 <c>[Algorithm.Perf]</c> 日志末尾（子类如含手部 AOI 可覆写）。</summary>
    protected virtual string? BuildMinutePerfLogExtraSuffix() => null;

    /// <inheritdoc />
    /// <remarks>
    /// 轻量级实现：仅绘制区域轮廓（Cv2.Polylines），不绘制区域名称和 FPS。
    /// 用于多算法合成场景，避免重复调用 ChineseTextRenderer 造成性能瓶颈。
    /// </remarks>
    public virtual void RenderAnnotations(Mat frame, Results.DetectionResult result, int yOffset = 0)
    {
        if (RenderOptions.ShowRegions)
        {
            foreach (var region in _regionManager.GetAllRegions())
                RegionRenderer.DrawRegion(frame, region);
        }
    }

    // ──────────── 通知 ────────────

    protected async Task SendNotificationAsync(Results.DetectionResult result)
    {
        if (_notifiers.Count == 0)
        {
            _logger.LogWarning(
                "[算法·通知] 本帧应发通知（ShouldNotify），但当前实例未挂载任何通知渠道。" +
                "若已在界面勾选启用 PLC/Webhook 等，通常是因为该渠道**启动时初始化失败**（例如 PLC 未填写 plcIp、Webhook 未填 URL），渠道被跳过。" +
                "请**向上滚动**查找会话启动阶段的 [配置·通知] 日志（含「未加入列表」与异常原因），修正配置后**重新启动会话**。");
            return;
        }

        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        var message = new NotificationMessage
        {
            Title = result.NotificationTitle is null ? AlgorithmType : result.NotificationTitle,
            Content = result.NotificationMessage ?? string.Empty,
            Level = result.NotificationLevel.ToString(),
            AlgorithmType = AlgorithmType,
            ExtraData = result.ExtraData
        };

        _logger.LogInformation(
            "[算法·通知] 开始发送 | 渠道={Types} | 级别={Level} | 摘要={Preview}",
            string.Join(", ", _notifiers.Select(n => n.NotificationType)),
            message.Level,
            TruncateForLog(message.Content, 120));

        foreach (var notifier in _notifiers)
        {
            try
            {
                var success = await notifier.SendAsync(message);
                if (success)
                    _logger.LogInformation("[算法·通知] [{Timestamp}] {Type} 处理完毕", timestamp, notifier.NotificationType);
                else
                    _logger.LogWarning("[算法·通知] {Type} 处理返回失败（如 PLC 连接/写入未成功）", notifier.NotificationType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[算法·通知] {Type} 发送异常", notifier.NotificationType);
            }
        }
    }

    private static string TruncateForLog(string? text, int maxLen)
    {
        if (string.IsNullOrEmpty(text))
            return "";
        var t = text.Trim();
        return t.Length <= maxLen ? t : t.Substring(0, maxLen) + "…";
    }

    // ──────────── 存储 ────────────

    protected async Task SaveErrorImageAsync(Mat renderFrame, string? timestamp = null)
    {
        if (_storageManager == null) return;
        // StorageManager 在「重训图/录像」任一为真时也会创建，须显式尊重 SaveErrorImage
        if (_config.Storage is not { SaveErrorImage: true }) return;

        await Task.Run(() =>
        {
            try
            {
                var errorPath = _storageManager.SaveErrorImage(renderFrame, timestamp);
                if (errorPath != null)
                {
                    _logger.LogError("[算法·存储] [{Timestamp}] 错误截图已写入: {Path}",
                        timestamp ?? DateTime.Now.ToString("yyyyMMddHHmmss"), errorPath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[算法·存储] 保存错误截图失败");
            }
        });
    }

    protected string? TriggerVideoRecording(string? timestamp = null)
    {
        return _recordingManager?.TriggerAndSave(timestamp);
    }

    // ──────────── 生命周期 ────────────

    public abstract void Reset();
    public abstract string GetCurrentState();

    /// <summary>
    /// 重置生产统计数据（OK/NG计数归零）
    /// </summary>
    public virtual void ResetStatistics() { }

    /// <summary>重置 60 秒滑动窗口统计（状态机复位等场景与基类 <see cref="Reset"/> 同步调用）。</summary>
    protected void ResetMinutePipelineStats()
    {
        _pipelineMinuteStats.Reset();
        _inferenceMinuteStats.Reset();
        _multiObjectTracker?.Reset();
    }

    /// <summary>
    /// <see cref="ModelType.DetectionTracking"/>（检测解码 + MOT）或 <see cref="ModelType.SegmentationTracking"/>（实例分割解码 + MOT）时创建 MOT，
    /// 仅用 <see cref="Inference.Results.Detection.BoundingBox"/> 与 <see cref="Inference.Results.Detection.ClassId"/>；
    /// 纯 <see cref="ModelType.Segmentation"/>、预留的 <see cref="ModelType.Track"/>（原生追踪）等不启用 MOT。其余任务清空跟踪状态。
    /// </summary>
    private void InitializeMultiObjectTracking()
    {
        _multiObjectTracker?.Reset();
        _multiObjectTracker = null;
        if (ModelTaskType is not ModelType.DetectionTracking and not ModelType.SegmentationTracking)
            return;

        var tc = _config.Tracking ?? new TrackingConfig();
        _multiObjectTracker = new MultiObjectTracker(tc);
        var mode = ModelTaskType == ModelType.SegmentationTracking ? "Track (Segment)+MOT" : "Track (Detect)+MOT";
        _logger.LogInformation(
            "[算法·跟踪] {AlgorithmType} {Mode} | IoU≥{Iou:F2} 丢轨≤{Miss}帧 MinHits={Hits} 每类 TrackId 0～{MaxId}",
            AlgorithmType,
            mode,
            tc.TrackIouThreshold,
            tc.MaxMissedFrames,
            tc.MinHits,
            tc.MaxTrackId);
    }

    public virtual void Dispose()
    {
        _recordingManager?.Dispose();
        _videoRecorder?.Dispose();
        _inferenceEngine?.Dispose();
        GC.SuppressFinalize(this);
    }
}

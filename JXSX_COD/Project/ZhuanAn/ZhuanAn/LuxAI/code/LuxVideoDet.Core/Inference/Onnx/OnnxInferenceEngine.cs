using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Inference.Postprocessors;
using LuxVideoDet.Core.Inference.Results;
using Microsoft.Extensions.Logging;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using OpenCvSharp;

namespace LuxVideoDet.Core.Inference.Onnx;

/// <summary>
/// ONNX Runtime 推理引擎 - 支持多种 YOLO 任务类型
/// </summary>
public class OnnxInferenceEngine : IInferenceEngine
{
    private readonly InferenceConfig _config;
    private readonly ILogger<OnnxInferenceEngine> _logger;
    private readonly PostprocessorFactory _postprocessorFactory;
    private InferenceSession? _session;
    private IPostprocessor? _postprocessor;
    private bool _isLoaded;

    private int _inputWidth = 640;
    private int _inputHeight = 640;
    private int _numClasses;
    private int _numAnchors;
    private int _numAttributes;
    private ModelType _detectedTaskType = ModelType.Detection;
    private TensorElementType _inputElementType = TensorElementType.Float;
    private string _inputName = "images";

    /// <summary>从 ONNX 元数据解析出的类别名称（按模型索引排序）</summary>
    private List<string>? _modelClassNames;
    /// <summary>类别名称→模型索引映射</summary>
    private Dictionary<string, int> _classIndexMap = new(StringComparer.OrdinalIgnoreCase);
    /// <summary>最终使用的类别列表（优先从模型元数据，其次配置）</summary>
    private List<string> _resolvedClassNames = new();

    public string EngineType => "ONNX Runtime";
    public string DeviceType { get; private set; } = "CPU";
    public bool IsLoaded => _isLoaded;

    public OnnxInferenceEngine(
        InferenceConfig config,
        ILogger<OnnxInferenceEngine> logger,
        PostprocessorFactory postprocessorFactory)
    {
        _config = config;
        _logger = logger;
        _postprocessorFactory = postprocessorFactory;
    }

    public async Task LoadModelAsync(string modelPath, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("[模型·加载] 开始载入 ONNX: {ModelPath}", modelPath);

            if (!File.Exists(modelPath))
            {
                throw new FileNotFoundException($"模型文件不存在: {modelPath}");
            }

            await Task.Run(() =>
            {
                var options = new SessionOptions();
                ConfigureDevice(options);

                _session = new InferenceSession(modelPath, options);

                DetectInputSize();
                ReadModelMetadata();
                DetectModelStructure();
                DetectElementTypes();
                ResolveClassNames();

                _postprocessor = _postprocessorFactory.GetPostprocessor(_detectedTaskType);

                _isLoaded = true;

                _logger.LogInformation(
                    "[推理运行时] {Runtime} | 输入={Width}x{Height} | 任务={TaskType} | 类别数={Classes} | 输入精度={Precision}",
                    $"{EngineType} + {DeviceType}",
                    _inputWidth,
                    _inputHeight,
                    ModelTypeYoloLabels.Format(_detectedTaskType),
                    _numClasses,
                    _inputElementType);

            }, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[模型·加载] 失败: {ModelPath}", modelPath);
            throw;
        }
    }

    public async Task<InferenceResult> InferAsync(
        Frame frame,
        float confidenceThreshold,
        float iouThreshold,
        CancellationToken cancellationToken = default)
    {
        if (!_isLoaded || _session == null || _postprocessor == null)
        {
            throw new InvalidOperationException("模型未加载");
        }

        var stopwatch = Stopwatch.StartNew();
        var timing = new InferenceTimingBreakdown();

        try
        {
            var sw = Stopwatch.StartNew();
            var (fp32Data, shape, ratio, padW, padH) = await PreprocessImageAsync(frame, cancellationToken);
            timing.PreprocessMs = (float)sw.Elapsed.TotalMilliseconds;

            var outputs = await Task.Run(() =>
            {
                sw.Restart();
                var inputs = new List<NamedOnnxValue> { CreateInputValue(_inputName, fp32Data, shape) };
                timing.InputTensorMs = (float)sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                using var results = _session!.Run(inputs);
                timing.NativeRunMs = (float)sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                var outputList = new List<float[]>();
                foreach (var result in results)
                    outputList.Add(ReadOutputAsFloatArray(result));
                timing.OutputToCpuMs = (float)sw.Elapsed.TotalMilliseconds;

                return outputList.ToArray();
            }, cancellationToken);

            // 后处理（使用 Postprocessor）
            var context = new PostprocessContext
            {
                OriginalWidth = frame.Width,
                OriginalHeight = frame.Height,
                Ratio = ratio,
                PadW = padW,
                PadH = padH,
                ConfThreshold = confidenceThreshold,
                IouThreshold = iouThreshold,
                ClassNames = _resolvedClassNames,
                NumClasses = _numClasses,
                NumAnchors = _numAnchors,
                NumAttributes = _numAttributes
            };

            sw.Restart();
            var detections = _postprocessor.Process(outputs, context);
            timing.EnginePostprocessMs = (float)sw.Elapsed.TotalMilliseconds;

            stopwatch.Stop();

            _logger.LogDebug(
                "推理完成 InferenceTime={Time}ms, Detections={Count}, 细分 预处理={Pre:F1} 输入张量={In:F1} Run={Run:F1} 输出到CPU={Out:F1} 引擎后处理={Ep:F1}ms",
                stopwatch.ElapsedMilliseconds, detections.Count,
                timing.PreprocessMs, timing.InputTensorMs, timing.NativeRunMs, timing.OutputToCpuMs,
                timing.EnginePostprocessMs);

            return new InferenceResult
            {
                InferenceTime = (float)stopwatch.Elapsed.TotalMilliseconds,
                Timing = timing,
                Detections = detections
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[模型] 单帧推理失败");
            throw;
        }
    }

    public async Task<InferenceResult[]> InferBatchAsync(
        Frame[] frames,
        float confidenceThreshold,
        float iouThreshold,
        CancellationToken cancellationToken = default)
    {
        var results = new List<InferenceResult>();

        foreach (var frame in frames)
        {
            var result = await InferAsync(frame, confidenceThreshold, iouThreshold, cancellationToken);
            results.Add(result);
        }

        return results.ToArray();
    }

    public ModelInfo GetModelInfo()
    {
        return new ModelInfo
        {
            Name = Path.GetFileName(_config.ModelPath),
            Type = _detectedTaskType,
            InputSize = (_inputWidth, _inputHeight),
            ClassCount = _numClasses,
            ClassNames = _resolvedClassNames.ToArray()
        };
    }

    private void ConfigureDevice(SessionOptions options)
    {
        var device = _config.Device;

        // 限制推理线程数，避免多引擎并行时 CPU 线程争抢导致性能崩溃
        if (_config.ThreadCount > 0)
        {
            options.IntraOpNumThreads = _config.ThreadCount;
            options.InterOpNumThreads = Math.Max(1, _config.ThreadCount / 2);
            _logger.LogDebug(
                "[模型·设备] 推理线程: IntraOp={IntraOp}, InterOp={InterOp}",
                options.IntraOpNumThreads, options.InterOpNumThreads);
        }

        switch (device)
        {
            case InferenceDevice.GPU:
                options.AppendExecutionProvider_CUDA(0);
                DeviceType = "CUDA";
                break;

            case InferenceDevice.TensorRT:
#if ORT_GPU
                if (!OperatingSystem.IsWindows() && !OperatingSystem.IsLinux())
                {
                    throw new InvalidOperationException(
                        "TensorRT 执行提供器仅支持 Windows / Linux。当前平台无法使用，请改用 CPU、GPU（CUDA）或其他设备。");
                }

                // 仅注册 TensorRT EP，不追加 CUDA：环境缺失或库加载失败时应直接失败，便于验证；需要 CUDA 回退时请改用 Device=GPU。
                {
                    using var trtOpts = new OrtTensorRTProviderOptions();
                    var trtDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        ["device_id"] = "0",
                        ["trt_fp16_enable"] = "1",
                        ["trt_engine_cache_enable"] = "1",
                    };
                    trtOpts.UpdateOptions(trtDict);
                    options.AppendExecutionProvider_Tensorrt(trtOpts);
                    DeviceType = "TensorRT";
                }

                break;
#else
                throw new InvalidOperationException(
                    "TensorRT 需要引用 Microsoft.ML.OnnxRuntime.Gpu。请使用：dotnet build -p:UseTensorRT=true（或 -p:UseCUDA=true）后再运行。");
#endif

            case InferenceDevice.QNN:
            {
                var qnnOptions = new Dictionary<string, string>
                {
                    ["backend_path"] = OperatingSystem.IsLinux() ? "libQnnHtp.so" : "QnnHtp.dll",
                    ["qnn_soc_model"] = "77",
                    ["htp_performance_mode"] = "burst",
                    ["qnn_context_priority"] = "high",
                    ["enable_htp_fp16_precision"] = "1"
                };

                options.AppendExecutionProvider("QNN", qnnOptions);
                DeviceType = "QNN (Qualcomm HTP)";
                break;
            }

            case InferenceDevice.CoreML:
                if (!OperatingSystem.IsMacOS())
                {
                    throw new InvalidOperationException(
                        "CoreML 执行提供器仅在 macOS 可用。当前平台无法使用，请改用 CPU 或其他设备。");
                }

                // 与 Python onnxruntime 同源；ANE/GPU 由 Core ML 与 EP 决定，可通过 CoreMLFlags 微调
                options.AppendExecutionProvider_CoreML();
                DeviceType = "CoreML";
                break;

            case InferenceDevice.OpenVINO:
                throw new InvalidOperationException(
                    "推理设备为 OpenVINO 时应使用 OpenVINO 引擎加载模型（.xml 或 .onnx 且由工厂路由到 OpenVinoInferenceEngine）。当前会话由 ONNX Runtime 创建，请勿在此组合下使用 Device=OpenVINO；请改为 OpenVINO 路径或改用 CPU/GPU。");

            default:
                DeviceType = "CPU";
                break;
        }

        _logger.LogDebug("[模型·设备] ONNX 执行提供器: {Device}", DeviceType);
    }

    private void DetectInputSize()
    {
        var inputEntry = _session!.InputMetadata.First();
        _inputName = inputEntry.Key;
        var inputMeta = inputEntry.Value;
        var inputShape = inputMeta.Dimensions;

        _logger.LogDebug("模型输入: Name={Name}, Shape=[{Shape}]", _inputName, string.Join(", ", inputShape));

        if (inputShape.Length == 4 && inputShape[2] > 0 && inputShape[3] > 0)
        {
            _inputHeight = inputShape[2];
            _inputWidth = inputShape[3];
            _logger.LogDebug("[模型·输入] 张量尺寸 {Width}x{Height}（来自模型 graph）", _inputWidth, _inputHeight);
        }
        else
        {
            _inputWidth = _config.InputSize.Width;
            _inputHeight = _config.InputSize.Height;
            _logger.LogDebug("[模型·输入] 张量尺寸 {Width}x{Height}（来自配置文件）", _inputWidth, _inputHeight);
        }
    }

    /// <summary>
    /// 从 ONNX 模型元数据读取类别名称和训练时输入尺寸。
    /// YOLO 导出的 ONNX 在 CustomMetadataMap 中包含 "names"、"imgsz" 等键。
    /// </summary>
    private void ReadModelMetadata()
    {
        try
        {
            var metadata = _session!.ModelMetadata.CustomMetadataMap;
            if (metadata == null || metadata.Count == 0)
            {
                _logger.LogDebug("模型不包含自定义元数据");
                return;
            }

            _logger.LogDebug("模型元数据键: [{Keys}]", string.Join(", ", metadata.Keys));

            // 读取类别名称: {0: 'box', 1: 'label', 2: 'tear_action', ...}
            if (metadata.TryGetValue("names", out var namesStr) && !string.IsNullOrWhiteSpace(namesStr))
            {
                _modelClassNames = OnnxModelMetadataReader.ParseYoloNamesMetadata(namesStr);
                if (_modelClassNames.Count > 0)
                {
                    _logger.LogInformation(
                        "[模型·类别] 元数据 names 共 {Count} 类: {Names}",
                        _modelClassNames.Count,
                        string.Join(", ", _modelClassNames));
                }
            }

            // 读取训练时输入尺寸并与配置比对
            if (metadata.TryGetValue("imgsz", out var imgszStr) && !string.IsNullOrWhiteSpace(imgszStr))
            {
                var sizes = Regex.Matches(imgszStr, @"\d+")
                    .Select(m => int.Parse(m.Value)).ToList();
                if (sizes.Count >= 2)
                {
                    var trainH = sizes[0];
                    var trainW = sizes.Count > 1 ? sizes[1] : sizes[0];

                    if (trainW != _config.InputSize.Width || trainH != _config.InputSize.Height)
                    {
                        _logger.LogWarning(
                            "[模型·输入] 元数据 imgsz={TrainW}x{TrainH} 与配置 {CfgW}x{CfgH} 不一致；推理将按模型张量 {ActualW}x{ActualH}",
                            trainW, trainH,
                            _config.InputSize.Width, _config.InputSize.Height,
                            _inputWidth, _inputHeight);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "[模型·类别] 读取元数据非致命异常，将回退为配置中的类别列表");
        }
    }

    /// <summary>
    /// 确定最终使用的类别列表和名称→索引映射。
    /// 优先级：模型元数据 > 配置。如果两者都有则验证一致性。
    /// </summary>
    private void ResolveClassNames()
    {
        ModelClassNamesResolution.Resolve(
            _modelClassNames,
            _config,
            _numClasses,
            _logger,
            out _resolvedClassNames,
            out _classIndexMap);
    }

    private void DetectModelStructure()
    {
        var outputMeta = _session!.OutputMetadata.First().Value;
        var outputShape = outputMeta.Dimensions;

        _logger.LogDebug("模型输出形状: [{Shape}]", string.Join(", ", outputShape));

        if (outputShape.Length == 3)
        {
            // YOLOv8 检测输出: [batch, attributes, anchors]
            _numAttributes = outputShape[1];
            _numAnchors = outputShape[2];

            // 检测任务类型
            _detectedTaskType = DetectTaskType();

            // 计算类别数
            _numClasses = CalculateNumClasses();

            _logger.LogInformation(
                "[模型·结构] 检测头: 任务={TaskType} | 属性维={Attributes} | 锚点数={Anchors} | 类别数={Classes}",
                ModelTypeYoloLabels.Format(_detectedTaskType), _numAttributes, _numAnchors, _numClasses);
        }
        else if (outputShape.Length == 2)
        {
            if (_config.ModelType != ModelType.Classification)
            {
                throw new InvalidOperationException(
                    "当前模型主输出为分类形状 [batch, classes]，仅 Classification 任务可用。");
            }

            // YOLOv8 分类输出: [batch, classes]
            _detectedTaskType = ModelType.Classification;
            _numAttributes = outputShape[1];
            _numAnchors = 1;
            _numClasses = _numAttributes;

            _logger.LogInformation(
                "[模型·结构] 分类头: 类别数={Classes}",
                _numClasses);
        }
        else
        {
            throw new InvalidOperationException(
                $"不支持的输出形状: [{string.Join(", ", outputShape)}]");
        }
    }

    private ModelType DetectTaskType()
    {
        // 如果用户明确指定了类型，使用用户配置
        if (_config.ModelType != ModelType.Auto)
        {
            _logger.LogDebug("[模型·结构] 任务类型由配置指定: {TaskType}", ModelTypeYoloLabels.Format(_config.ModelType));
            return _config.ModelType;
        }

        // 自动检测
        var outputCount = _session!.OutputMetadata.Count;

        // 检查是否有第二个输出（seg 模型特征）
        if (outputCount >= 2)
        {
            var output1Meta = _session.OutputMetadata.Skip(1).First().Value;
            var output1Shape = output1Meta.Dimensions;

            // seg 模型: output1 是 [1, 32, H, W] 形状的分割原型
            if (output1Shape.Length == 4 && output1Shape[1] == 32)
            {
                _logger.LogDebug("[模型·结构] 自动识别为分割模型（含 mask prototypes）");
                return ModelType.Segmentation;
            }
        }

        // 默认为检测模型
        return ModelType.Detection;
    }

    private int CalculateNumClasses()
    {
        return _detectedTaskType switch
        {
            ModelType.Detection => _numAttributes - 4,
            ModelType.Segmentation => _numAttributes - 4 - 32, // 32 mask coefficients
            ModelType.SegmentationTracking => _numAttributes - 4 - 32,
            ModelType.Classification => _numAttributes,
            ModelType.PoseEstimation => _numAttributes - 4 - 51, // 17 keypoints * 3
            ModelType.Obb => _numAttributes - 5, // OBB
            ModelType.DetectionTracking => _numAttributes - 4,
            ModelType.Track => _numAttributes - 4,
            _ => _numAttributes - 4
        };
    }

    private async Task<(float[] data, int[] shape, float ratio, float padW, float padH)> PreprocessImageAsync(
        Frame frame,
        CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            var originalHeight = frame.Height;
            var originalWidth = frame.Width;

            var ratio = Math.Min((float)_inputHeight / originalHeight, (float)_inputWidth / originalWidth);

            var newWidth = (int)Math.Round(originalWidth * ratio);
            var newHeight = (int)Math.Round(originalHeight * ratio);

            var padW = (_inputWidth - newWidth) / 2.0f;
            var padH = (_inputHeight - newHeight) / 2.0f;

            using var resized = new Mat();
            Cv2.Resize(frame.Mat, resized, new Size(newWidth, newHeight), interpolation: InterpolationFlags.Linear);

            var top = (int)Math.Round(padH - 0.1);
            var bottom = (int)Math.Round(padH + 0.1);
            var left = (int)Math.Round(padW - 0.1);
            var right = (int)Math.Round(padW + 0.1);

            using var padded = new Mat();
            Cv2.CopyMakeBorder(resized, padded, top, bottom, left, right,
                BorderTypes.Constant, new Scalar(114, 114, 114));

            using var rgb = new Mat();
            Cv2.CvtColor(padded, rgb, ColorConversionCodes.BGR2RGB);

            using var floatMat = new Mat();
            rgb.ConvertTo(floatMat, MatType.CV_32FC3, 1.0 / 255.0);

            var data = new float[1 * 3 * _inputHeight * _inputWidth];
            var channels = Cv2.Split(floatMat);

            try
            {
                for (int c = 0; c < 3; c++)
                {
                    var channelData = new float[_inputHeight * _inputWidth];
                    channels[c].GetArray(out channelData);
                    Array.Copy(channelData, 0, data, c * _inputHeight * _inputWidth, channelData.Length);
                }
            }
            finally
            {
                foreach (var channel in channels)
                    channel?.Dispose();
            }

            var shape = new[] { 1, 3, _inputHeight, _inputWidth };
            return (data, shape, ratio, padW, padH);

        }, cancellationToken);
    }

    // ──────────── 自适应精度支持（FP32 / FP16 / BFloat16） ────────────

    private void DetectElementTypes()
    {
        var inputMeta = _session!.InputMetadata.First().Value;
        _inputElementType = inputMeta.ElementDataType;

        if (_inputElementType != TensorElementType.Float)
            _logger.LogInformation(
                "[模型·输入] 张量元素类型 {Type}，预处理将按需转换",
                _inputElementType);
    }

    /// <summary>
    /// 根据模型输入精度自动创建对应类型的张量。
    /// FP32 路径零开销（直接包装原数组，无拷贝），非 FP32 路径仅额外一次类型转换。
    /// </summary>
    private NamedOnnxValue CreateInputValue(string name, float[] fp32Data, int[] shape)
    {
        switch (_inputElementType)
        {
            case TensorElementType.Float:
                return NamedOnnxValue.CreateFromTensor(name,
                    new DenseTensor<float>(fp32Data, shape));

            case TensorElementType.Float16:
            {
                var fp16Data = new Float16[fp32Data.Length];
                var bits = MemoryMarshal.Cast<Float16, ushort>(fp16Data.AsSpan());
                for (int i = 0; i < fp32Data.Length; i++)
                    bits[i] = BitConverter.HalfToUInt16Bits((Half)fp32Data[i]);
                return NamedOnnxValue.CreateFromTensor(name,
                    new DenseTensor<Float16>(fp16Data, shape));
            }

            case TensorElementType.BFloat16:
            {
                var bf16Data = new BFloat16[fp32Data.Length];
                var bits = MemoryMarshal.Cast<BFloat16, ushort>(bf16Data.AsSpan());
                for (int i = 0; i < fp32Data.Length; i++)
                    bits[i] = (ushort)(BitConverter.SingleToUInt32Bits(fp32Data[i]) >> 16);
                return NamedOnnxValue.CreateFromTensor(name,
                    new DenseTensor<BFloat16>(bf16Data, shape));
            }

            default:
                _logger.LogWarning("[模型·输入] 未知精度 {Type}，按 FP32 处理", _inputElementType);
                return NamedOnnxValue.CreateFromTensor(name,
                    new DenseTensor<float>(fp32Data, shape));
        }
    }

    /// <summary>
    /// 读取输出张量并统一转为 float[]。逐输出检查 OutputMetadata 中的精度，
    /// 兼容混合精度模型（如分割模型主输出 FP16 + mask prototype FP32）。
    /// </summary>
    private float[] ReadOutputAsFloatArray(DisposableNamedOnnxValue output)
    {
        var elementType = TensorElementType.Float;
        if (_session!.OutputMetadata.TryGetValue(output.Name, out var meta))
            elementType = meta.ElementDataType;

        switch (elementType)
        {
            case TensorElementType.Float:
                return output.AsTensor<float>().ToArray();

            case TensorElementType.Float16:
            {
                var fp16Array = output.AsTensor<Float16>().ToArray();
                var bits = MemoryMarshal.Cast<Float16, ushort>(fp16Array.AsSpan());
                var result = new float[bits.Length];
                for (int i = 0; i < bits.Length; i++)
                    result[i] = (float)BitConverter.UInt16BitsToHalf(bits[i]);
                return result;
            }

            case TensorElementType.BFloat16:
            {
                var bf16Array = output.AsTensor<BFloat16>().ToArray();
                var bits = MemoryMarshal.Cast<BFloat16, ushort>(bf16Array.AsSpan());
                var result = new float[bits.Length];
                for (int i = 0; i < bits.Length; i++)
                    result[i] = BitConverter.UInt32BitsToSingle((uint)bits[i] << 16);
                return result;
            }

            default:
                _logger.LogWarning("[模型] 输出张量未知精度 {Type}，按 FP32 读取", elementType);
                return output.AsTensor<float>().ToArray();
        }
    }

    // ──────────── 其他公共方法 ────────────

    public List<string> GetClassNames()
    {
        return _resolvedClassNames;
    }

    public IReadOnlyDictionary<string, int> GetClassIndexMap()
    {
        return _classIndexMap;
    }

    public InferenceResult Infer(OpenCvSharp.Mat frame)
    {
        // 必须用 FromBorrowedMat：临时 Frame 被终结器回收时不应 Dispose 调用方（如视频队列）仍持有的 Mat。
        return InferAsync(Frame.FromBorrowedMat(frame), _config.ConfidenceThreshold, _config.IouThreshold)
            .GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        _session?.Dispose();
        _session = null;
        _isLoaded = false;
        _logger.LogDebug("ONNX 推理引擎已释放");
        GC.SuppressFinalize(this);
    }
}

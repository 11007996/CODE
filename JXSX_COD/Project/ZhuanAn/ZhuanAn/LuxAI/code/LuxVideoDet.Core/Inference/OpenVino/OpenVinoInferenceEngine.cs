using System.Runtime.InteropServices;
using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Inference.Onnx;
using LuxVideoDet.Core.Inference.Postprocessors;
using LuxVideoDet.Core.Inference.Results;
using Microsoft.Extensions.Logging;
using OpenCvSharp;
using OpenVinoSharp;
using OpenVinoSharp.preprocess;
using System.Diagnostics;
using OVCore = OpenVinoSharp.Core;

namespace LuxVideoDet.Core.Inference.OpenVino;

/// <summary>
/// OpenVINO 推理引擎 - 支持多种 YOLO 任务类型
/// </summary>
public class OpenVinoInferenceEngine : IInferenceEngine
{
    private readonly InferenceConfig _config;
    private readonly ILogger<OpenVinoInferenceEngine> _logger;
    private readonly PostprocessorFactory _postprocessorFactory;
    
    private OVCore? _core;
    private Model? _model;
    private CompiledModel? _compiledModel;
    private InferRequest? _inferRequest;
    private IPostprocessor? _postprocessor;
    private bool _isLoaded;

    private int _inputWidth = 640;
    private int _inputHeight = 640;
    private int _numClasses;
    private int _numAnchors;
    private int _numAttributes;
    private ModelType _detectedTaskType = ModelType.Detection;
    private string _inputName = string.Empty;
    private string _outputName = string.Empty;
    private ulong _outputCount = 1;
    private OvType? _inputElementType;

    private Dictionary<string, int> _classIndexMap = new(StringComparer.OrdinalIgnoreCase);
    private List<string> _resolvedClassNames = new();
    /// <summary>OpenVINO 读 .onnx 时无法取 CustomMetadataMap，启动时用 ORT 扫一遍嵌入的 names。</summary>
    private List<string>? _onnxEmbeddedClassNames;

    public string EngineType => "OpenVINO";
    public string DeviceType { get; private set; } = "CPU";
    public bool IsLoaded => _isLoaded;

    public OpenVinoInferenceEngine(
        InferenceConfig config,
        ILogger<OpenVinoInferenceEngine> logger,
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
            _logger.LogDebug("开始加载 OpenVINO 模型: {ModelPath}", modelPath);

            if (!File.Exists(modelPath))
            {
                throw new FileNotFoundException($"模型文件不存在: {modelPath}");
            }

            await Task.Run(() =>
            {
                // 创建 OpenVINO Core
                _core = new OVCore();

                // 输出可用设备信息
                LogAvailableDevices();

                // 读取模型
                _model = _core.read_model(modelPath);

                // 检测输入尺寸和类型
                DetectInputInfo();

                // 检测模型结构
                DetectModelStructure();

                _onnxEmbeddedClassNames = string.Equals(Path.GetExtension(modelPath), ".onnx", StringComparison.OrdinalIgnoreCase)
                    ? OnnxModelMetadataReader.TryReadYoloClassNamesFromOnnx(modelPath, _logger)
                    : null;

                // 配置设备并编译模型
                ConfigureAndCompileModel();

                // 创建推理请求
                _inferRequest = _compiledModel!.create_infer_request();

                ResolveClassNames();

                // 创建后处理器
                _postprocessor = _postprocessorFactory.GetPostprocessor(_detectedTaskType);

                _isLoaded = true;

                _logger.LogInformation(
                    "[推理运行时] {Runtime} | 输入={Width}x{Height} | 任务={Task} | 类别数={Classes}",
                    $"{EngineType} + {DeviceType}",
                    _inputWidth,
                    _inputHeight,
                    ModelTypeYoloLabels.Format(_detectedTaskType),
                    _numClasses);

            }, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载 OpenVINO 模型失败: {ModelPath}", modelPath);
            throw;
        }
    }

    public async Task<InferenceResult> InferAsync(
        Frame frame,
        float confidenceThreshold,
        float iouThreshold,
        CancellationToken cancellationToken = default)
    {
        if (!_isLoaded || _inferRequest == null || _postprocessor == null)
        {
            throw new InvalidOperationException("模型未加载");
        }

        var stopwatch = Stopwatch.StartNew();
        var timing = new InferenceTimingBreakdown();

        try
        {
            var sw = Stopwatch.StartNew();
            var (inputData, ratio, padW, padH) = await PreprocessImageAsync(frame, cancellationToken);
            timing.PreprocessMs = (float)sw.Elapsed.TotalMilliseconds;

            float[][] outputs = null!;
            await Task.Run(() =>
            {
                sw.Restart();
                using var inputTensor = CreateInputTensor(inputData);
                _inferRequest!.set_input_tensor(inputTensor);
                timing.InputTensorMs = (float)sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                _inferRequest.infer();
                timing.NativeRunMs = (float)sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                var list = new float[_outputCount][];
                for (ulong i = 0; i < _outputCount; i++)
                {
                    using var outputTensor = _inferRequest.get_output_tensor(i);
                    list[i] = ReadOutputTensorAsFloat(outputTensor);
                }

                outputs = list;
                timing.OutputToCpuMs = (float)sw.Elapsed.TotalMilliseconds;
            }, cancellationToken);

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
                "推理完成 InferenceTime={Time}ms, Detections={Count}, 细分 预处理={Pre:F1} 输入张量={In:F1} infer={Run:F1} 输出到CPU={Out:F1} 引擎后处理={Ep:F1}ms",
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
            _logger.LogError(ex, "推理失败");
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

    private void LogAvailableDevices()
    {
        try
        {
            var availableDevices = _core!.get_available_devices();
            _logger.LogDebug("可用的 OpenVINO 设备: {Devices}", string.Join(", ", availableDevices));

            if (availableDevices.Contains("CPU"))
            {
                try
                {
                    var cpuName = _core.get_property("CPU", "FULL_DEVICE_NAME");
                    _logger.LogDebug("CPU 设备: {CpuName}", cpuName);

                    var capabilities = _core.get_property("CPU", "OPTIMIZATION_CAPABILITIES");
                    _logger.LogDebug("CPU 优化能力: {Capabilities}", capabilities);
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "无法查询设备能力");
        }
    }

    private void DetectInputInfo()
    {
        var input = _model!.input();
        _inputName = input.get_any_name();
        var inputShape = input.get_shape();
        
        _inputHeight = (int)inputShape[2];
        _inputWidth = (int)inputShape[3];
        _inputElementType = input.get_element_type();

        var inputTypeStr = _inputElementType.to_string();

        _logger.LogDebug(
            "模型输入: {Name}, 形状: [{Batch}, {Channels}, {Height}, {Width}], 类型: {Type}",
            _inputName, inputShape[0], inputShape[1], _inputHeight, _inputWidth, inputTypeStr);
    }

    private void DetectModelStructure()
    {
        _outputCount = _model!.get_outputs_size();
        if (_outputCount == 0)
            throw new InvalidOperationException("模型无任何输出");

        // 与 ONNX Runtime 一致：主结构由第一个输出决定（OutputMetadata.First）
        var primary = _model.output(0);
        _outputName = primary.get_any_name();
        var outputShape = primary.get_shape();

        if (!TryGetOpenVinoShapeDimensions(outputShape, out var dims) || dims.Length == 0)
        {
            throw new InvalidOperationException(
                $"无法解析主输出形状: {_outputName}（OpenVINO: {outputShape.to_string()}）");
        }

        _logger.LogDebug(
            "模型输出数={Count}, 主输出 {Name} 形状: [{Shape}]",
            _outputCount,
            _outputName,
            string.Join(", ", dims));

        // 与 OnnxInferenceEngine.DetectModelStructure 分支对齐（Shape 索引器可能抛 ArgumentOutOfRangeException，不可直接 [1]/[2]）
        if (dims.Length == 3)
        {
            _numAttributes = dims[1];
            _numAnchors = dims[2];

            _detectedTaskType = DetectTaskType();
            _numClasses = CalculateNumClasses();

            _logger.LogDebug(
                "检测到模型结构: TaskType={TaskType}, Attributes={Attributes}, Anchors={Anchors}, Classes={Classes}",
                ModelTypeYoloLabels.Format(_detectedTaskType), _numAttributes, _numAnchors, _numClasses);
        }
        else if (dims.Length == 2)
        {
            if (_config.ModelType != ModelType.Classification)
            {
                throw new InvalidOperationException(
                    "当前模型主输出为分类形状 [batch, classes]，仅 Classification 任务可用。");
            }

            _detectedTaskType = ModelType.Classification;
            _numAttributes = dims[1];
            _numAnchors = 1;
            _numClasses = _numAttributes;

            _logger.LogDebug("检测到分类模型: Classes={Classes}", _numClasses);
        }
        else
        {
            throw new InvalidOperationException(
                $"不支持的输出形状: [{string.Join(", ", dims)}]");
        }
    }

    /// <summary>
    /// 与 <see cref="Onnx.OnnxInferenceEngine"/> 中任务自动识别对齐：多输出时根据第二路形状判断 YOLOv8-seg。
    /// </summary>
    private ModelType DetectTaskType()
    {
        if (_config.ModelType != ModelType.Auto)
        {
            _logger.LogDebug("使用用户配置的任务类型: {TaskType}", ModelTypeYoloLabels.Format(_config.ModelType));
            return _config.ModelType;
        }

        if (_outputCount >= 2)
        {
            var second = _model!.output(1);
            var output1Shape = second.get_shape();

            if (TryGetOpenVinoShapeDimensions(output1Shape, out var dims)
                && dims.Length == 4
                && dims[1] == 32)
            {
                _logger.LogDebug("自动识别为分割模型（含 mask prototypes）");
                return ModelType.Segmentation;
            }
        }

        return ModelType.Detection;
    }

    /// <summary>
    /// OpenVinoSharp 的 <see cref="Shape"/> 索引器内部使用 List，越界时抛出
    /// <see cref="ArgumentOutOfRangeException"/>（而非 <see cref="IndexOutOfRangeException"/>），须同时捕获。
    /// </summary>
    private static bool TryGetOpenVinoShapeDimensions(Shape shape, out int[] dims)
    {
        var list = new List<int>();
        for (var i = 0; i < 16; i++)
        {
            try
            {
                list.Add((int)shape[i]);
            }
            catch (IndexOutOfRangeException)
            {
                break;
            }
            catch (ArgumentOutOfRangeException)
            {
                break;
            }
        }

        dims = list.ToArray();
        return dims.Length > 0;
    }

    private static string ShapeToDebugString(Shape shape)
    {
        if (!TryGetOpenVinoShapeDimensions(shape, out var dims))
            return shape.to_string();
        return string.Join(", ", dims);
    }

    /// <summary>
    /// 与 ONNX Runtime <see cref="Onnx.OnnxInferenceEngine"/> 输出读取对齐：统一为 float[]。
    /// </summary>
    private float[] ReadOutputTensorAsFloat(Tensor outputTensor)
    {
        var elementType = outputTensor.get_element_type();
        var typeStr = elementType.to_string();
        var size = (int)outputTensor.get_size();

        switch (typeStr)
        {
            case "f32":
                return outputTensor.get_data<float>(size);

            case "f16":
            {
                var byteLen = (int)outputTensor.get_byte_size();
                var bytes = new byte[byteLen];
                unsafe
                {
                    var p = outputTensor.data();
                    Marshal.Copy(p, bytes, 0, byteLen);
                }

                var n = byteLen / 2;
                var result = new float[n];
                for (var i = 0; i < n; i++)
                {
                    var u = BitConverter.ToUInt16(bytes, i * 2);
                    result[i] = (float)BitConverter.UInt16BitsToHalf(u);
                }

                return result;
            }

            case "bf16":
            {
                var byteLen = (int)outputTensor.get_byte_size();
                var bytes = new byte[byteLen];
                unsafe
                {
                    var p = outputTensor.data();
                    Marshal.Copy(p, bytes, 0, byteLen);
                }

                var n = byteLen / 2;
                var result = new float[n];
                for (var i = 0; i < n; i++)
                {
                    var u = BitConverter.ToUInt16(bytes, i * 2);
                    result[i] = BitConverter.UInt32BitsToSingle((uint)u << 16);
                }

                return result;
            }

            default:
                _logger.LogWarning("主输出未知精度 {Type}，按 FP32 读取", typeStr);
                return outputTensor.get_data<float>(size);
        }
    }

    private int CalculateNumClasses()
    {
        return _detectedTaskType switch
        {
            ModelType.Detection => _numAttributes - 4,
            ModelType.Segmentation => _numAttributes - 4 - 32,
            ModelType.SegmentationTracking => _numAttributes - 4 - 32,
            ModelType.Classification => _numAttributes,
            ModelType.PoseEstimation => _numAttributes - 4 - 51,
            ModelType.Obb => _numAttributes - 5,
            ModelType.Track => _numAttributes - 4,
            ModelType.DetectionTracking => _numAttributes - 4,
            _ => _numAttributes - 4
        };
    }

    private void ConfigureAndCompileModel()
    {
        var device = _config.Device;
        var deviceName = MapDeviceName(device);
        DeviceType = deviceName;

        // 限制推理线程数，避免多引擎并行时 CPU 线程争抢
        if (_config.ThreadCount > 0)
        {
            try
            {
                _core!.set_property(deviceName,
                    new KeyValuePair<string, string>("INFERENCE_NUM_THREADS", _config.ThreadCount.ToString()));
                _logger.LogDebug("OpenVINO 推理线程数: {ThreadCount}, Device={Device}",
                    _config.ThreadCount, deviceName);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "设置 OpenVINO 推理线程数失败");
            }
        }

        _logger.LogDebug("OpenVINO compile_model 目标: {Device}", deviceName);

        _compiledModel = _core!.compile_model(_model!, deviceName);

        try
        {
            var perfHint = _compiledModel.get_property("PERFORMANCE_HINT");
            _logger.LogDebug("OpenVINO PERFORMANCE_HINT: {PerfHint}", perfHint);
        }
        catch { }

        try
        {
            var numStreams = _compiledModel.get_property("NUM_STREAMS");
            _logger.LogDebug("OpenVINO NUM_STREAMS: {NumStreams}", numStreams);
        }
        catch { }
    }

    /// <summary>
    /// 映射到 OpenVINO <c>compile_model</c> 设备名。配置项 <see cref="InferenceDevice.OpenVINO"/> 表示「用 OpenVINO 运行时」，
    /// 此处用 <c>AUTO</c> 由 OpenVINO 选择实际硬件（CPU/GPU/NPU 等），避免与「CPU 表示 ONNX Runtime」混淆。
    /// </summary>
    private static string MapDeviceName(InferenceDevice device)
    {
        return device switch
        {
            InferenceDevice.GPU => "GPU",
            InferenceDevice.CPU => "CPU",
            InferenceDevice.OpenVINO => "AUTO",
            InferenceDevice.CoreML => "AUTO",
            // TensorRT 仅用于 ONNX Runtime；若误配 OpenVINO 路径则与 GPU 类似交由运行时选设备
            InferenceDevice.TensorRT => "AUTO",
            _ => "AUTO"
        };
    }

    private async Task<(float[], float, float, float)> PreprocessImageAsync(
        Frame frame,
        CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            var originalHeight = frame.Height;
            var originalWidth = frame.Width;

            // 计算缩放比例（保持宽高比）
            var ratio = Math.Min((float)_inputHeight / originalHeight, (float)_inputWidth / originalWidth);

            var newWidth = (int)Math.Round(originalWidth * ratio);
            var newHeight = (int)Math.Round(originalHeight * ratio);

            var padW = (_inputWidth - newWidth) / 2.0f;
            var padH = (_inputHeight - newHeight) / 2.0f;

            // Resize
            using var resized = new Mat();
            Cv2.Resize(frame.Mat, resized, new Size(newWidth, newHeight), interpolation: InterpolationFlags.Linear);

            // Add padding (letterbox)
            var top = (int)Math.Round(padH - 0.1);
            var bottom = (int)Math.Round(padH + 0.1);
            var left = (int)Math.Round(padW - 0.1);
            var right = (int)Math.Round(padW + 0.1);

            using var padded = new Mat();
            Cv2.CopyMakeBorder(resized, padded, top, bottom, left, right,
                BorderTypes.Constant, new Scalar(114, 114, 114));

            // Convert BGR to RGB
            using var rgb = new Mat();
            Cv2.CvtColor(padded, rgb, ColorConversionCodes.BGR2RGB);

            // Convert to float and normalize
            using var floatMat = new Mat();
            rgb.ConvertTo(floatMat, MatType.CV_32FC3, 1.0 / 255.0);

            // Split channels and create data [1, 3, H, W]
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
                {
                    channel?.Dispose();
                }
            }

            return (data, ratio, padW, padH);

        }, cancellationToken);
    }

    /// <summary>
    /// 根据模型输入类型自动创建 Tensor（支持 FP32、FP16、INT8 等）
    /// </summary>
    private Tensor CreateInputTensor(float[] inputData)
    {
        if (_inputElementType == null)
        {
            throw new InvalidOperationException("输入元素类型未初始化");
        }

        var inputTypeStr = _inputElementType.to_string();
        var shape = new Shape(1, 3, _inputHeight, _inputWidth);

        switch (inputTypeStr)
        {
            case "f32":
                // FP32: 直接使用 float[]
                return new Tensor(shape, inputData);

            case "f16":
                // FP16: 转换
                var fp16Bytes = new byte[inputData.Length * 2];
                for (int i = 0; i < inputData.Length; i++)
                {
                    var halfBits = BitConverter.HalfToUInt16Bits((Half)inputData[i]);
                    fp16Bytes[i * 2] = (byte)(halfBits & 0xFF);
                    fp16Bytes[i * 2 + 1] = (byte)((halfBits >> 8) & 0xFF);
                }

                var fp16Tensor = new Tensor(_inputElementType, shape);
                unsafe
                {
                    var tensorData = fp16Tensor.data();
                    System.Runtime.InteropServices.Marshal.Copy(fp16Bytes, 0, tensorData, fp16Bytes.Length);
                }
                return fp16Tensor;

            case "i8":
            case "u8":
                // INT8/UINT8: 转换 [0, 1] 到 [0, 255]
                var int8Data = new byte[inputData.Length];
                for (int i = 0; i < inputData.Length; i++)
                {
                    int8Data[i] = (byte)Math.Clamp((int)(inputData[i] * 255.0f), 0, 255);
                }

                var int8Tensor = new Tensor(_inputElementType, shape);
                unsafe
                {
                    var tensorData = int8Tensor.data();
                    System.Runtime.InteropServices.Marshal.Copy(int8Data, 0, tensorData, int8Data.Length);
                }
                return int8Tensor;

            default:
                _logger.LogWarning("不支持的输入类型: {Type}，回退到 FP32", inputTypeStr);
                return new Tensor(shape, inputData);
        }
    }

    public List<string> GetClassNames()
    {
        return _resolvedClassNames;
    }

    public IReadOnlyDictionary<string, int> GetClassIndexMap()
    {
        return _classIndexMap;
    }

    private void ResolveClassNames()
    {
        ModelClassNamesResolution.Resolve(
            _onnxEmbeddedClassNames,
            _config,
            _numClasses,
            _logger,
            out _resolvedClassNames,
            out _classIndexMap);
    }

    public InferenceResult Infer(OpenCvSharp.Mat frame)
    {
        return InferAsync(Frame.FromBorrowedMat(frame), _config.ConfidenceThreshold, _config.IouThreshold)
            .GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        _inferRequest?.Dispose();
        _compiledModel?.Dispose();
        _model?.Dispose();
        _core?.Dispose();

        _inferRequest = null;
        _compiledModel = null;
        _model = null;
        _core = null;
        _isLoaded = false;

        _logger.LogDebug("OpenVINO 推理引擎已释放");
        GC.SuppressFinalize(this);
    }
}

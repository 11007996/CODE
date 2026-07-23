using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Inference.Onnx;
using LuxVideoDet.Core.Inference.OpenVino;
using LuxVideoDet.Core.Inference.Postprocessors;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Inference;

/// <summary>
/// 推理引擎工厂 - 根据配置创建推理引擎。
/// </summary>
/// <remarks>
/// <c>.onnx</c> + <see cref="InferenceDevice.CPU"/> 时使用 ONNX Runtime（CPU EP），各平台均不再默认尝试 OpenVINO。
/// 需使用 OpenVINO 推理时，在配置中选择 <see cref="InferenceDevice.OpenVINO"/>。
/// OpenVINO 读 <c>.onnx</c> 时会用 ONNX Runtime 仅扫描同一文件内的 <c>names</c> 元数据，与 ORT 路径对齐类别名。
/// <c>.xml</c>（IR）固定走 OpenVINO。
/// </remarks>
public class InferenceEngineFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly PostprocessorFactory _postprocessorFactory;

    public InferenceEngineFactory(
        ILoggerFactory loggerFactory,
        PostprocessorFactory postprocessorFactory)
    {
        _loggerFactory = loggerFactory;
        _postprocessorFactory = postprocessorFactory;
    }

    /// <summary>
    /// 创建推理引擎实例（不加载模型）。<c>.onnx</c> 时根据 <see cref="InferenceConfig.Device"/> 选择 ONNX Runtime 或 OpenVINO。
    /// </summary>
    public IInferenceEngine CreateEngine(InferenceConfig config)
    {
        var extension = Path.GetExtension(config.ModelPath).ToLowerInvariant();

        return extension switch
        {
            ".onnx" => config.Device == InferenceDevice.OpenVINO
                ? CreateOpenVinoEngine(config)
                : CreateOnnxEngine(config),
            ".xml" => CreateOpenVinoEngine(config),
            _ => throw new NotSupportedException($"不支持的模型格式: {extension}")
        };
    }

    /// <summary>
    /// 创建推理引擎并加载模型。
    /// <c>.onnx</c> + <see cref="InferenceDevice.OpenVINO"/> 时使用 OpenVINO；<c>.onnx</c> + <see cref="InferenceDevice.CPU"/> 时使用 ONNX Runtime（CPU EP）。
    /// </summary>
    public async Task<IInferenceEngine> CreateEngineAsync(
        InferenceConfig config,
        CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(config.ModelPath).ToLowerInvariant();
        var modelPath = config.ModelPath;

        if (extension == ".xml")
        {
            var engine = CreateOpenVinoEngine(config);
            await engine.LoadModelAsync(modelPath, cancellationToken).ConfigureAwait(false);
            return engine;
        }

        if (extension != ".onnx")
            throw new NotSupportedException($"不支持的模型格式: {extension}");

        if (config.Device == InferenceDevice.OpenVINO)
        {
            var ov = CreateOpenVinoEngine(config);
            await ov.LoadModelAsync(modelPath, cancellationToken).ConfigureAwait(false);
            return ov;
        }

        // GPU / NPU / CoreML：固定 ONNX Runtime（与旧版策略一致）
        if (config.Device != InferenceDevice.CPU)
        {
            var onnxOnly = CreateOnnxEngine(config);
            await onnxOnly.LoadModelAsync(modelPath, cancellationToken).ConfigureAwait(false);
            return onnxOnly;
        }

        // CPU + ONNX：ONNX Runtime（CPU EP），不再默认尝试 OpenVINO
        var onnx = CreateOnnxEngine(config);
        await onnx.LoadModelAsync(modelPath, cancellationToken).ConfigureAwait(false);
        return onnx;
    }

    private IInferenceEngine CreateOnnxEngine(InferenceConfig config)
    {
        var logger = _loggerFactory.CreateLogger<OnnxInferenceEngine>();
        return new OnnxInferenceEngine(config, logger, _postprocessorFactory);
    }

    private IInferenceEngine CreateOpenVinoEngine(InferenceConfig config)
    {
        var logger = _loggerFactory.CreateLogger<OpenVinoInferenceEngine>();
        return new OpenVinoInferenceEngine(config, logger, _postprocessorFactory);
    }
}

using LuxVideoDet.Core.Inference.Results;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Inference.Postprocessors;

/// <summary>
/// 分类后处理器 (YOLOv8-cls)
/// 输出格式: [1, numClasses]
/// </summary>
public class ClassificationPostprocessor : IPostprocessor
{
    private readonly ILogger<ClassificationPostprocessor> _logger;

    public ModelType TaskType => ModelType.Classification;

    public ClassificationPostprocessor(ILogger<ClassificationPostprocessor> logger)
    {
        _logger = logger;
    }

    public List<Detection> Process(float[][] outputs, PostprocessContext context)
    {
        var output = outputs[0];
        var maxConf = 0f;
        var maxClassId = 0;

        _logger.LogDebug("开始分类后处理 Classes={Classes}", context.NumClasses);

        for (int c = 0; c < context.NumClasses; c++)
        {
            if (output[c] > maxConf)
            {
                maxConf = output[c];
                maxClassId = c;
            }
        }

        if (maxConf < context.ConfThreshold)
        {
            _logger.LogDebug("置信度 {Conf} 低于阈值 {Threshold}，无分类结果", maxConf, context.ConfThreshold);
            return new List<Detection>();
        }

        _logger.LogDebug("分类结果: ClassId={ClassId}, Confidence={Conf}", maxClassId, maxConf);

        var n = output.Length;
        var perClass = new List<string>(n);
        for (int i = 0; i < n; i++)
        {
            perClass.Add(i < context.ClassNames.Count && !string.IsNullOrWhiteSpace(context.ClassNames[i])
                ? context.ClassNames[i]
                : $"class{i}");
        }

        return new List<Detection>
        {
            new Detection
            {
                ClassId = maxClassId,
                ClassName = maxClassId < context.ClassNames.Count
                    ? context.ClassNames[maxClassId]
                    : $"class{maxClassId}",
                Confidence = maxConf,
                Probabilities = output,
                PerClassLabels = perClass,
                BoundingBox = new BoundingBox()
            }
        };
    }
}

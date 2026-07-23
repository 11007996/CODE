using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Notification;
using LuxVideoDet.Core.Region;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using OpenCvSharp;

namespace LuxVideoDet.Core.Algorithm.TypecPacking;

/// <summary>
/// Typec包装流水线目检测算法描述符
/// </summary>
public sealed class TypecPackingDescriptor : IAlgorithmDescriptor
{
    public string TypeKey => "Typec_packing";

    public string DisplayName => "Typec包装目检测";

    public List<string> DefaultClasses => new()
    {
        TypecPackingDetectionClass.BOX,
        TypecPackingDetectionClass.DIVIDER,
        TypecPackingDetectionClass.PRODUCT,
        TypecPackingDetectionClass.PRODUCT_DIVIDER,
        TypecPackingDetectionClass.SCALE_GREEN_LIGHT,
        TypecPackingDetectionClass.BOX_LID
    };

    /// <summary>
    /// 必需的区域定义
    /// </summary>
    public List<RegionDefinition> RequiredRegions => new()
    {
        new()
        {
            Name = "BoxArea",
            DisplayName = "纸箱区域",
            Description = "纸箱内部区域，用于检测空箱、隔板、产品、产品隔板和封箱",
            DefaultColor = new Scalar(0, 200, 0),
            Required = true
        },
        new()
        {
            Name = "ScaleArea",
            DisplayName = "称重区",
            Description = "电子秤区域，用于检测产品称重和绿灯状态",
            DefaultColor = new Scalar(200, 200, 0),
            Required = true
        }
    };

    /// <summary>
    /// 默认分类颜色
    /// </summary>
    public IReadOnlyList<Scalar>? DefaultClassColors => new List<Scalar>
    {
        new Scalar(139, 90, 43),   // 空箱 - 棕色
        new Scalar(255, 200, 100), // 隔板 - 浅黄色
        new Scalar(255, 255, 255), // 产品 - 白色
        new Scalar(200, 150, 100), // 产品隔板 - 浅棕黄色
        new Scalar(0, 255, 0),     // 电子秤绿灯 - 绿色
        new Scalar(139, 69, 19)    // 封箱 - 深棕色
    };

    /// <summary>
    /// 算法参数表单
    /// </summary>
    public IReadOnlyList<AlgorithmArgsFormSection> AlgorithmParameterSections { get; } =
    [
        new AlgorithmArgsFormSection
        {
            SectionTitle = "Typec包装·工序参数",
            Description = "配置产品称重和包装工序检测参数",
            ArgFields =
            [
                new NotificationParameterDefinition
                {
                    Name = "debounce_frames",
                    DisplayName = "防抖帧数",
                    Description = "连续检测到多少帧OK才判定步骤完成",
                    ParameterType = "int",
                    DefaultValue = 3,
                    Required = false
                },
                new NotificationParameterDefinition
                {
                    Name = "products_per_layer",
                    DisplayName = "每层产品数量",
                    Description = "每层包装的产品数量，默认16个",
                    ParameterType = "int",
                    DefaultValue = 16,
                    Required = false
                },
                new NotificationParameterDefinition
                {
                    Name = "auto_reset",
                    DisplayName = "完成后自动重置",
                    Description = "OK或NG后是否自动回到初始化状态",
                    ParameterType = "bool",
                    DefaultValue = true,
                    Required = false
                }
            ]
        },
        new AlgorithmArgsFormSection
        {
            SectionTitle = "Typec包装·工序流程",
            Description = 
                "检测流程：\n" +
                "1. 初始化（检测到空箱）\n" +
                "2. 箱内放第1层隔板\n" +
                "3. 第1层产品循环×16（称重→绿灯→放箱）\n" +
                "4. 箱内放第2层隔板\n" +
                "5. 第2层产品循环×16（称重→绿灯→放箱）\n" +
                "6. 关箱（封箱）\n\n" +
                "产品循环动作：\n" +
                "1. 产品放到电子秤上\n" +
                "2. 电子秤亮绿灯（称重完成）\n" +
                "3. 产品放入箱内",
            ArgFields = Array.Empty<NotificationParameterDefinition>()
        }
    ];

    /// <summary>
    /// 创建算法实例
    /// </summary>
    public IDetectionAlgorithm Create(
        IInferenceEngine inferenceEngine,
        RegionManager regionManager,
        List<INotificationService> notifiers,
        ILogger logger,
        IServiceProvider serviceProvider)
    {
        return new TypecPackingAlgorithm(inferenceEngine, regionManager, notifiers, logger);
    }
}
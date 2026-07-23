namespace LuxVideoDet.Core.Algorithm.TypecPacking;

/// <summary>
/// Typec包装工序步骤枚举
/// </summary>
public enum TypecPackingStep
{
    /// <summary>空闲状态，等待初始化</summary>
    Idle,
    
    /// <summary>1. 初始化（空箱）</summary>
    Initializing,
    
    /// <summary>2. 箱内放第1层隔板</summary>
    PlaceFirstLayerDivider,
    
    /// <summary>3. 第1层产品循环(16个)</summary>
    FirstLayerProducts,
    
    /// <summary>4. 箱内放第2层隔板</summary>
    PlaceSecondLayerDivider,
    
    /// <summary>5. 第2层产品循环(16个)</summary>
    SecondLayerProducts,
    
    /// <summary>6. 关箱（封箱）</summary>
    CloseBox,
    
    /// <summary>完成</summary>
    Completed,
    
    /// <summary>NG - 工序遗漏</summary>
    NG
}

/// <summary>
/// 产品循环子步骤
/// </summary>
public enum ProductCycleSubStep
{
    /// <summary>等待称重</summary>
    WaitingForWeighing,
    
    /// <summary>等待绿灯（称重完成）</summary>
    WaitingForGreenLight,
    
    /// <summary>等待放入箱内</summary>
    WaitingForPlacement
}

/// <summary>
/// Typec包装算法运行时上下文
/// </summary>
public sealed class TypecPackingContext
{
    /// <summary>当前工序步骤</summary>
    public TypecPackingStep CurrentStep { get; set; } = TypecPackingStep.Idle;
    
    /// <summary>当前步骤连续检测成功的帧数（防抖）</summary>
    public int ConsistentFrames { get; set; }
    
    /// <summary>OK计数</summary>
    public int OkCount { get; set; }
    
    /// <summary>NG计数</summary>
    public int NgCount { get; set; }
    
    /// <summary>当前箱内产品数量</summary>
    public int CurrentProductsInBox { get; set; }
    
    /// <summary>产品循环子步骤</summary>
    public ProductCycleSubStep CycleSubStep { get; set; } = ProductCycleSubStep.WaitingForWeighing;
    
    /// <summary>上一帧检测到的产品数量（用于检测新增）</summary>
    public int LastProductsInBox { get; set; }
    
    /// <summary>上一帧检测到的绿灯状态</summary>
    public bool LastGreenLight { get; set; }
    
    /// <summary>绿灯亮起时的产品数量（-1表示尚未进入绿灯阶段）</summary>
    public int ProductsWhenGreenLightOn { get; set; } = TypecPackingConstants.NotInGreenLightStage;
    
    /// <summary>
    /// 进入每个产品循环时的箱内产品基准数
    /// - L1产品第1个: 0（只有隔板）
    /// - L1产品第2-16个: 前一产品放入后的箱内产品数
    /// - L2产品第1个: 16（第一层16个产品已完成）
    /// - L2产品第2-16个: 前一产品放入后的箱内产品数
    /// </summary>
    public int ProductsInBoxBaseline { get; set; }
    
    /// <summary>子步骤超时帧数</summary>
    public int SubStepTimeout { get; set; }
    
    /// <summary>步骤完成标记</summary>
    public bool StepBCompleted { get; set; }  // 第1层隔板
    public bool StepDCompleted { get; set; }  // 第2层隔板
    
    /// <summary>当前层已放入的产品数量</summary>
    public int CurrentLayerProductCount { get; set; }
    
    /// <summary>当前会话是否已发送过通知（避免重复发送）</summary>
    public bool HasNotifiedThisSession { get; set; }
    
    /// <summary>上一帧的状态（用于检测状态变化）</summary>
    public TypecPackingStep LastStep { get; set; } = TypecPackingStep.Idle;
    
    /// <summary>
    /// 重置上下文
    /// </summary>
    public void Reset()
    {
        CurrentStep = TypecPackingStep.Idle;
        ConsistentFrames = 0;
        OkCount = 0;
        NgCount = 0;
        CurrentProductsInBox = 0;
        LastProductsInBox = 0;
        LastGreenLight = false;
        ProductsWhenGreenLightOn = TypecPackingConstants.NotInGreenLightStage;
        ProductsInBoxBaseline = 0;
        SubStepTimeout = 0;
        CycleSubStep = ProductCycleSubStep.WaitingForWeighing;
        StepBCompleted = false;
        StepDCompleted = false;
        CurrentLayerProductCount = 0;
        HasNotifiedThisSession = false;
        LastStep = TypecPackingStep.Idle;
    }
}

/// <summary>
/// Typec包装算法常量
/// </summary>
public static class TypecPackingConstants
{
    /// <summary>防抖帧数</summary>
    public const int DebounceFrames = 3;
    
    /// <summary>每层产品数量</summary>
    public const int ProductsPerLayer = 16;
    
    /// <summary>总产品数量</summary>
    public const int TotalProducts = 32;
    
    /// <summary>表示尚未进入绿灯阶段（用于跳工序检测）</summary>
    public const int NotInGreenLightStage = -1;
    
    /// <summary>绿灯刚亮起，产品尚未放入（用于待放入阶段记录箱内数）</summary>
    public const int GreenLightJustOn = -2;
    
    /// <summary>
    /// 绿灯熄灭时是否检查产品是否放入纸箱
    /// true: 绿灯熄灭时，如果产品未放入纸箱则NG
    /// false: 绿灯熄灭时，不检查产品是否放入纸箱（忽略）
    /// </summary>
    public const bool CheckProductPlacedWhenGreenLightOff = true;
    
    /// <summary>称重超时帧数（600秒 @24fps = 14400帧）</summary>
    public const int WeighingTimeoutFrames = 14400;
    
    /// <summary>绿灯等待超时帧数（600秒 @24fps = 14400帧）</summary>
    public const int GreenLightTimeoutFrames = 14400;
    
    /// <summary>放入纸箱超时帧数（600秒 @24fps = 14400帧）</summary>
    public const int PlacementTimeoutFrames = 14400;
}
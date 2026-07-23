using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Algorithm.TypecPacking;

/// <summary>
/// Typec包装算法状态机
/// 工序流程：
/// 1. 初始化(空箱) 
/// 2. L1隔板 
/// 3. L1产品(16次循环: 产品称重→绿灯→放入纸箱)
/// 4. L2隔板
/// 5. L2产品(16次循环: 产品称重→绿灯→放入纸箱)
/// 6. 关箱(封箱) → 完成
/// </summary>
public class TypecPackingStateMachine
{
    private readonly TypecPackingContext _context;
    private readonly ILogger _logger;
    
    public TypecPackingStateMachine(TypecPackingContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public void Update(TypecPackingFrameInput input)
    {
        var stateChanged = false;
        
        switch (_context.CurrentStep)
        {
            case TypecPackingStep.Idle:
                HandleIdleState(input, ref stateChanged);
                break;
            case TypecPackingStep.Initializing:
                HandleInitializingState(input, ref stateChanged);
                break;
            case TypecPackingStep.PlaceFirstLayerDivider:
                HandlePlaceFirstLayerDividerState(input, ref stateChanged);
                break;
            case TypecPackingStep.FirstLayerProducts:
                // 第一层产品循环（16次：称重→绿灯→放入纸箱）
                HandleFristProductCycleState(input, ref stateChanged, isFirstLayer: true);
                break;
            case TypecPackingStep.PlaceSecondLayerDivider:
                HandlePlaceSecondLayerDividerState(input, ref stateChanged);
                break;
            case TypecPackingStep.SecondLayerProducts:
                // 第二层产品循环（16次：称重→绿灯→放入纸箱）
                HandleSecdProductCycleState(input, ref stateChanged, isFirstLayer: false);
                break;
            case TypecPackingStep.CloseBox:
                HandleCloseBoxState(input, ref stateChanged);
                break;
            case TypecPackingStep.Completed:
            case TypecPackingStep.NG:
                break;
        }
        
        if (!stateChanged)
            _context.ConsistentFrames++;
        
        _context.LastProductsInBox = input.ProductsInBoxCount;
        _context.LastGreenLight = input.HasGreenLight;
    }
    
    private void HandleIdleState(TypecPackingFrameInput input, ref bool stateChanged)
    {
        _context.CurrentStep = TypecPackingStep.Initializing;
        _context.ConsistentFrames = 0;
        stateChanged = true;
        _logger.LogInformation("[Typec_packing] 1. 初始化（空箱）");
    }
    
    private void HandleInitializingState(TypecPackingFrameInput input, ref bool stateChanged)
    {
        if (input.HasEmptyBox)
        {
            _context.ConsistentFrames++;
            if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames)
            {
                _context.CurrentStep = TypecPackingStep.PlaceFirstLayerDivider;
                _context.ConsistentFrames = 0;
                stateChanged = true;
                _logger.LogInformation("[Typec_packing] 1. 初始化完成，检测到空箱 → 2.放第1层隔板");
            }
        }
        else
            _context.ConsistentFrames = 0;
    }
    
    private void HandlePlaceFirstLayerDividerState(TypecPackingFrameInput input, ref bool stateChanged)
    {
        if (input.HasDividerInBox)
        {
            _context.ConsistentFrames++;
            if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames && !_context.StepBCompleted)
            {
                _context.StepBCompleted = true;
                _context.CurrentStep = TypecPackingStep.FirstLayerProducts;
                _context.CycleSubStep = ProductCycleSubStep.WaitingForWeighing;
                _context.ConsistentFrames = 0;
                _context.CurrentLayerProductCount = 0;
                _context.ProductsInBoxBaseline = input.ProductsInBoxCount; // L1第1个产品的基准
                stateChanged = true;
                _logger.LogInformation("[Typec_packing] 2.L1隔板完成 → 3.L1产品工序（16次循环）");
            }
        }
        else
            _context.ConsistentFrames = 0;
    }
    
    /// <summary>
    /// 产品称重循环状态处理（第一层和第二层共用）
    /// 
    /// 工序流程：每个产品依次执行（产品 → 电子称亮绿灯 → 产品放入纸箱）
    /// - 工序A: 产品放到称重区
    /// - 工序B: 等待电子秤亮绿灯
    /// - 工序C: 产品放入纸箱
    /// 共16次循环后工序完成，否则NG
    /// 
    /// 跳工序NG检测：
    /// - 待称重时：产品放入箱内（跳过了称重和绿灯）→ NG
    /// - 待绿灯时：产品从称重区消失（拿起未称重产品）→ NG
    /// - 待绿灯时：产品放入箱内（没等绿灯）→ NG
    /// - 待放入时：绿灯熄灭（产品被拿走未放入）→ NG
    /// </summary>
    private void HandleFristProductCycleState(TypecPackingFrameInput input, ref bool stateChanged, bool isFirstLayer)
    {
        var productsInBox = input.ProductsInBoxCount;
        var layerName = isFirstLayer ? "第1层" : "第2层";
        
        switch (_context.CycleSubStep)
        {
            case ProductCycleSubStep.WaitingForWeighing:
                // ===== 工序A: 产品放到称重区 =====
                
                // 进入待称重阶段时，记录箱内产品基准数（用于跳工序检测）
                if (_context.ConsistentFrames == 0)
                {
                    _context.ProductsInBoxBaseline = productsInBox;
                }
                
                // 【跳工序检测】在待称重状态下，如果箱内产品数 > 基准数
                // 说明产品跳过了称重和绿灯直接放入纸箱
                if (productsInBox > _context.ProductsInBoxBaseline)
                {
                    TriggerNG($"{layerName}产品跳工序：未经过称重和绿灯直接放入纸箱", ref stateChanged);
                    break;
                }
                
                // 检测产品是否放到称重区
                if (input.HasProductOnScale)
                {
                    _context.ConsistentFrames++;
                    if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames)
                    {
                        _context.CycleSubStep = ProductCycleSubStep.WaitingForGreenLight;
                        _context.ConsistentFrames = 0;
                        _context.ProductsWhenGreenLightOn = TypecPackingConstants.NotInGreenLightStage;
                        stateChanged = true;
                        _logger.LogInformation("[Typec_packing] {Layer}产品放到称重区，等待绿灯亮起 ({Count}/16)", 
                            layerName, _context.CurrentLayerProductCount + 1);
                    }
                }
                else
                    _context.ConsistentFrames = 0;
                break;
                
            case ProductCycleSubStep.WaitingForGreenLight:
                // ===== 工序B: 等待电子秤亮绿灯 =====
                
                // 【跳工序检测】在待绿灯状态下，如果箱内产品数 > 基准数（没等绿灯就放入）→ NG
                if (productsInBox > _context.ProductsInBoxBaseline)
                {
                    TriggerNG($"{layerName}产品跳工序：未等待绿灯直接放入纸箱", ref stateChanged);
                    break;
                }
                
                // 检测电子秤绿灯是否亮起
                if (input.HasGreenLight)
                {
                    _context.ConsistentFrames++;
                    if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames)
                    {
                        // 绿灯亮起时，不记录箱内数，等待产品放入后再记录
                        // 记录绿灯已亮起（用于后续检测绿灯熄灭）
                        _context.ProductsWhenGreenLightOn = TypecPackingConstants.GreenLightJustOn;
                        _context.CycleSubStep = ProductCycleSubStep.WaitingForPlacement;
                        _context.ConsistentFrames = 0;
                        stateChanged = true;
                        _logger.LogInformation("[Typec_packing] {Layer}绿灯亮起，产品合格，可以放入纸箱", layerName);
                    }
                }
                else
                    _context.ConsistentFrames = 0;
                break;
                
            case ProductCycleSubStep.WaitingForPlacement:
                // ===== 工序C: 产品放入纸箱 =====
                
                // 检测产品是否放入纸箱（箱内产品数量增加）
                if (productsInBox > _context.ProductsInBoxBaseline)
                {
                    _context.ConsistentFrames++;
                    
                    // 绿灯刚亮时，产品放入后记录箱内数
                    if (_context.ProductsWhenGreenLightOn == TypecPackingConstants.GreenLightJustOn)
                    {
                        _context.ProductsWhenGreenLightOn = productsInBox;
                    }
                    
                    if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames)
                    {
                        // 当前层产品数量+1
                        _context.CurrentLayerProductCount++;
                        _logger.LogInformation("当前包装数量："+_context.CurrentLayerProductCount.ToString());

                        // 检查是否完成16个产品
                        if (_context.CurrentLayerProductCount >= TypecPackingConstants.ProductsPerLayer)
                        {
                            // 16个产品已完成，当前层工序完成
                            if (isFirstLayer)
                            {
                                // L1产品工序完成，进入L2隔板工序
                                _context.CurrentStep = TypecPackingStep.PlaceSecondLayerDivider;
                                _context.ConsistentFrames = 0;
                               // _context.CurrentLayerProductCount = 0;
                                _context.ProductsInBoxBaseline = productsInBox; // L2第1个产品的基准
                                
                                _logger.LogInformation("[Typec_packing] 3.L1产品工序完成（16个） → 4.L2隔板");
                            }
                            else
                            {
                                // L2产品工序完成，进入关箱工序
                                _context.CurrentStep = TypecPackingStep.CloseBox;
                                _context.ConsistentFrames = 0;
                               // _context.CurrentLayerProductCount = 0;
                                _logger.LogInformation("[Typec_packing] 5.L2产品工序完成（16个） → 6.关箱");
                            }
                            
                            // 重置子工序状态
                            _context.CycleSubStep = ProductCycleSubStep.WaitingForWeighing;
                            _context.ProductsWhenGreenLightOn = TypecPackingConstants.NotInGreenLightStage;
                            stateChanged = true;
                        }
                        //else
                        //{
                        //    // 继续下一个产品循环
                        //    _context.CycleSubStep = ProductCycleSubStep.WaitingForWeighing;
                        //    _context.ConsistentFrames = 0;
                        //    _context.ProductsWhenGreenLightOn = TypecPackingConstants.NotInGreenLightStage;
                        //    // 更新基准数为当前箱内产品数（下一个产品的起点）
                        //    _context.ProductsInBoxBaseline = productsInBox;
                        //    stateChanged = true;
                        //    _logger.LogInformation("[Typec_packing] {Layer}产品已放入纸箱 ({Count}/16)", 
                        //        layerName, _context.CurrentLayerProductCount);
                        //}
                    }
                }
                //else
                //{
                //    _context.ConsistentFrames = 0;
                    
                //    // 绿灯熄灭时的检测（仅当配置启用且产品尚未放入时检查）
                //    if (TypecPackingConstants.CheckProductPlacedWhenGreenLightOff && 
                //        productsInBox <= _context.ProductsInBoxBaseline)
                //    {
                //        // 【跳工序检测】绿灯熄灭且产品未放入箱内 → NG
                //        TriggerNG($"{layerName}产品跳工序：绿灯熄灭，产品未放入纸箱", ref stateChanged);
                //        break;
                //    }
                //}
                break;
        }
    }


    private void HandleSecdProductCycleState(TypecPackingFrameInput input, ref bool stateChanged, bool isFirstLayer)
    {
        var productsInBox = input.ProductsInBoxCount;
        var layerName = isFirstLayer ? "第1层" : "第2层";

        switch (_context.CycleSubStep)
        {
            case ProductCycleSubStep.WaitingForWeighing:
                // ===== 工序A: 产品放到称重区 =====

                // 进入待称重阶段时，记录箱内产品基准数（用于跳工序检测）
                if (_context.ConsistentFrames == 0)
                {
                    _context.ProductsInBoxBaseline = productsInBox;
                }

                // 【跳工序检测】在待称重状态下，如果箱内产品数 > 基准数
                // 说明产品跳过了称重和绿灯直接放入纸箱
                //if (productsInBox > _context.ProductsInBoxBaseline)
                //{
                //    TriggerNG($"{layerName}产品跳工序：未经过称重和绿灯直接放入纸箱", ref stateChanged);
                //    break;
                //}

                // 检测产品是否放到称重区
                if (input.HasProductOnScale)
                {
                    _context.ConsistentFrames++;
                    if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames)
                    {
                        _context.CycleSubStep = ProductCycleSubStep.WaitingForGreenLight;
                        _context.ConsistentFrames = 0;
                        _context.ProductsWhenGreenLightOn = TypecPackingConstants.NotInGreenLightStage;
                        stateChanged = true;
                        _logger.LogInformation("[Typec_packing] {Layer}产品放到称重区，等待绿灯亮起 ({Count}/16)",
                            layerName, _context.CurrentLayerProductCount + 1);
                    }
                }
                else
                    _context.ConsistentFrames = 0;
                break;

            case ProductCycleSubStep.WaitingForGreenLight:
                // ===== 工序B: 等待电子秤亮绿灯 =====

                // 【跳工序检测】在待绿灯状态下，如果箱内产品数 > 基准数（没等绿灯就放入）→ NG
                if (productsInBox > _context.ProductsInBoxBaseline)
                {
                    TriggerNG($"{layerName}产品跳工序：未等待绿灯直接放入纸箱", ref stateChanged);
                    break;
                }

                // 检测电子秤绿灯是否亮起
                if (input.HasGreenLight)
                {
                    _context.ConsistentFrames++;
                    if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames)
                    {
                        // 绿灯亮起时，不记录箱内数，等待产品放入后再记录
                        // 记录绿灯已亮起（用于后续检测绿灯熄灭）
                        _context.ProductsWhenGreenLightOn = TypecPackingConstants.GreenLightJustOn;
                        _context.CycleSubStep = ProductCycleSubStep.WaitingForPlacement;
                        _context.ConsistentFrames = 0;
                        stateChanged = true;
                        _logger.LogInformation("[Typec_packing] {Layer}绿灯亮起，产品合格，可以放入纸箱", layerName);
                    }
                }
                else
                    _context.ConsistentFrames = 0;
                break;

            case ProductCycleSubStep.WaitingForPlacement:
                // ===== 工序C: 产品放入纸箱 =====

                // 检测产品是否放入纸箱（箱内产品数量增加）
                if (productsInBox > _context.ProductsInBoxBaseline)
                {
                    _context.ConsistentFrames++;

                    // 绿灯刚亮时，产品放入后记录箱内数
                    if (_context.ProductsWhenGreenLightOn == TypecPackingConstants.GreenLightJustOn)
                    {
                        _context.ProductsWhenGreenLightOn = productsInBox;
                    }

                    if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames)
                    {
                        // 当前层产品数量+1
                        _context.CurrentLayerProductCount++;

                        // 检查是否完成16个产品
                        if (_context.CurrentLayerProductCount >= TypecPackingConstants.ProductsPerLayer)
                        {
                            // 16个产品已完成，当前层工序完成
                            if (isFirstLayer)
                            {
                                // L1产品工序完成，进入L2隔板工序
                                _context.CurrentStep = TypecPackingStep.PlaceSecondLayerDivider;
                                _context.ConsistentFrames = 0;
                                _context.CurrentLayerProductCount = 0;
                                _context.ProductsInBoxBaseline = productsInBox; // L2第1个产品的基准
                                _logger.LogInformation("[Typec_packing] 3.L1产品工序完成（16个） → 4.L2隔板");
                            }
                            else
                            {
                                // L2产品工序完成，进入关箱工序
                                _context.CurrentStep = TypecPackingStep.CloseBox;
                                _context.ConsistentFrames = 0;
                                _context.CurrentLayerProductCount = 0;
                                _logger.LogInformation("[Typec_packing] 5.L2产品工序完成（16个） → 6.关箱");
                            }

                            // 重置子工序状态
                            _context.CycleSubStep = ProductCycleSubStep.WaitingForWeighing;
                            _context.ProductsWhenGreenLightOn = TypecPackingConstants.NotInGreenLightStage;
                            stateChanged = true;
                        }
                        //else
                        //{
                        //    // 继续下一个产品循环
                        //    _context.CycleSubStep = ProductCycleSubStep.WaitingForWeighing;
                        //    _context.ConsistentFrames = 0;
                        //    _context.ProductsWhenGreenLightOn = TypecPackingConstants.NotInGreenLightStage;
                        //    // 更新基准数为当前箱内产品数（下一个产品的起点）
                        //    _context.ProductsInBoxBaseline = productsInBox;
                        //    stateChanged = true;
                        //    _logger.LogInformation("[Typec_packing] {Layer}产品已放入纸箱 ({Count}/16)", 
                        //        layerName, _context.CurrentLayerProductCount);
                        //}
                    }
                }
                //else
                //{
                //    _context.ConsistentFrames = 0;

                //    // 绿灯熄灭时的检测（仅当配置启用且产品尚未放入时检查）
                //    if (TypecPackingConstants.CheckProductPlacedWhenGreenLightOff && 
                //        productsInBox <= _context.ProductsInBoxBaseline)
                //    {
                //        // 【跳工序检测】绿灯熄灭且产品未放入箱内 → NG
                //        TriggerNG($"{layerName}产品跳工序：绿灯熄灭，产品未放入纸箱", ref stateChanged);
                //        break;
                //    }
                //}
                break;
        }
    }

    private void HandlePlaceSecondLayerDividerState(TypecPackingFrameInput input, ref bool stateChanged)
    {
        if (input.HasDividerInBox)
        {
            _context.ConsistentFrames++;
            if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames && !_context.StepDCompleted)
            {
                _context.StepDCompleted = true;
                _context.CurrentStep = TypecPackingStep.SecondLayerProducts;
                _context.CycleSubStep = ProductCycleSubStep.WaitingForWeighing;
                _context.ConsistentFrames = 0;
                _context.CurrentLayerProductCount = 0;
                _context.ProductsInBoxBaseline = input.ProductsInBoxCount; // L2第1个产品的基准
                stateChanged = true;
                _logger.LogInformation("[Typec_packing] 4.L2隔板完成 → 5.L2产品工序（16次循环）");
            }
        }
        else
            _context.ConsistentFrames = 0;
    }
    
    private void HandleCloseBoxState(TypecPackingFrameInput input, ref bool stateChanged)
    {
        if (input.HasBoxLidClosed)
        {
            _context.ConsistentFrames++;
            if (_context.ConsistentFrames >= TypecPackingConstants.DebounceFrames)
            {
                _context.CurrentStep = TypecPackingStep.Completed;
                _context.OkCount++;
                _context.ConsistentFrames = 0;
                stateChanged = true;
                _logger.LogInformation("[Typec_packing] 6. 关箱完成，包装流程完成! OK={OkCount}", _context.OkCount);
                ScheduleReset();
            }
        }
        else
            _context.ConsistentFrames = 0;
    }
    
    private void TriggerNG(string reason, ref bool stateChanged)
    {
        _context.CurrentStep = TypecPackingStep.NG;
        _context.NgCount++;
        stateChanged = true;
        _logger.LogWarning("[Typec_packing] 工序遗漏NG: {Reason}, NG计数: {NgCount}", reason, _context.NgCount);
        ScheduleReset();
    }
    
    private void ScheduleReset()
    {
        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(2000);
                _context.Reset();
                _logger.LogInformation("[Typec_packing] 自动重置到初始化状态");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Typec_packing] 自动重置失败");
            }
        });
    }
    
    public void Reset()
    {
        _context.Reset();
        _logger.LogInformation("[Typec_packing] 手动强制重置到初始化状态");
    }
}
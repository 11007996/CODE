using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Algorithm.Results;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Inference.Results;
using LuxVideoDet.Core.Notification;
using LuxVideoDet.Core.Region;
using LuxVideoDet.Core.Utils;
using Microsoft.Extensions.Logging;
using OpenCvSharp;
using SysPoint = System.Drawing.Point;

namespace LuxVideoDet.Core.Algorithm.TypecPacking;

/// <summary>
/// Typec包装流水线目检测算法
/// 检测产品称重和包装两个关键操作
/// 
/// 工序流程：
/// 1、初始化（空箱） → 2、箱内放第1层隔板 → 3、产品称重→放入纸箱（16次） 
/// → 4、箱内放第2层隔板 → 5、产品称重→放入纸箱（16次） → 6、关箱（封箱）
/// 
/// 检测对象：空箱、隔板、产品、产品隔板、电子秤绿灯、封箱
/// 区域：称重区、纸箱
/// </summary>
public sealed class TypecPackingAlgorithm : DetectionAlgorithmBase
{
    private readonly TypecPackingContext _context = new();
    private TypecPackingStateMachine _stateMachine = null!;

    private const string BoxAreaName = "BoxArea";       // 纸箱区域
    private const string ScaleAreaName = "ScaleArea";   // 电子秤区域

    public override string AlgorithmType => "Typec_packing";

    public override List<string> GetDefaultClasses() => new()
    {
        TypecPackingDetectionClass.BOX,              // 0 空箱
        TypecPackingDetectionClass.DIVIDER,          // 1 隔板
        TypecPackingDetectionClass.PRODUCT,          // 2 产品
        TypecPackingDetectionClass.PRODUCT_DIVIDER,  // 3 产品隔板
        TypecPackingDetectionClass.SCALE_GREEN_LIGHT, // 4 电子秤绿灯
        TypecPackingDetectionClass.BOX_LID           // 5 封箱
    };

    protected override IReadOnlyList<AlgorithmDetectionClassBinding>? GetDetectionClassBindings()
        => TypecPackingDetectionBindings.GetDefaultBindings();

    public TypecPackingAlgorithm(
        IInferenceEngine inferenceEngine,
        RegionManager regionManager,
        List<INotificationService> notifiers,
        ILogger logger)
        : base(inferenceEngine, regionManager, notifiers, logger)
    {
    }

    protected override void OnInitialize()
    {
        _stateMachine = new TypecPackingStateMachine(_context, _logger);

        _logger.LogInformation(
            "[Typec_packing] 初始化完成. 类别: 空箱={Box}, 隔板={Divider}, 产品={Product}, 产品隔板={ProductDivider}, 绿灯={Green}, 封箱={Lid}",
            TryGetResolvedClassId(TypecPackingLogicalClass.Box, out var boxId) ? boxId : -1,
            TryGetResolvedClassId(TypecPackingLogicalClass.Divider, out var dividerId) ? dividerId : -1,
            TryGetResolvedClassId(TypecPackingLogicalClass.Product, out var productId) ? productId : -1,
            TryGetResolvedClassId(TypecPackingLogicalClass.ProductDivider, out var pdId) ? pdId : -1,
            TryGetResolvedClassId(TypecPackingLogicalClass.ScaleGreenLight, out var greenId) ? greenId : -1,
            TryGetResolvedClassId(TypecPackingLogicalClass.BoxLid, out var lidId) ? lidId : -1);
    }

    // 工序步骤统一配置，和DrawPipelineChecklist共用
    private static readonly List<(string Text, TypecPackingStep StepEnum)> _allPackSteps = new()
{
    ("1. 初始化(空箱)", TypecPackingStep.Initializing),
    ("2. L1隔板", TypecPackingStep.PlaceFirstLayerDivider),
    ("3. L1产品(16)", TypecPackingStep.FirstLayerProducts),
    ("4. L2隔板", TypecPackingStep.PlaceSecondLayerDivider),
    ("5. L2产品(16)", TypecPackingStep.SecondLayerProducts),
    ("6. 关箱(封箱)", TypecPackingStep.CloseBox)
};

    /// 根据当前步骤生成步骤渲染信息（前缀、十六进制颜色）
    private static void ResolveStepStatus(TypecPackingStep currentStep, TypecPackingStep stepEnum, out string prefix, out string colorHex, out Scalar cvColor)
    {
        if (currentStep > stepEnum)
        {
            prefix = "OK";
            colorHex = "#00C800";
            cvColor = new Scalar(0, 200, 0); // BGR绿色
        }
        else if (currentStep == stepEnum)
        {
            prefix = ">>";
            colorHex = "#FFFF00"; // 你要的亮黄RGB
            cvColor = new Scalar(0, 255, 255); // 原有画面青色保持不变，不改动预览画面观感
        }
        else
        {
            prefix = "--";
            colorHex = "#646464";
            cvColor = new Scalar(100, 100, 100);
        }
    }

    protected override DetectionResult ProcessDetections(
        Mat rawFrame,
        Mat annotatedFrame,
        List<Detection> detections,
        DetectionResult result)
    {
        var boxArea = _regionManager.FindRegionByName(BoxAreaName);
        var scaleArea = _regionManager.FindRegionByName(ScaleAreaName);

        var frameInput = CreateFrameInput(detections, boxArea, scaleArea);
        _stateMachine.Update(frameInput);

        DrawTaskDetections(annotatedFrame, detections);
        //DrawPipelineChecklist(annotatedFrame, rawFrame.Width, rawFrame.Height);
        DrawCurrentStepInfo(annotatedFrame);
        DrawRegions(annotatedFrame, boxArea, scaleArea);
        BuildResult(result, frameInput);

        return result;
    }

    private TypecPackingFrameInput CreateFrameInput(
        List<Detection> detections,
        Core.Region.Region? boxArea,
        Core.Region.Region? scaleArea)
    {
        var input = new TypecPackingFrameInput();

        if (boxArea != null)
        {
            var boxId = RequireResolvedClassId(TypecPackingLogicalClass.Box);
            var dividerId = RequireResolvedClassId(TypecPackingLogicalClass.Divider);
            var productId = RequireResolvedClassId(TypecPackingLogicalClass.Product);
            var productDividerId = RequireResolvedClassId(TypecPackingLogicalClass.ProductDivider);
            var boxLidId = RequireResolvedClassId(TypecPackingLogicalClass.BoxLid);

            // 检测空箱
            var emptyBox = detections
                .Where(d => d.ClassId == boxId && boxArea.ContainsPoint(d.BoundingBox.CenterX, d.BoundingBox.CenterY))
                .ToList();
            input.HasEmptyBox = emptyBox.Any();

            // 检测隔板
            var dividerInBox = detections
                .Where(d => d.ClassId == dividerId && boxArea.ContainsPoint(d.BoundingBox.CenterX, d.BoundingBox.CenterY))
                .ToList();
            input.HasDividerInBox = dividerInBox.Any();
            input.DividerInBox = dividerInBox.FirstOrDefault();

            // 检测产品隔板
            var productDividerInBox = detections
                .Where(d => d.ClassId == productDividerId && boxArea.ContainsPoint(d.BoundingBox.CenterX, d.BoundingBox.CenterY))
                .ToList();
            input.HasProductDividerInBox = productDividerInBox.Any();
            input.ProductDividerInBox = productDividerInBox.FirstOrDefault();

            // 检测纸箱内的产品数量（产品+产品隔板）
            var productsInBox = detections
                .Where(d => (d.ClassId == productId || d.ClassId == productDividerId)
                         && boxArea.ContainsPoint(d.BoundingBox.CenterX, d.BoundingBox.CenterY))
                .ToList();
            input.ProductsInBoxCount = productsInBox.Count;

            // 检测封箱
            var boxLidClosed = detections
                .Where(d => d.ClassId == boxLidId && boxArea.ContainsPoint(d.BoundingBox.CenterX, d.BoundingBox.CenterY))
                .ToList();
            input.HasBoxLidClosed = boxLidClosed.Any();
        }

        if (scaleArea != null)
        {
            var greenLightId = RequireResolvedClassId(TypecPackingLogicalClass.ScaleGreenLight);
            var productId = RequireResolvedClassId(TypecPackingLogicalClass.Product);

            // 检测绿灯
            var greenLight = detections
                .Where(d => d.ClassId == greenLightId && scaleArea.ContainsPoint(d.BoundingBox.CenterX, d.BoundingBox.CenterY))
                .ToList();
            input.HasGreenLight = greenLight.Any();

            // 检测称重区产品
            var productOnScale = detections
                .Where(d => d.ClassId == productId && scaleArea.ContainsPoint(d.BoundingBox.CenterX, d.BoundingBox.CenterY))
                .ToList();
            input.HasProductOnScale = productOnScale.Any();
            input.ProductOnScale = productOnScale.FirstOrDefault();
        }

        _context.CurrentProductsInBox = input.ProductsInBoxCount;
        return input;
    }

    private void DrawRegions(Mat frame, Core.Region.Region? boxArea, Core.Region.Region? scaleArea)
    {
        if (boxArea != null && boxArea.Points.Count >= 3)
        {
            var points = boxArea.Points.Select(p => new Point(p.X, p.Y)).ToArray();
            Cv2.Polylines(frame, new[] { points }, true, new Scalar(0, 200, 0), 2, LineTypes.AntiAlias);
            var centerX = (int)boxArea.Points.Average(p => p.X);
            var centerY = (int)boxArea.Points.Average(p => p.Y);
            ChineseTextRenderer.DrawChineseText(frame, "纸箱区域", new SysPoint(centerX - 30, centerY),
                14, new Scalar(0, 200, 0), 4, 100, "center");
        }

        if (scaleArea != null && scaleArea.Points.Count >= 3)
        {
            var points = scaleArea.Points.Select(p => new Point(p.X, p.Y)).ToArray();
            Cv2.Polylines(frame, new[] { points }, true, new Scalar(200, 200, 0), 2, LineTypes.AntiAlias);
            var centerX = (int)scaleArea.Points.Average(p => p.X);
            var centerY = (int)scaleArea.Points.Average(p => p.Y);
            ChineseTextRenderer.DrawChineseText(frame, "称重区", new SysPoint(centerX - 30, centerY),
                14, new Scalar(200, 200, 0), 4, 100, "center");
        }
    }

    private void DrawPipelineChecklist(Mat frame, int frameWidth, int frameHeight)
    {
        var startX = 20;
        var startY = 30;
        var lineHeight = 28;

        for (int i = 0; i < _allPackSteps.Count; i++)
        {
            var (text, stepEnum) = _allPackSteps[i];
            ResolveStepStatus(_context.CurrentStep, stepEnum, out var prefix, out _, out var color);
            var y = startY + i * lineHeight;

            var displayText = prefix + " " + text;
            ChineseTextRenderer.DrawChineseText(frame, displayText, new SysPoint(startX, y),
                14, color, 4, 200, "left");
        }
    }

    private void DrawCurrentStepInfo(Mat frame)
    {
        var startX = 350;
        var startY = 30;
        var lineHeight = 28;

        var stateText = "State: " + GetStepDescription(_context.CurrentStep);
        ChineseTextRenderer.DrawChineseText(frame, stateText, new SysPoint(startX, startY),
            16, new Scalar(255, 255, 0), 4, 300, "left");

        var debounceText = "Debounce: " + _context.ConsistentFrames + "/" + TypecPackingConstants.DebounceFrames;
        ChineseTextRenderer.DrawChineseText(frame, debounceText, new SysPoint(startX, startY + lineHeight),
            16, new Scalar(200, 200, 0), 4, 300, "left");

        if (_context.CurrentStep == TypecPackingStep.FirstLayerProducts ||
            _context.CurrentStep == TypecPackingStep.SecondLayerProducts)
        {
            var productsText = "Products: " + _context.CurrentLayerProductCount + "/" + TypecPackingConstants.ProductsPerLayer;
            ChineseTextRenderer.DrawChineseText(frame, productsText, new SysPoint(startX, startY + lineHeight * 2),
                16, new Scalar(255, 150, 0), 4, 300, "left");
        }

        if (_context.CurrentStep == TypecPackingStep.FirstLayerProducts ||
            _context.CurrentStep == TypecPackingStep.SecondLayerProducts)
        {
            var subStepText = "SubStep: " + GetCycleSubStepDescription(_context.CycleSubStep);
            ChineseTextRenderer.DrawChineseText(frame, subStepText, new SysPoint(startX, startY + lineHeight * 3),
                16, new Scalar(150, 150, 255), 4, 300, "left");
        }

        var okText = "OK: " + _context.OkCount;
        var ngText = "NG: " + _context.NgCount;
        ChineseTextRenderer.DrawChineseText(frame, okText, new SysPoint(startX, startY + lineHeight * 4),
            16, new Scalar(0, 255, 0), 4, 150, "left");
        ChineseTextRenderer.DrawChineseText(frame, ngText, new SysPoint(startX + 100, startY + lineHeight * 4),
            16, new Scalar(0, 0, 255), 4, 150, "left");
    }

    private void BuildResult(DetectionResult result, TypecPackingFrameInput input)
    {
        result.CurrentState = GetStepDescription(_context.CurrentStep);

        result.ExtraData["CurrentStep"] = _context.CurrentStep.ToString();
        result.ExtraData["ConsistentFrames"] = _context.ConsistentFrames;
        result.ExtraData["CurrentProductsInBox"] = _context.CurrentProductsInBox;
        result.ExtraData["CycleSubStep"] = _context.CycleSubStep.ToString();
        result.ExtraData["OkCount"] = _context.OkCount;
        result.ExtraData["NgCount"] = _context.NgCount;

        result.ExtraData["HasEmptyBox"] = input.HasEmptyBox;
        result.ExtraData["HasDividerInBox"] = input.HasDividerInBox;
        result.ExtraData["HasProductDividerInBox"] = input.HasProductDividerInBox;
        result.ExtraData["HasProductOnScale"] = input.HasProductOnScale;
        result.ExtraData["HasGreenLight"] = input.HasGreenLight;
        result.ExtraData["HasBoxLidClosed"] = input.HasBoxLidClosed;
        result.ExtraData["ProductsInBoxCount"] = input.ProductsInBoxCount;

        // ========== 新增：流水线步骤全量数据塞入ExtraData ==========
        var stepTexts = new List<string>();
        var stepPrefixes = new List<string>();
        var stepColors = new List<string>();
        foreach (var (text, stepEnum) in _allPackSteps)
        {
            ResolveStepStatus(_context.CurrentStep, stepEnum, out var prefix, out var hex, out _);
            stepTexts.Add(text);
            stepPrefixes.Add(prefix);
            stepColors.Add(hex);
        }
        // 存入字典，桌面层直接读取三组数组
        result.ExtraData["StepTexts"] = stepTexts;
        result.ExtraData["StepPrefixes"] = stepPrefixes;
        result.ExtraData["StepColorHexes"] = stepColors;
        // ==========================================================

        // 检测状态变化，重置通知标志
        if (_context.LastStep != _context.CurrentStep)
        {
            _context.HasNotifiedThisSession = false;
            _context.LastStep = _context.CurrentStep;
        }

        if (_context.CurrentStep == TypecPackingStep.Completed)
        {
            if (!_context.HasNotifiedThisSession)
            {
                result.Judgement = ProductionJudgement.OK;
                result.StateMessage = "包装流程完成";
                result.ShouldNotify = true;
                result.NotificationLevel = NotificationLevel.Info;
                result.NotificationTitle = AlgorithmType;
                result.NotificationMessage = "包装流程完成";
                _context.HasNotifiedThisSession = true;
            }
            else
            {
                result.Judgement = ProductionJudgement.OK;
                result.StateMessage = "包装流程完成";
                result.ShouldNotify = false;  // 已通知过，不再发送
            }
        }
        else if (_context.CurrentStep == TypecPackingStep.NG)
        {
            if (!_context.HasNotifiedThisSession)
            {
                result.Judgement = ProductionJudgement.NG;
                result.StateMessage = "工序遗漏";
                result.ShouldNotify = true;
                result.ShouldSaveImage = true;
                result.NotificationLevel = NotificationLevel.Error;
                result.NotificationTitle = AlgorithmType + " NG";
                result.NotificationMessage = "工序遗漏，请检查";
                _context.HasNotifiedThisSession = true;
            }
            else
            {
                result.Judgement = ProductionJudgement.NG;
                result.StateMessage = "工序遗漏";
                result.ShouldNotify = false;  // 已通知过，不再发送
                result.ShouldSaveImage = false;
            }
        }
        else
        {
            result.Judgement = ProductionJudgement.None;
        }
    }

    public override void Reset()
    {
        _stateMachine.Reset();
        _logger.LogInformation("[Typec_packing] 手动强制重置到初始化状态");
    }

    /// <summary>
    /// 重置生产统计数据（OK/NG计数归零）
    /// </summary>
    public override void ResetStatistics()
    {
        _context.OkCount = 0;
        _context.NgCount = 0;
        _logger.LogInformation("[Typec_packing] 生产统计数据已归零: OK=0, NG=0");
    }

    public override string GetCurrentState()
    {
        return $"{GetStepDescription(_context.CurrentStep)} | OK:{_context.OkCount} NG:{_context.NgCount}";
    }

    private static string GetStepDescription(TypecPackingStep step) => step switch
    {
        TypecPackingStep.Idle => "IDLE",
        TypecPackingStep.Initializing => "初始化",
        TypecPackingStep.PlaceFirstLayerDivider => "L1隔板",
        TypecPackingStep.FirstLayerProducts => "L1产品",
        TypecPackingStep.PlaceSecondLayerDivider => "L2隔板",
        TypecPackingStep.SecondLayerProducts => "L2产品",
        TypecPackingStep.CloseBox => "关箱",
        TypecPackingStep.Completed => "OK",
        TypecPackingStep.NG => "NG",
        _ => "UNKNOWN"
    };

    private static string GetCycleSubStepDescription(ProductCycleSubStep subStep) => subStep switch
    {
        ProductCycleSubStep.WaitingForWeighing => "待称重",
        ProductCycleSubStep.WaitingForGreenLight => "待绿灯",
        ProductCycleSubStep.WaitingForPlacement => "待放入纸箱",
        _ => "UNKNOWN"
    };
}
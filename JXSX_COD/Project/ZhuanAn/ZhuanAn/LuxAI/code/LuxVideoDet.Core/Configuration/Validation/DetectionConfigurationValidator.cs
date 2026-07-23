using FluentValidation;
using LuxVideoDet.Core.Configuration.Models;

namespace LuxVideoDet.Core.Configuration.Validation;

/// <summary>
/// 检测配置验证器 - 使用 FluentValidation 进行强类型验证
/// </summary>
public class DetectionConfigurationValidator : AbstractValidator<DetectionConfiguration>
{
    public DetectionConfigurationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("配置 ID 不能为空");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("配置名称不能为空")
            .MaximumLength(100).WithMessage("配置名称不能超过 100 个字符");

        RuleFor(x => x.VideoSource)
            .NotNull().WithMessage("视频源配置不能为空")
            .SetValidator(new VideoSourceConfigValidator());

        RuleFor(x => x.Algorithms)
            .NotEmpty().WithMessage("至少需要配置一个算法");

        RuleForEach(x => x.Algorithms)
            .SetValidator(new AlgorithmConfigValidator());
    }
}

/// <summary>
/// 视频源配置验证器
/// </summary>
public class VideoSourceConfigValidator : AbstractValidator<VideoSourceConfig>
{
    public VideoSourceConfigValidator()
    {
        RuleFor(x => x.Source)
            .NotEmpty().WithMessage("视频源地址不能为空");

        RuleFor(x => x.Source)
            .Must(File.Exists).WithMessage("视频文件不存在")
            .When(x => x.Type == VideoSourceType.LocalVideo);

        RuleFor(x => x.ReconnectInterval)
            .GreaterThan(0).WithMessage("重连间隔必须大于 0")
            .When(x => x.Type == VideoSourceType.Rtsp);

        RuleFor(x => x.Timeout)
            .GreaterThan(0).WithMessage("超时时间必须大于 0")
            .When(x => x.Type == VideoSourceType.Rtsp);
    }
}

/// <summary>
/// 推理配置验证器
/// </summary>
public class InferenceConfigValidator : AbstractValidator<InferenceConfig>
{
    public InferenceConfigValidator()
    {
        RuleFor(x => x.ModelPath)
            .NotEmpty().WithMessage("模型路径不能为空")
            .Must(File.Exists).WithMessage("模型文件不存在");

        RuleFor(x => x.ConfidenceThreshold)
            .InclusiveBetween(0f, 1f).WithMessage("置信度阈值必须在 0 到 1 之间");

        RuleFor(x => x.IouThreshold)
            .InclusiveBetween(0f, 1f).WithMessage("IOU 阈值必须在 0 到 1 之间");

        RuleFor(x => x.InputSize.Width)
            .GreaterThan(0).WithMessage("输入宽度必须大于 0");

        RuleFor(x => x.InputSize.Height)
            .GreaterThan(0).WithMessage("输入高度必须大于 0");

        RuleFor(x => x.Classes)
            .NotEmpty().WithMessage("类别列表不能为空");
    }
}

/// <summary>
/// 区域配置验证器
/// </summary>
public class RegionConfigValidator : AbstractValidator<RegionConfig>
{
    public RegionConfigValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("区域名称不能为空");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("区域显示名称不能为空");

        RuleFor(x => x.Points)
            .NotEmpty().WithMessage("区域点集合不能为空")
            .Must(points => points.Count >= 3).WithMessage("区域至少需要 3 个点");

        RuleFor(x => x.Color)
            .Must(BeValidOptionalRegionColor)
            .WithMessage("颜色须为空、inherit，或 #RRGGBB");
    }

    private static bool BeValidOptionalRegionColor(string? color)
    {
        if (string.IsNullOrWhiteSpace(color)) return true;
        var t = color.Trim();
        if (t.Equals("inherit", StringComparison.OrdinalIgnoreCase)) return true;
        return System.Text.RegularExpressions.Regex.IsMatch(t, "^#[0-9A-Fa-f]{6}$");
    }
}

/// <summary>
/// 存储配置验证器
/// </summary>
public class StorageConfigValidator : AbstractValidator<StorageConfig>
{
    public StorageConfigValidator()
    {
        RuleFor(x => x.VideoDuration)
            .GreaterThan(0).WithMessage("视频时长必须大于 0")
            .When(x => x.SaveVideo);

        RuleFor(x => x.RecordingMaxWidth)
            .GreaterThanOrEqualTo(0).WithMessage("RecordingMaxWidth 不能为负数")
            .LessThanOrEqualTo(7680).WithMessage("RecordingMaxWidth 不能超过 7680")
            .Must(w => w == 0 || w >= 320).WithMessage("RecordingMaxWidth 若非 0 则须至少 320")
            .When(x => x.SaveVideo);

        RuleFor(x => x.MaxConcurrentRecordings)
            .GreaterThanOrEqualTo(1).WithMessage("MaxConcurrentRecordings 至少为 1")
            .LessThanOrEqualTo(8).WithMessage("MaxConcurrentRecordings 不能超过 8")
            .When(x => x.SaveVideo);

        RuleFor(x => x.RetentionDays)
            .GreaterThanOrEqualTo(0).WithMessage("保留天数不能为负数");
    }
}

/// <summary>
/// 通知配置验证器
/// </summary>
public class NotificationConfigValidator : AbstractValidator<NotificationConfig>
{
    public NotificationConfigValidator()
    {
        RuleForEach(x => x.Notifiers)
            .SetValidator(new NotifierConfigValidator())
            .When(x => x.Enabled);
    }
}

/// <summary>
/// 单个通知服务配置验证器
/// </summary>
public class NotifierConfigValidator : AbstractValidator<NotifierConfig>
{
    public NotifierConfigValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("通知类型不能为空");

        RuleFor(x => x.Parameters)
            .NotEmpty().WithMessage("通知参数不能为空")
            .When(x => x.Enabled && !IsSpeakerNotifier(x.Type));

        // Webhook 参数验证
        RuleFor(x => x.Parameters)
            .Must(p => p.ContainsKey("businessWebhook") && !string.IsNullOrEmpty(p["businessWebhook"]))
            .WithMessage("Webhook URL 不能为空")
            .When(x => x.Enabled && x.Type.Equals("webhook", StringComparison.OrdinalIgnoreCase));

        RuleFor(x => x.Parameters)
            .Must(p => p.ContainsKey("businessWebhook") && BeAValidUrl(p["businessWebhook"]))
            .WithMessage("Webhook URL 格式不正确")
            .When(x => x.Enabled && x.Type.Equals("webhook", StringComparison.OrdinalIgnoreCase));

        // 企业微信参数验证
        RuleFor(x => x.Parameters)
            .Must(p => p.ContainsKey("webhook_url") && !string.IsNullOrEmpty(p["webhook_url"]))
            .WithMessage("企业微信 Webhook URL 不能为空")
            .When(x => x.Enabled && x.Type.Equals("wechat", StringComparison.OrdinalIgnoreCase));

        RuleFor(x => x.Parameters)
            .Must(p => p.ContainsKey("webhook_url") && BeAValidUrl(p["webhook_url"]))
            .WithMessage("企业微信 Webhook URL 格式不正确")
            .When(x => x.Enabled && x.Type.Equals("wechat", StringComparison.OrdinalIgnoreCase));
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private static bool IsSpeakerNotifier(string type) =>
        type.Equals("speaker", StringComparison.OrdinalIgnoreCase);
}

/// <summary>
/// <see cref="TrackingConfig"/> 验证器。
/// </summary>
public class TrackingConfigValidator : AbstractValidator<TrackingConfig>
{
    public TrackingConfigValidator()
    {
        RuleFor(x => x.TrackIouThreshold)
            .InclusiveBetween(0.01f, 0.99f).WithMessage("TrackIouThreshold 须在 0.01～0.99");

        RuleFor(x => x.MaxMissedFrames)
            .InclusiveBetween(1, 1000).WithMessage("MaxMissedFrames 须在 1～1000");

        RuleFor(x => x.MinHits)
            .InclusiveBetween(1, 60).WithMessage("MinHits 须在 1～60");

        RuleFor(x => x.MaxTrackId)
            .InclusiveBetween(7, 65535).WithMessage("MaxTrackId 须在 7～65535（TrackId 为 0～MaxTrackId）");
    }
}

/// <summary>
/// 算法配置验证器
/// </summary>
public class AlgorithmConfigValidator : AbstractValidator<AlgorithmConfig>
{
    public AlgorithmConfigValidator()
    {
        RuleFor(x => x.AlgorithmType)
            .NotEmpty().WithMessage("算法类型不能为空");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("算法显示名称不能为空");

        RuleFor(x => x.Inference)
            .NotNull().WithMessage("推理配置不能为空")
            .SetValidator(new InferenceConfigValidator());

        RuleFor(x => x.Storage)
            .NotNull().WithMessage("存储配置不能为空")
            .SetValidator(new StorageConfigValidator());

        RuleFor(x => x.Notification)
            .NotNull().WithMessage("通知配置不能为空")
            .SetValidator(new NotificationConfigValidator());

        RuleFor(x => x.Tracking)
            .Custom((tracking, context) =>
            {
                if (tracking is null) return;
                var r = new TrackingConfigValidator().Validate(tracking);
                foreach (var e in r.Errors)
                    context.AddFailure(nameof(AlgorithmConfig.Tracking), e.ErrorMessage);
            });

        RuleForEach(x => x.Regions)
            .SetValidator(new RegionConfigValidator());

        RuleForEach(x => x.ClassColors ?? new List<string>())
            .Must(BeValidOptionalClassColor)
            .WithMessage("ClassColors 每项须为空、inherit，或 #RRGGBB")
            .When(x => x.ClassColors is { Count: > 0 });

        RuleFor(x => x)
            .Custom((algorithm, context) =>
            {
                foreach (var msg in AlgorithmRegionRequirements.GetMissingRegionMessages(algorithm))
                    context.AddFailure(nameof(AlgorithmConfig.Regions), msg);
            });
    }

    private static bool BeValidOptionalClassColor(string? entry)
    {
        if (string.IsNullOrWhiteSpace(entry)) return true;
        var t = entry.Trim();
        if (t.Equals("inherit", StringComparison.OrdinalIgnoreCase)) return true;
        return System.Text.RegularExpressions.Regex.IsMatch(t, "^#[0-9A-Fa-f]{6}$");
    }
}

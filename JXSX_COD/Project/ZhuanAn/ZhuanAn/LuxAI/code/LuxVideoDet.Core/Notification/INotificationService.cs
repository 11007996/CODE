namespace LuxVideoDet.Core.Notification;

/// <summary>
/// 通知参数定义 - 描述通知服务需要的配置参数
/// </summary>
public class NotificationParameterDefinition
{
    /// <summary>参数名称（内部使用，如 "url"）</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>参数显示名称（用户看到的，如 "Webhook URL"）</summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>参数描述</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>参数类型：string, int, bool, double</summary>
    public string ParameterType { get; set; } = "string";

    /// <summary>默认值</summary>
    public object? DefaultValue { get; set; }

    /// <summary>是否必需</summary>
    public bool Required { get; set; } = true;

    /// <summary>示例值</summary>
    public string? Example { get; set; }
}

/// <summary>
/// 通知服务接口
/// </summary>
public interface INotificationService
{
    /// <summary>通知类型</summary>
    string NotificationType { get; }

    /// <summary>
    /// 获取通知服务需要的参数定义
    /// </summary>
    List<NotificationParameterDefinition> GetRequiredParameters();

    /// <summary>
    /// 从合并后的配置字典初始化。须根据 <see cref="GetRequiredParameters"/> 中
    /// <see cref="NotificationParameterDefinition.Required"/> 及本渠道业务规则做校验：
    /// 必填缺失或非法时抛出 <see cref="ArgumentException"/>（消息应说明参数名与期望），
    /// 以便算法工厂跳过该通知器而不中止整个会话。
    /// </summary>
    void Initialize(Dictionary<string, object> config);

    /// <summary>
    /// 发送通知
    /// </summary>
    Task<bool> SendAsync(
        NotificationMessage message,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// 通知消息
/// </summary>
public class NotificationMessage
{
    /// <summary>标题</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>内容</summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>级别</summary>
    public string Level { get; set; } = "Info";

    /// <summary>机器名称</summary>
    public string MachineName { get; set; } = string.Empty;

    /// <summary>算法类型</summary>
    public string AlgorithmType { get; set; } = string.Empty;

    /// <summary>图像路径</summary>
    public string? ImagePath { get; set; }

    /// <summary>视频路径</summary>
    public string? VideoPath { get; set; }

    /// <summary>附加数据</summary>
    public Dictionary<string, object>? ExtraData { get; set; }

    /// <summary>时间戳</summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

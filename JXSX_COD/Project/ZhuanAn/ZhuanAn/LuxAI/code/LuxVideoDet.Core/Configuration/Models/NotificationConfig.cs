namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 通知配置
/// </summary>
public class NotificationConfig
{
    /// <summary>是否启用通知</summary>
    public bool Enabled { get; set; } = false;

    /// <summary>通知服务列表</summary>
    public List<NotifierConfig> Notifiers { get; set; } = new();
}

/// <summary>
/// 单个通知服务配置
/// </summary>
public class NotifierConfig
{
    /// <summary>是否启用</summary>
    public bool Enabled { get; set; } = true;

    /// <summary>通知类型</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>通知参数</summary>
    public Dictionary<string, string> Parameters { get; set; } = new();
}

/// <summary>
/// 通知级别枚举
/// </summary>
public enum NotificationLevel
{
    Info,
    Warning,
    Error,
    Critical
}

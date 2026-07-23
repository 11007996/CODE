using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.WeChat;

/// <summary>
/// 企业微信通知服务 - 通过企业微信机器人发送通知
/// </summary>
public class WeChatNotificationService : INotificationService
{
    private readonly ILogger<WeChatNotificationService> _logger;
    private readonly HttpClient _httpClient;
    private string _webhookUrl = string.Empty;

    public string NotificationType => "WeChat";

    public WeChatNotificationService(
        ILogger<WeChatNotificationService> logger,
        HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    public List<NotificationParameterDefinition> GetRequiredParameters() =>
        [.. WeChatNotificationDescriptor.Definitions];

    public void Initialize(Dictionary<string, object> config)
    {
        if (!config.TryGetValue("webhook_url", out var url) || url == null)
        {
            throw new ArgumentException(
                "企业微信通知缺少必填参数：请在 parameters 中填写 webhook_url（机器人 Webhook 地址）。");
        }

        _webhookUrl = url.ToString()?.Trim() ?? string.Empty;
        NotificationChannelInitRules.ThrowIfNullOrWhiteSpace(
            _webhookUrl,
            "企业微信通知缺少有效 webhook_url：当前为空。");

        _logger.LogInformation("企业微信通知服务已初始化");
    }

    public async Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(_webhookUrl))
        {
            _logger.LogWarning("企业微信 Webhook URL 未配置，无法发送通知");
            return false;
        }

        try
        {
            var content = BuildMessageContent(message);

            var payload = new
            {
                msgtype = "text",
                text = new
                {
                    content = content
                }
            };

            var json = JsonSerializer.Serialize(payload);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            _logger.LogDebug("发送企业微信通知");

            var response = await _httpClient.PostAsync(_webhookUrl, httpContent, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("企业微信通知发送成功");
                return true;
            }
            else
            {
                _logger.LogWarning("企业微信通知发送失败，状态码={StatusCode}", response.StatusCode);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "企业微信通知发送异常");
            return false;
        }
    }

    private string BuildMessageContent(NotificationMessage message)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"【{message.Level}】{message.Title}");
        sb.AppendLine($"内容: {message.Content}");
        sb.AppendLine($"机器: {message.MachineName}");
        sb.AppendLine($"算法: {message.AlgorithmType}");
        sb.AppendLine($"时间: {message.Timestamp:yyyy-MM-dd HH:mm:ss}");

        if (!string.IsNullOrEmpty(message.ImagePath))
        {
            sb.AppendLine($"图片: {message.ImagePath}");
        }

        if (!string.IsNullOrEmpty(message.VideoPath))
        {
            sb.AppendLine($"视频: {message.VideoPath}");
        }

        return sb.ToString();
    }
}

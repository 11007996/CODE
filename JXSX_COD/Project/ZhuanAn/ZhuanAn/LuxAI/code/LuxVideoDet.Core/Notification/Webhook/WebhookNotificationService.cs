using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.Webhook;

/// <summary>
/// Webhook 通知服务 - 通过 HTTP POST 发送通知
/// </summary>
public class WebhookNotificationService : INotificationService
{
    private readonly ILogger<WebhookNotificationService> _logger;
    private readonly HttpClient _httpClient;
    private string _url = string.Empty;

    public string NotificationType => "Webhook";

    public WebhookNotificationService(
        ILogger<WebhookNotificationService> logger,
        HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    public List<NotificationParameterDefinition> GetRequiredParameters() =>
        [.. WebhookNotificationDescriptor.Definitions];

    public void Initialize(Dictionary<string, object> config)
    {
        if (config.TryGetValue("url", out var url))
            _url = url?.ToString()?.Trim() ?? string.Empty;
        else if (config.TryGetValue("businessWebhook", out var businessWebhook))
            _url = businessWebhook?.ToString()?.Trim() ?? string.Empty;
        else
        {
            throw new ArgumentException(
                "Webhook 缺少必填参数：请在 parameters 中填写 url 或 businessWebhook（业务回调 HTTP 地址）。");
        }

        NotificationChannelInitRules.ThrowIfNullOrWhiteSpace(
            _url,
            "Webhook 缺少有效 URL：请在 parameters 中填写非空的 url 或 businessWebhook。");

        _logger.LogInformation("Webhook 通知服务已初始化，URL={Url}", _url);
    }

    public async Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(_url))
        {
            _logger.LogWarning("Webhook URL 未配置，无法发送通知");
            return false;
        }

        try
        {
            var payload = new
            {
                title = message.Title,
                content = message.Content,
                level = message.Level,
                machine_name = message.MachineName,
                algorithm_type = message.AlgorithmType,
                timestamp = message.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                image_path = message.ImagePath,
                video_path = message.VideoPath,
                extra_data = message.ExtraData
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _logger.LogDebug("发送 Webhook 通知到 {Url}", _url);

            var response = await _httpClient.PostAsync(_url, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Webhook 通知发送成功");
                return true;
            }
            else
            {
                _logger.LogWarning("Webhook 通知发送失败，状态码={StatusCode}", response.StatusCode);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Webhook 通知发送异常");
            return false;
        }
    }
}

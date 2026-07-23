# Webhook 通知

## 作用

通过 **HTTP POST** 将告警以 **JSON** 推送到你指定的**业务系统 URL**，用于与自建服务、中间件或其它平台对接，实现自定义处理（入库、转发、联动其它系统等）。

请求体包含通知标题、内容、级别、机器名、算法类型、时间戳，以及可选的图片路径、视频路径与扩展数据等字段，便于下游统一解析。

## 典型场景

- 将检测结果推送到自有 API，由后端决定如何展示、存储或告警升级。
- 对接第三方自动化平台（只要对方提供可接收 JSON 的 HTTP 端点）。

## 配置说明

- 必须配置接收地址：配置键 **`url`** 或 **`businessWebhook`**（二选一）。
- 使用 `IHttpClientFactory` 创建的 `HttpClient`，默认请求超时约 10 秒。

参数定义见 `WebhookNotificationDescriptor` 中的 `Definitions`。

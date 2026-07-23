# 企业微信通知

## 作用

通过**企业微信机器人 Webhook**，将告警以**文本消息**发送到指定企业微信群。适合团队已在企业微信协作、需要**移动端即时推送**的场景。

消息为 `msgtype: text`，正文包含级别、标题、内容、机器名、算法类型、时间，以及可选的图片与视频路径等信息。

## 典型场景

- 检测异常时推送到运维/安防群，便于手机端快速查看。
- 与喇叭、Webhook 等其它通道组合使用，实现「本地响铃 + 群里通知」。

## 配置说明

- 必须配置 **`webhook_url`**：在企业微信群中创建机器人后获得的 Webhook 地址（`https://qyapi.weixin.qq.com/cgi-bin/webhook/send?key=...`）。

参数定义见 `WeChatNotificationDescriptor` 中的 `Definitions`。

# 自定义通知（示例模板）

## 作用

本目录提供**实现自定义通知通道**的参考实现：`ExampleNotificationService` 实现 `INotificationService`，`ExampleNotificationDescriptor` 实现 `INotificationDescriptor`，演示如何声明参数、从配置初始化、以及在 `SendAsync` 中处理 `NotificationMessage`。

当前示例行为：仅将告警**写入日志**（带可配置前缀 `prefix`），**不向外部网络或服务发送任何内容**，适合作为复制改写的起点。

## 与正式通知的区别

- Descriptor 上带有 **`[ExampleTemplate]`**：`NotificationServiceFactory` 反射发现描述符时会**跳过**带此特性的类型，因此示例**不会出现在运行时可选通知类型列表**中，避免与真实通道混淆。
- 若基于本示例新增自有通知：复制并改名后，**去掉** `[ExampleTemplate]`，使工厂能注册你的 `INotificationDescriptor`。

## 实现要点（扩展时对照）

| 接口 | 职责 |
|------|------|
| `INotificationService` | `NotificationType`、`GetRequiredParameters`、`Initialize`、`SendAsync` 均需实现。 |
| `INotificationDescriptor` | `TypeKey`（配置中的类型键）、`DisplayName`、`ParameterDefinitions`、`Create`。 |

参数定义见 `ExampleNotificationDescriptor.Definitions`（示例仅含 `prefix`）。

## 初始化约定（与正式渠道一致）

- 在 **`Initialize`** 中校验 `ParameterDefinitions` 里 **`Required = true`** 的项，以及本渠道业务上的条件必填（如喇叭固定音频须填 `soundFile`）。
- **不满足时抛出 `ArgumentException`**（说明缺哪一项），算法工厂会跳过该渠道并打 `[配置·通知]` 日志，**不中止会话**。
- 可复用 `NotificationChannelInitRules.ThrowIfNullOrWhiteSpace` 等辅助方法。

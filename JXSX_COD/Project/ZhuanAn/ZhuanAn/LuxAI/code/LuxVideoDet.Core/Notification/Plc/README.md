# PLC 通知（Modbus TCP）

本目录实现 **通过 Modbus TCP 写单个保持寄存器（功能码 0x06）** 的告警通知渠道，与 Webhook、喇叭等并列，由 `NotificationServiceFactory` 反射注册，配置键为 **`plc`**。

---

## 与 Desktop / 配置文件的关系

| 问题 | 说明 |
|------|------|
| **会不会自动往 `configs.json` 里加一条 PLC？** | **不会。** 已有工程里不会出现「自动多出一条 notifier」；只有你**手动在 Desktop 里添加通知渠道**、或**直接编辑 JSON**，才会写入配置。 |
| **Desktop 里能选到 PLC 吗？** | **能。** 程序启动后，工厂会从程序集加载所有 `INotificationDescriptor`，`plc` 会出现在通知类型下拉里（显示名：**PLC（Modbus TCP）**）。 |
| **新渠道的参数从哪来？** | 1）**未保存过的字段**：使用 `PlcNotificationDescriptor.Definitions` 里各参数的 **`DefaultValue`**（Desktop 与 Headless 行为一致）。<br>2）若某算法在 `IAlgorithmDescriptor.GetDefaultNotifierParameters("plc")` 里提供了覆盖项，则**算法默认值先合并，再以配置文件为准**（与 `DetectionAlgorithmFactory.MergeNotifierParameters` 一致）。<br>3）可选在算法描述符中返回 `PlcNotificationDescriptor.GetDefaultParameterStringsForAlgorithms()` 的键值，避免重复手写字符串。 |

---

## 协议与业务约定（与现场一致）

- **传输**：TCP，默认端口 **`modbusPort`** = 502（以 PLC/网关实际为准）。
- **站号**：**`modbusUnitId`**（1～247），对应 Modbus TCP 帧中的单元标识符，多数单机为 **1**。
- **写入**：每次触发通知时 **建立连接 → 写 1 个寄存器 → 断开连接**，避免长时间占线；与「一次操作一套流程」的工业习惯一致。
- **常见业务映射**（需与 PLC 程序、地址表一致）：  
  - 寄存器地址 **`registerAddress`**（字偏移）常为 **0**（映射到 D 区等价的保持寄存器首字）。  
  - 告警时写入 **`registerValue`**，常见 **1** 表示停线/NG 请求；**0** 多表示允许运行（本通知仅在发告警时写入，一般为 **1**；若需向 PLC 写 0 复位，需在业务或扩展逻辑中另行处理）。

---

## 参数一览

| 参数 | 必填 | 默认值 | 说明 |
|------|------|--------|------|
| `plcIp` | 是 | *空字符串* | IPv4；也可用配置键 `ip`、`plc_ip`。默认留空，防止误连设备；新建渠道时请在 Desktop 或 JSON 中填写。 |
| `modbusPort` | 否 | `502` | Modbus TCP 端口。 |
| `modbusUnitId` | 否 | `1` | 站号 1～247。 |
| `registerAddress` | 否 | `0` | 保持寄存器地址（与 PLC 映射一致）。 |
| `registerValue` | 否 | `1` | 告警时写入的有符号 16 位值。 |
| `cooldownSeconds` | 否 | `1` | 两次成功写入之间的最短间隔（秒），减轻连续告警对 PLC 的冲击。 |
| `connectTimeoutMs` | 否 | `5000` | 连接/发送超时。 |
| `receiveTimeoutMs` | 否 | `5000` | 等待 Modbus 响应超时。 |

---

## 配置示例（`notification.notifiers`）

```json
{
  "enabled": true,
  "type": "plc",
  "parameters": {
    "plcIp": "192.168.250.100",
    "modbusPort": "502",
    "modbusUnitId": "1",
    "registerAddress": "0",
    "registerValue": "1",
    "cooldownSeconds": "1",
    "connectTimeoutMs": "5000",
    "receiveTimeoutMs": "5000"
  }
}
```

---

## 代码结构

| 文件 | 职责 |
|------|------|
| `PlcNotificationDescriptor.cs` | 注册 `TypeKey = plc`、参数定义、`GetDefaultParameterStringsForAlgorithms()`。 |
| `PlcNotificationService.cs` | 实现 `INotificationService`：解析配置、冷却、调用写入器。 |
| `ModbusTcpPlcWriter.cs` | 内部类：TCP 连接、构造 0x06 报文、校验响应（含站号回显）。 |

---

## 故障排查简述

- **连接失败**：检查 IP、防火墙、PLC 是否上电、端口是否为 502 或现场指定端口。  
- **写入无有效响应**：核对 **`modbusUnitId`**、**`registerAddress`** 与 PLC 侧 Modbus 映射表；确认 PLC 程序允许该站号写保持寄存器。  
- **Desktop 已选 PLC 但运行报错「需要配置 plcIp」**：在参数表里填写 **PLC IP** 并保存配置。若未填 IP 就启动会话，PLC 渠道**不会加入列表**，启动日志会有 `[配置·通知] 通知器「plc」未加入列表`，NG 时还会出现 `[算法·通知] …未挂载任何通知渠道`。

## 日志里如何确认

- 算法 **Initialize** 成功时会有 **`[算法·通知] 本算法已加载 N 个渠道: Plc, …`**；若为 0 条，说明未开启 `notification.enabled`、无 notifier，或某渠道初始化失败（见启动时的 **`[配置·通知]`**）。  
- NG 触发通知时先有 **`[算法·通知] 开始发送 | 渠道=Plc | …`**，随后 PLC 侧有 **`[通知·PLC]`**（连接失败、写入成功/失败、冷却跳过均有对应说明）。  
- UCS / 撕标签 仅在 **`notification.enabled=true`** 时才会置位 `ShouldNotify`；关闭通知时不会消耗通知冷却计时器。

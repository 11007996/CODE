# LuxVideoDet.Headless

无头检测服务 — 后台运行的轻量级视频检测引擎

---

## 简介

LuxVideoDet.Headless 是 LuxVideoDet 系统的无头运行模式，不提供可视化界面或实时渲染，仅在后台静默运行检测任务。适用于生产部署、无人值守、容器化运行等场景。

### 核心能力

- **零界面开销** — 无 GUI / 无 Web 服务，资源消耗最低
- **读取配置即运行** — 直接读取 `configs.json`，无需额外操作
- **默认全量运行** — 启动后自动运行所有已启用的配置
- **指定配置运行** — 通过 `-c` 参数选择性运行特定配置
- **事件驱动通知** — 检测异常时通过日志 + 通知渠道（Webhook / 企业微信 / 钉钉）推送
- **优雅关闭** — 支持 Ctrl+C / SIGTERM 信号安全退出

---

## 快速启动

### 前置要求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- 已编译的 LuxVideoDet.Core 项目
- 已配置好的 `configs.json`（可从 Desktop 或 Web 端导出）

### 运行

```bash
# 从解决方案根目录启动（运行所有已启用配置）
dotnet run --project LuxVideoDet.Headless

# 或进入项目目录
cd LuxVideoDet.Headless
dotnet run
```

---

## 命令行参数

| 参数 | 说明 |
|------|------|
| `-c, --config <名称\|ID>` | 指定要运行的配置（可多次使用） |
| `-l, --list` | 列出 configs.json 中的所有配置 |
| `-h, --help` | 显示帮助信息 |

### 使用示例

```bash
# 运行所有已启用的配置（默认行为）
dotnet run --project LuxVideoDet.Headless

# 只运行名为 "UCS" 的配置
dotnet run --project LuxVideoDet.Headless -- -c UCS

# 同时运行多个指定配置
dotnet run --project LuxVideoDet.Headless -- -c UCS -c Test

# 查看所有可用配置
dotnet run --project LuxVideoDet.Headless -- -l

# 查看帮助
dotnet run --project LuxVideoDet.Headless -- -h
```

> **注意**：使用 `dotnet run` 时，`--` 之后的参数才会传递给应用程序。直接运行编译后的可执行文件则无需 `--`。

---

## 配置

### configs.json

Headless 模式直接读取项目目录下的 `configs.json` 文件。该文件与 Desktop / Web 端共用相同的配置格式，可通过以下方式获取：

1. **从 Desktop 端导出** — 在桌面端配置好检测参数后，复制 `configs.json` 到 Headless 目录
2. **从 Web 端导出** — 调用 `GET /api/configs/export` 导出后放置到 Headless 目录
3. **手动编辑** — 参考下方配置结构说明

#### 配置结构

```json
[
  {
    "id": "uuid",
    "name": "配置名称",
    "videoSource": {
      "type": "RTSP | LocalVideo | Camera",
      "source": "rtsp://... 或视频文件路径",
      "loop": false,
      "reconnectInterval": 5,
      "timeout": 10
    },
    "algorithms": [
      {
        "algorithmType": "tearofftab | u7lite | test",
        "displayName": "显示名称",
        "inference": {
          "modelPath": "模型文件路径",
          "device": "CPU | GPU",
          "confidenceThreshold": 0.5,
          "iouThreshold": 0.5,
          "inputSize": { "width": 640, "height": 640 },
          "classes": ["class1", "class2"],
          "modelType": "Auto"
        },
        "regions": [],
        "storage": {
          "saveErrorImage": true,
          "saveRetrainImage": true,
          "saveVideo": false,
          "errorImagePath": "catch",
          "retrainImagePath": "retrain",
          "retentionDays": 7
        },
        "notification": {
          "enabled": true,
          "notifiers": [
            {
              "type": "Webhook",
              "webhookUrl": "https://...",
              "cooldown": 60
            }
          ]
        },
        "enabled": true
      }
    ],
    "enabled": true
  }
]
```

#### 关键字段说明

| 字段 | 说明 |
|------|------|
| `enabled`（顶层） | 配置总开关，`false` 时 Headless 默认不运行该配置 |
| `algorithms[].enabled` | 单个算法开关 |
| `notification.enabled` | 是否启用通知推送 |
| `storage.saveErrorImage` | 检测到异常时是否保存截图 |
| `storage.retentionDays` | 截图/视频自动清理天数 |

### 日志

- 日志文件写入 `logs/headless-<日期>.log`
- 保留最近 7 天，单文件上限 10MB
- 控制台和文件同时输出

#### 日志级别

通过环境变量控制：

```bash
# PowerShell
$env:LUXVIDEODET_LOG_LEVEL="Debug"; dotnet run --project LuxVideoDet.Headless

# CMD
set LUXVIDEODET_LOG_LEVEL=Debug && dotnet run --project LuxVideoDet.Headless

# Linux / macOS
LUXVIDEODET_LOG_LEVEL=Debug dotnet run --project LuxVideoDet.Headless
```

可选值：`Verbose` / `Debug` / `Information`（默认）/ `Warning` / `Error`

---

## 项目结构

```
LuxVideoDet.Headless/
├── Program.cs                     # 应用入口，流水线编排与生命周期管理
├── configs.json                   # 检测配置（与 Desktop/Web 共用格式）
├── logs/                          # 运行日志（自动创建）
│   └── headless-20260322.log
├── catch/                         # 异常截图存储（自动创建）
├── retrain/                       # 重训练图像存储（自动创建）
└── LuxVideoDet.Headless.csproj    # 项目文件
```

---

## 运行流程

```
启动 → 读取 configs.json → 筛选配置 → 创建检测流水线 → 后台运行
                                 │
                   ┌─────────────┼─────────────┐
                   ▼             ▼             ▼
              配置 A         配置 B         配置 C
           (Pipeline)     (Pipeline)     (Pipeline)
                   │             │             │
                   ▼             ▼             ▼
            视频源读帧      视频源读帧      视频源读帧
                   │             │             │
                   ▼             ▼             ▼
            算法推理+判定   算法推理+判定   算法推理+判定
                   │             │             │
                   ▼             ▼             ▼
            异常 → 通知    异常 → 通知    异常 → 通知
            截图 → 存储    截图 → 存储    截图 → 存储
```

---

## Docker 部署

```bash
# 构建
docker build -t luxvideodet-headless .

# 运行（挂载配置和模型）
docker run -d \
  --name luxvideodet-headless \
  -v /path/to/configs.json:/app/configs.json \
  -v /path/to/models:/app/models \
  -v /path/to/catch:/app/catch \
  luxvideodet-headless

# 运行指定配置
docker run -d \
  --name luxvideodet-headless \
  -v /path/to/configs.json:/app/configs.json \
  -v /path/to/models:/app/models \
  luxvideodet-headless -c UCS

# 查看日志
docker logs -f luxvideodet-headless
```

---

## 与其他模式的对比

| 特性 | Headless | Web | Desktop |
|------|----------|-----|---------|
| 可视化界面 | 无 | 浏览器 Web UI | 原生桌面窗口 |
| 内存占用 | 最低 (~400MB) | 中等 (~600MB) | 较高 (~800MB) |
| CPU 占用 | 最低 (15-25%) | 中等 (25-35%) | 较高 (35-45%) |
| 实时画面 | 不支持 | MJPEG 流 | GPU 渲染 |
| 配置管理 | 命令行 + JSON | Web API + UI | GUI 操作 |
| 适用场景 | 生产/无人值守 | 远程监控 | 开发调试 |

---

## 常见问题

### configs.json 为空或不存在

请先通过 Desktop 或 Web 端创建配置，然后将 `configs.json` 复制到 Headless 项目目录。也可以使用 `-l` 参数检查当前配置状态。

### 视频源连接失败

检查 `configs.json` 中的视频源路径或 RTSP 地址是否正确，确保网络可达。Headless 模式下视频源连续读帧失败超过阈值后会自动报错并记录日志。

### 如何接收异常通知

在配置中启用 `notification`，并配置对应的通知器（Webhook / 企业微信 / 钉钉）。Headless 模式下所有异常判定都会触发通知。

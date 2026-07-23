# LuxVideoDet.Core

工业视觉检测系统的 **.NET 核心库**（`net8.0`）。供桌面端、服务宿主等引用，提供配置、推理、视频源、检测算法管道、存储、通知等能力，与具体 UI 框架解耦。

## 职责概览

| 领域 | 说明 |
|------|------|
| **配置** | 强类型 `DetectionConfiguration`、JSON 持久化、FluentValidation 校验、`ConfigurationService` 统一加载/保存 |
| **推理** | `IInferenceEngine` 抽象；ONNX Runtime（CPU / 可选 CUDA、QNN）与 OpenVINO 实现；按模型类型选择检测 / 分割 / OBB / 姿态等后处理 |
| **视频源** | 本地文件、RTSP、摄像头等，经 `VideoSourceFactory` 创建 |
| **算法** | `IDetectionAlgorithm` 与多种业务实现、状态机与流水线；`PipelineFactory` 编排「视频源 + 算法」 |
| **区域与 AOI** | 区域管理、AOI 检测器（如 U 型等） |
| **存储** | 错图/再训练图、录像与统计等 |
| **通知** | Webhook、微信、PLC、扬声器等，由工厂按配置创建 |
| **日志** | `Microsoft.Extensions.Logging` + Serilog；可选桌面端实时日志 `ILogEventSink` |

## 代码目录

以下为源码根目录 `LuxVideoDet.Core/` 下主要文件夹（不含 `bin/`、`obj/` 构建输出）。

```
LuxVideoDet.Core/
├── Algorithm/                 # 检测算法抽象、工厂、流水线与各业务实现
│   ├── Implementations/       # 按产品/场景划分的算法（Example、MidFrameScan、UCS、U7Lite…）
│   ├── Pipeline/              # AlgorithmWorker、MultiAlgorithmPipeline 等编排
│   ├── Results/               # DetectionResult、判级与通知级别等
│   └── Ui/                    # 算法在桌面上的 UI 动作声明（与 Desktop 配合）
├── Aoi/                       # AOI 检测器（如 UDet/U 型）、描述符与示例
├── Common/                    # 帧、共享类型等跨模块基础类型
├── Configuration/           # 配置模型、JSON 存储、校验器、ConfigurationService
│   ├── Models/
│   └── Validation/
├── Extensions/                # DI 扩展：AddLuxVideoDetCore、Serilog、DesktopLogging
├── Inference/                 # 推理引擎接口与实现、设备注册、后处理与结果类型
│   ├── Onnx/
│   ├── OpenVino/
│   ├── Postprocessors/
│   └── Results/
├── Logging/                   # 桌面日志 Sink、DesktopLoggerProvider 等
├── Notification/              # 通知工厂与各通道（Webhook、微信、PLC、扬声器…）
├── Region/                    # 区域管理（与配置中的检测区域配合）
├── Rendering/                 # 检测结果/分类叠加绘制（如 TaskAwareDetectionRenderer）
├── Storage/                   # 错图与再训练图、录像、统计、StorageManager
├── Utils/                     # 绘图辅助、流水线统计等工具
├── VideoSource/               # 本地视频、RTSP、摄像头等及 VideoSourceFactory
│   ├── LocalVideo/
│   ├── Rtsp/
│   └── Camera/
├── ExampleTemplateAttribute.cs   # 示例/模板元数据标记（供工具或文档生成使用）
├── LuxVideoDet.Core.csproj
└── README.md
```

| 目录 | 内容摘要 |
|------|----------|
| `Algorithm` | `IDetectionAlgorithm`、`DetectionAlgorithmFactory`、各 `Implementations/*` 具体算法与 `DESCRIPTOR.md` 等说明 |
| `Inference` | `IInferenceEngine`、`InferenceEngineFactory`、`InferenceDeviceRegistry`；ONNX/OpenVINO 引擎；分割/检测/OBB/姿态等 `Postprocessors` |
| `VideoSource` | `VideoFrameCapture`、`FfmpegCaptureEnvironment` 及各类型视频源 |
| `Configuration` | `IConfigurationStore` 实现（如单文件 `configs.json`）、`DetectionConfiguration` 及 FluentValidation |
| `Storage` | `IStorageService`、`VideoRecorder`、`RecordingManager`、`ProductionStatisticsStore` 等 |
| `Notification` | `INotificationService`、按通道子目录实现，由 `NotificationServiceFactory` 创建 |
| `Aoi` | `AoiDetectorFactory`、U 型等 AOI 与 `IAoiDetectorDescriptor` |
| `Extensions` | 宿主启动时注册核心服务的入口 |

子目录内若存在 `README.md`，多为该模块或某一算法的补充说明，可优先阅读。

## 依赖与推理后端

- **图像**：OpenCvSharp（Windows / Linux / macOS 条件引用，Linux 可能依赖系统 OpenCV）
- **推理**：`Microsoft.ML.OnnxRuntime` 或条件包 `Microsoft.ML.OnnxRuntime.Gpu` / `OnnxRuntime.QNN`；OpenVINO C# API 与对应平台 runtime 包
- **其它**：FluentValidation、Serilog、SkiaSharp（含中文渲染场景）、`System.Text.Json` 等

构建 ONNX GPU 包示例：

```bash
dotnet build -p:UseCUDA=true
```

（QNN 等条件与平台以 `LuxVideoDet.Core.csproj` 中注释为准。）

## 在宿主中注册服务

```csharp
var services = new ServiceCollection();
services.AddLogging(); // 或 AddSerilogLogging(...)

services.AddLuxVideoDetCore(); // 注册配置、推理/视频/算法/通知等工厂与核心服务

// 可选：桌面 UI 订阅日志
services.AddDesktopLogging(minLevel: LogLevel.Information);

var sp = services.BuildServiceProvider();
```

默认通过 `SingleFileConfigurationStore` 使用工作目录下的 **`configs.json`** 集中存放多套检测配置。若需自定义路径或存储方式，可注册自定义的 `IConfigurationStore`。

## 配置与校验

- 配置模型见 `Configuration/Models`，例如 `DetectionConfiguration`、`InferenceConfig`、`VideoSourceConfig`、`RegionConfig` 等。
- 使用 `ConfigurationService` 的 `LoadAsync` / `SaveAsync` / `ValidateAsync`；保存前会经 FluentValidation 校验。

示例（节选）：

```csharp
var configService = sp.GetRequiredService<ConfigurationService>();
var config = await configService.LoadAsync("my-config-id");
await configService.SaveAsync(config);
```

JSON 字段结构与示例可参考仓库内既有配置；`Inference` 中可指定设备、模型路径、`ModelType`（如自动或分割等）等。

## 推理与后处理

- 通过 `InferenceEngineFactory` 按配置创建具体引擎（ONNX / OpenVINO 等）。
- 推理结果统一为 `InferenceResult`，后处理由 `PostprocessorFactory` 按模型类型选择，与 Ultralytics 风格导出模型配合使用。

## 日志

- `AddSerilogLogging`：控制台 + 按日滚动的文件日志。
- `AddDesktopLogging`：向 `ILogEventSink` 推送，便于 Avalonia / WPF 等绑定列表展示。
- 可通过标准 `Logging:LogLevel` 或 Serilog 配置调整命名空间级别。

## 扩展方式

- **配置存储**：实现 `IConfigurationStore` 并替换 DI 中的注册（例如多文件、数据库）。
- **日志**：实现或订阅 `ILogEventSink` / 自定义 `ILoggerProvider`。
- **新算法或通知通道**：按现有 `IAlgorithmDescriptor` / `INotificationService` 等模式接入工厂。

## License

MIT

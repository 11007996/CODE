# 算法 Descriptor（`IAlgorithmDescriptor`）与可视化颜色

## 三级优先级（config → Descriptor → 内置默认）

对 **区域轮廓色** 与 **检测框类别色** 均适用：

| 优先级 | 区域 `RegionConfig.Color` | 类别 `AlgorithmConfig.ClassColors` |
|--------|---------------------------|-------------------------------------|
| **1. 配置（最高）** | 显式 `#RRGGBB`（RGB 十六进制） | 显式 `#RRGGBB`，与 `inference.classes` 下标对齐 |
| **2. Descriptor** | `RequiredRegions` 中**同名**区域的 `DefaultColor` | `DefaultClassColors` 中同下标颜色 |
| **3. 内置默认** | `RegionManager` 内按区域名（如 PickupArea / FinishArea）映射，否则纯绿 | `DrawingHelper.GenerateColorPalette` |

**表示「未在 config 中指定」**（走 2→3）：`Color` / `ClassColors` 某项为 **空字符串**、**空白**，或 **`inherit`**（不区分大小写）。

> 已在磁盘上的配置里若写着 `#00FF00`，视为**用户显式指定绿色**，优先级仍为 1；若希望用 Descriptor，请删成空或 `inherit`。

解析实现：`LuxVideoDet.Core.Utils.VisualizationColors.TryParseRgbHexToBgrScalar`。

## 颜色逻辑放在哪里？

| 内容 | 位置 |
|------|------|
| **区域色合并** | `RegionManager.ResolveRegionColor` + `LoadFromConfig(..., algorithmType)` |
| **类别色合并** | `DetectionAlgorithmBase`：`GetSemanticOrderForClassColors` 默认取 Descriptor `DefaultClasses`，按类名匹配填色；否则按下标合并（读 `_config.ClassColors`） |
| **业务态覆盖** | 各算法 `GetDetectionColor`（如成品区标签变红） |

## Descriptor 中要维护的两处颜色

1. **`DefaultClassColors`**：与 **`DefaultClasses`** 顺序一致，BGR。  
2. **`RequiredRegions[*].DefaultColor`**：区域默认色（BGR）。

## 工厂静态查询

- `DetectionAlgorithmFactory.GetDefaultClasses(typeKey)`  
- `DetectionAlgorithmFactory.GetRequiredRegions(typeKey)`  
- `DetectionAlgorithmFactory.GetDefaultClassColors(typeKey)`  

`typeKey` 与配置中 `algorithmType` 一致（小写，如 `tearofftab`）。

## 配置表单与 `args`（AOI vs 纯算法）

二者都写入同一 `AlgorithmConfig.Args`（JSON），但在描述符中**分开声明**，避免与 AOI 检测器参数混淆：

| 属性 | 含义 | 典型内容 |
|------|------|----------|
| **`AoiParameterSections`** | AOI / 子进程检测器参数 | U7Lite U 型、MediaPipe 手部 `mediapipa_venv` 等 |
| **`AlgorithmParameterSections`** | 核心算法逻辑门控 | 当前主要为 **缠胶圈数**（TapeWrapCount）绕圈与轨迹门限 |

| 情况 | Descriptor 写法 |
|------|-------------------|
| 不需要对应类表单 | **不必**声明该属性；接口默认为**空列表**。 |
| 需要表单 | 在对应 Descriptor 中显式实现，使用 `AlgorithmArgsFormSection`。 |

静态查询：`GetAoiParameterSections` / `GetAlgorithmParameterSections`（`DetectionAlgorithmFactory`），供 Desktop/Web 分两块渲染。

## UI 操作（实时画面按钮）

| 机制 | 说明 |
|------|------|
| **元数据** | `IAlgorithmDescriptor.GetUiActionDefinitions()` 返回 `AlgorithmUiActionDefinition` 列表（`actionId`、显示名、说明）。 |
| **运行时** | 算法类实现 `IAlgorithmUiCommandHandler.TryInvokeUiAction(actionId, …)`，与上表 `actionId` 一致。 |
| **流水线** | `MultiAlgorithmPipeline.TryInvokeUiAction(algorithmType, actionId, …)` 按 `algorithmType` 找到对应 Worker 并调用。 |
| **静态查询** | `DetectionAlgorithmFactory.GetUiActionDefinitions(algorithmType)` 供配置页/前端拉取。 |
| **Web** | `GET /api/algorithms/{type}/ui-actions`，`POST /api/sessions/{configId}/ui-action`（body: `algorithmType`、`actionId`）。 |
| **Desktop** | `DetectionService.TryInvokeUiAction(configId, algorithmType, actionId, …)`。 |

当前 **UCS**、**TearOffTab** 提供 `*.acknowledgeError`（错误态恢复待机）。PLC 写 0 等可在该操作处理函数内扩展。

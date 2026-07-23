# Algorithm 模块

面向**业务开发**：如何在本仓库里新增一种检测算法，并接入配置、流水线与（可选）桌面 UI。

---

## 新增算法：最少要做的事

| 步骤 | 做什么 |
|------|--------|
| 1 | 在 `Implementations/<你的算法名>/` 下新建算法类，**继承** `DetectionAlgorithmBase`，实现 `ProcessDetections`（以及 `Reset`、`GetCurrentState` 等）。 |
| 2 | 在同一目录下新建 **Descriptor** 类，**实现** `IAlgorithmDescriptor`（`TypeKey`、`DisplayName`、`Create`、默认类别与区域等）。 |
| 3 | 在配置里把 `algorithmType` 写成 Descriptor 的 **`TypeKey`（小写）**，与模型、区域等一并保存。 |

**不要**改 `DetectionAlgorithmFactory`：工厂在启动时用反射收集所有 `IAlgorithmDescriptor`，按 `TypeKey` 注册。

模板目录：**`Implementations/Example/`**。复制后请**去掉** Descriptor 上的 `[ExampleTemplate]`，否则不会参与注册。

更细的 Descriptor 字段、颜色与 UI 按钮说明见 **`DESCRIPTOR.md`**。

---

## 运行时：一帧里发生了什么（基类已写好）

业务代码主要写在 **`ProcessDetections`** 里；其余由 `DetectionAlgorithmBase` 统一处理：

1. 调用推理引擎 **`Infer`** → 得到检测框列表。  
2. 调用 **`ProcessDetections`**（你实现）→ 判定 OK/NG、画框、写状态、决定是否通知等。  
3. 叠加 FPS/区域等、更新统计、按需 **存图 / 录像 / 发通知**。

初始化时基类会根据配置装配 **存储、录像、类别颜色** 等；子类可在 **`OnInitialize`** 里做自己的状态机、计时器等。

---

## 代码怎么组织（建议）

```
Implementations/
  YourAlgo/
    YourAlgorithm.cs          ← 继承 DetectionAlgorithmBase
    YourDescriptor.cs         ← 实现 IAlgorithmDescriptor
    YourStateMachine.cs       ← 可选：复杂逻辑拆出去
```

- **`AlgorithmType`**（算法类属性）与 Descriptor 的 **`TypeKey`** 在语义上应对应同一套标识；配置里用的是 **`TypeKey`** 的小写形式。

---

## Descriptor 里常见要填的项

| 成员 | 含义 |
|------|------|
| `TypeKey` | 配置 `algorithmType`，小写，如 `myalgo`。 |
| `DisplayName` | 界面下拉显示名称。 |
| `DefaultClasses` / `DefaultClassColors` | 默认类别与框颜色（可与 `DESCRIPTOR.md` 中颜色优先级配合）。 |
| `RequiredRegions` | 需要的区域名、默认色等。 |
| `Create(...)` | `return new YourAlgorithm(...)`，依赖由工厂注入。 |
| `GetUiActionDefinitions()` | 可选：声明实时预览上的按钮；运行时算法实现 **`IAlgorithmUiCommandHandler`** 处理点击。按钮在画面上的九宫格位置用 **`AlgorithmUiActionDefinition` 的 `Placement`**（见 `Ui/`）。 |

可选接口：

- **`IAlgorithmUiCommandHandler`**：处理预览区按钮。  
- **`IProductionStatisticsProvider`**：向界面提供 OK/NG 计数。

---

## 在 `ProcessDetections` 里常用结果字段

```csharp
// 通知（由基类在 Process 末尾统一发送）
result.ShouldNotify = true;
result.NotificationMessage = "异常说明";
result.NotificationLevel = NotificationLevel.Warning;

// 存图 / 触发 NG 录像（基类已按配置创建存储组件）
await SaveErrorImageAsync(annotatedFrame, timestamp);
TriggerVideoRecording(timestamp);
```

---

## 目录与职责（Implementations 以外）

路径对应仓库结构，`#` 后为该文件在模块内的作用。

### 根目录：契约与运行时骨架

```
Algorithm/
├── IDetectionAlgorithm.cs          # 运行时契约：Initialize / Process / RenderAnnotations / Reset…
├── RegionDefinition.cs             # 区域元数据：名称、默认色、是否必填等。
├── RenderOptions.cs                # 预览是否画检测框、区域、标签。
├── IAlgorithmDescriptor.cs         # 注册与元数据：TypeKey、DisplayName、Create、默认类别/区域/色、通知默认、UI 操作列表…
├── IAlgorithmUiCommandHandler.cs   # （可选）预览按钮：TryInvokeUiAction(actionId)。
├── IProductionStatisticsProvider.cs  # （可选）产线 OK/NG 等统计。
├── DetectionAlgorithmBase.cs       # 模板方法：Infer → ProcessDetections → 绘制与统计 → 存图/录像 → 通知；语义/按下标配色；FormatDetectionLabel + DrawTaskDetections（角标默认 ClassName，可覆写为业务名）。
├── DetectionAlgorithmFactory.cs    # 反射登记 Descriptor；CreateAlgorithmAsync；静态查询如 GetDefaultClasses、GetUiActionDefinitions。
├── ClassIdsResolution.cs           # 初始化：用 GetClassIndexMap 将「业务键→模型类名」解析为 ClassId，供 *ResolvedClassIds 等复用。
├── SemanticOrderClassColors.cs     # 按语义类名顺序为各 ClassId 填色（类名匹配），减轻模型类别顺序/数量变化导致的配色错位；子类需显式调用。
└── DESCRIPTOR.md                   # Descriptor、颜色、UI 与 Web API 说明（非代码）。
```

### `Ui/`

```
Algorithm/Ui/
├── AlgorithmUiActionDefinition.cs  # 单条预览按钮：ActionId、文案、Placement（九宫格）。
└── AlgorithmUiActionPlacement.cs   # 预览画布上的相对区域（九宫格枚举）。
```

### `Results/`

```
Algorithm/Results/
├── DetectionResult.cs              # 单帧结果：图、检测列表、耗时/FPS、状态、通知、Judgement、ExtraData 等。
├── NotificationLevel.cs            # 通知级别。
└── ProductionJudgement.cs          # 本帧判定 None / OK / NG。
```

### `Pipeline/`

```
Algorithm/Pipeline/
├── PipelineFactory.cs              # 从 DetectionConfiguration 创建 VideoSource、多路算法与 MultiAlgorithmPipeline。
├── MultiAlgorithmPipeline.cs       # 读帧广播、多路结果与合成、UI 操作转发等。
└── AlgorithmWorker.cs              # 单路队列与处理循环，调用 IDetectionAlgorithm.Process。
```

### `Implementations/`

```
Algorithm/Implementations/
├── Example/                        # 示例：对齐 Ucs 五件套（*Algorithm / *Context / *StateMachine / *DetectionBindings / ExampleDescriptor）+ README.md
├── …                               # 各产品线：常见为算法 + 描述符 + 上下文 + 状态机 + README
```

---

## 配置与调试提示

- 模型路径、阈值、区域点集等在 **`AlgorithmConfig`** / 会话配置里，通常用 Desktop 或 Web 编辑，**不必**为改参数而改代码。  
- 若类别名与模型不一致，启动日志里会有 **`[配置·类别]`** 提示，可先对照 `DESCRIPTOR.md` 与 `inference.classes`。

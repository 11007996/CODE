# 按视觉任务类型的渲染架构说明

## 背景

YOLO 系模型常见输出形态包括：**检测 (bbox)**、**实例分割 (mask)**、**姿态 (keypoints)**、**OBB (旋转框)**、**分类 (概率向量)** 等。  
统一用 `Detection` 承载各任务结果（见 `Inference/Results/InferenceResult.cs`），但**画到画面上时必须按 `ModelType` 选择不同绘制策略**，否则会出现「只有竖框、没有掩膜/骨架/旋转框」等问题。

本目录提供 **任务感知渲染** 的统一入口与扩展约定，避免每种业务算法各自复制一套 `if (mask) ...` 逻辑。

---

## 核心类型

| 类型 | 说明 |
|------|------|
| `LuxVideoDet.Core.Inference.ModelType` | 与推理引擎/后处理器一致的视觉任务枚举（`Detection`、`Segmentation`、`Classification`、`PoseEstimation`、`Obb`、`Auto`）。 |
| `Detection` | 统一 DTO：`BoundingBox`、`Mask`、`KeyPoints`、`RotationAngle`、`Probabilities` 等字段按任务填充。 |
| `DetectionAlgorithmBase.ModelTaskType` | 初始化后由 `IInferenceEngine.GetModelInfo().Type` 写入，子类绘制时使用。 |

---

## 统一入口

**`TaskAwareDetectionRenderer.Draw(...)`**（见 `TaskAwareDetectionRenderer.cs`）

- 入参包含 **`ModelType taskType`** 与 **`DetectionDrawOptions`**（线宽、是否画标签、掩膜透明度等）。
- 内部按任务分支：
  - **Detection / Auto**：轴对齐框 + 标签（`Auto` 时按检测处理）。
  - **Segmentation**：在检测基础上叠加 **实例掩膜**（与 `Detection.Mask` 一致）。
  - **PoseEstimation**：人体框 + **关键点** + **骨架连线**（COCO-17 约定）。
  - **Obb**：**旋转框**（`RotationAngle` + 中心与宽高；角度与 OpenCV 约定见代码注释）。
  - **Classification**：见下文「图像分类」；由 **`ClassificationOverlayRenderer`** 实现，不画检测框。

算法侧 **应调用 `TaskAwareDetectionRenderer`**，而不是直接 `Cv2.Rectangle` + 零散逻辑，以便新任务类型只改一处渲染层。

---

## 图像分类（Classification）

整图一个向量输出（如 YOLOv8-cls），**没有目标框**，逻辑上比检测/分割简单。

| 项目 | 说明 |
|------|------|
| **数据** | `Detection.Probabilities` 为各类得分（长度 = 类别数）；`Detection.PerClassLabels` 与下标对齐的类别名（由 **`ClassificationPostprocessor`** 从配置类别名填充，缺省为 `class0`…）。 |
| **绘制** | **`ClassificationOverlayRenderer`**：左上角半透明面板 + 标题「图像分类」+ **Top-K** 行（序号、类别名、百分比）。第 1 名用传入的强调色 **`accentColor`**（与算法调色板一致）。 |
| **选项** | `DetectionDrawOptions.ClassificationTopK`、`ClassificationShowPanel`、`ClassificationPanelBlend`（面板暗化强度）。 |
| **无概率时** | 仅尝试显示 Top-1 的 `ClassName` + `Confidence` 文本。 |

若模型输出为 **logits** 而非概率，可在后处理器中先做 **Softmax** 再写入 `Probabilities`，否则百分比仅表示原始得分比例。

---

## 扩展新任务类型时

1. **推理层**：在 `ModelType` 中增加枚举值（若需要）；后处理器填充 `Detection` 的对应字段。
2. **渲染层**：在 `TaskAwareDetectionRenderer` 中增加 `case` 或抽取私有方法（必要时增加 `DetectionDrawOptions` 字段）。
3. **业务算法**：继续只关心区域/状态机；绘制处传入 `ModelTaskType` 即可。

可选的进一步重构（未强制）：

- 将「仅绘制」抽成 `IRenderTaskStrategy` 策略接口 + DI 注册，适合任务类型很多、需要单元测试拆分的场景。
- 对 **OBB** 若需与 Ultralytics 角度定义完全一致，可在 `Detection` 中增加显式 `CenterX/Y/Width/Height` 旋转框参数，避免仅用轴对齐 `BoundingBox` 近似。

---

## 与 `DrawingHelper` 的关系

- `DrawingHelper`：**基础图元**（文字、FPS、单色框、`DrawInstanceMask`、旧的 `DrawDetectionAnnotation` 等）。
- `TaskAwareDetectionRenderer`：**任务编排**（何时画掩膜、何时画骨架、何时画旋转多边形、何时走分类叠加层）。
- `ClassificationOverlayRenderer`：**仅图像分类**的版面（中文标题与类别行依赖 `ChineseTextRenderer` / Skia）。

新增低层绘制函数时放在 `DrawingHelper`；按任务选择调用链时放在 `TaskAwareDetectionRenderer`；分类专用 UI 放在 `ClassificationOverlayRenderer`。

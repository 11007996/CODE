# Aoi Example — 自建 CV 类 AOI 的参考目录

本目录提供**最小可复制的骨架**：一个检测器类 + 一个描述符类。用于说明：若你要在 **OpenCV / 传统视觉** 路径上实现「单 ROI 辅助检测」（不走路推理模型），需要接哪些接口、写哪些文件。

当前示例带 **`[ExampleTemplate]`**，**不会**被 `AoiDetectorFactory` 反射注册；复制到自有目录并去掉该标记后，才会出现在工厂的可用列表中。

---

## 目录里有什么

```
Aoi/Example/
├── ExampleAoiDetector.cs    # 检测器：继承 AoiDetectorBase，实现单 ROI 算法
├── ExampleAoiDescriptor.cs  # 描述符：TypeKey、Create、供工厂发现
└── README.md                # 本说明
```

完整可参考实现见 **`../UDet/`**（U 型开口检测）。

---

## 基于 CV 自研 AOI 时要实现哪些内容

### 1. 检测器（必须）

- **继承** `AoiDetectorBase`（或自行实现 `IAoiDetector`，一般不推荐重复造轮子）。
- **必须实现**
  - `Name`：日志与诊断用标识。
  - `OnDetect(Mat roi, Dictionary<string, object> parameters)`：对本帧 **裁剪好的 ROI** 做处理，返回 **`AoiResult`**（`Success`、`Confidence`、`Message`，以及需要对外暴露的标量/小结构可放进 `Data`）。
- **可选覆盖**
  - `OnInitialize()`：一次性加载阈值、结构元素、小模型等；基类的 `Initialize` 会合并参数并设置 `_initialized`。
  - `DetectBatch`：默认逐张调 `Detect`；若有批处理优化可重写。
- **实践注意**
  - **`Mat` 生命周期**：中间图用完即 `Dispose`（或 `using`），避免实时流泄漏；见 `UDet` 中的写法。
  - **`AoiResult.Data`**：宜放角度、方向、点数等**轻量**字段；避免把整条轮廓点集、大图等长期塞在字典里（序列化/跨层传递成本高）。

基类已处理：**未初始化拦截**、**空 ROI**、**参数合并**（`Initialize` 字典 + 每次 `Detect` 传入的字典）、**异常转失败结果**。

### 2. 描述符（必须）

实现 **`IAoiDetectorDescriptor`**，供 `AoiDetectorFactory` 在启动时反射注册：

| 成员 | 说明 |
|------|------|
| `TypeKey` | 主键，与业务里 `CreateDetector("...")` 一致（不区分大小写会规范成小写存表）。 |
| `Aliases` | 可选别名列表，与 `TypeKey` 指向同一套 `Create`。 |
| `DisplayName` | 给人看的名称（如配置 UI）。 |
| `Create(ILogger)` | 返回 `new YourDetector(logger)`，**建议**检测器构造函数只收 `ILogger`，其它依赖用 `Initialize` 注入。 |

要求：**公共类、无参实例构造**，且 **不要**再标 `[ExampleTemplate]`（否则不会注册）。

### 3. 与业务算法对接

- 在需要 AOI 的算法（如 `DetectionAlgorithmBase` 子类）中注入 **`AoiDetectorFactory`**，在 `OnInitialize` 里 **`CreateDetector(typeKey)`** 并 **`Initialize(...)`**。
- 每帧对目标框裁 **`Mat` ROI** 后调用 **`Detect(roi, parameters)`**，把 `AoiResult` 写回上下文或参与状态机判定。

工厂注册与扩展说明见 **`../AoiDetectorFactory.cs`** 及接口 **`../IAoiDetector.cs`**、`../IAoiDetectorDescriptor.cs`。

---

## 复制为新 AOI 时的步骤

1. 将整个 **`Example`** 目录复制到 `Aoi/<YourName>/`（或平铺到 `Aoi` 下，按你项目习惯）。
2. 全局改名：`ExampleAoi` → 你的前缀；**`TypeKey`** 与配置/代码里的 **`CreateDetector` 字符串**一致。
3. 删除 **`ExampleAoiDescriptor`** 上的 **`[ExampleTemplate]`**。
4. 在 **`OnDetect`**（及可选 **`OnInitialize`**）中实现你的 OpenCV 流程，并规范填充 **`AoiResult`**。

---

## 相关代码

| 位置 | 说明 |
|------|------|
| `../AoiDetectorBase.cs` | 参数合并、Detect/DetectBatch 默认行为 |
| `../AoiResult.cs` | 结果模型与 `UShapeResultExtensions` 类比例子 |
| `../UDet/README.md` | 已上线 U 型 AOI 的参数与输出字段说明 |

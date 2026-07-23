# UDet — U 型符号方向检测（AOI）

本目录实现 **U 型（开口）方向检测** 的纯视觉 AOI 算法，不依赖深度学习推理，适用于 ROI 内仅有 U 形轮廓的场景。

## 功能概述

- **目标**：在输入 ROI 中识别 U 形轮廓，估计 **开口朝向**（角度 + 方向标签）。
- **方法**：图像预处理 → 轮廓提取与筛选 → 取主轮廓并平滑 → **凸包缺陷（Convexity Defects）** 定位开口两端 → 结合质心计算朝向；若无有效缺陷则回退到 **最小外接矩形 + 距质心最远两点**。

## 模块结构

| 文件 | 职责 |
|------|------|
| `UShapeDetector.cs` | 入口 `UShapeDetector`，继承 `AoiDetectorBase`，串联全流程并填充 `AoiResult` |
| `ImagePreprocessor.cs` | 重采样、灰度、CLAHE、Canny + Otsu 二值与形态学融合边缘 |
| `ContourAnalyzer.cs` | `FindContours`、按面积/中心距离筛选、`ApproxPolyDP` 平滑 |
| `OrientationCalculator.cs` | 凸包缺陷分析、角度与 `Up/Right/Down/Left`、置信度 |

## 在工厂中的注册名

通过 `AoiDetectorFactory` 创建时使用（不区分大小写，内部会规范为小写）：

- `u_shape`
- `udet`（与 `u_shape` 相同实现的别名）

## 输入参数（`Detect` 的 `parameters`）

| 键 | 类型 | 默认值 | 说明 |
|----|------|--------|------|
| `target_size` | int | `256` | ROI 短边小于该值时按比例放大，便于小图稳定出边 |
| `min_area_ratio` | double | `0.005` | 轮廓面积占图像面积的下限比例 |
| `max_area_ratio` | double | `0.95` | 轮廓面积占图像面积的上限比例 |
| `smooth_factor` | double | `0.003` | `ApproxPolyDP` 的 ε = `smooth_factor × 轮廓周长` |

## 输出（`AoiResult`）

成功时除 `Success`、`Message`、`Confidence` 外，常用数据键包括：

| 键 | 含义 |
|----|------|
| `angle` | 朝向角度（度，与 `OrientationCalculator` 中定义一致） |
| `direction` | `Up` / `Right` / `Down` / `Left` |
| `center` | 开口中心（`Point2f`，与 `OpeningCenter` 一致） |
| `centroid` | 轮廓质心 |
| `endpoint1` / `endpoint2` | 开口两侧端点（`Point`） |
| `contour_point_count` | 选用轮廓经平滑后的顶点数（不存放完整点集，避免大对象滞留） |
| `scale` | 相对原 ROI 的重采样缩放倍数（未放大则为 `1`） |

也可使用扩展方法：`GetAngle()`、`GetDirection()`、`GetCenter()` 等（见 `AoiResult.cs`）。

## 失败与回退

- 无满足条件的轮廓：`Success = false`，提示「未找到有效的 U 型轮廓」。
- 质心无效：`Success = false`。
- 无凸包缺陷：`OrientationCalculator` 使用备用算法，置信度通常较低（约 `0.5`）。

## 依赖

- OpenCvSharp（`Mat`、轮廓与形态学等）
- `Microsoft.Extensions.Logging`（调试与错误日志）

## 相关代码

- 抽象基类与接口：`../AoiDetectorBase.cs`、`../IAoiDetector.cs`
- 工厂：`../AoiDetectorFactory.cs`

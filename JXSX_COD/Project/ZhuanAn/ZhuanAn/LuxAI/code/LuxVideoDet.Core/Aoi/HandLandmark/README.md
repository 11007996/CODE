# HandLandmark（MediaPipe 手部关键点 AOI）

本目录实现 **手部 2D 关键点** 推理与结果模型（JSON 反序列化、坐标映射、画面叠加）。**要使手部 AOI 真正跑起来，必须在 Python 侧完成算法/运行环境集成**；仅编译 C# 工程不会自动具备推理能力。

## 为何依赖 Python

- 推理由 **MediaPipe / luxvideopyplugin** 在 **独立 Python 子进程** 中执行，C# 负责启动 worker、经 **stdin/stdout** 传 BGR 与 JSON 结果。
- 注册类型 **`hand_landmarker_subprocess`**（别名 `mediapipe_hand_subprocess`）对应实现 **`HandLandmarkerSubprocessAoiDetector`**，子进程入口为 **`python -m luxvideopyplugin.cli.hand_worker`**。与主进程 **OpenCvSharp** 分离，避免同进程加载多套 OpenCV（尤其在 macOS 等环境下减少冲突）。

## 使用前你需要准备什么

1. **Python 虚拟环境**（venv），并在该环境中安装 **luxvideopyplugin**（例如对 venv 执行 `pip install` 对应 wheel/包）。
2. 在启用手部 AOI 的算法配置里填写 **`mediapipa_venv`**（venv 根路径；键名为历史兼容，指向已安装该包的 venv）。未配置时，相关算法会记录警告并 **不启用手部推理**（仅跑其余逻辑）。
3. C# 启动子进程时会自动设置 **`LUXVIDEO_VENV`** = 上述 venv 的绝对路径，供 **luxvideopyplugin** 解析插件/项目根目录，从而可在 **pyplugin 根目录** 放置 `hand_landmarker.task`，通常**无需**再配 `HAND_LANDMARKER_MODEL_PATH`（仍以插件方文档为准）。
4. 可选 AOI 参数：`hand_landmarker_model_path`（显式覆盖模型路径）、`luxvideo_root`（`LUXVIDEO_ROOT`）、`extra_python_path`（追加 `PYTHONPATH`）、下采样长边、`num_hands` 等；具体字段以各算法的 Descriptor / `args` 说明为准。
5. 首帧或首次 `Detect` 时会拉起子进程并等待 worker 输出 **`READY`**；若 Python 未就绪、缺少依赖或 worker 崩溃，日志中会有相应错误，需检查 **venv 路径、pip 依赖与 Python stderr**。

## 与本目录相关的 C# 类型（便于搜代码）

| 注册名 | 说明 |
|--------|------|
| `hand_landmarker_subprocess` | 子进程 stdio + `hand_worker`（**唯一**手部 AOI 注册路径） |
| `mediapipe_hand_subprocess` | 同上（别名） |

结果载荷中的手部列表、landmark 坐标等见 `HandLandmarkerModels.cs`；归一化坐标到原图像素的映射见 `HandLandmarkerCoordinates.cs`。

## 小结

**没有配置好 Python venv + luxvideopyplugin（及可启动的 `hand_worker`），手部 Landmark AOI 不会工作。** 部署与排错时请先确认子进程能否单独在该 venv 下执行 `python -m luxvideopyplugin.cli.hand_worker`。

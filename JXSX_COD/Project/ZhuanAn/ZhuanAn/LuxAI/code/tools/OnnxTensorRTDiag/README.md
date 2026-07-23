# OnnxTensorRTDiag

用于排查 **ONNX Runtime TensorRT 执行提供器** 环境的小工具：检查原生库是否可加载、缺失时提示应安装的组件；在提供 `.onnx` 时还会 **创建 TensorRT Session 并跑一次推理**（与 LuxVideoDet 主程序一致：仅注册 TensorRT EP，不追加 CUDA EP）。

## 前置条件

- .NET SDK 8.0+
- **TensorRT EP 仅支持 Windows / Linux（NVIDIA）**；macOS 上会直接提示不支持
- 完整推理验证需要本机已安装匹配的 **NVIDIA 驱动、CUDA、TensorRT**，并将 TensorRT / CUDA 的 `bin`、`lib` 加入 `PATH`（Linux 上常需 `LD_LIBRARY_PATH`）
- 引用 `Microsoft.ML.OnnxRuntime.Gpu`（与仓库 `LuxVideoDet.Core` 中 OnnxRuntime 版本对齐）

## 运行方式

在仓库根目录：

**仅环境探测（不需要模型）：**

```bash
dotnet run --project tools/OnnxTensorRTDiag -- --env-only
```

**环境 + 加载模型 + 单次 TensorRT 推理：**

```bash
dotnet run --project tools/OnnxTensorRTDiag -- path/to/model.onnx
```

## 日志

输出同时写入控制台与日志文件：

- 目录：可执行文件同级（`AppContext.BaseDirectory`）
- 文件名：`OnnxTensorRTDiag_yyyyMMdd_HHmmss.log`

## 退出码

| 码 | 含义 |
|----|------|
| 0 | 成功：`--env-only` 下基础探测通过，或完整流程下 Session + Run 成功 |
| 1 | 环境或 TensorRT 会话/推理失败（日志中含详情与安装提示） |
| 2 | 参数错误（例如未提供模型且未使用 `--env-only`） |

## 参考

- [TensorRT Execution Provider（官方）](https://onnxruntime.ai/docs/execution-providers/TensorRT-ExecutionProvider.html)

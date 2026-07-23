# OnnxCudaDiag

`OnnxCudaDiag` 是一个用于排查 ONNX Runtime GPU（CUDA Execution Provider）环境问题的小工具。

它会做以下检查：

- 输出运行环境信息（OS、架构、.NET 版本、目录等）
- 输出 `OnnxRuntime` 托管库版本
- 列出程序目录下与 `onnxruntime/cuda` 相关的关键 DLL
- 在 Windows 上尝试 `LoadLibrary` 关键 DLL（如 `nvcuda.dll`、`onnxruntime_providers_cuda.dll`）
- 尝试创建 CUDA 推理 Session，验证模型是否能在 CUDA EP 下加载

## 前置条件

- .NET SDK 8.0+
- 可用的 ONNX 模型文件（`.onnx`）
- 若要验证 CUDA，需要目标机器已安装对应 NVIDIA 驱动与 CUDA 运行环境

## 运行方式

在仓库根目录执行：

```bash
dotnet run --project tools/OnnxCudaDiag -- <model.onnx>
```

示例：

```bash
dotnet run --project tools/OnnxCudaDiag -- "models/yolov8n.onnx"
```

## 日志输出

程序运行时会把输出同时写到控制台和日志文件。

- 日志目录：程序可执行文件同级目录（`AppContext.BaseDirectory`）
- 文件名格式：`OnnxCudaDiag_yyyyMMdd_HHmmss.log`

运行输出中也会显示实际日志路径，例如：

```text
LogFile             : /path/to/tools/OnnxCudaDiag/bin/Debug/net8.0/OnnxCudaDiag_20260324_153000.log
日志已写入: /path/to/tools/OnnxCudaDiag/bin/Debug/net8.0/OnnxCudaDiag_20260324_153000.log
```

## 退出码说明

- `0`：CUDA Session 诊断成功
- `1`：诊断失败（如依赖缺失、架构不匹配、ONNX Runtime 异常等）
- `2`：参数错误（未提供模型路径或模型文件不存在）

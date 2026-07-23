using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

internal static class Program
{
    private static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var logFilePath = Path.Combine(
            AppContext.BaseDirectory,
            $"OnnxTensorRTDiag_{DateTime.Now:yyyyMMdd_HHmmss}.log");

        var originalOut = Console.Out;
        var originalError = Console.Error;
        StreamWriter? logWriter = null;
        TeeTextWriter? teeWriter = null;

        try
        {
            logWriter = new StreamWriter(logFilePath, append: false, Encoding.UTF8) { AutoFlush = true };
            teeWriter = new TeeTextWriter(originalOut, logWriter);
            Console.SetOut(teeWriter);
            Console.SetError(teeWriter);

            Console.WriteLine("=== ONNX Runtime TensorRT 环境诊断工具 ===");
            Console.WriteLine($"Time                : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"OS                  : {RuntimeInformation.OSDescription}");
            Console.WriteLine($"ProcessArchitecture : {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine($"OSArchitecture      : {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"Framework           : {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"CurrentDirectory    : {Environment.CurrentDirectory}");
            Console.WriteLine($"BaseDirectory       : {AppContext.BaseDirectory}");
            Console.WriteLine($"LogFile             : {logFilePath}");
            Console.WriteLine();

            var envOnly = args.Any(a => string.Equals(a, "--env-only", StringComparison.OrdinalIgnoreCase)
                                         || string.Equals(a, "-e", StringComparison.OrdinalIgnoreCase));
            var modelPath = args.FirstOrDefault(a => !a.StartsWith("-", StringComparison.Ordinal)) ?? string.Empty;

            if (!envOnly && string.IsNullOrWhiteSpace(modelPath))
            {
                Console.WriteLine("用法:");
                Console.WriteLine("  仅检查环境（不加载模型、不做推理）:");
                Console.WriteLine("    dotnet run --project tools/OnnxTensorRTDiag -- --env-only");
                Console.WriteLine("  完整诊断（环境 + TensorRT 加载模型并跑一次推理）:");
                Console.WriteLine("    dotnet run --project tools/OnnxTensorRTDiag -- <model.onnx>");
                Console.WriteLine();
                Console.WriteLine("说明: TensorRT EP 仅支持 Windows / Linux + NVIDIA；需要安装匹配的 NVIDIA 驱动、CUDA、TensorRT，");
                Console.WriteLine("      并将 TensorRT 的 lib/bin 加入 PATH（或设置 ORT_TENSORRT_PATH 等，见 ONNX Runtime 文档）。");
                return 2;
            }

            PrintOnnxRuntimeVersion();
            Console.WriteLine();

            if (!IsTensorRtEpSupportedOs())
            {
                Console.WriteLine("[FAIL] TensorRT 执行提供器仅在 Windows / Linux 上与 ONNX Runtime 配合使用（不支持 macOS）。");
                Console.WriteLine("       需要: 在装有 NVIDIA GPU 的 Windows 或 Linux 上运行本工具。");
                Console.WriteLine("       安装: 不适用本机平台，请更换环境或使用 CPU/CUDA/CoreML 等其它 EP。");
                return 1;
            }

            if (RuntimeInformation.ProcessArchitecture != Architecture.X64
                && RuntimeInformation.ProcessArchitecture != Architecture.Arm64)
            {
                Console.WriteLine("[WARN] TensorRT 官方支持以 x64 为主；当前进程架构可能不受支持。");
            }

            PrintRelevantNativeArtifacts(AppContext.BaseDirectory);
            Console.WriteLine();

            var envOk = RunNativeProbes(out var probeDetailsFailed);
            if (!envOk)
            {
                Console.WriteLine();
                Console.WriteLine($"[FAIL] 以下必需项未通过: {probeDetailsFailed}");
                PrintTensorRtInstallHints();
                Console.WriteLine();
                Console.WriteLine($"诊断结束（环境未就绪），日志: {logFilePath}");
                return 1;
            }

            if (envOnly)
            {
                Console.WriteLine("[OK] 基础原生依赖探测通过（未执行 Session/推理）。");
                Console.WriteLine($"诊断结束，日志: {logFilePath}");
                return 0;
            }

            if (!File.Exists(modelPath))
            {
                Console.WriteLine($"[ERROR] 模型文件不存在: {modelPath}");
                return 2;
            }

            var exitCode = RunTensorRtSessionAndInfer(modelPath);
            Console.WriteLine();
            Console.WriteLine($"诊断结束，ExitCode={exitCode}");
            Console.WriteLine($"日志已写入: {logFilePath}");
            return exitCode;
        }
        finally
        {
            Console.SetOut(originalOut);
            Console.SetError(originalError);
            teeWriter?.Dispose();
            logWriter?.Dispose();
        }
    }

    private static bool IsTensorRtEpSupportedOs()
    {
        return OperatingSystem.IsWindows() || OperatingSystem.IsLinux();
    }

    private static void PrintOnnxRuntimeVersion()
    {
        try
        {
            var asm = typeof(InferenceSession).Assembly;
            var version = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                          ?? asm.GetName().Version?.ToString()
                          ?? "unknown";
            Console.WriteLine($"OnnxRuntime.Managed : {version}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WARN] 获取 OnnxRuntime 版本失败: {ex.Message}");
        }
    }

    private static void PrintRelevantNativeArtifacts(string dir)
    {
        Console.WriteLine($"[{dir}] 与 TensorRT / CUDA / ORT 相关的原生文件（浅层扫描）:");
        if (!Directory.Exists(dir))
        {
            Console.WriteLine("  目录不存在。");
            return;
        }

        var patterns = new[] { "*.dll", "*.so", "*.dylib" };
        var keywords = new[]
        {
            "onnxruntime", "tensorrt", "nvinfer", "nvonnxparser", "cudart", "cublas", "cudnn",
            "cuda", "nppc", "nppial"
        };

        var found = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var pattern in patterns)
        {
            foreach (var file in Directory.EnumerateFiles(dir, pattern, SearchOption.TopDirectoryOnly))
            {
                var name = Path.GetFileName(file);
                if (keywords.Any(k => name.Contains(k, StringComparison.OrdinalIgnoreCase)))
                    found.Add(name);
            }
        }

        if (found.Count == 0)
        {
            Console.WriteLine("  未在输出目录匹配到关键字文件（可能仍能从系统 PATH 加载 TensorRT/CUDA）。");
            return;
        }

        foreach (var name in found.OrderBy(s => s, StringComparer.OrdinalIgnoreCase))
            Console.WriteLine($"  - {name}");
    }

    private static bool RunNativeProbes(out string failedSummary)
    {
        failedSummary = string.Empty;
        var failed = new List<string>();
        var ok = true;

        if (OperatingSystem.IsWindows())
        {
            ok &= ProbeLoad("NVIDIA CUDA 驱动用户态库 nvcuda.dll", "nvcuda.dll", required: true, failed);
            ok &= ProbeLoad("ONNX Runtime TensorRT EP", "onnxruntime_providers_tensorrt.dll", required: true, failed);
            ProbeLoad("TensorRT 推理库（可选探测）", "nvinfer_10.dll", required: false, failed);
            ProbeLoad("TensorRT 推理库（可选探测）", "nvinfer.dll", required: false, failed);
        }
        else if (OperatingSystem.IsLinux())
        {
            ok &= ProbeLoad("NVIDIA CUDA 驱动（libcuda）", "libcuda.so.1", required: true, failed);
            ok &= ProbeLoad("ONNX Runtime TensorRT EP", "libonnxruntime_providers_tensorrt.so", required: true, failed);
            ProbeLoad("TensorRT（可选探测）", "libnvinfer.so.10", required: false, failed);
            ProbeLoad("TensorRT（可选探测）", "libnvinfer.so", required: false, failed);
        }

        failedSummary = string.Join("; ", failed);
        return ok;
    }

    private static bool ProbeLoad(string description, string libraryName, bool required, List<string> failed)
    {
        try
        {
            var handle = NativeLibrary.Load(libraryName);
            NativeLibrary.Free(handle);
            Console.WriteLine($"[OK]   {description}: {libraryName}");
            return true;
        }
        catch (DllNotFoundException ex)
        {
            if (required)
            {
                Console.WriteLine($"[FAIL] {description}: 无法加载 {libraryName}");
                Console.WriteLine($"       {ex.Message}");
                failed.Add($"{libraryName} ({description})");
            }
            else
                Console.WriteLine($"[SKIP] {description}: 未找到 {libraryName}（可选）");

            return !required;
        }
        catch (Exception ex)
        {
            if (required)
            {
                Console.WriteLine($"[FAIL] {description}: {libraryName} — {ex.GetType().Name}: {ex.Message}");
                failed.Add($"{libraryName} ({description})");
            }
            else
                Console.WriteLine($"[SKIP] {description}: {libraryName} — {ex.Message}");

            return !required;
        }
    }

    private static void PrintTensorRtInstallHints()
    {
        Console.WriteLine();
        Console.WriteLine("—— 建议安装的组件（按常见缺失顺序）——");
        Console.WriteLine("1) NVIDIA 显卡驱动：与 GPU 匹配的最新 Studio 或 Game Ready / Linux 专有驱动。");
        Console.WriteLine("2) CUDA Toolkit：与 ONNX Runtime GPU 包、TensorRT 说明中要求的 CUDA 主版本一致（见 onnxruntime.ai 发布说明）。");
        Console.WriteLine("3) TensorRT：从 NVIDIA 开发者站下载与 CUDA 版本匹配的 TensorRT，安装后将 lib/bin 加入 PATH。");
        Console.WriteLine("4) cuDNN：若 ORT/模型需要，按 NVIDIA 文档安装并与 CUDA 路径一致。");
        Console.WriteLine("5) 本工具与主工程均引用 Microsoft.ML.OnnxRuntime.Gpu；无需单独再装「CPU 版」ORT。");
        Console.WriteLine("6) Linux：若仍失败，检查 LD_LIBRARY_PATH 是否包含 TensorRT 的 lib 与 CUDA 的 lib64。");
        Console.WriteLine("7) 文档: https://onnxruntime.ai/docs/execution-providers/TensorRT-ExecutionProvider.html");
    }

    private static int RunTensorRtSessionAndInfer(string modelPath)
    {
        Console.WriteLine("开始 TensorRT Session 与单次推理验证（与 LuxVideoDet 主程序一致：仅注册 TensorRT EP，不追加 CUDA EP）...");
        try
        {
            using var options = new SessionOptions
            {
                LogId = "OnnxTensorRTDiag",
                LogSeverityLevel = OrtLoggingLevel.ORT_LOGGING_LEVEL_WARNING,
                LogVerbosityLevel = 0
            };

            using var trtOpts = new OrtTensorRTProviderOptions();
            var trtDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["device_id"] = "0",
                ["trt_fp16_enable"] = "1",
                ["trt_engine_cache_enable"] = "1",
            };
            trtOpts.UpdateOptions(trtDict);
            options.AppendExecutionProvider_Tensorrt(trtOpts);

            Console.WriteLine("Step1: AppendExecutionProvider_Tensorrt — OK（配置已应用）");

            Console.WriteLine("Step2: 创建 InferenceSession（可能触发 TensorRT 引擎构建，首次较慢）");
            using var session = new InferenceSession(modelPath, options);
            Console.WriteLine($"  [OK] Session 创建成功 | Inputs={session.InputMetadata.Count}, Outputs={session.OutputMetadata.Count}");

            var firstInput = session.InputMetadata.First();
            var inputName = firstInput.Key;
            var meta = firstInput.Value;
            var shape = FixDynamicDimensions(meta.Dimensions);
            Console.WriteLine($"  输入: name={inputName}, shape=[{string.Join(",", shape)}], elementType={meta.ElementDataType}");

            var elementCount = shape.Aggregate(1, (a, b) => a * b);
            var data = new float[elementCount]; // 全零即可做连通性验证

            Console.WriteLine("Step3: Run 单次推理");
            var inputTensor = new DenseTensor<float>(data, shape);
            var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor(inputName, inputTensor) };
            using var results = session.Run(inputs);
            var outputNames = string.Join(", ", results.Select(r => r.Name));
            Console.WriteLine($"  [OK] Run 完成，输出: {outputNames}");

            return 0;
        }
        catch (OnnxRuntimeException ex)
        {
            Console.WriteLine("[ONNXRuntimeException]");
            Console.WriteLine(ex.ToString());
            PrintTensorRtInstallHints();
            return 1;
        }
        catch (DllNotFoundException ex)
        {
            Console.WriteLine("[DllNotFoundException]");
            Console.WriteLine(ex.ToString());
            PrintTensorRtInstallHints();
            return 1;
        }
        catch (BadImageFormatException ex)
        {
            Console.WriteLine("[BadImageFormatException]（常见于 x86/x64 或架构不匹配）");
            Console.WriteLine(ex.ToString());
            return 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Exception] {ex.GetType().FullName}");
            Console.WriteLine(ex.ToString());
            PrintTensorRtInstallHints();
            return 1;
        }
    }

    /// <summary>将动态维度（≤0）替换为占位值以便试跑：batch=1，空间维默认 640（常见检测输入）。</summary>
    private static int[] FixDynamicDimensions(int[] dimensions)
    {
        var n = dimensions.Length;
        var shape = new int[n];
        for (var i = 0; i < n; i++)
        {
            var d = dimensions[i];
            if (d > 0)
            {
                shape[i] = d;
                continue;
            }

            shape[i] = i switch
            {
                0 => 1,
                _ when n == 4 && (i == 2 || i == 3) => 640,
                _ => 1
            };
        }

        return shape;
    }
}

internal sealed class TeeTextWriter : TextWriter
{
    private readonly TextWriter[] _targets;

    public TeeTextWriter(params TextWriter[] targets)
    {
        _targets = targets;
    }

    public override Encoding Encoding => _targets.Length > 0 ? _targets[0].Encoding : Encoding.UTF8;

    public override void Write(char value)
    {
        foreach (var target in _targets)
            target.Write(value);
    }

    public override void Write(string? value)
    {
        foreach (var target in _targets)
            target.Write(value);
    }

    public override void WriteLine(string? value)
    {
        foreach (var target in _targets)
            target.WriteLine(value);
    }

    public override void Flush()
    {
        foreach (var target in _targets)
            target.Flush();
    }
}

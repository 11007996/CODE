using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.ML.OnnxRuntime;

internal static class Program
{
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern nint LoadLibrary(string lpFileName);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool FreeLibrary(nint hModule);

    private static int Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var logFilePath = Path.Combine(
            AppContext.BaseDirectory,
            $"OnnxCudaDiag_{DateTime.Now:yyyyMMdd_HHmmss}.log");

        var originalOut = Console.Out;
        var originalError = Console.Error;
        StreamWriter? logWriter = null;
        TeeTextWriter? teeWriter = null;

        try
        {
            logWriter = new StreamWriter(logFilePath, append: false, Encoding.UTF8)
            {
                AutoFlush = true
            };
            teeWriter = new TeeTextWriter(originalOut, logWriter);
            Console.SetOut(teeWriter);
            Console.SetError(teeWriter);

            Console.WriteLine("=== ONNX Runtime CUDA 诊断工具 ===");
            Console.WriteLine($"Time                : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"OS                  : {RuntimeInformation.OSDescription}");
            Console.WriteLine($"ProcessArchitecture : {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine($"OSArchitecture      : {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"Framework           : {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"CurrentDirectory    : {Environment.CurrentDirectory}");
            Console.WriteLine($"BaseDirectory       : {AppContext.BaseDirectory}");
            Console.WriteLine($"LogFile             : {logFilePath}");
            Console.WriteLine();

            var modelPath = args.Length > 0 ? args[0] : string.Empty;
            if (string.IsNullOrWhiteSpace(modelPath))
            {
                Console.WriteLine("用法: dotnet run --project tools/OnnxCudaDiag -- <model.onnx>");
                Console.WriteLine("示例: dotnet run --project tools/OnnxCudaDiag -- \"C:\\path\\model.onnx\"");
                return 2;
            }

            if (!File.Exists(modelPath))
            {
                Console.WriteLine($"[ERROR] 模型文件不存在: {modelPath}");
                return 2;
            }

            PrintOnnxRuntimeVersion();
            Console.WriteLine();

            PrintDirectoryDlls(AppContext.BaseDirectory);
            Console.WriteLine();

            ProbeDll("nvcuda.dll");
            ProbeDll("onnxruntime.dll");
            ProbeDll("onnxruntime_providers_shared.dll");
            ProbeDll("onnxruntime_providers_cuda.dll");
            Console.WriteLine();

            var exitCode = RunCudaSessionProbe(modelPath);
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

    private static void PrintDirectoryDlls(string dir)
    {
        Console.WriteLine($"[{dir}] 关键 DLL 列表:");
        if (!Directory.Exists(dir))
        {
            Console.WriteLine("  目录不存在。");
            return;
        }

        var keywords = new[] { "onnxruntime", "cuda", "cudnn", "cublas", "cufft", "curand", "cusolver", "cusparse" };
        var files = Directory
            .EnumerateFiles(dir, "*.dll", SearchOption.TopDirectoryOnly)
            .Where(f => keywords.Any(k => Path.GetFileName(f).Contains(k, StringComparison.OrdinalIgnoreCase)))
            .OrderBy(Path.GetFileName)
            .ToList();

        if (files.Count == 0)
        {
            Console.WriteLine("  未找到匹配 DLL。");
            return;
        }

        foreach (var file in files)
        {
            Console.WriteLine($"  - {Path.GetFileName(file)}");
        }
    }

    private static void ProbeDll(string dllName)
    {
        var handle = LoadLibrary(dllName);
        if (handle != nint.Zero)
        {
            Console.WriteLine($"[OK]   LoadLibrary({dllName})");
            FreeLibrary(handle);
            return;
        }

        var error = Marshal.GetLastWin32Error();
        var message = new Win32Exception(error).Message;
        Console.WriteLine($"[FAIL] LoadLibrary({dllName}) => Win32Error={error} ({message})");
    }

    private static int RunCudaSessionProbe(string modelPath)
    {
        Console.WriteLine("开始 CUDA Session 诊断...");
        try
        {
            using var options = new SessionOptions
            {
                LogId = "OnnxCudaDiag",
                LogSeverityLevel = OrtLoggingLevel.ORT_LOGGING_LEVEL_VERBOSE,
                LogVerbosityLevel = 1
            };

            Console.WriteLine("Step1: AppendExecutionProvider_CUDA(0)");
            options.AppendExecutionProvider_CUDA(0);
            Console.WriteLine("  [OK] CUDA EP 已附加");

            Console.WriteLine("Step2: 创建 InferenceSession");
            using var session = new InferenceSession(modelPath, options);
            Console.WriteLine("  [OK] Session 创建成功");
            Console.WriteLine($"  Inputs={session.InputMetadata.Count}, Outputs={session.OutputMetadata.Count}");

            return 0;
        }
        catch (OnnxRuntimeException ex)
        {
            Console.WriteLine("[ONNXRuntimeException]");
            Console.WriteLine(ex.ToString());
            return 1;
        }
        catch (DllNotFoundException ex)
        {
            Console.WriteLine("[DllNotFoundException]");
            Console.WriteLine(ex.ToString());
            return 1;
        }
        catch (BadImageFormatException ex)
        {
            Console.WriteLine("[BadImageFormatException] (常见于 x86/x64 架构不匹配)");
            Console.WriteLine(ex.ToString());
            return 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine("[Exception]");
            Console.WriteLine(ex.ToString());
            return 1;
        }
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
        {
            target.Write(value);
        }
    }

    public override void Write(string? value)
    {
        foreach (var target in _targets)
        {
            target.Write(value);
        }
    }

    public override void WriteLine(string? value)
    {
        foreach (var target in _targets)
        {
            target.WriteLine(value);
        }
    }

    public override void Flush()
    {
        foreach (var target in _targets)
        {
            target.Flush();
        }
    }
}

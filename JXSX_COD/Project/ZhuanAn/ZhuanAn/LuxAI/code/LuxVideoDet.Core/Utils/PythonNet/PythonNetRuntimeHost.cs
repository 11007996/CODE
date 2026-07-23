using System.Diagnostics;
using System.Text.Json;
using Python.Runtime;

namespace LuxVideoDet.Core.Utils.PythonNet;

/// <summary>
/// 进程内单次初始化 Python 运行时，供嵌入推理等模块复用。
/// </summary>
public static class PythonNetRuntimeHost
{
    private static readonly object Gate = new();
    private static bool _initialized;

    /// <summary>是否已成功执行过 <see cref="EnsureInitialized"/>。</summary>
    public static bool IsInitialized
    {
        get
        {
            lock (Gate)
                return _initialized;
        }
    }

    /// <summary>
    /// 线程安全、幂等：按配置探测并调用 <c>PythonEngine.Initialize()</c>。
    /// </summary>
    public static void EnsureInitialized(PythonNetRuntimeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        lock (Gate)
        {
            if (_initialized)
                return;

            var exe = options.PythonExecutablePath ?? ResolveVenvPythonExecutable(options.VenvRoot);
            if (string.IsNullOrWhiteSpace(exe) || !File.Exists(exe))
                throw new InvalidOperationException($"未找到 Python 解释器: {exe}");

            var probe = ProbeInterpreter(exe);
            var dll = !string.IsNullOrEmpty(options.PythonDllPath)
                ? options.PythonDllPath!
                : probe.PythonDllPath;

            if (string.IsNullOrEmpty(dll) || !File.Exists(dll))
            {
                throw new InvalidOperationException(
                    "无法解析 libpython 路径，请在 PythonNetRuntimeOptions.PythonDllPath 中指定绝对路径。");
            }

            var pathParts = new List<string>();
            foreach (var p in options.ExtraPythonPath)
            {
                if (!string.IsNullOrWhiteSpace(p) && Directory.Exists(p))
                    pathParts.Add(Path.GetFullPath(p));
            }

            if (!string.IsNullOrEmpty(probe.SitePackages))
                pathParts.Add(probe.SitePackages);

            if (!string.IsNullOrEmpty(probe.Stdlib))
                pathParts.Add(probe.Stdlib);

            var joined = string.Join(Path.PathSeparator.ToString(), pathParts);

            Runtime.PythonDLL = dll;
            PythonEngine.PythonHome = probe.BasePrefix;
            PythonEngine.PythonPath = joined;
            PythonEngine.Initialize();
            _initialized = true;
        }
    }

    /// <summary>释放 Python 运行时（进程退出或测试收尾时调用）。</summary>
    public static void Shutdown()
    {
        lock (Gate)
        {
            if (!_initialized)
                return;

            PythonEngine.Shutdown();
            _initialized = false;
        }
    }

    /// <summary>对解释器执行探测脚本，解析 base_prefix / stdlib / site-packages / libpython。</summary>
    public static PythonNetProbeResult ProbeInterpreter(string pythonExecutablePath)
    {
        const string script = """
import json, os, sys, sysconfig
base = getattr(sys, "base_prefix", sys.prefix) or sys.prefix
paths = sysconfig.get_paths()
stdlib = paths.get("stdlib") or ""
libdir = sysconfig.get_config_var("LIBDIR") or ""
ldlibrary = sysconfig.get_config_var("LDLIBRARY") or ""
dll = ""
if libdir and ldlibrary:
    cand = os.path.join(libdir, ldlibrary)
    if os.path.isfile(cand):
        dll = os.path.abspath(cand)
site_packages = ""
for sp in sys.path:
    if sp and sp.endswith("site-packages") and os.path.isdir(sp):
        site_packages = os.path.abspath(sp)
        break
print(json.dumps({"base_prefix": base, "stdlib": stdlib, "site_packages": site_packages, "python_dll": dll}))
""";

        var psi = new ProcessStartInfo
        {
            FileName = pythonExecutablePath,
            ArgumentList = { "-c", script },
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using var p = Process.Start(psi) ?? throw new InvalidOperationException("无法启动 Python 探测进程");
        var stdout = p.StandardOutput.ReadToEnd();
        var stderr = p.StandardError.ReadToEnd();
        p.WaitForExit(30_000);
        if (p.ExitCode != 0)
            throw new InvalidOperationException($"Python 探测失败 (exit {p.ExitCode}): {stderr}");

        var line = stdout.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .LastOrDefault();
        if (string.IsNullOrEmpty(line))
            throw new InvalidOperationException($"Python 探测无输出: {stderr}");

        using var doc = JsonDocument.Parse(line);
        var root = doc.RootElement;
        var dllPath = root.GetProperty("python_dll").GetString();
        return new PythonNetProbeResult
        {
            BasePrefix = root.GetProperty("base_prefix").GetString() ?? "",
            Stdlib = root.GetProperty("stdlib").GetString() ?? "",
            SitePackages = root.GetProperty("site_packages").GetString() ?? "",
            PythonDllPath = string.IsNullOrEmpty(dllPath) ? null : dllPath,
        };
    }

    /// <summary>解析 venv 下的 Python 可执行文件路径。</summary>
    public static string? ResolveVenvPythonExecutable(string venvRoot)
    {
        if (string.IsNullOrWhiteSpace(venvRoot))
            return null;

        venvRoot = Path.GetFullPath(venvRoot);
        if (OperatingSystem.IsWindows())
        {
            var win = Path.Combine(venvRoot, "Scripts", "python.exe");
            return File.Exists(win) ? win : null;
        }

        var unix = Path.Combine(venvRoot, "bin", "python3");
        if (File.Exists(unix))
            return unix;

        unix = Path.Combine(venvRoot, "bin", "python");
        return File.Exists(unix) ? unix : null;
    }
}

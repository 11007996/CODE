namespace LuxVideoDet.Core.Utils.PythonNet;

/// <summary>
/// Python.NET 嵌入配置：venv、附加 <c>sys.path</c>、可选 DLL 覆盖。
/// </summary>
public sealed class PythonNetRuntimeOptions
{
    /// <summary>虚拟环境根（解析 <c>bin/python3</c> 或 <c>Scripts\python.exe</c>）。</summary>
    public required string VenvRoot { get; init; }

    /// <summary>追加到 <c>PYTHONPATH</c> 的目录（如 luxvideopyplugin 源码或包根）。</summary>
    public IReadOnlyList<string> ExtraPythonPath { get; init; } = Array.Empty<string>();

    /// <summary>覆盖自动探测的 libpython 绝对路径。</summary>
    public string? PythonDllPath { get; init; }

    /// <summary>非空时直接用该解释器探测，忽略 <see cref="VenvRoot"/> 解析出的路径。</summary>
    public string? PythonExecutablePath { get; init; }
}

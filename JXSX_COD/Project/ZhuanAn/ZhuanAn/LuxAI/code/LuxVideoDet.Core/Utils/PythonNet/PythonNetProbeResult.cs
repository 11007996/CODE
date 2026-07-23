namespace LuxVideoDet.Core.Utils.PythonNet;

/// <summary>
/// 由 venv 内解释器探测得到的嵌入参数（用于设置 <c>Runtime.PythonDLL</c>、<c>PythonHome</c>、<c>PythonPath</c>）。
/// </summary>
public sealed class PythonNetProbeResult
{
    public required string BasePrefix { get; init; }

    public required string Stdlib { get; init; }

    public required string SitePackages { get; init; }

    /// <summary>可为空：无法解析时需调用方设置 <see cref="PythonNetRuntimeOptions.PythonDllPath"/>。</summary>
    public string? PythonDllPath { get; init; }
}

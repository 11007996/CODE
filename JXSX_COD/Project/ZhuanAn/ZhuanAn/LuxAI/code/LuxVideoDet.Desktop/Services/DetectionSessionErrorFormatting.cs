using System;

namespace LuxVideoDet.Desktop.Services;

/// <summary>
/// 检测会话相关异常在日志与界面上的可读说明（与 Core 抛出的 <see cref="Exception.Message"/> 一致）。
/// </summary>
public static class DetectionSessionErrorFormatting
{
    /// <summary>用于 Serilog 等：一行内同时包含配置名与原因。</summary>
    public static string FormatStartFailure(string configName, Exception ex) =>
        $"{configName} - {ex.Message}";
}

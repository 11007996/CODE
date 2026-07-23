using System;
using LuxVideoDet.Core.Algorithm.Results;

namespace LuxVideoDet.Desktop.Models;

/// <summary>
/// 单次检测结果事件参数。
/// </summary>
public class DetectionResultEventArgs : EventArgs
{
    public string ConfigId { get; set; } = string.Empty;
    public string AlgorithmType { get; set; } = string.Empty;
    public DetectionResult Result { get; set; } = null!;
}

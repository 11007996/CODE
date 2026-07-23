using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.ML.OnnxRuntime;

namespace LuxVideoDet.Core.Inference.Onnx;

/// <summary>
/// 仅用于读取嵌入在 .onnx 中的 YOLO 风格元数据（如 <c>names</c>），供 ONNX Runtime 与 OpenVINO（读同一 .onnx）共用。
/// </summary>
public static class OnnxModelMetadataReader
{
    /// <summary>
    /// 用 ONNX Runtime 打开模型文件并尝试解析 <c>names</c>，不执行推理。
    /// OpenVINO 加载 .onnx 时无法读到 CustomMetadataMap，因此需借此与 Mac 上纯 ORT 路径行为对齐。
    /// </summary>
    public static List<string>? TryReadYoloClassNamesFromOnnx(string onnxPath, ILogger logger)
    {
        try
        {
            if (!File.Exists(onnxPath))
                return null;

            if (!string.Equals(Path.GetExtension(onnxPath), ".onnx", StringComparison.OrdinalIgnoreCase))
                return null;

            using var session = new InferenceSession(onnxPath);
            var metadata = session.ModelMetadata.CustomMetadataMap;
            if (metadata == null || metadata.Count == 0)
                return null;

            if (!metadata.TryGetValue("names", out var namesStr) || string.IsNullOrWhiteSpace(namesStr))
                return null;

            var list = ParseYoloNamesMetadata(namesStr);
            if (list.Count == 0)
                return null;

            logger.LogInformation(
                "（元数据扫描）从 ONNX 文件读取到 {Count} 个类别: {Names}",
                list.Count,
                string.Join(", ", list.Select(n => $"\"{n}\"")));
            return list;
        }
        catch (Exception ex)
        {
            logger.LogDebug(
                ex,
                "通过 ONNX Runtime 扫描 .onnx 类别元数据失败，OpenVINO 路径将回退为配置中的类别顺序");
            return null;
        }
    }

    /// <summary>
    /// 解析 YOLO 导出的 names 元数据。
    /// 常见形式：<c>{0: 'box', 1: 'label'}</c>；部分工具链会写成 JSON 数组 <c>["box","label"]</c> 或
    /// <c>{"0":"box","1":"label"}</c>。若解析失败，调用方会回退到配置里的类别顺序（易与模型索引错位）。
    /// </summary>
    public static List<string> ParseYoloNamesMetadata(string namesStr)
    {
        var trimmed = namesStr.Trim();

        // 1) Ultralytics 常见：{0: 'box', 1: 'label', ...}
        var dictByIndex = new SortedDictionary<int, string>();
        var matches = Regex.Matches(namesStr, @"(\d+)\s*:\s*['""]([^'""]+)['""]");
        foreach (Match m in matches)
        {
            if (int.TryParse(m.Groups[1].Value, out var idx))
                dictByIndex[idx] = m.Groups[2].Value.Trim();
        }

        if (dictByIndex.Count > 0)
            return dictByIndex.Values.ToList();

        // 2) JSON 数组：["a","b"]
        if (trimmed.StartsWith('['))
        {
            try
            {
                var arr = JsonSerializer.Deserialize<string[]>(trimmed);
                if (arr is { Length: > 0 })
                    return arr.Select(s => s.Trim()).ToList();
            }
            catch
            {
                /* fall through */
            }
        }

        // 3) JSON 对象（字符串键）：{"0":"box","1":"label"}
        if (trimmed.StartsWith('{') && trimmed.Contains('"'))
        {
            try
            {
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(trimmed);
                if (dict is { Count: > 0 })
                {
                    var ordered = dict
                        .Where(kv => int.TryParse(kv.Key, out _))
                        .OrderBy(kv => int.Parse(kv.Key))
                        .Select(kv => kv.Value.Trim())
                        .ToList();
                    if (ordered.Count > 0)
                        return ordered;
                }
            }
            catch
            {
                /* fall through */
            }
        }

        return [];
    }
}

using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi;

/// <summary>
/// AOI 检测器基类
/// </summary>
public abstract class AoiDetectorBase : IAoiDetector
{
    protected readonly ILogger _logger;
    protected Dictionary<string, object> _parameters = new();
    protected bool _initialized = false;

    /// <summary>检测器名称</summary>
    public abstract string Name { get; }

    protected AoiDetectorBase(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 初始化检测器
    /// </summary>
    public virtual void Initialize(Dictionary<string, object>? parameters = null)
    {
        _parameters = parameters ?? new Dictionary<string, object>();

        try
        {
            OnInitialize();
            _initialized = true;
            _logger.LogInformation("AOI 检测器 {Name} 初始化成功", Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AOI 检测器 {Name} 初始化失败", Name);
            throw;
        }
    }

    /// <summary>
    /// 子类重写的初始化方法
    /// </summary>
    protected virtual void OnInitialize()
    {
    }

    /// <summary>
    /// 检测单个 ROI 区域
    /// </summary>
    public AoiResult Detect(Mat roi, Dictionary<string, object>? parameters = null)
    {
        if (!_initialized)
        {
            throw new InvalidOperationException($"检测器 {Name} 未初始化");
        }

        if (roi == null || roi.Empty())
        {
            return new AoiResult { Success = false, Message = "输入图像为空" };
        }

        try
        {
            var activeParams = MergeParameters(parameters);
            return OnDetect(roi, activeParams);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AOI 检测失败: {Name}", Name);
            return new AoiResult { Success = false, Message = ex.Message };
        }
    }

    /// <summary>
    /// 子类实现的检测方法
    /// </summary>
    protected abstract AoiResult OnDetect(Mat roi, Dictionary<string, object> parameters);

    /// <summary>
    /// 批量检测多个 ROI 区域
    /// </summary>
    public virtual AoiResult[] DetectBatch(Mat[] rois, Dictionary<string, object>? parameters = null)
    {
        if (rois == null || rois.Length == 0)
        {
            return Array.Empty<AoiResult>();
        }

        var results = new AoiResult[rois.Length];
        for (int i = 0; i < rois.Length; i++)
        {
            results[i] = Detect(rois[i], parameters);
        }

        return results;
    }

    /// <summary>
    /// 获取参数值
    /// </summary>
    protected T GetParameter<T>(string key, T defaultValue = default!, Dictionary<string, object>? runtimeParams = null)
    {
        if (runtimeParams?.TryGetValue(key, out var runtimeValue) == true)
        {
            try
            {
                return (T)Convert.ChangeType(runtimeValue, typeof(T));
            }
            catch { }
        }

        if (_parameters.TryGetValue(key, out var value))
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch { }
        }

        return defaultValue;
    }

    /// <summary>
    /// 合并参数
    /// </summary>
    private Dictionary<string, object> MergeParameters(Dictionary<string, object>? runtimeParams)
    {
        var merged = new Dictionary<string, object>(_parameters);

        if (runtimeParams != null)
        {
            foreach (var kvp in runtimeParams)
            {
                merged[kvp.Key] = kvp.Value;
            }
        }

        return merged;
    }
}

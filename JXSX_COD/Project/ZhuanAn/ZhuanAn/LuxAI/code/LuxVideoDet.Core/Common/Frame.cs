using OpenCvSharp;

namespace LuxVideoDet.Core.Common;

/// <summary>
/// 帧数据 - 封装 OpenCV Mat，支持引用计数和自动释放
/// </summary>
public class Frame : IDisposable
{
    private Mat? _mat;
    private readonly bool _ownsMat;
    private int _refCount = 1;
    private readonly object _lock = new();
    private bool _disposed;

    public Mat Mat
    {
        get
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Frame));
            return _mat ?? throw new InvalidOperationException("Frame is empty");
        }
    }

    public int Width => Mat.Width;
    public int Height => Mat.Height;
    public bool IsEmpty => _mat == null || _mat.Empty();
    public DateTime Timestamp { get; set; } = DateTime.Now;

    /// <param name="mat">图像缓冲。</param>
    /// <param name="ownsMat">为 true 时 <see cref="Dispose"/> 会释放 <paramref name="mat"/>；为 false 时仅断开引用（供推理引擎等对调用方拥有的 Mat 做临时包装）。</param>
    public Frame(Mat mat, bool ownsMat = true)
    {
        _mat = mat ?? throw new ArgumentNullException(nameof(mat));
        _ownsMat = ownsMat;
    }

    /// <summary>
    /// 包装由调用方拥有生命周期的 <see cref="Mat"/>；本 <see cref="Frame"/> 被回收或 Dispose 时<strong>不会</strong>释放该 Mat。
    /// </summary>
    public static Frame FromBorrowedMat(Mat mat) => new Frame(mat, ownsMat: false);

    /// <summary>
    /// 增加引用计数
    /// </summary>
    public Frame AddRef()
    {
        lock (_lock)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Frame));

            _refCount++;
            return this;
        }
    }

    /// <summary>
    /// 释放引用
    /// </summary>
    public void Release()
    {
        lock (_lock)
        {
            if (_disposed)
                return;

            _refCount--;

            if (_refCount <= 0)
            {
                Dispose();
            }
        }
    }

    /// <summary>
    /// 克隆帧（深拷贝）
    /// </summary>
    public Frame Clone()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(Frame));

        return new Frame(Mat.Clone(), ownsMat: true)
        {
            Timestamp = Timestamp
        };
    }

    public void Dispose()
    {
        lock (_lock)
        {
            if (_disposed)
                return;

            if (_ownsMat)
                _mat?.Dispose();
            _mat = null;
            _disposed = true;
        }

        GC.SuppressFinalize(this);
    }

    ~Frame()
    {
        Dispose();
    }
}

using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using LuxVideoDet.Core.Utils.PythonNet;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>
/// 在独立 Python 子进程中运行 <c>luxvideopyplugin.cli.hand_worker</c>，通过 stdin/stdout 传 BGR 与 JSON 结果；
/// 主进程不加载 Python.NET / libpython，避免与 OpenCvSharp 在同一进程内再加载一套 OpenCV（如 macOS <c>CaptureDelegate</c>）。
/// </summary>
public sealed class HandLandmarkerSubprocessInferenceSession : IDisposable
{
    private readonly HandLandmarkerPythonOptions _options;
    private readonly ILogger? _logger;
    private readonly object _ioLock = new();

    private Process? _process;
    private Stream? _stdin;
    private StreamReader? _stdout;
    private bool _disposed;

    public HandLandmarkerSubprocessInferenceSession(HandLandmarkerPythonOptions options, ILogger? logger = null)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger;
    }

    /// <summary>启动子进程并等待 <c>READY</c>（幂等）。</summary>
    public void EnsureInitialized()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        lock (_ioLock)
        {
            if (_process is { HasExited: false })
            {
                return;
            }

            DisposeProcessUnlocked();
            StartWorkerUnlocked();
        }
    }

    /// <summary>
    /// 将一帧 BGR 送入子进程，返回手部关键点 JSON 反序列化并映射坐标后的结果。
    /// </summary>
    public HandLandmarkerFrameInferenceResult Detect(Mat bgr)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentNullException.ThrowIfNull(bgr);
        if (bgr.Empty())
        {
            throw new ArgumentException("图像为空", nameof(bgr));
        }

        if (bgr.Channels() != 3)
        {
            throw new ArgumentException("需要 BGR 三通道 (Channels=3)", nameof(bgr));
        }

        EnsureInitialized();

        var origW = bgr.Width;
        var origH = bgr.Height;

        Mat? resized = null;
        try
        {
            var inferMat = bgr;
            var iw = origW;
            var ih = origH;

            if (_options.InferenceMaxLongEdgePixels is { } cap && cap > 0)
            {
                (iw, ih) = HandLandmarkerCoordinates.ComputeInferenceSize(origW, origH, cap);
                if (iw != origW || ih != origH)
                {
                    resized = new Mat();
                    Cv2.Resize(bgr, resized, new Size(iw, ih), 0, 0, InterpolationFlags.Nearest);
                    inferMat = resized;
                }
            }

            var w = inferMat.Width;
            var h = inferMat.Height;
            var stride = (int)inferMat.Step();
            var need = (long)stride * h;

            var payload = new byte[need];
            CopyMatBgrRows(inferMat, payload, stride, h);

            string json;
            lock (_ioLock)
            {
                if (_process is not { HasExited: false } || _stdin == null || _stdout == null)
                {
                    throw new InvalidOperationException("手部子进程未就绪或已退出。");
                }

                WriteFrameHeaderAndPayload(_stdin, w, h, stride, payload);
                json = _stdout.ReadLine()
                       ?? throw new InvalidOperationException("子进程未返回 JSON 行。");
            }

            return HandLandmarkerCoordinates.DeserializeAndMap(
                json,
                origW,
                origH,
                w,
                h,
                _options.MapImageLandmarksToOriginalPixels,
                _options.ScaleWorldLandmarksWhenDownsampled);
        }
        finally
        {
            resized?.Dispose();
        }
    }

    private void StartWorkerUnlocked()
    {
        var venvRoot = Path.GetFullPath(_options.VenvRoot.Trim());
        var py = _options.PythonExecutablePath ?? PythonNetRuntimeHost.ResolveVenvPythonExecutable(_options.VenvRoot);
        if (string.IsNullOrWhiteSpace(py) || !File.Exists(py))
        {
            var hint = OperatingSystem.IsWindows()
                ? Path.Combine(venvRoot, "Scripts", "python.exe")
                : Path.Combine(venvRoot, "bin", "python3");
            throw new InvalidOperationException(
                $"未找到虚拟环境中的 Python 解释器。venv 根目录: {venvRoot}{Environment.NewLine}" +
                $"已探测路径示例: {hint}{Environment.NewLine}" +
                "请确认 mediapipa_venv 指向已创建的 venv 根目录（含 bin/python 或 Scripts/python.exe）。");
        }

        var psi = new ProcessStartInfo
        {
            FileName = py,
            ArgumentList = { "-u", "-m", "luxvideopyplugin.cli.hand_worker" },
            WorkingDirectory = venvRoot,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
        };

        psi.Environment["PYTHONUNBUFFERED"] = "1";
        // 供 luxvideopyplugin 解析「插件/项目根」与 hand_landmarker.task 位置（相对 venv 的上一级等），避免仅依赖 cwd=venv 时反复下载到 venv 内。
        psi.Environment["LUXVIDEO_VENV"] = venvRoot;
        psi.Environment["LUXVIDEO_HAND_FRAME_DELTA_MS"] = Math.Max(1, _options.FrameDeltaMs).ToString();
        psi.Environment["LUXVIDEO_HAND_NUM_HANDS"] = Math.Clamp(_options.NumHands, 1, 4).ToString();

        if (!string.IsNullOrWhiteSpace(_options.HandLandmarkerModelPath))
        {
            psi.Environment["HAND_LANDMARKER_MODEL_PATH"] = Path.GetFullPath(_options.HandLandmarkerModelPath.Trim());
        }

        if (!string.IsNullOrWhiteSpace(_options.LuxVideoRoot))
        {
            psi.Environment["LUXVIDEO_ROOT"] = Path.GetFullPath(_options.LuxVideoRoot.Trim());
        }

        if (!string.IsNullOrWhiteSpace(_options.ExtraPythonPathRoot))
        {
            var src = Path.GetFullPath(_options.ExtraPythonPathRoot.Trim());
            if (Directory.Exists(src))
            {
                if (psi.Environment.TryGetValue("PYTHONPATH", out var existing) && !string.IsNullOrWhiteSpace(existing))
                    psi.Environment["PYTHONPATH"] = src + Path.PathSeparator + existing;
                else
                    psi.Environment["PYTHONPATH"] = src;
            }
        }

        _logger?.LogInformation(
            "启动手部子进程 hand_worker：venv={Venv} python={Python} 命令: -m luxvideopyplugin.cli.hand_worker",
            venvRoot,
            py);

        var proc = Process.Start(psi) ?? throw new InvalidOperationException("无法启动 luxvideopyplugin hand_worker 子进程（Process.Start 返回 null）。");
        _process = proc;
        _stdin = proc.StandardInput.BaseStream;
        _stdout = proc.StandardOutput;

        var stderrLock = new object();
        var stderrLines = new List<string>();
        Task stderrTask = Task.Run(() =>
        {
            try
            {
                while (proc.StandardError.ReadLine() is { } line)
                {
                    lock (stderrLock)
                    {
                        stderrLines.Add(line);
                        if (stderrLines.Count > 120)
                            stderrLines.RemoveAt(0);
                    }

                    _logger?.LogWarning("[hand_worker stderr] {Line}", line);
                }
            }
            catch
            {
                // 进程关闭时读端可能结束
            }
        });

        string SnapshotStderr()
        {
            lock (stderrLock)
            {
                return stderrLines.Count == 0
                    ? "(尚无 stderr 输出；若为 ModuleNotFoundError，请在该 venv 内 pip install luxvideopyplugin 或安装对应 whl)"
                    : string.Join(Environment.NewLine, stderrLines);
            }
        }

        string? ready = null;
        try
        {
            ready = proc.StandardOutput.ReadLine();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "读取 hand_worker 首行 stdout 失败。venv={Venv}", venvRoot);
        }

        if (ready == "READY")
            return;

        try
        {
            if (!proc.HasExited)
                proc.Kill(entireProcessTree: true);
        }
        catch
        {
            // ignore
        }

        try
        {
            stderrTask.Wait(TimeSpan.FromSeconds(3));
        }
        catch
        {
            // ignore
        }

        var stderrBlob = SnapshotStderr();
        var exitNote = proc.HasExited ? $"子进程已退出，ExitCode={proc.ExitCode}。" : "子进程状态未知。";

        var userHint =
            "请在该虚拟环境内安装 luxvideopyplugin（例如: pip install <wheel> 或项目文档中的包名），" +
            "并确认可执行: python -m luxvideopyplugin.cli.hand_worker";

        _logger?.LogError(
            "hand_worker 启动失败。{ExitNote} 期望 stdout 首行为 READY，实际: {ReadyLine}。venv={Venv}。{Hint}",
            exitNote,
            ready ?? "(null)",
            venvRoot,
            userHint);
        _logger?.LogError($"hand_worker Python stderr:{Environment.NewLine}{stderrBlob}");

        DisposeProcessUnlocked();

        throw new InvalidOperationException(
            $"hand_worker 未能就绪（期望首行 READY，实际: {ready ?? "(null)"}）。{exitNote}{Environment.NewLine}" +
            $"虚拟环境: {venvRoot}{Environment.NewLine}" +
            $"{userHint}{Environment.NewLine}{Environment.NewLine}" +
            $"---- Python stderr ----{Environment.NewLine}{stderrBlob}");
    }

    private static void WriteFrameHeaderAndPayload(Stream stdin, int w, int h, int stride, byte[] payload)
    {
        Span<byte> hdr = stackalloc byte[12];
        BinaryPrimitives.WriteUInt32LittleEndian(hdr, (uint)w);
        BinaryPrimitives.WriteUInt32LittleEndian(hdr.Slice(4), (uint)h);
        BinaryPrimitives.WriteUInt32LittleEndian(hdr.Slice(8), (uint)stride);
        stdin.Write(hdr);
        stdin.Write(payload);
        stdin.Flush();
    }

    private static void CopyMatBgrRows(Mat inferMat, byte[] dest, int stride, int h)
    {
        if (inferMat.IsContinuous())
        {
            Marshal.Copy(inferMat.Data, dest, 0, stride * h);
            return;
        }

        var offset = 0;
        for (var y = 0; y < h; y++)
        {
            var rowPtr = inferMat.Ptr(y);
            Marshal.Copy(rowPtr, dest, offset, stride);
            offset += stride;
        }
    }

    private void DisposeProcessUnlocked()
    {
        try
        {
            if (_process is { HasExited: false })
            {
                _process.Kill(entireProcessTree: true);
            }
        }
        catch
        {
            // ignore
        }

        _stdin = null;
        _stdout = null;
        _process?.Dispose();
        _process = null;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        lock (_ioLock)
        {
            DisposeProcessUnlocked();
        }
    }
}

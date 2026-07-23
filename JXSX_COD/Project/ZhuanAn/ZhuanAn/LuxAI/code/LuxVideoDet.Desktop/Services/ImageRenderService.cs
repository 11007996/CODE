using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using OpenCvSharp;

namespace LuxVideoDet.Desktop.Services;

/// <summary>
/// 图像渲染服务 - 将 OpenCV Mat 转换为 Avalonia Bitmap
/// </summary>
public class ImageRenderService
{
    /// <summary>
    /// 将 Mat 转为 Avalonia WriteableBitmap（BGR→BGRA 内存拷贝，不经过 ImEncode）。
    /// macOS 等环境下 OpenCvSharp 可能缺少 imgcodecs 导出，区域编辑器等场景须使用本路径。
    /// </summary>
    public static WriteableBitmap? MatToWriteableBitmap(Mat frame)
    {
        if (frame == null || frame.Empty())
            return null;

        try
        {
            // 确保是 BGR 格式
            Mat bgrFrame = frame.Channels() == 3 ? frame : new Mat();
            if (frame.Channels() == 4)
            {
                Cv2.CvtColor(frame, bgrFrame, ColorConversionCodes.BGRA2BGR);
            }
            else if (frame.Channels() == 1)
            {
                Cv2.CvtColor(frame, bgrFrame, ColorConversionCodes.GRAY2BGR);
            }

            // 创建 WriteableBitmap
            var bitmap = new WriteableBitmap(
                new PixelSize(bgrFrame.Width, bgrFrame.Height),
                new Vector(96, 96),
                PixelFormat.Bgra8888,
                AlphaFormat.Opaque);

            using (var buffer = bitmap.Lock())
            {
                // 转换为 BGRA
                using var bgraFrame = new Mat();
                Cv2.CvtColor(bgrFrame, bgraFrame, ColorConversionCodes.BGR2BGRA);

                // 复制数据
                var sourcePtr = bgraFrame.Data;
                var destPtr = buffer.Address;
                var size = bgraFrame.Width * bgraFrame.Height * 4;

                unsafe
                {
                    Buffer.MemoryCopy(
                        sourcePtr.ToPointer(),
                        destPtr.ToPointer(),
                        size,
                        size);
                }
            }

            if (bgrFrame != frame)
                bgrFrame.Dispose();

            return bitmap;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// 将 Mat 转换为 WriteableBitmap
    /// </summary>
    public WriteableBitmap? ConvertToWriteableBitmap(Mat frame)
        => MatToWriteableBitmap(frame);

    /// <summary>
    /// 更新 WriteableBitmap（复用已有 bitmap）
    /// </summary>
    public bool UpdateWriteableBitmap(Mat frame, WriteableBitmap bitmap)
    {
        if (frame == null || frame.Empty() || bitmap == null)
            return false;

        if (bitmap.PixelSize.Width != frame.Width || bitmap.PixelSize.Height != frame.Height)
            return false;

        try
        {
            // 确保是 BGR 格式
            Mat bgrFrame = frame.Channels() == 3 ? frame : new Mat();
            if (frame.Channels() == 4)
            {
                Cv2.CvtColor(frame, bgrFrame, ColorConversionCodes.BGRA2BGR);
            }
            else if (frame.Channels() == 1)
            {
                Cv2.CvtColor(frame, bgrFrame, ColorConversionCodes.GRAY2BGR);
            }

            using (var buffer = bitmap.Lock())
            {
                // 转换为 BGRA
                using var bgraFrame = new Mat();
                Cv2.CvtColor(bgrFrame, bgraFrame, ColorConversionCodes.BGR2BGRA);

                // 复制数据
                var sourcePtr = bgraFrame.Data;
                var destPtr = buffer.Address;
                var size = bgraFrame.Width * bgraFrame.Height * 4;

                unsafe
                {
                    Buffer.MemoryCopy(
                        sourcePtr.ToPointer(),
                        destPtr.ToPointer(),
                        size,
                        size);
                }
            }

            if (bgrFrame != frame)
                bgrFrame.Dispose();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

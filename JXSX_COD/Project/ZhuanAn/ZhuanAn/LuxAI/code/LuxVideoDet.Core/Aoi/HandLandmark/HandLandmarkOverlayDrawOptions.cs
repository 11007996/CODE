using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary><see cref="HandLandmarkOverlay.DrawHands"/> 的绘制参数。</summary>
public sealed class HandLandmarkOverlayDrawOptions
{
    public bool ShowHandednessLabel { get; init; } = true;

    public int LineThickness { get; init; } = 2;

    public double LabelFontScale { get; init; } = 0.55;

    public Scalar LabelForegroundBgr { get; init; } = new(255, 255, 255);
}

namespace LuxVideoDet.Core.Algorithm.Ui;

/// <summary>
/// 实时预览画面上算法操作按钮所在的九宫格区域（相对预览画布，非像素坐标）。
/// </summary>
public enum AlgorithmUiActionPlacement
{
    TopLeft = 0,
    TopCenter = 1,
    TopRight = 2,
    MiddleLeft = 3,
    Center = 4,
    MiddleRight = 5,
    BottomLeft = 6,
    BottomCenter = 7,
    /// <summary>与历史布局一致：预览区右下角附近。</summary>
    BottomRight = 8,
}

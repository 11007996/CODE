using LuxVideoDet.Core.Inference.Results;

namespace LuxVideoDet.Core.Algorithm.TypecPacking;

/// <summary>
/// Typec包装算法帧输入数据
/// </summary>
public class TypecPackingFrameInput
{
    // 纸箱区域
    public bool HasEmptyBox { get; set; }
    public bool HasDividerInBox { get; set; }
    public bool HasProductDividerInBox { get; set; }
    public int ProductsInBoxCount { get; set; }
    public int ProductsFInBoxCount { get; set; }
    public int ProductsSInBoxCount { get; set; }
    public bool HasBoxLidClosed { get; set; }
    
    // 称重区域
    public bool HasGreenLight { get; set; }
    public bool HasProductOnScale { get; set; }
    
    // 检测对象
    public Detection? DividerInBox { get; set; }
    public Detection? ProductDividerInBox { get; set; }
    public Detection? ProductOnScale { get; set; }
}
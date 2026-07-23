using LuxVideoDet.Core.Algorithm;

namespace LuxVideoDet.Core.Algorithm.TypecPacking;

/// <summary>
/// Typec包装检测逻辑类别键
/// </summary>
public static class TypecPackingLogicalClass
{
    public const string Box = "Box";
    public const string Divider = "Divider";
    public const string Product = "Product";
    public const string ProductDivider = "ProductDivider";
    public const string ScaleGreenLight = "ScaleGreenLight";
    public const string BoxLid = "BoxLid";
}

/// <summary>
/// Typec包装检测类别绑定
/// </summary>
public static class TypecPackingDetectionBindings
{
    public static IReadOnlyList<AlgorithmDetectionClassBinding> GetDefaultBindings() =>
    [
        new(TypecPackingLogicalClass.Box, "a", TypecPackingDetectionClass.BOX),
        new(TypecPackingLogicalClass.Divider, "b", TypecPackingDetectionClass.DIVIDER),
        new(TypecPackingLogicalClass.Product, "c", TypecPackingDetectionClass.PRODUCT),
        new(TypecPackingLogicalClass.ProductDivider, "d", TypecPackingDetectionClass.PRODUCT_DIVIDER),
        new(TypecPackingLogicalClass.ScaleGreenLight, "e", TypecPackingDetectionClass.SCALE_GREEN_LIGHT),
        new(TypecPackingLogicalClass.BoxLid, "f", TypecPackingDetectionClass.BOX_LID)
    ];
}
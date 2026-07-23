namespace LuxVideoDet.Core.Algorithm.Results;

/// <summary>
/// 产品判定结果
/// </summary>
public enum ProductionJudgement
{
    /// <summary>本帧未产生判定</summary>
    None,
    /// <summary>产品合格</summary>
    OK,
    /// <summary>产品不合格</summary>
    NG
}

namespace EAM.Model.Dto
{
    /// <summary>
    /// 节点流向查询对象
    /// </summary>
    public class NodeFlowQueryDto : PagerInfo
    {
        public int? FromNodeId { get; set; }
    }

    /// <summary>
    /// 节点流向输入输出对象
    /// </summary>
    public class NodeFlowDto
    {
        [Required(ErrorMessage = "节点流向ID不能为空")]
        public int NodeFlowId { get; set; }

        [Required(ErrorMessage = "来源节点不能为空")]
        public int FromNodeId { get; set; }

        [Required(ErrorMessage = "操作类型不能为空")]
        public string ActionType { get; set; }

        [Required(ErrorMessage = "目标节点不能为空")]
        public int ToNodeId { get; set; }

        public string ConditionExpression { get; set; }

        [ExcelColumn(Name = "来源节点")]
        public string FromNodeIdLabel { get; set; }

        [ExcelColumn(Name = "操作类型")]
        public string ActionTypeLabel { get; set; }

        [ExcelColumn(Name = "并行")]
        public string ParallelLabel { get; set; }
    }
}
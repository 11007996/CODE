namespace EAM.Model.Dto
{
    /// <summary>
    /// 流程节点定义查询对象
    /// </summary>
    public class NodeDefineQueryDto : PagerInfo
    {
        public int? NodeId { get; set; }
        public string ProcessId { get; set; }
        public string NodeName { get; set; }
    }

    /// <summary>
    /// 流程节点定义输入输出对象
    /// </summary>
    public class NodeDefineDto
    {
        [Required(ErrorMessage = "节点ID不能为空")]
        public int NodeId { get; set; }

        [Required(ErrorMessage = "流程ID不能为空")]
        public string ProcessId { get; set; }

        [Required(ErrorMessage = "节点名称不能为空")]
        public string NodeName { get; set; }

        [Required(ErrorMessage = "节点类型不能为空")]
        public string NodeType { get; set; }

        public string AllowedActions { get; set; }

        [Required(ErrorMessage = "节点顺序不能为空")]
        public int OrderNo { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "节点类型")]
        public string NodeTypeLabel { get; set; }

        [ExcelColumn(Name = "允许的操作")]
        public string AllowedActionsLabel { get; set; }

        public List<NodeFlowDto> NodeFlowNav { get; set; }

        public List<NodeFieldControlDto> NodeFieldControlNav { get; set; }

        public NodeApproverDto NodeApproverNav { get; set; }
    }

    public class NodeDetailDto
    {
        [Required(ErrorMessage = "节点ID不能为空")]
        public int NodeId { get; set; }

        [Required(ErrorMessage = "流程ID不能为空")]
        public string ProcessId { get; set; }

        [Required(ErrorMessage = "流程ID不能为空")]
        public string ProcessName { get; set; }

        [Required(ErrorMessage = "节点名称不能为空")]
        public string NodeName { get; set; }

        [Required(ErrorMessage = "节点类型不能为空")]
        public string NodeType { get; set; }

        public string AllowedActions { get; set; }

        [Required(ErrorMessage = "节点顺序不能为空")]
        public int OrderNo { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "节点类型")]
        public string NodeTypeLabel { get; set; }

        [ExcelColumn(Name = "允许的操作")]
        public string AllowedActionsLabel { get; set; }

        public List<NodeFlowDto> NodeFlowNav { get; set; }

        public List<NodeFieldControlDto> NodeFieldControlNav { get; set; }

        public NodeApproverDto NodeApproverNav { get; set; }
    }
}
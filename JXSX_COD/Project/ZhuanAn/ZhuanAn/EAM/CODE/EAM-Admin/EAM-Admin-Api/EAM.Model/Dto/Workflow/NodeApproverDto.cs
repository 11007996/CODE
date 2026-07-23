namespace EAM.Model.Dto
{
    /// <summary>
    /// 节点审批人配置查询对象
    /// </summary>
    public class NodeApproverQueryDto : PagerInfo
    {
        public int NodeId { get; set; }
    }

    public class NodeApproverDictQueryDto : PagerInfo
    {
        public string ApproverType { get; set; }
        public string KeyWord { get; set; }
        public int? NodeId { get; set; }
    }

    /// <summary>
    /// 节点审批人配置输入输出对象
    /// </summary>
    public class NodeApproverDto
    {
        [Required(ErrorMessage = "节点ID不能为空")]
        public int NodeId { get; set; }

        //[Required(ErrorMessage = "任务ID不能为空")]
        //public int TaskId {  get; set; }

        [Required(ErrorMessage = "审批人类型不能为空")]
        public string ApproverType { get; set; }

        [Required(ErrorMessage = "审批类型值不能为空")]
        public string ApproverValue { get; set; }

        public string ApproverDesc { get; set; }

        public string NodeName { get; set; }

        [ExcelColumn(Name = "审批人类型")]
        public string ApproverTypeLabel { get; set; }
    }
}
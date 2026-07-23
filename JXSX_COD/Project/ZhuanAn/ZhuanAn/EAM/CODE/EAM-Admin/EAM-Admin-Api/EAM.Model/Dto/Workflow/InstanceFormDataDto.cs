namespace EAM.Model.Dto
{
    /// <summary>
    /// 流程实例表单数据查询对象
    /// </summary>
    public class InstanceFormDataQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 流程实例表单数据输入输出对象
    /// </summary>
    public class InstanceFormDataDto
    {
        [Required(ErrorMessage = "流程编号不能为空")]
        public string ProcessInstanceId { get; set; }

        [Required(ErrorMessage = "节点ID不能为空")]
        public int NodeId { get; set; }

        [Required(ErrorMessage = "表单字段名称不能为空")]
        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        [Required(ErrorMessage = "更新人员不能为空")]
        public string UpdateBy { get; set; }

        [Required(ErrorMessage = "更新时间不能为空")]
        public DateTime? UpdateTime { get; set; }
    }
}
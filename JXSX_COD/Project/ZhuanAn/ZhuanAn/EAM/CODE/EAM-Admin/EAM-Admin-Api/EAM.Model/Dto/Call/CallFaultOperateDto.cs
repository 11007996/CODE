namespace EAM.Model.Dto
{
    /// <summary>
    /// 呼叫操作记录查询对象
    /// </summary>
    public class CallFaultOperateQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 呼叫操作记录输入输出对象
    /// </summary>
    public class CallFaultOperateDto
    {
        [Required(ErrorMessage = "呼叫Id不能为空")]
        public int CallId { get; set; }

        [Required(ErrorMessage = "操作人工号不能为空")]
        public string OperaterNo { get; set; }

        public string OperaterName { get; set; }

        public string FaultStatus { get; set; }

        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "故障状态")]
        public string FaultStatusLabel { get; set; }
    }
}
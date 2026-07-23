namespace EAM.Model.Dto
{
    /// <summary>
    /// 呼叫通知记录查询对象
    /// </summary>
    public class CallFaultNoticeQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 呼叫通知记录输入输出对象
    /// </summary>
    public class CallFaultNoticeDto
    {
        [Required(ErrorMessage = "呼叫ID不能为空")]
        public int CallId { get; set; }

        [Required(ErrorMessage = "呼叫阶段不能为空")]
        public string CallStageType { get; set; }

        public long? WxNoticeId { get; set; }

        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "呼叫阶段")]
        public string CallStageTypeLabel { get; set; }
    }
}
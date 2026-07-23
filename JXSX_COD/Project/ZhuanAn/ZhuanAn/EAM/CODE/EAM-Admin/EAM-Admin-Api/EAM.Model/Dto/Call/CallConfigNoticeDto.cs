namespace EAM.Model.Dto
{
    /// <summary>
    /// 呼叫通知配置查询对象
    /// </summary>
    public class CallConfigNoticeQueryDto : PagerInfo
    {
        public string CallStageType { get; set; }
        public string EmpCode { get; set; }
    }

    /// <summary>
    /// 呼叫通知配置输入输出对象
    /// </summary>
    public class CallConfigNoticeDto
    {
        [Required(ErrorMessage = "ID不能为空")]
        public int NoticeConfigId { get; set; }

        [Required(ErrorMessage = "呼叫阶段不能为空")]
        public string CallStageType { get; set; }

        public int? AreaId { get; set; }

        public string AreaName { get; set; }

        public string CallTargetType { get; set; }

        public string WxChatId { get; set; }

        public string EmpCodes { get; set; }

        public string WxChatName { get; set; }

        public List<EmpSimpleDto> EmpNav { get; set; }
    }
}
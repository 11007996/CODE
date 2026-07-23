namespace EAM.Model.Dto
{
    /// <summary>
    /// 保养通知配置查询对象
    /// </summary>
    public class MaintainNoticeConfigQueryDto : PagerInfo
    {
        public string DateMark { get; set; }
    }

    /// <summary>
    /// 保养通知配置输入输出对象
    /// </summary>
    public class MaintainNoticeConfigDto
    {
        [Required(ErrorMessage = "通知配置ID不能为空")]
        public int NoticeConfigId { get; set; }

        public string DateMark { get; set; }

        public string WxChatId { get; set; }

        public string WxChatName { get; set; }

        public string EmpCodes { get; set; }

        public string EnableFlag { get; set; }

        public List<EmpSimpleDto> EmpNav { get; set; }

        public string DateMarkLabel { get; set; }
    }
}
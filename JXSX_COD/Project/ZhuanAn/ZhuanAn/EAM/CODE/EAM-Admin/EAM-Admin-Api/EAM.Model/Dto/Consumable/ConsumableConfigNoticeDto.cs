namespace EAM.Model.Dto
{
    /// <summary>
    /// 耗品通知配置查询对象
    /// </summary>
    public class ConsumableConfigNoticeQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 耗品通知配置输入输出对象
    /// </summary>
    public class ConsumableConfigNoticeDto
    {
        [Required(ErrorMessage = "通知配置ID不能为空")]
        public int NoticeConfigId { get; set; }

        public string WxChatId { get; set; }

        public string WxChatName { get; set; }

        public string EmpCodes { get; set; }

        private List<EmpSimpleDto> EmpNav { get; set; }
    }
}
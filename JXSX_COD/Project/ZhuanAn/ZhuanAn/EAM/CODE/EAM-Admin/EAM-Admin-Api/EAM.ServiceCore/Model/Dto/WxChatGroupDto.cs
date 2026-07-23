namespace EAM.Model.System.Dto
{
    /// <summary>
    /// 微信聊天群查询对象
    /// </summary>
    public class WxChatGroupQueryDto : PagerInfo
    {
        public string FactoryId { get; set; }
    }

    /// <summary>
    /// 微信聊天群输入输出对象
    /// </summary>
    public class WxChatGroupDto
    {
        public string ChatId { get; set; }

        public string ChatName { get; set; }

        public string FactoryId { get; set; }

        public string FactoryIdLabel { get; set; }
    }
}
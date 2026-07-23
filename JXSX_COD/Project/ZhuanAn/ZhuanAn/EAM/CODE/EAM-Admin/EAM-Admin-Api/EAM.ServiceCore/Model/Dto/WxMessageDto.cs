namespace EAM.Model.Dto
{
    /// <summary>
    /// 企业微信发送记录表查询对象
    /// </summary>
    public class WxMessageQueryDto : PagerInfo
    {
        public DateTime? BeginSendTime { get; set; }
        public DateTime? EndSendTime { get; set; }
    }

    /// <summary>
    /// 企业微信发送记录表输入输出对象
    /// </summary>
    public class WxMessageDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ChatId { get; set; }

        public string EmpCodes { get; set; }

        public string MsgType { get; set; }

        public string LinkUrl { get; set; }

        public string Articles { get; set; }

        public DateTime? SendTime { get; set; }

        public string ResultMsg { get; set; }
    }
}
namespace EAM.Model.Dto
{
    /// <summary>
    /// 耗品库存通知查询对象
    /// </summary>
    public class ConsumableStorageNoticeQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 耗品库存通知输入输出对象
    /// </summary>
    public class ConsumableStorageNoticeDto
    {
        [Required(ErrorMessage = "耗品ID不能为空")]
        public int ConsumableId { get; set; }

        public long? WxNoticeId { get; set; }

        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? CreateTime { get; set; }
    }
}
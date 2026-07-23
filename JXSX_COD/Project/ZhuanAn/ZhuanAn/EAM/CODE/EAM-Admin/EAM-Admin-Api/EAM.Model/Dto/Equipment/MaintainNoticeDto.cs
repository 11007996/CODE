namespace EAM.Model.Dto
{
    /// <summary>
    /// 保养通知记录查询对象
    /// </summary>
    public class MaintainNoticeQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 保养通知记录输入输出对象
    /// </summary>
    public class MaintainNoticeDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        public int EquipmentId { get; set; }

        [Required(ErrorMessage = "日期标识不能为空")]
        public string DateMark { get; set; }

        [Required(ErrorMessage = "日期标识值不能为空")]
        public int DateMarkStamp { get; set; }

        public int? WxNoticeId { get; set; }

        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? CreateTime { get; set; }
    }
}
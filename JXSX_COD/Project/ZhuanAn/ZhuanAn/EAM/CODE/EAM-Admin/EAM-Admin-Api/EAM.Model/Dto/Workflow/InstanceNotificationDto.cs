namespace EAM.Model.Dto
{
    /// <summary>
    /// 流程通知查询对象
    /// </summary>
    public class InstanceNotificationQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 流程通知输入输出对象
    /// </summary>
    public class InstanceNotificationDto
    {
        [Required(ErrorMessage = "通知ID不能为空")]
        public int NotificationId { get; set; }

        [Required(ErrorMessage = "流程编号不能为空")]
        public string ProcessInstanceId { get; set; }

        [Required(ErrorMessage = "节点ID不能为空")]
        public int NodeId { get; set; }

        [Required(ErrorMessage = "通知人员不能为空")]
        public int NotifyTo { get; set; }

        [Required(ErrorMessage = "通知时间不能为空")]
        public DateTime? NotifyTime { get; set; }

        [Required(ErrorMessage = "通知内容不能为空")]
        public string NotifyContent { get; set; }

        [Required(ErrorMessage = "是否已读不能为空")]
        public bool IsRead { get; set; }

        [ExcelColumn(Name = "是否已读")]
        public string IsReadLabel { get; set; }
    }
}
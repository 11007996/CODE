namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品事件处理动作查询对象
    /// </summary>
    public class IotProductEventActionQueryDto : PagerInfo
    {
        public int? EventId { get; set; }
    }

    /// <summary>
    /// 产品事件处理动作输入输出对象
    /// </summary>
    public class IotProductEventActionDto
    {
        [Required(ErrorMessage = "动作Id不能为空")]
        public int ActionId { get; set; }

        [Required(ErrorMessage = "事件ID不能为空")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "动作名称不能为空")]
        public string ActionName { get; set; }

        [Required(ErrorMessage = "动作类型不能为空")]
        public string ActionType { get; set; }

        [Required(ErrorMessage = "动作配置不能为空")]
        public string ActionConfig { get; set; }

        [Required(ErrorMessage = "动作顺序不能为空")]
        public int SortOrder { get; set; }

        [Required(ErrorMessage = "执行超时(秒)不能为空")]
        public int Timeout { get; set; }

        [Required(ErrorMessage = "是否可用不能为空")]
        public bool Enabled { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "动作类型")]
        public string ActionTypeLabel { get; set; }
    }
}
namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品主题类表查询对象
    /// </summary>
    public class IotProductTopicQueryDto : PagerInfo
    {
        public int? ProductId { get; set; }
        public string TopicName { get; set; }
    }

    /// <summary>
    /// 产品主题类表输入输出对象
    /// </summary>
    public class IotProductTopicDto
    {
        [Required(ErrorMessage = "主题ID不能为空")]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "产品ID不能为空")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "主题名称不能为空")]
        public string TopicName { get; set; }

        [Required(ErrorMessage = "主题格式不能为空")]
        public string TopicFormat { get; set; }

        [Required(ErrorMessage = "操作不能为空")]
        public string Operation { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "是否可用不能为空")]
        public bool Enabled { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "产品ID")]
        public string ProductIdLabel { get; set; }
    }
}
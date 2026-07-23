namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品表查询对象
    /// </summary>
    public class IotProductQueryDto : PagerInfo
    {
        public string ProductName { get; set; }
    }

    /// <summary>
    /// 产品表输入输出对象
    /// </summary>
    public class IotProductDto
    {
        [Required(ErrorMessage = "产品ID不能为空")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "产品名称不能为空")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "节点类型不能为空")]
        public string NodeType { get; set; }

        public string AccessProtocol { get; set; }

        [Required(ErrorMessage = "数据格式不能为空")]
        public string DataFormat { get; set; }

        public int? Version { get; set; }

        public string Description { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
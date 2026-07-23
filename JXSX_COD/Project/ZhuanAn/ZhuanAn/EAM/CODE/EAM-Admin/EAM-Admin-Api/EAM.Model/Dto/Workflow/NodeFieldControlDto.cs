namespace EAM.Model.Dto
{
    /// <summary>
    /// 节点字段控件配置查询对象
    /// </summary>
    public class NodeFieldControlQueryDto : PagerInfo
    {
        public int? NodeId { get; set; }
    }

    /// <summary>
    /// 节点字段控件配置输入输出对象
    /// </summary>
    public class NodeFieldControlDto
    {
        [Required(ErrorMessage = "节点ID不能为空")]
        public int NodeId { get; set; }

        [Required(ErrorMessage = "表单ID不能为空")]
        public int FormId { get; set; }

        [Required(ErrorMessage = "字段名称不能为空")]
        public string FieldName { get; set; }

        [Required(ErrorMessage = "是否显示不能为空")]
        public bool Hidden { get; set; }

        [Required(ErrorMessage = "是否编辑不能为空")]
        public bool Editable { get; set; }

        [Required(ErrorMessage = "是否必填不能为空")]
        public bool Required { get; set; }
    }

    public class NodeFieldControlDetailDto
    {
        [Required(ErrorMessage = "节点ID不能为空")]
        public int? NodeId { get; set; }

        [Required(ErrorMessage = "表单ID不能为空")]
        public int FormId { get; set; }

        [Required(ErrorMessage = "字段名称不能为空")]
        public string FieldName { get; set; }

        public string FieldDesc { get; set; }

        [Required(ErrorMessage = "是否隐藏不能为空")]
        public bool? Hidden { get; set; }

        [Required(ErrorMessage = "是否编辑不能为空")]
        public bool? Editable { get; set; }

        [Required(ErrorMessage = "是否必填不能为空")]
        public bool? Required { get; set; }
    }
}
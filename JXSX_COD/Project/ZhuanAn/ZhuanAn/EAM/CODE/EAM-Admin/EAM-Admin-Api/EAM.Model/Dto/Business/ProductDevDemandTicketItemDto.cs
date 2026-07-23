namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品开发需求单_需求清单查询对象
    /// </summary>
    public class ProductDevDemandTicketItemQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 产品开发需求单_需求清单输入输出对象
    /// </summary>
    public class ProductDevDemandTicketItemDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "制程名称不能为空")]
        public string ProcessName { get; set; }

        [Required(ErrorMessage = "标准规格不能为空")]
        public string StandardSpec { get; set; }

        [Required(ErrorMessage = "器材类型不能为空")]
        public string EquipmentType { get; set; }

        [Required(ErrorMessage = "说明不能为空")]
        public string Caption { get; set; }

        [Required(ErrorMessage = "数量不能为空")]
        public int Qty { get; set; }

        [Required(ErrorMessage = "开发方式不能为空")]
        public string DevMode { get; set; }

        public int? ExtendTargetId { get; set; }

        public string ExtendTargetDesc { get; set; }

        public string FileList { get; set; }

        [ExcelColumn(Name = "子项类型")]
        public string ItemTypeLabel { get; set; }
    }
}
namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备资产信息查询对象
    /// </summary>
    public class EquipmentBaseQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string AssetNo { get; set; }
        public string AssetName { get; set; }
        public string AssetClass { get; set; }
        public string CostCenter { get; set; }
        public string CustomModel { get; set; }
        public string Keyword { get; set; }
    }

    /// <summary>
    /// 设备资产信息输入输出对象
    /// </summary>
    public class EquipmentBaseDto
    {
        [ExcelColumn(Name = "设备ID")]
        public int? EquipmentId { get; set; }

        [Required(ErrorMessage = "设备名称不能为空")]
        [ExcelColumn(Name = "设备名称")]
        [ExcelColumnName("设备名称")]
        public string EquipmentName { get; set; }

        [ExcelColumn(Name = "资产编号")]
        public string AssetNo { get; set; }

        [Required(ErrorMessage = "公司代码不能为空")]
        [ExcelColumn(Name = "公司代码")]
        [ExcelColumnName("公司代码")]
        public string FactoryCode { get; set; }

        [Required(ErrorMessage = "资产主编号不能为空")]
        [ExcelColumn(Name = "资产主编号")]
        [ExcelColumnName("资产主编号")]
        public string AssetMainNo { get; set; }

        [Required(ErrorMessage = "资产子编号不能为空")]
        [ExcelColumn(Name = "资产子编号")]
        [ExcelColumnName("资产子编号")]
        public string AssetSubNo { get; set; }

        [Required(ErrorMessage = "资产名称不能为空")]
        [ExcelColumn(Name = "资产名称")]
        [ExcelColumnName("资产名称")]
        public string AssetName { get; set; }

        [ExcelColumn(Name = "资产分类")]
        [ExcelColumnName("资产分类")]
        public string AssetClass { get; set; }

        [ExcelColumn(Name = "型号规格")]
        [ExcelColumnName("型号规格")]
        public string Model { get; set; }

        [ExcelColumn(Name = "购置日期", Format = "yyyy-MM-dd", Width = 20)]
        [ExcelColumnName("购置日期")]
        public DateTime? EntryDate { get; set; }

        [ExcelColumn(Name = "成本中心")]
        [ExcelColumnName("成本中心")]
        public string CostCenter { get; set; }

        [ExcelColumn(Name = "耐用年限")]
        [ExcelColumnName("耐用年限")]
        public int? DurableYear { get; set; }

        [ExcelColumn(Name = "耐用月数")]
        [ExcelColumnName("耐用月数")]
        public int? DurableMonth { get; set; }

        [ExcelColumn(Name = "制造厂商")]
        [ExcelColumnName("制造厂商")]
        public string MadeFactory { get; set; }

        [ExcelColumn(Name = "校验管制编号")]
        [ExcelColumnName("校验管制编号")]
        public string ControlNo { get; set; }

        [ExcelColumn(Name = "自定义机型")]
        [ExcelColumnName("自定义机型")]
        public string CustomModel { get; set; }

        [ExcelColumn(Name = "设备状态")]
        [ExcelColumnName("设备状态")]
        public string Status { get; set; }

        [ExcelColumn(Name = "更新时间", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        [ExcelColumnName("更新时间")]
        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "更新人")]
        [ExcelColumnName("更新人")]
        public string UpdateBy { get; set; }
    }

    /// <summary>
    /// 查询闲置设备输入输出对象
    /// </summary>
    public class EquipmentIdleDto
    {
        public string EquipmentName { get; set; }
        public int TotalIdleQty { get; set; }
    }

    /// <summary>
    /// 一个简单的设备信息传递对象
    /// </summary>
    public class EquipmentSimpleDto
    {
        public int? EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public string AssetNo { get; set; }

        public string AssetName { get; set; }

        public string CustomModel { get; set; }

        public string Status { get; set; }
    }
}
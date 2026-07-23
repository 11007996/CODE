namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备保养项目查询对象
    /// </summary>
    public class MaintainItemQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
    }

    /// <summary>
    /// 设备保养项目输入输出对象
    /// </summary>
    public class MaintainItemDto
    {
        [ExcelColumn(Name = "ID")]
        [ExcelColumnName("ID")]
        public int? ItemId { get; set; }

        [Required(ErrorMessage = "设备Id不能为空")]
        [ExcelColumn(Name = "设备ID")]
        public int? EquipmentId { get; set; }

        [ExcelColumn(Name = "设备名称")]
        public string EquipmentName { get; set; }

        [ExcelColumn(Name = "资产编号")]
        [ExcelColumnName("资产编号")]
        public string AssetNo { get; set; }

        [ExcelColumn(Name = "资产名称")]
        public string AssetName { get; set; }

        [Required(ErrorMessage = "日期标记不能为空")]
        [ExcelColumn(Name = "日期标记")]
        [ExcelColumnName("日期标记")]
        public string DateMark { get; set; }

        [Required(ErrorMessage = "项目名称不能为空")]
        [ExcelColumn(Name = "项目名称")]
        [ExcelColumnName("项目名称")]
        public string ItemName { get; set; }

        [ExcelColumn(Name = "排序")]
        [ExcelColumnName("排序")]
        public int? SortNo { get; set; }

        //[ExcelColumn(Name = "日期标记")]
        //public string DateMarkLabel { get; set; }
    }

    /// <summary>
    /// 保养项目克隆 输入接收对象
    /// </summary>
    public class MaintainItemCloneDto
    {
        /// <summary>
        /// 被克隆设备资产编号
        /// </summary>
        public int FromEquipmentId { get; set; }

        /// <summary>
        /// 克隆设备资产编号列表
        /// </summary>
        public int[] ToEquipmentIdList { get; set; }
    }
}
namespace EAM.Model.Dto
{
    /// <summary>
    /// 耗品存储表查询对象
    /// </summary>
    public class ConsumableStorageQueryDto : PagerInfo
    {
        public int? ConsumableId { get; set; }
        public string ConsumablePart { get; set; }
        public string ConsumableName { get; set; }
        public string Spec { get; set; }
        public string Category {  get; set; }
        public int? StorageId { get; set; }
    }

    public class ConsumableStorageInfoDto
    {
        [Required(ErrorMessage = "耗品ID不能为空")]
        public int ConsumableId { get; set; }

        [Required(ErrorMessage = "储位ID不能为空")]
        public int StorageId { get; set; }
    }

    /// <summary>
    /// 耗品存储表输入输出对象
    /// </summary>
    public class ConsumableStorageDto
    {
        [Required(ErrorMessage = "耗品ID不能为空")]
        [ExcelColumn(Name = "耗品ID")]
        public int ConsumableId { get; set; }

        [ExcelColumn(Name = "请购料号")]
        public string ConsumablePart { get; set; }

        [ExcelColumn(Name = "耗品名称")]
        public string ConsumableName { get; set; }

        [ExcelColumn(Name = "规格")]
        public string Spec { get; set; }

        [Required(ErrorMessage = "储位ID不能为空")]
        [ExcelColumn(Name = "储位ID")]
        public int StorageId { get; set; }

        [ExcelColumn(Name = "储位全名")]
        public string StorageFullName { get; set; }

        [Required(ErrorMessage = "库存数量不能为空")]
        [ExcelColumn(Name = "数量")]
        public int Qty { get; set; }
    }

    /// <summary>
    /// 库存变更对象
    /// </summary>
    public class OperateConsumableStorageDto
    {
        [Required(ErrorMessage = "耗品ID不能为空")]
        public int ConsumableId { get; set; }

        [Required(ErrorMessage = "储位ID不能为空")]
        public int StorageId { get; set; }

        [Required(ErrorMessage = "数量不能为空")]
        public int ChangeQty { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remark { get; set; }

        public string TicketNo { get; set; }

        public string TicketType { get; set; }

        #region 领用或归还时使用
        /// <summary>
        /// 相关人员(领用时表示领用人，归还时表示归还人)
        /// </summary>
        public string RelatedUser { get; set; }

        public int? LineId { get; set; }

        #endregion 领用时使用

        #region 转换时使用

        /// <summary>
        /// 转移到的新储位
        /// </summary>
        public int NewStorageId { get; set; }

        #endregion 转换时使用
    }

    /// <summary>
    /// 耗品储存导入
    /// </summary>
    public class ConsumableStorageImportDto
    {
        [ExcelColumn(Name = "耗品ID")]
        public int? ConsumableId { get; set; }

        [ExcelColumn(Name = "请购料号")]
        public string ConsumablePart { get; set; }

        [ExcelColumn(Name = "耗品名称")]
        public string ConsumableName { get; set; }

        [ExcelColumn(Name = "规格")]
        public string Spec { get; set; }

        [ExcelColumn(Name = "储位ID")]
        public int? StorageId { get; set; }

        [ExcelColumn(Name = "储位名称")]
        public string StorageFullName { get; set; }

        [ExcelColumn(Ignore = true)]
        public int? OldQty { get; set; }

        [ExcelColumn(Name = "数量")]
        public int? Qty { get; set; }

        [ExcelColumn(Ignore = true)]
        public int? ChangeQty { get; set; }

        [ExcelColumn(Ignore = true)]
        public string StorageChangeType { get; set; }

        [ExcelColumn(Name = "备注")]
        public string Remark { get; set; }

        [ExcelColumn(Ignore = true)]
        public string CreateBy { get; set; }

        [ExcelColumn(Ignore = true)]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 导入时的错误描述，用于返回前端
        /// </summary>
        [ExcelColumn(Ignore = true)]
        public string ErrorDesc { get; set; }
    }

    /// <summary>
    /// 耗品储存操作导入
    /// </summary>
    public class ConsumableStorageOperateImportDto
    {
        [ExcelColumn(Name = "耗品ID")]
        public int? ConsumableId { get; set; }

        [ExcelColumn(Name = "请购料号")]
        public string ConsumablePart { get; set; }

        [ExcelColumn(Name = "耗品名称")]
        public string ConsumableName { get; set; }

        [ExcelColumn(Name = "规格")]
        public string Spec { get; set; }

        [ExcelColumn(Name = "储位ID")]
        public int? StorageId { get; set; }

        [ExcelColumn(Name = "储位名称")]
        public string StorageFullName { get; set; }

        [ExcelColumn(Name = "变动数量")]
        public int? ChangeQty { get; set; }

        [ExcelColumn(Name = "变动类型")]
        public string StorageChangeTypeLabel { get; set; }

        [ExcelColumn(Ignore = true)]
        public string StorageChangeType { get; set; }

        [ExcelColumn(Name = "领用人工号")]
        public string RelatedUser { get; set; }

        [ExcelColumn(Name = "领用人姓名")]
        public string RelatedUserName { get; set; }

        [ExcelColumn(Ignore = true)]
        public int? LineId { get; set; }

        [ExcelColumn(Name = "产线")]
        public string LineName { get; set; }

        [ExcelColumn(Name = "备注")]
        public string Remark { get; set; }

        [ExcelColumn(Ignore = true)]
        public string CreateBy { get; set; }

        [ExcelColumn(Ignore = true)]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 导入时的错误描述，用于返回前端
        /// </summary>
        [ExcelColumn(Ignore = true)]
        public string ErrorDesc { get; set; }
    }
}
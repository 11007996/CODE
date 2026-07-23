namespace EAM.Model.Dto
{
    /// <summary>
    /// 耗品库存记录查询对象
    /// </summary>
    public class ConsumableStorageRecordQueryDto : PagerInfo
    {
        public int? ConsumableId { get; set; }
        public int? StorageId { get; set; }
        public string Category { get; set; }
        public string TicketNo { get; set; }
        public string CreateBy { get; set; }
    }

    /// <summary>
    /// 耗品库存记录输入输出对象
    /// </summary>
    public class ConsumableStorageRecordDto
    {
        [Required(ErrorMessage = "耗品ID不能为空")]
        [ExcelColumn(Name = "耗品ID")]
        public int ConsumableId { get; set; }

        [ExcelColumn(Name = "采购料号")]
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

        [Required(ErrorMessage = "库存数量（变更前）不能为空")]
        [ExcelColumn(Name = "原数量")]
        public int BeforeQty { get; set; }

        [Required(ErrorMessage = "库存变动数量不能为空")]
        [ExcelColumn(Name = "变动数量")]
        public int ChangeQty { get; set; }

        [Required(ErrorMessage = "库存数量（变更后）不能为空")]
        [ExcelColumn(Name = "结存数量")]
        public int AfterQty { get; set; }

        [Required(ErrorMessage = "变动类型不能为空")]
        [ExcelColumn(Name = "变动类型")]
        public string StorageChangeType { get; set; }

        [ExcelColumn(Name = "相关人工号")]
        public string RelatedUser { get; set; }

        [ExcelColumn(Name = "相关人")]
        public string RelatedUserName { get; set; }

        [ExcelColumn(Ignore = true)]
        public string TicketNo { get; set; }

        [ExcelColumn(Ignore = true)]
        public string TicketType { get; set; }

        [ExcelColumn(Name = "备注")]
        public string Remark { get; set; }

        [ExcelColumn(Name = "操作人")]
        public string CreateBy { get; set; }

        [ExcelColumn(Name = "操作时间")]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "变动类型标签")]
        public string StorageChangeTypeLabel { get; set; }
    }
}
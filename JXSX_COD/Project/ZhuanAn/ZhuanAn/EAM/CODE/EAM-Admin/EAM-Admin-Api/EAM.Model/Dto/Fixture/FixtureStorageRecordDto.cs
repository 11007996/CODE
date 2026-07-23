namespace EAM.Model.Dto
{
    /// <summary>
    /// 治具出入库记录表查询对象
    /// </summary>
    public class FixtureStorageRecordQueryDto : PagerInfo
    {
        public int? FixtureId { get; set; }
        public int? StorageId { get; set; }
        public string TicketNo { get; set; }
        public string TicketType { get; set; }
    }

    /// <summary>
    /// 治具出入库记录表输入输出对象
    /// </summary>
    public class FixtureStorageRecordDto
    {
        [Required(ErrorMessage = "治具ID不能为空")]
        [ExcelColumn(Name = "治具ID")]
        public int FixtureId { get; set; }

        [ExcelColumn(Name = "系列")]
        public string Series { get; set; }

        [ExcelColumn(Name = "治具名称")]
        public string FixtureName { get; set; }

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
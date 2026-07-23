namespace EAM.Model.Dto
{
    /// <summary>
    /// 治具存储查询对象
    /// </summary>
    public class FixtureStorageQueryDto : PagerInfo
    {
        public int? FixtureId { get; set; }
        public int? StorageId { get; set; }
        public string FixtureName { get; set; }
        public string Series { get; set; }
    }

    public class FixtureStorageInfoDto
    {
        public int? FixtureId { get; set; }
        public int? StorageId { get; set; }
    }

    public class FixtureStorageSelectDto
    {
        /// <summary>
        /// 治具ID集合
        /// </summary>
        public string FixtureIds { get; set; }

        /// <summary>
        /// 扫描的储位
        /// </summary>
        public int? StorageId { get; set; }
    }

    /// <summary>
    /// 库存变更对象
    /// </summary>
    public class OperateFixtureStorageDto
    {
        [Required(ErrorMessage = "治具ID不能为空")]
        public int FixtureId { get; set; }

        [Required(ErrorMessage = "储位ID不能为空")]
        public int StorageId { get; set; }

        [Required(ErrorMessage = "数量不能为空")]
        public int ChangeQty { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remark { get; set; }

        public string TicketNo { get; set; }

        public string TicketType { get; set; }

        #region 领用时使用

        /// <summary>
        /// 相关人
        /// </summary>
        public string RelatedUser { get; set; }

        public int? LineId { get; set; }

        #endregion 领用时使用

        #region 归还时使用

        public int? FixtureUsingId { get; set; }

        #endregion 归还时使用

        #region 转移时使用

        public int? NewStorageId { get; set; }

        #endregion 转移时使用
    }

    public class FixtureStorageDto
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

        [ExcelColumn(Name = "储位")]
        public string StorageFullName { get; set; }

        [Required(ErrorMessage = "库存数量不能为空")]
        [ExcelColumn(Name = "数量")]
        public int Qty { get; set; }
    }

    /// <summary>
    /// 治具储存导入
    /// </summary>
    public class FixtureStorageImportDto
    {
        [ExcelColumn(Name = "治具ID")]
        public int? FixtureId { get; set; }

        [ExcelColumn(Name = "系列")]
        public string Series { get; set; }

        [ExcelColumn(Name = "治具名称")]
        public string FixtureName { get; set; }

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
    /// 治具储存导入
    /// </summary>
    public class FixtureStorageOperateImportDto
    {
        [ExcelColumn(Name = "治具ID")]
        public int? FixtureId { get; set; }

        [ExcelColumn(Name = "系列")]
        public string Series { get; set; }

        [ExcelColumn(Name = "治具名称")]
        public string FixtureName { get; set; }

        [ExcelColumn(Name = "储位ID")]
        public int? StorageId { get; set; }

        [ExcelColumn(Name = "储位名称")]
        public string StorageFullName { get; set; }

        [ExcelColumn(Name = "变动数量")]
        public int? ChangeQty { get; set; }

        [ExcelColumn(Ignore = true)]
        public string StorageChangeType { get; set; }

        [ExcelColumn(Name = "变动类型")]
        public string StorageChangeTypeLabel { get; set; }

        [ExcelColumn(Name = "领用人工号")]
        public string RelatedUser { get; set; }

        [ExcelColumn(Name = "领用人姓名")]
        public string RelatedUserName { get; set; }

        [ExcelColumn(Ignore = true)]
        public int? LineId { get; set; }

        [ExcelColumn(Name = "产线")]
        public string LineName { get; set; }

        [ExcelColumn(Ignore = true)]
        public int? FixtureUsingId { get; set; }

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
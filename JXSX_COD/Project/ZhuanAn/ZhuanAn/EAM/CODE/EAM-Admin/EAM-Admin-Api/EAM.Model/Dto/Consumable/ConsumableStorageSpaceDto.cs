namespace EAM.Model.Dto
{
    /// <summary>
    /// 耗品储位信息查询对象
    /// </summary>
    public class ConsumableStorageSpaceQueryDto : PagerInfo
    {
        public string StorageName { get; set; }
    }

    /// <summary>
    /// 耗品储位信息树形查询对象
    /// </summary>
    public class ConsumableStorageSpaceTreeQueryDto
    {
        public string StorageName { get; set; }
    }

    /// <summary>
    /// 耗品储位信息输入输出对象
    /// </summary>
    public class ConsumableStorageSpaceDto
    {
        [Required(ErrorMessage = "储位Id不能为空")]
        public int StorageId { get; set; }

        [Required(ErrorMessage = "储位名称不能为空")]
        public string StorageName { get; set; }

        public int? ParentId { get; set; }

        public int? OrderNum { get; set; }

        [Required(ErrorMessage = "储位类型不能为空")]
        public string StorageType { get; set; }

        [Required(ErrorMessage = "储位状态不能为空")]
        public string Status { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string Remark { get; set; }

        [ExcelColumn(Name = "父级ID")]
        public string ParentIdLabel { get; set; }

        [ExcelColumn(Name = "储位类型")]
        public string StorageTypeLabel { get; set; }

        [ExcelColumn(Name = "储位状态")]
        public string StatusLabel { get; set; }
    }
}
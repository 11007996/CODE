namespace EAM.Model.Consumable
{
    /// <summary>
    /// 耗品储位信息
    /// </summary>
    [SugarTable("CON_Storage_Space")]
    public class ConsumableStorageSpace
    {
        /// <summary>
        /// 储位Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "storage_ID")]
        public int StorageId { get; set; }

        /// <summary>
        /// 储位名称
        /// </summary>
        [SugarColumn(ColumnName = "storage_Name")]
        public string StorageName { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_Id")]
        public int? ParentId { get; set; }

        /// <summary>
        /// 储位全名
        /// </summary>
        [SugarColumn(ColumnName = "Storage_Full_Name")]
        public string StorageFullName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [SugarColumn(ColumnName = "order_Num")]
        public int? OrderNum { get; set; }

        /// <summary>
        /// 储位类型
        /// </summary>
        [SugarColumn(ColumnName = "storage_Type")]
        public string StorageType { get; set; }

        /// <summary>
        /// 储位状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 祖级列表
        /// </summary>
        public string Ancestors { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<ConsumableStorageSpace> Children { get; set; }
    }
}
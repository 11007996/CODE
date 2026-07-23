namespace EAM.Model.Equipment
{
    /// <summary>
    /// 设备文件
    /// </summary>
    [SugarTable("EQU_Equipment_File")]
    public class EquipmentFile
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "File_Id")]
        public long FileId { get; set; }
    }
}
namespace EAM.Model.Equipment
{
    [SugarTable("EQU_Equipment_File_Bind")]
    public class EquipmentFileBind
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "Equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "File_Id")]
        public long? FileId { get; set; }
    }
}
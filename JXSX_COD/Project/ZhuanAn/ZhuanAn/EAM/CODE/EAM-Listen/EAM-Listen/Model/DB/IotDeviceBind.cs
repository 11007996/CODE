using SqlSugar;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 产品设备表
    /// </summary>
    [SugarTable("IOT_Device_Bind")]
    public class IotDeviceBind
    {
        /// <summary>
        /// IOT设备ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "device_Id")]
        public int DeviceId { get; set; }

        /// <summary>
        /// 资产设备ID
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 呼叫盒ID
        /// </summary>
        [SugarColumn(ColumnName = "box_Id")]
        public int? BoxId { get; set; }
    }
}
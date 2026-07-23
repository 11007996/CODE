using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 设备采集数据
    /// </summary>
    [SugarTable("IOT_Device_Data")]
    public class IotDeviceData
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "device_Id")]
        public int DeviceId { get; set; }

        /// <summary>
        /// 属性标识
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string Identifier { get; set; }

        /// <summary>
        /// 上报时间
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "collect_Time")]
        public DateTime? CollectTime { get; set; }

        /// <summary>
        /// 可用值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 原始值
        /// </summary>
        [SugarColumn(ColumnName = "raw_Value")]
        public string RawValue { get; set; }

        /// <summary>
        /// 数据质量
        /// </summary>
        public byte Quality { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }
}
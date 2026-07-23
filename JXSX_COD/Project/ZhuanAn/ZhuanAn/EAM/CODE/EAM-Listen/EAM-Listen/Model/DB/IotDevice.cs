using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 产品设备表
    /// </summary>
    [SugarTable("IOT_Device")]
    public class IotDevice
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "device_Id")]
        public int DeviceId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(ColumnName = "product_Id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [SugarColumn(ColumnName = "device_Name")]
        public string DeviceName { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [SugarColumn(ColumnName = "device_Key")]
        public string DeviceKey { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(ColumnName = "ip_Address")]
        public string IpAddress { get; set; }

        /// <summary>
        /// 激活时间
        /// </summary>
        [SugarColumn(ColumnName = "activate_Time")]
        public DateTime? ActivateTime { get; set; }

        /// <summary>
        /// 最后上线时间
        /// </summary>
        [SugarColumn(ColumnName = "last_Online_Time")]
        public DateTime? LastOnlineTime { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enabled { get; set; }

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
        /// 注册包
        /// </summary>
        [SugarColumn(ColumnName = "Register_Packet")]
        public string RegisterPacket { get; set; }
    }
}
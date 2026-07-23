using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 设备扩展信息
    /// </summary>
    [SugarTable("EQU_Equipment_Extend")]
    public class EquipmentExtend
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 设备代码
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Code")]
        public int? EquipmentCode { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [SugarColumn(ColumnName = "equipment_No")]
        public int? EquipmentNo { get; set; }

        /// <summary>
        /// 理论CT
        /// </summary>
        [SugarColumn(ColumnName = "theory_CT")]
        public decimal TheoryCT { get; set; }

        /// <summary>
        /// 功率(KW)
        /// </summary>
        public decimal Power { get; set; }

        /// <summary>
        /// 是否连接
        /// </summary>
        [SugarColumn(ColumnName = "is_Link")]
        public string IsLink { get; set; }

        /// <summary>
        /// 产线Id
        /// </summary>
        [SugarColumn(ColumnName = "line_Id")]
        public int? LineId { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        [SugarColumn(ColumnName = "IP")]
        public string Ip { get; set; }

        /// <summary>
        /// 在线状态
        /// </summary>
        [SugarColumn(ColumnName = "Is_Online")]
        public bool? IsOnline { get; set; }

        /// <summary>
        /// 最后在线时间
        /// </summary>
        [SugarColumn(ColumnName = "Last_Online_Time")]
        public DateTime? LastOnlineTime { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string LineName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AssetNo { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string EquipmentName { get; set; }
    }
}
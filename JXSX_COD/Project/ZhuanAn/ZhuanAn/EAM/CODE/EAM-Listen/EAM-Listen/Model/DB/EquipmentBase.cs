using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 设备资产信息
    /// </summary>
    [SugarTable("EQU_Equipment_Base")]
    public class EquipmentBase
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Name")]
        public string EquipmentName { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [SugarColumn(ColumnName = "asset_No")]
        public string AssetNo { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "factory_Code")]
        public string FactoryCode { get; set; }

        /// <summary>
        /// 资产主编号
        /// </summary>
        [SugarColumn(ColumnName = "asset_Main_No")]
        public string AssetMainNo { get; set; }

        /// <summary>
        /// 资产子编号
        /// </summary>
        [SugarColumn(ColumnName = "asset_Sub_No")]
        public string AssetSubNo { get; set; }

        /// <summary>
        /// 资产名称
        /// </summary>
        [SugarColumn(ColumnName = "asset_Name")]
        public string AssetName { get; set; }

        /// <summary>
        /// 资产分类
        /// </summary>
        [SugarColumn(ColumnName = "asset_Class")]
        public string AssetClass { get; set; }

        /// <summary>
        /// 型号规格
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 购置日期
        /// </summary>
        [SugarColumn(ColumnName = "entry_Date")]
        public DateTime? EntryDate { get; set; }

        /// <summary>
        /// 成本中心
        /// </summary>
        [SugarColumn(ColumnName = "cost_Center")]
        public string CostCenter { get; set; }

        /// <summary>
        /// 耐用年限
        /// </summary>
        [SugarColumn(ColumnName = "durable_Year")]
        public int? DurableYear { get; set; }

        /// <summary>
        /// 耐用月数
        /// </summary>
        [SugarColumn(ColumnName = "durable_Month")]
        public int? DurableMonth { get; set; }

        /// <summary>
        /// 制造厂商
        /// </summary>
        [SugarColumn(ColumnName = "made_Factory")]
        public string MadeFactory { get; set; }

        /// <summary>
        /// 校验管制编号
        /// </summary>
        [SugarColumn(ColumnName = "control_No")]
        public string ControlNo { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        [SugarColumn(ColumnName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 删除标记(0有效，1删除)
        /// </summary>
        [SugarColumn(ColumnName = "del_Flag")]
        public int DelFlag { get; set; }
    }
}
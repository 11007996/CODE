using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 产品物模型属性
    /// </summary>
    [SugarTable("IOT_Product_Thing_Property")]
    public class IotProductThingProperty
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "property_Id")]
        public int PropertyId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(ColumnName = "product_Id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        [SugarColumn(ColumnName = "property_Name")]
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性标识
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [SugarColumn(ColumnName = "data_Type")]
        public string DataType { get; set; }

        /// <summary>
        /// 读写标记
        /// </summary>
        [SugarColumn(ColumnName = "rW_Flag")]
        public string RwFlag { get; set; }

        /// <summary>
        /// 扩展描述
        /// </summary>
        [SugarColumn(ColumnName = "expand_Desc")]
        public string ExpandDesc { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

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
    }
}
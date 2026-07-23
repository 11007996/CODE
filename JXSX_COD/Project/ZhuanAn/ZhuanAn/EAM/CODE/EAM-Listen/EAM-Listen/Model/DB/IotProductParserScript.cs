using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 数据解析脚本
    /// </summary>
    [SugarTable("IOT_Product_Parser_Script")]
    public class IotProductParserScript
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "product_Id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 脚本代码
        /// </summary>
        [SugarColumn(ColumnName = "Script_Code")]
        public string ScriptCode { get; set; }

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
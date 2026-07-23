using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 产品主题类表
    /// </summary>
    [SugarTable("IOT_Product_Topic")]
    public class IotProductTopic
    {
        /// <summary>
        /// 主题ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "topic_Id")]
        public int TopicId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(ColumnName = "product_Id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 主题名称
        /// </summary>
        [SugarColumn(ColumnName = "topic_Name")]
        public string TopicName { get; set; }

        /// <summary>
        /// 主题格式
        /// </summary>
        [SugarColumn(ColumnName = "topic_Format")]
        public string TopicFormat { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }

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
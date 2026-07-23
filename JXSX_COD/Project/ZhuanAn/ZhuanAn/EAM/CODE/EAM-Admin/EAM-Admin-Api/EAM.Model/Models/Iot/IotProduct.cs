namespace EAM.Model.Iot
{
    /// <summary>
    /// 产品表
    /// </summary>
    [SugarTable("IOT_Product")]
    public class IotProduct
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "product_Id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [SugarColumn(ColumnName = "product_Name")]
        public string ProductName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        [SugarColumn(ColumnName = "node_Type")]
        public string NodeType { get; set; }

        /// <summary>
        /// 接入协议
        /// </summary>
        [SugarColumn(ColumnName = "Access_Protocol")]
        public string AccessProtocol { get; set; }

        /// <summary>
        /// 数据格式
        /// </summary>
        [SugarColumn(ColumnName = "data_Format")]
        public string DataFormat { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public int? Version { get; set; }

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
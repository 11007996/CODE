namespace EAM.Model.Iot
{
    /// <summary>
    /// 产品参数定义
    /// </summary>
    [SugarTable("IOT_Product_Param_Define")]
    public class IotProductParamDefine
    {
        /// <summary>
        /// 所属类型
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "owner_Type")]
        public string OwnerType { get; set; }

        /// <summary>
        /// 所属Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "owner_Id")]
        public int OwnerId { get; set; }

        /// <summary>
        /// 参数标识
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string Identifier { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [SugarColumn(ColumnName = "param_Name")]
        public string ParamName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [SugarColumn(ColumnName = "data_Type")]
        public string DataType { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "sort_Order")]
        public int? SortOrder { get; set; }
    }
}
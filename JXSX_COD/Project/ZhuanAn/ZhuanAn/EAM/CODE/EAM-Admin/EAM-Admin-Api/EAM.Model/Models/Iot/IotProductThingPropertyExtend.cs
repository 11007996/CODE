namespace EAM.Model.Iot
{
    /// <summary>
    /// 属性扩展描述
    /// </summary>
    [SugarTable("IOT_Product_Thing_Property_Extend")]
    public class IotProductThingPropertyExtend
    {
        /// <summary>
        /// 属性ID 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "property_Id")]
        public int PropertyId { get; set; }

        /// <summary>
        /// 操作类型 
        /// </summary>
        [SugarColumn(ColumnName = "operate_Type")]
        public string OperateType { get; set; }

        /// <summary>
        /// 寄存器地址 
        /// </summary>
        [SugarColumn(ColumnName = "register_Address")]
        public int RegisterAddress { get; set; }

        /// <summary>
        /// 原始数据类型 
        /// </summary>
        [SugarColumn(ColumnName = "original_Data_Type")]
        public string OriginalDataType { get; set; }

        /// <summary>
        /// 寄存器数据个数 
        /// </summary>
        [SugarColumn(ColumnName = "register_Count")]
        public int? RegisterCount { get; set; }

        /// <summary>
        /// 交换寄存器内高低序 
        /// </summary>
        public bool Swap16 { get; set; }

        /// <summary>
        /// 交换寄存器顺序 
        /// </summary>
        [SugarColumn(ColumnName = "reverse_Register")]
        public bool ReverseRegister { get; set; }

        /// <summary>
        /// 缩放因子 
        /// </summary>
        public decimal Scaling { get; set; }

        /// <summary>
        /// 下限 
        /// </summary>
        [SugarColumn(ColumnName = "low_Limit")]
        public int? LowLimit { get; set; }

        /// <summary>
        /// 上限 
        /// </summary>
        [SugarColumn(ColumnName = "upper_Limit")]
        public int? UpperLimit { get; set; }

        /// <summary>
        /// 读写操作 
        /// </summary>
        [SugarColumn(ColumnName = "function_Code")]
        public string FunctionCode { get; set; }

        /// <summary>
        /// 上报方式 
        /// </summary>
        [SugarColumn(ColumnName = "Reporting_Method")]
        public string ReportingMethod { get; set; }

    }
}
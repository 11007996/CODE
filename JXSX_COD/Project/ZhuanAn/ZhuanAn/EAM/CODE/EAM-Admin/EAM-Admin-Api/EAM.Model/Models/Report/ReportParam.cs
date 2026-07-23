namespace EAM.Model.Report
{
    /// <summary>
    /// 报表参数
    /// </summary>
    [SugarTable("rep_report_param")]
    [Tenant("0")]
    public class ReportParam
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "param_Id")]
        public int ParamId { get; set; }

        /// <summary>
        /// 报表ID
        /// </summary>
        [SugarColumn(ColumnName = "report_Id")]
        public int ReportId { get; set; }

        /// <summary>
        /// 参数键
        /// </summary>
        [SugarColumn(ColumnName = "param_Key")]
        public string ParamKey { get; set; }

        /// <summary>
        /// 参数标签
        /// </summary>
        [SugarColumn(ColumnName = "param_Label")]
        public string ParamLabel { get; set; }

        /// <summary>
        /// 标签类型
        /// </summary>
        [SugarColumn(ColumnName = "element_Type")]
        public string ElementType { get; set; }

        /// <summary>
        /// 输入类型
        /// </summary>
        [SugarColumn(ColumnName = "input_Type")]
        public string InputType { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [SugarColumn(ColumnName = "default_Value")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 头部字符
        /// </summary>
        [SugarColumn(ColumnName = "Head_Value")]
        public string HeadValue { get; set; }

        /// <summary>
        /// 尾部字符
        /// </summary>
        [SugarColumn(ColumnName = "Tail_Value")]
        public string TailValue { get; set; }

        /// <summary>
        /// 选项源
        /// </summary>
        [SugarColumn(ColumnName = "options_Source")]
        public string OptionsSource { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "sort_Order")]
        public int SortOrder { get; set; }
    }
}
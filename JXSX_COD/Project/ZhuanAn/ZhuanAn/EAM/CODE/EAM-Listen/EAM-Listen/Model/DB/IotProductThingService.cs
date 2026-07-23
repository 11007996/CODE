using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 产品物模型服务
    /// </summary>
    [SugarTable("IOT_Product_Thing_Service")]
    public class IotProductThingService
    {
        /// <summary>
        /// 服务ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "service_Id")]
        public int ServiceId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(ColumnName = "product_Id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [SugarColumn(ColumnName = "service_Name")]
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务标识
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// 调用方式
        /// </summary>
        [SugarColumn(ColumnName = "invoke_Mode")]
        public string InvokeMode { get; set; }

        /// <summary>
        /// 输入参数
        /// </summary>
        [SugarColumn(ColumnName = "input_Params")]
        public string InputParams { get; set; }

        /// <summary>
        /// 输出参数
        /// </summary>
        [SugarColumn(ColumnName = "output_Params")]
        public string OutputParams { get; set; }

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
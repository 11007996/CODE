namespace EAM.Model.Iot
{
    /// <summary>
    /// 产品物模型事件
    /// </summary>
    [SugarTable("IOT_Product_Thing_Event")]
    public class IotProductThingEvent
    {
        /// <summary>
        /// 事件ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "event_Id")]
        public int EventId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(ColumnName = "product_Id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 事件名称
        /// </summary>
        [SugarColumn(ColumnName = "event_Name")]
        public string EventName { get; set; }

        /// <summary>
        /// 事件标识
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        [SugarColumn(ColumnName = "event_Type")]
        public string EventType { get; set; }

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

        /// <summary>
        /// 输出参数
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(IotProductParamDefine.OwnerId), nameof(EventId), "Owner_Type='event' AND Direction='out'")] //自定义关系映射
        public List<IotProductParamDefine> OutputParams { get; set; }
    }
}
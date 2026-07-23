using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 产品事件处理动作
    /// </summary>
    [SugarTable("IOT_Product_Event_Action")]
    public class IotProductEventAction
    {
        /// <summary>
        /// 动作Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "action_Id")]
        public int ActionId { get; set; }

        /// <summary>
        /// 事件ID
        /// </summary>
        [SugarColumn(ColumnName = "event_Id")]
        public int EventId { get; set; }

        /// <summary>
        /// 动作名称
        /// </summary>
        [SugarColumn(ColumnName = "action_Name")]
        public string ActionName { get; set; }

        /// <summary>
        /// 动作类型
        /// </summary>
        [SugarColumn(ColumnName = "action_Type")]
        public string ActionType { get; set; }

        /// <summary>
        /// 动作配置
        /// </summary>
        [SugarColumn(ColumnName = "action_Config")]
        public string ActionConfig { get; set; }

        /// <summary>
        /// 动作顺序
        /// </summary>
        [SugarColumn(ColumnName = "sort_Order")]
        public int SortOrder { get; set; }

        /// <summary>
        /// 执行超时(秒)
        /// </summary>
        public int Timeout { get; set; }

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
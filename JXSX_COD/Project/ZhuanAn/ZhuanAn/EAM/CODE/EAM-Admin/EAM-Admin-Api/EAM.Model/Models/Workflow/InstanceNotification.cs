namespace EAM.Model.Workflow
{
    /// <summary>
    /// 流程通知
    /// </summary>
    [SugarTable("WF_Instance_Notification")]
    public class InstanceNotification
    {
        /// <summary>
        /// 通知ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "notification_ID")]
        public int NotificationId { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        [SugarColumn(ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(ColumnName = "node_ID")]
        public int NodeId { get; set; }

        /// <summary>
        /// 通知人员
        /// </summary>
        [SugarColumn(ColumnName = "notify_To")]
        public string NotifyTo { get; set; }

        /// <summary>
        /// 通知时间
        /// </summary>
        [SugarColumn(ColumnName = "notify_Time")]
        public DateTime? NotifyTime { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        [SugarColumn(ColumnName = "notify_Content")]
        public string NotifyContent { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        [SugarColumn(ColumnName = "is_Read")]
        public bool IsRead { get; set; }
    }
}
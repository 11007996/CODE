namespace EAM.Model.Workflow
{
    /// <summary>
    /// 流程附件
    /// </summary>
    [SugarTable("WF_Instance_Attachment")]
    public class InstanceAttachment
    {
        /// <summary>
        /// 附件ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "attachment_ID")]
        public int AttachmentId { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        [SugarColumn(ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(ColumnName = "node_ID")]
        public int? NodeId { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        [SugarColumn(ColumnName = "task_ID")]
        public int? TaskId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [SugarColumn(ColumnName = "file_Name")]
        public string FileName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [SugarColumn(ColumnName = "file_Type")]
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [SugarColumn(ColumnName = "file_Size")]
        public long FileSize { get; set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        [SugarColumn(ColumnName = "file_Storage_Path")]
        public string FileStoragePath { get; set; }

        /// <summary>
        /// 上传人员
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }
    }
}
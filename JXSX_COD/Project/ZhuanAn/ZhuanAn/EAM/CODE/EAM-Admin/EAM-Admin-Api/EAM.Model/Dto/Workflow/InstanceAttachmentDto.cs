namespace EAM.Model.Dto
{
    /// <summary>
    /// 流程附件查询对象
    /// </summary>
    public class InstanceAttachmentQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 流程附件输入输出对象
    /// </summary>
    public class InstanceAttachmentDto
    {
        [Required(ErrorMessage = "附件ID不能为空")]
        public int AttachmentId { get; set; }

        [Required(ErrorMessage = "流程编号不能为空")]
        public string ProcessInstanceId { get; set; }

        public int? NodeId { get; set; }

        public int? TaskId { get; set; }

        [Required(ErrorMessage = "文件名不能为空")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "文件类型不能为空")]
        public string FileType { get; set; }

        [Required(ErrorMessage = "文件大小不能为空")]
        public long FileSize { get; set; }

        [Required(ErrorMessage = "存储路径不能为空")]
        public string FileStoragePath { get; set; }

        [Required(ErrorMessage = "上传人员不能为空")]
        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "文件类型")]
        public string FileTypeLabel { get; set; }
    }
}
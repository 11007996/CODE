namespace EAM.Model.System.Dto
{
    /// <summary>
    /// 文件预览信息
    /// </summary>
    public class SysFilePreviewDto
    {
        /// <summary>
        /// 预览文件ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long FileId { get; set; }

        /// <summary>
        /// 源文件ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long SourceFileId { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件别名（下载名）
        /// </summary>
        public string FileAliasName { get; set; }

        /// <summary>
        /// 文件后缀类型
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long FileSize { get; set; }
    }
}
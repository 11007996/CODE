namespace EAM.Common.Model
{
    /// <summary>
    /// 预览文件信息
    /// </summary>
    public class PreviewFileInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileAliasName { get; set; }

        /// <summary>
        /// 文件后缀类型
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }
    }
}
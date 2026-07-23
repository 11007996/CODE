namespace EAM.Model.System
{
    /// <summary>
    /// 文件预览信息
    /// </summary>
    [Tenant("0")]
    [SugarTable("sys_file_preview", "文件预览表")]
    public class SysFilePreview
    {
        /// <summary>
        /// 预览文件ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(IsPrimaryKey = true, ColumnName = "File_Id")]
        public long FileId { get; set; }

        /// <summary>
        /// 源文件ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnName = "Source_File_Id")]
        public long SourceFileId { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [SugarColumn(ColumnName = "Page_No")]
        public int PageNo { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [SugarColumn(ColumnName = "File_Name")]
        public string FileName { get; set; }

        /// <summary>
        /// 文件别名（下载名）
        /// </summary>
        [SugarColumn(ColumnName = "File_Alias_Name")]
        public string FileAliasName { get; set; }

        /// <summary>
        /// 文件后缀类型
        /// </summary>
        [SugarColumn(ColumnName = "File_Extension")]
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnName = "File_Size")]
        public long FileSize { get; set; }

        /// <summary>
        /// 文件存储地址 eg：/uploads/20220202
        /// </summary>
        [SugarColumn(ColumnName = "File_Url")]
        public string FileUrl { get; set; }

        /// <summary>
        /// 仓库位置 eg：/uploads
        /// </summary>
        [SugarColumn(ColumnName = "Store_Path")]
        public string StorePath { get; set; }

        /// <summary>
        /// 访问路径
        /// </summary>
        [SugarColumn(ColumnName = "Access_Url")]
        public string AccessUrl { get; set; }
    }
}
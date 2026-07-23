using Asset.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Model
{
    /// <summary>
    /// 上传文件的信息Model
    /// </summary>
    public class PreviewFileInfoDO
    {
        public int? FileId { get; set; }
        /// <summary>
        /// 文件ID
        /// </summary>
        public int? SourceFileId { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int? PageNo { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
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
        public long? FileSize { get; set; }

        /// <summary>
        /// 文件分类
        /// </summary>
        public FilePreviewTypeEnum? PreviewType { get; set; }

    }
}

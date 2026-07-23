using System.Collections.Generic;

namespace EAM.Listen.Common.Config
{
    /// <summary>
    /// 自定义通信协议配置
    /// </summary>
    public class SignalConfigDto
    {
        /// <summary>
        /// 数据上传开关
        /// </summary>
        public bool UploadDataFlag { get; set; } = true;

        /// <summary>
        /// 接收数据编码
        /// </summary>
        public List<ItemCode> ReceiveCodeItems { get; set; } = new List<ItemCode>();

        /// <summary>
        /// 发送数据编码
        /// </summary>
        public List<ItemCode> SendCodeItems { get; set; } = new List<ItemCode>();
    }

    /// <summary>
    /// 编码项目名称
    /// </summary>
    public class ItemCode
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 字节长度
        /// </summary>
        public int ByteLen { get; set; }

        /// <summary>
        /// 固定编码（编码不固定为空）
        /// </summary>
        public string FixedCode { get; set; }
    }
}
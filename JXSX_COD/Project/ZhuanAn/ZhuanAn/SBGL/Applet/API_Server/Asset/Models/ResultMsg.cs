using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asset.Models
{
    public class ResultMsg
    {
        /// <summary>
        /// 0表示成功
        /// </summary>
        public string MsgCode { get; set; }
        /// <summary>
        /// 消息提示
        /// </summary>
        public string MsgInfo { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
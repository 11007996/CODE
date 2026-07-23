namespace EAM.Common.Wechat.Model
{
    public class WxSendMessageParm
    {
        /// <summary>
        /// 是否必填  说明
        /// 否        指定接收消息的成员，成员ID列表（多个接收者用‘|’分隔，最多支持1000个）。
        ///           特殊情况：指定为"@all"，则向该企业应用的全部成员发送
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 是否必填  说明
        /// 否        指定接收消息的部门，部门ID列表，多个接收者用‘|’分隔，最多支持100个。
        ///           当touser为"@all"时忽略本参数
        /// </summary>
        public string toparty { get; set; }

        /// <summary>
        /// 是否必填  说明
        /// 否        指定接收消息的标签，标签ID列表，多个接收者用‘|’分隔，最多支持100个。
        //            当touser为"@all"时忽略本参数
        /// </summary>
        public string totag { get; set; }

        /// <summary>
        /// 是否必填  说明
        /// 是        消息类型，此时固定为：text
        /// </summary>
        public string msgtype { get; set; }

        /// <summary>
        /// 是否必填  说明
        /// 是        企业应用的id，整型。企业内部开发，可在应用的设置页面查看；第三方服务商，可通过接口 获取企业授权信息 获取该参数值
        /// </summary>
        public int agentid { get; set; }

        /// <summary>
        /// 是否必填  说明
        /// 否        表示是否是保密消息，0表示可对外分享，1表示不能分享且内容显示水印，默认为0
        /// </summary>
        public int safe { get; set; } = 0;

        /// <summary>
        /// 是否必填  说明
        /// 否        表示是否开启id转译，0表示否，1表示是，默认0。
        /// </summary>
        public int enable_id_trans { get; set; } = 0;

        /// <summary>
        /// 是否必填  说明
        /// 否        表示是否开启重复消息检查，0表示否，1表示是，默认0
        /// </summary>
        public int enable_duplicate_check { get; set; } = 0;

        /// <summary>
        /// 是否必填  说明
        /// 否        表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时
        /// </summary>
        public int duplicate_check_interval { get; set; } = 1800;
    }

    public class WxSendTextMessageParm : WxSendMessageParm
    {
        public WxMsgText text = new WxMsgText();
    }

    /// <summary>
    /// 文本消息内容
    /// </summary>
    public class WxMsgText
    {
        /// <summary>
        /// 是否必填  说明
        /// 是        消息内容，最长不超过2048个字节，超过将截断（支持id转译）
        /// 特殊说明：
        ///         其中text参数的content字段可以支持换行、以及A标签，即可打开自定义的网页（可参考以上示例代码）(注意：换行符请用转义过的\n)
        /// </summary>
        public string content { get; set; }
    }
}
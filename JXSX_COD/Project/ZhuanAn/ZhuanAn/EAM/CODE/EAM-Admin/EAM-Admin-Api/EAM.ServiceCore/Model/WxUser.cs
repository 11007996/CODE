namespace EAM.ServiceCore.Model
{
    [SugarTable("wx_user")]
    [Tenant("0")]
    public class WxUser
    {
        /// <summary>
        /// 关联的设备系统的用户名(工号)
        /// </summary>
        [SugarColumn(ColumnName = "sys_username")]
        public string sys_username { get; set; }

        //参数	说明
        /// <summary>
        /// 成员UserID。对应管理端的帐号，企业内必须唯一。不区分大小写，长度为1~64个字节；第三方应用返回的值为open_userid
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "userid")]
        public string userid { get; set; }

        /// <summary>
        /// 成员名称；第三方不可获取，调用时返回userid以代替name；代开发自建应用需要管理员授权才返回；对于非第三方创建的成员，第三方通讯录应用也不可获取；未返回name的情况需要通过通讯录展示组件来展示名字
        /// </summary>
        [SugarColumn(ColumnName = "name")]
        public string name { get; set; }

        /// <summary>
        /// 手机号码，代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "mobile")]
        public string mobile { get; set; }

        /// <summary>
        /// 成员所属部门id列表，仅返回该应用有查看权限的部门id；成员授权模式下，固定返回根部门id，即固定为1。对授权了“组织架构信息”权限的第三方应用，返回成员所属的全部部门id
        ///     逗号分隔
        /// </summary>
        [SugarColumn(ColumnName = "department")]
        public string department { get; set; }

        /// <summary>
        /// 部门内的排序值，默认为0。数量必须和department一致，数值越大排序越前面。值范围是[0, 2^32)。成员授权模式下不返回该字段
        ///     逗号分隔
        /// </summary>
        [SugarColumn(ColumnName = "order")]
        public string order { get; set; }

        /// <summary>
        /// 职务信息；代开发自建应用需要管理员授权才返回；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "position")]
        public string position { get; set; }

        /// <summary>
        /// 性别。0表示未定义，1表示男性，2表示女性。代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段。注：不可获取指返回值0
        /// </summary>
        [SugarColumn(ColumnName = "gender")]
        public string gender { get; set; }

        /// <summary>
        /// 邮箱，代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "email")]
        public string email { get; set; }

        /// <summary>
        /// 企业邮箱，代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "biz_mail")]
        public string biz_mail { get; set; }

        /// <summary>
        /// 表示在所在的部门内是否为部门负责人，数量与department一致；第三方通讯录应用或者授权了“组织架构信息-应用可获取企业的部门组织架构信息-部门负责人”权限的第三方应用可获取；对于非第三方创建的成员，第三方通讯录应用不可获取；上游企业不可获取下游企业成员该字段
        ///     逗号分隔
        /// </summary>
        [SugarColumn(ColumnName = "is_leader_in_dept")]
        public string is_leader_in_dept { get; set; }

        /// <summary>
        /// 直属上级UserID，返回在应用可见范围内的直属上级列表，最多有五个直属上级；第三方通讯录应用或者授权了“组织架构信息-应用可获取可见范围内成员组织架构信息-直属上级”权限的第三方应用可获取；对于非第三方创建的成员，第三方通讯录应用不可获取；上游企业不可获取下游企业成员该字段；代开发自建应用不可获取该字段
        ///     逗号分隔
        /// </summary>
        [SugarColumn(ColumnName = "direct_leader")]
        public string direct_leader { get; set; }

        /// <summary>
        /// 头像url。 代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "avatar")]
        public string avatar { get; set; }

        /// <summary>
        /// 头像缩略图url。第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        ///     逗号分隔
        /// </summary>
        [SugarColumn(ColumnName = "thumb_avatar")]
        public string thumb_avatar { get; set; }

        /// <summary>
        /// 座机。代开发自建应用需要管理员授权才返回；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "telephone")]
        public string telephone { get; set; }

        /// <summary>
        /// 别名；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "alias")]
        public string alias { get; set; }

        /// <summary>
        /// 扩展属性，代开发自建应用需要管理员授权才返回；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        ///     序列化数组[{name:"xx",value:"xx"}]
        /// </summary>
        [SugarColumn(ColumnName = "extattr")]
        public string extattr { get; set; }

        /// <summary>
        /// 激活状态: 1=已激活，2=已禁用，4=未激活，5=退出企业。
        /// 已激活代表已激活企业微信或已关注微信插件（原企业号）。未激活代表既未激活企业微信又未关注微信插件（原企业号）。
        /// </summary>
        [SugarColumn(ColumnName = "status")]
        public string status { get; set; }

        /// <summary>
        /// 员工个人二维码，扫描可添加为外部联系人(注意返回的是一个url，可在浏览器上打开该url以展示二维码)；代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "qr_code")]
        public string qr_code { get; set; }

        /// <summary>
        /// 成员对外属性，字段详情见对外属性；代开发自建应用需要管理员授权才返回；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "external_profile")]
        public string external_profile { get; set; }

        /// <summary>
        /// 对外职务，如果设置了该值，则以此作为对外展示的职务，否则以position来展示。代开发自建应用需要管理员授权才返回；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "external_position")]
        public string external_position { get; set; }

        /// <summary>
        /// 地址。代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        [SugarColumn(ColumnName = "address")]
        public string address { get; set; }

        /// <summary>
        /// 全局唯一。对于同一个服务商，不同应用获取到企业内同一个成员的open_userid是相同的，最多64个字节。仅第三方应用可获取
        /// </summary>
        [SugarColumn(ColumnName = "open_userid")]
        public string open_userid { get; set; }

        /// <summary>
        /// 主部门，仅当应用对主部门有查看权限时返回。
        /// </summary>
        [SugarColumn(ColumnName = "main_department")]
        public string main_department { get; set; }
    }
}
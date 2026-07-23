namespace EAM.Model.Dto
{
    /// <summary>
    /// 厂区配置查询对象
    /// </summary>
    public class FactoryConfigQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 厂区配置输入输出对象
    /// </summary>
    public class FactoryConfigDto
    {
        [Required(ErrorMessage = "主键ID不能为空")]
        public int Id { get; set; }

        [Required(ErrorMessage = "配置键名不能为空")]
        public string ConfigKey { get; set; }

        [Required(ErrorMessage = "配置键值不能为空")]
        public string ConfigValue { get; set; }

        public string ConfigType { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string EnableFlag { get; set; }
    }

    /// <summary>
    /// 设备数据主动上传编码配置
    /// </summary>
    public class EquipmentDataItemCode
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

    /// <summary>
    /// 接收设备数据项目名常量
    /// </summary>
    public class ReceiveItemNameConstant
    {
        public const string 前缀 = "Prefix";
        public const string 后缀 = "Suffix";
        public const string 设备编码 = "EquipmentCode";
        public const string 操作指令 = "OperateCode";
        public const string 运行状态 = "RunState";
        public const string 产线编码 = "LineCode";
        public const string 产能数量 = "ProductCount";
        public const string 不良数量 = "DefectCount";
        public const string 报警状态 = "WarnState";
        public const string 报警代码 = "WarnCode";
        public const string 其他 = "Other";
    }

    /// <summary>
    /// 发送设备数据项目名常量
    /// </summary>
    public class SendItemNameConstant
    {
        public const string 前缀 = "Prefix";
        public const string 后缀 = "Suffix";
        public const string 设备编码 = "EquipmentCode";
        public const string 操作指令 = "OperateCode";
        public const string 操作结果 = "ResultCode";
        public const string 其他 = "Other";
    }
}
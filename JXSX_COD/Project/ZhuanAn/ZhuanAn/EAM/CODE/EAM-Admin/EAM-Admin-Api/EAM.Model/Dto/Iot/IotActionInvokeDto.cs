namespace EAM.Model.Dto
{
    /// <summary>
    /// 动作调用 参数
    /// </summary>
    public class IotActionInvokeDto
    {
        /// <summary>
        /// 厂区Id
        /// </summary>
        [Required(ErrorMessage = "厂区Id不能为空")]
        public string FactoryId { get; set; }

        [Required(ErrorMessage = "设备Id不能为空")]
        public int DeviceId { get; set; }

        [Required(ErrorMessage = "跟踪Id不能为空")]
        public string TraceId { get; set; }

        [Required(ErrorMessage = "事件Id不能为空")]
        public int EventId { get; set; }

        /// <summary>
        /// 数据（Json字符串）
        /// </summary>
        [Required(ErrorMessage = "事件数据不能为空")]
        public string Data { get; set; }
    }

    /// <summary>
    /// 发送消息通知动作配置
    /// </summary>
    public class ActionConfigForSendWxMsgDto
    {
        public string WxMsgTemplate { get; set; }
        public string EmpCodes { get; set; }
        public string WxChatId { get; set; }
    }

    /// <summary>
    /// 检查保养状态的动作配置
    /// </summary>
    public class ActionConfigForCheckMaintainStatusDto
    {
        public int DaySeparation { get; set; }
        public string DateMarks { get; set; }
    }

    /// <summary>
    /// 产线同步的动作配置
    /// </summary>
    public class ActionConfigForSyncLineDto
    {
        /// <summary>
        /// 参数类型：lineCode:产线代码，lineId:产线Id,lineName:产线名称
        /// </summary>
        public string ParamValueType { get; set; }

        /// <summary>
        /// 参数标识
        /// </summary>
        public string ParamIdentifier { get; set; }
    }

    /// <summary>
    /// 添加呼叫盒操作 动作配置
    /// </summary>
    public class ActionConfigForAddCallBoxOperateDto
    {
        /// <summary>
        /// 呼叫操作类型 参数标识(必填)
        /// </summary>
        public string OperateTypeIdentifier { get; set; }

        /// <summary>
        /// 呼叫人 参数标识(非必填)
        /// </summary>
        public string UsernameIdentifier { get; set; }

        /// <summary>
        /// 呼叫操作类型值映射到标准值
        /// </summary>
        public List<KeyValuePair<string, int>> OperateTypeMapping { get; set; }
    }

    /// <summary>
    /// 响应数据的动作配置
    /// </summary>
    public class ActionConfigForResponseDataItemDto
    {
        public string Key { get; set; }//返回结果对象的属性
        public string ValueFromType { get; set; }//值来源,action:动作的返回结果，fixed：固定值，event:事件参数，property:属性值
        public string FixedValue { get; set; }//固定值
        public string FromTypePath { get; set; }//值来源对象的路径(action可以使用 动作类型名.属性来获取)
    }
}
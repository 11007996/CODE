namespace EAM.Listen.Model.Dto
{
    /// <summary>
    /// 动作调用 参数
    /// </summary>
    public class IotActionInvokeDto
    {
        /// <summary>
        /// 厂区Id
        /// </summary>
        public string FactoryId { get; set; }

        public int DeviceId { get; set; }

        public string TraceId { get; set; }

        public int EventId { get; set; }
        
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }
}
namespace EAM.Listen.Model.Dto
{
    /// <summary>
    /// 事件响应
    /// </summary>
    public class IotRes
    {
        public string ResponseTopic { get; set; }
        public string DataType { get; set; }
        public string Payload { get; set; }
    }
}
namespace EAM.Listen.Model
{
    public class ReceiveData
    {
        public int equipmentId {  get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public int equipmentCode { get; set; }

        /// <summary>
        /// 操作编码
        /// </summary>
        public int operateCode { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int runState { get; set; }

        /// <summary>
        /// 产线编码
        /// </summary>
        public int lineCode { get; set; }

        /// <summary>
        /// 生产数量
        /// </summary>
        public int productCount { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        public int defectCount { get; set; }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int warnState { get; set; }

        /// <summary>
        /// 报警代码
        /// </summary>
        public int warnCode { get; set; }

        /// <summary>
        /// 远程节点（数据来源）
        /// </summary>
        public string ip { get; set; }
    }
}
using SqlSugar;
using System.Text.Json.Serialization;

namespace EAM.Dashboard.Model.Dto
{
    public class ReadyCallScheduledTaskDto
    {
        public int CallTaskId { get; set; }

        public string TaskName { get; set; }

        public string TaskTime { get; set; }

        public int PlayCount {  get; set; }

        /// <summary>
        /// 播放介质
        /// </summary>
        public string PlayMedium { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(ValueToStringConverter))]
        public long? FileId { get; set; }

        public string TextContent { get; set; }


        /// <summary>
        /// 距离执行任务剩余秒数
        /// </summary>
        public int ExecuteWaitSeconds { get; set; }

        /// <summary>
        /// 执行任务具体时间
        /// </summary>
        public DateTime ExecuteTaskTime { get; set; }
    }
}
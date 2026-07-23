using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 设备采集数据
    /// </summary>
    [SugarTable("Iot_Device_Event")]
    public class IotDeviceEvent
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "Device_Id")]
        public int DeviceId { get; set; }

        /// <summary>
        /// 事件标识
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// 事件名称
        /// </summary>
        [SugarColumn(ColumnName = "Event_Name")]
        public string EventName { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        [SugarColumn(ColumnName = "Event_Type")]
        public string EventType { get; set; }

        /// <summary>
        /// 事件参数
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        [SugarColumn(ColumnName = "Resp_Content")]
        public string RespContent { get; set; }

        /// <summary>
        /// 事件追踪ID
        /// </summary>
        [SugarColumn(ColumnName = "Trace_Id")]
        public string TraceId { get; set; }

        /// <summary>
        /// 上报时间
        /// </summary>
        [SugarColumn(ColumnName = "Create_Time")]
        public DateTime CreateTime { get; set; }
    }
}
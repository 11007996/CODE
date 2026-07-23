using EAM.Listen.Api;
using EAM.Listen.Common;
using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.Model;
using EAM.Listen.Model.Constant;
using EAM.Listen.Model.Dto;
using Jint.Native;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace EAM.Listen.DataProcessing
{
    public class IotService
    {
        /// <summary>
        /// 处理订阅消息
        /// </summary>
        /// <param name="arg"></param>
        public static RespMsgDto HandleMsg(IotCommMsgDto arg)
        {
            //分析topic
            // 1.获取到devicekey
            string factoryId = arg.FactoryId;
            string deviceKey = arg.DeviceKey;
            int deviceId = arg.DeviceId;
            string topic = arg.Topic;
            byte[] payload = arg.Payload;
            string content = string.Empty;//解析后的JSON对象字符串
            string function = arg.Function;
            string protocol = arg.Protocol;
            string traceId = string.IsNullOrEmpty(arg.TtraceId) ? Guid.NewGuid().ToString() : arg.TtraceId;
            string logContent = string.Empty;
            Dictionary<string, object> apiResp = null;
            RespMsgDto res = null;
            try
            {
                //1.检查厂区代码
                if (string.IsNullOrEmpty(factoryId))
                {
                    logContent += "厂区代码为空" + Environment.NewLine;
                    goto errorHandle;
                }
                if (Setting.DbConfig.ConfigId != factoryId)
                {
                    logContent += $"厂区代码（{factoryId}）非法" + Environment.NewLine;
                    goto errorHandle;
                }

                //2.检查设备id、key
                if (deviceId <= 0)
                {
                    logContent += "设备Id为空" + Environment.NewLine;
                    goto errorHandle;
                }
                if (string.IsNullOrEmpty(deviceKey))
                {
                    logContent += "设备key为空" + Environment.NewLine;
                    goto errorHandle;
                }
                IotDevice device = IotProductConfig.GetIotDeviceById(deviceId);
                if (device == null)
                {
                    logContent += $"检查设备：未找到有效的设备Id【{deviceId}】" + Environment.NewLine;
                    goto errorHandle;
                }

                // 3.检查产品配置
                IotProductConfigDto pcd = IotProductConfig.GetProductConfig(device.ProductId);
                if (pcd == null)
                {
                    logContent += $"检查产品配置：当前设备所属产品ID:[{device.ProductId}],未找到有效的产品配置" + Environment.NewLine;
                    goto errorHandle;
                }

                // 4.检查topic是否合法
                if (protocol == "mqtt" && !topic.Contains("?parser=default") && !CheckTopic(topic, factoryId, deviceKey, pcd.Topics))
                {
                    logContent += "检查topic：当前Topic不合法" + Environment.NewLine;
                    goto errorHandle;
                }

                // 5.解析数据
                //判断topic是否带有 特殊参数，【?parser=default】，如果有，表示通过脚本代码解析
                if (protocol == "mqtt" && topic.Contains("?parser=default"))
                {//自义定topic，JS解析
                    content = TransformPayload(topic, payload, pcd, out function);
                }
                else if (protocol == "tcp")
                {//自定义tcp协议
                    content = RawDataToProtocol(payload, pcd, out function);
                }
                else
                {//标准物模型json字符串对象
                    content = Encoding.UTF8.GetString(payload);
                }
                logContent += DateTime.Now + " 解析后的内容：" + Environment.NewLine + content + Environment.NewLine;

                //添加日志
                AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据处理, true, logContent);

                // 6. 属性、事件处理
                if (function == "property")
                {// 6.1 属性
                    apiResp = HandleProperty(device, content, pcd);
                }
                else if (function == "event")
                {// 6.2 事件
                    apiResp = HandleEvent(factoryId, device, traceId, content, pcd);
                }

                // 7.返回结果
                if (apiResp != null)
                {
                    res = new RespMsgDto();

                    if (protocol == "mqtt")
                    {
                        if (topic.Contains("?parser=default"))
                        {//自义定topic，将标准数据转为自定义数据
                            res.Payload = ProtocolToRawData(content, apiResp, pcd);
                            res.Content = BitConverter.ToString(res.Payload);
                        }
                        else
                        {
                            res.Content = JsonConvert.SerializeObject(apiResp);
                            res.Payload = Encoding.UTF8.GetBytes(res.Content);
                        }
                    }
                    else if (protocol == "tcp")
                    {
                        res.Payload = ProtocolToRawData(content, apiResp, pcd);
                        res.Content = SoftBasic.ByteToHexString(res.Payload);
                    }
                    else if (protocol == "http")
                    {
                        res.Content = JsonConvert.SerializeObject(apiResp);
                        res.Payload = Encoding.UTF8.GetBytes(res.Content);
                    }

                    return res;
                }
            }
            catch (Exception ex)
            {
                logContent += "处理异常：" + ex.Message + Environment.NewLine;
                goto errorHandle;
            }
            return null;

        //错误日志
        errorHandle:
            AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据处理, false, logContent);
            return null;
        }

        #region 数据解析

        /// <summary>
        /// MQTT协议，自定义topic解析
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="payload"></param>
        /// <param name="pcd"></param>
        /// <returns></returns>
        public static string TransformPayload(string topic, byte[] payload, IotProductConfigDto pcd, out string function)
        {
            function = null;
            if (pcd.JsEngine == null)
                return null;
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                JsValue jsValue = pcd.JsEngine.Invoke("transformPayload", topic, payload);
                if (jsValue != null)
                {
                    function = jsValue.Get("function").ToString();
                    ExpandoObject obj = (ExpandoObject)jsValue.ToObject();
                    foreach (var item in obj)
                    {
                        dict.Add(item.Key, item.Value);
                    }
                }

                return JsonConvert.SerializeObject(dict);
            }
            catch (Exception ex)
            {
                throw new Exception("js解析失败,方法[transformPayload],异常:" + ex.Message);
            }
        }

        /// <summary>
        /// 自定义协议解析：字节转平台标准格式
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="pcd"></param>
        /// <returns></returns>
        private static string RawDataToProtocol(byte[] payload, IotProductConfigDto pcd, out string function)
        {
            function = null;
            if (pcd.JsEngine == null)
                return null;
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                JsValue jsValue = pcd.JsEngine.Invoke("rawDataToProtocol", payload);
                if (jsValue != null)
                {
                    function = jsValue.Get("function").ToString();
                    ExpandoObject obj = (ExpandoObject)jsValue.ToObject();
                    foreach (var item in obj)
                    {
                        dict.Add(item.Key, item.Value);
                    }
                }

                return JsonConvert.SerializeObject(dict);
            }
            catch (Exception ex)
            {
                throw new Exception("js解析失败,方法[rawDataToProtocol],异常:" + ex.Message);
            }
        }

        /// <summary>
        /// 自定义解析：平台数据转字节
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="pcd"></param>
        /// <returns></returns>
        private static byte[] ProtocolToRawData(string content, Dictionary<string, object> actionResult, IotProductConfigDto pcd)
        {
            if (pcd.JsEngine == null)
                return null;
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>(actionResult);
                JToken reqJT = JToken.Parse(content);
                //遍历属性
                var props = reqJT.Values();
                foreach (var prop in props)
                {
                    Console.WriteLine(prop.ToString());
                    dict.Add(prop.Path, prop.Value<object>());
                }

                JsValue jsValue = pcd.JsEngine.Invoke("protocolToRawData", dict);
                JsArray jsArray = (JsArray)jsValue;
                if (jsArray != null)
                {
                    byte[] data = new byte[jsArray.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = byte.Parse(jsArray[i].ToString());
                    }
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("js解析失败,方法[protocolToRawData],异常:" + ex.Message);
            }
        }

        #endregion 数据解析

        #region 设备最后状态
        /// <summary>
        /// 更新设备通信状态
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="ip"></param>
        /// <param name="status">true:在线，false:离线</param>
        public static void UpdateDeviceStatus(int deviceId, string ip, bool status = true)
        {
            IotDevice device = IotProductConfig.GetIotDeviceById(deviceId);
            if (device == null) return;
            device.Status = status ? IotDeviceStatusConstant.在线 : IotDeviceStatusConstant.离线;
            device.LastOnlineTime = DateTime.Now;
            device.IpAddress = ip;
            if (device.ActivateTime == null)
            {
                device.ActivateTime = DateTime.Now;
            }
            SqlSugarUtil.Conn().Updateable(device)
                .UpdateColumns(it => new { it.Status, it.LastOnlineTime, it.ActivateTime, it.IpAddress })
                .ExecuteCommand();

            //检查是否有绑定,有绑定，更新绑定对象的状态
            IotDeviceBind bind = IotProductConfig.GetIotDeviceBindById(deviceId);
            if (bind != null)
            {
                if (bind.EquipmentId > 0)
                {
                    SqlSugarUtil.Conn().Updateable<EquipmentExtend>()
                        .SetColumns(it => it.Ip == ip)
                        .SetColumns(it => it.IsOnline == status)
                        .SetColumns(it => it.LastOnlineTime == DateTime.Now)
                        .Where(it => it.EquipmentId == bind.EquipmentId)
                        .ExecuteCommand();
                }
                if (bind.BoxId > 0)
                {
                    SqlSugarUtil.Conn().Updateable<CallBoxBase>()
                        .SetColumns(it => it.Ip == ip)
                        .SetColumns(it => it.IsOnline == status)
                        .SetColumns(it => it.LastOnlineTime == DateTime.Now)
                        .Where(it => it.BoxId == bind.BoxId)
                        .ExecuteCommand();
                }
            }
        }
        #endregion 设备最后状态

        #region 属性上报

        /// <summary>
        /// 处理属性上报
        /// </summary>
        /// <param name="device"></param>
        /// <param name="content"></param>
        /// <param name="pcd"></param>
        public static Dictionary<string, object> HandleProperty(IotDevice device, string content, IotProductConfigDto pcd)
        {
            //匹配数据
            List<IotProductThingProperty> propertys = pcd.Propertys;
            if (propertys != null && propertys.Count > 0)
            {
                JToken jt = JToken.Parse(content);
                if (jt != null)
                {
                    List<IotDeviceData> datas = new List<IotDeviceData>();
                    IotDeviceData temData;

                    foreach (IotProductThingProperty prop in propertys)
                    {
                        if (jt[prop.Identifier] != null)
                        {//匹配到对应属性值
                            temData = CheckPropertyValue(prop, device.DeviceId, jt[prop.Identifier]);
                            if (temData != null)
                                datas.Add(temData);
                        }
                    }

                    if (datas.Count > 0)
                    {
                        SqlSugarUtil.Conn().Insertable(datas).ExecuteCommand();
                    }

                    //历史问题，检查设备是否有绑定EquipmentId,如果有，检查属性，并插入对应的表
                    IotDeviceBind bind = IotProductConfig.GetIotDeviceBindById(device.DeviceId);
                    if (bind != null && bind.EquipmentId > 0)
                    {
                        UploadEquipmentRunData(bind.EquipmentId.Value, jt);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 更新设备运行数据
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="jt"></param>
        public static void UploadEquipmentRunData(int equipmentId, JToken jt)
        {
            try
            {
                if (FactoryBaseConfig.SignalConfig.UploadDataFlag)
                {
                    int? runState = jt["runState"].Value<int?>();
                    int? productCount = jt["productCount"].Value<int?>();
                    int? defectCount = jt["defectCount"].Value<int?>();
                    int? warnState = jt["warnState"].Value<int?>();
                    int? warnCode = jt["warnCode"].Value<int?>();

                    if (runState != null&& productCount != null)
                    {
                        EquipmentRuningRecord record = new EquipmentRuningRecord()
                        {
                            EquipmentId = equipmentId,
                            RunState = runState,
                            ProductCount = productCount,
                            DefectCount = defectCount??0,
                            WarnState = warnState??0,
                            WarnCode = warnCode ?? 0,
                            CreateTime = DateTime.Now,
                        };
                        SqlSugarUtil.Conn().Insertable(record).ExecuteCommand();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 检查属性值
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="deviceId"></param>
        /// <param name="jtValue"></param>
        /// <returns></returns>
        protected static IotDeviceData CheckPropertyValue(IotProductThingProperty prop, int deviceId, JToken jtValue)
        {
            IotDeviceData temData = new IotDeviceData()
            {
                DeviceId = deviceId,
                Identifier = prop.Identifier,
                CollectTime = DateTime.Now,
                RawValue = jtValue.ToString(),
                Quality = (byte)IotDataQualityEnum.Good
            };

            //类型验证
            try
            {
                switch (prop.DataType)
                {
                    case IotDataTypeConstant.整数:
                    case IotDataTypeConstant.枚举:
                        if (int.TryParse(temData.RawValue, out int val))
                            temData.Value = val;
                        else
                            temData.Quality = (byte)IotDataQualityEnum.Bad;
                        break;

                    case IotDataTypeConstant.浮点:
                        if (float.TryParse(temData.RawValue, out float val2))
                            temData.Value = (decimal)val2;
                        else
                            temData.Quality = (byte)IotDataQualityEnum.Bad;
                        break;

                    case IotDataTypeConstant.双精度:
                        if (decimal.TryParse(temData.RawValue, out decimal val3))
                            temData.Value = val3;
                        else
                            temData.Quality = (byte)IotDataQualityEnum.Bad;
                        break;

                    case IotDataTypeConstant.布尔://0:false,1:true
                        if (int.TryParse(temData.RawValue, out int val4) && val4 == 0 || val4 == 1)
                            temData.Value = val4;
                        else
                            temData.Quality = (byte)IotDataQualityEnum.Bad;
                        break;

                    case IotDataTypeConstant.文本:
                        break;

                    case IotDataTypeConstant.日期:
                        if (DateTime.TryParse(temData.RawValue, out DateTime val5))
                            temData.RawValue = val5.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        else
                            temData.Quality = (byte)IotDataQualityEnum.Bad;
                        break;

                    case IotDataTypeConstant.结构:
                        temData.RawValue = JsonConvert.SerializeObject(jtValue, Formatting.Indented);
                        break;

                    case IotDataTypeConstant.数组:
                        temData.RawValue = JsonConvert.SerializeObject(jtValue, Formatting.Indented);
                        break;
                }
                ;
            }
            catch (Exception)
            {
                temData.Quality = (byte)IotDataQualityEnum.Bad;
            }

            return temData;
        }

        #endregion 属性上报

        #region 事件处理

        /// <summary>
        /// 处理事件上报
        /// </summary>
        /// <param name="factoryId"></param>
        /// <param name="device"></param>
        /// <param name="traceId"></param>
        /// <param name="content"></param>
        /// <param name="pcd"></param>
        /// <returns></returns>
        public static Dictionary<string, object> HandleEvent(string factoryId, IotDevice device, string traceId, string content, IotProductConfigDto pcd)
        {
            Dictionary<string, object> res = null;
            IotProductThingEvent thingEvent = ParseEventData(content, pcd.Events);
            if (thingEvent == null)
                return null;

            List<IotProductEventAction> actions = pcd.Actions.Where(it => it.EventId == thingEvent.EventId).ToList();
            if (actions != null && actions.Count > 0)
            {
                //调用EAM平台事件处理
                IotActionInvokeDto apiParam = new IotActionInvokeDto()
                {
                    FactoryId = factoryId,
                    DeviceId = device.DeviceId,
                    TraceId = traceId,
                    EventId = thingEvent.EventId,
                    Data = content
                };
                int timeout = pcd.Actions.Sum(it => it.Timeout);
                timeout = timeout <= 0 ? 10 : timeout;
                //发送请求
                res = EAMAPI.IotActionInvoke(apiParam, timeout);
            }

            IotDeviceEvent ide = new IotDeviceEvent()
            {
                DeviceId = device.DeviceId,
                Identifier = thingEvent.Identifier,
                EventName = thingEvent.EventName,
                EventType = thingEvent.EventType,
                Params = content,
                RespContent = res != null ? JsonConvert.SerializeObject(res) : null,
                TraceId = traceId,
                CreateTime = DateTime.Now,
            };
            SqlSugarUtil.Conn().Insertable(ide).ExecuteCommand();

            return res;
        }

        /// <summary>
        /// 事件上报标准处理
        /// </summary>
        /// <param name="content"></param>
        /// <param name="events"></param>
        /// <returns>事件ID</returns>
        protected static IotProductThingEvent ParseEventData(string content, List<IotProductThingEvent> events)
        {
            JToken jt = JToken.Parse(content);
            //获取事件标识
            string eventIdentifier = jt["eventIdentifier"].ToString();
            JToken jtParas = jt["params"].ToString();
            //根据事件标识获取到事件参数
            IotProductThingEvent pte = events.Where(it => it.Identifier == eventIdentifier).FirstOrDefault();

            if (pte == null)
                return null;

            //获取事件参数
            if (pte.Params != null && pte.Params.Count > 0)
            {
                //检查事件参数
                foreach (IotProductParamDefine parmd in pte.Params)
                {
                    //jtParas.co;
                }
            }

            return pte;
        }

        #endregion 事件处理

        #region 日志写入

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="traceId"></param>
        /// <param name="bussinessType"></param>
        /// <param name="operation"></param>
        /// <param name="status"></param>
        /// <param name="content"></param>
        public static void AddLog(int deviceId, string traceId, string bussinessType, string operation, bool status, string content)
        {
            try
            {
                IotDeviceLog log = new IotDeviceLog()
                {
                    DeviceId = deviceId,
                    TraceId = traceId,
                    BusinessType = bussinessType,
                    Operation = operation,
                    Content = content,
                    Status = status ? "S" : "F",
                    CreateTime = DateTime.Now,
                };
                SqlSugarUtil.Conn().Insertable(log).ExecuteCommand();
            }
            catch (Exception)
            {
            }
        }

        #endregion 日志写入

        #region 检查

        /// <summary>
        /// 检查当前topic是否合法
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="topicConfigs"></param>
        /// <returns></returns>
        private static bool CheckTopic(string topic, string factoryId, string deviceKey, List<IotProductTopic> topicConfigs)
        {
            string topicTemplate = topic.Replace(factoryId, "${factoryId}").Replace(deviceKey, "${deviceKey}");
            foreach (IotProductTopic productTopic in topicConfigs)
            {
                if (topicTemplate.StartsWith(productTopic.TopicFormat))
                    return true;
            }
            return false;
        }

        #endregion 检查
    }

    #region 消息对象DTO

    /// <summary>
    /// 消息接收对象
    /// </summary>
    public class IotCommMsgDto
    {
        /// <summary>
        /// 厂区ID
        /// </summary>
        public string FactoryId { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// 设备Key
        /// </summary>
        public string DeviceKey { get; set; }

        /// <summary>
        /// 网络通信协议(TCP,HTTP,MQTT)
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// 主题(mqtt协议必填)
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 上传数据
        /// </summary>
        public byte[] Payload { get; set; }

        /// <summary>
        /// 功能（属性，事件，服务）,tcp自定义协议可以不传
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// 方法(post,get)
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 日志追踪ID
        /// </summary>
        public string TtraceId { get; set; }

        public string FromIP { get; set; }
    }

    /// <summary>
    /// 消息返回对象
    /// </summary>
    public class RespMsgDto
    {
        /// <summary>
        /// 返回的数据内容(如果是字节编码，返回16进制字符串)
        /// </summary>
        public string Content { get; set; }

        public byte[] Payload { get; set; }
    }

    #endregion 消息对象DTO
}
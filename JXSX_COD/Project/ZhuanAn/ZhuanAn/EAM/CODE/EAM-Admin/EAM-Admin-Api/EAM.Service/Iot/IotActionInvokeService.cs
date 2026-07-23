using EAM.Model.Basic;
using EAM.Model.Call;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.Equipment;
using EAM.Model.Iot;
using EAM.Model.System;
using EAM.Service.Call.ICallService;
using EAM.Service.Iot.IIotService;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using NPOI.Util;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EAM.Service.Iot
{
    /// <summary>
    /// 动作调用 Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotActionInvokeService), ServiceLifetime = LifeTime.Transient)]
    public class IotActionInvokeService : BaseService<IotProductEventAction>, IIotActionInvokeService
    {
        private IWxMessageService _wxMessageService;
        private ICallBoxOperateService _callBoxOperateService;

        public IotActionInvokeService(IHttpContextAccessor httpContextAccessor,
            IWxMessageService wxMessageService,
            ICallBoxOperateService callBoxOperateService)
            : base(httpContextAccessor)
        {
            _wxMessageService = wxMessageService;
            _callBoxOperateService = callBoxOperateService;
        }

        public Dictionary<string, object> ExeIotActionInvoke(IotActionInvokeDto parm)
        {
            Dictionary<string, object> responseData = null;
            string logContent = string.Empty;
            bool exeStatus = true;
            try
            {
                //参数检查
                IotProductThingEvent thingEvent = Context.Queryable<IotProductThingEvent>().Where(it => it.EventId == parm.EventId).First();
                List<IotProductEventAction> actions = Context.Queryable<IotProductEventAction>().Where(it => it.EventId == parm.EventId && it.Enabled == true).OrderBy(it => it.SortOrder, OrderByType.Asc).ToList();

                if (thingEvent == null)
                    throw new CustomException("未找到事件Id");
                if (!thingEvent.Enabled)
                    throw new CustomException("事件已禁用");
                if (actions == null || actions.Count <= 0)
                    throw new CustomException("未找到事件无动作配置");

                //JSON数据
                JToken jsonTk = JToken.Parse(parm.Data);
                //将
                Dictionary<string, object> actionResultDict = new Dictionary<string, object>();
                object actionR = null;//每个动作的临时结果
                //循环事件执行动作
                foreach (var action in actions)
                {
                    try
                    {
                        actionR = null;
                        if (action.ActionType == IotEventActionTypeConstant.企业微信通知)
                        {
                            actionR = SendWxMsg(parm.DeviceId, action.ActionConfig, jsonTk["params"]);
                            actionResultDict.Add(action.ActionType, actionR);
                        }
                        else if (action.ActionType == IotEventActionTypeConstant.检查设备保养状态)
                        {
                            actionR = CheckMaintainStatus(parm.DeviceId, action.ActionConfig);
                            actionResultDict.Add(action.ActionType, actionR);
                        }
                        else if (action.ActionType == IotEventActionTypeConstant.同步产线)
                        {
                            actionR = SyncEquipmentExtendForLine(parm.DeviceId, action.ActionConfig, jsonTk["params"]);
                            actionResultDict.Add(action.ActionType, actionR);
                        }
                        else if (action.ActionType == IotEventActionTypeConstant.添加呼叫盒子操作)
                        {
                            actionR = AddCallBoxOperateRecored(parm.DeviceId, action.ActionConfig, jsonTk["params"]);
                            actionResultDict.Add(action.ActionType, actionR);
                        }
                        else if (action.ActionType == IotEventActionTypeConstant.响应数据)
                        {//返回结果给设备
                            responseData = ResponseData(actionResultDict, action.ActionConfig);
                            actionR = responseData;
                        }

                        if (actionR != null)
                        {
                            logContent += Environment.NewLine + DateTime.Now + " | 动作名称:" + action.ActionName + " | 动作ID:" + action.ActionId + " | 动作结果:" + Environment.NewLine + JsonConvert.SerializeObject(actionR);
                        }
                    }
                    catch (Exception ex)
                    {
                        logContent += Environment.NewLine + DateTime.Now + " | " + action.ActionName + " | " + action.ActionId + Environment.NewLine + ex.Message;
                        exeStatus = false;
                    }
                }
            }
            catch (Exception ex)
            {
                exeStatus = false;
                logContent += Environment.NewLine + DateTime.Now + " | 异常信息：" + Environment.NewLine + ex.Message;
                throw new CustomException("出现异常请查看日志");
            }
            finally
            {
                AddLog(parm, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.事件处理, exeStatus, logContent);
            }

            return responseData;
        }

        /// <summary>
        /// 发送企业微信
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="actionConfig"></param>
        /// <param name="jsonTk"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private WxMessage SendWxMsg(int deviceId, string actionConfig, JToken jsonTk)
        {
            //配置
            ActionConfigForSendWxMsgDto config = JsonConvert.DeserializeObject<ActionConfigForSendWxMsgDto>(actionConfig);
            //追加设备信息
            IotDevice dev = Context.Queryable<IotDevice>().Where(it => it.DeviceId == deviceId).First();
            //如果消息模板里包含 ${iotdevice. ，则将iot设备的信息嵌入到json对象中
            if (config.WxMsgTemplate.Contains("${iotdevice."))
                jsonTk["iotdevice"] = JObject.FromObject(dev);

            //模板内容替换
            string content = ReplaceContent(config.WxMsgTemplate, jsonTk);

            if (!string.IsNullOrEmpty(config.WxChatId))
                return _wxMessageService.SendWxChatMessage(config.WxChatId, content);
            else
                return _wxMessageService.SendWxEmpMessage(config.EmpCodes, content);
        }

        /// <summary>
        /// 检查设备保养状态
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="actionConfig"></param>
        /// <returns></returns>
        private bool CheckMaintainStatus(int deviceId, string actionConfig)
        {
            ActionConfigForCheckMaintainStatusDto config = JsonConvert.DeserializeObject<ActionConfigForCheckMaintainStatusDto>(actionConfig);

            //时间标记计算
            DateTime now = DateTime.Now;
            //如果小于指定开始检查小时，则表示前一天
            if (now.Hour < config.DaySeparation)
                now.AddDays(-1);
            int Year = now.Year;
            int DayStamp = now.DayOfYear;
            GregorianCalendar gregorianCalendar = new GregorianCalendar();
            int WeekStamp = gregorianCalendar.GetWeekOfYear(now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//当天所属周
            int MonthStamp = now.Month;

            //获取关联的资产设备信息
            EquipmentBase equ = Context.Queryable<EquipmentBase>().RightJoin<IotDeviceBind>((e, d) => e.EquipmentId == d.EquipmentId).Where((e, d) => d.DeviceId == deviceId).First();
            if (equ == null)
                throw new CustomException("当前iot设备未关联资产设备");

            //查询保养结果
            string[] dateMarks = config.DateMarks.Split(',');

            bool result = true;
            //日保养检查
            if (dateMarks.Contains(DateMarkConstant.日))
                result = Context.Queryable<MaintainRecord>().Where(it => it.EquipmentId == equ.EquipmentId && it.Year == Year && it.DateMark == DateMarkConstant.日 && it.DateMarkStamp == DayStamp).Any();
            if (result && dateMarks.Contains(DateMarkConstant.周))
                result = Context.Queryable<MaintainRecord>().Where(it => it.EquipmentId == equ.EquipmentId && it.Year == Year && it.DateMark == DateMarkConstant.周 && it.DateMarkStamp == WeekStamp).Any();

            return result;
        }

        /// <summary>
        /// 返回动作结果
        /// </summary>
        /// <param name="actionResultDict"></param>
        /// <param name="actionConfig"></param>
        /// <returns></returns>
        private Dictionary<string, object> ResponseData(Dictionary<string, object> actionResultDict, string actionConfig)
        {
            List<ActionConfigForResponseDataItemDto> config = JsonConvert.DeserializeObject<List<ActionConfigForResponseDataItemDto>>(actionConfig);

            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (var item in config)
            {
                if (item.ValueFromType == "fixed")
                {//属性值为固定值
                    result.Add(item.Key, item.FixedValue);
                }
                else if (item.ValueFromType == "action")
                {
                    if (string.IsNullOrEmpty(item.FromTypePath))
                        return null;
                    string[] action = item.FromTypePath.Split('.');//0:动作类型值，1:动作结果的具体属性的值
                    if (action.Length == 1)
                    {//指定动作返回值为基础类型
                        result.Add(item.Key, actionResultDict[action[0]]);
                    }
                    else if (action.Length == 2)
                    {//指定动作返回值为对象类型
                        JToken token = JObject.Parse(JsonConvert.SerializeObject(actionResultDict[action[0]]));
                        result.Add(item.Key, token[action[1]]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 添加呼叫盒的操作记录
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="actionConfig"></param>
        /// <param name="jsonTk"></param>
        /// <returns></returns>
        private bool AddCallBoxOperateRecored(int deviceId, string actionConfig, JToken jsonTk)
        {
            ActionConfigForAddCallBoxOperateDto config = JsonConvert.DeserializeObject<ActionConfigForAddCallBoxOperateDto>(actionConfig);
            //判断是否有绑定呼叫盒
            IotDeviceBind idb = Context.Queryable<IotDeviceBind>().Where(it => it.DeviceId == deviceId).First();
            if (idb != null && idb.BoxId > 0)
            {
                string iotOperateType = jsonTk[config.OperateTypeIdentifier].ToString();
                string userName = string.IsNullOrEmpty(config.UsernameIdentifier) ? null : jsonTk[config.UsernameIdentifier]?.ToString();//是否有操作人工号
                int operateType = (int)CallBoxOperateTypeEnum.取消呼叫;
                //检查是否有【操作类型】值的映射转换
                if (config.OperateTypeMapping != null && config.OperateTypeMapping.Count > 0)
                {
                    operateType = config.OperateTypeMapping.Where(it => it.Key == iotOperateType).Select(it => it.Value).FirstOrDefault();
                }
                else
                {
                    operateType = Convert.ToInt32(iotOperateType);
                }
                if (string.IsNullOrWhiteSpace(userName))
                {
                    userName = GlobalConstant.System;
                }

                CallBoxOperate parm = new CallBoxOperate()
                {
                    BoxId = idb.BoxId.Value,
                    OperateType = operateType,
                    CreateBy = userName,
                    CreateTime = DateTime.Now,
                };
                _callBoxOperateService.AddCallBoxOperate(parm);
                return true;
            }
            else
            {
                throw new CustomException("当前iot设备未关联呼叫盒");
            }
        }

        /// <summary>
        /// 同步设备扩展中的产线
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="actionConfig"></param>
        /// <param name="jsonTk"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private bool SyncEquipmentExtendForLine(int deviceId, string actionConfig, JToken jsonTk)
        {
            ActionConfigForSyncLineDto config = JsonConvert.DeserializeObject<ActionConfigForSyncLineDto>(actionConfig);

            //获取关联的资产设备信息
            EquipmentBase equ = Context.Queryable<EquipmentBase>().RightJoin<IotDeviceBind>((e, d) => e.EquipmentId == d.EquipmentId).Where((e, d) => d.DeviceId == deviceId).First();
            if (equ == null)
                throw new CustomException("当前iot设备未关联资产设备");

            //获取设备扩展信息
            EquipmentExtend extend = Context.Queryable<EquipmentExtend>().Where(it => it.EquipmentId == equ.EquipmentId).First();
            if (extend == null)
                throw new CustomException("设备未配置扩展信息");

            string paramValue = jsonTk[config.ParamIdentifier].ToString();
            if (string.IsNullOrEmpty(paramValue))
                throw new CustomException($"事件参数没有传递{config.ParamIdentifier}属性值");

            int LineId = 0;
            //查询产线
            if (config.ParamValueType == "lineCode")
            {//产线编码
                LineId = Context.Queryable<Line>().Where(it => it.LineCode == int.Parse(paramValue)).Select(it => it.LineId).First();
            }
            else if (config.ParamValueType == "lineName")
            {//产线名称
                LineId = Context.Queryable<Line>().Where(it => it.LineName == paramValue).Select(it => it.LineId).First();
            }
            else
            {//默认产线ID
                LineId = Context.Queryable<Line>().Where(it => it.LineId == int.Parse(paramValue)).Select(it => it.LineId).First();
            }

            if (LineId > 0)
                extend.LineId = LineId;

            return Context.Updateable(extend).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="bussinessType"></param>
        /// <param name="operation"></param>
        /// <param name="status"></param>
        /// <param name="content"></param>
        private void AddLog(IotActionInvokeDto parm, string bussinessType, string operation, bool status, string content)
        {
            IotDeviceLog log = new IotDeviceLog()
            {
                DeviceId = parm.DeviceId,
                TraceId = parm.TraceId,
                BusinessType = bussinessType,
                Operation = operation,
                Content = content,
                Status = status ? "S" : "F",
                CreateTime = DateTime.Now,
            };
            Context.Insertable(log).ExecuteCommand();
        }

        /// <summary>
        /// 替换所有${xxx}的内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="jToken"></param>
        private string ReplaceContent(string text, JToken jToken)
        {
            // 使用正则表达式匹配 ${...}
            string result = Regex.Replace(text, @"\$\{([^}]+)\}", match =>
            {
                string key = match.Groups[1].Value; // 提取花括号内的内容
                //判断是否有子key,
                if (key.Contains('.'))
                {
                    string[] keyarr = key.Split('.').ToArray();
                    JToken tempTK = jToken.Copy();
                    object lastVal = string.Empty;
                    foreach (string k in keyarr)
                    {
                        if (tempTK != null)
                            tempTK = tempTK[k];
                    }
                    return tempTK != null ? tempTK.ToString() : match.Value;
                }
                else
                    return jToken[key] != null ? jToken[key].ToString() : match.Value; // 如果找不到就保留原样
            });
            return result;
        }
    }
}
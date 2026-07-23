using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Repository;
using EAM.Service.Iot.IIotService;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace EAM.Service.Iot
{
    /// <summary>
    /// 产品事件处理动作Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotProductEventActionService), ServiceLifetime = LifeTime.Transient)]
    public class IotProductEventActionService : BaseService<IotProductEventAction>, IIotProductEventActionService
    {
        private readonly IWxChatGroupService _wxChatGroupService;

        public IotProductEventActionService(IHttpContextAccessor contextAccessor, IWxChatGroupService wxChatGroupService) : base(contextAccessor)
        {
            _wxChatGroupService = wxChatGroupService;
        }

        /// <summary>
        /// 查询产品事件处理动作列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotProductEventActionDto> GetList(IotProductEventActionQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<IotProductEventAction, IotProductEventActionDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ActionId"></param>
        /// <returns></returns>
        public IotProductEventAction GetInfo(int ActionId)
        {
            var response = Queryable()
                .Where(x => x.ActionId == ActionId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产品事件处理动作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotProductEventAction AddIotProductEventAction(IotProductEventAction model)
        {
            CheckData(model);

            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产品事件处理动作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotProductEventAction(IotProductEventAction model)
        {
            CheckData(model);

            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotProductEventAction> QueryExp(IotProductEventActionQueryDto parm)
        {
            var predicate = Expressionable.Create<IotProductEventAction>();

            predicate.AndIF(parm.EventId > 0, it => it.EventId == parm.EventId);

            return predicate;
        }

        private static void CheckNamesRegex(string text, string key)
        {
            if (string.IsNullOrEmpty(text))
                throw new CustomException($"{key}不能为空");
            string[] keyArr = new string[] { "set", "get", "post", "property", "event", "service", "value", "time" };
            if (keyArr.Contains(text.ToLower()))
                throw new CustomException($"{key}不能为特殊值");
            if (!Regex.IsMatch(text, "^[a-zA-Z0-9_]+$"))
                throw new CustomException($"{key}只能是字母数字下划线组成");
        }

        /// <summary>
        /// 检查数据
        /// </summary>
        /// <param name="model"></param>
        private void CheckData(IotProductEventAction model)
        {
            if (model.ActionType == IotEventActionTypeConstant.添加呼叫盒子操作)
            {
                model.ActionConfig = CheckDataForCallBoxOperate(model.ActionConfig);
            }
            else if (model.ActionType == IotEventActionTypeConstant.同步产线)
            {
                model.ActionConfig = CheckDataForSyncLine(model.ActionConfig);
            }
            else if (model.ActionType == IotEventActionTypeConstant.企业微信通知)
            {
                model.ActionConfig = CheckDataForSendWxMsg(model.ActionConfig);
            }
            else if (model.ActionType == IotEventActionTypeConstant.检查设备保养状态)
            {
                model.ActionConfig = CheckDataForCheckMaintainStatus(model.ActionConfig);
            }
            else if (model.ActionType == IotEventActionTypeConstant.响应数据)
            {
                model.ActionConfig = CheckDataForResponseData(model.ActionConfig, model.EventId);
            }
        }

        /// <summary>
        /// 检查【添加呼叫盒子操作】动作配置
        /// </summary>
        /// <param name="actionConfig"></param>
        private string CheckDataForCallBoxOperate(string actionConfig)
        {
            if (string.IsNullOrEmpty(actionConfig))
                throw new CustomException("动作配置不能为空");
            ActionConfigForAddCallBoxOperateDto config = null;
            try
            {
                config = JsonConvert.DeserializeObject<ActionConfigForAddCallBoxOperateDto>(actionConfig);
            }
            catch (Exception)
            {
                throw new CustomException("动作配置格式解析异常");
            }

            //检查通知对象
            if (string.IsNullOrEmpty(config.OperateTypeIdentifier))
                throw new CustomException("【操作类型参数标识】不能为空");

            return JsonConvert.SerializeObject(config);
        }

        /// <summary>
        /// 检查【同步产线】动作配置
        /// </summary>
        /// <param name="actionConfig"></param>
        private string CheckDataForSyncLine(string actionConfig)
        {
            if (string.IsNullOrEmpty(actionConfig))
                throw new CustomException("动作配置不能为空");
            ActionConfigForSyncLineDto config = null;
            try
            {
                config = JsonConvert.DeserializeObject<ActionConfigForSyncLineDto>(actionConfig);
            }
            catch (Exception)
            {
                throw new CustomException("动作配置格式解析异常");
            }

            //检查通知对象
            if (string.IsNullOrEmpty(config.ParamIdentifier))
                throw new CustomException("【参数标识】不能为空");
            if (string.IsNullOrEmpty(config.ParamValueType))
                throw new CustomException("【参数类型】不能为空");
            string[] types = new string[] { "lineCode", "lineId", "lineName" };
            if (!types.Contains(config.ParamValueType))
                throw new CustomException($"【参数类型】错误，值枚举：{String.Join(',', types)}");

            return JsonConvert.SerializeObject(config);
        }

        /// <summary>
        /// 检查【发送微信消息】动作配置
        /// </summary>
        /// <param name="actionConfig"></param>
        private string CheckDataForSendWxMsg(string actionConfig)
        {
            if (string.IsNullOrEmpty(actionConfig))
                throw new CustomException("动作配置不能为空");
            ActionConfigForSendWxMsgDto config = null;
            try
            {
                config = JsonConvert.DeserializeObject<ActionConfigForSendWxMsgDto>(actionConfig);
            }
            catch (Exception)
            {
                throw new CustomException("动作配置格式解析异常");
            }

            //检查通知对象
            if (string.IsNullOrEmpty(config.WxChatId) && string.IsNullOrEmpty(config.EmpCodes))
                throw new CustomException("通知对象【微信群】与【员工】必需填写一项");
            if (!string.IsNullOrEmpty(config.WxChatId) && !string.IsNullOrEmpty(config.EmpCodes))
                throw new CustomException("通知对象【微信群】与【员工】只需要填写一项");

            if (!string.IsNullOrEmpty(config.WxChatId))
            {//检查微信群
                string factoryId = Context.CurrentConnectionConfig.ConfigId.ToString();
                var wx = _wxChatGroupService.Queryable().Where(it => it.ChatId == config.WxChatId && it.FactoryId == factoryId).First();
                if (wx == null)
                    throw new CustomException("微信群ID不存在");
            }
            else
            {//检查员工
                List<string> empcodes = config.EmpCodes.Split(',').ToList();
                if (empcodes.Count != empcodes.Distinct().Count())
                    throw new CustomException("通知员工存在相同的工号");
                int count = Context.Queryable<Employee>().Where(it => empcodes.Contains(it.EmpCode)).Count();
                if (count != empcodes.Count())
                    throw new CustomException("存在无效的员工工号");
            }

            return JsonConvert.SerializeObject(config);
        }

        /// <summary>
        /// 检查【检查保养状态】动作配置
        /// </summary>
        /// <param name="actionConfig"></param>
        /// <returns></returns>
        private string CheckDataForCheckMaintainStatus(string actionConfig)
        {
            if (string.IsNullOrEmpty(actionConfig))
                throw new CustomException("动作配置不能为空");
            ActionConfigForCheckMaintainStatusDto config = null;
            try
            {
                config = JsonConvert.DeserializeObject<ActionConfigForCheckMaintainStatusDto>(actionConfig);
            }
            catch (Exception)
            {
                throw new CustomException("动作配置格式解析异常");
            }

            if (config.DaySeparation < 0 || config.DaySeparation > 24)
                throw new CustomException("【日分隔】的值范围不能超出0~24");

            if (string.IsNullOrEmpty(config.DateMarks))
                throw new CustomException("【日期标识】不能为空");

            string[] dateMarks = config.DateMarks.Split(",");
            string[] marks = { DateMarkConstant.日, DateMarkConstant.周, DateMarkConstant.月, DateMarkConstant.季, DateMarkConstant.年 };
            // 判断日期标记是否合法，是否子集
            if (!dateMarks.ToHashSet().IsSubsetOf(marks))
                throw new CustomException("【日期标识】存在不合法的值");

            return JsonConvert.SerializeObject(config);
        }

        /// <summary>
        /// 检查【返回数据】动作配置
        /// </summary>
        /// <param name="actionConfig"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        private string CheckDataForResponseData(string actionConfig, int eventId)
        {
            if (string.IsNullOrEmpty(actionConfig))
                throw new CustomException("动作配置不能为空");
            List<ActionConfigForResponseDataItemDto> config = null;
            try
            {
                config = JsonConvert.DeserializeObject<List<ActionConfigForResponseDataItemDto>>(actionConfig);
            }
            catch (Exception)
            {
                throw new CustomException("动作配置格式解析异常");
            }

            if (config.Count < 0)
                throw new CustomException("需要最少一条属性配置");

            List<string> actions = Queryable().Where(it => it.EventId == eventId).Select(it => it.ActionType).ToList();

            for (int i = 0; i < config.Count; i++)
            {
                CheckNamesRegex(config[i].Key, $"第{i + 1}行,属性key");
                if (config[i].ValueFromType == "fixed")
                {
                    if (string.IsNullOrEmpty(config[i].FixedValue))
                        throw new CustomException($"第{i + 1}行,固定值不能为空");
                }
                else if (config[i].ValueFromType == "action")
                {
                    string actiontype = config[i].FromTypePath;
                    if (string.IsNullOrEmpty(actiontype))
                        throw new CustomException($"第{i + 1}行,值路径不能为空");
                    if (actions == null || !actions.Contains(actiontype.Split('.')[0]))
                        throw new CustomException($"第{i + 1}行,值路径的动作类型不存在");
                }
                else
                {
                    throw new CustomException($"第{i + 1}行,未知的值来源类型");
                }
            }

            return JsonConvert.SerializeObject(config);
        }
    }
}
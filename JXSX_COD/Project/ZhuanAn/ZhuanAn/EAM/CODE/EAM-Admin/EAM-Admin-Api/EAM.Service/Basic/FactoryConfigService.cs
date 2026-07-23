using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;
using EAM.ServiceCore.Model.Enums;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Basic
{
    /// <summary>
    /// 厂区配置Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IFactoryConfigService), ServiceLifetime = LifeTime.Transient)]
    public class FactoryConfigService : BaseService<FactoryConfig>, IFactoryConfigService
    {
        public FactoryConfigService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询厂区配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FactoryConfigDto> GetList(FactoryConfigQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<FactoryConfig, FactoryConfigDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public FactoryConfig GetInfo(int Id)
        {
            var response = Queryable()
                .Where(x => x.Id == Id)
                .First();

            return response;
        }

        /// <summary>
        /// 根据配置key，获取值，如果没有配置返回默认值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FactoryConfig GetInfoByKey(string key)
        {
            return Queryable().Where(it => it.ConfigKey == key).First();
        }

        /// <summary>
        /// 添加厂区配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FactoryConfig AddFactoryConfig(FactoryConfig model)
        {
            if (!CheckConfigValue(model, out string errorMsg))
            {
                throw new CustomException(errorMsg);
            }

            var config = Queryable().Where(it => it.ConfigKey == model.ConfigKey).First();
            if (config != null)
                throw new CustomException("存在相同的配置项目");

            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改厂区配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateFactoryConfig(FactoryConfig model)
        {
            if (!CheckConfigValue(model, out string errorMsg))
            {
                throw new CustomException(errorMsg);
            }

            var config = Queryable().Where(it => it.ConfigKey == model.ConfigKey && it.Id != model.Id).First();
            if (config != null)
                throw new CustomException("存在相同的配置项目");

            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<FactoryConfig> QueryExp(FactoryConfigQueryDto parm)
        {
            var predicate = Expressionable.Create<FactoryConfig>();

            return predicate;
        }

        /// <summary>
        /// 检查配置值是否合规
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private static bool CheckConfigValue(FactoryConfig model, out string errorMsg)
        {
            bool isEnable = true;
            errorMsg = string.Empty;
            //检查【设备监听数据统计拆分时间】
            if (model.ConfigKey == FactoryConfigKeyConstant.设备监听数据统计拆分时间)
            {
                isEnable = DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd") + " " + model.ConfigValue, out DateTime date);
                if (!isEnable)
                    errorMsg = "配置值格式错误,需要格式【hh:mm:ss】";
            }

            //检查【呼叫接单超时时间】
            if (model.ConfigKey == FactoryConfigKeyConstant.呼叫接单超时时间)
            {
                isEnable = int.TryParse(model.ConfigValue, out int timeout);
                if (!isEnable)
                    errorMsg = "配置值类型错误,需要整数类型";
                else if (timeout <= 0)
                    errorMsg = "配置值范围错误,值需要大于0";
            }

            //检查【呼叫盒广播呼叫开关】
            if (model.ConfigKey == FactoryConfigKeyConstant.呼叫盒广播呼叫开关)
            {
                if (model.ConfigValue != SysYesNoConstant.是 && model.ConfigValue != SysYesNoConstant.否)
                    errorMsg = $"配置值只能为Y或N";
            }

            //检查【保养检查开关】
            if (model.ConfigKey == FactoryConfigKeyConstant.保养检查开关 && !string.IsNullOrEmpty(model.ConfigValue))
            {
                string[] dateMarks = { DateMarkConstant.日, DateMarkConstant.周, DateMarkConstant.月, DateMarkConstant.季, DateMarkConstant.年 };
                string[] vals = model.ConfigValue.Split(',');
                if (vals.Length > 0)
                {
                    foreach (string val in vals)
                    {
                        if (!dateMarks.Contains(val))
                        {
                            errorMsg = $"配置值只能为【{string.Join(",", dateMarks)}】中的值，多个用逗号隔开。";
                            break;
                        }
                    }
                }
            }

            //检查【设备运行数据上传开关】
            if (model.ConfigKey == FactoryConfigKeyConstant.设备运行数据上传开关)
            {
                if (model.ConfigValue != SysYesNoConstant.是 && model.ConfigValue != SysYesNoConstant.否)
                    errorMsg = $"配置值只能为Y或N";
            }

            //检查【设备发送数据编码配置】
            if (model.ConfigKey == FactoryConfigKeyConstant.设备接收数据编码配置 && !string.IsNullOrEmpty(model.ConfigValue))
            {
                //json
                try
                {
                    List<EquipmentDataItemCode> vals = JsonConvert.DeserializeObject<List<EquipmentDataItemCode>>(model.ConfigValue);

                    if (vals.Count > 0)
                    {
                        string[] itemNames =  {
                            ReceiveItemNameConstant.前缀,
                            ReceiveItemNameConstant.后缀 ,
                            ReceiveItemNameConstant.设备编码 ,
                            ReceiveItemNameConstant.操作指令 ,
                            ReceiveItemNameConstant.运行状态 ,
                            ReceiveItemNameConstant.产线编码,
                            ReceiveItemNameConstant.产能数量,
                            ReceiveItemNameConstant.不良数量 ,
                            ReceiveItemNameConstant.报警状态 ,
                            ReceiveItemNameConstant.报警代码 ,
                            ReceiveItemNameConstant.其他
                        };

                        for (int i = 0; i < vals.Count; i++)
                        {
                            EquipmentDataItemCode item = vals[i];
                            if (!itemNames.Contains(item.ItemName))
                            {
                                errorMsg = $"第【{i + 1}】项,内容值错误，配置代码名称(ItemName)只能为【{string.Join(",", itemNames)}】之一";
                                break;
                            }
                            if (item.Sort < 0)
                            {
                                errorMsg = $"第【{i + 1}】项,内容值错误，配置代码序号(Sort)不能小于0";
                                break;
                            }
                            if (item.ByteLen <= 0)
                            {
                                errorMsg = $"第【{i + 1}】项,内容值错误，配置代码长度(ByteLen)不能小于等于0";
                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(errorMsg))
                        {
                            var nameVals = vals.Select(it => it.ItemName).Distinct().ToList();
                            if (nameVals.Count != vals.Count)
                                errorMsg = "内容值错误，配置代码序号(ItemName)存在相同值";

                            var sortVals = vals.Select(it => it.Sort).Distinct().ToList();
                            if (sortVals.Count != vals.Count)
                                errorMsg = "内容值错误，配置代码序号(Sort)存在相同值";
                        }
                    }
                }
                catch (Exception)
                {
                    List<EquipmentDataItemCode> edis = new List<EquipmentDataItemCode>();
                    edis.Add(new EquipmentDataItemCode() { });
                    errorMsg = $"内容格式错误，请使用格式：{JsonConvert.SerializeObject(edis)}";
                }
            }

            //检查【设备发送数据编码配置】
            if (model.ConfigKey == FactoryConfigKeyConstant.设备发送数据编码配置 && !string.IsNullOrEmpty(model.ConfigValue))
            {
                //json
                try
                {
                    List<EquipmentDataItemCode> vals = JsonConvert.DeserializeObject<List<EquipmentDataItemCode>>(model.ConfigValue);

                    if (vals.Count > 0)
                    {
                        string[] itemNames =  {
                            SendItemNameConstant.前缀,
                            SendItemNameConstant.后缀,
                            SendItemNameConstant.设备编码,
                            SendItemNameConstant.操作指令,
                            SendItemNameConstant.操作结果,
                            SendItemNameConstant.其他
                        };

                        for (int i = 0; i < vals.Count; i++)
                        {
                            EquipmentDataItemCode item = vals[i];
                            if (!itemNames.Contains(item.ItemName))
                            {
                                errorMsg = $"第【{i + 1}】项,内容值错误，配置代码名称(ItemName)只能为【{string.Join(",", itemNames)}】之一";
                                break;
                            }
                            if (item.Sort < 0)
                            {
                                errorMsg = $"第【{i + 1}】项,内容值错误，配置代码序号(Sort)不能小于0";
                                break;
                            }
                            if (item.ByteLen <= 0)
                            {
                                errorMsg = $"第【{i + 1}】项,内容值错误，配置代码长度(ByteLen)不能小于等于0";
                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(errorMsg))
                        {
                            var nameVals = vals.Select(it => it.ItemName).Distinct().ToList();
                            if (nameVals.Count != vals.Count)
                                errorMsg = "内容值错误，配置代码序号(ItemName)存在相同值";

                            var sortVals = vals.Select(it => it.Sort).Distinct().ToList();
                            if (sortVals.Count != vals.Count)
                                errorMsg = "内容值错误，配置代码序号(Sort)存在相同值";
                        }
                    }
                }
                catch (Exception)
                {
                    List<EquipmentDataItemCode> edis = new List<EquipmentDataItemCode>();
                    edis.Add(new EquipmentDataItemCode() { });
                    errorMsg = $"内容格式错误，请使用格式：{JsonConvert.SerializeObject(edis)}";
                }
            }

            return errorMsg.Length <= 0;
        }
    }
}
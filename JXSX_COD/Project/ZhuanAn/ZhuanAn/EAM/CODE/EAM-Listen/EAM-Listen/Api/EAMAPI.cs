using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.Model.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EAM.Listen.Api
{
    public class EAMAPI
    {
        /// <summary>
        /// 登入，返回token
        /// </summary>
        /// <param name="factoryId"></param>
        /// <returns></returns>
        public static string Login(string factoryId)
        {
            LoginBodyDto parm = new LoginBodyDto()
            {
                Username = Setting.EAMLoginConfig.Username,
                Password = Setting.EAMLoginConfig.Password,
                FactoryId = factoryId,
                UseOaAccount = false
            };
            string res = HttpHelper.PostJson(Setting.CommonConfig.EAMHost + "login", parm);
            try
            {
                ApiResult apiResp = JsonConvert.DeserializeObject<ApiResult>(res);
                if (apiResp.code == 200 && apiResp.data != null)
                {
                    string token = apiResp.data.ToString();
                    CacheHelper.AddEAMToken(factoryId, parm.Username, token, Setting.EAMLoginConfig.Expire);
                    return token;
                }
                else
                {
                    LogHelper.Error(typeof(EAMAPI), "登入失败,结果：" + Environment.NewLine + res);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(EAMAPI), ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 事件动作接口调用
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Dictionary<string, object> IotActionInvoke(IotActionInvokeDto param, int timeout)
        {
            string token = CacheHelper.GetEAMToken(param.FactoryId, Setting.EAMLoginConfig.Username);
            if (token == null)
            {
                token = Login(param.FactoryId);
            }
            string res = HttpHelper.PostJson(Setting.CommonConfig.EAMHost + "iot/IotActionInvoke", param, token, timeout);
            try
            {
                ApiResult apiResp = JsonConvert.DeserializeObject<ApiResult>(res);
                if (apiResp.code == 200 && apiResp.data != null)
                {
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(apiResp.data));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(EAMAPI), ex.Message);
            }
            return null;
        }
    }
}
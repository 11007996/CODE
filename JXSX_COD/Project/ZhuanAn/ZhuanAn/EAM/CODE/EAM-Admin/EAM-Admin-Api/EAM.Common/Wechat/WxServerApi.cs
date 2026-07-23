using EAM.Common.Wechat.Model;
using Infrastructure;
using Infrastructure.Cache;
using Newtonsoft.Json;
using System;

namespace EAM.Common.Wechat
{
    /// <summary>
    /// 微信服务器端接口封装
    /// </summary>
    public class WxServerApi
    {
        /// <summary>
        /// 获取应用的jsapi_ticket
        ///     应用的jsapi_ticket用于计算agentConfig（参见“通过agentConfig注入应用的权限”）的签名
        ///     正常情况下，应用的jsapi_ticket的有效期为7200秒，通过access_token来获取。
        ///     由于获取jsapi_ticket的api调用次数非常有限（一小时内，每个应用不能超过100次），频繁刷新jsapi_ticket会导致api调用受限，影响自身业务，
        ///     开发者必须在自己的服务全局缓存应用的jsapi_ticket。
        /// </summary>
        /// <returns></returns>
        public static WxTicketDto GetJsApiTicket()
        {
            //请求参数
            // access_token	应用的调用接口凭证，

            //返回参数
            //ticket	    生成签名所需的jsapi_ticket，最长为512字节
            //expires_in	凭证的有效时间（秒）
            string token = GetAccessToken().access_token;
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/ticket/get?access_token={token}&type=agent_config";
            string res = HttpHelper.HttpGet(url);
            WxTicketDto ro = JsonConvert.DeserializeObject<WxTicketDto>(res);
            return ro;
        }

        /// <summary>
        /// 获取访问用户身份
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static WxUserAuthDto GetAuthUserId(string code)
        {
            //请求参数说明：
            //参数	必须	说明
            //access_token	是	调用接口凭证
            //code	是	通过成员授权获取到的code，最大为512字节。每次成员授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。

            //返回参数说明：
            //userid	成员UserID。若需要获得用户详情信息，可调用通讯录接口：读取成员。如果是互联企业/企业互联/上下游，则返回的UserId格式如：CorpId/userid
            //user_ticket	成员票据，最大为512字节，有效期为1800s。
            //      scope为snsapi_privateinfo，且用户在应用可见范围之内时返回此参数。
            //      后续利用该参数可以获取用户信息或敏感信息，参见"获取访问用户敏感信息"。暂时不支持上下游或/企业互联场景
            string token = GetAccessToken().access_token;
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/auth/getuserinfo?access_token={token}&code={code}";
            string res = HttpHelper.HttpGet(url);
            WxUserAuthDto user = JsonConvert.DeserializeObject<WxUserAuthDto>(res);
            return user;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户身份id</param>
        /// <returns></returns>
        public static WxUserDto GetUserInfo(string userId)
        {
            //参数说明：
            //参数	必须	说明
            //access_token	是	调用接口凭证
            //userid	是	成员UserID。对应管理端的帐号，企业内必须唯一。不区分大小写，长度为1~64个字节
            string token = GetAccessToken().access_token;
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={token}&userid={userId}";
            string res = HttpHelper.HttpGet(url);
            WxUserDto userInfo = JsonConvert.DeserializeObject<WxUserDto>(res);
            return userInfo;
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="deptId"></param>
        public static void GetDeptInfo(int? deptId = null)
        {
            //请求方式：GET（HTTPS）
            //请求地址：https://qyapi.weixin.qq.com/cgi-bin/department/simplelist?access_token=ACCESS_TOKEN&id=ID
            string token = GetAccessToken().access_token;
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/department/simplelist?access_token={token}&id={deptId}";
            string res = HttpHelper.HttpGet(url);
        }

        /// <summary>
        /// 发送文本类消息
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static WxSendMessageDto SendTextMsg(string touser, string content)
        {
            //应用支持推送文本、图片、视频、文件、图文等类型。
            //请求方式：POST（HTTPS）
            //请求地址： https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=ACCESS_TOKEN
            string token = GetAccessToken().access_token;
            WxSendTextMessageParm parm = new()
            {
                msgtype = "text",
                touser = touser,
                agentid = int.Parse(WxCorpConfig.AGENTID)
            };
            parm.text.content = content;
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={token}";
            string data = JsonConvert.SerializeObject(parm);
            string res = HttpHelper.HttpPost(url, data, "contentType/json");
            WxSendMessageDto resDto = JsonConvert.DeserializeObject<WxSendMessageDto>(res);
            return resDto;
        }

        /// <summary>
        /// 获取access_token
        ///     获取access_token是调用企业微信API接口的第一步，相当于创建了一个登录凭证，其它的业务API接口，都需要依赖于access_token来鉴权调用者身份。
        ///     因此开发者，在使用业务接口前，要明确access_token的颁发来源，使用正确的access_token。
        ///权限说明：
        ///     每个应用有独立的secret，获取到的access_token只能本应用使用，所以每个应用的access_token应该分开来获取
        /// </summary>
        private static WxTokenDto GetToken()
        {
            //请求参数
            //参数	        必须	说明
            //corpid	    是	    企业ID，获取方式参考：术语说明-corpid
            //corpsecret	是	    应用的凭证密钥，注意应用需要是启用状态，获取方式参考：术语说明-secret

            //返回参数
            //参数          说明
            //errcode	    出错返回码，为0表示成功，非0表示调用失败
            //errmsg	    返回码提示语
            //access_token	获取到的凭证，最长为512字节
            //expires_in	凭证的有效时间（秒）

            string url = $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={WxCorpConfig.CORPID}&corpsecret={WxCorpConfig.CORPSECRET}";
            string res = HttpHelper.HttpGet(url);
            WxTokenDto ro = JsonConvert.DeserializeObject<WxTokenDto>(res);
            return ro;
        }

        #region 获取访问token

        //获取token
        private static WxTokenDto GetAccessToken()
        {
            var Ck = "wxcrop_token";
            //优先从缓存获取token
            if (CacheHelper.Get(Ck) is WxTokenDto tokenResult)
            {
                return tokenResult;
            }
            else
            {
                //获取新的token
                tokenResult = WxServerApi.GetToken();

                if (tokenResult?.errcode == 0)
                {
                    CacheHelper.SetCaches(Ck, tokenResult, tokenResult.expires_in - 60);
                }
                else
                {
                    Console.WriteLine("GetToken失败,结果=" + tokenResult);
                    throw new Exception("获取AccessToken失败");
                }
            }
            return tokenResult;
        }

        #endregion 获取访问token
    }
}
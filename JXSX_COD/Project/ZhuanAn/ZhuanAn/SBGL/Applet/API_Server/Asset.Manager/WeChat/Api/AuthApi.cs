using Asset.Manager.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Manager.WeChat.Api
{
    //权限相关API
    class AuthApi
    {
        /// <summary>
        /// 获取access_token
        ///     获取access_token是调用企业微信API接口的第一步，相当于创建了一个登录凭证，其它的业务API接口，都需要依赖于access_token来鉴权调用者身份。
        ///     因此开发者，在使用业务接口前，要明确access_token的颁发来源，使用正确的access_token。 
        ///权限说明：
        ///     每个应用有独立的secret，获取到的access_token只能本应用使用，所以每个应用的access_token应该分开来获取
        /// </summary>
        public static string GetToken()
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

            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", WxConfig.corpid, WxConfig.corpsecret);
            string method = "GET";
            return HttpUtil.Send(url, method, null, null);
        }

        /// <summary>
        /// 获取应用的jsapi_ticket
        ///     应用的jsapi_ticket用于计算agentConfig（参见“通过agentConfig注入应用的权限”）的签名
        ///     正常情况下，应用的jsapi_ticket的有效期为7200秒，通过access_token来获取。
        ///     由于获取jsapi_ticket的api调用次数非常有限（一小时内，每个应用不能超过100次），频繁刷新jsapi_ticket会导致api调用受限，影响自身业务，
        ///     开发者必须在自己的服务全局缓存应用的jsapi_ticket。
        /// </summary>
        /// <returns></returns>
        public static string GetJsApiTicket(string token)
        {
            //请求参数
            // access_token	应用的调用接口凭证，

            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/ticket/get?access_token={0}&type=agent_config", token);
            string method = "GET";
            return HttpUtil.Send(url, method, null, null);

            //返回参数
            //ticket	    生成签名所需的jsapi_ticket，最长为512字节
            //expires_in	凭证的有效时间（秒）
        }


        /// <summary>
        /// 获取访问用户身份
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetAuthUserId(string token,string code)
        {
            //请求参数说明：
            //参数	必须	说明
            //access_token	是	调用接口凭证
            //code	是	通过成员授权获取到的code，最大为512字节。每次成员授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
          
            string url =  string.Format("https://qyapi.weixin.qq.com/cgi-bin/auth/getuserinfo?access_token={0}&code={1}",token,code);
             string method = "GET";
            return HttpUtil.Send(url, method, null, null);

            //返回参数说明：
            //userid	成员UserID。若需要获得用户详情信息，可调用通讯录接口：读取成员。如果是互联企业/企业互联/上下游，则返回的UserId格式如：CorpId/userid
            //user_ticket	成员票据，最大为512字节，有效期为1800s。
            //      scope为snsapi_privateinfo，且用户在应用可见范围之内时返回此参数。
            //      后续利用该参数可以获取用户信息或敏感信息，参见"获取访问用户敏感信息"。暂时不支持上下游或/企业互联场景
        }


        /// <summary>
        /// 读取用户
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId">用户身份id</param>
        /// <returns></returns>
        public static string GetUserInfo(string token, string userId)
        {
            //参数说明：
            //参数	必须	说明
            //access_token	是	调用接口凭证
            //userid	是	成员UserID。对应管理端的帐号，企业内必须唯一。不区分大小写，长度为1~64个字节
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}", token, userId);
            string method = "GET";
            return HttpUtil.Send(url, method, null, null);
        }
    }
}

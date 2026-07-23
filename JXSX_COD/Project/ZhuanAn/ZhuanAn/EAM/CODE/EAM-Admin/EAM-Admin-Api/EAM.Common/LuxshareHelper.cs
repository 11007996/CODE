using Infrastructure;
using Infrastructure.Model;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace EAM.Common
{
    public class LuxshareHelper
    {
        #region OA登入

        private static readonly string OA_LOGIN_URL = AppSettings.Get<OALoginConfig>("OALoginConfig").BaseUrl;

        public static OALoginResult OALogin(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    throw new ArgumentException("用户名不能为空");
                if (string.IsNullOrEmpty(password))
                    throw new ArgumentException("密码不能为空");
                OALoginParam param = new() { Code = username, Password = password };
                string data = Tools.SerializeHttpForm(param);
                //跳过证书验证，不跳过会报错(证书没有设置有效期): The remote certificate is invalid because of errors in the certificate chain: NotTimeValid
                var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,//绕过验证
                    ClientCertificateOptions = ClientCertificateOption.Manual
                };
                using var client = new HttpClient(httpClientHandler);
                using HttpContent httpContent = new StringContent(data, Encoding.UTF8);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = client.PostAsync(OA_LOGIN_URL, httpContent).Result;
                return JsonSerializer.Deserialize<OALoginResult>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                OALoginResult oALoginResult = new OALoginResult
                {
                    IsSuccess = false,
                    ErrMsg = e.Message
                };
                return oALoginResult;
            }
        }

        //临时作用,hr接口挂了时使用
        [Discardable]
        public static OALoginResult OALogin2(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    throw new ArgumentException("用户名不能为空");
                if (string.IsNullOrEmpty(password))
                    throw new ArgumentException("密码不能为空");
                OALoginParam param = new() { Code = username, Password = password };
                string data = Tools.SerializeHttpForm(param);
                //跳过证书验证，不跳过会报错(证书没有设置有效期): The remote certificate is invalid because of errors in the certificate chain: NotTimeValid
                //var httpClientHandler = new HttpClientHandler
                //{
                //    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,//绕过验证
                //    ClientCertificateOptions = ClientCertificateOption.Manual
                //};
                using var client = new HttpClient();
                using HttpContent httpContent = new StringContent(data, Encoding.UTF8);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = client.GetAsync($"http://172.18.20.172:8088/api/Login?empCode={username}&password={password}").Result;
                OALoginResult res = new OALoginResult();
                if (Convert.ToBoolean(response.Content.ReadAsStringAsync().Result))
                {
                    res.IsSuccess = true;
                    res.ErrMsg = "";
                }
                else
                {
                    res.IsSuccess = false;
                    res.ErrMsg = "登入失败";
                }
                return res;
            }
            catch (Exception e)
            {
                OALoginResult oALoginResult = new OALoginResult
                {
                    IsSuccess = false,
                    ErrMsg = e.Message
                };
                return oALoginResult;
            }
        }

        #endregion OA登入

        #region DCS消息早知道通知

        private static readonly DcsMessageConfig DCSMessageConfig = AppSettings.Get<DcsMessageConfig>("DcsMessageConfig");

        /// <summary>
        /// 发送微信群组消息
        /// </summary>
        /// <param name="chatId">群组ID</param>
        /// <param name="content">消息内容</param>
        /// <returns></returns>
        public static string SendGroupTextMessage(string chatId, string content)
        {
            string url = DCSMessageConfig.Url + "/api/WorkWeChat/GroupTextMessage";
            DcsGroupTextMessageParam parm = new DcsGroupTextMessageParam()
            {
                SendApp = "1",
                Code = DCSMessageConfig.Code,
                Password = DCSMessageConfig.Password,
                ChatId = chatId,
                Content = content
            };
            string postData = JsonSerializer.Serialize(parm);
            return HttpHelper.HttpPost(url, postData);
        }

        /// <summary>
        /// 发送微信群组消息
        /// </summary>
        /// <param name="chatId">群组ID</param>
        /// <param name="content">消息内容</param>
        /// <returns></returns>
        public static string SendEmpTextMessage(string empCodes, string content)
        {
            string url = DCSMessageConfig.Url + "/api/WorkWeChat/SendTextMessage";
            DcsEmpTextMessageParam parm = new DcsEmpTextMessageParam()
            {
                SendApp = "1",
                Account = DCSMessageConfig.Code,
                Password = DCSMessageConfig.Password,
                EmpCodes = empCodes,
                Content = content
            };
            string postData = JsonSerializer.Serialize(parm);
            return HttpHelper.HttpPost(url, postData);
        }


        /// <summary>
        /// 发送微信群组消息
        /// </summary>
        /// <param name="chatId">群组ID</param>
        /// <param name="content">消息内容</param>
        /// <returns></returns>
        public static string SendGroupCardMessage(string chatId,string title, string content,string msgUrl)
        {
            string url = DCSMessageConfig.Url + "/api/WorkWeChat/SendChatFileMessage";
            DcsGroupCardMessageParam parm = new DcsGroupCardMessageParam()
            {
                SendApp = "1",
                Code = DCSMessageConfig.Code,
                Password = DCSMessageConfig.Password,
                ChatId = chatId,
                Title = title,
                Content = content,
                Url = msgUrl
            };
            string postData = JsonSerializer.Serialize(parm);
            return HttpHelper.HttpPost(url, postData);
        }

        #endregion DCS消息早知道通知
    }

    /// <summary>
    /// Dcs群组消息参数
    /// </summary>
    public class DcsGroupTextMessageParam
    {
        /// <summary>
        /// 发送消息的应用，1=消息早知道,2=IT安全告警
        /// </summary>
        public string SendApp { get; set; }

        public string Code { get; set; }
        public string Password { get; set; }
        public string ChatId { get; set; }
        public string Content { get; set; }
    }

    /// <summary>
    /// Dcs群组消息参数
    /// </summary>
    public class DcsGroupCardMessageParam
    {
        /// <summary>
        /// 发送消息的应用，1=消息早知道,2=IT安全告警
        /// </summary>
        public string SendApp { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string ChatId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// Dcs个人消息参数
    /// </summary>
    public class DcsEmpTextMessageParam
    {
        public string SendApp { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string EmpCodes { get; set; }
        public string Content { get; set; }
    }

    public class OALoginParam
    {
        public string Code { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// OA登入接口返回结果对象
    /// </summary>
    public class OALoginResult
    {
        public bool IsSuccess { get; set; }
        public string ErrMsg { get; set; }
    }
}
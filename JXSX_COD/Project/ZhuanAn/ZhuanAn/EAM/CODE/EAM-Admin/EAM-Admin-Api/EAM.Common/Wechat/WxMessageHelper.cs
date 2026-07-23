using Infrastructure;
using Infrastructure.Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Web;

namespace EAM.Common.Wechat
{
    /// <summary>
    /// 企业微信应用【消息早知道】 接口帮助类
    /// </summary>
    public class WxMessageHelper
    {
        public static readonly string MSGTYPE_TEXT = "text";

        public static readonly string MSGTYPE_TEXTCARD = "textcard";

        public static readonly string MSGTYPE_NEWS = "news";

        //private static readonly string PASSWORD = "luxshare888..";
        //private static readonly string URL = "https://oa.luxshare-ict.com/luxshare/workflow/weixin/push_operation_kp.jsp";
        private static readonly WxMessageConfig config = AppSettings.Get<WxMessageConfig>("WxMessageConfig");

        /// <summary>
        /// 发送富文件消息
        /// </summary>
        /// <param name="empCodes"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SendTextMessage(string empCodes, string content)
        {
            Message msg = new()
            {
                msgtype = MSGTYPE_TEXT,
                content = content,
                workcodes = empCodes,
            };
            if (!CheckParam(msg))
                throw new CustomException("发送消息失败,参数错误");
            msg.pass = config.Password;
            string postData = JsonSerializer.Serialize(msg);
            return HttpHelper.HttpPost(config.Url, postData);
        }

        /// <summary>
        /// 发送卡片模板消息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="empCodes"></param>
        /// <param name="linkUrl"></param>
        /// <returns></returns>
        public static string SendTextCardMessage(string empCodes, string title, string content, string linkUrl)
        {
            Message msg = new()
            {
                msgtype = MSGTYPE_TEXTCARD,
                title = title,
                content = content,
                workcodes = empCodes,
                linkurl = linkUrl,
            };
            if (!CheckParam(msg))
                throw new CustomException("发送消息失败,参数错误");
            msg.pass = config.Password;
            string data = Serialize(msg);
            return HttpHelper.HttpPost(config.Url, data, "application/x-www-form-urlencoded");
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="empCodes"></param>
        /// <param name="articles"></param>
        /// <returns></returns>
        public static string SendNewsMessage(string empCodes, List<WxMsgArticle> articles)
        {
            Message msg = new()
            {
                msgtype = MSGTYPE_NEWS,
                workcodes = empCodes,
                articles = articles
            };
            if (!CheckParam(msg))
                throw new CustomException("发送消息失败,参数错误");
            msg.pass = config.Password;
            string data = Serialize(msg);
            return HttpHelper.HttpPost(config.Url, data, "application/x-www-form-urlencoded");
        }

        public static string Serialize(object query, string prefix = "?")
        {
            if (query is null)
            {
                return null;
            }

            var isContractedType = query.GetType().IsDefined(typeof(DataContractAttribute));
            var properties = from property in query.GetType().GetProperties()
                             where property.CanRead && (isContractedType ? property.IsDefined(typeof(DataMemberAttribute)) : true)
                             let memberName = isContractedType ? property.GetCustomAttribute<DataMemberAttribute>().Name : property.Name
                             let value = property.GetValue(query, null)
                             where value != null && !string.IsNullOrWhiteSpace(value.ToString())
                             select memberName + "=" + HttpUtility.UrlEncode(value.ToString());
            var queryString = string.Join("&", properties);
            return string.IsNullOrWhiteSpace(queryString) ? "" : prefix + queryString;
        }

        public static string Serialize(object query)
        {
            if (query is null)
            {
                return null;
            }

            var isContractedType = query.GetType().IsDefined(typeof(DataContractAttribute));
            var properties = from property in query.GetType().GetProperties()
                             where property.CanRead && (isContractedType ? property.IsDefined(typeof(DataMemberAttribute)) : true)
                             let memberName = isContractedType ? property.GetCustomAttribute<DataMemberAttribute>().Name : property.Name
                             let value = property.GetValue(query, null)
                             where value != null && !string.IsNullOrWhiteSpace(value.ToString())
                             select memberName + "=" + value.ToString();
            var queryString = string.Join("&", properties);
            return string.IsNullOrWhiteSpace(queryString) ? "" : queryString;
        }

        //参数检查
        private static bool CheckParam(Message msg)
        {
            if (string.IsNullOrWhiteSpace(msg.msgtype) || msg.msgtype != MSGTYPE_TEXT && msg.msgtype != MSGTYPE_TEXTCARD && msg.msgtype != MSGTYPE_NEWS || string.IsNullOrWhiteSpace(msg.workcodes))
            {
                return false;
            }
            if (msg.msgtype == MSGTYPE_TEXT)
            {
                if (string.IsNullOrWhiteSpace(msg.content))
                {
                    return false;
                }
            }
            else if (msg.msgtype == MSGTYPE_TEXTCARD)
            {
                if (string.IsNullOrWhiteSpace(msg.title) || string.IsNullOrWhiteSpace(msg.content) || string.IsNullOrWhiteSpace(msg.linkurl))
                {
                    return false;
                }
            }
            else if (msg.msgtype == MSGTYPE_NEWS)
            {
                if (msg.articles == null || msg.articles.Count > 6)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class Message
    {
        //工号集合
        public string workcodes { get; set; }

        //标题
        public string title { get; set; }

        //内容
        public string content { get; set; }

        //类型
        public string msgtype { get; set; }

        //模板url
        public string linkurl { get; set; }

        //文章
        public List<WxMsgArticle> articles { get; set; }

        //密码
        public string pass { get; set; }

        //用户名
        public string username { get; set; }
    }

    /// <summary>
    /// 文章类型消息，数据格式
    /// </summary>
    public class WxMsgArticle
    {
        //图片url
        public string picurl { get; set; }

        //图片标题
        public string title { get; set; }

        //描述
        public string description { get; set; }

        //可为空
        public string url { get; set; }
    }
}
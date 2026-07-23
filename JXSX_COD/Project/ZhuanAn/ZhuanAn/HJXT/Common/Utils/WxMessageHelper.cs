using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class WxMessageHelper
    {
        private static string PASSWORD = "luxshare888..";
        private static string URL = "https://oa.luxshare-ict.com/luxshare/workflow/weixin/push_operation_kp.jsp";


        public static string MSGTYPE_TEXT = "text";
        public static string MSGTYPE_TEXTCARD = "textcard";
        public static string MSGTYPE_NEWS = "news";

        /// <summary>
        /// 发送富文件消息
        /// </summary>
        /// <param name="workCodes"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SendTextMessage(string workCodes, string content)
        {
            return SendMessage(MSGTYPE_TEXT, workCodes, null, content, null, null);
        }

        /// <summary>
        /// 发送卡片模板消息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="workCodes"></param>
        /// <param name="linkUrl"></param>
        /// <returns></returns>
        public static string SendTextCardMessage(string workCodes, string title, string content, string linkUrl)
        {
            return SendMessage(MSGTYPE_TEXTCARD, workCodes, title, content, linkUrl, null);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="workCodes"></param>
        /// <param name="articles"></param>
        /// <returns></returns>
        public static string SendNewsMessage(string workCodes, List<Article> articles)
        {
            return SendMessage(MSGTYPE_NEWS, workCodes, null, null, null, articles);
        }

        //发送微信消息
        public static string SendMessage(string msgType, string workCodes, string title, string content, string linkUrl, List<Article> articles)
        {
            Message msg = new Message();
            msg.title = title;
            msg.content = content;
            msg.workcodes = workCodes;
            msg.msgtype = msgType;
            msg.linkurl = linkUrl;
            msg.articles = articles;
            return SendMessage(msg);
        }

        //发送微信消息
        public static string SendMessage(Message msg)
        {
            if (!CheckParam(msg)) return "参数错误";
            msg.pass = PASSWORD;
            return HttpHelper.Post(URL, msg);
        }


        //参数检查
        private static bool CheckParam(Message msg)
        {
            if (string.IsNullOrWhiteSpace(msg.msgtype) || (msg.msgtype != MSGTYPE_TEXT && msg.msgtype != MSGTYPE_TEXTCARD && msg.msgtype != MSGTYPE_NEWS) || string.IsNullOrWhiteSpace(msg.workcodes))
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
        public List<Article> articles { get; set; }
        //密码
        public string pass { get; set; }
        //用户名
        public string username { get; set; }
    }


    /// <summary>
    /// 文章类型消息，数据格式
    /// </summary>
    public class Article
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

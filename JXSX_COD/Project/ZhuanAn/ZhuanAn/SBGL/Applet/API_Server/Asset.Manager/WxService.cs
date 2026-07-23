using Asset.Manager.Util;
using Asset.Manager.WeChat;
using Asset.Manager.WeChat.Api;
using Asset.Manager.WeChat.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Manager
{
    public class WxService
    {
        #region 获取访问token
        //获取token
        public static string GetToken()
        {
            //先从TokenInfo中获取
            if (TokenInfo.tokenInfo == null || string.IsNullOrWhiteSpace(TokenInfo.tokenInfo.access_token))
            {
                //如果配置对象中不存在，则从json文件中获取
                TokenInfo.tokenInfo = FileUtil.DeserializeFromFile<TokenInfo>(WxConfig.token_file);
            }

            //判断token是否存在，以及是否过期（提前一分钟过期）
            if (TokenInfo.tokenInfo != null && !string.IsNullOrWhiteSpace(TokenInfo.tokenInfo.access_token) && TokenInfo.tokenInfo.expires_time > DateTime.Now.AddMinutes(1))
            {//未过期
                return TokenInfo.tokenInfo.access_token;
            }

            //如果读取不到或已过期，则通过接口调用获取token
            string res = AuthApi.GetToken();
            if (res != null)
            {
                TokenDTO ro = JsonConvert.DeserializeObject<TokenDTO>(res);
                if (ro.errcode == 0)
                {//获取token成功
                    //更新token信息
                    TokenInfo tokenInfo = new TokenInfo();
                    tokenInfo.access_token = ro.access_token;
                    tokenInfo.expires_in = ro.expires_in;
                    tokenInfo.expires_time = DateTime.Now.AddSeconds(ro.expires_in);
                    TokenInfo.tokenInfo = tokenInfo;
                    //更新到保存token的json文件
                    FileUtil.SerializeToFile(WxConfig.token_file, tokenInfo);
                    return ro.access_token;
                }
            }
            return null;
        }
        #endregion


        #region 通过用户授权的code获取用户信息
        public static UserInfoDTO GetUserInfo(string code, ref string message)
        {
            string token = GetToken();
            string res = AuthApi.GetAuthUserId(token, code);
            UserAuthDTO user = JsonConvert.DeserializeObject<UserAuthDTO>(res);
            if (user != null && user.errcode == 0 )
            {
                if (string.IsNullOrWhiteSpace(user.userid))
                {
                    message = "非本企业用户";
                    return null;
                }
                string res2 = AuthApi.GetUserInfo(token, user.userid);
                UserInfoDTO userInfo = JsonConvert.DeserializeObject<UserInfoDTO>(res2);
                if (userInfo.errcode == 0)
                {
                    //转换为特定对象
                    return userInfo;
                }
                else
                {
                    message = userInfo.errmsg;
                }
            }
            else
            {
                message = user.errmsg;
            }
            return null;
        }
        #endregion


        #region 获取应用的Ticket
        public static string GetJsApiTicket()
        {
            //先从TicketInfo中获取
            if (TicketInfo.ticketInfo == null || string.IsNullOrWhiteSpace(TicketInfo.ticketInfo.ticket))
            {
                //从json文件中获取
                TicketInfo.ticketInfo = FileUtil.DeserializeFromFile<TicketInfo>(WxConfig.ticket_file);
            }

            //判断ticket是否存在，以及是否过期（提前一分钟过期）
            if (TicketInfo.ticketInfo != null && !string.IsNullOrWhiteSpace(TicketInfo.ticketInfo.ticket) && TicketInfo.ticketInfo.expires_time > DateTime.Now.AddMinutes(1))
            {//未过期
                return TicketInfo.ticketInfo.ticket;
            }

            //如果读取不到或已过期，则调用接口获取
            //获取ticket
            string token = GetToken();
            if (token == null) return null;
            string res = AuthApi.GetJsApiTicket(token);
            if (res != null)
            {
                TicketDTO ro = JsonConvert.DeserializeObject<TicketDTO>(res);
                if (ro.errcode == 0)
                {//获取ticket成功
                    //更新ticket信息
                    TicketInfo tickInfo = new TicketInfo();
                    tickInfo.ticket = ro.ticket;
                    tickInfo.expires_in = ro.expires_in;
                    tickInfo.expires_time = DateTime.Now.AddSeconds(ro.expires_in);
                    TicketInfo.ticketInfo = tickInfo;
                    //更新到保存json文件
                    FileUtil.SerializeToFile(WxConfig.ticket_file, tickInfo);
                    return ro.ticket;
                }
            }
            return null;
        }
        #endregion
    }
}

using EAM.Common.Wechat;
using EAM.Common.Wechat.Model;
using EAM.ServiceCore.Model;
using Infrastructure;
using Infrastructure.Attribute;
using Mapster;

namespace EAM.ServiceCore.Services
{
    [AppService(ServiceType = typeof(IWxUserService), ServiceLifetime = LifeTime.Transient)]
    public class WxUserService : BaseService<WxUser>, IWxUserService
    {
        /// <summary>
        /// 通过code 获取到微信用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WxUser GetInfo(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new CustomException("微信授权code不能为空");
            }
            string wxUserId = GetWxUserId(code);
            //优先查询数据库 是否有这个用户的信息
            WxUser wxUser = GetWxUserById(wxUserId);
            return wxUser;
        }

        #region 发送消息

        public void SendTextMsg(string touser, string content)
        {
            WxSendMessageDto res = WxServerApi.SendTextMsg(touser, content);
            if (res == null || res.errcode != 0)
            {
                throw new CustomException("发送失败:" + res.errmsg);
            }
        }

        #endregion 发送消息

        #region 获取微信用户信息

        private static string GetWxUserId(string code)
        {
            WxUserAuthDto userAuth = WxServerApi.GetAuthUserId(code);
            if (userAuth == null)
                throw new CustomException("获取微信用户userid失败");
            if (userAuth.errcode != 0)
                throw new CustomException($"获取微信用户userid失败:{userAuth.errmsg}");
            if (string.IsNullOrWhiteSpace(userAuth.userid))
                throw new CustomException($"获取微信用户userid失败:非本企业用户");
            return userAuth.userid;
        }

        /// <summary>
        /// 获取企业微信用户信息
        /// </summary>
        /// <param name="wxUserId"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private WxUser GetWxUserById(string wxUserId)
        {
            WxUser wxUser = Queryable().Where(it => it.userid == wxUserId).First();
            if (wxUser == null)
            {//通过userid获取用户信息
                WxUserDto userInfo = WxServerApi.GetUserInfo(wxUserId);
                if (userInfo == null)
                    throw new CustomException("获取微信用户的信息失败");
                if (userInfo.errcode != 0)
                    throw new CustomException($"获取微信用户的信息失败:{userInfo.errmsg}");
                wxUser = userInfo.Adapt<WxUser>();
                //部分数据转为string存入数据库
                wxUser.department = JsonConvert.SerializeObject(userInfo.department);
                wxUser.order = JsonConvert.SerializeObject(userInfo.order);
                wxUser.is_leader_in_dept = JsonConvert.SerializeObject(userInfo.is_leader_in_dept);
                wxUser.direct_leader = JsonConvert.SerializeObject(userInfo.direct_leader);
                wxUser.extattr = JsonConvert.SerializeObject(userInfo.extattr);

                //获取到用户的扩展属性（工号）
                foreach (var attr in userInfo.extattr.attrs)
                {
                    if (attr.name == "工号" && !string.IsNullOrWhiteSpace(attr.value))
                    {
                        wxUser.sys_username = attr.value;
                        break;
                    }
                }

                wxUser = Insertable(wxUser).ExecuteReturnEntity();
            }
            return wxUser;
        }

        #endregion 获取微信用户信息

        #region 获取应用的Ticket

        //public static string GetJsApiTicket()
        //{
        //    //先从TicketInfo中获取
        //    if (TicketInfo.ticketInfo == null || string.IsNullOrWhiteSpace(TicketInfo.ticketInfo.ticket))
        //    {
        //        //从json文件中获取
        //        TicketInfo.ticketInfo = FileUtil.DeserializeFromFile<TicketInfo>(WxConfig.ticket_file);
        //    }

        //    //判断ticket是否存在，以及是否过期（提前一分钟过期）
        //    if (TicketInfo.ticketInfo != null && !string.IsNullOrWhiteSpace(TicketInfo.ticketInfo.ticket) && TicketInfo.ticketInfo.expires_time > DateTime.Now.AddMinutes(1))
        //    {//未过期
        //        return TicketInfo.ticketInfo.ticket;
        //    }

        //    //如果读取不到或已过期，则调用接口获取
        //    //获取ticket
        //    string token = GetToken();
        //    if (token == null) return null;
        //    string res = AuthApi.GetJsApiTicket(token);
        //    if (res != null)
        //    {
        //        WxTicketDto ro = JsonConvert.DeserializeObject<WxTicketDto>(res);
        //        if (ro.errcode == 0)
        //        {//获取ticket成功
        //            //更新ticket信息
        //            TicketInfo tickInfo = new TicketInfo();
        //            tickInfo.ticket = ro.ticket;
        //            tickInfo.expires_in = ro.expires_in;
        //            tickInfo.expires_time = DateTime.Now.AddSeconds(ro.expires_in);
        //            TicketInfo.ticketInfo = tickInfo;
        //            //更新到保存json文件
        //            FileUtil.SerializeToFile(WxConfig.ticket_file, tickInfo);
        //            return ro.ticket;
        //        }
        //    }
        //    return null;
        //}

        #endregion 获取应用的Ticket
    }
}
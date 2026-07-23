using Asset.BLL;
using Asset.Filter;
using Asset.Model;
using Asset.Models;
using Asset.Util;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Asset.Controllers
{

    public class HomeController : BaseController
    {
        private static readonly ILog Logger = log4net.LogManager.GetLogger(typeof(HomeController));
        UserService service = new UserService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 微信code获取登入凭证
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult WXAuthLogin(string code)
        {
            ResultMsg r = new ResultMsg();
            try
            {
                string message = "";
                //通过授权code获取用户信息
                Asset.Manager.WeChat.Model.UserInfoDTO wwUser = Asset.Manager.WxService.GetUserInfo(code, ref message);

                if (wwUser == null)
                {
                    r.MsgCode = "-1";
                    r.MsgInfo = "解析用户的授权code错误，原因：" + message;
                }
                else
                {
                    //用户信息 
                    UserInfoDO user = new UserInfoDO();
                    user.UserName = wwUser.name;
                    foreach (Asset.Manager.WeChat.Model.Attr attr in wwUser.extattr.attrs)
                    {
                        if (attr.name == "工号" && !string.IsNullOrWhiteSpace(attr.value))
                        {
                            user.WorkCode = attr.value;
                            break;
                        }
                    }
                    //判断是否有工号 ，因很多老员工在企业微信个人信息中的工号没有设置，去掉验证
                    //if (user.WorkCode == null)
                    //{
                    //    r.MsgCode = "1";
                    //    r.MsgInfo = "未找到此用户的工号，当前用户信息：" + JsonConvert.SerializeObject(wwUser);
                    //    r.Data = null;
                    //    return Json(r, JsonRequestBehavior.AllowGet);
                    //}
                    //获取当前用户在设备系统中的权限
                    SysUserDO sysUser = service.GetUserInfo(user.WorkCode);
                    if (sysUser != null) user.UserRight = sysUser.UserRight;
                    //设置创建和超时时间
                    DateTime now = DateTime.Now;
                    int TokenCacheTimes = Convert.ToInt32(ConfigurationManager.AppSettings["TokenCacheTimes"]);
                    DateTime expiresTime = now.AddMinutes(TokenCacheTimes);
                    user.CreateTime = now;
                    user.ExpiresTime = expiresTime;

                    //生成token
                    var payload = new Dictionary<string, object>
                    {
                        { "UserName", user.UserName },
                        { "WorkCode", user.WorkCode  },
                        { "UserRight",user.UserRight},
                        { "CreateTime",  user.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")},
                        { "ExpiresTime", user.ExpiresTime.ToString("yyyy-MM-dd HH:mm:ss")}
                    };
                    string token = JwtHelp.SetJwtEncode(payload);

                    //缓存token信息
                    Cache cache = HttpRuntime.Cache;
                    if (cache.Get(token) != null) cache.Remove(token);
                    cache.Insert(token, user, null, user.ExpiresTime, TimeSpan.Zero);//缓存token

                    service.SaveUserInfo(token, user);

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("token", token);
                    dic.Add("userInfo", user);
                    //返回结果
                    r.MsgCode = "0";
                    r.MsgInfo = "登入成功";
                    r.Data = dic;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.MsgInfo = "微信用户登入异常,异常信息：" + ex.Message;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 设备系统账号获取登入凭证
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult MachineUserLogin(string workcode, string password)
        {
            ResultMsg r = new ResultMsg();
            try
            {
                //通过授权code获取用户信息
                if (string.IsNullOrEmpty(workcode) || string.IsNullOrEmpty(password))
                {
                    r.MsgCode = "-1";
                    r.MsgInfo = "用户名或密码不能为空";
                }
                else
                {
                    //获取当前用户在设备系统中的权限
                    SysUserDO sysUser = service.GetUserInfo(workcode);
                    if(sysUser==null || sysUser.Pwd != password)
                    {
                        r.MsgCode = "-1";
                        r.MsgInfo = "用户名或密码错误";
                        return Json(r, JsonRequestBehavior.AllowGet);
                    }

                    //设置创建和超时时间
                    DateTime now = DateTime.Now;
                    //int TokenCacheTimes = Convert.ToInt32(ConfigurationManager.AppSettings["TokenCacheTimes"]);
                    DateTime expiresTime = now.AddDays(1);

                    //用户信息 
                    UserInfoDO user = new UserInfoDO();
                    user.WorkCode = sysUser.UserNo;
                    user.UserName = sysUser.UserName;
                    user.UserRight = sysUser.UserRight;
                    user.CreateTime = now;
                    user.ExpiresTime = expiresTime;

                    //生成token
                    var payload = new Dictionary<string, object>
                    {
                        { "UserName", user.UserName },
                        { "WorkCode", user.WorkCode  },
                        { "UserRight",user.UserRight},
                        { "CreateTime",  user.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")},
                        { "ExpiresTime", user.ExpiresTime.ToString("yyyy-MM-dd HH:mm:ss")}
                    };
                    string token = JwtHelp.SetJwtEncode(payload);

                    //缓存token信息
                    Cache cache = HttpRuntime.Cache;
                    if (cache.Get(token) != null) cache.Remove(token);
                    cache.Insert(token, user, null, user.ExpiresTime, TimeSpan.Zero);//缓存token

                    //service.SaveUserInfo(token, user);

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("token", token);
                    dic.Add("userInfo", user);
                    //返回结果
                    r.MsgCode = "0";
                    r.MsgInfo = "登入成功";
                    r.Data = dic;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.MsgInfo = "设备用户登入异常,异常信息：" + ex.Message;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        //获取当前登入用户的信息
        public ActionResult UserInfo(string token)
        {
            ResultMsg r = new ResultMsg();
            try
            {
                //权限验证
                UserInfoDO userinfo = JwtHelp.GetJwtDecode(token);
                if (userinfo != null)
                {
                    //返回结果
                    r.MsgCode = "0";
                    r.MsgInfo = "获取用户身份成功";
                    r.Data = userinfo;
                }
                else
                {
                    r.MsgCode = "1";
                    r.MsgInfo = "获取用户失败";
                    r.Data = null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.MsgInfo = "获取用户信息失败,异常信息：" + ex.Message;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}
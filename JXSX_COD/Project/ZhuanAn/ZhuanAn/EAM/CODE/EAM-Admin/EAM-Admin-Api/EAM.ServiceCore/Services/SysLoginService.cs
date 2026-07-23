using EAM.Common;
using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Constant;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.Model.System.Dto;
using EAM.Repository;
using EAM.ServiceCore.Model;
using EAM.ServiceCore.Model.Dto;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using SqlSugar.IOC;
using UAParser;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 登录
    /// </summary>
    [AppService(ServiceType = typeof(ISysLoginService), ServiceLifetime = LifeTime.Transient)]
    public class SysLoginService : BaseService<SysLogininfor>, ISysLoginService
    {
        private readonly ISysUserService SysUserService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWxUserService WxUserService;

        public SysLoginService(ISysUserService sysUserService, IWxUserService wxUserService, IHttpContextAccessor httpContextAccessor)
        {
            SysUserService = sysUserService;
            WxUserService = wxUserService;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="logininfor"></param>
        /// <param name="loginBody"></param>
        /// <returns></returns>
        public SysUser Login(LoginBodyDto loginBody, SysLogininfor logininfor)
        {
            if (loginBody.Password.Length != 32)
            {
                loginBody.Password = NETCore.Encrypt.EncryptProvider.Md5(loginBody.Password);
            }
            SysUser user = SysUserService.Login(loginBody);
            logininfor.UserName = loginBody.Username;
            logininfor.Status = "1";
            logininfor.LoginTime = DateTime.Now;
            logininfor.Ipaddr = loginBody.LoginIP;

            ClientInfo clientInfo = httpContextAccessor.HttpContext.GetClientInfo();
            logininfor.Browser = clientInfo?.ToString();
            logininfor.Os = clientInfo?.OS.ToString();

            if (user == null || user.UserId <= 0)
            {
                logininfor.Msg = "用户名或密码错误";
                AddLoginInfo(logininfor);
                throw new CustomException(ResultCode.LOGIN_ERROR, logininfor.Msg, false);
            }
            if (user.Status == 1)
            {
                logininfor.Msg = "该用户已禁用";
                AddLoginInfo(logininfor);
                throw new CustomException(ResultCode.LOGIN_ERROR, logininfor.Msg, false);
            }

            logininfor.Status = "0";
            logininfor.Msg = "登录成功";
            AddLoginInfo(logininfor);
            SysUserService.UpdateLoginInfo(loginBody.LoginIP, user.UserId);
            return user;
        }

        /// <summary>
        /// OA账号登录验证
        /// </summary>
        /// <param name="logininfor"></param>
        /// <param name="loginBody"></param>
        /// <returns></returns>
        public SysUser OALogin(LoginBodyDto loginBody, SysLogininfor logininfor)
        {
            logininfor.UserName = loginBody.Username;
            logininfor.Status = "1";
            logininfor.LoginTime = DateTime.Now;
            logininfor.Ipaddr = loginBody.LoginIP;

            ClientInfo clientInfo = httpContextAccessor.HttpContext.GetClientInfo();
            logininfor.Browser = clientInfo?.ToString();
            logininfor.Os = clientInfo?.OS.ToString();

            SysUser user = SysUserService.SelectUserByName(loginBody.Username);
            if (user == null || user.UserId <= 0)
            {
                logininfor.Msg = "当前系统不存在此用户";
                AddLoginInfo(logininfor);
                throw new CustomException(ResultCode.LOGIN_ERROR, logininfor.Msg, false);
            }
            //系统用户，不允许通过OA登入
            if (user.UserType == UserTypeConstant.系统用户)
            {
                logininfor.Msg = "当前用户为系统用户，不允许通过OA账号密码登入";
                AddLoginInfo(logininfor);
                throw new CustomException(ResultCode.LOGIN_ERROR, logininfor.Msg, false);
            }
            //OA api认证
            OALoginResult res = LuxshareHelper.OALogin(loginBody.Username, loginBody.Password);
            if (!res.IsSuccess)
            {
                logininfor.Msg = "OA验证失败:" + res.ErrMsg;
                AddLoginInfo(logininfor);
                throw new CustomException(ResultCode.LOGIN_ERROR, logininfor.Msg, false);
            }

            if (user.Status == 1)
            {
                logininfor.Msg = "该用户已禁用";
                AddLoginInfo(logininfor);
                throw new CustomException(ResultCode.LOGIN_ERROR, logininfor.Msg, false);
            }

            //绑定企业微信
            if (!string.IsNullOrEmpty(loginBody.WxCode))
            {
                string wxUserId = CacheService.GetWxCode(loginBody.WxCode)?.ToString();
                WxUser wxUser = Context.Queryable<WxUser>().Where(it => it.sys_username == loginBody.Username).First();
                if (!string.IsNullOrEmpty(wxUserId) && wxUser == null)
                {
                    try
                    {
                        SysUserService.BindWxUser(loginBody.Username, wxUserId);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            logininfor.Status = "0";
            logininfor.Msg = "登录成功";
            AddLoginInfo(logininfor);
            SysUserService.UpdateLoginInfo(loginBody.LoginIP, user.UserId);
            return user;
        }

        /// <summary>
        /// 微信code登入
        /// </summary>
        /// <param name="wxCode"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public SysUser WxLogin(string wxCode)
        {
            WxUser wxUser = WxUserService.GetInfo(wxCode);
            if (wxUser != null)
            {//这里将Code与微信用户Id缓存，OA注册时候可以使用到
                CacheService.SetWxCode(wxCode, wxUser.userid);
            }

            //检查当前企业微信用户是否有关联的系统用户
            if (string.IsNullOrEmpty(wxUser.sys_username))
                throw new CustomException($"当前企业微信用户【{wxUser.userid}】未关联工号信息");

            SysUser user = SysUserService.SelectUserByName(wxUser.sys_username);
            // 系统里没有找到，尝试注册用户。
            if (user == null || user.UserId <= 0)
            {
                //查询微信部门信息
                SysDeptExpand deptExpand = Context.Queryable<SysDeptExpand>().Where(it => it.WxDeptId == wxUser.main_department).First();
                if (deptExpand == null || string.IsNullOrEmpty(deptExpand.DefaultFactoryId))
                    throw new CustomException($"当前微信用户所属微信部门【{wxUser.main_department}】未关联厂区,请联系管理员添加");

                //将用户添加到系统
                user = new SysUser();
                user.Create_by = wxUser.sys_username;
                user.Create_time = DateTime.Now;
                user.DelFlag = (int)DeleteFlagEnum.存在;
                user.NickName = wxUser.name;
                user.UserName = wxUser.sys_username;
                user.DeptId = deptExpand.SysDeptId;
                user.Password = NETCore.Encrypt.EncryptProvider.Md5(wxUser.sys_username);
                user.UserType = UserTypeConstant.OA用户;
                user.FactoryIds = new string[] { deptExpand.DefaultFactoryId };
                //获取厂区默认的普通用户权限
                SysFactory factory = Context.Queryable<SysFactory>().Where(it => it.FactoryId == deptExpand.DefaultFactoryId).First();
                user.RoleIds = factory == null || factory.DefaultRoleId == null ? null : new long[] { factory.DefaultRoleId.Value };
                SysUserService.InsertUser(user);

                try
                {
                    // 在对应厂区ID的数据库添加员工信息
                    var factoryDB = DbScoped.SugarScope.GetConnectionScope(deptExpand.DefaultFactoryId);//根据类传入的ConfigId自动选择
                    Employee employee = factoryDB.Queryable<Employee>().Where(it => it.EmpCode == wxUser.sys_username).First();
                    if (employee == null)
                    {
                        employee = new Employee();
                        employee.EmpCode = wxUser.sys_username;
                        employee.EmpName = wxUser.name;
                        employee.DeptId = deptExpand.SysDeptId;
                        employee.Status = 0;
                        employee.DelFlag = (int)DeleteFlagEnum.存在;
                        employee.CreateBy = wxUser.sys_username;
                        employee.CreateTime = DateTime.Now;
                        factoryDB.Insertable(employee).ExecuteCommand();
                    }
                }
                catch (Exception)
                {
                }
            }

            string ip = httpContextAccessor.HttpContext.GetClientUserIp();
            SysLogininfor logininfor = new SysLogininfor();
            logininfor.UserName = user.UserName;
            logininfor.Status = "1";
            logininfor.LoginTime = DateTime.Now;
            logininfor.Ipaddr = HttpContextExtension.GetIpInfo(ip);
            ClientInfo clientInfo = httpContextAccessor.HttpContext.GetClientInfo();
            logininfor.Browser = clientInfo?.ToString();
            logininfor.Os = clientInfo?.OS.ToString();

            if (user.Status == 1)
            {
                logininfor.Msg = "该用户已禁用";
                AddLoginInfo(logininfor);
                throw new CustomException(ResultCode.LOGIN_ERROR, logininfor.Msg, false);
            }

            logininfor.Status = "0";
            logininfor.Msg = "登录成功";
            AddLoginInfo(logininfor);
            SysUserService.UpdateLoginInfo(ip, user.UserId);
            return user;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="logininfor"></param>
        /// <param name="loginBody"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public SysUser PhoneLogin(PhoneLoginDto loginBody, SysLogininfor logininfor, SysUser user)
        {
            logininfor.UserName = user.UserName;
            logininfor.Status = "1";
            logininfor.LoginTime = DateTime.Now;
            logininfor.Ipaddr = loginBody.LoginIP;

            ClientInfo clientInfo = httpContextAccessor.HttpContext.GetClientInfo();
            logininfor.Browser = clientInfo?.ToString();
            logininfor.Os = clientInfo?.OS.ToString();

            if (user.Status == 1)
            {
                logininfor.Msg = "该用户已禁用";
                AddLoginInfo(logininfor);
                throw new CustomException(ResultCode.LOGIN_ERROR, logininfor.Msg, false);
            }

            logininfor.Status = "0";
            logininfor.Msg = "登录成功";
            AddLoginInfo(logininfor);
            SysUserService.UpdateLoginInfo(loginBody.LoginIP, user.UserId);
            return user;
        }

        /// <summary>
        /// 查询登录日志
        /// </summary>
        /// <param name="logininfoDto"></param>
        /// <param name="pager">分页</param>
        /// <returns></returns>
        public PagedInfo<SysLogininfor> GetLoginLog(SysLogininfor logininfoDto, PagerInfo pager)
        {
            var exp = Expressionable.Create<SysLogininfor>();

            exp.AndIF(logininfoDto.BeginTime == null, it => it.LoginTime >= DateTime.Now.ToShortDateString().ParseToDateTime());
            exp.AndIF(logininfoDto.BeginTime != null, it => it.LoginTime >= logininfoDto.BeginTime && it.LoginTime <= logininfoDto.EndTime);
            exp.AndIF(logininfoDto.Ipaddr.IfNotEmpty(), f => f.Ipaddr == logininfoDto.Ipaddr);
            exp.AndIF(logininfoDto.UserName.IfNotEmpty(), f => f.UserName.Contains(logininfoDto.UserName));
            exp.AndIF(logininfoDto.Status.IfNotEmpty(), f => f.Status == logininfoDto.Status);
            var query = Queryable().Where(exp.ToExpression())
            .OrderBy(it => it.InfoId, OrderByType.Desc);

            return query.ToPage(pager);
        }

        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="sysLogininfor"></param>
        /// <returns></returns>
        public void AddLoginInfo(SysLogininfor sysLogininfor)
        {
            Insert(sysLogininfor);
        }

        /// <summary>
        /// 清空登录日志
        /// </summary>
        public void TruncateLogininfo()
        {
            Truncate();
        }

        /// <summary>
        /// 删除登录日志
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteLogininforByIds(long[] ids)
        {
            return Delete(ids);
        }

        public void CheckLockUser(string userName)
        {
            var lockTimeStamp = CacheService.GetLockUser(userName);
            var lockTime = DateTimeHelper.ToLocalTimeDateBySeconds(lockTimeStamp);
            var ts = lockTime - DateTime.Now;

            if (lockTimeStamp > 0 && ts.TotalSeconds > 0)
            {
                throw new CustomException(ResultCode.LOGIN_ERROR, $"你的账号已被锁,剩余{Math.Round(ts.TotalMinutes, 0)}分钟");
            }
        }

        public List<StatiLoginLogDto> GetStatiLoginlog()
        {
            var time = DateTime.Now;

            //如果是查询当月那么 time就是 DateTime.Now
            var days = (time.AddMonths(1) - time).Days;//获取当月天数
            var dayArray = Enumerable.Range(1, days).Select(it => Convert.ToDateTime(time.ToString("yyyy-MM-" + it))).ToList();//转成时间数组

            var queryableLeft = Context.Reportable(dayArray)
                .ToQueryable<DateTime>();

            var queryableRight = Context.Queryable<SysLogininfor>();
            var list = Context.Queryable(queryableLeft, queryableRight, JoinType.Left, (x1, x2)
                 => x2.LoginTime.ToString("yyyy-MM-dd") == x1.ColumnName.ToString("yyyy-MM-dd"))
                .GroupBy((x1, x2) => x1.ColumnName)
                .Where((x1, x2) => x1.ColumnName >= DateTime.Now.AddDays(-7) && x1.ColumnName <= DateTime.Now)
                .Select((x1, x2) => new StatiLoginLogDto()
                {
                    DeRepeatNum = SqlFunc.AggregateDistinctCount(x2.Ipaddr),
                    Num = SqlFunc.AggregateCount(x2.InfoId),
                    Date = x1.ColumnName,
                })
                .Mapper(it =>
                {
                    it.WeekName = Tools.GetWeekByDate(it.Date);//相当于ToList循环赋值
                }).ToList();
            return list;
        }
    }
}
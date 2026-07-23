using EAM.Model.System;
using EAM.Model.System.Dto;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EAM.Admin.WebApi.Controllers.System
{
    /// <summary>
    /// 登录
    /// </summary>
    [ApiExplorerSettings(GroupName = "sys")]
    public class SysLoginController : BaseController
    {
        private readonly ISysUserService sysUserService;
        private readonly ISysMenuService sysMenuService;
        private readonly ISysLoginService sysLoginService;
        private readonly ISysPermissionService permissionService;
        private readonly ICaptcha SecurityCodeHelper;
        private readonly ISysConfigService sysConfigService;
        private readonly ISysRoleService roleService;
        private readonly ISmsCodeLogService smsCodeLogService;
        private readonly ISysUserFactoryService userFactoryService;
        private readonly ISysFactoryService factoryService;

        public SysLoginController(
            ISysMenuService sysMenuService,
            ISysUserService sysUserService,
            ISysLoginService sysLoginService,
            ISysPermissionService permissionService,
            ISysConfigService configService,
            ISysRoleService sysRoleService,
            ISysUserFactoryService userFactoryService,
            ISysFactoryService factoryService,
            ISmsCodeLogService smsCodeLogService,
            ICaptcha captcha)
        {
            SecurityCodeHelper = captcha;
            this.sysMenuService = sysMenuService;
            this.sysUserService = sysUserService;
            this.sysLoginService = sysLoginService;
            this.permissionService = permissionService;
            this.sysConfigService = configService;
            this.smsCodeLogService = smsCodeLogService;
            this.userFactoryService = userFactoryService;
            this.factoryService = factoryService;
            roleService = sysRoleService;
        }

        /// <summary>
        /// 登录(用户名、密码)
        /// </summary>
        /// <param name="loginBody">登录对象</param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        [Log(Title = "登录")]
        public IActionResult Login([FromBody] LoginBodyDto loginBody)
        {
            if (loginBody == null) { throw new CustomException("请求参数错误"); }
            loginBody.LoginIP = HttpContextExtension.GetClientUserIp(HttpContext);
            SysConfig sysConfig = sysConfigService.GetSysConfigByKey("sys.account.captchaOnOff");
            if (sysConfig?.ConfigValue != "off" && !SecurityCodeHelper.Validate(loginBody.Uuid, loginBody.Code))
            {
                return ToResponse(ResultCode.CAPTCHA_ERROR, "验证码错误");
            }

            //检查登入厂区（当登入时没有传厂区或传入的厂区权限时，默认登入第一个有权限的厂区）
            if (string.IsNullOrEmpty(loginBody.FactoryId) || !userFactoryService.CheckUserFactoryPermission(loginBody.Username, loginBody.FactoryId))
            {
                // 获取用户的可登入厂区
                List<SysFactory> factorys = userFactoryService.GetUserFactorysByUserName(loginBody.Username);
                //检查是否有厂区的登入权限
                if (factorys == null || factorys.Count <= 0)
                    throw new CustomException("用户没有可登入的厂区");
                else
                    loginBody.FactoryId = factorys[0].FactoryId;
            }

            sysLoginService.CheckLockUser(loginBody.Username);
            string location = HttpContextExtension.GetIpInfo(loginBody.LoginIP);
            SysUser user = null;

            if (loginBody.UseOaAccount)
            {//集团OA账号密码登入
                //密码对称解密
                RSASecurityConfig rsaSecurityConfig = AppSettings.Get<RSASecurityConfig>("RSASecurityConfig");
                string privateKey = $"-----BEGIN PRIVATE KEY-----{Environment.NewLine}{rsaSecurityConfig.PrivateKey}{Environment.NewLine}-----END PRIVATE KEY-----";
                loginBody.Password = Encoding.Default.GetString(NETCore.Encrypt.EncryptProvider.RSADecryptWithPem(privateKey, Convert.FromBase64String(loginBody.Password)));

                user = sysLoginService.OALogin(loginBody, new SysLogininfor() { LoginLocation = location });
            }
            else
            {//系统账号密码登入
                user = sysLoginService.Login(loginBody, new SysLogininfor() { LoginLocation = location });
            }

            List<SysRole> roles = roleService.SelectUserRoleListByUserId(user.UserId);
            //权限集合 eg *:*:*,system:user:list
            List<string> permissions = permissionService.GetMenuPermission(user);

            TokenModel loginUser = new(user.Adapt<TokenModel>(), roles.Adapt<List<Roles>>())
            {
                FactoryId = loginBody.FactoryId,
            };

            CacheService.SetUserPerms(GlobalConstant.UserPermKEY + user.UserId, permissions);
            return SUCCESS(JwtUtil.GenerateJwtToken(JwtUtil.AddClaims(loginUser)));
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [Log(Title = "注销")]
        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            //Task.Run(async () =>
            //{
            //    //注销登录的用户，相当于ASP.NET中的FormsAuthentication.SignOut
            //    await HttpContext.SignOutAsync();
            //}).Wait();
            var userid = HttpContext.GetUId();
            var name = HttpContext.GetName();

            CacheService.RemoveUserPerms(GlobalConstant.UserPermKEY + userid);
            return SUCCESS(new { name, id = userid });
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [Verify]
        [HttpGet("getInfo")]
        public IActionResult GetUserInfo()
        {
            long userid = HttpContext.GetUId();
            var user = sysUserService.SelectUserById(userid);

            //前端校验按钮权限使用
            //角色集合 eg: admin,yunying,common
            List<string> roles = permissionService.GetRolePermission(user);
            //权限集合 eg *:*:*,system:user:list
            List<string> permissions = permissionService.GetMenuPermission(user);
            user.WelcomeContent = GlobalConstant.WelcomeMessages[new Random().Next(0, GlobalConstant.WelcomeMessages.Length)];
            string loginFactoryId = HttpContext.GetFactoryId();
            SysFactory factory = factoryService.GetById(loginFactoryId);
            return SUCCESS(new { user, roles, permissions, factory });
        }

        /// <summary>
        /// 获取路由信息
        /// </summary>
        /// <returns></returns>
        [Verify]
        [HttpGet("getRouters")]
        public IActionResult GetRouters()
        {
            long uid = HttpContext.GetUId();
            var menus = sysMenuService.SelectMenuTreeByUserId(uid);

            return SUCCESS(sysMenuService.BuildMenus(menus));
        }

        /// <summary>
        /// 生成图片验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("captchaImage")]
        public IActionResult CaptchaImage()
        {
            string uuid = Guid.NewGuid().ToString().Replace("-", "");

            SysConfig sysConfig = sysConfigService.GetSysConfigByKey("sys.account.captchaOnOff");
            var captchaOff = sysConfig?.ConfigValue ?? "0";
            var info = SecurityCodeHelper.Generate(uuid, 60);
            var obj = new { captchaOff, uuid, img = info.Base64 };// File(stream, "image/png")

            return SUCCESS(obj);
        }

        /// <summary>
        /// 获取RSA公钥
        /// </summary>
        /// <returns></returns>
        [HttpGet("RSAPublicKey")]
        public IActionResult GetRSAPublicKey()
        {
            RSASecurityConfig rsaSecurityConfig = AppSettings.Get<RSASecurityConfig>("RSASecurityConfig");
            string publicKey = $"-----BEGIN PUBLIC KEY-----{Environment.NewLine}{rsaSecurityConfig.PublicKey}{Environment.NewLine}-----END PUBLIC KEY-----";
            return SUCCESS(publicKey);
        }

        /// <summary>
        /// 切换登入厂区
        /// </summary>
        /// <param name="factoryId">登录厂区</param>
        /// <returns></returns>
        [HttpPost("SwitchFactory/{factoryId}")]
        [Log(Title = "切换登入厂区")]
        [Verify]
        public IActionResult SwitchFactory(string factoryId)
        {
            if (string.IsNullOrEmpty(factoryId))
            {
                throw new CustomException("未传递厂区参数");
            }
            var userid = HttpContext.GetUId();
            SysUser user = sysUserService.GetById(userid);
            if (!userFactoryService.CheckUserFactoryPermission(user.UserName, factoryId))
            {
                throw new CustomException($"当前用户没有厂区{factoryId}的权限");
            }

            List<SysRole> roles = roleService.SelectUserRoleListByUserId(user.UserId);
            //权限集合 eg *:*:*,system:user:list
            List<string> permissions = permissionService.GetMenuPermission(user);

            TokenModel loginUser = new(user.Adapt<TokenModel>(), roles.Adapt<List<Roles>>())
            {
                FactoryId = factoryId
            };

            CacheService.SetUserPerms(GlobalConstant.UserPermKEY + user.UserId, permissions);
            return SUCCESS(JwtUtil.GenerateJwtToken(JwtUtil.AddClaims(loginUser)));
        }

        #region 企业微信登录

        /// <summary>
        /// 企业微信code登录
        /// </summary>
        /// <param name="loginBody">登录对象</param>
        /// <returns></returns>
        [HttpPost("wxLogin")]
        [Log(Title = "微信登录")]
        public IActionResult WxLogin([FromBody] WxLoginDto loginBody)
        {
            if (string.IsNullOrEmpty(loginBody.Code)) { throw new CustomException("请求参数错误"); }

            SysUser user = sysLoginService.WxLogin(loginBody.Code);
            sysLoginService.CheckLockUser(user.UserName);

            //检查登入厂区（当登入时没有传厂区或传入的厂区权限时，默认登入第一个有权限的厂区）
            if (string.IsNullOrEmpty(loginBody.FactoryId) || !userFactoryService.CheckUserFactoryPermission(user.UserName, loginBody.FactoryId))
            {
                // 获取用户的可登入厂区
                List<SysFactory> factorys = userFactoryService.GetUserFactorysByUserName(user.UserName);
                //检查是否有厂区的登入权限
                if (factorys == null || factorys.Count <= 0)
                    throw new CustomException("用户没有可登入的厂区");
                else
                    loginBody.FactoryId = factorys[0].FactoryId;
            }

            List<SysRole> roles = roleService.SelectUserRoleListByUserId(user.UserId);
            //权限集合 eg *:*:*,system:user:list
            List<string> permissions = permissionService.GetMenuPermission(user);

            TokenModel loginUser = new(user.Adapt<TokenModel>(), roles.Adapt<List<Roles>>())
            {
                FactoryId = loginBody.FactoryId
            };

            CacheService.SetUserPerms(GlobalConstant.UserPermKEY + user.UserId, permissions);
            return SUCCESS(JwtUtil.GenerateJwtToken(JwtUtil.AddClaims(loginUser)));
        }

        #endregion 企业微信登录

        #region OA注册

        /// <summary>
        /// OA用户注册
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("/register")]
        [AllowAnonymous]
        [Log(Title = "注册", BusinessType = BusinessType.INSERT)]
        public IActionResult Register([FromBody] OAUserRegisterDto dto)
        {
            SysConfig config = sysConfigService.GetSysConfigByKey("sys.account.register");
            if (config?.ConfigValue != "true")
            {
                return ToResponse(ResultCode.CUSTOM_ERROR, "当前系统没有开启注册功能！");
            }
            SysConfig sysConfig = sysConfigService.GetSysConfigByKey("sys.account.captchaOnOff");
            if (sysConfig?.ConfigValue != "off" && !SecurityCodeHelper.Validate(dto.Uuid, dto.Code))
            {
                return ToResponse(ResultCode.CAPTCHA_ERROR, "验证码错误");
            }
            dto.UserIP = HttpContext.GetClientUserIp();

            //通过code获取缓存的微信用户id
            if (!string.IsNullOrEmpty(dto.WxCode))
            {
                dto.WxUserId = CacheService.GetWxCode(dto.WxCode)?.ToString();
            }

            //解析OA密码
            RSASecurityConfig rsaSecurityConfig = AppSettings.Get<RSASecurityConfig>("RSASecurityConfig");
            string privateKey = $"-----BEGIN PRIVATE KEY-----{Environment.NewLine}{rsaSecurityConfig.PrivateKey}{Environment.NewLine}-----END PRIVATE KEY-----";
            dto.Password = Encoding.Default.GetString(NETCore.Encrypt.EncryptProvider.RSADecryptWithPem(privateKey, Convert.FromBase64String(dto.Password)));

            SysUser user = sysUserService.OAUserRegister(dto);
            if (user.UserId > 0)
            {
                user.Password = null;
                return SUCCESS(true);
            }
            return ToResponse(ResultCode.CUSTOM_ERROR, "注册失败，请联系管理员");
        }

        #endregion OA注册

        #region 二维码登录

        ///// <summary>
        ///// 生成二维码
        ///// </summary>
        ///// <param name="uuid"></param>
        ///// <param name="deviceId"></param>
        ///// <returns></returns>
        //[HttpGet("/GenerateQrcode")]
        //public IActionResult GenerateQrcode(string uuid, string deviceId)
        //{
        //    var state = Guid.NewGuid().ToString();
        //    var dict = new Dictionary<string, object>
        //    {
        //        { "state", state }
        //    };
        //    CacheService.SetScanLogin(uuid, dict);
        //    return SUCCESS(new
        //    {
        //        status = 1,
        //        state,
        //        uuid,
        //        codeContent = new { uuid, deviceId }// "https://qm.qq.com/cgi-bin/qm/qr?k=kgt4HsckdljU0VM-0kxND6d_igmfuPlL&authKey=r55YUbruiKQ5iwC/folG7KLCmZ++Y4rQVgNlvLbUniUMkbk24Y9+zNuOmOnjAjRc&noverify=0"
        //    });
        //}

        ///// <summary>
        ///// 轮询判断扫码状态
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost("/VerifyScan")]
        ////[AllowAnonymous]
        //public IActionResult VerifyScan([FromBody] ScanDto dto)
        //{
        //    int status = -1;
        //    object token = string.Empty;
        //    if (CacheService.GetScanLogin(dto.Uuid) is Dictionary<string, object> str)
        //    {
        //        status = 0;
        //        str.TryGetValue("token", out token);
        //        if (str.ContainsKey("status") && (string)str.GetValueOrDefault("status") == "success")
        //        {
        //            status = 2;//扫码成功
        //            CacheService.RemoveScanLogin(dto.Uuid);
        //        }
        //    }

        //    return SUCCESS(new { status, token });
        //}

        ///// <summary>
        ///// 移动端扫码登录
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost("/ScanLogin")]
        //[Log(Title = "扫码登录")]
        //[Verify]
        //public IActionResult ScanLogin([FromBody] ScanDto dto)
        //{
        //    if (dto == null) { return ToResponse(ResultCode.CUSTOM_ERROR, "扫码失败"); }
        //    var name = App.HttpContext.GetName();

        //    sysLoginService.CheckLockUser(name);

        //    TokenModel tokenModel = JwtUtil.GetLoginUser(HttpContext);
        //    if (CacheService.GetScanLogin(dto.Uuid) is not null)
        //    {
        //        Dictionary<string, object> dict = new()
        //        {
        //            { "status", "success" },
        //            { "token", JwtUtil.GenerateJwtToken(JwtUtil.AddClaims(tokenModel)) }
        //        };
        //        CacheService.SetScanLogin(dto.Uuid, dict);

        //        return SUCCESS(1);
        //    }
        //    return ToResponse(ResultCode.FAIL, "二维码已失效");
        //}

        #endregion 二维码登录

        #region 无效

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost("checkMobile")]
        //[Log(Title = "发送短息", BusinessType = BusinessType.INSERT)]
        //public IActionResult CheckMobile([FromBody] PhoneLoginDto dto)
        //{
        //    dto.LoginIP = HttpContextExtension.GetClientUserIp(HttpContext);

        //    SysConfig sysConfig = sysConfigService.GetSysConfigByKey("sys.account.captchaOnOff");
        //    if (sysConfig?.ConfigValue != "off" && !SecurityCodeHelper.Validate(dto.Uuid, dto.Code, false))
        //    {
        //        return ToResponse(ResultCode.CUSTOM_ERROR, "验证码错误");
        //    }
        //    string location = HttpContextExtension.GetIpInfo(dto.LoginIP);
        //    var info = sysUserService.GetFirst(f => f.Phonenumber == dto.PhoneNum) ?? throw new CustomException(ResultCode.CUSTOM_ERROR, "该手机号不存在", false);

        //    var smsCode = RandomHelper.GenerateNum(6);
        //    var smsContent = $"验证码{smsCode}（随机验证码）,有效期10分钟。";
        //    //TODO 发送短息验证码,1分钟内允许一次
        //    smsCodeLogService.AddSmscodeLog(new ServiceCore.Model.SmsCodeLog()
        //    {
        //        Userid = info.UserId,
        //        PhoneNum = dto.PhoneNum.ParseToLong(),
        //        AddTime = DateTime.Now,
        //        SendType = 1,
        //        SmsCode = smsCode,
        //        SmsContent = smsContent,
        //        UserIP = dto.LoginIP,
        //        Location = location,
        //    });
        //    CacheService.SetPhoneCode(dto.PhoneNum, smsCode);

        //    return SUCCESS(new { smsCode });
        //}

        ///// <summary>
        ///// 手机号登录
        ///// </summary>
        ///// <param name="loginBody">登录对象</param>
        ///// <returns></returns>
        //[Route("PhoneLogin")]
        //[HttpPost]
        //[Log(Title = "手机号登录")]
        //public IActionResult PhoneLogin([FromBody] PhoneLoginDto loginBody)
        //{
        //    if (loginBody == null) { throw new CustomException("请求参数错误"); }
        //    loginBody.LoginIP = HttpContextExtension.GetClientUserIp(HttpContext);

        //    if (!CacheService.CheckPhoneCode(loginBody.PhoneNum, loginBody.PhoneCode))
        //    {
        //        return ToResponse(ResultCode.CUSTOM_ERROR, "短信验证码错误");
        //    }
        //    var info = sysUserService.GetFirst(f => f.Phonenumber == loginBody.PhoneNum) ?? throw new CustomException(ResultCode.CUSTOM_ERROR, "该手机号不存在", false);
        //    sysLoginService.CheckLockUser(info.UserName);
        //    string location = HttpContextExtension.GetIpInfo(loginBody.LoginIP);
        //    var user = sysLoginService.PhoneLogin(loginBody, new SysLogininfor() { LoginLocation = location }, info);

        //    List<SysRole> roles = roleService.SelectUserRoleListByUserId(user.UserId);
        //    //权限集合 eg *:*:*,system:user:list
        //    List<string> permissions = permissionService.GetMenuPermission(user);

        //    TokenModel loginUser = new(user.Adapt<TokenModel>(), roles.Adapt<List<Roles>>());
        //    CacheService.SetUserPerms(GlobalConstant.UserPermKEY + user.UserId, permissions);
        //    return SUCCESS(JwtUtil.GenerateJwtToken(JwtUtil.AddClaims(loginUser)));
        //}

        #endregion 无效
    }
}
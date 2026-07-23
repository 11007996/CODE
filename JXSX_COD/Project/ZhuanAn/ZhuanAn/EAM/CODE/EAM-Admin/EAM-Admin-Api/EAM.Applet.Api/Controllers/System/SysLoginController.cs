using EAM.Model.System;
using EAM.Model.System.Dto;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EAM.Applet.Api.Controllers.System
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
        /// 登录
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
            SysUser user;
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
    }
}
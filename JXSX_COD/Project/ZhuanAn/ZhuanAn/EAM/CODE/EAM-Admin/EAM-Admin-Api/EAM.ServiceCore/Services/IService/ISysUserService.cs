using EAM.Model;
using EAM.Model.System;
using EAM.Model.System.Dto;

namespace EAM.ServiceCore.Services
{
    public interface ISysUserService : IBaseService<SysUser>
    {
        public PagedInfo<SysUser> SelectUserList(SysUserQueryDto user, PagerInfo pager);

        /// <summary>
        /// 通过用户ID查询用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysUser SelectUserById(long userId);

        /// <summary>
        /// 通过用户UserName查询用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        SysUser SelectUserByName(string username);

        /// <summary>
        /// 校验用户名称是否唯一
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CheckUserNameUnique(string userName);

        /// <summary>
        /// 新增保存用户信息
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public SysUser InsertUser(SysUser sysUser);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpdateUser(SysUser user);

        public int ChangeUser(SysUser user);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int ResetPwd(long userid, string password);

        public int ChangeUserStatus(SysUser user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteUser(long userid);

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpdatePhoto(SysUser user);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        SysUser Register(RegisterDto dto);

        /// <summary>
        /// OA用户注册
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        SysUser OAUserRegister(OAUserRegisterDto dto);

        /// <summary>
        /// 系统用户绑定企业微信用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="wxUserId"></param>
        /// <returns></returns>
        bool BindWxUser(string username, string wxUserId);

        void CheckUserAllowed(SysUser user);

        void CheckUserDataScope(long userid, long loginUserId);

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        (string, object, object) ImportUsers(List<SysUser> users);

        /// <summary>
        /// 同步员工信息到系统用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SyncEmployeeToSysUser(EmployeeToSysUserDto model);

        /// <summary>
        /// 同步系统用户到员工信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SyncSysUserToEmployee(SysUserToEmployeeDto model);

        SysUser Login(LoginBodyDto user);

        void UpdateLoginInfo(string userIP, long userId);

        List<string> ExtractUsersByUserType(List<string> userNames, string userType);
    }
}
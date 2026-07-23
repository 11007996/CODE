using EAM.Common;
using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Constant;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.Model.System.Dto;
using EAM.Repository;
using EAM.ServiceCore.Model;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Enums;
using IPTools.Core;
using SqlSugar.IOC;
using System.Collections;
using System.Text.RegularExpressions;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [AppService(ServiceType = typeof(ISysUserService), ServiceLifetime = LifeTime.Transient)]
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        private readonly ISysRoleService RoleService;
        private readonly ISysUserRoleService UserRoleService;
        private readonly ISysUserPostService UserPostService;
        private readonly ISysUserFactoryService UserFactoryService;

        public SysUserService(
            ISysRoleService sysRoleService,
            ISysUserRoleService userRoleService,
            ISysUserPostService userPostService,
            ISysUserFactoryService userFactoryService)
        {
            RoleService = sysRoleService;
            UserRoleService = userRoleService;
            UserPostService = userPostService;
            UserFactoryService = userFactoryService;
        }

        /// <summary>
        /// 根据条件分页查询用户列表
        /// </summary>
        /// <returns></returns>
        public PagedInfo<SysUser> SelectUserList(SysUserQueryDto user, PagerInfo pager)
        {
            var exp = Expressionable.Create<SysUser>();
            exp.AndIF(!string.IsNullOrEmpty(user.UserName), u => u.UserName.Contains(user.UserName));
            exp.AndIF(!string.IsNullOrEmpty(user.NickName), u => u.NickName.Contains(user.NickName));
            exp.AndIF(user.UserId > 0, u => u.UserId == user.UserId);
            exp.AndIF(user.Status != -1, u => u.Status == user.Status);
            exp.AndIF(user.BeginTime != DateTime.MinValue && user.BeginTime != null, u => u.Create_time >= user.BeginTime);
            exp.AndIF(user.EndTime != DateTime.MinValue && user.EndTime != null, u => u.Create_time <= user.EndTime);
            exp.AndIF(!user.Phonenumber.IsEmpty(), u => u.Phonenumber == user.Phonenumber);
            exp.And(u => u.DelFlag == (int)DeleteFlagEnum.存在);

            if (user.DeptId != null && user.DeptId != 0)
            {
                var allChildDepts = Context.Queryable<SysDept>().ToChildList(it => it.ParentId, user.DeptId);

                exp.And(u => allChildDepts.Select(f => f.DeptId).ToList().Contains(u.DeptId));
            }
            var query = Queryable()
                .LeftJoin<SysDept>((u, dept) => u.DeptId == dept.DeptId)
                .Where(exp.ToExpression())
                .Select((u, dept) => new SysUser
                {
                    UserId = u.UserId.SelectAll(),
                    DeptName = dept.DeptName,
                });

            return query.ToPage(pager);
        }

        /// <summary>
        /// 通过用户ID查询用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysUser SelectUserById(long userId)
        {
            var user = Queryable().Filter(null, true).WithCache(60 * 5)
                .Where(f => f.UserId == userId).First();
            if (user != null && user.UserId > 0)
            {
                user.Roles = RoleService.SelectUserRoleListByUserId(userId);
                user.RoleIds = user.Roles.Select(x => x.RoleId).ToArray();
                user.FactoryIds = UserFactoryService.GetUserFactorysByUserName(user.UserName).Select(it => it.FactoryId).ToArray();
            }
            return user;
        }

        /// <summary>
        /// 校验用户名称是否唯一
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CheckUserNameUnique(string userName)
        {
            int count = Count(it => it.UserName == userName);
            if (count > 0)
            {
                return UserConstants.NOT_UNIQUE;
            }
            return UserConstants.UNIQUE;
        }

        /// <summary>
        /// 新增保存用户信息
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public SysUser InsertUser(SysUser sysUser)
        {
            var result = UseTran(() =>
            {
                sysUser.UserId = Insertable(sysUser).ExecuteReturnIdentity();
                //新增用户角色信息
                UserRoleService.InsertUserRole(sysUser);
                //新增用户岗位信息
                UserPostService.InsertUserPost(sysUser);
                //增增用户厂区信息
                UserFactoryService.InsertUserFactory(sysUser);
            });

            if (!result.IsSuccess)
            {
                throw new Exception("提交数据异常," + result.ErrorMessage, result.ErrorException);
            }

            return sysUser;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpdateUser(SysUser user)
        {
            var roleIds = RoleService.SelectUserRoles(user.UserId);
            var diffArr = roleIds.Where(c => !((IList)user.RoleIds).Contains(c)).ToArray();
            var diffArr2 = user.RoleIds.Where(c => !((IList)roleIds).Contains(c)).ToArray();
            bool result = UseTran2(() =>
            {
                if (diffArr.Length > 0 || diffArr2.Length > 0)
                {
                    //删除用户与角色关联
                    UserRoleService.DeleteUserRoleByUserId((int)user.UserId);
                    //新增用户与角色关联
                    UserRoleService.InsertUserRole(user);
                }
                // 删除用户与岗位关联
                UserPostService.Delete(user.UserId);
                // 新增用户与岗位管理
                UserPostService.InsertUserPost(user);

                // 删除用户与厂区关联
                UserFactoryService.Delete(user.UserId);
                // 新增用户与厂区管理
                UserFactoryService.InsertUserFactory(user);
                ChangeUser(user);
            });
            return result ? 1 : 0;
        }

        public int ChangeUser(SysUser user)
        {
            user.Update_time = DateTime.Now;
            return Update(user, t => new
            {
                t.NickName,
                t.Email,
                t.Phonenumber,
                t.DeptId,
                t.Status,
                t.Sex,
                t.PostIds,
                t.Remark,
                t.Update_by,
                t.Update_time
            }, true);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int ResetPwd(long userid, string password)
        {
            return Update(new SysUser() { UserId = userid, Password = password }, it => new { it.Password }, f => f.UserId == userid);
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int ChangeUserStatus(SysUser user)
        {
            CheckUserAllowed(user);
            return Update(user, it => new { it.Status }, f => f.UserId == user.UserId);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteUser(long userid)
        {
            CheckUserAllowed(new SysUser() { UserId = userid });
            bool result = UseTran2(() =>
            {
                //删除用户与角色关联
                UserRoleService.DeleteUserRoleByUserId((int)userid);
                // 删除用户与岗位关联
                UserPostService.Delete(userid);
                Update(new SysUser() { UserId = userid, DelFlag = (int)DeleteFlagEnum.删除 }, it => new { it.DelFlag }, f => f.UserId == userid);
            });
            return result ? 1 : 0;
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpdatePhoto(SysUser user)
        {
            return Update(user, it => new { it.Avatar }, f => f.UserId == user.UserId); ;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public SysUser Register(RegisterDto dto)
        {
            if (!Tools.PasswordStrength(dto.Password))
            {
                throw new CustomException("密码强度不符合要求");
            }
            if (!Tools.CheckUserName(dto.Username))
            {
                throw new CustomException("用户名不符合要求");
            }
            //密码md5
            string password = NETCore.Encrypt.EncryptProvider.Md5(dto.Password);
            var ip_info = IpTool.Search(dto.UserIP);
            SysUser user = new()
            {
                Create_time = DateTime.Now,
                UserName = dto.Username,
                NickName = dto.Username,
                Password = password,
                Status = 0,
                DeptId = 0,
                Remark = "用户注册",
                Province = ip_info.Province,
                City = ip_info.City
            };
            if (UserConstants.NOT_UNIQUE.Equals(CheckUserNameUnique(dto.Username)))
            {
                throw new CustomException($"保存用户{dto.Username}失败，注册账号已存在");
            }
            user.UserId = Insertable(user).ExecuteReturnIdentity();
            return user;
        }

        /// <summary>
        /// 注册用户(OA账号密码注册)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public SysUser OAUserRegister(OAUserRegisterDto dto)
        {
            dto.Username = dto.Username.Trim().ToUpper();
            if (!Regex.IsMatch(dto.Username, @"^[a-zA-Z0-9]*$"))
            {
                throw new CustomException("用户名不符合要求");
            }

            //检查OA账号密码是否正确
            OALoginResult res = LuxshareHelper.OALogin(dto.Username, dto.Password);
            if (!res.IsSuccess)
            {
                throw new CustomException("OA账号密码校验失败");
            }

            //检查用户名是否已存在
            SysUser sysUser = SelectUserByName(dto.Username);
            if (sysUser != null)
                throw new CustomException($"已存在用户名【{dto.Username}】,无法注册。");

            //获取集团员工信息
            LuxEmpService luxEmpService = new LuxEmpService();
            LuxEmp luxEmp = luxEmpService.GetInfo(dto.Username);
            if (luxEmp == null)
                throw new CustomException($"当前用户【{dto.Username}】未在通讯录中,无法注册,需要管理人员手动添加。");
            if (luxEmp.LeaveDate != null)
                throw new CustomException($"当前用户【{dto.Username}】已离职,无法注册。");

            //根据部门配置获取默认厂区
            SysDeptExpand deptExpand = Context.Queryable<SysDeptExpand>().Where(it => it.LuxDeptId == luxEmp.DeptCode).First();
            if (deptExpand == null || string.IsNullOrEmpty(deptExpand.DefaultFactoryId))
                throw new CustomException($"当前用户【{dto.Username}】所在立讯部门【{luxEmp.DeptCode}】未维护默认厂区,请联系管理员配置。");

            //检查工厂ID
            SysFactory sysFactory = Context.Queryable<SysFactory>().Where(it => it.FactoryId == deptExpand.DefaultFactoryId).First();
            if (sysFactory == null)
                throw new CustomException($"未找到部门配置的默认工厂【{deptExpand.DefaultFactoryId}】信息，请联系管理员处理");

            //将用户添加到系统
            SysUser user = new SysUser();
            user.Create_by = dto.Username;
            user.Create_time = DateTime.Now;
            user.DelFlag = (int)DeleteFlagEnum.存在;
            user.NickName = luxEmp.EmpName;
            user.UserName = dto.Username;
            user.DeptId = deptExpand.SysDeptId;
            user.Password = NETCore.Encrypt.EncryptProvider.Md5(dto.Password);
            user.UserType = UserTypeConstant.OA用户;
            user.FactoryIds = new string[] { sysFactory.FactoryId };
            //厂区默认角色
            if (sysFactory.DefaultRoleId > 1)
                user.RoleIds = new long[] { sysFactory.DefaultRoleId.Value };
            InsertUser(user);

            //检查对应厂区是否有员工信息
            try
            {
                // 在对应厂区ID的数据库添加员工信息
                var factoryDB = DbScoped.SugarScope.GetConnectionScope(sysFactory.FactoryId);//根据类传入的ConfigId自动选择
                Employee employee = factoryDB.Queryable<Employee>().Where(it => it.EmpCode == dto.Username).First();
                if (employee == null)
                {
                    employee = new Employee();
                    employee.EmpCode = dto.Username;
                    employee.EmpName = luxEmp.EmpName;
                    employee.DeptId = deptExpand.SysDeptId;
                    employee.Status = 0;
                    employee.DelFlag = (int)DeleteFlagEnum.存在;
                    employee.CreateBy = dto.Username;
                    employee.CreateTime = DateTime.Now;
                    factoryDB.Insertable(employee).ExecuteCommand();
                }

                //关联微信ID
                if (!string.IsNullOrEmpty(dto.WxUserId))
                {
                    BindWxUser(dto.Username, dto.WxUserId);
                }
            }
            catch (Exception)
            {
            }

            return user;
        }

        /// <summary>
        /// 系统用户绑定企业微信用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="wxUserId"></param>
        public bool BindWxUser(string username, string wxUserId)
        {
            SysUser user = SelectUserByName(username);
            if (user == null)
                throw new CustomException($"未找到用户【{username}】的信息");

            //检查用户是否已有关联企业微信用户
            WxUser wxuser = Context.Queryable<WxUser>().Where(it => it.sys_username == username).First();
            if (wxuser != null)
            {
                if (wxuser.userid == wxUserId)
                    throw new CustomException("用户已绑定微信,请匆重复操作");
                else
                    throw new CustomException("此用户与存在绑定企业微信信息");
            }

            //是否存在企业微信用户信息
            wxuser = Context.Queryable<WxUser>().Where(it => it.userid == wxUserId).First();
            if (wxuser == null)
                throw new CustomException("未找到指定企业微信用户信息");

            //当前要绑定的企业用户是否已有其他用户绑定
            if (!string.IsNullOrEmpty(wxuser.sys_username))
                throw new CustomException($"当前微信用户已存在与系统用户【{wxuser.sys_username}】关联");

            //系统用户绑定企业微信用户
            wxuser.sys_username = username;
            return Context.Updateable(wxuser).UpdateColumns(it => it.sys_username).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 校验角色是否允许操作
        /// </summary>
        /// <param name="user"></param>
        public void CheckUserAllowed(SysUser user)
        {
            if (user.IsAdmin)
            {
                throw new CustomException("不允许操作超级管理员角色");
            }
        }

        /// <summary>
        /// 校验用户是否有数据权限
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="loginUserId"></param>
        public void CheckUserDataScope(long userid, long loginUserId)
        {
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public (string, object, object) ImportUsers(List<SysUser> users)
        {
            users.ForEach(x =>
            {
                x.Create_time = DateTime.Now;
                x.Status = 0;
                x.DelFlag = (int)DeleteFlagEnum.存在;
                x.Password = "E10ADC3949BA59ABBE56E057F20F883E";
                x.Remark = x.Remark.IsEmpty() ? "数据导入" : x.Remark;
            });
            var x = Context.Storageable(users)
                .SplitInsert(it => !it.Any())
                .SplitIgnore(it => it.Item.UserName == GlobalConstant.AdminRole)
                .SplitError(x => x.Item.UserName.IsEmpty(), "用户名不能为空")
                .SplitError(x => !Tools.CheckUserName(x.Item.UserName), "用户名不符合规范")
                .WhereColumns(it => it.UserName)//如果不是主键可以这样实现（多字段it=>new{it.x1,it.x2}）
                .ToStorage();
            var result = x.AsInsertable.ExecuteCommand();//插入可插入部分;

            string msg = string.Format(" 插入{0} 更新{1} 错误数据{2} 不计算数据{3} 删除数据{4} 总共{5}",
                               x.InsertList.Count,
                               x.UpdateList.Count,
                               x.ErrorList.Count,
                               x.IgnoreList.Count,
                               x.DeleteList.Count,
                               x.TotalList.Count);
            //输出统计
            Console.WriteLine(msg);

            //输出错误信息
            foreach (var item in x.ErrorList)
            {
                Console.WriteLine("userName为" + item.Item.UserName + " : " + item.StorageMessage);
            }
            foreach (var item in x.IgnoreList)
            {
                Console.WriteLine("userName为" + item.Item.UserName + " : " + item.StorageMessage);
            }

            return (msg, x.ErrorList, x.IgnoreList);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">登录实体</param>
        /// <returns></returns>
        public SysUser Login(LoginBodyDto user)
        {
            return GetFirst(it => it.UserName == user.Username && it.Password.ToLower() == user.Password.ToLower());
        }

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="username">登录实体</param>
        /// <returns></returns>
        public SysUser SelectUserByName(string username)
        {
            return GetFirst(it => it.UserName == username);
        }

        /// <summary>
        /// 修改登录信息
        /// </summary>
        /// <param name="userIP"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void UpdateLoginInfo(string userIP, long userId)
        {
            Update(new SysUser() { LoginIP = userIP, LoginDate = DateTime.Now, UserId = userId }, it => new { it.LoginIP, it.LoginDate });
        }

        /// <summary>
        /// 根据用户类型提取用户名
        /// </summary>
        /// <param name="userNames">用户名集合</param>
        /// <param name="userType">用户类型</param>
        /// <returns></returns>
        public List<string> ExtractUsersByUserType(List<string> userNames, string userType)
        {
            if (userNames == null || userType == null) { return null; }
            return Context.Queryable<SysUser>().Where(it => it.UserType == userType && userNames.Contains(it.UserName)).Select(it => it.UserName).ToList();
        }

        /// <summary>
        /// 厂区员工操作同到到系统用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SyncEmployeeToSysUser(EmployeeToSysUserDto model)
        {
            //获取用户
            SysUser user = Queryable().ClearFilter().Where(it => it.UserName == model.UserName).First();

            //获取工厂
            SysFactory factory = Context.Queryable<SysFactory>().Where(it => it.FactoryId == model.FactoryId).First();
            if (factory == null)
                throw new CustomException($"非找到工厂【${model.FactoryId}】，无法将员工信息同步到系统用户");

            //是否存在其他关联的工厂
            bool hasOtherFactory = false;
            if (user != null)
                hasOtherFactory = Context.Queryable<SysUserFactory>().Where(it => it.UserId == user.UserId && it.FactoryId != factory.FactoryId).Any();

            //只能操作OA类型用户
            if (user != null && user.UserType != UserTypeConstant.OA用户)
                throw new CustomException("非OA用户，不可操作");

            DbResult<bool> r = UseTran(() =>
            {
                if (model.BusinessType == BusinessType.INSERT || model.BusinessType == BusinessType.UPDATE)
                {//新增员工
                    if (user == null)
                    {//不存在系统用户，自动创建
                     //增加系统用户信息
                        user = new SysUser
                        {
                            UserName = model.UserName,
                            NickName = model.NickName,
                            UserType = UserTypeConstant.OA用户,
                            DeptId = (long)model.DeptId,
                            Status = model.Status,
                            DelFlag = (int)DeleteFlagEnum.存在,
                            Create_by = model.OperateBy,
                            Create_time = DateTime.Now,
                            Password = NETCore.Encrypt.EncryptProvider.Md5(string.Concat(model.UserName, model.UserName.AsSpan(0, 3))),//密码默认为工号
                            RoleIds = factory == null || factory.DefaultRoleId == null ? null : new long[] { factory.DefaultRoleId.Value },//厂区默认角色
                            PostIds = model.PostIds,
                            FactoryIds = new string[] { factory.FactoryId }
                        };

                        InsertUser(user);
                    }
                    else
                    {//存在系统用户，
                     //更新用户信息
                        if (hasOtherFactory)
                        {//存在其他厂区权限，只能修改部门和信息
                         //更新用户信息，防止其他厂删除操作
                            user.DeptId = (long)model.DeptId;
                            user.Update_by = model.OperateBy;
                            user.Update_time = DateTime.Now;
                            //user.Status = model.Status;
                        }
                        else
                        {//不存在其他厂区权限，可以修改所有信息
                            user.NickName = model.NickName;
                            user.DeptId = (long)model.DeptId;
                            user.Update_by = model.OperateBy;
                            user.Update_time = DateTime.Now;
                            user.Status = model.Status;
                        }
                        Update(user, true);

                        //更新岗位信息
                        Context.Deleteable<SysUserPost>().Where(it => it.UserId == user.UserId).ExecuteCommand();
                        if (model.PostIds.Any())
                        {
                            List<SysUserPost> posts = new List<SysUserPost>();
                            foreach (int postId in model.PostIds)
                            {
                                posts.Add(new SysUserPost() { UserId = user.UserId, PostId = postId });
                            }
                            Context.Insertable<SysUserPost>(posts).ExecuteCommand();
                        }

                        //角色信息
                        if (factory.DefaultRoleId > 1)//1为超级管理员，不允许给其他人。
                        {
                            bool hasRole = Context.Queryable<SysUserRole>().Where(it => it.UserId == user.UserId && it.RoleId == factory.DefaultRoleId).Any();
                            if (!hasRole)
                            {
                                Context.Insertable(new SysUserRole() { UserId = user.UserId, RoleId = factory.DefaultRoleId.Value }).ExecuteCommand();
                            }
                        }

                        //厂区关联
                        bool hasFactoryPerms = Context.Queryable<SysUserFactory>().Where(it => it.UserId == user.UserId && it.FactoryId == factory.FactoryId).Any();
                        if (!hasFactoryPerms && model.Status == 0)
                        {//可用
                            SysUserFactory suf = new SysUserFactory()
                            {
                                FactoryId = factory.FactoryId,
                                UserId = user.UserId
                            };
                            Context.Insertable(suf).ExecuteCommand();
                        }
                        else if (hasFactoryPerms && model.Status == 1)
                        {//停用
                            Context.Deleteable<SysUserFactory>().Where(it => it.UserId == user.UserId && it.FactoryId == factory.FactoryId).ExecuteCommand();
                        }
                    }
                }
                else if (model.BusinessType == BusinessType.DELETE)
                {//删除员工
                    if (!hasOtherFactory)
                    {//没有关联其他厂区，假删除用户
                        user.Update_by = model.OperateBy;
                        user.Update_time = DateTime.Now;
                        user.DelFlag = (int)DeleteFlagEnum.删除;
                        user.Status = (int)DisableStatusEnum.禁用;
                        Update(user, true);
                        //删除用户角色信息
                        Context.Deleteable<SysUserRole>().Where(it => it.UserId == user.UserId).ExecuteCommand();
                        //删除用户岗位信息
                        Context.Deleteable<SysUserPost>().Where(it => it.UserId == user.UserId).ExecuteCommand();
                    }

                    //解除用户关联到厂区
                    Context.Deleteable<SysUserFactory>().Where(it => it.UserId == user.UserId && it.FactoryId == factory.FactoryId).ExecuteCommand();
                }
            });

            return r.IsSuccess;
        }

        /// <summary>
        /// 同步系统用户到用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SyncSysUserToEmployee(SysUserToEmployeeDto model)
        {
            if (model.FactoryIds == null || model.FactoryIds.Count() == 0)
                throw new CustomException("未选择需要同步的厂区");
            if (model.UserNames == null || model.UserNames.Count() == 0)
                throw new CustomException("未选择需要同步的用户");

            foreach (string factoryId in model.FactoryIds)
            {
                try
                {
                    SqlSugarScopeProvider provider = DbScoped.SugarScope.GetConnectionScope(factoryId);
                    foreach (string username in model.UserNames)
                    {
                        SysUser user = Queryable().ClearFilter().Where(it => it.UserName == username).First();
                        Employee emp = provider.Queryable<Employee>().Where(it => it.EmpCode == username).First();
                        if (user != null)
                        {
                            if (emp == null)
                            {//不存在，新增
                                emp = new Employee();
                                emp.EmpCode = user.UserName;
                                emp.EmpName = user.NickName;
                                emp.Status = user.Status;
                                emp.DeptId = user.DeptId;
                                provider.Insertable(emp).ExecuteCommand();
                            }
                            else
                            {//存在，更新
                                emp.EmpName = user.NickName;
                                emp.Status = user.Status;
                                emp.DeptId = user.DeptId;
                                provider.Updateable(emp).ExecuteCommand();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new CustomException($"厂区【{factoryId}】同步失败");
                }
            }

            return true;
        }
    }
}
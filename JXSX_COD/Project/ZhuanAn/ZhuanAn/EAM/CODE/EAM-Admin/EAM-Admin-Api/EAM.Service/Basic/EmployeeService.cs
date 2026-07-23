using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.Model.System.Dto;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Basic
{
    /// <summary>
    /// 员工信息Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IEmployeeService), ServiceLifetime = LifeTime.Transient)]
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly ISysUserService _sysUserService;

        public EmployeeService(IHttpContextAccessor contextAccessor, ISysUserService sysUserService) : base(contextAccessor)
        {
            _sysUserService = sysUserService;
        }

        /// <summary>
        /// 查询员工信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EmployeeDto> GetList(EmployeeQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<Employee, EmployeeDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="empCode"></param>
        /// <returns></returns>
        public Employee GetInfo(string empCode)
        {
            var response = Queryable()
                .Where(x => x.EmpCode == empCode)
                .First();

            return response;
        }

        /// <summary>
        /// 添加员工信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Employee AddEmployee(Employee model)
        {
            model.EmpCode = model.EmpCode.Trim().ToUpper();
            model.DelFlag = (int)DeleteFlagEnum.存在;
            if (GetInfo(model.EmpCode) != null)
                throw new CustomException($"已存在工号【{model.EmpCode}】，请不要重复添加");

            //同步到主库
            EmployeeToSysUserDto dto = new EmployeeToSysUserDto()
            {
                FactoryId = Context.CurrentConnectionConfig.ConfigId.ToString(),
                UserName = model.EmpCode,
                OperateBy = model.CreateBy,
                BusinessType = BusinessType.INSERT,
                DeptId = model.DeptId,
                PostIds = model.PostIds,
                Status = model.Status,
                NickName = model.EmpName
            };
            if (!_sysUserService.SyncEmployeeToSysUser(dto))
            {
                throw new CustomException("员工信息同步到系统用户失败");
            }

            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateEmployee(Employee model)
        {
            //同步到主库
            EmployeeToSysUserDto dto = new EmployeeToSysUserDto()
            {
                FactoryId = Context.CurrentConnectionConfig.ConfigId.ToString(),
                UserName = model.EmpCode,
                OperateBy = model.UpdateBy,
                BusinessType = BusinessType.UPDATE,
                DeptId = model.DeptId,
                PostIds = model.PostIds,
                Status = model.Status,
                NickName = model.EmpName
            };
            if (!_sysUserService.SyncEmployeeToSysUser(dto))
            {
                throw new CustomException("员工信息同步到系统用户失败");
            }

            return Update(model, true);
        }

        public int DeleteEmployee(string empCode)
        {
            Employee emp = GetInfo(empCode);
            emp.DelFlag = (int)DeleteFlagEnum.删除;
            emp.Status = (int)DisableStatusEnum.禁用;

            //同步到主库
            EmployeeToSysUserDto dto = new EmployeeToSysUserDto()
            {
                FactoryId = Context.CurrentConnectionConfig.ConfigId.ToString(),
                UserName = emp.EmpCode,
                BusinessType = BusinessType.DELETE,
            };
            if (!_sysUserService.SyncEmployeeToSysUser(dto))
            {
                throw new CustomException("员工信息同步到系统用户失败");
            }

            return Update(emp, true);
        }

        /// <summary>
        /// 获取人员字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(EmployeeQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .OrderBy(it => it.EmpCode)
                .Select(it => new DictDataDto()
                {
                    DictValue = it.EmpCode,
                    DictLabel = it.EmpName
                })
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 将系统用户信息，同步到员工信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Employee AddEmployeeFromUser(string username)
        {
            Employee emp = GetInfo(username);
            if (emp != null)
            {
                throw new CustomException($"已存在员工【{username}】的信息。");
            }

            SysUser user = _sysUserService.SelectUserByName(username);
            if (user == null)
                throw new CustomException($"未找到的用户【{username}】的信息。");

            emp = new Employee
            {
                EmpCode = user.UserName,
                EmpName = user.NickName,
                DeptId = user.DeptId,
                DelFlag = user.DelFlag,
                Status = user.Status,
                CreateTime = DateTime.Now,
                CreateBy = username,
            };

            return Insertable(emp).ExecuteReturnEntity();
        }

        /// <summary>
        /// 导入员工信息
        /// </summary>
        /// <returns></returns>
        public (string, object, object) ImportEmployee(List<Employee> list)
        {
            List<StorageableMessage<Employee>> insertList = new List<StorageableMessage<Employee>>();
            List<StorageableMessage<Employee>> ignoreList = new List<StorageableMessage<Employee>>();
            List<StorageableMessage<Employee>> errorList = new List<StorageableMessage<Employee>>();
            foreach (Employee emp in list)
            {
                try
                {
                    Employee oldEmp = Queryable().Where(x => x.EmpCode == emp.EmpCode).First();
                    if (oldEmp == null)
                    {
                        AddEmployee(emp);
                        insertList.Add(new StorageableMessage<Employee>() { StorageMessage = emp.EmpCode + "成功", Item = emp });
                    }
                    else
                        ignoreList.Add(new StorageableMessage<Employee>() { StorageMessage = emp.EmpCode + "已存在", Item = emp });
                }
                catch (Exception)
                {
                    ignoreList.Add(new StorageableMessage<Employee>() { StorageMessage = emp.EmpCode + "错误", Item = emp });
                }
            }

            string msg = $"插入{insertList.Count} 忽略{ignoreList.Count} 错误{errorList.Count} 总共{list.Count}";
            Console.WriteLine(msg);

            return (msg, errorList, ignoreList);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<Employee> QueryExp(EmployeeQueryDto parm)
        {
            var predicate = Expressionable.Create<Employee>();

            predicate.And(it => it.DelFlag == (int)DeleteFlagEnum.存在);
            predicate.AndIF(!string.IsNullOrEmpty(parm.EmpCode), it => it.EmpCode == parm.EmpCode);
            predicate.AndIF(!string.IsNullOrEmpty(parm.EmpName), it => it.EmpName.Contains(parm.EmpName));
            predicate.AndIF(parm.DeptId > 0, it => it.DeptId == parm.DeptId);
            predicate.AndIF(!string.IsNullOrEmpty(parm.Keyword), it => it.EmpCode.Contains(parm.Keyword) || it.EmpName.Contains(parm.Keyword));

            return predicate;
        }
    }
}
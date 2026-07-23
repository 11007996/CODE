using Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace EAM.Model.System.Dto
{
    public class SysUserDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string Phonenumber { get; set; }

        /// <summary>
        /// 用户性别（0男 1女 2未知）
        /// </summary>
        public int Sex { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 帐号状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LoginIP { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 角色id集合
        /// </summary>
        public long[] RoleIds { get; set; }

        /// <summary>
        /// 岗位集合
        /// </summary>
        public int[] PostIds { get; set; }

        /// <summary>
        /// 厂区集合
        /// </summary>
        public string[] FactoryIds { get; set; }
    }

    public class SysUserQueryDto
    {
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string Phonenumber { get; set; }

        /// <summary>
        /// 用户性别（0男 1女 2未知）
        /// </summary>
        public int Sex { get; set; }

        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Status { get; set; }
        public long? DeptId { get; set; }
    }

    /// <summary>
    /// 员工信息操作同步到系统用户dto
    /// </summary>
    public class EmployeeToSysUserDto
    {
        [Required(ErrorMessage = "同步参数：厂区不能为空")]
        public string FactoryId { get; set; }

        [Required(ErrorMessage = "同步参数：用户名不能为空")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "同步参数：业务类型不能为空")]
        public BusinessType BusinessType { get; set; }

        public string OperateBy { get; set; }

        public string NickName { get; set; }

        public long? DeptId { get; set; }

        public int[] PostIds { get; set; }

        public int Status { get; set; }
    }

    /// <summary>
    /// 系统用户同步到员工信息
    /// </summary>
    public class SysUserToEmployeeDto
    {
        /// <summary>
        /// 厂区Id
        /// </summary>
        public string[] FactoryIds { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string[] UserNames { get; set; }
    }

}
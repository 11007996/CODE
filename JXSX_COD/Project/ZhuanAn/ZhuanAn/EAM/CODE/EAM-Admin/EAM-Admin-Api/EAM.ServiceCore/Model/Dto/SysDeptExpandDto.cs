using System.ComponentModel.DataAnnotations;

namespace EAM.Model.System.Dto
{
    /// <summary>
    /// 系统部门扩展查询对象
    /// </summary>
    public class SysDeptExpandQueryDto : PagerInfo
    {
        public long? SysDeptId { get; set; }
        public string DeptName {  get; set; }
        public string FactoryId { get; set; }
    }

    /// <summary>
    /// 系统部门扩展输入输出对象
    /// </summary>
    public class SysDeptExpandDto
    {
        [Required(ErrorMessage = "系统部门ID不能为空")]
        public long SysDeptId { get; set; }

        public long ParentId {  get; set; }

        public string LuxDeptId { get; set; }

        public string WxDeptId { get; set; }

        public string DefaultFactoryId { get; set; }

        public string DeptName { get; set; }
    }
}
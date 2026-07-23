using EAM.ServiceCore.Model;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 集团员工信息service接口
    /// </summary>
    public interface ILuxEmpService : IBaseService<LuxEmp>
    {
        LuxEmp GetInfo(string empCode);
    }
}
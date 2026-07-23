using EAM.ServiceCore.Model;
using Infrastructure;
using Infrastructure.Attribute;

namespace EAM.ServiceCore.Services
{
    [AppService(ServiceType = typeof(ILuxEmpService), ServiceLifetime = LifeTime.Transient)]
    public class LuxEmpService : BaseService<LuxEmp>, ILuxEmpService
    {
        /// <summary>
        /// 通过工号 获取到集团用户信息
        /// </summary>
        /// <param name="empCode"></param>
        /// <returns></returns>
        public LuxEmp GetInfo(string empCode)
        {
            if (string.IsNullOrEmpty(empCode))
            {
                throw new CustomException("员工工号不能为空");
            }

            return Queryable().Where(it => it.EmpCode == empCode).First();
        }
    }
}
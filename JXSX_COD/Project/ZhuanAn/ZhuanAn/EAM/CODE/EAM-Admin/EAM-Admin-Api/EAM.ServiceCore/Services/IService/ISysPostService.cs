using EAM.Model.System;

namespace EAM.ServiceCore.Services
{
    public interface ISysPostService : IBaseService<SysPost>
    {
        string CheckPostNameUnique(SysPost sysPost);

        string CheckPostCodeUnique(SysPost sysPost);

        List<SysPost> GetAll();
    }
}
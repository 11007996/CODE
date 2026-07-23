using EAM.Model;
using EAM.Model.System;
using EAM.Model.System.Dto;

namespace EAM.ServiceCore.Services
{
    public interface ISysTasksQzService : IBaseService<SysTasks>
    {
        PagedInfo<SysTasks> SelectTaskList(TasksQueryDto parm);

        //SysTasksQz GetId(object id);
        int AddTasks(SysTasks parm);

        int UpdateTasks(SysTasks parm);
    }
}
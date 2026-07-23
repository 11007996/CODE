using EAM.Model;
using EAM.Model.System;
using EAM.Model.System.Dto;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 通知公告表service接口
    /// @date 2021-12-15
    /// </summary>
    public interface ISysNoticeService : IBaseService<SysNotice>
    {
        List<SysNotice> GetSysNotices();

        PagedInfo<SysNotice> GetPageList(SysNoticeQueryDto parm);

        PagedInfo<SysNoticeDto> ExportList(SysNoticeQueryDto parm);
    }
}
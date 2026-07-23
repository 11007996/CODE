using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using System.Data;
using static EAM.Model.Dto.MaintainReportSheetDto;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 资产保养报表service接口
    /// </summary>
    public interface IMaintainReportService : IBaseService<MaintainReport>
    {
        PagedInfo<MaintainReportDto> GetList(MaintainReportQueryDto parm);

        MaintainReport GetInfo(int Id);

        MaintainReport AddMaintainReport(MaintainReport parm);

        int BatchAddMaintainReport(int year);

        int UpdateMaintainReport(MaintainReport parm);

        MaintainReportSheetDto GetReportSheet(MaintainReportSheetQueryDto param);

        string ExportReportExcel(MaintainReportSheetQueryDto param);

        (PagedInfo<EquipmentBase>, DataTable, List<SheetPartColumn>) GetReportOverview(MaintainReportOverviewQueryDto param);

    }
}
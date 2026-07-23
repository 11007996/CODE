using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Model.Statistics;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using EAM.Service.Statistics.IStatisticsService;
using EAM.ServiceCore.Model.Enums;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 设备运行数据Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IEquipmentRuningRecordService), ServiceLifetime = LifeTime.Transient)]
    public class EquipmentRuningRecordService : BaseService<EquipmentRuningRecord>, IEquipmentRuningRecordService
    {
        private readonly IStatEquipmentRuningRecordService _statEquipmentRuningRecordService;

        public EquipmentRuningRecordService(IHttpContextAccessor contextAccessor, IStatEquipmentRuningRecordService statEquipmentRuningRecordService) : base(contextAccessor)
        {
            _statEquipmentRuningRecordService = statEquipmentRuningRecordService;
        }

        /// <summary>
        /// 查询设备运行数据列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentRuningRecordDto> GetList(EquipmentRuningRecordQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .WhereIF(parm.EquipmentId > 0, (it, e) => e.EquipmentId == parm.EquipmentId)
                .OrderBy(it => it.CreateTime, OrderByType.Desc)
                .Select((it, e) => new EquipmentRuningRecordDto()
                {
                    EquipmentId = e.EquipmentId,
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                    EquipmentName = e.EquipmentName
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 添加设备运行数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EquipmentRuningRecord AddEquipmentRuningRecord(EquipmentRuningRecord model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 导出设备运行数据
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentRuningRecordDto> ExportList(EquipmentRuningRecordQueryDto parm)
        {
            return GetList(parm);
        }

        /// <summary>
        /// 获取实时的运行记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentRuningWatchDto> GetWatchList(EquipmentRuningRecordQueryDto parm)
        {
            DateTime startTime = parm.BeginCreateTime == null ? DateTime.Now.AddMinutes(-5) : parm.BeginCreateTime.Value;//默认5分钟以内

            var QueryRunRercord = Context.Queryable<EquipmentRuningRecord>()
                .Where(err => err.CreateTime >= startTime)
                .WhereIF(parm.EndCreateTime != null, err => err.CreateTime <= parm.EndCreateTime)
                .Take(1).PartitionBy(err => new { err.EquipmentId });//开窗获取最后一条记录;

            var result = Context.Queryable<EquipmentExtend>()
                 .Where(ee => ee.IsLink == SysYesNoConstant.是)
                 .LeftJoin<EquipmentBase>((ee, e) => ee.EquipmentId == e.EquipmentId)
                 .LeftJoin(QueryRunRercord, (ee, e, err) => ee.EquipmentId == err.EquipmentId)
                 .LeftJoin<Line>((ee, e, err, le) => ee.LineId == le.LineId)
                 .WhereIF(parm.EquipmentId > 0, (ee, e, err, le) => ee.EquipmentId == parm.EquipmentId)
                 .WhereIF(parm.RunState != null, (ee, e, err, le) => err.RunState == parm.RunState)
                 .OrderBy((ee, e, err, le) => new { e.EquipmentName, ee.EquipmentNo })
                 .Select((ee, e, err, le) => new EquipmentRuningWatchDto() { }, true)
                 .ToPageNoSort(parm);

            return result;
        }

        #region 获取监听详情

        public StatEquipmentRuningRecordDto GetWatchDetail(EquipmentWatchDetailQueryDto parm)
        {
            //数据检查
            if (parm.BeginCreateTime == null) parm.BeginCreateTime = DateTime.Now.AddDays(-1);
            if (parm.EndCreateTime == null) parm.EndCreateTime = DateTime.Now;
            EquipmentBase equipmentBase = Context.Queryable<EquipmentBase>().Where(it => it.EquipmentId == parm.EquipmentId).First();
            EquipmentExtend ext = Context.Queryable<EquipmentExtend>().Where(it => it.EquipmentId == parm.EquipmentId).First();
            if (equipmentBase == null || ext == null)
                throw new CustomException("未找到设备信息");

            //获取统计分析数据
            StatEquipmentRuningRecord serr = _statEquipmentRuningRecordService.StatOneEquipmentRunData(parm.EquipmentId.Value, parm.BeginCreateTime.Value, parm.EndCreateTime.Value);

            //转换信息
            StatEquipmentRuningRecordDto serrDto = serr.Adapt<StatEquipmentRuningRecordDto>();

            //补全信息
            serrDto.EquipmentName = equipmentBase.EquipmentName;
            serrDto.EquipmentNo = ext.EquipmentNo;
            if (serrDto.StatEquipmentRuningWarnNav != null && serrDto.StatEquipmentRuningWarnNav.Count() > 0)
            {//补全报警状态码描述
                List<int> warnCodeList = serrDto.StatEquipmentRuningWarnNav.Select(it => it.WarnCode).Distinct().ToList();
                List<EquipmentWarnCode> warnCodes = Context.Queryable<EquipmentWarnCode>().Where(it => it.EquipmentId == ext.EquipmentId && warnCodeList.Contains((int)it.WarnCode)).ToList();
                foreach (var item in serrDto.StatEquipmentRuningWarnNav)
                {
                    item.WarnDesc = warnCodes.Find(it => it.WarnCode == item.WarnCode)?.WarnDesc;
                }
            }

            return serrDto;
        }

        #endregion 获取监听详情

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<EquipmentRuningRecord> QueryExp(EquipmentRuningRecordQueryDto parm)
        {
            var predicate = Expressionable.Create<EquipmentRuningRecord>();

            predicate = predicate.AndIF(parm.EquipmentId != null, it => it.EquipmentId == parm.EquipmentId);
            predicate = predicate.AndIF(parm.RunState != null, it => it.RunState == parm.RunState);
            predicate = predicate.AndIF(parm.BeginCreateTime == null, it => it.CreateTime >= DateTime.Now.ToShortDateString().ParseToDateTime());
            predicate = predicate.AndIF(parm.BeginCreateTime != null, it => it.CreateTime >= parm.BeginCreateTime);
            predicate = predicate.AndIF(parm.EndCreateTime != null, it => it.CreateTime <= parm.EndCreateTime);
            return predicate;
        }
    }
}
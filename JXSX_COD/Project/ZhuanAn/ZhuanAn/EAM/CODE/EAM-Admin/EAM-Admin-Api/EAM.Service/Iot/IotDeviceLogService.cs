using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Repository;
using EAM.Service.Iot.IIotService;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Iot
{
    /// <summary>
    /// 设备日志Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotDeviceLogService), ServiceLifetime = LifeTime.Transient)]
    public class IotDeviceLogService : BaseService<IotDeviceLog>, IIotDeviceLogService
    {
        public IotDeviceLogService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询设备日志列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotDeviceLogDto> GetList(IotDeviceLogQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .LeftJoin<IotDevice>((it, d) => it.DeviceId == d.DeviceId)
                .Where(predicate.ToExpression())
                .OrderBy((it, d) => it.CreateTime, OrderByType.Desc)
                .Select((it, d) => new IotDeviceLogDto()
                {
                    DeviceName = d.DeviceName,
                    DeviceKey = d.DeviceKey
                }, true)
                .ToPageNoSort<IotDeviceLogDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="LogId"></param>
        /// <returns></returns>
        public IotDeviceLog GetInfo(long LogId)
        {
            var response = Queryable()
                .Where(x => x.LogId == LogId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加设备日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotDeviceLog AddIotDeviceLog(IotDeviceLog model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotDeviceLog> QueryExp(IotDeviceLogQueryDto parm)
        {
            var predicate = Expressionable.Create<IotDeviceLog>();

            predicate = predicate.AndIF(parm.DeviceId != null, it => it.DeviceId == parm.DeviceId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TraceId), it => it.TraceId == parm.TraceId);
            predicate = predicate.AndIF(parm.BeginCreateTime == null, it => it.CreateTime >= DateTime.Now.ToShortDateString().ParseToDateTime());
            predicate = predicate.AndIF(parm.BeginCreateTime != null, it => it.CreateTime >= parm.BeginCreateTime);
            predicate = predicate.AndIF(parm.EndCreateTime != null, it => it.CreateTime <= parm.EndCreateTime);
            return predicate;
        }
    }
}
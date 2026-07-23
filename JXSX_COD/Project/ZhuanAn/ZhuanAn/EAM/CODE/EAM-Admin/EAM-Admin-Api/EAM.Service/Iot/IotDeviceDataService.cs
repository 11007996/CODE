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
    /// 设备采集数据Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotDeviceDataService), ServiceLifetime = LifeTime.Transient)]
    public class IotDeviceDataService : BaseService<IotDeviceData>, IIotDeviceDataService
    {
        public IotDeviceDataService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询设备采集数据列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotDeviceDataDto> GetList(IotDeviceDataQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .LeftJoin<IotDevice>((it, d) => it.DeviceId == d.DeviceId)
                .Where(predicate.ToExpression())
                .OrderBy((it, d) => it.CollectTime, OrderByType.Desc)
                .Select((it,d)=>new IotDeviceDataDto() {
                    DeviceName = d.DeviceName,
                    DeviceKey = d.DeviceKey
                },true)
                .ToPageNoSort< IotDeviceDataDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        public IotDeviceData GetInfo(int DeviceId)
        {
            var response = Queryable()
                .Where(x => x.DeviceId == DeviceId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加设备采集数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotDeviceData AddIotDeviceData(IotDeviceData model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotDeviceData> QueryExp(IotDeviceDataQueryDto parm)
        {
            var predicate = Expressionable.Create<IotDeviceData>();

            predicate = predicate.AndIF(parm.DeviceId != null, it => it.DeviceId == parm.DeviceId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Identifier), it => it.Identifier == parm.Identifier);
            predicate = predicate.AndIF(parm.BeginCollectTime == null, it => it.CollectTime >= DateTime.Now.ToShortDateString().ParseToDateTime());
            predicate = predicate.AndIF(parm.BeginCollectTime != null, it => it.CollectTime >= parm.BeginCollectTime);
            predicate = predicate.AndIF(parm.EndCollectTime != null, it => it.CollectTime <= parm.EndCollectTime);
            return predicate;
        }
    }
}
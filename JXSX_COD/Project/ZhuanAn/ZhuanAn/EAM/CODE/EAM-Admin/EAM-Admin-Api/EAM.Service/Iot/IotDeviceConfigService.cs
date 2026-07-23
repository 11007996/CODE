using Infrastructure.Attribute;
using Infrastructure.Extensions;
using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Repository;
using EAM.Service.Iot.IIotService;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Iot
{
    /// <summary>
    /// 设备配置Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotDeviceConfigService), ServiceLifetime = LifeTime.Transient)]
    public class IotDeviceConfigService : BaseService<IotDeviceConfig>, IIotDeviceConfigService
    {
        public IotDeviceConfigService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询设备配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotDeviceConfigDto> GetList(IotDeviceConfigQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<IotDeviceConfig, IotDeviceConfigDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        public IotDeviceConfig GetInfo(int DeviceId)
        {
            var response = Queryable()
                .LeftJoin<IotCommonChannel>((x, c) => x.ChannelId == c.ChannelId)
                .Where(x => x.DeviceId == DeviceId)
                .Select((x,c)=>new IotDeviceConfig() { 
                    ChannelName = c.ChannelName,
                },true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加设备配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotDeviceConfig AddIotDeviceConfig(IotDeviceConfig model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改设备配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotDeviceConfig(IotDeviceConfig model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotDeviceConfig> QueryExp(IotDeviceConfigQueryDto parm)
        {
            var predicate = Expressionable.Create<IotDeviceConfig>();

            predicate = predicate.AndIF(parm.DeviceId != null, it => it.DeviceId == parm.DeviceId);
            return predicate;
        }
    }
}
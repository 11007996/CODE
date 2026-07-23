using EAM.Model;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Repository;
using EAM.Service.Iot.IIotService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace EAM.Service.Iot
{
    /// <summary>
    /// 产品物模型事件Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotProductThingEventService), ServiceLifetime = LifeTime.Transient)]
    public class IotProductThingEventService : BaseService<IotProductThingEvent>, IIotProductThingEventService
    {
        public IotProductThingEventService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询产品物模型事件列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotProductThingEventDto> GetList(IotProductThingEventQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<IotProductThingEvent, IotProductThingEventDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        public IotProductThingEvent GetInfo(int EventId)
        {
            var response = Queryable()
                .Includes(x => x.OutputParams) //填充子对象
                .Where(x => x.EventId == EventId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产品物模型事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotProductThingEvent AddIotProductThingEvent(IotProductThingEvent model)
        {
            CheckData(model);

            IotProductThingEvent entity = null;
            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                entity = Insertable(model).ExecuteReturnEntity();
                //插入输出参数
                if (model.OutputParams != null)
                {
                    for (int i = 0; i < model.OutputParams.Count; i++)
                    {
                        model.OutputParams[i].OwnerId = entity.EventId;
                        model.OutputParams[i].OwnerType = IotParamOwnerTypeConstant.事件;
                        model.OutputParams[i].Direction = IotParamDirectionConstant.输出;
                        model.OutputParams[i].SortOrder = i;
                    }
                    Context.Insertable(model.OutputParams).ExecuteCommand();
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return entity;
        }

        /// <summary>
        /// 修改产品物模型事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotProductThingEvent(IotProductThingEvent model)
        {
            CheckData(model);

            int count = 0;
            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                count = Update(model, true);
                //插入输出参数
                if (model.OutputParams != null)
                {
                    //删除原来的参数定义
                    Context.Deleteable<IotProductParamDefine>()
                    .Where(it => it.OwnerId == model.EventId && it.OwnerType == IotParamOwnerTypeConstant.事件 && it.Direction == IotParamDirectionConstant.输出)
                    .ExecuteCommand();
                    //插入新的参数定义
                    for (int i = 0; i < model.OutputParams.Count; i++)
                    {
                        model.OutputParams[i].OwnerId = model.EventId;
                        model.OutputParams[i].OwnerType = IotParamOwnerTypeConstant.事件;
                        model.OutputParams[i].Direction = IotParamDirectionConstant.输出;
                        model.OutputParams[i].SortOrder = i;
                    }
                    Context.Insertable(model.OutputParams).ExecuteCommand();
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return count;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotProductThingEvent> QueryExp(IotProductThingEventQueryDto parm)
        {
            var predicate = Expressionable.Create<IotProductThingEvent>();

            predicate = predicate.AndIF(parm.ProductId != null, it => it.ProductId == parm.ProductId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.EventName), it => it.EventName.Contains(parm.EventName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Identifier), it => it.Identifier == parm.Identifier);
            return predicate;
        }

        /// <summary>
        /// 数据检查
        /// </summary>
        /// <param name="model"></param>
        private void CheckData(IotProductThingEvent model)
        {
            if (string.IsNullOrEmpty(model.Identifier))
                throw new CustomException("标识符不能为空");
            string[] keyArr = new string[] { "set", "get", "post", "property", "event", "service", "value", "time" };
            if (keyArr.Contains(model.Identifier.ToLower()))
                throw new CustomException("标识符不能为特殊值");
            if (!Regex.IsMatch(model.Identifier, "^[a-zA-Z0-9_]+$"))
                throw new CustomException("标识符只能是字母数字下划线组成");
        }
    }
}
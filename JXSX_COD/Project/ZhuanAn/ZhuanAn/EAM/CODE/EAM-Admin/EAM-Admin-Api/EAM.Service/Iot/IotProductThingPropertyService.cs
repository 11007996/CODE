using EAM.Model;
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
    /// 产品物模型属性Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotProductThingPropertyService), ServiceLifetime = LifeTime.Transient)]
    public class IotProductThingPropertyService : BaseService<IotProductThingProperty>, IIotProductThingPropertyService
    {
        public IotProductThingPropertyService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询产品物模型属性列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotProductThingPropertyDto> GetList(IotProductThingPropertyQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<IotProductThingProperty, IotProductThingPropertyDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="PropertyId"></param>
        /// <returns></returns>
        public IotProductThingProperty GetInfo(int PropertyId)
        {
            var response = Queryable()
                .Where(x => x.PropertyId == PropertyId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产品物模型属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotProductThingProperty AddIotProductThingProperty(IotProductThingProperty model)
        {
            CheckData(model);

            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产品物模型属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotProductThingProperty(IotProductThingProperty model)
        {
            CheckData(model);

            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotProductThingProperty> QueryExp(IotProductThingPropertyQueryDto parm)
        {
            var predicate = Expressionable.Create<IotProductThingProperty>();

            predicate = predicate.AndIF(parm.ProductId != null, it => it.ProductId == parm.ProductId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.PropertyName), it => it.PropertyName.Contains(parm.PropertyName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Identifier), it => it.Identifier == parm.Identifier);
            return predicate;
        }

        /// <summary>
        /// 数据检查
        /// </summary>
        /// <param name="model"></param>
        private void CheckData(IotProductThingProperty model)
        {
            if (string.IsNullOrEmpty(model.Identifier))
                throw new CustomException("标识符不能为空");
            string[] keyArr = new string[] { "set", "get", "post", "property", "event", "service", "value", "time" };
            if (keyArr.Contains(model.Identifier.ToLower()))
                throw new CustomException("标识符不能为特殊值");
            if (!Regex.IsMatch(model.Identifier, "^[a-zA-Z0-9_]+$"))
                throw new CustomException("标识符只能是字母数字下划线组成");
        }

        #region 属性扩展

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="PropertyId"></param>
        /// <returns></returns>
        public IotProductThingPropertyExtend GetExtendInfo(int PropertyId)
        {
            var response = Context.Queryable<IotProductThingPropertyExtend>()
                .Where(x => x.PropertyId == PropertyId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加属性扩展描述
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotProductThingPropertyExtend AddIotProductThingPropertyExtend(IotProductThingPropertyExtend model)
        {
            return Context.Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改属性扩展描述
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotProductThingPropertyExtend(IotProductThingPropertyExtend model)
        {
            return Context.Updateable<IotProductThingPropertyExtend>(model).ExecuteCommand();
        }

        /// <summary>
        /// 删除属性扩展
        /// </summary>
        /// <param name="PropertyId"></param>
        /// <returns></returns>
        public int DeleteIotProductThingPropertyExtend(int PropertyId)
        {
            return Context.Deleteable<IotProductThingPropertyExtend>().Where(it => it.PropertyId == PropertyId).ExecuteCommand();
        }

        #endregion 属性扩展
    }
}
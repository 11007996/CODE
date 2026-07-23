using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Repository;
using EAM.Service.Iot.IIotService;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Iot
{
    /// <summary>
    /// 产品表Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotProductService), ServiceLifetime = LifeTime.Transient)]
    public class IotProductService : BaseService<IotProduct>, IIotProductService
    {
        public IotProductService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询产品表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotProductDto> GetList(IotProductQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<IotProduct, IotProductDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public IotProduct GetInfo(int ProductId)
        {
            var response = Queryable()
                .Where(x => x.ProductId == ProductId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产品表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotProduct AddIotProduct(IotProduct model)
        {
            model.Version = null;
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产品表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotProduct(IotProduct model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 发布产品
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public int ReleaseIotProduct(int ProductId)
        {
            var model = Queryable().Where(it => it.ProductId == ProductId).First();
            if (model != null)
            {
                if (model.Version != null)
                {
                    model.Version++;
                }
                else
                {
                    model.Version = 1;
                }
                return Context.Updateable(model).UpdateColumns(it => it.Version).ExecuteCommand();
            }
            return 0;
        }

        /// <summary>
        /// 获取Iot产品字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(IotProductQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto()
                {
                    DictLabel = it.ProductName,
                    DictValue = it.ProductId.ToString(),
                })
                .ToPage<DictDataDto>(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotProduct> QueryExp(IotProductQueryDto parm)
        {
            var predicate = Expressionable.Create<IotProduct>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProductName), it => it.ProductName.Contains(parm.ProductName));
            return predicate;
        }
    }
}
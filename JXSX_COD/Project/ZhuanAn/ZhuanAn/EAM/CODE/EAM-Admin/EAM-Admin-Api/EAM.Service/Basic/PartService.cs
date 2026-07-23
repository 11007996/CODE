using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;

using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Basic
{
    /// <summary>
    /// 料号Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IPartService), ServiceLifetime = LifeTime.Transient)]
    public class PartService : BaseService<Part>, IPartService
    {
        public PartService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询料号列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<PartDto> GetList(PartQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<Part, PartDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="PartId"></param>
        /// <returns></returns>
        public Part GetInfo(int PartId)
        {
            var response = Queryable()
                .Where(x => x.PartId == PartId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加料号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Part AddPart(Part model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改料号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatePart(Part model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 获取料号字典类型数据
        /// </summary>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(PartQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto
                {
                    DictValue = it.PartId.ToString(),
                    DictLabel = it.PartNo
                })
                .ToPage(parm);
            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<Part> QueryExp(PartQueryDto parm)
        {
            var predicate = Expressionable.Create<Part>();
            predicate.AndIF(parm.PartId > 0, it => it.PartId == parm.PartId);
            predicate.AndIF(!string.IsNullOrEmpty(parm.PartNo), it => it.PartNo.Contains(parm.PartNo));
            return predicate;
        }
    }
}
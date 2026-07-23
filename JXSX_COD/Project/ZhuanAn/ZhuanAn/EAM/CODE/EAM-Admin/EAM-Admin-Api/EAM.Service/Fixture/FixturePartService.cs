using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Model.Fixture;
using EAM.Repository;
using EAM.Service.Fixture.IFixtureService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Fixture
{
    /// <summary>
    /// 治具料号关联表Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IFixturePartService), ServiceLifetime = LifeTime.Transient)]
    public class FixturePartService : BaseService<FixturePart>, IFixturePartService
    {
        public FixturePartService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询治具料号关联表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixturePartDto> GetList(FixturePartQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable().LeftJoin<FixtureBase>((it, f) => it.FixtureId == f.FixtureId)
                .LeftJoin<Part>((it, f, p) => it.PartId == p.PartId)
                .Where(predicate.ToExpression())
                .Select((it, f, p) => new FixturePartDto
                {
                    PartNo = p.PartNo,
                    FixtureName = f.Series + " / " + f.FixtureName
                }, true)
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public FixturePart GetInfo(FixturePartQueryDto parm)
        {
            var response = Queryable()
                .Where(x => x.PartId == parm.PartId && x.FixtureId == parm.FixtureId)
                .First();

            return response;
        }

        public bool IsExist(FixturePart parm)
        {
            var response = Queryable()
                .Where(x => x.FixtureId == parm.FixtureId && x.PartId == parm.PartId)
                .First();

            return response != null;
        }

        /// <summary>
        /// 添加治具料号关联表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FixturePart AddFixturePart(FixturePart model)
        {
            if (IsExist(model))
                throw new CustomException("治具已经与此料号关联。");
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改治具料号关联表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateFixturePart(FixturePart model)
        {
            return Update(model, true);
        }

        public int DeleteFixturePart(FixturePart model)
        {
            if (model.FixtureId == null || model.PartId == null)
                throw new CustomException("未指定要删除的治具或料号");
            return Delete(model);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<FixturePart> QueryExp(FixturePartQueryDto parm)
        {
            var predicate = Expressionable.Create<FixturePart>();

            predicate = predicate.AndIF(parm.PartId != null && parm.PartId > 0, it => it.PartId == parm.PartId);
            predicate = predicate.AndIF(parm.FixtureId != null && parm.FixtureId > 0, it => it.FixtureId == parm.FixtureId);
            return predicate;
        }
    }
}
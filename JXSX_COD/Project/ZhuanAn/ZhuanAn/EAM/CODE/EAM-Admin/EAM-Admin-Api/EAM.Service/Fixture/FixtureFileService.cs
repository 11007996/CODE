using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Fixture;
using EAM.Model.System;
using EAM.Repository;
using EAM.Service.Fixture.IFixtureService;
using EAM.ServiceCore.Services;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Fixture
{
    /// <summary>
    /// 治具文件关联Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IFixtureFileService), ServiceLifetime = LifeTime.Transient)]
    public class FixtureFileService : BaseService<FixtureFile>, IFixtureFileService
    {
        private readonly ISysFileService _sysFileService;

        public FixtureFileService(
            IHttpContextAccessor contextAccessor,
            ISysFileService sysFileService)
            : base(contextAccessor)
        {
            _sysFileService = sysFileService;
        }

        /// <summary>
        /// 查询治具文件关联列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixtureFileDto> GetList(FixtureFileQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<FixtureFile, FixtureFileDto>(parm);
            return response;
        }

        /// <summary>
        /// 查询治具文件关联列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<SysFile> GetFileList(FixtureFileQueryDto parm)
        {
            var predicate = QueryExp(parm);

            PagedInfo<long> fileIds = Queryable()
                .WhereIF(parm.FixtureId != null, it => it.FixtureId == parm.FixtureId)
                .Select(it => it.FileId)
                .ToPage(parm);
            var response = _sysFileService.Queryable().Where(it => fileIds.Result.Contains(it.Id)).ToPage(parm);
            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="FixtureId"></param>
        /// <returns></returns>
        public FixtureFile GetInfo(int FixtureId)
        {
            var response = Queryable()
                .Where(x => x.FixtureId == FixtureId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加治具文件关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FixtureFile AddFixtureFile(FixtureFile model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 批量添加治具文件关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int BatchAddFixtureFile(List<FixtureFile> model)
        {
            return Insert(model);
        }

        /// <summary>
        /// 修改治具文件关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateFixtureFile(FixtureFile model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 删除治具文件关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteFixtureFile(FixtureFileDto model)
        {
            return Deleteable().Where(it => it.FixtureId == model.FixtureId && it.FileId == model.FileId).ExecuteCommand();
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<FixtureFile> QueryExp(FixtureFileQueryDto parm)
        {
            var predicate = Expressionable.Create<FixtureFile>();
            predicate.AndIF(parm.FixtureId > 0, it => it.FixtureId == parm.FixtureId);
            return predicate;
        }
    }
}
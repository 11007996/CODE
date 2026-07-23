using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace EAM.Service.Basic
{
    /// <summary>
    /// 工站信息Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IStationService), ServiceLifetime = LifeTime.Transient)]
    public class StationService : BaseService<Station>, IStationService
    {
        public StationService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询工站信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<StationDto> GetList(StationQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .Where(predicate.ToExpression())
                .Select((it, l) => new StationDto
                {
                    LineName = l.LineName
                }, true)
                .ToPage<StationDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="StationId"></param>
        /// <returns></returns>
        public Station GetInfo(int StationId)
        {
            var response = Queryable()
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .Where(it => it.StationId == StationId)
                .Select((it, l) => new Station
                {
                    LineName = l.LineName
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加工站信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Station AddStation(Station model)
        {
            CheckData(model);
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改工站信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateStation(Station model)
        {
            CheckData(model);
            return Update(model, true);
        }

        /// <summary>
        /// 查询工站信息字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(StationQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Where(it => it.Enabled == true)
                .Select(it => new DictDataDto
                {
                    DictValue = it.StationId.ToString(),
                    DictLabel = it.StationName
                })
                .ToPage<DictDataDto>(parm);

            return response;
        }


        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<Station> QueryExp(StationQueryDto parm)
        {
            var predicate = Expressionable.Create<Station>();

            predicate.AndIF(parm.LineId > 0, it => it.LineId == parm.LineId);
            predicate.AndIF(!string.IsNullOrEmpty(parm.StationCode), it => it.StationCode == parm.StationCode);
            predicate.AndIF(!string.IsNullOrEmpty(parm.StationName), it => it.StationName.Contains(parm.StationName));

            return predicate;
        }

        /// <summary>
        /// 检查数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool CheckData(Station model)
        {
            bool has = false;
            //检查工站名称
            if (string.IsNullOrEmpty(model.StationName))
                throw new CustomException("工站名称不能为空");
            has = Queryable().Where(it => it.StationName == model.StationName && it.LineId == model.LineId)
                .WhereIF(model.StationId > 0, it => it.StationId != model.StationId).Count() > 0;
            if (has)
                throw new CustomException("同一产线存在相同工站名称");

            //检查工站编码
            if (string.IsNullOrEmpty(model.StationCode))
                throw new CustomException("工站编码不能为空");
            if (!Regex.IsMatch(model.StationCode, "^[A-Z0-9_]+$"))
                throw new CustomException("工站编码只能由大写字母、数字、下划线组成");
            if (model.StationCode.Length < 5)
                throw new CustomException("工站编码长度不能小于5位字符");

            if (model.StationId > 0)
            {
                has = Queryable().Where(it => it.StationCode == model.StationCode && it.StationId != model.StationId).Count() > 0;
            }
            else
            {
                has = Queryable().Where(it => it.StationCode == model.StationCode).Count() > 0;
            }
            if (has)
                throw new CustomException("工站编码已存在");

            return true;
        }
    }
}
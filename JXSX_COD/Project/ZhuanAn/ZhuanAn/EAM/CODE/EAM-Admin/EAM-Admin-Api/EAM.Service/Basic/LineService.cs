using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Basic
{
    /// <summary>
    /// 产线信息Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ILineService), ServiceLifetime = LifeTime.Transient)]
    public class LineService : BaseService<Line>, ILineService
    {
        public LineService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询产线信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<LineDto> GetList(LineQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("LineId asc")
                .Where(predicate.ToExpression())
                .ToPage<Line, LineDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="LineId"></param>
        /// <returns></returns>
        public Line GetInfo(int LineId)
        {
            var response = Queryable()
                .Where(x => x.LineId == LineId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产线信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Line AddLine(Line model)
        {
            CheckData(model);
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产线信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateLine(Line model)
        {
            CheckData(model);
            return Update(model, true);
        }

        /// <summary>
        /// 获取产线字典类型数据
        /// </summary>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(LineQueryDto parm)
        {
            var response = Queryable()
                .Select(it => new DictDataDto
                {
                    DictValue = it.LineId.ToString(),
                    DictLabel = it.LineName
                })
                .ToPage(parm);
            return response;
        }


        /// <summary>
        /// 检查表单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private bool CheckData(Line model)
        {
            //检查是否存在同名
            Line line = null;
            if (model.LineId > 0)
            {
                line = Queryable().Where(it => it.LineName == model.LineName && it.LineId != model.LineId).First();
            }
            else
            {
                line = Queryable().Where(it => it.LineName == model.LineName).First();
            }
            if (line != null)
                throw new CustomException($"产线名称已存在");

            //检查产线编码是否存在相同
            if (model.LineCode > 0)
            {
                if (model.LineId > 0)
                {
                    line = Queryable().Where(it => it.LineCode == model.LineCode && it.LineId != model.LineId).First();
                }
                else
                {
                    line = Queryable().Where(it => it.LineCode == model.LineCode).First();
                }
                if (line != null)
                    throw new CustomException($"产线编码已存在");
            }

            return true;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<Line> QueryExp(LineQueryDto parm)
        {
            var predicate = Expressionable.Create<Line>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.LineName), it => it.LineName.Contains(parm.LineName));
            predicate = predicate.AndIF(parm.LineCode > 0, it => it.LineCode == parm.LineCode);
            return predicate;
        }
    }
}
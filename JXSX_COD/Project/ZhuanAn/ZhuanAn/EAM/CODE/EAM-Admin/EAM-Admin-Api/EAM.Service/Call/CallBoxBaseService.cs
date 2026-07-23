using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Call.ICallService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Call
{
    /// <summary>
    /// 呼叫盒信息Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ICallBoxBaseService), ServiceLifetime = LifeTime.Transient)]
    public class CallBoxBaseService : BaseService<CallBoxBase>, ICallBoxBaseService
    {
        public CallBoxBaseService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询呼叫盒信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<CallBoxBaseDto> GetList(CallBoxBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .LeftJoin<Station>((it, l, s) => it.StationId == s.StationId)
                .Where(predicate.ToExpression())
                .Select((it, l, s) => new CallBoxBaseDto()
                {
                    LineName = l.LineName,
                    StationName = s.StationName,
                }, true)
                .ToPage<CallBoxBaseDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="BoxId"></param>
        /// <returns></returns>
        public CallBoxBase GetInfo(int BoxId)
        {
            var response = Queryable()
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .LeftJoin<Station>((it, l, s) => it.StationId == s.StationId)
                .Where(it => it.BoxId == BoxId)
                .Select((it, l, s) => new CallBoxBase()
                {
                    LineName = l.LineName,
                    StationName = s.StationName,
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加呼叫盒信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CallBoxBase AddCallBoxBase(CallBoxBase model)
        {
            CheckData(model);
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改呼叫盒信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCallBoxBase(CallBoxBase model)
        {
            CheckData(model);
            return Update(model, true);
        }


        /// <summary>
        /// 查询呼叫盒信息字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(CallBoxBaseQueryDto parm)
        {
            var response = Queryable()
                .WhereIF(!string.IsNullOrEmpty(parm.Keyword), it => it.BoxName.Contains(parm.Keyword) || it.Mac.Contains(parm.Keyword) || it.Ip.Contains(parm.Keyword))
                .Select(it => new DictDataDto()
                {
                    DictValue = it.BoxId.ToString(),
                    DictLabel = '[' + it.Mac + ']' + it.BoxName,
                })
                .ToPage<DictDataDto>(parm);

            return response;
        }



        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<CallBoxBase> QueryExp(CallBoxBaseQueryDto parm)
        {
            var predicate = Expressionable.Create<CallBoxBase>();

            predicate.AndIF(!string.IsNullOrEmpty(parm.BoxName), it => it.BoxName.Contains(parm.BoxName));
            predicate.AndIF(!string.IsNullOrEmpty(parm.Mac), it => it.Mac.Contains(parm.Mac));
            predicate.AndIF(!string.IsNullOrEmpty(parm.Ip), it => it.Ip.Contains(parm.Ip));
            predicate.AndIF(parm.LineId > 0, it => it.LineId == parm.LineId);
            predicate.AndIF(parm.StationId > 0, it => it.StationId == parm.StationId);

            return predicate;
        }

        /// <summary>
        /// 检查数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool CheckData(CallBoxBase model)
        {
            //检查名称
            if (string.IsNullOrEmpty(model.BoxName))
                throw new CustomException("呼叫盒名称不能为空");

            bool has = false;
            //检查MAC地址
            if (string.IsNullOrEmpty(model.Mac))
                throw new CustomException("MAC地址不能为空");
            has = Queryable().Where(it => it.Mac == model.Mac).WhereIF(model.BoxId > 0, it => it.BoxId != model.BoxId).Count() > 0;
            if (has)
                throw new CustomException("MAC地址已存在");

            //检查IP地址
            if (!string.IsNullOrEmpty(model.Ip))
                has = Queryable().Where(it => it.Ip == model.Ip).WhereIF(model.BoxId > 0, it => it.BoxId != model.BoxId).Count() > 0;
            if (has)
                throw new CustomException("IP地址已存在");

            //检查工站
            if (model.StationId > 0)
                has = Queryable().Where(it => it.StationId == model.StationId).WhereIF(model.BoxId > 0, it => it.BoxId != model.BoxId).Count() > 0;
            if (has)
                throw new CustomException("当前工站已被绑定");

            return true;
        }
    }
}
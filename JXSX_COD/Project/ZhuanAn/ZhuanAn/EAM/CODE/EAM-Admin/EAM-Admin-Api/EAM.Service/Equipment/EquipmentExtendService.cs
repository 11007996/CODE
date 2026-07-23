using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 设备扩展信息Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IEquipmentExtendService), ServiceLifetime = LifeTime.Transient)]
    public class EquipmentExtendService : BaseService<EquipmentExtend>, IEquipmentExtendService
    {
        public EquipmentExtendService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询设备扩展信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentExtendDto> GetList(EquipmentExtendQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("AssetNo asc")
                .Where(predicate.ToExpression())
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .LeftJoin<Line>((it, e, l) => it.LineId == l.LineId)
                .WhereIF(!string.IsNullOrEmpty(parm.EquipmentName), (it, e, l) => e.EquipmentName.Contains(parm.EquipmentName))
                .Select((it, e, l) => new EquipmentExtendDto()
                {
                    AssetNo = e.AssetNo,
                    EquipmentName = e.EquipmentName,
                    LineName = l.LineName
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public EquipmentExtend GetInfo(int equipmentId)
        {
            var response = Queryable()

                .Where(it => it.EquipmentId == equipmentId)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .LeftJoin<Line>((it, e, l) => it.LineId == l.LineId)
                .Select((it, e, l) => new EquipmentExtend()
                {
                    AssetNo = e.AssetNo,
                    EquipmentName = e.EquipmentName,
                    LineName = l.LineName
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加设备扩展信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EquipmentExtend AddEquipmentExtend(EquipmentExtend model)
        {
            //检查设备ID是否正确
            var equipment = Context.Queryable<EquipmentBase>().Where(it => it.EquipmentId == model.EquipmentId).First();
            if (equipment == null)
                throw new CustomException($"未找到设备ID【{model.EquipmentId}】的设备信息");
            //检查是否存在扩展信息
            if (GetInfo((int)model.EquipmentId) != null)
                throw new CustomException($"此设备【{model.EquipmentId}】已存在扩展配置");
            //检查是否存在相同设备编码
            var equipmentExtend = Context.Queryable<EquipmentExtend>().Where(it => it.EquipmentCode == model.EquipmentCode).First();
            if (equipmentExtend != null)
                throw new CustomException($"此设备编码【{model.EquipmentCode}】已存在扩展配置");
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改设备扩展信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateEquipmentExtend(EquipmentExtend model)
        {
            //检查资产编号是否正确
            var equipment = Context.Queryable<EquipmentBase>().Where(it => it.EquipmentId == model.EquipmentId).First();
            if (equipment == null)
                throw new CustomException($"未找到设备【{model.EquipmentId}】的信息");

            //检查是否有相同资产
            var equipmentExt = GetInfo((int)model.EquipmentId);
            if (equipmentExt == null)
                throw new CustomException($"未找到设备【{model.EquipmentId}】的扩展配置");
            //检查是否存在相同设备编码
            var equipmentExtend = Context.Queryable<EquipmentExtend>().Where(it => it.EquipmentId != model.EquipmentId && it.EquipmentCode == model.EquipmentCode).First();
            if (equipmentExtend != null)
                throw new CustomException($"此设备编码【{model.EquipmentCode}】已存在扩展配置");
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<EquipmentExtend> QueryExp(EquipmentExtendQueryDto parm)
        {
            var predicate = Expressionable.Create<EquipmentExtend>();

            predicate = predicate.AndIF(parm.EquipmentId != null && parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId);
            predicate = predicate.AndIF(parm.EquipmentCode != null, it => it.EquipmentCode == parm.EquipmentCode);
            return predicate;
        }
    }
}
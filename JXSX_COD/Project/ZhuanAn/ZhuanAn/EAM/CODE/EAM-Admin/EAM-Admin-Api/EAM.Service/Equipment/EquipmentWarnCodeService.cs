using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 设备报警代码Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IEquipmentWarnCodeService), ServiceLifetime = LifeTime.Transient)]
    public class EquipmentWarnCodeService : BaseService<EquipmentWarnCode>, IEquipmentWarnCodeService
    {
        public EquipmentWarnCodeService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询设备报警代码列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentWarnCodeDto> GetList(EquipmentWarnCodeQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .OrderBy((it, e) => it.EquipmentId, OrderByType.Asc)
                .OrderBy((it, e) => it.WarnCode, OrderByType.Asc)
                .Select((it, e) => new EquipmentWarnCodeDto()
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                    EquipmentName = e.EquipmentName
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <returns></returns>
        public EquipmentWarnCode GetInfo(int equipmentId, int warnCode)
        {
            var response = Queryable()
                .Where(it => it.EquipmentId == equipmentId && it.WarnCode == warnCode)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .Select((it, e) => new EquipmentWarnCode()
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                    EquipmentName = e.EquipmentName
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加设备报警代码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EquipmentWarnCode AddEquipmentWarnCode(EquipmentWarnCode model)
        {
            // 检查是否存在设备
            var equipment = Context.Queryable<EquipmentBase>().Where(it => it.EquipmentId == model.EquipmentId).First();
            if (equipment == null)
                throw new CustomException($"未找到设备【{model.EquipmentId}】的信息");
            // 检查是否存在相同的数据
            var ew = Queryable().Where(it => it.EquipmentId == model.EquipmentId && it.WarnCode == model.WarnCode).First();
            if (ew != null)
                throw new CustomException($"设备已存在报警代码【{model.WarnCode}】");
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改设备报警代码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateEquipmentWarnCode(EquipmentWarnCode model)
        {
            var ew = Queryable().Where(it => it.EquipmentId == model.EquipmentId && it.WarnCode == model.WarnCode).First();
            if (ew == null)
                throw new CustomException($"未找到要修改的报警代码");
            return Update(model, true);
        }

        /// <summary>
        /// 导入设备报警代码
        /// </summary>
        /// <returns></returns>
        public (string, object, object) ImportEquipmentWarnCode(List<EquipmentWarnCode> list)
        {
            var x = Context.Storageable(list)
                .SplitInsert(it => !it.Any())
                .SplitError(x => x.Item.EquipmentId.IsEmpty(), "设备ID不能为空")
                .SplitError(x => x.Item.WarnCode.IsEmpty(), "报警代码不能为空")
                .SplitError(x => x.Item.WarnDesc.IsEmpty(), "报警代码描述不能为空")
                //.WhereColumns(it => it.UserName)//如果不是主键可以这样实现（多字段it=>new{it.x1,it.x2}）
                .ToStorage();
            var result = x.AsInsertable.ExecuteCommand();//插入可插入部分;

            string msg = $"插入{x.InsertList.Count} 更新{x.UpdateList.Count} 错误数据{x.ErrorList.Count} 不计算数据{x.IgnoreList.Count} 删除数据{x.DeleteList.Count} 总共{x.TotalList.Count}";
            Console.WriteLine(msg);

            //输出错误信息
            foreach (var item in x.ErrorList)
            {
                Console.WriteLine("错误" + item.StorageMessage);
            }
            foreach (var item in x.IgnoreList)
            {
                Console.WriteLine("忽略" + item.StorageMessage);
            }

            return (msg, x.ErrorList, x.IgnoreList);
        }

        /// <summary>
        /// 删除报警代码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int DeleteEquipmentWarnCode(EquipmentWarnCode model)
        {
            if (model.EquipmentId == null || model.EquipmentId <= 0)
                throw new CustomException("未传递有效设备ID");
            if (model.WarnCode <= 0)
                throw new CustomException("未传递有效报警代码");
            return Context.Deleteable(model).ExecuteCommand();
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<EquipmentWarnCode> QueryExp(EquipmentWarnCodeQueryDto parm)
        {
            var predicate = Expressionable.Create<EquipmentWarnCode>();

            predicate = predicate.AndIF(parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId);
            return predicate;
        }
    }
}
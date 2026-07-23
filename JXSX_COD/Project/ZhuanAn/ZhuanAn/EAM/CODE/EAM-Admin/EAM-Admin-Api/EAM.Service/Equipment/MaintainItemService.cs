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
    /// 设备保养项目Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IMaintainItemService), ServiceLifetime = LifeTime.Transient)]
    public class MaintainItemService : BaseService<MaintainItem>, IMaintainItemService
    {
        private readonly IEquipmentBaseService EquipmentService;

        public MaintainItemService(IHttpContextAccessor contextAccessor, IEquipmentBaseService equipmentService) : base(contextAccessor)
        {
            EquipmentService = equipmentService;
        }

        /// <summary>
        /// 查询设备保养项目列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<MaintainItemDto> GetList(MaintainItemQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .OrderBy((it, e) => it.EquipmentId, OrderByType.Asc)
                .OrderBy("CHARINDEX(Date_Mark+',','D,W,M,Q,Y,')")
                .OrderBy((it, e) => it.SortNo, OrderByType.Asc)
                .Select((it, e) => new MaintainItemDto
                {
                    AssetName = e.AssetName,
                    EquipmentName = e.EquipmentName
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public MaintainItem GetInfo(int Id)
        {
            var response = Queryable()
                .Where(it => it.ItemId == Id)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .Select((it, e) => new MaintainItem()
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加设备保养项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MaintainItem AddMaintainItem(MaintainItem model)
        {
            if (EquipmentService.GetInfo((int)model.EquipmentId) == null)
                throw new CustomException($"未找到设备【{model.EquipmentId}】的信息");

            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改设备保养项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMaintainItem(MaintainItem model)
        {
            if (EquipmentService.GetInfo((int)model.EquipmentId) == null)
                throw new CustomException($"未找到设备【{model.EquipmentId}】的信息");
            return Update(model, true);
        }

        public int DeleteMaintainItem(int[] ids)
        {
            return Delete(ids);
        }

        /// <summary>
        /// 导入设备保养项目
        /// </summary>
        /// <returns></returns>
        public (string, object, object) ImportMaintainItem(List<MaintainItem> list)
        {
            List<string> assetNoList = list.Select(it => it.AssetNo).Distinct().ToList();
            if (assetNoList.Count > 0)
            {
                List<EquipmentBase> equipments = Context.Queryable<EquipmentBase>().Where(it => assetNoList.Contains(it.AssetNo)).ToList();
                if (equipments.Count > 0)
                {
                    foreach (MaintainItem item in list)
                    {
                        if (item.EquipmentId == null || item.EquipmentId <= 0)
                            item.EquipmentId = equipments.Where(it => it.AssetNo == item.AssetNo).Select(it => it.EquipmentId).FirstOrDefault();
                    }
                }
            }

            var x = Context.Storageable(list)
                .SplitError(x => x.Item.EquipmentId.IsEmpty(), "资产编号的信息未找到")
                .SplitError(x => x.Item.DateMark.IsEmpty(), "日期标记不能为空")
                .SplitError(x => x.Item.ItemName.IsEmpty(), "项目名称不能为空")
                .SplitError(x => Queryable().Any(it => it.EquipmentId == x.Item.EquipmentId && it.DateMark == x.Item.DateMark && it.ItemName == x.Item.ItemName), $"保养项目已存在")
                .SplitUpdate(x => x.Any())//数据库存在更新 根据主键
                                          //.WhereColumns(it => it.ItemId ) //如果不是主键可以这样实现（多字段it=>new{it.x1,it.x2}）
                .SplitInsert(x => true)//其余插入(因为插入优先级最低不满其他条件就是插入)
                .ToStorage();

            x.AsInsertable.ExecuteCommand();//插入可插入部分;
            x.AsUpdateable.ExecuteCommand();

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
        /// 导出设备保养项目
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<MaintainItemDto> ExportList(MaintainItemQueryDto parm)
        {
            return GetList(parm);
        }

        /// <summary>
        /// 设备保养项目克隆
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int CloneMaintainItem(MaintainItemCloneDto parm)
        {
            if (parm.FromEquipmentId <= 0)
                throw new CustomException("克隆设备不能为空");
            if (parm.ToEquipmentIdList == null || parm.ToEquipmentIdList.Length == 0)
                throw new CustomException("未传递有效的覆盖目标设备");
            if (parm.ToEquipmentIdList.Contains(parm.FromEquipmentId))
                throw new CustomException("覆盖目标设备中包含克隆设备");

            int count = 0;
            DbResult<bool> r = UseTran(() =>
            {
                foreach (var item in parm.ToEquipmentIdList)
                {
                    if (EquipmentService.GetInfo(item) == null)
                        throw new CustomException($"未找设备【{item}】的信息");
                    //检查是否已存在保养记录，存在不可以克隆覆盖
                    if (Context.Queryable<MaintainRecord>().Where(it => it.EquipmentId == item).Count() > 0)
                        throw new CustomException($"设备【{item}】已存在保养记录,不可以被克隆覆盖");
                    //删除原来的项目
                    Context.Deleteable<MaintainItem>().Where(it => it.EquipmentId == item).ExecuteCommand();
                    //复制克隆的项目
                    count += Context.Queryable<MaintainItem>().IgnoreColumns(it => it.ItemId) //如果是自增可以忽略，不过ID就不一样了
                        .Where(it => it.EquipmentId == parm.FromEquipmentId)
                        .Select(it => new MaintainItem { EquipmentId = item, DateMark = it.DateMark, ItemName = it.ItemName, SortNo = it.SortNo })
                        .IntoTable<MaintainItem>();
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return count;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<MaintainItem> QueryExp(MaintainItemQueryDto parm)
        {
            var predicate = Expressionable.Create<MaintainItem>();

            predicate = predicate.AndIF(parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId);
            return predicate;
        }
    }
}
using EAM.Model;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
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
    /// 设备资产信息Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IEquipmentBaseService), ServiceLifetime = LifeTime.Transient)]
    public class EquipmentBaseService : BaseService<EquipmentBase>, IEquipmentBaseService
    {
        public EquipmentBaseService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询设备资产信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentBaseDto> GetList(EquipmentBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<EquipmentBase, EquipmentBaseDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public EquipmentBase GetInfo(int equipmentId)
        {
            var response = Queryable()
                .Where(x => x.EquipmentId == equipmentId)
                .First();

            return response;
        }

        public EquipmentBase GetInfoByAssetNo(string assetNo)
        {
            var response = Queryable()
              .Where(x => x.AssetNo == assetNo)
              .First();

            return response;
        }

        /// <summary>
        /// 添加设备资产信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EquipmentBase AddEquipmentBase(EquipmentBase model)
        {
            if (model.FactoryCode == null || model.FactoryCode.Length != 4)
                throw new CustomException("工厂代码格式不正确，格式:长度为4且不为空");
            if (model.AssetMainNo == null || model.AssetMainNo.Length != 12)
                throw new CustomException("资产主编号格式不正确，格式:长度为12且不为空");
            if (model.AssetSubNo == null || model.AssetSubNo.Length != 4)
                throw new CustomException("资产子编号格式不正确，格式:长度为4且不为空");
            if (string.IsNullOrEmpty(model.AssetName))
                throw new CustomException("资产名称不能为空");
            string[] assetNoInfo = { model.FactoryCode, model.AssetMainNo, model.AssetSubNo };
            model.AssetNo = string.Join("-", assetNoInfo);

            int count = Queryable().Where(it => it.AssetNo == model.AssetNo).Count();
            if (count > 0)
                throw new CustomException($"已存在相同的资产编号:{model.AssetNo}");

            model.Status = EquipmentStatusConstant.正常;
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改设备资产信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateEquipmentBase(EquipmentBase model)
        {
            return Context.Updateable(model).IgnoreColumns(x => new { x.FactoryCode, x.AssetMainNo, x.AssetSubNo }).ExecuteCommand();
        }

        /// <summary>
        /// 删除设备资产信息
        /// </summary>
        /// <param name="idArr"></param>
        /// <returns></returns>
        public int DeleteEquipmentBase(int[] idArr)
        {
            if (idArr == null || idArr.Length <= 0)
                throw new CustomException("参数设备ID不能为空");
            return Context.Updateable<EquipmentBase>().SetColumns(it => it.DelFlag == (int)DeleteFlagEnum.删除).Where(it => idArr.Contains((int)it.EquipmentId)).ExecuteCommand();
        }

        /// <summary>
        /// 导入设备资产信息
        /// </summary>
        /// <returns></returns>
        public (string, object, object) ImportEquipmentBase(List<EquipmentBase> list)
        {
            var x = Context.Storageable(list)
                .SplitInsert(it => !it.Any())
                .SplitError(x => x.Item.AssetNo.IsEmpty(), "资产编号不能为空")
                .SplitError(x => x.Item.FactoryCode.IsEmpty(), "公司代码不能为空")
                .SplitError(x => x.Item.AssetMainNo.IsEmpty(), "资产主编号不能为空")
                .SplitError(x => x.Item.AssetSubNo.IsEmpty(), "资产子编号不能为空")
                .SplitError(x => x.Item.AssetName.IsEmpty(), "资产名称不能为空")
                .SplitError(x => x.Item.UpdateTime.IsEmpty(), "更新时间不能为空")
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
        /// 导出设备资产信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentBaseDto> ExportList(EquipmentBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select((it) => new EquipmentBaseDto()
                {
                }, true)
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询设备表字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(EquipmentBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto()
                {
                    DictValue = it.EquipmentId.ToString(),
                    DictLabel = it.AssetNo + " : " + it.EquipmentName,
                    DictDesc = it.AssetName
                })
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询成本中心字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetCostCenterDict(EquipmentBaseQueryDto parm)
        {
            var response = Queryable()
                .Where(it => it.DelFlag == 0 && it.CostCenter != null && it.CostCenter != "")
                .WhereIF(!string.IsNullOrEmpty(parm.CostCenter), it => it.CostCenter.Contains(parm.CostCenter))
                .Select(it => new DictDataDto()
                {
                    DictValue = it.CostCenter,
                    DictLabel = it.CostCenter
                })
                .Distinct()
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取自定义机型字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetCustomModelDict(EquipmentBaseQueryDto parm)
        {
            var response = Queryable()
                .Where(it => it.DelFlag == 0 && it.CustomModel != null && it.CustomModel != "")
                .WhereIF(!string.IsNullOrEmpty(parm.CustomModel), it => it.CustomModel.Contains(parm.CustomModel))
                .Select(it => new DictDataDto()
                {
                    DictValue = it.CustomModel,
                    DictLabel = it.CustomModel
                })
                .Distinct()
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询可以领用的设备
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentIdleDto> GetIdle(EquipmentBaseQueryDto parm)
        {
            var response = Queryable()
                .WhereIF(!string.IsNullOrEmpty(parm.EquipmentName), it => it.EquipmentName.Contains(parm.EquipmentName))
                .GroupBy(it => new { it.EquipmentName })
                .Select(it => new EquipmentIdleDto()
                {
                    EquipmentName = it.EquipmentName,
                    TotalIdleQty = SqlFunc.AggregateSum(SqlFunc.IF(it.Status == EquipmentStatusConstant.闲置 || it.Status == EquipmentStatusConstant.正常).Return(1).End(0)),
                })
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<EquipmentBase> QueryExp(EquipmentBaseQueryDto parm)
        {
            var predicate = Expressionable.Create<EquipmentBase>();

            predicate = predicate.And(it => it.DelFlag == (int)DeleteFlagEnum.存在);
            predicate = predicate.AndIF(parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AssetNo), it => it.AssetNo.Contains(parm.AssetNo));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AssetName), it => it.AssetName.Contains(parm.AssetName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AssetClass), it => it.AssetClass == parm.AssetClass);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.EquipmentName), it => it.EquipmentName.Contains(parm.EquipmentName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.CostCenter), it => it.CostCenter.Contains(parm.CostCenter));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.CustomModel), it => it.CustomModel.Contains(parm.CustomModel));

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Keyword),
                it => it.AssetNo.Contains(parm.Keyword)
                || it.AssetName.Contains(parm.Keyword)
                || it.EquipmentName.Contains(parm.Keyword)
                || it.CustomModel.Contains(parm.Keyword));

            return predicate;
        }
    }
}
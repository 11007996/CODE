using EAM.Model;
using EAM.Model.Consumable;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Repository;
using EAM.Service.Consumable.IConsumableService;
using EAM.ServiceCore.Model.Enums;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Consumable
{
    /// <summary>
    /// 耗品表Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IConsumableBaseService), ServiceLifetime = LifeTime.Transient)]
    public class ConsumableBaseService : BaseService<ConsumableBase>, IConsumableBaseService
    {
        public ConsumableBaseService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询耗品表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ConsumableBaseDto> GetList(ConsumableBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("ConsumableId asc")
                .Where(predicate.ToExpression())
                .ToPage<ConsumableBase, ConsumableBaseDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ConsumableDetailDto> GetDetailList(ConsumableBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);
            var sqlcs = Context.Queryable<ConsumableStorage>()
                .GroupBy(cs => cs.ConsumableId)
                .Select(cs => new
                {
                    cs.ConsumableId,
                    TotalStackQty = SqlFunc.AggregateSumNoNull(cs.Qty),
                });
            var response = Queryable().Where(predicate.ToExpression())
                .LeftJoin(sqlcs, (it, cs) => it.ConsumableId == cs.ConsumableId)
                .WhereIF(parm.IsStackWarning != null && parm.IsStackWarning.Value == true, (it, cs) => cs.TotalStackQty < it.SafetyQty)
                .WhereIF(parm.IsStackWarning != null && parm.IsStackWarning.Value == false, (it, cs) => cs.TotalStackQty >= it.SafetyQty)
                .Select((it, cs) => new ConsumableDetailDto
                {
                    TotalStackQty = cs.TotalStackQty,
                }, true)
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ConsumableId"></param>
        /// <returns></returns>
        public ConsumableBase GetInfo(int ConsumableId)
        {
            var response = Queryable()
                .Where(x => x.ConsumableId == ConsumableId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加耗品表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ConsumableBase AddConsumableBase(ConsumableBase model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改耗品表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateConsumableBase(ConsumableBase model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 删除耗品
        /// </summary>
        /// <param name="idArr"></param>
        /// <returns></returns>
        public int DeleteConsumableBase(int[] idArr)
        {
            //int count = Context.Queryable<ConsumableStorage>().Where(x => consumableIds.Contains(x.ConsumableId)).Count();
            //if (count > 0)
            //    throw new CustomException("存在库存数据,请先删除库存");
            if (idArr == null || idArr.Length == 0)
                throw new CustomException("参数耗品ID不能为空");

            return Context.Updateable<ConsumableBase>().SetColumns(it => it.DelFlag == (int)DeleteFlagEnum.删除).Where(it => idArr.Contains((int)it.ConsumableId)).ExecuteCommand();
        }

        /// <summary>
        /// 导入耗品表
        /// </summary>
        /// <returns></returns>
        public (string, object, object) ImportConsumableBase(List<ConsumableBase> list)
        {
            foreach (ConsumableBase cons in list)
            {
                cons.Status = SysStatusConstant.正常;
            }
            var x = Context.Storageable(list)
                .SplitInsert(it => !it.Any())
                .SplitError(x => x.Item.ConsumablePart.IsEmpty(), "请购料号不能为空")
                .SplitError(x => x.Item.ConsumableName.IsEmpty(), "耗品名称不能为空")
                .SplitError(x => x.Item.Spec.IsEmpty(), "规格不能为空")
                .SplitError(x => x.Item.Price.IsEmpty(), "单价不能为空")
                .SplitError(x => x.Item.SafetyQty.IsEmpty(), "安全库存不能为空")
                //.SplitError(x => x.Item.Status.IsEmpty(), "状态不能为空")
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
        /// 导出耗品表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ConsumableDetailDto> ExportList(ConsumableBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);
            var sqlcs = Context.Queryable<ConsumableStorage>()
                .GroupBy(cs => cs.ConsumableId)
                .Select(cs => new
                {
                    cs.ConsumableId,
                    TotalStackQty = SqlFunc.AggregateSumNoNull(cs.Qty),
                });
            var response = Queryable().Where(predicate.ToExpression())
                .LeftJoin(sqlcs, (it, cs) => it.ConsumableId == cs.ConsumableId)
                //.WhereIF(!string.IsNullOrEmpty(parm.FixtureName), (f, fs) => f.FixtureName.Contains(parm.FixtureName))
                //.OrderBy("FixtureId asc")
                .Select((it, cs) => new ConsumableDetailDto
                {
                    TotalStackQty = cs.TotalStackQty,
                }, true)
                .ToPage(parm);
            return response;
        }

        /// <summary>
        /// 查询耗品表字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(ConsumableBaseQueryDto parm)
        {
            var response = Queryable()
                .WhereIF(!string.IsNullOrEmpty(parm.ConsumablePart), it => it.ConsumablePart.Contains(parm.ConsumablePart))
                .WhereIF(!string.IsNullOrEmpty(parm.ConsumableName), it => it.ConsumableName.Contains(parm.ConsumableName))
                .WhereIF(!string.IsNullOrEmpty(parm.Spec), it => it.Spec.Contains(parm.Spec))
                .WhereIF(!string.IsNullOrEmpty(parm.Keyword), it => it.ConsumablePart.Contains(parm.Keyword) || it.ConsumableName.Contains(parm.Keyword) || it.Spec.Contains(parm.Keyword))
                .Select(it => new DictDataDto()
                {
                    DictValue = it.ConsumableId.ToString(),
                    DictLabel = it.ConsumablePart + " / " + it.ConsumableName + " / " + it.Spec,
                })
                .Distinct()
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询耗品类别表字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetCategoryDict(ConsumableBaseQueryDto parm)
        {
            var response = Queryable()
                .Where(it => it.Category != null)
                .Select(it => new DictDataDto()
                {
                    DictValue = it.Category,
                    DictLabel = it.Category
                })
                .Distinct()
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ConsumableBase> QueryExp(ConsumableBaseQueryDto parm)
        {
            var predicate = Expressionable.Create<ConsumableBase>();

            predicate = predicate.And(it => it.DelFlag == (int)DeleteFlagEnum.存在);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Category), it => it.Category == parm.Category);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ConsumablePart), it => it.ConsumablePart.Contains(parm.ConsumablePart));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ConsumableName), it => it.ConsumableName.Contains(parm.ConsumableName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Spec), it => it.Spec.Contains(parm.Spec));

            return predicate;
        }
    }
}
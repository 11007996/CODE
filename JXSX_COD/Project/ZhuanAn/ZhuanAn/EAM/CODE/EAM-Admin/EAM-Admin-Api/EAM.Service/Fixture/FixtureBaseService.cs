using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Consumable;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.Fixture;
using EAM.Repository;
using EAM.Service.Fixture.IFixtureService;
using EAM.ServiceCore.Model.Enums;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Fixture
{
    /// <summary>
    /// 治具信息Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IFixtureBaseService), ServiceLifetime = LifeTime.Transient)]
    public class FixtureBaseService : BaseService<FixtureBase>, IFixtureBaseService
    {
        public FixtureBaseService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询治具信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixtureBaseDto> GetList(FixtureBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("FixtureId asc")
                .Where(predicate.ToExpression())
                .ToPage<FixtureBase, FixtureBaseDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixtureDetailDto> GetFixtureDetailList(FixtureBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);
            var stackQty = Context.Queryable<FixtureStorage>()
                .GroupBy(it => it.FixtureId)
                .Select(it => new
                {
                    it.FixtureId,
                    TotalIdleQty = SqlFunc.AggregateSumNoNull(it.Qty),
                });

            var usedQty = Context.Queryable<FixtureStorageUsing>()
                .GroupBy(it => it.FixtureId)
                .Select(it => new
                {
                    it.FixtureId,
                    TotalUsingQty = SqlFunc.AggregateSumNoNull(it.Qty),
                });
            var response = Queryable().Where(predicate.ToExpression())
                .LeftJoin(stackQty, (f, fs) => f.FixtureId == fs.FixtureId)
                .LeftJoin(usedQty, (f, fs, fu) => f.FixtureId == fu.FixtureId)
                .WhereIF(!string.IsNullOrEmpty(parm.FixtureName), (f, fs) => f.FixtureName.Contains(parm.FixtureName))
                //.OrderBy("FixtureId asc")
                .Select((f, fs, fu) => new FixtureDetailDto
                {
                    TotalIdleQty = fs.TotalIdleQty,
                    TotalUsingQty = fu.TotalUsingQty,
                    TotalQty = SqlFunc.IsNull(fs.TotalIdleQty, 0) + SqlFunc.IsNull(fu.TotalUsingQty, 0)
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 查询闲置治具列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IdleFixtureDto> IdleFixtureList(FixtureBaseQueryDto parm)
        {
            var stackQty = Context.Queryable<FixtureStorage>()
              .GroupBy(it => it.FixtureId)
              .Select(it => new
              {
                  it.FixtureId,
                  TotalIdleQty = SqlFunc.AggregateSumNoNull(it.Qty),
              });

            var response = Context.Queryable<FixturePart>()
                .LeftJoin<FixtureBase>((it, f) => it.FixtureId == f.FixtureId)
                .LeftJoin(stackQty, (it, f, sq) => it.FixtureId == sq.FixtureId)
                .WhereIF(parm.PartId != null && parm.PartId > 0, it => it.PartId == parm.PartId)
                .WhereIF(!string.IsNullOrEmpty(parm.FixtureName), (it, f, sq) => f.FixtureName.Contains(parm.FixtureName))
                .WhereIF(!string.IsNullOrEmpty(parm.Series), (it, f, sq) => f.Series.Contains(parm.Series))
                .Select((it, f, sq) => new IdleFixtureDto
                {
                    FixtureName = f.FixtureName
                }, true)
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="FixtureId"></param>
        /// <returns></returns>
        public FixtureBase GetInfo(int FixtureId)
        {
            var response = Queryable()
                .Where(x => x.FixtureId == FixtureId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加治具信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FixtureBase AddFixtureBase(FixtureBase model)
        {
            string errorMsg = "";
            //检查
            if (!Check(model, ref errorMsg))
                throw new CustomException(errorMsg);
            //新增
            FixtureBase fixture = Insertable(model).ExecuteReturnEntity();
            //添加一条默认的料号绑定
            if (fixture != null && fixture.FixtureId != null)
            {
                // 获取关联的料号
                Part part = Context.Queryable<Part>().Where(it => it.PartNo == model.Series).First();

                FixturePart fixturePart = new()
                {
                    PartId = part.PartId,
                    FixtureId = fixture.FixtureId,
                    DefaultQty = 1
                };
                Context.Insertable<FixturePart>(fixturePart).ExecuteCommand();
            }
            return fixture;
        }

        /// <summary>
        /// 修改治具信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateFixtureBase(FixtureBase model)
        {
            //检查
            string errorMsg = "";
            if (!Check(model, ref errorMsg))
                throw new CustomException(errorMsg);
            //更新
            return Update(model, true);
        }

        /// <summary>
        /// 删除治具
        /// </summary>
        /// <param name="idArr"></param>
        /// <returns></returns>
        public int DeleteFixtureBase(int[] idArr)
        {
            if (idArr == null || idArr.Length == 0)
                throw new CustomException("参数治具ID不能为空");

            return Context.Updateable<FixtureBase>().SetColumns(it => it.DelFlag == (int)DeleteFlagEnum.删除).Where(it => idArr.Contains((int)it.FixtureId)).ExecuteCommand();
        }

        /// <summary>
        /// 导出治具表信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixtureDetailDto> ExportList(FixtureBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);
            var stackQty = Context.Queryable<FixtureStorage>()
                .GroupBy(it => it.FixtureId)
                .Select(it => new
                {
                    it.FixtureId,
                    TotalIdleQty = SqlFunc.AggregateSum(it.Qty),
                });

            var usedQty = Context.Queryable<FixtureStorageUsing>()
                .GroupBy(it => it.FixtureId)
                .Select(it => new
                {
                    it.FixtureId,
                    TotalUsingQty = SqlFunc.AggregateSum(it.Qty),
                });
            var response = Queryable().Where(predicate.ToExpression())
                .LeftJoin(stackQty, (f, fs) => f.FixtureId == fs.FixtureId)
                .LeftJoin(usedQty, (f, fs, fu) => f.FixtureId == fu.FixtureId)
                .WhereIF(!string.IsNullOrEmpty(parm.FixtureName), (f, fs) => f.FixtureName.Contains(parm.FixtureName))
                //.OrderBy("FixtureId asc")
                .Select((f, fs, fu) => new FixtureDetailDto
                {
                    TotalIdleQty = fs.TotalIdleQty,
                    TotalUsingQty = fu.TotalUsingQty,
                    TotalQty = SqlFunc.IsNull(fs.TotalIdleQty, 0) + SqlFunc.IsNull(fu.TotalUsingQty, 0)
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 导入治具信息
        /// </summary>
        /// <returns></returns>
        public (string, object, object) ImportFixtureBase(List<FixtureBase> list)
        {
            foreach (FixtureBase fix in list)
            {
                fix.Status = SysStatusConstant.正常;
            }
            var x = Context.Storageable(list)
                .SplitInsert(it => !it.Any())
                .SplitError(x => x.Item.FixtureName.IsEmpty(), "治具名称不能为空")
                .SplitError(x => x.Item.Series.IsEmpty(), "系列不能为空")
                .SplitError(x => x.Item.Price.IsEmpty(), "单价不能为空")
                .SplitError(x => x.Item.SafetyQty.IsEmpty(), "安全库存不能为空")
                //.SplitError(x => x.Item.Status.IsEmpty(), "状态不能为空")
                //.SplitError(x => x.Item.CreateBy.IsEmpty(), "创建人不能为空")
                //.SplitError(x => x.Item.CreateTime.IsEmpty(), "创建时间不能为空")
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
        /// 治具字典查询
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(FixtureBaseQueryDto parm)
        {
            var response = Queryable().LeftJoin<FixturePart>((it, fp) => it.FixtureId == fp.FixtureId)
                .WhereIF(!string.IsNullOrEmpty(parm.FixtureName), it => it.FixtureName.Contains(parm.FixtureName))
                .WhereIF(!string.IsNullOrEmpty(parm.Series), it => it.Series.Contains(parm.Series))
                .WhereIF(!string.IsNullOrEmpty(parm.Keyword), it => it.Series.Contains(parm.Keyword) || it.FixtureName.Contains(parm.Keyword))
                .WhereIF(parm.PartId != null && parm.PartId > 0, (it, fp) => fp.PartId == parm.PartId)
                .Select(it => new DictDataDto()
                {
                    DictValue = it.FixtureId.ToString(),
                    DictLabel = it.Series + " / " + it.FixtureName,
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
        private static Expressionable<FixtureBase> QueryExp(FixtureBaseQueryDto parm)
        {
            var predicate = Expressionable.Create<FixtureBase>();

            predicate = predicate.And(it => it.DelFlag == (int)DeleteFlagEnum.存在);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.FixtureName), it => it.FixtureName.Contains(parm.FixtureName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Series), it => it.Series == parm.Series);
            return predicate;
        }

        private bool Check(FixtureBase model, ref string errorMsg)
        {
            errorMsg = "";
            //基础检查
            if (model.SafetyQty == null || model.SafetyQty < 0)
                errorMsg = "【安全库存】数量不能小于0";
            else if (model.Price == null || model.Price < 0)
                errorMsg = "【单价】不能小于0";
            else if (string.IsNullOrEmpty(model.Series))
                errorMsg = "【系列】不能为空";

            int count = 0;
            //检查系列是否存在
            if (string.IsNullOrEmpty(errorMsg))
            {
                count = Context.Queryable<Part>().Where(it => it.PartNo == model.Series).Count();
                if (count <= 0)
                    errorMsg = $"此系列【{model.Series}】不存在，请先在料号管理中添加这个系列的料号";
            }

            //检查是否重复，唯一规则【系列】+【治具名称】
            if (string.IsNullOrEmpty(errorMsg))
            {
                count = Queryable().Where(it => it.FixtureName == model.FixtureName && it.Series == model.Series && it.Price == model.Price).WhereIF(model.FixtureId != null, it => it.FixtureId != model.FixtureId).Count();
                if (count > 0)
                    errorMsg = $"此系列【{model.Series}】已存在同名的治具名称【{model.FixtureName}】、单价的信息数据。";
            }

            return errorMsg.Length <= 0;
        }
    }
}
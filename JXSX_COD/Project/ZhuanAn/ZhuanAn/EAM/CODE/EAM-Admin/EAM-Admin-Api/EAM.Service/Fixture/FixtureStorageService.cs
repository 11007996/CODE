using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Fixture;
using EAM.Repository;
using EAM.Service.Fixture.IFixtureService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Fixture
{
    /// <summary>
    /// 治具存储Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IFixtureStorageService), ServiceLifetime = LifeTime.Transient)]
    public class FixtureStorageService : BaseService<FixtureStorage>, IFixtureStorageService
    {
        public IFixtureBaseService FixtureRepository;

        public FixtureStorageService(IHttpContextAccessor httpContextAccessor, IFixtureBaseService fixtureRepository) : base(httpContextAccessor)
        {
            FixtureRepository = fixtureRepository;
        }

        /// <summary>
        /// 查询（闲置）治具存储列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixtureStorageDto> GetList(FixtureStorageQueryDto parm)
        {
            var response = Queryable()
                .LeftJoin<FixtureBase>((it, f) => it.FixtureId == f.FixtureId)
                .LeftJoin<FixtureStorageSpace>((it, f, ss) => it.StorageId == ss.StorageId)
                .WhereIF(parm.FixtureId != null, it => it.FixtureId == parm.FixtureId)
                .WhereIF(parm.StorageId != null, it => it.StorageId == parm.StorageId)
                .WhereIF(!string.IsNullOrEmpty(parm.FixtureName), (it, f, ss) => f.FixtureName.Contains(parm.FixtureName))
                .WhereIF(!string.IsNullOrEmpty(parm.Series), (it, f, ss) => f.Series.Contains(parm.Series))
                .Select((it, f, ss) => new FixtureStorageDto
                {
                    Series = f.Series,
                    FixtureName = f.FixtureName,
                    StorageFullName = ss.StorageFullName
                }, true)
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public FixtureStorage GetInfo(FixtureStorageInfoDto parm)
        {
            var response = Queryable()
                .Where(x => x.FixtureId == parm.FixtureId && x.StorageId == parm.StorageId)
                .First();

            return response;
        }

        /// <summary>
        /// 批量入库
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool BatchInFixtureStorage(List<OperateFixtureStorageDto> models)
        {
            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                foreach (var model in models)
                {
                    InFixtureStorage(model);
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InFixtureStorage(OperateFixtureStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");

            FixtureBase fixture = FixtureRepository.GetInfo(model.FixtureId);

            FixtureStorage fs = null;
            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                fs = Context.Queryable<FixtureStorage>().Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).First();
                //更新记录数据
                FixtureStorageRecord rec = new()
                {
                    FixtureId = model.FixtureId,
                    StorageId = model.StorageId,
                    BeforeQty = fs == null ? 0 : fs.Qty,
                    ChangeQty = model.ChangeQty,

                    StorageChangeType = StorageChangeTypeConstant.入库,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                AddFixtureStorageRecord(rec);

                if (fs == null)
                {//新增入库
                    fs = new FixtureStorage()
                    {
                        FixtureId = model.FixtureId,
                        StorageId = model.StorageId,
                        Qty = model.ChangeQty
                    };
                    Insertable(fs).ExecuteReturnEntity();
                }
                else
                {
                    //更新数量
                    fs.Qty += model.ChangeQty;
                    Context.Updateable<FixtureStorage>(fs).Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).ExecuteCommand();
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool OutFixtureStorage(OperateFixtureStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");

            FixtureStorage fs = Context.Queryable<FixtureStorage>().Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).First();
            //检查出库的数量
            if (fs == null)
                throw new CustomException("未找到治具相关存储信息");
            if (fs.Qty - model.ChangeQty < 0)
                throw new CustomException($"出库数量{model.ChangeQty},超出储位库存数量{fs.Qty}");

            //修正数量的符号
            model.ChangeQty = -model.ChangeQty;

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新记录数据
                FixtureStorageRecord rec = new()
                {
                    FixtureId = model.FixtureId,
                    StorageId = model.StorageId,
                    BeforeQty = fs.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.出库,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                AddFixtureStorageRecord(rec);

                //更新数量
                fs.Qty += model.ChangeQty;
                Context.Updateable<FixtureStorage>(fs).Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 报废
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool ScrappedFixtureStorage(OperateFixtureStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");

            FixtureStorage fs = Context.Queryable<FixtureStorage>().Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).First();

            //检查数量
            if (fs == null)
                throw new CustomException("未找到治具相关存储信息");
            if (fs.Qty - model.ChangeQty < 0)
                throw new CustomException($"报废数量{model.ChangeQty},超出储位库存数量{fs.Qty}");

            //修正数量的符号
            model.ChangeQty = -model.ChangeQty;

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新记录数据
                FixtureStorageRecord rec = new()
                {
                    FixtureId = model.FixtureId,
                    StorageId = model.StorageId,
                    BeforeQty = fs.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.报废,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                AddFixtureStorageRecord(rec);

                //更新数量
                fs.Qty += model.ChangeQty;
                Context.Updateable<FixtureStorage>(fs).Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 领用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool ReceiveFixtureStorage(OperateFixtureStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");

            //通过单据领用时，检查单据信息
            if (!string.IsNullOrEmpty(model.TicketNo) && !string.IsNullOrEmpty(model.TicketType))
            {
                switch (model.TicketType)
                {
                    case TicketTypeConstant.上线通知单:
                        OnlineNoticeTicket ont = Context.Queryable<OnlineNoticeTicket>().Where(it => it.TicketNo == model.TicketNo).First();
                        model.LineId = ont.LineId;
                        model.RelatedUser = ont.InitiatorId;
                        break;

                    default:
                        throw new CustomException($"未找到类型[{model.TicketType}]单据[{model.TicketNo}]");
                }
            }

            //检查领用人信息
            if (string.IsNullOrEmpty(model.RelatedUser))
                throw new CustomException("领用人不能为空");
            if (model.LineId == null && model.LineId <= 0)
                throw new CustomException("产线不能为空");

            FixtureStorage fs = Context.Queryable<FixtureStorage>().Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).First();

            //检查数量
            if (fs == null)
                throw new CustomException("未找到治具相关存储信息");
            if (fs.Qty - model.ChangeQty < 0)
                throw new CustomException($"领用数量{model.ChangeQty},超出储位库存数量{fs.Qty}");

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新记录数据
                FixtureStorageRecord rec = new()
                {
                    FixtureId = model.FixtureId,
                    StorageId = model.StorageId,
                    BeforeQty = fs.Qty,
                    ChangeQty = -model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.领用,
                    RelatedUser = model.RelatedUser,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark + Environment.NewLine + $"领用人工号:{model.RelatedUser},产线:{model.LineId}",
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                AddFixtureStorageRecord(rec);

                //更新到领用表中
                FixtureStorageUsing us = new()
                {
                    FixtureId = model.FixtureId,
                    StorageId = model.StorageId,
                    ReceiveQty = model.ChangeQty,
                    Qty = model.ChangeQty,
                    RelatedUser = model.RelatedUser,
                    LineId = model.LineId,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime
                };
                Context.Insertable<FixtureStorageUsing>(us).ExecuteCommand();

                //更新数量
                fs.Qty -= model.ChangeQty;
                Context.Updateable<FixtureStorage>(fs).Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 批量治具领用
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public int BatchReceiveFixtureStorage(List<OperateFixtureStorageDto> models)
        {
            if (models.Count <= 0)
                throw new CustomException("未选择治具");

            //开启事务更新
            DbResult<bool> r = UseTran(() =>
            {
                //数据
                foreach (var item in models)
                {
                    ReceiveFixtureStorage(item);
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess ? models.Count : 0;
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool BackFixtureStorage(OperateFixtureStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");

            //获取要归还的占用信息
            List<FixtureStorageUsing> fsuList = new();
            if (model.FixtureUsingId > 0)
            {//根据占用信息归还
                FixtureStorageUsing fsu = Context.Queryable<FixtureStorageUsing>().Where(it => it.FixtureUsingId == model.FixtureUsingId).First();
                fsuList.Add(fsu);
            }
            else if (model.FixtureId > 0 && !string.IsNullOrEmpty(model.TicketNo))
            {//根据单据归还
                fsuList = Context.Queryable<FixtureStorageUsing>().Where(it => it.TicketNo == model.TicketNo && it.FixtureId == model.FixtureId).ToList();
            }

            //检查是否有领用信息
            if (fsuList.Count <= 0)
                throw new CustomException("未找到治具领用相关信息");
            //检查数量是否超范围
            int usingQty = fsuList.Select(it => it.Qty).Sum();
            if (usingQty < model.ChangeQty)
                throw new CustomException($"归还数量{model.ChangeQty},超出占用数量{usingQty}");

            //初始化
            model.TicketNo = fsuList[0].TicketNo;
            model.TicketType = fsuList[0].TicketType;
            model.FixtureId = fsuList[0].FixtureId;

            //归还储位治具信息
            FixtureStorage fs = Context.Queryable<FixtureStorage>().Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).First();

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新记录数据
                FixtureStorageRecord rec = new()
                {
                    FixtureId = model.FixtureId,
                    StorageId = model.StorageId,
                    BeforeQty = fs == null ? 0 : fs.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.归还,
                    RelatedUser = model.RelatedUser,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                AddFixtureStorageRecord(rec);

                //更新领用表
                int backQty = model.ChangeQty;
                foreach (FixtureStorageUsing fsu in fsuList)
                {
                    if (backQty <= 0)
                        break;
                    if (fsu.Qty <= backQty)
                    {
                        Context.Deleteable<FixtureStorageUsing>(fsu).ExecuteCommand();
                        backQty -= fsu.Qty;
                    }
                    else
                    {
                        fsu.Qty -= backQty;
                        Context.Updateable<FixtureStorageUsing>(fsu).UpdateColumns(it => it.Qty).ExecuteCommand();
                        backQty = 0;
                    }
                }

                //储位更新
                if (fs == null)
                {//新增
                    fs = new FixtureStorage
                    {
                        FixtureId = model.FixtureId,
                        StorageId = model.StorageId,
                        Qty = model.ChangeQty
                    };
                    Insertable(fs).ExecuteReturnEntity();
                }
                else
                {//更新
                    fs.Qty += model.ChangeQty;
                    Context.Updateable<FixtureStorage>(fs).Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).ExecuteCommand();
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 批量归还治具
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public int BatchBackFixtureStorage(List<OperateFixtureStorageDto> models)
        {
            if (models.Count <= 0)
                throw new CustomException("未选择治具");

            //开启事务更新
            DbResult<bool> r = UseTran(() =>
            {
                //数据
                foreach (var item in models)
                {
                    BackFixtureStorage(item);
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess ? models.Count : 0;
        }

        /// <summary>
        /// 转移
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool TransferFixtureStorage(OperateFixtureStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");
            if (model.NewStorageId == null || model.NewStorageId <= 0)
                throw new CustomException("目标储位不能为空");
            if (model.StorageId == model.NewStorageId)
                throw new CustomException("目标储位不能与原储位相同");

            //转移前储位治具信息
            FixtureStorage fs = Context.Queryable<FixtureStorage>().Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).First();
            if (fs == null)
                throw new CustomException("未找到转移前的存储信息");
            if (fs.Qty - model.ChangeQty < 0)
                throw new CustomException("转移数量超出库存数量");

            //转移后储位治具信息
            FixtureStorage newFS = Context.Queryable<FixtureStorage>().Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.NewStorageId).First();

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //**********************更新原储位数据*************************
                //更新记录数据
                FixtureStorageRecord rec = new()
                {
                    FixtureId = model.FixtureId,
                    StorageId = model.StorageId,
                    BeforeQty = fs == null ? 0 : fs.Qty,
                    ChangeQty = -model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.转移,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                AddFixtureStorageRecord(rec);

                fs.Qty -= model.ChangeQty;
                Context.Updateable<FixtureStorage>(fs).Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.StorageId).ExecuteCommand();

                //**********************更新目标储位数据*************************
                //更新记录数据
                FixtureStorageRecord newRec = new()
                {
                    FixtureId = model.FixtureId,
                    StorageId = model.NewStorageId,
                    BeforeQty = newFS == null ? 0 : newFS.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.转移,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                newRec.AfterQty = newRec.BeforeQty + newRec.ChangeQty;
                AddFixtureStorageRecord(newRec);

                //储位更新
                if (newFS == null)
                {//新增
                    newFS = new FixtureStorage
                    {
                        FixtureId = model.FixtureId,
                        StorageId = model.NewStorageId,
                        Qty = model.ChangeQty
                    };
                    Insertable(newFS).ExecuteReturnEntity();
                }
                else
                {//更新
                    newFS.Qty += model.ChangeQty;
                    Context.Updateable<FixtureStorage>(newFS).Where(it => it.FixtureId == model.FixtureId && it.StorageId == model.NewStorageId).ExecuteCommand();
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        public int DeleteFixtureStorage(FixtureStorageInfoDto parm)
        {
            //检查是否还有治具
            FixtureStorage entity = GetInfo(parm);
            if (entity == null)
                throw new CustomException("未找到要删除的治具储位信息");
            if (entity.Qty > 0)
                throw new CustomException("删除的治具储位上的库存数量不能大于0");
            return Deleteable().Where(it => it.FixtureId == parm.FixtureId && it.StorageId == parm.StorageId).ExecuteCommand();
        }

        /// <summary>
        /// 导出治具库存信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixtureStorageDto> ExportList(FixtureStorageQueryDto parm)
        {
            return GetList(parm);
        }

        /// <summary>
        /// 检查导入数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public (bool, object) ImportFixtureStorageCheck(List<FixtureStorageImportDto> list)
        {
            if (list == null || list.Count <= 0)
                throw new CustomException("未传递有效的记录");

            //治具信息
            List<int?> fixtureIds = list.Where(it => it.FixtureId > 0).Select(it => it.FixtureId).Distinct().ToList();
            List<FixtureBase> fixtureBases = Context.Queryable<FixtureBase>()
                .Where(it => fixtureIds.Contains(it.FixtureId))
                .Select(it => new FixtureBase()
                {
                    FixtureId = it.FixtureId,
                    FixtureName = it.FixtureName,
                    Series = it.Series
                })
                .ToList();
            FixtureBase tempFB = null;

            //储位信息
            List<int?> storageIds = list.Where(it => it.StorageId > 0).Select(it => it.StorageId).Distinct().ToList();
            List<FixtureStorageSpace> storageSpaces = Context.Queryable<FixtureStorageSpace>()
                .Where(it => storageIds.Contains(it.StorageId))
                .Select(it => new FixtureStorageSpace()
                {
                    StorageId = it.StorageId,
                    StorageName = it.StorageName,
                    StorageFullName = it.StorageFullName
                })
                .ToList();
            FixtureStorageSpace tempSS = null;

            //治具储位关联信息
            List<FixtureStorage> fss = Context.Queryable<FixtureStorage>().Where(it => fixtureIds.Contains(it.FixtureId)).ToList();
            FixtureStorage tempFS = null;

            //数据检查
            foreach (FixtureStorageImportDto record in list)
            {
                record.ErrorDesc = null;

                //治具ID检查
                if (record.FixtureId == null || record.FixtureId <= 0)
                {
                    record.ErrorDesc = "【治具ID】不能为空或小于等于0";
                    continue;
                }
                else
                {
                    tempFB = fixtureBases.Where(it => it.FixtureId == record.FixtureId).FirstOrDefault();
                    if (tempFB == null)
                    {
                        record.ErrorDesc = $"【治具ID:{record.FixtureId}】的信息未找到";
                        continue;
                    }
                }
                record.FixtureName = tempFB?.FixtureName;
                record.Series = tempFB?.Series;

                //储位ID检查
                if (record.StorageId == null || record.StorageId <= 0)
                {
                    record.ErrorDesc = "【储位ID】不能为空或小于等于0";
                    continue;
                }
                else
                {
                    tempSS = storageSpaces.Where(it => it.StorageId == record.StorageId).FirstOrDefault();
                    if (tempSS == null)
                    {
                        record.ErrorDesc = $"【储位ID:{record.StorageId}】的信息未找到";
                        continue;
                    }
                }
                record.StorageFullName = tempSS?.StorageFullName;

                //治具与储位的关联信息检查
                tempFS = fss.Where(it => it.FixtureId == record.FixtureId && it.StorageId == record.StorageId).FirstOrDefault();
                if (tempFS == null)
                {
                    tempFS = new FixtureStorage()
                    {
                        FixtureId = record.FixtureId.Value,
                        StorageId = record.StorageId.Value,
                        Qty = 0
                    };
                    fss.Add(tempFS);
                }

                record.OldQty = tempFS?.Qty;

                //数量检查
                if (record.Qty == null || record.Qty < 0)
                {
                    record.ErrorDesc = "【数量】不能为空或小于0";
                    continue;
                }

                //变动类型计算
                if (tempFS.Qty > record.Qty)
                {//出库：原来的数量大于导入的数量
                    record.StorageChangeType = StorageChangeTypeConstant.出库;
                }
                else if (tempFS.Qty < record.Qty)
                {//入库：原来的数量小于导入的数量
                    record.StorageChangeType = StorageChangeTypeConstant.入库;
                }

                //变动数量计算
                record.ChangeQty = Math.Abs(tempFS.Qty - record.Qty.Value);
            }

            //过滤掉未报错且变动数量为0的数据，此类数据没有库存的变化
            list = list.Where(it => !string.IsNullOrEmpty(it.ErrorDesc) || it.ChangeQty > 0).ToList();

            if (list.Where(it => it.ErrorDesc != null).Count() > 0)
                return (false, list);

            return (true, list);
        }

        /// <summary>
        /// 导入治具库存
        /// </summary>
        /// <returns></returns>
        public (bool, object) ImportFixtureStorage(List<FixtureStorageImportDto> list)
        {
            var checkReuslt = ImportFixtureStorageCheck(list);
            if (!checkReuslt.Item1)
                return checkReuslt;

            list = checkReuslt.Item2 as List<FixtureStorageImportDto>;
            if (list.Count <= 0)
                throw new CustomException("没有需要变更的数据");

            try
            {
                //开启事务，执行操作
                DbResult<bool> r = UseTran(() =>
                {
                    bool isRollback = false;
                    OperateFixtureStorageDto ocsd = null;
                    for (int i = 0; i < list.Count; i++)
                    {
                        try
                        {
                            ocsd = new OperateFixtureStorageDto()
                            {
                                FixtureId = list[i].FixtureId.Value,
                                StorageId = list[i].StorageId.Value,
                                ChangeQty = list[i].ChangeQty.Value,
                                CreateBy = list[i].CreateBy,
                                CreateTime = list[i].CreateTime,
                                Remark = "导入;" + list[i].Remark,
                            };
                            if (list[i].StorageChangeType == StorageChangeTypeConstant.入库)
                                InFixtureStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.出库)
                                OutFixtureStorage(ocsd);
                            else
                                throw new CustomException($"未知的变动类型{list[i].StorageChangeType}");
                        }
                        catch (Exception ex)
                        {
                            list[i].ErrorDesc = "异常：" + ex.Message;
                            isRollback = true;
                        }
                    }
                    //判断是否有需要回滚
                    if (isRollback)
                        throw new CustomException("操作异常");
                });

                if (!r.IsSuccess)
                {
                    return (false, list);
                }
            }
            catch (Exception)
            {
                return (false, list);
            }
            return (true, list);
        }

        /// <summary>
        /// 【操作导入】检查导入数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public (bool, object) ImportFixtureStorageOperateCheck(List<FixtureStorageOperateImportDto> list)
        {
            if (list == null || list.Count <= 0)
                throw new CustomException("未传递有效的记录");

            //变动类型，两个数组一一对应
            string[] changeTypeLabels = new string[] { "入库", "出库", "领用", "归还", "报废" };
            string[] changeTypes = new string[] { StorageChangeTypeConstant.入库, StorageChangeTypeConstant.出库, StorageChangeTypeConstant.领用, StorageChangeTypeConstant.归还, StorageChangeTypeConstant.报废 };

            //治具信息
            List<int?> fixtureIds = list.Where(it => it.FixtureId > 0).Select(it => it.FixtureId).Distinct().ToList();
            List<FixtureBase> fixtureBases = Context.Queryable<FixtureBase>()
                .Where(it => fixtureIds.Contains(it.FixtureId))
                .Select(it => new FixtureBase()
                {
                    FixtureId = it.FixtureId,
                    FixtureName = it.FixtureName,
                    Series = it.Series
                })
                .ToList();
            FixtureBase tempFB = null;

            //储位信息
            List<int?> storageIds = list.Where(it => it.StorageId > 0).Select(it => it.StorageId).Distinct().ToList();
            List<FixtureStorageSpace> storageSpaces = Context.Queryable<FixtureStorageSpace>()
                .Where(it => storageIds.Contains(it.StorageId))
                .Select(it => new FixtureStorageSpace()
                {
                    StorageId = it.StorageId,
                    StorageName = it.StorageName,
                    StorageFullName = it.StorageFullName
                })
                .ToList();
            FixtureStorageSpace tempSS = null;

            //治具储位关联信息
            List<FixtureStorage> fss = Context.Queryable<FixtureStorage>().Where(it => fixtureIds.Contains(it.FixtureId)).ToList();
            FixtureStorage tempFS = null;

            //治具领用信息
            List<FixtureStorageUsing> fsu = Context.Queryable<FixtureStorageUsing>().Where(it => fixtureIds.Contains(it.FixtureId)).ToList();
            FixtureStorageUsing tempFSU = null;

            //线别信息
            List<int?> lineIds = list.Where(it => it.LineId > 0).Select(it => it.LineId).Distinct().ToList();
            List<string> lineNames = list.Where(it => it.LineName != null).Select(it => it.LineName).Distinct().ToList();
            List<Line> lines = Context.Queryable<Line>().Where(it => lineNames.Contains(it.LineName) || lineIds.Contains(it.LineId)).ToList();
            Line tempLine = null;

            //人员信息
            List<string> empCodes = list.Where(it => it.RelatedUser != null).Select(it => it.RelatedUser).Distinct().ToList();
            List<string> empNames = list.Where(it => it.RelatedUserName != null).Select(it => it.RelatedUserName).Distinct().ToList();
            List<Employee> emps = Context.Queryable<Employee>().Where(it => empCodes.Contains(it.EmpCode) || empNames.Contains(it.EmpName)).ToList();
            Employee tempEmp = null;

            //数据检查
            foreach (FixtureStorageOperateImportDto record in list)
            {
                record.ErrorDesc = null;

                //治具ID检查
                if (record.FixtureId == null || record.FixtureId <= 0)
                {
                    record.ErrorDesc = "【治具ID】不能为空或小于等于0";
                    continue;
                }
                else
                {
                    tempFB = fixtureBases.Where(it => it.FixtureId == record.FixtureId).FirstOrDefault();
                    if (tempFB == null)
                    {
                        record.ErrorDesc = $"【治具ID:{record.FixtureId}】的信息未找到";
                        continue;
                    }
                }
                record.FixtureName = tempFB?.FixtureName;
                record.Series = tempFB?.Series;
                //储位ID检查
                if (record.StorageId == null || record.StorageId <= 0)
                {
                    record.ErrorDesc = "【储位ID】不能为空或小于等于0";
                    continue;
                }
                else
                {
                    tempSS = storageSpaces.Where(it => it.StorageId == record.StorageId).FirstOrDefault();
                    if (tempSS == null)
                    {
                        record.ErrorDesc = $"【储位ID:{record.StorageId}】的信息未找到";
                        continue;
                    }
                }
                record.StorageFullName = tempSS?.StorageFullName;
                //治具与储位的关联信息检查
                tempFS = fss.Where(it => it.FixtureId == record.FixtureId && it.StorageId == record.StorageId).FirstOrDefault();
                if (tempFS == null)
                {
                    record.ErrorDesc = "未找到治具与储位关联的信息";
                    continue;
                }
                //变动类型检查
                if (string.IsNullOrEmpty(record.StorageChangeType))
                {
                    if (record.StorageChangeTypeLabel == null || !changeTypeLabels.Contains(record.StorageChangeTypeLabel))
                    {
                        record.ErrorDesc = $"【变动类型】不能为空，且值只能为【{string.Join(",", changeTypeLabels)}】之一";
                        continue;
                    }
                    else
                    {
                        record.StorageChangeType = changeTypes[Array.IndexOf(changeTypeLabels, record.StorageChangeTypeLabel)];
                    }
                }
                else if (!changeTypes.Contains(record.StorageChangeType))
                {
                    record.ErrorDesc = $"【变动类型】未知,当前值【{record.StorageChangeType}】";
                    continue;
                }
                //变动数量检查
                if (record.ChangeQty == null || record.ChangeQty <= 0)
                {
                    record.ErrorDesc = "【变动数量】不能为空或小于等于0";
                    continue;
                }
                if (record.StorageChangeType == StorageChangeTypeConstant.出库 || record.StorageChangeType == StorageChangeTypeConstant.领用 || record.StorageChangeType == StorageChangeTypeConstant.报废)
                {
                    tempFS.Qty -= (int)record.ChangeQty;
                    if (tempFS.Qty < 0)
                    {
                        record.ErrorDesc = $"变动数量超过储位库存数量,当前储位数量:{tempFS.Qty + record.ChangeQty.Value}";
                        continue;
                    }
                }
                else
                    tempFS.Qty += (int)record.ChangeQty;

                //领用归还检查
                if (record.StorageChangeType == StorageChangeTypeConstant.领用 || record.StorageChangeType == StorageChangeTypeConstant.归还)
                {
                    //人员检查
                    if (string.IsNullOrEmpty(record.RelatedUser) && string.IsNullOrEmpty(record.RelatedUserName))
                    {
                        record.ErrorDesc = "【领用】或【归还】操作，领用人员不能为空";
                        continue;
                    }
                    else if (!string.IsNullOrEmpty(record.RelatedUser))
                    {
                        tempEmp = emps.Where(it => it.EmpCode == record.RelatedUser).FirstOrDefault();
                        if (tempEmp == null)
                        {
                            record.ErrorDesc = $"不存在人员工号【{record.RelatedUser}】的信息";
                            continue;
                        }
                    }
                    else
                    {
                        var listEmp = emps.Where(it => it.EmpName == record.RelatedUserName).ToList();
                        if (listEmp.Count > 1)
                        {
                            record.ErrorDesc = $"人员姓名【{record.RelatedUserName}】存在多个同名";
                            continue;
                        }
                        else if (listEmp.Count <= 0)
                        {
                            record.ErrorDesc = $"不存在人员姓名【{record.RelatedUserName}】的信息";
                            continue;
                        }
                        tempEmp = listEmp.FirstOrDefault();
                    }
                    record.RelatedUser = tempEmp?.EmpCode;
                    record.RelatedUserName = tempEmp?.EmpName;

                    //产线检查
                    if (record.LineId > 0)
                    {
                        tempLine = lines.Where(it => it.LineId == record.LineId).FirstOrDefault();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(record.LineName))
                        {
                            record.ErrorDesc = "【领用】或【归还】操作，产线不能为空";
                            continue;
                        }
                        tempLine = lines.Where(it => it.LineName == record.LineName).FirstOrDefault();
                    }
                    if (tempLine == null)
                    {
                        record.ErrorDesc = $"不存在产线【{record.LineName}】的信息";
                        continue;
                    }
                    record.LineId = tempLine?.LineId;
                    record.LineName = tempLine?.LineName;

                    //归还操作，检查领用信息
                    if (record.StorageChangeType == StorageChangeTypeConstant.归还)
                    {
                        tempFSU = fsu.Where(it => it.LineId == tempLine.LineId && it.FixtureId == record.FixtureId && it.StorageId == record.StorageId).FirstOrDefault();
                        if (tempFSU == null)
                        {
                            record.ErrorDesc = $"未找到需要归还的领用治具信息";
                            continue;
                        }
                        tempFSU.Qty -= (int)record.ChangeQty;
                        if (tempFSU.Qty < 0)
                        {
                            record.ErrorDesc = $"归还数量超过领用占用数量，当前占用:{tempFSU.Qty + record.ChangeQty.Value}";
                            continue;
                        }
                        record.FixtureUsingId = tempFSU.FixtureUsingId;
                    }
                }
            }

            if (list.Where(it => it.ErrorDesc != null).Count() > 0)
                return (false, list);

            return (true, list);
        }

        /// <summary>
        /// 【操作导入】导入治具库存变更
        /// </summary>
        /// <returns></returns>
        public (bool, object) ImportFixtureStorageOperate(List<FixtureStorageOperateImportDto> list)
        {
            var checkReuslt = ImportFixtureStorageOperateCheck(list);
            if (!checkReuslt.Item1)
                return checkReuslt;

            try
            {
                //开启事务，执行操作
                DbResult<bool> r = UseTran(() =>
                {
                    bool isRollback = false;
                    OperateFixtureStorageDto ocsd = null;
                    for (int i = 0; i < list.Count; i++)
                    {
                        try
                        {
                            ocsd = new OperateFixtureStorageDto()
                            {
                                FixtureId = list[i].FixtureId.Value,
                                StorageId = list[i].StorageId.Value,
                                ChangeQty = list[i].ChangeQty.Value,
                                CreateBy = list[i].CreateBy,
                                CreateTime = list[i].CreateTime,
                                Remark = "操作导入;" + list[i].Remark,
                                RelatedUser = list[i].RelatedUser,
                                LineId = list[i].LineId,
                                FixtureUsingId = list[i].FixtureUsingId
                            };
                            if (list[i].StorageChangeType == StorageChangeTypeConstant.入库)
                                InFixtureStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.出库)
                                OutFixtureStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.领用)
                                ReceiveFixtureStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.归还)
                                BackFixtureStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.报废)
                                ScrappedFixtureStorage(ocsd);
                            else
                                throw new CustomException($"未知的变动类型{list[i].StorageChangeType}");
                        }
                        catch (Exception ex)
                        {
                            list[i].ErrorDesc = "异常：" + ex.Message;
                            isRollback = true;
                        }
                    }
                    //判断是否有需要回滚
                    if (isRollback)
                        throw new CustomException("操作异常");
                });

                if (!r.IsSuccess)
                {
                    return (false, list);
                }
            }
            catch (Exception)
            {
                return (false, list);
            }
            return (true, list);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<FixtureStorage> QueryExp(FixtureStorageQueryDto parm)
        {
            var predicate = Expressionable.Create<FixtureStorage>();
            predicate = predicate.AndIF(parm.FixtureId != null, it => it.FixtureId == parm.FixtureId);
            return predicate;
        }

        /// <summary>
        /// 判断治具是否已存在此储位
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private bool IsExist(FixtureStorage parm)
        {
            var response = Queryable()
                .Where(x => x.FixtureId == parm.FixtureId && x.StorageId == parm.StorageId)
                .First();
            return response != null;
        }

        #region 占用治具

        /// <summary>
        /// 查询（使用中）治具存储列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixtureStorageUsingDto> GetUsingList(FixtureStorageUsingQueryDto parm)
        {
            var response = Context.Queryable<FixtureStorageUsing>()
                .LeftJoin<FixtureBase>((it, f) => it.FixtureId == f.FixtureId)
                .LeftJoin<FixtureStorageSpace>((it, f, ss) => it.StorageId == ss.StorageId)
                .LeftJoin<Employee>((it, f, ss, e) => it.RelatedUser == e.EmpCode)
                .LeftJoin<Line>((it, f, ss, e, l) => it.LineId == l.LineId)
                .WhereIF(parm.FixtureId != null, it => it.FixtureId == parm.FixtureId)
                .WhereIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo)
                .WhereIF(parm.LineId > 0, it => it.LineId == parm.LineId)
                .Select((it, f, ss, e, l) => new FixtureStorageUsingDto
                {
                    Series = f.Series,
                    FixtureName = f.FixtureName,
                    RelatedUserName = e.EmpName,
                    LineName = l.LineName,
                    StorageFullName = ss.StorageFullName
                }, true)
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取领用详情
        /// </summary>
        /// <param name="fixtureUsingId"></param>
        /// <returns></returns>
        public FixtureStorageUsing GetUsingInfo(int fixtureUsingId)
        {
            var response = Context.Queryable<FixtureStorageUsing>()
                .Where(x => x.FixtureUsingId == fixtureUsingId)
                .First();

            return response;
        }

        #endregion 占用治具

        #region 存储记录 相关

        /// <summary>
        /// 查询治具出入库记录表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixtureStorageRecordDto> GetRecordList(FixtureStorageRecordQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Context.Queryable<FixtureStorageRecord>()
                .LeftJoin<FixtureBase>((it, f) => it.FixtureId == f.FixtureId)
                .LeftJoin<FixtureStorageSpace>((it, f, ss) => it.StorageId == ss.StorageId)
                .LeftJoin<Employee>((it, f, ss, e) => it.RelatedUser == e.EmpCode)
                .Where(predicate.ToExpression())
                .OrderByIF(parm.Sort?.ToLower() == "createtime", it => it.CreateTime, parm.SortType.ToLower().StartsWith("asc") ? OrderByType.Asc : OrderByType.Desc)
                .Select((it, f, ss, e) => new FixtureStorageRecordDto
                {
                    Series = f.Series,
                    FixtureName = f.FixtureName,
                    StorageFullName = ss.StorageFullName,
                    RelatedUserName = e.EmpName
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 添加治具出入库记录表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FixtureStorageRecord AddFixtureStorageRecord(FixtureStorageRecord model)
        {
            return Context.Insertable<FixtureStorageRecord>(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<FixtureStorageRecord> QueryExp(FixtureStorageRecordQueryDto parm)
        {
            var predicate = Expressionable.Create<FixtureStorageRecord>();
            predicate = predicate.AndIF(parm.FixtureId != null, it => it.FixtureId == parm.FixtureId);
            predicate = predicate.AndIF(parm.StorageId != null, it => it.StorageId == parm.StorageId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TicketType), it => it.TicketType == parm.TicketType);
            return predicate;
        }

        #endregion 存储记录 相关
    }
}
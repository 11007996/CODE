using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Constant;
using EAM.Model.Consumable;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Consumable.IConsumableService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Consumable
{
    /// <summary>
    /// 耗品存储表Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IConsumableStorageService), ServiceLifetime = LifeTime.Transient)]
    public class ConsumableStorageService : BaseService<ConsumableStorage>, IConsumableStorageService
    {
        public ConsumableStorageService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询耗品存储表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ConsumableStorageDto> GetList(ConsumableStorageQueryDto parm)
        {
            var response = Queryable().LeftJoin<ConsumableStorageSpace>((it, ss) => it.StorageId == ss.StorageId)
                .LeftJoin<ConsumableBase>((it, ss, c) => it.ConsumableId == c.ConsumableId)
                .WhereIF(parm.ConsumableId != null, (it, ss, c) => it.ConsumableId == parm.ConsumableId)
                .WhereIF(parm.StorageId != null, (it, ss, c) => it.StorageId == parm.StorageId)
                .WhereIF(!string.IsNullOrEmpty(parm.Category), (it, ss, c) => c.Category == parm.Category)
                .WhereIF(!string.IsNullOrEmpty(parm.ConsumablePart), (it, ss, c) => c.ConsumablePart == parm.ConsumablePart)
                .WhereIF(!string.IsNullOrEmpty(parm.ConsumableName), (it, ss, c) => c.ConsumableName.Contains(parm.ConsumableName))
                .WhereIF(!string.IsNullOrEmpty(parm.Spec), (it, ss, c) => c.Spec.Contains(parm.Spec))
                .Select((it, ss, c) => new ConsumableStorageDto
                {
                    ConsumablePart = c.ConsumablePart,
                    ConsumableName = c.ConsumableName,
                    Spec = c.Spec,
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
        public ConsumableStorage GetInfo(ConsumableStorageInfoDto parm)
        {
            var response = Queryable()
                .Where(x => x.ConsumableId == parm.ConsumableId && x.StorageId == parm.StorageId)
                .First();

            return response;
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InConsumableStorage(OperateConsumableStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");

            ConsumableStorage cs = null;
            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                cs = Context.Queryable<ConsumableStorage>().Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).First();
                //更新记录数据
                ConsumableStorageRecord rec = new()
                {
                    ConsumableId = model.ConsumableId,
                    StorageId = model.StorageId,
                    BeforeQty = cs == null ? 0 : cs.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.入库,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                Context.Insertable(rec).ExecuteCommand();

                if (cs == null)
                {//新增入库
                    cs = new ConsumableStorage()
                    {
                        ConsumableId = model.ConsumableId,
                        StorageId = model.StorageId,
                        Qty = model.ChangeQty
                    };
                    Insertable(cs).ExecuteReturnEntity();
                }
                else
                {
                    //更新数量
                    cs.Qty += model.ChangeQty;
                    Context.Updateable<ConsumableStorage>(cs).Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).ExecuteCommand();
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
        public bool OutConsumableStorage(OperateConsumableStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");

            ConsumableStorage cs = Context.Queryable<ConsumableStorage>().Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).First();
            //检查出库的数量
            if (cs == null)
                throw new CustomException("未找到耗品相关存储信息");
            if (cs.Qty - model.ChangeQty < 0)
                throw new CustomException($"出库数量{model.ChangeQty},超出储位库存数量{cs.Qty}");

            //转换数量符号
            model.ChangeQty = -model.ChangeQty;

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新记录数据
                ConsumableStorageRecord rec = new()
                {
                    ConsumableId = model.ConsumableId,
                    StorageId = model.StorageId,
                    BeforeQty = cs.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.出库,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                Context.Insertable(rec).ExecuteCommand();

                //更新数量
                cs.Qty += model.ChangeQty;
                Context.Updateable<ConsumableStorage>(cs).UpdateColumns(it => it.Qty).Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).ExecuteCommand();
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
        public bool ScrappedConsumableStorage(OperateConsumableStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");

            ConsumableStorage cs = Context.Queryable<ConsumableStorage>().Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).First();

            //检查数量
            if (cs == null)
                throw new CustomException("未找到耗品相关存储信息");
            if (cs.Qty - model.ChangeQty < 0)
                throw new CustomException($"报废数量{model.ChangeQty},超出储位库存数量{cs.Qty}");

            //转换数量符号
            model.ChangeQty = -model.ChangeQty;

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新记录数据
                ConsumableStorageRecord rec = new()
                {
                    ConsumableId = model.ConsumableId,
                    StorageId = model.StorageId,
                    BeforeQty = cs.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.报废,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                Context.Insertable(rec).ExecuteCommand();

                //更新数量
                cs.Qty += model.ChangeQty;
                Context.Updateable<ConsumableStorage>(cs).Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).ExecuteCommand();
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
        public bool ReceiveConsumableStorage(OperateConsumableStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");
            if (string.IsNullOrEmpty(model.RelatedUser))
                throw new CustomException("领用人不能为空");

            ConsumableStorage cs = Context.Queryable<ConsumableStorage>().Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).First();

            //检查数量
            if (cs == null)
                throw new CustomException("未找到耗品相关存储信息");
            if (cs.Qty - model.ChangeQty < 0)
                throw new CustomException($"领用数量{model.ChangeQty},超出储位库存数量{cs.Qty}");

            //转换数量符号
            model.ChangeQty = -model.ChangeQty;

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新记录数据
                ConsumableStorageRecord rec = new()
                {
                    ConsumableId = model.ConsumableId,
                    StorageId = model.StorageId,
                    BeforeQty = cs.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.领用,
                    RelatedUser = model.RelatedUser,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark + Environment.NewLine + $"产线Id:{model.LineId}",
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                Context.Insertable(rec).ExecuteCommand();

                //更新数量
                cs.Qty += model.ChangeQty;
                Context.Updateable<ConsumableStorage>(cs).Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool BackConsumableStorage(OperateConsumableStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");
            if (string.IsNullOrEmpty(model.RelatedUser))
                throw new CustomException("归还人不能为空");

            //归还储位耗品信息
            ConsumableStorage cs = Context.Queryable<ConsumableStorage>().Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).First();
            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新记录数据
                ConsumableStorageRecord rec = new()
                {
                    ConsumableId = model.ConsumableId,
                    StorageId = model.StorageId,
                    BeforeQty = cs == null ? 0 : cs.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.归还,
                    RelatedUser = model.RelatedUser,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark + Environment.NewLine + $"产线Id:{model.LineId}",
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                Context.Insertable(rec).ExecuteCommand();

                //储位更新
                if (cs == null)
                {//新增
                    cs = new ConsumableStorage
                    {
                        ConsumableId = model.ConsumableId,
                        StorageId = model.StorageId,
                        Qty = model.ChangeQty
                    };
                    Insertable(cs).ExecuteReturnEntity();
                }
                else
                {//更新
                    cs.Qty += model.ChangeQty;
                    Context.Updateable<ConsumableStorage>(cs).Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).ExecuteCommand();
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 转移
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool TransferConsumableStorage(OperateConsumableStorageDto model)
        {
            //参数检查
            if (model.ChangeQty <= 0)
                throw new CustomException("数量不能小于等于0");
            if (model.StorageId <= 0)
                throw new CustomException("储位ID不能为空");
            if (model.NewStorageId <= 0)
                throw new CustomException("新的目标储位ID不能为空");
            if (model.StorageId == model.NewStorageId)
                throw new CustomException("目标储位不能与原储位相同");

            //转移前储位耗品信息
            ConsumableStorage cs = Context.Queryable<ConsumableStorage>().Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).First();
            if (cs == null)
                throw new CustomException("未找到转移前的存储信息");
            if (cs.Qty - model.ChangeQty < 0)
                throw new CustomException("转移数量超出库存数量");

            //转移后储位耗品信息
            ConsumableStorage newCS = Context.Queryable<ConsumableStorage>().Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.NewStorageId).First();

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //**********************更新原储位数据*************************
                //更新记录数据
                ConsumableStorageRecord rec = new()
                {
                    ConsumableId = model.ConsumableId,
                    StorageId = model.StorageId,
                    BeforeQty = cs == null ? 0 : cs.Qty,
                    ChangeQty = -model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.转移,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                rec.AfterQty = rec.BeforeQty + rec.ChangeQty;
                Context.Insertable(rec).ExecuteCommand();

                cs.Qty -= model.ChangeQty;
                Context.Updateable<ConsumableStorage>(cs).Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).ExecuteCommand();

                //**********************更新目标储位数据*************************
                //更新记录数据
                ConsumableStorageRecord newRec = new()
                {
                    ConsumableId = model.ConsumableId,
                    StorageId = model.NewStorageId,
                    BeforeQty = newCS == null ? 0 : newCS.Qty,
                    ChangeQty = model.ChangeQty,
                    StorageChangeType = StorageChangeTypeConstant.转移,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                newRec.AfterQty = newRec.BeforeQty + newRec.ChangeQty;
                Context.Insertable(newRec).ExecuteCommand();

                //储位更新
                if (newCS == null)
                {//新增
                    newCS = new ConsumableStorage
                    {
                        ConsumableId = model.ConsumableId,
                        StorageId = model.NewStorageId,
                        Qty = model.ChangeQty
                    };
                    Insertable(newCS).ExecuteReturnEntity();
                }
                else
                {//更新
                    newCS.Qty += model.ChangeQty;
                    Context.Updateable<ConsumableStorage>(newCS).Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.NewStorageId).ExecuteCommand();
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 删除耗品储位
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int DeleteConsumableStorage(ConsumableStorageInfoDto parm)
        {
            //检查是否还有耗品
            ConsumableStorage entity = GetInfo(parm);
            if (entity == null)
                throw new CustomException("未找到要删除的耗品储位信息");
            if (entity.Qty > 0)
                throw new CustomException("删除的耗品储位上的库存数量不能大于0");
            return Deleteable().Where(it => it.ConsumableId == parm.ConsumableId && it.StorageId == parm.StorageId).ExecuteCommand();
        }

        /// <summary>
        /// 导出耗品库存信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ConsumableStorageDto> ExportList(ConsumableStorageQueryDto parm)
        {
            return GetList(parm);
        }

        /// <summary>
        /// 检查导入数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public (bool, object) ImportConsumableStorageCheck(List<ConsumableStorageImportDto> list)
        {
            if (list == null || list.Count <= 0)
                throw new CustomException("未传递有效的记录");

            //检查是否有相同的耗品与库存关联数据
            var groupList = list.GroupBy(it => new { it.ConsumableId, it.StorageId }).Select(g => new ConsumableStorageImportDto { ConsumableId = g.Key.ConsumableId, StorageId = g.Key.StorageId, ErrorDesc = g.Count().ToString() }).Where(g => int.Parse(g.ErrorDesc) > 1).ToList();
            if (groupList != null && groupList.Count > 0)
            {
                List<string> msgs = new List<string>();
                foreach (var g in groupList)
                {
                    msgs.Add($"【耗品ID:{g.ConsumableId},储位ID:{g.StorageId}】");
                }
                throw new CustomException("存在多个相同的耗品与库位数据：" + string.Join(';', msgs));
            }

            //耗品信息
            List<int?> consumableIds = list.Where(it => it.ConsumableId > 0).Select(it => it.ConsumableId).Distinct().ToList();
            List<ConsumableBase> consumableBases = Context.Queryable<ConsumableBase>()
                .Where(it => consumableIds.Contains(it.ConsumableId))
                .Select(it => new ConsumableBase()
                {
                    ConsumableId = it.ConsumableId,
                    ConsumableName = it.ConsumableName,
                    ConsumablePart = it.ConsumablePart,
                    Spec = it.Spec
                })
                .ToList();
            ConsumableBase tempCB = null;

            //储位信息
            List<int?> storageIds = list.Where(it => it.StorageId > 0).Select(it => it.StorageId).Distinct().ToList();
            List<ConsumableStorageSpace> storageSpaces = Context.Queryable<ConsumableStorageSpace>()
                .Where(it => storageIds.Contains(it.StorageId))
                .Select(it => new ConsumableStorageSpace()
                {
                    StorageId = it.StorageId,
                    StorageName = it.StorageName,
                    StorageFullName = it.StorageFullName
                })
                .ToList();
            ConsumableStorageSpace tempSS = null;

            //耗品储位关联信息
            List<ConsumableStorage> fss = Context.Queryable<ConsumableStorage>().Where(it => consumableIds.Contains(it.ConsumableId)).ToList();
            ConsumableStorage tempCS = null;

            //数据检查
            foreach (ConsumableStorageImportDto record in list)
            {
                record.ErrorDesc = null;

                //耗品ID检查
                if (record.ConsumableId == null || record.ConsumableId <= 0)
                {
                    record.ErrorDesc = "【耗品ID】不能为空或小于等于0";
                    continue;
                }
                else
                {
                    tempCB = consumableBases.Where(it => it.ConsumableId == record.ConsumableId).FirstOrDefault();
                    if (tempCB == null)
                    {
                        record.ErrorDesc = $"【耗品ID:{record.ConsumableId}】的信息未找到";
                        continue;
                    }
                }
                record.ConsumableName = tempCB?.ConsumableName;
                record.ConsumablePart = tempCB?.ConsumablePart;
                record.Spec = tempCB?.Spec;

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

                //耗品与储位的关联信息检查
                tempCS = fss.Where(it => it.ConsumableId == record.ConsumableId && it.StorageId == record.StorageId).FirstOrDefault();
                if (tempCS == null)
                {
                    tempCS = new ConsumableStorage()
                    {
                        ConsumableId = record.ConsumableId.Value,
                        StorageId = record.StorageId.Value,
                        Qty = 0
                    };
                    fss.Add(tempCS);
                }
                record.OldQty = tempCS?.Qty;

                //数量检查
                if (record.Qty == null || record.Qty < 0)
                {
                    record.ErrorDesc = "【数量】不能为空或小于0";
                    continue;
                }

                //变动类型计算
                if (tempCS.Qty > record.Qty)
                {//出库：原来的数量大于导入的数量
                    record.StorageChangeType = StorageChangeTypeConstant.出库;
                }
                else if (tempCS.Qty < record.Qty)
                {//入库：原来的数量小于导入的数量
                    record.StorageChangeType = StorageChangeTypeConstant.入库;
                }

                //变动数量计算
                record.ChangeQty = Math.Abs(tempCS.Qty - record.Qty.Value);
            }

            //过滤掉未报错且变动数量为0的数据，此类数据没有库存的变化
            list = list.Where(it => !string.IsNullOrEmpty(it.ErrorDesc) || it.ChangeQty > 0).ToList();

            if (list.Where(it => !string.IsNullOrEmpty(it.ErrorDesc)).Count() > 0)
                return (false, list);

            return (true, list);
        }

        /// <summary>
        /// 导入耗品库存
        /// </summary>
        /// <returns></returns>
        public (bool, object) ImportConsumableStorage(List<ConsumableStorageImportDto> list)
        {
            var checkReuslt = ImportConsumableStorageCheck(list);
            if (!checkReuslt.Item1)
                return checkReuslt;

            list = checkReuslt.Item2 as List<ConsumableStorageImportDto>;
            if (list.Count <= 0)
                throw new CustomException("没有需要变更的数据");

            try
            {
                //开启事务，执行操作
                DbResult<bool> r = UseTran(() =>
                {
                    bool isRollback = false;
                    OperateConsumableStorageDto ocsd = null;
                    for (int i = 0; i < list.Count; i++)
                    {
                        try
                        {
                            ocsd = new OperateConsumableStorageDto()
                            {
                                ConsumableId = list[i].ConsumableId.Value,
                                StorageId = list[i].StorageId.Value,
                                ChangeQty = list[i].ChangeQty.Value,
                                CreateBy = list[i].CreateBy,
                                CreateTime = list[i].CreateTime,
                                Remark = "导入操作;" + list[i].Remark,
                            };
                            if (list[i].StorageChangeType == StorageChangeTypeConstant.入库)
                                InConsumableStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.出库)
                                OutConsumableStorage(ocsd);
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
        public (bool, object) ImportConsumableStorageOperateCheck(List<ConsumableStorageOperateImportDto> list)
        {
            if (list == null || list.Count <= 0)
                throw new CustomException("未传递有效的记录");

            //变动类型，两个数组一一对应
            string[] changeTypeLabels = new string[] { "入库", "出库", "领用", "归还", "报废" };
            string[] changeTypes = new string[] { StorageChangeTypeConstant.入库, StorageChangeTypeConstant.出库, StorageChangeTypeConstant.领用, StorageChangeTypeConstant.归还, StorageChangeTypeConstant.报废 };

            //耗品信息
            List<int?> consumableIds = list.Where(it => it.ConsumableId > 0).Select(it => it.ConsumableId).Distinct().ToList();
            List<ConsumableBase> consumableBases = Context.Queryable<ConsumableBase>()
                .Where(it => consumableIds.Contains(it.ConsumableId))
                .Select(it => new ConsumableBase()
                {
                    ConsumableId = it.ConsumableId,
                    ConsumableName = it.ConsumableName,
                    ConsumablePart = it.ConsumablePart,
                    Spec = it.Spec
                })
                .ToList();
            ConsumableBase tempCB = null;

            //储位信息
            List<int?> storageIds = list.Where(it => it.StorageId > 0).Select(it => it.StorageId).Distinct().ToList();
            List<ConsumableStorageSpace> storageSpaces = Context.Queryable<ConsumableStorageSpace>()
                .Where(it => storageIds.Contains(it.StorageId))
                .Select(it => new ConsumableStorageSpace()
                {
                    StorageId = it.StorageId,
                    StorageName = it.StorageName,
                    StorageFullName = it.StorageFullName
                })
                .ToList();
            ConsumableStorageSpace tempSS = null;

            //耗品储位关联信息
            List<ConsumableStorage> fss = Context.Queryable<ConsumableStorage>().Where(it => consumableIds.Contains(it.ConsumableId)).ToList();
            ConsumableStorage tempCS = null;

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
            foreach (ConsumableStorageOperateImportDto record in list)
            {
                record.ErrorDesc = null;

                //耗品ID检查
                if (record.ConsumableId == null || record.ConsumableId <= 0)
                {
                    record.ErrorDesc = "【耗品ID】不能为空或小于等于0";
                    continue;
                }
                else
                {
                    tempCB = consumableBases.Where(it => it.ConsumableId == record.ConsumableId).FirstOrDefault();
                    if (tempCB == null)
                    {
                        record.ErrorDesc = $"【耗品ID:{record.ConsumableId}】的信息未找到";
                        continue;
                    }
                }
                record.ConsumableName = tempCB?.ConsumableName;
                record.ConsumablePart = tempCB?.ConsumablePart;
                record.Spec = tempCB?.Spec;
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
                //耗品与储位的关联信息检查
                tempCS = fss.Where(it => it.ConsumableId == record.ConsumableId && it.StorageId == record.StorageId).FirstOrDefault();
                if (tempCS == null)
                {
                    record.ErrorDesc = "未找到耗品与储位关联的信息";
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
                    tempCS.Qty -= (int)record.ChangeQty;
                    if (tempCS.Qty < 0)
                    {
                        record.ErrorDesc = $"变动数量超过储位库存数量,当前储位数量:{tempCS.Qty + record.ChangeQty.Value}";
                        continue;
                    }
                }
                else
                    tempCS.Qty += (int)record.ChangeQty;

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
                        if (tempLine == null)
                        {
                            record.ErrorDesc = $"不存在产线【{record.LineName}】的信息";
                            continue;
                        }
                    }
                    record.LineId = tempLine?.LineId;
                    record.LineName = tempLine?.LineName;
                }
            }

            if (list.Where(it => it.ErrorDesc != null).Count() > 0)
                return (false, list);

            return (true, list);
        }

        /// <summary>
        /// 【操作导入】导入耗品库存操作记录
        /// </summary>
        /// <returns></returns>
        public (bool, object) ImportConsumableStorageOperate(List<ConsumableStorageOperateImportDto> list)
        {
            var checkReuslt = ImportConsumableStorageOperateCheck(list);
            if (!checkReuslt.Item1)
                return checkReuslt;

            try
            {
                //开启事务，执行操作
                DbResult<bool> r = UseTran(() =>
                {
                    bool isRollback = false;
                    OperateConsumableStorageDto ocsd = null;
                    for (int i = 0; i < list.Count; i++)
                    {
                        try
                        {
                            ocsd = new OperateConsumableStorageDto()
                            {
                                ConsumableId = list[i].ConsumableId.Value,
                                StorageId = list[i].StorageId.Value,
                                ChangeQty = list[i].ChangeQty.Value,
                                CreateBy = list[i].CreateBy,
                                CreateTime = list[i].CreateTime,
                                Remark = "操作导入;" + list[i].Remark,
                                RelatedUser = list[i].RelatedUser,
                                LineId = list[i].LineId
                            };
                            if (list[i].StorageChangeType == StorageChangeTypeConstant.入库)
                                InConsumableStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.出库)
                                OutConsumableStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.领用)
                                ReceiveConsumableStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.归还)
                                BackConsumableStorage(ocsd);
                            else if (list[i].StorageChangeType == StorageChangeTypeConstant.报废)
                                ScrappedConsumableStorage(ocsd);
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
        /// 查询库存变更记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ConsumableStorageRecordDto> GetRecordList(ConsumableStorageRecordQueryDto parm)
        {
            var predicate = Expressionable.Create<ConsumableStorageRecord>();
            predicate.AndIF(parm.ConsumableId != null, it => it.ConsumableId == parm.ConsumableId);
            predicate.AndIF(parm.StorageId != null, it => it.StorageId == parm.StorageId);
            predicate.AndIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo);
            predicate.AndIF(!string.IsNullOrEmpty(parm.CreateBy), it => it.CreateBy == parm.CreateBy);

            var response = Context.Queryable<ConsumableStorageRecord>().LeftJoin<ConsumableStorageSpace>((it, ss) => it.StorageId == ss.StorageId)
                .LeftJoin<ConsumableBase>((it, ss, c) => it.ConsumableId == c.ConsumableId)
                .LeftJoin<Employee>((it, ss, c, e) => it.RelatedUser == e.EmpCode)
                .Where(predicate.ToExpression())
                .WhereIF(!string.IsNullOrEmpty(parm.Category), (it, ss, c) => c.Category == parm.Category)
                .OrderByIF(parm.Sort?.ToLower() == "createtime", it => it.CreateTime, parm.SortType.ToLower().StartsWith("asc") ? OrderByType.Asc : OrderByType.Desc)
                .Select((it, ss, c, e) => new ConsumableStorageRecordDto
                {
                    ConsumablePart = c.ConsumablePart,
                    ConsumableName = c.ConsumableName,
                    Spec = c.Spec,
                    StorageFullName = ss.StorageFullName,
                    RelatedUserName = e.EmpName
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ConsumableStorage> QueryExp(ConsumableStorageQueryDto parm)
        {
            var predicate = Expressionable.Create<ConsumableStorage>();
            predicate.AndIF(parm.ConsumableId != null, it => it.ConsumableId == parm.ConsumableId);
            return predicate;
        }

        private bool IsExist(ConsumableStorage model)
        {
            int count = Queryable().Where(it => it.ConsumableId == model.ConsumableId && it.StorageId == model.StorageId).Count();
            return count > 0;
        }
    }
}
using EAM.Dashboard.Model;
using EAM.Dashboard.Model.Dto;
using EAM.Dashboard.Service.IService;
using EAM.Model.Basic;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.Equipment;
using EAM.Model.Statistics;
using EAM.Model.System;
using EAM.ServiceCore;
using EAM.ServiceCore.Model.Enums;
using SqlSugar;
using SqlSugar.IOC;
using System.Data;

namespace EAM.Dashboard.Service
{
    /// <summary>
    /// 设备Service
    /// </summary>
    [AppService(ServiceType = typeof(IEquipmentBaseService), ServiceLifetime = LifeTime.Transient)]
    public class EquipmentBaseService : BaseService<EquipmentRuningRecord>, IEquipmentBaseService
    {
        public EquipmentBaseService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 设备预设状态
        /// </summary>
        /// <returns></returns>
        public List<KanBanEquipmentState> GetEquipmentStateStat()
        {
            List<KanBanEquipmentState> list = Context.Queryable<KanBanEquipmentState>().ToList();
            if (list != null)
            {
                int allCount = list.Sum(i => i.Count);
                KanBanEquipmentState equipmentState = new KanBanEquipmentState();
                equipmentState.Count = allCount;
                equipmentState.StateName = "设备总数";
                list.Insert(0, equipmentState);
            }
            return list;
        }

        /// <summary>
        /// 设备分布
        /// </summary>
        /// <returns></returns>
        public List<KanbanEquipmentDistribute> GetEquipmentDistributeStat()
        {
            List<KanbanEquipmentDistribute> list = Context.Queryable<KanbanEquipmentDistribute>().ToList();
            if (list == null) return null;
            list = list.OrderByDescending(t => t.Count).ToList();
            //超过4个，将其他统计数量的放入第四个中
            if (list.Count > 4)
            {
                int other = 0;
                other = list.Skip(3).Sum(r => r.Count);
                KanbanEquipmentDistribute md = new KanbanEquipmentDistribute();
                md.PointName = "其他";
                md.Count = other;
                list.Insert(3, md);
            }
            return list.Take(4).ToList();
        }

        /// <summary>
        /// 获取设备最新的运行记录
        /// </summary>
        /// <param name="count">记录个数</param>
        /// <returns></returns>
        public List<EquipmentRuningReportVo> GetLastEquipmentRuningRecord(int minutes = 10, int count = 10)
        {
            //获取设备信息
            //获取设备5分钟内的上报记录
            DateTime startTime = DateTime.Now.AddMinutes(-minutes);
            List<EquipmentRuningReportVo> equipments = Context.Queryable<EquipmentExtend>()
                .Where(ee => ee.IsLink == "Y")
                .LeftJoin<EquipmentBase>((ee, e) => ee.EquipmentId == e.EquipmentId)
                .LeftJoin<Line>((ee, e, l) => ee.LineId == l.LineId)
                .Select((ee, e, l) => new EquipmentRuningReportVo
                {
                    LineId = ee.LineId,
                    LineName = l.LineName,
                    EquipmentId = e.EquipmentId,
                    EquipmentName = e.EquipmentName + "(" + ee.EquipmentNo.ToString() + ")",
                }).ToList();

            List<EquipmentRuningRecord> records = Context.Queryable<EquipmentRuningRecord>().Where(it => it.CreateTime >= startTime)
                .OrderBy(it => it.CreateTime, OrderByType.Desc)
                .Select(it => new EquipmentRuningRecord()
                {
                    EquipmentId = it.EquipmentId,
                    ProductCount = it.ProductCount,
                    CreateTime = it.CreateTime,
                    RunState = it.RunState,
                    WarnState = it.WarnState
                }).ToList();

            if (equipments != null)
            {
                //将 设备与上报记录关联
                foreach (EquipmentRuningRecord item in records)
                {
                    EquipmentRuningReportVo m = equipments.FirstOrDefault(t => t.EquipmentId == item.EquipmentId);
                    if (m != null)
                    {
                        if (m.Records == null)
                        {
                            List<EquipmentRuningRecord> rp = new List<EquipmentRuningRecord>();
                            m.Records = rp;
                            m.ProductCount = item.ProductCount;
                            m.RunState = item.RunState;
                            m.WarnState = item.WarnState;
                        }
                        //前6条记录
                        if (m.Records.Count < count)
                            m.Records.Add(item);
                    }
                }
                //清除没有上报记录的设备
                for (int i = equipments.Count - 1; i >= 0; i--)
                {
                    if (equipments[i].Records == null || equipments[i].Records.Count == 0)
                    {
                        equipments.RemoveAt(i);
                    }
                }
            }
            return equipments;
        }

        /// <summary>
        /// 设备的能耗
        /// </summary>
        /// <returns></returns>
        public EquipmentEnergyStatVo StatEquipmentEnergy()
        {
            EquipmentEnergyStatVo meStat = new EquipmentEnergyStatVo();

            List<EquipmentExtend> equipmentExt = Context.Queryable<EquipmentExtend>()
                .Select(it => new EquipmentExtend()
                {
                    EquipmentId = it.EquipmentId,
                    Power = it.Power,
                }).ToList();

            meStat.day90 = GetEnergyByDays(equipmentExt, 90);
            meStat.day30 = GetEnergyByDays(equipmentExt, 30);
            meStat.day1 = GetEnergyByDays(equipmentExt, 1);
            return meStat;
        }

        /// <summary>
        /// 统计当前设备各状态数量
        /// </summary>
        public List<EquipmentRuningStateCountVo> StatEquipmentStateCount(int minutes = 10)
        {
            //返回结果
            List<EquipmentRuningStateCountVo> esv = new List<EquipmentRuningStateCountVo>();
            // 查询每个设备最后上报的数据(10分钟以内)
            var recordQuery = Context.Queryable<EquipmentRuningRecord>()
                 .Where(it => it.CreateTime > DateTime.Now.AddMinutes(-minutes))
                 .OrderBy(it => it.CreateTime, OrderByType.Desc)
                 .Take(1)
                 .PartitionBy(it => it.EquipmentId); //根据设备编码取第一条数据

            List<EquipmentRuningRecord> records = Context.Queryable<EquipmentExtend>()
                .Where(ee => ee.IsLink == SysYesNoConstant.是)
                .LeftJoin(recordQuery, (ee, r) => ee.EquipmentId == r.EquipmentId)
                .Select((ee, r) => new EquipmentRuningRecord()
                {
                }, true)
                .ToList();

            //存在数据
            if (records != null && records.Count > 0)
            {
                //全部
                EquipmentRuningStateCountVo allState = new()
                {
                    StateName = "全部",
                    Color = "blue",
                    Count = records.Count,
                    Rate = 100
                };
                //运行
                int runCount = records.Where(it => it.RunState == (int)EquipmentRunStateEnum.Runing).Count();
                EquipmentRuningStateCountVo runState = new()
                {
                    StateName = "运行",
                    Color = "green",
                    Count = runCount,
                    Rate = (int)(Math.Round((decimal)runCount / records.Count, 2) * 100)
                };
                //待机
                int stopCount = records.Where(it => it.RunState == (int)EquipmentRunStateEnum.Stop && it.WarnState == (int)EquipmentWarnStateEnum.Normal).Count();
                EquipmentRuningStateCountVo stopState = new()
                {
                    StateName = "待机",
                    Color = "red",
                    Count = stopCount,
                    Rate = (int)(Math.Round((decimal)stopCount / records.Count, 2) * 100)
                };
                // 报警
                int warnCount = records.Where(it => it.RunState == (int)EquipmentRunStateEnum.Stop && it.WarnState == (int)EquipmentWarnStateEnum.Warning).Count();
                EquipmentRuningStateCountVo warnState = new()
                {
                    StateName = "报警",
                    Color = "yellow",
                    Count = warnCount,
                    Rate = (int)(Math.Round((decimal)warnCount / records.Count, 2) * 100)
                };
                // 停机（没有上报数据）
                EquipmentRuningStateCountVo otherState = new()
                {
                    StateName = "停机",
                    Color = "gray",
                    Count = records.Count - runCount - stopCount - warnCount,
                    Rate = 100 - runState.Rate - stopState.Rate - warnState.Rate
                };
                //保存到返回对象
                esv.Add(allState);
                esv.Add(runState);
                esv.Add(stopState);
                esv.Add(warnState);
                esv.Add(otherState);
            }
            return esv;
        }

        /// <summary>
        /// 当前报警的设备
        /// </summary>
        public List<EquipmentRuningRecordVo> GetWarnEquipmentRuningRecord(int minutes = 10)
        {
            // 查询每个设备最后上报的数据(10分钟以内)
            var recordQuery = Context.Queryable<EquipmentRuningRecord>()
                 .Where(it => it.CreateTime > DateTime.Now.AddMinutes(-minutes))
                 .OrderBy(it => it.CreateTime, OrderByType.Desc)
                 .Take(1)
                 .PartitionBy(it => it.EquipmentId); //根据设备编码取第一条数据

            List<EquipmentRuningRecordVo> records = Context.Queryable<EquipmentExtend>()
                .Where(ee => ee.IsLink == "Y")
                .LeftJoin<EquipmentBase>((ee, e) => ee.EquipmentId == e.EquipmentId)
                .LeftJoin(recordQuery, (ee, e, r) => ee.EquipmentId == r.EquipmentId)
                .LeftJoin<Line>((ee, e, r, l) => ee.LineId == l.LineId)
                .Select((ee, e, r, l) => new EquipmentRuningRecordVo()
                {
                    EquipmentName = e.EquipmentName + "(" + ee.EquipmentNo.ToString() + ")",
                    LineId = ee.LineId,
                    LineName = l.LineName
                }, true)
                .ToList();

            return records.Where(it => it.RunState == (int)EquipmentRunStateEnum.Stop && it.WarnState == (int)EquipmentWarnStateEnum.Warning).ToList();
        }

        /// <summary>
        /// 统计设备的OEE
        /// </summary>
        /// <returns></returns>
        public EquipmentOEEStatVo StatEquipmentOEE()
        {
            //初始前端视图模型
            EquipmentOEEStatVo msVo = new EquipmentOEEStatVo();
            List<ChartBaseVo<string, decimal>> equipmentOEE = new List<ChartBaseVo<string, decimal>>();
            msVo.EquipmentOEE = equipmentOEE;

            List<StatEquipmentRuningRecordDto> serr = Context.Queryable<StatEquipmentRuningRecord>()
                .LeftJoin<EquipmentBase>((it, eb) => it.EquipmentId == eb.EquipmentId)
                .LeftJoin<EquipmentExtend>((it, eb, ee) => it.EquipmentId == ee.EquipmentId)
                .Where((it, eb, ee) => it.StatStartTime <= DateTime.Now && it.StatEndTime > DateTime.Now && ee.IsLink == SysYesNoConstant.是)
                .Select((it, eb, ee) => new StatEquipmentRuningRecordDto()
                {
                    EquipmentName = eb.EquipmentName + "(" + ee.EquipmentNo.ToString() + ")"
                }, true).ToList();
            ParseEquipmentsStat(serr, msVo);
            return msVo;
        }

        /// <summary>
        /// 获取产能占比
        /// </summary>
        public ChartXYData<string, string, List<int>> StatEquipmentProductRate()
        {
            List<StatEquipmentRuningRecordVo> serr = Context.Queryable<StatEquipmentRuningRecord>()
              .LeftJoin<EquipmentBase>((it, eb) => it.EquipmentId == eb.EquipmentId)
              .LeftJoin<EquipmentExtend>((it, eb, ee) => it.EquipmentId == ee.EquipmentId)
              .LeftJoin<Line>((it, eb, ee, l) => ee.LineId == l.LineId)
              .Where((it, eb, ee, l) => it.StatStartTime <= DateTime.Now && it.StatEndTime > DateTime.Now && ee.IsLink == SysYesNoConstant.是)
              .Select((it, eb, ee, l) => new StatEquipmentRuningRecordVo()
              {
                  LineName = l.LineName,
                  EquipmentName = eb.EquipmentName,
                  ProductCount = it.ProductCount
              }).ToList();

            List<string> lineNames = serr.Where(s => s.LineName != null).Select(s => s.LineName).Distinct().Order().ToList();
            List<string> equipmentNames = serr.Where(s => s.EquipmentName != null).Select(s => s.EquipmentName).Distinct().Order().ToList();

            //转为二维数据给前端
            List<List<int>> data = new List<List<int>>();
            foreach (string lineName in lineNames)
            {
                List<int> lineData = new List<int>();
                foreach (string equipmentName in equipmentNames)
                {
                    int? productCount = serr.Where(it => it.LineName == lineName && it.EquipmentName == equipmentName).Select(it => it.ProductCount).Sum();
                    lineData.Add(productCount ?? 0);
                }
                data.Add(lineData);
            }

            //封装到对象中
            ChartXYData<string, string, List<int>> result = new ChartXYData<string, string, List<int>>()
            {
                XData = equipmentNames,
                YData = lineNames,
                Data = data
            };
            return result;
        }

        /// <summary>
        /// 获取产线每日平均性能开动率
        /// </summary>
        /// <param name="days">天数</param>
        /// <returns></returns>
        public ChartXYData<string, string, List<decimal>> StatLinePerformanceRate(int days = 7)
        {
            DateTime startTime = DateTime.Now.AddDays(-(days + 1));
            List<StatEquipmentRuningRecordVo> serr = Context.Queryable<StatEquipmentRuningRecord>()
             .LeftJoin<EquipmentExtend>((it, ee) => it.EquipmentId == ee.EquipmentId)
             .LeftJoin<Line>((it, ee, l) => ee.LineId == l.LineId)
             .Where((it, ee, l) => it.StatStartTime >= startTime && ee.IsLink == SysYesNoConstant.是)
             .Select((it, ee, l) => new StatEquipmentRuningRecordVo()
             {
                 LineName = l.LineName,
                 PerformanceRate = it.PerformanceRate,
                 StatDate = it.StatDate
             }).ToList();

            //根据产线与统计日期分组求平均性能开动率
            var list = serr.Where(it => it.LineName != null).GroupBy(it => new { it.LineName, it.StatDate })
                 .Select(g => new StatEquipmentRuningRecordVo
                 {
                     LineName = g.Key.LineName,
                     StatDate = g.Key.StatDate,
                     PerformanceRate = g.Average(it => it.PerformanceRate)
                 });

            List<string> lineNames = list.Where(it => it.LineName != null).Select(it => it.LineName).Distinct().Order().ToList();
            List<string> statDates = list.Where(it => it.StatDate != null).OrderByDescending(it => it.StatDate).Select(it => it.StatDate.Value.ToString("yyyy-MM-dd")).Distinct().Take(days).ToList();
            statDates.Reverse();
            //转为二维护数据
            List<List<decimal>> data = new List<List<decimal>>();
            foreach (string lineName in lineNames)
            {
                List<decimal> lineData = new List<decimal>();
                foreach (string statDate in statDates)
                {
                    decimal? performanceRate = serr.Where(it => it.LineName == lineName && it.StatDate.Value.ToString("yyyy-MM-dd") == statDate).Select(it => it.PerformanceRate).FirstOrDefault();
                    lineData.Add(performanceRate ?? 0);
                }
                data.Add(lineData);
            }

            ChartXYData<string, string, List<decimal>> result = new()
            {
                XData = statDates,
                YData = lineNames,
                Data = data
            };

            return result;
        }

        /// <summary>
        /// 获取设备故障时长排序
        /// </summary>
        /// <param name="count">获取个数</param>
        /// <returns></returns>
        public ChartXYData<string, string, List<decimal>> StatEquipmentFaultTime(int count = 10)
        {
            List<StatEquipmentRuningRecordVo> serr = Context.Queryable<StatEquipmentRuningRecord>()
            .LeftJoin<EquipmentBase>((it, eb) => it.EquipmentId == eb.EquipmentId)
            .LeftJoin<EquipmentExtend>((it, eb, ee) => it.EquipmentId == ee.EquipmentId)
            .LeftJoin<Line>((it, eb, ee, l) => ee.LineId == l.LineId)
            .Where((it, eb, ee, l) => it.StatStartTime <= DateTime.Now && it.StatEndTime > DateTime.Now && ee.IsLink == SysYesNoConstant.是)
            .OrderBy((it, eb, ee, l) => new { it.FaultSeconds }, OrderByType.Desc)
            .Select((it, eb, ee, l) => new StatEquipmentRuningRecordVo()
            {
                LineName = l.LineName,
                EquipmentName = eb.EquipmentName + "(" + ee.EquipmentNo.ToString() + ")",
                FaultSeconds = it.FaultSeconds,
                AvailabilityRate = it.AvailabilityRate,
            }).Take(count).ToList();

            List<string> xData = serr.Select(x => x.LineName + Environment.NewLine + x.EquipmentName).ToList();
            List<string> yData = new List<string>() { "时长(分)", "时间开动率" };
            List<decimal> series1 = serr.Select(x => Math.Round((decimal)x.FaultSeconds / 60, 1)).ToList();
            List<decimal> series2 = serr.Select(x => x.AvailabilityRate).ToList();

            ChartXYData<string, string, List<decimal>> r = new()
            {
                XData = xData,
                YData = yData,
                Data = new List<List<decimal>>() { series1, series2 }
            };
            return r;
        }

        /// <summary>
        /// 获取产线优秀生技组长信息
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<StatTopEmp> StatTopEmp(int count)
        {
            List<StatTopEmp> steList = Context.Queryable<StatEquipmentRuningRecord>()
            .LeftJoin<EquipmentExtend>((it, ee) => it.EquipmentId == ee.EquipmentId)
            .LeftJoin<Line>((it, ee, l) => ee.LineId == l.LineId)
            .Where((it, ee, l) => it.StatStartTime <= DateTime.Now && it.StatEndTime > DateTime.Now && ee.IsLink == SysYesNoConstant.是 && l.LineId != null)
            .GroupBy((it, ee, l) => new { l.LineId, l.LineName })
            .Select((it, ee, l) => new StatTopEmp()
            {
                LineId = l.LineId,
                LineName = l.LineName,
                FaultSeconds = SqlFunc.AggregateSum(it.FaultSeconds),
            }).ToList();

            //排序取最前的几条数据
            var r = steList.OrderBy(it => it.FaultSeconds).Take(count).ToList();

            //配置的产线生技组长信息
            List<int?> lineIds = r.Where(it => it.LineId > 0).Select(it => it.LineId).Distinct().ToList();
            var lineEmps = Context.Queryable<LineEmp>().Where(it => lineIds.Contains(it.LineId) && it.Position == LineEmpPositionConstant.生技组长).ToList();
            List<string> empCodes = lineEmps.Where(it => it.EmpCode != null).Select(it => it.EmpCode).Distinct().ToList();

            //从主库获取到用户信息
            var adminDB = DbScoped.SugarScope.GetConnectionScope("0");//根据类传入的ConfigId自动选择
            List<SysUser> users = adminDB.Queryable<SysUser>().Where(it => empCodes.Contains(it.UserName)).Select(it => new SysUser() { UserName = it.UserName, NickName = it.NickName, Avatar = it.Avatar }).ToList();

            foreach (StatTopEmp ste in r)
            {
                LineEmp lineEmp = lineEmps.Where(it => it.LineId == ste.LineId).FirstOrDefault();
                if (lineEmp != null)
                {
                    SysUser user = users.Where(it => it.UserName == lineEmp.EmpCode).FirstOrDefault();
                    if (user != null)
                    {
                        ste.EmpCode = user.UserName;
                        ste.EmpName = user.NickName;
                        ste.Avatar = user.Avatar;
                    }
                }
            }

            return r;
        }

        #region 私有方法

        private EnergyItem GetEnergyByDays(List<EquipmentExtend> equipmentExt, int days)
        {
            var serr = Context.Queryable<StatEquipmentRuningRecord>()
              .Where(it => it.StatDate >= DateTime.Now.AddDays(-days))
              .GroupBy(it => it.EquipmentId)
              .Select(it => new StatEquipmentRuningRecord()
              {
                  EquipmentId = it.EquipmentId,
                  RunSeconds = SqlFunc.AggregateSum(it.RunSeconds),
              }).ToList();

            int seconds = 0;//运行时间(秒)
            decimal energy = 0;//能耗

            foreach (var item in serr)
            {
                decimal power = equipmentExt.Where(it => it.EquipmentId == item.EquipmentId).Select(it => it.Power).FirstOrDefault();
                if (power > 0)
                {
                    seconds += (int)item.RunSeconds;
                    energy += power * ((decimal)item.RunSeconds / 60 / 60);
                }
            }

            EnergyItem eI = new()
            {
                Hour = seconds / 60 / 60,
                Energy = (int)energy
            };
            return eI;
        }

        private void ParseEquipmentsStat(List<StatEquipmentRuningRecordDto> statEquipment, EquipmentOEEStatVo msVo)
        {
            if (statEquipment != null)
            {
                decimal sumOEE = 0;
                decimal sumAvailabilityRate = 0;
                decimal sumPerformanceRate = 0;
                decimal sumQualityRate = 0;
                int countOEE = 0;

                //遍历所有设备
                foreach (StatEquipmentRuningRecordDto row in statEquipment)
                {
                    if (row.OEE > 50)
                    {
                        //获取平均OEE
                        sumOEE += row.OEE;
                        sumAvailabilityRate += row.AvailabilityRate;
                        sumPerformanceRate += row.PerformanceRate;
                        sumQualityRate += row.QualityRate;
                        countOEE += 1;

                        //设备OEE图表(取前20条记录)
                        if (msVo.EquipmentOEE.Count < 20 && row.OEE > 0)
                        {
                            msVo.EquipmentOEE.Add(new ChartBaseVo<string, decimal>()
                            {
                                Name = row.EquipmentName,
                                Value = row.OEE,
                            });
                        }
                    }
                }

                //平均OEE
                if (countOEE > 0)
                {
                    msVo.OEE = Math.Round(sumOEE / countOEE, 2);
                    msVo.Count = countOEE;
                    List<ChartBaseVo<string, decimal>> datas = new List<ChartBaseVo<string, decimal>>();

                    ChartBaseVo<string, decimal> timeUR = new ChartBaseVo<string, decimal>();
                    timeUR.Name = "时间稼动率";
                    timeUR.Value = Math.Round(sumAvailabilityRate / countOEE, 2);
                    ChartBaseVo<string, decimal> efficacyUR = new ChartBaseVo<string, decimal>();
                    efficacyUR.Name = "性能稼动率";
                    efficacyUR.Value = Math.Round(sumPerformanceRate / countOEE, 2);
                    ChartBaseVo<string, decimal> passR = new ChartBaseVo<string, decimal>();
                    passR.Name = "良品率";
                    passR.Value = Math.Round(sumQualityRate / countOEE, 2);

                    datas.Add(timeUR);
                    datas.Add(efficacyUR);
                    datas.Add(passR);
                    msVo.Rate = datas;
                }
            }
        }

        #endregion 私有方法
    }
}
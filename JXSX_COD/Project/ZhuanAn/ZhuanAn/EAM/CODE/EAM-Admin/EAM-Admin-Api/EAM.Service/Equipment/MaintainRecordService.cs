using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using EAM.ServiceCore.Model.Enums;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 设备保养记录Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IMaintainRecordService), ServiceLifetime = LifeTime.Transient)]
    public class MaintainRecordService : BaseService<MaintainRecord>, IMaintainRecordService
    {
        private readonly IEquipmentBaseService EquipmentService;
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly ISysMenuService SysMenuService;
        private readonly IMaintainReportService MaintainReportService;

        public MaintainRecordService(IHttpContextAccessor contextAccessor,
            IEquipmentBaseService equipmentService,
            ISysMenuService sysMenuService,
            IMaintainReportService maintainReportService) : base(contextAccessor)
        {
            EquipmentService = equipmentService;
            HttpContextAccessor = contextAccessor;
            SysMenuService = sysMenuService;
            MaintainReportService = maintainReportService;
        }

        /// <summary>
        /// 查询设备保养记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<MaintainRecordDto> GetList(MaintainRecordQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .Where(predicate.ToExpression())
                .OrderByIF(parm.Sort == "CreateTime", it => it.CreateTime, OrderByType.Desc)
                .Select((it, e) => new MaintainRecordDto
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public MaintainRecord GetInfo(int Id)
        {
            var response = Queryable()
                .Where(it => it.Id == Id)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .Select((it, e) => new MaintainRecord()
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName
                }, true)
                .First();

            List<MaintainRecordDetail> mrd = new();
            if (response != null)
            {
                mrd = Context.Queryable<MaintainItem>().Where(it => it.EquipmentId == response.EquipmentId && it.DateMark == response.DateMark)
                   .LeftJoin<MaintainRecordDetail>((it, mrd) => mrd.RecordId == response.Id && mrd.ItemId == it.ItemId)
                   .OrderBy((it, mrd) => it.SortNo)
                   .Select((it, mrd) => new MaintainRecordDetail()
                   {
                       RecordId = response.Id,
                       ItemId = it.ItemId,
                       ItemName = it.ItemName,
                       ItemValue = mrd.ItemValue
                   }).ToList();
            }

            response.MaintainRecordDetailNav = mrd;
            return response;
        }

        /// <summary>
        /// 添加设备保养记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MaintainRecord AddMaintainRecord(MaintainRecord model)
        {
            //参数检查
            if (string.IsNullOrEmpty(model.DateMark))
                throw new CustomException($"保养【日期标记】不能为空");

            if (model.DateMarkStamp == null || model.DateMarkStamp <= 0)
            {
                if (model.MaintainDate != null)
                {
                    model.Year = model.MaintainDate.Value.Year;
                    model.DateMarkStamp = GetDateMarkStamp(model.DateMark, model.MaintainDate.Value);
                }
                else
                {
                    throw new CustomException($"未传递有效日期戳");
                }
            }

            //防止除日保养之外的其他类型保养有时间标记
            if (model.DateMark != DateMarkConstant.日)
            {
                model.TimeMark = null;
            }

            // 检查设备的资产编号
            EquipmentBase equipment = EquipmentService.GetInfo(model.EquipmentId.Value);
            if (equipment == null)
                throw new CustomException($"未找到设备【{model.EquipmentId}】的信息");

            //检查是否存在
            if (Queryable().Where(it => it.EquipmentId == model.EquipmentId && it.Year == model.Year && it.DateMark == model.DateMark && it.DateMarkStamp == model.DateMarkStamp && it.TimeMark == model.TimeMark).Count() > 0)
                throw new CustomException($"已存在保养记录，不可重复操作");

            //操作权限检查
            CheckOperatePerms(model);

            //更新统计报表
            int? month = GetMonthFromDateMark(model.DateMark, model.Year.Value, model.DateMarkStamp.Value);
            UpdateReport(model.EquipmentId.Value, model.Year.Value, month, model.DateMark);

            //添加保养记录
            model.IsVisible = SysYesNoConstant.是;
            if (string.IsNullOrEmpty(model.ExecutorId))
            {
                model.ExecutorId = HttpContextAccessor.HttpContext.GetName();
                model.ExecutorName = HttpContextAccessor.HttpContext.GetNickName();
            }

            return Context.InsertNav(model).Include(s1 => s1.MaintainRecordDetailNav).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改设备保养记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMaintainRecord(MaintainRecord model)
        {
            //防止除日保养之外的其他类型保养有时间标记
            if (model.DateMark != DateMarkConstant.日)
            {
                model.TimeMark = null;
            }

            //操作权限检查
            CheckOperatePerms(model);
            return Context.UpdateNav(model,
                    new UpdateNavRootOptions()
                    {
                        UpdateColumns = new string[] { nameof(model.UpdateBy), nameof(model.UpdateTime), nameof(model.ExecutorId), nameof(model.ExecutorName) }
                    }
                )
                .Include(z1 => z1.MaintainRecordDetailNav)
                .ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 删除设备保养记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMaintainRecord(int[] ids)
        {
            return Context.Updateable<MaintainRecord>().SetColumns(it => it.IsVisible == SysYesNoConstant.否).Where(it => ids.Contains(it.Id.Value)).ExecuteCommand();
        }

        /// <summary>
        /// 导出设备保养记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<MaintainRecordDto> ExportList(MaintainRecordQueryDto parm)
        {
            return GetList(parm);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public MaintainRecord GetDetail(MaintainRecordQueryDetailDto param)
        {
            //尝试用记录ID获取记录
            MaintainRecord record = null;
            if (param.Id != null)
            {
                record = Queryable().Where(x => x.Id == param.Id).First();
            }
            //尝试获取设备信息
            EquipmentBase equipment = null;
            if (record != null)
            {
                equipment = Context.Queryable<EquipmentBase>().Where(it => it.EquipmentId == record.EquipmentId).First();
            }
            if (equipment == null && param.EquipmentId > 0)
            {
                equipment = Context.Queryable<EquipmentBase>().Where(it => it.EquipmentId == param.EquipmentId).First();
            }
            if (equipment == null && !string.IsNullOrEmpty(param.AssetNo))
            {
                equipment = Context.Queryable<EquipmentBase>().Where(it => it.AssetNo == param.AssetNo).First();
            }
            if (equipment == null)
                throw new CustomException("未找到设备信息");
            param.EquipmentId = equipment.EquipmentId;

            //尝试用唯一数据组获取
            if (record == null)
            {
                //如果传了保养日期，优先以保养日期计算出日期标记值
                if (param.MaintainDate != null)
                {
                    param.Year = param.MaintainDate.Value.Year;
                    param.DateMarkStamp = GetDateMarkStamp(param.DateMark, param.MaintainDate.Value);
                }

                //防止除日保养之外的其他类型保养有时间标记
                if (param.DateMark != DateMarkConstant.日)
                {
                    param.TimeMark = null;
                }
                record = Queryable().Where(it => it.EquipmentId == param.EquipmentId && it.Year == param.Year && it.DateMark == param.DateMark && it.DateMarkStamp == param.DateMarkStamp && it.TimeMark == param.TimeMark).First();
            }

            // 获取保养记录的详情（保养项目、保养值）
            List<MaintainRecordDetail> mrd;
            if (record == null)
            {//未保养过，查找保养项目
                record = new MaintainRecord()
                {
                    EquipmentId = param.EquipmentId,
                    AssetNo = param.AssetNo,
                    Year = param.Year,
                    DateMark = param.DateMark,
                    DateMarkStamp = param.DateMarkStamp,
                    TimeMark = param.TimeMark,
                    MaintainDate = param.MaintainDate
                };

                mrd = Context.Queryable<MaintainItem>()
                    .Where(it => it.EquipmentId == param.EquipmentId && it.DateMark == param.DateMark)
                    .OrderBy(it => it.SortNo)
                    .Select(it => new MaintainRecordDetail()
                    {
                        ItemId = it.ItemId,
                        ItemName = it.ItemName,
                    }).ToList();
            }
            else
            {//有保养记录，查找保养项目及其保养结果值
                mrd = Context.Queryable<MaintainItem>().Where(it => it.EquipmentId == record.EquipmentId && it.DateMark == record.DateMark)
                    .LeftJoin<MaintainRecordDetail>((it, mrd) => mrd.RecordId == record.Id && mrd.ItemId == it.ItemId)
                    .OrderBy((it, mrd) => it.SortNo)
                    .Select((it, mrd) => new MaintainRecordDetail()
                    {
                        ItemId = it.ItemId,
                        ItemName = it.ItemName,
                        ItemValue = mrd.ItemValue
                    }).ToList();
            }
            record.MaintainRecordDetailNav = mrd;
            record.AssetNo = equipment.AssetNo;
            record.AssetName = equipment.AssetName;

            return record;
        }

        /// <summary>
        /// 全局保养（作弊）
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int GlobalMaintainRecord(GlobalMaintainRecordDto parm)
        {
            //检查参数
            if (string.IsNullOrEmpty(parm.DateMark))
                throw new CustomException("日期标志不能为空");
            if (parm.MaintainDate == null)
                throw new CustomException("保养日期不能为空");
            if ((parm.ExecutorList != null && parm.ExecutorList.Count > 0) && parm.DeptId != null)
                throw new CustomException("部门与保养人只需填写其中一个");
            if ((parm.EquipmentList != null && parm.EquipmentList.Count > 0) && !string.IsNullOrEmpty(parm.CostCenter))
                throw new CustomException("成本中心与设备只需填写其中一个");
            if (parm.MaintainDate.Value >= DateTime.Now.Date.AddDays(1))
                throw new CustomException("保养日期超出当前日期");

            int dateMarkStamp = GetDateMarkStamp(parm.DateMark, parm.MaintainDate.Value);
            //设备列表
            if (!string.IsNullOrEmpty(parm.CostCenter))
            {
                parm.EquipmentList = Context.Queryable<EquipmentBase>()
                    .LeftJoin<MaintainReport>((it, r) => it.EquipmentId == r.EquipmentId)
                    .Where((it, r) => it.CostCenter == parm.CostCenter && it.Status != EquipmentStatusConstant.报废 && r.Year == parm.MaintainDate.Value.Year)
                    .Select(it => (int)it.EquipmentId)
                    .Distinct()
                    .ToList();
                if (parm.EquipmentList == null || parm.EquipmentList.Count <= 0)
                    throw new CustomException("当前成本中心下没有有效的设备或设备对应保养年份没有创建报表");
            }
            else if (parm.EquipmentList == null || parm.EquipmentList.Count <= 0)
            {//全部资产报表
                parm.EquipmentList = Context.Queryable<MaintainReport>().Where(it => it.Year == parm.MaintainDate.Value.Year).Select(it => (int)it.EquipmentId).ToList();
            }

            //保养人
            List<EmployeeDto> empList = null;
            if (parm.DeptId != null && parm.DeptId > 0)
            {//指定部门下的人员为保养人
                empList = Context.Queryable<Employee>()
                    .Where(it => it.DeptId == parm.DeptId && it.Status == 0)
                    .Select(it => new EmployeeDto()
                    {
                        EmpCode = it.EmpCode,
                        EmpName = it.EmpName
                    }).ToList();
                if (empList == null || empList.Count <= 0)
                    throw new CustomException("当前部门下没有任何员工");
            }
            else if (parm.ExecutorList != null && parm.ExecutorList.Count > 0)
            {//指定具体保养人
                empList = Context.Queryable<Employee>()
                  .Where(it => parm.ExecutorList.Contains(it.EmpCode) && it.Status == 0)
                  .Select(it => new EmployeeDto()
                  {
                      EmpCode = it.EmpCode,
                      EmpName = it.EmpName
                  }).ToList();
                if (empList == null || empList.Count <= 0 || empList.Count != parm.ExecutorList.Count)
                    throw new CustomException("当前保养人员存在无效人员工号");
            }

            Random random = new Random();

            int count = 0;
            DbResult<bool> r = UseTran(() =>
            {
                // 循环设备资产编号
                foreach (var equipmentId in parm.EquipmentList)
                {
                    //查询是否存在记录
                    MaintainRecord record = GetDetail(new MaintainRecordQueryDetailDto()
                    {
                        EquipmentId = equipmentId,
                        Year = parm.MaintainDate.Value.Year,
                        DateMark = parm.DateMark,
                        DateMarkStamp = dateMarkStamp
                    });

                    //如果没有保养项目，跳过
                    if (record.MaintainRecordDetailNav.Count <= 0)
                        continue;
                    record.UpdateBy = parm.UpdateBy;
                    record.UpdateTime = parm.UpdateTime;

                    if (record.Id == null || record.Id <= 0)
                    {//新增
                        record.IsVisible = SysYesNoConstant.是;
                        record.CreateTime = parm.MaintainDate?.Date.AddSeconds(random.Next(28800, 36000));
                        record.ExecutorId = empList[count % empList.Count].EmpCode;
                        record.ExecutorName = empList[count % empList.Count].EmpName;
                        foreach (var item in record.MaintainRecordDetailNav)
                        {
                            item.ItemValue = "V";
                        }
                        count += Context.InsertNav(record).Include(s1 => s1.MaintainRecordDetailNav).ExecuteCommand() ? 1 : 0;
                    }
                    else
                    {//修改
                        foreach (var item in record.MaintainRecordDetailNav)
                        {
                            //判断是否强制覆盖
                            if (parm.Cover && (string.IsNullOrEmpty(item.ItemValue) || item.ItemValue == "V" || item.ItemValue == "X"))
                            {//所有的项目结果改为 'V'
                                item.ItemValue = "V";
                            }
                            else if (string.IsNullOrEmpty(item.ItemValue))
                            {//值为null的项目补全为'V'
                                item.ItemValue = "V";
                            }
                        }
                        count += Context.UpdateNav(record).Include(z1 => z1.MaintainRecordDetailNav).ExecuteCommand() ? 1 : 0;
                    }
                    UpdateReport(equipmentId, parm.MaintainDate.Value.Year, parm.MaintainDate.Value.Month, parm.DateMark);
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
        private static Expressionable<MaintainRecord> QueryExp(MaintainRecordQueryDto parm)
        {
            var predicate = Expressionable.Create<MaintainRecord>();

            predicate = predicate.AndIF(parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId);
            predicate = predicate.AndIF(parm.Year > 0, it => it.Year == parm.Year);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.DateMark), it => it.DateMark == parm.DateMark);
            predicate = predicate.AndIF(parm.DateMarkStamp > 0, it => it.DateMarkStamp == parm.DateMarkStamp);
            predicate = predicate.And(it => it.IsVisible == SysYesNoConstant.是);
            return predicate;
        }

        private void UpdateReport(int equipmentId, int year, int? month, string dataMark)
        {
            MaintainReport report = Context.Queryable<MaintainReport>().Where(it => it.EquipmentId == equipmentId && it.Year == year).First();
            if (report == null)
                report = MaintainReportService.AddMaintainReport(new MaintainReport { EquipmentId = equipmentId, Year = year });
            //Jan、Feb、Mar、Apr 、May、Jun、Jul、Aug、Sept、Oct、Nov、Dec
            switch (month)
            {
                case 1:
                    if (string.IsNullOrEmpty(report.Jan) || report.Jan == "0")
                        report.Jan = "1";
                    break;

                case 2:
                    if (string.IsNullOrEmpty(report.Feb) || report.Feb == "0")
                        report.Feb = "1";
                    break;

                case 3:
                    if (string.IsNullOrEmpty(report.Mar) || report.Mar == "0")
                        report.Mar = "1";
                    break;

                case 4:
                    if (string.IsNullOrEmpty(report.Apr) || report.Apr == "0")
                        report.Apr = "1";
                    break;

                case 5:
                    if (string.IsNullOrEmpty(report.May) || report.May == "0")
                        report.May = "1";
                    break;

                case 6:
                    if (string.IsNullOrEmpty(report.Jun) || report.Jun == "0")
                        report.Jun = "1";
                    break;

                case 7:
                    if (string.IsNullOrEmpty(report.Jul) || report.Jul == "0")
                        report.Jul = "1";
                    break;

                case 8:
                    if (string.IsNullOrEmpty(report.Aug) || report.Aug == "0")
                        report.Aug = "1";
                    break;

                case 9:
                    if (string.IsNullOrEmpty(report.Sept) || report.Sept == "0")
                        report.Sept = "1";
                    break;

                case 10:
                    if (string.IsNullOrEmpty(report.Oct) || report.Oct == "0")
                        report.Oct = "1";
                    break;

                case 11:
                    if (string.IsNullOrEmpty(report.Nov) || report.Nov == "0")
                        report.Nov = "1";
                    break;

                case 12:
                    if (string.IsNullOrEmpty(report.Dec) || report.Dec == "0")
                        report.Dec = "1";
                    break;

                default:
                    if (string.IsNullOrEmpty(report.Yearly) || report.Yearly == "0")
                        report.Yearly = "1";
                    break;
            }

            // 更新年度报表状态
            if (dataMark == DateMarkConstant.月 || dataMark == DateMarkConstant.季 || dataMark == DateMarkConstant.年)
                if (string.IsNullOrEmpty(report.Yearly) || report.Yearly == "0")
                    report.Yearly = "1";

            MaintainReportService.UpdateMaintainReport(report);
        }

        /// <summary>
        /// 检查操作权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private bool CheckOperatePerms(MaintainRecord model)
        {
            //判断操作的时间是否合规则
            DateTime now = DateTime.Now;
            GregorianCalendar gregorianCalendar = new GregorianCalendar();
            int weekStamp = gregorianCalendar.GetWeekOfYear(now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//当天所属周
            bool TimeCheckFlag = true;
            //日期检查
            if (model.Year != now.Year) TimeCheckFlag = false;//年表，判断是否当年
            else if (model.DateMark == DateMarkConstant.日 && (model.TimeMark == null || model.TimeMark == TimeMarkConstant.白班) && model.DateMarkStamp != now.DayOfYear) TimeCheckFlag = false;//日期是否当天
            else if (model.DateMark == DateMarkConstant.日 && model.TimeMark == TimeMarkConstant.夜班)
            {//夜班跨天特殊处理
                if (now.Hour < 7 && model.DateMarkStamp != now.AddDays(-1).DayOfYear)
                {//次日
                    TimeCheckFlag = false;
                }
                else if (now.Hour >= 7 && model.DateMarkStamp != now.DayOfYear)
                {//当天
                    TimeCheckFlag = false;
                }
            }
            else if (model.DateMark == DateMarkConstant.周 && model.DateMarkStamp != weekStamp) TimeCheckFlag = false;//周期是否合规
            else if (model.DateMark == DateMarkConstant.月 && model.DateMarkStamp != now.Month) TimeCheckFlag = false;//月表，判断是否当年当月
            else if (model.DateMark == DateMarkConstant.月 && now.Day < 25) TimeCheckFlag = false;//日期是否大于等于当月25号
            //时间检查
            if (model.DateMark == DateMarkConstant.日 && model.TimeMark == TimeMarkConstant.白班 && (now.Hour < 7 || now.Hour >= 19)) TimeCheckFlag = false;
            else if (model.DateMark == DateMarkConstant.日 && model.TimeMark == TimeMarkConstant.夜班 && !(now.Hour < 7 || now.Hour >= 19)) TimeCheckFlag = false;

            //权限验证
            if (HttpContextAccessor.HttpContext.IsAdmin())
                return true;

            List<string> perms = SysMenuService.SelectMenuPermsByUserId(HttpContextAccessor.HttpContext.GetUId());
            if (!TimeCheckFlag && !perms.Exists(f => f.ToLower() == "maintain:record:exceed"))//【超限】权限
                throw new CustomException("超出编辑期限，不可操作");

            if (model.DateMark == DateMarkConstant.日 && !perms.Exists(f => f.ToLower() == "maintain:record:day"))
                throw new CustomException("无【日保养】操作权限");
            if (model.DateMark == DateMarkConstant.周 && !perms.Exists(f => f.ToLower() == "maintain:record:week"))
                throw new CustomException("无【周保养】操作权限");
            if (model.DateMark == DateMarkConstant.月 && !perms.Exists(f => f.ToLower() == "maintain:record:month"))
                throw new CustomException("无【月保养】操作权限");
            if (model.DateMark == DateMarkConstant.季 && !perms.Exists(f => f.ToLower() == "maintain:record:quarter"))
                throw new CustomException("无【季保养】操作权限");
            if (model.DateMark == DateMarkConstant.年 && !perms.Exists(f => f.ToLower() == "maintain:record:year"))
                throw new CustomException("无【年保养】操作权限");
            return true;
        }

        /// <summary>
        /// 获取日期标记对应时间月份
        /// </summary>
        /// <param name="dateMark"></param>
        /// <param name="year"></param>
        /// <param name="dateMarkStamp"></param>
        /// <returns></returns>
        private int? GetMonthFromDateMark(string dateMark, int year, int dateMarkStamp)
        {
            if (dateMark == DateMarkConstant.日)
            {
                DateTime date = new DateTime(year, 1, 1).AddDays(dateMarkStamp - 1);
                return date.Month;
            }
            if (dateMark == DateMarkConstant.周)
            {
                int weekNumber = dateMarkStamp; // 假设我们要找的是第10周
                //DayOfWeek friday = DayOfWeek.Friday;

                // 创建一个 GregorianCalendar 实例
                GregorianCalendar calendar = new GregorianCalendar();
                // 获取这一年的第一天
                DateTime firstDayOfYear = new DateTime(year, 1, 1);
                // 根据 GregorianCalendar 获取这一年的第一天是一周中的哪一天
                DayOfWeek firstDayOfWeek = calendar.GetDayOfWeek(firstDayOfYear);
                // 计算需要加上多少天才能到达这一年的第一周的星期五
                int daysToFirstFriday = (DayOfWeek.Friday - firstDayOfWeek + 7) % 7;

                // 如果第一周不是完整的周，则可能需要调整
                // 这里我们假设一周从星期一开始，这符合 ISO 8601 的定义
                int daysToAddForNthWeek = (weekNumber - 1) * 7 + daysToFirstFriday;
                // 计算第 n 周的星期五
                DateTime nthWeekFriday = firstDayOfYear.AddDays(daysToAddForNthWeek);
                return nthWeekFriday.Month;
            }
            if (dateMark == DateMarkConstant.月)
            {
                return dateMarkStamp;
            }
            return null;
        }

        /// <summary>
        /// 根据日期标记与日期计算出对应时间戳
        /// </summary>
        /// <param name="dateMark"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private int GetDateMarkStamp(string dateMark, DateTime date)
        {
            int dateMarkStamp = 0;
            switch (dateMark)
            {
                case DateMarkConstant.日:
                    dateMarkStamp = date.DayOfYear;
                    break;

                case DateMarkConstant.周:
                    GregorianCalendar gregorianCalendar = new GregorianCalendar();
                    dateMarkStamp = gregorianCalendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//当天所属周
                    break;

                case DateMarkConstant.月:
                    dateMarkStamp = date.Month;
                    break;

                case DateMarkConstant.季:
                    dateMarkStamp = (int)Math.Ceiling((double)date.Month / 3);
                    break;

                case DateMarkConstant.年:
                    dateMarkStamp = 1;
                    break;

                default:
                    throw new CustomException($"未知日期标记:{dateMark}");
            }
            return dateMarkStamp;
        }
    }
}
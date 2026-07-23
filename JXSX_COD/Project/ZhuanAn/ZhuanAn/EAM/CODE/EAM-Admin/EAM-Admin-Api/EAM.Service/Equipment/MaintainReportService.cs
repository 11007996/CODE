using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using EAM.Service.Extensions;
using EAM.ServiceCore.Model.Enums;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Globalization;
using static EAM.Model.Dto.MaintainReportSheetDto;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 资产保养报表Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IMaintainReportService), ServiceLifetime = LifeTime.Transient)]
    public class MaintainReportService : BaseService<MaintainReport>, IMaintainReportService
    {
        public MaintainReportService(IHttpContextAccessor contextAccessor, IEquipmentBaseService equipmentService) : base(contextAccessor)
        {
            EquipmentService = equipmentService;
        }

        private IEquipmentBaseService EquipmentService { get; set; }

        /// <summary>
        /// 查询资产保养报表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<MaintainReportDto> GetList(MaintainReportQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("AssetNo asc")
                .Where(predicate.ToExpression())
                .InnerJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .Select((it, e) => new MaintainReportDto()
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName
                }, true)
                .MergeTable()
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public MaintainReport GetInfo(int Id)
        {
            //var response = Queryable()
            //    .Where(x => x.Id == Id)
            //    .First();

            //return response;
            return null;
        }

        /// <summary>
        /// 添加资产保养报表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MaintainReport AddMaintainReport(MaintainReport model)
        {
            //参数检查
            if (EquipmentService.GetInfo((int)model.EquipmentId) == null)
                throw new CustomException($"未找到设备【{model.EquipmentId}】的信息");
            //年份检查,不能小于当前年份前5年，不能大于次年
            int currYear = DateTime.Now.Year;
            if (model.Year < currYear - 5 || model.Year > currYear + 1)
                throw new CustomException("年份非法或超出合规期限");
            //检查是否已创建
            int count = Context.Queryable<MaintainReport>().Where(it => it.EquipmentId == model.EquipmentId && it.Year == model.Year).Count();
            if (count > 0)
                throw new CustomException("已创建此报表，请勿重复创建");
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 批量添加设备保养报表(全年所有的资产报表)
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public int BatchAddMaintainReport(int year)
        {
            //年份检查,不能小于当前年份前5年，不能大于次年
            int currYear = DateTime.Now.Year;
            if (year < currYear - 5 || year > currYear + 1)
                throw new CustomException("年份非法或超出合规期限");
            //批量插入 未创建对应年份的资产报表
            return Context.Queryable<EquipmentBase>().LeftJoin<MaintainReport>((e, mr) => e.EquipmentId == mr.EquipmentId && mr.Year == year)
                .Where((e, mr) => mr.EquipmentId == null)
                .Select((e, mr) => new MaintainReport() { EquipmentId = e.EquipmentId, Year = year }).IntoTable<MaintainReport>();
        }

        /// <summary>
        /// 修改资产保养报表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMaintainReport(MaintainReport model)
        {
            return Update(model, true);
        }

        #region 报表Sheet页查询

        public MaintainReportSheetDto GetReportSheet(MaintainReportSheetQueryDto param)
        {
            //检查
            var equipment = EquipmentService.GetInfo(param.EquipmentId.Value);
            if (equipment == null)
                throw new CustomException($"未找到设备【{param.EquipmentId}】的信息");
            //年份检查,不能小于当前年份前5年，不能大于次年
            int currYear = DateTime.Now.Year;
            if (param.Year < currYear - 5 || param.Year > currYear + 1)
                throw new CustomException("年份非法或超出合规期限");
            if (param.Month != null && (param.Month < 0 || param.Month > 12))
                throw new CustomException("月份非法，限制范围:1 ~ 12。");

            string[] dateMarks = param.Month != null ? new string[] { "D", "W", "M" } : new string[] { "M", "Q", "Y" };

            MaintainReportSheetDto reportSheet = new();
            reportSheet.EquipmentId = param.EquipmentId;
            reportSheet.Year = param.Year;
            reportSheet.Month = param.Month;
            reportSheet.Equipment = equipment;
            reportSheet.SheetPart = new List<ReportSheetPart>();
            foreach (string dateMark in dateMarks)
            {
                ReportSheetPart rsp = new();
                DataTable dt = new();
                rsp.DateMark = dateMark;
                //确定日期列
                rsp.PartColumn = GetSheetPartColumn(param.Year, param.Month, dateMark);
                DefineColumn(dt, rsp.PartColumn);
                //获取保养项目
                rsp.PartRow = GetSheetPartRow((int)param.EquipmentId, dateMark);
                FillSheetItem(dt, rsp.PartRow);

                //获取保养记录数据
                int[] stamps = rsp.PartColumn.Select(it => it.DateMarkStamp).ToArray();
                List<MaintainRecord> records = Context.Queryable<MaintainRecord>()
                    .Includes(x => x.MaintainRecordDetailNav) //填充子对象
                    .Where(it => it.EquipmentId == param.EquipmentId && it.Year == param.Year && it.DateMark == dateMark && it.IsVisible == SysYesNoConstant.是 && it.DateMarkStamp >= stamps[0] && it.DateMarkStamp <= stamps[stamps.Length - 1])
                    .WhereIF(dateMark == DateMarkConstant.日, it => it.TimeMark == param.TimeMark)//判断当日期为“D”，日保养时，增加时间标记判断
                    .ToList();

                //转换为Sheet结构(行标题为项目名，列标题为时间戳
                foreach (MaintainRecord record in records)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        //填充ID 行数据
                        if (row["itemName"].ToString() == "ID")
                            row[record.DateMarkStamp.ToString()] = record.Id;
                        //填充保养人签名
                        else if (row["itemName"].ToString() == MaintainItem.Executor_Name)
                            row[record.DateMarkStamp.ToString()] = record.ExecutorName;
                        //填充项目值
                        else if (record.MaintainRecordDetailNav != null)
                            row[record.DateMarkStamp.ToString()] = record.MaintainRecordDetailNav.Where(it => it.ItemId == Convert.ToInt32(row["itemId"])).Select(it => it.ItemValue).FirstOrDefault();
                    }
                }

                rsp.SheetTable = dt;
                reportSheet.SheetPart.Add(rsp);
            }
            return reportSheet;
        }

        /// <summary>
        /// 获取日期标记相关列信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="dateMark"></param>
        /// <returns></returns>
        private static List<SheetPartColumn> GetSheetPartColumn(int year, int? month, string dateMark)
        {
            List<SheetPartColumn> spcList = new();
            if (month != null)
            {
                int baseWidth = 40;
                int days = DateTime.DaysInMonth(year, (int)month);//指定月份天数
                DateTime dayOne = Convert.ToDateTime(year + "-" + month + "-1");//指定月份第一天
                DateTime dayLast = Convert.ToDateTime(year + "-" + month + "-" + days);//指定月份最后一天
                //日表
                if (dateMark == DateMarkConstant.日)
                {
                    int dayStamp = dayOne.DayOfYear;

                    for (int i = 1; i < days + 1; i++)
                    {
                        spcList.Add(new SheetPartColumn()
                        {
                            ColumnName = i.ToString().PadLeft(2, '0'),
                            DateMark = dateMark,
                            DateMarkStamp = dayStamp + i - 1,
                            WidthPercent = 1 * baseWidth,
                            SpanColumn = 1,
                        });
                    }
                }

                //周表
                if (dateMark == DateMarkConstant.周)
                {
                    DayOfWeek firstWeek = dayOne.DayOfWeek;//月的第一天星期几
                    int weekMark;
                    if (firstWeek == DayOfWeek.Sunday)
                    {
                        weekMark = 7;
                    }
                    else
                    {
                        weekMark = (int)firstWeek;
                    }
                    int firstWeekDays = 7;//第一周的天数
                    if (weekMark > 1)
                    {
                        firstWeekDays = 7 - weekMark + 1;
                    }
                    GregorianCalendar gregorianCalendar = new();
                    int weekStamp = gregorianCalendar.GetWeekOfYear(dayOne, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//第一天所属周
                    int lastWeekStamp = gregorianCalendar.GetWeekOfYear(dayLast, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//最后一天所属性周
                    int totalDays = 0;
                    for (int i = 1; i <= lastWeekStamp - weekStamp + 1; i++)
                    {
                        int weekDays = i == 1 ? firstWeekDays : i == lastWeekStamp - weekStamp + 1 ? days - totalDays : 7;//对应周在当月占几天
                        spcList.Add(new SheetPartColumn()
                        {
                            ColumnName = (weekStamp + i - 1).ToString(),
                            DateMark = dateMark,
                            DateMarkStamp = weekStamp + i - 1,
                            WidthPercent = weekDays * baseWidth,
                            SpanColumn = weekDays,
                        });
                        totalDays += weekDays;
                    }
                }

                //月表
                if (dateMark == DateMarkConstant.月)
                {
                    spcList.Add(new SheetPartColumn()
                    {
                        ColumnName = month.ToString(),
                        DateMark = dateMark,
                        DateMarkStamp = (int)month,
                        WidthPercent = days * baseWidth,
                        SpanColumn = days,
                    });
                }
            }
            else
            {
                int baseWidth = 100;
                //月表
                if (dateMark == DateMarkConstant.月)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        spcList.Add(new SheetPartColumn()
                        {
                            ColumnName = i.ToString(),
                            DateMark = dateMark,
                            DateMarkStamp = i,
                            WidthPercent = 1 * baseWidth,
                            SpanColumn = 1
                        });
                    }
                }

                //季表
                if (dateMark == DateMarkConstant.季)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        spcList.Add(new SheetPartColumn()
                        {
                            ColumnName = i.ToString(),
                            DateMark = dateMark,
                            DateMarkStamp = i,
                            WidthPercent = 1 * 3 * baseWidth,
                            SpanColumn = 3
                        });
                    }
                }

                //年表
                if (dateMark == DateMarkConstant.年)
                {
                    spcList.Add(new SheetPartColumn()
                    {
                        ColumnName = year.ToString(),
                        DateMark = dateMark,
                        DateMarkStamp = 1,
                        WidthPercent = 12 * baseWidth,
                        SpanColumn = 12
                    });
                }
            }
            return spcList;
        }

        /// <summary>
        /// 定义列名称
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columns"></param>
        private static void DefineColumn(DataTable dt, List<SheetPartColumn> columns)
        {
            dt.Columns.Add("itemId", typeof(int));
            dt.Columns.Add("itemName", typeof(string));
            foreach (SheetPartColumn column in columns)
            {
                dt.Columns.Add(column.DateMarkStamp.ToString(), typeof(string)).Caption = column.ColumnName;
            }
        }

        /// <summary>
        /// 填充报表保养项目列数据
        /// </summary>
        private static void FillSheetItem(DataTable dt, List<SheetPartItem> items)
        {
            DataRow row = dt.NewRow();
            row["itemName"] = "ID";
            dt.Rows.Add(row);
            foreach (var item in items)
            {
                row = dt.NewRow();
                row["itemId"] = item.ItemId;
                row["itemName"] = item.ItemName;
                dt.Rows.Add(row);
            }
            if (dt.Rows.Count > 0)
            {
                row = dt.NewRow();
                row["itemName"] = "保养人签名";
                dt.Rows.Add(row);
            }
        }

        /// <summary>
        /// 获取报表行内容（保养项目）
        /// </summary>
        private List<SheetPartItem> GetSheetPartRow(int equipmentId, string dateMark)
        {
            List<SheetPartItem> items = Context.Queryable<MaintainItem>()
                .Where(it => it.EquipmentId == equipmentId && it.DateMark == dateMark)
                .OrderBy(it => it.SortNo)
                .Select(it => new SheetPartItem()
                {
                    ItemId = it.ItemId,
                    ItemName = it.ItemName
                })
                .ToList();
            if (items == null)
                return new List<SheetPartItem>();
            else
                return items;
        }

        #endregion 报表Sheet页查询

        #region 报表Sheet导出

        public string ExportReportExcel(MaintainReportSheetQueryDto param)
        {
            MaintainReportSheetDto data = GetReportSheet(param);
            if (param.Month > 0)
                data.Tip = Context.Queryable<FactoryConfig>().Where(it => it.ConfigKey == FactoryConfigKeyConstant.月保养报表提示 && it.EnableFlag == SysYesNoConstant.是).First()?.ConfigValue;
            else
                data.Tip = Context.Queryable<FactoryConfig>().Where(it => it.ConfigKey == FactoryConfigKeyConstant.年保养报表提示 && it.EnableFlag == SysYesNoConstant.是).First()?.ConfigValue;

            IWebHostEnvironment webHostEnvironment = (IWebHostEnvironment)App.ServiceProvider.GetService(typeof(IWebHostEnvironment));
            string sFileName = $"{data.Equipment.AssetName}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            string fullPath = Path.Combine(webHostEnvironment.WebRootPath, "export", sFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            MaintenanceExcel.WriteToExcel(fullPath, data);
            return fullPath;
        }

        #endregion 报表Sheet导出

        #region 报表概况
        /// <summary>
        /// 查询报表概况
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public (PagedInfo<EquipmentBase>, DataTable, List<SheetPartColumn>) GetReportOverview(MaintainReportOverviewQueryDto param)
        {
            if (param.DateMark == DateMarkConstant.月 || param.DateMark == DateMarkConstant.季 || param.DateMark == DateMarkConstant.年)
                param.Month = null;

            //获取对应保养日期标记的列信息
            List<SheetPartColumn> cols = GetSheetPartColumn(param.Year.Value, param.Month, param.DateMark);

            List<int> stamps = cols.Select(it => it.DateMarkStamp).ToList();

            PagedInfo<EquipmentBase> equPages = Context.Queryable<EquipmentBase>()
                .WhereIF(param.EquipmentId > 0, it => it.EquipmentId == param.EquipmentId.Value)
                .WhereIF(!string.IsNullOrEmpty(param.CostCenter), it => it.CostCenter == param.CostCenter)
                .ToPage(param);

            List<int> equipmentIds = equPages.Result.Select(it => it.EquipmentId).ToList();

            List<MaintainRecord> records = Context.Queryable<MaintainRecord>()
                    .Where(it => equipmentIds.Contains(it.EquipmentId.Value) && it.Year == param.Year && it.DateMark == param.DateMark && it.IsVisible == SysYesNoConstant.是 && it.DateMarkStamp >= stamps.First() && it.DateMarkStamp <= stamps.Last())
                    .WhereIF(param.DateMark == DateMarkConstant.日, it => it.TimeMark == param.TimeMark)//判断当日期为“D”，日保养时，增加时间标记判断
                    .Select(it => new MaintainRecord { Id = it.Id, EquipmentId = it.EquipmentId, DateMarkStamp = it.DateMarkStamp })
                    .ToList();

            DataTable dt = new();
            dt.Columns.Add("EquipmentId");
            dt.Columns.Add("EquipmentName");
            dt.Columns.Add("AssetNo");
            foreach (SheetPartColumn col in cols)
            {
                dt.Columns.Add(col.DateMarkStamp.ToString()).Caption = col.ColumnName;
            }
            foreach (EquipmentBase equ in equPages.Result)
            {
                dt.Rows.Add(new object[] { equ.EquipmentId, equ.EquipmentName, equ.AssetNo });
            }

            //转换为DataTable结构(行标题为项目名，列标题为时间戳
            foreach (MaintainRecord record in records)
            {
                foreach (DataRow row in dt.Rows)
                {
                    //填充ID 行数据
                    if (row["EquipmentId"].ToString() == record.EquipmentId.ToString())
                        row[record.DateMarkStamp.ToString()] = record.Id;
                }
            }

            return (equPages, dt, cols);
        }
        #endregion

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<MaintainReport> QueryExp(MaintainReportQueryDto parm)
        {
            var predicate = Expressionable.Create<MaintainReport>();

            predicate = predicate.AndIF(parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId);
            predicate = predicate.AndIF(parm.Year != null, it => it.Year == parm.Year);
            return predicate;
        }
    }
}
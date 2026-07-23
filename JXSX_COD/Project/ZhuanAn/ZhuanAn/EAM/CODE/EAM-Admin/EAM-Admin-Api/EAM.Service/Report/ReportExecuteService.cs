using EAM.Model;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Report;
using EAM.Service.Report.IReportService;
using Infrastructure;
using Infrastructure.Attribute;
using Newtonsoft.Json.Linq;
using SqlSugar.IOC;
using System.Data;
using System.Text.RegularExpressions;

namespace EAM.Service.Report
{
    /// <summary>
    /// 报表执行Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IReportExecuteService), ServiceLifetime = LifeTime.Transient)]
    public class ReportExecuteService : BaseService<ReportBase>, IReportExecuteService
    {
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        public ReportInfoDto GetReportInfo(int ReportId)
        {
            ReportInfoDto response = new ReportInfoDto();
            response.ReportId = ReportId;
            response.Params = Context.Queryable<ReportParam>().Where(it => it.ReportId == ReportId)
                .OrderBy(it => it.SortOrder)
                .Select(it => new ReportParamDto() { }, true)
                .ToList();
            response.Columns = Context.Queryable<ReportColumn>().Where(it => it.ReportId == ReportId)
              .OrderBy(it => it.SortOrder)
              .Select(it => new ReportColumnDto() { }, true)
              .ToList();

            return response;
        }

        /// <summary>
        /// 获取参数的下拉选项目
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public List<DictDataDto> GetReportParamOptions(ReportParamOptionsQueryDto parm)
        {
            ReportParam reportParam = Context.Queryable<ReportParam>().Where(it => it.ReportId == parm.ReportId && it.ParamKey == parm.ParamKey).First();
            if (reportParam == null)
                return null;
            if (reportParam.ElementType.ToLower() == "select" || reportParam.ElementType.ToLower() == "multiselect")
            {
                SqlSugarScopeProvider provider = DbScoped.SugarScope.GetConnectionScope(parm.FactoryId);
                if (!string.IsNullOrEmpty(reportParam.OptionsSource))
                {
                    //判断是否有过键字
                    List<SugarParameter> sqlParams = new List<SugarParameter>();
                    if (parm.Keyword != null)
                    {
                        if (reportParam.OptionsSource.ToLower().Contains(" like @keyword"))
                            sqlParams.Add(new SugarParameter("@keyword", "%" + parm.Keyword + "%"));
                        else
                            sqlParams.Add(new SugarParameter("@keyword", parm.Keyword));
                    }

                    string sql = ConvertSqlTemplate(reportParam.OptionsSource, sqlParams);

                    return provider.SqlQueryable<DictDataDto>(sql)
                           .AddParameters(sqlParams)
                           .ToPageList(parm.PageNum, parm.PageSize);
                }
                else
                {
                    return new List<DictDataDto>();
                }
            }
            return null;
        }

        /// <summary>
        /// 查询报表执行结果
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DataTable> GetPageList(ReportExecuteQueryDto parm)
        {
            //获取SQL模板
            ReportBase report = Context.Queryable<ReportBase>().Where(it => it.Enabled == true && it.ReportId == parm.ReportId).First();
            if (report == null)
            {
                throw new CustomException("未找到有效的报表模板");
            }
            //获取参数定义
            List<ReportParam> repParams = Context.Queryable<ReportParam>().Where(it => it.ReportId == parm.ReportId)
              .OrderBy(it => it.SortOrder)
              .ToList();
            //获取结果列定义
            List<ReportColumn> repColumns = Context.Queryable<ReportColumn>().Where(it => it.ReportId == parm.ReportId)
                .OrderBy(it => it.SortOrder)
                .ToList();

            int total = 0;
            DataTable dt;
            try
            {
                //转换参数
                JObject jsonParams = null;
                if (!string.IsNullOrEmpty(parm.JsonParams))
                    jsonParams = JObject.Parse(parm.JsonParams);
                List<SugarParameter> sqlParams = ConvertSqlParams(jsonParams, repParams);
                //转换sql
                string sql = ConvertSqlTemplate(report.SqlTemplate, sqlParams);

                //指定数据源
                SqlSugarScopeProvider provider = null;
                if (!string.IsNullOrEmpty(report.DatasourceId))
                {
                    provider = DbScoped.SugarScope.GetConnectionScope(report.DatasourceId);
                }
                else
                {
                    provider = DbScoped.SugarScope.GetConnectionScope(parm.FactoryId);
                }
                //数量

                dt = provider.SqlQueryable<object>(sql)
                       .AddParameters(sqlParams)
                       .OrderByIF(!string.IsNullOrEmpty(parm.Sort), parm.Sort + " " + parm.SortType)
                       .ToDataTablePage(parm.PageNum, parm.PageSize, ref total);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }

            //分页结果返回
            PagedInfo<DataTable> result = new PagedInfo<DataTable>();
            result.Result = new List<DataTable>();
            result.Result.Add(dt);
            result.PageSize = parm.PageSize;
            result.PageIndex = parm.PageNum;
            result.TotalNum = total;

            return result;
        }

        /// <summary>
        /// 查询报表执行结果
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public DataTable GetAllList(ReportExecuteQueryDto parm)
        {
            //获取SQL模板
            ReportBase report = Context.Queryable<ReportBase>().Where(it => it.Enabled == true && it.ReportId == parm.ReportId).First();
            if (report == null)
            {
                throw new CustomException("未找到有效的报表模板");
            }
            //获取参数定义
            List<ReportParam> repParams = Context.Queryable<ReportParam>().Where(it => it.ReportId == parm.ReportId)
              .OrderBy(it => it.SortOrder)
              .ToList();
            //获取结果列定义
            List<ReportColumn> repColumns = Context.Queryable<ReportColumn>().Where(it => it.ReportId == parm.ReportId)
                .OrderBy(it => it.SortOrder)
                .ToList();

            DataTable dt;
            try
            {
                //转换参数
                JObject jsonParams = null;
                if (!string.IsNullOrEmpty(parm.JsonParams))
                    jsonParams = JObject.Parse(parm.JsonParams);
                List<SugarParameter> sqlParams = ConvertSqlParams(jsonParams, repParams);
                //转换sql
                string sql = ConvertSqlTemplate(report.SqlTemplate, sqlParams);

                //指定数据源
                SqlSugarScopeProvider provider = null;
                if (!string.IsNullOrEmpty(report.DatasourceId))
                {
                    provider = DbScoped.SugarScope.GetConnectionScope(report.DatasourceId);
                }
                else
                {
                    provider = DbScoped.SugarScope.GetConnectionScope(parm.FactoryId);
                }

                dt = provider.SqlQueryable<object>(sql)
                       .AddParameters(sqlParams)
                       .OrderByIF(!string.IsNullOrEmpty(parm.Sort), parm.Sort + " " + parm.SortType)
                       .ToDataTable();

                if (dt != null)
                {
                    //列名转换
                    foreach (DataColumn col in dt.Columns)
                    {
                        col.Caption = repColumns.Where(it => it.ColumnKey == col.ColumnName).Select(it => it.ColumnLabel).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 将接口的参数转换为sql参数
        /// </summary>
        /// <param name="jsonParams"></param>
        /// <param name="reportParams"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private List<SugarParameter> ConvertSqlParams(JObject jsonParams, List<ReportParam> reportParams)
        {
            List<SugarParameter> sqlParams = new List<SugarParameter>();
            JToken tempValue = null;
            object val = null;
            bool isArray = false;
            foreach (ReportParam p in reportParams)
            {
                tempValue = null;
                val = null;
                isArray = p.ElementType.ToLower() == "multiselect";//多选组件是数组类型
                if (jsonParams != null)
                    jsonParams.TryGetValue(p.ParamKey, out tempValue);
                //检查是否必填
                if (p.Required == true && tempValue == null)
                    throw new CustomException($"必填参数【{p.ParamLabel}】未输入有效的值");
                //判断是否有指定参数
                if (tempValue != null)
                {
                    switch (p.InputType)
                    {
                        case ReportParamInputTypeConstant.字符串:
                            val = p.HeadValue + GetParamValue<string>(tempValue, isArray) + p.TailValue;
                            break;

                        case ReportParamInputTypeConstant.整数:
                            val = GetParamValue<int>(tempValue, isArray);
                            break;

                        case ReportParamInputTypeConstant.长整数:
                            val = GetParamValue<long>(tempValue, isArray);
                            break;

                        case ReportParamInputTypeConstant.双精度:
                            val = GetParamValue<double>(tempValue, isArray);
                            break;

                        case ReportParamInputTypeConstant.布尔:
                            val = GetParamValue<bool>(tempValue, isArray);
                            break;

                        case ReportParamInputTypeConstant.日期:
                            val = GetParamValue<DateTime>(tempValue, isArray);
                            break;

                        case ReportParamInputTypeConstant.日期时间:
                            val = GetParamValue<DateTime>(tempValue, isArray);
                            break;

                        default:
                            val = p.HeadValue + GetParamValue<string>(tempValue, isArray) + p.TailValue;
                            break;
                    }

                    if(val != null)
                    {
                        sqlParams.Add(new SugarParameter(p.ParamKey, val));
                    }
                }
            }

            return sqlParams;
        }

        /// <summary>
        /// 获取JToken中的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jToken"></param>
        /// <param name="isArray"></param>
        /// <returns></returns>
        private object GetParamValue<T>(JToken jToken, bool isArray = false)
        {
            if (isArray)
            {
                List<T> r = jToken.ToObject<List<T>>();
                return r == null || r.Count <= 0 ? null : r;
            }
            else
                return jToken.Value<T>();
        }

        /// <summary>
        /// 将换SQL模板
        /// </summary>
        /// <param name="sqlTemplate"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private string ConvertSqlTemplate(string sqlTemplate, List<SugarParameter> sqlParams)
        {
            var ifPattern = new Regex(@"\[(.*?)\]", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection matches;
            //获取模板参数作用区域
            while (true)
            {
                matches = ifPattern.Matches(sqlTemplate);
                if (matches.Count == 0) break;

                // 从后向前替换，避免索引偏移问题，或者每次只处理第一个匹配并重新扫描
                // 这里选择处理第一个匹配，然后重新扫描，逻辑最清晰
                var match = matches[0];
                string innerContent = match.Groups[1].Value.Trim();//内容
                string fullMatch = match.Value;

                //检查是否有传递参数
                bool hasValue = false;
                foreach (SugarParameter p in sqlParams)
                {
                    if (innerContent.Contains(p.ParameterName))
                    {
                        hasValue = true;
                        break;
                    }
                }

                if (hasValue)
                {
                    // 有值：保留内容，去掉标签
                    // 注意：innerContent 中可能还包含其他的 :param 引用，稍后统一处理
                    sqlTemplate = sqlTemplate.Replace(fullMatch, innerContent);
                }
                else
                {
                    // 无值：整个块（包括内容）都删掉
                    sqlTemplate = sqlTemplate.Replace(fullMatch, string.Empty);
                }
            }
            return sqlTemplate;
        }
    }
}
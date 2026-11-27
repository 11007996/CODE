using Microsoft.Extensions.Primitives;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Text;

namespace POMS_API.Models
{
    public class BasicData
    {
        /// <summary>
        /// 线体资料
        /// </summary>
        public class Line
        {
            #region 字段
            public int lineId {  get; set; }
            public string lineName {  get; set; }
            public string lineAlias {  get; set; }
            public string lineDesc {  get; set; }
            public int empId {  get; set; }
            public string enabled{  get; set; }
            public string remark{  get; set; }
            public int shiftid { get; set; }   //白夜班ID
            public string shiftName { get; set; }   //白夜班
            #endregion

            //新增线体里检查数据
            private string checkAddInfo()
            {
                string res = "OK";
                if(string.IsNullOrEmpty( this.lineName))
                {
                    res = "线体名称不能为空";
                    return res;
                }
                string sSql = "select 1 from poms.sys_line where LINE_NAME=:line";
                var paras = new Dictionary<string, Object>();
                paras.Add("line", this.lineName);
                DataTable dt = DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras).Tables[0];
                if (dt.Rows.Count > 0) 
                {
                    res = "线体名称重复，请修改";
                    return res;
                }
                return res;
            }

            /// 新增线体
            public string AddLine()
            {
                string res = checkAddInfo();
                if(res!= "OK")
                {
                    return res;
                }
                try
                {
                    var sqlParams = new Dictionary<string, Object>();
                    sqlParams.Add("TFIELD", "LINE_ID");
                    sqlParams.Add("TTABLE", "poms.sys_line");
                    sqlParams.Add("TNUM", 5);
                    sqlParams.Add("TRES", "1111111111111111111111111111111");
                    sqlParams.Add("T_MAXID", "111111111111");
                    Dictionary<string, object> results = DBhelper.mesdbhelper.ExecuteStoreProc("POMS.POMS_GET_MAXID", sqlParams);
                    res = results["TRES"].ToString();
                    string maxid = string.Empty;
                    if(res == "OK")
                    {
                        res = results["T_MAXID"].ToString();
                        this.lineId =Convert.ToInt32(res);
                    }
                    else
                    {
                        return res;
                    }

                    string sSql = "insert into poms.sys_line(LINE_ID,LINE_NAME,LINE_ALIAS,LINE_DESC,UPDATE_EMPID,ENABLED,REMARK,SHIFT_ID)values(:lineId,:lineName,:lineAlias,:lineDesc,:empId,:enabled,:remark,:shiftid)";
                    var paras = new Dictionary<string, Object>();
                    paras.Add("lineId", this.lineId);
                    paras.Add("lineName", this.lineName);
                    paras.Add("lineAlias", this.lineAlias);
                    paras.Add("lineDesc", this.lineDesc);
                    paras.Add("empId", this.empId);
                    paras.Add("enabled", this.enabled);
                    paras.Add("remark", this.remark);
                    paras.Add("shiftid", this.shiftid);
                    DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras);
                    return "OK";
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }
            }


            /// 获取线体基本信息(初步设计用于显示线别页面)
            public Line GetLineById()
            {
                Line line = new Line();
                string sSql = "select LINE_NAME,LINE_ALIAS,LINE_DESC from poms.sys_line where LINE_ID=:lineId";
                var paras = new Dictionary<string, Object>();
                paras.Add("lineId", this.lineId);
                DataTable dt = DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras).Tables[0];
                if(dt.Rows.Count > 0)
                {
                    line = new Line();
                    line.lineId = this.lineId;
                    line.lineName = dt.Rows[0][0].ToString();
                    line.lineAlias = dt.Rows[0][1].ToString();
                    line.lineDesc = dt.Rows[0][2].ToString();
                    return line;
                }
                else
                {
                    return line;
                }
            }


            /// 获取线体详细信息(初步设计用于显示线别设置页面)
            public Line GetLineDetailsById()
            {
                Line line = new Line();
                string sSql = "select  a.LINE_ID,a.LINE_NAME,a.LINE_ALIAS,a.LINE_DESC,a.ENABLED,a.REMARK,b.SHIFT_ID,b.SHIFT_NAME from poms.sys_line a,poms.sys_shift b where a.shift_id=b.shift_id(+) and a.line_id=:lineId";
                var paras = new Dictionary<string, Object>();
                paras.Add("lineId", this.lineId);
                DataTable dt = DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    line.lineId = this.lineId;
                    line.lineName = dt.Rows[0]["LINE_NAME"].ToString();
                    line.lineAlias = dt.Rows[0]["LINE_ALIAS"].ToString();
                    line.lineDesc = dt.Rows[0]["LINE_DESC"].ToString();
                    line.enabled = dt.Rows[0]["ENABLED"].ToString();
                    line.remark = dt.Rows[0]["REMARK"].ToString();
                    line.shiftid = dt.Rows[0]["SHIFT_ID"].ToString()==string.Empty ?0: Convert.ToInt32(dt.Rows[0]["SHIFT_ID"].ToString());
                    line.shiftName = dt.Rows[0]["SHIFT_NAME"].ToString();
                    return line;
                }
                else
                {
                    return line;
                }
            }

            //修改线体
            public string ModifyLine()
            {
                string res = "OK";
                if (string.IsNullOrEmpty(this.lineName))
                {
                    res = "线体名称不能为空";
                    return res;
                }
                if(this.lineId == 0)
                {
                    res = "线体ID为空，修改异常";
                    return res;
                }
                try
                {
                    string sSql = "update poms.SYS_LINE set LINE_NAME=:linename,LINE_ALIAS=:linealias,LINE_DESC=:linedesc,UPDATE_EMPID=:empid,UPDATE_TIME=sysdate,ENABLED=:enabled,REMARK=:remark,SHIFT_ID=:shiftid where LINE_ID=:lineid";
                    var paras = new Dictionary<string, Object>();
                    paras.Add("linename", this.lineName);
                    paras.Add("linealias", this.lineAlias);
                    paras.Add("linedesc", this.lineDesc);
                    paras.Add("empid", this.empId);
                    paras.Add("enabled", this.enabled);
                    paras.Add("remark", this.remark);
                    paras.Add("shiftid", this.shiftid);
                    paras.Add("lineid", this.lineId);
                    DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras);
                    return res;
                }
                catch (Exception ex)
                {
                    res = ex.Message;
                    return res;
                }
                
            }


            //删除线体
            public string DeleteLine()
            {
                string res = "OK";
                if (this.lineId == 0)
                {
                    res = "线别ID为空";
                }
                else
                {
                    try
                    {
                        string sSql = "delete from poms.SYS_LINE where LINE_ID=:lineid";
                        var paras = new Dictionary<string, Object>();
                        paras.Add("lineid", this.lineId);
                        DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras);
                        res = "OK";
                    }
                    catch (Exception ex) 
                    {
                        res = ex.Message;
                    }
                }
                return res;
            }


            //查询所有线体
            public List<Line> GetLineAll()
            {
                StringBuilder strbSql = new StringBuilder();
                strbSql.Append("select LINE_ID,LINE_NAME,LINE_ALIAS,LINE_DESC from poms.SYS_LINE where 1=1");
                var paras = new Dictionary<string, Object>();
                if (!string.IsNullOrEmpty(this.lineName))
                {
                    strbSql.Append(" and LINE_NAME=:linename");
                    paras.Add("linename", this.lineName);
                }
                if (!string.IsNullOrEmpty(this.enabled))
                {
                    strbSql.Append(" and ENABLED=:lineenabled");
                    paras.Add("lineenabled", this.enabled);
                }

                DataTable dt;
                if (paras.Count > 0)
                {
                    dt = DBhelper.mesdbhelper.ExecuteSQLList(strbSql.ToString(), paras).Tables[0];
                }
                else
                {
                    dt = DBhelper.mesdbhelper.ExecuteDataTable(strbSql.ToString());
                }
                List<Line> lines = new List<Line>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Line line = new Line();
                        line.lineId = int.Parse(dt.Rows[i][0].ToString());
                        line.lineName = dt.Rows[i][1].ToString();
                        line.lineAlias = dt.Rows[i][2].ToString();
                        line.lineDesc = dt.Rows[i][3].ToString();
                        lines.Add(line);
                    }
                    return lines;
                }
                else
                {
                    return lines;
                }
            }
        }
        /// <summary>
        /// 班别资料
        /// </summary>
        public class Shift
        {
            #region 班别
            public int shiftid { set; get; }
            public string shiftName { set; get; }
            #endregion

            public List<Shift> GetShiftOverview()
            {
                List<Shift> shiftList = new List<Shift>();
                string sSql = "select SHIFT_ID,SHIFT_NAME from poms.sys_shift where ENABLED='Y'";
                DataTable dt = DBhelper.mesdbhelper.ExecuteDataTable(sSql);
                if(dt.Rows.Count>0)
                {
                    for(int i = 0;i<dt.Rows.Count;i++)
                    {
                        Shift shift = new Shift();
                        shift.shiftid =Convert.ToInt32(dt.Rows[i][0].ToString());
                        shift.shiftName = dt.Rows[i][1].ToString();
                        shiftList.Add(shift);
                    }
                    return shiftList;
                }
                else
                {
                    return shiftList;
                }
            }
        }

        /// <summary>
        /// 部门资料
        /// </summary>
        public class Departmant
        {
            public int departId { set; get; }
            public string departCode { set; get; }
            public string departName { set; get; }
            public string enabled { set; get; }
            public int update_empid { set; get; }

            //获取所有部门
            public List<Departmant> GetDepartAll()
            {
                List<Departmant> departmants = new List<Departmant>();
                string sSql = "select DEPART_ID,DEPART_CODE,DEPART_NAME,ENABLED from poms.sys_depart where enabled=:enabled";
                var paras = new Dictionary<string, Object>();
                paras.Add("enabled", this.enabled);
                DataTable dt = DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras).Tables[0];
                if(dt.Rows.Count>0)
                {
                    
                    for (int i = 0;i< dt.Rows.Count; i++)
                    {
                        Departmant depart = new Departmant();
                        depart.departId =int.Parse(dt.Rows[i][0].ToString());
                        depart.departCode = dt.Rows[i][1].ToString();
                        depart.departName = dt.Rows[i][2].ToString();
                        depart.enabled = dt.Rows[i][3].ToString();
                        departmants.Add(depart);
                    }
                }
                return departmants;
            }

            //修改部门
            public string ModifyDepartById()
            {
                string res = "OK";
                if(this.departId ==0)
                {
                    res = "部门ID为空";
                    return res;
                }
                try
                {
                    string sSql = "update poms.sys_depart set DEPART_CODE=:code,DEPART_NAME=:name,ENABLED=:enabled,UPDATE_TIME=sysdate,UPDATE_EMPID=:emp where DEPART_ID=:departid";
                    var paras = new Dictionary<string, Object>();
                    paras.Add("code", this.departCode);
                    paras.Add("name", this.departName);
                    paras.Add("enabled", this.enabled);
                    paras.Add("emp", this.update_empid);
                    paras.Add("departid", this.departId);
                    DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras);
                    return res;
                }
                catch(Exception ex)
                {
                    res = ex.Message;
                    return res;
                }
            }

            //新增线体
            public string AddDepart()
            {
                string res = "OK";
                if(string.IsNullOrEmpty(this.departName))
                {
                    res = "部门名称不能为空";
                    return res;
                }
                if(string.IsNullOrEmpty(this.enabled))
                {
                    res = "是否启用不能为空";
                    return res;
                }
                if (string.IsNullOrEmpty(this.departCode))
                {
                    res = "部门代码不能为空";
                    return res;
                }
                try
                {
                    string sSql = "select 1 from poms.sys_depart where DEPART_CODE=:code or DEPART_NAME=:name";
                    var paras = new Dictionary<string, Object>();
                    paras.Add("code", this.departCode);
                    paras.Add("name", this.departName);
                    DataTable dt= DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        res = "部门代码或部门名称重复";
                        return res;
                    }

                    var sqlParams = new Dictionary<string, Object>();
                    sqlParams.Add("TFIELD", "DEPART_ID");
                    sqlParams.Add("TTABLE", "poms.sys_depart");
                    sqlParams.Add("TNUM", 5);
                    sqlParams.Add("TRES", "1111111111111111111111111111111");
                    sqlParams.Add("T_MAXID", "111111111111");
                    Dictionary<string, object> results = DBhelper.mesdbhelper.ExecuteStoreProc("POMS.POMS_GET_MAXID", sqlParams);
                    res = results["TRES"].ToString();
                    if (res == "OK")
                    {
                        this.departId = Convert.ToInt32(results["T_MAXID"].ToString());
                    }
                    else
                    {
                        return res;
                    }

                    sSql = "insert into poms.sys_depart(DEPART_ID,DEPART_CODE,DEPART_NAME,ENABLED,UPDATE_TIME,UPDATE_EMPID)values(:id,:code,:name,'Y',sysdate,:empid)";
                    paras.Clear();
                    paras.Add("id", this.departId);
                    paras.Add("code", this.departCode);
                    paras.Add("name", this.departName);
                    paras.Add("empid", this.update_empid);
                    DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras);
                    return res +":"+ this.departId;
                }
                catch(Exception ex)
                {
                    res = ex.Message;
                    return res;
                }
            }

            public string DateleDepart()
            {
                string res = "OK";
                if(this.departId == 0)
                {
                    res = "部门ID不能为空";
                    return res;
                }
                try
                {
                    string sSql = "delete from poms.sys_depart where DEPART_ID=:departid";
                    var paras = new Dictionary<string, Object>();
                    paras.Add("departid", this.departId);
                    DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras);
                    return res;
                }
                catch (Exception ex)
                {
                    res = ex.Message;
                    return res;
                }
            }
        }
    }
}

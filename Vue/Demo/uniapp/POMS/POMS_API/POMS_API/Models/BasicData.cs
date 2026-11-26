using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

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
        }

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
    }
}

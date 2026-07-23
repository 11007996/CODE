using Asset.DAL.Util;
using Asset.Model;
using Asset.Model.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asset.DAL
{
    public class AssetDAL
    {

        /// <summary>
        /// 搜索匹配的资产，最多10条记录
        /// </summary>
        /// <returns></returns>
        public IList<AssetInfoDO> GetAssetList(string keywords)
        {
            string sql = "Select top(10) * From A_AssetInfo_T WHERE 1<>1 ";
            List<SqlParameter> param = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                sql += " Or AssetName LIKE @keywords OR AssetNo LIKE @keywords";
                param.Add(new SqlParameter("@keywords", "%" + keywords + "%"));
            }
            DataTable dt = DBUtil.GetDataTable(sql, param);
            return DataTableConvert.ToList<AssetInfoDO>(dt);
        }


        /// <summary>
        /// 获取资产详情
        /// </summary>
        /// <returns></returns>
        public AssetInfoDO GetAssetInfo(string assetNo)
        {
            string sql = string.Format("Select  * From A_AssetInfo_T WHERE AssetNo=@AssetNo ", assetNo);
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@AssetNo", assetNo));
            DataTable dt = DBUtil.GetDataTable(sql, param);
            if (dt.Rows.Count == 1)
            {
                return DataTableConvert.ToList<AssetInfoDO>(dt)[0];
            }
            return null;
        }

        /// <summary>
        /// 获取保养项目
        /// </summary>
        /// <param name="assetNo">资产编号</param>
        /// <param name="year">年份</param>
        /// <param name="timeMark">日期标志</param>
        /// <returns></returns>
        public List<string> GetMaintenanceItems(string assetNo, int year, string timeMark)
        {
            string sql = "Select ItemName From A_MaintenanceReportItem_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND  TimeMark=@TimeMark Order by SortNo ASC ";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@AssetNo", assetNo));
            param.Add(new SqlParameter("@RYear", year));
            param.Add(new SqlParameter("@TimeMark", timeMark));
            DataTable dt = DBUtil.GetDataTable(sql, param);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dt.AsEnumerable().Select(t => t.Field<string>("ItemName")).ToList();
            }
        }

        public int? GetMaintenanceRecordId(string assetNo, int year, string timeMark, int timeMarkStamp)
        {
            string sql = "Select Id FROM  A_MaintenanceReportD_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark=@TimeMark AND TimeMarkStamp=@TimeMarkStamp AND ISNULL(IsVisible,'Y')<>'N';";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@AssetNo", assetNo));
            param.Add(new SqlParameter("@RYear", year));
            param.Add(new SqlParameter("@TimeMark", timeMark));
            param.Add(new SqlParameter("@TimeMarkStamp", timeMarkStamp));
            DataTable dt = DBUtil.GetDataTable(sql, param);
            if (dt != null && dt.Rows.Count == 1)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return null;
        }

        public DataTable GetMaintenanceDetailItemData(int id)
        {
            string sql = "Select ItemName,ItemValue From A_MaintenanceReportDV_T WHERE MRDId=@Id ";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", id));
            DataTable dt = DBUtil.GetDataTable(sql, param);
            return dt;
        }

        //添加保养记录
        public int? InsertMaintenanceD(MaintenanceReportDetialDO param, string workCode)
        {
            string sql = @"Insert INTO   A_MaintenanceReportD_T(AssetNo,[Year],TimeMark,TimeMarkStamp,UpdateUser,UpdateTime) 
                           Values(@AssetNo,@RYear,@TimeMark,@TimeMarkStamp,@UpdateUser,GETDATE())  SELECT  SCOPE_IDENTITY() ";
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@AssetNo", param.AssetNo));
            sqlParam.Add(new SqlParameter("@RYear", param.Year));
            sqlParam.Add(new SqlParameter("@TimeMark", param.TimeMark));
            sqlParam.Add(new SqlParameter("@TimeMarkStamp", param.TimeMarkStamp));
            sqlParam.Add(new SqlParameter("@UpdateUser", workCode == null ? "" : workCode));
            DataTable dt = DBUtil.GetDataTable(sql, sqlParam);
            if (dt != null && dt.Rows.Count == 1)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return null;
        }

        public void UpdateMaintenanceD(int id, string workCode)
        {
            string sql = "Update  A_MaintenanceReportD_T SET UpdateUser=@UpdateUser,UpdateTime=GETDATE() WHERE Id=@Id;";
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@UpdateUser", workCode == null ? "" : workCode));
            sqlParam.Add(new SqlParameter("@Id", id));
            DBUtil.ExecSQL(sql, sqlParam);
        }

        //删除原有详情，添加新的保养记录详情
        public bool SaveMaintenanceDV(int id, List<MaintenanceItemDO> items)
        {
            if (items == null || id == 0) return false;

            //删除原有的数据
            string sql = "DELETE A_MaintenanceReportDV_T WHERE MRDId=@MRDId;";
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@MRDId", id));
            DBUtil.ExecSQL(sql, sqlParam);

            //批量插入
            DataTable dt = new DataTable();
            dt.Columns.Add("MRDId", typeof(int));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemValue", typeof(string));
            foreach (MaintenanceItemDO item in items)
            {
                dt.Rows.Add(id, item.ItemName, item.ItemValue);
            }
            DBUtil.BatchInsertData("A_MaintenanceReportDV_T", dt, null);
            return true;
        }


        public FileInfoDO GetAssetFileInfo(string assetNo, FileClassEnum fileClass)
        {
            string sql = "SELECT TOP(1) f.* FROM A_AssetFile_T a,S_FileInfo f  WHERE a.FileId=f.FileId AND a.AssetNo=@AssetNo AND a.FileClass=@FileClass";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@AssetNo", assetNo));
            param.Add(new SqlParameter("@FileClass", (int)fileClass));
            DataTable dt = DBUtil.GetDataTable(sql, param);
            if (dt.Rows.Count == 1)
            {
                return DataTableConvert.ToList<FileInfoDO>(dt)[0];
            }
            return null;
        }

        public List<FileInfoDO> GetAssetFileInfos(string assetNo)
        {
            string sql = "SELECT  f.* FROM A_AssetFile_T a,S_FileInfo f  WHERE a.FileId=f.FileId AND a.AssetNo=@AssetNo";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@AssetNo", assetNo));
            DataTable dt = DBUtil.GetDataTable(sql, param);
            if (dt != null)
            {
                return DataTableConvert.ToList<FileInfoDO>(dt);
            }
            return null;
        }
    }
}

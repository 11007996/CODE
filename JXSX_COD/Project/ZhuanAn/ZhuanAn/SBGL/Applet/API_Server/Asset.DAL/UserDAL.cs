using Asset.DAL.Util;
using Asset.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.DAL
{
    public class UserDAL
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public SysUserDO GetUserInfo(string workCode)
        {
            if (string.IsNullOrWhiteSpace(workCode)) return null;
            string sql = "Select * From S_User_T WHERE  UserNo=@UserNo";
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@UserNo", workCode ?? ""));
            DataTable dt = DBUtil.GetDataTable(sql, sqlParam);
            if (dt.Rows.Count == 1)
            {
                return DataTableConvert.ToList<SysUserDO>(dt)[0];
            }
            return null;
        }

        public void SaveUserInfo(string token, UserInfoDO user)
        {
            string sql = "Insert S_UserInfo_T(WorkCode,UserName,Token,CreateTime,ExpiresTime,UserRight) VALUES(@WorkCode,@UserName,@Token,GETDATE(),@ExpiresTime,@UserRight)";
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@WorkCode", user.WorkCode ?? ""));
            sqlParam.Add(new SqlParameter("@UserName", user.UserName));
            sqlParam.Add(new SqlParameter("@Token", token));
            sqlParam.Add(new SqlParameter("@ExpiresTime", user.ExpiresTime));
            sqlParam.Add(new SqlParameter("@UserRight", user.UserRight ?? ""));
            DBUtil.ExecSQL(sql, sqlParam);
        }

        public DataTable GetAllCacheTokenUser()
        {
            string sql = string.Format("Select * From S_UserInfo_T WHERE ExpiresTime>GETDATE()");
            return DBUtil.GetDataTable(sql);
        }

        public UserInfoDO GetUserInfoByToken(string token)
        {
            string sql = "Select TOP(1) * From S_UserInfo_T WHERE ExpiresTime>GETDATE() AND Token=@Token";
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Token", token));
            DataTable dt = DBUtil.GetDataTable(sql, sqlParam);
            if (dt.Rows.Count == 1)
            {
                return DataTableConvert.ToList<UserInfoDO>(dt)[0];
            }
            return null;
        }
    }
}

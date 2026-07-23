using Asset.DAL;
using Asset.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asset.BLL
{
    public class UserService
    {
        private UserDAL dao = new UserDAL();

        //获取用户信息
        public SysUserDO GetUserInfo(string workCode)
        {
            return dao.GetUserInfo(workCode);
        }


        //保存用户token令牌 
        public void SaveUserInfo(string token ,UserInfoDO  user)
        {
            dao.SaveUserInfo(token,user);
        }

        //获取所有未过期的token
        public DataTable GetAllCacheTokenUser()
        {
            return dao.GetAllCacheTokenUser();
        }

        public UserInfoDO GetUserInfoByToken(string token)
        {
           return dao.GetUserInfoByToken(token);
        }
    }
}

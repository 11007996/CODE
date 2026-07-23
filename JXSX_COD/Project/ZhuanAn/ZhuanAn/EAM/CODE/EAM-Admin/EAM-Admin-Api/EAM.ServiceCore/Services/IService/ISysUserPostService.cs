using EAM.Model.System;

namespace EAM.ServiceCore.Services
{
    public interface ISysUserPostService
    {
        public void InsertUserPost(SysUser user);

        public List<int> GetUserPostsByUserId(long userId);

        public string GetPostsStrByUserId(long userId);

        bool Delete(long userId);
    }
}
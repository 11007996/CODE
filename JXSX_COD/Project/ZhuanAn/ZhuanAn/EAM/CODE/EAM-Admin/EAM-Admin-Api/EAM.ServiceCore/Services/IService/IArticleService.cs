using EAM.Model;
using EAM.Model.System;
using EAM.Model.System.Dto;

namespace EAM.ServiceCore.Services
{
    public interface IArticleService : IBaseService<Article>
    {
        PagedInfo<ArticleDto> GetList(ArticleQueryDto parm);

        PagedInfo<ArticleDto> GetMyList(ArticleQueryDto parm);

        /// <summary>
        /// 修改文章管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateArticle(Article model);

        PagedInfo<ArticleDto> GetHotList(ArticleQueryDto parm);

        int TopArticle(Article model);

        int ChangeArticlePublic(Article model);

        int UpdateArticleHit(int cid);
    }
}
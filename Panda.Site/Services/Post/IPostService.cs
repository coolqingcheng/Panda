using Panda.Site.Services.Post.Models;
using Panda.Tools.Aop;
using Panda.Tools.Web.RSS;

namespace Panda.Site.Services.Post;

public interface IPostService
{
    /// <summary>
    /// 获取最新
    /// </summary>
    /// <param name="top"></param>
    /// <returns></returns>
    [Cache(60 * 5)]
    Task<List<NewestPostModel>> GetNewest(int top);


    /// <summary>
    /// 获取标签
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<string>> GetTags(int pageIndex, int pageSize);


    /// <summary>
    /// 获取分类
    /// </summary>
    /// <param name="top"></param>
    /// <returns></returns>
    Task<List<CategoriesModel>> GetCategories(int top);

    /// <summary>
    /// 获取RSS数据
    /// </summary>
    /// <param name="top"></param>
    /// <returns></returns>
    Task<RssModel> GetRssData(int top);
}
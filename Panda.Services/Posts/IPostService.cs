using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using PostRequest = Panda.Entity.Models.PostRequest;

namespace Panda.Services.Posts;

public interface IPostService
{
    Task<PageResponse<ArticleItem>> GetArticleList(PostRequest request);
    
    Task<PageResponse<ArticleItem>> GetArticleListByCategoryId(PostCategoryRequest request);
    
    Task<ArticleDetailItem> GetArticle(int id);

    Task AddOrUpdate(PostAddOrUpdate request);

    Task<PageResponse<AdminArticleItemResponse>> AdminGetList(AdminPostGetListRequest request);
}
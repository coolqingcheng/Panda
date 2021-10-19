using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using ArticleRequest = Panda.Entity.Models.ArticleRequest;

namespace Panda.Services.Article;

public interface IArticleService
{
    Task<PageResponse<ArticleItem>> GetArticleList(ArticleRequest request);
    
    Task<PageResponse<ArticleItem>> GetArticleListByCategoryId(ArticleCategoryRequest request);
    
    Task<ArticleDetailItem> GetArticle(int id);

    Task AddOrUpdate(ArticleAddOrUpdate request);

    Task<PageResponse<AdminArticleItemResponse>> AdminGetList(AdminArticleGetListRequest request);
}
using Panda.Entity.Models;
using Panda.Entity.Responses;

namespace Panda.Services.Article;

public interface IArticleService
{
    Task<PageResponse<ArticleItem>> GetArticleList(ArticleRequest request);
    
    Task<PageResponse<ArticleItem>> GetArticleListByCategoryId(ArticleCategoryRequest request);
    
    Task<ArticleDetailItem> GetArticle(int Id);
}
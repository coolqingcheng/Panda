using Panda.Entity.Models;
using Panda.Entity.Responses;
using Panda.Repository.Article;

namespace Panda.Services.Article;

public interface IArticleService
{
    
    Task<PageResponse<List<ArticleItem>>> GetArticleList(ArticleRequest request);
}

public class ArticleService : IArticleService
{
    private readonly ArticleRepository _articleRepository;

    public ArticleService(ArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<PageResponse<List<ArticleItem>>> GetArticleList(ArticleRequest request)
    {
        return await _articleRepository.GetArticleList(request.Index,request.Size);
    }
}
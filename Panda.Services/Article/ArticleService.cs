using Microsoft.EntityFrameworkCore;
using Panda.Entity.Models;
using Panda.Entity.Responses;
using Panda.Repository.Article;
using Panda.Repository.Category;

namespace Panda.Services.Article;

public class ArticleService : IArticleService
{
    private readonly ArticleRepository _articleRepository;

    private readonly CategoryRepository _categoryRepository;

    public ArticleService(ArticleRepository articleRepository, CategoryRepository categoryRepository)
    {
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<PageResponse<ArticleItem>> GetArticleList(ArticleRequest request)
    {
        return await _articleRepository.GetArticleList(request.Index, request.Size);
    }

    public async Task<ArticleDetailItem> GetArticle(int id)
    {
        var item = await _articleRepository.Where(a => a.Id == id).Include(a => a.Account).Select(a =>
            new ArticleDetailItem
            {
                Id = a.Id,
                Title = a.Title,
                Summary = a.Summary,
                Content = a.Content,
                AddTime = a.AddTime,
                AccountId = a.Account.Id,
                AccountName = a.Account.UserName
            }).FirstOrDefaultAsync();
        var categories = await _categoryRepository.GetCategories(item.Id);
        item.Categories = categories.Select(a => new ArticleCategories()
        {
            CategoryId = a.Id,
            CategoryName = a.categoryName
        }).ToList();
        return item;
    }
}
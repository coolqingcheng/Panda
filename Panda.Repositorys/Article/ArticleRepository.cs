using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Entity.Models;
using Panda.Entity.Responses;
using Panda.Tools.Extensions;

namespace Panda.Repository.Article;

public class ArticleRepository : PandaRepository<Articles>
{
    public ArticleRepository(PandaContext context) : base(context)
    {
    }

    public async Task<PageResponse<ArticleItem>> GetArticleList(int index, int size)
    {
        var query = _context.Articles.AsQueryable();
        var res = await query.Page(index, size).Select(a => new ArticleItem()
        {
            Id = a.Id,
            Title = a.Title,
            Summary = a.Summary,
            AddTime = a.AddTime,
            Categories = a.ArticleCategoryRelations.Select(b => new ArticleCategories()
            {
                CategoryId = b.Categories.Id,
                CategoryName = b.Categories.categoryName
            }).ToList()
        }).ToListAsync();
        return new PageResponse<ArticleItem>()
        {
            Total = await query.CountAsync(),
            Data = res
        };
    }
}
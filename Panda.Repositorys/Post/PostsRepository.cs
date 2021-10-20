using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Tools.Extensions;

namespace Panda.Repository.Article;

public class ArticleRepository : PandaRepository<Posts>
{
    public ArticleRepository(PandaContext context) : base(context)
    {
    }

    public async Task<PageResponse<ArticleItem>> GetArticleList(int index, int size)
    {
        var query = _context.Posts.AsQueryable();
        var res = await query.Page(index, size).Select(a => new ArticleItem()
        {
            Id = a.Id,
            Title = a.Title,
            Summary = a.Summary,
            AddTime = a.AddTime,
            Categories = a.ArticleCategoryRelations.Select(b => new ArticleCategories()
            {
                Id = b.Categories.Id,
                CateName = b.Categories.categoryName
            }).ToList()
        }).ToListAsync();
        return new PageResponse<ArticleItem>()
        {
            Total = await query.CountAsync(),
            Data = res
        };
    }

    public async Task<PageResponse<AdminArticleItemResponse>> AdminGetList(AdminPostGetListRequest request)
    {
        var query = _context.Posts.AsQueryable();

       var list = await  query.Page(request).Include(a=>a.ArticleCategoryRelations).OrderByDescending(a => a.UpdateTime)
           .ThenByDescending(a=>a.AddTime).Select(a =>
            new AdminArticleItemResponse()
            {
                Title = a.Title,
                Id = a.Id,
                UpdateTime = a.UpdateTime,
                CategoryItems = a.ArticleCategoryRelations.Select(b=>new AdminCategoryItem()
                {
                    Id = b.Categories.Id,
                    CateName = b.Categories.categoryName
                }).ToList()
            }).ToListAsync();
       return new PageResponse<AdminArticleItemResponse>()
       {
           Data = list,
           Total = await query.CountAsync()
       };

    }
}
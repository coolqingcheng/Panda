using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Tools.Extensions;

namespace Panda.Repository.Article;

public class PostRepository : PandaRepository<Posts>
{
    public PostRepository(PandaContext context) : base(context)
    {
    }

    public async Task<List<ArticleItem>> GetLatestPosts(int top)
    {
        var res = await _context.Posts.OrderByDescending(a => a.AddTime).Take(top).Select(a => new ArticleItem()
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
        return res;
    }

    public async Task<PageDto<ArticleItem>> GetArticleList(PostRequest request)
    {
        var query = _context.Posts.Where(a=>a.Status==PostStatus.Publish);
        if (request.CategoryId > 0)
        {
            var category = await _context.Categories.Where(a => a.Id == request.CategoryId).FirstOrDefaultAsync();
            query = query.Where(a => a.ArticleCategoryRelations.Any(b => b.Categories == category));
        }

        var res = await query.OrderByDescending(a=>a.UpdateTime).Page(request).Select(a => new ArticleItem()
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
        return new PageDto<ArticleItem>()
        {
            Total = await query.CountAsync(),
            Data = res
        };
    }

    public async Task<PageDto<AdminPostItemResponse>> AdminGetList(AdminPostGetListRequest request)
    {
        var query = _context.Posts.AsQueryable();

        var list = await query.Page(request).Include(a => a.ArticleCategoryRelations)
            .OrderByDescending(a => a.UpdateTime)
            .ThenByDescending(a => a.AddTime).Select(a =>
                new AdminPostItemResponse()
                {
                    Title = a.Title,
                    Id = a.Id,
                    UpdateTime = a.UpdateTime,
                    Status = a.Status,
                    CategoryItems = a.ArticleCategoryRelations.Select(b => new AdminCategoryItem()
                    {
                        Id = b.Categories.Id,
                        CateName = b.Categories.categoryName
                    }).ToList()
                }).ToListAsync();
        return new PageDto<AdminPostItemResponse>()
        {
            Data = list,
            Total = await query.CountAsync()
        };
    }

    /// <summary>
    /// 全文搜索内容
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public async Task<PageDto<AdminPostItemResponse>> SearchPost(string keyword)
    {
        var query = _context.Posts.FromSqlRaw("select * from posts where match(title,text) against({0}) order by AddTime desc limit 20", keyword);
        var res =
            await query.Include(a => a.ArticleCategoryRelations).Include(a=>a.Account).OrderByDescending(a => a.UpdateTime)
                .ThenByDescending(a => a.AddTime).Select(a =>
                    new AdminPostItemResponse()
                    {
                        Title = a.Title,
                        Id = a.Id,
                        UpdateTime = a.UpdateTime,
                        AddTime = a.AddTime,
                        Summary = a.Summary,
                        AccountName = a.Account.UserName,
                        CategoryItems = a.ArticleCategoryRelations.Select(b => new AdminCategoryItem()
                        {
                            Id = b.Categories.Id,
                            CateName = b.Categories.categoryName
                        }).ToList()
                    }).ToListAsync();
        return new PageDto<AdminPostItemResponse>()
        {
            Total = 0,
            Data = res
        };
    }
}
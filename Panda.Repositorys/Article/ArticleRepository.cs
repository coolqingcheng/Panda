using System.Drawing;
using Panda.Entity.DataModels;
using Panda.Entity.Models;
using Panda.Entity.Responses;

namespace Panda.Repository.Article;

public class ArticleRepository:PandaRepository<Articles>
{
    public ArticleRepository(IFreeSql freeSql) : base(freeSql)
    {
    }

    public async Task<PageResponse<List<ArticleItem>>> GetArticleList(int index,int size)
    {
        var res =  await _freeSql.Select<Articles, ArticleCategoryRelations>()
            .LeftJoin(a => a.t1.Id == a.t2.ArticleId)
            .Page(index, size).Count(out var count).ToListAsync(a => new ArticleItem()
            {
                Title = a.t1.Title,
                Summary = a.t1.Summary,
                AddTime = a.t1.AddTime,
                Id = a.t1.Id,
                Categories = _freeSql.Select<Categorys>().Where(b=>b.Id==a.t2.CategoryId)
                    .ToList(c=>new ArticleCategories()
                    {
                        CategoryName = c.categoryName,
                        CategoryId = c.Id
                    })
            });
        return new PageResponse<List<ArticleItem>>()
        {
            Total = count,
            Data = res
        };
    }
}
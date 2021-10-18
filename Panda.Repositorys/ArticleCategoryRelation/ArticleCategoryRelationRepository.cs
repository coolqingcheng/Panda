using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Repository;

namespace Panda.Repository.ArticleCategoryRelation;

public class ArticleCategoryRelationRepository : PandaRepository<ArticleCategoryRelations>
{
    public ArticleCategoryRelationRepository(PandaContext context) : base(context)
    {
    }

    public async Task AddRelationAsync(Articles article, Categorys category)
    {
        await _context.ArticleCategoryRelations.AddAsync(new ArticleCategoryRelations()
        {
            Articles = article,
            Categories = category
        });
        await _context.SaveChangesAsync();
    }

    public async Task DiffUpdateRelation(Articles article, List<Categorys> before, List<Categorys> after)
    {
        var delList = before.Except(after);
        var addList = after.Except(before);
        var relations = await _context.ArticleCategoryRelations.Where(a => a.Articles == article).ToListAsync();
        _context.ArticleCategoryRelations.RemoveRange(relations.Where(a => delList.Contains(a.Categories)));
        foreach (var category in addList)
        {
            await _context.ArticleCategoryRelations.AddAsync(new ArticleCategoryRelations()
            {
                Articles = article,
                Categories = category
            });
        }

        await _context.SaveChangesAsync();
    }
}
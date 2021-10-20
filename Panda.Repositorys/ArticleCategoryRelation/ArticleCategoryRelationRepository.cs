using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Repository;

namespace Panda.Repository.ArticleCategoryRelation;

public class ArticleCategoryRelationRepository : PandaRepository<PostsCategoryRelations>
{
    public ArticleCategoryRelationRepository(PandaContext context) : base(context)
    {
    }

    public async Task AddRelationAsync(Posts post, Categorys category)
    {
        await _context.ArticleCategoryRelations.AddAsync(new PostsCategoryRelations()
        {
            Posts = post,
            Categories = category
        });
        await _context.SaveChangesAsync();
    }

    public async Task DiffUpdateRelation(Posts post, List<int> delIds, List<int> addList)
    {
       //移除
       var delList = await _context.ArticleCategoryRelations.Where(a => delIds.Contains(a.Categories.Id) &&a.Posts==post).ToListAsync();
       _context.ArticleCategoryRelations.RemoveRange(delList);
       await _context.SaveChangesAsync();
        var addCateList = _context.Categories.Where(a => addList.Contains(a.Id)).ToList();
        foreach (var category in addCateList)
        {
            await _context.ArticleCategoryRelations.AddAsync(new PostsCategoryRelations()
            {
                Posts = post,
                Categories = category
            });
        }

        await _context.SaveChangesAsync();
    }
}
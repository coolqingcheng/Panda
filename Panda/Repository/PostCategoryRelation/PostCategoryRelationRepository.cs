using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Repository;

namespace Panda.Repository.ArticleCategoryRelation;

public class PostCategoryRelationRepository : PandaRepository<PostsCategoryRelations>
{
    public PostCategoryRelationRepository(PandaContext context) : base(context)
    {
    }

    public async Task AddRelationAsync(Posts post, Categorys category)
    {
        await _context.ArticleCategoryRelations.AddAsync(new PostsCategoryRelations()
        {
            Posts = post,
            Categories = category
        });
        category.Count += 1;
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRelationAsync(Posts post)
    {
        var res = await _context.Posts.Where(a => a.Id == 100).ToListAsync();
        var list = await Where(a => a.Posts == post).Include(a => a.Categories).ToListAsync();
        foreach (var category in list)
        {
            category.Categories.Count -= 1;
        }

        await DeleteWhereAsync(a => a.Posts == post);

        await SaveAsync();
    }

    public async Task DiffUpdateRelation(Posts post, List<int> delIds, List<int> addList)
    {
        //移除
        var delList = await _context.ArticleCategoryRelations
            .Where(a => delIds.Contains(a.Categories.Id) && a.Posts == post).ToListAsync();
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
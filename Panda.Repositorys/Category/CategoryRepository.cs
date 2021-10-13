using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Category;

public class CategoryRepository : PandaRepository<Categorys>
{
    public CategoryRepository(PandaContext context) : base(context)
    {
    }

    public async Task<List<Categorys>> GetCategories(int articleId)
    {
        return await _context.ArticleCategoryRelations.Where(a => a.Articles.Id == articleId).Include(a => a.Categories)
            .Select(a => a.Categories)
            .ToListAsync();
    }
}
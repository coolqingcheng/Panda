using Mapster;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;

namespace Panda.Site.Areas.Admin.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly DbContext _dbContext;

    public CategoryService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CategoryItem>> GetCategories(CategoryPageRequest request)
    {
        var res = await _dbContext.Set<Categorys>()
            .WhereIf(string.IsNullOrWhiteSpace(request.CateName) == false,
                a => a.CategoryName.Contains(request.CateName!))
            .OrderByDescending(a => a.AddTime).Page(request).Select(a=>new CategoryItem()
            {
                CategoryName = a.CategoryName,
                CategoryDesc = a.CategoryDesc,
                Count = _dbContext.Set<PostsCategoryRelations>().Count(b => b.Categories==a),
                IsShow = a.IsShow,
                Id = a.Id,
                Pid = a.Pid
            }).ToListAsync();
        return res;
    }

    public async Task AddOrUpdate(CategoryAddOrUpdate request)
    {
        if (request.Id > 0)
        {
            var item = await _dbContext.Set<Categorys>().Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (item == null) throw new UserException("更新的分类不存");

            request.Adapt(item);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            var any = await _dbContext.Set<Categorys>().Where(a => a.CategoryName == request.CategoryName).AnyAsync();
            if (any) throw new UserException("新增分类已经存在");

            var entity = request.Adapt<Categorys>();
            await _dbContext.Set<Categorys>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var res = await _dbContext.Set<Categorys>().Where(a => a.Id == id).Select(a => new
        {
            a.Id,
            a.ArticleCategoryRelations.Count
        }).FirstOrDefaultAsync();
        if (res == null) throw new UserException("分类不存在");

        if (res.Count > 0) throw new UserException("分类不为空，无法删除");

        var item = await _dbContext.Set<Categorys>().FirstOrDefaultAsync(a => a.Id == id);
        if (item != null) _dbContext.Set<Categorys>().Remove(item);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<CategoryItem> GetCategoryById(int id)
    {
        var item = await _dbContext.Set<Categorys>().Where(a => a.Id == id).ProjectToType<CategoryItem>()
            .FirstOrDefaultAsync();
        item.IsNullThrow("分类为空！");
        return item!;
    }
}
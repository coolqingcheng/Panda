using EasyCaching.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Repository.Category;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;

namespace Panda.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly CategoryRepository _categoryRepository;

    private readonly IEasyCachingProvider _easyCachingProvider;

    public CategoryService(CategoryRepository categoryRepository, IEasyCachingProvider easyCachingProvider)
    {
        _categoryRepository = categoryRepository;
        _easyCachingProvider = easyCachingProvider;
    }

    public async Task<List<CategoryItem>> GetCategories(CategoryPageRequest request)
    {
        var res = await _categoryRepository.Queryable()
            .WhereIf(string.IsNullOrWhiteSpace(request.CateName) == false,
                a => a.CategoryName.Contains(request.CateName!))
            .OrderByDescending(a => a.AddTime).Page(request).ProjectToType<CategoryItem>().ToListAsync();
        return res;
    }

    public async Task AddOrUpdate(CategoryAddOrUpdate request)
    {
        if (request.Id > 0)
        {
            var item = await _categoryRepository.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (item == null)
            {
                throw new UserException("更新的分类不存");
            }

            request.Adapt(item);
            await _categoryRepository.SaveAsync();
        }
        else
        {
            var any = await _categoryRepository.Where(a => a.CategoryName == request.CategoryName).AnyAsync();
            if (any)
            {
                throw new UserException("新增分类已经存在");
            }

            var entity = request.Adapt<Categorys>();
            await _categoryRepository.AddAsync(entity);
        }
    }

    public async Task Delete(int id)
    {
        var res = await _categoryRepository.Where(a => a.Id == id).Select(a => new
        {
            a.Id,
            a.ArticleCategoryRelations.Count
        }).FirstOrDefaultAsync();
        if (res == null)
        {
            throw new UserException("分类不存在");
        }

        if (res.Count > 0)
        {
            throw new UserException("分类不为空，无法删除");
        }

        await _categoryRepository.DeleteWhereAsync(a => a.Id == id);
    }

    public async Task<List<CategoryItem>> GetCategoriesByCache(CategoryPageRequest request, TimeSpan timeSpan)
    {
        var cache = await _easyCachingProvider.GetAsync<List<CategoryItem>>(CacheKeys.Categories);
        if (cache.HasValue)
        {
            return cache.Value;
        }

        var res = (await GetCategories(request)).Where(a => a.Count > 0 && a.IsShow).ToList();
        await _easyCachingProvider.SetAsync(CacheKeys.Categories, res, timeSpan);
        return res;
    }

    public async Task<CategoryItem> GetCategoryById(int id)
    {
        var item = await _categoryRepository.Where(a => a.Id == id).ProjectToType<CategoryItem>().FirstOrDefaultAsync();
        item.IsNullThrow("分类为空！");
        return item!;
    }
}
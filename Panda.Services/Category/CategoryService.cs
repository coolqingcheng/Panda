using EasyCaching.Core;
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

    private IEasyCachingProvider _easyCachingProvider;

    public CategoryService(CategoryRepository categoryRepository, IEasyCachingProvider easyCachingProvider)
    {
        _categoryRepository = categoryRepository;
        _easyCachingProvider = easyCachingProvider;
    }

    public async Task<List<CategoryItem>> GetCategories(CategoryPageRequest request)
    {
        var res = await _categoryRepository.Where(a => a.IsShow)
            .WhereIf(string.IsNullOrWhiteSpace(request.CateName) == false,
                a => a.categoryName.Contains(request.CateName))
            .OrderByDescending(a => a.AddTime).Page(request).Select(a => new CategoryItem()
            {
                CateName = a.categoryName,
                Id = a.Id,
                Pid = a.Pid
            }).ToListAsync();
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

            item.categoryName = request.CateName;
            item.Pid = request.Pid;

            await _categoryRepository.SaveAsync();
        }
        else
        {
            var any = await _categoryRepository.Where(a => a.categoryName == request.CateName).AnyAsync();
            if (any)
            {
                throw new UserException("新增分类已经存在");
            }

            await _categoryRepository.AddAsync(new Categorys()
            {
                categoryName = request.CateName,
                Pid = request.Pid,
                IsShow = true
            });
        }
    }

    public async Task Delete(int Id)
    {
        var res = await _categoryRepository.Where(a => a.Id == Id).Select(a => new
        {
            a.Id,
            Count = a.ArticleCategoryRelations.Count
        }).FirstOrDefaultAsync();
        if (res == null)
        {
            throw new UserException("分类不存在");
        }

        if (res.Count > 0)
        {
            throw new UserException("分类不为空，无法删除");
        }

        await _categoryRepository.DeleteOne(a => a.Id == Id);
    }

    public async Task<List<CategoryItem>> GetCategoriesByCache(CategoryPageRequest request, TimeSpan timeSpan)
    {
        var cache = await _easyCachingProvider.GetAsync<List<CategoryItem>>(CacheKeys.Categories);
        if (cache.HasValue)
        {
            return cache.Value;
        }

        var res = await GetCategories(request);
        await _easyCachingProvider.SetAsync(CacheKeys.Categories, res, timeSpan);
        return res;
    }
}
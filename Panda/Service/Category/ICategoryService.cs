using Panda.Entity.Requests;
using Panda.Entity.Responses;

namespace Panda.Services.Category;

public interface ICategoryService
{
    Task<List<CategoryItem>> GetCategories(CategoryPageRequest request);

    Task AddOrUpdate(CategoryAddOrUpdate request);

    Task Delete(int id);

    /// <summary>
    ///     获取分类从缓存
    /// </summary>
    /// <param name="request"></param>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    Task<List<CategoryItem>> GetCategoriesByCache(CategoryPageRequest request, TimeSpan timeSpan);


    Task<CategoryItem> GetCategoryById(int id);
}
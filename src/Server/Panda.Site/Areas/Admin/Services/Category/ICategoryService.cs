using Panda.Entity.Requests;
using Panda.Entity.Responses;

namespace Panda.Site.Areas.Admin.Services.Category;

public interface ICategoryService
{
    Task<List<CategoryItem>> GetCategories(CategoryPageRequest request);

    Task AddOrUpdate(CategoryAddOrUpdate request);

    Task Delete(int id);

    Task<CategoryItem> GetCategoryById(int id);
}
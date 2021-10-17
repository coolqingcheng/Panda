using Panda.Entity.Requests;
using Panda.Entity.Responses;

namespace Panda.Services.Category;

public interface ICategoryService
{
    Task<List<CategoryItem>> GetCategories(CategoryPageRequest request);

    Task AddOrUpdate(CategoryAddOrUpdate request);

    Task Delete(int Id);
}
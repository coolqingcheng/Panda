using Panda.Entity.Responses;

namespace Panda.Services.Category;

public interface ICategoryService
{
    Task<List<CategoryItem>> GetCategories();
}
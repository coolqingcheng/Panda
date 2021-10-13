using Microsoft.EntityFrameworkCore;
using Panda.Entity.Responses;
using Panda.Repository.Category;

namespace Panda.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryItem>> GetCategories()
    {
        var res = await _categoryRepository.Where(a => a.IsShow).Select(a => new CategoryItem()
        {
            CategoryName = a.categoryName,
            Id = a.Id,
            Pid = a.Pid
        }).ToListAsync();
        return res;
    }
}
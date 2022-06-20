using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Services.Category;
using Panda.Tools.Auth.Controllers;

namespace Panda.Site.Areas.Admin.Controllers;

public class CategoryController : AdminController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public Task<CategoryItem> Get(int id)
    {
        return _categoryService.GetCategoryById(id);
    }

    [HttpPost]
    public async Task AddOrUpdate(CategoryAddOrUpdate input)
    {
        await _categoryService.AddOrUpdate(input);
    }

    [HttpGet]
    public async Task<List<CategoryItem>> GetList([FromQuery] CategoryPageRequest request)
    {
        return await _categoryService.GetCategories(request);
    }

    [HttpDelete]
    public async Task Delete(int CategoryId)
    {
        await _categoryService.Delete(CategoryId);
    }
}
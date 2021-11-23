using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Services.Category;
using Panda.Admin.Models;

namespace Panda.Admin.Controllers;

public class CategoryController : AdminBaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task AddOrUpdate(CategoryAddOrUpdate input)
    {
         await _categoryService.AddOrUpdate(input);
    }

    [HttpGet]
    public async Task<List<CategoryItem>> GetList([FromQuery]CategoryPageRequest request)
    {
        return await _categoryService.GetCategories(request);
    }

    [HttpDelete]
    public async Task Delete(int CategoryId)
    {
         await _categoryService.Delete(CategoryId);
    }
}
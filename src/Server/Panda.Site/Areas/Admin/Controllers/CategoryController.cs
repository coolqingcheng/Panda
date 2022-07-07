using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Services.Category;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Controllers;

namespace Panda.Site.Areas.Admin.Controllers;

[PermissionGroup("博客-分类")]
public class CategoryController : AdminController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [Permission("查看")]
    [HttpGet]
    public Task<CategoryItem> Get(int id)
    {
        return _categoryService.GetCategoryById(id);
    }
    [Permission("添加和编辑")]
    [HttpPost]
    public async Task AddOrUpdate(CategoryAddOrUpdate input)
    {
        await _categoryService.AddOrUpdate(input);
    }

    [Permission("查看")]
    [HttpGet]
    public async Task<List<CategoryItem>> GetList([FromQuery] CategoryPageRequest request)
    {
        return await _categoryService.GetCategories(request);
    }
    [Permission("删除")]
    [HttpDelete]
    public async Task Delete(int CategoryId)
    {
        await _categoryService.Delete(CategoryId);
    }
}
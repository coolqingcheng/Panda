using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Services.Category;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Controllers;

namespace Panda.Site.Areas.Admin.Controllers;

[PermissionGroup("����-����")]
public class CategoryController : AdminController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [Permission("�鿴")]
    [HttpGet]
    public Task<CategoryItem> Get(int id)
    {
        return _categoryService.GetCategoryById(id);
    }
    [Permission("��Ӻͱ༭")]
    [HttpPost]
    public async Task AddOrUpdate(CategoryAddOrUpdate input)
    {
        await _categoryService.AddOrUpdate(input);
    }

    [Permission("�鿴")]
    [HttpGet]
    public async Task<List<CategoryItem>> GetList([FromQuery] CategoryPageRequest request)
    {
        return await _categoryService.GetCategories(request);
    }
    [Permission("ɾ��")]
    [HttpDelete]
    public async Task Delete(int CategoryId)
    {
        await _categoryService.Delete(CategoryId);
    }
}
using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Services.Category;
using Panda.Services.Posts;

namespace Panda.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    private readonly IPostService _postService;

    public CategoryController(
        ICategoryService categoryService,
        IPostService postService
    )
    {
        _categoryService = categoryService;
        _postService = postService;
    }

    // GET
    [HttpGet("/category/{id:int}")]
    [HttpGet("/category/{id:int}-{index:int}")]
    public async Task<IActionResult> Index(int id,int index = 1)
    {
        var list = await _postService.GetPostList(new PostRequest()
        {
            CategoryId = id,
            Index = index,
            Size = 10
        });
        var categoryItem = await _categoryService.GetCategoryById(id);
        ViewData["cateName"] = categoryItem.CategoryName;
        ViewData["res"] = list;
        return View();
    }
}
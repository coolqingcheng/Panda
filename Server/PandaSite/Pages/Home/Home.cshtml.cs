using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panda.Models.Dtos.Blogs;
using Panda.Models.Dtos.Blogs.Dto;
using Panda.Services.Blogs;

namespace PandaSite.Pages.Home;

public class HomeModel : PageModel
{
    private readonly PostService _postService;

    public List<PostItemModel> list = new();

    public HomeModel(PostService postService)
    {
        _postService = postService;
    }

    [BindProperty] public TestUser TestUser { get; set; }
    public string Name { get; set; }

    public int PageIndex { get; set; }

    public int Total { get; set; }

    public async Task OnGet(int pageIndex)
    {
        var res = await _postService.GetHomeList(new PostRequestModel
        {
            Index = pageIndex,
            PageSize = 10
        });
        list.AddRange(res.Data);
        Total = res.Total;
        PageIndex = pageIndex;
        ViewData["title"] = "主页";
    }
}

public class TestUser
{
    public string UserName { get; set; }
}
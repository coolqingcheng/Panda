using Microsoft.AspNetCore.Mvc.RazorPages;
using Panda.Models.Dtos.Blogs;
using Panda.Models.Dtos.Blogs.Dto;
using Panda.Services.Blogs;

namespace PandaSite.Pages.Category;

public class Category : PageModel
{
    public int PageIndex { get; set; }
    public int CateId { get; set; }

    public int PageSize { get; set; } = 10;

    public string? CateName { get; set; }


    readonly PostService _postService;

    readonly PostCateService _postCateService;

    public Category(PostService postService, PostCateService postCateService)
    {
        _postService = postService;
        _postCateService = postCateService;
    }
    public PageDto<PostItemModel> pageDto;
    public async Task OnGet(int cateid)
    {
        PageIndex = 1;
        CateId = cateid;
        pageDto = await _postService.GetHomeList(new PostRequestModel()
        {
            CateId = CateId,
            Index = PageIndex,
            PageSize = PageSize
        });
        CateName = await _postCateService.GetCateNameById(cateid);
    }
}
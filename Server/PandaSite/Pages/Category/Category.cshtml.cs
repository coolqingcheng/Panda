﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Panda.Models.Dtos.Blogs;
using Panda.Models.Dtos.Blogs.Dto;
using Panda.Services.Blogs;

namespace PandaSite.Pages.Category;

public class Category : PageModel
{
    private readonly PostCateService _postCateService;


    private readonly PostService _postService;

    public PageDto<PostItemModel> pageDto;

    public Category(PostService postService, PostCateService postCateService)
    {
        _postService = postService;
        _postCateService = postCateService;
    }

    public int PageIndex { get; set; }
    public int CateId { get; set; }

    public int PageSize { get; set; } = 10;

    public string? CateName { get; set; }

    public async Task OnGet(int cateid)
    {
        PageIndex = 1;
        CateId = cateid;
        pageDto = await _postService.GetHomeList(new PostRequestModel
        {
            CateId = CateId,
            Index = PageIndex,
            PageSize = PageSize,
            FilterPublish = true,
            PublishStatus = true
        });
        CateName = await _postCateService.GetCateNameById(cateid);
    }
}
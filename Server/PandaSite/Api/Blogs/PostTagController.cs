using Microsoft.AspNetCore.Mvc;
using PandaSite.Api.Blogs.Services;
using PandaSite.Api.Blogs.Services.Models;
using PandaSite.Models;

namespace PandaSite.Api.Blogs
{
    public class PostTagController : BaseAdminController
    {
        readonly PostTagService _postTagService;

        public PostTagController(PostTagService postTagService)
        {
            _postTagService = postTagService;
        }



        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageDto<TagDtoModel>> GetList([FromQuery]TagListRequest request)
        {
            return await _postTagService.GetList(request);
        }
    }
}

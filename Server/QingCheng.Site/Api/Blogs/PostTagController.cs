using Microsoft.AspNetCore.Mvc;
using QingCheng.Site.Api.Blogs.Services;
using QingCheng.Site.Api.Blogs.Services.Models;
using QingCheng.Tools.Controllers;
using QingCheng.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Api.Blogs
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

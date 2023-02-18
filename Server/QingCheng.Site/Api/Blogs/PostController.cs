using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QingCheng.Site.Api.Blogs.Services;
using QingCheng.Site.Api.Blogs.Services.Models;
using QingCheng.Site.Api.Config;
using QingCheng.Tools.Controllers;
using QingCheng.Site.Models;
using QingCheng.Site.Models.Blogs;
using QingCheng.Site.Models.Dto;

namespace QingCheng.Site.Api.Blogs
{

    /// <summary>
    /// 文章
    /// </summary>
    public class PostController : BaseAdminController
    {
        private readonly PostService _post;


        public PostController(PostService post )
        {
            _post = post;
        }

        [HttpGet]
        public async Task<PostEditRequest> Get(int Id)
        {
            return await _post.Get(Id);
        }

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task Top(int Id)
        {
            await _post.Top(Id);
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageDto<PostItemModel>> List([FromQuery] PostRequestModel model)
        {
            var res = await _post.GetHomeList(model);
            return res;
        }


        /// <summary>
        /// 新增和编辑文章
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Edit(PostEditRequest request)
        {
            await _post.Edit(request);
        }

        //删除
    }
}

using Microsoft.AspNetCore.Mvc;
using PandaSite.Api.Blogs.Services.Models;
using PandaSite.Models;
using PandaSite.Models.Blogs;
using PandaSite.Models.Blogs.Dto;
using PandaSite.Services.Blogs;

namespace PandaSite.Api.Blogs
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
        public async Task<PostDetailModel> Get(int Id)
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

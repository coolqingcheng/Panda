using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QingCheng.Site.Api.Blogs.Services;
using QingCheng.Site.Api.Blogs.Services.Models;
using QingCheng.Tools.Controllers;
using QingCheng.Site.Models;

namespace QingCheng.Site.Api.Blogs
{
    /// <summary>
    /// 分类
    /// </summary>
    public class CateController : BaseAdminController
    {
        private readonly PostCateService _postCateService;

        public CateController(PostCateService postCateService)
        {
            _postCateService = postCateService;
        }

        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageDto<CateDtoModel>> GetList([FromQuery] BasePageModel req)
        {
            return await _postCateService.GetList(req);
        }


        /// <summary>
        /// 添加和编辑分类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Edit(CateRequest request)
        {
            await _postCateService.Edit(request);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete(int Id)
        {
            await _postCateService.Delete(Id);
        }

    }
}

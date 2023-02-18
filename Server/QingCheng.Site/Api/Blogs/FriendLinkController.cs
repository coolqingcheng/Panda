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
    public class FriendLinkController : BaseAdminController
    {
        readonly FriendLinkService _friendLinkService;

        public FriendLinkController(FriendLinkService friendLinkService)
        {
            _friendLinkService = friendLinkService;
        }

        /// <summary>
        /// 友情链接列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageDto<FriendLinkModel>> GetList([FromQuery]FriendLinkRequestModel request)
        {
            return await _friendLinkService.GetList(request);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Edit(FriendLinkModel model)
        {
            await _friendLinkService.Edit(model);
        }
    }
}

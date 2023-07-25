﻿using Microsoft.AspNetCore.Mvc;
using PandaSite.Api.Blogs.Services;
using PandaSite.Api.Blogs.Services.Models;
using PandaSite.Models;

namespace PandaSite.Api.Blogs
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
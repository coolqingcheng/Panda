﻿using QingCheng.Site.Models;

namespace QingCheng.Site.Api.Services.Models
{
    public class RoleModels
    {
    }

    public class RoleListRequest : BasePageModel
    {

    }

    public class RoleListResponse
    {
        public Guid Id { get; set; }

        public string RoleName { get; set; }

        public DateTime CreateTime { get; set; }


        public int Count { get; set; }
    }
}
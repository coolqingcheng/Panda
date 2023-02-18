using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QingCheng.Site.Models;

namespace QingCheng.Site.Models.Dto;

public class PostRequestModel : BasePageModel
{
    public int? CateId { get; set; }

    public int? TagId { get; set; }
}

public class GetCateModel : BasePageModel
{

}

public class GetTagModel : BasePageModel
{

}

public class GetFriendLinkModel : BasePageModel
{

}
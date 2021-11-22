using Panda.Tools.Models;

namespace Panda.Entity.Requests;

public class FriendlyLinkRequest:BasePageRequest
{
    
}

public class AddFriendLinkRequest
{
    /// <summary>
    /// 站点名称
    /// </summary>
    public string Name { get; set; }

    public string Url { get; set; }
}
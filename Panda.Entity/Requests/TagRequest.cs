using Panda.Tools.Models;

namespace Panda.Entity.Requests;

public class TagRequest:BasePageRequest
{
    
}

public class TagAddRequest
{
    
    public string TagName { get; set; }
}
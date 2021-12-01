using Panda.Tools.Models;

namespace Panda.Entity.Requests;

public class TagRequest:BasePageRequest
{
    
}

public class TagAddRequest
{
    public int Id { get; set; }
    
    public string TagName { get; set; }
}
using Panda.Tools.Models;

namespace Panda.Entity.Requests;

public class CategoryRequest
{
}

public class CategoryPageRequest : BasePageRequest
{
    public string? CateName { get; set; }
}

public class CategoryAddOrUpdate
{
    public int Id { get; set; }

    public string CateName { get; set; }

    public int Pid { get; set; }
}
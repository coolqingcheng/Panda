using System.ComponentModel.DataAnnotations;
using Panda.Tools.Models;

namespace Panda.Entity.Requests;

public class CategoryRequest
{
}

public class CategoryPageRequest : BasePageRequest
{
    public string? CateName { get; set; }

    public bool? IsShow { get; set; }
}

public class CategoryAddOrUpdate
{
    public int Id { get; set; }

    [Required] public string CategoryName { get; set; }


    public string? CategoryDesc { get; set; }

    public int Pid { get; set; }

    public bool IsShow { get; set; }
}
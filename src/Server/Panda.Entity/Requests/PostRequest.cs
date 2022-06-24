using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Panda.Tools.Models;

namespace Panda.Entity.Requests;

public class PostAddOrUpdate
{
    public int Id { get; set; }

    [Required] public string Title { get; set; }

    /// <summary>
    ///     markdown语法
    /// </summary>
    public string MarkDown { get; set; }

    [Required] public List<int> Categories { get; set; }

    public string[]? Tags { get; set; }

    /// <summary>
    ///     封面图
    /// </summary>
    public string? Cover { get; set; }
}

public class AdminPostGetListRequest : BasePageRequest
{
    public string? KeyWord { get; set; }
}
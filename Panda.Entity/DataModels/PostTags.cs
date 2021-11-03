using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Panda.Entity.DataModels;

public class PostTags : PandaBaseTable
{
    [Required, StringLength(20)] public string TagName { get; set; }


    public int PostCount { get; set; }
}

[Keyless]
public class TagsRelation
{
    public Posts Posts { get; set; }

    public PostTags Type { get; set; }
}
using Microsoft.EntityFrameworkCore;

namespace Panda.Entity.DataModels;

[Keyless]
public class TagsRelation
{
    public Posts Posts { get; set; }

    public PostTags Type { get; set; }
}
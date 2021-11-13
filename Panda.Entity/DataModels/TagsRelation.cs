using Microsoft.EntityFrameworkCore;

namespace Panda.Entity.DataModels;

public class TagsRelation:PandaBaseTable
{
    public Posts Posts { get; set; }

    public PostTags Tags { get; set; }
}
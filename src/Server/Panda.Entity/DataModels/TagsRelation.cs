namespace Panda.Entity.DataModels;

public class TagsRelation : PandaBaseTable
{
    public virtual Posts Posts { get; set; }

    public virtual PostTags Tags { get; set; }
}
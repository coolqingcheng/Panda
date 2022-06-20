namespace Panda.Entity.DataModels;

public class PostsCategoryRelations : PandaBaseTable
{
    public virtual Posts Posts { get; set; }

    public virtual Categorys Categories { get; set; }
}
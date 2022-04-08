namespace Panda.Entity.DataModels;

public class PostsCategoryRelations : PandaBaseTable
{
    public Posts Posts { get; set; }

    public Categorys Categories { get; set; }
}
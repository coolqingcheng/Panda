using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace Panda.Entity.DataModels;

public class ArticleCategoryRelations:PandaBaseTable
{
    public Articles Articles { get; set; }

    public Categorys Categories { get; set; }
}
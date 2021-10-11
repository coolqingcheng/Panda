using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace Panda.Entity.DataModels;

[Index("article_id","ArticleId")]
[Index("category_id","CategoryId")]
public class ArticleCategoryRelations:PandaBaseTable
{
    public int ArticleId { get; set; }

    public int CategoryId { get; set; }
}
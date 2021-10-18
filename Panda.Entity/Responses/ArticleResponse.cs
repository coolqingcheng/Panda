namespace Panda.Entity.Responses;

public class ArticleResponse
{
    
}

public class AdminArticleItemResponse
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public List<AdminCategoryItem> CategoryItems { get; set; }
}

public class AdminCategoryItem
{
    public int Id { get; set; }

    public string CateName { get; set; }
}
using Panda.Entity.DataModels;

namespace Panda.Entity.Responses;

public class ArticleResponse
{
    
}

public class AdminPostItemResponse
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public DateTimeOffset AddTime { get; set; }
    
    public DateTimeOffset UpdateTime { get; set; }

    public string AccountName { get; set; }

    public PostStatus Status { get; set; }

    public List<AdminCategoryItem> CategoryItems { get; set; }


    public string CustomLink { get; set; }
}

public class AdminCategoryItem
{
    public int Id { get; set; }

    public string CateName { get; set; }
}
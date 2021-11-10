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

    public DateTime AddTime { get; set; }
    
    public DateTime UpdateTime { get; set; }

    public string AccountName { get; set; }

    public PostStatus Status { get; set; }

    public List<AdminCategoryItem> CategoryItems { get; set; }
}

public class AdminCategoryItem
{
    public int Id { get; set; }

    public string CateName { get; set; }
}
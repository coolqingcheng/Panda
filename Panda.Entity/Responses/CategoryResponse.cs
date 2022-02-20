namespace Panda.Entity.Responses;

public class CategoryResponse
{
    public List<CategoryItem> CategoryItems { get; set; }
}

public class CategoryItem
{
    public int Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string CateName { get; set; }

    /// <summary>
    /// 分类上级Id
    /// </summary>
    public int Pid { get; set; }

    /// <summary>
    /// 关联文章数量
    /// </summary>
    public int Count { get; set; }
}
namespace Panda.Models.Data.Blogs;

public class PostCates : BaseTableModel<int>
{
    public PostCates()
    {
        
    }

    public PostCates(string cateName)
    {
        CateName = cateName;
    }

    public string CateName { get; set; }

    public List<PostCateRelation> PostCateRelations { get; set; }
}

public class PostCateRelation : BaseTableModel<int>
{
    public PostCateRelation()
    {
        
    }
    public PostCateRelation(PostCates postCate, Posts posts)
    {
        PostCate = postCate;
        Post = posts;
    }

    public PostCates PostCate { get; set; }

    public Posts Post { get; set; }
}

using QingCheng.Tools.EFCore;

namespace QingCheng.Site.Data.Blogs;

public class PostTags : BaseTableModel<int>
{
    public PostTags() { }
    public PostTags(string tagName)
    {
        TagName = tagName;
    }

    public string TagName { get; set; }

    public List<PostTagRelation> TagRelation { get; set; }

}

public class PostTagRelation : BaseTableModel<int>
{
    public PostTagRelation() { }

    public PostTagRelation(Posts posts, PostTags postTags)
    {
        Post = posts;
        PostTags = postTags;
    }

    public Posts Post { get; set; }

    public PostTags PostTags { get; set; }
}
using QingCheng.Site.Data.Blogs;

namespace QingCheng.Site.Repositories.Blogs;

public class PostRepository : BaseRepository<Posts>
{
    public PostRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
using QingCheng.Site.Data.Blogs;

namespace QingCheng.Site.Repositories.Blogs;

public class PostCatesRepository : BaseRepository<PostCates>
{
    public PostCatesRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<PostCates>> GetCatesAsync(List<int> ids)
    {
        return await DbContext.Set<PostCates>().Where(a => ids.Contains(a.Id)).ToListAsync();
    }

    public async Task PostSetCateRelation(Posts post, List<PostCates> list)
    {
        await DbContext.Set<PostCateRelation>().Where(a => a.Post == post).ExecuteDeleteAsync();
        foreach (var item in list)
        {
            await DbContext.Set<PostCateRelation>()
                .AddAsync(new PostCateRelation()
                {
                    Post = post,PostCate = item
                });
        }

        await DbContext.SaveChangesAsync();
    }
}
using PandaSite.Data.Blogs;

namespace PandaSite.Repositories.Blogs;

public class TagRepository : BaseRepository<PostTags>
{
    public TagRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<PostTags>> GetTagsAsync(List<string> tags)
    {
        return await DbContext.Set<PostTags>().Where(a => tags.Contains(a.TagName)).ToListAsync();
    }

    public async Task PostSetTagRelationAsync(Posts post, List<PostTags> list)
    {
        await DbContext.Set<PostTagRelation>().Where(a => a.Post == post).ExecuteDeleteAsync();
        foreach (var item in list)
        {
            await DbContext.Set<PostTagRelation>()
                .AddAsync(new PostTagRelation(post, item));
        }

        await DbContext.SaveChangesAsync();
    }
}
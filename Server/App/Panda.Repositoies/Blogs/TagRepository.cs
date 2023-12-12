namespace Panda.Repositoies.Blogs;

public class TagRepository : BaseRepository<PostTags, int>
{
    public TagRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<PostTags>> GetTagsAsync(List<string> tags)
    {
        return await Ctx.Set<PostTags>().Where(a => tags.Contains(a.TagName)).ToListAsync();
    }

    public async Task PostSetTagRelationAsync(Posts post, List<PostTags> list)
    {
        await Ctx.Set<PostTagRelation>().Where(a => a.Post == post).ExecuteDeleteAsync();
        foreach (var item in list)
            await Ctx.Set<PostTagRelation>()
                .AddAsync(new PostTagRelation(post, item));

        await Ctx.SaveChangesAsync();
    }
}
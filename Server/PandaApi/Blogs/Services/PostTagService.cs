namespace PandaApi.Blogs.Services;

public class PostTagService
{
    private readonly PandaDbContext _context;

    public PostTagService(PandaDbContext context)
    {
        _context = context;
    }

    public async Task<PageDto<TagDtoModel>> GetList(TagListRequest request)
    {
        var query = _context.PostTags.AsQueryable();
        var list = await query.Skip(request.Skip).Take(request.PageSize)
            .Include(a => a.TagRelation)
            .Select(a => new TagDtoModel
            {
                Id = a.Id,
                TagName = a.TagName,
                Count = a.TagRelation.Count()
            }).ToListAsync();
        return new PageDto<TagDtoModel>(await query.CountAsync(), list);
    }
}
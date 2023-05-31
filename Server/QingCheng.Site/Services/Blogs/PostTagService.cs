using QingCheng.Site.Data.Blogs;
using QingCheng.Site.Models;
using QingCheng.Site.Models.Blogs;
using QingCheng.Site.Models.Dto;

namespace QingCheng.Site.Services.Blogs;

public class PostTagService
{
    private readonly DbContext _context;

    public PostTagService(DbContext context)
    {
        _context = context;
    }

    public async Task<PageDto<TagCountItem>> GetList(GetTagModel model)
    {
        var list = await _context.Set<PostTags>().AsNoTracking().Skip(model.Skip).Take(model.PageSize)
            .Select(a => new TagCountItem()
            {
                Id = a.Id,
                TagName = a.TagName,
                Count = _context.Set<PostTagRelation>().Count(x => x.PostTags == a)
            }).ToListAsync();
        return new PageDto<TagCountItem>(await _context.Set<PostTags>().CountAsync(), list);
    }
}
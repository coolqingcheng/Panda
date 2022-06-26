using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Tools.Extensions;

namespace Panda.Site.Areas.Admin.Services.PostTag;

public class PostTagService : IPostTagService
{
    private readonly DbContext _dbContext;

    public PostTagService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TagResponse>> SearchTag(string searchValue)
    {
        var list = await _dbContext.Set<PostTags>().Where(a => a.TagName.Contains(searchValue))
            .OrderByDescending(a => a.PostCount).Take(10).Select(a => new TagResponse
            {
                TagName = a.TagName,
                Id = a.Id
            }).ToListAsync();
        return list;
    }

    public async Task<PageDto<TagResponse>> GetList(TagRequest request)
    {
        var query = _dbContext.Set<PostTags>().AsQueryable();
        var list = await query.Page(request).Select(a => new TagResponse
        {
            TagName = a.TagName, Count = _dbContext.Set<TagsRelation>().Where((b=>b.Tags==a)).Count()
            , Id = a.Id
        }).ToListAsync();
        return new PageDto<TagResponse>
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }

    public async Task AddRelation(Entity.DataModels.Posts post, PostTags tag)
    {
        await _dbContext.Set<TagsRelation>().AddAsync(new TagsRelation
        {
            Tags = tag, Posts = post
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveRelation(Entity.DataModels.Posts post, PostTags tag)
    {
        var relations = await _dbContext.Set<TagsRelation>().Where(a => a.Posts == post && a.Tags == tag).ToListAsync();
        _dbContext.RemoveRange(relations);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PostTags> GetId(int Id)
    {
        var item = await _dbContext.Set<PostTags>().Where(a => a.Id == Id).FirstOrDefaultAsync();
        item.IsNullThrow("标签Id为空！");
        return item!;
    }

    public async Task AddOrUpdate(TagAddRequest request)
    {
        if (request.Id != 0)
        {
            var tag = await _dbContext.Set<PostTags>().Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (tag != null) tag.TagName = request.TagName;
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            var any = await _dbContext.Set<PostTags>().Where(a => a.TagName == request.TagName).AnyAsync();
            if (any == false)
                await _dbContext.Set<PostTags>().AddAsync(new PostTags
                {
                    TagName = request.TagName
                });
            await _dbContext.SaveChangesAsync();
        }
    }
}
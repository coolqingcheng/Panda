using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Repository.Tags;
using Panda.Tools.Extensions;

namespace Panda.Services.Tags;

public interface IPostTagService
{
    Task<List<TagResponse>> SearchTag(string searchValue);

    Task<PageDto<TagResponse>> GetList(TagRequest request);


    Task AddRelation(Entity.DataModels.Posts post, PostTags tag);
    Task RemoveRelation(Entity.DataModels.Posts post, PostTags tag);


    Task<PostTags> GetId(int Id);


    Task AddOrUpdate(TagAddRequest request);
}

public class PostTagService : IPostTagService
{
    private readonly TagRelationRepository _relationRepository;
    private readonly PostTagsRepository _tagsRepository;

    public PostTagService(PostTagsRepository tagsRepository, TagRelationRepository relationRepository)
    {
        _tagsRepository = tagsRepository;
        _relationRepository = relationRepository;
    }

    public async Task<List<TagResponse>> SearchTag(string searchValue)
    {
        var list = await _tagsRepository.Where(a => a.TagName.Contains(searchValue))
            .OrderByDescending(a => a.PostCount).Take(10).Select(a => new TagResponse
            {
                TagName = a.TagName,
                Id = a.Id
            }).ToListAsync();
        return list;
    }

    public async Task<PageDto<TagResponse>> GetList(TagRequest request)
    {
        var query = _tagsRepository.Queryable();
        var list = await query.Page(request).Select(a => new TagResponse
        {
            TagName = a.TagName, Count = a.PostCount, Id = a.Id
        }).ToListAsync();
        return new PageDto<TagResponse>
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }

    public async Task AddRelation(Entity.DataModels.Posts post, PostTags tag)
    {
        await _relationRepository.AddAsync(new TagsRelation
        {
            Tags = tag, Posts = post
        });
    }

    public async Task RemoveRelation(Entity.DataModels.Posts post, PostTags tag)
    {
        await _relationRepository.DeleteWhereAsync(a => a.Posts == post && a.Tags == tag);
    }

    public async Task<PostTags> GetId(int Id)
    {
        var item = await _tagsRepository.Where(a => a.Id == Id).FirstOrDefaultAsync();
        item.IsNullThrow("标签Id为空！");
        return item!;
    }

    public async Task AddOrUpdate(TagAddRequest request)
    {
        if (request.Id != 0)
        {
            var tag = await _tagsRepository.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (tag != null) tag.TagName = request.TagName;
            await _tagsRepository.SaveAsync();
        }
        else
        {
            var any = await _tagsRepository.Where(a => a.TagName == request.TagName).AnyAsync();
            if (any == false)
                await _tagsRepository.AddAsync(new PostTags
                {
                    TagName = request.TagName
                });
        }
    }
}
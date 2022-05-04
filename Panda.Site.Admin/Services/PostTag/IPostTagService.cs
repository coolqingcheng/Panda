using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;

namespace Panda.Site.Admin.Services.PostTag;

public interface IPostTagService
{
    Task<List<TagResponse>> SearchTag(string searchValue);

    Task<PageDto<TagResponse>> GetList(TagRequest request);


    Task AddRelation(Entity.DataModels.Posts post, PostTags tag);
    Task RemoveRelation(Entity.DataModels.Posts post, PostTags tag);


    Task<PostTags> GetId(int Id);


    Task AddOrUpdate(TagAddRequest request);
}
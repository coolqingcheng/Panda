using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Tags;

public class TagRelationRepository:PandaRepository<TagsRelation>
{
    public TagRelationRepository(PandaContext context) : base(context)
    {
        
    }

    public async Task PostDeleteRelation(Posts post)
    {
        await DeleteRange(a => a.Posts == post);
    }
}
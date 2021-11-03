using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Tags;

public class PostTagsRepository:PandaRepository<PostTags>
{
    public PostTagsRepository(PandaContext context) : base(context)
    {
    }
}
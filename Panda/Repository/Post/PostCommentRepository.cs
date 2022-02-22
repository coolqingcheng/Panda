using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Post;

public class PostCommentRepository : PandaRepository<PostComments>
{
    public PostCommentRepository(PandaContext context) : base(context)
    {
    }
}
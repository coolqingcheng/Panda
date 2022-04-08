using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.FriendlyLink;

public class FriendLinkRepository : PandaRepository<FriendlyLinks>
{
    public FriendLinkRepository(PandaContext context) : base(context)
    {
    }
}
using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Notify;

public class NoticeRepository : PandaRepository<Notices>
{
    public NoticeRepository(PandaContext context) : base(context)
    {
    }
}
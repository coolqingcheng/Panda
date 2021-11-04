using System.Linq.Expressions;
using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.DicData;

public class DicDataRepository : PandaRepository<DicDatas>
{
    public DicDataRepository(PandaContext context) : base(context)
    {
    }

    public IQueryable<DicDatas> WhereGroupName(string groupName)
    {
        var item = Where(a => a.DicKey == groupName && a.Pid == 0);
        return item;
    }
    public IQueryable<DicDatas> WhereItemsByGroupName(string groupName)
    {
        var item = Where(a => a.DicKey == groupName && a.Pid == 0);
        return item;
    }
}
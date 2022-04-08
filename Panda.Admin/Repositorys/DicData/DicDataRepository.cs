using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Models;
using Panda.Tools.Auth.Repositorys;

namespace Panda.Admin.Repositorys.DicData;

public class DicDataRepository : BaseRepository
{
    public DicDataRepository(DbContext context) : base(context)
    {
    }

    public IQueryable<DicDatas> WhereGroupName(string groupName)
    {
        var item = _context.Set<DicDatas>().Where(a => a.DicKey == groupName && a.Pid == 0);
        return item;
    }

    public async Task<List<DicDatas>> WhereItemsByGroupName(string groupName)
    {
        var groupItem = await _context.Set<DicDatas>().Where(a => a.DicKey == groupName && a.Pid == 0)
            .FirstOrDefaultAsync();
        if (groupItem == null) return new List<DicDatas>();

        return await _context.Set<DicDatas>().Where(a => a.Pid == groupItem.Id).ToListAsync();
    }
}
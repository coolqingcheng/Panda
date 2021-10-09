using System.Linq.Expressions;

namespace Panda.Repository;
using FreeSql;

public class PandaRepository<T>:BaseRepository<T> where T:class
{
    public PandaRepository(IFreeSql freeSql) 
        : base(freeSql, null, null)
    {
    }
}
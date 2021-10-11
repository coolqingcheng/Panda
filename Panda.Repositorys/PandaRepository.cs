using System.Linq.Expressions;

namespace Panda.Repository;
using FreeSql;

public class PandaRepository<T>:BaseRepository<T> where T:class
{
    protected readonly IFreeSql _freeSql;

    protected PandaRepository(IFreeSql freeSql) 
        : base(freeSql, null, null)
    {
        _freeSql = freeSql;
    }
}
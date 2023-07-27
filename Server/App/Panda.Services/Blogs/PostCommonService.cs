namespace Panda.Services.Blogs;

[IgnoreInject]
public abstract class PostCommonService
{
    private readonly DbContext _context;

    protected PostCommonService(DbContext context)
    {
        _context = context;
    }
}
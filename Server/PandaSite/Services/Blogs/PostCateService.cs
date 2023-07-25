using Microsoft.Extensions.Caching.Distributed;
using PandaSite.Data.Blogs;
using PandaSite.Models.Blogs;

namespace PandaSite.Services.Blogs;

public class PostCateService
{
    private readonly DbContext _context;

    readonly IDistributedCache _cache;

    public PostCateService(DbContext context, IDistributedCache client)
    {
        _context = context;
        _cache = client;
    }

    public async Task<List<CateCountItem>> GetCateList(int top)
    {
        var res = await _cache.GetModelAsync($"{nameof(PostCateService)}:list", async () =>
         {
             var list = await _context.Set<PostCates>().AsNoTracking().Take(top).Select(a => new CateCountItem()
             {
                 Id = a.Id,
                 CateName = a.CateName,
                 Count = _context.Set<PostCateRelation>().Count(x => x.PostCate == a)
             }).ToListAsync();
             return list;
         }, _cache.GetOption(TimeSpan.FromMinutes(5)));
        return res ?? new List<CateCountItem>();
    }

    public async Task<string?> GetCateNameById(int Id)
    {
        return await _context.Set<PostCates>().AsNoTracking().Where(a => a.Id == Id).Select(a => a.CateName).FirstOrDefaultAsync();
    }
}
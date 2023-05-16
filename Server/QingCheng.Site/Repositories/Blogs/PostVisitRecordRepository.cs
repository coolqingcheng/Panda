using QingCheng.Site.Data.Blogs;

namespace QingCheng.Site.Repositories.Blogs;

public class PostVisitRecordRepository : BaseRepository<PostVisitRecord>
{
    public PostVisitRecordRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task VisitAsync(Posts post, string ip, string? userAgent, string? uid)
    {
        await AddAsync(new PostVisitRecord()
        {
            Post = post!,
            IP = ip,
            UA = userAgent ?? "",
            UId = uid ?? ""
        });

        var record = await DbContext.Set<PostVisitRecord>().Where(a => a.UId == uid).FirstOrDefaultAsync();
        if (record == null)
        {
            post!.ReadCount += 1;
        }
        await DbContext.SaveChangesAsync();
    }
}
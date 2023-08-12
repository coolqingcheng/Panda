namespace Panda.Repositoies.Blogs;

public class PostVisitRecordRepository : BaseRepository<PostVisitRecord,int>
{
    public PostVisitRecordRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task VisitAsync(Posts post, string ip, string? userAgent, string? uid)
    {
        await AddAsync(new PostVisitRecord
        {
            Post = post!,
            IP = ip,
            UA = userAgent ?? "",
            UId = uid ?? ""
        });

        var record = await Ctx.Set<PostVisitRecord>().Where(a => a.UId == uid).FirstOrDefaultAsync();
        if (record == null) post!.ReadCount += 1;
        await Ctx.SaveChangesAsync();
    }
}
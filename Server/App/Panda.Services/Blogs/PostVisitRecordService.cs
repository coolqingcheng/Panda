using Panda.Models.Dtos.DashBoard;
using Panda.Repositoies.Blogs;

namespace Panda.Services.Blogs;

/// <summary>
/// 文章访问记录
/// </summary>
public class PostVisitRecordService
{
    private readonly PostVisitRecordRepository _postVisitRecord;

    public PostVisitRecordService(PostVisitRecordRepository postVisitRecord)
    {
        _postVisitRecord = postVisitRecord;
    }

    public async Task<PageDto<VisitLogModel>> GetVisitList(int pageIndex, int pageSize)
    {
        var res = await _postVisitRecord.GetListAsync(a => true, pageIndex, pageSize);
        var list = res.Data.Select(a => new VisitLogModel()
        {
        }).ToList();
        return new PageDto<VisitLogModel>(res.Total, list);
    }
}
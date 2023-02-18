using Microsoft.EntityFrameworkCore;
using QingCheng.Site.Services.Blogs;
using QingCheng.Site.Data;
using QingCheng.Tools.Helper;
using QingCheng.Site.Models.Blogs;
using QingCheng.Site.Models;
using QingCheng.Site.Models.Dto;
using QingCheng.Site.Data.Blogs;

namespace QingCheng.Site.Services;

public class PostService : PostCommonService
{
    private readonly DbContext _context;

    public PostService(DbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PostDetailModel?> Get(int Id)
    {
        var item = await _context.Set<Posts>().AsNoTracking()
            .Include(a => a.CateRelations)
            .Include(a => a.TagRelations)
            .Where(a => a.Id == Id).Select(a => new PostDetailModel()
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Cates = a.CateRelations.Select(b => new PostCateModel() { Id = b.PostCate.Id, CateName = b.PostCate.CateName })
            .ToList(),
                Tags = a.TagRelations.Select(b => new PostTagModel()
                {
                    Id = b.PostTags.Id,
                    TagName = b.PostTags.TagName
                }).ToList(),
                CreateTime = a.CreateTime,
                UpdateTime = a.UpdateTime,
                ReadCount = a.ReadCount
            }).FirstOrDefaultAsync();
        return item;
    }
    /// <summary>
    /// 访问
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="ip"></param>
    /// <param name="userAgent"></param>
    /// <param name="uid"></param>
    /// <returns></returns>
    public async Task Visit(int postId, string ip, string? userAgent, string? uid)
    {
        var post = await _context.Set<Posts>().FirstOrDefaultAsync(a => a.Id == postId);
        await _context.Set<PostVisitRecord>().AddAsync(new PostVisitRecord()
        {
            Post = post!,
            IP = ip,
            UA = userAgent ?? "",
            UId = uid ?? ""
        });

        var record = await _context.Set<PostVisitRecord>().Where(a => a.UId == uid).FirstOrDefaultAsync();
        if (record == null)
        {
            post!.ReadCount += 1;
        }
        await _context.SaveChangesAsync();
    }

}
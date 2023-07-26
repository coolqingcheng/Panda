
using Panda.Models.Dtos.Blogs.Posts;
using PandaTools.Helper;

namespace Panda.Repositoies.Blogs;

public class PostRepository : BaseRepository<Posts>
{
    public PostRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PostDetailModel?> GetById(int Id)
    {
        var item = await DbContext.Set<Posts>().AsNoTracking()
            .Include(a => a.CateRelations)
            .Include(a => a.TagRelations)
            .Where(a => a.Id == Id).Select(a => new PostDetailModel()
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Cates = a.CateRelations.Select(b => new PostCateModel()
                        { Id = b.PostCate.Id, CateName = b.PostCate.CateName })
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

    public async Task<PageDto<PostItemModel>> GetHomeList(PostRequestModel request)
    {
        var query = DbContext.Set<Posts>().AsQueryable();

        var count = await query.CountAsync();
        var list = await query
            .Include(a => a.TagRelations)
            .Include(a => a.PostComments)
            .Include(a => a.CateRelations)
            .WhereIf(request.TagId.HasValue, a => a.TagRelations.Any(x => x.PostTags.Id == request.TagId))
            .WhereIf(request.CateId.HasValue, a => a.CateRelations.Any(x => x.PostCate.Id == request.CateId))
            .Skip(request.Skip).Take(request.PageSize)
            .OrderByDescending(a => a.CreateTime)
            .Select(a => new PostItemModel()
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                LastUpdateTime = a.UpdateTime,
                Snippet = a.Snippet,
                CateItems = a.CateRelations.Select(x => new CateItem()
                {
                    Id = x.PostCate.Id,
                    CateName = x.PostCate.CateName
                }).ToList(),
                TagItems = a.TagRelations.Select(x => new TagItem()
                {
                    Id = x.PostTags.Id,
                    TagName = x.PostTags.TagName
                }).ToList(),
                ReadCount = a.ReadCount,
                CommentCount = a.PostComments.Count(),
                IsTop = a.IsTop,
                IsPublish = a.IsPublish,
                Thumb = a.Thumb
            }).ToListAsync();
        return new PageDto<PostItemModel>(count, list);
    }


    public async Task<PostEditRequest> Get(int Id)
    {
        var item = await DbContext.Set<Posts>().Include(a => a.CateRelations).Where(a => a.Id == Id).Select(a =>
            new PostEditRequest()
            {
                Id = a.Id,
                Cates = a.CateRelations.Select(x => x.PostCate.Id).ToList(),
                Title = a.Title,
                Content = a.Content,
                Snippet = a.Snippet,
                Thumb = a.Thumb,
                IsPublish = a.IsPublish,
                Tags = a.TagRelations.Select(x => x.PostTags.TagName).ToList(),
            }).FirstOrDefaultAsync();
        if (item == null)
        {
            throw new UserException("未找到文章");
        }

        item.TagListToStr();
        return item;
    }

    


    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task Delete(int Id)
    {
        var post = await FindByIdAsync(Id);
        if (post != null)
        {
            await base.DeleteAsync(post);
        }
    }

    /// <summary>
    /// 置顶
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task Top(int Id)
    {
        var item = await DbContext.Set<Posts>().FirstOrDefaultAsync(a => a.Id == Id);
        if (item != null)
        {
            item.IsTop = !item.IsTop;
            await UpdateAsync(item);
        }
    }
}
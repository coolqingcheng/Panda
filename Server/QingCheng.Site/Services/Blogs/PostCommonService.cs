using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QingCheng.Site.Data.Blogs;
using QingCheng.Tools;
using QingCheng.Tools.Helper;
using QingCheng.Site.Models;
using QingCheng.Site.Models.Blogs;
using QingCheng.Site.Models.Dto;

namespace QingCheng.Site.Services.Blogs
{
    [IgnoreInject]
    public abstract class PostCommonService
    {
        readonly DbContext _context;

        protected PostCommonService(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<PageDto<PostItemModel>> GetHomeList(PostRequestModel request)
        {
            var query = _context.Set<Posts>().AsQueryable();

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
    }
}

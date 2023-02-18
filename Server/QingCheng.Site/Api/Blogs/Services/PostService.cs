using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QingCheng.Site.Api.Blogs.Services.Models;
using QingCheng.Site.Services.Blogs;
using QingCheng.Site.Data.Blogs;
using QingCheng.Site.Data.Blogs.Extensions;

namespace QingCheng.Site.Api.Blogs.Services
{
    public class PostService : PostCommonService
    {
        readonly QingChengContext _context;

        public PostService(QingChengContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PostEditRequest> Get(int Id)
        {
            var item = await _context.Posts.Include(a => a.CateRelations).Where(a => a.Id == Id).Select(a => new PostEditRequest()
            {
                Id = a.Id,
                Cates = a.CateRelations.Select(a => a.PostCate.Id).ToList(),
                Title = a.Title,
                Content = a.Content,
                Snippet = a.Snippet,
                Thumb = a.Thumb,
                IsPublish = a.IsPublish,
                Tags = a.TagRelations.Select(a => a.PostTags.TagName).ToList(),

            }).FirstOrDefaultAsync();
            if (item == null)
            {
                throw new UserException("未找到文章");
            }
            item.TagListToStr();
            return item;
        }

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task Edit(PostEditRequest request)
        {
            request.TagStringToList();
            var tran = await _context.Database.BeginTransactionAsync();
            Posts post;
            var tagList = await _context.GetTagsAsync(request.Tags);
            var cateList = await _context.GetCatesAsync(request.Cates);
            if (request.Id > 0)
            {
                post = await _context.Posts.FirstAsync(a => a.Id == request.Id);
                request.Adapt(post);
            }
            else
            {
                post = request.Adapt<Posts>();
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
            }

            await _context.PostSetTagRelationAsync(post, tagList);

            await _context.PostSetCateRelation(post, cateList);

            await _context.SaveChangesAsync();

            await tran.CommitAsync();
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task Delete(int Id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(a => a.Id == Id);
            if (post!=null)
            {
                _context.Remove<Posts>(post);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task Top(int Id)
        {
            var item = await _context.Posts.FirstOrDefaultAsync(a => a.Id == Id);
            if (item != null)
            {
                item.IsTop = !item.IsTop;
                await _context.SaveChangesAsync();
            }
        }
    }
}
